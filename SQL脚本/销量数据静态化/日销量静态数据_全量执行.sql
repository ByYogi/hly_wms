
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
)
SELECT
	* INTO #childArea
FROM
	ChildAreaCTE 
OPTION (MAXRECURSION 2); --只查到2级子仓库，如有3级子仓库就报错，防止无限递归。业务逻辑也只允许最大2级子仓（注：根仓库是0级）
CREATE UNIQUE INDEX IX_#childArea
ON #childArea (HouseID,AreaID)
INCLUDE(RootArea);


PRINT('------------ 清除所有静态数据 ------------')
TRUNCATE TABLE Tbl_Cargo_DailySaleStatic

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
WHERE a.ThrowGood != 25 --非退仓单
    AND a.OrderModel = 0  --订单类型为客户单，非退货单
    AND c.SpecsType != 5  --非次日达
    AND ISNULL(c.ProductCode, '') <> '' 
	AND CAST(a.CreateDate AS DATE) <> CAST(GETDATE() AS DATE)
GROUP BY CAST(a.CreateDate AS DATE), c.ProductCode, c.TypeID, c.HouseID, ca.RootArea
ORDER BY CAST(a.CreateDate AS DATE), c.ProductCode, c.TypeID, c.HouseID, ca.RootArea

