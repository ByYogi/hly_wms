using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.House;
using Memcached.ClientLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Arrive
{
    public partial class ArriveApi : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string methodName = string.Empty;
            try
            {
                methodName = Request["method"];
                if (String.IsNullOrEmpty(methodName)) return;
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                method.Invoke(this, null);
            }
            catch (Exception ex)
            {
                LogBus bus = new LogBus();
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Operate = "";
                log.Moudle = "";
                log.Status = "1";
                log.NvgPage = "";
                log.UserID = UserInfor.LoginName.Trim();
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
        }
        #region 托运合同录入

        /// <summary>
        /// 自动完成客户名称信息
        /// </summary>
        public void AutoCompleteClient()
        {
            ClientEntity queryEntity = new ClientEntity();
            #region 缓存
            //string[] serverlist = ConfigurationSettings.AppSettings["memcachedServer"].Split('/');
            //SockIOPool pool = SockIOPool.GetInstance(ConfigurationSettings.AppSettings["PoolName"]);
            //pool.SetServers(serverlist);
            //pool.InitConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["InitConnections"]);//连接池初始容量
            //pool.MinConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MinConnections"]);//最小容量
            //pool.MaxConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MaxConnections"]);//最大容量
            //pool.SocketConnectTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketConnectTimeout"]);//数据读取超时时间
            //pool.SocketTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketTimeout"]);//Socket连接超时时间
            //pool.MaintenanceSleep = Convert.ToInt64(ConfigurationSettings.AppSettings["MaintenanceSleep"]);//线程池维护线程之间的休眠时间
            //pool.Failover = Convert.ToBoolean(ConfigurationSettings.AppSettings["Failover"]);//使用缓存服务器自动切换功能，当一台服务器死了可以自动切换到另外一台查找缓存

            //pool.Nagle = Convert.ToBoolean(ConfigurationSettings.AppSettings["Nagle"]);//禁用Nagle算法
            //pool.Initialize();
            //MemcachedClient mc = new MemcachedClient();
            //mc.PoolName = ConfigurationSettings.AppSettings["PoolName"];
            //mc.EnableCompression = true;
            //mc.CompressionThreshold = 10240;
            #endregion
            queryEntity.DelFlag = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            List<ClientSessionEntity> list = new List<ClientSessionEntity>();
            //string memc = UserInfor.CacheTag.Trim();
            //if (mc.KeyExists(memc))
            //{
            //    list = (List<ClientSessionEntity>)mc.Get(memc);
            //    if (list.Count <= 0)
            //    {
            //        list = bus.QueryClientSession(queryEntity);
            //        mc.Add(memc, list);
            //    }
            //}
            //else
            //{
            //    list = bus.QueryClientSession(queryEntity);
            //    mc.Add(memc, list);
            //}
            list = bus.QueryClientSession(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据入库员代码查询用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void GetUserNameByClerkNo()
        {
            string id = Convert.ToString(Request["cnum"]);
            ArriveBus bus = new ArriveBus();
            SystemUserEntity result = bus.GetUserNameByClerkNo(new SystemUserEntity { ClerkNo = id, NewLandBelongSystem = UserInfor.NewLandBelongSystem });
            String json = string.IsNullOrEmpty(result.UserName) ? "" : result.UserName;
            Response.Write(json);
        }
        /// <summary>
        /// 查询好来运运单
        /// </summary>
        public void QueryHlyAwbInfo()
        {
            HlyExchangeEntity queryEntity = new HlyExchangeEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); } else { queryEntity.StartDate = DateTime.Now.AddDays(-5); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); } else { queryEntity.EndDate = DateTime.Now; }
            queryEntity.HlyFiveNo = Convert.ToString(Request["HlyFiveNo"]);
            queryEntity.SaveStatus = "0";
            ArriveBus bus = new ArriveBus();
            List<HlyExchangeEntity> list = bus.QueryHlyAwbInfo(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        /// <summary>
        /// 是否存在客户编号
        /// </summary>
        public void IsExistClientNum()
        {
            int id = Convert.ToInt32(Request["cnum"]);
            ArriveBus bus = new ArriveBus();
            ClientEntity result = bus.GetClientByID(id);
            String json = result.ClientID.Equals(0) ? "0" : "1";
            Response.Write(json);
        }
        /// <summary>
        /// 根据客户信息查询该客户所录入的收货地址
        /// </summary>
        public void QueryClientAcceptAddress()
        {
            string clientID = Request["id"];
            if (string.IsNullOrEmpty(clientID)) { return; }
            ClientAcceptAddressEntity queryEntity = new ClientAcceptAddressEntity();
            queryEntity.ClientID = Convert.ToInt32(clientID);
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            List<ClientAcceptAddressEntity> list = bus.QueryClientAcceptAddress(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>   
        /// 计算文本长度，区分中英文字符，中文算两个长度，英文算一个长度
        /// </summary>
        /// <param name="Text">需计算长度的字符串</param>
        /// <returns>int</returns>
        public static int Text_Length(string Text)
        {
            int len = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                byte[] byte_len = Encoding.Default.GetBytes(Text.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 2;  //如果长度大于1，是中文，占两个字节，+2
                else
                    len += 1;  //如果长度等于1，是英文，占一个字节，+1
            }
            return len;
        }
        /// <summary>
        /// 检验是否是合法的日期格式
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool IsDateLegal(string strDate)
        {
            DateTime dt;
            return (DateTime.TryParse(strDate, out dt));
        }
        /// <summary>
        /// 保存运单信息数据
        /// </summary>
        public void SaveAwb()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            if (GridRows.Count <= 0)
            {
                msg.Message = "没有货物品名数据";
                msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
                return;
            }
            AwbEntity ent = new AwbEntity();
            List<AwbGoodsEntity> entDest = new List<AwbGoodsEntity>();
            ArriveBus bus = new ArriveBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "拖运单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";

            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Request["CheckOutType"])))
                {
                    msg.Message = "结款方式不能为空";
                    msg.Result = false;
                }
                if (string.IsNullOrEmpty(Convert.ToString(Request["DeliveryType"])))
                {
                    msg.Message = "送货方式不能为空";
                    msg.Result = false;
                }
                if (string.IsNullOrEmpty(Convert.ToString(Request["TimeLimit"])))
                {
                    msg.Message = "运输时效不能为空";
                    msg.Result = false;
                }
                if (Text_Length(Convert.ToString(Request["Transit"])) > 20)
                {
                    msg.Message = "中转站长度不能超过20个字符";
                    msg.Result = false;
                }
                if (!IsDateLegal(Convert.ToString(Request["HandleTime"])))
                {
                    msg.Message = "受理日期格式不正确";
                    msg.Result = false;
                }
                if (msg.Result)
                {
                    #region 赋值
                    ent.AwbNo = Convert.ToString(Request["AwbNo"]);
                    ent.HAwbNo = Convert.ToString(Request["HAwbNo"]);
                    ent.Dep = Convert.ToString(Request["Dep"]);
                    ent.Dest = Convert.ToString(Request["Dest"]);
                    ent.Transit = Convert.ToString(Request["Transit"]);
                    ent.Piece = string.IsNullOrEmpty(Convert.ToString(Request["Piece"])) ? 0 : Convert.ToInt32(Request["Piece"]);
                    ent.Weight = string.IsNullOrEmpty(Convert.ToString(Request["Weight"])) ? 0 : Convert.ToDecimal(Request["Weight"]);
                    ent.Volume = string.IsNullOrEmpty(Convert.ToString(Request["Volume"])) ? 0 : Convert.ToDecimal(Request["Volume"]);
                    ent.Attach = string.IsNullOrEmpty(Convert.ToString(Request["Attach"])) ? 0 : Convert.ToInt32(Request["Attach"]);
                    ent.InsuranceFee = string.IsNullOrEmpty(Convert.ToString(Request["InsuranceFee"])) ? 0 : Convert.ToDecimal(Request["InsuranceFee"]);
                    ent.TransitFee = string.IsNullOrEmpty(Convert.ToString(Request["TransitFee"])) ? 0 : Convert.ToDecimal(Request["TransitFee"]);
                    ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                    ent.DeliverFee = string.IsNullOrEmpty(Convert.ToString(Request["DeliverFee"])) ? 0 : Convert.ToDecimal(Request["DeliverFee"]);
                    ent.OtherFee = string.IsNullOrEmpty(Convert.ToString(Request["OtherFee"])) ? 0 : Convert.ToDecimal(Request["OtherFee"]);
                    ent.TotalCharge = string.IsNullOrEmpty(Convert.ToString(Request["TotalCharge"])) ? 0 : Convert.ToDecimal(Request["TotalCharge"]);
                    ent.Rebate = string.IsNullOrEmpty(Convert.ToString(Request["Rebate"])) ? 0 : Convert.ToDecimal(Request["Rebate"]);
                    ent.NowPay = string.IsNullOrEmpty(Convert.ToString(Request["NowPay"])) ? 0 : Convert.ToDecimal(Request["NowPay"]);
                    ent.PickPay = string.IsNullOrEmpty(Convert.ToString(Request["PickPay"])) ? 0 : Convert.ToDecimal(Request["PickPay"]);
                    ent.CollectMoney = string.IsNullOrEmpty(Convert.ToString(Request["CollectMoney"])) ? 0 : Convert.ToDecimal(Request["CollectMoney"]);
                    ent.CheckOutType = Convert.ToString(Request["CheckOutType"]);
                    ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(Request["ReturnAwb"])) ? 0 : Convert.ToInt32(Request["ReturnAwb"]);
                    ent.TrafficType = Convert.ToString(Request["TrafficType"]);
                    ent.TimeLimit = Convert.ToInt32(Request["TimeLimit"]);
                    ent.DeliveryType = Convert.ToString(Request["DeliveryType"]);
                    ent.SteveDore = Convert.ToString(Request["SteveDore"]);
                    ent.ShipperName = Convert.ToString(Request["ShipperName"]);
                    ent.ShipperUnit = Convert.ToString(Request["AShipperUnit"]);
                    ent.ShipperTelephone = Convert.ToString(Request["ShipperTelephone"]);
                    ent.ShipperCellphone = Convert.ToString(Request["ShipperCellphone"]);
                    ent.ShipperAddress = Convert.ToString(Request["ShipperAddress"]);
                    ent.AcceptUnit = Convert.ToString(Request["AAcceptUnit"]);
                    ent.AcceptAddress = Convert.ToString(Request["AcceptAddress"]);
                    ent.AcceptPeople = Convert.ToString(Request["AAcceptPeople"]);
                    ent.AcceptTelephone = Convert.ToString(Request["AcceptTelephone"]);
                    ent.AcceptCellphone = Convert.ToString(Request["AcceptCellphone"]);
                    ent.HandleTime = Convert.ToDateTime(Request["HandleTime"]);
                    ent.ClerkNo = Convert.ToString(Request["ClerkNo"]);
                    ent.ClerkName = Convert.ToString(Request["ClerkName"]);
                    ent.DelFlag = "0";
                    ent.HLY = Convert.ToString(Request["HLY"]);
                    if (Request["Dep"].Equals(Request["Dest"]))
                    {
                        ent.DelFlag = "3";
                        ent.AwbStatus = "3";
                    }
                    ent.CreateAwb = Convert.ToString(Request["CreateAwb"]);
                    ent.CreateDate = Convert.ToDateTime(Request["CreateDate"]);
                    ent.OP_ID = UserInfor.LoginName.Trim();
                    ent.Remark = Convert.ToString(Request["Remark"]);
                    ent.ClientNum = Convert.ToString(Request["ClientNum"]);
                    ent.BelongSystem = UserInfor.NewLandBelongSystem;
                    if (ent.CheckOutType.Equals("0"))//现付，直接过一审和二审
                    {
                        ent.FinanceFirstCheck = "1";
                        ent.FirstCheckName = UserInfor.UserName.Trim();
                        ent.FirstCheckDate = DateTime.Now;
                        ent.FinanceSecondCheck = "1";
                        ent.SecondCheckName = UserInfor.UserName.Trim();
                        ent.SecondCheckDate = DateTime.Now;
                        ent.ShouFlag = "1";
                        ent.ShouCheckName = UserInfor.UserName.Trim();
                        ent.SecondCheckName = UserInfor.UserName.Trim();
                        ent.SecondCheckDate = DateTime.Now;
                    }

                    #endregion
                    foreach (Hashtable row in GridRows)
                    {
                        entDest.Add(new AwbGoodsEntity
                        {
                            AwbNo = ent.AwbNo,
                            Goods = Convert.ToString(row["Goods"]),
                            Package = Convert.ToString(row["Package"]),
                            Piece = string.IsNullOrEmpty(Convert.ToString(row["Piece"])) ? 0 : Convert.ToInt32(row["Piece"]),
                            PiecePrice = string.IsNullOrEmpty(Convert.ToString(row["PiecePrice"])) ? 0 : Convert.ToDecimal(row["PiecePrice"]),
                            Weight = string.IsNullOrEmpty(Convert.ToString(row["Weight"])) ? 0 : Convert.ToDecimal(row["Weight"]),
                            WeightPrice = string.IsNullOrEmpty(Convert.ToString(row["WeightPrice"])) ? 0 : Convert.ToDecimal(row["WeightPrice"]),
                            Volume = string.IsNullOrEmpty(Convert.ToString(row["Volume"])) ? 0 : Convert.ToDecimal(row["Volume"]),
                            VolumePrice = string.IsNullOrEmpty(Convert.ToString(row["VolumePrice"])) ? 0 : Convert.ToDecimal(row["VolumePrice"]),
                            DeclareValue = Convert.ToString(row["DeclareValue"]),
                            BelongSystem = UserInfor.NewLandBelongSystem,
                            OP_ID = UserInfor.LoginName.Trim()
                        });
                    }
                    ent.AwbGoods = entDest;
                    if (!bus.IsRangeAwbNo(ent))
                    {
                        msg.Message = ent.AwbNo.ToUpper().Trim() + " 不在允许制单范围，请更改运单号再保存";
                        msg.Result = false;
                    }
                    else
                    {
                        if (bus.IsExistAwb(ent))
                        {
                            msg.Message = "已经存在 " + ent.AwbNo.ToUpper().Trim() + " 运单数据，请更改运单号再保存";
                            msg.Result = false;
                        }
                    }
                    if (msg.Result)
                    {
                        bus.AddAwbInfo(ent, log);
                    }
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            try
            {
                if (ent.HLY.Equals("1"))//说明是好来运单
                {
                    HlyCommon hly = new HlyCommon();
                    if (!string.IsNullOrEmpty(ent.HAwbNo))
                    {
                        string[] ha = ent.HAwbNo.Split(',');
                        if (ha.Length > 0)
                        {
                            for (int i = 0; i < ha.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(ha[i]))
                                {
                                    bus.UpdateHlyAwbStatus(new HlyExchangeEntity { SaveStatus = "1", HlyFiveNo = ha[i] }, log);
                                }
                            }
                        }
                    }
                    ////如果不是好来运单就不需要发送跟踪状态接口
                    //hly.SaveNewayAwb(new HlyEntity { Hawbno = ent.HAwbNo, Createuser = UserInfor.UserName, Other = ent.Remark, Toname = ent.AcceptPeople, ToPhone = ent.AcceptCellphone, ToAddress = ent.AcceptAddress, Shipper = ent.ShipperName, Pcs = ent.Piece, Weight = ent.Weight, SplitCB = ",", Xawbno = ent.AwbNo });
                }
            }
            catch (ApplicationException ex)
            {
                msg.Result = true;
            }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        #endregion
        #region 托运合同维护

        /// <summary>
        /// 运单数据查询
        /// </summary>
        public void QueryAwb()
        {
            AwbEntity queryEntity = new AwbEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["Piece"]))) { queryEntity.Piece = Convert.ToInt32(Request["Piece"]); }
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.ShipperUnit = Convert.ToString(Request["ShipperUnit"]);
            queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
            queryEntity.Dep = string.IsNullOrEmpty(Convert.ToString(Request["Dep"])) ? UserInfor.DepCity : Convert.ToString(Request["Dep"]);
            queryEntity.Dest = Convert.ToString(Request["Dest"]);
            queryEntity.HAwbNo = Convert.ToString(Request["HAwbNo"]);
            queryEntity.CheckOutType = Convert.ToString(Request["CheckOutType"]);
            queryEntity.DelFlag = Convert.ToString(Request["dFlag"]);
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryAwb(pageIndex, pageSize, queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        /// <summary>
        /// 修改运单信息
        /// </summary>
        public void UpdateAwb()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            AwbEntity ent = new AwbEntity();
            List<AwbGoodsEntity> entDest = new List<AwbGoodsEntity>();
            ArriveBus bus = new ArriveBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            msg.Message = "";
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "拖运单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            int oPiece = 0; decimal oWeight = 0M, oVolume = 0M;
            try
            {
                oPiece = Convert.ToInt32(Request["OldPiece"]);
                oWeight = Convert.ToDecimal(Request["OldWeight"]);
                oVolume = Convert.ToDecimal(Request["OldVolume"]);
                ent.AwbID = Convert.ToInt64(Request["AwbID"]);
                ent.AwbNo = Convert.ToString(Request["AwbNo"]);
                ent.HAwbNo = Convert.ToString(Request["HAwbNo"]);
                ent.Dep = Convert.ToString(Request["Dep"]);
                ent.Dest = Convert.ToString(Request["Dest"]);
                ent.Transit = Convert.ToString(Request["Transit"]);
                ent.Piece = string.IsNullOrEmpty(Convert.ToString(Request["Piece"])) ? 0 : Convert.ToInt32(Request["Piece"]);
                ent.Weight = string.IsNullOrEmpty(Convert.ToString(Request["Weight"])) ? 0 : Convert.ToDecimal(Request["Weight"]);

                ent.Volume = string.IsNullOrEmpty(Convert.ToString(Request["Volume"])) ? 0 : Convert.ToDecimal(Request["Volume"]);
                ent.AwbPiece = string.IsNullOrEmpty(Convert.ToString(Request["AwbPiece"])) ? 0 : Convert.ToInt32(Request["AwbPiece"]);
                ent.AwbWeight = string.IsNullOrEmpty(Convert.ToString(Request["AwbWeight"])) ? 0 : Convert.ToDecimal(Request["AwbWeight"]);
                ent.AwbVolume = string.IsNullOrEmpty(Convert.ToString(Request["AwbVolume"])) ? 0 : Convert.ToDecimal(Request["AwbVolume"]);
                ent.Attach = string.IsNullOrEmpty(Convert.ToString(Request["Attach"])) ? 0 : Convert.ToInt32(Request["Attach"]);
                ent.InsuranceFee = string.IsNullOrEmpty(Convert.ToString(Request["InsuranceFee"])) ? 0 : Convert.ToDecimal(Request["InsuranceFee"]);
                ent.TransitFee = string.IsNullOrEmpty(Convert.ToString(Request["TransitFee"])) ? 0 : Convert.ToDecimal(Request["TransitFee"]);
                ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                ent.DeliverFee = string.IsNullOrEmpty(Convert.ToString(Request["DeliverFee"])) ? 0 : Convert.ToDecimal(Request["DeliverFee"]);
                ent.OtherFee = string.IsNullOrEmpty(Convert.ToString(Request["OtherFee"])) ? 0 : Convert.ToDecimal(Request["OtherFee"]);
                ent.TotalCharge = string.IsNullOrEmpty(Convert.ToString(Request["TotalCharge"])) ? 0 : Convert.ToDecimal(Request["TotalCharge"]);
                ent.Rebate = string.IsNullOrEmpty(Convert.ToString(Request["Rebate"])) ? 0 : Convert.ToDecimal(Request["Rebate"]);
                ent.NowPay = string.IsNullOrEmpty(Convert.ToString(Request["NowPay"])) ? 0 : Convert.ToDecimal(Request["NowPay"]);
                ent.PickPay = string.IsNullOrEmpty(Convert.ToString(Request["PickPay"])) ? 0 : Convert.ToDecimal(Request["PickPay"]);
                ent.CollectMoney = string.IsNullOrEmpty(Convert.ToString(Request["CollectMoney"])) ? 0 : Convert.ToDecimal(Request["CollectMoney"]);

                ent.CheckOutType = Convert.ToString(Request["CheckOutType"]);
                ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(Request["ReturnAwb"])) ? 0 : Convert.ToInt32(Request["ReturnAwb"]);
                ent.TimeLimit = Convert.ToInt32(Request["TimeLimit"]);
                ent.TrafficType = Convert.ToString(Request["TrafficType"]);
                ent.DeliveryType = Convert.ToString(Request["DeliveryType"]);
                ent.SteveDore = Convert.ToString(Request["SteveDore"]);

                ent.ShipperName = Convert.ToString(Request["ShipperName"]);
                ent.ShipperUnit = Convert.ToString(Request["AShipperUnit"]);
                ent.ShipperTelephone = Convert.ToString(Request["ShipperTelephone"]);
                ent.ShipperCellphone = Convert.ToString(Request["ShipperCellphone"]);
                ent.ShipperAddress = Convert.ToString(Request["ShipperAddress"]);

                ent.AcceptUnit = Convert.ToString(Request["AAcceptUnit"]);
                ent.AcceptAddress = Convert.ToString(Request["AcceptAddress"]);
                ent.AcceptPeople = Convert.ToString(Request["AAcceptPeople"]);
                ent.AcceptTelephone = Convert.ToString(Request["AcceptTelephone"]);
                ent.AcceptCellphone = Convert.ToString(Request["AcceptCellphone"]);
                ent.ClerkNo = Convert.ToString(Request["ClerkNo"]);
                ent.ClerkName = Convert.ToString(Request["ClerkName"]);
                ent.HandleTime = Convert.ToDateTime(Request["HandleTime"]);
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                ent.OP_ID = UserInfor.LoginName.Trim();
                ent.Remark = Convert.ToString(Request["Remark"]);
                ent.FinanceFirstCheck = Convert.ToString(Request["FinanceFirstCheck"]);
                ent.FirstCheckName = Convert.ToString(Request["FirstCheckName"]);
                ent.FirstCheckDate = Convert.ToDateTime(Request["FirstCheckDate"]);
                ent.FinanceSecondCheck = Convert.ToString(Request["FinanceSecondCheck"]);
                ent.SecondCheckName = Convert.ToString(Request["SecondCheckName"]);
                ent.SecondCheckDate = Convert.ToDateTime(Request["SecondCheckDate"]);
                ent.ClientNum = Convert.ToString(Request["ClientNum"]);
                ent.BelongSystem = UserInfor.NewLandBelongSystem;
                ent.HLY = Convert.ToString(Request["HLY"]);
                if (ent.CheckOutType.Equals("0") && Convert.ToString(Request["OldCheckOutType"]).Equals(ent.CheckOutType))//现付，直接过一审和二审
                {
                    ent.FinanceFirstCheck = "1";
                    ent.FirstCheckName = UserInfor.UserName.Trim();
                    ent.FirstCheckDate = DateTime.Now;
                    ent.FinanceSecondCheck = "1";
                    ent.ShouFlag = "1";
                    ent.ShouCheckName = UserInfor.UserName.Trim();
                    ent.SecondCheckName = UserInfor.UserName.Trim();
                    ent.SecondCheckDate = DateTime.Now;
                }
                foreach (Hashtable row in GridRows)
                {
                    entDest.Add(new AwbGoodsEntity
                    {
                        AwbNo = ent.AwbNo,
                        GoodsID = Convert.ToInt32(row["GoodsID"]),
                        Goods = Convert.ToString(row["Goods"]),
                        Package = Convert.ToString(row["Package"]),
                        Piece = string.IsNullOrEmpty(Convert.ToString(row["Piece"])) ? 0 : Convert.ToInt32(row["Piece"]),
                        PiecePrice = string.IsNullOrEmpty(Convert.ToString(row["PiecePrice"])) ? 0 : Convert.ToDecimal(row["PiecePrice"]),
                        Weight = string.IsNullOrEmpty(Convert.ToString(row["Weight"])) ? 0 : Convert.ToDecimal(row["Weight"]),
                        WeightPrice = string.IsNullOrEmpty(Convert.ToString(row["WeightPrice"])) ? 0 : Convert.ToDecimal(row["WeightPrice"]),
                        Volume = string.IsNullOrEmpty(Convert.ToString(row["Volume"])) ? 0 : Convert.ToDecimal(row["Volume"]),
                        VolumePrice = string.IsNullOrEmpty(Convert.ToString(row["VolumePrice"])) ? 0 : Convert.ToDecimal(row["VolumePrice"]),
                        DeclareValue = Convert.ToString(row["DeclareValue"]),
                        BelongSystem = UserInfor.NewLandBelongSystem,
                        OP_ID = UserInfor.LoginName.Trim()
                    });
                }
                ent.AwbGoods = entDest;
                if (!bus.IsExistAwb(ent))
                {
                    msg.Message = ent.AwbNo.ToUpper().Trim() + " 运单不存在，请重新刷新再修改";
                    msg.Result = false;
                }
                List<AwbEntity> elist = bus.QueryAwb(new AwbEntity { AwbNo = ent.AwbNo, BelongSystem = UserInfor.NewLandBelongSystem });
                bool su = false;
                if (elist.Count > 1)//表示分批了
                {
                    foreach (var it in elist)
                    {
                        if (!it.DelFlag.Equals("0"))
                        {
                            su = true;
                            break;
                        }
                    }
                    if (!su)
                    {
                        if (ent.Piece != oPiece)
                        {
                            msg.Message = "分批运单必须先合并后才能修改件数信息";
                            msg.Result = false;
                        }
                    }
                    else//如果有一批运单的状态不是在站，就不允许修改运单件数
                    {
                        ent.Piece = oPiece; ent.Weight = oWeight; ent.Volume = oVolume;
                    }
                }
                if (msg.Result)
                {
                    bus.UpdateAwbInfo(ent, log);
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 作废运单
        /// </summary>
        public void DelAwb()
        {
            String idStr = Request["data"];
            int ty = Convert.ToInt32(Request["ty"]);
            if (String.IsNullOrEmpty(idStr)) return;
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            List<AwbEntity> list = new List<AwbEntity>();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "托运合同维护";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            ArriveBus bus = new ArriveBus();
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new AwbEntity
                    {
                        AwbID = Convert.ToInt64(row["AwbID"]),
                        BelongSystem = UserInfor.NewLandBelongSystem,
                        AwbNo = Convert.ToString(row["AwbNo"])
                    });
                }
                bus.DelAwb(list, ty, log);
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 粤东线运单 上传好来运系统
        /// </summary>
        public void uploadHLY()
        {
            String idStr = Request["data"];
            int ty = Convert.ToInt32(Request["ty"]);
            if (String.IsNullOrEmpty(idStr)) return;
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            List<HlyAwbEntity> list = new List<HlyAwbEntity>();
            ArriveBus bus = new ArriveBus();
            try
            {
                foreach (Hashtable row in rows)
                {
                    string paym = string.Empty;
                    string otherPay = string.Empty;
                    string TrafficType = string.Empty;
                    switch (Convert.ToString(row["CheckOutType"]))
                    {
                        case "0":
                            paym = "现付";
                            break;
                        case "1":
                            paym = "回单";
                            break;
                        case "2":
                            paym = "月结";
                            break;
                        case "3":
                            paym = "到付";
                            break;
                        case "4":
                            paym = "代收款";
                            break;
                        default:
                            paym = "现付";
                            break;
                    }
                    otherPay = Convert.ToString(Convert.ToDecimal(row["InsuranceFee"]) + Convert.ToDecimal(row["TransitFee"]) + Convert.ToDecimal(row["DeliverFee"]) + Convert.ToDecimal(row["OtherFee"]));

                    switch (Convert.ToString(row["TrafficType"]))
                    {
                        case "0":
                            TrafficType = "普汽";
                            break;
                        case "1":
                            TrafficType = "包车";
                            break;
                        case "2":
                            TrafficType = "加急";
                            break;
                        case "3":
                            TrafficType = "铁路";
                            break;
                        default:
                            TrafficType = "普汽";
                            break;
                    }
                    list.Add(new HlyAwbEntity
                    {
                        awbno = Convert.ToString(row["AwbNo"]),
                        frmname = Convert.ToString(row["ShipperName"]),
                        frmtel = Convert.ToString(row["ShipperCellphone"]),
                        frmaddress = Convert.ToString(row["ShipperAddress"]),
                        toname = Convert.ToString(row["AcceptPeople"]),
                        toaddress = Convert.ToString(row["AcceptAddress"]),
                        totel = Convert.ToString(row["AcceptCellphone"]),
                        fdep = Convert.ToString(row["Dep"]),
                        fdest = Convert.ToString(row["Dest"]).Equals("其它") ? Convert.ToString(row["Transit"]) : Convert.ToString(row["Dest"]),
                        payment = paym,
                        pcs = Convert.ToString(row["Piece"]),
                        weight = Convert.ToString(row["Weight"]),
                        volume = Convert.ToString(row["Volume"]),
                        charge = Convert.ToString(row["TransportFee"]),
                        otherCharge = otherPay,
                        grossfee = Convert.ToString(row["TotalCharge"]),
                        passGet = TrafficType,
                        ruser = Convert.ToString(row["CreateAwb"]),
                        other = Convert.ToString(row["Remark"])
                    });
                }
                string postData = "{\"token\":\"KJoGyh!$#kpxlc\",\"awbnodetailed\":" + JSON.Encode(list) + "}";

                string result = wxHttpUtility.SendHttpRequest(@"http://api.hlyex.net:9001/xlc.ashx?action=inputawbno", postData);

                LogBus logBus = new LogBus();
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "营运管理";
                log.NvgPage = "粤东线数据";
                log.UserID = UserInfor.LoginName.Trim();
                log.Operate = "U";
                log.Memo = "上传成功" + result;
                logBus.InsertLog(log);
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 查询所有的运单数据
        /// </summary>
        public void QueryAllAwb()
        {
            AwbEntity queryEntity = new AwbEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.AwbNo = arr[i].Trim();
                        }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.ShipperUnit = arr[i].Trim();
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.StartDate = Convert.ToDateTime(arr[i].Trim());
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.EndDate = Convert.ToDateTime(arr[i].Trim());
                        }
                        break;
                    case 4:
                        queryEntity.Dest = string.IsNullOrEmpty(arr[i]) ? UserInfor.DepCity : arr[i].Trim();
                        break;
                    case 5:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.Piece = Convert.ToInt32(arr[i].Trim());
                        }
                        break;
                    case 6:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.DelFlag = Convert.ToString(arr[i]);
                        }
                        break;
                    case 7:
                        queryEntity.Dep = string.IsNullOrEmpty(arr[i]) ? UserInfor.DepCity : arr[i].Trim();
                        break;
                    case 8:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.AcceptPeople = arr[i].Trim();
                        }
                        break;
                    case 9:
                        if (!arr[i].Equals("-1"))
                        {
                            queryEntity.CheckOutType = Convert.ToString(arr[i]);
                        }
                        break;
                    default:
                        break;
                }
            }
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            string err = "OK";
            List<AwbEntity> awbList = bus.QueryAwb(queryEntity);
            if (awbList.Count > 0)
            {
                PageList = awbList;
            }
            else
            {
                err = "没有数据可以进行导出，请重新查询！";
            }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        public List<AwbEntity> PageList
        {
            get
            {
                if (Session["PageList"] == null)
                {
                    Session["PageList"] = new List<AwbEntity>();
                }
                return (List<AwbEntity>)(Session["PageList"]);
            }
            set
            {
                Session["PageList"] = value;
            }
        }
        #endregion
        #region 长运配载操作

        /// <summary>
        /// 查询所有运单状态为正常的数据进行配载
        /// </summary>
        public void QueryAwbByDest()
        {
            AwbEntity queryEntity = new AwbEntity();
            queryEntity.Dep = string.IsNullOrEmpty(Convert.ToString(Request["Dep"])) ? UserInfor.DepCity : Convert.ToString(Request["Dep"]);
            queryEntity.Dest = Convert.ToString(Request["Dest"]);
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.DelFlag = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 1000 : Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryAwbToManifest(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据单位ID查询该单位下的所有部门数据
        /// </summary>
        public void GetDeptByUnitID()
        {
            //查询条件
            string id = Convert.ToString(Request["id"]);
            ArriveBus bus = new ArriveBus();
            List<SystemDepartEntity> list = bus.GetDeptByUnitID(id, UserInfor.NewLandBelongSystem);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 保存配载信息，并设置运单的状态为配载，更新司机合同号
        /// </summary>
        public void SaveManifest()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            DepManifestEntity ent = new DepManifestEntity();
            List<AwbEntity> entDest = new List<AwbEntity>();
            ArriveBus bus = new ArriveBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "长运配载操作";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            if (string.IsNullOrEmpty(Convert.ToString(Request["PTruckNum"])))
            {
                msg.Message = "车牌号不能为空";
                msg.Result = false;
            }
            if (string.IsNullOrEmpty(Convert.ToString(Request["Dep"])))
            {
                msg.Message = "出发站不能为空";
                msg.Result = false;
            }
            if (string.IsNullOrEmpty(Convert.ToString(Request["Dest"])))
            {
                msg.Message = "到达站不能为空";
                msg.Result = false;
            }
            try
            {
                if (msg.Result)
                {
                    ent.TruckNum = Convert.ToString(Request["PTruckNum"]);
                    ent.Length = Convert.ToDecimal(Request["Length"]);
                    ent.Model = Convert.ToString(Request["Model"]);
                    ent.Dep = Convert.ToString(Request["Dep"]);
                    ent.Dest = Convert.ToString(Request["Dest"]);
                    ent.Transit = Convert.ToString(Request["Transit"]);
                    ent.Driver = Convert.ToString(Request["Driver"]);
                    ent.DriverCellPhone = Convert.ToString(Request["DriverCellPhone"]);
                    ent.Weight = string.IsNullOrEmpty(Convert.ToString(Request["Weight"])) ? 0 : Convert.ToDecimal(Request["Weight"]);
                    ent.Volume = string.IsNullOrEmpty(Convert.ToString(Request["Volume"])) ? 0 : Convert.ToDecimal(Request["Volume"]);
                    ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                    ent.PrepayFee = string.IsNullOrEmpty(Convert.ToString(Request["PrepayFee"])) ? 0 : Convert.ToDecimal(Request["PrepayFee"]);
                    ent.ArriveFee = string.IsNullOrEmpty(Convert.ToString(Request["ArriveFee"])) ? 0 : Convert.ToDecimal(Request["ArriveFee"]);
                    ent.ContractNum = GetCityCodeShortName(ent.Dep.Trim(), ent.Dest.Trim(), Convert.ToDateTime(Request["StartTime"]), "0", UserInfor.NewLandBelongSystem);//司机合同号
                    ent.DriverIDNum = Convert.ToString(Request["DriverIDNum"]);
                    ent.DriverIDAddress = Convert.ToString(Request["DriverIDAddress"]);
                    ent.DestPeople = Convert.ToString(Request["DestPeople"]);
                    ent.DestCellphone = Convert.ToString(Request["DestCellphone"]);
                    ent.UnLoadAddress = Convert.ToString(Request["UnLoadAddress"]);
                    ent.DriverCellPhone = Convert.ToString(Request["DriverCellPhone"]);
                    ent.StartTime = Convert.ToDateTime(Request["StartTime"]);
                    ent.PassTime = string.IsNullOrEmpty(Convert.ToString(Request["PassTime"])) ? 0 : Convert.ToDecimal(Request["PassTime"]);
                    ent.PreArriveTime = ent.StartTime.AddMinutes(Convert.ToDouble(ent.PassTime) * 60);
                    ent.OP_ID = UserInfor.LoginName.Trim();
                    ent.Remark = Convert.ToString(Request["Remark"]);
                    ent.DelFlag = "0";
                    ent.Loader = Convert.ToString(Request["Loader"]).Trim();
                    ent.Manifester = string.IsNullOrEmpty(Convert.ToString(Request["OPNAME"]).Trim()) ? UserInfor.UserName.Trim() : Convert.ToString(Request["OPNAME"]).Trim();
                    ent.Dispatcher = Convert.ToString(Request["Dispatcher"]).Trim();
                    ent.CardName = Convert.ToString(Request["PCardName"]).Trim();
                    ent.CardBank = Convert.ToString(Request["CardBank"]).Trim();
                    ent.CardNum = Convert.ToString(Request["CardNum"]).Trim();
                    ent.BelongSystem = UserInfor.NewLandBelongSystem;
                    foreach (Hashtable row in GridRows)
                    {
                        entDest.Add(new AwbEntity
                        {
                            AwbID = Convert.ToInt64(row["AwbID"]),
                            AwbNo = Convert.ToString(row["AwbNo"]),
                            TransKind = "0",
                            BelongSystem = UserInfor.NewLandBelongSystem,
                            ContractNum = ent.ContractNum.Trim()
                        });
                    }
                    ent.AwbInfo = entDest;
                    int result = bus.SaveManifest(ent, log);
                    msg.Message = ent.ContractNum;
                    if (result.Equals(1))
                    {
                        msg.Message = "运单已经做过配载，请勿重复保存";
                        msg.Result = false;
                    }
                    if (result.Equals(2))
                    {
                        msg.Message = "该车辆已经配载，请更改后保存";
                        msg.Result = false;
                    }
                    if (result.Equals(3))
                    {
                        msg.Message = "该车辆已被拉入黑名单，请更改后保存";
                        msg.Result = false;
                    }
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            if (msg.Result)
            {
                HlyCommon hly = new HlyCommon();
                foreach (Hashtable row in GridRows)
                {
                    if (Convert.ToString(row["HLY"]).Equals("0")) { continue; }//如果不是好来运单就不需要发送跟踪状态接口
                    //hly.SaveAwbStatus(new HlyEntity { Hawbno = Convert.ToString(row["HAwbNo"]), City = UserInfor.DepCity, Cuser = UserInfor.UserName, Cdatetime = DateTime.Now, fdest = ent.Dest, Other = Convert.ToString(row["Remark"]), SplitCB = ",", Xawbno = Convert.ToString(row["AwbNo"]), State = "2" });
                }
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 合并运单数据
        /// </summary>
        public void merge()
        {
            String json = Request["submitData"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            AwbEntity entity = new AwbEntity();
            List<AwbEntity> listEntity = new List<AwbEntity>();
            ArriveBus bus = new ArriveBus();
            string awbno = string.Empty;
            int p = 0;
            decimal w = 0, v = 0;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "长运配载操作";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            try
            {
                foreach (Hashtable row in rows)
                {
                    #region 修改数据
                    if (!string.IsNullOrEmpty(awbno) && !awbno.Equals(Convert.ToString(row["AwbNo"]).Trim()))
                    {
                        msg.Message = "必须是相同的运单号";
                        msg.Result = false;
                        break;
                    }
                    p += Convert.ToInt32(row["AwbPiece"]);
                    w += Convert.ToDecimal(row["AwbWeight"]);
                    v += Convert.ToDecimal(row["AwbVolume"]);

                    awbno = Convert.ToString(row["AwbNo"]).Trim();

                    listEntity.Add(new AwbEntity { AwbID = Convert.ToInt32(row["AwbID"]), AwbNo = awbno, AwbPiece = Convert.ToInt32(row["AwbPiece"]), AwbWeight = Convert.ToDecimal(row["AwbWeight"]), AwbVolume = Convert.ToDecimal(row["AwbVolume"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    #endregion
                }
                if (msg.Result)
                {
                    entity.AwbPiece = p;
                    entity.AwbWeight = w;
                    entity.AwbVolume = v;
                    entity.AwbNo = awbno;
                    entity.BelongSystem = UserInfor.NewLandBelongSystem;
                    bus.merge(entity, listEntity, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 分批
        /// </summary>
        public void Tear()
        {
            ArriveBus bus = new ArriveBus();
            List<AwbEntity> queryEntity = new List<AwbEntity>();
            int tp = 0, p = 0;
            decimal w = 0, v = 0;
            long awbid = 0; string awbno = string.Empty;
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "长运配载操作";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            foreach (Hashtable row in rows)
            {
                awbid = Convert.ToInt64(row["AwbID"]);
                p = Convert.ToInt32(row["AwbPiece"]);
                w = Convert.ToDecimal(row["AwbWeight"]);
                v = Convert.ToDecimal(row["AwbVolume"]);
                awbno = Convert.ToString(row["AwbNo"]).Trim();
            }
            tp = Convert.ToInt32(Request["TearPiece"]);
            if (tp >= p)
            {
                msg.Message = "分批件数不能大于总件数";
                msg.Result = false;
            }
            try
            {
                int op = p - tp; //剩余件数
                decimal tw = Convert.ToDecimal(w * tp / p); //分批重量
                decimal tv = Convert.ToDecimal(v * tp / p); //分批体积
                AwbEntity ae = new AwbEntity { AwbID = awbid, AwbNo = awbno, AwbPiece = tp, AwbWeight = tw, AwbVolume = tv, Piece = p, Weight = w, Volume = v, BelongSystem = UserInfor.NewLandBelongSystem };
                queryEntity.Add(ae);
                AwbEntity oe = new AwbEntity { AwbID = awbid, AwbNo = awbno, AwbPiece = op, AwbWeight = w - tw, AwbVolume = v - tv, Piece = p, Weight = w, Volume = v, BelongSystem = UserInfor.NewLandBelongSystem };
                queryEntity.Add(oe);
                if (msg.Result)
                {
                    bus.Tear(queryEntity, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 根据车牌号获取司机的银行卡信息
        /// </summary>
        public void GetDriverCardByTruckNum()
        {
            //查询条件
            string trucknum = Convert.ToString(Request["id"]).Trim();
            ArriveBus bus = new ArriveBus();
            List<DriverCardEntity> list = bus.GetDriverCardByTruckNum(trucknum);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        #endregion
        #region 车辆合同管理

        /// <summary>
        /// 查看所有已经生成司机合同的运单信息
        /// </summary>
        public void QueryDepManifest()
        {
            DepManifestEntity queryEntity = new DepManifestEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.TruckNum = Convert.ToString(Request["TruckNum"]);
            queryEntity.ContractNum = Convert.ToString(Request["ContractNum"]);
            queryEntity.Dest = Convert.ToString(Request["Dest"]);
            queryEntity.Dep = Convert.ToString(Request["Dep"]);
            if (Request["eFlag"] != "-1") { queryEntity.CancelFlag = Convert.ToString(Request["eFlag"]); }
            if (Request["dFlag"] != "-1") { queryEntity.DelFlag = Convert.ToString(Request["dFlag"]); }
            queryEntity.TransKind = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            queryEntity.ProfitPCT = 1 - Common.GetProfitPCT();// 0.3M;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryDepManifest(pageIndex, pageSize, queryEntity);
            //List<DepManifestEntity> eList = bus.QueryDepManifestForExport(queryEntity);
            //if (eList.Count > 0) { driverlist = eList; }
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 粤东线配载合同 上传好来运系统
        /// </summary>
        public void uploadHLYDepmanifest()
        {
            String json = Request["data"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<HlyOnlyAwbEntity> list = new List<HlyOnlyAwbEntity>();
            AwbEntity depm = new AwbEntity();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;

            try
            {
                string contract = string.Empty;
                foreach (Hashtable row in rows)
                {
                    contract = Convert.ToString(row["ContractNum"]);
                    break;
                }
                depm.ContractNum = contract;
                ArriveBus bus = new ArriveBus();
                Hashtable listht = bus.QueryAwbByContractNum(1, 1000, depm);
                List<AwbEntity> eList = listht["rows"] as List<AwbEntity>;
                if (msg.Result)
                {
                    foreach (var it in eList)
                    {
                        list.Add(new HlyOnlyAwbEntity { awbno = it.AwbNo });
                    }
                    string postData = "{\"token\":\"KJoGyh!$#kpxlc\",\"contract\":\"" + contract + "\",\"addpeizdetailed\":" + JSON.Encode(list) + "}";

                    string result = wxHttpUtility.SendHttpRequest(@"http://api.hlyex.net:9001/xlc.ashx?action=addpeiz", postData);
                    ArrayList ress = (ArrayList)JSON.Decode("[" + result + "]");
                    LogBus logBus = new LogBus();
                    LogEntity log = new LogEntity();
                    log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                    log.Moudle = "营运管理";
                    log.NvgPage = "上传配载合同";
                    log.UserID = UserInfor.LoginName.Trim();
                    log.Operate = "U";
                    log.Memo = "上传成功" + result;
                    logBus.InsertLog(log);
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 查询运单信息和车辆配载信息
        /// </summary>
        public void QueryAwbInfoByContractNumForExport()
        {
            DepManifestEntity queryEntity = new DepManifestEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode("参数有误")); return; }
            queryEntity.ContractNum = key.Trim();
            queryEntity.TransKind = "0";
            queryEntity.DelFlag = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            string err = "OK";
            DepManifestEntity eList = bus.QueryAwbInfoByContractNumForExport(queryEntity);
            depmanifest = eList;
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        public DepManifestEntity depmanifest
        {
            get
            {
                if (Session["depmanifest"] == null)
                {
                    Session["depmanifest"] = new DepManifestEntity();
                }
                return (DepManifestEntity)(Session["depmanifest"]);
            }
            set
            {
                Session["depmanifest"] = value;
            }
        }
        /// <summary>
        /// 车辆合同管理界面导出
        /// </summary>
        public void QueryDepManifestForExport()
        {
            DepManifestEntity queryEntity = new DepManifestEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.TruckNum = arr[i].Trim(); } break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.ContractNum = arr[i].Trim(); } break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.StartDate = Convert.ToDateTime(arr[i].Trim()); } break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.EndDate = Convert.ToDateTime(arr[i].Trim()); } break;
                    case 5:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Dest = arr[i].Trim(); } break;
                    case 4:
                        if (!arr[i].Equals("-1")) { queryEntity.DelFlag = Convert.ToString(arr[i]); } break;
                    case 6:
                        if (!arr[i].Equals("-1")) { queryEntity.CancelFlag = Convert.ToString(arr[i]); } break;
                    default:
                        break;
                }
            }
            queryEntity.TransKind = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            queryEntity.ProfitPCT = 1 - Common.GetProfitPCT();// 0.3M;
            ArriveBus bus = new ArriveBus();
            string err = "OK";
            List<DepManifestEntity> eList = bus.QueryDepManifestForExport(queryEntity);
            if (eList.Count > 0) { driverlist = eList; }
            else
            {
                err = "没有数据可以进行导出，请重新查询！";
            }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        public List<DepManifestEntity> driverlist
        {
            get
            {
                if (Session["driverlist"] == null)
                {
                    Session["driverlist"] = new List<DepManifestEntity>();
                }
                return (List<DepManifestEntity>)(Session["driverlist"]);
            }
            set
            {
                Session["driverlist"] = value;
            }
        }
        /// <summary>
        /// 查询所有运单状态为正常的数据进行配载 在修改的界面调用此方法
        /// </summary>
        public void QueryAwbFromUpdate()
        {
            AwbEntity queryEntity = new AwbEntity();
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.Dep = string.IsNullOrEmpty(Convert.ToString(Request["Dep"])) ? UserInfor.DepCity : Convert.ToString(Request["Dep"]);
            queryEntity.Dest = Convert.ToString(Request["Dest"]);
            queryEntity.DelFlag = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 1000 : Convert.ToInt32(Request["rows"]);

            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryAwbFromUpdate(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        #endregion
        #region 回单录入

        /// <summary>
        /// 查询到达的舱单来自录入回单的界面方法
        /// </summary>
        public void QueryArriveManifestFromReturnAwb()
        {
            DepManifestEntity queryEntity = new DepManifestEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.TruckNum = Convert.ToString(Request["TruckNum"]);
            queryEntity.ContractNum = Convert.ToString(Request["ContractNum"]);
            queryEntity.Dest = string.IsNullOrEmpty(Convert.ToString(Request["Dest"])) ? UserInfor.DepCity : Convert.ToString(Request["Dest"]);
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryArriveManifestFromReturnAwb(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询自己所拥有的城市
        /// </summary>
        public void QueryOnlyCity()
        {
            string json = "[";
            string[] cc = UserInfor.DepCity.Split(',');
            for (int i = 0; i < cc.Length; i++)
            {
                json += "{\"CityName\":\"" + cc[i].Trim() + "\",\"CityName\":\"" + cc[i].Trim() + "\"},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            String jsons = JSON.Encode(json);
            Response.Clear();
            Response.Write(jsons);
        }
        /// <summary>
        /// 保存录入的回单信息
        /// </summary>
        public void AddReturnAwbInfo()
        {
            string json = Request["data"];
            string[] res = json.Split('|');
            ArrayList FormRows = (ArrayList)JSON.Decode(res[0]);
            ArrayList GridRows = (ArrayList)JSON.Decode(res[1]);
            List<AwbEntity> entDest = new List<AwbEntity>();
            ArriveBus bus = new ArriveBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "回单录入";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            try
            {
                string Contract = Convert.ToString(Request["ContractNum"]);
                foreach (Hashtable row in FormRows)
                {
                    if (Convert.ToString(row["ReturnStatus"]).ToUpper().Equals("Y")) { continue; }
                    if (Convert.ToInt32(row["DelayDay"]) > 0)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(row["DelayReason"])))
                        {
                            msg.Message = "运单号：" + Convert.ToString(row["AwbNo"]) + "延误天数大于1必须选择延误原因";
                            msg.Result = false;
                            break;
                        }
                    }
                    entDest.Add(new AwbEntity
                    {
                        AwbID = Convert.ToInt64(row["AwbID"]),
                        AwbNo = Convert.ToString(row["AwbNo"]),
                        ContractNum = Contract,
                        ReturnStatus = "Y",
                        AwbStatus = "7",
                        ActMoney = Convert.ToDecimal(row["ActMoney"]),
                        Transit = Convert.ToString(row["Transit"]).Trim(),
                        Signer = Convert.ToString(row["Signer"]).Trim(),
                        DelayReason = Convert.ToString(row["DelayReason"]).Trim(),
                        BelongSystem = UserInfor.NewLandBelongSystem,
                        SignTime = !string.IsNullOrEmpty(Convert.ToString(row["SignTime"])) ? Convert.ToDateTime(row["SignTime"]) : DateTime.Now,
                        OP_ID = UserInfor.LoginName
                    });
                }
                foreach (Hashtable rowC in GridRows)
                {
                    if (Convert.ToInt32(rowC["DelayDay"]) > 0)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(rowC["DelayReason"])))
                        {
                            msg.Message = "运单号：" + Convert.ToString(rowC["AwbNo"]) + "延误天数大于1必须选择延误原因";
                            msg.Result = false;
                            break;
                        }
                    }
                    AwbEntity ae = entDest.Find(c => c.AwbID.Equals(Convert.ToInt64(rowC["AwbID"])));
                    if (ae != null && !string.IsNullOrEmpty(ae.AwbNo))
                    {
                        entDest.Remove(ae);
                        ae.ReturnInfo = Convert.ToString(rowC["ReturnInfo"]);
                        if (string.IsNullOrEmpty(Convert.ToString(rowC["ActMoney"]))) { msg.Message = "实收金额不能为空"; msg.Result = false; break; }
                        else
                        {
                            if (!Common.IsFloat(Convert.ToString(rowC["ActMoney"]))) { msg.Message = "请输入合法数据"; msg.Result = false; break; }
                        }
                        ae.ActMoney = Convert.ToDecimal(rowC["ActMoney"]);
                        ae.ContractNum = Contract;
                        ae.AwbID = Convert.ToInt64(rowC["AwbID"]);
                        ae.AwbNo = Convert.ToString(rowC["AwbNo"]);
                        ae.OP_ID = UserInfor.LoginName;
                        ae.ReturnStatus = "Y";
                        ae.AwbStatus = "7";
                        ae.BelongSystem = UserInfor.NewLandBelongSystem;
                        ae.Transit = Convert.ToString(rowC["Transit"]).Trim();
                        ae.Signer = Convert.ToString(rowC["Signer"]).Trim();
                        ae.DelayReason = Convert.ToString(rowC["DelayReason"]).Trim();
                        ae.SignTime = DateTime.Now;
                        if (!string.IsNullOrEmpty(Convert.ToString(rowC["SignTime"]))) { ae.SignTime = Convert.ToDateTime(rowC["SignTime"]); }
                        entDest.Add(ae);
                    }
                    else
                    {
                        AwbEntity aee = new AwbEntity();
                        aee.ReturnInfo = Convert.ToString(rowC["ReturnInfo"]);
                        if (string.IsNullOrEmpty(Convert.ToString(rowC["ActMoney"]))) { msg.Message = "实收金额不能为空"; msg.Result = false; break; }
                        else
                        {
                            if (!Common.IsFloat(Convert.ToString(rowC["ActMoney"]))) { msg.Message = "请输入合法数据"; msg.Result = false; break; }
                        }
                        aee.ActMoney = Convert.ToDecimal(rowC["ActMoney"]);
                        aee.ContractNum = Contract;
                        aee.AwbID = Convert.ToInt64(rowC["AwbID"]);
                        aee.AwbNo = Convert.ToString(rowC["AwbNo"]);
                        aee.OP_ID = UserInfor.LoginName;
                        aee.ReturnStatus = "Y";
                        aee.AwbStatus = "7";
                        aee.BelongSystem = UserInfor.NewLandBelongSystem;
                        aee.Transit = Convert.ToString(rowC["Transit"]).Trim();
                        aee.Signer = Convert.ToString(rowC["Signer"]).Trim();
                        aee.DelayReason = Convert.ToString(rowC["DelayReason"]).Trim();
                        aee.SignTime = DateTime.Now;
                        if (!string.IsNullOrEmpty(Convert.ToString(rowC["SignTime"]))) { aee.SignTime = Convert.ToDateTime(rowC["SignTime"]); }
                        entDest.Add(aee);
                    }
                }
                if (entDest.Count <= 0)
                {
                    msg.Message = "请选择录入回单的数据";
                    msg.Result = false;
                }
                if (msg.Result) { bus.AddReturnAwbInfo(entDest, log); }
                if (msg.Result)
                {
                    HlyCommon hly = new HlyCommon();
                    foreach (AwbEntity row in entDest)
                    {
                        if (row.HLY.Equals("0")) { continue; }//如果不是好来运单就不需要发送跟踪状态接口
                        //hly.SaveAwbStatus(new HlyEntity { Hawbno = row.HAwbNo, City = UserInfor.DepCity, Cuser = UserInfor.UserName, Cdatetime = DateTime.Now, Other = row.Remark, SplitCB = ",", Xawbno = row.AwbNo, State = row.AwbStatus });
                    }
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        /// <summary>
        /// 查询所有运单通过司机合同号
        /// </summary>
        public void QueryAwbByContractNum()
        {
            AwbEntity queryEntity = new AwbEntity();
            //查询条件
            string key = Request["ContractNum"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            queryEntity.ContractNum = key.Trim();
            queryEntity.TransKind = "0";
            queryEntity.DelFlag = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 1000 : Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryAwbByContractNum(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        /// <summary>
        /// 按运单录入回单
        /// </summary>
        public void QueryAwbForReturn()
        {
            AwbEntity queryEntity = new AwbEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.ShipperUnit = Convert.ToString(Request["ShipperUnit"]);
            queryEntity.Dest = string.IsNullOrEmpty(Convert.ToString(Request["Dest"])) ? UserInfor.DepCity : Convert.ToString(Request["Dest"]);

            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            queryEntity.TransKind = "0";
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 1000 : Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryAwbForReturn(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 批量审核车辆合同
        /// </summary>
        public void plCheckManifest()
        {
            String json = Request["data"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<DepManifestEntity> list = new List<DepManifestEntity>();
            ArriveBus bus = new ArriveBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "长运配载操作";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Memo = "审核成功";
            try
            {
                foreach (Hashtable row in rows)
                {
                    if (Convert.ToString(row["DelFlag"]).Trim().Equals("1"))//审核过的跳过
                    {
                        continue;
                    }
                    if (!Convert.ToString(row["TruckFlag"]).Trim().Equals("0"))//请审核在站的车辆合同
                    {
                        msg.Message = "请审核在站的车辆合同";
                        msg.Result = false;
                    }
                    if (Convert.ToString(row["CancelFlag"]).Trim().Equals("1"))//合同已作废
                    {
                        msg.Message = "合同已作废，不允许审核";
                        msg.Result = false;
                    }
                    DepManifestEntity dmf = bus.QueryDepManifest(new DepManifestEntity { ContractNum = Convert.ToString(row["ContractNum"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    if (!dmf.TruckFlag.Equals("0") && !dmf.TruckFlag.Equals("1"))
                    {
                        msg.Message = "车辆合同不是在站和出发状态，请刷新重新操作！";
                        msg.Result = false;
                        break;
                    }
                    list.Add(new DepManifestEntity
                    {
                        ContractNum = Convert.ToString(row["ContractNum"]),
                        TruckNum = Convert.ToString(row["TruckNum"]),
                        StartTime = Convert.ToDateTime(row["StartTime"]),
                        FinanceFirstCheck = "1",
                        FirstCheckName = UserInfor.UserName,
                        FirstCheckDate = DateTime.Now,
                        OP_ID = UserInfor.LoginName,
                        BelongSystem = UserInfor.NewLandBelongSystem,
                        DelFlag = "1"
                    });
                }
                if (msg.Result)
                {
                    if (list.Count <= 0) { msg.Result = false; msg.Message = "请选择数据"; }
                    else { bus.UpdateManifest(list, log); }
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 未审
        /// </summary>
        public void plunCheckManifest()
        {
            String json = Request["data"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<DepManifestEntity> list = new List<DepManifestEntity>();
            ArriveBus bus = new ArriveBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "长运配载操作";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Memo = "未审成功";
            try
            {
                foreach (Hashtable row in rows)
                {
                    if (!Convert.ToString(row["TruckFlag"]).Trim().Equals("0") && !Convert.ToString(row["TruckFlag"]).Trim().Equals("1"))//请审核在站的车辆合同
                    {
                        msg.Message = "请未审在站和出发的车辆合同";
                        msg.Result = false;
                    }
                    DepManifestEntity dmf = bus.QueryDepManifest(new DepManifestEntity { ContractNum = Convert.ToString(row["ContractNum"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    if (!dmf.TruckFlag.Equals("0") && !dmf.TruckFlag.Equals("1"))
                    {
                        msg.Message = "车辆合同不是在站和出发状态，请刷新重新操作！";
                        msg.Result = false;
                        break;
                    }
                    if (Convert.ToString(row["CancelFlag"]).Trim().Equals("1"))//合同已作废
                    {
                        msg.Message = "合同已作废，不允许未审";
                        msg.Result = false;
                    }
                    list.Add(new DepManifestEntity
                    {
                        ContractNum = Convert.ToString(row["ContractNum"]),
                        TruckNum = Convert.ToString(row["TruckNum"]),
                        StartTime = Convert.ToDateTime(row["StartTime"]),
                        FinanceFirstCheck = "0",
                        FirstCheckName = UserInfor.UserName,
                        FirstCheckDate = DateTime.Now,
                        OP_ID = UserInfor.LoginName,
                        BelongSystem = UserInfor.NewLandBelongSystem,
                        DelFlag = "0"
                    });
                }
                if (msg.Result)
                {
                    if (list.Count <= 0) { msg.Result = false; msg.Message = "请选择数据"; }
                    else { bus.UpdateManifest(list, log); }
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 删除司机合同
        /// </summary>
        public void DelManifest()
        {
            String json = Request["data"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<DepManifestEntity> list = new List<DepManifestEntity>();
            ArriveBus bus = new ArriveBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "车辆合同管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            try
            {
                foreach (Hashtable row in rows)
                {
                    if (!Convert.ToString(row["TruckFlag"]).Trim().Equals("0") && !Convert.ToString(row["TruckFlag"]).Trim().Equals("1"))//请作废在站或出发的车辆合同
                    {
                        msg.Message = "请作废在站或出发的车辆合同";
                        msg.Result = false;
                    }
                    DepManifestEntity dmf = bus.QueryDepManifest(new DepManifestEntity { ContractNum = Convert.ToString(row["ContractNum"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    if (!dmf.TruckFlag.Equals("0") && !dmf.TruckFlag.Equals("1"))
                    {
                        msg.Message = "车辆合同不是在站和出发状态，请刷新重新操作！";
                        msg.Result = false;
                        break;
                    }
                    list.Add(new DepManifestEntity
                    {
                        ContractNum = Convert.ToString(row["ContractNum"]),
                        TruckNum = Convert.ToString(row["TruckNum"]),
                        Dep = Convert.ToString(row["Dep"]),
                        BelongSystem = UserInfor.NewLandBelongSystem,
                        CancelFlag = "1"
                    });
                }
                if (msg.Result)
                {
                    bus.DelManifest(list, log);
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 启用作废的车辆合同
        /// </summary>
        public void RenewManifest()
        {
            String json = Request["data"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<DepManifestEntity> list = new List<DepManifestEntity>();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "车辆合同管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            ArriveBus bus = new ArriveBus();
            try
            {
                foreach (Hashtable row in rows)
                {
                    DepManifestEntity dmf = bus.QueryDepManifest(new DepManifestEntity { ContractNum = Convert.ToString(row["ContractNum"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    if (!dmf.CancelFlag.Equals("1"))
                    {
                        msg.Message = "车辆合同不是作废状态，请刷新重新操作！";
                        msg.Result = false;
                        break;
                    }
                    list.Add(new DepManifestEntity
                    {
                        ContractNum = Convert.ToString(row["ContractNum"]),
                        TruckNum = Convert.ToString(row["TruckNum"]),
                        Dep = Convert.ToString(row["Dep"]),
                        BelongSystem = UserInfor.NewLandBelongSystem,
                        CancelFlag = "0"
                    });
                }
                if (msg.Result) { bus.RenewManifest(list, log); }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 修改配载信息，并设置运单的状态为配载，更新司机合同号
        /// </summary>
        public void UpdateContractInfo()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            DepManifestEntity ent = new DepManifestEntity();
            List<AwbEntity> entDest = new List<AwbEntity>();
            ArriveBus bus = new ArriveBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "营运管理";
            log.NvgPage = "长运配载操作";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            DepManifestEntity dmf = bus.QueryDepManifest(new DepManifestEntity { ContractNum = Convert.ToString(Request["ContractNum"]), BelongSystem = UserInfor.NewLandBelongSystem });
            if (dmf.DelFlag.Equals("1"))
            {
                msg.Message = "车辆合同已审核，请刷新重新操作！";
                msg.Result = false;
            }
            if (msg.Result)
            {
                try
                {
                    ent.ContractNum = Convert.ToString(Request["ContractNum"]);
                    ent.TruckNum = Convert.ToString(Request["PTruckNum"]);
                    ent.Model = Convert.ToString(Request["Model"]);
                    ent.Length = Convert.ToDecimal(Request["Length"]);
                    ent.Dep = Convert.ToString(Request["Dep"]);
                    ent.Dest = Convert.ToString(Request["Dest"]);
                    ent.Transit = Convert.ToString(Request["Transit"]);
                    ent.Driver = Convert.ToString(Request["Driver"]);
                    ent.DriverCellPhone = Convert.ToString(Request["DriverCellPhone"]);
                    ent.DriverIDAddress = Convert.ToString(Request["DriverIDAddress"]);
                    ent.DriverIDNum = Convert.ToString(Request["DriverIDNum"]);
                    ent.Weight = string.IsNullOrEmpty(Convert.ToString(Request["Weight"])) ? 0 : Convert.ToDecimal(Request["Weight"]);
                    ent.Volume = string.IsNullOrEmpty(Convert.ToString(Request["Volume"])) ? 0 : Convert.ToDecimal(Request["Volume"]);
                    ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                    ent.PrepayFee = string.IsNullOrEmpty(Convert.ToString(Request["PrepayFee"])) ? 0 : Convert.ToDecimal(Request["PrepayFee"]);
                    ent.ArriveFee = string.IsNullOrEmpty(Convert.ToString(Request["ArriveFee"])) ? 0 : Convert.ToDecimal(Request["ArriveFee"]);
                    ent.PayMode = Convert.ToString(Request["PayMode"]);
                    ent.DestPeople = Convert.ToString(Request["DestPeople"]);
                    ent.DestCellphone = Convert.ToString(Request["DestCellphone"]);
                    ent.UnLoadAddress = Convert.ToString(Request["UnLoadAddress"]);
                    ent.StartTime = Convert.ToDateTime(Request["StartTime"]);
                    ent.PassTime = string.IsNullOrEmpty(Convert.ToString(Request["PassTime"])) ? 0 : Convert.ToDecimal(Request["PassTime"]);
                    ent.PreArriveTime = ent.StartTime.AddMinutes(Convert.ToDouble(ent.PassTime) * 60);
                    ent.OP_ID = UserInfor.LoginName.Trim();
                    ent.Remark = Convert.ToString(Request["Remark"]);
                    ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                    ent.Loader = Convert.ToString(Request["Loader"]).Trim();
                    ent.Manifester = string.IsNullOrEmpty(Convert.ToString(Request["Manifester"]).Trim()) ? UserInfor.UserName.Trim() : Convert.ToString(Request["Manifester"]).Trim();
                    ent.Dispatcher = Convert.ToString(Request["Dispatcher"]).Trim();
                    ent.CardName = Convert.ToString(Request["PCardName"]).Trim();
                    ent.CardBank = Convert.ToString(Request["CardBank"]).Trim();
                    ent.CardNum = Convert.ToString(Request["CardNum"]).Trim();
                    ent.TransKind = "0";
                    ent.BelongSystem = UserInfor.NewLandBelongSystem;
                    foreach (Hashtable row in GridRows)
                    {
                        entDest.Add(new AwbEntity
                        {
                            AwbID = Convert.ToInt64(row["AwbID"]),
                            AwbNo = Convert.ToString(row["AwbNo"]),
                            BelongSystem = UserInfor.NewLandBelongSystem,
                            ContractNum = ent.ContractNum.Trim()
                        });
                    }
                    if (string.IsNullOrEmpty(ent.ContractNum))
                    {
                        msg.Message = "系统错误，请刷新重新操作！";
                        msg.Result = false;
                    }
                    if (msg.Result)
                    {
                        ent.AwbInfo = entDest;
                        bus.UpdateContractInfo(ent, log);
                    }
                }
                catch (ApplicationException ex)
                {
                    msg.Message = ex.Message;
                    msg.Result = false;
                }
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
        #region 车辆在途跟踪

        /// <summary>
        /// 车辆在途情况查询
        /// </summary>
        public void QueryCarStatusTrack()
        {
            DepManifestEntity queryEntity = new DepManifestEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.TruckNum = Convert.ToString(Request["TruckNum"]);
            queryEntity.ContractNum = Convert.ToString(Request["ContractNum"]);
            queryEntity.Dest = string.IsNullOrEmpty(Convert.ToString(Request["Dest"])) ? UserInfor.DepCity : Convert.ToString(Request["Dest"]);
            if (Request["TruckFlag"] != "-1") { queryEntity.TruckFlag = Convert.ToString(Request["TruckFlag"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;

            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryArriveManifest(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询所有运单通过司机合同号为了导出所用
        /// </summary>
        public void QueryAwbByContractNumForExport()
        {
            AwbEntity queryEntity = new AwbEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode("参数有误")); return; }
            queryEntity.ContractNum = key.Trim();
            queryEntity.TransKind = "0";
            queryEntity.DelFlag = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            string err = "OK";
            Hashtable list = bus.QueryAwbByContractNum(1, 1000, queryEntity);
            List<AwbEntity> eList = list["rows"] as List<AwbEntity>;
            if (eList.Count > 0) { AwbMonitor = eList; } else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<AwbEntity> AwbMonitor
        {
            get
            {
                if (Session["AwbMonitor"] == null)
                {
                    Session["AwbMonitor"] = new List<AwbEntity>();
                }
                return (List<AwbEntity>)(Session["AwbMonitor"]);
            }
            set
            {
                Session["AwbMonitor"] = value;
            }
        }
        /// <summary>
        /// 通过车辆合同号查询配载信息
        /// </summary>
        public void QueryDepManifestByNum()
        {
            string AwbNo = Request["id"];
            DepManifestEntity ae = new DepManifestEntity();
            if (!string.IsNullOrEmpty(AwbNo))
            {
                ae.ContractNum = AwbNo.ToUpper().Trim();
            }
            ae.TransKind = "0";
            ae.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            DepManifestEntity result = bus.QueryDepManifest(ae);
            if (result == null)
            {
                Response.Write("");
                return;
            }
            String json = JSON.Encode(result);
            Response.Write(json);
        }
        /// <summary>
        /// 查询车辆跟踪信息
        /// </summary>
        public void QueryTruckStatusTrack()
        {
            string ContractNum = Request["id"];
            TruckStatusTrackEntity queryEntity = new TruckStatusTrackEntity();
            queryEntity.ContractNum = ContractNum;
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            DataTable result = bus.QueryTruckStatusTrack(queryEntity);
            string res = "跟踪记录，目前状态：\r\n";
            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow it in result.Rows)
                {
                    string yj = "预计";
                    if (Convert.ToString(it["TruckFlag"]).Trim().Equals("3"))
                    {
                        yj = "实际";
                    }
                    double db = 0;
                    if (!string.IsNullOrEmpty(Convert.ToString(it["LastHour"])))
                    {
                        db = Convert.ToDouble(it["LastHour"]);
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(it["ArriveTime"])))
                    {
                        continue;
                    }
                    res += Convert.ToString(it["UserName"]).Trim() + "[" + Convert.ToDateTime(it["OP_DATE"]).ToString("yyyy-MM-dd HH:mm:ss") + "]：\r\n" + Convert.ToString(it["DetailInfo"]).Trim() + "，目前在" + Convert.ToString(it["CurrentLocation"]).Trim() + "，" + yj + "[" + Convert.ToDateTime(it["ArriveTime"]).ToString("yyyy-MM-dd HH:mm:ss") + "]到达\r\n";
                }
            }
            String json = JSON.Encode(res);
            Response.Write(json);
        }
        /// <summary>
        /// 保存车辆状态跟踪信息
        /// </summary>
        public void SaveTruckStatusTrack()
        {
            TruckStatusTrackEntity queryEntity = new TruckStatusTrackEntity();
            queryEntity.TruckFlag = Convert.ToString(Request["TruckFlag"]);
            queryEntity.CurrentLocation = Convert.ToString(Request["CurrentLocation"]);
            queryEntity.DetailInfo = Convert.ToString(Request["DetailInfo"]);
            queryEntity.TruckNum = Convert.ToString(Request["TruckNum"]);
            queryEntity.ContractNum = Convert.ToString(Request["ContractNum"]);
            queryEntity.ArriveTime = Convert.ToDateTime(Request["ArriveTime"]);
            TimeSpan ts = queryEntity.ArriveTime - DateTime.Now;
            queryEntity.LastHour = ts.Hours;
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            if (string.IsNullOrEmpty(queryEntity.ContractNum))
            {
                msg.Message = "车辆合同号为空，系统错误！";
                msg.Result = false;
            }
            if (string.IsNullOrEmpty(queryEntity.TruckNum))
            {
                msg.Message = "车牌号为空，系统错误！";
                msg.Result = false;
            }
            ArriveBus bus = new ArriveBus();
            DepManifestEntity result = bus.QueryDepManifest(new DepManifestEntity { TransKind = "0", ContractNum = queryEntity.ContractNum, BelongSystem = UserInfor.NewLandBelongSystem });
            if (result != null)
            {
                if (result.TruckFlag.Equals("3"))
                {
                    msg.Message = "车辆已经到达，请重新查询数据再操作！";
                    msg.Result = false;
                }
                if (result.TruckFlag.Equals("4"))
                {
                    msg.Message = "车辆状态已经结束，请重新查询数据再操作！";
                    msg.Result = false;
                }
            }
            try
            {
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "营运管理";
                log.NvgPage = "车辆状态跟踪";
                log.UserID = UserInfor.LoginName.Trim();
                log.Operate = "U";

                queryEntity.OP_ID = UserInfor.LoginName;
                if (msg.Result)
                {
                    bus.SaveTruckStatusTrack(queryEntity, log);
                }
                // queryEntity.ContractNum = "GV191019001";
                //增加运单跟踪状态
                List<AwbEntity> awblist = bus.QueryAwbByContractNum(new AwbEntity { ContractNum = queryEntity.ContractNum.Trim(), BelongSystem = UserInfor.NewLandBelongSystem });
                HlyCommon hly = new HlyCommon();
                foreach (AwbEntity awb in awblist)
                {
                    if (awb.HLY.Equals("0")) { continue; }
                    //hly.SaveAwbStatus(new HlyEntity { Hawbno = awb.HAwbNo, City = UserInfor.DepCity, Cuser = UserInfor.UserName, Cdatetime = DateTime.Now, Other = awb.Remark, SplitCB = ",", Xawbno = awb.AwbNo, State = queryEntity.TruckFlag });
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
        #region 到达运单派送
        /// <summary>
        /// 查询到达本站的运单
        /// </summary>
        public void QueryArriveDestAwb()
        {
            AwbEntity queryEntity = new AwbEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["AwbNo"]))) { queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]); }
            else
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
                if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
                queryEntity.ShipperUnit = Convert.ToString(Request["ShipperUnit"]);
                queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
                queryEntity.Dep = Convert.ToString(Request["Dep"]);
                queryEntity.Dest = string.IsNullOrEmpty(Convert.ToString(Request["Dest"])) ? UserInfor.DelFlag : Convert.ToString(Request["Dest"]);
                queryEntity.Transit = Convert.ToString(Request["Transit"]);
                queryEntity.CheckOutType = Convert.ToString(Request["CheckOutType"]);
                queryEntity.Transit = Convert.ToString(Request["Transit"]);
                queryEntity.ReturnStatus = "N";
                queryEntity.AwbStatus = "3";
                queryEntity.TransKind = "0";
            }
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 1000 : Convert.ToInt32(Request["rows"]);

            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryAwbRetrunStatus(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        /// <summary>
        /// 合并运单数据
        /// </summary>
        public void DeliveryMerge()
        {
            String json = Request["submitData"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            AwbEntity entity = new AwbEntity();
            List<AwbEntity> listEntity = new List<AwbEntity>();
            //ManifestBus bus = new ManifestBus();
            ArriveBus bus = new ArriveBus();
            string awbno = string.Empty;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            int p = 0;
            decimal w = 0, v = 0;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "到达运单配送";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "U";
            log.Status = "0";
            try
            {
                foreach (Hashtable row in rows)
                {
                    #region 修改数据
                    if (!string.IsNullOrEmpty(awbno) && !awbno.Equals(Convert.ToString(row["AwbNo"]).Trim()))
                    {
                        msg.Message = "必须是相同的运单号";
                        msg.Result = false;
                        break;
                    }
                    bool cz = bus.IsExistArriveAwb(new DeliveryEntity { ArriveID = Convert.ToInt64(row["ArriveID"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    if (!cz)
                    {
                        msg.Message = "运单不存在，请刷新后操作！";
                        msg.Result = false;
                        break;
                    }
                    p += Convert.ToInt32(row["AwbPiece"]);
                    w += Convert.ToDecimal(row["AwbWeight"]);
                    v += Convert.ToDecimal(row["AwbVolume"]);
                    awbno = Convert.ToString(row["AwbNo"]).Trim();
                    listEntity.Add(new AwbEntity { ArriveID = Convert.ToInt64(row["ArriveID"]), AwbID = Convert.ToInt64(row["AwbID"]), AwbNo = awbno, AwbPiece = Convert.ToInt32(row["AwbPiece"]), AwbWeight = Convert.ToDecimal(row["AwbWeight"]), AwbVolume = Convert.ToDecimal(row["AwbVolume"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    #endregion
                }
                if (msg.Result)
                {
                    entity.AwbPiece = p;
                    entity.AwbWeight = w;
                    entity.AwbVolume = v;
                    entity.AwbNo = awbno;
                    entity.BelongSystem = UserInfor.NewLandBelongSystem;
                    bus.DeliveryMerge(entity, listEntity, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 到达配送分批
        /// </summary>
        public void DeliveryTear()
        {
            ArriveBus bus = new ArriveBus();
            List<AwbEntity> queryEntity = new List<AwbEntity>();
            int tp = 0, p = 0;
            decimal w = 0, v = 0;
            long awbid = 0; string awbno = string.Empty;
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "到达运单配送";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            foreach (Hashtable row in rows)
            {
                awbid = Convert.ToInt64(row["ArriveID"]);
                p = Convert.ToInt32(row["AwbPiece"]);
                w = Convert.ToDecimal(row["AwbWeight"]);
                v = Convert.ToDecimal(row["AwbVolume"]);
                awbno = Convert.ToString(row["AwbNo"]).Trim();
            }
            tp = Convert.ToInt32(Request["TearPiece"]);
            if (tp >= p)
            {
                msg.Message = "分批件数不能大于总件数";
                msg.Result = false;
            }
            try
            {
                bool cz = bus.IsExistArriveAwb(new DeliveryEntity { ArriveID = awbid, BelongSystem = UserInfor.NewLandBelongSystem });
                if (!cz)
                {
                    msg.Message = "运单不存在，请刷新后操作！";
                    msg.Result = false;
                }
                if (msg.Result)
                {
                    int op = p - tp; //剩余件数
                    decimal tw = Convert.ToDecimal(w * tp / p); //分批重量
                    decimal tv = Convert.ToDecimal(v * tp / p); //分批体积
                    AwbEntity ae = new AwbEntity { ArriveID = awbid, AwbNo = awbno, AwbPiece = tp, AwbWeight = tw, AwbVolume = tv, Piece = p, Weight = w, Volume = v, BelongSystem = UserInfor.NewLandBelongSystem };
                    queryEntity.Add(ae);
                    AwbEntity oe = new AwbEntity { ArriveID = awbid, AwbNo = awbno, AwbPiece = op, AwbWeight = w - tw, AwbVolume = v - tv, Piece = p, Weight = w, Volume = v, BelongSystem = UserInfor.NewLandBelongSystem };
                    queryEntity.Add(oe);
                    bus.DeliveryTear(queryEntity, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #region
        /// <summary>
        /// 短途配送合同号
        /// 2014-08-11 当发车时间改为系统当前时间
        /// </summary>
        /// <param name="dep">当前站点</param>
        /// <param name="starttime">发车时间</param>
        /// <returns></returns>
        public static string GetCityCodeShortName(string dep, DateTime starttime, string BelongSystem)
        {
            ArriveBus bus = new ArriveBus();
            string result = string.Empty;
            string PreStr = GetCityName(dep, BelongSystem);
            //Random rd = new Random((int)DateTime.Now.Ticks);
            //int sd = rd.Next(10000000, 99999999);
            string sd = DateTime.Now.ToString("yyMMdd");// starttime.ToString("yyMMdd");

            string cnum = bus.ReturnDayTheTruckNum(new DeliveryEntity { StartDate = DateTime.Now, EndDate = DateTime.Now, Dest = dep, BelongSystem = BelongSystem });

            result = PreStr + PreStr + sd + cnum;
            return result;
        }
        /// <summary>
        /// 根据传入的城市名称返回该城市的缩写用以自动生成合同号
        /// 合同号由：城市缩写+当前月日时分组成  
        /// 例如：广州到南宁2014-04-20 当天第5辆车，GN140420005
        /// 2014-08-11 当发车时间改为系统当前时间
        /// </summary>
        /// <param name="dep">出发站</param>
        /// <param name="dest">到达站</param>
        /// <param name="starttime">发车时间</param>
        /// <param name="transkind">配载类型</param>
        /// <returns></returns>
        public static string GetCityCodeShortName(string dep, string dest, DateTime starttime, string transkind, string BelongSystem)
        {
            ArriveBus bus = new ArriveBus();
            string result = string.Empty;
            string PreStr = GetCityName(dep, BelongSystem);
            string AftStr = GetCityName(dest, BelongSystem);
            string sd = DateTime.Now.ToString("yyMMdd"); //starttime.ToString("yyMMdd");
            string cnum = bus.ReturnDayTheTruckNum(new DepManifestEntity { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), TransKind = transkind, Dep = dep, Dest = dest, BelongSystem = BelongSystem });
            result = PreStr + AftStr + sd + cnum;
            return result;
        }
        /// <summary>
        /// 返回城市的缩写词
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        private static string GetCityName(string city, string BelongSystem)
        {
            ArriveBus bus = new ArriveBus();
            List<CityEntity> result = bus.QueryAllCity(new CityEntity { CityName = city, BelongSystem = BelongSystem });
            string res = "G";
            if (result.Count > 0)
            {
                res = result[0].CityCode.Trim();
            }
            return res;
        }
        /// <summary>
        /// 查询所有车辆信息
        /// </summary>
        public void QueryCarrier()
        {
            ArriveBus bus = new ArriveBus();
            List<CarrierEntity> list = bus.QueryCarrier(UserInfor.NewLandBelongSystem);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        #endregion
        /// <summary>
        /// 保存配送运单信息
        /// </summary>
        public void AddDelivery()
        {
            string json = Request["data"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            List<DeliveryEntity> entDest = new List<DeliveryEntity>();
            ArriveBus bus = new ArriveBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "到达运单配送";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "A";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string htnum = string.Empty;
            try
            {
                int p = 0;
                if (DateTime.Compare(Convert.ToDateTime(Request["StartTime"]), DateTime.Now) > 0)
                {
                    msg.Message = "发车时间不能晚于当前时间";
                    msg.Result = false;
                }
                if (msg.Result)
                {
                    foreach (Hashtable rows in GridRows)
                    {
                        DeliveryEntity ent = new DeliveryEntity();
                        string dep = Convert.ToString(rows["Dep"]).Trim();
                        string dest = Convert.ToString(rows["Dest"]).Trim();
                        if (string.IsNullOrEmpty(htnum))
                        {
                            htnum = GetCityCodeShortName(UserInfor.DepCity.Trim(), Convert.ToDateTime(Request["StartTime"]), UserInfor.NewLandBelongSystem);//配送合同号
                        }
                        //ent.DeliveryNum = Common.GetCityCodeShortName(dep);//配送合同号
                        ent.DeliveryNum = htnum;
                        ent.TruckNum = Convert.ToString(Request["PTruckNum"]);
                        ent.Driver = Convert.ToString(Request["Driver"]);
                        ent.DriverCellPhone = Convert.ToString(Request["DriverCellPhone"]);
                        ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                        ent.OtherFee = string.IsNullOrEmpty(Convert.ToString(Request["OtherFee"])) ? 0 : Convert.ToDecimal(Request["OtherFee"]);
                        ent.DType = "0";
                        ent.StartTime = Convert.ToDateTime(Request["StartTime"]);
                        ent.PreArriveTime = Convert.ToDateTime(Request["StartTime"]);
                        ent.OP_ID = UserInfor.LoginName.Trim();
                        ent.Remark = Convert.ToString(Request["Remark"]);
                        ent.ArriveID = Convert.ToInt64(rows["ArriveID"]);
                        ent.AwbID = Convert.ToInt64(rows["AwbID"]);
                        ent.AwbNo = Convert.ToString(rows["AwbNo"]);
                        ent.AwbStatus = Convert.ToString(rows["AwbStatus"]);
                        ent.Dest = UserInfor.DepCity.Trim();
                        ent.CurCity = UserInfor.DepCity.Trim();
                        ent.Mode = "0";
                        ent.BelongSystem = UserInfor.NewLandBelongSystem;
                        entDest.Add(ent);
                        p += Convert.ToInt32(rows["AwbPiece"]);
                    }
                    entDest[0].PieceTotal = p;

                    msg.Message = htnum;
                    int result = bus.AddDelivery(entDest, log);
                    if (result.Equals(1))
                    {
                        msg.Message = "运单已经做过配送，请勿重复保存";
                        msg.Result = false;
                    }
                    if (result.Equals(2))
                    {
                        msg.Message = "该车辆已经被配载，请更改车辆";
                        msg.Result = false;
                    }
                    if (result.Equals(3))
                    {
                        msg.Message = "该车辆已被拉入黑名单，请更改后保存";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    HlyCommon hly = new HlyCommon();
                    foreach (Hashtable row in GridRows)
                    {
                        if (Convert.ToString(row["HLY"]).Equals("0")) { continue; }//如果不是好来运单就不需要发送跟踪状态接口
                        //hly.SaveAwbStatus(new HlyEntity { Hawbno = Convert.ToString(row["HAwbNo"]), City = UserInfor.DepCity, Cuser = UserInfor.UserName, Cdatetime = entDest[0].StartTime, Other = Convert.ToString(row["Remark"]), SplitCB = ",", Xawbno = Convert.ToString(row["AwbNo"]), State = "8" });
                    }
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
        #region 配送运单管理

        /// <summary>
        /// 配送运单管理
        /// </summary>
        public void QueryDeliveryOrder()
        {
            DeliveryEntity queryEntity = new DeliveryEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.TruckNum = Convert.ToString(Request["TruckNum"]);
            queryEntity.Driver = Convert.ToString(Request["Driver"]);
            queryEntity.DeliveryNum = Convert.ToString(Request["DeliveryNum"]);
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.CurCity = string.IsNullOrEmpty(Convert.ToString(Request["Dest"])) ? UserInfor.DepCity : Convert.ToString(Request["Dest"]);
            queryEntity.OP_ID = Convert.ToString(Request["OPID"]);
            queryEntity.Mode = "0";
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryDeliveryOrder(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询所有车辆信息
        /// </summary>
        public void QueryTruck()
        {
            ArriveBus bus = new ArriveBus();
            TruckEntity te = new TruckEntity();
            //te.TripMark = "0";
            te.CurCity = UserInfor.DepCity;
            te.BelongSystem = UserInfor.NewLandBelongSystem;
            //te.DelFlag = "0";
            List<TruckCacheEntity> list = bus.QueryTruck(te);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据配送单号查询所有运单
        /// </summary>
        public void QueryAwbInfoByDeliveryID()
        {
            DeliveryEntity queryEntity = new DeliveryEntity();
            //查询条件
            string key = Request["id"];
            if (string.IsNullOrEmpty(key))
            {
                Response.Clear();
                Response.Write(JSON.Encode(queryEntity));
                return;
            }
            queryEntity.DeliveryID = Convert.ToInt64(key.Trim());
            queryEntity.Mode = Convert.ToString(Request["mode"]);
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            //queryEntity.DType = "0";

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);

            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryAwbInfoByDeliveryID(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 审核配送单
        /// </summary>
        public void CheckDelivery()
        {
            String idStr = Request["data"];
            if (String.IsNullOrEmpty(idStr)) return;
            List<DeliveryEntity> list = new List<DeliveryEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "配送运单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            log.Memo = "审核成功";
            ArriveBus bus = new ArriveBus();
            foreach (Hashtable row in rows)
            {
                if (Convert.ToString(row["FinanceFirstCheck"]).Trim().Equals("1"))
                {
                    continue;
                }
                DeliveryEntity rest = bus.QueryAwbByDeliveryID(Convert.ToInt64(row["DeliveryID"]), "0");
                if (!rest.FinanceSecondCheck.Equals("0"))
                {
                    msg.Message = "合同号：" + row["DeliveryNum"] + "二审已审核";
                    msg.Result = false;
                    break;
                }
                if (!rest.CheckStatus.Equals("0"))
                {
                    msg.Message = "合同号：" + row["DeliveryNum"] + "二审已结算";
                    msg.Result = false;
                    break;
                }
                list.Add(new DeliveryEntity
                {
                    DeliveryID = Convert.ToInt64(row["DeliveryID"]),
                    FinanceFirstCheck = "1",
                    FirstCheckName = UserInfor.UserName.Trim(),
                    BelongSystem = UserInfor.NewLandBelongSystem,
                    FirstCheckDate = DateTime.Now
                });
            }
            try
            {
                if (msg.Result)
                {
                    bus.CheckDelivery(list, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 修改为未审
        /// </summary>
        public void UnLockDelivery()
        {
            String idStr = Request["data"];
            if (String.IsNullOrEmpty(idStr)) return;
            List<DeliveryEntity> list = new List<DeliveryEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "配送运单管理";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "U";
            log.Status = "0";
            log.Memo = "未审成功";
            ArriveBus bus = new ArriveBus();
            foreach (Hashtable row in rows)
            {
                if (Convert.ToString(row["FinanceFirstCheck"]).Trim().Equals("0")) { continue; }
                DeliveryEntity rest = bus.QueryAwbByDeliveryID(Convert.ToInt64(row["DeliveryID"]), "0");
                if (!rest.FinanceSecondCheck.Equals("0"))
                {
                    msg.Message = "合同号：" + row["DeliveryNum"] + "二审已审核";
                    msg.Result = false;
                    break;
                }
                if (!rest.CheckStatus.Equals("0"))
                {
                    msg.Message = "合同号：" + row["DeliveryNum"] + "二审已结算";
                    msg.Result = false;
                    break;
                }
                list.Add(new DeliveryEntity
                {
                    DeliveryID = Convert.ToInt64(row["DeliveryID"]),
                    FinanceFirstCheck = "0",
                    FirstCheckName = UserInfor.UserName.Trim(),
                    BelongSystem = UserInfor.NewLandBelongSystem,
                    FirstCheckDate = DateTime.Now
                });
            }
            try
            {
                if (msg.Result)
                {
                    bus.CheckDelivery(list, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 删除配送运单
        /// </summary>
        public void DelDelivery()
        {
            String idStr = Request["data"];
            if (String.IsNullOrEmpty(idStr)) return;
            List<DeliveryEntity> list = new List<DeliveryEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "配送运单管理";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "D";
            log.Status = "0";
            ArriveBus bus = new ArriveBus();
            foreach (Hashtable row in rows)
            {
                if (Convert.ToString(row["FinanceFirstCheck"]).Trim().Equals("1"))
                {
                    msg.Message = "审核状态的配送单不能删除";
                    msg.Result = false;
                    break;
                }
                if (bus.IsAllDeliveryByDeliverID(Convert.ToInt64(row["DeliveryID"]), "0", "8", UserInfor.NewLandBelongSystem))
                {
                    msg.Message = "配送单里的运单不是配送状态，不能删除";
                    msg.Result = false;
                    break;
                }
                DeliveryEntity rest = bus.QueryAwbByDeliveryID(Convert.ToInt64(row["DeliveryID"]), "0");
                if (!rest.FinanceSecondCheck.Equals("0"))
                {
                    msg.Message = "合同号：" + row["DeliveryNum"] + "二审已审核";
                    msg.Result = false;
                    break;
                }
                if (!rest.CheckStatus.Equals("0"))
                {
                    msg.Message = "合同号：" + row["DeliveryNum"] + "二审已结算";
                    msg.Result = false;
                    break;
                }
                list.Add(new DeliveryEntity
                {
                    DeliveryID = Convert.ToInt64(row["DeliveryID"]),
                    DeliveryNum = Convert.ToString(row["DeliveryNum"]),
                    TruckNum = Convert.ToString(row["TruckNum"]),
                    Driver = Convert.ToString(row["Driver"]),
                    BelongSystem = UserInfor.NewLandBelongSystem,
                    OP_ID = UserInfor.LoginName
                });
            }
            try
            {
                if (msg.Result)
                {
                    bus.DelDelivery(list, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 配送运单查询导出
        /// </summary>
        public void QueryDeliveryOrderForExport()
        {
            DeliveryEntity queryEntity = new DeliveryEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key))
            {
                Response.Clear();
                Response.Write(JSON.Encode(queryEntity));
                return;
            }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.TruckNum = arr[i].Trim();
                        }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.AwbNo = arr[i].Trim();
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.Driver = Convert.ToString(arr[i]);
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.StartDate = Convert.ToDateTime(arr[i].Trim());
                        }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.EndDate = Convert.ToDateTime(arr[i].Trim());
                        }
                        break;
                    case 5:
                        queryEntity.CurCity = string.IsNullOrEmpty(arr[i]) ? UserInfor.DepCity : Convert.ToString((arr[i]));
                        break;
                    case 6:
                        if (!string.IsNullOrEmpty(arr[i]))
                        {
                            queryEntity.DeliveryNum = Convert.ToString(arr[i]);
                        }
                        break;
                    default:
                        break;
                }
            }
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            queryEntity.Mode = "0";
            ArriveBus bus = new ArriveBus();
            //Hashtable list = bus.QueryDeliveryOrder(pageIndex, pageSize, queryEntity);
            string err = "OK";
            List<DeliveryEntity> awbList = bus.QueryDeliveryOrderForExport(queryEntity);
            if (awbList.Count > 0) { DeliveryList = awbList; } else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<DeliveryEntity> DeliveryList
        {
            get
            {
                if (Session["DeliveryList"] == null)
                {
                    Session["DeliveryList"] = new List<DeliveryEntity>();
                }
                return (List<DeliveryEntity>)(Session["DeliveryList"]);
            }
            set
            {
                Session["DeliveryList"] = value;
            }
        }
        /// <summary>
        /// 修改配送订单
        /// </summary>
        public void UpdateDeliveryOrder()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            List<DeliveryEntity> entDest = new List<DeliveryEntity>();
            ArriveBus bus = new ArriveBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "到达运单配送";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "U";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            try
            {
                if (GridRows.Count <= 0)
                {
                    msg.Message = "配送运单数据为空";
                    msg.Result = false;
                }
                if (msg.Result)
                {
                    int p = 0;
                    foreach (Hashtable rows in GridRows)
                    {
                        DeliveryEntity ent = new DeliveryEntity();
                        ent.DeliveryID = Convert.ToInt64(Request["DeliveryID"]);
                        ent.DeliveryNum = Convert.ToString(Request["DeliveryNum"]);
                        ent.TruckNum = Convert.ToString(Request["PTruckNum"]);
                        ent.Driver = Convert.ToString(Request["Driver"]);
                        ent.DriverCellPhone = Convert.ToString(Request["DriverCellPhone"]);
                        ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                        ent.OtherFee = string.IsNullOrEmpty(Convert.ToString(Request["OtherFee"])) ? 0 : Convert.ToDecimal(Request["OtherFee"]);
                        ent.StartTime = Convert.ToDateTime(Request["StartTime"]);
                        ent.PreArriveTime = Convert.ToDateTime(Request["StartTime"]);
                        ent.OP_ID = UserInfor.LoginName.Trim();
                        ent.CurCity = UserInfor.DepCity.Trim();
                        ent.Remark = Convert.ToString(Request["Remark"]);
                        ent.ArriveID = Convert.ToInt64(rows["ArriveID"]);
                        ent.AwbID = Convert.ToInt64(rows["AwbID"]);
                        ent.AwbNo = Convert.ToString(rows["AwbNo"]);
                        ent.Dest = UserInfor.DepCity.Trim();
                        ent.BelongSystem = UserInfor.NewLandBelongSystem;
                        ent.Mode = "0";
                        entDest.Add(ent);
                        p += Convert.ToInt32(rows["AwbPiece"]);
                    }
                    if (msg.Result)
                    {
                        entDest[0].PieceTotal = p;
                        bus.UpdateDeliveryOrder(entDest, log);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
        #region 到达运单中转
        /// <summary>
        /// 合并运单数据
        /// </summary>
        public void TransitMerge()
        {
            String json = Request["submitData"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            AwbEntity entity = new AwbEntity();
            List<AwbEntity> listEntity = new List<AwbEntity>();
            ArriveBus bus = new ArriveBus();
            string awbno = string.Empty;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            int p = 0;
            decimal w = 0, v = 0;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "到达运单中转";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "U";
            log.Status = "0";
            try
            {
                foreach (Hashtable row in rows)
                {
                    #region 修改数据
                    if (!string.IsNullOrEmpty(awbno) && !awbno.Equals(Convert.ToString(row["AwbNo"]).Trim()))
                    {
                        msg.Message = "必须是相同的运单号";
                        msg.Result = false;
                        break;
                    }
                    bool cz = bus.IsExistArriveAwb(new DeliveryEntity { ArriveID = Convert.ToInt64(row["ArriveID"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    if (!cz)
                    {
                        msg.Message = "运单不存在，请刷新后操作！";
                        msg.Result = false;
                        break;
                    }
                    p += Convert.ToInt32(row["AwbPiece"]);
                    w += Convert.ToDecimal(row["AwbWeight"]);
                    v += Convert.ToDecimal(row["AwbVolume"]);
                    awbno = Convert.ToString(row["AwbNo"]).Trim();
                    listEntity.Add(new AwbEntity { ArriveID = Convert.ToInt64(row["ArriveID"]), AwbID = Convert.ToInt64(row["AwbID"]), AwbNo = awbno, AwbPiece = Convert.ToInt32(row["AwbPiece"]), AwbWeight = Convert.ToDecimal(row["AwbWeight"]), AwbVolume = Convert.ToDecimal(row["AwbVolume"]), BelongSystem = UserInfor.NewLandBelongSystem });
                    #endregion
                }
                if (msg.Result)
                {
                    entity.AwbPiece = p;
                    entity.AwbWeight = w;
                    entity.AwbVolume = v;
                    entity.AwbNo = awbno;
                    entity.BelongSystem = UserInfor.NewLandBelongSystem;
                    bus.TransitMerge(entity, listEntity, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 到达中转分批
        /// </summary>
        public void TransitTear()
        {
            ArriveBus bus = new ArriveBus();
            List<AwbEntity> queryEntity = new List<AwbEntity>();
            int tp = 0, p = 0;
            decimal w = 0, v = 0;
            long awbid = 0; string awbno = string.Empty;
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "到达运单中转";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            foreach (Hashtable row in rows)
            {
                awbid = Convert.ToInt64(row["ArriveID"]);
                p = Convert.ToInt32(row["AwbPiece"]);
                w = Convert.ToDecimal(row["AwbWeight"]);
                v = Convert.ToDecimal(row["AwbVolume"]);
                awbno = Convert.ToString(row["AwbNo"]).Trim();
            }
            tp = Convert.ToInt32(Request["TearPiece"]);
            try
            {
                bool cz = bus.IsExistArriveAwb(new DeliveryEntity { ArriveID = awbid, BelongSystem = UserInfor.NewLandBelongSystem });
                if (!cz)
                {
                    msg.Message = "运单不存在，请刷新后操作！";
                    msg.Result = false;
                }
                if (msg.Result)
                {
                    int op = p - tp; //剩余件数
                    decimal tw = Convert.ToDecimal(w * tp / p); //分批重量
                    decimal tv = Convert.ToDecimal(v * tp / p); //分批体积
                    AwbEntity ae = new AwbEntity { ArriveID = awbid, AwbNo = awbno, AwbPiece = tp, AwbWeight = tw, AwbVolume = tv, Piece = p, Weight = w, Volume = v, BelongSystem = UserInfor.NewLandBelongSystem };
                    queryEntity.Add(ae);
                    AwbEntity oe = new AwbEntity { ArriveID = awbid, AwbNo = awbno, AwbPiece = op, AwbWeight = w - tw, AwbVolume = v - tv, Piece = p, Weight = w, Volume = v, BelongSystem = UserInfor.NewLandBelongSystem };
                    queryEntity.Add(oe);
                    bus.TransitTear(queryEntity, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 保存中转运单信息
        /// </summary>
        public void AddTransit()
        {
            string json = Request["data"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            List<TransitEntity> entDest = new List<TransitEntity>();
            ArriveBus bus = new ArriveBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "到达运单中转";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            msg.Message = "";
            try
            {
                if (DateTime.Compare(Convert.ToDateTime(Request["StartTime"]), DateTime.Now) > 0)
                {
                    msg.Message = "发车时间不能晚于当前时间";
                    msg.Result = false;
                }
                if (msg.Result)
                {
                    int p = 0;
                    foreach (Hashtable rows in GridRows)
                    {
                        TransitEntity ent = new TransitEntity();

                        ent.CarrierID = Convert.ToInt64(Request["CarrierShortName"]);
                        ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                        ent.PrepayFee = string.IsNullOrEmpty(Convert.ToString(Request["PrepayFee"])) ? 0 : Convert.ToDecimal(Request["PrepayFee"]);
                        ent.ArriveFee = string.IsNullOrEmpty(Convert.ToString(Request["ArriveFee"])) ? 0 : Convert.ToDecimal(Request["ArriveFee"]);
                        ent.OtherFee = string.IsNullOrEmpty(Convert.ToString(Request["OtherFee"])) ? 0 : Convert.ToDecimal(Request["OtherFee"]);
                        ent.DeliveryFee = string.IsNullOrEmpty(Convert.ToString(Request["DeliveryFee"])) ? 0 : Convert.ToDecimal(Request["DeliveryFee"]);
                        ent.HandFee = string.IsNullOrEmpty(Convert.ToString(Request["HandFee"])) ? 0 : Convert.ToDecimal(Request["HandFee"]);
                        ent.SendFee = string.IsNullOrEmpty(Convert.ToString(Request["SendFee"])) ? 0 : Convert.ToDecimal(Request["SendFee"]);
                        ent.CollectMoney = string.IsNullOrEmpty(Convert.ToString(Request["CollectMoney"])) ? 0 : Convert.ToDecimal(Request["CollectMoney"]);
                        ent.Piece = string.IsNullOrEmpty(Convert.ToString(Request["Piece"])) ? 0 : Convert.ToInt32(Request["Piece"]);
                        ent.Weight = string.IsNullOrEmpty(Convert.ToString(Request["Weight"])) ? 0 : Convert.ToDecimal(Request["Weight"]);
                        ent.Volume = string.IsNullOrEmpty(Convert.ToString(Request["Volume"])) ? 0 : Convert.ToDecimal(Request["Volume"]);
                        ent.PiecePrice = string.IsNullOrEmpty(Convert.ToString(Request["PiecePrice"])) ? 0 : Convert.ToDecimal(Request["PiecePrice"]);
                        ent.WeightPrice = string.IsNullOrEmpty(Convert.ToString(Request["WeightPrice"])) ? 0 : Convert.ToDecimal(Request["WeightPrice"]);
                        ent.VolumePrice = string.IsNullOrEmpty(Convert.ToString(Request["VolumePrice"])) ? 0 : Convert.ToDecimal(Request["VolumePrice"]);
                        ent.StartTime = Convert.ToDateTime(Request["StartTime"]);
                        ent.PreArriveTime = Convert.ToDateTime(Request["StartTime"]);
                        ent.Remark = Convert.ToString(Request["Remark"]);
                        ent.OP_ID = UserInfor.LoginName.Trim();
                        ent.AssistNum = Convert.ToString(Request["AssistNum"]);
                        ent.CheckOutType = Convert.ToString(Request["CheckOutType"]);
                        ent.ArriveID = Convert.ToInt64(rows["ArriveID"]);
                        ent.AwbID = Convert.ToInt64(rows["AwbID"]);
                        ent.AwbNo = Convert.ToString(rows["AwbNo"]);
                        ent.CurCity = UserInfor.DepCity.Trim();
                        ent.Dest = Convert.ToString(rows["Dest"]).Trim();
                        ent.Mode = "1";
                        ent.BelongSystem = UserInfor.NewLandBelongSystem;
                        if (bus.IsDeliveryAwb(new DeliveryEntity { AwbNo = ent.AwbNo, ArriveID = ent.ArriveID, Mode = "1", BelongSystem = UserInfor.NewLandBelongSystem }))
                        {
                            msg.Message = "运单号:" + ent.AwbNo + "已经中转，请勿重复中转";
                            msg.Result = false;
                            break;
                        }
                        entDest.Add(ent);
                        p += Convert.ToInt32(rows["AwbPiece"]);
                    }
                    if (msg.Result)
                    {
                        entDest[0].PieceTotal = p;
                        bus.AddTransit(entDest, log);
                    }
                }
                if (msg.Result)
                {
                    HlyCommon hly = new HlyCommon();
                    foreach (Hashtable row in GridRows)
                    {
                        if (Convert.ToString(row["HLY"]).Equals("0")) { continue; }//如果不是好来运单就不需要发送跟踪状态接口
                       // hly.SaveAwbStatus(new HlyEntity { Hawbno = Convert.ToString(row["HAwbNo"]), City = UserInfor.DepCity, Cuser = UserInfor.UserName, Cdatetime = DateTime.Now, Other = Convert.ToString(row["Remark"]), SplitCB = ",", Xawbno = Convert.ToString(row["AwbNo"]), State = "9" });
                    }
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
        #region 中转运单管理
        /// <summary>
        /// 中转运单管理
        /// </summary>
        public void QueryTransitOrder()
        {
            TransitEntity queryEntity = new TransitEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.AssistNum = Convert.ToString(Request["AssistNum"]);
            queryEntity.CarrierShortName = Convert.ToString(Request["CarrierShortName"]);
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.CurCity = string.IsNullOrEmpty(Convert.ToString(Request["Dest"])) ? UserInfor.DepCity : Convert.ToString(Request["Dest"]);
            if (!Convert.ToString(Request["FinanceFirstCheck"]).Equals("-1")) { queryEntity.FinanceFirstCheck = Convert.ToString(Request["FinanceFirstCheck"]); };
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            queryEntity.Mode = "1";
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryTransitOrder(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据中转单号查询所有运单
        /// </summary>
        public void QueryAwbInfoByTransitID()
        {
            DeliveryEntity queryEntity = new DeliveryEntity();

            //查询条件
            string key = Request["id"];

            if (string.IsNullOrEmpty(key))
            {
                Response.Clear();
                Response.Write(JSON.Encode(queryEntity));
                return;
            }

            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            queryEntity.DeliveryID = Convert.ToInt64(key.Trim());
            queryEntity.Mode = "1";
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);

            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryAwbInfoByDeliveryID(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 审核中转单
        /// </summary>
        public void CheckTransit()
        {
            String idStr = Request["data"];
            if (String.IsNullOrEmpty(idStr)) return;
            List<TransitEntity> list = new List<TransitEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "中转运单管理";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "U";
            log.Status = "0";
            log.Memo = "审核成功";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            foreach (Hashtable row in rows)
            {
                if (Convert.ToString(row["FinanceFirstCheck"]).Trim().Equals("1")) { continue; }
                list.Add(new TransitEntity
                {
                    TransitID = Convert.ToInt64(row["TransitID"]),
                    FinanceFirstCheck = "1",
                    FirstCheckName = UserInfor.UserName.Trim(),
                    BelongSystem = UserInfor.NewLandBelongSystem,
                    FirstCheckDate = DateTime.Now
                });
            }
            try
            {
                if (msg.Result)
                {
                    ArriveBus bus = new ArriveBus();
                    bus.CheckTransit(list, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 中转单未审
        /// </summary>
        public void UnLockTransit()
        {
            String idStr = Request["data"];
            if (String.IsNullOrEmpty(idStr)) return;
            List<TransitEntity> list = new List<TransitEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "中转运单管理";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "U";
            log.Status = "0";
            log.Memo = "未审成功";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            foreach (Hashtable row in rows)
            {
                if (Convert.ToString(row["FinanceFirstCheck"]).Trim().Equals("0")) { continue; }
                list.Add(new TransitEntity
                {
                    TransitID = Convert.ToInt64(row["TransitID"]),
                    FinanceFirstCheck = "0",
                    FirstCheckName = UserInfor.UserName.Trim(),
                    BelongSystem = UserInfor.NewLandBelongSystem,
                    FirstCheckDate = DateTime.Now
                });
            }
            try
            {
                if (msg.Result)
                {
                    ArriveBus bus = new ArriveBus();
                    bus.CheckTransit(list, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 删除中转单
        /// </summary>
        public void DelTransit()
        {
            String idStr = Request["data"];
            if (String.IsNullOrEmpty(idStr)) return;
            List<TransitEntity> list = new List<TransitEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "中转运单管理";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "D";
            log.Status = "0";
            ArriveBus bus = new ArriveBus();
            foreach (Hashtable row in rows)
            {
                if (Convert.ToString(row["FinanceFirstCheck"]).Trim().Equals("1"))
                {
                    msg.Message = "审核状态的中转单不能删除";
                    msg.Result = false;
                    break;
                }
                if (bus.IsAllDeliveryByDeliverID(Convert.ToInt64(row["DeliveryID"]), "1", "9", UserInfor.NewLandBelongSystem))
                {
                    msg.Message = "配送单里的运单不是配送状态，不能删除";
                    msg.Result = false;
                    break;
                }
                list.Add(new TransitEntity
                {
                    TransitID = Convert.ToInt64(row["TransitID"]),
                    BelongSystem = UserInfor.NewLandBelongSystem,
                    OP_ID = UserInfor.LoginName
                });
            }
            try
            {
                if (msg.Result)
                {
                    bus.DelTransit(list, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 中转运单导出查询
        /// </summary>
        public void QueryTransitOrderForExport()
        {
            TransitEntity queryEntity = new TransitEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.CarrierShortName = arr[i].Trim(); } break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.AwbNo = arr[i].Trim(); } break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.StartDate = Convert.ToDateTime(arr[i].Trim()); } break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.EndDate = Convert.ToDateTime(arr[i].Trim()); } break;
                    case 4:
                        queryEntity.CurCity = string.IsNullOrEmpty(Convert.ToString(arr[i])) ? UserInfor.DepCity : Convert.ToString(arr[i]);
                        break;
                    case 5:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.AssistNum = arr[i].Trim(); } break;
                    case 6:
                        if (!arr[i].Equals("-1"))
                        {
                            queryEntity.FinanceFirstCheck = Convert.ToString(arr[i]);
                        }
                        break;
                    default: break;
                }
            }
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            queryEntity.Mode = "1";
            ArriveBus bus = new ArriveBus();
            string err = "OK";
            List<TransitEntity> awbList = bus.QueryTransitOrderForExport(queryEntity);
            if (awbList.Count > 0) { TransitList = awbList; } else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 中转导出实体
        /// </summary>
        public List<TransitEntity> TransitList
        {
            get
            {
                if (Session["TransitList"] == null)
                {
                    Session["TransitList"] = new List<TransitEntity>();
                }
                return (List<TransitEntity>)(Session["TransitList"]);
            }
            set
            {
                Session["TransitList"] = value;
            }
        }
        /// <summary>
        /// 修改中转订单
        /// </summary>
        public void UpdateTransitOrder()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            List<TransitEntity> entDest = new List<TransitEntity>();
            ArriveBus bus = new ArriveBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "到达管理";
            log.NvgPage = "到达运单中转";
            log.UserID = UserInfor.LoginName.Trim(); 
            log.Operate = "U";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            try
            {
                int p = 0;
                foreach (Hashtable rows in GridRows)
                {
                    TransitEntity ent = new TransitEntity();
                    ent.TransitID = Convert.ToInt64(Request["TransitID"]);
                    ent.CarrierID = Convert.ToInt64(Request["CarrierID"]);
                    ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                    ent.OtherFee = string.IsNullOrEmpty(Convert.ToString(Request["OtherFee"])) ? 0 : Convert.ToDecimal(Request["OtherFee"]);
                    ent.PrepayFee = string.IsNullOrEmpty(Convert.ToString(Request["PrepayFee"])) ? 0 : Convert.ToDecimal(Request["PrepayFee"]);
                    ent.ArriveFee = string.IsNullOrEmpty(Convert.ToString(Request["ArriveFee"])) ? 0 : Convert.ToDecimal(Request["ArriveFee"]);
                    ent.DeliveryFee = string.IsNullOrEmpty(Convert.ToString(Request["DeliveryFee"])) ? 0 : Convert.ToDecimal(Request["DeliveryFee"]);
                    ent.HandFee = string.IsNullOrEmpty(Convert.ToString(Request["HandFee"])) ? 0 : Convert.ToDecimal(Request["HandFee"]);
                    ent.SendFee = string.IsNullOrEmpty(Convert.ToString(Request["SendFee"])) ? 0 : Convert.ToDecimal(Request["SendFee"]);
                    ent.CollectMoney = string.IsNullOrEmpty(Convert.ToString(Request["CollectMoney"])) ? 0 : Convert.ToDecimal(Request["CollectMoney"]);
                    ent.Piece = string.IsNullOrEmpty(Convert.ToString(Request["Piece"])) ? 0 : Convert.ToInt32(Request["Piece"]);
                    ent.Weight = string.IsNullOrEmpty(Convert.ToString(Request["Weight"])) ? 0 : Convert.ToDecimal(Request["Weight"]);
                    ent.Volume = string.IsNullOrEmpty(Convert.ToString(Request["Volume"])) ? 0 : Convert.ToDecimal(Request["Volume"]);
                    ent.PiecePrice = string.IsNullOrEmpty(Convert.ToString(Request["PiecePrice"])) ? 0 : Convert.ToDecimal(Request["PiecePrice"]);
                    ent.WeightPrice = string.IsNullOrEmpty(Convert.ToString(Request["WeightPrice"])) ? 0 : Convert.ToDecimal(Request["WeightPrice"]);
                    ent.VolumePrice = string.IsNullOrEmpty(Convert.ToString(Request["VolumePrice"])) ? 0 : Convert.ToDecimal(Request["VolumePrice"]);
                    ent.StartTime = Convert.ToDateTime(Request["StartTime"]);
                    ent.PreArriveTime = Convert.ToDateTime(Request["StartTime"]);
                    ent.Remark = Convert.ToString(Request["Remark"]);
                    ent.AssistNum = Convert.ToString(Request["AssistNum"]);
                    ent.CheckOutType = Convert.ToString(Request["CheckOutType"]);
                    ent.OP_ID = UserInfor.LoginName.Trim();
                    ent.BelongSystem = UserInfor.NewLandBelongSystem;
                    ent.ArriveID = Convert.ToInt64(rows["ArriveID"]);
                    ent.AwbID = Convert.ToInt64(rows["AwbID"]);
                    ent.AwbNo = Convert.ToString(rows["AwbNo"]);
                    ent.Dest = UserInfor.DepCity.Trim();
                    ent.CurCity = UserInfor.DepCity.Trim();
                    ent.Mode = "1";
                    entDest.Add(ent);
                    p += Convert.ToInt32(rows["AwbPiece"]);
                }
                if (msg.Result)
                {
                    entDest[0].PieceTotal = p;
                    bus.UpdateTransitOrder(entDest, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
        #region 中转状态跟踪

        /// <summary>
        /// 查询所有中转状态的运单
        /// </summary>
        public void QueryTransitAwbStatusTrack()
        {
            TransitEntity queryEntity = new TransitEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.ShipperUnit = Convert.ToString(Request["ShipperUnit"]);
            queryEntity.AssistNum = Convert.ToString(Request["AssistNum"]);
            queryEntity.CarrierShortName = Convert.ToString(Request["CarrierShortName"]);
            queryEntity.Dest = string.IsNullOrEmpty(Convert.ToString(Request["Dest"])) ? UserInfor.DepCity : Convert.ToString(Request["Dest"]);
            if (!Convert.ToString(Request["AwbStatus"]).Equals("-1")) { queryEntity.FinanceFirstCheck = Convert.ToString(Request["AwbStatus"]); };
            //分页
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryTransitAwbStatusTrack(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询运单的跟踪状态
        /// </summary>
        public void QueryAwbStatusTrack()
        {
            string awbno = Request["id"];
            string awbid = Request["awbid"];
            AwbStatus queryEntity = new AwbStatus();
            queryEntity.AwbID = Convert.ToInt64(awbid);
            queryEntity.AwbNo = awbno;
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            List<AwbStatus> result = bus.QueryAwbTrackInfo(queryEntity);
            //DataTable result = bus.QueryTruckStatusTrack(queryEntity);
            string res = "跟踪记录，目前状态：\r\n";
            if (result.Count > 0)
            {
                foreach (var it in result)
                {
                    string yj = "预计";
                    if (Convert.ToString(it.TruckFlag).Trim().Equals("6"))
                    {
                        yj = "实际";
                    }
                    double db = 0;
                    if (!string.IsNullOrEmpty(Convert.ToString(it.LastHour)))
                    {
                        db = Convert.ToDouble(it.LastHour);
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(it.ArriveTime)))
                    {
                        continue;
                    }
                    res += Convert.ToString(it.UserName).Trim() + "[" + Convert.ToDateTime(it.OP_DATE).ToString("yyyy-MM-dd HH:mm:ss") + "]：\r\n" + Convert.ToString(it.DetailInfo).Trim() + "，目前在" + Convert.ToString(it.CurrentLocation).Trim() + "，" + yj + "[" + Convert.ToDateTime(it.ArriveTime).ToString("yyyy-MM-dd HH:mm:ss") + "]到达客户\r\n";
                }
            }
            String json = JSON.Encode(res);
            Response.Write(json);
        }
        /// <summary>
        /// 保存中转运单跟踪状态
        /// </summary>
        public void SaveAwbTruckStatus()
        {
            TruckStatusTrackEntity queryEntity = new TruckStatusTrackEntity();
            queryEntity.ArriveID = Convert.ToInt64(Request["ArriveID"]);
            queryEntity.TruckFlag = Convert.ToString(Request["AwbStatus"]);
            queryEntity.CurrentLocation = Convert.ToString(Request["CurrentLocation"]);
            queryEntity.DetailInfo = Convert.ToString(Request["DetailInfo"]);
            queryEntity.TruckNum = Convert.ToString(Request["AwbID"]);
            queryEntity.ContractNum = Convert.ToString(Request["AwbNo"]);
            queryEntity.ArriveTime = Convert.ToDateTime(Request["ArriveTime"]);
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            TimeSpan ts = queryEntity.ArriveTime - DateTime.Now;
            queryEntity.LastHour = ts.Hours;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            ArriveBus bus = new ArriveBus();
            try
            {
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "到达管理";
                log.NvgPage = "中转状态跟踪";
                log.UserID = UserInfor.LoginName.Trim();
                log.Operate = "U";
                queryEntity.OP_ID = UserInfor.LoginName;
                if (msg.Result)
                {
                    bus.SaveAwbTruckStatus(queryEntity, log);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            if (msg.Result)
            {
                try
                {
                    if (msg.Result)
                    {
                        if (queryEntity.TruckFlag.Equals("6"))
                        {
                            List<AwbEntity> result = bus.QueryAwb(new AwbEntity { AwbNo = queryEntity.ContractNum, BelongSystem = UserInfor.NewLandBelongSystem });
                            HlyCommon hly = new HlyCommon();
                            //送达客户表示签收
                            if (result.Count > 0)
                            {
                                //hly.SaveAwbStatus(new HlyEntity { Hawbno = result[0].HAwbNo, City = queryEntity.CurrentLocation, Cuser = UserInfor.UserName, Cdatetime = DateTime.Now, Other = result[0].Remark, SplitCB = ",", Xawbno = result[0].AwbNo, State = "7" });
                            }
                        }
                    }
                }
                catch (ApplicationException ex)
                {
                    msg.Result = true;
                }
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
        #region 客户货物查询
        /// <summary>
        /// 查询运单状态
        /// </summary>
        public void QueryClientAwbStatusTrack()
        {
            AwbEntity queryEntity = new AwbEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["AwbNo"])))
            {
                queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            }
            else
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
                if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
                if (!string.IsNullOrEmpty(Convert.ToString(Request["ADate"]))) { queryEntity.CStartDate = Convert.ToDateTime(Request["ADate"]); }
                if (!string.IsNullOrEmpty(Convert.ToString(Request["BDate"]))) { queryEntity.CEndDate = Convert.ToDateTime(Request["BDate"]); }
                if (!string.IsNullOrEmpty(Convert.ToString(Request["Piece"]))) { queryEntity.Piece = Convert.ToInt32(Request["Piece"]); }
                queryEntity.ShipperUnit = Convert.ToString(Request["ShipperUnit"]);
                queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
                queryEntity.Goods = Convert.ToString(Request["Goods"]);
                queryEntity.Dep = Convert.ToString(Request["Dep"]);
                queryEntity.Dest = Convert.ToString(Request["Dest"]);
                queryEntity.MidDest = Convert.ToString(Request["MidDest"]);
                queryEntity.Transit = Convert.ToString(Request["Transit"]);
                queryEntity.HAwbNo = Convert.ToString(Request["HAwbNo"]);
                if (Request["DeliveryType"] != "-1") { queryEntity.DeliveryType = Convert.ToString(Request["DeliveryType"]); }
                if (Request["TruckFlag"] != "-1") { queryEntity.DelFlag = Convert.ToString(Request["TruckFlag"]); }
                if (Request["TransKind"] != "-1") { queryEntity.TransKind = Convert.ToString(Request["TransKind"]); }
                if (Request["CheckStatus"] != "-1") { queryEntity.CheckStatus = Convert.ToString(Request["CheckStatus"]); }
            }
            //分页
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            ArriveBus bus = new ArriveBus();
            Hashtable list = bus.QueryClientAwbStatusTrack(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询所有的城市数据
        /// </summary>
        public void QueryAllCity()
        {
            CityEntity entity = new CityEntity();
            entity.CityName = Request["City"];
            entity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            List<CityEntity> list = bus.QueryAllCity(entity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            //string json = "[";
            //foreach (var it in UserInfor.DepCity)
            //{
            //    json += "{\"CityName\":\"" + it.CityName.Trim() + "\",\"CityName\":\"" + it.CityName.Trim() + "\"},";

            //}
            //json = json.Substring(0, json.Length - 1);
            //json += "]";
            //String jsons = JSON.Encode(json);
            //Response.Clear();
            //Response.Write(jsons);
        }
        /// <summary>
        /// 查询所有用户
        /// </summary>
        public void QueryALLUser()
        {
            ArriveBus bus = new ArriveBus();
            List<NewaySystemUserEntity> list = bus.QueryALLUser(UserInfor.NewLandBelongSystem);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 按CityCode查询单位数据
        /// </summary>
        public void GetUnitByCityCode()
        {
            string cityCode = Convert.ToString(Request["id"]);
            ArriveBus bus = new ArriveBus();
            SystemUnitEntity result = bus.GetUnitByCityCode(cityCode, UserInfor.NewLandBelongSystem);
            String json = JSON.Encode(result);
            Response.Write(json);
        }
        /// <summary>
        /// 保存运单备注信息
        /// </summary>
        /// <param name="goods"></param>
        public void AddAwbRemarkInfo()
        {
            AwbRemarkEntity queryEntity = new AwbRemarkEntity();
            queryEntity.AwbNo = Convert.ToString(Request["AwbNo"]);
            queryEntity.Remark = Convert.ToString(Request["DetailInfo"]);
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            try
            {
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "客户管理";
                log.NvgPage = "客户货物跟踪";
                log.UserID = UserInfor.LoginName.Trim();
                log.Operate = "U";
                queryEntity.OP_ID = UserInfor.LoginName;
                queryEntity.OP_NAME = UserInfor.UserName;
                ArriveBus bus = new ArriveBus();
                bus.AddAwbRemarkInfo(queryEntity, log);
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 根据运单号查询运单的备注信息
        /// </summary>
        public void QueryAwbNoRemarkInfo()
        {
            string awbno = Request["id"];
            AwbRemarkEntity queryEntity = new AwbRemarkEntity();
            queryEntity.AwbNo = awbno;
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            List<AwbRemarkEntity> result = bus.QueryAwbNoRemarkInfo(queryEntity);
            string res = string.Empty;
            if (result.Count > 0)
            {
                res += "<table class='commTblStyle_8' border='0' width='100%' style='border-spacing:0;border-collapse:collapse;font-size:14px;table-layout:fixed;'><tbody><tr><th>备注内容</th><th style='width:50px;'>操作人</th><th style='width:130px;'>操作时间</th></tr>";
                foreach (var it in result)
                {
                    res += "<tr><td style='word-wrap:break-word;'>" + it.Remark + "</td><td>" + it.OP_NAME + "</td><td>" + it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss") + "</td></tr>";
                }
                res += "</tbody><table>";
            }
            String json = JSON.Encode(res);
            Response.Write(json);
        }
        /// <summary>
        /// 通过运单号查询运单信息
        /// </summary>
        public void GetAwbInfoByAwbNo()
        {
            string AwbNo = Request["id"];
            AwbEntity ae = new AwbEntity();
            if (!string.IsNullOrEmpty(AwbNo))
            {
                ae.AwbNo = AwbNo.ToUpper().Trim();
            }
            ae.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            List<AwbEntity> result = bus.QueryAwb(ae);
            if (result.Count <= 0)
            {
                Response.Write("");
                return;
            }
            String json = JSON.Encode(result[0]);
            Response.Write(json);
        }
        /// <summary>
        /// 查询运单状态跟踪信息
        /// </summary>
        public void QueryAwbTrackInfo()
        {
            string awbid = Request["id"];
            string aid = Request["aid"];
            string hawbno = Request["hawbno"];
            AwbStatus queryEntity = new AwbStatus();
            queryEntity.AwbID = 0;// Convert.ToInt64(aid);
            queryEntity.AwbNo = awbid;
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            List<AwbStatus> result = bus.QueryAwbTrackInfo(queryEntity);
            string res = string.Empty;
            if (result.Count > 0)
            {
                bool isSign = false;

                res = "<div style='font-size:14px;font-weight:bold'>运单号：" + result[0].AwbNo + "  的跟踪记录";
                if (!string.IsNullOrEmpty(hawbno))
                {
                    res += "（" + hawbno.Trim() + "）";
                }
                res += "</div>";
                res += "<table class='commTblStyle_8' border='0' width='100%' style='border-spacing:0;border-collapse:collapse;font-size:14px;'><tbody><tr><th>操作时间</th><th>当前地点</th><th>货物状态(到达时间)</th><th>操作人</th></tr>";
                foreach (var it in result)
                {
                    List<AwbFilesEntity> files = bus.QueryAwbFilesByAwbNo(new AwbEntity { AwbNo = it.AwbNo, AwbID = it.AwbID, BelongSystem = it.BelongSystem });
                    if (it.TruckFlag.Trim().Equals("7"))
                    {
                        isSign = true;
                    }
                    string tf = string.Empty;
                    string arriveTime = string.Empty;
                    switch (it.TruckFlag.Trim())
                    {
                        case "0":
                            tf = "<span style='color:red;font-weight:bold;'>在站</span>";
                            if (!isSign)
                            {
                                if (files.Count > 0)
                                {
                                    foreach (var file in files)
                                    {
                                        if (!string.IsNullOrEmpty(file.TbFilePath))
                                        {
                                            //tf += ",<a href=../" + it.TbFilePath + " target=\"_blank\">签收照片</a>";
                                            tf += "<img onclick=download(\"" + file.FileName + "\") style='width:30px; height:20px;margin-left:3px;' src='http://120.236.158.136:9101/" + file.FileName + "' title='点击查看图片'/>";
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(file.FilePath))
                                            {
                                                //tf += ",<a href=../" + it.FilePath + " target=\"_blank\">签收照片</a>";
                                                tf += "<img onclick=download(\"" + file.FileName + "\") style='width:30px; height:20px;margin-left:3px;' src='http://120.236.158.136:9101/" + file.FileName + "' title='点击查看图片'/>";
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case "1":
                            tf = "<span style='color:red;font-weight:bold;'>出发</span>";
                            break;
                        case "2":
                            tf = "<span style='color:red;font-weight:bold;'>在途</span>";
                            break;
                        case "3":
                            tf = "<span style='color:red;font-weight:bold;'>到达：</span>";
                            arriveTime = "(" + Convert.ToDateTime(it.ArriveTime).ToString("yyyy-MM-dd HH:mm:ss") + ")";
                            break;
                        case "4":
                            tf = "<span style='color:red;font-weight:bold;'>回单确认，结束</span>";
                            break;
                        case "5":
                            tf = "<span style='color:red;font-weight:bold;'>关注</span>";
                            break;
                        case "6":
                            tf = "<span style='color:red;font-weight:bold;'>送达</span>";
                            break;
                        case "7":
                            tf = "<span style='color:red;font-weight:bold;'>客户签收</span>";
                            if (it.ArriveTime != null && !Convert.ToDateTime(it.ArriveTime).ToString("yyyy-MM-dd").Equals("1900-01-01") && !Convert.ToDateTime(it.ArriveTime).ToString("yyyy-MM-dd").Equals("0001-01-01") && !Convert.ToDateTime(it.ArriveTime).ToString("yyyy-MM-dd").Equals("1901-01-01"))
                            {
                                it.OP_DATE = Convert.ToDateTime(it.ArriveTime);
                            }
                            if (!string.IsNullOrEmpty(it.Signer))
                            {
                                tf += "，签收人：" + it.Signer;
                            }
                            if (files.Count > 0)
                            {
                                foreach (var file in files)
                                {
                                    if (!string.IsNullOrEmpty(file.TbFilePath))
                                    {
                                        //tf += ",<a href=../" + it.TbFilePath + " target=\"_blank\">签收照片</a>";
                                        tf += "<img onclick=download(\"" + file.FileName + "\") style='width:30px; height:20px;margin-left:3px;' src='http://120.236.158.136:9101/" + file.FileName + "' title='点击查看图片'/>";
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(file.FilePath))
                                        {
                                            //tf += ",<a href=../" + it.FilePath + " target=\"_blank\">签收照片</a>";
                                            tf += "<img onclick=download(\"" + file.FileName + "\") style='width:30px; height:20px;margin-left:3px;' src='http://120.236.158.136:9101/" + file.FileName + "' title='点击查看图片'/>";
                                        }
                                    }
                                }
                            }
                            break;
                        case "8":
                            tf = "<span style='color:red;font-weight:bold;'>配送</span>";
                            break;
                        case "9":
                            tf = "<span style='color:red;font-weight:bold;'>中转</span>";
                            break;
                        case "10":
                            tf = "<span style='color:red;font-weight:bold;'>回单发送</span>";
                            break;
                        case "11":
                            tf = "<span style='color:red;font-weight:bold;'>移库</span>";
                            break;
                        case "12":
                            tf = "<span style='color:red;font-weight:bold;'>到达移库</span>";
                            break;
                        case "13":
                            tf = "<span style='color:red;font-weight:bold;'>异常</span>";
                            break;
                        default:
                            break;
                    }
                    res += "<tr><td>" + it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss") + "</td><td>" + it.CurrentLocation + "</td><td>" + tf + it.DetailInfo + arriveTime + "</td><td>" + it.UserName + "</td></tr>";
                }
                res += "</tbody><table>";
            }
            String json = JSON.Encode(res);
            Response.Write(json);
        }
        /// <summary>
        /// 运单路线图
        /// </summary>
        public void QueryAwbRoad()
        {
            string awbid = Request["id"];
            string aid = Request["aid"];
            AwbRoadEntity queryEntity = new AwbRoadEntity();
            queryEntity.AwbID = Convert.ToInt64(aid);
            queryEntity.AwbNo = awbid;
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            List<AwbRoadEntity> result = bus.QueryAwbRoad(queryEntity);
            StringBuilder res = new StringBuilder();
            if (result.Count > 0)
            {
                res.Append("<div class=\"container\"><div class=\"main\"><ul class=\"cbp_tmtimeline\">");
                foreach (var it in result)
                {
                    //录单
                    if (!it.CreateDate.ToString("yyyy-MM-dd").Equals("0001-01-01"))
                    {
                        res.Append("<li><time class=\"cbp_tmtime\" datetime=\"" + it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"><span>" + it.CreateDate.ToString("yyyy-MM-dd") + "</span> <span>" + it.CreateDate.ToString("HH:mm:ss") + "</span></time><div class=\"cbp_tmicon cbp_tmicon-phone\"></div><div class=\"cbp_tmlabel\"><h2>制&nbsp;&nbsp;单</h2><p>录单人：" + it.CreateAwb + "，托运人：" + it.ShipperName + "，托运单位：" + it.ShipperUnit + "，收货人：" + it.AcceptPeople + "，收货单位：" + it.AcceptUnit + "，件数：" + it.Piece.ToString() + "，重量：" + it.Weight.ToString() + "，体积：" + it.Volume.ToString() + "，出发站：" + it.Dep + "，到达站：" + it.Dest + "，中转站：" + it.Transit + "</p></div></li>");
                    }
                    //配载
                    if (!it.CreateTime.ToString("yyyy-MM-dd").Equals("0001-01-01"))
                    {
                        TimeSpan ts = it.CreateTime - it.CreateDate;
                        double wt = 0;
                        if (ts.TotalMinutes / 4 > 200)
                        {
                            wt = 200;
                        }
                        else
                        {
                            wt = ts.TotalMinutes / 4;
                        }
                        decimal x = 0M;
                        if (decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2) > 1)
                        {
                            x = decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2);
                        }
                        res.Append("<li><time class=\"cbp_tmtime\" datetime=\"" + it.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") + "\"><span>" + it.CreateTime.ToString("yyyy-MM-dd") + "</span> <span>" + it.CreateTime.ToString("HH:mm:ss") + "</span></time><div class=\"cbp_tmicon cbp_tmicon-phone\"></div><div class=\"cbp_tmlabel\"><h2>配&nbsp;&nbsp;载<div class=\"Bar\" style=\"width: " + wt.ToString() + "px;\"><div><span>" + x.ToString() + "</span></div></div></h2><p>配载合同号：" + it.ContractNum + "，车牌号：" + it.TruckNum + "，司机姓名：" + it.Driver + "，司机手机号码：" + it.DriverCellPhone + "</p></div></li>");
                    }
                    //出发
                    if (!it.StartTime.ToString("yyyy-MM-dd").Equals("0001-01-01"))
                    {
                        TimeSpan ts = it.StartTime - it.CreateTime;
                        double wt = 0;
                        if (ts.TotalMinutes / 4 > 200)
                        {
                            wt = 200;
                        }
                        else
                        {
                            wt = ts.TotalMinutes / 4;
                        }
                        decimal x = 0M;
                        if (decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2) > 1)
                        {
                            x = decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2);
                        }
                        res.Append("<li><time class=\"cbp_tmtime\" datetime=\"" + it.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\"><span>" + it.StartTime.ToString("yyyy-MM-dd") + "</span> <span>" + it.StartTime.ToString("HH:mm:ss") + "</span></time><div class=\"cbp_tmicon cbp_tmicon-phone\"></div><div class=\"cbp_tmlabel\"><h2>出&nbsp;&nbsp;发 <div class=\"Bar\" style=\"width: " + wt.ToString() + "px;\"><div><span>" + x.ToString() + "</span></div></div></h2><p>从" + it.Dep + "出发</p></div></li>");
                    }
                    //到达
                    if (!it.ActArriveTime.ToString("yyyy-MM-dd").Equals("0001-01-01"))
                    {
                        TimeSpan ts = it.ActArriveTime - it.StartTime;
                        double wt = 0;
                        if (ts.TotalMinutes / 4 > 200)
                        {
                            wt = 200;
                        }
                        else
                        {
                            wt = ts.TotalMinutes / 4;
                        }
                        decimal x = 0M;
                        if (decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2) > 1)
                        {
                            x = decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2);
                        }
                        res.Append("<li><time class=\"cbp_tmtime\" datetime=\"" + it.ActArriveTime.ToString("yyyy-MM-dd HH:mm:ss") + "\"><span>" + it.ActArriveTime.ToString("yyyy-MM-dd") + "</span> <span>" + it.ActArriveTime.ToString("HH:mm:ss") + "</span></time><div class=\"cbp_tmicon cbp_tmicon-phone\"></div><div class=\"cbp_tmlabel\"><h2>到&nbsp;&nbsp;达 <div class=\"Bar\" style=\"width: " + wt.ToString() + "px;\"><div><span>" + x.ToString() + "</span></div></div></h2><p>到达" + it.Dest + "</p></div></li>");
                    }
                    //签收回单
                    if (it.ReturnStatus.Equals("Y"))
                    {
                        if (!it.SignTime.ToString("yyyy-MM-dd").Equals("0001-01-01"))
                        {
                            TimeSpan ts = it.SignTime - it.ActArriveTime;
                            double wt = 0;
                            if (ts.TotalMinutes / 4 > 200)
                            {
                                wt = 200;
                            }
                            else
                            {
                                wt = ts.TotalMinutes / 4;
                            }
                            decimal x = 0M;
                            if (decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2) > 1)
                            {
                                x = decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2);
                            }
                            res.Append("<li><time class=\"cbp_tmtime\" datetime=\"" + it.SignTime.ToString("yyyy-MM-dd HH:mm:ss") + "\"><span>" + it.SignTime.ToString("yyyy-MM-dd") + "</span> <span>" + it.SignTime.ToString("HH:mm:ss") + "</span></time><div class=\"cbp_tmicon cbp_tmicon-phone\"></div><div class=\"cbp_tmlabel\"><h2>回&nbsp;&nbsp;单&nbsp;&nbsp;签&nbsp;&nbsp;收 <div class=\"Bar\" style=\"width: " + wt.ToString() + "px;\"><div><span>" + x.ToString() + "</span></div></div></h2><p>回单信息：" + it.ReturnInfo + "，签收人：" + it.Signer + "</p></div></li>");
                        }
                    }
                    //发送回单
                    if (it.SendReturnAwbStatus.Equals("Y"))
                    {
                        if (!it.SendReturnAwbDate.ToString("yyyy-MM-dd").Equals("0001-01-01"))
                        {
                            TimeSpan ts = it.SendReturnAwbDate - it.SignTime;
                            double wt = 0;
                            if (ts.TotalMinutes / 4 > 200)
                            {
                                wt = 200;
                            }
                            else
                            {
                                wt = ts.TotalMinutes / 4;
                            }
                            decimal x = 0M;
                            if (decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2) > 1)
                            {
                                x = decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2);
                            }
                            res.Append("<li><time class=\"cbp_tmtime\" datetime=\"" + it.SendReturnAwbDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"><span>" + it.SendReturnAwbDate.ToString("yyyy-MM-dd") + "</span> <span>" + it.SendReturnAwbDate.ToString("HH:mm:ss") + "</span></time><div class=\"cbp_tmicon cbp_tmicon-phone\"></div><div class=\"cbp_tmlabel\"><h2>回&nbsp;&nbsp;单&nbsp;&nbsp;发&nbsp;&nbsp;送 <div class=\"Bar\" style=\"width: " + wt.ToString() + "px;\"><div><span>" + x.ToString() + "</span></div></div></h2><p></p></div></li>");
                        }
                    }
                    //确认回单
                    if (it.ConfirmReturnAwbStatus.Equals("Y"))
                    {
                        if (!it.ConfirmReturnAwbDate.ToString("yyyy-MM-dd").Equals("0001-01-01"))
                        {
                            TimeSpan ts = it.ConfirmReturnAwbDate - it.SendReturnAwbDate;
                            double wt = 0;
                            if (ts.TotalMinutes / 4 > 200)
                            {
                                wt = 200;
                            }
                            else
                            {
                                wt = ts.TotalMinutes / 4;
                            }
                            decimal x = 0M;
                            if (decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2) > 1)
                            {
                                x = decimal.Round(Convert.ToDecimal((ts.TotalMinutes / 60)), 2);
                            }
                            res.Append("<li><time class=\"cbp_tmtime\" datetime=\"" + it.ConfirmReturnAwbDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"><span>" + it.ConfirmReturnAwbDate.ToString("yyyy-MM-dd") + "</span> <span>" + it.ConfirmReturnAwbDate.ToString("HH:mm:ss") + "</span></time><div class=\"cbp_tmicon cbp_tmicon-phone\"></div><div class=\"cbp_tmlabel\"><h2>回&nbsp;&nbsp;单&nbsp;&nbsp;确&nbsp;&nbsp;认 <div class=\"Bar\" style=\"width: " + wt.ToString() + "px;\"><div><span>" + x.ToString() + "</span></div></div></h2><p></p></div></li>");
                        }
                    }
                }
                res.Append("</ul></div></div>");
            }
            String json = JSON.Encode(res.ToString());
            Response.Write(json);
        }
        /// <summary>
        /// 查询运单状态供导出用
        /// </summary>
        public void QueryAwbStatusTrackForExport()
        {
            AwbEntity queryEntity = new AwbEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }

            #region 赋值

            string[] arr = key.Split(',');
            if (!string.IsNullOrEmpty(arr[3]))
            {
                queryEntity.AwbNo = arr[3].Trim();
            }
            else
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.StartDate = Convert.ToDateTime(arr[i].Trim()); } break;
                        case 1:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.EndDate = Convert.ToDateTime(arr[i].Trim()); } break;
                        case 2:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Dest = arr[i].Trim(); } break;
                        case 3:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.AwbNo = arr[i].Trim(); } break;
                        case 4:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.ShipperUnit = arr[i].Trim(); } break;
                        case 5:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.AcceptPeople = arr[i].Trim(); } break;
                        case 6:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Goods = arr[i].Trim(); } break;
                        case 7:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Piece = Convert.ToInt32(arr[i].Trim()); } break;
                        case 8:
                            if (!arr[i].Equals("-1")) { queryEntity.DeliveryType = Convert.ToString(arr[i]); } break;
                        case 9:
                            if (!arr[i].Equals("-1")) { queryEntity.DelFlag = Convert.ToString(arr[i]); } break;
                        case 10:
                            if (!arr[i].Equals("-1"))
                            {
                                queryEntity.TransKind = Convert.ToString(arr[i]);//TransKind运单类型是自发还是外协
                            }
                            break;
                        case 11:
                            if (!string.IsNullOrEmpty(arr[i]))
                            {
                                queryEntity.HAwbNo = Convert.ToString(arr[i].Trim());//副单号
                            }
                            break;
                        case 12:
                            if (!arr[i].Equals("-1"))
                            {
                                queryEntity.CheckStatus = Convert.ToString(arr[i]);//CheckStatus结算状态
                            }
                            break;
                        case 13:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Dep = arr[i].Trim(); } break;
                        case 14:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.MidDest = arr[i].Trim(); } break;
                        case 15:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.CStartDate = Convert.ToDateTime(arr[i].Trim()); } break;
                        case 16:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.CEndDate = Convert.ToDateTime(arr[i].Trim()); } break;
                        case 17:
                            if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Transit = arr[i].Trim(); } break;
                        default:
                            break;
                    }
                }
            }
            #endregion
            queryEntity.BelongSystem = UserInfor.NewLandBelongSystem;
            ArriveBus bus = new ArriveBus();
            string err = "OK";
            List<AwbStatusTrackEntity> list = bus.QueryAwbStatusTrack(queryEntity);
            if (list.Count > 0) { AwbStatusList = list; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 客户货物跟踪导出实体
        /// </summary>
        public List<AwbStatusTrackEntity> AwbStatusList
        {
            get
            {
                if (Session["AwbStatusList"] == null)
                {
                    Session["AwbStatusList"] = new List<AwbStatusTrackEntity>();
                }
                return (List<AwbStatusTrackEntity>)(Session["AwbStatusList"]);
            }
            set
            {
                Session["AwbStatusList"] = value;
            }
        }
        #endregion
    }
}