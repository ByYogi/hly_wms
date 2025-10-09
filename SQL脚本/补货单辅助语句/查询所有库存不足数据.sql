
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

-- 产品&品牌
WITH prdctGrp AS (
SELECT 
	p.ProductCode,
	ca.RootArea AreaID,
	SUM(cg.Piece) Piece
FROM
	Tbl_Cargo_Product p
	INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
	INNER JOIN Tbl_Cargo_ContainerGoods cg ON p.ProductID = cg.ProductID
	INNER JOIN Tbl_Cargo_Container c ON cg.ContainerID = c.ContainerID
	INNER JOIN #childArea ca ON ca.AreaID = c.AreaID
WHERE
	ISNULL(p.ProductCode, '') <> '' 
GROUP BY
	p.ProductCode, ca.RootArea
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
	pg.Piece InPiece,
	pg.AreaID
FROM
	Tbl_Cargo_Product p
	INNER JOIN prdctGrp pg ON p.ProductCode = pg.ProductCode
	--INNER JOIN ##productTemp pTemp ON p.ProductID = pTemp.ProductID
	INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
	LEFT JOIN Tbl_Cargo_ProductType pt2 ON pt.ParentID = pt2.TypeID
	LEFT JOIN Tbl_Cargo_House h ON p.HouseID = h.HouseID
WHERE
	ISNULL(p.ProductCode, '') <> ''
)
,
--在途
iti AS (
	SELECT
		fo.ProductCode,
		fo.HouseID,
		SUM(ReplyNumber - fo.InPiece) Piece
	FROM
		Tbl_Cargo_FactoryOrder fo
	WHERE
		(1 = 1)
		AND fo.InCargoStatus IN (0,2)
	GROUP BY
		fo.ProductCode,
		fo.HouseID
)
--select * from iti
,

--在补货
ro as (
SELECT
	rog.ProductCode, 
	ro.FromHouse HouseID,
	SUM(rog.Piece - rog.DonePiece) Piece
FROM
	Tbl_Cargo_RplOrderGoods rog 
	INNER JOIN Tbl_Cargo_RplOrder ro ON rog.RplID = ro.RplID
WHERE
	ro.Status NOT IN (2,3)
GROUP BY 
	rog.ProductCode, ro.FromHouse
)

--安全库存配置
SELECT
	ss.SID,
	ss.MinStock,
	ss.MaxStock,
	ISNULL(mss.WeightedMonthSale, 0) WeightedMonthSale,
	ISNULL(iti.Piece, 0) InTransitPiece,
	ISNULL(ro.Piece, 0) RestockingPiece,
	(ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) + ISNULL(ro.Piece, 0)) RealPiece,
	ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) + ISNULL(ro.Piece, 0)) Piece,
	p.*
FROM
	Tbl_Cargo_SafeStock ss
	INNER JOIN pCTE p ON p.ProductCode = ss.ProductCode AND p.AreaID = ss.AreaID
	LEFT JOIN iti ON iti.ProductCode = p.ProductCode AND iti.HouseID = p.HouseID
	LEFT JOIN Tbl_Cargo_MonthSaleStatic mss ON mss.ProductCode = p.ProductCode AND mss.AreaID = p.AreaID AND mss.YearMonth = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) -1 ,0)
	LEFT JOIN ro ON ro.ProductCode = p.ProductCode AND ro.HouseID = p.HouseID
	LEFT JOIN Tbl_Cargo_OutOfStock oos ON oos.ProductCode = ss.ProductCode AND oos.HouseID = ss.HouseID
WHERE 
	ss.MinStock > 0 AND MaxStock > 0
	AND (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) - ISNULL(ro.Piece, 0)) < ss.MinStock 
	AND ISNULL(oos.Piece, 0) <> ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) + ISNULL(ro.Piece, 0))
	AND p.HouseID = 93 -- 查询东平的
ORDER BY ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) + ISNULL(ro.Piece, 0)) DESC

--SELECT * FROM Tbl_Cargo_Product WHERE ProductID = 938921