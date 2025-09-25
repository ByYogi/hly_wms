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
        og.AreaID,
        DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0) YearMonth,
        SUM(ISNULL(og.Piece,0)) AS Piece
    FROM Tbl_Cargo_Product AS p
    INNER JOIN Tbl_Cargo_OrderGoods AS og
        ON og.ProductID = p.ProductID
    WHERE ISNULL(p.ProductCode, '') <> ''
        AND DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0) BETWEEN DATEADD(MONTH, -2, @LastMonth) AND @LastMonth
    GROUP BY 
        p.ProductCode, p.TypeID, p.HouseID, og.AreaID, DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0)
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
