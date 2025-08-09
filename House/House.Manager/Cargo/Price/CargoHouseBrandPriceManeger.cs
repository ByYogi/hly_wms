using House.DataAccess;
using House.Entity.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;


namespace House.Manager.Cargo
{
    public class CargoHouseBrandPriceManeger
    {
        private SqlHelper conn = new SqlHelper();

        /// <summary>
        /// 仓库品牌加价列表查询
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public Hashtable QueryHouseBrandData(int pIndex, int pNum, CargoHouseBrandPriceEntity entity)
        {
            List<CargoHouseBrandPriceEntity> result = new List<CargoHouseBrandPriceEntity>();
            Hashtable resHT = new Hashtable();
            CargoProductManager pro = new CargoProductManager();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(select ROW_NUMBER() OVER (ORDER BY a.BID DESC) AS RowNumber, a.*,b.Name as HouseName,c.TypeName as TypeName From Tbl_Cargo_HouseBrandPrice as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID inner join Tbl_Cargo_ProductType as c on a.TypeID=c.TypeID Where (1=1) ";
                //查询条件
                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
                if (!entity.TypeID.Equals(0)) { strSQL += " and a.TypeID =" + entity.TypeID; }
                if (!entity.CloudHouseType.Equals(-1)) { strSQL += " and a.CloudHouseType =" + entity.CloudHouseType; }
                
                strSQL += ")  as Tbl_Cargo_HouseBrandPrice";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取数据
                            result.Add(new CargoHouseBrandPriceEntity
                            {
                                 BID= Convert.ToInt64(idr["BID"]), 
                                 HouseID = Convert.ToInt32(idr["HouseID"]),
                                 HouseName = Convert.ToString(idr["HouseName"]),
                                 TypeID = Convert.ToInt32(idr["TypeID"]),
                                 TypeName = Convert.ToString(idr["TypeName"]),
                                 UpMoney =Convert.ToDecimal(idr["UpMoney"]),
                                 UpRate = Convert.ToDecimal(idr["UpRate"]), 
                                 //UpType = Convert.ToInt32(idr["UpType"]),
                                 CloudHouseType = Convert.ToInt32(idr["CloudHouseType"]),
                                 OPDATE= Convert.ToDateTime(idr["OPDATE"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_HouseBrandPrice  Where (1=1)";

                //条件
                if (!entity.HouseID.Equals(0)) { strCount += " and HouseID =" + entity.HouseID; }
                if (!entity.TypeID.Equals(0)) { strCount += " and TypeID =" + entity.TypeID; }
                if (!entity.CloudHouseType.Equals(-1)) { strCount += " and CloudHouseType =" + entity.CloudHouseType; }

                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }


        /// <summary>
        /// 判断仓库品牌是否存在 true:表示存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true:表示存在</returns>
        public bool IsExistHouseBrandPrice(CargoHouseBrandPriceEntity entity)
        {

            string strQ = @"Select BID from Tbl_Cargo_HouseBrandPrice Where HouseID=@HouseID and TypeID=@TypeID";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@HouseID", DbType.String, entity.HouseID);
                conn.AddInParameter(cmdQ, "@TypeID", DbType.String, entity.TypeID);
               // conn.AddInParameter(cmdQ, "@UpType", DbType.String, entity.UpType);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 判断仓库品牌是否可修改 true:表示存在可以
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true:表示存在</returns>
        public bool IsExistUpdateHouseBrandPrice(CargoHouseBrandPriceEntity entity)
        {
            string strQ = @"Select BID from Tbl_Cargo_HouseBrandPrice Where HouseID=@HouseID and TypeID=@TypeID";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@HouseID", DbType.String, entity.HouseID);
                conn.AddInParameter(cmdQ, "@TypeID", DbType.String, entity.TypeID);
                //conn.AddInParameter(cmdQ, "@UpType", DbType.String, entity.UpType);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    foreach (DataRow time in idr.Rows)
                    {
                        if (entity.BID!=Convert.ToInt64(time["BID"])) {
                            return false;
                        }
                       
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 新增仓库品牌加价 
        /// </summary>
        /// <param name="entity"></param>
        public void AddHouseBrandPrice(CargoHouseBrandPriceEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_Cargo_HouseBrandPrice VALUES (@HouseID,@TypeID,@UpRate,@UpMoney,0,@OPID,@OPDATE,@CloudHouseType)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@TypeID", DbType.Int32, entity.TypeID);
                    conn.AddInParameter(cmd, "@UpRate", DbType.Decimal, entity.UpRate);
                    conn.AddInParameter(cmd, "@UpMoney", DbType.Decimal, entity.UpMoney);
                    //conn.AddInParameter(cmd, "@UpType", DbType.Int32, entity.UpType);
                    conn.AddInParameter(cmd, "@OPID", DbType.String, entity.OPID);
                    conn.AddInParameter(cmd, "@OPDATE", DbType.DateTime, entity.OPDATE);
                    conn.AddInParameter(cmd, "@CloudHouseType", DbType.Int32, entity.CloudHouseType);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改仓库品牌加价
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateHouseBrandPrice(CargoHouseBrandPriceEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_Cargo_HouseBrandPrice Set HouseID=@HouseID,TypeID=@TypeID,UpRate=@UpRate,UpMoney=@UpMoney,OPID=@OPID,OPDATE=@OPDATE,CloudHouseType=@CloudHouseType Where BID=@BID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@BID", DbType.Int64, entity.BID);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@TypeID", DbType.Int32, entity.TypeID);
                    conn.AddInParameter(cmd, "@UpRate", DbType.Decimal, entity.UpRate);
                    conn.AddInParameter(cmd, "@UpMoney", DbType.Decimal, entity.UpMoney);
                    //conn.AddInParameter(cmd, "@UpType", DbType.Int32, entity.UpType);
                    conn.AddInParameter(cmd, "@OPID", DbType.String, entity.OPID);
                    conn.AddInParameter(cmd, "@OPDATE", DbType.DateTime, entity.OPDATE);
                    conn.AddInParameter(cmd, "@CloudHouseType", DbType.Int32, entity.CloudHouseType);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }

        /// <summary>
        /// 删除仓库品牌加价
        /// </summary>
        /// <param name="entity"></param>
        public void DelHouseBrandPrice(List<CargoHouseBrandPriceEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"Delete from Tbl_Cargo_HouseBrandPrice where BID=@BID";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@BID", DbType.Int64, it.BID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }

        /// <summary>
        /// 获取轮播图数据表数据
        /// </summary>
        /// <returns></returns>
        public List<CargoBannerEntity> QueryHouseFile(CargoBannerEntity entity)
        {
            List<CargoBannerEntity> list = new List<CargoBannerEntity>();
            string strSQL = "select  * from Tbl_Cargo_HouseBanner where HouseID=@HouseID order by DelFlag, OPDATE desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@HouseID", DbType.String, entity.HouseID);
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        list.Add(new CargoBannerEntity() {
                            BID = Convert.ToInt32(idr["BID"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            PicName = Convert.ToString(idr["PicName"]),
                            Title = Convert.ToString(idr["Title"]),
                            DelFlag = Convert.ToString(idr["DelFlag"]),
                            OPDATE = Convert.ToDateTime(idr["OPDATE"])
                        });
                    }
                }
            }
            return list;
        }

      

        /// <summary>
        /// 删除轮播图数据表数据
        /// </summary>
        /// <returns></returns>
        public void DelHousePic(List<CargoBannerEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"UPDATE Tbl_Cargo_HouseBanner SET DelFlag='1' WHERE BID=@BID";
                    //if (it.DelFlag.Equals("1"))//彻底删除
                    //{
                    //    strSQL = @"Delete from Tbl_Cargo_House where HouseID=@HouseID ";
                    //}
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@BID", DbType.Int32, it.BID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }


        /// <summary>
        /// 新增小程序照片数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCargoHouseBrandFile(CargoBannerEntity entity)
        {
            string inSQL = @"insert into Tbl_Cargo_HouseBanner(HouseID, PicName, Title, DelFlag, OPDATE) values (@HouseID, @PicName, @Title, @DelFlag, @OPDATE)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(inSQL))
                {
                    conn.AddInParameter(cmd, "@HouseID", DbType.String, entity.HouseID);
                    conn.AddInParameter(cmd, "@PicName", DbType.String, entity.PicName);
                    conn.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@OPDATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
    }
}
