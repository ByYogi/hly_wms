
PRINT('------------ 区域仓库 ------------');
--WITH ChildAreaCTE AS (
--	SELECT
--		HouseID,
--		AreaID AS RootArea,
--		ParentID AS ParentArea,
--		AreaID AS AreaID,
--		Name AS RootName,
--		CAST('' AS varchar(50)) AS ParentName,
--		Name AS AreaName,
--		1 AS Level
--	FROM
--		Tbl_Cargo_Area a
--	WHERE (1=1)
--		AND ParentID = 0
--	UNION ALL
	
--	SELECT
--		c.HouseID,
--		c.RootArea,
--		a.ParentID AS ParentArea,
--		a.AreaID,
--		c.RootName,
--		c.AreaName,
--		a.Name AS AreaName,
--		c.Level + 1
--	FROM
--		Tbl_Cargo_Area a
--		INNER JOIN ChildAreaCTE c ON a.ParentID = c.AreaID
--    WHERE (1=1)
--)
--SELECT
--	* INTO #childArea
--FROM
--	ChildAreaCTE 
--OPTION (MAXRECURSION 2); --只查到2级子仓库，如有3级子仓库就报错，防止无限递归。业务逻辑也只允许最大2级子仓（注：根仓库是0级）
--CREATE UNIQUE INDEX IX_#childArea
--ON #childArea (HouseID,AreaID)
--INCLUDE(RootArea);


-- 产品
WITH 
containerGoods as (
	SELECT
		b.ProductID,
		ca.RootArea AreaID,
		SUM(a.Piece) AS Piece
	FROM Tbl_Cargo_ContainerGoods AS a
	INNER JOIN Tbl_Cargo_Product AS b ON a.ProductID = b.ProductID
	INNER JOIN Tbl_Cargo_Container AS d ON a.ContainerID = d.ContainerID
	INNER JOIN Tbl_Cargo_ProductType c ON a.TypeID = c.TypeID
	INNER JOIN #childArea ca ON d.AreaID = ca.AreaID AND b.HouseID = ca.HouseID
	WHERE b.SpecsType != 5
	GROUP BY b.ProductID, ca.RootArea
),
pCTE as (
SELECT 
	DISTINCT
	p.ProductID,
	p.ProductName,
	p.ProductCode,
	p.GoodsCode,
	p.Specs,
	p.Figure,
	P.LoadIndex,
	p.SpeedLevel,
	pt.TypeID,
	pt.TypeName,
	pt.ParentID,
	pt2.TypeName ParentName,
    h.HouseID,
	h.Name HouseName,
	h.ParentID HouseParentID,
	h.ParentName HouseParentName,
	cg.Piece InPiece,
	ca.RootArea AreaID
FROM
	Tbl_Cargo_Product p
	INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
	INNER JOIN #childArea ca ON ca.AreaID = 3677
	INNER JOIN containerGoods cg ON p.ProductID = cg.ProductID AND cg.AreaID = ca.RootArea
	LEFT JOIN Tbl_Cargo_ProductType pt2 ON pt.ParentID = pt2.TypeID
	LEFT JOIN Tbl_Cargo_House h ON p.HouseID = h.HouseID
WHERE
	ISNULL(p.ProductCode, '') <> '' AND p.ProductID IN (943096, 526076, 490778)
)
,
--在途
iti AS (
SELECT ProductCode, HouseID, SUM(Piece) Piece FROM (
-- 移库单
SELECT
    p.ProductCode,
    mo2.NewHouseID AS HouseID,
    SUM(mo.Piece - mo.NewPiece) AS Piece
FROM Tbl_Cargo_MoveOrderGood AS mo
INNER JOIN Tbl_Cargo_MoveOrder AS mo2 ON mo2.MoveNo = mo.MoveNo
INNER JOIN Tbl_Cargo_Product AS p ON mo.ProductID = p.ProductID
INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
INNER JOIN #childArea ca ON mo2.NewAreaID = ca.AreaID AND mo2.NewHouseID = ca.HouseID
WHERE mo2.MoveStatus <> 2
    AND ISNULL(p.ProductCode, '') <> ''
    AND mo.Piece - mo.NewPiece > 0
GROUP BY p.ProductCode, mo2.NewHouseID
UNION ALL

SELECT
    p.ProductCode,
    p.HouseID,
	SUM(ReplyNumber - fo.InPiece) Piece
FROM
	Tbl_Cargo_FactoryOrder fo
    INNER JOIN (SELECT DISTINCT ProductCode, TypeID, HouseID FROM Tbl_Cargo_Product) AS p ON fo.ProductCode = p.ProductCode AND fo.HouseID = p.HouseID --无法使用ProductID匹配，用ProductID匹配会匹配不到数据；产品表有异常重复数据，需要去重
    INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
WHERE
	(1 = 1)
	AND (fo.InCargoStatus = 0 OR fo.InCargoStatus = 2)
    AND fo.OrderType <> 2 --筛除退货单
    AND ISNULL(p.ProductCode, '') <> '' 
GROUP BY p.ProductCode, p.TypeID, p.HouseID
) a 
WHERE (1=1)
GROUP BY ProductCode, HouseID
)
--select * from iti

--主查询
SELECT
	ss.SID,
    ss.MinStockDay,
    ss.MaxStockDay,
	ss.MinStock,
	ss.MaxStock,
	ISNULL(mss.WeightedMonthSale, 0) WeightedMonthSale,
	ISNULL(iti.Piece, 0) InTransitPiece,
	0 RestockingPiece,
	(ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0)) RealPiece,
	ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0)) Piece,
	p.*
FROM
	Tbl_Cargo_SafeStock ss
	INNER JOIN pCTE p ON p.ProductCode = ss.ProductCode AND p.AreaID = ss.AreaID
	LEFT JOIN iti ON iti.ProductCode = p.ProductCode AND iti.HouseID = p.HouseID
	LEFT JOIN Tbl_Cargo_MonthSaleStatic mss ON mss.ProductCode = p.ProductCode AND mss.AreaID = p.AreaID AND mss.YearMonth = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) -1 ,0)
	LEFT JOIN Tbl_Cargo_OutOfStock oos ON oos.ProductCode = ss.ProductCode AND oos.HouseID = ss.HouseID
WHERE 
	ss.MinStock > 0 AND MaxStock > 0
	AND (oos.OOSID IS NOT NULL OR (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0)) < ss.MinStock)
	AND ISNULL(oos.Piece, 0) <> (ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0)))
ORDER BY ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0)) DESC










