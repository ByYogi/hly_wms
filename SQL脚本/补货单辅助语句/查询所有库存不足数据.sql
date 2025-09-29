

-- 产品&品牌
WITH prdctGrp AS (
SELECT 
	MAX(p.ProductID) ProductID,
	p.ProductCode,
	ca.RootArea AreaID,
	SUM(cg.Piece) Piece
FROM
	Tbl_Cargo_Product p
	INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
	INNER JOIN Tbl_Cargo_ContainerGoods cg ON p.ProductID = cg.ProductID
	INNER JOIN Tbl_Cargo_Container c ON cg.ContainerID = c.ContainerID
	INNER JOIN #childArea ca ON ca.AreaID = c.AreaID
	--INNER JOIN ##productTemp pTemp ON p.ProductID = pTemp.ProductID
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
    h.HouseID,
	h.Name HouseName,
	h.ParentID HouseParentID,
	h.ParentName HouseParentName,
	pg.Piece InPiece,
	pg.AreaID
FROM
	Tbl_Cargo_Product p
	INNER JOIN prdctGrp pg ON p.ProductID = pg.ProductID
	INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
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
		--INNER JOIN ##productTemp ptemp ON fo.ProductID = ptemp.ProductID
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
	ro.HouseID,
	SUM(rog.Piece - rog.DonePiece) Piece
FROM
	Tbl_Cargo_RplOrderGoods rog 
	INNER JOIN Tbl_Cargo_RplOrder ro ON rog.RplID = ro.RplID
WHERE
	ro.Status IN (0,1,2)
GROUP BY 
	rog.ProductCode, ro.HouseID
)

--安全库存配置
SELECT
	ss.SID,
	ss.AreaID,
	ss.MinStock,
	ss.MaxStock,
	ISNULL(mss.WeightedMonthSale, 0) WeightedMonthSale,
	ISNULL(iti.Piece, 0) InTransitPiece,
	ISNULL(ro.Piece, 0) RestockingPiece,
	(ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) - ISNULL(ro.Piece, 0)) RealPiece,
	ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) - ISNULL(ro.Piece, 0)) Piece,
	p.*
FROM
	Tbl_Cargo_SafeStock ss
	INNER JOIN pCTE p ON p.ProductCode = ss.ProductCode AND p.AreaID = ss.AreaID
	LEFT JOIN iti ON iti.ProductCode = p.ProductCode AND iti.HouseID = p.HouseID
	LEFT JOIN Tbl_Cargo_MonthSaleStatic mss ON mss.ProductCode = p.ProductCode AND mss.AreaID = p.AreaID AND mss.YearMonth = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) -1 ,0)
	LEFT JOIN ro ON ro.ProductCode = p.ProductCode AND ro.HouseID = p.HouseID
WHERE 
	ss.MinStock > 0 AND MaxStock > 0
	AND (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) - ISNULL(ro.Piece, 0)) < ss.MinStock 
	AND ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) - ISNULL(ro.Piece, 0)) > 0
ORDER BY ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) - ISNULL(ro.Piece, 0)) DESC

	--select COUNT(*) FROM Tbl_Cargo_SafeStock

	select * from Tbl_Cargo_ProductType where TypeID = 648