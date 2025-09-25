DROP TABLE Tbl_Cargo_MonthSaleStatic
CREATE TABLE Tbl_Cargo_MonthSaleStatic (
    YearMonth DATE NOT NULL,
    ProductCode NVARCHAR(50) NOT NULL,
    TypeID INT NOT NULL,
    HouseID INT NOT NULL,
    AreaID INT NOT NULL,
    Piece INT NOT NULL, --当月总销量
    SumLast3Months INT NOT NULL,
    WeightedMonthSale INT NOT NULL, --月均销量
    LastUpdateTime DATETIME DEFAULT GETDATE(), 
    CONSTRAINT PK_Tbl_Cargo_SaleSummary PRIMARY KEY 
        (YearMonth, ProductCode, TypeID, HouseID, AreaID)
);


DROP TABLE Tbl_Cargo_DailySaleStatic
--TRUNCATE TABLE Tbl_Cargo_DailySaleStatic
CREATE TABLE Tbl_Cargo_DailySaleStatic (
    SalesDate DATE NOT NULL,
    ProductCode NVARCHAR(50) NOT NULL,
    TypeID INT NOT NULL,
    HouseID INT NOT NULL,
    AreaID INT NOT NULL,
    Piece INT,
    WXPiece INT,
    LastUpdateTime DATETIME DEFAULT GETDATE(), 
    CONSTRAINT PK_Tbl_Cargo_DailySaleStatic PRIMARY KEY 
        (SalesDate, ProductCode, TypeID, HouseID, AreaID)
);

------------ 索引 ------------
CREATE INDEX IX_DailySaleStatic_Product_Type_SalesDate
ON Tbl_Cargo_DailySaleStatic(ProductCode, TypeID, SalesDate)
INCLUDE (Piece);
CREATE INDEX IX_DailySaleStatic_Query
ON Tbl_Cargo_DailySaleStatic (SalesDate, HouseID, TypeID, ProductCode, AreaID)
INCLUDE (Piece, WXPiece);



CREATE INDEX IX_ContainerGoods_Type_Container_Product
ON Tbl_Cargo_ContainerGoods (ProductID, TypeID, ContainerID)
INCLUDE (Piece);

