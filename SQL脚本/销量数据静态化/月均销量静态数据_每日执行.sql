
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


PRINT('------------ 删除上月数据 ------------');
DECLARE @LastMonth DATE = DATEADD(MONTH, -1, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0));
DELETE FROM Tbl_Cargo_MonthSaleStatic
WHERE YearMonth = @LastMonth;

PRINT('------------ 重新计算上月销量（近3个月） ------------');
PRINT('------------ 获取产品每月销量 ------------')
;WITH 
-- 获取从第一个销售单的创建时间到今天所有年月时间
DateRange AS (
    SELECT DATEADD(MONTH, -2, @LastMonth) AS MinMonth,
           @LastMonth AS MaxMonth
)
--SELECT * FROM DateRange
,MonthDim AS (
    SELECT MinMonth AS YearMonth
    FROM DateRange
    UNION ALL
    SELECT DATEADD(MONTH, 1, YearMonth)
    FROM MonthDim
    CROSS JOIN DateRange
    WHERE YearMonth < DateRange.MaxMonth
)
--SELECT * FROM MonthDim
,MonthlySalesGroup AS (
    SELECT
        p.ProductCode,
        p.TypeID,
        p.HouseID,
        ca.RootArea AreaID,
        DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0) YearMonth,
        SUM(ISNULL(og.Piece,0)) AS Piece
    FROM Tbl_Cargo_Product AS p
        INNER JOIN Tbl_Cargo_OrderGoods AS og ON og.ProductID = p.ProductID
        INNER JOIN #childArea ca ON ca.AreaID = og.AreaID
    WHERE ISNULL(p.ProductCode, '') <> ''
        AND DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0) BETWEEN DATEADD(MONTH, -2, @LastMonth) AND @LastMonth
    GROUP BY 
        p.ProductCode, p.TypeID, p.HouseID, ca.RootArea, DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0)
)

,MonthlySalesOrg AS (
    SELECT
        p.*,
        ISNULL(m.Piece,0) AS Piece
    FROM (
            SELECT * FROM
            (SELECT ProductCode, TypeID, HouseID, AreaID FROM MonthlySalesGroup GROUP BY ProductCode, TypeID, HouseID, AreaID) p
            CROSS JOIN MonthDim d
        ) p
        LEFT JOIN MonthlySalesGroup m ON m.ProductCode = p.ProductCode AND m.TypeID = p.TypeID AND m.HouseID = p.HouseID AND m.AreaID = p.AreaID AND m.YearMonth = p.YearMonth
)

SELECT * 
    INTO #MonthlySales FROM MonthlySalesOrg;
CREATE UNIQUE INDEX IDX_#MonthlySales ON #MonthlySales (ProductCode,
        TypeID,
        HouseID,
        AreaID,
        YearMonth)
        INCLUDE(Piece)
        ;
-- select * from #MonthlySales
-- drop table #MonthlySales

PRINT('------------ 加权近3个月的月均销量 ------------')
PRINT('------------ 持久化插入计算数据 ------------')
;WITH MonthlyRanked AS
(
    SELECT a.*,
           x.SumLast3Months
    FROM #MonthlySales a
    OUTER APPLY
    (
        SELECT SUM(b.Piece) AS SumLast3Months
        FROM #MonthlySales b
        WHERE b.ProductCode = a.ProductCode
          AND b.TypeID = a.TypeID
          AND b.HouseID = a.HouseID
          AND b.AreaID = a.AreaID
          AND b.YearMonth BETWEEN DATEADD(MONTH, -2, a.YearMonth) AND a.YearMonth
    ) x
),
Weighted AS
(
    SELECT a.*,
           ISNULL(b.Piece * 0.2, 0) +
           ISNULL(c.Piece * 0.3, 0) +
           a.Piece * 0.5 AS WeightedMonthSale
    FROM MonthlyRanked a
    OUTER APPLY
    (
        SELECT TOP 1 b.Piece
        FROM #MonthlySales b
        WHERE b.ProductCode = a.ProductCode
          AND b.TypeID = a.TypeID
          AND b.HouseID = a.HouseID
          AND b.AreaID = a.AreaID
          AND b.YearMonth = DATEADD(MONTH, -2, a.YearMonth)
    ) b
    OUTER APPLY
    (
        SELECT TOP 1 c.Piece
        FROM #MonthlySales c
        WHERE c.ProductCode = a.ProductCode
          AND c.TypeID = a.TypeID
          AND c.HouseID = a.HouseID
          AND c.AreaID = a.AreaID
          AND c.YearMonth = DATEADD(MONTH, -1, a.YearMonth)
    ) c
    WHERE a.SumLast3Months > 0
)

INSERT INTO Tbl_Cargo_MonthSaleStatic
(
    YearMonth,
    ProductCode,
    TypeID,
    HouseID,
    AreaID,
    Piece,
    SumLast3Months,
    WeightedMonthSale,
    LastUpdateTime
)
SELECT
    YearMonth,
    ProductCode,
    TypeID,
    HouseID,
    AreaID,
    Piece,--当月总销量
    CEILING(SumLast3Months) SumLast3Months, --近3月总销量
    CEILING(WeightedMonthSale) WeightedMonthSale, --近3月加权总销量
    GETDATE() LastUpdateTime
FROM Weighted
WHERE YearMonth = @LastMonth
ORDER BY YearMonth,ProductCode, HouseID, AreaID;

PRINT('------------ 完成上月销量入库 ------------');
