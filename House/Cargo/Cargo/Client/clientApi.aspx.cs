using Cargo.QY;
using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.Cargo.Product;
using House.Entity.House;
using Memcached.ClientLibrary;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Formula.Functions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Client
{
    public partial class clientApi : BasePage
    {
        /// <summary>
        /// 方法的入口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                //ErrMessage msg = new ErrMessage(); msg.Message = ex.Message;
                //msg.Result = false;
                //string res = JSON.Encode(msg);
                //Response.Write(res);

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

        #region 客户管理操作方法集合
        /// <summary>
        /// 客户查询
        /// </summary>
        public void QueryCargoClient()
        {
            CargoClientEntity queryEntity = new CargoClientEntity();
            if (!string.IsNullOrEmpty(Request["ClientNum"]))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            queryEntity.ClientName = Convert.ToString(Request["ClientName"]);
            queryEntity.ClientShortName = Convert.ToString(Request["ClientShortName"]);
            queryEntity.Boss = Convert.ToString(Request["Boss"]);
            queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
            queryEntity.Telephone = Convert.ToString(Request["Telephone"]);
            queryEntity.Cellphone = Convert.ToString(Request["Cellphone"]);
            queryEntity.ShopCode = Convert.ToString(Request["ShopCode"]);
            queryEntity.Province = Convert.ToString(Request["Province"]);
            queryEntity.City = Convert.ToString(Request["City"]);
            queryEntity.UserID = Convert.ToString(Request["UserID"]);
            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                //queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
                queryEntity.CargoPermisID = Convert.ToString(Request["HID"]);
            }
            else
            {
                queryEntity.CargoPermisID = UserInfor.CargoPermisID;
            }
            if (Request["DelFlag"] != "-1")
            {
                queryEntity.DelFlag = Convert.ToString(Request["DelFlag"]);
            }
            if (Request["ClientType"] != "-1")
            {
                queryEntity.ClientType = Convert.ToString(Request["ClientType"]);
            }

            if (Request["ArrivePayLimit"] != "-1")
            {
                queryEntity.ArrivePayLimit = Convert.ToString(Request["ArrivePayLimit"]);
            }
            if (!string.IsNullOrEmpty(Request["UpClientID"]))//所属公司
            {
                queryEntity.UpClientID = Convert.ToInt32(Request["UpClientID"]);
            }
            if (!string.IsNullOrEmpty(Request["BizUserId"]))//分账客户编码（通联
            {
                queryEntity.BizUserId = Convert.ToString(Request["BizUserId"]);
            }
            queryEntity.PushAccount = Convert.ToString(Request["PushAccount"]);

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryCargoClient(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }


        /// <summary>
        /// 查询客户供应商信息
        /// </summary>
        public void QueryCargoClientSupplier()
        {
            CargoClientEntity queryEntity = new CargoClientEntity();
            queryEntity.ClientType = Convert.ToString(Request["ClientType"]);
            if (!string.IsNullOrEmpty(Convert.ToString(Request["UpClientID"])))
            {
                queryEntity.UpClientID = Convert.ToInt32(Request["UpClientID"]);
            }
            CargoClientBus bus = new CargoClientBus();
            List<CargoClientEntity> list = bus.QueryCargoClientList(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 客户数据 导出
        /// </summary>
        public void QueryCargoClientForExport()
        {
            CargoClientEntity queryEntity = new CargoClientEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.ClientNum = Convert.ToInt32(arr[i]); }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.ClientName = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.ShopCode = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Boss = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Telephone = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 5:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Cellphone = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 6:
                        if (!arr[i].Equals("-1")) { queryEntity.DelFlag = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 7:
                        if (!arr[i].Equals("-1")) { queryEntity.ClientType = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 8://所属仓库ID
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.CargoPermisID = Convert.ToString(arr[i].Trim()); }
                        else { queryEntity.CargoPermisID = UserInfor.CargoPermisID; }
                        break;
                    case 9:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.UpClientID = Convert.ToInt32(arr[i].Trim()); }
                        break;
                    default:
                        break;
                }
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 100000 : Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryCargoClient(pageIndex, pageSize, queryEntity);
            string err = "OK";
            List<CargoClientEntity> awbList = (List<CargoClientEntity>)list["rows"];
            if (awbList.Count > 0) { CargoClientList = awbList; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        public List<CargoClientEntity> CargoClientList
        {
            get
            {
                if (Session["CargoClientList"] == null)
                {
                    Session["CargoClientList"] = new List<CargoClientEntity>();
                }
                return (List<CargoClientEntity>)(Session["CargoClientList"]);
            }
            set
            {
                Session["CargoClientList"] = value;
            }
        }
        /// <summary>
        /// 根据客户编码查询客户信息
        /// </summary>
        public void QueryCargoClientInfo()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            string clientNum = Request["ClientNum"];
            try
            {
                if (!string.IsNullOrEmpty(clientNum))
                {
                    CargoClientBus bus = new CargoClientBus();
                    CargoClientEntity client = bus.QueryCargoClient(Convert.ToInt32(clientNum));
                    if (client != null)
                    {
                        msg.Result = true;
                        msg.Message = client.LogisID.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }

            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        public void DelCargoClient()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoClientEntity> list = new List<CargoClientEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "客户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoClientEntity
                    {
                        ClientID = Convert.ToInt64(row["ClientID"]),
                        ClientNum = Convert.ToInt32(row["ClientNum"]),
                        ClientName = Convert.ToString(row["ClientName"]),
                        ClientShortName = Convert.ToString(row["ClientShortName"]),
                        Cellphone = Convert.ToString(row["Cellphone"]),
                        Boss = Convert.ToString(row["Boss"]),
                        ClientType = Convert.ToString(row["ClientType"]),
                        Telephone = Convert.ToString(row["Telephone"]),
                        DelFlag = Convert.ToString(row["DelFlag"])
                    });
                }
                bus.DelCargoClient(list, log);
                msg.Result = true;
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
            Response.End();

        }
        /// <summary>
        /// 保存客户信息
        /// </summary>
        public void SaveCargoClient()
        {
            CargoClientEntity ent = new CargoClientEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["ClientID"] != null ? Request["ClientID"].ToString() : "";
            if (UserInfor.IsAddClient.Equals("0"))
            {
                msg.Message = "没有权限添加修改客户";
                msg.Result = false;
                goto ERROR;
            }
            try
            {
                string bossName = Request["Boss"]?.ToString().Trim() ?? string.Empty;
                if (bossName.Length > 15)
                {
                    msg.Message = "负责人名不可超过15字符";
                    msg.Result = false;
                    goto ERROR;
                }

                ent.UpClientID = string.IsNullOrEmpty(Request["UpClientID"]) ? 0 : Convert.ToInt32(Request["UpClientID"]);
                ent.UpClientShortName = Convert.ToString(Request["UpClientShortName"]);
                ent.ClientName = Convert.ToString(Request["ClientName"]).Trim();
                ent.ClientShortName = Convert.ToString(Request["ClientShortName"]).Trim();
                ent.Cellphone = Convert.ToString(Request["Cellphone"]).Trim();
                ent.ClientType = Convert.ToString(Request["ClientType"]);
                ent.Province = Convert.ToString(Request["AProvince"]);
                ent.City = Convert.ToString(Request["ACity"]);
                ent.Country = Convert.ToString(Request["ACountry"]);
                ent.Address = Convert.ToString(Request["Address"]).Trim();
                ent.ZipCode = Convert.ToString(Request["ZipCode"]);
                ent.Telephone = Convert.ToString(Request["Telephone"]).Trim();
                ent.Fax = Convert.ToString(Request["Fax"]);
                ent.Boss = Convert.ToString(Request["Boss"]).Trim();
                ent.Email = Convert.ToString(Request["Email"]).Trim();
                ent.Product = Convert.ToString(Request["Product"]);
                ent.Remark = Convert.ToString(Request["Remark"]).Trim();
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                ent.CheckOutType = Convert.ToString(Request["CheckOutType"]);
                ent.ShopCode = Convert.ToString(Request["ShopCode"]);
                ent.UserID = Convert.ToString(Request["UserID"]);
                ent.UserName = Convert.ToString(Request["UserName"]);
                ent.BusUserID = Convert.ToString(Request["BusUserID"]);//市场业务员
                ent.BusUserName = Convert.ToString(Request["BusUserName"]);
                ent.ClientTypeID = Convert.ToString(Request["ClientTypeID"]);//客户关联产品分类
                ent.ClientTypeName = Convert.ToString(Request["ClientTypeName"]);//客户关联产品分类名称
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                ent.LimitMoney = string.IsNullOrEmpty(Convert.ToString(Request["LimitMoney"])) ? 0 : Convert.ToDecimal(Request["LimitMoney"]);
                ent.TargetNum = string.IsNullOrEmpty(Convert.ToString(Request["TargetNum"])) ? 0 : Convert.ToInt32(Request["TargetNum"]);
                ent.TryeCompany = Convert.ToString(Request["TryeCompany"]);
                ent.TryeClientCode = Convert.ToString(Request["TryeClientCode"]);
                ent.TryeClientType = Convert.ToString(Request["TryeClientType"]);
                ent.LogisID = string.IsNullOrEmpty(Convert.ToString(Request["LogisID"])) ? 0 : Convert.ToInt32(Request["LogisID"]);
                ent.LogisName = Convert.ToString(Request["LogisName"]);
                ent.LogisFee = string.IsNullOrEmpty(Convert.ToString(Request["LogisFee"])) ? 0 : Convert.ToDecimal(Request["LogisFee"]);
                ent.PushAccount = Convert.ToString(Request["PushAccount"]);
                ent.Discount = string.IsNullOrEmpty(Convert.ToString(Request["Discount"])) ? 0 : Convert.ToDecimal(Request["Discount"]);
                ent.SettleHouseID = Convert.ToString(Request["SettleHouseID"]);
                ent.SettleHouseName = Convert.ToString(Request["SettleHouseName"]);
                ent.GroundPushName = Convert.ToString(Request["GroundPushName"]);
                ent.MulAddressOrder = string.IsNullOrEmpty(Request["MulAddressOrder"].ToString())?0:Convert.ToInt32(Request["MulAddressOrder"]);
                
                ent.ArrivePayLimit = Convert.ToString(Request["ArrivePayLimit"]);
                if (Request.Form.AllKeys.Contains("NoType"))
                {
                    if (!string.IsNullOrEmpty(Request["NoType"].ToString()))
                    {
                        ent.NoType = Convert.ToString(Request["NoType"]).Trim(',');
                    }
                }
                
                
                ent.BizUserId = Convert.ToString(Request["BizUserId"]);
                ent.IsBuy = string.IsNullOrEmpty(Convert.ToString(Request["IsBuy"])) ? 0 : Convert.ToInt32(Request["IsBuy"]);
                if (ent.ClientType.Equals("5"))
                {
                    if (!UserInfor.LoginName.Equals("1000") && !UserInfor.LoginName.Equals("3326"))
                    {
                        msg.Result = false;
                        msg.Message = "您无权限修改";
                        goto ERROR;
                    }
                }
                if (id == "")
                {
                    //如果不是VIP客户就随便自动生成6位的客户编码
                    if (!ent.ClientType.Equals("2"))
                    {
                        ent.ClientNum = Common.GetClientNum();//生成随机的六位数字做为客户编码
                    }
                    else
                    {
                        ent.ClientNum = Convert.ToInt32(Request["ClientNum"]);
                    }
                    if (bus.IsExistCargoClient(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在相同的客户简称/负责人名";
                        goto ERROR;
                    }
                    else
                    {
                        ent.BookCheck = "1";
                        if (ent.HouseID.Equals(9) || ent.HouseID.Equals(34) || ent.HouseID.Equals(44) || ent.HouseID.Equals(45))
                        {
                            if (ent.ClientType.Equals("1"))
                            {
                                //月结客户
                                ent.BookCheck = "0";
                            }
                        }
                        log.Operate = "A";
                        bus.AddCargoClient(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                }
                else
                {
                    ent.ClientID = Convert.ToInt64(id);
                    if (bus.IsExistCargoClient(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在相同的客户简称/负责人名";
                        goto ERROR;
                    }
                    else
                    {
                        ent.ClientNum = Convert.ToInt32(Request["ClientNum"]);
                        log.Operate = "U";
                        bus.UpdateCargoClient(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        //返回处理结果
        ERROR:
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        public void GetAvailableLimit()
        {
            var a1 = DateTime.Now;
            CargoOrderEntity entity = new CargoOrderEntity();
            entity.HouseID = UserInfor.HouseID;
            entity.RuleType = "6";
            int ClientNum = Convert.ToInt32(Request["ClientNum"]);
            CargoPriceBus bus = new CargoPriceBus();
            DataTable dt = bus.QueryPriceRuleBankInfo(entity);
            var a2 = DateTime.Now;
            DataRow[] rows = dt.Select("MeetLimit=0 and Regular=0 and SuitClientNum=''");
            CargoProductBus busProduct = new CargoProductBus();
            int InPiece = 0;
            int PieceSum = 0;
            List<CargoContainerShowEntity> list = busProduct.QueryInHouseProductData(new CargoContainerShowEntity { HouseID = UserInfor.HouseID, TypeID = 163 });
            var a3 = DateTime.Now;
            foreach (CargoContainerShowEntity item in list)
            {
                PieceSum += item.InPiece;
            }
            var a4 = DateTime.Now;
            for (int i = 0; i < rows.Count(); i++)
            {
                list = busProduct.QueryInHouseProductData(new CargoContainerShowEntity { HouseID = UserInfor.HouseID, TypeID = 163, Specs = rows[i]["Specs"].ToString(), Figure = rows[i]["Figure"].ToString(), LoadIndex = rows[i]["LoadIndex"].ToString(), SpeedLevel = rows[i]["SpeedLevel"].ToString() });
                foreach (CargoContainerShowEntity item in list)
                {
                    InPiece += item.InPiece;
                }
            }
            var a5 = DateTime.Now;
            CargoClientBus busClient = new CargoClientBus();
            List<ClientSessionEntity> clientList = busClient.QueryClientSession(new CargoClientEntity { HouseID = UserInfor.HouseID, DelFlag = "0" });
            Decimal LimitMoneySum = 0;
            foreach (ClientSessionEntity item in clientList)
            {
                LimitMoneySum += item.LimitMoney;
            }
            var a6 = DateTime.Now;
            //Decimal money = InPiece - (PieceSum * 10 / 85) - LimitMoneySum;
            Decimal money = InPiece - 509 - LimitMoneySum;
            string res = JSON.Encode(money);
            Response.Write(res);
            Response.End();
        }
        public void UpdateClentMoney()
        {
            CargoClientEntity ent = new CargoClientEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户管理";
            log.Status = "0";
            log.Operate = "U";
            msg.Result = true;
            msg.Message = "成功";
            log.UserID = UserInfor.LoginName.Trim();
            try
            {
                ent.ClientNum = Convert.ToInt32(Request["ClientNum"]);
                ent.ClientID = Convert.ToInt64(Request["ClientID"].ToString());
                ent.HouseID = UserInfor.HouseID;
                ent.LimitMoney = string.IsNullOrEmpty(Convert.ToString(Request["LimitMoney"])) ? 0 : Convert.ToDecimal(Request["LimitMoney"]);
                ent.TargetNum = string.IsNullOrEmpty(Convert.ToString(Request["TargetNum"])) ? 0 : Convert.ToInt32(Request["TargetNum"]);
                bus.UpdateClentMoney(ent, log);
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 自动完成客户名称信息
        /// </summary>
        public void AutoCompleteClient()
        {
            CargoClientEntity queryEntity = new CargoClientEntity();
            //#region 缓存

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
            //#endregion
            queryEntity.DelFlag = "0";
            if (!string.IsNullOrEmpty(Convert.ToString(Request["BelongHouse"])))
            {
                queryEntity.BelongHouse = Convert.ToString(Request["BelongHouse"]);
            }
            else
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Request["houseID"])))
                {

                    if (Convert.ToInt32(Request["houseID"]) == 1 || Convert.ToInt32(Request["houseID"]) == 96 || Convert.ToInt32(Request["houseID"]) == 100 || Convert.ToInt32(Request["houseID"]) == 116)
                    {
                        queryEntity.HouseIDStr = "1,96,100,116";
                    }
                    else if (Convert.ToInt32(Request["houseID"]) == 3 || Convert.ToInt32(Request["houseID"]) == 99 || Convert.ToInt32(Request["houseID"]) == 130 || Convert.ToInt32(Request["houseID"]) == 145)
                    {
                        queryEntity.HouseIDStr = "3,99,130,145";
                    }
                    else if (Convert.ToInt32(Request["houseID"]) == 9 || Convert.ToInt32(Request["houseID"]) == 44 || Convert.ToInt32(Request["houseID"]) == 45 || Convert.ToInt32(Request["houseID"]) == 65 || Convert.ToInt32(Request["houseID"]) == 83 || Convert.ToInt32(Request["houseID"]) == 84 || Convert.ToInt32(Request["houseID"]) == 85 || Convert.ToInt32(Request["houseID"]) == 91 || Convert.ToInt32(Request["houseID"]) == 93 || Convert.ToInt32(Request["houseID"]) == 95 || Convert.ToInt32(Request["houseID"]) == 97 || Convert.ToInt32(Request["houseID"]) == 98 || Convert.ToInt32(Request["houseID"]) == 101 || Convert.ToInt32(Request["houseID"]) == 102 || Convert.ToInt32(Request["houseID"]) == 105 || Convert.ToInt32(Request["houseID"]) == 106 || Convert.ToInt32(Request["houseID"]) == 107 || Convert.ToInt32(Request["houseID"]) == 108 || Convert.ToInt32(Request["houseID"]) == 109 || Convert.ToInt32(Request["houseID"]) == 110 || Convert.ToInt32(Request["houseID"]) == 111 || Convert.ToInt32(Request["houseID"]) == 112 || Convert.ToInt32(Request["houseID"]) == 113 || Convert.ToInt32(Request["houseID"]) == 114 || Convert.ToInt32(Request["houseID"]) == 115 || Convert.ToInt32(Request["houseID"]) == 116 || Convert.ToInt32(Request["houseID"]) == 117 || Convert.ToInt32(Request["houseID"]) == 118 || Convert.ToInt32(Request["houseID"]) == 119 || Convert.ToInt32(Request["houseID"]) == 120 || Convert.ToInt32(Request["houseID"]) == 121 || Convert.ToInt32(Request["houseID"]) == 122 || Convert.ToInt32(Request["houseID"]) == 123 || Convert.ToInt32(Request["houseID"]) == 124 || Convert.ToInt32(Request["houseID"]) == 125 || Convert.ToInt32(Request["houseID"]) == 126 || Convert.ToInt32(Request["houseID"]) == 127 || Convert.ToInt32(Request["houseID"]) == 128 || Convert.ToInt32(Request["houseID"]) == 129 || Convert.ToInt32(Request["houseID"]) == 138 || Convert.ToInt32(Request["houseID"]) == 132 || Convert.ToInt32(Request["houseID"]) == 133 || Convert.ToInt32(Request["houseID"]) == 134 || Convert.ToInt32(Request["houseID"]) == 135 || Convert.ToInt32(Request["houseID"]) == 96 || Convert.ToInt32(Request["houseID"]) == 100 || Convert.ToInt32(Request["houseID"]) == 99 || Convert.ToInt32(Request["houseID"]) == 130 || Convert.ToInt32(Request["houseID"]) == 136 || Convert.ToInt32(Request["houseID"]) == 139 || Convert.ToInt32(Request["houseID"]) == 141 || Convert.ToInt32(Request["houseID"]) == 142 || Convert.ToInt32(Request["houseID"]) == 143 || Convert.ToInt32(Request["houseID"]) == 144)
                    {
                        queryEntity.HouseIDStr = "9,44,45,65,83,84,85,91,93,95,97,98,101,102,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,138,132,133,134,135,96,100,99,130,136,139,141,142,143,144";
                    }
                    else
                    {
                        queryEntity.HouseID = Convert.ToInt32(Request["houseID"]);
                    }
                }
                else
                {

                    if (UserInfor.HouseID == 1 || UserInfor.HouseID == 96 || UserInfor.HouseID == 100 || UserInfor.HouseID == 116)
                    {
                        queryEntity.HouseIDStr = "1,96,100,116";
                    }
                    else if (UserInfor.HouseID == 3 || UserInfor.HouseID == 99 || UserInfor.HouseID == 130 || UserInfor.HouseID == 145)
                    {
                        queryEntity.HouseIDStr = "3,99,130,145";
                    }
                    else if (UserInfor.HouseID == 9 || UserInfor.HouseID == 45 || UserInfor.HouseID == 44 || UserInfor.HouseID == 65 || UserInfor.HouseID == 83 || UserInfor.HouseID == 84 || UserInfor.HouseID == 85 || UserInfor.HouseID == 91 || UserInfor.HouseID == 93 || UserInfor.HouseID == 95 || UserInfor.HouseID == 97 || UserInfor.HouseID == 98 || UserInfor.HouseID == 101 || UserInfor.HouseID == 102 || UserInfor.HouseID == 105 || UserInfor.HouseID == 106 || UserInfor.HouseID == 107 || UserInfor.HouseID == 108 || UserInfor.HouseID == 109 || UserInfor.HouseID == 110 || UserInfor.HouseID == 111 || UserInfor.HouseID == 112 || UserInfor.HouseID == 113 || UserInfor.HouseID == 114 || UserInfor.HouseID == 115 || UserInfor.HouseID == 116 || UserInfor.HouseID == 117 || UserInfor.HouseID == 118 || UserInfor.HouseID == 119 || UserInfor.HouseID == 120 || UserInfor.HouseID == 121 || UserInfor.HouseID == 122 || UserInfor.HouseID == 123 || UserInfor.HouseID == 124 || UserInfor.HouseID == 125 || UserInfor.HouseID == 126 || UserInfor.HouseID == 127 || UserInfor.HouseID == 128 || UserInfor.HouseID == 129 || UserInfor.HouseID == 138 || UserInfor.HouseID == 132 || UserInfor.HouseID == 133 || UserInfor.HouseID == 134 || UserInfor.HouseID == 135 || UserInfor.HouseID == 96 || UserInfor.HouseID == 99 || UserInfor.HouseID == 100 || UserInfor.HouseID == 130 || UserInfor.HouseID == 136 || UserInfor.HouseID == 139 || UserInfor.HouseID == 141 || UserInfor.HouseID == 142 || UserInfor.HouseID == 143 || UserInfor.HouseID == 144)
                    {
                        queryEntity.HouseIDStr = "9,44,45,65,83,84,85,91,93,95,97,98,101,102,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,138,132,133,134,135,96,99,100,130,136,139,141,142,143,144";
                    }
                    else
                    {
                        queryEntity.HouseID = UserInfor.HouseID;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["UpClientID"])))
            {
                queryEntity.UpClientID = Convert.ToInt32(Request["UpClientID"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ClientNums"])))
            {
                queryEntity.ClientNums = Convert.ToString(Request["ClientNums"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ClientType"])))
            {
                queryEntity.ClientType = Convert.ToString(Request["ClientType"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["Boss"])))
            {
                queryEntity.Boss = Convert.ToString(Request["Boss"]).Trim();
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["UserName"])))
            {
                queryEntity.UserName = Convert.ToString(Request["UserName"]).Trim();
            }
            queryEntity.SettleHouseID = Convert.ToString(Request["SettleHouseID"]);
            CargoClientBus bus = new CargoClientBus();
            List<ClientSessionEntity> list = new List<ClientSessionEntity>();
            list = bus.QueryClientSession(queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            //Response.End();
        }
        /// <summary>
        /// 自动完成客户名称信息
        /// </summary>
        public void GetCompleteClient()
        {
            CargoClientEntity queryEntity = new CargoClientEntity();
            queryEntity.DelFlag = "0";
            if (!string.IsNullOrEmpty(Convert.ToString(Request["BelongHouse"])))
            {
                queryEntity.BelongHouse = Convert.ToString(Request["BelongHouse"]);
            }
            else
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Request["houseID"])))
                {

                    if (Convert.ToInt32(Request["houseID"]) == 1 || Convert.ToInt32(Request["houseID"]) == 96 || Convert.ToInt32(Request["houseID"]) == 100 || Convert.ToInt32(Request["houseID"]) == 116)
                    {
                        queryEntity.HouseIDStr = "1,96,100,116";
                    }
                    else if (Convert.ToInt32(Request["houseID"]) == 3 || Convert.ToInt32(Request["houseID"]) == 99 || Convert.ToInt32(Request["houseID"]) == 130)
                    {
                        queryEntity.HouseIDStr = "3,99,130";
                    }
                    else if (Convert.ToInt32(Request["houseID"]) == 9 || Convert.ToInt32(Request["houseID"]) == 44 || Convert.ToInt32(Request["houseID"]) == 45 || Convert.ToInt32(Request["houseID"]) == 65 || Convert.ToInt32(Request["houseID"]) == 83 || Convert.ToInt32(Request["houseID"]) == 84 || Convert.ToInt32(Request["houseID"]) == 85 || Convert.ToInt32(Request["houseID"]) == 91 || Convert.ToInt32(Request["houseID"]) == 93 || Convert.ToInt32(Request["houseID"]) == 95 || Convert.ToInt32(Request["houseID"]) == 97 || Convert.ToInt32(Request["houseID"]) == 98 || Convert.ToInt32(Request["houseID"]) == 101 || Convert.ToInt32(Request["houseID"]) == 102 || Convert.ToInt32(Request["houseID"]) == 105 || Convert.ToInt32(Request["houseID"]) == 106 || Convert.ToInt32(Request["houseID"]) == 107 || Convert.ToInt32(Request["houseID"]) == 108 || Convert.ToInt32(Request["houseID"]) == 109 || Convert.ToInt32(Request["houseID"]) == 110 || Convert.ToInt32(Request["houseID"]) == 111 || Convert.ToInt32(Request["houseID"]) == 112 || Convert.ToInt32(Request["houseID"]) == 113 || Convert.ToInt32(Request["houseID"]) == 114 || Convert.ToInt32(Request["houseID"]) == 115 || Convert.ToInt32(Request["houseID"]) == 116 || Convert.ToInt32(Request["houseID"]) == 117 || Convert.ToInt32(Request["houseID"]) == 118 || Convert.ToInt32(Request["houseID"]) == 119 || Convert.ToInt32(Request["houseID"]) == 120 || Convert.ToInt32(Request["houseID"]) == 121 || Convert.ToInt32(Request["houseID"]) == 122 || Convert.ToInt32(Request["houseID"]) == 123 || Convert.ToInt32(Request["houseID"]) == 124 || Convert.ToInt32(Request["houseID"]) == 125 || Convert.ToInt32(Request["houseID"]) == 126 || Convert.ToInt32(Request["houseID"]) == 127 || Convert.ToInt32(Request["houseID"]) == 128 || Convert.ToInt32(Request["houseID"]) == 129 || Convert.ToInt32(Request["houseID"]) == 138 || Convert.ToInt32(Request["houseID"]) == 132 || Convert.ToInt32(Request["houseID"]) == 133 || Convert.ToInt32(Request["houseID"]) == 134 || Convert.ToInt32(Request["houseID"]) == 135 || Convert.ToInt32(Request["houseID"]) == 96 || Convert.ToInt32(Request["houseID"]) == 100 || Convert.ToInt32(Request["houseID"]) == 99 || Convert.ToInt32(Request["houseID"]) == 130 || Convert.ToInt32(Request["houseID"]) == 136)
                    {
                        queryEntity.HouseIDStr = "9,44,45,65,83,84,85,91,93,95,97,98,101,102,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,138,132,133,134,135,96,100,99,130,136";
                    }
                    else
                    {
                        queryEntity.HouseID = Convert.ToInt32(Request["houseID"]);
                    }
                }
                else
                {

                    if (UserInfor.HouseID == 1 || UserInfor.HouseID == 96 || UserInfor.HouseID == 100 || UserInfor.HouseID == 116)
                    {
                        queryEntity.HouseIDStr = "1,96,100,116";
                    }
                    else if (UserInfor.HouseID == 3 || UserInfor.HouseID == 99 || UserInfor.HouseID == 130)
                    {
                        queryEntity.HouseIDStr = "3,99,130";
                    }
                    else if (UserInfor.HouseID == 9 || UserInfor.HouseID == 45 || UserInfor.HouseID == 44 || UserInfor.HouseID == 65 || UserInfor.HouseID == 83 || UserInfor.HouseID == 84 || UserInfor.HouseID == 85 || UserInfor.HouseID == 91 || UserInfor.HouseID == 93 || UserInfor.HouseID == 95 || UserInfor.HouseID == 97 || UserInfor.HouseID == 98 || UserInfor.HouseID == 101 || UserInfor.HouseID == 102 || UserInfor.HouseID == 105 || UserInfor.HouseID == 106 || UserInfor.HouseID == 107 || UserInfor.HouseID == 108 || UserInfor.HouseID == 109 || UserInfor.HouseID == 110 || UserInfor.HouseID == 111 || UserInfor.HouseID == 112 || UserInfor.HouseID == 113 || UserInfor.HouseID == 114 || UserInfor.HouseID == 115 || UserInfor.HouseID == 116 || UserInfor.HouseID == 117 || UserInfor.HouseID == 118 || UserInfor.HouseID == 119 || UserInfor.HouseID == 120 || UserInfor.HouseID == 121 || UserInfor.HouseID == 122 || UserInfor.HouseID == 123 || UserInfor.HouseID == 124 || UserInfor.HouseID == 125 || UserInfor.HouseID == 126 || UserInfor.HouseID == 127 || UserInfor.HouseID == 128 || UserInfor.HouseID == 129 || UserInfor.HouseID == 138 || UserInfor.HouseID == 132 || UserInfor.HouseID == 133 || UserInfor.HouseID == 134 || UserInfor.HouseID == 135 || UserInfor.HouseID == 96 || UserInfor.HouseID == 99 || UserInfor.HouseID == 100 || UserInfor.HouseID == 130 || UserInfor.HouseID == 136)
                    {
                        queryEntity.HouseIDStr = "9,44,45,65,83,84,85,91,93,95,97,98,101,102,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,138,132,133,134,135,96,99,100,130,136";
                    }
                    else
                    {
                        queryEntity.HouseID = UserInfor.HouseID;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["UpClientID"])))
            {
                queryEntity.UpClientID = Convert.ToInt32(Request["UpClientID"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ClientNums"])))
            {
                queryEntity.ClientNums = Convert.ToString(Request["ClientNums"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ClientType"])))
            {
                queryEntity.ClientType = Convert.ToString(Request["ClientType"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["Boss"])))
            {
                queryEntity.Boss = Convert.ToString(Request["Boss"]).Trim();
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["UserName"])))
            {
                queryEntity.UserName = Convert.ToString(Request["UserName"]).Trim();
            }
            queryEntity.SettleHouseID = Convert.ToString(Request["SettleHouseID"]);
            CargoClientBus bus = new CargoClientBus();
            List<ClientSessionEntity> list = new List<ClientSessionEntity>();
            list = bus.GetCompleteClient(queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            //Response.End();
        }
        /// <summary>
        /// 查询客户所有收货地址
        /// </summary>
        public void AutoCompleteClientAcceptPeople()
        {
            CargoClientAcceptAddressEntity queryEntity = new CargoClientAcceptAddressEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ClientNum"])))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            else
            {
                return;
            }
            CargoClientBus bus = new CargoClientBus();
            List<CargoClientAcceptAddressEntity> list = new List<CargoClientAcceptAddressEntity>();
            list = bus.QueryAcceptAddress(queryEntity);

            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            //Response.End();
        }
        /// <summary>
        /// 查询客户所有业务编号
        /// </summary>
        public void AutoBusinessID()
        {
            CargoUpClientEntity queryEntity = new CargoUpClientEntity();
            int ClientID = 0;
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ClientID"])))
            {
                ClientID = Convert.ToInt32(Request["ClientID"]);
            }
            else
            {
                return;
            }
            CargoClientBus bus = new CargoClientBus();
            List<CargoUpClientEntity> list = new List<CargoUpClientEntity>();
            list = bus.QueryBusinessID(ClientID);

            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
           // Response.End();
        }
        public void QueryBusinessIDDefault()
        {
            CargoUpClientEntity entity = new CargoUpClientEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["UpClientID"])))
            {
                entity.UpClientID = Convert.ToInt64(Request["UpClientID"]);
            }


            CargoClientBus bus = new CargoClientBus();
            List<CargoUpClientEntity> list = new List<CargoUpClientEntity>();

            list = bus.QueryBusinessIDDefault(entity);

            string json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            //Response.End();
        }
        /// <summary>
        /// 保存客户来款记录
        /// </summary>
        public void saveIncomeRecord()
        {
            CargoClientIncomeEntity ent = new CargoClientIncomeEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "财务管理";
            log.NvgPage = "客户来款记录";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["IncomeID"] != null ? Request["IncomeID"].ToString() : "";
            try
            {
                ent.ClientID = Convert.ToInt64(Request["ClientID"]);
                ent.ClientNum = Convert.ToInt32(Request["ClientNum"]);
                ent.Money = string.IsNullOrEmpty(Convert.ToString(Request["Money"])) ? 0 : Convert.ToDecimal(Request["Money"]);
                ent.CreateDate = Convert.ToDateTime(Request["CreateDate"]);
                ent.BasicID = Convert.ToInt32(Request["BasicID"]);
                ent.RecordType = "0";
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                ent.OP_ID = UserInfor.LoginName;
                if (id == "")
                {

                    log.Operate = "A";
                    bus.AddClientIncome(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";

                }
                else
                {
                    //ent.ClientNum = Convert.ToInt32(Request["ClientNum"]);
                    log.Operate = "U";
                    ent.IncomeID = Convert.ToInt64(id);
                    bus.UpdateClientIncome(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();

        }
        /// <summary>
        /// 删除客户来款记录
        /// </summary>
        public void DelClientIncome()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoClientIncomeEntity> list = new List<CargoClientIncomeEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "客户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoClientIncomeEntity
                    {
                        IncomeID = Convert.ToInt64(row["IncomeID"]),
                        ClientID = Convert.ToInt64(row["ClientID"]),
                        ClientNum = Convert.ToInt32(row["ClientNum"]),
                        Money = Convert.ToDecimal(row["Money"]),
                        BasicID = Convert.ToInt32(row["BasicID"]),
                        CreateDate = Convert.ToDateTime(row["CreateDate"]),
                        HouseID = Convert.ToInt32(row["HouseID"]),
                        OP_ID = UserInfor.LoginName.Trim()
                    });
                }
                bus.DelClientIncome(list, log);
                msg.Result = true;
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
            Response.End();

        }
        /// <summary>
        /// 查询客户来款记录
        /// </summary>
        public void QueryClientIncome()
        {
            CargoClientIncomeEntity queryEntity = new CargoClientIncomeEntity();
            if (!string.IsNullOrEmpty(Request["ClientNum"]))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            queryEntity.Boss = Convert.ToString(Request["Boss"]);
            queryEntity.Cellphone = Convert.ToString(Request["Cellphone"]);
            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                //queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
                queryEntity.CargoPermisID = Convert.ToString(Request["HID"]);
            }
            else
            {
                queryEntity.CargoPermisID = UserInfor.CargoPermisID;
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryClientIncome(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 导入Excel返利文件
        /// </summary>
        public void saveRebateFile()
        {
            System.Web.HttpFileCollection files = this.Request.Files;
            if (files == null || files.Count == 0) return;
            string attachmentId = Guid.NewGuid().ToString();
            DataTable data = ToExcel.ImportExcelData(files);
            CargoImportEntity import = new CargoImportEntity();
            import.Result = true;
            import.Data = "";
            import.Message = "";
            import.Type = 0;
            import.ExistCount = 0;

            List<CargoClientPreRecordEntity> ent = new List<CargoClientPreRecordEntity>();

            //验证上传excel文件列数是否有效
            if (data.Columns.Count != 14)
            {
                import.Result = false;
                import.Type = 1;
                import.Message = "模板有误或缺少列，请使用指定模板";
                String abnormalJson = JSON.Encode(import);
                Response.Clear();
                Response.Write(abnormalJson);
                Response.End();
                return;
            }
            //清空table中的空行
            removeEmpty(data);
            if (data.Rows.Count <= 0)
            {
                import.Result = false;
                import.Type = 1;
                import.Message = "Excel无有效数据，请检查导入数据";
                String abnormalJson = JSON.Encode(import);
                Response.Clear();
                Response.Write(abnormalJson);
                Response.End();
                return;
            }
            int abnormalCount = 0;

            string msg = "";
            CargoClientBus bus = new CargoClientBus();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                CargoClientEntity cce = new CargoClientEntity();
                //if (Convert.ToString(data.Rows[i][1]).Trim().Equals("马牌"))
                //{
                if (string.IsNullOrEmpty(Convert.ToString(data.Rows[i][0])))
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行客户编码不能为空\r\n";
                    continue;
                }
                if (!isNumeric(Convert.ToString(data.Rows[i][0])))
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行客户编码数据有误\r\n";
                    continue;
                }
                //cce = bus.QueryCargoClient(new CargoClientEntity { TryeClientCode = Convert.ToString(data.Rows[i][2]) });
                cce = bus.QueryCargoClient(Convert.ToInt32(data.Rows[i][0]));
                if (cce.ClientID.Equals(0))
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行客户编码不存在\r\n";
                    continue;
                }
                // }
                CargoClientPreRecordEntity pe = new CargoClientPreRecordEntity();
                pe.ClientID = cce.ClientID;
                pe.ClientNum = cce.ClientNum;
                pe.Boss = cce.Boss;
                pe.TypeID = Convert.ToString(data.Rows[i][1]).Equals("马牌") ? 9 : Convert.ToString(data.Rows[i][1]).Equals("锦湖") ? 163 : 0;
                pe.TryeClientCode = Convert.ToString(data.Rows[i][2]);
                pe.RebateDate = Convert.ToDateTime(data.Rows[i][3]);
                pe.RebateMonth = Convert.ToDateTime(data.Rows[i][3]).ToString("yyyy-MM");
                //pe.RebateType = "0";
                pe.RecordType = "0";
                pe.OperaType = "1";
                pe.Remark = Convert.ToString(data.Rows[i][10]);
                if (Convert.ToString(data.Rows[i][1]).Equals("马牌"))
                {

                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][6])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][6])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行延保返利数据有误\r\n";
                            continue;
                        }
                        pe.YBMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][6])) ? 0 : Convert.ToDecimal(data.Rows[i][6]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][7])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][7])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行好评返利数据有误\r\n";
                            continue;
                        }
                        pe.HPMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][7])) ? 0 : Convert.ToDecimal(data.Rows[i][7]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][8])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][8])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行补贴返利数据有误\r\n";
                            continue;
                        }
                        pe.BTMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][8])) ? 0 : Convert.ToDecimal(data.Rows[i][8]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][9])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][9])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行ROOS返利数据有误\r\n";
                            continue;
                        }
                        pe.ROSSMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][9])) ? 0 : Convert.ToDecimal(data.Rows[i][9]);
                    }
                }
                else if (Convert.ToString(data.Rows[i][1]).Equals("锦湖"))
                {

                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][4])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][4])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行月度达成返利数据有误\r\n";
                            continue;
                        }
                        pe.MonthMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][4])) ? 0 : Convert.ToDecimal(data.Rows[i][4]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][5])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][5])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行18寸达成返利数据有误\r\n";
                            continue;
                        }
                        pe.ShiBaMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][5])) ? 0 : Convert.ToDecimal(data.Rows[i][5]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][6])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][6])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行早期订单返利数据有误\r\n";
                            continue;
                        }
                        pe.StageOrderMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][6])) ? 0 : Convert.ToDecimal(data.Rows[i][6]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][7])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][7])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行季度达成返利数据有误\r\n";
                            continue;
                        }
                        pe.QuaterMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][7])) ? 0 : Convert.ToDecimal(data.Rows[i][7]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][8])))
                    {
                        if (!isNumeric(Convert.ToString(data.Rows[i][8])))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行追加促销返利数据有误\r\n";
                            continue;
                        }
                        pe.PromotionMoney = string.IsNullOrEmpty(Convert.ToString(data.Rows[i][8])) ? 0 : Convert.ToDecimal(data.Rows[i][8]);
                    }
                }
                ent.Add(pe);
            }
            String json = JSON.Encode(ent);

            if (abnormalCount > 0)
            {
                import.Type = 1;
                import.Message = msg;
            }

            import.Result = true;
            import.Data = json;
            json = JSON.Encode(import);

            Response.Clear();
            Response.Write(json);
            Response.End();

        }
        /// <summary>
        /// 保存导入的返利数据
        /// </summary>
        public void SaveRebateData()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            CargoClientBus bus = new CargoClientBus();
            List<CargoClientPreRecordEntity> list = new List<CargoClientPreRecordEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "返利管理";
            log.Status = "0";
            log.NvgPage = "客户返利导入";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            List<CargoClientEntity> clientList = new List<CargoClientEntity>();
            try
            {
                foreach (Hashtable row in rows)
                {
                    CargoClientEntity client = new CargoClientEntity();
                    if (!string.IsNullOrEmpty(Convert.ToString(row["ClientNum"])))
                    {
                        //client = bus.QueryCargoClient(new CargoClientEntity { TryeClientCode = Convert.ToString(row["TryeClientCode"]) });
                        client = bus.QueryCargoClient(Convert.ToInt32(row["ClientNum"]));
                        client.RebateMoney = 0;
                    }
                    if (client.ClientID.Equals(0))
                    {
                        msg.Result = false;
                        msg.Message = "客户编码" + Convert.ToString(row["ClientNum"]) + "有误";
                        break;
                    }
                    CargoClientPreRecordEntity pe = new CargoClientPreRecordEntity();
                    CargoClientPreRecordEntity hp = new CargoClientPreRecordEntity();
                    CargoClientPreRecordEntity bt = new CargoClientPreRecordEntity();
                    CargoClientPreRecordEntity ross = new CargoClientPreRecordEntity();
                    CargoClientPreRecordEntity mt = new CargoClientPreRecordEntity();
                    CargoClientPreRecordEntity yb = new CargoClientPreRecordEntity();
                    if (!string.IsNullOrEmpty(Convert.ToString(row["YBMoney"])))
                    {
                        pe.RecordType = "0";
                        pe.OP_ID = UserInfor.LoginName;
                        pe.OperaType = "1";
                        pe.ClientID = client.ClientID;
                        pe.ClientNum = client.ClientNum;
                        pe.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]);
                        pe.TypeID = Convert.ToInt32(row["TypeID"]);
                        pe.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        pe.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        pe.Money = Convert.ToInt32(row["YBMoney"]);
                        pe.RebateType = "1";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(pe);
                        client.RebateMoney += pe.Money;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row["HPMoney"])))
                    {
                        hp.RecordType = "0";
                        hp.OP_ID = UserInfor.LoginName;
                        hp.OperaType = "1";
                        hp.ClientID = client.ClientID;
                        hp.ClientNum = client.ClientNum;
                        hp.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]).Trim();
                        hp.TypeID = Convert.ToInt32(row["TypeID"]);
                        hp.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        hp.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        hp.Money = Convert.ToInt32(row["HPMoney"]);
                        hp.RebateType = "3";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(hp);
                        client.RebateMoney += hp.Money;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row["BTMoney"])))
                    {
                        bt.RecordType = "0";
                        bt.OP_ID = UserInfor.LoginName;
                        //bt.ClientID = Convert.ToInt64(row["ClientID"]);
                        bt.ClientID = client.ClientID;
                        bt.ClientNum = client.ClientNum;
                        bt.OperaType = "1";
                        bt.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]).Trim();
                        bt.TypeID = Convert.ToInt32(row["TypeID"]);
                        bt.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        bt.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        bt.Money = Convert.ToInt32(row["BTMoney"]);
                        bt.RebateType = "2";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(bt);
                        client.RebateMoney += bt.Money;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row["ROSSMoney"])))
                    {
                        ross.RecordType = "0";
                        ross.OP_ID = UserInfor.LoginName;
                        //ross.ClientID = Convert.ToInt64(row["ClientID"]);
                        ross.ClientID = client.ClientID;
                        ross.ClientNum = client.ClientNum;
                        ross.OperaType = "1";
                        ross.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]).Trim();
                        ross.TypeID = Convert.ToInt32(row["TypeID"]);
                        ross.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        ross.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        ross.Money = Convert.ToInt32(row["ROSSMoney"]);
                        ross.RebateType = "4";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(ross);
                        client.RebateMoney += ross.Money;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row["MonthMoney"])))
                    {
                        mt.RecordType = "0";
                        mt.OP_ID = UserInfor.LoginName;
                        mt.ClientID = client.ClientID;
                        mt.ClientNum = client.ClientNum;
                        mt.OperaType = "1";
                        mt.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]).Trim();
                        mt.TypeID = Convert.ToInt32(row["TypeID"]);
                        mt.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        mt.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        mt.Money = Convert.ToDecimal(row["MonthMoney"]);
                        mt.RebateType = "5";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(mt);
                        client.RebateMoney += mt.Money;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row["ShiBaMoney"])))
                    {
                        yb.RecordType = "0";
                        yb.OP_ID = UserInfor.LoginName;
                        yb.ClientID = client.ClientID;
                        yb.ClientNum = client.ClientNum;
                        yb.OperaType = "1";
                        yb.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]).Trim();
                        yb.TypeID = Convert.ToInt32(row["TypeID"]);
                        yb.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        yb.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        yb.Money = Convert.ToDecimal(row["ShiBaMoney"]);
                        yb.RebateType = "6";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(yb);
                        client.RebateMoney += yb.Money;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row["StageOrderMoney"])))
                    {
                        yb.RecordType = "0";
                        yb.OP_ID = UserInfor.LoginName;
                        yb.ClientID = client.ClientID;
                        yb.ClientNum = client.ClientNum;
                        yb.OperaType = "1";
                        yb.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]).Trim();
                        yb.TypeID = Convert.ToInt32(row["TypeID"]);
                        yb.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        yb.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        yb.Money = Convert.ToDecimal(row["StageOrderMoney"]);
                        yb.RebateType = "7";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(yb);
                        client.RebateMoney += yb.Money;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row["QuaterMoney"])))
                    {
                        yb.RecordType = "0";
                        yb.OP_ID = UserInfor.LoginName;
                        yb.ClientID = client.ClientID;
                        yb.ClientNum = client.ClientNum;
                        yb.OperaType = "1";
                        yb.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]).Trim();
                        yb.TypeID = Convert.ToInt32(row["TypeID"]);
                        yb.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        yb.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        yb.Money = Convert.ToDecimal(row["QuaterMoney"]);
                        yb.RebateType = "8";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(yb);
                        client.RebateMoney += yb.Money;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row["PromotionMoney"])))
                    {
                        yb.RecordType = "0";
                        yb.OP_ID = UserInfor.LoginName;
                        yb.ClientID = client.ClientID;
                        yb.ClientNum = client.ClientNum;
                        yb.OperaType = "1";
                        yb.TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"])) ? "" : Convert.ToString(row["TryeClientCode"]).Trim();
                        yb.TypeID = Convert.ToInt32(row["TypeID"]);
                        yb.RebateDate = Convert.ToDateTime(row["RebateDate"]);
                        yb.RebateMonth = Convert.ToString(row["RebateMonth"]);
                        yb.Money = Convert.ToDecimal(row["PromotionMoney"]);
                        yb.RebateType = "9";
                        pe.Remark = Convert.ToString(row["Remark"]);
                        list.Add(yb);
                        client.RebateMoney += yb.Money;
                    }
                    clientList.Add(client);
                }
                if (msg.Result)
                {
                    bus.SaveRebateData(list, log);
                    bus.UpdateCargoClientRebateMoney(clientList, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();

        }
        /// <summary>
        /// 导入收货地址文件
        /// </summary>

        public void saveAddrFile()
        {
            System.Web.HttpFileCollection files = this.Request.Files;
            if (files == null || files.Count == 0) return;
            string attachmentId = Guid.NewGuid().ToString();
            DataTable data = ToExcel.ImportExcelData(files);
            CargoClientBus bus = new CargoClientBus();

            CargoImportEntity import = new CargoImportEntity();
            import.Result = true;
            import.Data = "";
            import.Message = "";
            import.Type = 0;
            import.ExistCount = 0;
            int clientNum = Convert.ToInt32(Request["clientNum"]);
            CargoClientEntity clientEntity = bus.QueryCargoClient(clientNum);
            List<CargoClientAcceptAddressEntity> ent = new List<CargoClientAcceptAddressEntity>();

            //验证上传excel文件列数是否有效
            if (data.Columns.Count != 7)
            {
                import.Result = false;
                import.Type = 1;
                import.Message = "模板有误或缺少列，请使用指定模板";
                String abnormalJson = JSON.Encode(import);
                Response.Clear();
                Response.Write(abnormalJson);
                Response.End();
                return;
            }
            //清空table中的空行
            removeEmpty(data);
            if (data.Rows.Count <= 0)
            {
                import.Result = false;
                import.Type = 1;
                import.Message = "Excel无有效数据，请检查导入数据";
                String abnormalJson = JSON.Encode(import);
                Response.Clear();
                Response.Write(abnormalJson);
                Response.End();
                return;
            }

            //获取所有仓库用于表格显示品牌
            Dictionary<string, int> dicHouse = new Dictionary<string, int>();
            foreach (var item in UserInfor.CargoList)
            {
                dicHouse.Add(item.Name, item.ID);
            }
            int abnormalCount = 0;
            string msg = "";
            for (int i = 0; i < data.Rows.Count; i++)
            {
                //验证Excel表格指定列是否缺少数据
                bool isContinue = false;
                for (int j = 0; j < data.Columns.Count - 1; j++)
                {
                    if (string.IsNullOrEmpty((data.Rows[i][j]).ToString()))
                    {
                        isContinue = true;
                        break;
                    }
                }
                if (isContinue)
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行数据有空值\r\n";
                    continue;
                }
                //验证手机号码是否为数字
                //if (!IsNumber(data.Rows[i][5].ToString()))
                //{
                //    abnormalCount++;
                //    msg += "第" + (i + 2) + "行数据手机号码有误\r\n";
                //    continue;
                //}
                ent.Add(new CargoClientAcceptAddressEntity
                {
                    HouseID = clientEntity.HouseID,
                    ClientNum = clientEntity.ClientNum,
                    ClientID = clientEntity.ClientID,
                    AcceptProvince = Convert.ToString(data.Rows[i][0]),
                    AcceptCity = Convert.ToString(data.Rows[i][1]),
                    AcceptCompany = Convert.ToString(data.Rows[i][2]),
                    AcceptPeople = Convert.ToString(data.Rows[i][3]),
                    AcceptCellphone = Convert.ToString(data.Rows[i][4]),
                    AcceptTelephone = Convert.ToString(data.Rows[i][5]),
                    AcceptAddress = Convert.ToString(data.Rows[i][6]),
                });
            }
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "收货地址导入";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            foreach (var it in ent)
            {
                bus.AddAcceptAddress(it, log);
            }
            String json = JSON.Encode(ent);

            if (abnormalCount > 0)
            {
                import.Type = 1;
                import.Message = msg;
            }

            import.Result = true;
            import.Data = json;
            json = JSON.Encode(import);

            Response.Clear();
            Response.Write(json);
            Response.End();

        }
        public void saveFile()
        {
            System.Web.HttpFileCollection files = this.Request.Files;
            if (files == null || files.Count == 0) return;
            string attachmentId = Guid.NewGuid().ToString();
            DataTable data = ToExcel.ImportExcelData(files);
            CargoClientBus bus = new CargoClientBus();

            CargoImportEntity import = new CargoImportEntity();
            import.Result = true;
            import.Data = "";
            import.Message = "";
            import.Type = 0;
            import.ExistCount = 0;

            List<CargoClientEntity> ent = new List<CargoClientEntity>();

            //验证上传excel文件列数是否有效
            if (data.Columns.Count != 13)
            {
                import.Result = false;
                import.Type = 1;
                import.Message = "模板有误或缺少列，请使用指定模板";
                String abnormalJson = JSON.Encode(import);
                Response.Clear();
                Response.Write(abnormalJson);
                Response.End();
                return;
            }
            //清空table中的空行
            removeEmpty(data);
            if (data.Rows.Count <= 0)
            {
                import.Result = false;
                import.Type = 1;
                import.Message = "Excel无有效数据，请检查导入数据";
                String abnormalJson = JSON.Encode(import);
                Response.Clear();
                Response.Write(abnormalJson);
                Response.End();
                return;
            }

            //获取所有仓库用于表格显示品牌
            Dictionary<string, int> dicHouse = new Dictionary<string, int>();
            foreach (var item in UserInfor.CargoList)
            {
                dicHouse.Add(item.Name, item.ID);
            }

            int abnormalCount = 0;

            string msg = "";

            for (int i = 0; i < data.Rows.Count; i++)
            {
                //验证Excel表格指定列是否缺少数据
                bool isContinue = false;
                for (int j = 0; j < data.Columns.Count - 4; j++)
                {
                    if (string.IsNullOrEmpty((data.Rows[i][j]).ToString()))
                    {
                        if (j == 6 || j == 7 || j == 8 || j == 9 || j == 10 || j == 11 || j == 12)
                        {

                        }
                        else
                        {
                            isContinue = true;
                            break;
                        }
                    }
                }
                if (isContinue)
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行数据有空值\r\n";
                    continue;
                }

                //验证导入仓库是否在数据库中有效，无效则跳过
                int houseID = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][0]).Trim()))
                {
                    if (!dicHouse.ContainsKey(Convert.ToString(data.Rows[i][0]).Trim()))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行所属仓库不存在\r\n";
                        continue;
                    }
                    else
                    {
                        dicHouse.TryGetValue(Convert.ToString(data.Rows[i][0]).Trim(), out houseID);
                    }
                }
                //验证客户类型是否有效
                if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][1]).Trim()))
                {
                    if (Convert.ToString(data.Rows[i][1]).Trim() != "门店客户" && Convert.ToString(data.Rows[i][1]).Trim() != "个人客户" && Convert.ToString(data.Rows[i][1]).Trim() != "VIP客户")
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据客户类型有误\r\n";
                        continue;
                    }
                }
                //验证手机号码是否为数字
                if (!IsNumber(data.Rows[i][5].ToString()))
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行数据手机号码有误\r\n";
                    continue;
                }
                //验证目标销量是否为数字
                if (!string.IsNullOrEmpty(data.Rows[i][8].ToString()))
                {
                    if (!IsNumber(data.Rows[i][8].ToString()))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据目标销量有误\r\n";
                        continue;
                    }
                }
                CargoClientEntity ent2 = new CargoClientEntity();
                ent2.ClientName = Convert.ToString(data.Rows[i][2]);
                ent2.ClientShortName = Convert.ToString(data.Rows[i][3]);
                ent2.Boss = Convert.ToString(data.Rows[i][4]);
                ent2.HouseID = houseID;
                ent2.Boss = Convert.ToString(data.Rows[i][4]);
                if (bus.IsExistCargoClient(ent2))
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行数据客户名称已存在\r\n";
                    continue;
                }

                ent.Add(new CargoClientEntity
                {
                    HouseID = houseID,
                    HouseName = Convert.ToString(data.Rows[i][0]),
                    ClientType = Convert.ToString(data.Rows[i][1]).ToUpper() == "VIP客户" ? "2" : Convert.ToString(data.Rows[i][1]) == "门店客户" ? "1" : "0",
                    ClientName = Convert.ToString(data.Rows[i][2]),
                    ClientShortName = Convert.ToString(data.Rows[i][3]),
                    Boss = Convert.ToString(data.Rows[i][4]),
                    Cellphone = Convert.ToString(data.Rows[i][5]),
                    Address = Convert.ToString(data.Rows[i][6]),
                    Telephone = Convert.ToString(data.Rows[i][7]),
                    TargetNum = String.IsNullOrEmpty(data.Rows[i][8].ToString()) ? 0 : Convert.ToInt32(data.Rows[i][8]),
                    ShopCode = Convert.ToString(data.Rows[i][9]),
                    TryeClientCode = Convert.ToString(data.Rows[i][10]),
                    TryeClientType = Convert.ToString(data.Rows[i][11]),
                    Remark = Convert.ToString(data.Rows[i][12])
                });
            }
            String json = JSON.Encode(ent);

            if (abnormalCount > 0)
            {
                import.Type = 1;
                import.Message = msg;
            }

            import.Result = true;
            import.Data = json;
            json = JSON.Encode(import);

            Response.Clear();
            Response.Write(json);
            Response.End();

        }
        /// <summary>
        /// 判断字符串是否为整数或小数
        /// </summary>
        /// <param name="value">验证字符串</param>
        /// <returns></returns>
        public static bool isNumeric(String value)
        {
            System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$");
            if (rex.IsMatch(value))
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool IsNumber(string message)
        {
            System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(message))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 删除导入的table中的空行
        /// </summary>
        /// <param name="dt"></param>
        protected void removeEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool rowdataisnull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {

                        rowdataisnull = false;
                    }
                }
                if (rowdataisnull)
                {
                    removelist.Add(dt.Rows[i]);
                }

            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
        }
        /// <summary>
        /// 保存导入的数据
        /// </summary>
        public void SaveImportData()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            CargoClientBus bus = new CargoClientBus();
            List<CargoClientEntity> list = new List<CargoClientEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "客户管理导入";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoClientEntity
                    {
                        ClientNum = Common.GetClientNum(),
                        HouseID = Convert.ToInt32(row["HouseID"]),
                        ClientType = Convert.ToString(row["ClientType"]).Trim(),
                        ClientName = Convert.ToString(row["ClientName"]).Trim(),
                        ClientShortName = Convert.ToString(row["ClientShortName"]).Trim(),
                        Address = string.IsNullOrEmpty(Convert.ToString(row["Address"]).Trim()) ? "" : Convert.ToString(row["Address"]).Trim(),
                        Telephone = string.IsNullOrEmpty(Convert.ToString(row["Telephone"]).Trim()) ? "" : Convert.ToString(row["Telephone"]).Trim(),
                        Boss = string.IsNullOrEmpty(Convert.ToString(row["Boss"]).Trim()) ? "" : Convert.ToString(row["Boss"]).Trim(),
                        Cellphone = string.IsNullOrEmpty(Convert.ToString(row["Cellphone"]).Trim()) ? "" : Convert.ToString(row["Cellphone"]).Trim(),
                        TargetNum = Convert.ToInt32(row["TargetNum"]),
                        ShopCode = string.IsNullOrEmpty(Convert.ToString(row["ShopCode"]).Trim()) ? "" : Convert.ToString(row["ShopCode"]).Trim(),
                        TryeClientType = string.IsNullOrEmpty(Convert.ToString(row["TryeClientType"]).Trim()) ? "" : Convert.ToString(row["TryeClientType"]).Trim(),
                        TryeClientCode = string.IsNullOrEmpty(Convert.ToString(row["TryeClientCode"]).Trim()) ? "" : Convert.ToString(row["TryeClientCode"]).Trim(),
                        Remark = string.IsNullOrEmpty(Convert.ToString(row["Remark"]).Trim()) ? "" : Convert.ToString(row["Remark"]).Trim(),
                    });
                }
                if (msg.Result)
                {
                    bus.SaveData(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();
        }
        /// <summary>
        /// 保存客户资料
        /// </summary>
        public void SaveInsurcePic()
        {
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "A";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            List<CargoClientFileEntity> result = new List<CargoClientFileEntity>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                string FileType = string.Empty;
                if (Request.Files.AllKeys[i].Equals("InsurePic")) { FileType = "0"; }
                if (Request.Files.AllKeys[i].Equals("TrafficPic")) { FileType = "1"; }
                if (Request.Files.AllKeys[i].Equals("DrivingLicensePic")) { FileType = "2"; }
                if (Request.Files.AllKeys[i].Equals("CarRegisterCardPic")) { FileType = "3"; }
                if (Request.Files.AllKeys[i].Equals("ShopHeadPic")) { FileType = "4"; }
                string imgName = string.Empty;
                string imgPath = string.Empty;
                HttpPostedFile imgFile = Request.Files[i];
                if (string.IsNullOrEmpty(imgFile.FileName)) { continue; }
                Common.SaveUserPic(Request.Files[i], ref imgName, ref imgPath);
                CargoClientFileEntity f = new CargoClientFileEntity();
                f.ClientID = Convert.ToInt64(Request["ClientID"]);
                f.FileName = imgName;
                f.FilePath = imgPath;
                f.FileType = FileType;
                f.ClientNum = Convert.ToInt32(Request["ClientNum"]);
                f.ClientName = Convert.ToString(Request["ClientName"]);
                f.OP_ID = UserInfor.LoginName;
                result.Add(f);
            }
            try
            {
                if (result.Count > 0)
                {
                    CargoClientBus bus = new CargoClientBus();
                    bus.AddClientCredenFile(result, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();
        }
        /// <summary>
        /// 删除照片
        /// </summary>
        public void DelPicInfo()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoClientFileEntity> list = new List<CargoClientFileEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";
            foreach (Hashtable row in rows)
            {
                list.Add(new CargoClientFileEntity
                {
                    FID = Convert.ToInt64(row["FID"]),
                    ClientID = Convert.ToInt64(row["ClientID"]),
                    FileName = Convert.ToString(row["FileName"]),
                    FileType = Convert.ToString(row["FileType"])
                });
            }
            CargoClientBus bus = new CargoClientBus();
            try
            {
                bus.DeleteClientCredenFile(list, log);
                msg.Result = true;
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 查询客户证件资料照片
        /// </summary>
        public void QueryCarInsurceRecord()
        {
            CargoClientFileEntity queryEntity = new CargoClientFileEntity();
            int ClientID = Convert.ToInt32(Request["ClientID"]);
            if (ClientID == 0) { return; }
            queryEntity.ClientID = Convert.ToInt64(ClientID);
            CargoClientBus bus = new CargoClientBus();
            List<CargoClientFileEntity> list = bus.QueryClientCredenFileByID(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 客户证件资料审批
        /// </summary>
        public void SaveClientApprove()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string json = Request["data"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            if (GridRows.Count <= 0)
            {
                msg.Message = "没有客户证件资料数据"; msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
                return;
            }
            long ClientID = Convert.ToInt64(Request["ClientID"]);
            if (ClientID <= 0)
            {
                msg.Message = "客户数据有误"; msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
                return;
            }
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户审批";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "U";
            CargoApproveEntity entity = new CargoApproveEntity();
            List<CargoApproveRelateEntity> relist = new List<CargoApproveRelateEntity>();
            List<CargoApproveFileEntity> fil = new List<CargoApproveFileEntity>();
            CargoFinanceBus fin = new CargoFinanceBus();
            CargoOrderBus b = new CargoOrderBus();
            List<QyUserEntity> qyUser = new List<QyUserEntity>();
            QiyeBus q = new QiyeBus();
            CargoStaticBus statis = new CargoStaticBus();

            CargoClientBus bus = new CargoClientBus();
            CargoClientEntity clientEnt = bus.QueryCargoClient(ClientID);

            #region 处理申请
            entity.ApplyID = UserInfor.LoginName;
            entity.ApplyName = UserInfor.UserName;
            entity.ApplyDate = DateTime.Now;
            entity.Title = Convert.ToString(Request["Title"]);
            entity.Memo = Convert.ToString(Request["memo"]);
            entity.ApplyType = Convert.ToString(Request["ApplyType"]);
            entity.CurrID = UserInfor.LoginName;
            entity.CurrName = UserInfor.UserName;
            entity.CheckTime = DateTime.Now;
            entity.ApplyStatus = "0";//待审
            entity.LimitMoney = string.IsNullOrEmpty(Request["LimitMoney"]) ? 0 : Convert.ToDecimal(Request["LimitMoney"]);//申请额度
            List<CargoApproveSetEntity> configEnt = fin.QueryApproveSet(new CargoApproveSetEntity { ApproveType = Convert.ToString(Request["ApplyType"]), DelFlag = "0", HouseID = clientEnt.HouseID });
            CargoApproveSetEntity AppSet = new CargoApproveSetEntity();
            if (configEnt.Count > 0) { AppSet = configEnt[0]; }
            entity.AppSetID = AppSet.ID;
            //2.审批流程的每一级和当前人的审批角色相匹配
            #region 审批流程
            if (!string.IsNullOrEmpty(AppSet.OneCheckID))
            {
                entity.NextCheckID = AppSet.OneCheckID;//下一审批人
                entity.NextCheckName = AppSet.OneCheckName;
                //判断如果是分公司领导就查找该 分公司领导是谁
                if (AppSet.OneCheckID.Equals("3"))
                {
                    List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                    if (Bosslist.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                        {
                            qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                        }
                        else
                        {
                            entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                            entity.NextCheckName = AppSet.TwoCheckName;
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = clientEnt.HouseID.ToString() });
                        }
                    }
                }
                else if (AppSet.OneCheckID.Equals("7"))
                {
                    //分公司财务
                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, HouseID = clientEnt.HouseID, CheckHouseID = clientEnt.HouseID.ToString() });
                    if (qyUser.Count <= 0)
                    {
                        entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                        entity.NextCheckName = AppSet.TwoCheckName;
                        if (AppSet.TwoCheckID.Equals("3"))
                        {
                            List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                            if (Bosslist.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                {
                                    qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                }
                            }
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = clientEnt.HouseID.ToString() });
                        }
                    }
                }
                else
                {
                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, CheckHouseID = clientEnt.HouseID.ToString() });
                }
            }
            #endregion
            foreach (Hashtable row in GridRows)
            {
                fil.Add(new CargoApproveFileEntity
                {
                    FileName = Convert.ToString(row["FileName"]),
                    FilePath = Convert.ToString(row["FilePath"]),
                    FileType = Convert.ToString(row["FileType"]),
                });
                if (relist.Exists(c => c.RelateID.Equals(Convert.ToInt64(row["ClientID"])))) { continue; }
                relist.Add(new CargoApproveRelateEntity
                {
                    RelateID = Convert.ToInt64(row["ClientID"]),
                    ApplyType = entity.ApplyType
                });
            }
            entity.RelateList = relist;
            entity.FileList = fil;
            entity.OStartTime = entity.OEndTime = DateTime.Now;
            #endregion
            //新增申请
            long AID = statis.AddApplication(entity, log);
            msg.Result = true;
            msg.Message = "新增成功";
            entity.ID = AID;
            try
            {
                //推送企业号通知 推送申请审批通知
                QySendInfoEntity send = new QySendInfoEntity();
                send.agentID = "1000010";//申请审批应用
                send.msgType = msgType.textcard;
                send.url = "http://dlt.neway5.com/QY/qyApplicationCheck.aspx?ApproveID=" + entity.ID.ToString();
                switch (entity.ApplyType)
                {
                    case "8":
                        send.title = "客户资料审批";
                        send.content = "<div></div><div>申请标题：" + entity.Title + "</div><div>客户姓名：" + clientEnt.ClientShortName + " " + clientEnt.Boss + "</div><div>申请额度：" + entity.LimitMoney.ToString("F2") + "元</div><div>所属仓库：" + clientEnt.HouseName + "</div><div>申请内容：" + entity.Memo + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
                        break;
                    default:
                        break;
                }
                foreach (var it in qyUser)
                {
                    send.toUser = it.UserID;
                    //send.toUser = "1000";
                    if (!fin.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, ApproveType = entity.ApplyType, CheckUserID = it.UserID, CheckType = "0" }))
                    {
                        fin.AddApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, CheckUserID = it.UserID, CheckName = it.WxName, ReadStatus = "1", CheckType = "0", ApproveType = entity.ApplyType }, log);
                    }
                    WxQYSendHelper.QiyePushInfo(send);
                    Common.WriteTextLog("推送成功：" + it.WxName + entity.ID.ToString());
                }

            }
            catch (ApplicationException ex)
            {
                msg.Message = "成功";
                msg.Result = true;
            }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Clear();
            Response.Write(ress);
            Response.End();

        }
        public void QueryClientInvoiceHeader()
        {
            CargoClientInvoiceHeaderEntity queryEntity = new CargoClientInvoiceHeaderEntity();
            queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryClientInvoiceHeader(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void QueryClientPostAddress()
        {
            CargoClientPostAddressEntity queryEntity = new CargoClientPostAddressEntity();
            queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryClientPostAddress(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        public void SaveClientType()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoClientEntity> list = new List<CargoClientEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "客户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoClientEntity
                    {
                        ClientID = Convert.ToInt64(row["ClientID"]),
                        ClientNum = Convert.ToInt32(row["ClientNum"]),
                        ClientType = Convert.ToString(Request["MClientType"]),
                    });
                }
                bus.SaveClientType(list, log);
                msg.Result = true;
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
            Response.End();

        }

        #endregion
        #region 收货地址操作方法集合
        /// <summary>
        /// 查询客户的所有收货地址
        /// </summary>
        public void QueryAcceptAddress()
        {
            CargoClientAcceptAddressEntity queryEntity = new CargoClientAcceptAddressEntity();
            if (!string.IsNullOrEmpty(Request["ClientNum"]))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            queryEntity.AcceptCompany = Convert.ToString(Request["AcceptCompany"]);
            queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
            queryEntity.AcceptCellphone = Convert.ToString(Request["AcceptCellphone"]);
            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                queryEntity.CargoPermisID = Convert.ToString(Request["HID"]);
            }
            else
            {
                queryEntity.CargoPermisID = UserInfor.CargoPermisID;
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryAcceptAddress(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 删除客户收货地址
        /// </summary>
        public void DelAcceptAddress()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoClientAcceptAddressEntity> list = new List<CargoClientAcceptAddressEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "收货地址管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoClientAcceptAddressEntity
                    {
                        ADID = Convert.ToInt64(row["ADID"]),
                        ClientID = Convert.ToInt64(row["ClientID"]),
                        ClientNum = Convert.ToInt32(row["ClientNum"]),
                        AcceptPeople = Convert.ToString(row["AcceptPeople"]),
                        AcceptAddress = Convert.ToString(row["AcceptAddress"]),
                        AcceptCellphone = Convert.ToString(row["AcceptCellphone"])
                    });
                }
                bus.DelAcceptAddress(list, log);
                msg.Result = true;
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
            Response.End();
        }
        /// <summary>
        /// 保存客户收货地址
        /// </summary>
        public void SaveAcceptAddress()
        {
            CargoClientAcceptAddressEntity ent = new CargoClientAcceptAddressEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "收货地址管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["ADID"] != null ? Request["ADID"].ToString() : "";
            try
            {
                ent.ClientID = Convert.ToInt64(Request["ClientID"]);
                ent.ClientNum = Convert.ToInt32(Request["ClientNum"]);
                ent.AcceptCompany = Convert.ToString(Request["AcceptCompany"]).Trim();
                ent.AcceptPeople = Convert.ToString(Request["AcceptPeople"]).Trim();
                ent.AcceptProvince = Convert.ToString(Request["AProvince"]);
                ent.AcceptCity = Convert.ToString(Request["ACity"]);
                ent.AcceptCountry = Convert.ToString(Request["ACountry"]);
                ent.AcceptAddress = Convert.ToString(Request["AcceptAddress"]).Trim();
                ent.AcceptCellphone = Convert.ToString(Request["AcceptCellphone"]);
                ent.AcceptTelephone = Convert.ToString(Request["AcceptTelephone"]);
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);

                ent.isDefault = string.IsNullOrEmpty(Convert.ToString(Request["isDefault"])) ? 0 : Convert.ToInt32(Request["isDefault"]);
                ent.TargetNum = string.IsNullOrEmpty(Convert.ToString(Request["TargetNum"])) ? 0 : Convert.ToInt32(Request["TargetNum"]);
                ent.TryeCompany = Convert.ToString(Request["TryeCompany"]);
                ent.TryeClientCode = Convert.ToString(Request["TryeClientCode"]);
                ent.TryeClientType = Convert.ToString(Request["TryeClientType"]);
                Cargo.House.houseApi house = new House.houseApi();
                string[] coordinate = house.geocodeAddress(ent.AcceptProvince, ent.AcceptCity, ent.AcceptAddress);
                if (!string.IsNullOrEmpty(coordinate[0]) && !string.IsNullOrEmpty(coordinate[1]))
                {
                    ent.Longitude = coordinate[0];
                    ent.Latitude = coordinate[1];
                }
                if (id == "")
                {
                    //2000-06-18取消相同收货人判断
                    //if (bus.IsExistAcceptAddress(ent))
                    //{
                    //    msg.Result = false;
                    //    msg.Message = "已经存在相同的收货人";
                    //}
                    //else
                    //{
                    log.Operate = "A";
                    bus.AddAcceptAddress(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                    //}
                }
                else
                {
                    ent.ADID = Convert.ToInt64(id);
                    ent.ClientNum = Convert.ToInt32(Request["ClientNum"]);
                    log.Operate = "U";
                    bus.UpdateAcceptAddress(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";

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
            Response.End();
        }
        #endregion
        #region VIP编码操作方法集合
        /// <summary>
        /// 查询所有的VIP编码 
        /// </summary>
        public void QuerySpecialCode()
        {
            CargoSpecialVIPNumEntity queryEntity = new CargoSpecialVIPNumEntity();
            if (!string.IsNullOrEmpty(Request["sCode"]))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["sCode"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QuerySpecialCode(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询所有编码返回List
        /// </summary>
        public void QuerySpecialCodeList()
        {
            CargoSpecialVIPNumEntity queryEntity = new CargoSpecialVIPNumEntity();
            if (!string.IsNullOrEmpty(Request["sCode"]))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["sCode"]);
            }
            queryEntity.Status = "0";
            //分页
            int pageIndex = 1;
            int pageSize = 10000;
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QuerySpecialCode(pageIndex, pageSize, queryEntity);
            List<CargoSpecialVIPNumEntity> result = (List<CargoSpecialVIPNumEntity>)list["rows"];
            //JSON
            String json = JSON.Encode(result);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 保存特殊客户编码
        /// </summary>
        public void SaveSpecialCode()
        {
            CargoSpecialVIPNumEntity ent = new CargoSpecialVIPNumEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户编码管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            try
            {
                ent.ClientNum = Convert.ToInt32(Request["speCode"]);
                if (bus.IsExistSpecialCode(ent))
                {
                    msg.Result = false;
                    msg.Message = "已经存在相同的客户编码";
                }
                else
                {
                    log.Operate = "A";
                    bus.SaveSpecialCode(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
        /// 删除特殊客户编码
        /// </summary>
        public void DelSpecialCode()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoSpecialVIPNumEntity> list = new List<CargoSpecialVIPNumEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "客户编码管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    //如果已经存在关联客户的客户编码，不予删除
                    if (bus.IsExistCargoClientNum(new CargoClientEntity { ClientNum = Convert.ToInt32(row["ClientNum"]) }))
                    {
                        continue;
                    }
                    list.Add(new CargoSpecialVIPNumEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        ClientNum = Convert.ToInt32(row["ClientNum"]),
                        ClientShortName = Convert.ToString(row["ClientShortName"]),
                        Status = Convert.ToString(row["Status"])
                    });
                }
                bus.DelSpecialCode(list, log);
                msg.Result = true;
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
            Response.End();
        }
        /// <summary>
        /// 解绑特殊的客户编码
        /// </summary>
        public void CancelSpecialCode()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoSpecialVIPNumEntity> list = new List<CargoSpecialVIPNumEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "客户编码管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoSpecialVIPNumEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        ClientNum = Convert.ToInt32(row["ClientNum"]),
                        ClientShortName = Convert.ToString(row["ClientShortName"]),
                        Status = Convert.ToString(row["Status"])
                    });
                }
                bus.CancelSpecialCode(list, log);
                msg.Result = true;
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
            Response.End();
        }
        #endregion
        #region 客户预收款操作方法集合
        /// <summary>
        /// 客户预收款查询
        /// </summary>
        public void QueryPreRecord()
        {
            CargoClientPreRecordEntity queryEntity = new CargoClientPreRecordEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Request["ClientID"]))
            {
                queryEntity.ClientID = Convert.ToInt64(Request["ClientID"]);
            }
            queryEntity.ClientName = Convert.ToString(Request["ClientName"]);
            queryEntity.Boss = Convert.ToString(Request["Boss"]);
            if (!string.IsNullOrEmpty(Request["ClientNum"]))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                //queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
                queryEntity.CargoPermisID = Convert.ToString(Request["HID"]);
            }
            else
            {
                queryEntity.CargoPermisID = UserInfor.CargoPermisID;
            }
            queryEntity.OperaType = Convert.ToString(Request["operaType"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryPreRecord(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        #endregion
        #region 微信服务号客户审核
        /// <summary>
        /// 查询所有微信服务号用户数据
        /// </summary>
        public void QueryWxClientCheckData()
        {
            WXUserEntity queryEntity = new WXUserEntity();
            //if (!string.IsNullOrEmpty(Request["ClientNum"]))
            //{
            //    queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            //}
            queryEntity.Name = Convert.ToString(Request["Name"]);
            queryEntity.CompanyName = Convert.ToString(Request["CompanyName"]);
            queryEntity.wxName = Convert.ToString(Request["wxName"]);
            queryEntity.Cellphone = Convert.ToString(Request["Cellphone"]);
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ClientNum"])))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (Request["dFlag"] != "-1") { queryEntity.IsBind = Convert.ToString(Request["dFlag"]); }
            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryWxClientCheckData(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 导出客户数据
        /// </summary>
        public void QueryWxClientCheckDataForExport()
        {
            WXUserEntity queryEntity = new WXUserEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Name = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.CompanyName = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.wxName = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Cellphone = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.StartDate = Convert.ToDateTime(arr[i].Trim()); }
                        break;
                    case 5:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.EndDate = Convert.ToDateTime(arr[i].Trim()); }
                        break;
                    case 6:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.HouseID = Convert.ToInt32(arr[i].Trim()); }
                        break;
                    case 7:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.ClientNum = Convert.ToInt32(arr[i].Trim()); }
                        break;
                    case 8: if (arr[i] != "-1") { queryEntity.IsBind = Convert.ToString(arr[i]); } break;
                    default:
                        break;
                }
            }

            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 10000 : Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryWxClientCheckData(pageIndex, pageSize, queryEntity);
            string err = "OK";
            List<WXUserEntity> awbList = (List<WXUserEntity>)list["rows"];
            if (awbList.Count > 0) { WxClient = awbList; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public List<WXUserEntity> WxClient
        {
            get
            {
                if (Session["WxClient"] == null)
                {
                    Session["WxClient"] = new List<WXUserEntity>();
                }
                return (List<WXUserEntity>)(Session["WxClient"]);
            }
            set
            {
                Session["WxClient"] = value;
            }
        }

        /// <summary>
        /// 微信用户绑定客户店代码
        /// </summary>
        public void saveWxUserBind()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            WXUserEntity entity = new WXUserEntity();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "微信用户审核";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            CargoClientBus bus = new CargoClientBus();
            foreach (Hashtable row in rows)
            {
                entity.ClientNum = Convert.ToInt32(row["ClientNum"]);
                entity.LogisID = Convert.ToInt32(row["LogisID"]);
                entity.LogicName = Convert.ToString(row["LogisName"]);
                entity.ID = Convert.ToInt64(Request["ID"]);
                entity.Name = Convert.ToString(Request["Name"]);
                entity.wxOpenID = Convert.ToString(Request["wxOpenID"]);
                entity.IsManager = "1";
            }
            bus.saveWxUserBind(entity, log);
            CargoWeiXinBus wbus = new CargoWeiXinBus();
            WXUserEntity wxUser = wbus.QueryWeixinUserByID(new WXUserEntity { ID = entity.ID });
            CargoClientEntity clientEnt = bus.QueryCargoClient(entity.ClientNum);
            if (!clientEnt.ClientID.Equals(0))
            {
                List<CargoClientFileEntity> clientFile = new List<CargoClientFileEntity>();
                if (!string.IsNullOrEmpty(wxUser.BusLicenseImg))
                {
                    string[] img = wxUser.BusLicenseImg.Split('/');
                    clientFile.Add(new CargoClientFileEntity
                    {
                        ClientID = clientEnt.ClientID,
                        FilePath = wxUser.BusLicenseImg,
                        FileName = Convert.ToString(img[img.Length - 1]),
                        FileType = "2",
                        OP_ID = UserInfor.LoginName
                    });
                }
                if (!string.IsNullOrEmpty(wxUser.IDCardImg))
                {
                    string[] img = wxUser.IDCardImg.Split('/');
                    clientFile.Add(new CargoClientFileEntity
                    {
                        ClientID = clientEnt.ClientID,
                        FilePath = wxUser.IDCardImg,
                        FileName = Convert.ToString(img[img.Length - 1]),
                        FileType = "0",
                        OP_ID = UserInfor.LoginName
                    });
                }
                if (!string.IsNullOrEmpty(wxUser.IDCardBackImg))
                {
                    string[] img = wxUser.IDCardBackImg.Split('/');
                    clientFile.Add(new CargoClientFileEntity
                    {
                        ClientID = clientEnt.ClientID,
                        FilePath = wxUser.IDCardBackImg,
                        FileName = Convert.ToString(img[img.Length - 1]),
                        FileType = "1",
                        OP_ID = UserInfor.LoginName
                    });
                }
                bus.AddClientCredenFile(clientFile, log);
            }
            msg.Result = true;
            msg.Message = "成功";
            if (!string.IsNullOrEmpty(entity.wxOpenID))
            {
                //if (clientEnt.HouseID.Equals(101) || clientEnt.HouseID.Equals(135))
                //{
                //    //揭阳云仓和汕头云仓新注册用户返券
                //    bus.AddCoupon(new WXCouponEntity { WXID = entity.ID, Piece = 4, Money = 10, UseStatus = "0", GainDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), TypeID = "338,539,582", TypeName = "驰风,康佩森,狄安祺达", CouponType = "1", SuppClientNum = 542207, IsSuperPosition = "1", IsFollowQuantity = "0" }, log);
                //}
                //else if (clientEnt.HouseID.Equals(129) || clientEnt.HouseID.Equals(107) || clientEnt.HouseID.Equals(93) || clientEnt.HouseID.Equals(131))
                //{
                //    //花都云仓和东平，从化云仓新注册用户返券
                //    bus.AddCoupon(new WXCouponEntity { WXID = entity.ID, Piece = 3, Money = 10, UseStatus = "0", GainDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), TypeID = "338,539,582", TypeName = "驰风,康佩森,狄安祺达", CouponType = "1", SuppClientNum = 542207, IsSuperPosition = "1", IsFollowQuantity = "0" }, log);
                //}
                //else if (clientEnt.HouseID.Equals(136))
                //{
                //    //南宁云仓新注册用户返券
                //    bus.AddCoupon(new WXCouponEntity { WXID = entity.ID, Piece = 10, Money = 10, UseStatus = "0", GainDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), TypeID = "582,539,338,34,31", TypeName = "狄安祺达,康佩森,驰风,马牌,韩泰", CouponType = "1", SuppClientNum = 542207, IsSuperPosition = "1", IsFollowQuantity = "1" }, log);
                //}
                //else if (clientEnt.HouseID.Equals(133))
                //{
                //    //海南秀英云仓新注册用户返券
                //    bus.AddCoupon(new WXCouponEntity { WXID = entity.ID, Piece = 300, Money = 10, UseStatus = "0", GainDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(90), TypeID = "582", TypeName = "狄安祺达", CouponType = "1", SuppClientNum = 542207, IsSuperPosition = "1", IsFollowQuantity = "1" }, log);
                //}
                //try
                //{
                //    string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());
                //    var data = new
                //    {
                //        touser = entity.wxOpenID,
                //        msgtype = "text",
                //        text = new
                //        {
                //            content = "恭喜您：" + entity.Name + "，您提交的信息审核已通过，您的店代码是：" + entity.ClientNum + "！欢迎在商城下单购物！"
                //        }
                //    };
                //    string URL_FORMAT = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
                //    WxJsonResult wxRe = CommonJsonSend.Send(token, URL_FORMAT, data);
                //}
                //catch (ApplicationException ex)
                //{
                //    msg.Message = ex.Message;
                //    msg.Result = true;
                //}
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 保存拒审
        /// </summary>
        public void saveDenyReason()
        {
            String idStr = Request["data"];
            String DenyReason = Request["reason"];
            if (String.IsNullOrEmpty(idStr)) return;
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            WXUserEntity list = new WXUserEntity();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "微信用户审核";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Memo = "拒审成功 ";
            foreach (Hashtable row in rows)
            {
                if (!Convert.ToString(row["ClientNum"]).Equals("0"))
                {
                    msg.Message = "已绑定店代码不允许拒审";
                    msg.Result = false;
                    break;
                }
                list.ID = Convert.ToInt64(row["ID"]);
                list.wxName = Convert.ToString(row["wxName"]);
                list.wxOpenID = Convert.ToString(row["wxOpenID"]);
                list.DenyReason = DenyReason;
            }
            if (msg.Result)
            {
                CargoClientBus bus = new CargoClientBus();
                bus.saveDenyReason(list, log);
                try
                {
                    //string token = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(ConfigurationManager.AppSettings["dltAPPID"], ConfigurationManager.AppSettings["dltAppSecret"], false);
                    string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());
                    var data = new
                    {
                        touser = list.wxOpenID,
                        msgtype = "text",
                        text = new
                        {
                            content = list.wxName + "，您提交的信息审核未通过！" + DenyReason
                        }
                    };
                    string URL_FORMAT = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
                    WxJsonResult wxRe = CommonJsonSend.Send(token, URL_FORMAT, data);
                }
                catch (ApplicationException ex)
                {
                    msg.Message = ex.Message;
                    msg.Result = true;
                }
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 解绑店代码 
        /// </summary>
        public void saveWxUserUnBind()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<WXUserEntity> list = new List<WXUserEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "微信用户审核";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            CargoClientBus bus = new CargoClientBus();
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new WXUserEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Name = Convert.ToString(row["Name"]),
                        wxName = Convert.ToString(row["wxName"]),
                        wxOpenID = Convert.ToString(row["wxOpenID"]),
                        IsManager = "0",
                        ClientNum = 0
                    });
                }
                bus.saveWxUserUnBind(list, log);
                msg.Result = true;
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
            Response.End();
        }
        #endregion
        #region 客户账单管理
        /// <summary>
        /// 保存客户账单
        /// </summary>
        public void saveOrderAccount()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            CargoClientAccountEntity ent = new CargoClientAccountEntity();
            List<string> entAwb = new List<string>();
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户账单管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoHouseBus house = new CargoHouseBus();
            try
            {
                string id = string.IsNullOrEmpty(Convert.ToString(Request["AccountID"])) ? "" : Request["AccountID"].ToString();
                #region 赋值
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                CargoHouseEntity houseEnt = house.QueryCargoHouseByID(ent.HouseID);
                ent.AccountTitle = Convert.ToString(Request["AccountTitle"]);
                ent.ClientID = string.IsNullOrEmpty(Convert.ToString(Request["ClientID"])) ? 0 : Convert.ToInt64(Request["ClientID"]);
                ent.ClientNum = string.IsNullOrEmpty(Convert.ToString(Request["ClientNum"])) ? 0 : Convert.ToInt32(Request["ClientNum"]);
                ent.ClientName = Convert.ToString(Request["ClientName"]);
                //ent.ARMoney = string.IsNullOrEmpty(Convert.ToString(Request["ARMoney"])) ? 0 : Convert.ToDecimal(Request["ARMoney"]);//总计
                ent.Total = string.IsNullOrEmpty(Convert.ToString(Request["Total"])) ? 0 : Convert.ToDecimal(Request["Total"]);//总计
                //ent.CollectMoney = string.IsNullOrEmpty(Convert.ToString(Request["CollectMoney"])) ? 0 : Convert.ToDecimal(Request["CollectMoney"]);//代收
                //ent.TaxFee = string.IsNullOrEmpty(Convert.ToString(Request["TaxFee"])) ? 0 : Convert.ToDecimal(Request["TaxFee"]);//税费
                //ent.OtherFee = string.IsNullOrEmpty(Convert.ToString(Request["OtherFee"])) ? 0 : Convert.ToDecimal(Request["OtherFee"]);//其它费用
                ent.CreateDate = DateTime.Now;
                ent.OPID = UserInfor.LoginName.Trim();
                ent.Memo = Convert.ToString(Request["Memo"]);
                ent.Status = "0";
                ent.AType = "0";
                ent.CheckStatus = "0";
                #endregion
                if (string.IsNullOrEmpty(id))//新增
                {
                    log.Memo = "新增账单成功";
                    log.Operate = "A";
                    ent.AccountID = Common.GetMaxAccountNo(houseEnt.HouseCode, ent.HouseID);
                    foreach (Hashtable grid in GridRows)
                    {
                        entAwb.Add(Convert.ToString(grid["OrderNo"]));
                    }
                    ent.OrderNoList = entAwb;
                    bus.AddCargoAccount(ent, log);
                }
                else//修改
                {
                    log.Operate = "U";
                    log.Memo = "修改账单成功";
                    ent.AccountID = id;
                    foreach (Hashtable grid in GridRows)
                    {
                        entAwb.Add(Convert.ToString(grid["OrderNo"]));
                    }
                    ent.OrderNoList = entAwb;
                    bus.UpdateCargoAccount(ent, log);
                }
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
            Response.End();
        }
        /// <summary>
        /// 查询客户账单
        /// </summary>
        public void QueryBillManager()
        {
            CargoClientAccountEntity queryEntity = new CargoClientAccountEntity();
            if (!string.IsNullOrEmpty(Request["ClientID"]))
            {
                queryEntity.ClientID = Convert.ToInt64(Request["ClientID"]);
            }
            queryEntity.AccountID = Convert.ToString(Request["AccountID"]);
            if (!string.IsNullOrEmpty(Request["HouseID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]);
            }
            if (Request["Status"] != "-1")
            {
                queryEntity.Status = Convert.ToString(Request["Status"]);
            }
            if (Request["CheckStatus"] != "-1")
            {
                queryEntity.CheckStatus = Convert.ToString(Request["CheckStatus"]);
            }
            if (Request["ElecSign"] != "-1")
            {
                queryEntity.ElecSign = Convert.ToString(Request["ElecSign"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryBillManager(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 导出客户账单 
        /// </summary>
        public void QueryBillManagerForExport()
        {
            CargoClientAccountEntity queryEntity = new CargoClientAccountEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.AccountID = Convert.ToString(arr[i]); }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.HouseID = Convert.ToInt32(arr[i]); }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.ClientID = Convert.ToInt64(arr[i]); }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.StartDate = Convert.ToDateTime(arr[i]); }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.EndDate = Convert.ToDateTime(arr[i]); }
                        break;
                    case 5:
                        if (!arr[i].Equals("-1")) { queryEntity.Status = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 6:
                        if (!arr[i].Equals("-1")) { queryEntity.ElecSign = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 7:
                        if (!arr[i].Equals("-1")) { queryEntity.CheckStatus = Convert.ToString(arr[i].Trim()); }
                        break;
                    default:
                        break;
                }
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 10000 : Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryBillManager(pageIndex, pageSize, queryEntity);
            string err = "OK";
            List<CargoClientAccountEntity> awbList = (List<CargoClientAccountEntity>)list["rows"];
            if (awbList.Count > 0) { CargoClientAccountList = awbList; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        public List<CargoClientAccountEntity> CargoClientAccountList
        {
            get
            {
                if (Session["CargoClientAccountList"] == null)
                {
                    Session["CargoClientAccountList"] = new List<CargoClientAccountEntity>();
                }
                return (List<CargoClientAccountEntity>)(Session["CargoClientAccountList"]);
            }
            set
            {
                Session["CargoClientAccountList"] = value;
            }
        }
        /// <summary>
        /// 删除账单
        /// </summary>
        public void DelCargoAccount()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoClientAccountEntity> list = new List<CargoClientAccountEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "客户账单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    CargoClientAccountEntity account = bus.QueryCargoClientAccount(new CargoClientAccountEntity { AccountID = Convert.ToString(row["AccountID"]) });
                    if (account.Status.Equals("1"))
                    {
                        msg.Result = false;
                        msg.Message = "账单号" + Convert.ToString(row["AccountID"]) + "已审核";
                        break;
                    }
                    if (account.Status.Equals("3"))
                    {
                        msg.Result = false;
                        msg.Message = "账单号" + Convert.ToString(row["AccountID"]) + "审核结束";
                        break;
                    }
                    if (account.CheckStatus.Equals("1"))
                    {
                        msg.Result = false;
                        msg.Message = "账单号" + Convert.ToString(row["AccountID"]) + "已结算";
                        break;
                    }
                    list.Add(new CargoClientAccountEntity
                    {
                        ClientID = Convert.ToInt64(row["ClientID"]),
                        ClientNum = Convert.ToInt32(row["ClientNum"]),
                        HouseID = Convert.ToInt32(row["HouseID"]),
                        AccountID = Convert.ToString(row["AccountID"]),
                        ClientName = Convert.ToString(row["ClientName"]),
                        HouseName = Convert.ToString(row["HouseName"]),
                        AccountTitle = Convert.ToString(row["AccountTitle"]),
                        OPID = UserInfor.LoginName,
                        Total = Convert.ToDecimal(row["Total"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelCargoAccount(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();
        }
        /// <summary>
        /// 根据账单号查询订单数据
        /// </summary>
        public void QueryOrderByAccountNo()
        {
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            queryEntity.AccountNo = Convert.ToString(Request["AccountNo"]);
            queryEntity.FinanceSecondCheck = "1";
            queryEntity.FromTO = "0";
            queryEntity.RType = "0";

            CargoFinanceBus bus = new CargoFinanceBus();
            List<CargoOrderEntity> list = bus.QueryOrderForCash(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询客户应收应付记录
        /// </summary>
        public void QueryClientAccountPay()
        {
            CargoClientAccountEntity queryEntity = new CargoClientAccountEntity();
            queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            CargoClientBus bus = new CargoClientBus();
            List<CargoClientAccountEntity> list = bus.QueryClientAccountPay(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        #endregion
        #region 上游客户管理
        /// <summary>
        /// 上游客户查询
        /// </summary>
        public void QueryUpClientData()
        {
            CargoUpClientEntity queryEntity = new CargoUpClientEntity();
            queryEntity.UpClientName = Convert.ToString(Request["UpClientName"]);
            queryEntity.UpClientShortName = Convert.ToString(Request["UpClientShortName"]);
            queryEntity.ClientType = Convert.ToInt32(Request["ClientType"]);
            queryEntity.Address = Convert.ToString(Request["Address"]);
            queryEntity.Telephone = Convert.ToString(Request["Telephone"]);
            queryEntity.Boss = Convert.ToString(Request["Boss"]);
            queryEntity.Cellphone = Convert.ToString(Request["Cellphone"]);
            queryEntity.DelFlag = Convert.ToInt32(Request["DelFlag"]);
            queryEntity.Remark = Convert.ToString(Request["Remark"]);
            queryEntity.Province = Convert.ToString(Request["Province"]);
            queryEntity.City = Convert.ToString(Request["City"]);
            queryEntity.Country = Convert.ToString(Request["Country"]);
            queryEntity.HouseID = Convert.ToString(Request["HouseID"]);

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryUpClientData(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void QeurylistUpClientData()
        {
            CargoClientBus bus = new CargoClientBus();
            //List<CargoUpClientEntity> result = bus.QueryUpClientData();
            string UpClientID = Request["UpClientID"];
            List<CargoUpClientEntity> result = bus.QueryUpClientData(UpClientID);
            String json = JSON.Encode(result);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 删除上游客户信息
        /// </summary>
        public void DelUpClientData()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoUpClientEntity> list = new List<CargoUpClientEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "上游客户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    CargoUpClientEntity queryEntity = new CargoUpClientEntity();
                    queryEntity.UpClientID = Convert.ToInt64(row["UpClientID"]);
                    Hashtable UpClientDepList = bus.QueryUpClientDepData(1, 1, queryEntity);
                    if (Convert.ToInt32(UpClientDepList["total"]) > 0)
                    {
                        msg.Message = "上游客户[" + Convert.ToString(row["UpClientName"]) + "]存在部门数据无法直接删除";
                        msg.Result = false;
                        break;
                    }
                    list.Add(new CargoUpClientEntity
                    {
                        UpClientID = Convert.ToInt64(row["UpClientID"]),
                        UpClientName = Convert.ToString(row["UpClientName"]),
                        UpClientShortName = Convert.ToString(row["UpClientShortName"]),
                        ClientType = Convert.ToInt32(row["ClientType"]),
                        Boss = Convert.ToString(row["Boss"]),
                        Cellphone = Convert.ToString(row["Cellphone"]),
                        DelFlag = Convert.ToInt32(row["DelFlag"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelUpClientData(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();

        }
        /// <summary>
        /// 保存上游客户信息
        /// </summary>
        public void SaveUpClientData()
        {
            CargoUpClientEntity ent = new CargoUpClientEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "上游客户管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["UpClientID"] != null ? Request["UpClientID"].ToString() : "";
            try
            {
                ent.UpClientName = Convert.ToString(Request["UpClientName"]).Trim();
                ent.UpClientShortName = Convert.ToString(Request["UpClientShortName"]).Trim();
                ent.ClientType = Convert.ToInt32(Request["ClientType"]);
                ent.Address = Convert.ToString(Request["Address"]).Trim();
                ent.ZipCode = Convert.ToString(Request["ZipCode"]);
                ent.Telephone = Convert.ToString(Request["Telephone"]).Trim();
                ent.Boss = Convert.ToString(Request["Boss"]).Trim();
                ent.Cellphone = Convert.ToString(Request["Cellphone"]).Trim();
                ent.DelFlag = Convert.ToInt32(Request["DelFlag"]);
                ent.Remark = Convert.ToString(Request["Remark"]).Trim();
                ent.Province = Convert.ToString(Request["eProvince"]);
                ent.City = Convert.ToString(Request["eCity"]);
                ent.Country = Convert.ToString(Request["eCountry"]);
                ent.HouseID = Convert.ToString(Request["HouseID"]);
                if (id == "")
                {
                    if (bus.IsExistUpClient(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在相同的上游公司全称/简称";
                    }
                    else
                    {
                        log.Operate = "A";
                        bus.AddUpClient(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                }
                else
                {
                    ent.UpClientID = Convert.ToInt64(id);
                    if (bus.IsExistUpClient(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在相同的上游公司全称/简称";
                    }
                    else
                    {
                        ent.UpClientID = Convert.ToInt32(Request["UpClientID"]);
                        log.Operate = "U";
                        bus.UpdateUpClient(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
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
            Response.End();
        }
        /// <summary>
        /// 上游客户部门查询
        /// </summary>
        public void QueryClientDep()
        {
            CargoUpClientEntity queryEntity = new CargoUpClientEntity();
            queryEntity.DepName = Convert.ToString(Request["DepName"]);
            if (!string.IsNullOrEmpty(Convert.ToString(Request["UpClientID"])))
            {
                queryEntity.UpClientID = Convert.ToInt64(Request["UpClientID"]);
            }

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryUpClientDepData(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 删除上游客户信息
        /// </summary>
        public void DelUpClientDepData()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoUpClientEntity> list = new List<CargoUpClientEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "上游客户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoUpClientEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        UpClientName = Convert.ToString(row["UpClientName"]),
                        UpClientShortName = Convert.ToString(row["UpClientShortName"]),
                        ClientType = Convert.ToInt32(row["ClientType"]),
                        Boss = Convert.ToString(row["Boss"]),
                        Cellphone = Convert.ToString(row["Cellphone"])
                    });
                }
                bus.DelUpClientDep(list, log);
                msg.Result = true;
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
            Response.End();

        }
        /// <summary>
        /// 保存上游客户信息
        /// </summary>
        public void SaveUpClientDepData()
        {
            CargoUpClientEntity ent = new CargoUpClientEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "上游客户管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["UpClientDepID"] != null ? Request["UpClientDepID"].ToString() : "";
            try
            {
                ent.UpClientID = Convert.ToInt64(Request["eUpClientID"]);
                ent.DepName = Convert.ToString(Request["DepName"]).Trim();
                ent.SmallStockCheckFee = Convert.ToDouble(Request["SmallStockCheckFee"]);
                ent.BigStockCheckFee = Convert.ToDouble(Request["BigStockCheckFee"]);
                ent.MarketType = Convert.ToString(Request["MarketType"]);
                ent.OP_ID = UserInfor.LoginName;
                ent.OP_Name = UserInfor.UserName;
                if (id == "")
                {
                    if (bus.IsExistUpClientDep(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在相同的部门名称";
                    }
                    else
                    {
                        log.Operate = "A";
                        bus.AddUpClientDep(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                }
                else
                {
                    ent.ID = Convert.ToInt32(id);
                    if (bus.IsExistUpClientDep(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在相同的部门名称";
                    }
                    else
                    {
                        log.Operate = "U";
                        bus.UpdateUpClientDep(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
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
            Response.End();
        }
        /// <summary>
        /// 获取所有客户部门
        /// </summary>
        public void QueryAllUpClientDep()
        {
            CargoClientBus bus = new CargoClientBus();
            CargoUpClientEntity entity = new CargoUpClientEntity();

            List<CargoUpClientEntity> list = new List<CargoUpClientEntity>();
            entity.HouseID = Request.QueryString["houseID"];
            if (!string.IsNullOrEmpty(entity.HouseID) && entity.HouseID.Contains(","))
            {
                entity.HouseIDs= Request.QueryString["houseID"];
                entity.HouseID = null;
            }
            list = bus.QueryAllUpClientDep(entity);

            string type = Request.QueryString["type"];

            if (!string.IsNullOrEmpty(type))
            {
                list.Insert(0, new CargoUpClientEntity
                {
                    ID = -1,
                    DepName = "全部"
                });
            }
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            //Response.End();
        }
        #endregion
        #region 采购商管理

        public void QueryDeliveryAddress()
        {
            CargoPurchaserDeliveryAddressEntity queryEntity = new CargoPurchaserDeliveryAddressEntity();
            if (!string.IsNullOrEmpty(Request["PurchaserID"]))
            {
                queryEntity.PurchaserID = Convert.ToInt32(Request["PurchaserID"]);
            }
            queryEntity.DeliveryName = Convert.ToString(Request["DeliveryName"]);
            queryEntity.DeliveryBoss = Convert.ToString(Request["DeliveryBoss"]);
            queryEntity.DeliveryCellphone = Convert.ToString(Request["DeliveryCellphone"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryDeliveryAddress(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void DelDeliveryAddress()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoPurchaserDeliveryAddressEntity> list = new List<CargoPurchaserDeliveryAddressEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购商管理";
            log.Status = "0";
            log.NvgPage = "提货地址管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoPurchaserDeliveryAddressEntity
                    {
                        DAID = Convert.ToInt64(row["DAID"]),
                        PurchaserID = Convert.ToInt64(row["PurchaserID"]),
                        PurchaserName = Convert.ToString(row["PurchaserName"]),
                        DeliveryBoss = Convert.ToString(row["DeliveryBoss"]),
                        DeliveryAddress = Convert.ToString(row["DeliveryAddress"]),
                        DeliveryCellphone = Convert.ToString(row["DeliveryCellphone"])
                    });
                }
                bus.DelDeliveryAddress(list, log);
                msg.Result = true;
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
            Response.End();
        }
        public void SaveDeliveryAddress()
        {
            CargoPurchaserDeliveryAddressEntity ent = new CargoPurchaserDeliveryAddressEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "收货地址管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["DAID"] != null ? Request["DAID"].ToString() : "";
            try
            {
                ent.PurchaserID = Convert.ToInt64(Request["PurchaserID"]);
                ent.DeliveryName = Convert.ToString(Request["DeliveryName"]);
                ent.DeliveryAddress = Convert.ToString(Request["DeliveryAddress"]).Trim();
                ent.DeliveryTelephone = Convert.ToString(Request["DeliveryTelephone"]).Trim();
                ent.DeliveryBoss = Convert.ToString(Request["DeliveryBoss"]);
                ent.DeliveryCellphone = Convert.ToString(Request["DeliveryCellphone"]);
                ent.DeliveryRemark = Convert.ToString(Request["DeliveryRemark"]);
                ent.DeliveryCity = Convert.ToString(Request["ACity"]).Trim();
                ent.DeliveryCountry = Convert.ToString(Request["ACountry"]);
                ent.DeliveryProvince = Convert.ToString(Request["AProvince"]).Trim();
                ent.Longitude = Convert.ToString(Request["Longitude"]);
                ent.Latitude = Convert.ToString(Request["Latitude"]);

                Cargo.House.houseApi house = new House.houseApi();
                string[] coordinate = house.geocodeAddress(ent.DeliveryProvince, ent.DeliveryCity, ent.DeliveryAddress);
                if (!string.IsNullOrEmpty(coordinate[0]) && !string.IsNullOrEmpty(coordinate[1]))
                {
                    ent.Longitude = coordinate[0];
                    ent.Latitude = coordinate[1];
                }
                if (id == "")
                {
                    log.Operate = "A";
                    bus.AddDeliveryAddress(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
                else
                {
                    ent.DAID = Convert.ToInt64(id);
                    log.Operate = "U";
                    bus.UpdateDeliveryAddress(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";

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
            Response.End();
        }
        public void AutoCompletePurchaser()
        {
            CargoPurchaserEntity queryEntity = new CargoPurchaserEntity();
            queryEntity.DelFlag = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(Request["houseID"])))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["houseID"]);
            }
            else
            {
                queryEntity.HouseIDStr = UserInfor.CargoPermisID;
            }
            CargoClientBus bus = new CargoClientBus();
            List<CargoPurchaserEntity> list = new List<CargoPurchaserEntity>();
            list = bus.AutoCompletePurchaser(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            //Response.End();
        }
        /// <summary>
        /// 客户查询
        /// </summary>
        public void QueryCargoPurchaser()
        {
            CargoPurchaserEntity queryEntity = new CargoPurchaserEntity();
            queryEntity.PurchaserName = Convert.ToString(Request["PurchaserName"]).Trim();
            queryEntity.PurchaserShortName = Convert.ToString(Request["ShortName"]).Trim();
            queryEntity.Boss = Convert.ToString(Request["Boss"]).Trim();
            queryEntity.Address = Convert.ToString(Request["Address"]).Trim();
            queryEntity.Telephone = Convert.ToString(Request["Telephone"]);
            queryEntity.Cellphone = Convert.ToString(Request["Cellphone"]);
            queryEntity.DelFlag = Convert.ToInt32(Request["DelFlag"]);

            queryEntity.PurchaserType = Convert.ToInt32(Request["PurchaserType"]);


            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryCargoPurchaser(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void QueryCargoPurchaserForExport()
        {
            CargoPurchaserEntity queryEntity = new CargoPurchaserEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.PurchaserName = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Address = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Boss = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.Cellphone = Convert.ToString(arr[i].Trim()); }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.DelFlag = Convert.ToInt32(arr[i].Trim()); }
                        break;
                    default:
                        break;
                }
            }

            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 10000 : Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryCargoPurchaser(pageIndex, pageSize, queryEntity);
            string err = "OK";
            List<CargoPurchaserEntity> awbList = (List<CargoPurchaserEntity>)list["rows"];
            if (awbList.Count > 0) { QueryCargoPurchaserList = awbList; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public List<CargoPurchaserEntity> QueryCargoPurchaserList
        {
            get
            {
                if (Session["QueryCargoPurchaserList"] == null)
                {
                    Session["QueryCargoPurchaserList"] = new List<CargoPurchaserEntity>();
                }
                return (List<CargoPurchaserEntity>)(Session["QueryCargoPurchaserList"]);
            }
            set
            {
                Session["QueryCargoPurchaserList"] = value;
            }
        }
        /// <summary>
        /// 保存客户信息
        /// </summary>
        public void SaveCargoPurchaser()
        {
            CargoPurchaserEntity ent = new CargoPurchaserEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购商管理";
            log.NvgPage = "采购商管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["PurchaserID"] != null ? Request["PurchaserID"].ToString() : "";
            try
            {
                ent.PurchaserName = Convert.ToString(Request["PurchaserName"]).Trim();
                ent.PurchaserShortName = Convert.ToString(Request["PurchaserShortName"]).Trim();
                ent.PurchaserType = Convert.ToInt32(Request["PurchaserType"]);
                ent.Address = Convert.ToString(Request["Address"]).Trim();
                ent.Telephone = Convert.ToString(Request["Telephone"]);
                ent.Boss = Convert.ToString(Request["Boss"]).Trim();
                ent.Cellphone = Convert.ToString(Request["Cellphone"]).Trim();
                ent.DelFlag = Convert.ToInt32(Request["DelFlag"]);
                ent.Remark = Convert.ToString(Request["Remark"]).Trim();
                ent.City = Convert.ToString(Request["ACity"]);
                ent.Country = Convert.ToString(Request["ACountry"]);
                ent.Address = Convert.ToString(Request["Address"]).Trim();
                ent.Province = Convert.ToString(Request["AProvince"]).Trim();
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                ent.Longitude = String.IsNullOrEmpty(Request["Longitude"]) ? "" : Convert.ToString(Request["Longitude"]).Trim();
                ent.Latitude = String.IsNullOrEmpty(Request["Latitude"]) ? "" : Convert.ToString(Request["Latitude"]);

                ent.SocialCreditCode = String.IsNullOrEmpty(Request["SocialCreditCode"]) ? "" : Convert.ToString(Request["SocialCreditCode"]);
                ent.LegalPerson = String.IsNullOrEmpty(Request["LegalPerson"]) ? "" : Convert.ToString(Request["LegalPerson"]);
                ent.SupplierEvaluation = String.IsNullOrEmpty(Request["SupplierEvaluation"]) ? "" : Convert.ToString(Request["SupplierEvaluation"]);
                if (id == "")
                {
                    if (bus.IsExistCargoPurchaser(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在相同的采购商名称/负责人名";
                    }
                    else
                    {
                        log.Operate = "A";
                        bus.AddCargoPurchaser(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                }
                else
                {
                    ent.PurchaserID = Convert.ToInt64(id);
                    if (bus.IsExistCargoPurchaser(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在相同的采购商名称/负责人名";
                    }
                    else
                    {
                        log.Operate = "U";
                        bus.UpdateCargoPurchaser(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
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
            Response.End();
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        public void DelCargoPurchaser()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoPurchaserEntity> list = new List<CargoPurchaserEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购商管理";
            log.Status = "0";
            log.NvgPage = "采购商管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoPurchaserEntity
                    {
                        PurchaserID = Convert.ToInt64(row["PurchaserID"]),
                        PurchaserName = Convert.ToString(row["PurchaserName"]),
                        PurchaserType = Convert.ToInt32(row["PurchaserType"]),
                        Cellphone = Convert.ToString(row["Cellphone"]),
                        Boss = Convert.ToString(row["Boss"]),
                        Telephone = Convert.ToString(row["Telephone"]),
                        DelFlag = Convert.ToInt32(row["DelFlag"])
                    });
                }
                bus.DelCargoPurchaser(list, log);
                msg.Result = true;
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
            Response.End();
        }
        #endregion
        #region 优惠券
        public void QueryWXClient()
        {
            WXCouponEntity queryEntity = new WXCouponEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["houseID"])))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["houseID"]);
            }
            else
            {
                queryEntity.HouseID = UserInfor.HouseID;
            }
            CargoClientBus bus = new CargoClientBus();
            List<WXCouponEntity> list = new List<WXCouponEntity>();
            list = bus.QueryWXClient(queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void QueryCoupon()
        {
            WXCouponEntity queryEntity = new WXCouponEntity();
            if (!string.IsNullOrEmpty(Request["ClientNum"]))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            queryEntity.ClientName = Convert.ToString(Request["ClientName"]);
            queryEntity.CompanyName = Convert.ToString(Request["CompanyName"]);
            if (Request["UseStatus"] != "-1")
            {
                queryEntity.UseStatus = Convert.ToString(Request["UseStatus"]);
            }
            if (!string.IsNullOrEmpty(Request["HouseID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryCouponList(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void SaveCoupon()
        {
            WXCouponEntity ent = new WXCouponEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "优惠券管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {

                if (id == "")
                {
                    ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                    if (string.IsNullOrEmpty(Convert.ToString(Request["CompanyName"])))
                    {
                        //针对仓库所有客户发放优惠券
                        List<WXCouponEntity> houseClient = bus.QueryWXClient(new WXCouponEntity { HouseID = ent.HouseID });
                        foreach (var hclient in houseClient)
                        {
                            ent = new WXCouponEntity();
                            ent.Money = Convert.ToDecimal(Request["Money"]);
                            ent.StartDate = Convert.ToDateTime(Request["StartDate"]);
                            ent.EndDate = Convert.ToDateTime(Request["EndDate"]);
                            ent.Remark = Convert.ToString(Request["Remark"]);
                            ent.IsSuperPosition = Convert.ToString(Request["IsSuperPosition"]);
                            ent.IsFollowQuantity = string.IsNullOrEmpty(Convert.ToString(Request["IsFollowQuantity"])) ? "0" : Convert.ToString(Request["IsFollowQuantity"]);
                            ent.WXID = hclient.WXID;
                            ent.Piece = Convert.ToInt32(Request["Piece"]);
                            log.Operate = "A";
                            bus.AddCoupon(ent, log);
                            msg.Result = true;
                            msg.Message = "成功";
                        }
                    }
                    else
                    {
                        ent.Money = Convert.ToDecimal(Request["Money"]);
                        ent.StartDate = Convert.ToDateTime(Request["StartDate"]);
                        ent.EndDate = Convert.ToDateTime(Request["EndDate"]);
                        ent.Remark = Convert.ToString(Request["Remark"]);
                        ent.IsSuperPosition = Convert.ToString(Request["IsSuperPosition"]);
                        ent.IsFollowQuantity = string.IsNullOrEmpty(Convert.ToString(Request["IsFollowQuantity"])) ? "0" : Convert.ToString(Request["IsFollowQuantity"]);
                        ent.WXID = Convert.ToInt32(Request["CompanyName"]);
                        ent.Piece = Convert.ToInt32(Request["Piece"]);
                        log.Operate = "A";
                        bus.AddCoupon(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                }
                else
                {
                    log.Operate = "U";
                    ent.ID = Convert.ToInt32(id);
                    bus.UpdateCoupon(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();
        }
        public void DelCoupon()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<WXCouponEntity> list = new List<WXCouponEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "优惠券管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new WXCouponEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Money = Convert.ToDecimal(row["Money"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelCoupon(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();
        }
        #endregion
        #region 

        public void QuerySettleHouse()
        {
            CargoSettleHouseEntity queryEntity = new CargoSettleHouseEntity();
            if (!string.IsNullOrEmpty(Request["ClientNum"]))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QuerySettleHouseList(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void SaveSettleHouse()
        {
            CargoSettleHouseEntity ent = new CargoSettleHouseEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "入驻仓库管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                ent.ClientID = Convert.ToInt32(Request["SettleHouseClientID"]);
                ent.ClientNum = Convert.ToInt32(Request["SettleHouseClientNum"]);
                ent.SettleHouseID = Convert.ToString(Request["SettleHouseID"]);
                ent.SettleHouseName = Convert.ToString(Request["SettleHouseName"]);
                ent.ClientTypeID = Convert.ToString(Request["ClientTypeID"]);
                ent.ClientTypeName = Convert.ToString(Request["ClientTypeName"]);
                List<CargoSupplierProductPrice> productPrices = new List<CargoSupplierProductPrice>();
                if (id == "")
                {
                    List<CargoProductSpecEntity> specList = bus.QueryProductSpecList(new CargoProductEntity { TypeIDStr = ent.ClientTypeID, UpClientID = 1 });
                    foreach (var item in specList)
                    {
                        productPrices.Add(new CargoSupplierProductPrice
                        {
                            SID = item.SID,
                            TypeID = item.TypeID,
                            ProductCode = item.ProductCode,
                            HouseID = Convert.ToInt32(ent.SettleHouseID),
                            SupplierID = ent.ClientID,
                            SupplierNum = ent.ClientNum,
                            UnitPrice = 0,
                            DelFlag = 0
                        });
                    }
                    log.Operate = "A";
                    bus.AddSettleHouse(ent, productPrices, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
                else
                {
                    log.Operate = "U";
                    ent.ID = Convert.ToInt32(id);
                    CargoSettleHouseEntity info = bus.QuerySettleHouseInfo(new CargoSettleHouseEntity { ID = ent.ID });
                    CargoSettleHouseUpdateEntity updateEntity = new CargoSettleHouseUpdateEntity();
                    if (!info.SettleHouseID.Equals(ent.SettleHouseID))
                    {
                        updateEntity.OldSettleHouseID = info.SettleHouseID;
                        List<CargoProductSpecEntity> specList = bus.QueryProductSpecList(new CargoProductEntity { TypeIDStr = ent.ClientTypeID, UpClientID = 1 });
                        foreach (var item in specList)
                        {
                            productPrices.Add(new CargoSupplierProductPrice
                            {
                                SID = item.SID,
                                TypeID = item.TypeID,
                                ProductCode = item.ProductCode,
                                HouseID = Convert.ToInt32(ent.SettleHouseID),
                                SupplierID = ent.ClientID,
                                SupplierNum = ent.ClientNum,
                                UnitPrice = 0,
                                DelFlag = 0
                            });
                        }
                        updateEntity.ProductPrices = productPrices;
                    }
                    else
                    {
                        if (!info.ClientTypeID.Equals(ent.ClientTypeID))
                        {
                            string AddClientTypeID = String.Join(",", ent.ClientTypeID.Split(',').Except(info.ClientTypeID.Split(',')));
                            updateEntity.DelClientTypeID = String.Join(",", info.ClientTypeID.Split(',').Except(ent.ClientTypeID.Split(',')));
                            if (!string.IsNullOrEmpty(AddClientTypeID))
                            {
                                List<CargoProductSpecEntity> specList = bus.QueryProductSpecList(new CargoProductEntity { TypeIDStr = AddClientTypeID, UpClientID = 1 });
                                foreach (var item in specList)
                                {
                                    productPrices.Add(new CargoSupplierProductPrice
                                    {
                                        SID = item.SID,
                                        TypeID = item.TypeID,
                                        ProductCode = item.ProductCode,
                                        HouseID = Convert.ToInt32(ent.SettleHouseID),
                                        SupplierID = ent.ClientID,
                                        SupplierNum = ent.ClientNum,
                                        UnitPrice = 0,
                                        DelFlag = 0
                                    });
                                }
                                updateEntity.AddProductPrices = productPrices;
                            }
                        }
                    }
                    bus.UpdateSettleHouse(ent, updateEntity, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();
        }
        public void DelSettleHouse()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoSettleHouseEntity> list = new List<CargoSettleHouseEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "入驻仓库管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoSettleHouseEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        ClientNum = Convert.ToInt32(row["ClientNum"]),
                        SettleHouseID = Convert.ToString(row["SettleHouseID"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelSettleHouse(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
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
            Response.End();
        }
        #endregion

        #region 余额分账 BILL
        /// <summary>
        /// 查询余额分账账单
        /// </summary>
        public void QuerySuppClientBillManager()
        {
            CargoSuppClientAccountEntity queryEntity = new CargoSuppClientAccountEntity();

            if (!string.IsNullOrEmpty(Request["AccountNO"]))
            {
                queryEntity.AccountNO = Convert.ToString(Request["AccountNO"]);
            }
            if (!string.IsNullOrEmpty(Request["ClientID"]))
            {
                queryEntity.ClientID = Convert.ToInt32(Request["ClientID"]);
            }
            if (!string.IsNullOrEmpty(Request["HouseID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]);
            }

            queryEntity.Status = Convert.ToInt32(Request["Status"]);
            queryEntity.CheckStatus = Convert.ToInt32(Request["CheckStatus"]);
            queryEntity.ElecSign = Convert.ToInt32(Request["ElecSign"]);
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QuerySuppClientBillManager(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        //余额账单查询

        /// <summary>
        /// 根据账单号查询订单数据
        /// </summary>
        public void QueryBillOrderByAccountNo()
        {
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            queryEntity.AccountNo = Convert.ToString(Request["AccountNo"]);
            queryEntity.FinanceSecondCheck = "1";
            queryEntity.FromTO = "0";
            queryEntity.RType = "0";
            queryEntity.OrderType = "4";
            CargoFinanceBus bus = new CargoFinanceBus();
            List<CargoOrderEntity> list = bus.QueryOrderForBillCash(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 查询财务二审通过的订单数据用以出纳收款操作
        /// </summary>
        public void QueryOrderForBillCash()
        {
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            //queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
            //queryEntity.Dest = Convert.ToString(Request["Dest"]);
            if (Request["CheckStatus"] != "-1") { queryEntity.CheckStatus = Convert.ToString(Request["CheckStatus"]); }
            if (Request["IsSupplierType"] != "-1") { queryEntity.IsSupplierType = Convert.ToInt32(Request["IsSupplierType"]); }
            if (Request["IsHouseType"] != "-1") { queryEntity.IsHouseType = Convert.ToInt32(Request["IsHouseType"]); }
            if (!string.IsNullOrEmpty(Request["HouseID"]))//一级分类
            {
                queryEntity.CargoPermisID = Convert.ToString(Request["HouseID"]);//所属仓库ID
            }
            else
            {
                queryEntity.CargoPermisID = UserInfor.CargoPermisID;//用户所属仓库权限ID
            }
            //if (Convert.ToString(Request["OrderType"]) != "-1")
            //{
            //    queryEntity.OrderType = Convert.ToString(Request["OrderType"]);
            //}
            //2021-01-26注释付款人编码查询条件，添加付款人名称查询条件
            if (!string.IsNullOrEmpty(Convert.ToString(Request["PayClientNum"])))
            {
                queryEntity.PayClientNum = Convert.ToInt32(Request["PayClientNum"]);
            }
            //if (Convert.ToString(Request["ThrowGood"]) != "-1")
            //{
            //    queryEntity.ThrowGood = Convert.ToString(Request["ThrowGood"]);
            //}
            //if (!string.IsNullOrEmpty(Convert.ToString(Request["PayClientName"]))) { queryEntity.PayClientName = Convert.ToString(Request["PayClientName"]); }
            queryEntity.FinanceSecondCheck = "1";

            queryEntity.FromTO = "0";
            queryEntity.RType = "0";
            queryEntity.OrderType = "4";
            CargoFinanceBus bus = new CargoFinanceBus();
            List<CargoOrderEntity> list = bus.QueryOrderForBillCash(queryEntity);

            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 保存分账账单
        /// </summary>
        public void saveOrderBillAccount()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            //CargoClientAccountEntity ent = new CargoClientAccountEntity();
            List<string> entAwb = new List<string>();
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户账单管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoHouseBus house = new CargoHouseBus();
            CargoOrderBus order = new CargoOrderBus();
            CargoFinanceBus fin = new CargoFinanceBus();
            try
            {
                //string id = string.IsNullOrEmpty(Convert.ToString(Request["AccountID"])) ? "" : Request["AccountID"].ToString();
                #region 赋值

                List<CargoSuppClientAccountGoodsEntity> suppBillGoodsList = new List<CargoSuppClientAccountGoodsEntity>();
                List<CargoSuppClientAccountGoodsEntity> houseBillGoodsList = new List<CargoSuppClientAccountGoodsEntity>();
                foreach (Hashtable grid in GridRows)
                {
                    //供应商账单列表
                    CargoSuppClientAccountEntity suppBill = new CargoSuppClientAccountEntity();
                    decimal SupplierMoney = 0;
                    //仓库账单列表
                    CargoSuppClientAccountEntity houseBill = new CargoSuppClientAccountEntity();
                    decimal HouseMoney = 0;

                    //金额赋值
                    decimal InsuranceFee = Convert.ToDecimal(grid["InsuranceFee"]);// 优惠卷金额
                    decimal TransitFee = Convert.ToDecimal(grid["TransitFee"]);// 配送费
                    decimal DeliveryFee = Convert.ToDecimal(grid["DeliveryFee"]);// 物流费用
                    decimal OverDueFee = Convert.ToDecimal(grid["OverDueFee"]);// 仓库超期费
                    decimal OutStorageFee = 10 * Convert.ToInt32(grid["Piece"]);// 出仓费
                    decimal OtherFee = Convert.ToDecimal(grid["OtherFee"]);//其他费用
                    decimal TransportFee = Convert.ToDecimal(grid["TransportFee"]);//销售收入
                    //decimal InHousePrice = Convert.ToDecimal(grid["InHousePrice"]);//云仓进仓价
                    //decimal StoReleaseFee = 0;// 退仓费
                    decimal TotalCharge = Convert.ToDecimal(grid["TotalCharge"]);//总收入
                    int Piece = Convert.ToInt32(grid["Piece"]);//数量

                    string HouseName = Convert.ToString(grid["OutHouseName"]);
                    string OrderNo= Convert.ToString(grid["OrderNo"]);
                    int HouseID = Convert.ToInt32(grid["HouseID"]);
                    //退货单  OrderModel  0:客户单1:退货单
                    bool entire = true;
                    if (Convert.ToString(grid["OrderModel"]) == "1")
                    {
                        //查询关联订单信息
                        CargoOrderEntity orderData = order.QueryOrderDataInfo(new CargoOrderEntity { OrderNo = Convert.ToString(grid["RelateOrderNo"]) }).FirstOrDefault();
                        if (orderData == null)
                        {
                            msg.Message += "退货单：" + Convert.ToString(grid["OrderNo"]) + "无关联订单信息<br />";
                            continue;
                        }
                        //判断是不是退整单
                        if (orderData.Piece != Piece)
                        {
                            entire = false;
                        }
                        //是否退运费
                        //decimal HTransitFee = 0;
                        if (Convert.ToString(grid["IsTransitFee"]) == "0")
                        {
                            //不退运费  
                            //HTransitFee = TransitFee;
                            TransitFee = 0;
                        }

                    }

                    //共享次日达订单
                    string OpenOrderNo = "";
                    if (!Convert.ToInt32(grid["ShareHouseID"]).Equals(0))
                    {
                        HouseID = Convert.ToInt32(grid["ShareHouseID"]);
                        HouseName=house.QueryCargoHouse(new CargoHouseEntity { HouseID = HouseID }).Name;
                        OpenOrderNo = Convert.ToString(grid["OrderNo"]);
                        OrderNo = Convert.ToString(grid["OpenOrderNo"]);
                    }

                    //查询优惠卷   平台卷
                    if (Convert.ToString(grid["CouponType"]) == "0")
                    {
                        TotalCharge = TotalCharge + InsuranceFee;
                        InsuranceFee = 0;
                    }
                   
                    //不计算出仓费    车友惠
                    //if (Convert.ToString(grid["ClientNum"])== "100143") {
                    //    OutStorageFee = 0;
                    //}
                    //出仓费减半
                    //广州市白云区京溪金大轮胎商行(佳通)(揭阳 135\汕头 101\龙华 91\南宁 136\顺捷 100)989827
                    if (Convert.ToString(grid["ClientNum"]) == "989827"&& (HouseID == 135 || HouseID == 101 || HouseID == 91 || HouseID == 136 || HouseID == 100)) 
                    {
                        OutStorageFee = OutStorageFee / 2;
                    }
                    //广州市白云区太和向前旺轮胎店(玲珑)(汕头 101\龙华 91\南宁 136)172833
                    //光明 132 251118
                    if (Convert.ToString(grid["ClientNum"]) == "172833" && (HouseID == 101 || HouseID == 91 || HouseID == 136 || HouseID == 132))
                    {
                        OutStorageFee = OutStorageFee / 2;
                    }
                    //广州锦兴轮胎商行(普利司通/米其林/玲珑)(海南 143  秀英 133)630009
                    //东平 93  251018
                    //从化 107 251118
                    if (Convert.ToString(grid["ClientNum"]) == "630009" && (HouseID == 143 || HouseID == 133 || HouseID == 93 || HouseID == 107))
                    {
                        OutStorageFee = OutStorageFee / 2;
                    }

                    //计算平台费用
                    //OtherFee = Math.Ceiling((TransportFee - OtherFee - (!HTransitFee.Equals(0)?HTransitFee: TransitFee)) * Convert.ToDecimal(0.009));
                    //销售收入-平台费-配送费=轮胎款*0.009=平台费
                    //轮胎款
                    //decimal fee = TotalCharge - OtherFee- TransitFee;
                    //OtherFee = Math.Ceiling(fee * Convert.ToDecimal(0.009));
                    decimal fee = TotalCharge / Piece;
                    OtherFee = Math.Ceiling(fee - fee / Convert.ToDecimal(1.009)) * Piece;

                    //计算明细金额
                    //是  
                    if (entire)
                    {
                        SupplierMoney = TotalCharge - InsuranceFee - TransitFee - DeliveryFee - OverDueFee - OutStorageFee - OtherFee;
                        HouseMoney = InsuranceFee + TransitFee + DeliveryFee + OverDueFee + OutStorageFee;
                    }
                    //否  退货单
                    if (!entire)
                    {
                        SupplierMoney = TotalCharge - OutStorageFee - OtherFee;
                        HouseMoney = OutStorageFee;
                    }

                    //开思订单、天猫订单
                    if (grid["PayClientNum"].ToString() == "863602" || grid["PayClientNum"].ToString() == "165462")
                    {
                        //供应商分账公式 = 进仓价 - 10 / 条(出仓费) - 配送费 - 超期费
                        //仓库分账公式 = 10 / 条(出仓费) + 配送费  +超期费
                        //配送费:根据订单仓库阶梯收费
                        //CargoHouseEntity houseEnt = house.QueryCargoHouseByID(Convert.ToInt32(grid["HouseID"]));
                        //switch (Piece)
                        //{
                        //    case 1:
                        //        TransitFee = 15;
                        //        break;
                        //    case 2:
                        //        TransitFee = 20;
                        //        break;
                        //    default:
                        //        if (Piece >= 3)
                        //            TransitFee = 30;
                        //        break;
                        //}
                        //251018  开思和天猫不收配送费
                        TransitFee = 0;
                        //查询订单进价
                        decimal InHousePrice = fin.QuryOrderGoods(Convert.ToString(grid["OrderNo"])).Sum(w=>w.SupplySalePrice * w.Piece);
                        //如果没有进仓价，不分账，人工调整
                        if (InHousePrice.Equals(0))
                        {
                            continue;
                        }
                        SupplierMoney = InHousePrice - OutStorageFee- TransitFee - OverDueFee;
                        HouseMoney = OutStorageFee + TransitFee + OverDueFee;
                        
                    }

                    //添加供应商账单明细
                    suppBillGoodsList.Add(new CargoSuppClientAccountGoodsEntity
                    {
                        ClientID = Convert.ToInt32(grid["ClientID"]),
                        ClientNum = Convert.ToString(grid["ClientNum"]),
                        HouseID = HouseID,
                        OrderNo = OrderNo,
                        Total = SupplierMoney,
                        InsuranceFee = InsuranceFee,
                        TransitFee = TransitFee,
                        DeliveryFee = DeliveryFee,
                        OverDueFee = OverDueFee,
                        OutStorageFee = OutStorageFee,
                        OtherFee = OtherFee,
                        OrderModel = Convert.ToString(grid["OrderModel"]),
                        OpenOrderNo= OpenOrderNo
                    });

                    //添加仓库账单明细
                    houseBillGoodsList.Add(new CargoSuppClientAccountGoodsEntity
                    {
                        ClientID = Convert.ToInt32(grid["ClientID"]),
                        ClientNum = Convert.ToString(grid["ClientNum"]),
                        HouseID = HouseID,
                        HouseName = HouseName,
                        OrderNo = OrderNo,
                        Total = HouseMoney,
                        InsuranceFee = InsuranceFee,
                        TransitFee =  TransitFee,
                        DeliveryFee = DeliveryFee,
                        OverDueFee = OverDueFee,
                        OutStorageFee = OutStorageFee,
                        OtherFee = OtherFee,
                        OrderModel = Convert.ToString(grid["OrderModel"]),
                        OpenOrderNo = OpenOrderNo
                    });

                }
                //供应商账单列表
                List<CargoSuppClientAccountEntity> BillList = new List<CargoSuppClientAccountEntity>();
                List<int> supp = suppBillGoodsList.Select(w => w.ClientID).Distinct().ToList();
                foreach (int it in supp)
                {
                    CargoSuppClientAccountEntity bill = new CargoSuppClientAccountEntity();
                    CargoSuppClientAccountGoodsEntity data = suppBillGoodsList.Where(w => w.ClientID == it).FirstOrDefault();
                    //账单编号
                    bill.AccountNO = Common.GetMaxAccountNoNum("B");
                    bill.AccountTitle = $"供应商【{data.ClientNum}】{DateTime.Now.ToString("yyyyMM")}余额分账账单-手工";
                    bill.CreateDate = DateTime.Now;
                    bill.ClientID = data.ClientID;
                    bill.ClientNum = Convert.ToInt32(data.ClientNum);
                    //金额
                    bill.ARMoney = suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "0").Sum(w => w.Total) - suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "1").Sum(w => w.Total);
                    bill.Total = suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "0").Sum(w => w.Total) - suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "1").Sum(w => w.Total);
                    if (bill.Total <= 0)
                    {
                        msg.Message += bill.AccountTitle + "分账金额小于0，不可分账<br />";
                        continue;
                    }

                    bill.InsuranceFee = suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "0").Sum(w => w.InsuranceFee) - suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "1").Sum(w => w.InsuranceFee);
                    bill.TransitFee = suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "0").Sum(w => w.TransitFee) - suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "1").Sum(w => w.TransitFee);
                    bill.DeliveryFee = suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "0").Sum(w => w.DeliveryFee) - suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "1").Sum(w => w.DeliveryFee);
                    bill.OverDueFee = suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "0").Sum(w => w.OverDueFee) - suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "1").Sum(w => w.OverDueFee);
                    bill.OutStorageFee = suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "0").Sum(w => w.OutStorageFee) - suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "1").Sum(w => w.OutStorageFee);
                    bill.OtherFee = suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "0").Sum(w => w.OtherFee) - suppBillGoodsList.Where(w => w.ClientID == it && w.OrderModel == "1").Sum(w => w.OtherFee);
                    //备注不能有换行符
                    bill.Memo = Convert.ToString(Request["Memo"]).Replace("\n", "").Replace("\r", "");
                    bill.OPID = UserInfor.LoginName;
                    bill.OPDATE = DateTime.Now;
                    bill.AType = 0;//0:供应商账单  1:仓库账单
                    bill.SuppBillGoods = suppBillGoodsList.Where(w => w.ClientID == it).ToList();
                    BillList.Add(bill);
                }
                //仓库账单列表
                List<int> hou = houseBillGoodsList.Select(w => w.HouseID).Distinct().ToList();
                foreach (int it in hou)
                {
                    CargoSuppClientAccountEntity bill = new CargoSuppClientAccountEntity();
                    CargoSuppClientAccountGoodsEntity data = houseBillGoodsList.Where(w => w.HouseID == it).FirstOrDefault();
                    //账单编号
                    bill.AccountNO = Common.GetMaxAccountNoNum("B");
                    bill.AccountTitle = $"仓库【{data.HouseName}】{DateTime.Now.ToString("yyyyMM")}余额分账账单-手工";
                    bill.CreateDate = DateTime.Now;
                    bill.HouseID = data.HouseID;
                    bill.ClientID = data.ClientID;
                    bill.ClientNum = Convert.ToInt32(data.ClientNum);
                    bill.ARMoney = houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "0").Sum(w => w.Total) - houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "1").Sum(w => w.Total);
                    bill.Total = houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "0").Sum(w => w.Total) - houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "1").Sum(w => w.Total);
                    if (bill.Total <= 0)
                    {
                        msg.Message += bill.AccountTitle + "分账金额小于0，不可分账<br />";
                        continue;
                    }

                    bill.InsuranceFee = houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "0").Sum(w => w.InsuranceFee) - houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "1").Sum(w => w.InsuranceFee);
                    bill.TransitFee = houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "0").Sum(w => w.TransitFee) - houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "1").Sum(w => w.TransitFee);
                    bill.DeliveryFee = houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "0").Sum(w => w.DeliveryFee) - houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "1").Sum(w => w.DeliveryFee);
                    bill.OverDueFee = houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "0").Sum(w => w.OverDueFee) - houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "1").Sum(w => w.OverDueFee);
                    bill.OutStorageFee = houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "0").Sum(w => w.OutStorageFee) - houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "1").Sum(w => w.OutStorageFee);
                    bill.OtherFee = houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "0").Sum(w => w.OtherFee) - houseBillGoodsList.Where(w => w.HouseID == it && w.OrderModel == "1").Sum(w => w.OtherFee);

                    bill.Memo = Convert.ToString(Request["Memo"]).Replace("\n", "").Replace("\r", "");
                    bill.OPID = UserInfor.LoginName;
                    bill.OPDATE = DateTime.Now;
                    bill.AType = 1;//0:供应商账单  1:仓库账单
                    bill.SuppBillGoods = houseBillGoodsList.Where(w => w.HouseID == it).ToList();
                    BillList.Add(bill);
                }
                #endregion

                //添加账单信息
                log.Memo = "新增账单成功";
                log.Operate = "A";
                foreach (CargoSuppClientAccountEntity list in BillList)
                {
                    bus.AddCargoSuppAccount(list, log);
                }
                //修改共享订单的结算状态
                foreach (CargoSuppClientAccountGoodsEntity list in suppBillGoodsList.Where(w=>!string.IsNullOrEmpty(w.OpenOrderNo)))
                {
                    bus.UpdateSuppOrderAccountNo(new CargoSuppClientAccountGoodsEntity { OrderNo= list.OpenOrderNo }, log);
                }

                msg.Result = true;
                msg.Message += "保存成功";
                //msg.Message += ",成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }

        /// <summary>
        /// 一键分账
        /// </summary>
        public void suppClientAccountPay()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            //CargoClientAccountEntity ent = new CargoClientAccountEntity();
            List<string> entAwb = new List<string>();
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户账单管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoHouseBus house = new CargoHouseBus();
            CargoOrderBus order = new CargoOrderBus();

            try
            {
                #region 赋值
                List<CargoSuppClientAccountEntity> suppBillList = new List<CargoSuppClientAccountEntity>();
                List<CargoSuppClientAccountGoodsEntity> suppBillGoodsList = new List<CargoSuppClientAccountGoodsEntity>();
                List<CargoSuppClientAccountGoodsEntity> houseBillGoodsList = new List<CargoSuppClientAccountGoodsEntity>();
                foreach (Hashtable grid in GridRows)
                {
                    if (Convert.ToInt32(grid["CheckStatus"]) == 1)
                    {
                        msg.Message += Convert.ToString(grid["AccountNO"]) + "账单已结算，不可分账<br />";
                        continue;
                    }
                    //查询收款方   会员编码   分账金额  订单号
                    SplitRuleList splitRuleData = new SplitRuleList();
                    splitRuleData.transMessage = Convert.ToString(grid["AccountTitle"]) + "；" + Convert.ToString(grid["Memo"]);
                    splitRuleData.subOutOrderNo = Convert.ToString(grid["AccountNO"]) + "-1";
                    splitRuleData.value = Convert.ToInt32(Convert.ToDecimal(grid["Total"]) * 100);

                    //供应商账单
                    if (Convert.ToString(grid["AType"]) == "0")
                    {
                        CargoClientEntity client = bus.QueryCargoClientEntity(new CargoClientEntity
                        {
                            ClientNum = Convert.ToInt32(grid["ClientNum"])
                        });
                        if (client == null || client.ClientID == 0 || string.IsNullOrEmpty(client.BizUserId))
                        {
                            msg.Message += splitRuleData.transMessage + "无客户信息<br />";
                            continue;
                        }
                        splitRuleData.bizUserId = client.BizUserId;
                    }
                    //仓库账单
                    if (Convert.ToString(grid["AType"]) == "1")
                    {
                        CargoHouseEntity houseData = house.QueryCargoHouse(new CargoHouseEntity
                        {
                            HouseID = Convert.ToInt32(grid["HouseID"])
                        });
                        if (houseData == null || houseData.HouseID == 0 || string.IsNullOrEmpty(houseData.BizUserId))
                        {
                            msg.Message += splitRuleData.transMessage + "无仓库信息<br />";
                            continue;
                        }
                        splitRuleData.bizUserId = houseData.BizUserId;
                    }
                    List<SplitRuleList> splitRuleList = new List<SplitRuleList>();
                    splitRuleList.Add(splitRuleData);

                    SplitRule splitRules = new SplitRule();
                    splitRules.feeTakeMchId = AppConstants.USERIDBILL;
                    splitRules.type = "0";
                    splitRules.splitRuleList = splitRuleList;

                    string a = JsonConvert.SerializeObject(splitRules);

                    Dictionary<String, String> bizContent = new Dictionary<string, string>();
                    bizContent.Add("bizUserId", AppConstants.USERIDBILL);//商户号
                    bizContent.Add("amount", splitRuleData.value.ToString());//商户号
                    bizContent.Add("outOrderNo", Convert.ToString(grid["AccountNO"]));//商户号
                    bizContent.Add("splitRule", a);//分账信息
                    bizContent.Add("notifyUrl", AppConstants.NOTIFYURL);//分账信息

                    string bizContentJson = JsonConvert.SerializeObject(bizContent);
                    SybWxPayService sybService = new SybWxPayService();
                    Dictionary<String, String> rsp = sybService.bill(bizContentJson);

                    if ("000000".Equals(rsp["respCode"]))
                    {
                        //修改分账信息的结算状态
                        CargoSuppClientAccountEntity entity = new CargoSuppClientAccountEntity();
                        entity.AccountNO = Convert.ToString(grid["AccountNO"]);
                        entity.Status = 1;
                        entity.CheckStatus = 1;
                        bus.UpdateCargoSuppAccount(entity, log);
                    }

                }

                #endregion
                msg.Message += "分账成功";
                msg.Result = true;
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Clear();
            Response.Write(res);
            Response.End();
        }

        /// <summary>
        /// 删除成功
        /// </summary>
        public void DelCargoSuppClientAccount()
        {
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            //CargoClientAccountEntity ent = new CargoClientAccountEntity();
            List<string> entAwb = new List<string>();
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "客户账单管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoHouseBus house = new CargoHouseBus();
            CargoOrderBus order = new CargoOrderBus();

            try
            {
                #region 赋值
                List<CargoSuppClientAccountEntity> list = new List<CargoSuppClientAccountEntity>();
                foreach (Hashtable grid in GridRows)
                {
                    if (Convert.ToInt32(grid["Status"]) == 1)
                    {
                        msg.Message += Convert.ToString(grid["AccountNO"]) + "账单已审核，不可删除<br />";
                        continue;
                    }
                    if (Convert.ToInt32(grid["CheckStatus"]) == 1)
                    {
                        msg.Message += Convert.ToString(grid["AccountNO"]) + "账单已结算，不可删除<br />";
                        continue;
                    }

                    list.Add(new CargoSuppClientAccountEntity
                    {
                        AccountID = Convert.ToInt32(grid["AccountID"]),
                        AccountNO = Convert.ToString(grid["AccountNO"]),
                        AType = Convert.ToInt32(grid["AType"])
                    });
                }
                bus.DelCargoSuppClientAccount(list, log);
                #endregion
                if (list.Count > 0)
                {
                    msg.Message += "删除成功!";
                }

                msg.Result = true;
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        #endregion

        #region 冲账以及导出账单

        public void QueryBillOrderByAccountGoods()
        {
            CargoSuppClientAccountEntity queryEntity = new CargoSuppClientAccountEntity();
                queryEntity.AccountNO = Convert.ToString(Request["AccountNO"]);

            CargoFinanceBus bus = new CargoFinanceBus();
            Hashtable ht = new Hashtable();
            List<CargoSuppClientAccountGoodsEntity> list = bus.QueryBillOrderByAccountGoods(queryEntity);

            ht["row"] = list;
            ht["total"] = list.Count();

            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        public void SaveChargeOff()
        {
            CargoSuppClientAccountEntity ent = new CargoSuppClientAccountEntity();
            CargoSuppClientAccountGoodsEntity entGoods = new CargoSuppClientAccountGoodsEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "分账账单管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                //判断订单状态
                //if () { 

                //}


                //新增账单详细
                entGoods.AccountNO = Convert.ToString(Request["AccountNO"]);
                entGoods.OtherExpensesFee = Convert.ToDecimal(Request["Money"]);
                entGoods.OrderModel = Convert.ToString(Request["OrderModel"]);
                entGoods.Memo = Convert.ToString(Request["Memo"]);
                bus.AddCargoSuppAccountGoods(entGoods, log);

                //回写账单主表
                bus.UpdateSuppAccountData(entGoods);

                msg.Result = true;
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
            Response.End();
        }
        #endregion

        #region 账单推送

        public void wxBillPush()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string res = string.Empty;
            string json = Request["submitData"];
            var GridRows = JsonConvert.DeserializeObject<List<CargoSuppClientAccountEntity>>(json);
            GridRows = GridRows.Where(a => a.AType == 0 && a.Status == 1 && a.CheckStatus == 1).ToList();
            if (GridRows.Count == 0)
            {
                msg.Message = "无可推送数据";
                msg.Result = false;

                res = JSON.Encode(msg);
                Response.Write(res);
                Response.End();
                return;
            }
            foreach (var item in GridRows)
            {
                Common.SendBillPushMsg(item.ClientNum.ToString(), item.AccountNO, item.Total);
            }
            //返回处理结果
            res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
            //SendBillPushMsg
        }

        /// <summary>
        /// 导出分账账单 
        /// </summary>
        public void QueryAccountSplittingForExport()
        {
            CargoSuppClientAccountEntity queryEntity = new CargoSuppClientAccountEntity();
            //#region 查询主表数据
            if (!string.IsNullOrEmpty(Request["AccountNO"]))
            {
                queryEntity.AccountNO = Convert.ToString(Request["AccountNO"]);
            }
            if (!string.IsNullOrEmpty(Request["ClientID"]))
            {
                queryEntity.ClientID = Convert.ToInt32(Request["ClientID"]);
            }
            if (!string.IsNullOrEmpty(Request["HouseID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]);
            }

            queryEntity.Status = Convert.ToInt32(Request["Status"]);
            queryEntity.CheckStatus = Convert.ToInt32(Request["CheckStatus"]);
            queryEntity.ElecSign = Convert.ToInt32(Request["ElecSign"]);
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            //分页
            int pageIndex = 1;
            int pageSize = 10000;
            CargoClientBus bus = new CargoClientBus();
            var list = (List<CargoSuppClientAccountEntity>)bus.QuerySuppClientBillManager(pageIndex, pageSize, queryEntity)["rows"];


            //获取账单详情
            var dataGoods = bus.QueryCargoSuppClientAccountGoodsBatch(list);

            string err = "OK";
            var awbList = dataGoods;

            foreach (var item in list)
            {
                item.SuppBillGoods = dataGoods.Where(a => a.AccountNO == item.AccountNO).ToList();
            }

            if (list.Count > 0) { CargoSuppClientAccountList = list; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public List<CargoSuppClientAccountEntity> CargoSuppClientAccountList
        {
            get
            {
                if (Session["CargoSuppClientAccountList"] == null)
                {
                    Session["CargoSuppClientAccountList"] = new List<CargoSuppClientAccountEntity>();
                }
                return (List<CargoSuppClientAccountEntity>)(Session["CargoSuppClientAccountList"]);
            }
            set
            {
                Session["CargoSuppClientAccountList"] = value;
            }
        }

        #endregion
    }
}