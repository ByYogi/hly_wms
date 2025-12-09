using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using NPOI.HSSF.Record.Formula.Functions;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using ThoughtWorks.QRCode.Codec;

namespace Cargo.Weixin
{
    public partial class myAPI : WXBasePage
    {
        public static void WriteTextLog(string strMessage)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"System\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + DateTime.Now.ToString("yyyy-MM-dd") + ".System.txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + DateTime.Now.ToString() + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }
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
                log.UserID = WxUserInfo.wxOpenID;
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
        }
        /// <summary>
        /// 查询推广促销在售产品数据
        /// </summary>
        public void QueryOnSaleShelvesData()
        {
            //销售类型  3： 打折促销  4：积分兑换
            string SaleType = Convert.ToString(Request["SaleType"]);
            //模糊查询输入的内容
            string key = Convert.ToString(Request["key"]);
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<CargoProductShelvesEntity> result = bus.QueryShelvesData(new CargoProductShelvesEntity { SaleType = SaleType, ShelveStatus = "0", HouseID = WxUserInfo.HouseID, Title = key });
            //JSON
            String json = JSON.Encode(result);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 通过产品上架ID查询产品详细信息
        /// </summary>
        public void QueryOnShelvesByID()
        {
            Int64 sid = Convert.ToInt64(Request["ID"]);
            CargoWeiXinBus wx = new CargoWeiXinBus();
            CargoProductEntity result = wx.QueryOnShelvesByID(sid);
            //JSON
            String json = JSON.Encode(result);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询我的积分记录情况 
        /// </summary>
        public void QueryWXUserPoint()
        {
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<WXUserPointEntity> point = bus.QueryWXUserPoint(new WXUserPointEntity { WXID = WxUserInfo.ID });
            //JSON
            String json = JSON.Encode(point);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询我的微信商城订单列表
        /// </summary>
        public void QueryMyWeixinOrderList()
        {
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(pageIndex, pageSize, new WXOrderEntity { WXID = WxUserInfo.ID });
            //JSON
            String json = JSON.Encode(result);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询我的账单
        /// </summary>
        public void QueryMyWeixinAccount()
        {
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<WXOrderManagerEntity> result = bus.QueryWxOrderInfo(new WXOrderManagerEntity { WXID = WxUserInfo.ID });
            //JSON
            String json = JSON.Encode(result);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 添加新地址
        /// </summary>
        public void AddAddress()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            WXUserAddressEntity ent = new WXUserAddressEntity();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.NvgPage = "地址管理";
            log.Status = "0";
            log.UserID = WxUserInfo.wxOpenID;
            try
            {
                string id = string.Empty;
                foreach (Hashtable row in rows)
                {
                    id = Convert.ToString(row["ID"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(row["ID"])))
                    {
                        ent.ID = Convert.ToInt64(row["ID"]);
                    }
                    ent.WXID = WxUserInfo.ID;
                    ent.Name = Convert.ToString(row["Name"]).Replace("/", "-");
                    ent.Cellphone = Convert.ToString(row["Cellphone"]);
                    ent.Address = Convert.ToString(row["Address"]).Replace("/", "-");
                    ent.Cellphone = Convert.ToString(row["Cellphone"]);
                    ent.IsDefault = Convert.ToString(row["IsDefault"]);
                    string start = Convert.ToString(row["start"]);//省市乡
                    if (!string.IsNullOrEmpty(start))
                    {
                        string[] add = start.Split(' ');
                        if (add.Length == 1)
                        {
                            ent.Province = add[0];
                        }
                        if (add.Length == 2)
                        {
                            ent.Province = add[0];
                            ent.City = add[1];
                        }
                        if (add.Length == 3)
                        {
                            ent.Province = add[0];
                            ent.City = add[1];
                            ent.Country = add[2];
                        }
                    }
                    ent.AddressType = "0";
                }
                if (string.IsNullOrEmpty(id))
                {
                    log.Operate = "A";
                    ent.WXID = WxUserInfo.ID;
                    bus.AddAddress(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
                else
                {
                    log.Operate = "U";
                    ent.ID = Convert.ToInt64(id);
                    bus.UpdateAddress(ent, log);
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
        /// 删除地址
        /// </summary>
        public void DeleteAddress()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoWeiXinBus bus = new CargoWeiXinBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "地址管理";
            log.UserID = WxUserInfo.wxOpenID;
            log.Operate = "D";
            WXUserAddressEntity entity = new WXUserAddressEntity();
            try
            {
                entity.ID = Convert.ToInt64(json);
                bus.DeleteAddress(entity, log);
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

        }
        /// <summary>
        /// 设置默认地址
        /// </summary>
        public void SetAddress()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoWeiXinBus bus = new CargoWeiXinBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "地址管理";
            log.UserID = WxUserInfo.wxOpenID;
            log.Operate = "U";
            WXUserAddressEntity entity = new WXUserAddressEntity();
            try
            {
                entity.ID = Convert.ToInt64(json);
                entity.WXID = WxUserInfo.ID;
                bus.SetAddressDefault(entity, log);
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

        }
        /// <summary>
        /// 今日签到
        /// </summary>
        public void TodaySign()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoWeiXinBus bus = new CargoWeiXinBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "我的积分";
            log.UserID = WxUserInfo.wxOpenID;
            log.Operate = "U";
            WXUserPointEntity entity = new WXUserPointEntity();
            try
            {
                entity.WXID = WxUserInfo.ID;
                entity.PointType = "2";
                entity.Point = Convert.ToInt32(Request["Point"]);
                entity.CutPoint = "0";
                bus.AddWeixinPoint(entity, log);
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

        }
        public void QueryInHouseProductData()
        {
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            if (Request["onShelves"] != "-1")
            {
                queryEntity.OnShelves = Convert.ToString(Request["onShelves"]);//是否上架
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoProductBus bus = new CargoProductBus();
            Hashtable list = bus.QueryWeixinInHouseProduct(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据ID查询在库产品信息
        /// </summary>
        public void QueryInHouseProductByID()
        {
            string SaleT = Request["SaleT"];
            String json = Request["data"];
            String address = Request["addr"];
            WriteTextLog(json);
            WriteTextLog(address);
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            int houseID = WxUserInfo.HouseID;
            CargoAreaEntity careaEnt = new CargoAreaEntity();
            List<CargoAreaEntity> areaList = new List<CargoAreaEntity>();
            List<CargoProductEntity> result = new List<CargoProductEntity>();
            if (address != "null" && !string.IsNullOrEmpty(address))
            {
                ArrayList addr = (ArrayList)JSON.Decode(address);
                string Province = string.Empty;
                string Country = string.Empty;
                string City = string.Empty;
                foreach (Hashtable it in addr)
                {
                    Province = Convert.ToString(it["Province"]);
                    Country = Convert.ToString(it["Country"]);
                    City = Convert.ToString(it["City"]);
                    break;
                }
                WriteTextLog(Province + Country + City);
                CargoHouseBus house = new CargoHouseBus();
                //CargoHouseEntity houseEnt = house.QueryCargoHouse(new CargoHouseEntity { DeliveryArea = Province });
                //if (houseEnt.HouseID.Equals(0))
                //{
                //    //该省份未开放购买权限
                //    return;
                //}
                //houseID = houseEnt.HouseID;
                //1.根据城市查询该城市是否在仓库配送区域，在则从该仓库出库，不在
                //2.根据区域查询 该区域是否在仓库配送区域 ，在则从该仓库出库，不在，则直接返回
                //deliv = house.IsExistDeliveryArea(new CargoAreaEntity { HouseID = houseID, DeliveryArea = Country });
                if (!string.IsNullOrEmpty(Country))
                {
                    careaEnt = house.QueryAreaByDeliveryArea(new CargoAreaEntity { HouseID = houseID, DeliveryArea = Country });
                }
                if (careaEnt.AreaID.Equals(0))
                {
                    careaEnt = house.QueryAreaByDeliveryArea(new CargoAreaEntity { HouseID = houseID, DeliveryArea = City });
                    if (careaEnt.AreaID.Equals(0))
                    {
                        //该省份未开放购买权限
                        result.Add(new CargoProductEntity { Memo = "该省份未开放购买权限" });
                        goto ERR;
                    }
                }
                areaList = house.QueryAreaListByDeliveryArea(new CargoAreaEntity { HouseID = houseID, DeliveryArea = City });
            }


            CargoWeiXinBus bus = new CargoWeiXinBus();
            foreach (Hashtable row in rows)
            {
                if (Convert.ToInt32(row["PC"]) <= 0) { continue; }
                if (result.Exists(c => c.OnSaleID == Convert.ToInt64(row["ID"])))
                {
                    CargoProductEntity exE = result.Find(c => c.OnSaleID == Convert.ToInt64(row["ID"]));
                    exE.Numbers += Convert.ToInt32(row["PC"]);
                }
                else
                {
                    CargoProductEntity cargo = bus.QueryOnShelvesByID(Convert.ToInt64(row["ID"]));
                    cargo.Numbers = Convert.ToInt32(row["PC"]);
                    cargo.DelieryType = "0";//快递
                    if (careaEnt.AreaType != null && careaEnt.AreaType.Equals("1")) { cargo.DelieryType = "1"; }//极速配送
                    cargo.HouseID = houseID;
                    result.Add(cargo);
                }
            }
            #region 取库存
            if (!houseID.Equals(0))
            {
                //根据指定仓库区域ID查询 该仓库区域下的货是否有货
                CargoInterfaceBus interBus = new CargoInterfaceBus();
                foreach (var it in result)
                {
                    it.IsModifyPrice = "0";
                    if (it.SaleType.Equals("1"))
                    {
                        //表示 特价促销
                        if (it.RealStockNum < it.Numbers)
                        {
                            it.IsModifyPrice = "1";//表示缺货
                            it.OnSaleNum = it.RealStockNum;
                        }
                    }
                    else if (it.SaleType.Equals("3"))
                    {
                        //表示 推广限量限价
                        if (it.OnSaleNum < it.Numbers)
                        {
                            it.IsModifyPrice = "1";//表示限量已卖完
                            it.OnSaleNum = it.OnSaleNum;
                        }
                    }
                    else
                    {
                        bool noTyre = false;
                        CargoInterfaceEntity stockEntity = new CargoInterfaceEntity
                        {
                            HouseID = houseID,
                            AreaID = careaEnt.AreaID,
                            //GoodsCode = it.GoodsCode,
                            //LoadIndex = it.LoadIndex,
                            //SpeedLevel = it.SpeedLevel,
                            Figure = it.Figure,
                            TypeID = it.TypeID,
                            BatchYear = it.BatchYear,
                            Specs = it.Specs
                        };
                        List<CargoInterfaceEntity> stockList = interBus.queryMiniCargoStock(stockEntity);
                        if (stockList.Count <= 0)
                        {
                            noTyre = true;
                            it.IsModifyPrice = "1";//表示缺货
                            it.OnSaleNum = 0;
                        }
                        else if (stockList.Sum(c => c.StockNum) < it.Numbers)
                        {
                            noTyre = true;
                            it.IsModifyPrice = "1";//表示缺货
                            it.OnSaleNum = stockList.Sum(c => c.StockNum);
                        }
                        else
                        {
                            it.OnSaleNum = stockList.Sum(c => c.StockNum);
                        }
                        //缺货，查别的仓库
                        if (noTyre)
                        {
                            int kc = 0;
                            foreach (var area in areaList)
                            {
                                CargoInterfaceEntity stockEntityA = new CargoInterfaceEntity
                                {
                                    HouseID = houseID,
                                    AreaID = area.AreaID,
                                    //GoodsCode = it.GoodsCode,
                                    //LoadIndex = it.LoadIndex,
                                    //SpeedLevel = it.SpeedLevel,
                                    Figure = it.Figure,
                                    TypeID = it.TypeID,
                                    BatchYear = it.BatchYear,
                                    Specs = it.Specs
                                };
                                List<CargoInterfaceEntity> stockListA = interBus.queryMiniCargoStock(stockEntityA);
                                kc += stockListA.Sum(c => c.StockNum);
                            }
                            it.IsModifyPrice = "0";
                            it.OnSaleNum = kc;
                            if (kc < it.Numbers)
                            {
                                it.IsModifyPrice = "1";//表示缺货
                            }
                        }
                    }

                }
            }
            #endregion
        ERR:
            //返回处理结果
            string res = JSON.Encode(result);
            Response.Write(res);
        }
        /// <summary>
        /// 查询订单状态
        /// </summary>
        public void QueryWeixinUserOrderInfo()
        {
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXOrderEntity result = bus.QueryWeixinUserOrderInfo(Convert.ToInt64(WxUserInfo.ID));
            //返回处理结果
            string res = JSON.Encode(result);
            Response.Write(res);
        }
        /// <summary>
        /// 收货完成
        /// </summary>
        public void setWeixinOrderOk()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoWeiXinBus bus = new CargoWeiXinBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "收货确认";
            log.UserID = WxUserInfo.wxOpenID;
            log.Operate = "U";
            WXOrderEntity entity = new WXOrderEntity();
            try
            {
                entity.OrderNo = json;
                entity.OrderStatus = "4";
                bus.setWeixinOrderOk(entity, log);
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
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        public void DeleteWeixinOrder()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoWeiXinBus bus = new CargoWeiXinBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "删除订单";
            log.UserID = WxUserInfo.wxOpenID;
            log.Operate = "D";
            WXOrderEntity entity = new WXOrderEntity();
            try
            {
                entity.ID = Convert.ToInt64(json);
                WXOrderEntity wo = bus.QueryWeixinOrderByOrderNo(entity);
                if (wo != null && !wo.ID.Equals(0))
                {
                    if (wo.PayWay.Equals("0") && wo.PayStatus.Equals("1"))
                    {
                        msg.Message = "订单已付款，不允许删除";
                        msg.Result = false;
                    }
                    if (!wo.OrderStatus.Equals("0"))
                    {
                        msg.Message = "仓库已发货，不允许删除";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    bus.DeleteWeixinOrder(wo, log);
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
        /// 绑定店代码
        /// </summary>
        public void WxClientBind()
        {
            String json = Request["data"];
            WriteTextLog(json);
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            WXUserEntity ent = new WXUserEntity();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.NvgPage = "店代码绑定";
            log.Status = "0";
            log.Operate = "U";
            log.UserID = WxUserInfo.wxOpenID;
            try
            {
                string code = string.Empty;
                foreach (Hashtable row in rows)
                {
                    ent.ClientNum = Convert.ToInt32(row["ClientNum"]);
                    ent.Cellphone = Convert.ToString(row["Cellphone"]);
                    string start = Convert.ToString(row["start"]);//省市乡
                    if (!string.IsNullOrEmpty(start))
                    {
                        string[] add = start.Split(' ');
                        if (add.Length == 1)
                        {
                            ent.Province = add[0];
                        }
                        if (add.Length == 2)
                        {
                            ent.Province = add[0];
                            ent.City = add[1];
                        }
                        if (add.Length == 3)
                        {
                            ent.Province = add[0];
                            ent.City = add[1];
                            ent.Country = add[2];
                        }
                    }
                    code = Convert.ToString(row["CellCheckCode"]);
                    ent.wxOpenID = WxUserInfo.wxOpenID;
                }
                string cacheCode = Convert.ToString(CookiesHelper.Get(ent.Cellphone));
                if (!code.Equals(cacheCode))
                {
                    msg.Result = false;
                    msg.Message = "验证码不正确";
                    goto RESU;
                }
                if (bus.IsCellphoneBind(ent))
                {
                    msg.Result = false;
                    msg.Message = "该手机号码已绑定";
                    goto RESU;
                }
                //绑定
                if (bus.IsMaxBindingNum(ent))
                {
                    msg.Result = false;
                    msg.Message = "同一个店代码最多允许绑定3个微信";
                    goto RESU;
                }
                CargoClientBus client = new CargoClientBus();

                if (!client.IsExistCargoClientNum(new CargoClientEntity { ClientNum = ent.ClientNum }))
                {
                    msg.Result = false;
                    msg.Message = "店代码不存在,请确认后输入";
                    goto RESU;
                }

                CargoClientEntity wen = client.QueryCargoClient(ent.ClientNum);
                ent.Name = string.IsNullOrEmpty(wen.ClientShortName) ? wen.ClientName : wen.ClientShortName;
                bus.UpdateClientForBindingClientNum(ent, log);
                msg.Result = true;
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        RESU:
            //返回处理结果
            string res = JSON.Encode(msg);
            //Response.ContentType = "content-type";
            Response.Write(res);
        }
        /// <summary>
        /// 手机短信验证
        /// </summary>
        public void SendCode()
        {
            string PostUrl = ConfigurationManager.AppSettings["msgUrl"];
            string Mobile = Convert.ToString(Request["Mobile"]);
            if (!string.IsNullOrEmpty(Mobile))
            {
                string sname = ConfigurationManager.AppSettings["msgName"].ToString();
                string spwd = ConfigurationManager.AppSettings["msgPwd"].ToString();
                string scorpid = ConfigurationManager.AppSettings["msgCorpid"].ToString(); //企业代码
                string sprdid = ConfigurationManager.AppSettings["msgPrdid"].ToString();   //产品id
                string sdst = Mobile;                      //接收号码

                string[] source = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                string code = "";
                Random rd = new Random();
                for (int i = 0; i < 5; i++)
                {
                    code += source[rd.Next(0, source.Length)];
                }
                if (code != "")
                {
                    //通过手机号和验证码存入缓存
                    CookiesHelper.Insert(Mobile, code, 2);

                    //JSON
                    String json = JSON.Encode(code);
                    Response.Clear();
                    Response.Write(json);                                    //推送验证码到页面
                }
                string smsg = "【迪乐泰中国】尊敬的客户，您的验证码为：" + code;
                string postStrTpl = string.Format("sname={0}&spwd={1}&scorpid={2}&sprdid={3}&sdst={4}&smsg={5}", sname, spwd, scorpid, sprdid, sdst, smsg);

                string result = wxHttpUtility.SendHttpRequest(PostUrl, postStrTpl);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlElement rootNode = xmlDoc.DocumentElement;
                string state = rootNode["State"].InnerText;
                string msgState = rootNode["MsgState"].InnerText;

                if (msgState == "提交成功")
                {
                    WriteTextLog(sdst + ":本次验证码发送成功，验证码：" + code);
                    // Log.Info("ExpressAPI", sdst + ":本次验证码发送成功，验证码：" + code);
                }
                else
                {
                    WriteTextLog(sdst + ":本次验证码发送失败，验证码：" + code);
                    // Log.Info("ExpressAPI", sdst + ":本次验证码发送失败，验证码：" + code);
                }
            }
        }
        public void QueryTypeStock()
        {
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            string res = Convert.ToString(Request["key"]);
            res = res.ToUpper().Replace("/", "").Replace("R", "").Replace("C", "").Replace("F", "");
            if (!res.Contains("/") && !res.ToUpper().Contains("R"))
            {
                if (res.Length <= 3)
                {
                    queryEntity.Specs = res;
                }
                if (res.Length > 3 && res.Length <= 5)
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3);
                }
                if (res.Length > 5)
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5);
                }
            }
            if (res.ToUpper().Contains("F"))
            {
                queryEntity.Specs = res;
                if (!res.ToUpper().Contains("/"))
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3);
                }
            }
            if (res.ToUpper().Contains("R") && res.ToUpper().Contains("C"))
            {
                queryEntity.Specs = res;
            }
            queryEntity.TypeID = Convert.ToInt32(Request["ProductTypeID"]);
            queryEntity.OnShelves = "0";
            queryEntity.HouseID = WxUserInfo.HouseID;
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HubDiameter"])))
            {
                queryEntity.HubDiameter = Convert.ToInt32(Request["HubDiameter"]);
            }
            CargoProductBus bus = new CargoProductBus();
            Hashtable list = bus.QueryWeixinInHouseProduct(1, 1000, queryEntity);
            List<CargoContainerShowEntity> goods = list["rows"] as List<CargoContainerShowEntity>;
            //JSON
            String json = JSON.Encode(goods);
            //Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据车辆品牌查询适配轮胎规格型号
        /// </summary>
        public void QueryCarTyreMatch()
        {
            CargoProductCarTyreEntity queryEntity = new CargoProductCarTyreEntity();
            string res = Convert.ToString(Request["key"]);
            queryEntity.Car = res;
            CargoProductBus bus = new CargoProductBus();
            List<CargoProductCarTyreEntity> cars = bus.QueryCarTyreMatch(queryEntity);
            //JSON
            String json = JSON.Encode(cars);
            //Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 保存营业执照照片
        /// </summary>
        public void SaveBusLicense()
        {
            string img = Request["imgbase64"];
            string[] img_array = img.Split(',');
            byte[] bytes = Convert.FromBase64String(img_array[1]);
            string SavePath = "../Weixin/UploadFile/";
            string modifyFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            string path = Server.MapPath(SavePath + modifyFileName + ".jpg");

            using (MemoryStream memoryStream = new MemoryStream(bytes, 0, bytes.Length))
            {
                //  转成图片
                System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);
                image.Save(path);  // 将图片存到本地
                image.Dispose();
            }
            ErrMessage msg = new ErrMessage();
            msg.Message = modifyFileName + ".jpg";
            msg.Result = true;
            //JSON
            String json = JSON.Encode(msg);
            //Response.Clear();
            Response.Write(json);
            Response.Flush();
        }
        /// <summary>
        /// 绑定手机号
        /// </summary>
        public void wxUserRegeist()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            WXUserEntity ent = new WXUserEntity();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            try
            {
                string code = string.Empty;
                foreach (Hashtable row in rows)
                {
                    ent.Cellphone = Convert.ToString(row["Cellphone"]);
                    code = Convert.ToString(row["CellCheckCode"]);
                }
                string cacheCode = Convert.ToString(CookiesHelper.Get(ent.Cellphone));
                if (!code.Equals(cacheCode))
                {
                    msg.Result = false;
                    msg.Message = "验证码不正确";
                    goto RESU;
                }
                if (bus.IsCellphoneBind(ent))
                {
                    msg.Result = false;
                    msg.Message = "该手机号码已绑定";
                    goto RESU;
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        RESU:
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 提交注册
        /// </summary>
        public void saveRegeist()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            WXUserEntity ent = new WXUserEntity();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.NvgPage = "用户注册";
            log.Status = "0";
            log.Operate = "U";
            log.UserID = WxUserInfo.wxOpenID;
            try
            {
                foreach (Hashtable row in rows)
                {
                    ent.Cellphone = Convert.ToString(row["Cellphone"]);
                    ent.CompanyName = Convert.ToString(row["CompanyName"]).Replace("/", "-");
                    ent.Name = Convert.ToString(row["Name"]).Replace("/", "-");
                    ent.Address = Convert.ToString(row["Address"]).Replace("/", "-");
                    ent.BusLicenseImg = Convert.ToString(row["BusLicenseImg"]);
                    ent.IDCardImg = Convert.ToString(row["IDCardImg"]);
                    ent.IDCardBackImg = Convert.ToString(row["IDCardBackImg"]);
                    string start = Convert.ToString(row["start"]);//省市乡
                    if (!string.IsNullOrEmpty(start))
                    {
                        string[] add = start.Split(' ');
                        if (add.Length == 1)
                        {
                            ent.Province = add[0];
                        }
                        if (add.Length == 2)
                        {
                            ent.Province = add[0];
                            ent.City = add[1];
                        }
                        if (add.Length == 3)
                        {
                            ent.Province = add[0];
                            ent.City = add[1];
                            ent.Country = add[2];
                        }
                    }
                    ent.wxOpenID = WxUserInfo.wxOpenID;
                    ent.AvatarBig = WxUserInfo.AvatarBig;
                    ent.AvatarSmall= WxUserInfo.AvatarSmall;
                    ent.wxName = WxUserInfo.wxName;
                }
                bus.UpdateWxUserRegeist(ent, log);
                msg.Result = true;
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //JSON
            String res = JSON.Encode(msg);
            Response.Write(res);
            Response.Flush();
        }
        /// <summary>
        /// 透支额度付款
        /// </summary>
        public void saveWxOrderPayStatus()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            WXUserEntity ent = new WXUserEntity();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "微信付款";
            log.Operate = "U";
            log.UserID = WxUserInfo.wxOpenID;
            try
            {
                WXUserEntity we = new WXUserEntity();
                we.wxOpenID = WxUserInfo.wxOpenID;
                if (string.IsNullOrEmpty(WxUserInfo.wxOpenID))
                {
                    we.UnionID = WxUserInfo.UnionID;
                }
                WXUserEntity wxUser = bus.QueryWeixinUserByOpendID(we);
                if (wxUser.ID.Equals(0))
                {
                    msg.Message = "用户未登陆，请重新操作";
                    msg.Result = false;
                    goto ERR;
                }
                if (wxUser.LimitMoney <= 0)
                {
                    msg.Message = "信用额度已用完，请选择微信支付，谢谢！";
                    msg.Result = false;
                    goto ERR;
                }
                WXOrderEntity order = bus.QueryWeixinOrderByOrderNo(new WXOrderEntity { OrderNo = json });
                if (order.ID.Equals(0))
                {
                    msg.Message = "订单号有误，请重新操作";
                    msg.Result = false;
                    goto ERR;
                }
                if (order.PayStatus.Equals("1"))
                {
                    msg.Message = "您已支付，请勿重复支付！";
                    msg.Result = false;
                    goto ERR;
                }
                if (wxUser.LimitMoney < order.TotalCharge)
                {
                    msg.Message = "信用额度不足，请选择微信支付，谢谢！";
                    msg.Result = false;
                    goto ERR;
                }
                order.ClientNum = wxUser.ClientNum;
                order.PayStatus = "1";
                order.PayWay = "1";
                bus.UpdateClientLimitAndWxOrderPayStatus(order, log);
                msg.Result = true;
                //List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(1, 5, new WXOrderEntity { OrderNo = order.OrderNo });
                if (!string.IsNullOrEmpty(WxUserInfo.wxOpenID))
                {
                    //推送客户消息
                    TemplateMsg tmMsg = new TemplateMsg
                    {
                        first = new TemplateDataItem("尊敬的迪乐泰客户，您的订单已支付成功！~正在秒速为您安排发货，请耐心等待哦~", "#173177"),
                        keyword1 = new TemplateDataItem(order.TotalCharge + "元", "#173177"),
                        //keyword1 = new TemplateDataItem(result[0].TotalCharge.ToString("F2") + "元", "#173177"),
                        keyword2 = new TemplateDataItem(order.Piece.ToString() + "条轮胎", "#173177"),
                        keyword3 = new TemplateDataItem(order.Province + " " + order.City + " " + order.Country + " " + order.Address + "~" + order.Cellphone, "#173177"),
                        keyword4 = new TemplateDataItem(order.OrderNo, "#173177"),
                        remark = new TemplateDataItem("点击查看订单详情!", "#173177")
                    };
                    SendTempleteMessage send = new SendTempleteMessage();
                    string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());

                    //oo1LEt_z6Yhx-g_qdgrYhLaYaazE//测试
                    string errmsg = send.SendMessage(token, WxUserInfo.wxOpenID, "F6e8g72dWLpVUStvkjv3--ZBhmjheE7wa84bgcQ_c40", "http://dlt.neway5.com/Weixin/OrderInfo.aspx?orderNo=" + order.OrderNo, tmMsg);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        ERR:
            //JSON
            String res = JSON.Encode(msg);
            Response.Write(res);
            Response.Flush();
        }
        /// <summary>
        /// 判断当前微信用户是否在商城下过单
        /// </summary>
        public void IsExistOrderByWXID()
        {

            ErrMessage msg = new ErrMessage();
            msg.Message = "";
            msg.Result = true;
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXOrderEntity entity = new WXOrderEntity();
            try
            {
                bool wo = bus.IsExistWeixinOrderPay(new WXOrderEntity { WXID = WxUserInfo.ID });
                msg.Result = wo;
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
        /// 查询客户地址
        /// </summary>
        public void QueryClientSession()
        {
            CargoClientEntity queryEntity = new CargoClientEntity();

            queryEntity.DelFlag = "0";
            queryEntity.HouseID = 12;
            //queryEntity.Specs = Convert.ToString(Request["key"]);
            CargoClientBus bus = new CargoClientBus();
            List<ClientSessionEntity> Clientlist = bus.QueryClientSession(queryEntity);
            List<WXClientSessionEntity> wxClient = new List<WXClientSessionEntity>();
            foreach (var it in Clientlist)
            {
                wxClient.Add(new WXClientSessionEntity { Address = it.Address, label = it.Boss, Cellphone = it.Cellphone, HouseName = "梅州揭阳仓库" });
            }
            //JSON
            String json = JSON.Encode(wxClient);
            Response.Clear();
            Response.Write(json);
            Response.Flush();
        }
        /// <summary>
        /// 领取优惠
        /// </summary>
        public void SaveSecKill()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            WXSecKillEntity ent = new WXSecKillEntity();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.NvgPage = "优惠领取";
            log.Status = "0";
            log.Operate = "A";
            log.UserID = WxUserInfo.wxOpenID;
            try
            {
                foreach (Hashtable row in rows)
                {
                    ent.WXID = WxUserInfo.ID;
                    ent.wxName = WxUserInfo.wxName;
                    ent.wxOpenID = WxUserInfo.wxOpenID;
                    ent.CarNum = Convert.ToString(row["CarNum"]).Trim();
                    ent.Cellphone = Convert.ToString(row["Cellphone"]).Trim();
                    ent.ParentID = Convert.ToInt64(row["ParentID"]);
                    ent.Company = Convert.ToString(row["Company"]).Trim();
                }
                if (bus.IsExistReceive(ent))
                {
                    msg.Message = ent.CarNum + "此车牌号已经领取过优惠";
                    msg.Result = false;
                }
                if (msg.Result)
                {
                    bus.AddSecKill(ent, log);
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
        /// 查询领取人数
        /// </summary>
        public void QuerySecKillData()
        {
            string company = Convert.ToString(Request["company"]);
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<WXSecKillEntity> result = bus.QuerySecKillData(new WXSecKillEntity { Company = company });
            if (result.Count > 0)
            {
                foreach (var it in result)
                {
                    it.Cellphone = it.Cellphone.Substring(0, 3) + "***" + it.Cellphone.Substring(it.Cellphone.Length - 2, 2);
                    it.CarNum = it.CarNum.Substring(0, 2) + "***" + it.CarNum.Substring(it.CarNum.Length - 2, 2);
                    it.UseStatus = "已登记";
                    it.ReceiveTime = it.OP_DATE.ToString("yyyy-MM-dd HH:mm");
                }
            }
            //返回处理结果
            string res = JSON.Encode(result);
            Response.Write(res);
        }
        public void configJssdk()
        {


            //2.获取Token
            //AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), secret);
            //JsApiTicketResult apiTicket= CommonApi.GetTicket(Common.GetQYCorpID(), secret);
            //string titck = JsApiTicketContainer.GetJsApiTicket(Common.GetdltAPPID());

            WriteTextLog("1");
            JsApiTicketResult jtr = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetTicket(Common.GetdltAPPID(), Common.GetdltAppSecret(), "jsapi");
            WriteTextLog("2");
            string titck = jtr.ticket;

            WriteTextLog(titck);

            string Url = Request["Url"].ToString();
            string appId = Common.GetdltAPPID();//System.Configuration.ConfigurationManager.AppSettings["CorpID"];
            string appSecret = Common.GetdltAppSecret();//System.Configuration.ConfigurationManager.AppSettings["EncodingAESKey"];
            bool debug = true;
            JSSDK sdk = new JSSDK(appId, appSecret, debug);
            SignPackage config = sdk.GetSignPackage(Url, JsApiEnum.scanQRCode);
            Dictionary<string, object> result = new Dictionary<string, object>();
            //appId = config.appId;
            result.Add("appId", config.appId);
            result.Add("timestamp", config.timestamp);
            result.Add("nonceStr", config.nonceStr);
            result.Add("signature", JSSDKHelper.GetSignature(titck, config.nonceStr, config.timestamp.ToString(), Url));

            Response.ContentType = "text/plain";

            string resultString = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            WriteTextLog(resultString);
            Response.Write(resultString);
            Response.Flush();
        }
        /// <summary>
        /// 保存统计数据
        /// </summary>
        public void AddSecStatisData()
        {
            string typ = Convert.ToString(Request["Type"]);
            //string company = Convert.ToString(Request["Company"]);
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXSecStatisEntity entity = new WXSecStatisEntity();
            if (typ.Equals("0"))
            {
                entity.ShareAppNum = 1;
            }
            else
            {
                entity.ShareTimNum = 1;
            }
            entity.SecID = Convert.ToInt32(Request["Company"]);
            bus.AddSecStatisData(entity);
        }
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        public void MakeQRCode()
        {
            string result = Convert.ToString(Request["Code"]);

            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            try
            {
                qrCodeEncoder.QRCodeScale = 4;
            }
            catch { }
            String data = result;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.White; //背景颜色
            // qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;//图像颜色
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;

            System.Drawing.Image myimg = qrCodeEncoder.Encode(data, System.Text.Encoding.UTF8); //kedee 增加utf-8编码，可支持中文汉字  
            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
        }
        /// <summary>
        /// 保存淘宝订单号
        /// </summary>
        public void SaveTaobaoOrder()
        {
            string tbOrder = Convert.ToString(Request["key"]);
            if (String.IsNullOrEmpty(tbOrder)) return;

            WXTaobaoEntity ent = new WXTaobaoEntity();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.NvgPage = "秒杀菌";
            log.Status = "0";
            log.UserID = WxUserInfo.wxOpenID;
            try
            {
                ent.WXID = WxUserInfo.ID;
                ent.TaobaoID = tbOrder;
                if (bus.IsExistTaobaoOrder(ent))
                {
                    msg.Result = false;
                    msg.Message = "淘宝订单号已经录入";
                }
                if (msg.Result)
                {
                    log.Operate = "A";
                    bus.SaveTaobaoOrder(ent, log);
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
        /// 确认商城订单
        /// </summary>
        public void MakeSureOrder()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信商城订单";
            log.Status = "0";
            log.NvgPage = "仓库确认订单";
            log.UserID = WxUserInfo.SysLoginID;
            log.Operate = "A";
            try
            {
                if (string.IsNullOrEmpty(WxUserInfo.SysLoginID))
                {
                    msg.Message = "您没有权限确认订单，请联系管理员";
                    msg.Result = false;
                    goto ERR;
                }
                WXOrderManagerEntity queryEntity = new WXOrderManagerEntity();
                queryEntity.OrderNo = json;
                CargoOrderBus bus = new CargoOrderBus();

                List<WXOrderManagerEntity> wxOrderList = bus.QueryContainerGoodsMakeSureWeixinOrder(queryEntity);
                if (wxOrderList.Count <= 0)
                {
                    msg.Message = "订单数据有误";
                    msg.Result = false;
                    goto ERR;
                }
                if (!wxOrderList[0].OrderStatus.Equals("0"))
                {
                    msg.Message = "商城订单已确认，请重新查询后再操作";
                    msg.Result = false;
                    goto ERR;
                }

                CargoHouseBus house = new CargoHouseBus();
                CargoAreaEntity careaEnt = new CargoAreaEntity();
                List<CargoAreaEntity> areaList = new List<CargoAreaEntity>();
                //1.根据城市查询该城市是否在仓库配送区域，在则从该仓库出库，不在
                //2.根据区域查询 该区域是否在仓库配送区域 ，在则从该仓库出库，不在，则直接返回
                //deliv = house.IsExistDeliveryArea(new CargoAreaEntity { HouseID = houseID, DeliveryArea = Country });
                careaEnt = house.QueryAreaByDeliveryArea(new CargoAreaEntity { HouseID = wxOrderList[0].HouseID, DeliveryArea = wxOrderList[0].Country });
                if (careaEnt.AreaID.Equals(0))
                {
                    careaEnt = house.QueryAreaByDeliveryArea(new CargoAreaEntity { HouseID = wxOrderList[0].HouseID, DeliveryArea = wxOrderList[0].City });
                    if (careaEnt.AreaID.Equals(0))
                    {
                        //该省份未开放购买权限
                        msg.Message = "该省份未开放购买权限";
                        msg.Result = false;
                        goto ERR;
                    }
                }
                areaList = house.QueryAreaListByDeliveryArea(new CargoAreaEntity { HouseID = wxOrderList[0].HouseID, DeliveryArea = wxOrderList[0].City });
                CargoWeiXinBus wx = new CargoWeiXinBus();
                WXCouponEntity coupon = new WXCouponEntity();
                if (!wxOrderList[0].CouponID.Equals(0))
                {
                    coupon = wx.QueryCouponByID(new WXCouponEntity { ID = wxOrderList[0].CouponID });
                }
                CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxOrderList[0].HouseID);
                CargoOrderEntity order = new CargoOrderEntity();
                int OrderNum = 0;
                order.OrderNo = Common.GetMaxOrderNumByCurrentDate(wxOrderList[0].HouseID, houseEnt.HouseCode, out OrderNum); // Convert.ToString(Request["OrderNo"]);//生成最新顺序订单号
                order.OrderNum = OrderNum;//最新订单顺序号
                order.WXOrderNo = json;
                order.HAwbNo = "";
                order.Dep = houseEnt.DepCity; order.Dest = wxOrderList[0].City;
                order.Piece = wxOrderList[0].Piece; order.Weight = order.Volume = order.DeliveryFee = order.InsuranceFee = order.OtherFee = order.Rebate = 0.00M;
                order.TransitFee = wxOrderList[0].TransitFee;
                order.InsuranceFee = Convert.ToDecimal(coupon.Money);
                order.TransportFee = wxOrderList[0].TotalCharge - wxOrderList[0].TransitFee + Convert.ToDecimal(coupon.Money);
                order.TotalCharge = wxOrderList[0].TotalCharge;
                order.CheckOutType = "0";
                if (wxOrderList[0].PayWay.Equals("0"))
                {
                    order.CheckOutType = "5";
                }
                else if (wxOrderList[0].PayWay.Equals("1"))
                {
                    order.CheckOutType = "6";
                }
                order.TrafficType = order.DeliveryType = "0";
                order.ClientNum = wxOrderList[0].ClientNum;
                order.PayClientNum = wxOrderList[0].ClientNum;
                order.AcceptUnit = order.AcceptPeople = wxOrderList[0].Name;
                order.AcceptTelephone = order.AcceptCellphone = wxOrderList[0].Cellphone;
                order.AcceptAddress = wxOrderList[0].AcceptAddress;
                order.PayClientName = wxOrderList[0].Name;
                order.HouseID = wxOrderList[0].HouseID;
                order.PayWay = wxOrderList[0].PayWay;
                order.PayStatus = wxOrderList[0].PayStatus;
                if ((wxOrderList[0].PayWay.Equals("0") && wxOrderList[0].PayStatus.Equals("1")) || wxOrderList[0].PayWay.Equals("2"))
                {
                    //微信付款并且支付成功
                    order.FinanceSecondCheck = "1";
                    order.FinanceSecondCheckName = WxUserInfo.SysLoginName;
                    order.FinanceSecondCheckDate = DateTime.Now;
                }
                order.OP_ID = WxUserInfo.SysLoginID;
                order.AwbStatus = "1";//正在备货
                //order.OrderType = "2";//微信公众号下单 
                order.OrderType = wxOrderList[0].OrderType;
                order.OrderModel = "0";//订货单 
                order.Remark = wxOrderList[0].Memo;
                order.LogisID = wxOrderList[0].LogisID;
                order.CreateAwbID = WxUserInfo.SysLoginID;
                order.CreateAwb = WxUserInfo.SysLoginName;
                order.CreateDate = DateTime.Now;
                order.IsAppFirstOrder = wxOrderList[0].IsAppFirstOrder;
                order.CouponID = wxOrderList[0].CouponID;
                //CargoClientBus client = new CargoClientBus();
                //CargoClientEntity clientEnt = client.QueryCargoClient(order.ClientNum);
                WXUserAddressEntity clerkEntity = wx.QueryWxAreaClient(new WXUserAddressEntity { Province = wxOrderList[0].Province, City = wxOrderList[0].City });
                if (!string.IsNullOrEmpty(clerkEntity.LoginName))
                {
                    order.SaleManID = clerkEntity.LoginName;
                    order.SaleManName = clerkEntity.UserName;
                }
                //order.SaleManID = clientEnt.UserID;
                //order.SaleManName = clientEnt.UserName;
                order.OutHouseName = careaEnt.Name;//出库仓库名称

                List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();
                List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
                string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
                CargoInterfaceBus interBus = new CargoInterfaceBus();
                bool bz = false;
                foreach (var it in wxOrderList)
                {
                    if (it.HouseID.Equals(9) && it.TypeID.Equals(34)) { bz = true; }
                    int piece = it.OrderNum;
                    if (it.SaleType.Equals("1") || it.PayWay.Equals("2"))
                    {
                        #region 直接出库
                        entDest.Add(new CargoOrderGoodsEntity
                        {
                            OrderNo = order.OrderNo,
                            ProductID = it.ProductID,
                            HouseID = it.HouseID,
                            AreaID = it.AreaID,
                            Piece = it.OrderNum,
                            ActSalePrice = it.OrderPrice,
                            ContainerCode = it.ContainerCode,
                            OutCargoID = outID,
                            OP_ID = WxUserInfo.SysLoginID
                        });

                        CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                        cargo.OrderNo = order.OrderNo;//订单号
                        cargo.OutCargoID = outID;
                        cargo.ContainerID = it.ContainerID;
                        cargo.TypeID = it.TypeID;
                        cargo.ProductID = it.ProductID;
                        cargo.Piece = it.OrderNum;//出库件数
                        cargo.ActSalePrice = it.OrderPrice;
                        cargo.InPiece = it.InCargoPiece;
                        cargo.ID = it.InContainID;

                        //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
                        cargo.HouseName = houseEnt.Name;
                        cargo.AreaName = it.AreaName;
                        cargo.ProductName = it.ProductName;
                        cargo.Model = it.Model;
                        cargo.Specs = it.Specs;
                        cargo.Figure = it.Figure;
                        outHouseList.Add(cargo);
                        #endregion
                    }
                    else
                    {
                        #region 优先出库方法
                        bool noTyre = false;//没有库存
                        CargoInterfaceEntity stockEntity = new CargoInterfaceEntity
                        {
                            HouseID = it.HouseID,
                            AreaID = careaEnt.AreaID,
                            //GoodsCode = it.GoodsCode,
                            Specs = it.Specs,
                            TypeID = it.TypeID,
                            Figure = it.Figure,
                            BatchYear = it.BatchYear,
                            //LoadIndex = it.LoadIndex,
                            //SpeedLevel = it.SpeedLevel,
                        };
                        List<CargoInterfaceEntity> stockList = interBus.queryMiniCargoStock(stockEntity);
                        if (stockList.Count <= 0)
                        {
                            msg.Result = false;
                            msg.Message = "商品名称：" + it.Title + "库存为空";
                            noTyre = true;
                            //goto ERR;
                        }
                        if (stockList.Sum(c => c.StockNum) < piece)
                        {
                            msg.Result = false;
                            msg.Message = "商品名称：" + it.Title + "库存不足，库存只剩：" + stockList.Sum(c => c.StockNum).ToString();
                            noTyre = true;
                            //goto ERR;
                        }
                        bool HaveTyre = false;
                        //查询另外仓库
                        if (noTyre)
                        {
                            foreach (var area in areaList)
                            {
                                if (area.AreaID.Equals(careaEnt.AreaID)) { continue; }
                                CargoInterfaceEntity stockEntityA = new CargoInterfaceEntity
                                {
                                    HouseID = it.HouseID,
                                    AreaID = area.AreaID,
                                    //GoodsCode = it.GoodsCode,
                                    Specs = it.Specs,
                                    TypeID = it.TypeID,
                                    Figure = it.Figure,
                                    BatchYear = it.BatchYear,
                                    //LoadIndex = it.LoadIndex,
                                    //SpeedLevel = it.SpeedLevel,
                                };
                                int curKC = stockList.Sum(c => c.StockNum);//现在库存
                                List<CargoInterfaceEntity> stock = interBus.queryMiniCargoStock(stockEntityA);
                                if (stock.Count <= 0)
                                {
                                    msg.Result = false;
                                    msg.Message = "商品名称：" + it.Title + "库存为空";
                                    continue;
                                }
                                stockList.AddRange(stock);
                                if (stock.Sum(c => c.StockNum) + curKC < piece)
                                {
                                    msg.Result = false;
                                    msg.Message = "商品名称：" + it.Title + "库存不足，库存只剩：" + (stockList.Sum(c => c.StockNum) + curKC).ToString();
                                    continue;
                                }
                                order.OutHouseName = area.Name;//出库仓库名称
                                HaveTyre = true; break;
                            }
                        }
                        if (!noTyre || (noTyre && HaveTyre))
                        {
                            //减库存规则，一周期早的先出先进先出，二数量和库存数刚好一样的先出
                            foreach (var kc in stockList)
                            {
                                if (kc.StockNum <= 0) { continue; }
                                CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                                cargo.OrderNo = order.OrderNo;//订单号
                                cargo.OutCargoID = outID;
                                cargo.ContainerID = kc.ContainerID;
                                cargo.TypeID = kc.TypeID;
                                cargo.ProductID = kc.ProductID;

                                cargo.ID = kc.ContainerGoodsID;//库存表ID

                                //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
                                cargo.HouseName = houseEnt.Name;
                                //cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
                                cargo.ProductName = kc.ProductName;
                                cargo.GoodsCode = it.GoodsCode;
                                cargo.Model = kc.Model;
                                cargo.Specs = kc.Specs;
                                cargo.Figure = kc.Figure;
                                #region 减库存逻辑
                                if (piece < kc.StockNum)
                                {
                                    //部分出
                                    entDest.Add(new CargoOrderGoodsEntity
                                    {
                                        OrderNo = order.OrderNo,
                                        ProductID = kc.ProductID,
                                        HouseID = order.HouseID,
                                        AreaID = kc.AreaID,
                                        Piece = piece,
                                        ActSalePrice = it.OrderPrice,
                                        ContainerCode = kc.ContainerCode,
                                        OutCargoID = outID,
                                        OP_ID = log.UserID
                                    });
                                    cargo.Piece = piece;
                                    cargo.ActSalePrice = it.OrderPrice;
                                    cargo.InPiece = kc.StockNum;

                                    outHouseList.Add(cargo);
                                    break;
                                }
                                if (piece.Equals(kc.StockNum))
                                {
                                    //要出库件数和第一条库存件数刚刚好，则就全部出
                                    entDest.Add(new CargoOrderGoodsEntity
                                    {
                                        OrderNo = order.OrderNo,
                                        ProductID = kc.ProductID,
                                        HouseID = order.HouseID,
                                        AreaID = kc.AreaID,
                                        Piece = piece,
                                        ActSalePrice = it.OrderPrice,
                                        ContainerCode = kc.ContainerCode,
                                        OutCargoID = outID,
                                        OP_ID = log.UserID
                                    });
                                    cargo.Piece = piece;
                                    cargo.ActSalePrice = it.OrderPrice;
                                    cargo.InPiece = kc.StockNum;

                                    outHouseList.Add(cargo);
                                    break;
                                }
                                if (piece > kc.StockNum)
                                {
                                    //全部出
                                    entDest.Add(new CargoOrderGoodsEntity
                                    {
                                        OrderNo = order.OrderNo,
                                        ProductID = kc.ProductID,
                                        HouseID = order.HouseID,
                                        AreaID = kc.AreaID,
                                        Piece = kc.StockNum,
                                        ActSalePrice = it.OrderPrice,
                                        ContainerCode = kc.ContainerCode,
                                        OutCargoID = outID,
                                        OP_ID = log.UserID
                                    });
                                    cargo.Piece = kc.StockNum;
                                    cargo.ActSalePrice = it.OrderPrice;
                                    cargo.InPiece = kc.StockNum;

                                    outHouseList.Add(cargo);
                                    piece = piece - kc.StockNum;
                                    continue;
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            goto ERR;
                        }

                        #endregion
                    }
                }
                if (bz) { order.Remark += "  打包装"; }
                order.goodsList = entDest;
                //仓库同步缓存
                foreach (CargoContainerShowEntity time in outHouseList)
                {
                    CargoProductEntity syncProduct = house.SyncTypeProduct(time.ProductID.ToString());

                    if (Common.IsAllSyncStock(syncProduct.HouseID, syncProduct.TypeID, "Cass"))
                    {
                        RedisHelper.HashSet("OpenSystemStockSyc", "" + syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode + "", syncProduct.GoodsCode);
                    }
                    if (Common.IsAllSyncStock(syncProduct.HouseID, syncProduct.TypeID, "DILE"))
                    {
                        RedisHelper.HashSet("HCYCHouseStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.ProductCode);
                    }
                    if (Common.IsAllSyncStock(syncProduct.HouseID, syncProduct.TypeID, "Tuhu"))
                    {
                        RedisHelper.HashSet("TuhuStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.ProductCode);
                    }


                }

                bus.makeSureWxOrder(order, outHouseList, log);
                //bus.DeleteOrderInfo(list, log);
                msg.Result = true;
                msg.Message = "成功";
                //if (order.HouseID.Equals(9) || order.HouseID.Equals(15) || order.HouseID.Equals(49) || order.HouseID.Equals(50) || order.HouseID.Equals(51) || order.HouseID.Equals(52) || order.HouseID.Equals(53) || order.HouseID.Equals(54))
                //{
                //    //内部订单
                //    bus.SaveHlyOrderData(outHouseList, order);
                //}
                //else
                //{
                    bus.InsertCargoOrderPush(new CargoOrderPushEntity
                    {
                        OrderNo = order.OrderNo,
                        Dep = order.Dep,
                        Dest = order.Dest,
                        Piece = order.Piece,
                        ClientNum = order.ClientNum.ToString(),
                        AcceptAddress = order.AcceptAddress,
                        AcceptCellphone = order.AcceptCellphone,
                        AcceptTelephone = order.AcceptTelephone,
                        AcceptPeople = order.AcceptPeople,
                        AcceptUnit = order.AcceptUnit,
                        HouseID = order.HouseID.ToString(),
                        HouseName = houseEnt.Name,
                        OP_ID = WxUserInfo.SysLoginID,
                        PushType = "0",
                        PushStatus = "0",
                        LogisID = order.LogisID,
                        BusinessID="12"
                    }, log);
                //}
                if (!string.IsNullOrEmpty(wxOrderList[0].wxOpenID))
                {
                    //仓库确认成功向客户微信发送推送消息
                    //推送客户消息
                    TemplateMsg tmMsg = new TemplateMsg
                    {
                        first = new TemplateDataItem("尊敬的迪乐泰客户，您的订单仓库已确认！~正在秒速为您安排发货，请耐心等待哦~", "#173177"),
                        keyword1 = new TemplateDataItem(wxOrderList[0].Title, "#173177"),
                        keyword2 = new TemplateDataItem(wxOrderList[0].TotalCharge.ToString("F2") + "元", "#173177"),
                        keyword3 = new TemplateDataItem(wxOrderList[0].OrderNo, "#173177"),
                        remark = new TemplateDataItem("点击查看物流跟踪详情!", "#173177")
                    };
                    SendTempleteMessage send = new SendTempleteMessage();
                    //string token = AccessTokenContainer.TryGetAccessToken(Common.GetdltAPPID(), Common.GetdltAppSecret(), false);
                    string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());
                    //oo1LEt_z6Yhx-g_qdgrYhLaYaazE//测试
                    string errmsg = send.SendMessage(token, wxOrderList[0].wxOpenID, "heDfICkOgVf3gfod4kM6CL4MiK4YikrFGm6k9QgUZ8A", "http://dlt.neway5.com/Weixin/OrderInfo.aspx?orderNo=" + wxOrderList[0].OrderNo, tmMsg);
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        ERR:
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 查询特价胎
        /// </summary>
        public void QuerySpecialSaleData()
        {
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            string res = Convert.ToString(Request["Specs"]);
            if (!res.Contains("/") && !res.ToUpper().Contains("R"))
            {
                if (res.Length <= 3)
                {
                    queryEntity.Specs = res;
                }
                if (res.Length > 3 && res.Length <= 5)
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3);
                }
                if (res.Length > 5)
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5);
                }
            }
            if (res.ToUpper().Contains("F"))
            {
                queryEntity.Specs = res;

                if (!res.ToUpper().Contains("/"))
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3);
                }
            }
            if (res.ToUpper().Contains("R") && res.ToUpper().Contains("C"))
            {
                queryEntity.Specs = res;
            }
            string tna = string.Empty;
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"])))
            {
                string[] tn = Convert.ToString(Request["TypeID"]).Split('/');
                if (tn.Length > 0)
                {
                    for (int i = 0; i < tn.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(tn[i]))
                        {
                            tna += tn[i] + ",";
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(tna))
            {
                queryEntity.TypeName = tna.Substring(0, tna.Length - 1);
            }
            queryEntity.SaleType = Convert.ToString(Request["SaleType"]);
            //queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]);
            queryEntity.OnShelves = "0";
            queryEntity.HouseID = WxUserInfo.HouseID;

            CargoProductBus bus = new CargoProductBus();
            List<CargoContainerShowEntity> list = bus.QuerySpecialSaleData(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            //Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 自动处理所有已上架的推广商品
        /// </summary>
        public void AutoUnShelveAdvertProduct()
        {
            CargoProductBus bus = new CargoProductBus();
            bus.AutoUnShelveAdvertProduct();
        }
        private string GetOrderNumber()
        {
            string Number = DateTime.Now.ToString("yyMMddHHmmss");
            return Number + Next(1000, 1).ToString();
        }
        private static int Next(int numSeeds, int length)
        {
            byte[] buffer = new byte[length];
            System.Security.Cryptography.RNGCryptoServiceProvider Gen = new System.Security.Cryptography.RNGCryptoServiceProvider();
            Gen.GetBytes(buffer);
            uint randomResult = 0x0;
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)buffer[i] << ((length - 1 - i) * 8));
            }
            return (int)(randomResult % numSeeds);
        }
        /// <summary>
        /// 积分兑换
        /// </summary>
        public void ConsumeConvert()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            Int64 ProductID = Convert.ToInt64(Request["ProductID"]);
            Int64 ShelveID = Convert.ToInt64(Request["ShelveID"]);

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "积分兑换";
            log.UserID = WxUserInfo.wxOpenID;
            log.Operate = "A";

            CargoWeiXinBus wbus = new CargoWeiXinBus();
            WXUserEntity wxUser = wbus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = WxUserInfo.wxOpenID });
            CargoProductEntity result = wbus.QueryOnShelvesByID(ShelveID);
            if (wxUser.ConsumerPoint < result.Consume)
            {
                msg.Message = "积分不足，您有：" + wxUser.ConsumerPoint.ToString() + "积分";
                msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
            }
            List<WXUserAddressEntity> wxAddress = wbus.QueryWxAddressByWXID(new WXUserAddressEntity { WXID = WxUserInfo.ID });
            if (wxAddress.Count <= 0)
            {
                msg.Message = "请添加收货地址！";
                msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
            }
            string orderno = GetOrderNumber();
            List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
            pro.Add(new CargoProductShelvesEntity
            {
                ID = ShelveID,
                OrderNum = 1,
                OrderPrice = 0
            });
            wbus.SaveWeixinOrder(new WXOrderEntity
            {
                OrderNo = orderno,
                TotalCharge = 0,
                WXID = WxUserInfo.ID,
                PayStatus = "1",
                OrderStatus = "0",
                PayWay = "2",
                Piece = 1,
                Address = wxAddress[0].Address,
                Cellphone = wxAddress[0].Cellphone,
                City = wxAddress[0].City,
                Province = wxAddress[0].Province,
                Country = wxAddress[0].Country,
                Name = wxAddress[0].Name,
                HouseID = WxUserInfo.HouseID,
                Memo = "",
                LogisID = WxUserInfo.LogisID,
                LogicName = WxUserInfo.LogicName,
                productList = pro
            }, log);
            //扣除积分
            wbus.ConsumeConvert(new WXOrderEntity { WXID = WxUserInfo.ID, OrderNo = orderno, TotalCharge = result.Consume });

            //推送客户消息
            TemplateMsg tmMsg = new TemplateMsg
            {
                first = new TemplateDataItem("尊敬的迪乐泰客户，积分兑换礼品成功！", "#173177"),
                keyword1 = new TemplateDataItem(result.Title + "1件", "#173177"),
                keyword2 = new TemplateDataItem(result.Consume + "积分", "#173177"),
                keyword3 = new TemplateDataItem("积分兑换", "#173177"),
                keyword4 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "#173177"),
                keyword5 = new TemplateDataItem(wxAddress[0].Province + " " + wxAddress[0].City + " " + wxAddress[0].Country + " " + wxAddress[0].Address, "#173177"),
                remark = new TemplateDataItem("点击查看订单详情!", "#173177")
            };
            SendTempleteMessage send = new SendTempleteMessage();

            //string token = AccessTokenContainer.TryGetAccessToken(Common.GetdltAPPID(), Common.GetdltAppSecret(), true);
            //WriteTextLog(token);
            string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());
            //oo1LEt_z6Yhx-g_qdgrYhLaYaazE//测试
            string errmsg = send.SendMessage(token, WxUserInfo.wxOpenID, "kAbX9tpdCB8Sz3TLYwDqk7k4ytQ7ZJLUG3RcdofXp6A", "http://dlt.neway5.com/Weixin/OrderInfo.aspx?orderNo=" + orderno, tmMsg);
            //WriteTextLog("02");
            //推送给客户的所属业务员消息
            WXUserAddressEntity clerkEntity = wbus.QueryWxAreaClient(new WXUserAddressEntity { Province = wxAddress[0].Province, City = wxAddress[0].City });
            if (!string.IsNullOrEmpty(clerkEntity.wxOpenID))
            {
                TemplateMsg bustmMsg = new TemplateMsg
                {
                    first = new TemplateDataItem("客户积分兑换，您的客户" + wxAddress[0].Name + "积分兑换成功！", "#173177"),
                    keyword1 = new TemplateDataItem(result.Title + "1件", "#173177"),
                    keyword2 = new TemplateDataItem(result.Consume + "积分", "#173177"),
                    keyword3 = new TemplateDataItem("积分兑换", "#173177"),
                    keyword4 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "#173177"),
                    keyword5 = new TemplateDataItem(wxAddress[0].Province + " " + wxAddress[0].City + " " + wxAddress[0].Country + " " + wxAddress[0].Address, "#173177"),
                    remark = new TemplateDataItem("点击查看订单详情并确认订单!", "#173177")
                };
                string buserrmsg = send.SendMessage(token, clerkEntity.wxOpenID, "kAbX9tpdCB8Sz3TLYwDqk7k4ytQ7ZJLUG3RcdofXp6A", "http://dlt.neway5.com/Weixin/OrderInfoManager.aspx?orderNo=" + orderno, bustmMsg);
            }

            string noticeArray = string.Empty;
            if (WxUserInfo.HouseID.Equals(1))
            {
                //湖南仓库
                noticeArray = Common.GetdltGetOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(3))
            {
                //湖北仓库
                noticeArray = Common.GetdltGetHuBeiOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(11))
            {
                //西安迪乐泰 
                noticeArray = Common.GetdltGetXiAnOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(12))
            {
                //梅州 揭阳仓库
                noticeArray = Common.GetdltGetMeiZhouOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(9))
            {
                //广州仓库
                noticeArray = Common.GetdltGetGuangZhouOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(34))
            {
                //广州仓库
                noticeArray = Common.GetdltGetHaiNanOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(44))
            {
                //揭阳仓库
                noticeArray = Common.GetdltGetJieYangOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(46))
            {
                //四川仓库
                noticeArray = Common.GetdltGetSiChuanOrderNotice();
            }
            if (!string.IsNullOrEmpty(noticeArray))
            {
                string[] notice = noticeArray.Split('/');
                for (int i = 0; i < notice.Length; i++)
                {
                    if (!string.IsNullOrEmpty(notice[i]))
                    {
                        //推送客户消息
                        TemplateMsg tmMs = new TemplateMsg
                        {
                            first = new TemplateDataItem("有新订单,请尽快确认订单并拣货出库发货！", "#173177"),
                            keyword1 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "1件", "#173177"),
                            keyword2 = new TemplateDataItem(result.Title, "#173177"),
                            keyword3 = new TemplateDataItem(result.Consume + "积分", "#173177"),
                            keyword4 = new TemplateDataItem(wxAddress[0].Province + " " + wxAddress[0].City + " " + wxAddress[0].Country + " " + wxAddress[0].Address, "#173177"),
                            keyword5 = new TemplateDataItem("积分兑换", "#173177"),
                            remark = new TemplateDataItem("点击查看订单详情并确认订单!", "#173177")
                        };

                        string err = send.SendMessage(token, notice[i], "tocpvplR_z6_K6R8stAbMHhEeYhVm_BaQ3MF4iC9vDw", "http://dlt.neway5.com/Weixin/OrderInfoManager.aspx?orderNo=" + orderno, tmMs);
                    }
                }
            }


            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        /// <summary>
        /// 查询推广促销销售轮胎数量
        /// </summary>
        public void QueryPurchaseNum()
        {
            int ShelveID = Convert.ToInt32(Request["ID"]);//上架表ID
            int Num = Convert.ToInt32(Request["Num"]);//购买数量
            CargoWeiXinBus wbus = new CargoWeiXinBus();
            int result = wbus.QueryPurchaseNum(new WXOrderEntity { WXID = WxUserInfo.ID, StartDate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)), EndDate = DateTime.Now, ID = ShelveID });

            //JSON
            //String json = JSON.Encode(result);
            //Response.Clear();
            Response.Write((result + Num).ToString());
        }
        /// <summary>
        /// 各仓库轮胎微信商城销售数据统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void QueryWeixinMalReportData()
        {
            CargoWeixinMallReportEntity queryEntity = new CargoWeixinMallReportEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            CargoReportBus bus = new CargoReportBus();
            List<CargoWeixinMallReportEntity> list = bus.QueryWeixinMalReportData(queryEntity);
            List<CargoWeixinMallReportEntity> footlist = new List<CargoWeixinMallReportEntity>();
            footlist.Add(new CargoWeixinMallReportEntity
            {
                ReportDate = "汇总：",
                HNOrderClientNum = list.Sum(c => c.HNOrderClientNum),
                HNOrderNum = list.Sum(c => c.HNOrderNum),
                HNSaleTyreNum = list.Sum(c => c.HNSaleTyreNum),
                HNSaleTotalCharge = list.Sum(c => c.HNSaleTotalCharge),
                HBOrderClientNum = list.Sum(c => c.HBOrderClientNum),
                HBOrderNum = list.Sum(c => c.HBOrderNum),
                HBSaleTotalCharge = list.Sum(c => c.HBSaleTotalCharge),
                HBSaleTyreNum = list.Sum(c => c.HBSaleTyreNum),
                HASaleTyreNum = list.Sum(c => c.HASaleTyreNum),
                HASaleTotalCharge = list.Sum(c => c.HASaleTotalCharge),
                HAOrderClientNum = list.Sum(c => c.HAOrderClientNum),
                HAOrderNum = list.Sum(c => c.HAOrderNum),
                XBOrderClientNum = list.Sum(c => c.XBOrderClientNum),
                XBOrderNum = list.Sum(c => c.XBOrderNum),
                XBSaleTotalCharge = list.Sum(c => c.XBSaleTotalCharge),
                XBSaleTyreNum = list.Sum(c => c.XBSaleTyreNum),
                MZOrderClientNum = list.Sum(c => c.MZOrderClientNum),
                MZOrderNum = list.Sum(c => c.MZOrderNum),
                MZSaleTotalCharge = list.Sum(c => c.MZSaleTotalCharge),
                MZSaleTyreNum = list.Sum(c => c.MZSaleTyreNum),
                GZOrderClientNum = list.Sum(c => c.GZOrderClientNum),
                GZOrderNum = list.Sum(c => c.GZOrderNum),
                GZSaleTotalCharge = list.Sum(c => c.GZSaleTotalCharge),
                GZSaleTyreNum = list.Sum(c => c.GZSaleTyreNum),
                JYOrderClientNum = list.Sum(c => c.JYOrderClientNum),
                JYOrderNum = list.Sum(c => c.JYOrderNum),
                JYSaleTotalCharge = list.Sum(c => c.JYSaleTotalCharge),
                JYSaleTyreNum = list.Sum(c => c.JYSaleTyreNum),
                DayOrderClientNum = list.Sum(c => c.DayOrderClientNum),
                DayOrderNum = list.Sum(c => c.DayOrderNum),
                DaySaleTotalCharge = list.Sum(c => c.DaySaleTotalCharge),
                DaySaleTyreNum = list.Sum(c => c.DaySaleTyreNum)
            });
            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = footlist;
            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 月统计
        /// </summary>
        public void QueryWeixinMallStatisData()
        {
            CargoReportBus bus = new CargoReportBus();
            List<CargoWeixinMallReportEntity> list = bus.QueryWeixinMallStatisData(new CargoWeixinMallReportEntity { StatisType = "2" });
            List<CargoWeixinMallReportEntity> result = new List<CargoWeixinMallReportEntity>();
            if (list.Count > 0)
            {
                foreach (var it in list)
                {
                    if (result.Exists(c => c.ReportDate.Equals(it.ReportDate)))
                    {
                        CargoWeixinMallReportEntity en = result.Find(c => c.ReportDate.Equals(it.ReportDate));
                        en.DayOrderClientNum += it.DayOrderClientNum;
                        en.DayOrderNum += it.DayOrderNum;
                        en.DaySaleTyreNum += it.DaySaleTyreNum;
                        en.DaySaleTotalCharge += it.DaySaleTotalCharge;
                    }
                    else
                    {
                        result.Add(it);
                    }
                }
            }

            List<CargoWeixinMallReportEntity> monthEnt = new List<CargoWeixinMallReportEntity>();
            List<CargoWeixinMallReportEntity> month = bus.QueryWeixinMallStatisData(new CargoWeixinMallReportEntity { StatisType = "1" });
            if (month.Count > 0)
            {
                foreach (var it in month)
                {
                    if (monthEnt.Exists(c => c.ReportDate.Equals(it.ReportDate)))
                    {
                        CargoWeixinMallReportEntity en = monthEnt.Find(c => c.ReportDate.Equals(it.ReportDate));
                        en.DayOrderClientNum += it.DayOrderClientNum;
                        en.DayOrderNum += it.DayOrderNum;
                        en.DaySaleTyreNum += it.DaySaleTyreNum;
                        en.DaySaleTotalCharge += it.DaySaleTotalCharge;
                    }
                    else
                    {
                        monthEnt.Add(it);
                    }
                }
            }
            Hashtable resHT = new Hashtable();
            resHT["monthStatis"] = monthEnt;//月统计 
            resHT["weekStatis"] = result;//周统计
            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
        }
        #region  APP接口
        /// <summary>
        /// 查询我的收货地址
        /// </summary>
        public void QueryWxAddressByWXID()
        {
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<WXUserAddressEntity> result = bus.QueryWxAddressByWXID(new WXUserAddressEntity { WXID = WxUserInfo.ID });
            //JSON
            String json = JSON.Encode(result);
            Response.Clear();
            Response.ContentType = "Content-Type: application/json; charset=utf-8";
            Response.Write(json);
        }
        /// <summary>
        /// 积分兑换
        /// </summary>
        public void ConsumeExChange()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            Int64 ProductID = Convert.ToInt64(Request["ProductID"]);
            Int64 ShelveID = Convert.ToInt64(Request["ShelveID"]);
            int num = Convert.ToInt32(Request["OrderNum"]);
            string address = Convert.ToString(Request["Address"]);

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "积分兑换";
            log.UserID = WxUserInfo.UnionID;
            log.Operate = "A";
            //WriteTextLog(WxUserInfo.ID.ToString());
            #region 积分兑换逻辑
            CargoWeiXinBus wbus = new CargoWeiXinBus();
            WXUserEntity wxUser = wbus.QueryWeixinUserByID(new WXUserEntity { ID = WxUserInfo.ID });
            CargoProductEntity result = wbus.QueryOnShelvesByID(ShelveID);
            //WriteTextLog("JF1");
            if (wxUser.ConsumerPoint < result.Consume)
            {
                msg.Message = "积分不足，您有：" + wxUser.ConsumerPoint.ToString() + "积分";
                msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
            }
            WXUserAddressEntity addressEnt = new WXUserAddressEntity();
            ArrayList addre = (ArrayList)JSON.Decode(address);
            foreach (Hashtable dz in addre)
            {
                addressEnt.Name = Convert.ToString(dz["Name"]);
                addressEnt.Province = Convert.ToString(dz["Province"]);
                addressEnt.City = Convert.ToString(dz["City"]);
                addressEnt.Country = Convert.ToString(dz["Country"]);
                addressEnt.Address = Convert.ToString(dz["Address"]);
                addressEnt.Cellphone = Convert.ToString(dz["Cellphone"]);
            }
            //WriteTextLog("JF2");
            string orderno = GetOrderNumber();
            List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
            pro.Add(new CargoProductShelvesEntity
            {
                ID = ShelveID,
                OrderNum = num,
                OrderPrice = 0
            });
            wbus.SaveWeixinOrder(new WXOrderEntity
            {
                OrderNo = orderno,
                TotalCharge = 0,
                WXID = wxUser.ID,
                PayStatus = "1",
                OrderStatus = "0",
                PayWay = "2",
                Piece = num,
                Address = addressEnt.Address,
                Cellphone = addressEnt.Cellphone,
                City = addressEnt.City,
                Province = addressEnt.Province,
                Country = addressEnt.Country,
                Name = addressEnt.Name,
                HouseID = WxUserInfo.HouseID,
                Memo = "",
                LogisID = wxUser.LogisID,
                LogicName = wxUser.LogicName,
                productList = pro
            }, log);
            //扣除积分
            wbus.ConsumeConvert(new WXOrderEntity { WXID = wxUser.ID, OrderNo = orderno, TotalCharge = result.Consume });

            #endregion

            SendTempleteMessage send = new SendTempleteMessage();
            string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());
            #region 向客户推送微信通知逻辑
            try
            {
                if (!string.IsNullOrEmpty(wxUser.wxOpenID))
                {
                    //推送客户消息
                    TemplateMsg tmMsg = new TemplateMsg
                    {
                        first = new TemplateDataItem("尊敬的迪乐泰客户，积分兑换礼品成功！", "#173177"),
                        keyword1 = new TemplateDataItem(result.Title + "1件", "#173177"),
                        keyword2 = new TemplateDataItem(result.Consume + "积分", "#173177"),
                        keyword3 = new TemplateDataItem("积分兑换", "#173177"),
                        keyword4 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "#173177"),
                        keyword5 = new TemplateDataItem(addressEnt.Province + " " + addressEnt.City + " " + addressEnt.Country + " " + addressEnt.Address, "#173177"),
                        remark = new TemplateDataItem("点击查看订单详情!", "#173177")
                    };
                    string errmsg = send.SendMessage(token, wxUser.wxOpenID, "kAbX9tpdCB8Sz3TLYwDqk7k4ytQ7ZJLUG3RcdofXp6A", "http://dlt.neway5.com/Weixin/OrderInfo.aspx?orderNo=" + orderno, tmMsg);
                }
            }
            catch (Exception) { }
            #endregion

            //推送给客户的所属业务员消息
            #region 推送给客户的所属业务员消息
            WXUserAddressEntity clerkEntity = wbus.QueryWxAreaClient(new WXUserAddressEntity { Province = addressEnt.Province, City = addressEnt.City });
            if (!string.IsNullOrEmpty(clerkEntity.wxOpenID))
            {
                TemplateMsg bustmMsg = new TemplateMsg
                {
                    first = new TemplateDataItem("客户积分兑换，您的客户" + addressEnt.Name + "积分兑换成功！", "#173177"),
                    keyword1 = new TemplateDataItem(result.Title + "1件", "#173177"),
                    keyword2 = new TemplateDataItem(result.Consume + "积分", "#173177"),
                    keyword3 = new TemplateDataItem("积分兑换", "#173177"),
                    keyword4 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "#173177"),
                    keyword5 = new TemplateDataItem(addressEnt.Province + " " + addressEnt.City + " " + addressEnt.Country + " " + addressEnt.Address, "#173177"),
                    remark = new TemplateDataItem("点击查看订单详情并确认订单!", "#173177")
                };
                string buserrmsg = send.SendMessage(token, clerkEntity.wxOpenID, "kAbX9tpdCB8Sz3TLYwDqk7k4ytQ7ZJLUG3RcdofXp6A", "http://dlt.neway5.com/Weixin/OrderInfoManager.aspx?orderNo=" + orderno, bustmMsg);
            }
            #endregion

            #region 推送仓库管理和客服下单通知

            string noticeArray = string.Empty;
            if (WxUserInfo.HouseID.Equals(1))
            {
                //湖南仓库
                noticeArray = Common.GetdltGetOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(3))
            {
                //湖北仓库
                noticeArray = Common.GetdltGetHuBeiOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(11))
            {
                //西安迪乐泰 
                noticeArray = Common.GetdltGetXiAnOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(12))
            {
                //梅州 揭阳仓库
                noticeArray = Common.GetdltGetMeiZhouOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(9))
            {
                //广州仓库
                noticeArray = Common.GetdltGetGuangZhouOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(34))
            {
                //海南仓库
                noticeArray = Common.GetdltGetHaiNanOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(44))
            {
                //揭阳仓库
                noticeArray = Common.GetdltGetJieYangOrderNotice();
            }
            else if (WxUserInfo.HouseID.Equals(46))
            {
                //四川仓库
                noticeArray = Common.GetdltGetSiChuanOrderNotice();
            }
            if (!string.IsNullOrEmpty(noticeArray))
            {
                string[] notice = noticeArray.Split('/');
                for (int i = 0; i < notice.Length; i++)
                {
                    if (!string.IsNullOrEmpty(notice[i]))
                    {
                        //推送客户消息
                        TemplateMsg tmMs = new TemplateMsg
                        {
                            first = new TemplateDataItem("有新订单,请尽快确认订单并拣货出库发货！", "#173177"),
                            keyword1 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "1件", "#173177"),
                            keyword2 = new TemplateDataItem(result.Title, "#173177"),
                            keyword3 = new TemplateDataItem(result.Consume + "积分", "#173177"),
                            keyword4 = new TemplateDataItem(addressEnt.Province + " " + addressEnt.City + " " + addressEnt.Country + " " + addressEnt.Address, "#173177"),
                            keyword5 = new TemplateDataItem("积分兑换", "#173177"),
                            remark = new TemplateDataItem("点击查看订单详情并确认订单!", "#173177")
                        };

                        string err = send.SendMessage(token, notice[i], "tocpvplR_z6_K6R8stAbMHhEeYhVm_BaQ3MF4iC9vDw", "http://dlt.neway5.com/Weixin/OrderInfoManager.aspx?orderNo=" + orderno, tmMs);
                    }
                }
            }
            #endregion

            //确认订单
            MakeSureConsumeOrder(orderno);
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        /// <summary>
        /// 确认积分兑换订单
        /// </summary>
        private void MakeSureConsumeOrder(string ConOrder)
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信商城订单";
            log.Status = "0";
            log.NvgPage = "仓库确认订单";
            log.UserID = WxUserInfo.SysLoginID;
            log.Operate = "A";

            WXOrderManagerEntity queryEntity = new WXOrderManagerEntity();
            queryEntity.OrderNo = ConOrder;
            CargoOrderBus bus = new CargoOrderBus();
            List<WXOrderManagerEntity> wxOrderList = bus.QueryContainerGoodsMakeSureWeixinOrder(queryEntity);
            CargoHouseBus house = new CargoHouseBus();
            CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxOrderList[0].HouseID);
            CargoOrderEntity order = new CargoOrderEntity();
            int OrderNum = 0;
            order.OrderNo = Common.GetMaxOrderNumByCurrentDate(wxOrderList[0].HouseID, houseEnt.HouseCode, out OrderNum); // Convert.ToString(Request["OrderNo"]);//生成最新顺序订单号
            order.OrderNum = OrderNum;//最新订单顺序号
            order.WXOrderNo = ConOrder;
            order.HAwbNo = "";
            order.Dep = houseEnt.DepCity; order.Dest = wxOrderList[0].City;
            order.Piece = wxOrderList[0].Piece; order.Weight = order.Volume = order.TransitFee = order.DeliveryFee = order.InsuranceFee = order.OtherFee = order.Rebate = 0.00M;
            order.TransportFee = order.TotalCharge = wxOrderList[0].TotalCharge;
            order.CheckOutType = "0";
            order.TrafficType = order.DeliveryType = "0";
            order.ClientNum = wxOrderList[0].ClientNum;
            order.AcceptUnit = order.AcceptPeople = wxOrderList[0].Name;
            order.AcceptTelephone = order.AcceptCellphone = wxOrderList[0].Cellphone;
            order.AcceptAddress = wxOrderList[0].AcceptAddress;
            order.HouseID = wxOrderList[0].HouseID;
            order.PayWay = wxOrderList[0].PayWay;
            order.PayStatus = wxOrderList[0].PayStatus;
            if ((wxOrderList[0].PayWay.Equals("0") && wxOrderList[0].PayStatus.Equals("1")) || wxOrderList[0].PayWay.Equals("2"))
            {
                //微信付款并且支付成功
                order.FinanceSecondCheck = "1";
                order.FinanceSecondCheckName = WxUserInfo.SysLoginName;
                order.FinanceSecondCheckDate = DateTime.Now;
            }
            order.OP_ID = WxUserInfo.SysLoginID;
            order.AwbStatus = "1";//正在备货
            //order.OrderType = "2";//微信公众号下单 
            order.OrderType = wxOrderList[0].OrderType;
            order.OrderModel = "0";//订货单 
            order.Remark = wxOrderList[0].Memo;
            order.LogisID = wxOrderList[0].LogisID;
            order.CreateAwbID = WxUserInfo.SysLoginID;
            order.CreateAwb = WxUserInfo.SysLoginName;
            order.CreateDate = DateTime.Now;
            //CargoClientBus client = new CargoClientBus();
            //CargoClientEntity clientEnt = client.QueryCargoClient(order.ClientNum);
            CargoWeiXinBus wx = new CargoWeiXinBus();
            WXUserAddressEntity clerkEntity = wx.QueryWxAreaClient(new WXUserAddressEntity { Province = wxOrderList[0].Province, City = wxOrderList[0].City });
            if (!string.IsNullOrEmpty(clerkEntity.LoginName))
            {
                order.SaleManID = clerkEntity.LoginName;
                order.SaleManName = clerkEntity.UserName;
            }
            order.OutHouseName = houseEnt.Name;//出库仓库名称

            List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();
            List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
            string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
            CargoInterfaceBus interBus = new CargoInterfaceBus();
            bool bz = false;
            foreach (var it in wxOrderList)
            {
                if (it.HouseID.Equals(9) && it.TypeID.Equals(34)) { bz = true; }
                int piece = it.OrderNum;
                #region 直接出库
                entDest.Add(new CargoOrderGoodsEntity
                {
                    OrderNo = order.OrderNo,
                    ProductID = it.ProductID,
                    HouseID = it.HouseID,
                    AreaID = it.AreaID,
                    Piece = it.OrderNum,
                    ActSalePrice = it.OrderPrice,
                    ContainerCode = it.ContainerCode,
                    OutCargoID = outID,
                    OP_ID = WxUserInfo.SysLoginID
                });

                CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                cargo.OrderNo = order.OrderNo;//订单号
                cargo.OutCargoID = outID;
                cargo.ContainerID = it.ContainerID;
                cargo.TypeID = it.TypeID;
                cargo.ProductID = it.ProductID;
                cargo.Piece = it.OrderNum;//出库件数
                cargo.ActSalePrice = it.OrderPrice;
                cargo.InPiece = it.InCargoPiece;
                cargo.ID = it.InContainID;

                //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
                cargo.HouseName = houseEnt.Name;
                cargo.AreaName = it.AreaName;
                cargo.ProductName = it.ProductName;
                cargo.Model = it.Model;
                cargo.Specs = it.Specs;
                cargo.Figure = it.Figure;
                outHouseList.Add(cargo);
                #endregion
            }
            if (bz) { order.Remark += "  打包装"; }
            order.goodsList = entDest;
            //仓库同步缓存
            foreach (CargoContainerShowEntity time in outHouseList)
            {
                CargoProductEntity syncProduct = house.SyncTypeProduct(time.ProductID.ToString());

                if (Common.IsAllSyncStock(syncProduct.HouseID, syncProduct.TypeID, "Cass"))
                {
                    RedisHelper.HashSet("OpenSystemStockSyc", "" + syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode + "", syncProduct.GoodsCode);
                }
                if (Common.IsAllSyncStock(syncProduct.HouseID, syncProduct.TypeID, "DILE"))
                {
                    RedisHelper.HashSet("HCYCHouseStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.ProductCode);
                }
                if (Common.IsAllSyncStock(syncProduct.HouseID, syncProduct.TypeID, "Tuhu"))
                {
                    RedisHelper.HashSet("TuhuStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.ProductCode);
                }


            }

            bus.makeSureWxOrder(order, outHouseList, log);
            //bus.DeleteOrderInfo(list, log);
            msg.Result = true;
            msg.Message = "成功";
            //if (order.HouseID.Equals(9) || order.HouseID.Equals(15) || order.HouseID.Equals(49) || order.HouseID.Equals(50) || order.HouseID.Equals(51) || order.HouseID.Equals(52) || order.HouseID.Equals(53) || order.HouseID.Equals(54))
            //{
            //    //内部订单
            //    bus.SaveHlyOrderData(outHouseList, order);
            //}
            //else
            //{
                bus.InsertCargoOrderPush(new CargoOrderPushEntity
                {
                    OrderNo = order.OrderNo,
                    Dep = order.Dep,
                    Dest = order.Dest,
                    Piece = order.Piece,
                    ClientNum = order.ClientNum.ToString(),
                    AcceptAddress = order.AcceptAddress,
                    AcceptCellphone = order.AcceptCellphone,
                    AcceptTelephone = order.AcceptTelephone,
                    AcceptPeople = order.AcceptPeople,
                    AcceptUnit = order.AcceptUnit,
                    HouseID = order.HouseID.ToString(),
                    HouseName = houseEnt.Name,
                    OP_ID = WxUserInfo.SysLoginID,
                    PushType = "0",
                    PushStatus = "0",
                    LogisID=order.LogisID,
                    BusinessID="12"
                }, log);
            //}
            try
            {
                //仓库确认成功向客户微信发送推送消息
                //推送客户消息
                TemplateMsg tmMsg = new TemplateMsg
                {
                    first = new TemplateDataItem("尊敬的迪乐泰客户，您的订单仓库已确认！~正在秒速为您安排发货，请耐心等待哦~", "#173177"),
                    keyword1 = new TemplateDataItem(wxOrderList[0].Title, "#173177"),
                    keyword2 = new TemplateDataItem(wxOrderList[0].TotalCharge.ToString("F2") + "元", "#173177"),
                    keyword3 = new TemplateDataItem(wxOrderList[0].OrderNo, "#173177"),
                    remark = new TemplateDataItem("点击查看物流跟踪详情!", "#173177")
                };
                SendTempleteMessage send = new SendTempleteMessage();
                //string token = AccessTokenContainer.TryGetAccessToken(Common.GetdltAPPID(), Common.GetdltAppSecret(), false);
                string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());
                //oo1LEt_z6Yhx-g_qdgrYhLaYaazE//测试
                string errmsg = send.SendMessage(token, wxOrderList[0].wxOpenID, "heDfICkOgVf3gfod4kM6CL4MiK4YikrFGm6k9QgUZ8A", "http://dlt.neway5.com/Weixin/OrderInfo.aspx?orderNo=" + wxOrderList[0].OrderNo, tmMsg);
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        }
        #endregion
    }
}