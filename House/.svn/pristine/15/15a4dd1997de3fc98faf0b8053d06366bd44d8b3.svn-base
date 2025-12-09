using House.DataAccess;
using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using House.Entity.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace House.Manager.Cargo
{
    public class CargoWeiXinManager
    {
        private SqlHelper conn = new SqlHelper();
        #region 微信用户管理
        /// <summary>
        /// 根据OpenID判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistWeixinUser(WXUserEntity entity)
        {
            string strQ = @"Select wxOpenID from Tbl_WX_Client Where wxOpenID=@wxOpenID";

            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@wxOpenID", DbType.String, entity.wxOpenID);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 添加 微信用户
        /// </summary>
        /// <param name="entity"></param>
        public void AddWeixinUser(WXUserEntity entity)
        {
            entity.EnSafe();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_WX_Client(Name,wxOpenID,wxName,AvatarSmall,AvatarBig,Cellphone,IDCardNum,IsCertific,ConsumerPoint,Sex,Province,City,Country,Address,RegisterDate,OP_DATE,UnionID,DevOpenID,StorePhone,CompanyName,ClientNum,LogisID,LogicName,UserType) VALUES (@Name,@wxOpenID,@wxName,@AvatarSmall,@AvatarBig,@Cellphone,@IDCardNum,@IsCertific,@ConsumerPoint,@Sex,@Province,@City,@Country,@Address,@RegisterDate,@OP_DATE,@UnionID,@DevOpenID,@StorePhone,@CompanyName,@ClientNum,@LogisID,@LogicName,@UserType) SELECT @@IDENTITY";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@wxOpenID", DbType.String, entity.wxOpenID);
                    conn.AddInParameter(cmd, "@wxName", DbType.String, entity.wxName);
                    conn.AddInParameter(cmd, "@AvatarSmall", DbType.String, entity.AvatarSmall);
                    conn.AddInParameter(cmd, "@AvatarBig", DbType.String, entity.AvatarBig);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@IDCardNum", DbType.String, entity.IDCardNum);
                    conn.AddInParameter(cmd, "@IsCertific", DbType.String, entity.IsCertific);
                    conn.AddInParameter(cmd, "@ConsumerPoint", DbType.Int32, entity.ConsumerPoint);
                    conn.AddInParameter(cmd, "@Sex", DbType.Int32, entity.Sex);
                    conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                    conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                    conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                    conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    conn.AddInParameter(cmd, "@UnionID", DbType.String, entity.UnionID);
                    conn.AddInParameter(cmd, "@DevOpenID", DbType.String, entity.DevOpenID);
                    conn.AddInParameter(cmd, "@StorePhone", DbType.String, entity.StorePhone);
                    conn.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@LogicName", DbType.String, entity.LogicName);
                    conn.AddInParameter(cmd, "@RegisterDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@UserType", DbType.String, entity.UserType);
                    conn.AddInParameter(cmd, "@AcceptName", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.Cellphone);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }

                AddWeixinPoint(new WXUserPointEntity { WXID = did, Point = entity.ConsumerPoint, CutPoint = "0", PointType = "1" });
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询微信用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXUserEntity> QueryWeixinUserInfo(WXUserEntity entity)
        {
            List<WXUserEntity> result = new List<WXUserEntity>();
            //string strSQL = "Select * From Tbl_WX_Client Where (1=1) and UnionID is null";
            string strSQL = "Select * From Tbl_WX_Client Where (1=1) ";
            if (!entity.ClientNum.Equals(0)) { strSQL += " and ClientNum=" + entity.ClientNum; }
            if (!string.IsNullOrEmpty(entity.UnionID)) { strSQL += " and UnionID='" + entity.UnionID + "'"; }
            if (!string.IsNullOrEmpty(entity.wxOpenID)) { strSQL += " and wxOpenID='" + entity.wxOpenID + "'"; }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new WXUserEntity
                        {
                            ID = Convert.ToInt64(idr["ID"]),
                            Name = Convert.ToString(idr["Name"]),
                            wxName = Convert.ToString(idr["wxName"]),
                            wxOpenID = Convert.ToString(idr["wxOpenID"]),
                            AvatarBig = Convert.ToString(idr["AvatarBig"]),
                            AvatarSmall = Convert.ToString(idr["AvatarSmall"]),
                            Cellphone = Convert.ToString(idr["Cellphone"]),
                            StorePhone = Convert.ToString(idr["StorePhone"]),
                            IDCardNum = Convert.ToString(idr["IDCardNum"]),
                            Sex = Convert.ToInt32(idr["Sex"]),
                            Province = Convert.ToString(idr["Province"]),
                            City = Convert.ToString(idr["City"]),
                            Country = Convert.ToString(idr["Country"]),
                            CompanyName = Convert.ToString(idr["CompanyName"]),
                            DevOpenID = Convert.ToString(idr["DevOpenID"]),
                            IsManager = Convert.ToString(idr["IsManager"]),
                            UnionID = Convert.ToString(idr["UnionID"])
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 修改微信用户信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWeixinUser(List<WXUserEntity> entity)
        {
            foreach (var it in entity)
            {
                it.EnSafe();
                string strSQL = "Update Tbl_WX_Client set Sex=@Sex";
                if (!string.IsNullOrEmpty(it.Name)) { strSQL += ",Name=@Name"; }
                if (!string.IsNullOrEmpty(it.wxName)) { strSQL += ",wxName=@wxName"; }
                if (!string.IsNullOrEmpty(it.AvatarBig)) { strSQL += ",AvatarBig=@AvatarBig"; }
                if (!string.IsNullOrEmpty(it.AvatarSmall)) { strSQL += ",AvatarSmall=@AvatarSmall"; }
                if (!string.IsNullOrEmpty(it.UnionID)) { strSQL += ",UnionID=@UnionID"; }
                if (!string.IsNullOrEmpty(it.Cellphone)) { strSQL += ",Cellphone=@Cellphone"; }
                if (!string.IsNullOrEmpty(it.AcceptName)) { strSQL += ",AcceptName=@AcceptName"; }
                if (!string.IsNullOrEmpty(it.AcceptCellphone)) { strSQL += ",AcceptCellphone=@AcceptCellphone"; }
                if (!string.IsNullOrEmpty(it.CompanyName)) { strSQL += ",CompanyName=@CompanyName"; }
                if (!string.IsNullOrEmpty(it.Address)) { strSQL += ",Address=@Address"; }
                if (!string.IsNullOrEmpty(it.BusLicenseImg)) { strSQL += ",BusLicenseImg=@BusLicenseImg"; }
                if (!it.ClientNum.Equals(0)) { strSQL += ",ClientNum=@ClientNum"; }
                strSQL += " Where wxOpenID=@wxOpenID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Sex", DbType.Int32, it.Sex);
                    conn.AddInParameter(cmd, "@wxOpenID", DbType.String, it.wxOpenID);
                    if (!string.IsNullOrEmpty(it.Name)) { conn.AddInParameter(cmd, "@Name", DbType.String, it.Name); }
                    if (!string.IsNullOrEmpty(it.wxName)) { conn.AddInParameter(cmd, "@wxName", DbType.String, it.wxName); }
                    if (!string.IsNullOrEmpty(it.AvatarSmall)) { conn.AddInParameter(cmd, "@AvatarSmall", DbType.String, it.AvatarSmall); }
                    if (!string.IsNullOrEmpty(it.AvatarBig)) { conn.AddInParameter(cmd, "@AvatarBig", DbType.String, it.AvatarBig); }
                    if (!string.IsNullOrEmpty(it.UnionID)) { conn.AddInParameter(cmd, "@UnionID", DbType.String, it.UnionID); }
                    if (!string.IsNullOrEmpty(it.Cellphone)) { conn.AddInParameter(cmd, "@Cellphone", DbType.String, it.Cellphone); }
                    if (!string.IsNullOrEmpty(it.AcceptName)) { conn.AddInParameter(cmd, "@AcceptName", DbType.String, it.AcceptName); }
                    if (!string.IsNullOrEmpty(it.AcceptCellphone)) { conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, it.AcceptCellphone); }
                    if (!string.IsNullOrEmpty(it.CompanyName)) { conn.AddInParameter(cmd, "@CompanyName", DbType.String, it.CompanyName); }
                    if (!string.IsNullOrEmpty(it.Address)) { conn.AddInParameter(cmd, "@Address", DbType.String, it.Address); }
                    if (!string.IsNullOrEmpty(it.BusLicenseImg)) { conn.AddInParameter(cmd, "@BusLicenseImg", DbType.String, it.BusLicenseImg); }
                    if (!it.ClientNum.Equals(0)) { conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, it.ClientNum); }
                    conn.ExecuteNonQuery(cmd);
                }
            }
        }
        public void UpdateWeixinUserByUnionID(WXUserEntity entity)
        {
            entity.EnSafe();
            string strSQL = $@"Update Tbl_WX_Client set wxOpenID=@wxOpenID,wxName=@wxName,Sex=@Sex,Province=@Province,City=@City,IsCertific=@IsCertific,ConsumerPoint=@ConsumerPoint,Country=@Country,AvatarBig=@AvatarBig,AvatarSmall=@AvatarSmall,OP_DATE=@OP_DATE Where UnionID=@UnionID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@wxOpenID", DbType.String, entity.wxOpenID);
                conn.AddInParameter(cmd, "@wxName", DbType.String, entity.wxName);
                conn.AddInParameter(cmd, "@AvatarSmall", DbType.String, entity.AvatarSmall);
                conn.AddInParameter(cmd, "@AvatarBig", DbType.String, entity.AvatarBig);
                conn.AddInParameter(cmd, "@IsCertific", DbType.String, entity.IsCertific);
                conn.AddInParameter(cmd, "@ConsumerPoint", DbType.Int32, entity.ConsumerPoint);
                conn.AddInParameter(cmd, "@Sex", DbType.Int32, entity.Sex);
                conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                conn.AddInParameter(cmd, "@UnionID", DbType.String, entity.UnionID);
                conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 根据OPENID查询微信用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXUserEntity QueryWeixinUserByOpendID(WXUserEntity entity)
        {
            entity.EnSafe();
            WXUserEntity result = new WXUserEntity();
            try
            {
                string strSQL = @"Select a.*,ISNULL(b.MulAddressOrder,0) as MulAddressOrder,ISNULL(b.PaymentMethod,0) as PaymentMethod,ISNULL(b.LimitMoney,0) as LimitMoney,ISNULL(b.QuotaMoney,0) as QuotaMoney,ISNULL(b.Discount,1) as Discount,ISNULL(b.HouseID,0) as HouseID,ISNULL(b.TargetNum,0) as TargetNum,b.QuotaLimit,b.ArrivePayLimit,b.PushAccount,b.CheckOutType,b.ClientID,b.ClientNum,b.ClientName,b.UserID,b.UserName,ISNULL(b.IsBuy,0) as IsBuy,d.Name as HouseName,d.Address as HouseAddress,d.Cellphone as HouseCellphone,d.OperaBrand,ISNULL(d.LogisFee,0) as LogisFee,ISNULL(d.TwoLogisFee,0) as TwoLogisFee,ISNULL(d.ThreeLogisFee,0) as ThreeLogisFee,ISNULL(d.NextDayLogisFee,0) as NextDayLogisFee,ISNULL(d.OverDayNum,0) as OverDayNum,ISNULL(d.OverDueUnitPrice,0) as OverDueUnitPrice,ISNULL(d.EndBusHours,0) as EndBusHours,d.IsCanRush,d.IsCanPickUp,d.IsCanNextDay,ISNULL(d.StockShareHouseID,0) as StockShareHouseID,d.Lng,d.Lat,b.ClientType,b.NoType,ISNULL(b.PreReceiveMoney,0) as PreReceiveMoney from Tbl_WX_Client as a left join Tbl_Cargo_Client as b on a.ClientNum=b.ClientNum left join Tbl_Cargo_House as d on b.HouseID=d.HouseID where (1=1) ";
                if (!string.IsNullOrEmpty(entity.wxOpenID)) { strSQL += " and a.wxOpenID='" + entity.wxOpenID + "'"; }
                if (!string.IsNullOrEmpty(entity.UnionID)) { strSQL += " and a.UnionID='" + entity.UnionID + "'"; }
                if (!string.IsNullOrEmpty(entity.Cellphone)) { strSQL += " and a.Cellphone='" + entity.Cellphone + "'"; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    //conn.AddInParameter(cmd, "@wxOpenID", DbType.String, entity.wxOpenID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.ID = Convert.ToInt64(idr["ID"]);
                            result.wxName = Convert.ToString(idr["wxName"]);
                            result.Name = Convert.ToString(idr["Name"]);
                            result.wxOpenID = Convert.ToString(idr["wxOpenID"]);
                            result.AvatarBig = Convert.ToString(idr["AvatarBig"]);
                            result.AvatarSmall = Convert.ToString(idr["AvatarSmall"]);
                            result.Cellphone = Convert.ToString(idr["Cellphone"]);
                            result.IDCardNum = Convert.ToString(idr["IDCardNum"]);
                            result.IsCertific = Convert.ToString(idr["IsCertific"]);
                            result.Sex = Convert.ToInt32(idr["Sex"]);
                            result.BusLicenseImg = Convert.ToString(idr["BusLicenseImg"]);
                            result.IDCardImg = Convert.ToString(idr["IDCardImg"]);
                            result.IDCardBackImg = Convert.ToString(idr["IDCardBackImg"]);
                            result.Province = Convert.ToString(idr["Province"]);
                            result.City = Convert.ToString(idr["City"]);
                            result.Country = Convert.ToString(idr["Country"]);
                            result.Address = Convert.ToString(idr["Address"]);
                            result.RegisterDate = Convert.ToDateTime(idr["RegisterDate"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            result.ConsumerPoint = Convert.ToInt32(idr["ConsumerPoint"]);
                            result.ClientID = string.IsNullOrEmpty(Convert.ToString(idr["ClientID"])) ? 0 : Convert.ToInt32(idr["ClientID"]);
                            result.ClientNum = string.IsNullOrEmpty(Convert.ToString(idr["ClientNum"])) ? 0 : Convert.ToInt32(idr["ClientNum"]);
                            result.ClientName = Convert.ToString(idr["ClientName"]);
                            result.SaleManID = Convert.ToString(idr["UserID"]);
                            result.SaleManName = Convert.ToString(idr["UserName"]);
                            result.LimitMoney = Convert.ToDecimal(idr["LimitMoney"]);
                            result.QuotaMoney = Convert.ToDecimal(idr["QuotaMoney"]);
                            result.Discount = Convert.ToDecimal(idr["Discount"]);
                            result.HouseID = Convert.ToInt32(idr["HouseID"]);
                            result.HouseName = Convert.ToString(idr["HouseName"]);
                            result.HouseAddress = Convert.ToString(idr["HouseAddress"]);
                            result.HouseCellphone = Convert.ToString(idr["HouseCellphone"]);
                            result.IsSign = IsSign(new WXUserPointEntity { WXID = Convert.ToInt64(idr["ID"]), PointType = "2" });
                            result.LogisID = string.IsNullOrEmpty(Convert.ToString(idr["LogisID"])) ? 0 : Convert.ToInt32(idr["LogisID"]);
                            //result.BusinessID = Convert.ToString(idr["BusinessID"]);
                            result.LogicName = Convert.ToString(idr["LogicName"]);
                            result.TargetNum = Convert.ToInt32(idr["TargetNum"]);
                            result.SysLoginID = Convert.ToString(idr["SysLoginID"]);
                            result.SysLoginName = Convert.ToString(idr["SysLoginName"]);
                            result.QuotaLimit = Convert.ToString(idr["QuotaLimit"]);
                            result.UnionID = Convert.ToString(idr["UnionID"]);
                            result.DevOpenID = Convert.ToString(idr["DevOpenID"]);
                            result.UserType = Convert.ToString(idr["UserType"]);
                            result.LogisFee = Convert.ToDecimal(idr["LogisFee"]);
                            result.TwoLogisFee = Convert.ToDecimal(idr["TwoLogisFee"]);
                            result.ThreeLogisFee = Convert.ToDecimal(idr["ThreeLogisFee"]);
                            result.NextDayLogisFee = Convert.ToDecimal(idr["NextDayLogisFee"]);
                            result.PushAccount = Convert.ToString(idr["PushAccount"]);
                            result.OperaBrand = Convert.ToString(idr["OperaBrand"]);
                            result.AcceptName = Convert.ToString(idr["AcceptName"]);
                            result.AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]);
                            result.CompanyName = Convert.ToString(idr["CompanyName"]);
                            result.CheckOutType = Convert.ToString(idr["CheckOutType"]);
                            result.ArrivePayLimit = Convert.ToString(idr["ArrivePayLimit"]);
                            result.EndBusHours = Convert.ToString(idr["EndBusHours"]);
                            result.OverDayNum = Convert.ToInt32(idr["OverDayNum"]);
                            result.OverDueUnitPrice = Convert.ToDecimal(idr["OverDueUnitPrice"]);
                            result.IsCanRush = Convert.ToString(idr["IsCanRush"]);
                            result.IsCanPickUp = Convert.ToString(idr["IsCanPickUp"]);
                            result.IsCanNextDay = Convert.ToString(idr["IsCanNextDay"]);
                            result.StockShareHouseID = Convert.ToInt32(idr["StockShareHouseID"]);
                            result.MulAddressOrder = Convert.ToInt32(idr["MulAddressOrder"]);
                            result.PaymentMethod = Convert.ToInt32(idr["PaymentMethod"]);
                            result.ClientType = Convert.ToString(idr["ClientType"]);
                            result.PreReceiveMoney = Convert.ToDecimal(idr["PreReceiveMoney"]);
                            result.IsManager = Convert.ToString(idr["IsManager"]);
                            result.IsReckon = false;
                            result.IsShowLogic = true;
                            if (result.HouseID.Equals(46))
                            {
                                result.IsShowLogic = false;
                                if (result.LogisID.Equals(34))
                                {
                                    result.IsReckon = true;
                                }
                            }
                            //result.IsAPPFirstOrder = !IsExistWeixinOrderPay(new WXOrderEntity { WXID = Convert.ToInt64(idr["ID"]), OrderType = "3" });
                            result.Longitude = Convert.ToString(idr["Longitude"]);
                            result.Latitude = Convert.ToString(idr["Latitude"]);
                            result.HouseLng = Convert.ToString(idr["Lng"]);
                            result.HouseLat = Convert.ToString(idr["Lat"]);
                            result.IsBuy = Convert.ToInt32(idr["IsBuy"]);
                            result.NoType = Convert.ToString(idr["NoType"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            if (string.IsNullOrEmpty(result.DevOpenID) && !string.IsNullOrEmpty(entity.DevOpenID))
            {
                UpdateWxUserDevOpenID(new WXUserEntity { ID = result.ID, DevOpenID = entity.DevOpenID });
                result.DevOpenID = entity.DevOpenID;
            }
            return result;
        }
        /// <summary>
        /// 根据ID查询微信 用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXUserEntity QueryWeixinUserByID(WXUserEntity entity)
        {
            WXUserEntity result = new WXUserEntity();
            try
            {
                string strSQL = @"Select * from Tbl_WX_Client where ID=@ID ";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.ID = Convert.ToInt64(idr["ID"]);
                            result.wxName = Convert.ToString(idr["wxName"]);
                            result.Name = Convert.ToString(idr["Name"]);
                            result.wxOpenID = Convert.ToString(idr["wxOpenID"]);
                            result.AvatarBig = Convert.ToString(idr["AvatarBig"]);
                            result.AvatarSmall = Convert.ToString(idr["AvatarSmall"]);
                            result.Cellphone = Convert.ToString(idr["Cellphone"]);
                            result.IDCardNum = Convert.ToString(idr["IDCardNum"]);
                            result.IsCertific = Convert.ToString(idr["IsCertific"]);
                            result.Sex = Convert.ToInt32(idr["Sex"]);
                            result.Province = Convert.ToString(idr["Province"]);
                            result.City = Convert.ToString(idr["City"]);
                            result.Country = Convert.ToString(idr["Country"]);
                            result.Address = Convert.ToString(idr["Address"]);
                            result.ConsumerPoint = Convert.ToInt32(idr["ConsumerPoint"]);
                            //result.QuotaLimit = Convert.ToString(idr["QuotaLimit"]);
                            result.UnionID = Convert.ToString(idr["UnionID"]);
                            result.DevOpenID = Convert.ToString(idr["DevOpenID"]);
                            //result.BusinessID = Convert.ToString(idr["BusinessID"]);
                            result.BusLicenseImg = Convert.ToString(idr["BusLicenseImg"]);
                            result.IDCardImg = Convert.ToString(idr["IDCardImg"]);
                            result.IDCardBackImg = Convert.ToString(idr["IDCardBackImg"]);
                            result.SysLoginID = Convert.ToString(idr["SysLoginID"]);
                            result.SysLoginName = Convert.ToString(idr["SysLoginName"]);
                            result.UserType = Convert.ToString(idr["UserType"]);
                            result.RegisterDate = Convert.ToDateTime(idr["RegisterDate"]);
                            //result.HouseID = Convert.ToInt32(idr["HouseID"]);
                            result.LogisID = Convert.ToInt32(idr["LogisID"]);
                            result.LogicName = Convert.ToString(idr["LogicName"]);
                            result.CompanyName = Convert.ToString(idr["CompanyName"]);
                            result.Latitude = Convert.ToString(idr["Latitude"]);
                            result.Longitude = Convert.ToString(idr["Longitude"]);
                            result.ClientNum = string.IsNullOrEmpty(Convert.ToString(idr["ClientNum"])) ? 0 : Convert.ToInt32(idr["ClientNum"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public void UpdateWxUserConsume(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set ConsumerPoint=ConsumerPoint+@ConsumerPoint Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@ConsumerPoint", DbType.Int32, entity.ConsumerPoint);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改用户的微信开发者平台 OpendID
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserDevOpenID(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set DevOpenID=@DevOpenID Where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.AddInParameter(cmd, "@DevOpenID", DbType.String, entity.DevOpenID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改用户姓名和手机号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserNameCellphone(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set Name=@Name,Cellphone=@Cellphone Where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 解绑删除客户数据
        /// </summary>
        /// <param name="entity"></param>
        public void ClearWxUserData(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set Name='',Cellphone='',wxOpenID='',ClientNum=0 Where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        public void SubtractWxUserConsume(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set ConsumerPoint=ConsumerPoint-@ConsumerPoint Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@ConsumerPoint", DbType.Int32, entity.ConsumerPoint);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 更新用户的物流信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserLogicName(WXUserEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_WX_Client Set LogisID=@LogisID,LogicName=@LogicName Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@LogicName", DbType.String, entity.LogicName);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void UpdateWxUserLogicNameByClientNum(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set LogisID=@LogisID,LogicName=@LogicName Where ClientNum=@ClientNum";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@LogicName", DbType.String, entity.LogicName);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改微信 用户的上级人
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserParentID(WXUserEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_WX_Client Set ParentID=@ParentID Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@ParentID", DbType.Int64, entity.ParentID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 根据用户ID查询该用户的订单状态
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public WXOrderEntity QueryWeixinUserOrderInfo(Int64 userid)
        {
            WXOrderEntity result = new WXOrderEntity();
            result.UnPayNum = getUnPayNum(userid);
            result.UnSendNum = getUnSendNum(userid);
            result.UnConfirmNum = getUnConfirmNum(userid);
            result.UnAcceptNum = getUnAcceptNum(userid);
            return result;
        }
        private int getUnAcceptNum(long userid)
        {
            int result = 0;
            string strSQL = @"select COUNT(*) as Num from Tbl_WX_Order where (OrderStatus=2 or OrderStatus=3) and WXID=@WXID ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@WXID", DbType.Int64, userid);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    if (dd.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dd.Rows[0]["Num"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 待确认
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private int getUnConfirmNum(long userid)
        {
            int result = 0;
            string strSQL = @"select COUNT(*) as Num from Tbl_WX_Order where PayStatus='1' and OrderStatus='0' and WXID=@WXID ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@WXID", DbType.Int64, userid);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    if (dd.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dd.Rows[0]["Num"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 未发货
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private int getUnSendNum(long userid)
        {
            int result = 0;
            string strSQL = @"select COUNT(*) as Num from Tbl_WX_Order where PayStatus='1' and OrderStatus='1' and WXID=@WXID ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@WXID", DbType.Int64, userid);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    if (dd.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dd.Rows[0]["Num"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 未付款
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private int getUnPayNum(long userid)
        {
            int result = 0;
            string strSQL = @"select COUNT(*) as Num from Tbl_WX_Order where PayStatus='0' and WXID=@WXID ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@WXID", DbType.Int64, userid);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    if (dd.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dd.Rows[0]["Num"]);
                    }
                }
            }
            return result;
        }
        #endregion
        #region 微信服务号地址管理
        /// <summary>
        /// 查询用户地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXUserAddressEntity> QueryWxAddressByWXID(WXUserAddressEntity entity)
        {
            List<WXUserAddressEntity> result = new List<WXUserAddressEntity>();
            try
            {
                string strSQL = @"Select * from Tbl_WX_Address where WXID=@WXID Order by IsDefault desc, OP_DATE Desc";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new WXUserAddressEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                Province = Convert.ToString(idr["Province"]),
                                Country = Convert.ToString(idr["Country"]),
                                City = Convert.ToString(idr["City"]),
                                Name = Convert.ToString(idr["Name"]),
                                AddressType = Convert.ToString(idr["AddressType"]),
                                Address = Convert.ToString(idr["Address"]),
                                IsDefault = Convert.ToString(idr["IsDefault"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 微信用户地址管理
        /// </summary>
        /// <param name="entity"></param>
        public void AddAddress(WXUserAddressEntity entity)
        {
            entity.EnSafe();
            if (entity.IsDefault.Equals("1"))
            {
                SetAddressNotDefault(entity.WXID);
            }
            string strSQL = @"INSERT INTO Tbl_WX_Address(WXID,Province,City,Country,Address,Name,Cellphone,AddressType,IsDefault,OP_DATE) VALUES (@WXID,@Province,@City,@Country,@Address,@Name,@Cellphone,@AddressType,@IsDefault,@OP_DATE)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                    conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                    conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                    conn.AddInParameter(cmd, "@AddressType", DbType.String, entity.AddressType);
                    conn.AddInParameter(cmd, "@IsDefault", DbType.String, entity.IsDefault);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAddress(WXUserAddressEntity entity)
        {
            entity.EnSafe();
            if (entity.IsDefault.Equals("1"))
            {
                SetAddressNotDefault(entity.WXID);
            }
            string strSQL = @"UPDATE Tbl_WX_Address Set Province=@Province,City=@City,Country=@Country,Address=@Address,Cellphone=@Cellphone,Name=@Name,IsDefault=@IsDefault,OP_DATE=@OP_DATE Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                    conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                    conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                    conn.AddInParameter(cmd, "@AddressType", DbType.String, entity.AddressType);
                    conn.AddInParameter(cmd, "@IsDefault", DbType.String, entity.IsDefault);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 设置该用户所有地址为未默认地址
        /// </summary>
        /// <param name="wxid"></param>
        public void SetAddressNotDefault(Int64 wxid)
        {
            string strSQL = @"UPDATE Tbl_WX_Address Set IsDefault=@IsDefault,OP_DATE=@OP_DATE Where WXID=@WXID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, wxid);
                    conn.AddInParameter(cmd, "@IsDefault", DbType.String, "0");
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 设为默认地址
        /// </summary>
        /// <param name="addressid"></param>
        public void SetAddressDefault(Int64 addressid)
        {
            string strSQL = @"UPDATE Tbl_WX_Address Set IsDefault=@IsDefault,OP_DATE=@OP_DATE Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, addressid);
                    conn.AddInParameter(cmd, "@IsDefault", DbType.String, "1");
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteAddress(WXUserAddressEntity entity)
        {
            string strSQL = @"DELETE FROM Tbl_WX_Address Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 通过省市数据查询负责该省市的业务员账号信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXUserAddressEntity QueryWxAreaClient(WXUserAddressEntity entity)
        {
            WXUserAddressEntity result = new WXUserAddressEntity();
            string strSQL = "select * From Tbl_Cargo_AreaClient Where Province=@Province and City=@City";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    if (dd.Rows.Count > 0)
                    {
                        result.LoginName = Convert.ToString(dd.Rows[0]["LoginName"]);
                        result.UserName = Convert.ToString(dd.Rows[0]["UserName"]);
                        result.Province = Convert.ToString(dd.Rows[0]["Province"]);
                        result.wxOpenID = Convert.ToString(dd.Rows[0]["wxOpenID"]);
                        result.City = Convert.ToString(dd.Rows[0]["City"]);
                    }
                }
            }
            return result;
        }
        #endregion
        #region 积分管理
        /// <summary>
        /// 添加微信用户积分记录
        /// </summary>
        /// <param name="entity"></param>
        public void AddWeixinPoint(WXUserPointEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_WX_Point(WXID,Point,PointType,CutPoint,OP_DATE,WXOrderNo) VALUES (@WXID,@Point,@PointType,@CutPoint,@OP_DATE,@WXOrderNo)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    conn.AddInParameter(cmd, "@Point", DbType.Int32, entity.Point);
                    conn.AddInParameter(cmd, "@PointType", DbType.String, entity.PointType);
                    conn.AddInParameter(cmd, "@CutPoint", DbType.String, entity.CutPoint);
                    conn.AddInParameter(cmd, "@WXOrderNo", DbType.String, entity.WXOrderNo);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询用户的积分记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXUserPointEntity> QueryWXUserPoint(WXUserPointEntity entity)
        {
            List<WXUserPointEntity> result = new List<WXUserPointEntity>();
            try
            {
                string strSQL = @"Select * from Tbl_WX_Point where WXID=@WXID Order By OP_DATE desc";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new WXUserPointEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                Point = Convert.ToInt32(idr["Point"]),
                                PointType = Convert.ToString(idr["PointType"]),
                                CutPoint = Convert.ToString(idr["CutPoint"]),
                                WXOrderNo = Convert.ToString(idr["WXOrderNo"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 判断今天是否完成签到任务 
        /// </summary>
        /// <param name="wxid"></param>
        /// <returns></returns>
        public bool IsSign(WXUserPointEntity entity)
        {
            string strSQL = @"Select ID from Tbl_WX_Point Where PointType=@PointType and WXID=@WXID ";
            strSQL += " and OP_DATE>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            if (!string.IsNullOrEmpty(entity.WXOrderNo)) { strSQL += " and WXOrderNo='" + entity.WXOrderNo + "'"; }
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@PointType", DbType.String, entity.PointType);
                conn.AddInParameter(cmdQ, "@WXID", DbType.Int64, entity.WXID);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #region 商品管理
        /// <summary>
        /// 根据ID查询上架的商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CargoProductEntity QueryOnShelvesByID(Int64 id)
        {
            CargoProductEntity result = new CargoProductEntity();
            CargoPriceManager price = new CargoPriceManager();
            try
            {
                string strSQL = @" select a.ID,a.Title,a.OnSaleNum,a.Memo,a.FileName,a.Consume,a.ProductPrice,a.BatchShelveStatus,b.ProductID,b.ProductName,b.TypeID,b.Model,b.GoodsCode,b.Specs,b.Figure,b.HubDiameter,b.LoadIndex,b.SpeedLevel,b.TradePrice,b.SalePrice,b.Batch,b.HouseID,b.Source,b.BatchYear,b.BatchWeek,a.SaleType,ISNULL(c.Piece,0) as Piece,b.Assort,b.Born from Tbl_Cargo_Shelves as a inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID inner join Tbl_Cargo_ContainerGoods as c on a.ProductID=c.ProductID where a.ID=@ID";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, id);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            decimal sj = Convert.ToDecimal(idr["SalePrice"]);
                            if (Convert.ToString(idr["ShelveStatus"]).Equals("0"))
                            {
                                if (Convert.ToString(idr["SaleType"]).Equals("1") || Convert.ToString(idr["SaleType"]).Equals("3"))
                                {
                                    sj = Convert.ToDecimal(idr["ProductPrice"]);
                                }
                            }
                            string batch = Convert.ToString(idr["BatchYear"]) + Convert.ToString(idr["BatchWeek"]);
                            List<CargoRuleBankEntity> rule = price.QueryRuleBank(new CargoRuleBankEntity { HouseID = Convert.ToInt32(idr["HouseID"]), TypeID = Convert.ToInt32(idr["TypeID"]), Specs = Convert.ToString(idr["Specs"]), Figure = Convert.ToString(idr["Figure"]), DelFlag = "0", StartBatch = Convert.ToInt32(batch) });
                            CargoRuleBankEntity rb = new CargoRuleBankEntity();
                            if (rule.Count > 0)
                            {
                                rb = rule.Find(c => c.RuleType.Equals("6"));
                            }
                            result.ProductID = Convert.ToInt64(idr["ProductID"]);
                            result.OnSaleID = Convert.ToInt64(idr["ID"]);
                            result.Title = Convert.ToString(idr["Title"]);
                            result.Memo = Convert.ToString(idr["Memo"]);
                            result.FileName = Convert.ToString(idr["FileName"]);
                            result.OnSaleNum = Convert.ToInt32(idr["OnSaleNum"]);
                            result.ProductName = Convert.ToString(idr["ProductName"]);
                            result.TypeID = Convert.ToInt32(idr["TypeID"]);
                            result.Model = Convert.ToString(idr["Model"]);
                            result.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            result.Specs = Convert.ToString(idr["Specs"]);
                            result.Figure = Convert.ToString(idr["Figure"]);
                            result.HubDiameter = Convert.ToInt32(idr["HubDiameter"]);
                            result.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            result.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            result.TradePrice = Convert.ToDecimal(idr["TradePrice"]);
                            //result.SalePrice = Convert.ToInt32(idr["TypeID"]).Equals(66) || Convert.ToInt32(idr["HouseID"]).Equals(12) ? Convert.ToDecimal(idr["SalePrice"]) : Convert.ToInt32(idr["BatchYear"]).Equals(18) ? Convert.ToDecimal(Convert.ToDouble(idr["SalePrice"]) * 0.85) : Convert.ToDecimal(idr["SalePrice"]);
                            result.SalePrice = sj;
                            result.Batch = Convert.ToString(idr["Batch"]);
                            result.Source = Convert.ToString(idr["Source"]);
                            result.BatchYear = Convert.ToInt32(idr["BatchYear"]);
                            result.HouseID = Convert.ToInt32(idr["HouseID"]);
                            result.SaleType = Convert.ToString(idr["SaleType"]);
                            result.RealStockNum += Convert.ToInt32(idr["Piece"]);
                            result.Assort = Convert.ToString(idr["Assort"]);
                            result.Consume = Convert.ToInt32(idr["Consume"]);
                            result.Born = Convert.ToString(idr["Born"]);
                            result.CutEntry = rb.CutEntry;
                            result.IsCut = rb.ID.Equals(0) ? false : true;
                            result.RuleID = rb.ID;
                        }
                    }
                }

                result.fileList = QueryProductFile(id);
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

            return result;
        }

        private List<CargoProductFileEntity> QueryProductFile(Int64 id)
        {
            List<CargoProductFileEntity> result = new List<CargoProductFileEntity>();
            try
            {
                string strSQL = @"Select * from Tbl_Cargo_ProductFile where ShelvesID=@ShelvesID";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ShelvesID", DbType.Int64, id);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CargoProductFileEntity
                            {
                                FileName = Convert.ToString(idr["FileName"]),
                                FilePath = Convert.ToString(idr["FilePath"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                ShelvesID = Convert.ToInt64(idr["ShelvesID"]),
                                ID = Convert.ToInt64(idr["ID"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        #endregion
        #region 微信下单
        /// <summary>
        /// 保存微信订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void SaveWeixinOrder(WXOrderEntity entity)
        {
            entity.EnSafe();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_WX_Order(OrderNo,WXID,Piece,TotalCharge,CreateDate,PayStatus,PayWay,OrderStatus,OP_DATE,Province,City,Country,Address,Name,Cellphone,HouseID,LogisID,Memo,OrderType,SaleManID,TransitFee,CouponID,RuleTitle,IsAppFirstOrder,ThrowGood,SuppClientNum) VALUES (@OrderNo,@WXID,@Piece,@TotalCharge,@CreateDate,@PayStatus,@PayWay,@OrderStatus,@OP_DATE,@Province,@City,@Country,@Address,@Name,@Cellphone,@HouseID,@LogisID,@Memo,@OrderType,@SaleManID,@TransitFee,@CouponID,@RuleTitle,@IsAppFirstOrder,@ThrowGood,@SuppClientNum) SELECT @@IDENTITY";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@PayStatus", DbType.String, entity.PayStatus);
                    conn.AddInParameter(cmd, "@PayWay", DbType.String, entity.PayWay);
                    conn.AddInParameter(cmd, "@OrderStatus", DbType.String, entity.OrderStatus);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                    conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                    conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                    conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@Memo", DbType.String, entity.Memo);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@CouponID", DbType.Int64, entity.CouponID);
                    conn.AddInParameter(cmd, "@RuleTitle", DbType.String, entity.RuleTitle);
                    conn.AddInParameter(cmd, "@IsAppFirstOrder", DbType.String, entity.IsAppFirstOrder);
                    conn.AddInParameter(cmd, "@ThrowGood", DbType.String, entity.ThrowGood);
                    conn.AddInParameter(cmd, "@SuppClientNum", DbType.Int32, entity.SuppClientNum);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                foreach (var it in entity.productList)
                {
                    //CargoProductShelvesEntity shel = QueryProductShelvesByID(it.ID);
                    if (entity.isReserve)
                    {
                        AddWeixinReserveOrderProduct(new CargoProductShelvesEntity { OrderID = did, ID = it.ID, OrderNum = it.OrderNum, OrderPrice = it.OrderPrice, ModifyPrice = it.ModifyPrice, CutEntry = it.CutEntry, ProductCode = it.ProductCode, Batch = it.Batch, BatchYear = it.BatchYear, RuleID = it.RuleID });
                    }
                    else
                    {
                        AddWeixinOrderProduct(new CargoProductShelvesEntity { OrderID = did, ID = it.ID, OrderNum = it.OrderNum, OrderPrice = it.OrderPrice, ModifyPrice = it.ModifyPrice, CutEntry = it.CutEntry, ProductCode = it.ProductCode, Batch = it.Batch, BatchYear = it.BatchYear, RuleID = it.RuleID });
                    }
                    //修改上架产品数量
                    if (!it.ID.Equals(0))
                    {
                        UpdateOnShelvesNum(new CargoProductShelvesEntity { ID = it.ID, OnSaleNum = it.OrderNum });
                    }
                }
                //AddWeixinPoint(new WXUserPointEntity { WXID = did, Point = entity.ConsumerPoint, CutPoint = "0", PointType = "1" });
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void AddWeixinOrderProduct(CargoProductShelvesEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_WX_OrderProduct(OrderID,ShelvesID,OrderNum,OrderPrice,OP_DATE,ProductID,ModifyPrice,CutEntry,ProductCode,Batch,BatchYear,RuleID) VALUES (@OrderID,@ShelvesID,@OrderNum,@OrderPrice,@OP_DATE,@ProductID,@ModifyPrice,@CutEntry,@ProductCode,@Batch,@BatchYear,@RuleID)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@ShelvesID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                    conn.AddInParameter(cmd, "@OrderPrice", DbType.Decimal, entity.OrderPrice);
                    conn.AddInParameter(cmd, "@ModifyPrice", DbType.Decimal, entity.ModifyPrice);
                    conn.AddInParameter(cmd, "@CutEntry", DbType.Int32, entity.CutEntry);
                    conn.AddInParameter(cmd, "@ProductCode", DbType.String, entity.ProductCode);
                    conn.AddInParameter(cmd, "@Batch", DbType.String, entity.Batch);
                    conn.AddInParameter(cmd, "@BatchYear", DbType.Int32, entity.BatchYear);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@RuleID", DbType.Int32, entity.RuleID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void AddWeixinReserveOrderProduct(CargoProductShelvesEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_WX_ReserveOrderProduct(OrderID,ShelvesID,OrderNum,OrderPrice,OP_DATE,ModifyPrice,CutEntry,ProductCode,Batch,BatchYear) VALUES (@OrderID,@ShelvesID,@OrderNum,@OrderPrice,@OP_DATE,@ModifyPrice,@CutEntry,@ProductCode,@Batch,@BatchYear)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@ShelvesID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                    conn.AddInParameter(cmd, "@OrderPrice", DbType.Decimal, entity.OrderPrice);
                    conn.AddInParameter(cmd, "@ModifyPrice", DbType.Decimal, entity.ModifyPrice);
                    conn.AddInParameter(cmd, "@CutEntry", DbType.Int32, entity.CutEntry);
                    conn.AddInParameter(cmd, "@ProductCode", DbType.String, entity.ProductCode);
                    conn.AddInParameter(cmd, "@Batch", DbType.String, entity.Batch);
                    conn.AddInParameter(cmd, "@BatchYear", DbType.Int32, entity.BatchYear);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public CargoProductShelvesEntity QueryProductShelvesByID(long shelveID)
        {
            CargoProductShelvesEntity result = new CargoProductShelvesEntity();
            string strSQL = "select * From Tbl_Cargo_Shelves where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, shelveID);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.ID = Convert.ToInt64(idr["ID"]);
                        result.ProductID = Convert.ToInt64(idr["ProductID"]);
                        result.Consume = Convert.ToInt32(idr["Consume"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 修改上架产品的数量 
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateOnShelvesNum(CargoProductShelvesEntity entity)
        {
            try
            {
                string strSQL = @"UPDATE Tbl_Cargo_Shelves SET OnSaleNum=OnSaleNum-@OnSaleNum,OP_DATE=@OP_DATE Where ID=@ID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@OnSaleNum", DbType.Int32, entity.OnSaleNum);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void UpdateOnShelvesNumAdd(CargoProductShelvesEntity entity)
        {
            try
            {
                string strSQL = @"UPDATE Tbl_Cargo_Shelves SET OnSaleNum=OnSaleNum+@OnSaleNum,OP_DATE=@OP_DATE Where ID=@ID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@OnSaleNum", DbType.Int32, entity.OnSaleNum);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改微信订单支付状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWeixinOrderPayStatus(WXOrderEntity entity)
        {
            string strSQL = @"Update Tbl_WX_Order Set WXPayOrderNo=@WXPayOrderNo,PayStatus=@PayStatus ,Trxid=@Trxid where OrderNo=@OrderNo";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@WXPayOrderNo", DbType.String, entity.WXPayOrderNo);
                conn.AddInParameter(cmd, "@PayStatus", DbType.String, entity.PayStatus);
                conn.AddInParameter(cmd, "@Trxid", DbType.String, entity.Trxid);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改预订单支付状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateReserveOrderStatus(CargoReserveOrderEntity entity)
        {
            string strSQL = @"update Tbl_Cargo_ReserveOrder set CheckStatus=@CheckStatus ";
            if (Convert.ToInt32(entity.CheckStatus) == 2)
            {
                strSQL += $@",CheckDate=@CheckDate";
            }
            strSQL += $@" where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@CheckStatus", DbType.Int32, Convert.ToInt32(entity.CheckStatus));
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                if (Convert.ToInt32(entity.CheckStatus) == 2)
                {
                    conn.AddInParameter(cmd, "@CheckDate", DbType.DateTime, DateTime.Now);
                }
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 判断微信订单是否支付成功
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistWeixinOrderPay(WXOrderEntity entity)
        {
            string strQ = @"Select top 1 ID from Tbl_WX_Order Where (1=1)";
            if (!string.IsNullOrEmpty(entity.OrderNo))
            {
                strQ += " and OrderNo=@OrderNo";
            }
            if (!string.IsNullOrEmpty(entity.PayStatus))
            {
                strQ += " and PayStatus=@PayStatus";
            }
            if (!string.IsNullOrEmpty(entity.OrderType))
            {
                strQ += " and OrderType=@OrderType";
            }
            if (!entity.WXID.Equals(0))
            {
                strQ += " and WXID=@WXID";
            }
            using (DbCommand cmd = conn.GetSqlStringCommond(strQ))
            {
                if (!string.IsNullOrEmpty(entity.OrderNo))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                }
                if (!string.IsNullOrEmpty(entity.PayStatus))
                {
                    conn.AddInParameter(cmd, "@PayStatus", DbType.String, entity.PayStatus);
                }
                if (!string.IsNullOrEmpty(entity.OrderType))
                {
                    conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                }
                if (!entity.WXID.Equals(0))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                }
                using (DataTable idr = conn.ExecuteDataTable(cmd))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public List<WXOrderEntity> QueryWeixinOrderInfo(int pIndex, int pNum, WXOrderEntity entity)
        {
            List<WXOrderEntity> result = new List<WXOrderEntity>();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,b.OrderNo as CargoOrderNo,c.wxOpenID,ISNULL(b.OrderID,0) as OrderID,c.ClientNum ";
                if (entity.isReserve)
                {
                    strSQL += " ,'' as OpenOrderNo,0 as ShareHouseID,isnull(b.ActualAmounts,0) as ActualAmounts,isnull(a2.PaymentAmount,0) as PaymentAmountSum From Tbl_WX_Order as a left join Tbl_Cargo_ReserveOrder as b on a.OrderNo=b.WXOrderNo left join (select OrderNo,WxOrderNo,sum(PaymentAmount) as PaymentAmount from Tbl_Cargo_ReserveOrderPaymentRecord group by OrderNo,WxOrderNo) as a2 on a.OrderNo=a2.WxOrderNo and b.OrderNo=a2.OrderNo";
                }
                else
                {
                    strSQL += " ,b.OpenOrderNo,b.ShareHouseID,0 as ActualAmounts,0 as PaymentAmountSum From Tbl_WX_Order as a left join Tbl_Cargo_Order as b on a.OrderNo=b.WXOrderNo";
                }
                strSQL += " inner join Tbl_WX_Client as c on a.WXID=c.ID Where (1=1) ";
                //微信用户ID
                if (!entity.WXID.Equals(0)) { strSQL += " and a.WXID = " + entity.WXID + ""; }
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
                //付款方式
                if (!string.IsNullOrEmpty(entity.PayWay)) { strSQL += " and a.PayWay = '" + entity.PayWay + "'"; }
                //订单付款状态
                if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and a.PayStatus = '" + entity.PayStatus + "'"; }
                if ((entity.CreateDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.CreateDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.CreateDate.ToString("yyyy-MM-dd") + "'";
                }
                //订单状态方式
                if (!string.IsNullOrEmpty(entity.OrderStatus))
                {
                    strSQL += " and a.OrderStatus = '" + entity.OrderStatus + "'";
                    //if (entity.OrderStatus.Equals("0"))
                    //{
                    //    strSQL += " and a.OrderStatus = '0'";
                    //}
                    //else if (entity.OrderStatus.Equals("1"))
                    //{
                    //    strSQL += " an a.OrderStatus='1'";
                    //}
                    //else if (entity.OrderStatus.Equals("2"))
                    //{
                    //    strSQL += " and (a.OrderStatus='2' or a.OrderStatus='3') ";
                    //}
                }
                if (!entity.HouseID.Equals(0))
                {
                    strSQL += " and a.HouseID=" + entity.HouseID + "";
                }
                if (!string.IsNullOrEmpty(entity.AccountNo)) { strSQL += " and a.AccountNo='" + entity.AccountNo + "'"; }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            List<CargoProductShelvesEntity> pro = queryInShelvesProduct(Convert.ToInt64(idr["ID"]));
                            #region 获取运单数据

                            var ActualAmounts = Convert.ToDecimal(idr["ActualAmounts"]);
                            var TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                            if (ActualAmounts > 0)
                            {
                                var PaymentAmountSum = Convert.ToDecimal(idr["PaymentAmountSum"]);
                                TotalCharge = ActualAmounts - PaymentAmountSum;
                            }

                            result.Add(new WXOrderEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                CargoOrderNo = Convert.ToString(idr["CargoOrderNo"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                wxOpenID = Convert.ToString(idr["wxOpenID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                Name = Convert.ToString(idr["Name"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                Address = Convert.ToString(idr["Address"]),
                                Province = Convert.ToString(idr["Province"]),
                                City = Convert.ToString(idr["City"]),
                                Country = Convert.ToString(idr["Country"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TotalCharge = TotalCharge,
                                PayWay = Convert.ToString(idr["PayWay"]).Trim(),
                                PayStatus = Convert.ToString(idr["PayStatus"]).Trim(),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]).Trim(),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]).Trim(),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]).Trim(),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                AccountNo = Convert.ToString(idr["AccountNo"]),
                                IsAppFirstOrder = Convert.ToString(idr["IsAppFirstOrder"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OpenOrderNo = Convert.ToString(idr["OpenOrderNo"]),
                                ShareHouseID = string.IsNullOrEmpty(Convert.ToString(idr["ShareHouseID"])) ? 0 : Convert.ToInt32(idr["ShareHouseID"]),
                                TotalConsume = pro.Sum(c => c.Consume * c.OrderNum),
                                productList = pro
                            });
                            #endregion
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public List<WXOrderEntity> QueryWeixinReserveOrderInfo(int pIndex, int pNum, WXOrderEntity entity)
        {
            List<WXOrderEntity> result = new List<WXOrderEntity>();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber, a.*, b.OrderNo as CargoOrderNo, c.wxOpenID, ISNULL(b.OrderID, 0) as OrderID, c.ClientNum,isnull(b.PaymentType,0) as PaymentType From Tbl_WX_Order as a left join Tbl_Cargo_ReserveOrder as b on a.OrderNo = b.WXOrderNo inner join Tbl_WX_Client as c on a.WXID = c.ID Where (1 = 1) ";
                //微信用户ID
                if (!entity.WXID.Equals(0)) { strSQL += " and a.WXID = " + entity.WXID + ""; }
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
                //付款方式
                if (!string.IsNullOrEmpty(entity.PayWay)) { strSQL += " and a.PayWay = '" + entity.PayWay + "'"; }
                //订单付款状态
                if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and a.PayStatus = '" + entity.PayStatus + "'"; }
                if ((entity.CreateDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.CreateDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.CreateDate.ToString("yyyy-MM-dd") + "'";
                }
                //订单状态方式
                if (!string.IsNullOrEmpty(entity.OrderStatus))
                {
                    strSQL += " and a.OrderStatus = '" + entity.OrderStatus + "'";
                    //if (entity.OrderStatus.Equals("0"))
                    //{
                    //    strSQL += " and a.OrderStatus = '0'";
                    //}
                    //else if (entity.OrderStatus.Equals("1"))
                    //{
                    //    strSQL += " an a.OrderStatus='1'";
                    //}
                    //else if (entity.OrderStatus.Equals("2"))
                    //{
                    //    strSQL += " and (a.OrderStatus='2' or a.OrderStatus='3') ";
                    //}
                }
                if (!entity.HouseID.Equals(0))
                {
                    strSQL += " and a.HouseID=" + entity.HouseID + "";
                }
                if (!string.IsNullOrEmpty(entity.AccountNo)) { strSQL += " and a.AccountNo='" + entity.AccountNo + "'"; }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            List<CargoProductShelvesEntity> pro = queryInShelvesProduct(Convert.ToInt64(idr["ID"]));
                            #region 获取运单数据

                            result.Add(new WXOrderEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                PaymentType = Convert.ToString(idr["PaymentType"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                CargoOrderNo = Convert.ToString(idr["CargoOrderNo"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                wxOpenID = Convert.ToString(idr["wxOpenID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                Name = Convert.ToString(idr["Name"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                Address = Convert.ToString(idr["Address"]),
                                Province = Convert.ToString(idr["Province"]),
                                City = Convert.ToString(idr["City"]),
                                Country = Convert.ToString(idr["Country"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                PayWay = Convert.ToString(idr["PayWay"]).Trim(),
                                PayStatus = Convert.ToString(idr["PayStatus"]).Trim(),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]).Trim(),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]).Trim(),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]).Trim(),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                AccountNo = Convert.ToString(idr["AccountNo"]),
                                IsAppFirstOrder = Convert.ToString(idr["IsAppFirstOrder"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                TotalConsume = pro.Sum(c => c.Consume * c.OrderNum),
                                productList = pro
                            });
                            #endregion
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        private List<CargoProductShelvesEntity> queryInShelvesProduct(long orderID)
        {
            List<CargoProductShelvesEntity> result = new List<CargoProductShelvesEntity>();
            try
            {
                string strSQL = @"select a.OrderID,a.ShelvesID,a.OrderNum,a.OrderPrice,a.Batch,a.BatchYear,a.RuleID,c.ProductName,c.TypeID,c.Model,c.GoodsCode,c.Specs,c.Figure,c.TreadWidth,c.HubDiameter,c.LoadIndex,c.SpeedLevel,d.TypeName from Tbl_WX_OrderProduct as a inner join Tbl_Cargo_ProductSpec as c on a.ProductCode=c.ProductCode inner join Tbl_Cargo_ProductType as d on c.TypeID=d.TypeID where  a.OrderID=@OrderID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, orderID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            //if (Convert.ToInt64(idr["ShelvesID"]).Equals(0)) { break; }
                            //string batch = Convert.ToString(idr["BatchYear"]) + Convert.ToString(idr["BatchWeek"]);
                            //List<CargoRuleBankEntity> rule = price.QueryRuleBank(new CargoRuleBankEntity { HouseID = Convert.ToInt32(idr["HouseID"]), TypeID = Convert.ToInt32(idr["TypeID"]), Specs = Convert.ToString(idr["Specs"]), Figure = Convert.ToString(idr["Figure"]), DelFlag = "0", StartBatch = Convert.ToInt32(batch) });
                            //CargoRuleBankEntity rb = new CargoRuleBankEntity();
                            //if (rule.Count > 0)
                            //{
                            //    rb = rule.Find(c => c.RuleType.Equals("6"));
                            //}
                            result.Add(new CargoProductShelvesEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                ID = Convert.ToInt64(idr["ShelvesID"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                                //ProductID = Convert.ToInt64(idr["ProductID"]),
                                //Title = Convert.ToString(idr["Title"]),
                                //FileName = Convert.ToString(idr["FileName"]),
                                ProductName = Convert.ToString(idr["TypeName"]) + " " + Convert.ToString(idr["Specs"]) + " " + Convert.ToString(idr["Figure"]) + " " + Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                Model = Convert.ToString(idr["Model"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                TreadWidth = Convert.ToInt32(idr["TreadWidth"]),
                                HubDiameter = Convert.ToInt32(idr["HubDiameter"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                RuleID = string.IsNullOrEmpty(Convert.ToString(idr["RuleID"])) ? 0 : Convert.ToInt32(idr["RuleID"]),
                                //TradePrice = Convert.ToDecimal(idr["TradePrice"]),
                                //SaleType = Convert.ToString(idr["SaleType"]),
                                //Consume = Convert.ToInt32(idr["Consume"]),
                                //CutEntry = rb.CutEntry,
                                //IsCut = rb.ID.Equals(0) ? false : true,
                                //RuleID = rb.ID,
                                //SalePrice = Convert.ToDecimal(idr["SalePrice"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 根据订单ID查询该订单的产品数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        //private List<CargoProductShelvesEntity> queryInShelvesProduct(long orderID)
        //{
        //    CargoPriceManager price = new CargoPriceManager();
        //    List<CargoProductShelvesEntity> result = new List<CargoProductShelvesEntity>();
        //    try
        //    {
        //        string strSQL = @"select a.OrderID,a.ShelvesID,a.OrderNum,a.OrderPrice,b.ProductID,b.Title,b.FileName,b.SaleType,b.Consume,c.ProductName,c.TypeID,c.Model,c.GoodsCode,c.Specs,c.Figure,c.TreadWidth,c.HubDiameter,c.LoadIndex,c.SpeedLevel,c.TradePrice,c.SalePrice,c.HouseID,c.BatchYear,c.BatchWeek from Tbl_WX_OrderProduct as a left join Tbl_Cargo_Shelves as b on a.ShelvesID=b.ID left join Tbl_Cargo_Product as c on b.ProductID=c.ProductID where a.OrderID=@OrderID";

        //        using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
        //        {
        //            conn.AddInParameter(cmd, "@OrderID", DbType.Int64, orderID);
        //            using (DataTable dd = conn.ExecuteDataTable(cmd))
        //            {
        //                foreach (DataRow idr in dd.Rows)
        //                {
        //                    if (Convert.ToInt64(idr["ShelvesID"]).Equals(0)) { break; }
        //                    string batch = Convert.ToString(idr["BatchYear"]) + Convert.ToString(idr["BatchWeek"]);
        //                    List<CargoRuleBankEntity> rule = price.QueryRuleBank(new CargoRuleBankEntity { HouseID = Convert.ToInt32(idr["HouseID"]), TypeID = Convert.ToInt32(idr["TypeID"]), Specs = Convert.ToString(idr["Specs"]), Figure = Convert.ToString(idr["Figure"]), DelFlag = "0", StartBatch = Convert.ToInt32(batch) });
        //                    CargoRuleBankEntity rb = new CargoRuleBankEntity();
        //                    if (rule.Count > 0)
        //                    {
        //                        rb = rule.Find(c => c.RuleType.Equals("6"));
        //                    }
        //                    result.Add(new CargoProductShelvesEntity
        //                    {
        //                        OrderID = Convert.ToInt64(idr["OrderID"]),
        //                        ID = Convert.ToInt64(idr["ShelvesID"]),
        //                        OrderNum = Convert.ToInt32(idr["OrderNum"]),
        //                        OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
        //                        ProductID = Convert.ToInt64(idr["ProductID"]),
        //                        Title = Convert.ToString(idr["Title"]),
        //                        FileName = Convert.ToString(idr["FileName"]),
        //                        ProductName = Convert.ToString(idr["ProductName"]),
        //                        TypeID = Convert.ToInt32(idr["TypeID"]),
        //                        Model = Convert.ToString(idr["Model"]),
        //                        GoodsCode = Convert.ToString(idr["GoodsCode"]),
        //                        Specs = Convert.ToString(idr["Specs"]),
        //                        Figure = Convert.ToString(idr["Figure"]),
        //                        TreadWidth = Convert.ToInt32(idr["TreadWidth"]),
        //                        HubDiameter = Convert.ToInt32(idr["HubDiameter"]),
        //                        LoadIndex = Convert.ToString(idr["LoadIndex"]),
        //                        SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
        //                        TradePrice = Convert.ToDecimal(idr["TradePrice"]),
        //                        SaleType = Convert.ToString(idr["SaleType"]),
        //                        Consume = Convert.ToInt32(idr["Consume"]),
        //                        CutEntry = rb.CutEntry,
        //                        IsCut = rb.ID.Equals(0) ? false : true,
        //                        RuleID = rb.ID,
        //                        SalePrice = Convert.ToDecimal(idr["SalePrice"])
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        //    return result;
        //}
        public List<CargoProductShelvesEntity> QueryWeixinOrderProductInfo(long orderID)
        {
            List<CargoProductShelvesEntity> result = new List<CargoProductShelvesEntity>();
            try
            {
                string strSQL = @"select * from Tbl_WX_OrderProduct where OrderID=@OrderID";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, orderID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CargoProductShelvesEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                ID = Convert.ToInt64(idr["ShelvesID"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                OnSaleNum = Convert.ToInt32(idr["OrderNum"]),
                                OrderPrice = Convert.ToDecimal(idr["OrderPrice"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 确认收货修改状态
        /// </summary>
        /// <param name="entity"></param>
        public void setWeixinOrderOk(WXOrderEntity entity)
        {
            string strSQL = @"Update Tbl_WX_Order Set OrderStatus=@OrderStatus ";
            if (!string.IsNullOrEmpty(entity.OutHouseName))
            {
                strSQL += ",OutHouseName=@OutHouseName";
            }
            strSQL += " where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                if (!string.IsNullOrEmpty(entity.OutHouseName))
                {
                    conn.AddInParameter(cmd, "@OutHouseName", DbType.String, entity.OutHouseName);
                }
                conn.AddInParameter(cmd, "@OrderStatus", DbType.String, entity.OrderStatus);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 订单评价
        /// </summary>
        /// <param name="entity"></param>
        public void setOrderEvaluate(WXOrderEntity entity)
        {
            string strSQL = @"Update Tbl_WX_Order Set GoodEvaluate=@GoodEvaluate,LogisEvaluate=@LogisEvaluate,EvaluateMemo=@EvaluateMemo where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@GoodEvaluate", DbType.String, entity.GoodEvaluate);
                conn.AddInParameter(cmd, "@LogisEvaluate", DbType.String, entity.LogisEvaluate);
                conn.AddInParameter(cmd, "@EvaluateMemo", DbType.String, entity.EvaluateMemo);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteWeixinOrder(WXOrderEntity entity)
        {
            string strSQL = @"Delete From Tbl_WX_Order where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.ExecuteNonQuery(cmd);
            }
            var field = entity.isReserve ? "Tbl_WX_ReserveOrderProduct" : "Tbl_WX_OrderProduct";
            string strDel = $@"Delete From {field} where OrderID=@OrderID";
            using (DbCommand cmdDel = conn.GetSqlStringCommond(strDel))
            {
                conn.AddInParameter(cmdDel, "@OrderID", DbType.Int64, entity.ID);
                conn.ExecuteNonQuery(cmdDel);
            }
        }
        /// <summary>
        /// 通过微信订单号查询订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXOrderEntity QueryWeixinOrderByOrderNo(WXOrderEntity entity)
        {
            WXOrderEntity result = new WXOrderEntity();
            string strSQL = @"Select a.*,b.OrderNo as CargoOrderNo,b.OutHouseName From Tbl_WX_Order as a left join tbl_Cargo_order as b on a.OrderNo=b.WxOrderNo where (1=1)";
            if (!entity.ID.Equals(0)) { strSQL += " and a.ID=@ID"; }
            if (!entity.WXID.Equals(0)) { strSQL += " and a.WXID=@WXID"; }
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo=@OrderNo"; }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                if (!entity.WXID.Equals(0)) { conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID); }
                if (!entity.ID.Equals(0)) { conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID); }
                if (!string.IsNullOrEmpty(entity.OrderNo)) { conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo); }
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.ID = Convert.ToInt64(idr["ID"]);
                        result.OrderNo = Convert.ToString(idr["OrderNo"]);
                        result.CargoOrderNo = Convert.ToString(idr["CargoOrderNo"]);
                        result.WXID = Convert.ToInt64(idr["WXID"]);
                        result.Piece = Convert.ToInt32(idr["Piece"]);
                        result.TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                        result.CreateDate = Convert.ToDateTime(idr["CreateDate"]);
                        result.PayStatus = Convert.ToString(idr["PayStatus"]);
                        result.OrderStatus = Convert.ToString(idr["OrderStatus"]);
                        result.PayWay = Convert.ToString(idr["PayWay"]);
                        result.ThrowGood = Convert.ToString(idr["ThrowGood"]);
                        result.OutHouseName = Convert.ToString(idr["OutHouseName"]);
                        //result.RefundReason = Convert.ToString(idr["RefundReason"]);
                        //result.RefundMemo = Convert.ToString(idr["RefundMemo"]);
                        result.WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]);
                        result.Trxid = Convert.ToString(idr["Trxid"]);
                        result.SuppClientNum = Convert.ToInt32(idr["SuppClientNum"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 修改客户的信用额度并修改微信商城订单的支付状态 
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateClientLimitAndWxOrderPayStatus(WXOrderEntity entity)
        {
            string strSQL = "Update Tbl_Cargo_Client set LimitMoney=LimitMoney-@LimitMoney where ClientNum=@ClientNum ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@LimitMoney", DbType.Decimal, entity.TotalCharge);
                conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                conn.ExecuteNonQuery(cmd);
            }
            string wxSQL = @"Update Tbl_WX_Order Set PayStatus=@PayStatus,PayWay=@PayWay where OrderNo=@OrderNo and WXID=@WXID";

            using (DbCommand cmd = conn.GetSqlStringCommond(wxSQL))
            {
                conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                conn.AddInParameter(cmd, "@PayStatus", DbType.String, entity.PayStatus);
                conn.AddInParameter(cmd, "@PayWay", DbType.String, entity.PayWay);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改客户的额度
        /// </summary>
        /// <param name="LimitMoney">金额</param>
        /// <param name="ClientNum">客户编码</param>
        /// <param name="isAdd">0：加  1：减</param>
        public void UpdateClientLimitMoney(decimal LimitMoney, int ClientNum, string isAdd)
        {
            string strSQL = "Update Tbl_Cargo_Client set ";
            if (isAdd.Equals("0"))
            {
                //加
                strSQL += " LimitMoney=LimitMoney+@LimitMoney";
            }
            else
            {
                //减
                strSQL += " LimitMoney=LimitMoney-@LimitMoney";
            }
            strSQL += " where ClientNum=@ClientNum ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@LimitMoney", DbType.Decimal, LimitMoney);
                conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, ClientNum);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改商城订单总金额
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderTotalCharge(WXOrderEntity entity)
        {
            string strSQL = "Update Tbl_WX_Order set TotalCharge=@TotalCharge where OrderNo=@OrderNo ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改商城订单总金额
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderTotalChargeByID(WXOrderEntity entity)
        {
            string strSQL = "Update Tbl_WX_Order set TotalCharge=TotalCharge-@TotalCharge where ID=@ID ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改绑定客户的微信商城订单的账单号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderAccountByID(List<WXOrderEntity> entity)
        {
            foreach (var it in entity)
            {
                it.EnSafe();
                string strSQL = "Update Tbl_WX_Order set AccountNo=@AccountNo where ID=@ID ";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@AccountNo", DbType.String, it.AccountNo);
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, it.ID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
        }
        /// <summary>
        /// 查询某个用户的所有微信商城订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXOrderManagerEntity> QueryWxOrderInfo(WXOrderManagerEntity entity)
        {
            List<WXOrderManagerEntity> result = new List<WXOrderManagerEntity>();
            try
            {
                string strSQL = "select b.ID,b.OrderNo as WXOrderNo,b.Piece,b.TotalCharge,b.CreateDate,b.PayStatus,b.OrderStatus,b.Province,b.City,b.Country,b.Address,b.HouseID,b.PayWay,b.OutHouseName,c.OrderNo,c.CheckStatus From Tbl_WX_Order as b left join Tbl_Cargo_Order as c on b.OrderNo=c.WXOrderNo where b.WXID=@WXID Order by b.CreateDate desc";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new WXOrderManagerEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                WXOrderNo = Convert.ToString(idr["WXOrderNo"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                PayStatus = Convert.ToString(idr["PayStatus"]),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]),
                                Province = Convert.ToString(idr["Province"]),
                                City = Convert.ToString(idr["City"]),
                                Country = Convert.ToString(idr["Country"]),
                                Address = Convert.ToString(idr["Address"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                PayWay = Convert.ToString(idr["PayWay"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询客户当前月的订单情况 计算可以购买特价轮胎数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderGoodsEntity> QueryClientOrderData(CargoOrderEntity entity)
        {
            List<CargoOrderGoodsEntity> result = new List<CargoOrderGoodsEntity>();
            string strSQL = "select a.OrderModel,b.OrderNo,b.ProductID,b.Piece,b.RelateOrderNo,case when ISNULL(c.ID,0)=0 then 0 else 1 end as SpecialID  from Tbl_Cargo_Order as a inner join Tbl_Cargo_OrderGoods as b on a.OrderNo=b.OrderNo inner join Tbl_Cargo_Product as d on b.ProductID=d.ProductID left join (select ID,ProductID From Tbl_Cargo_Shelves where SaleType='" + entity.SaleType + "' ) as c on b.ProductID=c.ProductID where (1=1) and a.OrderModel='0'";
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID=" + entity.HouseID + ""; }
            if (!entity.ClientNum.Equals(0)) { strSQL += " and a.ClientNum=" + entity.ClientNum + ""; }
            if (!entity.TypeID.Equals(0))
            {
                if (entity.TypeID.Equals(9))
                {
                    strSQL += " and d.TypeID=9 and d.Assort='REP' ";
                }
                else
                {
                    strSQL += " and d.TypeID=18 ";
                }
            }

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new CargoOrderGoodsEntity
                        {
                            OrderModel = Convert.ToString(idr["OrderModel"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            ProductID = Convert.ToInt64(idr["ProductID"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            RelateOrderNo = Convert.ToString(idr["RelateOrderNo"]),
                            SpecialID = Convert.ToInt32(idr["SpecialID"])
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询推广规格上架产品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoProductShelvesEntity> QueryShelvesData(CargoProductShelvesEntity entity)
        {
            CargoPriceManager price = new CargoPriceManager();
            List<CargoProductShelvesEntity> result = new List<CargoProductShelvesEntity>();
            string strSQL = "select a.*,b.Specs,b.Figure,b.LoadIndex,b.SpeedLevel,b.SalePrice,b.TradePrice,b.HouseID,b.BatchYear,b.BatchWeek from Tbl_Cargo_Shelves as a inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID  where (1=1) ";
            if (!entity.HouseID.Equals(0)) { strSQL += " and b.HouseID=" + entity.HouseID + ""; }
            if (!string.IsNullOrEmpty(entity.Title)) { strSQL += " and a.Title like '%" + entity.Title + "%'"; }
            if (!string.IsNullOrEmpty(entity.SaleType)) { strSQL += " and a.SaleType='" + entity.SaleType + "'"; }
            if (!string.IsNullOrEmpty(entity.ShelveStatus)) { strSQL += " and a.ShelveStatus='" + entity.ShelveStatus + "'"; }
            strSQL += " order by a.AdvertEndDate desc";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        decimal sj = Convert.ToDecimal(idr["SalePrice"]);
                        if (Convert.ToString(idr["ShelveStatus"]).Equals("0"))
                        {
                            if (Convert.ToString(idr["SaleType"]).Equals("1") || Convert.ToString(idr["SaleType"]).Equals("3"))
                            {
                                sj = Convert.ToDecimal(idr["ProductPrice"]);
                            }
                        }
                        string batch = Convert.ToString(idr["BatchYear"]) + Convert.ToString(idr["BatchWeek"]);
                        List<CargoRuleBankEntity> rule = price.QueryRuleBank(new CargoRuleBankEntity { HouseID = Convert.ToInt32(idr["HouseID"]), TypeID = Convert.ToInt32(idr["TypeID"]), Specs = Convert.ToString(idr["Specs"]), Figure = Convert.ToString(idr["Figure"]), DelFlag = "0", StartBatch = Convert.ToInt32(batch) });
                        CargoRuleBankEntity rb = new CargoRuleBankEntity();
                        if (rule.Count > 0)
                        {
                            rb = rule.Find(c => c.RuleType.Equals("6"));
                        }
                        result.Add(new CargoProductShelvesEntity
                        {
                            ID = Convert.ToInt64(idr["ID"]),
                            ProductID = Convert.ToInt64(idr["ProductID"]),
                            TypeID = Convert.ToInt32(idr["TypeID"]),
                            OnSaleNum = Convert.ToInt32(idr["OnSaleNum"]),
                            Title = Convert.ToString(idr["Title"]),
                            Memo = Convert.ToString(idr["Memo"]),
                            FileName = Convert.ToString(idr["FileName"]),
                            SaleType = Convert.ToString(idr["SaleType"]),
                            ShelveStatus = Convert.ToString(idr["ShelveStatus"]),
                            AdvertStartDate = string.IsNullOrEmpty(Convert.ToString(idr["AdvertStartDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["AdvertStartDate"]),
                            AdvertEndDate = string.IsNullOrEmpty(Convert.ToString(idr["AdvertEndDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["AdvertEndDate"]),
                            Consume = Convert.ToInt32(idr["Consume"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            SalePrice = sj,
                            CutEntry = rb.CutEntry,
                            IsCut = rb.ID.Equals(0) ? false : true,
                            RuleID = rb.ID,
                            TradePrice = Convert.ToDecimal(idr["TradePrice"])
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询客户本月的特价轮胎数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int QueryPurchaseNum(WXOrderEntity entity)
        {
            int result = 0;
            string strSQL = "select ISNULL(SUM(b.OrderNum),0) as OrderNum From Tbl_Cargo_Shelves as a inner join Tbl_WX_OrderProduct as b on a.ID=b.ShelvesID inner join Tbl_WX_Order as c on b.OrderID=c.ID where  a.SaleType=3 ";
            if (!entity.ID.Equals(0)) { strSQL += " and a.ID=" + entity.ID + ""; }
            if (!entity.WXID.Equals(0)) { strSQL += " and c.WXID=" + entity.WXID + ""; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and c.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and c.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result = Convert.ToInt32(idr["OrderNum"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询客户特价轮胎数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int QueryYunSpecialTyreNum(WXOrderEntity entity)
        {
            int result = 0;
            string strSQL = "select sum(a.Piece) as Piece from Tbl_WX_Order as a inner join Tbl_WX_OrderProduct as b on a.ID=b.OrderID inner join Tbl_WX_Client as c on a.WXID=c.ID where (1=1) and b.CutEntry>=0 ";
            if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID=" + entity.HouseID; }
            if (!entity.ClientNum.Equals(0)) { strSQL += " and c.ClientNum=" + entity.ClientNum; }
            if (!entity.WXID.Equals(0)) { strSQL += " and c.WXID=" + entity.WXID + ""; }
            if (!string.IsNullOrEmpty(entity.ProductCode)) { strSQL += " and b.ProductCode='" + entity.ProductCode + "'"; }
            if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and a.PayStatus='" + entity.PayStatus + "'"; }
            //制单日期范围
            //if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            //{
            //    strSQL += " and c.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            //}
            //if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            //{
            //    strSQL += " and c.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            //}
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result = Convert.ToInt32(idr["Piece"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 修改商城订单单价
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderGoodsPrice(QyOrderUpdateGoodsEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_WX_OrderProduct set OrderPrice=@OrderPrice where OrderID=@OrderID and ShelvesID=@ShelvesID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderPrice", DbType.Decimal, entity.OrderPrice);
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.AddInParameter(cmd, "@ShelvesID", DbType.Int64, entity.ShelvesID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        #endregion
        #region 微信绑定
        /// <summary>
        /// 绑定客户店代码
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateClientForBindingClientNum(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set Name=@Name,Cellphone=@Cellphone,Province=@Province,City=@City,Country=@Country,ClientNum=@ClientNum,BindDate=@BindDate Where wxOpenID=@wxOpenID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                    conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                    conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                    conn.AddInParameter(cmd, "@BindDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@wxOpenID", DbType.String, entity.wxOpenID);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void BindingClientNumAPP(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set Name=@Name,Province=@Province,City=@City,Country=@Country,ClientNum=@ClientNum,BindDate=@BindDate,Cellphone=@Cellphone Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                    conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                    conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                    conn.AddInParameter(cmd, "@BindDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 保存用户注册
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserRegeist(WXUserEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_WX_Client Set Name=@Name,CompanyName=@CompanyName,Cellphone=@Cellphone,Province=@Province,City=@City,Country=@Country,Address=@Address,BusLicenseImg=@BusLicenseImg,IDCardImg=@IDCardImg,IDCardBackImg=@IDCardBackImg,RegisterDate=@RegisterDate,AvatarBig=@AvatarBig,AvatarSmall=@AvatarSmall,wxName=@wxName,Longitude=@Longitude,Latitude=@Latitude,AcceptName=@AcceptName,AcceptCellphone=@AcceptCellphone Where wxOpenID=@wxOpenID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                    conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                    conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                    conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    conn.AddInParameter(cmd, "@BusLicenseImg", DbType.String, entity.BusLicenseImg);
                    conn.AddInParameter(cmd, "@IDCardImg", DbType.String, entity.IDCardImg);
                    conn.AddInParameter(cmd, "@IDCardBackImg", DbType.String, entity.IDCardBackImg);
                    conn.AddInParameter(cmd, "@RegisterDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@wxOpenID", DbType.String, entity.wxOpenID);
                    conn.AddInParameter(cmd, "@AvatarSmall", DbType.String, entity.AvatarSmall);
                    conn.AddInParameter(cmd, "@AvatarBig", DbType.String, entity.AvatarBig);
                    conn.AddInParameter(cmd, "@wxName", DbType.String, entity.wxName);
                    conn.AddInParameter(cmd, "@Longitude", DbType.String, entity.Longitude);
                    conn.AddInParameter(cmd, "@Latitude", DbType.String, entity.Latitude);
                    conn.AddInParameter(cmd, "@AcceptName", DbType.String, entity.AcceptName);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void AddAppCellphoneUser(WXUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_WX_Client Set Name=@Name,CompanyName=@CompanyName,Cellphone=@Cellphone,Province=@Province,City=@City,Country=@Country,Address=@Address,BusLicenseImg=@BusLicenseImg,IDCardImg=@IDCardImg,IDCardBackImg=@IDCardBackImg,RegisterDate=@RegisterDate Where ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
                    conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
                    conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
                    conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    conn.AddInParameter(cmd, "@BusLicenseImg", DbType.String, entity.BusLicenseImg);
                    conn.AddInParameter(cmd, "@IDCardImg", DbType.String, entity.IDCardImg);
                    conn.AddInParameter(cmd, "@IDCardBackImg", DbType.String, entity.IDCardBackImg);
                    conn.AddInParameter(cmd, "@RegisterDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    conn.ExecuteNonQuery(cmd);
                }

                AddWeixinPoint(new WXUserPointEntity { WXID = entity.ID, Point = entity.ConsumerPoint, CutPoint = "0", PointType = "1" });
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            //entity.EnSafe();
            //Int64 did = 0;
            //string strSQL = @"INSERT INTO Tbl_WX_Client(Name,CompanyName,Address,Cellphone,BusLicenseImg,IDCardImg,IDCardBackImg,Province,City,Country,RegisterDate,OP_DATE,UserType) VALUES (@Name,@CompanyName,@Address,,@Cellphone,@BusLicenseImg,@IDCardImg,@IDCardBackImg,@Province,@City,@Country,@RegisterDate,@OP_DATE,@UserType) SELECT @@IDENTITY";
            //try
            //{
            //    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            //    {
            //        conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            //        conn.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            //        conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
            //        conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
            //        conn.AddInParameter(cmd, "@BusLicenseImg", DbType.String, entity.BusLicenseImg);
            //        conn.AddInParameter(cmd, "@IDCardImg", DbType.String, entity.IDCardImg);
            //        conn.AddInParameter(cmd, "@IDCardBackImg", DbType.String, entity.IDCardBackImg);
            //        conn.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
            //        conn.AddInParameter(cmd, "@City", DbType.String, entity.City);
            //        conn.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
            //        conn.AddInParameter(cmd, "@UserType", DbType.String, entity.UserType);
            //        conn.AddInParameter(cmd, "@RegisterDate", DbType.DateTime, DateTime.Now);
            //        conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
            //        did = Convert.ToInt64(conn.ExecuteScalar(cmd));
            //    }

            //    AddWeixinPoint(new WXUserPointEntity { WXID = did, Point = entity.ConsumerPoint, CutPoint = "0", PointType = "1" });
            //}
            //catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 根据用户OpendID判断该用户是否绑定客户编码True：已经绑定，False：未绑定
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsBindingClientNum(WXUserEntity entity)
        {
            string strQ = @"Select ClientNum from Tbl_WX_Client Where wxOpenID=@wxOpenID";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@wxOpenID", DbType.String, entity.wxOpenID);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(idr.Rows[0]["ClientNum"])))
                    {
                        return false;
                    }
                    if (Convert.ToInt32(idr.Rows[0]["ClientNum"]) <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 判断超过绑定人数，一个店代码允许最多绑定3个微信 True:超过或等于3个，False：允许继续绑定
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsMaxBindingNum(WXUserEntity entity)
        {
            string strQ = @"Select COUNT(ClientNum) as Num from Tbl_WX_Client Where ClientNum=@ClientNum group by ClientNum";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@ClientNum", DbType.Int32, entity.ClientNum);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(idr.Rows[0]["Num"]) < 6)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 判断手机号码是否已经绑定 True:绑定过了False:未绑定
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsCellphoneBind(WXUserEntity entity)
        {
            string strQ = @"select ClientNum from Tbl_WX_Client where Cellphone=@Cellphone";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@Cellphone", DbType.String, entity.Cellphone);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(idr.Rows[0]["ClientNum"])))
                    {
                        return false;
                    }
                    if (Convert.ToInt32(idr.Rows[0]["ClientNum"]) <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        #endregion
        #region 优惠活动领取
        /// <summary>
        /// 新增领取记录
        /// </summary>
        /// <param name="entity"></param>
        public void AddSecKill(WXSecKillEntity entity)
        {
            entity.EnSafe();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_WX_SecKill(CarNum,WXID,Cellphone,ParentID,Company) VALUES (@CarNum,@WXID,@Cellphone,@ParentID,@Company) SELECT @@IDENTITY";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CarNum", DbType.String, entity.CarNum);
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    conn.AddInParameter(cmd, "@ParentID", DbType.Int64, entity.ParentID);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@Company", DbType.String, entity.Company);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 车牌号是否已经领取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistReceive(WXSecKillEntity entity)
        {
            string strQ = @"Select CarNum from Tbl_WX_SecKill Where CarNum=@CarNum";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@CarNum", DbType.String, entity.CarNum);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 查询优惠领取情况 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXSecKillEntity> QuerySecKillData(WXSecKillEntity entity)
        {
            List<WXSecKillEntity> result = new List<WXSecKillEntity>();
            try
            {
                string strSQL = @"Select * from Tbl_WX_SecKill Where (1=1)";
                if (!entity.ParentID.Equals(0))
                {
                    strSQL += " and ParentID=" + entity.ParentID + "";
                }
                if (!string.IsNullOrEmpty(entity.Company))
                {
                    strSQL += " and Company='" + entity.Company + "'";
                }
                strSQL += " Order by OP_DATE desc";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new WXSecKillEntity
                            {
                                CarNum = Convert.ToString(idr["CarNum"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                UseStatus = Convert.ToString(idr["UseStatus"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                ParentID = Convert.ToInt64(idr["ParentID"]),
                                Company = Convert.ToString(idr["Company"]),
                                ID = Convert.ToInt64(idr["ID"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 新增统计数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void AddSecStatisData(WXSecStatisEntity entity)
        {
            string strSQL = string.Empty;
            if (!entity.BrowseNum.Equals(0))
            {
                strSQL = "Update Tbl_WX_SecStatis Set BrowseNum=BrowseNum+1 Where SecID=" + entity.SecID + "";
            }
            if (!entity.ShareAppNum.Equals(0))
            {
                strSQL = "Update Tbl_WX_SecStatis Set ShareAppNum=ShareAppNum+1 Where SecID=" + entity.SecID + "";
            }
            if (!entity.ShareTimNum.Equals(0))
            {
                strSQL = "Update Tbl_WX_SecStatis Set ShareTimNum=ShareTimNum+1 Where SecID=" + entity.SecID + "";
            }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXSecStatisEntity QuerySecStatisEntity(WXSecStatisEntity entity)
        {
            WXSecStatisEntity result = new WXSecStatisEntity();
            string strSQL = "Select * from Tbl_WX_SecStatis where SecID=@SecID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@SecID", DbType.Int32, entity.SecID);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.SecID = Convert.ToInt32(idr["SecID"]);
                        result.BrowseNum = Convert.ToInt32(idr["BrowseNum"]);
                        result.ShareAppNum = Convert.ToInt32(idr["ShareAppNum"]);
                        result.ShareTimNum = Convert.ToInt32(idr["ShareTimNum"]);
                        result.ShareNum = Convert.ToInt32(idr["ShareAppNum"]) + Convert.ToInt32(idr["ShareTimNum"]);
                    }
                }
            }

            List<WXSecKillEntity> sec = QuerySecKillData(new WXSecKillEntity { Company = Convert.ToString(entity.SecID) });
            if (sec.Count > 0)
            {
                result.RegNum = sec.Count;
                result.ReceiveNum = sec.Where(c => c.UseStatus == "1").Count();
            }
            return result;
        }

        #endregion
        #region 微信商城与淘宝连接
        /// <summary>
        /// 查询淘宝订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXTaobaoEntity> QueryTaobaoOrderInfo(WXTaobaoEntity entity)
        {
            List<WXTaobaoEntity> result = new List<WXTaobaoEntity>();
            string strSQL = "select b.* From Tbl_WX_Client as a inner join Tbl_Taobao_Order as b on a.ID=b.WXID where (1=1)";
            if (!entity.ID.Equals(0))
            {
                strSQL += " and a.ID=" + entity.ID + "";
            }
            if (!entity.ParentID.Equals(0))
            {
                strSQL += " and a.ParentID=" + entity.ParentID + "";
            }
            if (!string.IsNullOrEmpty(entity.status))
            {
                strSQL += " and b.status='" + entity.status + "'";
            }
            strSQL += " order by b.created desc";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new WXTaobaoEntity
                        {
                            WXID = Convert.ToInt64(idr["WXID"]),
                            TaobaoID = Convert.ToString(idr["TaobaoID"]),
                            buyer_nick = Convert.ToString(idr["buyer_nick"]),
                            pic_path = Convert.ToString(idr["pic_path"]),
                            payment = Convert.ToDecimal(idr["payment"]),
                            price = Convert.ToDecimal(idr["price"]),
                            total_fee = Convert.ToDecimal(idr["total_fee"]),
                            receiver_address = Convert.ToString(idr["receiver_address"]),
                            receiver_city = Convert.ToString(idr["receiver_city"]),
                            receiver_district = Convert.ToString(idr["receiver_district"]),
                            receiver_mobile = Convert.ToString(idr["receiver_mobile"]),
                            receiver_name = Convert.ToString(idr["receiver_name"]),
                            receiver_phone = Convert.ToString(idr["receiver_phone"]),
                            receiver_state = Convert.ToString(idr["receiver_state"]),
                            num = Convert.ToInt32(idr["num"]),
                            status = Convert.ToString(idr["status"]),
                            Title = Convert.ToString(idr["Title"]),
                            buyer_rate = Convert.ToString(idr["buyer_rate"]),
                            created = string.IsNullOrEmpty(Convert.ToString(idr["created"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["created"]),
                            pay_time = string.IsNullOrEmpty(Convert.ToString(idr["pay_time"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["pay_time"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            ID = Convert.ToInt64(idr["ID"])
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询今日统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXTaobaoEntity QueryTodayTaobaoStatis(WXTaobaoEntity entity)
        {
            WXTaobaoEntity result = new WXTaobaoEntity();
            string strSQL = @"select a.WXID,ISNULL(a.FX,0) as FX,ISNULL(b.TC,0) as TC,ISNULL(c.Num,0) as Num from (
select WXID, SUM(Cash) as FX From dbo.Tbl_Taobao_CashBack where WXID=" + entity.WXID + " and CashType=0 and payment!=0";
            if ((entity.created.ToString("yyyy-MM-dd") != "0001-01-01" && entity.created.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and OP_DATE>='" + entity.created.ToString("yyyy-MM-dd") + "'";
                strSQL += " and OP_DATE<'" + entity.created.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            strSQL += " group by WXID) as a full join (select WXID, SUM(Cash) as TC From dbo.Tbl_Taobao_CashBack where WXID=" + entity.WXID + " and CashType=1 and payment!=0 ";
            if ((entity.created.ToString("yyyy-MM-dd") != "0001-01-01" && entity.created.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and OP_DATE>='" + entity.created.ToString("yyyy-MM-dd") + "'";
                strSQL += " and OP_DATE<'" + entity.created.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            strSQL += " group by WXID) as b on a.WXID=b.WXID full join (select WXID, COUNT(*) as Num From Tbl_Taobao_Order where WXID=" + entity.WXID + " ";
            if ((entity.created.ToString("yyyy-MM-dd") != "0001-01-01" && entity.created.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and OP_DATE>='" + entity.created.ToString("yyyy-MM-dd") + "'";
                strSQL += " and OP_DATE<'" + entity.created.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            strSQL += " group by WXID) as c on a.WXID=c.WXID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.TodayOrderNum = Convert.ToInt32(idr["Num"]);
                        result.TodayTiCheng = Convert.ToDecimal(idr["TC"]);
                        result.TodayFanLi = Convert.ToDecimal(idr["FX"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 保存淘宝订单
        /// </summary>
        /// <param name="entity"></param>
        public void SaveTaobaoOrder(WXTaobaoEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_Taobao_Order(WXID,TaobaoID,OP_DATE) VALUES (@WXID,@TaobaoID,@OP_DATE)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    conn.AddInParameter(cmd, "@TaobaoID", DbType.String, entity.TaobaoID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.String, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改淘宝订单的微信用户ID
        /// </summary>
        public void UpdateTaobaoOrderWxID(WXTaobaoEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"Update Tbl_Taobao_Order set WXID=@WXID Where TaobaoID=@TaobaoID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    conn.AddInParameter(cmd, "@TaobaoID", DbType.String, entity.TaobaoID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 查询我的上级信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXUserEntity QueryMyParentEntity(WXUserEntity entity)
        {
            WXUserEntity result = new WXUserEntity();
            try
            {
                string strSQL = @" select b.* from Tbl_WX_Client as a inner join Tbl_WX_Client as b on a.ParentID=b.ID where a.ID=@ID";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.ID = Convert.ToInt64(idr["ID"]);
                            result.wxName = Convert.ToString(idr["wxName"]);
                            result.Name = Convert.ToString(idr["Name"]);
                            result.wxOpenID = Convert.ToString(idr["wxOpenID"]);
                            result.AvatarBig = Convert.ToString(idr["AvatarBig"]);
                            result.AvatarSmall = Convert.ToString(idr["AvatarSmall"]);
                            result.Cellphone = Convert.ToString(idr["Cellphone"]);
                            result.IDCardNum = Convert.ToString(idr["IDCardNum"]);
                            result.IsCertific = Convert.ToString(idr["IsCertific"]);
                            result.Sex = Convert.ToInt32(idr["Sex"]);
                            result.Province = Convert.ToString(idr["Province"]);
                            result.City = Convert.ToString(idr["City"]);
                            result.Country = Convert.ToString(idr["Country"]);
                            result.RegisterDate = Convert.ToDateTime(idr["RegisterDate"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 判断是否存在淘宝订单号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistTaobaoOrder(WXTaobaoEntity entity)
        {
            string strQ = @"Select TaobaoID from Tbl_Taobao_Order Where TaobaoID=@TaobaoID";

            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@TaobaoID", DbType.String, entity.TaobaoID);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #region APP商城管理
        /// <summary>
        /// 查询最新的版本记录数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public APPVersionEntity QueryLatestAppVersionEntity(APPVersionEntity entity)
        {
            APPVersionEntity result = new APPVersionEntity();
            string strSQL = "select top 1 * From Tbl_App_Version Where LimitStatus=0 ";
            if (!string.IsNullOrEmpty(entity.AppType)) { strSQL += " and AppType='" + entity.AppType + "'"; }
            strSQL += " Order by OP_DATE desc";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.ID = Convert.ToInt32(idr["ID"]);
                        result.AppVersion = Convert.ToString(idr["AppVersion"]);
                        result.DowloadUrl = Convert.ToString(idr["DowloadUrl"]);
                        result.Memo = Convert.ToString(idr["Memo"]);
                        result.LimitStatus = Convert.ToString(idr["LimitStatus"]);
                        result.AppType = Convert.ToString(idr["AppType"]);
                        result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询苹果提审数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public AppleCheckEntity QueryAppleCheckEntity(AppleCheckEntity entity)
        {
            AppleCheckEntity result = new AppleCheckEntity();
            string strSQL = "select * From Tbl_App_AppleCheck Where AppVersion=@AppVersion ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@AppVersion", DbType.String, entity.AppVersion);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.ID = Convert.ToInt32(idr["ID"]);
                        result.AppVersion = Convert.ToString(idr["AppVersion"]);
                        result.isOpenAppleLogin = Convert.ToString(idr["isOpenAppleLogin"]);
                        result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                    }
                }
            }
            return result;

        }
        /// <summary>
        /// 新增优惠券
        /// </summary>
        /// <param name="entity"></param>
        public void AddCoupon(WXCouponEntity entity)
        {
            entity.EnSafe();
            string strSQL = "INSERT INTO Tbl_WX_Coupon(WXID,Money,UseStatus,GainDate,StartDate,EndDate,ClientID) VALUES (@WXID,@Money,@UseStatus,@GainDate,@StartDate,@EndDate,@ClientID)";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                conn.AddInParameter(cmd, "@Money", DbType.Decimal, entity.Money);
                conn.AddInParameter(cmd, "@UseStatus", DbType.String, entity.UseStatus);
                conn.AddInParameter(cmd, "@GainDate", DbType.DateTime, entity.GainDate);
                conn.AddInParameter(cmd, "@StartDate", DbType.DateTime, entity.StartDate);
                conn.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate);
                conn.AddInParameter(cmd, "@ClientID", DbType.Int64, entity.ClientID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 设置优惠券已使用或已过期
        /// </summary>
        /// <param name="entity"></param>
        public void SetCouponUsed(WXCouponEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_WX_Coupon set UseStatus=@UseStatus,UseDate=@UseDate,OrderID=@OrderID,OrderType=@OrderType Where ID=@ID";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                //conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                conn.AddInParameter(cmd, "@UseStatus", DbType.String, entity.UseStatus);
                conn.AddInParameter(cmd, "@UseDate", DbType.DateTime, entity.UseDate);
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 批量修改优惠券状态
        /// </summary>
        /// <param name="entityList"></param>
        public void BatchUpdateCoupon(List<WXCouponEntity> entityList)
        {
            if (entityList == null || entityList.Count == 0) return;

            //分页批量执行，防止执行语句太大
            const int batchSize = 10000;
            int totalBatches = (int)Math.Ceiling(entityList.Count / (double)batchSize);
            for (int i = 0; i < totalBatches; i++)
            {
                var batch = entityList.Skip(i * batchSize).Take(batchSize);
                StringBuilder sqlBuilder = new StringBuilder();

                foreach (var entity in batch)
                {
                    sqlBuilder.AppendLine(
                        $"UPDATE Tbl_WX_Coupon SET UseStatus={entity.UseStatus},UseDate='{DateTime.Now}' ,OrderID={entity.OrderID},OrderType='{entity.OrderType}' WHERE ID={entity.ID};");
                }

                using (DbCommand cmd = conn.GetSqlStringCommond(sqlBuilder.ToString()))
                {
                    conn.ExecuteNonQuery(cmd);
                }
            }
        }

        /// <summary>
        /// 设置优惠券未使用
        /// </summary>
        /// <param name="entity"></param>
        public void SetCouponNoUsed(WXCouponEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_WX_Coupon set UseStatus=@UseStatus,OrderID=0,OrderType='' Where OrderID=@OrderID";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@UseStatus", DbType.String, entity.UseStatus);
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 删除发放的优惠券
        /// </summary>
        /// <param name="entity"></param>
        public void DelCoupon(string FromOrderNO)
        {
            string strSQL = "Delete From Tbl_WX_Coupon Where FromOrderNO=@FromOrderNO";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@FromOrderNO", DbType.String, FromOrderNO);
                conn.ExecuteNonQuery(cmd);
            }
        }
        public void DelCoupon(WXCouponEntity entity)
        {
            string strSQL = "Delete From Tbl_WX_Coupon Where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询我的优惠券或我的店代码下面的所有优惠券
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXCouponEntity> QueryCouponData(WXCouponEntity entity)
        {
            List<WXCouponEntity> result = new List<WXCouponEntity>();
            string strSQL = "select * From Tbl_WX_Coupon where (1=1)";
            if (!entity.WXID.Equals(0)) { strSQL += " and WXID=" + entity.WXID; }
            if (!entity.ClientID.Equals(0)) { strSQL += " and ClientID=" + entity.ClientID; }
            if (!entity.SuppClientNum.Equals(0)) { strSQL += " and SuppClientNum=" + entity.SuppClientNum; }
            if (!string.IsNullOrEmpty(entity.UseStatus)) { strSQL += " and UseStatus='" + entity.UseStatus + "'"; }
            if (!string.IsNullOrEmpty(entity.FromOrderNO)) { strSQL += " and FromOrderNO='" + entity.FromOrderNO + "'"; }
            if (!string.IsNullOrEmpty(entity.TypeID)) { strSQL += " and (CHARINDEX(','+'" + entity.TypeID + "'+',' , ','+TypeID+',') > 0 or  TypeID='' or  TypeID is null)"; }
            if (!string.IsNullOrEmpty(entity.IsLoseEfficacy)) { strSQL += " and EndDate< GETDATE() "; }

            strSQL += " Order By UseStatus asc ,GainDate desc";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new WXCouponEntity
                        {
                            Money = Convert.ToDecimal(idr["Money"]),
                            ID = Convert.ToInt64(idr["ID"]),
                            WXID = Convert.ToInt64(idr["WXID"]),
                            UseStatus = Convert.ToString(idr["UseStatus"]),
                            GainDate = Convert.ToDateTime(idr["GainDate"]),
                            StartDate = Convert.ToDateTime(idr["StartDate"]),
                            IsSuperPosition = Convert.ToString(idr["IsSuperPosition"]),
                            CouponType = Convert.ToString(idr["CouponType"]),
                            TypeID = Convert.ToString(idr["TypeID"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            EndDate = Convert.ToDateTime(idr["EndDate"]),
                            FromOrderNO = Convert.ToString(idr["FromOrderNO"]),
                            IsFollowQuantity = string.IsNullOrEmpty(idr["IsFollowQuantity"].ToString()) ? "0" : Convert.ToString(idr["IsFollowQuantity"]),
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 通过ID查询优惠券信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXCouponEntity QueryCouponByID(WXCouponEntity entity)
        {
            WXCouponEntity result = new WXCouponEntity();
            string strSQL = "Select * From Tbl_WX_Coupon Where (1=1)";
            if (!entity.ID.Equals(0)) { strSQL += " and ID=" + entity.ID; }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Money = Convert.ToDecimal(idr["Money"]);
                        result.ID = Convert.ToInt64(idr["ID"]);
                        result.WXID = Convert.ToInt64(idr["WXID"]);
                        result.UseStatus = Convert.ToString(idr["UseStatus"]);
                        result.GainDate = Convert.ToDateTime(idr["GainDate"]);
                        result.StartDate = Convert.ToDateTime(idr["StartDate"]);
                        result.EndDate = Convert.ToDateTime(idr["EndDate"]);
                        result.IsSuperPosition = Convert.ToString(idr["IsSuperPosition"]);
                        result.IsFollowQuantity = Convert.ToString(idr["IsFollowQuantity"]);
                        result.CouponType = Convert.ToString(idr["CouponType"]);
                        result.TypeID = Convert.ToString(idr["TypeID"]);
                        result.TypeName = Convert.ToString(idr["TypeName"]);
                        result.ActualType = Convert.ToString(idr["ActualType"]);
                        result.SuppClientNum = string.IsNullOrEmpty(Convert.ToString(idr["SuppClientNum"])) ? 0 : Convert.ToInt32(idr["SuppClientNum"]);
                        result.ThrowGood = Convert.ToString(idr["ThrowGood"]);
                    }
                }
            }
            return result;
        }
        #endregion
        #region 发送手机号码验证码
        /// <summary>
        /// 通过手机号码查询当天发送验证码记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXMobileCodeEntity QueryMobileCodeEntity(WXMobileCodeEntity entity)
        {
            WXMobileCodeEntity result = new WXMobileCodeEntity();
            string strSQL = "select * From Tbl_Cargo_MobileCode Where (1=1)";
            if (!string.IsNullOrEmpty(entity.Cellphone)) { strSQL += " and Cellphone='" + entity.Cellphone + "'"; }
            if ((entity.OP_DATE.ToString("yyyy-MM-dd") != "0001-01-01" && entity.OP_DATE.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and OP_DATE>='" + entity.OP_DATE.ToString("yyyy-MM-dd") + "'";
            }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.ID = Convert.ToInt32(idr["ID"]);
                        result.Cellphone = Convert.ToString(idr["Cellphone"]);
                        result.SendNum = Convert.ToInt32(idr["SendNum"]);
                        result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddMobileCodeEntity(WXMobileCodeEntity entity)
        {
            string strSQL = "Insert into Tbl_Cargo_MobileCode(Cellphone) values (@Cellphone)";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改记录计数
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateMobileCodeEntity(WXMobileCodeEntity entity)
        {
            string strSQL = @"UPDATE Tbl_Cargo_MobileCode SET SendNum=SendNum+@SendNum Where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int32, entity.ID);
                conn.AddInParameter(cmd, "@SendNum", DbType.Int32, entity.SendNum);
                conn.ExecuteNonQuery(cmd);
            }
        }
        #endregion
        #region 小程序购物车
        public List<ShippingCarItemsInfo> QueryShippingCarInfo(ShippingCarItemsInfo entity)
        {
            List<ShippingCarItemsInfo> result = new List<ShippingCarItemsInfo>();
            try
            {
                string strSQL = @"Select sc.*,h.Name as HouseName,a.Latitude,a.Longitude from Tbl_Applet_ShopCart sc left join Tbl_Cargo_House h on sc.HouseID=h.HouseID inner join Tbl_Cargo_Area a on sc.ParentAreaID=a.AreaID where 1=1";
                if (!string.IsNullOrEmpty(entity.ClientNum)) { strSQL += " and sc.ClientNum=@ClientNum"; }
                if (!string.IsNullOrEmpty(entity.OpenID)) { strSQL += " and sc.OpenID=@OpenID"; }
                if (!string.IsNullOrEmpty(entity.key)) { strSQL += " and sc.GoodsKey=@GoodsKey"; }
                if (!entity.selected.Equals(false)) { strSQL += " and sc.selected=@selected"; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    if (!string.IsNullOrEmpty(entity.ClientNum))
                    {
                        conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    }
                    if (!string.IsNullOrEmpty(entity.OpenID))
                    {
                        conn.AddInParameter(cmd, "@OpenID", DbType.String, entity.OpenID);
                    }
                    if (!string.IsNullOrEmpty(entity.key))
                    {
                        conn.AddInParameter(cmd, "@GoodsKey", DbType.String, entity.key);
                    }
                    if (!entity.selected.Equals(false))
                    {
                        conn.AddInParameter(cmd, "@selected", DbType.Int32, 1);
                    }
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new ShippingCarItemsInfo
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OpenID = Convert.ToString(idr["OpenID"]),
                                key = Convert.ToString(idr["GoodsKey"]),
                                name = Convert.ToString(idr["Name"]),
                                number = Convert.ToInt32(idr["Number"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                Model = Convert.ToString(idr["Model"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                BatchYear = Convert.ToInt32(idr["BatchYear"]),
                                price = Convert.ToDecimal(idr["SalePrice"]),
                                score = Convert.ToDecimal(idr["Score"]),
                                pic = Convert.ToString(idr["pic"]),
                                selected = Convert.ToInt32(idr["Selected"]) == 1 ? true : false,
                                HouseID = string.IsNullOrEmpty(Convert.ToString(idr["HouseID"])) ? 0 : Convert.ToInt32(idr["HouseID"]),
                                ParentAreaID = string.IsNullOrEmpty(Convert.ToString(idr["ParentAreaID"])) ? 0 : Convert.ToInt32(idr["ParentAreaID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                Longitude = Convert.ToString(idr["Longitude"]),
                                Latitude = Convert.ToString(idr["Latitude"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public void AddShippingCarItem(ShippingCarItemsInfo entity)
        {
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_Applet_ShopCart(OpenID,ClientNum,GoodsKey,Name,Number,TypeID,Model,GoodsCode,Specs,Figure,LoadIndex,SpeedLevel,BatchYear,SalePrice,Score,pic,Selected,OP_DATE,HouseID,ParentAreaID) VALUES (@OpenID,@ClientNum,@GoodsKey,@Name,@Number,@TypeID,@Model,@GoodsCode,@Specs,@Figure,@LoadIndex,@SpeedLevel,@BatchYear,@SalePrice,@Score,@pic,@Selected,@OP_DATE,@HouseID,@ParentAreaID)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OpenID", DbType.String, entity.OpenID);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@GoodsKey", DbType.String, entity.key);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.name);
                    conn.AddInParameter(cmd, "@Number", DbType.Int32, entity.number);
                    conn.AddInParameter(cmd, "@TypeID", DbType.Int32, entity.TypeID);
                    conn.AddInParameter(cmd, "@Model", DbType.String, entity.Model);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    conn.AddInParameter(cmd, "@Specs", DbType.String, entity.Specs);
                    conn.AddInParameter(cmd, "@Figure", DbType.String, entity.Figure);
                    conn.AddInParameter(cmd, "@LoadIndex", DbType.String, entity.LoadIndex);
                    conn.AddInParameter(cmd, "@SpeedLevel", DbType.String, entity.SpeedLevel);
                    conn.AddInParameter(cmd, "@BatchYear", DbType.Int32, entity.BatchYear);
                    conn.AddInParameter(cmd, "@SalePrice", DbType.Decimal, entity.price);
                    conn.AddInParameter(cmd, "@Score", DbType.Decimal, entity.score);
                    conn.AddInParameter(cmd, "@pic", DbType.String, entity.pic);
                    conn.AddInParameter(cmd, "@Selected", DbType.Int32, entity.selected == true ? 1 : 0);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@ParentAreaID", DbType.Int32, entity.ParentAreaID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void ModifyShippingCarItem(ShippingCarItemsInfo entity)
        {
            string strSQL = @"UPDATE Tbl_Applet_ShopCart Set Number=Number+@Number,OP_DATE=@OP_DATE Where ClientNum=@ClientNum and GoodsKey=@GoodsKey";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@GoodsKey", DbType.String, entity.key);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@Number", DbType.Int32, entity.number);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void SelectedShippingCarInfo(ShippingCarItemsInfo entity)
        {
            string strSQL = @"UPDATE Tbl_Applet_ShopCart Set Selected=@Selected,OP_DATE=@OP_DATE Where ClientNum=@ClientNum and GoodsKey=@GoodsKey";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@GoodsKey", DbType.String, entity.key);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@Selected", DbType.Int32, entity.selected ? 1 : 0);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void DelShippingCarItem(ShippingCarItemsInfo entity)
        {
            string strSQL = @"DELETE FROM Tbl_Applet_ShopCart Where ClientNum=@ClientNum and GoodsKey=@GoodsKey";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@GoodsKey", DbType.String, entity.key);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        #endregion

        #region 云配小程序
        /// <summary>
        /// 查询我的商城订单
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<MiniProOrderEntity> QueryMyMiniProOrder(int pIndex, int pNum, WXOrderEntity entity)
        {
            //--订金视为未支付
            List<MiniProOrderEntity> result = new List<MiniProOrderEntity>();
            string strSQL = "SELECT TOP " + pNum + " * from (select * from (SELECT ROW_NUMBER() OVER (ORDER BY b.CreateDate desc) AS RowNumber, c.Address as SupplierAddress, c.ClientShortName as SupperName, b.SuppClientNum, b.Name, b.Cellphone, b.ID, b.OrderNo, b.Piece, isnull((re.ActualAmounts), 0) as ActualAmounts, b.TotalCharge, b.CreateDate, iif(isnull(a2.PaymentAmount, 0)>0 and isnull(a2.PaymentAmount, 0)>=isnull((re.ActualAmounts), 0),1,iif(isnull(a2.PaymentAmount, 0)>0,0,b.PayStatus)) as PayStatus, b.OrderStatus, b.WXPayOrderNo, a.CompanyName, b.ThrowGood, b.RefundCheckStatus, b.SendType, (case when (select count(1) from Tbl_Cargo_ReserveOrder re where re.WXOrderNo = b.OrderNo) > 0 then 1 else 0 end) IsReserve, isnull((select max(PaymentType) from Tbl_Cargo_ReserveOrderPaymentRecord as re where re.WXOrderNo = b.OrderNo), 0) PaymentType, isnull(a2.PaymentAmount, 0) as PaymentAmountSum From Tbl_WX_Client as a inner join Tbl_WX_Order as b on a.ID = b.WXID inner join Tbl_Cargo_Client as c on b.SuppClientNum = c.ClientNum left join (select OrderNo,WxOrderNo,sum(PaymentAmount) as PaymentAmount from Tbl_Cargo_ReserveOrderPaymentRecord group by OrderNo,WxOrderNo) as a2 on b.OrderNo=a2.WxOrderNo left join (select max(ActualAmounts) as ActualAmounts,re.WXOrderNo from Tbl_Cargo_ReserveOrder re inner join Tbl_Cargo_ReserveOrderPaymentRecord as rep on re.OrderNo = rep.OrderNo and re.WXOrderNo = rep.WxOrderNo group by re.WXOrderNo having max(rep.PaymentType) = 1)re on re.WXOrderNo = b.OrderNo where (1 = 1)  ";
            //left join tbl_Cargo_order as d on b.OrderNo=d.WXOrderNo,d.DeliveryType
            if (!entity.HouseID.Equals(0)) { strSQL += " and b.HouseID=" + entity.HouseID; }
            if (!entity.ClientNum.Equals(0)) { strSQL += " and a.ClientNum=" + entity.ClientNum; }

            if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and b.OrderStatus='" + entity.OrderStatus + "'"; }
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and b.OrderNo='" + entity.OrderNo + "'"; }
            strSQL += $@" ) as a2 where 1=1 ";
            if (!string.IsNullOrEmpty(entity.PayStatus))
            {
                if (entity.PayStatus.Equals("2"))
                {
                    strSQL += " and PayStatus in ('2','3')";
                }
                else
                {
                    strSQL += $@" and ((IsReserve=1 and PayStatus in('{entity.PayStatus}')) or (IsReserve=0 and PayStatus in('{entity.PayStatus}')))";
                }
            }
            strSQL += ") as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
            strSQL += " order by RowNumber asc";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //string DeliveryType = "急送";
                        //if (Convert.ToString(idr["DeliveryType"]).Equals("1"))
                        //{
                        //    DeliveryType = "自提";
                        //}
                        //else if (Convert.ToString(idr["DeliveryType"]).Equals("2"))
                        //{
                        //    DeliveryType = "次日达";
                        //}
                        var TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                        var ActualAmounts = Convert.ToDecimal(idr["ActualAmounts"]);
                        var PaymentAmountSum = Convert.ToDecimal(idr["PaymentAmountSum"]);
                        if (ActualAmounts > 0)
                        {
                            TotalCharge = ActualAmounts - PaymentAmountSum;
                            if (TotalCharge <= 0)
                            {
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                            }
                        }
                        var PayStatus = Convert.ToString(idr["PayStatus"]);
                        //if (PaymentAmountSum > 0 && PaymentAmountSum >= ActualAmounts)
                        //{
                        //    PayStatus = "1";
                        //}
                        //else
                        //{
                        //    if (PaymentAmountSum > 0)
                        //        PayStatus = "0";
                        //}
                        result.Add(new MiniProOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["ID"]),
                            Name = Convert.ToString(idr["Name"]),
                            Cellphone = Convert.ToString(idr["Cellphone"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            TotalNum = Convert.ToInt32(idr["Piece"]),
                            TotalCharge = TotalCharge,
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            PayStatus = PayStatus,
                            OrderStatus = Convert.ToString(idr["OrderStatus"]),
                            RefundCheckStatus = Convert.ToString(idr["RefundCheckStatus"]),
                            WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            CompanyName = Convert.ToString(idr["CompanyName"]),
                            SupperName = Convert.ToString(idr["SupperName"]),
                            SupplierAddress = Convert.ToString(idr["SupplierAddress"]),
                            SuppClientNum = Convert.ToInt32(idr["SuppClientNum"]),
                            DeliveryType = Convert.ToString(idr["SendType"]),
                            IsReserve = Convert.ToInt32(idr["IsReserve"]),
                            PaymentType = Convert.ToInt32(idr["PaymentType"]),
                            goods = Convert.ToInt32(idr["IsReserve"]) == 1 ? QueryMiniProGoodsList_Reserve(new WXOrderEntity { OrderID = Convert.ToInt64(idr["ID"]) }) : QueryMiniProGoodsList(new WXOrderEntity { OrderID = Convert.ToInt64(idr["ID"]) })
                        });
                    }
                }
            }
            return result;
        }

        private List<MiniProOrderGoodsEntity> QueryMiniProGoodsList(WXOrderEntity entity)
        {
            List<MiniProOrderGoodsEntity> result = new List<MiniProOrderGoodsEntity>();
            string strSQL = @"select a.OrderID,a.OrderNum,a.OrderPrice,a.ProductCode,a.BatchYear,a.Batch,b.Specs,b.Figure,b.LoadIndex,b.SpeedLevel,b.Thumbnail,c.TypeName from Tbl_WX_OrderProduct as a inner join Tbl_Cargo_ProductSpec as b on a.ProductCode=b.ProductCode inner join Tbl_Cargo_ProductType as c on b.TypeID=c.TypeID where a.OrderID=@OrderID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        string Thumbnail = "";
                        if (string.IsNullOrEmpty(Convert.ToString(idr["Thumbnail"]))) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; } else { Thumbnail = Convert.ToString(idr["Thumbnail"]).Replace("../", "https://dlt.neway5.com/"); }
                        result.Add(new MiniProOrderGoodsEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNum = Convert.ToInt32(idr["OrderNum"]),
                            OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                            ProductCode = Convert.ToString(idr["ProductCode"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            BatchYear = Convert.ToInt32(idr["BatchYear"]),
                            name = Convert.ToString(idr["TypeName"]) + " " + Convert.ToString(idr["Specs"]) + " " + Convert.ToString(idr["Figure"]) + " " + Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]) + " " + Convert.ToString(idr["BatchYear"]) + "年",
                            TypeName = Convert.ToString(idr["TypeName"]),
                            pic = Thumbnail,
                        });
                    }
                }
            }
            return result;
        }

        private List<MiniProOrderGoodsEntity> QueryMiniProGoodsList_Reserve(WXOrderEntity entity)
        {
            List<MiniProOrderGoodsEntity> result = new List<MiniProOrderGoodsEntity>();
            string strSQL = @"select a.OrderID,a.OrderNum,a.OrderPrice,a.ProductCode,a.BatchYear,a.Batch,b.Specs,b.Figure,b.LoadIndex,b.SpeedLevel,b.Thumbnail,c.TypeName from Tbl_WX_ReserveOrderProduct as a inner join Tbl_Cargo_ProductSpec as b on a.ProductCode=b.ProductCode inner join Tbl_Cargo_ProductType as c on b.TypeID=c.TypeID where a.OrderID=@OrderID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        string Thumbnail = "";
                        if (string.IsNullOrEmpty(Convert.ToString(idr["Thumbnail"]))) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; } else { Thumbnail = Convert.ToString(idr["Thumbnail"]).Replace("../", "https://dlt.neway5.com/"); }
                        result.Add(new MiniProOrderGoodsEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNum = Convert.ToInt32(idr["OrderNum"]),
                            OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                            ProductCode = Convert.ToString(idr["ProductCode"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            BatchYear = Convert.ToInt32(idr["BatchYear"]),
                            name = Convert.ToString(idr["TypeName"]) + " " + Convert.ToString(idr["Specs"]) + " " + Convert.ToString(idr["Figure"]) + " " + Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]) + " " + Convert.ToString(idr["BatchYear"]) + "年",
                            TypeName = Convert.ToString(idr["TypeName"]),
                            pic = Thumbnail,
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 新增微信小程序商城订单退款申请单
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderRefund(WXOrderEntity entity)
        {
            string strSQL = @"Update Tbl_WX_Order set PayStatus=@PayStatus,RefundDate=@RefundDate,RefundReason=@RefundReason,RefundMemo=@RefundMemo ";
            if (entity.ThrowGood.Equals("23"))
            {
                //次日达
                strSQL += ",RefundCheckID=@RefundCheckID,RefundCheckName=@RefundCheckName,RefundCheckStatus=@RefundCheckStatus,RefundCheckDate=@RefundCheckDate ";
            }
            strSQL += " Where OrderNo=@OrderNo";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@PayStatus", DbType.String, entity.PayStatus);
                conn.AddInParameter(cmd, "@RefundDate", DbType.DateTime, DateTime.Now);
                conn.AddInParameter(cmd, "@RefundReason", DbType.String, entity.RefundReason);
                conn.AddInParameter(cmd, "@RefundMemo", DbType.String, entity.RefundMemo);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                if (entity.ThrowGood.Equals("23"))
                {
                    conn.AddInParameter(cmd, "@RefundCheckID", DbType.String, entity.RefundCheckID);
                    conn.AddInParameter(cmd, "@RefundCheckName", DbType.String, entity.RefundCheckName);
                    conn.AddInParameter(cmd, "@RefundCheckStatus", DbType.String, entity.RefundCheckStatus);
                    conn.AddInParameter(cmd, "@RefundCheckDate", DbType.DateTime, DateTime.Now);
                }
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 更新微信订单退货来货入库单号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderFacOrderNo(WXOrderEntity entity)
        {
            string strSQL = @"Update Tbl_WX_Order set FacOrderNo=@FacOrderNo Where ID=@ID";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, entity.FacOrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询我的企业订单 企业客户
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<MiniProOrderEntity> QueryMyEnterpriseOrder(int pIndex, int pNum, CargoOrderEntity entity)
        {
            List<MiniProOrderEntity> result = new List<MiniProOrderEntity>();

            string strSQL = "SELECT TOP " + pNum + " * from (SELECT ROW_NUMBER() OVER (ORDER BY a.CreateDate desc) AS RowNumber,a.OrderID,a.OrderNo,a.HAwbNo,a.LogisAwbNo,a.Piece,a.TotalCharge,a.AcceptUnit,a.AcceptPeople,a.AcceptCellphone,a.AcceptTelephone,a.AcceptAddress,a.CreateDate,a.AwbStatus,a.OutHouseName,c.UpClientID,c.UpClientShortName from Tbl_Cargo_Order as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID inner join Tbl_Cargo_Client as c on a.PayClientNum=c.ClientNum where (1=1) ";
            if (!entity.PayClientNum.Equals(0)) { strSQL += " and a.PayClientNum=" + entity.PayClientNum; }
            if (!string.IsNullOrEmpty(entity.AwbStatus))
            {
                //传入0时，查询已下单和出库中的订单
                if (entity.AwbStatus.Equals("0"))
                {
                    strSQL += " and a.AwbStatus in ('0','1')";
                }
                else
                {
                    strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                }
            }
            if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and b.BelongHouse=" + entity.BelongHouse; }
            if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and a.OrderType=" + entity.OrderType; }
            if (!string.IsNullOrEmpty(entity.OrderModel)) { strSQL += " and a.OrderModel=" + entity.OrderModel; }
            if (!string.IsNullOrEmpty(entity.ShopCode)) { strSQL += " and c.ShopCode='" + entity.ShopCode + "'"; }
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
            strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
            strSQL += " order by RowNumber asc";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new MiniProOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            HAwbNo = Convert.ToString(idr["HAwbNo"]),
                            TotalNum = Convert.ToInt32(idr["Piece"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            OrderStatus = Convert.ToString(idr["AwbStatus"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                            CompanyName = Convert.ToString(idr["AcceptUnit"]),
                            Name = Convert.ToString(idr["AcceptPeople"]),
                            Cellphone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"])) ? Convert.ToString(idr["AcceptTelephone"]) : Convert.ToString(idr["AcceptCellphone"]),
                            SupplierAddress = Convert.ToString(idr["AcceptAddress"]),

                            SupperName = Convert.ToString(idr["UpClientShortName"]),
                            SuppClientNum = Convert.ToInt32(idr["UpClientID"]),

                            goods = QueryMyEnterOrderGoodsList(new CargoOrderEntity { OrderNo = Convert.ToString(idr["OrderNo"]) })
                        });
                    }
                }
            }
            return result;
        }

        private List<MiniProOrderGoodsEntity> QueryMyEnterOrderGoodsList(CargoOrderEntity entity)
        {
            List<MiniProOrderGoodsEntity> result = new List<MiniProOrderGoodsEntity>();
            string strSQL = @"select distinct a.Piece,a.ActSalePrice,a.ShowGoodsCode,b.Batch,b.BatchYear,d.TypeName,c.ProductCode,c.Specs,c.Figure,c.LoadIndex,c.SpeedLevel,c.GoodsCode,c.Thumbnail From Tbl_Cargo_OrderGoods as a inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID and a.HouseID=b.HouseID inner join Tbl_Cargo_ProductSpec as c on b.ProductCode=c.ProductCode inner join Tbl_Cargo_ProductType as d on b.TypeID=d.TypeID where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        string Thumbnail = "";
                        if (string.IsNullOrEmpty(Convert.ToString(idr["Thumbnail"]))) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; } else { Thumbnail = Convert.ToString(idr["Thumbnail"]).Replace("../", "https://dlt.neway5.com/"); }
                        result.Add(new MiniProOrderGoodsEntity
                        {
                            OrderPrice = Convert.ToDecimal(idr["Piece"]),
                            ProductCode = Convert.ToString(idr["ProductCode"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            BatchYear = Convert.ToInt32(idr["BatchYear"]),
                            name = Convert.ToString(idr["TypeName"]) + " " + Convert.ToString(idr["Specs"]) + " " + Convert.ToString(idr["Figure"]) + " " + Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]) + " " + Convert.ToString(idr["BatchYear"]) + "年",
                            TypeName = Convert.ToString(idr["TypeName"]),
                            GoodsCode = !string.IsNullOrEmpty(Convert.ToString(idr["ShowGoodsCode"])) ? Convert.ToString(idr["ShowGoodsCode"]) : Convert.ToString(idr["GoodsCode"]),
                            pic = Thumbnail,
                        });
                    }
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 查询供应商个人信息
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WxClientUserEntity> QueryWxDetailData(string OpenID)
        {
            List<WxClientUserEntity> result = new List<WxClientUserEntity>();

            string strSQL = $@"select top 1 * from Tbl_WX_Client  where wxOpenID='{OpenID}' ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new WxClientUserEntity
                        {
                            ID = long.Parse(Convert.ToString(idr["ID"])),
                            wxOpenID = Convert.ToString(idr["wxOpenID"]),
                            Name = Convert.ToString(idr["Name"]),
                            wxName = Convert.ToString(idr["wxName"]),
                            AvatarSmall = Convert.ToString(idr["AvatarSmall"]),
                            Cellphone = Convert.ToString(idr["Cellphone"]),
                            ClientNum = Convert.ToString(idr["ClientNum"]),
                            UserType = Convert.ToInt32(idr["UserType"]),
                            Province = Convert.ToString(idr["Province"]),
                            City = Convert.ToString(idr["City"]),
                            Country = Convert.ToString(idr["Country"]),
                            RegisterDate = Convert.ToDateTime(idr["RegisterDate"]),
                            Address = Convert.ToString(idr["Address"]),
                            ConsumerPoint = Convert.ToString(idr["ConsumerPoint"]),
                        });

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询供应商账单信息
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoSuppClientAccountEntityParams> QueryWxSupplierBillData(string AccountNO)
        {
            List<CargoSuppClientAccountEntityParams> result = new List<CargoSuppClientAccountEntityParams>();

            string strSQL = $@"select  * from Tbl_Cargo_SuppClientAccount as a where AccountNO='{AccountNO}' and a.Status = 1 and a.CheckStatus = 1 and a.AType = 0  order by CreateDate desc ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoSuppClientAccountEntityParams
                        {
                            AccountID = Convert.ToInt32(idr["AccountID"]),
                            AccountNO = Convert.ToString(idr["AccountNO"]),//账单号
                            AccountTitle = Convert.ToString(idr["AccountTitle"]),//账单名称
                            ClientID = Convert.ToInt32(idr["ClientID"]),//供应商编号
                            ClientNum = Convert.ToInt32(idr["ClientNum"]),//供应商编码
                            Status = Convert.ToInt32(idr["Status"]),//账单审核状态
                            Total = Convert.ToInt32(idr["Total"]),//账单金额
                            CheckStatus = Convert.ToInt32(idr["CheckStatus"]),//结算状态
                            AType = Convert.ToInt32(idr["AType"]),//账单类型
                            HouseID = Convert.ToInt32(idr["HouseID"]),//仓库ID
                            ElecSign = Convert.ToInt32(idr["ElecSign"]),//电子签名
                            ElecSignDate = string.IsNullOrEmpty(Convert.ToString(idr["ElecSignDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ElecSignDate"]),//签名时间
                            CreateDate = string.IsNullOrEmpty(Convert.ToString(idr["CreateDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CreateDate"]),//创建账单日期
                            ElecSignImg = string.IsNullOrEmpty(Convert.ToString(idr["ElecSignImg"])) ? "" : Convert.ToString(idr["ElecSignImg"]),//签名照片路径
                            GoodsList = GetSuppClientAccountGoods(Convert.ToString(idr["AccountNO"]))
                        });

                    }
                }
            }
            return result;
        }
        public List<CargoSuppClientAccountGoodsEntity> GetSuppClientAccountGoods(string AccountNO)
        {
            List<CargoSuppClientAccountGoodsEntity> result = new List<CargoSuppClientAccountGoodsEntity>();

            string strSQL = $@"select  * from Tbl_Cargo_SuppClientAccountGoods as a where AccountNO='{AccountNO}' order by ID ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoSuppClientAccountGoodsEntity
                        {
                            AccountNO = Convert.ToString(idr["AccountNO"]),//
                            OrderNo = Convert.ToString(idr["OrderNo"]),//
                            ProductID = Convert.ToInt32(idr["ProductID"]),
                            Total = Convert.ToInt32(idr["Total"]),//

                        });

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询服务号供应商信息
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WxClientUserEntity> QueryWxSupplierOpenID(string clientNum = null)
        {
            List<WxClientUserEntity> result = new List<WxClientUserEntity>();

            string strSQL = $@"select * from Tbl_WX_Client  where UserType=2 and ClientNum<>0 and isnull(Cellphone,'')<>'' ";
            if (!string.IsNullOrEmpty(clientNum)) strSQL += $@" and ClientNum='{clientNum}' ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new WxClientUserEntity
                        {
                            wxOpenID = Convert.ToString(idr["wxOpenID"]),
                            Name = Convert.ToString(idr["Name"]),
                            wxName = Convert.ToString(idr["wxName"]),
                            AvatarSmall = Convert.ToString(idr["AvatarSmall"]),
                            Cellphone = Convert.ToString(idr["Cellphone"]),
                            ClientNum = Convert.ToString(idr["ClientNum"]),
                            UserType = Convert.ToInt32(idr["UserType"]),
                        });

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询供应商销售数据
        /// </summary>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryWxSupplierSalesData(CargoOrderEntity entity, int days)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            days = days * -1;
            string end = DateTime.Now.ToString("yyyy-MM-dd 18:00:00");
            string start = DateTime.Now.AddDays(days).ToString("yyyy-MM-dd 18:00:00");

            //string strSQL = $@"select sum(b.Piece) as Piece,d.ClientName from Tbl_Cargo_Order as a inner join Tbl_Cargo_OrderGoods as b on a.OrderNo = b.OrderNo left join Tbl_Cargo_Client as d on a.SuppClientNum = d.ClientNum where a.SuppClientNum = '{entity.SuppClientNum}' and convert(varchar(10), CreateDate, 23) >= '{start}' and convert(varchar(10), CreateDate, 23) <= '{end}' group by d.ClientName ";
            string strSQL = $@"select *
                                     ,(SELECT STUFF((select top 3 ',' +SettleHouseName from Tbl_Cargo_SettleHouse   WHERE ClientNum=aa.SuppClientNum group by SettleHouseName FOR XML PATH('')), 1, 1, '') AS combined_values) HouseName
                                     ,(SELECT STUFF((select top 3 ',' + isnull(a3.ProductName, '-') from  Tbl_Cargo_OrderGoods as a1 inner join Tbl_Cargo_Product as a2 on a1.ProductID = a2.ProductID inner join Tbl_Cargo_ProductSpec as a3 on a2.ProductCode = a3.ProductCode WHERE a1.OrderNo in(select top 1 OrderNo from Tbl_Cargo_Order where ThrowGood in(23,22) and SuppClientNum in(aa.SuppClientNum) and convert(varchar(18), CreateDate, 20) >= '{start}'  and convert(varchar(18), CreateDate, 20) <= '{end}') group by a3.ProductName FOR XML PATH('')), 1, 1, '') AS combined_values) GoodsName
                                from (
                                    select sum(b.Piece) as Piece, d.ClientName,SuppClientNum
                                from Tbl_Cargo_Order as a
                                         inner join Tbl_Cargo_OrderGoods as b on a.OrderNo = b.OrderNo
                                         left join Tbl_Cargo_Client as d on a.SuppClientNum = d.ClientNum
                                where a.SuppClientNum in(select distinct aa.ClientNum from Tbl_WX_Client as aa where aa.UserType=2 and isnull(aa.ClientNum,0)<>0) and ThrowGood in(23,22)
                                  and convert(varchar(18), CreateDate, 20) >= '{start}'
                                  and convert(varchar(18), CreateDate, 20) <= '{end}'
                                group by d.ClientName,a.SuppClientNum
                                              ) as aa ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoOrderEntity
                        {
                            //wxOpenID = Convert.ToString(idr["wxOpenID"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            ClientName = Convert.ToString(idr["ClientName"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            GoodsName = Convert.ToString(idr["GoodsName"]),
                            SuppClientNum = Convert.ToInt32(idr["SuppClientNum"]),
                        });

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询供应商对应仓库数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXSerViceSettleHouseEntity> QueryServiceHouseData(string OpenID, string days, string currdate)
        {

            List<WXSerViceSettleHouseEntity> result = new List<WXSerViceSettleHouseEntity>();

            string strSQL = $@"select  b.* from Tbl_WX_Client as a inner join Tbl_Cargo_SettleHouse as b on a.ClientNum=b.ClientNum where a.wxOpenID='{OpenID}' ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {

                        result.Add(new WXSerViceSettleHouseEntity
                        {
                            WxOpenID = (OpenID),
                            SettleHouseID = Convert.ToString(idr["SettleHouseID"]),
                            SettleHouseName = Convert.ToString(idr["SettleHouseName"]),
                            ClientID = Convert.ToInt32(idr["ClientID"]),
                            ClientNum = Convert.ToInt32(idr["ClientNum"]),
                            OrderList = null,
                            ReturnOrderList = null
                        });

                    }
                }

                //获取所有销售详情数据
                var orderList = QueryServiceOrderData(OpenID, Convert.ToInt32(days), currdate);
                var OrderList = orderList.Where(a => a.OrderModel != "1" && a.ThrowGood != "5").ToList();
                var ReturnOrderList = orderList.Where(a => a.OrderModel == "1" && a.ThrowGood == "5").ToList();

                foreach (var item in result)
                {
                    item.OrderList = OrderList.Where(a => a.HouseID.ToString() == item.SettleHouseID).ToList();
                    item.ReturnOrderList = ReturnOrderList.Where(a => a.HouseID.ToString() == item.SettleHouseID).ToList();
                }
            }
            result = result.Where(a => a.OrderList.Count > 0 || a.ReturnOrderList.Count > 0).OrderByDescending(a => a.OrderList.Count + a.ReturnOrderList.Count).ToList();
            return result;
        }

        /// <summary>
        /// 获取退款数据
        /// </summary>
        /// <returns></returns>
        public List<WXOrderEntity> QueryRefundOrderData(string orderNo)
        {

            List<WXOrderEntity> result = new List<WXOrderEntity>();

            string strSQL = $@"select a.*,b.Name as HouseName,c.CompanyName,d.IsTransitFee,d.TransitFee,c.wxName from Tbl_WX_Order as a inner join tbl_Cargo_house as b on a.HouseID = b.HouseID inner join Tbl_WX_Client as c on a.WXID = c.ID inner join Tbl_Cargo_Order as d on a.OrderNo = d.WXOrderNo and a.RefundCheckStatus = 1 where a.OrderNo='{orderNo}'";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new WXOrderEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            PayStatus = Convert.ToString(idr["PayStatus"]),
                            IsTransitFee = Convert.ToString(idr["IsTransitFee"]),
                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                            Trxid = Convert.ToString(idr["Trxid"]),
                            WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            WXID = long.Parse(Convert.ToString(idr["WXID"])),
                            wxName = (Convert.ToString(idr["wxName"])),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            RefundMemo = Convert.ToString(idr["RefundMemo"]),
                            RefundReason = Convert.ToString(idr["RefundReason"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            AccountNo = Convert.ToString(idr["AccountNo"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            RefundCheckStatus = Convert.ToString(idr["RefundCheckStatus"]),
                            RefundDate = string.IsNullOrEmpty(Convert.ToString(idr["RefundDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["RefundDate"]),
                            SuppClientNum = Convert.ToInt32(idr["SuppClientNum"])
                        });

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取订单数据
        /// </summary>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryOrderDatas(string orderNo)
        {

            List<CargoOrderEntity> result = new List<CargoOrderEntity>();

            string strSQL = $@"
                select a.*,b.Name as HouseName,b.Cellphone from Tbl_Cargo_Order as a
                         inner join tbl_Cargo_house as b on a.HouseID = b.HouseID
                         inner join Tbl_WX_Order as d on d.OrderNo=a.WXOrderNo
                         inner join Tbl_WX_Client as c on d.WXID = c.ID
                where a.OrderNo= '{orderNo}'
                ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                            DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                            OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            Rebate = Convert.ToDecimal(idr["Rebate"]),
                            CheckOutType = Convert.ToString(idr["CheckOutType"]),
                            TrafficType = Convert.ToString(idr["TrafficType"]),
                            DeliveryType = Convert.ToString(idr["DeliveryType"]),
                            PayClientName = Convert.ToString(idr["PayClientName"]),
                            PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                            CheckStatus = Convert.ToString(idr["CheckStatus"]),
                            Remark = Convert.ToString(idr["Remark"]),
                            AwbStatus = Convert.ToString(idr["AwbStatus"]),
                            CreateAwb = Convert.ToString(idr["CreateAwb"]),
                            SaleManID = Convert.ToString(idr["SaleManID"]),
                            SaleManName = Convert.ToString(idr["SaleManName"]),
                            Cellphone = Convert.ToString(idr["Cellphone"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            OrderModel = Convert.ToString(idr["OrderModel"]),
                            FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]),
                            FinanceSecondCheckName = Convert.ToString(idr["FinanceSecondCheckName"]),
                            FinanceSecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FinanceSecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FinanceSecondCheckDate"]),
                            //PreReceiveMoney = Convert.ToDecimal(idr["PreReceiveMoney"]),
                            //RebateMoney = Convert.ToDecimal(idr["RebateMoney"]),
                            ClientNum = Convert.ToInt32(idr["ClientNum"]),
                            OP_ID = Convert.ToString(idr["OP_ID"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            //WXOrderNo = Convert.ToString(idr["OrderType"]).Equals("0") || Convert.ToString(idr["OrderType"]).Equals("1") ? "" : Convert.ToString(idr["WXOrderNo"]),
                            //PayWay = Convert.ToString(idr["PayWay"]),
                            //WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            //ReceivedMoney = Convert.ToDecimal(idr["ReceivedMoney"]),
                            goodsList = QueryOrderGoodsDatas(Convert.ToString(idr["OrderNo"]))
                        });

                    }
                }
            }
            return result;
        }

        public List<CargoOrderGoodsEntity> QueryOrderGoodsDatas(string OrderNo)
        {
            List<CargoOrderGoodsEntity> result = new List<CargoOrderGoodsEntity>();

            string strSQL = $@"
         select a.OrderID, a.OrderNo, a.OrderNum, a.HAwbNo, a.LogisAwbNo, a.LogisID, a.Dep, a.Dest, a.Weight, a.Volume, a.InsuranceFee, a.TransportFee
     , a.DeliveryFee, a.OtherFee, a.TotalCharge, a.Rebate, a.CheckOutType, a.TrafficType, a.DeliveryType, a.AcceptUnit, a.AcceptPeople, a.AcceptTelephone
     , a.AcceptCellphone, a.AcceptAddress, a.CreateAwb, a.CreateDate, a.CheckStatus, a.Signer, a.SignTime, a.OP_ID, a.OP_DATE, a.AwbStatus, a.Remark, a.SaleManID
     , a.SaleManName, a.SaleCellphone, a.CreateAwbID, a.OrderType, a.FinanceSecondCheck, a.FinanceSecondCheckName, a.FinanceSecondCheckDate, a.OrderModel
     , a.ClientNum, a.WXOrderNo, a.ThrowGood, a.TranHouse, a.OutHouseName, a.ModifyPriceStatus, a.PayClientNum, a.PayClientName, a.BelongHouse, a.IsPrintPrice
     , a.AccountNo, a.PostponeShip, a.OutCargoTime, a.DeliverySettlement, a.OrderAging, a.PrintNum, a.PollStatus, a.CheckDate, a.PickStatus, a.LineID, a.LineName
     , a.ShopCode, a.CollectMoney, a.PlanOrderNo, a.TakeOrderName, a.TakeOrderTime, a.SendCarName, a.SendCarTime, a.OpenOrderNo, a.OpenOrderSource
     , a.BusinessID, a.OverDueFee, a.OpenExpressName, a.OpenExpressNum, a.CouponType, a.AbSignStatus, a.DeliveryDriverName, a.DriverIDNum, a.DriverCellphone
     , a.DriverCarNum, a.IsSupplierType, a.IsHouseType, a.ShareHouseID, a.ShareHouseName, a.OrignHouseID, a.OrignHouseName, a.BateAmount
     , a.OnlinePaidAmount
     ,e.ProductID,e.HouseID,e.AreaID,e.Piece,e.ContainerCode, b.Name as HouseName, c.CompanyName, a.IsTransitFee, d.TransitFee
     ,f.ProductCode,ProductName,GoodsName,GoodsCode,f.TypeID,g.TypeName,Specs,Model,Figure,LoadIndex,SpeedLevel,f.SuppClientNum,e.ActSalePrice
                from Tbl_Cargo_Order as a
                         inner join tbl_Cargo_house as b on a.HouseID = b.HouseID
                         inner join Tbl_WX_Order as d on d.OrderNo=a.WXOrderNo
                         inner join Tbl_WX_Client as c on d.WXID = c.ID
                         inner join Tbl_Cargo_OrderGoods as e on a.OrderNo=e.OrderNo
                         inner join Tbl_Cargo_Product as f on e.ProductID=f.ProductID
                         inner join Tbl_Cargo_ProductType as g on f.TypeID=g.TypeID
                where a.OrderNo = '{OrderNo}'
                ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoOrderGoodsEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            ProductID = Convert.ToInt64(idr["ProductID"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            AreaID = Convert.ToInt32(idr["AreaID"]),
                            ProductName = Convert.ToString(idr["ProductName"]),
                            ProductCode = Convert.ToString(idr["ProductCode"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            Model = Convert.ToString(idr["Model"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            ActSalePrice = string.IsNullOrEmpty(Convert.ToString(idr["ActSalePrice"])) ? 0 : Convert.ToDecimal(idr["ActSalePrice"]),
                            TypeID = Convert.ToInt32(idr["TypeID"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            //Born = Convert.ToString(idr["Born"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            OP_ID = Convert.ToString(idr["OP_ID"]),
                            //OutCargoID = Convert.ToString(idr["OutCargoID"]),
                            //RuleType = Convert.ToString(idr["RuleType"]),
                            SuppClientNum = string.IsNullOrEmpty(Convert.ToString(idr["SuppClientNum"])) ? 0 : Convert.ToInt32(idr["SuppClientNum"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            LoadSpeed = Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                        });

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询供应商对应仓库销售数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderGoodsEntity> QueryServiceOrderData(string openid, int days, string currdate)
        {
            List<CargoOrderGoodsEntity> result = new List<CargoOrderGoodsEntity>();
            var CurrDate = string.IsNullOrEmpty(currdate) ? DateTime.Now : Convert.ToDateTime(currdate);
            var end = CurrDate.ToString("yyyy-MM-dd 18:00:00");
            var start = CurrDate.AddDays((days * -1)).ToString("yyyy-MM-dd 18:00:00");

            string strSQL = $@"
select b.Piece,OrderModel,ThrowGood ,b.ActSalePrice,c.Model,c.Specs,c.Figure ,c.Batch,c.SpeedLevel,c.LoadIndex,ff.TypeName,c.GoodsCode,c.ProductID,c.ProductCode,a.HouseID from Tbl_Cargo_Order as a inner join Tbl_Cargo_OrderGoods as b on a.OrderNo=b.OrderNo inner join Tbl_Cargo_Product c on b.ProductID=c.ProductID inner join Tbl_Cargo_ProductType as ff on c.TypeID = ff.TypeID inner join Tbl_WX_Client as twx on a.SuppClientNum=twx.ClientNum inner join Tbl_Cargo_SettleHouse as tcs on twx.ClientNum=tcs.ClientNum and a.HouseID=tcs.SettleHouseID
where   ThrowGood in(23,22) and twx.UserType=2 and isnull(twx.ClientNum,0)<>0 and twx.wxOpenID='{openid}' and convert(varchar(18), CreateDate, 20)>='{start}' and convert(varchar(18), CreateDate, 20)<='{end}'

 ";
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmdAdd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoOrderGoodsEntity
                        {
                            Piece = Convert.ToInt32(idr["Piece"]),
                            OrderModel = Convert.ToString(idr["OrderModel"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                            Model = Convert.ToString(idr["Model"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            ProductCode = Convert.ToString(idr["ProductCode"]),
                            ProductID = Convert.ToInt32(idr["ProductID"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 更新服务号数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxStoresData(WxClientUserEntity entity)
        {
            string strSQL = @"update Tbl_WX_Client set ClientNum=@ClientNum,Cellphone=@Cellphone where wxOpenID=@wxOpenID and UserType=@UserType";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int64, entity.ClientNum);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@UserType", DbType.Int32, entity.UserType);
                    conn.AddInParameter(cmd, "@wxOpenID", DbType.String, entity.wxOpenID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// 更新签名数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxSuppSignature(CargoSuppClientAccountEntity entity)
        {
            string strSQL = @"update Tbl_Cargo_SuppClientAccount set ElecSign=@ElecSign,ElecSignImg=@ElecSignImg,ElecSignDate=@ElecSignDate where AccountNO=@AccountNO";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ElecSign", DbType.Int64, entity.ElecSign);
                    conn.AddInParameter(cmd, "@ElecSignImg", DbType.String, entity.ElecSignImg);
                    conn.AddInParameter(cmd, "@ElecSignDate", DbType.DateTime, entity.ElecSignDate);
                    conn.AddInParameter(cmd, "@AccountNO", DbType.String, entity.AccountNO);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
