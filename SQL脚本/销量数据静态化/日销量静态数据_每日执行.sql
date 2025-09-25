
PRINT('------------ 区域仓库 ------------');
WITH ChildAreaCTE AS (
	SELECT
		HouseID,
		AreaID AS RootArea,
		ParentID AS ParentArea,
		AreaID AS AreaID,
		Name AS RootName,
		CAST('' AS varchar(50)) AS ParentName,
		Name AS AreaName,
		1 AS Level
	FROM
		Tbl_Cargo_Area a
	WHERE (1=1)
		AND ParentID = 0
		AND a.IsShowStock = 0 AND a.HouseID IN (1,3,9,45,65,82,84,93,96,97,98,99,100,101,102,105,106,107,108,111,109,110,112,113,114,115,116,117,118,119,121,120,122,123,124,125,126,127,13,14,15,10,23,24,25,27,128,30,64,32,131,129,130,29,132,135,134,133,94,90,91,89,88,87,86,83,81,80,79,78,77,76,75,73,74,72,71,70,69,68,67,63,60,58,59,55,57,56,49,50,53,48,47,46,44,43,33,31,136,137,138)
	UNION ALL
	
	SELECT
		c.HouseID,
		c.RootArea,
		a.ParentID AS ParentArea,
		a.AreaID,
		c.RootName,
		c.AreaName,
		a.Name AS AreaName,
		c.Level + 1
	FROM
		Tbl_Cargo_Area a
		INNER JOIN ChildAreaCTE c ON a.ParentID = c.AreaID
    WHERE (1=1)
		AND a.IsShowStock = 0 AND a.HouseID IN (1,3,9,45,65,82,84,93,96,97,98,99,100,101,102,105,106,107,108,111,109,110,112,113,114,115,116,117,118,119,121,120,122,123,124,125,126,127,13,14,15,10,23,24,25,27,128,30,64,32,131,129,130,29,132,135,134,133,94,90,91,89,88,87,86,83,81,80,79,78,77,76,75,73,74,72,71,70,69,68,67,63,60,58,59,55,57,56,49,50,53,48,47,46,44,43,33,31,136,137,138)
)
SELECT
	* INTO #childArea
FROM
	ChildAreaCTE 
OPTION (MAXRECURSION 2); --只查到2级子仓库，如有3级子仓库就报错，防止无限递归。业务逻辑也只允许最大2级子仓（注：根仓库是0级）
CREATE UNIQUE INDEX IX_#childArea
ON #childArea (HouseID,AreaID)
INCLUDE(RootArea);

DELETE Tbl_Cargo_DailySaleStatic WHERE SalesDate = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE)

PRINT('------------ 获取产品每日销量 ------------')
INSERT INTO Tbl_Cargo_DailySaleStatic
(
    SalesDate,
    ProductCode,
    TypeID,
    HouseID,
    AreaID,
    Piece,
    WXPiece,
    LastUpdateTime
)
SELECT
	CAST(a.CreateDate AS DATE) SalesDate,
    c.ProductCode,
    c.TypeID,
    c.HouseID,
	ca.RootArea AreaID,
    SUM(b.Piece) AS Piece,
    SUM(CASE WHEN a.OrderType = 4 THEN b.Piece ELSE 0 END) AS WXPiece,
	GETDATE() LastUpdateTime
FROM Tbl_Cargo_OrderGoods AS b
INNER JOIN Tbl_Cargo_Order AS a ON a.OrderNo = b.OrderNo
INNER JOIN Tbl_Cargo_Product AS c ON b.ProductID = c.ProductID
INNER JOIN #childArea ca ON b.HouseID = ca.HouseID AND b.AreaID = ca.AreaID
WHERE a.ThrowGood != 25
    AND a.OrderModel = 0 
    AND ISNULL(c.ProductCode, '') <> ''
    AND c.SpecsType != 5 
	AND CAST(a.CreateDate AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE)
GROUP BY CAST(a.CreateDate AS DATE), c.ProductCode, c.TypeID, c.HouseID, ca.RootArea
ORDER BY CAST(a.CreateDate AS DATE), c.ProductCode, c.TypeID, c.HouseID, ca.RootArea

