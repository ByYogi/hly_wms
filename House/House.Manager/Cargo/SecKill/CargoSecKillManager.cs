using House.DataAccess;
using House.Entity.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace House.Manager.Cargo
{
    public class CargoSecKillManager
    {
        private SqlHelper conn = new SqlHelper();
        /// <summary>
        /// 查询淘宝订单数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryCargoHouse(int pIndex, int pNum, WXTaobaoEntity entity)
        {
            List<WXTaobaoEntity> result = new List<WXTaobaoEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.*,b.wxName,b.wxOpenID,b.AvatarBig,ISNULL(b.ParentID,0) as ParentID,c.wxName as OneWxName,d.wxName as SecendWxName,ISNULL(d.ID,0) as SecendWxID from Tbl_Taobao_Order as a left join Tbl_WX_Client as b on a.WXID=b.ID left join Tbl_WX_Client as c on b.ParentID=c.ID left join Tbl_WX_Client as d on c.ParentID=d.ID WHERE (1=1)";
                if (!string.IsNullOrEmpty(entity.wxName))
                {
                    strSQL += " and b.wxName like '%" + entity.wxName + "%'";
                }
                if (!string.IsNullOrEmpty(entity.buyer_nick))
                {
                    strSQL += " and a.buyer_nick like '%" + entity.buyer_nick + "%'";
                }
                if (!string.IsNullOrEmpty(entity.buyer_alipay))
                {
                    strSQL += " and a.buyer_alipay like '%" + entity.buyer_alipay + "%'";
                }
                if (!string.IsNullOrEmpty(entity.status))
                {
                    strSQL += " and a.status like '%" + entity.status + "%'";
                }
                //收货人姓名
                if (!string.IsNullOrEmpty(entity.receiver_name))
                {
                    strSQL += " and a.receiver_name like '%" + entity.receiver_name + "%'";
                }
                if (!string.IsNullOrEmpty(entity.receiver_mobile))
                {
                    strSQL += " and a.receiver_mobile like '%" + entity.receiver_mobile + "%'";
                }
                if (!string.IsNullOrEmpty(entity.TaobaoID))
                {
                    strSQL += " and a.TaobaoID like '%" + entity.TaobaoID + "%'";
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new WXTaobaoEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                ParentID = Convert.ToInt64(idr["ParentID"]),
                                wxName = Convert.ToString(idr["wxName"]).Trim(),
                                AvatarBig = Convert.ToString(idr["AvatarBig"]).Trim(),
                                wxOpenID = Convert.ToString(idr["wxOpenID"]).Trim(),
                                TaobaoID = Convert.ToString(idr["TaobaoID"]).Trim(),
                                buyer_nick = Convert.ToString(idr["buyer_nick"]).Trim(),
                                pic_path = Convert.ToString(idr["pic_path"]).Trim(),
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
                                buyer_alipay = Convert.ToString(idr["buyer_alipay"]),
                                logicCompany = Convert.ToString(idr["logicCompany"]),
                                logicno = Convert.ToString(idr["logicno"]),
                                num = Convert.ToInt32(idr["num"]),
                                Rebate = Convert.ToDecimal(idr["payment"]).Equals(0) ? 0 : 4 * Convert.ToInt32(idr["num"]),
                                status = Convert.ToString(idr["status"]),
                                Title = Convert.ToString(idr["Title"]),
                                OneWxName = Convert.ToString(idr["OneWxName"]),
                                OneMoney = Convert.ToInt64(idr["ParentID"]).Equals(0) || Convert.ToDecimal(idr["payment"]).Equals(0) ? 0 : 10 * Convert.ToInt32(idr["num"]),
                                SecendWxName = Convert.ToString(idr["SecendWxName"]),
                                SecendWxID = Convert.ToInt64(idr["SecendWxID"]),
                                SecendMoney = Convert.ToInt64(idr["SecendWxID"]).Equals(0) || Convert.ToDecimal(idr["payment"]).Equals(0) ? 0 : 6 * Convert.ToInt32(idr["num"]),
                                buyer_rate = Convert.ToString(idr["buyer_rate"]),
                                created = string.IsNullOrEmpty(Convert.ToString(idr["created"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["created"]),
                                pay_time = string.IsNullOrEmpty(Convert.ToString(idr["pay_time"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["pay_time"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_Taobao_Order as a left join Tbl_WX_Client as b on a.WXID=b.ID Where (1=1)";
                if (!string.IsNullOrEmpty(entity.wxName))
                {
                    strCount += " and b.wxName like '%" + entity.wxName + "%'";
                }
                if (!string.IsNullOrEmpty(entity.buyer_nick))
                {
                    strCount += " and a.buyer_nick like '%" + entity.buyer_nick + "%'";
                }
                if (!string.IsNullOrEmpty(entity.buyer_alipay))
                {
                    strCount += " and a.buyer_alipay like '%" + entity.buyer_alipay + "%'";
                }
                if (!string.IsNullOrEmpty(entity.status))
                {
                    strCount += " and a.status like '%" + entity.status + "%'";
                }
                //收货人姓名
                if (!string.IsNullOrEmpty(entity.receiver_name))
                {
                    strCount += " and a.receiver_name like '%" + entity.receiver_name + "%'";
                }
                if (!string.IsNullOrEmpty(entity.receiver_mobile))
                {
                    strCount += " and a.receiver_mobile like '%" + entity.receiver_mobile + "%'";
                }
                if (!string.IsNullOrEmpty(entity.TaobaoID))
                {
                    strCount += " and a.TaobaoID like '%" + entity.TaobaoID + "%'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 判断是否存在此淘宝订单号
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
        /// <summary>
        /// 保存淘宝订单
        /// </summary>
        /// <param name="entity"></param>
        public void SaveTaobaoOrderData(List<WXTaobaoEntity> entity)
        {
            foreach (var it in entity)
            {
                it.EnSafe();
                //1.判断是否已经存在此淘宝订单数据 
                if (IsExistTaobaoOrder(it))
                {
                    WXTaobaoEntity tb = QueryTaobaoOrderByID(it.TaobaoID);
                    //2.已经存在此淘宝订单数据，修改
                    UpdateTaobaoOrderData(it);
                    //3.修改上级和上上级人返利和提成金额
                    WXTaobaoEntity userLevel = QueryWxUserLevel(new WXTaobaoEntity { WXID = tb.WXID });
                    //4.先删除返利表的返利数据 
                    DeleteTaobaoCashBack(tb.TaobaoID);
                    //5.再新增进来返利和提成数据
                    if (!userLevel.WXID.Equals(0))
                    {
                        AddTaobaoCashBack(new WXTaobaoCashBackEntity
                        {
                            WXID = userLevel.WXID,
                            TaobaoID = it.TaobaoID,
                            payment = it.payment,
                            CashType = "0",
                            Cash = it.num * 4
                        });
                    }
                    if (!userLevel.OneWxID.Equals(0))
                    {
                        AddTaobaoCashBack(new WXTaobaoCashBackEntity
                        {
                            WXID = userLevel.OneWxID,
                            TaobaoID = it.TaobaoID,
                            payment = it.payment,
                            CashType = "1",
                            Cash = it.num * 10
                        });
                    }
                    if (!userLevel.SecendWxID.Equals(0))
                    {
                        AddTaobaoCashBack(new WXTaobaoCashBackEntity
                        {
                            WXID = userLevel.SecendWxID,
                            TaobaoID = it.TaobaoID,
                            payment = it.payment,
                            CashType = "1",
                            Cash = it.num * 6
                        });
                    }
                }
                else
                {
                    AddTaobaoOrderData(it);
                }
            }

        }
        /// <summary>
        /// 删除订单返利表数据
        /// </summary>
        /// <param name="taobaoID"></param>
        public void DeleteTaobaoCashBack(string taobaoID)
        {
            string strSQL = "Delete from Tbl_Taobao_CashBack Where TaobaoID=@TaobaoID";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@TaobaoID", DbType.String, taobaoID);
                conn.ExecuteNonQuery(cmdQ);
            }
        }
        /// <summary>
        /// 新增淘宝返利数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddTaobaoCashBack(WXTaobaoCashBackEntity entity)
        {
            string strSQL = "Insert into Tbl_Taobao_CashBack(WXID,TaobaoID,payment,CashType,Cash) values (@WXID,@TaobaoID,@payment,@CashType,@Cash)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    conn.AddInParameter(cmd, "@TaobaoID", DbType.String, entity.TaobaoID);
                    conn.AddInParameter(cmd, "@payment", DbType.Decimal, entity.payment);
                    conn.AddInParameter(cmd, "@CashType", DbType.String, entity.CashType);
                    conn.AddInParameter(cmd, "@Cash", DbType.Decimal, entity.Cash);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 通过淘宝订单号查询淘宝订单数据
        /// </summary>
        /// <param name="taobaoID"></param>
        /// <returns></returns>
        public WXTaobaoEntity QueryTaobaoOrderByID(string taobaoID)
        {
            WXTaobaoEntity result = new WXTaobaoEntity();
            string strSQL = "Select * from Tbl_Taobao_Order Where TaobaoID=@TaobaoID";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@TaobaoID", DbType.String, taobaoID);
                using (DataTable dt = conn.ExecuteDataTable(cmdQ))
                {
                    foreach (DataRow idrCount in dt.Rows)
                    {
                        result.WXID = Convert.ToInt64(idrCount["WXID"]);
                        result.TaobaoID = Convert.ToString(idrCount["TaobaoID"]).Trim();
                        result.payment = Convert.ToDecimal(idrCount["payment"]);
                        result.num = Convert.ToInt32(idrCount["num"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 修改淘宝订单
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateTaobaoOrderData(WXTaobaoEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_Taobao_Order set buyer_nick=@buyer_nick,pic_path=@pic_path,payment=@payment,price=@price,total_fee=@total_fee,buyer_rate=@buyer_rate,receiver_name=@receiver_name,receiver_state=@receiver_state,receiver_city=@receiver_city,receiver_district=@receiver_district,receiver_address=@receiver_address,receiver_mobile=@receiver_mobile,receiver_phone=@receiver_phone,num=@num,status=@status,Title=@Title,created=@created,pay_time=@pay_time,logicCompany=@logicCompany,logicno=@logicno,buyer_alipay=@buyer_alipay,Shop=@Shop Where TaobaoID=@TaobaoID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@TaobaoID", DbType.String, entity.TaobaoID);
                    conn.AddInParameter(cmd, "@buyer_nick", DbType.String, entity.buyer_nick);
                    conn.AddInParameter(cmd, "@pic_path", DbType.String, entity.pic_path);
                    conn.AddInParameter(cmd, "@payment", DbType.Decimal, entity.payment);
                    conn.AddInParameter(cmd, "@price", DbType.Decimal, entity.price);
                    conn.AddInParameter(cmd, "@total_fee", DbType.Decimal, entity.total_fee);
                    conn.AddInParameter(cmd, "@buyer_rate", DbType.String, entity.buyer_rate);
                    conn.AddInParameter(cmd, "@receiver_name", DbType.String, entity.receiver_name);
                    conn.AddInParameter(cmd, "@receiver_state", DbType.String, entity.receiver_state);
                    conn.AddInParameter(cmd, "@receiver_city", DbType.String, entity.receiver_city);
                    conn.AddInParameter(cmd, "@receiver_district", DbType.String, entity.receiver_district);
                    conn.AddInParameter(cmd, "@receiver_address", DbType.String, entity.receiver_address);
                    conn.AddInParameter(cmd, "@receiver_mobile", DbType.String, entity.receiver_mobile);
                    conn.AddInParameter(cmd, "@receiver_phone", DbType.String, entity.receiver_phone);
                    conn.AddInParameter(cmd, "@num", DbType.Int32, entity.num);
                    conn.AddInParameter(cmd, "@status", DbType.String, entity.status);
                    conn.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
                    conn.AddInParameter(cmd, "@created", DbType.DateTime, entity.created);
                    conn.AddInParameter(cmd, "@pay_time", DbType.DateTime, entity.pay_time);
                    conn.AddInParameter(cmd, "@logicCompany", DbType.String, entity.logicCompany);
                    conn.AddInParameter(cmd, "@logicno", DbType.String, entity.logicno);
                    conn.AddInParameter(cmd, "@buyer_alipay", DbType.String, entity.buyer_alipay);
                    conn.AddInParameter(cmd, "@Shop", DbType.String, entity.Shop);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增淘宝订单
        /// </summary>
        /// <param name="entity"></param>
        public void AddTaobaoOrderData(WXTaobaoEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Insert into Tbl_Taobao_Order(WXID,TaobaoID,buyer_nick,pic_path,payment,price,total_fee,buyer_rate,receiver_name,receiver_state,receiver_city,receiver_district,receiver_address,receiver_mobile,receiver_phone,num,status,Title,created,pay_time,logicCompany,logicno,buyer_alipay,Shop) values (@WXID,@TaobaoID,@buyer_nick,@pic_path,@payment,@price,@total_fee,@buyer_rate,@receiver_name,@receiver_state,@receiver_city,@receiver_district,@receiver_address,@receiver_mobile,@receiver_phone,@num,@status,@Title,@created,@pay_time,@logicCompany,@logicno,@buyer_alipay,@Shop)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@WXID", DbType.Int64, entity.WXID);
                    conn.AddInParameter(cmd, "@TaobaoID", DbType.String, entity.TaobaoID);
                    conn.AddInParameter(cmd, "@buyer_nick", DbType.String, entity.buyer_nick);
                    conn.AddInParameter(cmd, "@pic_path", DbType.String, entity.pic_path);
                    conn.AddInParameter(cmd, "@payment", DbType.Decimal, entity.payment);
                    conn.AddInParameter(cmd, "@price", DbType.Decimal, entity.price);
                    conn.AddInParameter(cmd, "@total_fee", DbType.Decimal, entity.total_fee);
                    conn.AddInParameter(cmd, "@buyer_rate", DbType.String, entity.buyer_rate);
                    conn.AddInParameter(cmd, "@receiver_name", DbType.String, entity.receiver_name);
                    conn.AddInParameter(cmd, "@receiver_state", DbType.String, entity.receiver_state);
                    conn.AddInParameter(cmd, "@receiver_city", DbType.String, entity.receiver_city);
                    conn.AddInParameter(cmd, "@receiver_district", DbType.String, entity.receiver_district);
                    conn.AddInParameter(cmd, "@receiver_address", DbType.String, entity.receiver_address);
                    conn.AddInParameter(cmd, "@receiver_mobile", DbType.String, entity.receiver_mobile);
                    conn.AddInParameter(cmd, "@receiver_phone", DbType.String, entity.receiver_phone);
                    conn.AddInParameter(cmd, "@num", DbType.Int32, entity.num);
                    conn.AddInParameter(cmd, "@status", DbType.String, entity.status);
                    conn.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
                    conn.AddInParameter(cmd, "@created", DbType.DateTime, entity.created);
                    conn.AddInParameter(cmd, "@pay_time", DbType.DateTime, entity.pay_time);
                    conn.AddInParameter(cmd, "@logicCompany", DbType.String, entity.logicCompany);
                    conn.AddInParameter(cmd, "@logicno", DbType.String, entity.logicno);
                    conn.AddInParameter(cmd, "@buyer_alipay", DbType.String, entity.buyer_alipay);
                    conn.AddInParameter(cmd, "@Shop", DbType.String, entity.Shop);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询微信用户的上下级
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXTaobaoEntity QueryWxUserLevel(WXTaobaoEntity entity)
        {
            WXTaobaoEntity result = new WXTaobaoEntity();
            string strSQL = "select ISNULL(a.ParentID,0) as OneWxID,ISNULL(b.ParentID,0) as SecendWxID,a.ID as WXID from Tbl_WX_Client as a left join Tbl_WX_Client as b on a.ParentID=b.ID where a.ID=@ID";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@ID", DbType.Int64, entity.WXID);
                using (DataTable dt = conn.ExecuteDataTable(cmdQ))
                {
                    foreach (DataRow idrCount in dt.Rows)
                    {
                        result.WXID = Convert.ToInt64(idrCount["WXID"]);
                        result.OneWxID = Convert.ToInt64(idrCount["OneWxID"]);
                        result.SecendWxID = Convert.ToInt64(idrCount["SecendWxID"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 删除 淘宝订单
        /// </summary>
        public void DelTaobaoOrder(List<WXTaobaoEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"Delete from Tbl_Taobao_Order where TaobaoID=@TaobaoID ";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@TaobaoID", DbType.String, it.TaobaoID);
                        conn.ExecuteNonQuery(cmd);
                    }
                    DeleteTaobaoCashBack(it.TaobaoID);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询关注汽修推广数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryAutoCarSpread(int pIndex, int pNum, WXSecKillEntity entity)
        {
            List<WXSecKillEntity> result = new List<WXSecKillEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.*,b.wxName,b.wxOpenID,b.AvatarBig,c.wxName as OneWxName from Tbl_WX_SecKill as a left join Tbl_WX_Client as b on a.WXID=b.ID left join Tbl_WX_Client as c on a.ParentID=c.ID WHERE (1=1)";
                if (!string.IsNullOrEmpty(entity.wxName))
                {
                    strSQL += " and b.wxName like '%" + entity.wxName + "%'";
                }
                if (!string.IsNullOrEmpty(entity.CarNum))
                {
                    strSQL += " and a.CarNum like '%" + entity.CarNum + "%'";
                }
                if (!string.IsNullOrEmpty(entity.Cellphone))
                {
                    strSQL += " and a.Cellphone like '%" + entity.Cellphone + "%'";
                }
                if (!string.IsNullOrEmpty(entity.UseStatus))
                {
                    strSQL += " and a.UseStatus = '" + entity.UseStatus + "'";
                }
                if (!string.IsNullOrEmpty(entity.OneWxName))
                {
                    strSQL += " and c.wxName like '%" + entity.OneWxName + "%'";
                }
                if (!string.IsNullOrEmpty(entity.Company))
                {
                    strSQL += " and a.Company='" + entity.Company + "'";
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new WXSecKillEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                ParentID = Convert.ToInt64(idr["ParentID"]),
                                wxName = Convert.ToString(idr["wxName"]).Trim(),
                                AvatarBig = Convert.ToString(idr["AvatarBig"]).Trim(),
                                wxOpenID = Convert.ToString(idr["wxOpenID"]).Trim(),
                                CarNum = Convert.ToString(idr["CarNum"]).Trim(),
                                Cellphone = Convert.ToString(idr["Cellphone"]).Trim(),
                                UseStatus = Convert.ToString(idr["UseStatus"]).Trim(),
                                OneWxName = Convert.ToString(idr["OneWxName"]),
                                Company = Convert.ToString(idr["Company"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_WX_SecKill as a left join Tbl_WX_Client as b on a.WXID=b.ID left join Tbl_WX_Client as c on a.ParentID=c.ID Where (1=1)";
                if (!string.IsNullOrEmpty(entity.wxName))
                {
                    strCount += " and b.wxName like '%" + entity.wxName + "%'";
                }
                if (!string.IsNullOrEmpty(entity.CarNum))
                {
                    strCount += " and a.CarNum like '%" + entity.CarNum + "%'";
                }
                if (!string.IsNullOrEmpty(entity.Cellphone))
                {
                    strCount += " and a.Cellphone like '%" + entity.Cellphone + "%'";
                }
                if (!string.IsNullOrEmpty(entity.UseStatus))
                {
                    strCount += " and a.UseStatus = '" + entity.UseStatus + "'";
                }
                if (!string.IsNullOrEmpty(entity.OneWxName))
                {
                    strCount += " and c.wxName like '%" + entity.OneWxName + "%'";
                }
                if (!string.IsNullOrEmpty(entity.Company))
                {
                    strCount += " and a.Company='" + entity.Company + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 设置使用状态
        /// </summary>
        /// <param name="entity"></param>
        public void setUseStatus(List<WXSecKillEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"Update Tbl_WX_SecKill Set UseStatus=@UseStatus where ID=@ID ";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@UseStatus", DbType.String, it.UseStatus);
                        conn.AddInParameter(cmd, "@ID", DbType.Int64, it.ID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
    }
}
