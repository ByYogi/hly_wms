using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using House.DataAccess;
using House.Entity.Cargo.Report;

namespace House.Manager.Cargo
{
    /// <summary>
    /// 出库标签报表 - SQL构建辅助方法
    /// </summary>
    public partial class CargoReportManager
    {
        #region 出库标签报表 - SQL构建辅助方法

        /// <summary>
        /// 构建出库标签报表查询SQL（核心逻辑）
        /// 注意：此方法被 QueryOutboundLabelReport 和 QueryOutboundLabelReportPaged 共同使用
        /// </summary>
        /// <param name="entity">查询条件</param>
        /// <param name="cmd">输出参数：构建好的DbCommand</param>
        /// <returns>SQL语句</returns>
        private string BuildOutboundLabelReportSQL(CargoOutboundLabelReportEntity entity, out DbCommand cmd)
        {
            StringBuilder sql = new StringBuilder();

            // 1. CTE部分 - 仓库区域层级结构
            sql.Append(@"
;WITH ChildAreaCTE (HouseID, RootArea, ParentArea, AreaID, RootName, ParentName, SectionName, Level) AS (
    SELECT a.HouseID,
           a.AreaID,
           a.ParentID,
           a.AreaID,
           a.Name,
           CAST('' AS varchar(50)),
           a.Name,
           0
    FROM Tbl_Cargo_Area a
    WHERE a.ParentID = 0 AND a.IsShowStock = 0
    UNION ALL
    SELECT c.HouseID,
           c.RootArea,
           a.ParentID,
           a.AreaID,
           c.RootName,
           c.SectionName,
           a.Name,
           c.Level + 1
    FROM Tbl_Cargo_Area a
    INNER JOIN ChildAreaCTE c ON a.ParentID = c.AreaID
)");

            // 2. SELECT和FROM子句
            sql.Append(@"
SELECT a.ProductID, a.TagCode, a.OutCargoStatus, a.OutCargoOperID, a.OutCargoTime, a.OrderNo,
       b.ProductName, b.GoodsCode, b.Model, b.Specs, b.Figure, b.LoadIndex, b.SpeedLevel, b.Batch,
       b.SourceOrderNo, b.SpecsType, b.Supplier, b.ProductCode, b.BelongDepart,
       h.HouseID, h.Name AS HouseName,
       ISNULL(pct.TypeName, '') AS CategoryName,
       pt.TypeID, pt.TypeName,
       ha.RootArea, ha.RootName AS AreaName, ha.ParentName AS SubAreaName, ha.SectionName,
       c.ContainerID, c.ContainerCode,
       ISNULL(ps.SourceName, '') AS SourceName
FROM Tbl_Cargo_ProductTag AS a
INNER JOIN Tbl_Cargo_Product AS b ON a.ProductID = b.ProductID
INNER JOIN Tbl_Cargo_House AS h ON h.HouseID = b.HouseID
INNER JOIN Tbl_Cargo_ProductType pt ON pt.TypeID = b.TypeID
LEFT JOIN Tbl_Cargo_ProductType pct ON pct.TypeID = pt.ParentID AND pct.ParentID = 0
INNER JOIN Tbl_Cargo_Container c ON c.ContainerID = a.ContainerID
INNER JOIN ChildAreaCTE ha ON ha.AreaID = c.AreaID
LEFT JOIN Tbl_Cargo_ProductSource ps ON b.Source = ps.Source
WHERE a.OutCargoStatus = 1 ");

            // 3. 动态WHERE条件
            BuildWhereClause(sql, entity);

            // 4. 创建Command并添加参数
            cmd = conn.GetSqlStringCommond(sql.ToString());
            AddOutboundLabelReportParameters(cmd, entity);

            return sql.ToString();
        }

        /// <summary>
        /// 构建WHERE子句
        /// </summary>
        private void BuildWhereClause(StringBuilder sql, CargoOutboundLabelReportEntity entity)
        {
            // 出库时间范围
            if (entity.StartDate.HasValue)
                sql.Append(" AND a.OutCargoTime >= @StartDate ");
            if (entity.EndDate.HasValue)
                sql.Append(" AND a.OutCargoTime < @EndDate ");

            // 仓库ID
            if (!string.IsNullOrEmpty(entity.CargoPermisID))
                sql.Append(" AND h.HouseID IN (" + entity.CargoPermisID + ") ");
            else if (entity.HouseID != 0)
                sql.Append(" AND h.HouseID = @HouseID ");

            // 库区筛选
            if (entity.AreaID != 0)
                sql.Append(" AND ha.RootArea = @AreaID ");

            // 分区筛选
            if (entity.SubAreaID != 0)
                sql.Append(" AND ha.ParentArea = @SubAreaID ");

            // 货架筛选
            if (entity.SectionID != 0)
                sql.Append(" AND ha.AreaID = @SectionID ");

            // 品类筛选
            if (entity.CategoryID != 0)
                sql.Append(" AND pt.ParentID = @CategoryID ");

            // 品牌筛选
            if (entity.TypeID != 0)
                sql.Append(" AND pt.TypeID = @TypeID ");
        }

        /// <summary>
        /// 添加查询参数到Command
        /// </summary>
        private void AddOutboundLabelReportParameters(DbCommand cmd, CargoOutboundLabelReportEntity entity)
        {
            if (entity.StartDate.HasValue)
                conn.AddInParameter(cmd, "@StartDate", DbType.DateTime, entity.StartDate);
            if (entity.EndDate.HasValue)
                conn.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate.Value.AddDays(1));
            if (entity.HouseID != 0)
                conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
            if (entity.AreaID != 0)
                conn.AddInParameter(cmd, "@AreaID", DbType.Int32, entity.AreaID);
            if (entity.SubAreaID != 0)
                conn.AddInParameter(cmd, "@SubAreaID", DbType.Int32, entity.SubAreaID);
            if (entity.SectionID != 0)
                conn.AddInParameter(cmd, "@SectionID", DbType.Int32, entity.SectionID);
            if (entity.CategoryID != 0)
                conn.AddInParameter(cmd, "@CategoryID", DbType.Int32, entity.CategoryID);
            if (entity.TypeID != 0)
                conn.AddInParameter(cmd, "@TypeID", DbType.Int32, entity.TypeID);
        }

        /// <summary>
        /// 将DataRow映射为实体对象
        /// </summary>
        private CargoOutboundLabelReportEntity MapToOutboundLabelReportEntity(DataRow row)
        {
            return new CargoOutboundLabelReportEntity
            {
                ProductID = Convert.ToInt64(row["ProductID"]),
                TagCode = Convert.ToString(row["TagCode"]),
                OutCargoStatus = Convert.ToInt32(row["OutCargoStatus"]),
                OutCargoOperID = Convert.ToString(row["OutCargoOperID"]),
                OutCargoTime = Convert.ToDateTime(row["OutCargoTime"]),
                OrderNo = Convert.ToString(row["OrderNo"]),
                ProductName = Convert.ToString(row["ProductName"]),
                GoodsCode = Convert.ToString(row["GoodsCode"]),
                Model = Convert.ToString(row["Model"]),
                Specs = Convert.ToString(row["Specs"]),
                Figure = Convert.ToString(row["Figure"]),
                LoadIndex = Convert.ToString(row["LoadIndex"]),
                SpeedLevel = Convert.ToString(row["SpeedLevel"]),
                LoadSpeed = Convert.ToString(row["LoadIndex"]) + Convert.ToString(row["SpeedLevel"]),
                Batch = Convert.ToString(row["Batch"]),
                SourceOrderNo = Convert.ToString(row["SourceOrderNo"]),
                SpecsType = Convert.ToString(row["SpecsType"]),
                Supplier = Convert.ToString(row["Supplier"]),
                ProductCode = Convert.ToString(row["ProductCode"]),
                BelongDepart = Convert.ToString(row["BelongDepart"]),
                HouseID = Convert.ToInt32(row["HouseID"]),
                HouseName = Convert.ToString(row["HouseName"]),
                CategoryName = Convert.ToString(row["CategoryName"]),
                TypeID = Convert.ToInt32(row["TypeID"]),
                TypeName = Convert.ToString(row["TypeName"]),
                AreaName = Convert.ToString(row["AreaName"]),
                SubAreaName = Convert.ToString(row["SubAreaName"]),
                SectionName = Convert.ToString(row["SectionName"]),
                ContainerID = Convert.ToInt32(row["ContainerID"]),
                ContainerCode = Convert.ToString(row["ContainerCode"]),
                SourceName = Convert.ToString(row["SourceName"])
            };
        }

        #endregion
    }
}
