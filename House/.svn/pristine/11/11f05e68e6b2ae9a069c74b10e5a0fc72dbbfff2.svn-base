using Cargo.QY;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.Cargo.Interface;
using House.Entity.Cargo.Order;
using House.Entity.Cargo.Product;
using House.Entity.Cargo.WxApplet;
using iText.Kernel.Geom;
using iText.Kernel.XMP.Impl;
using iText.Layout.Element;
using Memcached.ClientLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Formula.Functions;
using NPOI.POIFS.Properties;
using NPOI.SS.Formula;
using Org.BouncyCastle.Asn1.Ocsp;
using Senparc.Weixin;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.TenPayLibV3;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.EnterpriseServices;
using System.IO;
//using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using static Cargo.Order.orderApi;
using static iText.IO.Image.Jpeg2000ImageData;
using static System.Net.WebRequestMethods;

namespace Cargo.Interface
{
    /// <summary>
    /// MiniProSer 的摘要说明
    /// </summary>
    public class MiniProSer : IHttpHandler
    {
        //缓存
        protected MemcachedClient mc = new MemcachedClient();
        //慧采云仓小程序配置信息
        private static string appid = Common.GetHCYCAppID();
        private static string appsecret = Common.GetHCYCAppSecret();
        private static string _key = Common.GetHCYCWxPayKey();
        public static string _mch_id = Common.GetHCYCMachID();

#if DEBUG
        public bool IsDebug = true;
#else
        public bool IsDebug = false;
#endif

        private static string Acctoken = string.Empty;

        //public void OrderPushTime_Elapsed()
        //private void OrderPushTime_Elapsed(HttpContext context)
        //{
        //    //机器人账号
        //    LogEntity log = new LogEntity();
        //    log.IPAddress = "";
        //    log.Moudle = "服务管理";
        //    log.Status = "0";
        //    log.NvgPage = "定单推送服务";
        //    //log.UserID = RobotID;
        //    log.Operate = "A";
        //    CargoOrderBus order = new CargoOrderBus();

        //    #region 推送订单到好来运系统和新陆程系统作业
        //    List<CargoOrderPushEntity> result = order.QueryCargoOrderPushInfoList(new CargoOrderPushEntity { PushStatus = "0" });
        //    if (result.Count > 0)
        //    {
        //        foreach (var it in result)
        //        {
        //            if (it.HouseName.Equals("广州仓库")&& it.Type != 2)
        //            {
        //                order.UpdateCargoOrderPushAwbNo(new CargoOrderPushEntity { OrderNo = it.OrderNo, PushStatus = "1" }, log);
        //                continue;
        //            }
        //            try
        //            {
        //                #region 推送信息
        //                if (it.PushType.Equals("0"))
        //                {
        //                    //LogHelper.WriteLog("Begin 上传新增订单号：" + it.OrderNo);
        //                    string DeliveryDepartment = string.Empty;
        //                    //if (it.HouseName.Equals("西安仓库"))
        //                    //{
        //                    //    DeliveryDepartment = "好来运安泰路斯轮胎西安仓业务部";
        //                    //}
        //                    if (it.HouseID.Equals("65"))
        //                    {
        //                        DeliveryDepartment = it.HLYSendUnit;
        //                    }
        //                    string OPID = it.OP_ID;
        //                    if (it.OP_ID.Length > 8)
        //                    {
        //                        OPID = it.OP_ID.Substring(0, 6);
        //                    }

        //                    //新增
        //                    string url = "http://apphome.hlyex.com:9891/api.ashx?action=SaveCargoToAwbno&token=jgds$jh";
        //                    url += "&OrderNO=" + it.OrderNo + "&Fdep=" + it.Dep + "&Fdest=" + it.Dest + "&Transport=汽运&Department=" + it.HouseName + "&Toname=" + it.AcceptPeople + "&Totel=" + it.AcceptTelephone + "&Tomobile=" + it.AcceptCellphone + "&Toaddress=" + it.AcceptAddress + "&Guestid=" + it.ClientNum + "&Guestname=" + it.AcceptUnit + "&User=" + OPID + "&CarrierCompany=&Pcs=" + it.Piece.ToString() + "&Weight=0&Volume=0&grossfee=" + it.TransportFee.ToString("F2") + "&DeliveryDepartment=" + DeliveryDepartment + "&HouseID=" + it.HouseID + "&HouseName=" + it.HouseName;
        //                    //LogHelper.WriteLog("URL：" + url);
        //                    string awbno = wxHttpUtility.GetData(url);
        //                    //LogHelper.WriteLog("订单号：" + it.OrderNo + "返回运单号：" + awbno);
        //                    if (!string.IsNullOrEmpty(awbno) && it.Type != 2)
        //                    {
        //                        ArrayList GridRows = (ArrayList)JSON.Decode("[" + awbno + "]");
        //                        foreach (Hashtable row in GridRows)
        //                        {
        //                            if (Convert.ToString(row["state"]).Equals("true"))
        //                            {
        //                                order.UpdateCargoOrderPushAwbNo(new CargoOrderPushEntity { OrderNo = it.OrderNo, AwbNo = Convert.ToString(row["awbno"]), PushStatus = "1" }, log);
        //                                if (it.LogisID.Equals(34))
        //                                {
        //                                    order.UpdateLogisAwbNo(it.OrderNo, Convert.ToString(row["awbno"]), "", log);
        //                                }
        //                            }
        //                        }

        //                    }
        //                    if (!string.IsNullOrEmpty(awbno) && it.Type == 2)
        //                    {
        //                        ArrayList GridRows = (ArrayList)JSON.Decode("[" + awbno + "]");
        //                        foreach (Hashtable row in GridRows)
        //                        {
        //                            if (Convert.ToString(row["state"]).Equals("true"))
        //                            {
        //                                order.UpdateCargoOrderPushAwbNo(new CargoOrderPushEntity { OrderNo = it.OrderNo, AwbNo = Convert.ToString(row["awbno"]), PushStatus = "1" }, log);
        //                                order.UpdateMoveOrderLogis(new CargoMoveOrderEntity() { MoveNo = it.OrderNo, LogisAwbNo = Convert.ToString(row["awbno"]) }, log);
        //                            }
        //                        }

        //                    }
        //                    //if (it.HouseName.Equals("西安仓库"))
        //                    //{
        //                    //    //1.保存订单数据到好来运Tbl_ProductOrder表
        //                    //    List<CargoContainerShowEntity> orderContainerList = order.QueryOrderByOrderNo(new CargoOrderEntity { OrderNo = it.OrderNo });
        //                    //    CargoOrderEntity cargoOrderEntity = order.QueryOrderInfo(new CargoOrderEntity { OrderNo = it.OrderNo });
        //                    //    order.SaveHlyOrderData(orderContainerList, cargoOrderEntity);
        //                    //}
        //                }
        //                else if (it.PushType.Equals("1"))
        //                {
        //                    //删除
        //                    string url = "http://apphome.hlyex.com:9891/api.ashx?action=DelCargoToAwbno&token=jgds$jh";
        //                    url += "&OrderNO=" + it.OrderNo + "&Awbno=" + it.AwbNo;
        //                    string awbno = wxHttpUtility.GetData(url);

        //                    string DeliveryDepartment = string.Empty;
        //                    if (it.HouseName.Equals("西安仓库"))
        //                    {
        //                        DeliveryDepartment = "好来运安泰路斯轮胎西安仓业务部";
        //                    }
        //                    if (it.HouseID.Equals("65"))
        //                    {
        //                        DeliveryDepartment = it.HLYSendUnit;
        //                    }
        //                    string OPID = it.OP_ID;
        //                    if (it.OP_ID.Length > 8)
        //                    {
        //                        OPID = it.OP_ID.Substring(0, 6);
        //                    }
        //                    //新增
        //                    string Addurl = "http://apphome.hlyex.com:9891/api.ashx?action=SaveCargoToAwbno&token=jgds$jh";
        //                    Addurl += "&OrderNO=" + it.OrderNo + "&Fdep=" + it.Dep + "&Fdest=" + it.Dest + "&Transport=汽运&Department=" + it.HouseName + "&Toname=" + it.AcceptPeople + "&Totel=" + it.AcceptTelephone + "&Tomobile=" + it.AcceptCellphone + "&Toaddress=" + it.AcceptAddress + "&Guestid=" + it.ClientNum + "&Guestname=" + it.AcceptUnit + "&User=" + OPID + "&CarrierCompany=&Pcs=" + it.Piece.ToString() + "&Weight=0&Volume=0&grossfee=" + it.TransportFee.ToString("F2") + "&DeliveryDepartment=" + DeliveryDepartment + "&HouseID=" + it.HouseID + "&HouseName=" + it.HouseName;
        //                    string awbawbno = wxHttpUtility.GetData(Addurl);
        //                    //LogHelper.WriteLog("修改订单号：" + it.OrderNo + "返回运单号：" + awbawbno);

        //                    //LogHelper.WriteLog("Begin 上传修改订单号：" + it.OrderNo);
        //                    ////修改
        //                    //string url = "http://apphome.hlyex.com:9891/api.ashx?action=UpdateCargoToAwbno&token=jgds$jh";
        //                    //url += "&OrderNO=" + it.OrderNo + "&Awbno=" + it.AwbNo + "&Pcs=" + it.Piece.ToString() + "&Weight=0&Volume=0";
        //                    //string awbno = wxHttpUtility.GetData(url);
        //                    order.UpdateCargoOrderPushAwbNo(new CargoOrderPushEntity { OrderNo = it.OrderNo, PushStatus = "1" }, log);
        //                }
        //                else if (it.PushType.Equals("2"))
        //                {
        //                    //LogHelper.WriteLog("Begin 上传删除订单号：" + it.OrderNo);
        //                    //删除
        //                    string url = "http://apphome.hlyex.com:9891/api.ashx?action=DelCargoToAwbno&token=jgds$jh";
        //                    url += "&OrderNO=" + it.OrderNo + "&Awbno=" + it.AwbNo;
        //                    string awbno = wxHttpUtility.GetData(url);
        //                    order.UpdateCargoOrderPushAwbNo(new CargoOrderPushEntity { OrderNo = it.OrderNo, PushStatus = "1" }, log);
        //                }
        //                #endregion

        //            }
        //            catch (ApplicationException ex)
        //            {
        //                //LogHelper.WriteLog("订单上传出错" + it.OrderNo);
        //                //PushWeixinNotice("7");
        //                continue;
        //            }
        //        }
        //    }
        //    #endregion


        //}

        #region 小程序的验证
        /// <summary>
        /// 反序列化包含openid和sessionkey的json数据包
        /// </summary>
        /// <param name="code">json数据包</param>
        /// <returns>包含openid和sessionkey的类</returns>
        public openidandsessionkey decodeopenidandsessionkey(wechatlogininfo logininfo)
        {

            openidandsessionkey oiask = JsonConvert.DeserializeObject<openidandsessionkey>(getopenidandsessionkeystring(logininfo.code));
            if (!string.IsNullOrEmpty(oiask.errcode))
                return null;
            return oiask;
        }
        /// <summary>
        /// 获取openid和sessionkey的json数据包
        /// </summary>
        /// <param name="code">客户端发来的code</param>
        /// <returns>json数据包</returns>
        private string getopenidandsessionkeystring(string code)
        {
            string temp = "https://api.weixin.qq.com/sns/jscode2session?" +
              "appid=" + appid
              + "&secret=" + appsecret
              + "&js_code=" + code
              + "&grant_type=authorization_code";
            string resu = wxHttpUtility.GetData(temp);
            return resu;
        }
        private void GetWxProcessToken()
        {
            //AccessTokenResult accessTokenResult = CommonApi.GetToken(appid, appsecret);
            //Acctoken = accessTokenResult.access_token;
            try
            {
                Acctoken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appid, appsecret);
                Senparc.Weixin.MP.Entities.GetCallBackIpResult bip = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetCallBackIp(Acctoken);
                if (bip.errcode.Equals("40001"))
                {
                    //WriteTextLog(bip.errcode.ToString());
                    Senparc.Weixin.MP.Containers.AccessTokenContainer.Register(appid, appsecret, "DLQFYPMallToken");
                    Acctoken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appid, appsecret);
                }
            }
            catch (Exception ex)
            {
                //WriteTextLog("Token:" + ex.Message);
                Senparc.Weixin.MP.Containers.AccessTokenContainer.Register(appid, appsecret, "DLQFYPMallToken");
                Acctoken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appid, appsecret);
            }
        }
        #endregion

        #region 判断登陆注册
        /// <summary>
        /// 检查Token是否有效
        /// code 为 1 token失效，重新获取，为 0 正常成功
        /// </summary>
        /// <param name="context"></param>
        private void CheckToken(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();
            weiChat.code = 0;

            string DToken = context.Request["token"];
            if (string.IsNullOrEmpty(DToken)) { weiChat.code = 1; weiChat.msg = "请求Token为空"; }
            if (mc.KeyExists(DToken))
            {
                weiChat.code = 0;
            }
            String json = JSON.Encode(weiChat);
            context.Response.Write(json);
        }
        /// <summary>
        /// 获取用户的微信OpenID
        /// </summary>
        /// <param name="context"></param>
        private void GetWxOpenID(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();
            weiChat.code = 0;
            string code = context.Request["code"];
            openidandsessionkey oiask = decodeopenidandsessionkey(new wechatlogininfo { code = code });
            if (oiask == null)
            {
                weiChat.code = 1;
                weiChat.msg = "未获取到OPENID";
                goto ERROR;
            }
            if (string.IsNullOrEmpty(oiask.openid))
            {
                weiChat.code = 1;
                weiChat.msg = "未获取到OPENID";
                goto ERROR;
            }
            weiChat.code = 0;
            weiChat.msg = oiask.openid;
        ERROR:
            String json = JSON.Encode(weiChat);
            context.Response.Write(json);
        }

        /// <summary>
        /// 判断是否登陆成功 小程序前端传code进来，获取到用户的openid
        /// </summary>
        /// <param name="context"></param>
        private void wxLogin(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();
            WeiChartData weiChartData = new WeiChartData();
            weiChat.code = 1;
            weiChat.msg = "错误";
            string IsInitLogin = context.Request["IsInitLogin"];
            string code = context.Request["code"];
            openidandsessionkey oiask = decodeopenidandsessionkey(new wechatlogininfo { code = code });
            //openidandsessionkey oiask = new openidandsessionkey();
            //oiask.openid = "oUu1465s96PL2THhzOKrqK6824Mg";
            CargoHouseBus houseBus = new CargoHouseBus();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXUserEntity wxUser = bus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = oiask.openid });
            weiChartData.openid = oiask.openid;
            
            if (wxUser.ID.Equals(0))
            {
                //第一次进入小程序，需要注册 自动添加一条用户数据，显示主界面，前往登记门店
                weiChat.code = 1000;
                weiChat.msg = "前往登记门店";

                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "慧采云仓";
                log.Status = "0";
                log.NvgPage = "新增小程序用户";
                log.UserID = oiask.openid;
                log.Operate = "A";
                //wxUser.wxName = nickname;
                //wxUser.Sex = sex;
                //wxUser.Province = province;
                //wxUser.City = city;
                wxUser.IsCertific = "0";
                //wxUser.AvatarBig = headimgurl;
                //wxUser.AvatarSmall = headimgurl;
                wxUser.ConsumerPoint = 10;
                wxUser.UnionID = "";
                wxUser.DevOpenID = "";
                //wxUser.Country = country;
                wxUser.wxOpenID = oiask.openid;
                bus.AddWeixinUser(wxUser, log);
            }
            else if (wxUser.HouseID.Equals(0) && string.IsNullOrEmpty(wxUser.BusLicenseImg))
            {
                //不是第一次进入，没有绑定 且手机号为空，表示没有登记注册
                weiChat.code = 1001;
                weiChat.msg = "前往登记门店";
            }
            else if (wxUser.HouseID.Equals(0) && !string.IsNullOrEmpty(wxUser.BusLicenseImg))
            {
                //没有绑定 且手机号不为空，表示登记注册正在审核中
                weiChat.code = 1002;
                weiChat.msg = "正在审核中";
                Common.WriteTextLog("微信OPENID：" + wxUser.wxName + " " + wxUser.wxOpenID);
            }
            else
            {
                //登记审核后的正常登陆
                string dtoken = string.Empty;
                //实体转化为Json字符串
                string userJson = JSON.Encode(wxUser);
                //MD5加密
                using (MD5 md5Hash = MD5.Create())
                {
                    dtoken = Common.GetMd5Hash(md5Hash, userJson);
                }
                weiChat.code = 0;
                weiChat.msg = "success";
                weiChartData.openid = oiask.openid;
                weiChartData.token = dtoken;
                weiChartData.uid = wxUser.ClientNum;//绑定的客户编码
                weiChartData.OperaBrand = wxUser.OperaBrand;
                weiChartData.address = wxUser.Address;//收货地址
                weiChartData.mobile = wxUser.AcceptCellphone;//收货联系电话
                weiChartData.name = wxUser.AcceptName;//收货联系人
                weiChartData.LogisFee = wxUser.LogisFee;
                weiChartData.TwoLogisFee = wxUser.TwoLogisFee;
                weiChartData.ThreeLogisFee = wxUser.ThreeLogisFee;
                weiChartData.NextDayLogisFee = wxUser.NextDayLogisFee;
                weiChartData.CompanyName = wxUser.CompanyName;
                weiChartData.LimitMoney = wxUser.LimitMoney;
                weiChartData.QuotaLimit = wxUser.QuotaLimit;
                weiChartData.ArrivePayLimit = wxUser.ArrivePayLimit;
                weiChartData.HouseName = wxUser.HouseName;
                weiChartData.HouseID = wxUser.HouseID;
                weiChartData.HouseCellphone = wxUser.HouseCellphone.Replace(" ", "");
                weiChartData.EndBusHours = wxUser.EndBusHours;
                weiChartData.IsCanRush = wxUser.IsCanRush;
                weiChartData.IsCanPickUp = wxUser.IsCanPickUp;
                weiChartData.IsCanNextDay = wxUser.IsCanNextDay;
                weiChartData.MulAddressOrder = wxUser.MulAddressOrder;
                weiChartData.PaymentMethod = wxUser.PaymentMethod;
                // weiChartData.NextDayLogisFee = 15;
                weiChartData.Discount = wxUser.Discount.Equals(0) ? 1 : wxUser.Discount;
                weiChartData.IsShowExpedit = wxUser.IsShowExpedit;
                //weiChartData.IsShowNextDay = "0";
                weiChartData.IsShowNextDay = wxUser.IsShowNextDay;
                weiChartData.ClientType = wxUser.ClientType;
                weiChartData.IsBuy = wxUser.IsBuy;
                weiChartData.NoType = wxUser.NoType;
                weiChartData.IsManager = wxUser.IsManager;
                weiChartData.PreReceiveMoney = wxUser.IsManager.Equals("1") ? wxUser.PreReceiveMoney : 0;
                double distance = 0;
                //仓库经纬度和客户门店地址经纬度判断比较距离
                //if (!string.IsNullOrEmpty(wxUser.HouseLat) && !string.IsNullOrEmpty(wxUser.HouseLng) && !string.IsNullOrEmpty(wxUser.Longitude) && !string.IsNullOrEmpty(wxUser.Latitude))
                //{
                //    distance = Common.CalculateDistance(Convert.ToDouble(wxUser.HouseLat), Convert.ToDouble(wxUser.HouseLng), Convert.ToDouble(wxUser.Latitude), Convert.ToDouble(wxUser.Longitude));
                //}
                //if (distance > 15)
                //{
                //    weiChartData.IsShowExpedit = "0";
                //}


                mc.Add(dtoken, wxUser);

                //收集登陆行为数据
                //if (!string.IsNullOrEmpty(IsInitLogin))
                //{
                //    if (IsInitLogin.Equals("0"))
                //    {
                //        //如果关键字搜索不为空，则表示是关键字搜索，则将搜索记录插入到关键字搜索表中
                //        houseBus.InsertCargoBehavior(new CargoBehaviorEntity
                //        {
                //            HouseID = wxUser.HouseID,
                //            ClientNum = wxUser.ClientNum,
                //            wxOpenID = wxUser.wxOpenID,
                //            BehaType = "0",
                //            OPDATE = DateTime.Now,
                //            KeyWord = "",
                //            TypeName = ""
                //        });

                //    }
                //}

            }
            weiChat.data = weiChartData;
            String json = JSON.Encode(weiChat);
            context.Response.Write(json);
        }
        /// <summary>
        /// 查询首页广告
        /// </summary>
        /// <param name="context"></param>
        private void QueryAdBanner(HttpContext context)
        {
            BannerListEntity result = new BannerListEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            List<BannerInfo> banners = new List<BannerInfo>();
            CargoHouseBus houseBus = new CargoHouseBus();
            List<CargoBannerEntity> bannerEntities = houseBus.QueryHouseBannerList(new CargoBannerEntity { DelFlag = "0", HouseID = wxUser.HouseID });
            if (bannerEntities.Count > 0)
            {
                foreach (var banner in bannerEntities)
                {
                    banners.Add(new BannerInfo { id = banner.BID, picUrl = banner.PicName, title = banner.Title });
                }
            }
            else
            {
                banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg21.jpg", title = "" });
            }
            //if (wxUser.HouseID.Equals(93))
            //{
            //    //东平云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg01.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg02.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg03.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(95))
            //{
            //    //深圳沙井云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg04.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg05.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg06.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(96))
            //{
            //    //星沙云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg07.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg08.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg09.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(97))
            //{
            //    //增城云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg10.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg11.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg12.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(98))
            //{
            //    //西安云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg13.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg14.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg15.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(99))
            //{
            //    //汉口云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg16.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg17.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg18.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(100))
            //{
            //    //顺捷云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg19.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg20.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg21.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(101))
            //{
            //    //汕头云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg22.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg23.jpg", title = "" });
            //    banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg24.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(102))
            //{
            //    //渭南云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg25.jpg", title = "" });
            //    //banners.Add(new BannerInfo { id = 2, picUrl = "https://dlt.neway5.com/CSS/image/miniBg26.jpg", title = "" });
            //    //banners.Add(new BannerInfo { id = 3, picUrl = "https://dlt.neway5.com/CSS/image/miniBg24.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(105))
            //{
            //    //北辰云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg26.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(106))
            //{
            //    //南沙云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg27.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(107))
            //{
            //    //从化云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg28.jpg", title = "" });
            //}
            //else if (wxUser.HouseID.Equals(108))
            //{
            //    //南海云仓
            //    banners.Add(new BannerInfo { id = 1, picUrl = "https://dlt.neway5.com/CSS/image/miniBg28.jpg", title = "" });
            //}
            result.data = banners;
        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }

        #endregion

        #region 注册申请 
        /// <summary>
        /// 提交注册申请
        /// </summary>
        private void saveRegeist(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();
            weiChat.code = 0;
            weiChat.msg = "成功";
            string wxOpenID = Convert.ToString(context.Request["wxOpenID"]);
            //string DToken = Convert.ToString(context.Request["token"]);
            ////WriteTextLog("绑定" + DToken);
            //if (string.IsNullOrEmpty(DToken))
            //{
            //    weiChat.code = 1;
            //    weiChat.msg = "请求Token为空";
            //    goto ERROR;
            //}
            //WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            //if (wxUser.ID.Equals(0))
            //{
            //    weiChat.code = 1;
            //    weiChat.msg = "请求Token有误";
            //    goto ERROR;
            //}
            if (string.IsNullOrEmpty(wxOpenID))
            {
                weiChat.code = 2;
                weiChat.msg = "wxOpenID为空";
                goto ERROR;
            }
            String json = context.Request["data"];

            if (String.IsNullOrEmpty(json))
            {
                weiChat.code = 2;
                weiChat.msg = "参数有误";
                goto ERROR;
            }

            ArrayList rows = (ArrayList)JSON.Decode(json);
            WXUserEntity ent = new WXUserEntity();
            CargoWeiXinBus bus = new CargoWeiXinBus();
            ent.wxOpenID = wxOpenID;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.NvgPage = "用户注册";
            log.Status = "0";
            log.Operate = "U";
            try
            {
                foreach (Hashtable row in rows)
                {
                    ent.wxName = Convert.ToString(row["WxName"]);//微信名
                    ent.AvatarBig = Convert.ToString(row["Avatar"]);//微信头像
                    ent.AvatarSmall = Convert.ToString(row["Avatar"]);//微信头像
                    ent.Cellphone = Convert.ToString(row["Cellphone"]);//手机号
                    ent.CompanyName = Convert.ToString(row["CompanyName"]).Replace("/", "-");//门店名
                    ent.Name = Convert.ToString(row["Name"]).Replace("/", "-");//联系人姓名
                    ent.Address = Convert.ToString(row["Address"]).Replace("/", "-");//门店地址
                    ent.StorePhone = Convert.ToString(row["StorePhone"]);//门店电话
                    ent.BusLicenseImg = Convert.ToString(row["BusLicenseImg"]);//营业执照照片或门头门面图片
                    ent.IDCardImg = Convert.ToString(row["IDCardImg"]);
                    ent.IDCardBackImg = Convert.ToString(row["IDCardBackImg"]);
                    ent.UserType = "0";
                    ent.Province = Convert.ToString(row["Province"]);//省
                    ent.City = Convert.ToString(row["City"]);//市
                    ent.Longitude = Convert.ToString(row["Longitude"]);
                    ent.Latitude = Convert.ToString(row["Latitude"]);
                    ent.AcceptName = ent.Name;
                    ent.AcceptCellphone = ent.Cellphone;
                    //string start = ent.Address;//省市区
                    //if (!string.IsNullOrEmpty(start))
                    //{
                    //    string[] add = start.Split(' ');
                    //    if (add.Length > 2)
                    //    {
                    //        ent.Province = add[0];
                    //        ent.City = add[1];
                    //    }
                    //    //if (add.Length == 1)
                    //    //{
                    //    //    ent.Province = add[0];
                    //    //}
                    //    //if (add.Length == 2)
                    //    //{
                    //    //    ent.Province = add[0];
                    //    //    ent.City = add[1];
                    //    //}
                    //    //if (add.Length == 3)
                    //    //{
                    //    //    ent.Province = add[0];
                    //    //    ent.City = add[1];
                    //    //    ent.Country = add[2];
                    //    //}
                    //}
                }
                //ent.ID = wxUser.ID;
                log.UserID = wxOpenID;
                ent.ConsumerPoint = 10;
                bus.UpdateWxUserRegeist(ent, log);
                //bus.AddAppCellphoneUser(ent, log);

            }
            catch (ApplicationException ex)
            {
                weiChat.code = 1003;
                weiChat.msg = "系统故障";
            }
        ERROR:
            //JSON
            String result = JSON.Encode(weiChat);
            context.Response.Write(result);
        }
        /// <summary>
        /// 保存门店门头照片
        /// </summary>
        private void SaveBusLicense(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();

            string SavePath = "../Weixin/UploadFile/";
            string modifyFileName = string.Empty;
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                HttpPostedFile file = context.Request.Files[i];
                modifyFileName = "WX" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string path = System.Web.HttpContext.Current.Server.MapPath(SavePath + modifyFileName + ".jpg");
                file.SaveAs(path);//存到upimg目录中，同文件名
            }

            //string img = context.Request["imgbase64"];
            //Common.WriteTextLog(img);
            //string[] img_array = img.Split(',');
            //byte[] bytes = Convert.FromBase64String(img_array[1]);
            //string modifyFileName = "WX" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            ////string path = Server.MapPath(SavePath + modifyFileName + ".jpg");
            //string path = System.Web.HttpContext.Current.Server.MapPath(SavePath + modifyFileName + ".jpg");
            //Common.WriteTextLog("200：");
            //using (MemoryStream memoryStream = new MemoryStream(bytes, 0, bytes.Length))
            //{
            //    //  转成图片
            //    System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);
            //    image.Save(path);  // 将图片存到本地
            //    image.Dispose();
            //}
            weiChat.code = 0;
            weiChat.msg = Common.GetSystemDomain() + "Weixin/UploadFile/" + modifyFileName + ".jpg";
            //weiChat.msg = "OK";
            //JSON
            String json = JSON.Encode(weiChat);
            context.Response.Write(json);
        }
        /// <summary>
        /// 手机号授权获取用户手机号绑定
        /// </summary>
        /// <param name="context"></param>
        private void ModifyUserCellphone(HttpContext context)
        {
            AppletResultData result = new AppletResultData();
            result.code = 0;
            result.msg = "成功";
            WeiChartData weiChartData = new WeiChartData();

            CargoWeiXinBus bus = new CargoWeiXinBus();
            string wxopenid = Convert.ToString(context.Request["wxopenid"]);
            if (string.IsNullOrEmpty(wxopenid))
            {
                result.code = 1;
                result.msg = "OPENID不能为空";
                goto ERR;
            }
            string code = Convert.ToString(context.Request["code"]);
            //if (string.IsNullOrEmpty(Acctoken))
            //{
            GetWxProcessToken();
            //}
            string postData = "{\"code\":\"" + code + "\"}";
            string userCellphone = wxHttpUtility.SendHttpRequest("https://api.weixin.qq.com/wxa/business/getuserphonenumber?access_token=" + Acctoken, postData);
            string mobile = string.Empty;
            ArrayList rows = (ArrayList)JSON.Decode("[" + userCellphone + "]");
            foreach (Hashtable row in rows)
            {
                if (Convert.ToString(row["errcode"]).Equals("0"))
                {
                    //获取到手机号
                    Hashtable crow = (Hashtable)row["phone_info"];
                    mobile = Convert.ToString(crow["phoneNumber"]);
                }
                else
                {
                    result.code = 1;
                    result.msg = "获取失败";
                    goto ERR;
                }
            }
            if (result.code.Equals(0))
            {
                //WXUserEntity wxUser = bus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = wxopenid });
                List<WXUserEntity> wXUsers = new List<WXUserEntity>();
                wXUsers.Add(new WXUserEntity
                {
                    wxOpenID = wxopenid,
                    Cellphone = mobile
                });
                bus.UpdateWeixinUser(wXUsers);
            }
            weiChartData.openid = wxopenid;
            weiChartData.mobile = mobile;
            result.data = weiChartData;
        ERR:
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 通过扫描二维码添加该公司(ClientNum)下的用户
        /// </summary>
        /// <param name="context"></param>
        private void AddScanWxUser(HttpContext context)
        {
            AppletResultData result = new AppletResultData();
            result.code = 0;
            result.msg = "成功";
            WeiChartData weiChartData = new WeiChartData();
            string wxID = Convert.ToString(context.Request["wxID"]);//二维码里的内容，生成二维码人的微信ID
            //Common.WriteTextLog("慧采云仓扫描注册：" + wxID);
            if (string.IsNullOrEmpty(wxID))
            {
                result.code = 1; result.msg = "参数不能为空"; goto ERROR;
            }
            string Name = Convert.ToString(context.Request["Name"]);//用户姓名
            string Cellphone = Convert.ToString(context.Request["Cellphone"]);//用户姓名
            string openid = Convert.ToString(context.Request["OpenID"]);//用户姓名
            //Common.WriteTextLog("慧采云仓扫描注册：" + Name);
            //Common.WriteTextLog("慧采云仓扫描注册：" + Cellphone);
            //Common.WriteTextLog("慧采云仓扫描注册：" + openid);
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXUserEntity wxUser = bus.QueryWeixinUserByID(new WXUserEntity { ID = Convert.ToInt64(wxID) });
            if (wxUser == null)
            {
                result.code = 1; result.msg = "二维码数据有误"; goto ERROR;
            }
            if (wxUser.ClientNum.Equals(0))
            {
                result.code = 1; result.msg = "公司未审核"; goto ERROR;
            }
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.NvgPage = "扫描注册";
            log.Status = "0";
            log.Operate = "U";
            log.UserID = openid;
            try
            {
                if (bus.IsExistWeixinUser(new WXUserEntity { wxOpenID = openid }))
                {
                    //bus.UpdateWxUserRegeist(new WXUserEntity { wxOpenID = openid, Name = Name, CompanyName = wxUser.CompanyName, Cellphone = Cellphone, Province = wxUser.Province, City = wxUser.City, Country = wxUser.Country, Address = wxUser.Address, BusLicenseImg = wxUser.BusLicenseImg, IDCardImg = wxUser.IDCardImg, IDCardBackImg = wxUser.IDCardBackImg, ClientNum = wxUser.ClientNum }, log);
                    bus.UpdateWeixinUser(new List<WXUserEntity> {
                        new WXUserEntity {
                            wxOpenID=openid,
                            Name= Name,
                            CompanyName=wxUser.CompanyName,
                            Cellphone=Cellphone,
                            Province=wxUser.Province,
                            City=wxUser.City,
                            Country=wxUser.Country,
                            Address=wxUser.Address,
                            ClientNum=wxUser.ClientNum,
                            Longitude=wxUser.Longitude,
                            Latitude=wxUser.Latitude,
                            Sex=wxUser.Sex,
                        }
                    });
                    //result.code = 1; result.msg = "该用户已存在"; goto ERROR;
                }
                else
                {
                    bus.AddWeixinUser(new WXUserEntity { Name = Name, Cellphone = Cellphone, wxOpenID = openid, Province = wxUser.Province, City = wxUser.City, ClientNum = wxUser.ClientNum, Address = wxUser.Address, CompanyName = wxUser.CompanyName, BusLicenseImg = wxUser.BusLicenseImg, LogisID = wxUser.LogisID, LogicName = wxUser.LogicName, StorePhone = wxUser.StorePhone, IsManager = "0", ConsumerPoint = 10 }, log);

                    //bus.UpdateClientForBindingClientNum(new WXUserEntity { wxOpenID = openid, Name = Name, Cellphone = Cellphone, Province = wxUser.Province, City = wxUser.City, Country = wxUser.Country, ClientNum = wxUser.ClientNum }, log);
                }
            }
            catch (ApplicationException ex)
            {
                result.code = 1; result.msg = "新增出错"; goto ERROR;
            }

        ERROR:
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 获取省市区信息
        /// </summary>
        /// <param name="context"></param>
        private void GetAllProvincesInfo(HttpContext context)
        {
            ProvincesEntity result = new ProvincesEntity();
            result.code = 0;
            result.msg = "成功";
            int parentId = string.IsNullOrEmpty(context.Request["pid"]) ? 0 : Convert.ToInt32(context.Request["pid"]);
            CargoHouseBus bus = new CargoHouseBus();
            List<CargoCityEntity> list = bus.QueryCityData(new CargoCityEntity { ParentID = parentId });
            List<ProvincesInfo> provincesList = new List<ProvincesInfo>();
            foreach (CargoCityEntity item in list)
            {
                provincesList.Add(new ProvincesInfo
                {
                    id = item.ID,
                    level = item.ParentID,
                    name = item.City
                });
            }
            result.data = provincesList;
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 生成不受限制的小程序二维码
        /// </summary>
        /// <param name="context"></param>
        private void GetQrCodeUnLimit(HttpContext context)
        {
            //AppletResultData weiChat = new AppletResultData();
            //weiChat.code = 0;

            string scene = Convert.ToString(context.Request["scene"]);
            string pageUrl = Convert.ToString(context.Request["page"]);
            GetWxProcessToken();
            string postData = "{\"page\":\"" + pageUrl + "\",\"scene\":\"" + scene + "\"}";
            //string postData = "{\"scene\":\"" + scene + "\"}";
            string qrCodeImage = wxHttpUtility.SendPostHttpRequestForBase64Image("https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token=" + Acctoken, "application/json;charset=UTF-8", postData);
            //string qrCodeImages = wxHttpUtility.SendPostHttpRequest("https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token=" + Acctoken, "application/json;charset=UTF-8", postData);
            //Common.WriteTextLog(qrCodeImages);
            //weiChat.msg = qrCodeImage;
            //String json = JSON.Encode(weiChat);
            context.Response.Write(qrCodeImage);
        }

        #endregion

        #region 我的管理
        /// <summary>
        /// 查询我的员工数据信息
        /// </summary>
        /// <param name="context"></param>
        private void QueryWxUserList(HttpContext context)
        {
            WxClientReturnEntity weiChat = new WxClientReturnEntity();
            weiChat.code = 0;
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                weiChat.code = 1;
                weiChat.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                weiChat.code = 1;
                weiChat.msg = "Token有误";
                goto ERROR;
            }
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<WXUserEntity> result = bus.QueryWeixinUserInfo(new WXUserEntity { ClientNum = wxUser.ClientNum });
            List<WxClientUserEntity> wxClients = new List<WxClientUserEntity>();
            foreach (var it in result)
            {
                wxClients.Add(new WxClientUserEntity
                {
                    ID = it.ID,
                    AvatarSmall = it.AvatarSmall,
                    Cellphone = it.Cellphone,
                    CompanyName = it.CompanyName,
                    IsManager = it.IsManager,
                    Name = it.Name,
                    wxName = it.Name,
                    wxOpenID = it.wxOpenID
                });
            }
            weiChat.wxClientUsers = wxClients;
        ERROR:
            //JSON
            String returnString = JSON.Encode(weiChat);
            context.Response.Write(returnString);
        }

        /// <summary>
        /// 修改用户的姓名和手机号码
        /// </summary>
        /// <param name="context"></param>
        private void UpdateWxClientInfo(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                weiChat.code = 1;
                weiChat.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                weiChat.code = 1;
                weiChat.msg = "Token有误";
                goto ERROR;
            }
            String json = context.Request["data"];
            if (String.IsNullOrEmpty(json))
            {
                weiChat.code = 2;
                weiChat.msg = "参数有误";
                goto ERROR;
            }
            ArrayList rows = (ArrayList)JSON.Decode(json);
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.NvgPage = "员工修改";
            log.Status = "0";
            log.Operate = "U";
            log.UserID = wxUser.wxOpenID;
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXUserEntity ent = new WXUserEntity();
            foreach (Hashtable row in rows)
            {
                ent.ID = Convert.ToInt32(row["ID"]);
                ent.Name = Convert.ToString(row["Name"]);
                ent.Cellphone = Convert.ToString(row["Cellphone"]);
            }
            try
            {
                bus.UpdateWxUserNameCellphone(ent, log);
                weiChat.code = 0;
                weiChat.msg = "修改成功";
            }
            catch (ApplicationException ex)
            {
                weiChat.code = 1003;
                weiChat.msg = "系统故障";
            }

        ERROR:
            //JSON
            String result = JSON.Encode(weiChat);
            context.Response.Write(result);
        }

        /// <summary>
        /// 解绑删除用户
        /// </summary>
        /// <param name="context"></param>
        private void saveWxUserUnBind(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                weiChat.code = 1;
                weiChat.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                weiChat.code = 1;
                weiChat.msg = "Token有误";
                goto ERROR;
            }
            String json = context.Request["data"];
            if (String.IsNullOrEmpty(json))
            {
                weiChat.code = 2;
                weiChat.msg = "参数有误";
                goto ERROR;
            }
            ArrayList rows = (ArrayList)JSON.Decode(json);
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.NvgPage = "员工修改";
            log.Status = "0";
            log.Operate = "U";
            log.UserID = wxUser.wxOpenID;
            CargoClientBus bus = new CargoClientBus();
            List<WXUserEntity> entList = new List<WXUserEntity>();
            foreach (Hashtable row in rows)
            {
                entList.Add(new WXUserEntity
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = Convert.ToString(row["Name"]),
                    wxName = Convert.ToString(row["wxName"]),
                    wxOpenID = Convert.ToString(row["wxOpenID"]),
                    IsManager = "0"
                });
            }
            try
            {
                bus.saveWxUserUnBind(entList, log);
                weiChat.code = 0;
                weiChat.msg = "删除成功";
                mc.Delete(DToken);
            }
            catch (ApplicationException ex)
            {
                weiChat.code = 1003;
                weiChat.msg = "系统故障";
            }

        ERROR:
            //JSON
            String result = JSON.Encode(weiChat);
            context.Response.Write(result);
        }
        /// <summary>
        /// 移交管理员权限
        /// </summary>
        /// <param name="context"></param>
        private void TranferManager(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                weiChat.code = 1;
                weiChat.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                weiChat.code = 1;
                weiChat.msg = "Token有误";
                goto ERROR;
            }
            if (wxUser.IsManager.Equals("0"))
            {
                weiChat.code = 1;
                weiChat.msg = "您不是管理员";
                goto ERROR;
            }
            String wxID = context.Request["ID"];
            if (String.IsNullOrEmpty(wxID))
            {
                weiChat.code = 2;
                weiChat.msg = "参数有误";
                goto ERROR;
            }
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.NvgPage = "员工修改";
            log.Status = "0";
            log.Operate = "U";
            log.UserID = wxUser.wxOpenID;
            CargoClientBus bus = new CargoClientBus();

            try
            {
                bus.UpdateWxClientManager(new WXUserEntity { ID = wxUser.ID, IsManager = "0" }, log);
                bus.UpdateWxClientManager(new WXUserEntity { ID = Convert.ToInt32(wxID), IsManager = "1" }, log);
                weiChat.code = 0;
                weiChat.msg = "修改成功";
            }
            catch (ApplicationException ex)
            {
                weiChat.code = 1003;
                weiChat.msg = "系统故障";
            }

        ERROR:
            //JSON
            String result = JSON.Encode(weiChat);
            context.Response.Write(result);
        }

        #endregion

        #region 商品查询
        /// <summary>
        /// 分页查询商品列表
        /// </summary>
        /// <param name="context"></param>
        private void GetProductList(HttpContext context)
        {
            GoodsEntity result = new GoodsEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            //if (wxUser.ID.Equals(0))
            //{
            //    result.code = 1;
            //    result.msg = "Token有误";
            //    goto ERROR;
            //}

            CargoHouseBus bus = new CargoHouseBus();
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            if (!wxUser.StockShareHouseID.Equals(0))
            {
                queryEntity.HouseIDStr = wxUser.HouseID.ToString() + "," + wxUser.StockShareHouseID.ToString();
                queryEntity.HouseIDASC = wxUser.HouseID.ToString();
            }
            else
            {
                //没有共享仓库，就是自己的仓库，只查询自己的仓库库存即可
                queryEntity.HouseID = wxUser.HouseID;
            }
            //queryEntity.HouseID = 93;
            queryEntity.Specs = Convert.ToString(context.Request["Specs"]).ToUpper().Replace("UNDEFIND", "");//查询条件 规格
            queryEntity.TypeID = string.IsNullOrEmpty(context.Request["TypeID"]) ? 0 : Convert.ToInt32(context.Request["TypeID"]);//查询条件 品牌分类ID     
            queryEntity.SpecsType = Convert.ToString(context.Request["SpecsType"]);//查询条件 产品类型 即日达产品 4 次日达产品 5
            queryEntity.NoType = string.IsNullOrEmpty(context.Request["NoType"]) ? "" : Convert.ToString(context.Request["NoType"]);//限制品牌
            if (string.IsNullOrEmpty(context.Request["TypeID"]) || Convert.ToInt32(context.Request["TypeID"]) == 0)
            {
                queryEntity.WXTypeParentID = string.IsNullOrEmpty(context.Request["TypeParentID"]) ? "1" : Convert.ToString(context.Request["TypeParentID"]);//父ID   默认1
            }


            queryEntity.IsLockStock = "0";//非锁定的库存
            //queryEntity.TypeParentID = 1; //查询分类是轮胎的品牌产品库存
            queryEntity.SalePrice = 100;//设置过滤价格低于50元的

            int pageIndex = Convert.ToInt32(context.Request["page"]);//查询条件 分页 第几页
            int pageSize = Convert.ToInt32(context.Request["pageSize"]); //查询条件 分页 每页数量
            List<CargoContainerShowEntity> list = null;
            list = bus.QueryHouseStockDataMiniPro(pageIndex, pageSize, queryEntity);

            //如果关键字搜索不为空，则表示是关键字搜索，则将搜索记录插入到关键字搜索表中
            bus.InsertCargoBehavior(new CargoBehaviorEntity
            {
                HouseID = wxUser.HouseID,
                ClientNum = wxUser.ClientNum,
                wxOpenID = wxUser.wxOpenID,
                SearchResult = list.Count <= 0 ? "0" : "1",
                BehaType = !string.IsNullOrEmpty(queryEntity.Specs) ? "1" : "0",
                OPDATE = DateTime.Now,
                KeyWord = queryEntity.Specs,
                TypeName = queryEntity.TypeID.Equals(0) ? "" : bus.QueryProductTypeName(new CargoProductTypeEntity { TypeID = queryEntity.TypeID })
            });
            if (list.Count <= 0)
            {
                result.code = 1004;
                result.msg = "商品未上架";
                goto ERROR;
            }
            CargoPriceBus priceBus = new CargoPriceBus();
            List<GoodsInfo> lstCategory = new List<GoodsInfo>();
            Random rd = new Random();
            foreach (CargoContainerShowEntity product in list)
            {
                long RuleID = 0;
                decimal CutEntry = 0.00M;
                string batch = product.BatchYear.ToString() + "10";
                CargoOrderEntity entity = new CargoOrderEntity();
                entity.HouseID = product.HouseID;
                entity.TypeID = product.TypeID;
                DataTable dt = priceBus.QueryPriceRuleBankInfo(entity);
                #region 直减优惠
                //规格、花纹、周期匹配
                DataRow[] rows = dt.Select("RuleType=6 and StartBatch <=" + batch + " and EndBatch >=" + batch + " and Specs='' and Figure=''");
                for (int i = 0; i < rows.Count(); i++)
                {
                    RuleID = Convert.ToInt64(rows[i]["ID"]);
                    CutEntry = Convert.ToDecimal(rows[i]["CutEntry"]);
                }
                #endregion

                string Thumbnail = Common.GetSystemDomain() + product.Thumbnail.Replace("../", "");
                //string picUrl = Thumbnail.Replace("../", HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.LocalPath, "/"));
                if (string.IsNullOrEmpty(product.Thumbnail)) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; }
                string PName = string.Empty;
                if (product.TypeParentID.Equals(1))
                {
                    PName = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel + " " + product.BatchYear.ToString() + "年";
                }
                else
                {
                    PName = product.ProductName;
                }
                //if (product.TypeParentID.Equals(598))
                //{
                //    //电池
                //    PName = product.ProductName;
                //}
                //else {
                //    PName = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel + " " + product.BatchYear.ToString() + "年";
                //}

                if (wxUser.ClientType.Equals("5"))
                {
                    //云仓二批客户 急送和次日达均显示次日达价格
                    product.SalePrice = product.NextDayPrice.Equals(0) ? product.SalePrice : product.NextDayPrice;
                }
                //else
                //{
                //    if (queryEntity.SpecsType == "5")
                //    {
                //        product.SalePrice = product.NextDayPrice;
                //    }
                //}
                lstCategory.Add(new GoodsInfo
                {
                    ProductCode = product.ProductCode,//商品唯一编码
                    Supplier = product.Supplier,//商品供应商
                    SpecsType = product.SpecsType,//产品类型 即日达和次日达
                    SuppClientNum = product.SuppClientNum,//商品供应商编码
                    BatchYear = product.BatchYear.ToString(),//商品周期 年
                    RuleID = RuleID,
                    minBuyNumber = 1,//最小购买数量
                    minPrice = product.SalePrice - CutEntry,//取产品的销售价 即供应商的小程序价
                    listPrice = product.SalePrice + rd.Next(5, 20),//划线价
                    originalPrice = product.InHousePrice,//原价 取产品的进仓价 即供应商的进仓价
                    name = PName,//标题
                    numberOrders = 0,//浏览数量
                    clickNum = product.Star + 200,//浏览点击数量
                    numberSells = 0,//售出件数
                    StoreNum = product.Piece,//库存数 数字型
                    stores = product.Piece.ToString(),//库存数 字符型
                    storesStr = product.Piece > 2 ? "库存充足" : "库存紧张",//库存中文显示 库存充足 绿色显示，库存紧张 红色显示
                    notes = PName,//商品详情
                    pic = Thumbnail,//商品图片
                    HouseID = product.HouseID,
                    HouseName = product.HouseName,//仓库名称
                    PickUpAddress = wxUser.HouseAddress,//自提地址也就是仓库地址
                    DeliveryAddress = product.SupplierAddress,//配送地址即供应商的地址
                    IsMyHouseStock = product.HouseID.Equals(wxUser.HouseID) ? 0 : 1,//是否是本仓库存 0是 1否
                    PromotionType = 0,
                    TypeID= product.TypeID,
                });
            }

            result.data = lstCategory;

        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        /// <summary>
        /// 查询商品详情
        /// </summary>
        /// <param name="context"></param>
        private void GetProductDetail(HttpContext context)
        {
            GoodsDetailEntity result = new GoodsDetailEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            GoodsDetail lstCategory = new GoodsDetail();
            CargoPriceBus priceBus = new CargoPriceBus();

            CargoHouseBus bus = new CargoHouseBus();
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            //queryEntity.HouseID = wxUser.HouseID;
            queryEntity.HouseID = string.IsNullOrEmpty(context.Request["HouseID"]) ? wxUser.HouseID : Convert.ToInt32(context.Request["HouseID"]);
            queryEntity.ProductCode = Convert.ToString(context.Request["ProductCode"]);//查询条件 产品编码
            //queryEntity.NoType = Convert.ToString(context.Request["NoType"]);//品牌限制
            queryEntity.BatchYear = string.IsNullOrEmpty(context.Request["BatchYear"]) ? 0 : Convert.ToInt32(context.Request["BatchYear"]);//查询条件 周期年
            queryEntity.SpecsType = Convert.ToString(context.Request["SpecsType"]);//查询条件 产品类型 即日达产品 4 次日达产品 5
            queryEntity.IsLockStock = "0";//非锁定的库存
                                          // queryEntity.TypeParentID = 1; //查询分类是轮胎的品牌产品库
            queryEntity.SuppClientNum = string.IsNullOrEmpty(context.Request["SuppClientNum"]) ? 0 : Convert.ToInt32(context.Request["SuppClientNum"]);//供应商
            int pageIndex = 1;//查询条件 分页 第几页
            int pageSize = 1000; //查询条件 分页 每页数量
            List<CargoContainerShowEntity> list = bus.QueryHouseStockDataMiniPro(pageIndex, pageSize, queryEntity);
            if (list.Count <= 0)
            {
                result.code = 1005;
                result.msg = "商品已下架";
                goto ERROR;
            }
            CargoContainerShowEntity product = list[0];
            long RuleID = 0;
            decimal CutEntry = 0.00M;
            string batch = product.BatchYear.ToString() + "10";
            CargoOrderEntity entity = new CargoOrderEntity();
            entity.HouseID = product.HouseID;
            entity.TypeID = product.TypeID;
            DataTable dt = priceBus.QueryPriceRuleBankInfo(entity);
            #region 直减优惠
            //规格、花纹、周期匹配
            DataRow[] rows = dt.Select("RuleType=6 and StartBatch <=" + batch + " and EndBatch >=" + batch + " and Specs='' and Figure=''");
            for (int i = 0; i < rows.Count(); i++)
            {
                RuleID = Convert.ToInt64(rows[i]["ID"]);
                CutEntry = Convert.ToDecimal(rows[i]["CutEntry"]);
            }
            #endregion

            string Thumbnail = Common.GetSystemDomain() + product.Thumbnail.Replace("../", "");
            //string picUrl = Thumbnail.Replace("../", HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.LocalPath, "/"));
            if (string.IsNullOrEmpty(product.Thumbnail)) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; }

            //string name = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel + " " + product.BatchYear.ToString() + "年";
            string name = string.Empty;
            if (product.TypeParentID.Equals(1))
            {
                name = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel + " " + product.BatchYear.ToString() + "年";
            }
            else
            {
                name = product.ProductName;
            }
            //if (product.TypeParentID.Equals(598))
            //{
            //    //电池
            //    name = product.ProductName;
            //}
            //else
            //{
            //    name = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel + " " + product.BatchYear.ToString() + "年";
            //}
            //if (queryEntity.SpecsType == "5")
            //{
            //    product.SalePrice = product.NextDayPrice;
            //}
            if (wxUser.ClientType.Equals("5"))
            {
                //云仓二批客户 急送和次日达均显示次日达价格
                product.SalePrice = product.NextDayPrice.Equals(0) ? product.SalePrice : product.NextDayPrice;
            }
            GoodsInfo BasicInfo = (new GoodsInfo
            {
                ProductCode = product.ProductCode,//商品唯一编码
                SpecsType = product.SpecsType,//产品类型 即日达和次日达
                Supplier = product.Supplier,//商品供应商
                SuppClientNum = product.SuppClientNum,//商品供应商编码
                BatchYear = product.BatchYear.ToString(),//商品周期 年
                RuleID = RuleID,
                TypeID = product.TypeID,//商品类型ID
                minBuyNumber = 1,//最小购买数量
                minPrice = product.SalePrice - CutEntry,//取产品的销售价 即供应商的小程序价
                listPrice = product.SalePrice,//划线价
                //minPrice = product.SalePrice - CutEntry,//取产品的销售价 即供应商的小程序价
                //listPrice = product.SalePrice,//划线价
                originalPrice = product.InHousePrice,//原价 取产品的成本价 即供应商的进仓价
                name = name,//标题
                numberOrders = 0,//购买人数
                clickNum = product.Star + 200,//浏览点击数量
                numberSells = 0,//售出件数
                StoreNum = product.Piece,//库存数 数字型
                stores = product.Piece.ToString(),//库存数 字符型
                storesStr = product.Piece > 2 ? "库存充足" : "库存紧张",//库存中文显示 库存充足 绿色显示，库存紧张 红色显示
                notes = name,//商品详情
                pic = Thumbnail,//商品图片
                HouseID = product.HouseID,
                HouseName = product.HouseName,//仓库名称
                PickUpAddress = wxUser.HouseAddress,//自提地址也就是仓库地址
                DeliveryAddress = product.SupplierAddress,//配送地址即供应商的地址
                IsMyHouseStock = product.HouseID.Equals(wxUser.HouseID) ? 0 : 1,//是否是本仓库存 0是 1否
                NextDayLogisFee = product.NextDayLogisFee,
                PromotionType = 0,

            });
            lstCategory.basicInfo = BasicInfo;
            result.data = lstCategory;
            //新增访问量
            CargoProductBus productBus = new CargoProductBus();
            productBus.AddProductAccessCount(new CargoProductAccessDetailsEntity()
            {
                SuppClientNum = product.SuppClientNum,
                HouseID = product.HouseID,
                ProductCode = product.ProductCode,
                wxOpenID = wxUser.wxOpenID,
                AccessTime = DateTime.Now
            }, new LogEntity()
            {
                IPAddress = Common.GetUserIP(HttpContext.Current.Request),
                Moudle = "慧采云仓",
                Status = "0",
                NvgPage = "更新商品访问量",
                UserID = product.SuppClientNum.ToString(),
                Operate = "A",
            });
        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        /// <summary>
        /// 特价商品促销数据列表
        /// </summary>
        /// <param name="context"></param>
        private void GetReduceProductList(HttpContext context)
        {
            GoodsEntity result = new GoodsEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            //if (wxUser.ID.Equals(0))
            //{
            //    result.code = 1;
            //    result.msg = "Token有误";
            //    goto ERROR;
            //}

            CargoHouseBus bus = new CargoHouseBus();
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            if (!wxUser.StockShareHouseID.Equals(0))
            {
                queryEntity.HouseIDStr = wxUser.HouseID.ToString() + "," + wxUser.StockShareHouseID.ToString();
                queryEntity.HouseIDASC = wxUser.HouseID.ToString();
            }
            else
            {
                //没有共享仓库，就是自己的仓库，只查询自己的仓库库存即可
                queryEntity.HouseID = wxUser.HouseID;
            }
            //queryEntity.HouseID = 93;
            queryEntity.Specs = Convert.ToString(context.Request["Specs"]).ToUpper().Replace("UNDEFIND", "");//查询条件 规格
            queryEntity.TypeID = string.IsNullOrEmpty(context.Request["TypeID"]) ? 0 : Convert.ToInt32(context.Request["TypeID"]);//查询条件 品牌分类ID     
            queryEntity.SpecsType = Convert.ToString(context.Request["SpecsType"]);//查询条件 产品类型 即日达产品 4 次日达产品 5
            if (string.IsNullOrEmpty(context.Request["TypeID"]) || Convert.ToInt32(context.Request["TypeID"]) == 0)
            {
                queryEntity.WXTypeParentID = string.IsNullOrEmpty(context.Request["TypeParentID"]) ? "1" : Convert.ToString(context.Request["TypeParentID"]);//父ID   默认1
            }


            queryEntity.IsLockStock = "0";//非锁定的库存
            //queryEntity.TypeParentID = 1; //查询分类是轮胎的品牌产品库存
            queryEntity.ProductPrice = 100;//设置过滤价格低于50元的

            int pageIndex = Convert.ToInt32(context.Request["page"]);//查询条件 分页 第几页
            int pageSize = Convert.ToInt32(context.Request["pageSize"]); //查询条件 分页 每页数量
            List<CargoContainerShowEntity> list = bus.QueryReduceSaleMiniPro(pageIndex, pageSize, queryEntity);
            //如果关键字搜索不为空，则表示是关键字搜索，则将搜索记录插入到关键字搜索表中
            bus.InsertCargoBehavior(new CargoBehaviorEntity
            {
                HouseID = wxUser.HouseID,
                ClientNum = wxUser.ClientNum,
                wxOpenID = wxUser.wxOpenID,
                SearchResult = list.Count <= 0 ? "0" : "1",
                BehaType = !string.IsNullOrEmpty(queryEntity.Specs) ? "1" : "0",
                OPDATE = DateTime.Now,
                KeyWord = queryEntity.Specs,
                TypeName = queryEntity.TypeID.Equals(0) ? "" : bus.QueryProductTypeName(new CargoProductTypeEntity { TypeID = queryEntity.TypeID })
            });
            if (list.Count <= 0)
            {
                result.code = 1004;
                result.msg = "商品未上架";
                goto ERROR;
            }
            CargoPriceBus priceBus = new CargoPriceBus();
            List<GoodsInfo> lstCategory = new List<GoodsInfo>();
            foreach (CargoContainerShowEntity product in list)
            {
                long RuleID = 0;
                decimal CutEntry = 0.00M;

                //string batch = product.BatchYear.ToString() + "10";
                //CargoOrderEntity entity = new CargoOrderEntity();
                //entity.HouseID = product.HouseID;
                //entity.TypeID = product.TypeID;
                //DataTable dt = priceBus.QueryPriceRuleBankInfo(entity);
                //#region 直减优惠
                ////规格、花纹、周期匹配
                //DataRow[] rows = dt.Select("RuleType=6 and StartBatch <=" + batch + " and EndBatch >=" + batch + " and Specs='' and Figure=''");
                //for (int i = 0; i < rows.Count(); i++)
                //{
                //    RuleID = Convert.ToInt64(rows[i]["ID"]);
                //    CutEntry = Convert.ToDecimal(rows[i]["CutEntry"]);
                //}
                //#endregion
                string Thumbnail = Common.GetSystemDomain() + product.Thumbnail.Replace("../", "");
                //string picUrl = Thumbnail.Replace("../", HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.LocalPath, "/"));
                if (string.IsNullOrEmpty(product.Thumbnail)) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; }
                string PName = string.Empty;
                if (product.TypeParentID.Equals(1))
                {
                    PName = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel + " " + product.BatchYear.ToString() + "年";
                }
                else
                {
                    PName = product.ProductName;
                }
                Random rd = new Random();

                lstCategory.Add(new GoodsInfo
                {
                    ProductCode = product.ProductCode,//商品唯一编码
                    Supplier = product.Supplier,//商品供应商
                    SpecsType = product.SpecsType,//产品类型 即日达和次日达
                    SuppClientNum = product.SuppClientNum,//商品供应商编码
                    BatchYear = product.BatchYear.ToString(),//商品周期 年
                    RuleID = RuleID,
                    minBuyNumber = 1,//最小购买数量
                    minPrice = product.SalePrice - CutEntry,//取产品的销售价 即供应商的小程序价
                    listPrice = product.SalePrice + rd.Next(5, 20),//划线价
                    originalPrice = product.InHousePrice,//原价 取产品的进仓价 即供应商的进仓价
                    name = PName,//标题
                    numberOrders = 0,//浏览数量
                    clickNum = product.Star + 200,//浏览点击数量
                    numberSells = 0,//售出件数
                    StoreNum = product.Piece,//库存数 数字型
                    stores = product.Piece.ToString(),//库存数 字符型
                    storesStr = product.Piece > 2 ? "库存充足" : "库存紧张",//库存中文显示 库存充足 绿色显示，库存紧张 红色显示
                    notes = PName,//商品详情
                    pic = Thumbnail,//商品图片
                    HouseID = product.HouseID,
                    HouseName = product.HouseName,//仓库名称
                    PickUpAddress = wxUser.HouseAddress,//自提地址也就是仓库地址
                    DeliveryAddress = product.SupplierAddress,//配送地址即供应商的地址
                    IsMyHouseStock = product.HouseID.Equals(wxUser.HouseID) ? 0 : 1,//是否是本仓库存 0是 1否
                    PromotionType = 1,
                    TypeID= product.TypeID,
                });
            }

            result.data = lstCategory;

        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        /// <summary>
        /// 特价商品详情
        /// </summary>
        /// <param name="context"></param>
        private void GetReduceProductDetail(HttpContext context)
        {
            GoodsDetailEntity result = new GoodsDetailEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            GoodsDetail lstCategory = new GoodsDetail();
            CargoPriceBus priceBus = new CargoPriceBus();

            CargoHouseBus bus = new CargoHouseBus();
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            //queryEntity.HouseID = wxUser.HouseID;
            queryEntity.HouseID = string.IsNullOrEmpty(context.Request["HouseID"]) ? wxUser.HouseID : Convert.ToInt32(context.Request["HouseID"]);
            queryEntity.ProductCode = Convert.ToString(context.Request["ProductCode"]);//查询条件 产品编码
            queryEntity.BatchYear = string.IsNullOrEmpty(context.Request["BatchYear"]) ? 0 : Convert.ToInt32(context.Request["BatchYear"]);//查询条件 周期年
            queryEntity.IsLockStock = "0";//非锁定的库存
                                          // queryEntity.TypeParentID = 1; //查询分类是轮胎的品牌产品库
            queryEntity.SuppClientNum = string.IsNullOrEmpty(context.Request["SuppClientNum"]) ? 0 : Convert.ToInt32(context.Request["SuppClientNum"]);//供应商
            int pageIndex = 1;//查询条件 分页 第几页
            int pageSize = 1000; //查询条件 分页 每页数量
            List<CargoContainerShowEntity> list = bus.QueryReduceSaleMiniPro(pageIndex, pageSize, queryEntity);
            if (list.Count <= 0)
            {
                result.code = 1005;
                result.msg = "商品已下架";
                goto ERROR;
            }
            CargoContainerShowEntity product = list[0];
            long RuleID = 0;
            decimal CutEntry = 0.00M;
            string Thumbnail = Common.GetSystemDomain() + product.Thumbnail.Replace("../", "");

            if (string.IsNullOrEmpty(product.Thumbnail)) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; }

            string name = string.Empty;
            if (product.TypeParentID.Equals(1))
            {
                name = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel + " " + product.BatchYear.ToString() + "年";
            }
            else
            {
                name = product.ProductName;
            }
            GoodsInfo BasicInfo = (new GoodsInfo
            {
                ProductCode = product.ProductCode,//商品唯一编码
                SpecsType = product.SpecsType,//产品类型 即日达和次日达
                Supplier = product.Supplier,//商品供应商
                SuppClientNum = product.SuppClientNum,//商品供应商编码
                BatchYear = product.BatchYear.ToString(),//商品周期 年
                RuleID = RuleID,
                TypeID = product.TypeID,//商品类型ID
                minBuyNumber = 1,//最小购买数量
                //minPrice = product.SpecsType.Equals("4") ? Math.Round(product.SalePrice + Common.GetHCYCTodayOrderTransitFee() + (product.SalePrice * 0.009M), MidpointRounding.AwayFromZero) : Math.Round(product.SalePrice + (product.SalePrice * 0.009M), MidpointRounding.AwayFromZero),//现价销售价价
                //originalPrice = product.SalePrice,//原价
                minPrice = product.SalePrice - CutEntry,//取产品的销售价 即供应商的小程序价
                listPrice = product.SalePrice,//划线价
                originalPrice = product.InHousePrice,//原价 取产品的成本价 即供应商的进仓价
                name = name,//标题
                numberOrders = 0,//购买人数
                clickNum = product.Star + 200,//浏览点击数量
                numberSells = 0,//售出件数
                StoreNum = product.Piece,//库存数 数字型
                stores = product.Piece.ToString(),//库存数 字符型
                storesStr = product.Piece > 2 ? "库存充足" : "库存紧张",//库存中文显示 库存充足 绿色显示，库存紧张 红色显示
                notes = name,//商品详情
                pic = Thumbnail,//商品图片
                HouseID = product.HouseID,
                HouseName = product.HouseName,//仓库名称
                PickUpAddress = wxUser.HouseAddress,//自提地址也就是仓库地址
                DeliveryAddress = product.SupplierAddress,//配送地址即供应商的地址
                IsMyHouseStock = product.HouseID.Equals(wxUser.HouseID) ? 0 : 1,//是否是本仓库存 0是 1否
                NextDayLogisFee = product.NextDayLogisFee,
                PromotionType = 1,

            });
            lstCategory.basicInfo = BasicInfo;
            result.data = lstCategory;
            //新增访问量
            CargoProductBus productBus = new CargoProductBus();
            productBus.AddProductAccessCount(new CargoProductAccessDetailsEntity()
            {
                SuppClientNum = product.SuppClientNum,
                HouseID = product.HouseID,
                ProductCode = product.ProductCode,
                wxOpenID = wxUser.wxOpenID,
                AccessTime = DateTime.Now
            }, new LogEntity()
            {
                IPAddress = Common.GetUserIP(HttpContext.Current.Request),
                Moudle = "慧采云仓",
                Status = "0",
                NvgPage = "更新商品访问量",
                UserID = product.SuppClientNum.ToString(),
                Operate = "A",
            });
        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }

        /// <summary>
        /// 预订单商品数据列表
        /// </summary>
        /// <param name="context"></param>
        private void GetReserveProductList(HttpContext context)
        {
            GoodsEntity result = new GoodsEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            //if (wxUser.ID.Equals(0))
            //{
            //    result.code = 1;
            //    result.msg = "Token有误";
            //    goto ERROR;
            //}

            CargoHouseBus bus = new CargoHouseBus();
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            if (!wxUser.StockShareHouseID.Equals(0))
            {
                queryEntity.HouseIDStr = wxUser.HouseID.ToString() + "," + wxUser.StockShareHouseID.ToString();
                queryEntity.HouseIDASC = wxUser.HouseID.ToString();
            }
            else
            {
                //没有共享仓库，就是自己的仓库，只查询自己的仓库库存即可
                queryEntity.HouseID = wxUser.HouseID;
            }
            //queryEntity.HouseID = 93;
            queryEntity.Specs = Convert.ToString(context.Request["Specs"]).ToUpper().Replace("UNDEFIND", "");//查询条件 规格
            queryEntity.TypeID = string.IsNullOrEmpty(context.Request["TypeID"]) ? 0 : Convert.ToInt32(context.Request["TypeID"]);//查询条件 品牌分类ID     
            queryEntity.SpecsType = Convert.ToString(context.Request["SpecsType"]);//查询条件 产品类型 即日达产品 4 次日达产品 5
            if (string.IsNullOrEmpty(context.Request["TypeID"]) || Convert.ToInt32(context.Request["TypeID"]) == 0)
            {
                queryEntity.WXTypeParentID = string.IsNullOrEmpty(context.Request["TypeParentID"]) ? "1" : Convert.ToString(context.Request["TypeParentID"]);//父ID   默认1
            }


            queryEntity.IsLockStock = "0";//非锁定的库存
            //queryEntity.TypeParentID = 1; //查询分类是轮胎的品牌产品库存
            //queryEntity.ProductPrice = 100;//设置过滤价格低于50元的

            int pageIndex = Convert.ToInt32(context.Request["page"]);//查询条件 分页 第几页
            int pageSize = Convert.ToInt32(context.Request["pageSize"]); //查询条件 分页 每页数量
            List<CargoContainerShowEntity> list = bus.QueryRerserveListedDataMiniPro(pageIndex, pageSize, queryEntity);
            //如果关键字搜索不为空，则表示是关键字搜索，则将搜索记录插入到关键字搜索表中
            bus.InsertCargoBehavior(new CargoBehaviorEntity
            {
                HouseID = wxUser.HouseID,
                ClientNum = wxUser.ClientNum,
                wxOpenID = wxUser.wxOpenID,
                SearchResult = list.Count <= 0 ? "0" : "1",
                BehaType = !string.IsNullOrEmpty(queryEntity.Specs) ? "1" : "0",
                OPDATE = DateTime.Now,
                KeyWord = queryEntity.Specs,
                TypeName = queryEntity.TypeID.Equals(0) ? "" : bus.QueryProductTypeName(new CargoProductTypeEntity { TypeID = queryEntity.TypeID })
            });
            if (list.Count <= 0)
            {
                result.code = 1004;
                result.msg = "商品未上架";
                goto ERROR;
            }
            CargoPriceBus priceBus = new CargoPriceBus();
            List<GoodsInfo> lstCategory = new List<GoodsInfo>();
            foreach (CargoContainerShowEntity product in list)
            {
                long RuleID = 0;
                decimal CutEntry = 0.00M;
                string Thumbnail = Common.GetSystemDomain() + product.Thumbnail.Replace("../", "");
                //string picUrl = Thumbnail.Replace("../", HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.LocalPath, "/"));
                if (string.IsNullOrEmpty(product.Thumbnail)) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; }
                string PName = string.Empty;
                if (product.TypeParentID.Equals(1))
                {
                    PName = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel;
                }
                else
                {
                    PName = product.ProductName;
                }

                if (wxUser.ClientType.Equals("5"))
                {
                    //云仓二批客户 急送和次日达均显示签约价格
                    product.SalePrice = product.SigningPrice.Equals(0) ? product.SalePrice : product.SigningPrice;
                }
                Random rd = new Random();

                lstCategory.Add(new GoodsInfo
                {
                    ProductCode = product.ProductCode,//商品唯一编码
                    Specs = product.Specs,//商品供应商
                    GoodsCode = product.GoodsCode,//商品供应商
                    Supplier = product.Supplier,//商品供应商
                    SpecsType = product.SpecsType,//产品类型 即日达和次日达
                    SuppClientNum = product.SuppClientNum,//商品供应商编码
                    BatchYear = product.BatchYear.ToString(),//商品周期 年
                    RuleID = RuleID,
                    minBuyNumber = Convert.ToInt32(product.minPurchase),//最小购买数量
                    minPrice = product.SalePrice - CutEntry,//取产品的销售价 即供应商的小程序价
                    signingPrice = product.SigningPrice - CutEntry,//取产品的签约价 即供应商的小程序价
                    listPrice = product.SalePrice + rd.Next(5, 20),//划线价
                    originalPrice = product.InHousePrice,//原价 取产品的进仓价 即供应商的进仓价
                    name = PName,//标题
                    numberOrders = 0,//浏览数量
                    clickNum = product.Star + 200,//浏览点击数量
                    numberSells = 0,//售出件数
                    //StoreNum = product.Piece,//库存数 数字型
                    StoreNum = 999,//库存数 数字型
                    stores = product.Piece.ToString(),//库存数 字符型
                    storesStr = product.Piece > 2 ? "库存充足" : "库存紧张",//库存中文显示 库存充足 绿色显示，库存紧张 红色显示
                    notes = PName,//商品详情
                    pic = Thumbnail,//商品图片
                    HouseID = product.HouseID,
                    TypeID = product.TypeID,
                    HouseName = product.HouseName,//仓库名称
                    PickUpAddress = wxUser.HouseAddress,//自提地址也就是仓库地址
                    DeliveryAddress = product.SupplierAddress,//配送地址即供应商的地址
                    IsMyHouseStock = product.HouseID.Equals(wxUser.HouseID) ? 0 : 1,//是否是本仓库存 0是 1否
                    PromotionType = 1,
                    Proportion = 0.9M,
                });
            }

            result.data = lstCategory;

        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        /// <summary>
        /// 预订单商品详情
        /// </summary>
        /// <param name="context"></param>
        private void GetReserveProductDetail(HttpContext context)
        {
            GoodsDetailEntity result = new GoodsDetailEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            GoodsDetail lstCategory = new GoodsDetail();
            CargoPriceBus priceBus = new CargoPriceBus();

            CargoHouseBus bus = new CargoHouseBus();
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            //queryEntity.HouseID = wxUser.HouseID;
            queryEntity.HouseID = string.IsNullOrEmpty(context.Request["HouseID"]) ? wxUser.HouseID : Convert.ToInt32(context.Request["HouseID"]);
            queryEntity.ProductCode = Convert.ToString(context.Request["ProductCode"]);//查询条件 产品编码
            queryEntity.Specs = Convert.ToString(context.Request["Specs"]);//查询条件 产品编码
            queryEntity.GoodsCode = Convert.ToString(context.Request["GoodsCode"]);//查询条件 产品编码
            queryEntity.BatchYear = string.IsNullOrEmpty(context.Request["BatchYear"]) ? 0 : Convert.ToInt32(context.Request["BatchYear"]);//查询条件 周期年
            queryEntity.IsLockStock = "0";//非锁定的库存
                                          // queryEntity.TypeParentID = 1; //查询分类是轮胎的品牌产品库
            queryEntity.SuppClientNum = string.IsNullOrEmpty(context.Request["SuppClientNum"]) ? 0 : Convert.ToInt32(context.Request["SuppClientNum"]);//供应商
            int pageIndex = 1;//查询条件 分页 第几页
            int pageSize = 1000; //查询条件 分页 每页数量
            List<CargoContainerShowEntity> list = bus.QueryRerserveListedDataMiniPro(pageIndex, pageSize, queryEntity);
            if (list.Count <= 0)
            {
                result.code = 1005;
                result.msg = "商品已下架";
                goto ERROR;
            }
            CargoContainerShowEntity product = list[0];
            long RuleID = 0;
            decimal CutEntry = 0.00M;
            string Thumbnail = Common.GetSystemDomain() + product.Thumbnail.Replace("../", "");

            if (string.IsNullOrEmpty(product.Thumbnail)) { Thumbnail = "https://dlt.neway5.com/DefaultImg.jpg"; }

            string name = string.Empty;
            if (product.TypeParentID.Equals(1))
            {
                name = product.TypeName + " " + product.Specs + " " + product.Figure + " " + product.LoadIndex + product.SpeedLevel;
            }
            else
            {
                name = product.ProductName;
            }

            if (wxUser.ClientType.Equals("5"))
            {
                //云仓二批客户 急送和次日达均显示签约价格
                product.SalePrice = product.SigningPrice.Equals(0) ? product.SalePrice : product.SigningPrice;
            }
            GoodsInfo BasicInfo = (new GoodsInfo
            {
                ProductCode = product.ProductCode,//商品唯一编码
                SpecsType = "5",// product.SpecsType,//产品类型 即日达和次日达
                Supplier = product.Supplier,//商品供应商
                SuppClientNum = product.SuppClientNum,//商品供应商编码
                BatchYear = product.BatchYear.ToString(),//商品周期 年
                RuleID = RuleID,
                TypeID = product.TypeID,//商品类型ID
                minBuyNumber = Convert.ToInt32(product.minPurchase),//最小购买数量
                //minPrice = product.SpecsType.Equals("4") ? Math.Round(product.SalePrice + Common.GetHCYCTodayOrderTransitFee() + (product.SalePrice * 0.009M), MidpointRounding.AwayFromZero) : Math.Round(product.SalePrice + (product.SalePrice * 0.009M), MidpointRounding.AwayFromZero),//现价销售价价
                //originalPrice = product.SalePrice,//原价
                minPrice = product.SalePrice - CutEntry,//取产品的销售价 即供应商的小程序价
                signingPrice = product.SigningPrice - CutEntry,//取产品的签约价 即供应商的小程序价
                listPrice = product.SalePrice,//划线价
                originalPrice = product.InHousePrice,//原价 取产品的成本价 即供应商的进仓价
                name = name,//标题
                numberOrders = 0,//购买人数
                clickNum = product.Star + 200,//浏览点击数量
                numberSells = 0,//售出件数
                StoreNum = 999, //product.Piece,//库存数 数字型
                stores = product.Piece.ToString(),//库存数 字符型
                storesStr = product.Piece > 2 ? "库存充足" : "库存紧张",//库存中文显示 库存充足 绿色显示，库存紧张 红色显示
                notes = name,//商品详情
                pic = Thumbnail,//商品图片
                HouseID = product.HouseID,
                HouseName = product.HouseName,//仓库名称
                PickUpAddress = wxUser.HouseAddress,//自提地址也就是仓库地址
                DeliveryAddress = product.SupplierAddress,//配送地址即供应商的地址
                IsMyHouseStock = product.HouseID.Equals(wxUser.HouseID) ? 0 : 1,//是否是本仓库存 0是 1否
                NextDayLogisFee = product.NextDayLogisFee,
                PromotionType = 2,
                Proportion = 0.9M, //支付比例 0.3

            });
            lstCategory.basicInfo = BasicInfo;
            result.data = lstCategory;
            //新增访问量
            CargoProductBus productBus = new CargoProductBus();
            productBus.AddProductAccessCount(new CargoProductAccessDetailsEntity()
            {
                SuppClientNum = product.SuppClientNum,
                HouseID = product.HouseID,
                ProductCode = product.ProductCode,
                wxOpenID = wxUser.wxOpenID,
                AccessTime = DateTime.Now
            }, new LogEntity()
            {
                IPAddress = Common.GetUserIP(HttpContext.Current.Request),
                Moudle = "慧采云仓",
                Status = "0",
                NvgPage = "更新商品访问量",
                UserID = product.SuppClientNum.ToString(),
                Operate = "A",
            });
        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        #endregion

        #region 订单管理
        /// <summary>
        /// 生成小程序商城订单号
        /// </summary>
        /// <returns></returns>
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
        /// 小程序提交订单
        /// </summary>
        /// <param name="context"></param>
        //private void CreateMiniProOrder(HttpContext context)
        //{
        //    CreateOrderEntity weiChat = new CreateOrderEntity();
        //    weiChat.code = 0;
        //    weiChat.msg = "成功";

        //    CreateOrderInfo cor = new CreateOrderInfo();
        //    string DToken = Convert.ToString(context.Request["token"]);
        //    //WriteTextLog("绑定" + DToken);
        //    if (string.IsNullOrEmpty(DToken))
        //    {
        //        weiChat.code = 1;
        //        weiChat.msg = "请求Token为空";
        //        goto ERROR;
        //    }
        //    WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
        //    if (wxUser.ID.Equals(0))
        //    {
        //        weiChat.code = 1;
        //        weiChat.msg = "Token有误";
        //        goto ERROR;
        //    }
        //    String json = context.Request["YPOrder"];//订单商品明细 主要是产品编码ProductCode，购买数量BuyNum，购买单价BuyPrice
        //    if (String.IsNullOrEmpty(json))
        //    {
        //        weiChat.code = 2;
        //        weiChat.msg = "参数有误";
        //        goto ERROR;
        //    }
        //    CargoInterfaceBus interBus = new CargoInterfaceBus();
        //    CargoHouseBus house = new CargoHouseBus();
        //    CargoOrderBus orderbus = new CargoOrderBus();
        //    CargoWeiXinBus wbus = new CargoWeiXinBus();
        //    CargoPriceBus price = new CargoPriceBus();
        //    CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxUser.HouseID);
        //    string YPSendType = string.IsNullOrEmpty(Convert.ToString(context.Request["YPSendType"])) ? "0" : Convert.ToString(context.Request["YPSendType"]);//配送方式 0：急送，1：自提2：快递
        //    DateTime StartBusHours = DateTime.Now;
        //    DateTime now = DateTime.Now;
        //    DateTime EndBusHours = DateTime.Now;
        //    if (!string.IsNullOrEmpty(houseEnt.StartBusHours))
        //    {
        //        string[] sbh = houseEnt.StartBusHours.Split(':');
        //        StartBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
        //    }
        //    if (!string.IsNullOrEmpty(houseEnt.EndBusHours))
        //    {
        //        string[] sbh = houseEnt.EndBusHours.Split(':');
        //        EndBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
        //    }
        //    if (YPSendType.Equals("0"))
        //    {
        //        if (now < StartBusHours || now > EndBusHours)
        //        {
        //            //送货和自提，判断时间是否在早上8点30和晚上19：30之间
        //            weiChat.code = 2; weiChat.msg = "很抱歉，已经过了送货时间！"; goto ERROR;
        //        }
        //    }
        //    if (YPSendType.Equals("1"))
        //    {
        //        if (now < StartBusHours || now > EndBusHours)
        //        {
        //            //送货和自提，判断时间是否在早上8点30和晚上19：30之间
        //            weiChat.code = 2; weiChat.msg = "很抱歉，已经过了自提时间！"; goto ERROR;
        //        }
        //    }
        //    string CheckOutType = string.IsNullOrEmpty(Convert.ToString(context.Request["CheckOutType"])) ? "5" : Convert.ToString(context.Request["CheckOutType"]);//付款方式 5：现付微信支付，3：货到付款 6：额度付款
        //    string YPOrderType = Convert.ToString(context.Request["YPOrderType"]);//订单类型 22：即日达，23：次日达
        //    string YPCompany = Convert.ToString(context.Request["YPCompany"]);//收货单位
        //    string YPAddress = Convert.ToString(context.Request["YPAddress"]);//收货地址
        //    string YPName = Convert.ToString(context.Request["YPName"]);//收货人
        //    string YPCellphone = Convert.ToString(context.Request["YPCellphone"]);//手机号码
        //    string YPRemark = Convert.ToString(context.Request["YPRemark"]).Replace("undefined", "");//备注
        //    string YPOrderMoney = Convert.ToString(context.Request["YPOrderMoney"]);//订单总金额
        //    string YPLogisMoney = Convert.ToString(context.Request["YPLogisMoney"]);//物流费
        //    string YPTotalMoney = Convert.ToString(context.Request["YPTotalMoney"]);//总金额
        //    string YPTotalPiece = Convert.ToString(context.Request["YPTotalPiece"]);//总数量 总条数
        //    string SuppClientNum = Convert.ToString(context.Request["YPSuppClientNum"]);//供应商编码
        //    string RuleID = Convert.ToString(context.Request["RuleID"]);//促销规则ID
        //    if (String.IsNullOrEmpty(YPOrderType)) { weiChat.code = 2; weiChat.msg = "订单类型有误"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPName)) { weiChat.code = 2; weiChat.msg = "购买人不能为空"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPCellphone)) { weiChat.code = 2; weiChat.msg = "购买手机号不能为空"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPTotalPiece)) { weiChat.code = 2; weiChat.msg = "总数量不能为空"; goto ERROR; }
        //    if (Convert.ToDecimal(YPTotalPiece) <= 0) { weiChat.code = 2; weiChat.msg = "总数量数据有误"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPOrderMoney)) { weiChat.code = 2; weiChat.msg = "订单金额不能为空"; goto ERROR; }
        //    if (Convert.ToDecimal(YPOrderMoney) <= 0) { weiChat.code = 2; weiChat.msg = "订单金额数据有误"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPTotalMoney)) { weiChat.code = 2; weiChat.msg = "总金额不能为空"; goto ERROR; }
        //    if (Convert.ToDecimal(YPTotalMoney) <= 0) { weiChat.code = 2; weiChat.msg = "总金额数据有误"; goto ERROR; }
        //    if (String.IsNullOrEmpty(SuppClientNum)) { weiChat.code = 2; weiChat.msg = "供应商不能为空"; goto ERROR; }
        //    if (Convert.ToInt32(SuppClientNum).Equals(0)) { weiChat.code = 2; weiChat.msg = "供应商数据有误"; goto ERROR; }
        //    if (YPOrderType.Equals("22") && YPSendType.Equals("0"))
        //    {
        //        //即日达
        //        if (YPTotalPiece.Equals("1"))
        //        {
        //            if (!wxUser.LogisFee.Equals(0))
        //            {
        //                if (!Convert.ToDecimal(YPLogisMoney).Equals(Convert.ToDecimal(YPTotalPiece) * wxUser.LogisFee))
        //                {
        //                    weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
        //                }
        //            }
        //        }
        //        else if (YPTotalPiece.Equals("2"))
        //        {
        //            if (!wxUser.TwoLogisFee.Equals(0))
        //            {
        //                if (!Convert.ToDecimal(YPLogisMoney).Equals(wxUser.TwoLogisFee))
        //                {
        //                    weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
        //                }
        //            }
        //        }
        //        else if (Convert.ToInt32(YPTotalPiece) >= 3)
        //        {
        //            if (!wxUser.ThreeLogisFee.Equals(0))
        //            {
        //                if (!Convert.ToDecimal(YPLogisMoney).Equals(wxUser.ThreeLogisFee))
        //                {
        //                    weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
        //                }
        //            }
        //        }
        //    }
        //    else if (YPSendType.Equals("2"))
        //    {
        //        //次日达
        //        decimal nextdayLogis = Convert.ToInt32(YPTotalPiece) * wxUser.NextDayLogisFee;
        //        if (!Convert.ToDecimal(YPLogisMoney).Equals(nextdayLogis))
        //        {
        //            weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
        //        }
        //    }

        //    string CouponID = Convert.ToString(context.Request["CouponID"]).Equals("0") ? "" : Convert.ToString(context.Request["CouponID"]).ToLower().Equals("undefined") ? "" : Convert.ToString(context.Request["CouponID"]);
        //    //string RuleID = context.Request["RuleID"];
        //    //促销规则
        //    CargoRuleBankEntity mrule = new CargoRuleBankEntity();
        //    if (!string.IsNullOrEmpty(RuleID))
        //    {
        //        mrule = price.QueryRuleBank(Convert.ToInt64(RuleID));
        //    }
        //    //判断促销规则，满送  赠送的数量
        //    int GiftNum = 0;
        //    if (mrule != null && mrule.RuleType != null && mrule.RuleType.Equals("8"))
        //    {
        //        //取整 买一送几的数值
        //        GiftNum = (int)Math.Truncate(Convert.ToDecimal(Convert.ToInt32(YPTotalPiece) / mrule.FullEntry));

        //    }
        //    //优惠券金额变量 优惠券类型，平台券或商家券
        //    decimal InsuranceFee = 0; string CouponType = "0";
        //    List<long> couponIDList = new List<long>();
        //    //20240930 传多张优惠券叠加使用，优惠券ID用逗号隔开，如：1,2,3，拆分成数组再循环处理
        //    WXCouponEntity coupon = new WXCouponEntity();
        //    if (!string.IsNullOrEmpty(CouponID))
        //    {
        //        string[] couponArr = CouponID.Split(',');
        //        foreach (string couponID in couponArr)
        //        {
        //            coupon = wbus.QueryCouponByID(new WXCouponEntity { ID = Convert.ToInt64(couponID) });
        //            if (!coupon.UseStatus.Equals("0"))
        //            { weiChat.code = 2; weiChat.msg = "优惠券已使用"; goto ERROR; }
        //            if (coupon.EndDate < DateTime.Now)
        //            {
        //                weiChat.code = 2; weiChat.msg = "优惠券已过期"; goto ERROR;
        //            }
        //            InsuranceFee += coupon.Money;
        //            CouponType = coupon.CouponType;
        //            couponIDList.Add(coupon.ID);
        //        }
        //        //coupon = wbus.QueryCouponByID(new WXCouponEntity { ID = Convert.ToInt64(CouponID) });
        //        //if (!coupon.UseStatus.Equals("0"))
        //        //{
        //        //    weiChat.code = 2; weiChat.msg = "优惠券已使用"; goto ERROR;
        //        //}
        //    }

        //    ArrayList rows = (ArrayList)JSON.Decode(json);
        //    List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
        //    List<CargoInterfaceEntity> goodList = new List<CargoInterfaceEntity>();
        //    int oNum = 0;
        //    decimal hzj = 0.00M;
        //    decimal originalPrice = 0M;
        //    foreach (Hashtable ht in rows)
        //    {
        //        if (string.IsNullOrEmpty(Convert.ToString(ht["BuyNum"]))) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
        //        if (Convert.ToInt32(ht["BuyNum"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
        //        if (string.IsNullOrEmpty(Convert.ToString(ht["BuyPrice"]))) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
        //        if (Convert.ToDecimal(ht["BuyPrice"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
        //        if (string.IsNullOrEmpty(Convert.ToString(ht["BatchYear"]))) { weiChat.code = 2; weiChat.msg = "商品周期有误"; goto ERROR; }
        //        pro.Add(new CargoProductShelvesEntity
        //        {
        //            ProductCode = Convert.ToString(ht["ProductCode"]),//产品编码
        //            BatchYear = Convert.ToInt32(ht["BatchYear"]),//商品周期批次年
        //            OrderNum = Convert.ToInt32(ht["BuyNum"]),//商品购买数量
        //            OrderPrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
        //            RuleID = mrule.ID,
        //            CutEntry = mrule.CutEntry
        //        });
        //        goodList.Add(new CargoInterfaceEntity
        //        {
        //            ProductCode = Convert.ToString(ht["ProductCode"]),
        //            BatchYear = Convert.ToInt32(ht["BatchYear"]),
        //            StockNum = Convert.ToInt32(ht["BuyNum"]),
        //            ActSalePrice = Convert.ToDecimal(ht["BuyPrice"])//商品购买单价
        //        });
        //        //originalPrice += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["originalPrice"]);
        //        oNum += Convert.ToInt32(ht["BuyNum"]);
        //        hzj += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["BuyPrice"]);
        //    }
        //    if (!Convert.ToInt32(YPTotalPiece).Equals(oNum))
        //    {
        //        weiChat.code = 2; weiChat.msg = "订单总数量有误"; goto ERROR;
        //    }

        //    if (!Convert.ToDecimal(YPOrderMoney).Equals(hzj))
        //    {
        //        weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
        //    }
        //    //订单总金额=商品明细总金额+物流运费-优惠券
        //    if (!Convert.ToDecimal(YPTotalMoney).Equals(Convert.ToDecimal(YPOrderMoney) + Convert.ToDecimal(YPLogisMoney) - InsuranceFee))
        //    {
        //        weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
        //    }
        //    //汕头仓玲珑2155517一个门店限购两条
        //    if (wxUser.HouseID.Equals(101) && pro[0].ProductCode.Equals("LTLL215551701") && mrule.ID > 0)
        //    {
        //        //特价限购2条
        //        if (pro[0].OrderNum > 2)
        //        {
        //            weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
        //        }

        //        int OrderPiece = wbus.QueryYunSpecialTyreNum(new WXOrderEntity { ProductCode = pro[0].ProductCode, HouseID = wxUser.HouseID, ClientNum = wxUser.ClientNum, PayStatus = "1" });
        //        if (OrderPiece >= 2)
        //        {
        //            weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
        //        }
        //        if (pro[0].OrderNum + OrderPiece > 2)
        //        {
        //            weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
        //        }
        //    }


        //    decimal wxZJ = Convert.ToDecimal(YPTotalMoney) * 100;
        //    string orderno = GetOrderNumber();//商城订单号
        //    decimal OverDayFee = 0.00M;//超期费用


        //    LogEntity log = new LogEntity();
        //    log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
        //    log.Moudle = "慧采云仓";
        //    log.Status = "0";
        //    log.NvgPage = "新增订单";
        //    log.UserID = wxUser.wxOpenID;
        //    log.Operate = "A";
        //    //同步修改收货人信息
        //    wbus.UpdateWeixinUser(new List<WXUserEntity> { new WXUserEntity { AcceptCellphone = YPCellphone, AcceptName = YPName, wxOpenID = wxUser.wxOpenID, Sex = wxUser.Sex } });

        //    CargoOrderEntity ent = new CargoOrderEntity();
        //    List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
        //    //保存生成仓库订单
        //    List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();

        //    ent.Dep = houseEnt.DepCity;
        //    ent.Dest = wxUser.City;
        //    int OrderNum = 0;
        //    ent.HouseID = wxUser.HouseID;
        //    ent.LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : YPSendType.Equals("1") ? 46 : 34;
        //    ent.Rebate = 0;
        //    ent.CheckOutType = CheckOutType;// "5";//Convert.ToString(row["CheckOutType"]);
        //                                    //ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(row["ReturnAwb"])) ? 0 : Convert.ToInt32(row["ReturnAwb"]);
        //    ent.TrafficType = "0";// Convert.ToString(row["TrafficType"]);
        //    ent.DeliveryType = YPOrderType.Equals("0") ? "2" : Convert.ToString(context.Request["YPSendType"]);//Convert.ToString(row["DeliveryType"]);
        //    ent.AcceptUnit = !string.IsNullOrEmpty(YPCompany) ? YPCompany : wxUser.ClientName;//Convert.ToString(row["AcceptUnit"]);取公司名称
        //    ent.AcceptAddress = !string.IsNullOrEmpty(YPAddress) ? YPAddress : wxUser.Address;// Convert.ToString(row["AcceptAddress"]);取注册时填写的公司地址
        //    ent.AcceptPeople = YPName;//Convert.ToString(row["AcceptPeople"]);
        //    ent.AcceptTelephone = YPCellphone;//Convert.ToString(row["AcceptTelephone"]);
        //    ent.AcceptCellphone = YPCellphone;//Convert.ToString(row["AcceptCellphone"]);
        //    ent.CreateAwb = wxUser.Name;//开单人生成订单人取当前微信人
        //    ent.CreateAwbID = wxUser.ClientNum.ToString();//开单人ID取
        //    ent.CreateDate = DateTime.Now;
        //    ent.OP_ID = wxUser.ClientNum.ToString();
        //    ent.SaleManID = wxUser.SaleManID;
        //    ent.SaleManName = wxUser.SaleManName;
        //    ent.SaleCellPhone = "";
        //    ent.Remark = YPRemark;
        //    //ent.CouponID = string.IsNullOrEmpty(CouponID) ? 0 : Convert.ToInt64(CouponID);
        //    ent.CouponIDList = couponIDList;
        //    //ent.ThrowGood = "0";
        //    ent.ThrowGood = YPOrderType;
        //    ent.BusinessID = "22";
        //    ent.IsPrintPrice = 1;
        //    ent.TranHouse = "";
        //    ent.PostponeShip = "1";
        //    ent.ClientNum = wxUser.ClientNum;
        //    ent.PayClientNum = wxUser.ClientNum;
        //    ent.PayClientName = wxUser.ClientName;//付款人客户姓名
        //    ent.ClientID = wxUser.ClientID;
        //    string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
        //    ent.OrderNo = Common.GetMaxOrderNumByCurrentDate(wxUser.HouseID, houseEnt.HouseCode, out OrderNum); // Convert.ToString(row["OrderNo"]);//生成最新顺序订单号
        //    ent.OutHouseName = houseEnt.Name;
        //    int pieceSum = 0;
        //    string proStr = string.Empty;
        //    foreach (var itt in goodList)
        //    {
        //        pieceSum += itt.StockNum;
        //        int piece = itt.StockNum;
        //        List<CargoProductEntity> productBasic = house.QueryALLProductData(new CargoProductEntity { ProductCode = itt.ProductCode, SuppClientNum = Convert.ToInt64(SuppClientNum) });
        //        if (productBasic.Count <= 0)
        //        {
        //            weiChat.code = 2; weiChat.msg = "商品基础数据有误"; goto ERROR;
        //        }
        //        proStr = productBasic[0].TypeName + " " + productBasic[0].Specs + " " + productBasic[0].Figure + " " + productBasic[0].LoadIndex + productBasic[0].SpeedLevel;
        //        CargoInterfaceEntity queryEntity = new CargoInterfaceEntity
        //        {
        //            ProductCode = itt.ProductCode,
        //            HouseID = wxUser.HouseID,
        //            TypeID = productBasic[0].TypeID,
        //            BatchYear = itt.BatchYear,
        //            ParentID = productBasic[0].ParentID
        //        };
        //        List<CargoInterfaceEntity> stockList = interBus.queryCargoStock(queryEntity);
        //        if (stockList.Count <= 0)
        //        {
        //            weiChat.code = 1006; weiChat.msg = "商品库存不足"; goto ERROR;
        //        }
        //        if (stockList.Sum(c => c.StockNum) < piece)
        //        {
        //            weiChat.code = 1006; weiChat.msg = productBasic[0].Specs + " " + productBasic[0].Figure + "库存不足"; goto ERROR;
        //        }
        //        //减库存规则，一周期早的先出先进先出，二数量和库存数刚好一样的先出
        //        foreach (var it in stockList)
        //        {
        //            if (it.StockNum <= 0) { continue; }
        //            CargoContainerShowEntity cargo = new CargoContainerShowEntity();
        //            cargo.OrderNo = ent.OrderNo;//订单号
        //            cargo.OutCargoID = outID;
        //            cargo.ContainerID = it.ContainerID;
        //            cargo.TypeID = it.TypeID;
        //            cargo.ProductID = it.ProductID;

        //            cargo.ID = it.ContainerGoodsID;//库存表ID

        //            //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
        //            cargo.HouseName = houseEnt.Name;
        //            //cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
        //            cargo.ProductName = it.ProductName;
        //            cargo.Model = it.Model;
        //            cargo.Specs = it.Specs;
        //            cargo.Figure = it.Figure;
        //            int inHouseDay = (int)(DateTime.Now - it.InHouseTime).TotalDays;
        //            int OverDay = 0;
        //            decimal OnlyOverDayFee = 0;

        //            #region 减库存逻辑
        //            if (piece < it.StockNum)
        //            {
        //                if (inHouseDay > wxUser.OverDayNum)
        //                {
        //                    //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
        //                    OverDay = inHouseDay - wxUser.OverDayNum;
        //                    OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * piece;
        //                    OverDayFee += OnlyOverDayFee;
        //                }
        //                //部分出
        //                entDest.Add(new CargoOrderGoodsEntity
        //                {
        //                    OrderNo = ent.OrderNo,
        //                    ProductID = it.ProductID,
        //                    HouseID = ent.HouseID,
        //                    AreaID = it.AreaID,
        //                    Piece = piece,
        //                    //ActSalePrice = it.SalePrice,
        //                    ActSalePrice = itt.ActSalePrice,
        //                    SupplySalePrice = it.InHousePrice,
        //                    ContainerCode = it.ContainerCode,
        //                    OutCargoID = outID,
        //                    RuleID = mrule.ID.ToString(),
        //                    RuleTitle = mrule.Title,
        //                    RuleType = mrule.RuleType,
        //                    OP_ID = log.UserID,
        //                    OverDayNum = OverDay,
        //                    OverDueFee = OnlyOverDayFee,
        //                });

        //                cargo.Piece = piece;
        //                cargo.InPiece = it.StockNum;
        //                originalPrice += piece * it.InHousePrice;//计算成本价
        //                outHouseList.Add(cargo);
        //                break;
        //            }
        //            if (piece.Equals(it.StockNum))
        //            {
        //                if (inHouseDay > wxUser.OverDayNum)
        //                {
        //                    //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
        //                    OverDay = inHouseDay - wxUser.OverDayNum;
        //                    OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * piece;
        //                    OverDayFee += OnlyOverDayFee;
        //                }
        //                //要出库件数和第一条库存件数刚刚好，则就全部出
        //                entDest.Add(new CargoOrderGoodsEntity
        //                {
        //                    OrderNo = ent.OrderNo,
        //                    ProductID = it.ProductID,
        //                    HouseID = ent.HouseID,
        //                    AreaID = it.AreaID,
        //                    Piece = piece,
        //                    //ActSalePrice = it.SalePrice,
        //                    SupplySalePrice = it.InHousePrice,
        //                    ActSalePrice = itt.ActSalePrice,
        //                    ContainerCode = it.ContainerCode,
        //                    OutCargoID = outID,
        //                    RuleID = mrule.ID.ToString(),
        //                    RuleTitle = mrule.Title,
        //                    RuleType = mrule.RuleType,
        //                    OP_ID = log.UserID,
        //                    OverDayNum = OverDay,
        //                    OverDueFee = OnlyOverDayFee,
        //                });
        //                cargo.Piece = piece;
        //                cargo.InPiece = it.StockNum;
        //                originalPrice += piece * it.InHousePrice;//计算成本价

        //                outHouseList.Add(cargo);
        //                break;
        //            }
        //            if (piece > it.StockNum)
        //            {
        //                if (inHouseDay > wxUser.OverDayNum)
        //                {
        //                    //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
        //                    OverDay = inHouseDay - wxUser.OverDayNum;
        //                    OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * it.StockNum;
        //                    OverDayFee += OnlyOverDayFee;
        //                }
        //                //全部出
        //                entDest.Add(new CargoOrderGoodsEntity
        //                {
        //                    OrderNo = ent.OrderNo,
        //                    ProductID = it.ProductID,
        //                    HouseID = ent.HouseID,
        //                    AreaID = it.AreaID,
        //                    Piece = it.StockNum,
        //                    //ActSalePrice = it.SalePrice,
        //                    SupplySalePrice = it.InHousePrice,
        //                    ActSalePrice = itt.ActSalePrice,
        //                    ContainerCode = it.ContainerCode,
        //                    OutCargoID = outID,
        //                    RuleID = mrule.ID.ToString(),
        //                    RuleTitle = mrule.Title,
        //                    RuleType = mrule.RuleType,
        //                    OP_ID = log.UserID,
        //                    OverDayNum = OverDay,
        //                    OverDueFee = OnlyOverDayFee,
        //                });
        //                cargo.Piece = it.StockNum;
        //                cargo.InPiece = it.StockNum;
        //                originalPrice += it.StockNum * it.InHousePrice;//计算成本价
        //                outHouseList.Add(cargo);
        //                piece = piece - it.StockNum;
        //                continue;
        //            }
        //            #endregion
        //        }
        //    }
        //    //判断是否有赠品，如果有赠品，则将赠品加入订单
        //    if (GiftNum > 0)
        //    {
        //        List<CargoContainerShowEntity> showEntities = house.QueryALLHouseData(new CargoContainerShowEntity { ProductID = mrule.ProductID });
        //        CargoContainerShowEntity showEntity = new CargoContainerShowEntity();
        //        foreach (var item in showEntities)
        //        {
        //            if (item.Piece < GiftNum)
        //            {
        //                weiChat.code = 2; weiChat.msg = "赠品库存不足"; goto ERROR;
        //            }
        //            showEntity = item; break;
        //        }
        //        //全部出
        //        entDest.Add(new CargoOrderGoodsEntity
        //        {
        //            OrderNo = ent.OrderNo,
        //            ProductID = mrule.ProductID,
        //            HouseID = ent.HouseID,
        //            AreaID = showEntity.AreaID,
        //            Piece = GiftNum,
        //            //ActSalePrice = it.SalePrice,
        //            SupplySalePrice = showEntity.InHousePrice,
        //            ActSalePrice = 0,
        //            ContainerCode = showEntity.ContainerCode,
        //            OutCargoID = outID,
        //            RuleID = mrule.ID.ToString(),
        //            RuleTitle = mrule.Title,
        //            RuleType = mrule.RuleType,
        //            OP_ID = log.UserID,
        //            OverDayNum = 0,
        //            OverDueFee = 0,
        //        });

        //        CargoContainerShowEntity cargo = new CargoContainerShowEntity();
        //        cargo.OrderNo = ent.OrderNo;//订单号
        //        cargo.OutCargoID = outID;
        //        cargo.ContainerID = showEntity.ContainerID;
        //        cargo.TypeID = showEntity.TypeID;
        //        cargo.ProductID = mrule.ProductID;

        //        cargo.ID = showEntity.ID;//库存表ID

        //        //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
        //        cargo.HouseName = houseEnt.Name;
        //        //cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
        //        cargo.ProductName = showEntity.ProductName;
        //        cargo.Model = showEntity.Model;
        //        cargo.Specs = showEntity.Specs;
        //        cargo.Figure = showEntity.Figure;

        //        cargo.Piece = GiftNum;
        //        cargo.InPiece = GiftNum;
        //        outHouseList.Add(cargo);

        //        pieceSum += GiftNum;
        //    }

        //    //订单总数量
        //    ent.Piece = Convert.ToInt32(YPTotalPiece) + GiftNum;
        //    ent.Weight = 0;
        //    ent.Volume = 0;
        //    ent.InsuranceFee = InsuranceFee;// coupon.Money;//优惠券金额 
        //    ent.TransitFee = Convert.ToDecimal(YPLogisMoney);//即日达20*数量  即日达的运输费用
        //    ent.TransportFee = originalPrice;
        //    ent.OverDueFee = OverDayFee;//超期费用
        //    //ent.TransportFee = Convert.ToDecimal(YPOrderMoney);//订单的费用
        //    ent.DeliveryFee = 0;
        //    ent.OtherFee = Convert.ToDecimal(YPTotalMoney) - ent.TransitFee - ent.TransportFee;//平台服服务费=总金额-配送费-销售金额
        //    ent.TotalCharge = Convert.ToDecimal(YPTotalMoney);

        //    ent.CouponType = CouponType;
        //    ent.AwbStatus = "0";
        //    ent.OrderType = "4";
        //    ent.OrderNum = OrderNum;//最新订单顺序号
        //    //ent.FinanceSecondCheck = "1";
        //    //ent.FinanceSecondCheckName = wxUser.Name;
        //    //ent.FinanceSecondCheckDate = DateTime.Now;
        //    ent.goodsList = entDest;
        //    ent.FinanceSecondCheck = "0";
        //    ent.OrderModel = "0";
        //    ent.SuppClientNum = Convert.ToInt32(SuppClientNum);
        //    ent.WXOrderNo = orderno;//微信商城订单号
        //    if (!ent.Piece.Equals(pieceSum))
        //    {
        //        weiChat.code = 2; weiChat.msg = "购买数量不一致"; goto ERROR;
        //    }
        //    //保存生成商城订单
        //    wbus.SaveWeixinOrder(new WXOrderEntity
        //    {
        //        OrderNo = orderno,
        //        TotalCharge = Convert.ToDecimal(YPTotalMoney),
        //        SuppClientNum = Convert.ToInt32(SuppClientNum),
        //        TransitFee = Convert.ToDecimal(YPLogisMoney),
        //        WXID = wxUser.ID,
        //        PayStatus = "0",
        //        OrderStatus = "0",
        //        PayWay = "0",
        //        OrderType = "4",//云配小程序订单
        //        ThrowGood = YPOrderType,//云配订单类型即日达和次日达
        //        Piece = Convert.ToInt32(YPTotalPiece) + GiftNum,//商城订单总数量
        //        Address = wxUser.Address,
        //        Cellphone = YPCellphone,
        //        City = wxUser.City,
        //        Province = wxUser.Province,
        //        Country = wxUser.Country,
        //        Name = YPName,//wxUser.Name,
        //        HouseID = wxUser.HouseID,
        //        SaleManID = wxUser.SaleManID,
        //        Memo = YPRemark,
        //        CouponID = 0,//!string.IsNullOrEmpty(CouponID) ? Convert.ToInt64(CouponID) : 0,//优惠券ID
        //        LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : YPSendType.Equals("1") ? 46 : 34,
        //        LogicName = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? "新陆程" : YPSendType.Equals("1") ? "自提" : "好来运速递",
        //        productList = pro
        //    }, log);

        //    //保存生成仓库出库订单
        //    orderbus.AddOrderInfo(ent, outHouseList, log);

        //    //支付成功，向客户发放优惠券根据订单明细返回的优惠规则ID，查询该规则的促销优惠内容，并向客户发放优惠券
        //    //如果是发放优惠券的规则，则向客户发放优惠券
        //    //CargoClientBus clientBus = new CargoClientBus();
        //    //if (mrule != null && mrule.IssueCoupon == 1)
        //    //{
        //    //    int couponNum = Convert.ToInt32(YPTotalPiece) / mrule.FullEntry;
        //    //    for (int i = 0; i < couponNum; i++)
        //    //    {
        //    //        clientBus.AddCoupon(new WXCouponEntity { WXID = wxUser.ID, Piece = 1, Money = mrule.CutEntry, UseStatus = "0", GainDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(mrule.ServiceTime), TypeID = mrule.UseTypeID,TypeName=mrule.UseTypeName, CouponType = mrule.CouponType.ToString(), SuppClientNum = mrule.SuppClientNum, IsSuperPosition = mrule.IsSuperPosition.ToString(), FromOrderNO = ent.WXOrderNo }, log);
        //    //    }
        //    //}

        //    if (!CheckOutType.Equals(6))
        //    {
        //        string tmst = TenPayV3Util.GetTimestamp();
        //        string nocs = TenPayV3Util.GetNoncestr();

        //        TenPayV3Info tenPayV3 = new TenPayV3Info(appid, appsecret, Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());
        //        string prepayID = PayInfo("", "慧采云仓小程序支付", wxUser.wxOpenID, wxZJ.ToString("F0"), orderno, tenPayV3, nocs);
        //        //string prepayID = Getprepay_id(appid, "", "慧采云仓小程序支付", _mch_id, GetRandomString(30), Common.GetHCYCWxPayTranUrl(), wxUser.wxOpenID, "JSAPI" + orderno, wxZJ.ToString("F0"));


        //        //设置支付参数
        //        RequestHandler paySignReqHandler = new RequestHandler();
        //        paySignReqHandler.SetParameter("appId", appid);
        //        paySignReqHandler.SetParameter("timeStamp", tmst);
        //        paySignReqHandler.SetParameter("nonceStr", nocs);
        //        //paySignReqHandler.SetParameter("partnerid", tenPayV3.MchId);
        //        //paySignReqHandler.SetParameter("prepayid", prepayID);
        //        paySignReqHandler.SetParameter("package", string.Format("prepay_id={0}", prepayID));
        //        paySignReqHandler.SetParameter("signType", "MD5");
        //        string paySign = paySignReqHandler.CreateMd5Sign("key", tenPayV3.Key);

        //        weiChat.msg = "{";
        //        weiChat.msg += " \"appId\": \"" + tenPayV3.AppId + "\",";
        //        weiChat.msg += " \"partnerId\": \"" + tenPayV3.MchId + "\",";
        //        weiChat.msg += " \"prepayId\": \"" + prepayID + "\",";
        //        weiChat.msg += " \"packageValue\": \"" + string.Format("prepay_id={0}", prepayID) + "\",";
        //        weiChat.msg += " \"timeStamp\": \"" + tmst + "\",";
        //        weiChat.msg += " \"nonceStr\": \"" + nocs + "\",";
        //        weiChat.msg += " \"sign\": \"" + paySign + "\",";
        //        weiChat.msg += " \"orderNo\": \"" + orderno + "\"";
        //        weiChat.msg += "}";
        //    }

        //    cor.orderno = orderno;
        //    weiChat.data = cor;

        //    if (CheckOutType.Equals("3"))
        //    {
        //        #region 推送好来运系统

        //        if (ent.HouseID.Equals(93))
        //        {
        //            //内部订单 || ent.HouseID.Equals(98)
        //            orderbus.SaveHlyOrderData(outHouseList, ent);
        //        }
        //        else
        //        {
        //            orderbus.InsertCargoOrderPush(new CargoOrderPushEntity
        //            {
        //                OrderNo = ent.OrderNo,
        //                Dep = ent.Dep,
        //                Dest = ent.Dest,
        //                Piece = ent.Piece,
        //                TransportFee = ent.TransportFee,
        //                ClientNum = ent.ClientNum.ToString(),
        //                AcceptAddress = ent.AcceptAddress,
        //                AcceptCellphone = ent.AcceptCellphone,
        //                AcceptTelephone = ent.AcceptTelephone,
        //                AcceptPeople = ent.AcceptPeople,
        //                AcceptUnit = ent.AcceptUnit,
        //                HouseID = ent.HouseID.ToString(),
        //                HouseName = ent.OutHouseName,
        //                OP_ID = wxUser.Name,
        //                PushType = "0",
        //                PushStatus = "0",
        //                LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : YPSendType.Equals("1") ? 46 : 34
        //            }, log);

        //        }

        //        #endregion

        //        string fkfs = ent.CheckOutType.Equals("3") ? "货到付款" : "现付";
        //        string shf = ent.LogisID.Equals(46) ? "自提" : YPSendType.Equals("0") ? "急送" : "次日达";
        //        string tit = ent.ThrowGood.Equals("22") ? YPSendType.Equals("2") ? "次日达" : "急速达" : "次日达";

        //        CargoNewOrderNoticeEntity cargoNewOrder = new CargoNewOrderNoticeEntity();
        //        cargoNewOrder.HouseName = houseEnt.Name;
        //        cargoNewOrder.OrderNo = ent.WXOrderNo;
        //        cargoNewOrder.OrderNum = ent.Piece.ToString();
        //        cargoNewOrder.ClientInfo = ent.AcceptPeople + " " + ent.AcceptCellphone + " " + ent.AcceptAddress;// "泰乐 华笙 广东省广州市白云区东平加油站左侧";
        //        cargoNewOrder.ProductInfo = proStr;// "马牌 215/55R16 CC6 98V";
        //        cargoNewOrder.DeliveryName = shf;// "自提";
        //        cargoNewOrder.ReceivePeople = "";
        //        string hcno = JSON.Encode(cargoNewOrder);
        //        //Common.WriteTextLog(hcno);
        //        List<CargoVoiceBroadEntity> voiceBroadList = house.GetVoiceBroadList(new CargoVoiceBroadEntity { HouseID = houseEnt.HouseID });
        //        foreach (var voice in voiceBroadList)
        //        {
        //            mc.Add("NewOrderNotice_" + voice.LoginName, hcno);

        //        }
        //        //mc.Add("NewOrderNotice_1000", hcno);
        //        //mc.Add("NewOrderNotice_2215", hcno);
        //        //mc.Add("NewOrderNotice_2856", hcno);

        //        //RedisHelper.SetString("NewOrderNotice", hcno);
        //        //货到付款
        //        try
        //        {
        //            QySendInfoEntity send = new QySendInfoEntity();
        //            send.title = tit + " 有新订单";
        //            //推送给提交人
        //            send.msgType = msgType.textcard;
        //            send.agentID = "1000003";//消息通知的应用
        //            send.AgentSecret = "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM";
        //            //send.toUser = qup.ApplyID;<div>订单金额：" + ord.TotalCharge.ToString("F2") + "</div>
        //            //send.toTag = "19";
        //            send.toTag = houseEnt.HCYCOrderPushTagID.ToString();
        //            send.content = "<div></div><div>出库仓库：" + houseEnt.Name + "</div><div>商城订单号：" + ent.WXOrderNo + "</div><div>出库订单号：" + ent.OrderNo + "</div><div>订单数量：" + ent.Piece.ToString() + "</div><div>货物信息：" + proStr + "</div><div>付款方式：" + fkfs + "</div><div>送货方式：" + shf + "</div><div>门店名称：" + ent.AcceptUnit + "</div><div>收货信息：" + ent.AcceptPeople + " " + ent.AcceptCellphone + "</div><div>收货地址：" + ent.AcceptAddress + "</div><div>请仓管人员留意尽快出库！</div>";
        //            send.url = "http://dlt.neway5.com/QY/qyScanOrderSign.aspx?OrderNo=" + ent.OrderNo;
        //            WxQYSendHelper.DLTQYPushInfo(send);
        //        }
        //        catch (ApplicationException ex)
        //        {

        //        }
        //        weiChat.msg = "提交成功，请收到货后在我的订单里支付货款";
        //    }
        //    else if (CheckOutType.Equals("6"))
        //    {
        //        weiChat.msg = "提交成功";
        //    }


        //ERROR:
        //    //JSON
        //    String result = JSON.Encode(weiChat);
        //    context.Response.Write(result);
        //}

        /// <summary>
        /// 通联支付提交订单
        /// </summary>
        /// <param name="context"></param>
        private void CreateMiniProOrder(HttpContext context)
        {
            CreateOrderEntity weiChat = new CreateOrderEntity();
            weiChat.code = 0;
            weiChat.msg = "成功";

            CreateOrderInfo cor = new CreateOrderInfo();
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken)) { weiChat.code = 1; weiChat.msg = "请求Token为空"; goto ERROR; }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0)) { weiChat.code = 1; weiChat.msg = "Token有误"; goto ERROR; }
            String json = context.Request["YPOrder"];//订单商品明细 主要是产品编码ProductCode，购买数量BuyNum，购买单价BuyPrice
            if (String.IsNullOrEmpty(json)) { weiChat.code = 2; weiChat.msg = "参数有误"; goto ERROR; }
            CargoInterfaceBus interBus = new CargoInterfaceBus();
            CargoHouseBus house = new CargoHouseBus();
            CargoOrderBus orderbus = new CargoOrderBus();
            CargoWeiXinBus wbus = new CargoWeiXinBus();
            CargoPriceBus price = new CargoPriceBus();
            wxUser.HouseID = string.IsNullOrEmpty(Convert.ToString(context.Request["HouseID"])) ? wxUser.HouseID : Convert.ToInt32(context.Request["HouseID"]);
            int PromotionType = Convert.ToInt32(context.Request["PromotionType"]);//0:正价1:特价促销
            CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxUser.HouseID);
            string YPSendType = string.IsNullOrEmpty(Convert.ToString(context.Request["YPSendType"])) ? "0" : Convert.ToString(context.Request["YPSendType"]);//配送方式 0：急送，1：自提2：快递
            if (Convert.ToString(context.Request["YPOrderType"]).Equals("23"))
            {
                YPSendType = "2";
            }
            DateTime StartBusHours = DateTime.Now;
            DateTime now = DateTime.Now;
            DateTime EndBusHours = DateTime.Now;
            if (!string.IsNullOrEmpty(houseEnt.StartBusHours))
            {
                string[] sbh = houseEnt.StartBusHours.Split(':');
                StartBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
            }
            if (!string.IsNullOrEmpty(houseEnt.EndBusHours))
            {
                string[] sbh = houseEnt.EndBusHours.Split(':');
                EndBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
            }
            if (wxUser.HouseID.Equals(101) || wxUser.HouseID.Equals(91) || wxUser.HouseID.Equals(106) || wxUser.HouseID.Equals(93))
            {
                //汕头,龙华，东平，南沙单独设置 
                if (YPSendType.Equals("0"))
                {
                    if (now < StartBusHours || now > EndBusHours)
                    {
                        //送货和自提，判断时间是否在早上8点30和晚上19：30之间
                        weiChat.code = 2; weiChat.msg = "很抱歉，已经过了送货时间！"; goto ERROR;
                    }
                }
            }
            else
            {
                if (YPSendType.Equals("0"))
                {
                    if (now < StartBusHours || now > EndBusHours)
                    {
                        //送货和自提，判断时间是否在早上8点30和晚上19：30之间
                        weiChat.code = 2; weiChat.msg = "很抱歉，已经过了送货时间！"; goto ERROR;
                    }
                }
                if (YPSendType.Equals("1"))
                {
                    if (now < StartBusHours || now > EndBusHours)
                    {
                        //送货和自提，判断时间是否在早上8点30和晚上19：30之间
                        weiChat.code = 2; weiChat.msg = "很抱歉，已经过了自提时间！"; goto ERROR;
                    }
                }
            }
            string CheckOutType = string.IsNullOrEmpty(Convert.ToString(context.Request["CheckOutType"])) ? "5" : Convert.ToString(context.Request["CheckOutType"]);//付款方式 5：现付微信支付，3：货到付款 6：额度付款  10:预收款付款
            string YPOrderType = Convert.ToString(context.Request["YPOrderType"]);//订单类型 22：即日达，23：次日达
            string YPCompany = Convert.ToString(context.Request["YPCompany"]);//收货单位
            string YPAddress = Convert.ToString(context.Request["YPAddress"]);//收货地址
            string YPName = Convert.ToString(context.Request["YPName"]);//收货人
            string YPCellphone = Convert.ToString(context.Request["YPCellphone"]);//手机号码
            string YPRemark = Convert.ToString(context.Request["YPRemark"]).Replace("undefined", "");//备注
            string YPOrderMoney = Convert.ToString(context.Request["YPOrderMoney"]);//订单总金额
            string YPLogisMoney = Convert.ToString(context.Request["YPLogisMoney"]);//物流费
            string YPTotalMoney = Convert.ToString(context.Request["YPTotalMoney"]);//总金额
            string YPTotalPiece = Convert.ToString(context.Request["YPTotalPiece"]);//总数量 总条数
            string SuppClientNum = Convert.ToString(context.Request["YPSuppClientNum"]);//供应商编码
            string RuleID = Convert.ToString(context.Request["RuleID"]);//促销规则ID    第一道防线
            if (String.IsNullOrEmpty(YPOrderType)) { weiChat.code = 2; weiChat.msg = "订单类型有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPName)) { weiChat.code = 2; weiChat.msg = "购买人不能为空"; goto ERROR; }
            if (String.IsNullOrEmpty(YPCellphone)) { weiChat.code = 2; weiChat.msg = "购买手机号不能为空"; goto ERROR; }
            if (String.IsNullOrEmpty(YPTotalPiece)) { weiChat.code = 2; weiChat.msg = "总数量不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPTotalPiece) <= 0) { weiChat.code = 2; weiChat.msg = "总数量数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPOrderMoney)) { weiChat.code = 2; weiChat.msg = "订单金额不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPOrderMoney) <= 0) { weiChat.code = 2; weiChat.msg = "订单金额数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPTotalMoney)) { weiChat.code = 2; weiChat.msg = "总金额不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPTotalMoney) <= 0) { weiChat.code = 2; weiChat.msg = "总金额数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(SuppClientNum)) { weiChat.code = 2; weiChat.msg = "供应商不能为空"; goto ERROR; }
            if (Convert.ToInt32(SuppClientNum).Equals(0)) { weiChat.code = 2; weiChat.msg = "供应商数据有误"; goto ERROR; }
            if ((YPOrderType.Equals("22")|| YPOrderType.Equals("26")) && YPSendType.Equals("0"))
            {
                //即日达
                if (YPTotalPiece.Equals("1"))
                {
                    if (!wxUser.LogisFee.Equals(0))
                    {
                        if (!Convert.ToDecimal(YPLogisMoney).Equals(Convert.ToDecimal(YPTotalPiece) * wxUser.LogisFee))
                        {
                            weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
                        }
                    }
                }
                else if (YPTotalPiece.Equals("2"))
                {
                    if (!wxUser.TwoLogisFee.Equals(0))
                    {
                        if (!Convert.ToDecimal(YPLogisMoney).Equals(wxUser.TwoLogisFee))
                        {
                            weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
                        }
                    }
                }
                else if (Convert.ToInt32(YPTotalPiece) >= 3)
                {
                    if (!wxUser.ThreeLogisFee.Equals(0))
                    {
                        if (!Convert.ToDecimal(YPLogisMoney).Equals(wxUser.ThreeLogisFee))
                        {
                            weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
                        }
                    }
                }
            }
            if ((YPOrderType.Equals("22") || YPOrderType.Equals("26")) && YPSendType.Equals("2"))
            {
                //次日达
                decimal nextdayLogis = Convert.ToInt32(YPTotalPiece) * wxUser.NextDayLogisFee;
                if (!Convert.ToDecimal(YPLogisMoney).Equals(nextdayLogis))
                {
                    weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
                }
            }

            string CouponID = Convert.ToString(context.Request["CouponID"]).Equals("0") ? "" : Convert.ToString(context.Request["CouponID"]).ToLower().Equals("undefined") ? "" : Convert.ToString(context.Request["CouponID"]);
            //string RuleID = context.Request["RuleID"];
            //促销规则
            CargoRuleBankEntity mrule = new CargoRuleBankEntity();
            if (!string.IsNullOrEmpty(RuleID))
            {
                mrule = price.QueryRuleBank(Convert.ToInt64(RuleID));
            }
            //第二道防线  再次查询
            //20250825  特价不查询优化信息
            if (mrule.ID == 0 && PromotionType != 1)
            {
                CargoRuleBankEntity entity = new CargoRuleBankEntity();
                entity.TypeID = Convert.ToInt32(context.Request["TypeID"]);
                entity.HouseID = wxUser.HouseID;
                entity.SuppClientNum = Convert.ToInt32(SuppClientNum);
                entity.StartDate = DateTime.Now;
                mrule = price.QueryRuleBank(entity).FirstOrDefault();
                if (mrule == null)
                {
                    mrule = new CargoRuleBankEntity();
                }
            }
            //判断促销规则，满送  赠送的数量
            int GiftNum = 0;
            if (mrule != null && mrule.RuleType != null && mrule.RuleType.Equals("8"))
            {
                //取整 买一送几的数值
                GiftNum = (int)Math.Truncate(Convert.ToDecimal(Convert.ToInt32(YPTotalPiece) / mrule.FullEntry));

            }
            //优惠券金额变量 优惠券类型，平台券或商家券
            decimal InsuranceFee = 0; string CouponType = "0";
            List<long> couponIDList = new List<long>();
            List<WXCouponEntity> wXCoupons = new List<WXCouponEntity>();
            //20240930 传多张优惠券叠加使用，优惠券ID用逗号隔开，如：1,2,3，拆分成数组再循环处理
            if (!string.IsNullOrEmpty(CouponID))
            {
                string[] couponArr = CouponID.Split(',');
                foreach (string couponID in couponArr)
                {
                    WXCouponEntity coupon = wbus.QueryCouponByID(new WXCouponEntity { ID = Convert.ToInt64(couponID) });
                    wXCoupons.Add(coupon);
                    if (!coupon.UseStatus.Equals("0"))
                    { weiChat.code = 2; weiChat.msg = "优惠券已使用"; goto ERROR; }
                    bool isExists = !string.IsNullOrWhiteSpace(coupon.ThrowGood)&& coupon.ThrowGood.Split(',')
                       .Select(s => s.Trim())
                       .Any(s => int.TryParse(s, out int val) && val == Convert.ToInt32(YPOrderType));
                    if (coupon.ThrowGood!="0"&& !isExists) {
                        weiChat.code = 2; weiChat.msg = "此订单无法使用该优惠卷"; goto ERROR;
                    }
                    if (PromotionType != 0)
                    {
                        weiChat.code = 2; weiChat.msg = "此订单无法使用该优惠卷"; goto ERROR;
                    }
                    if (coupon.EndDate < DateTime.Now)
                    {
                        weiChat.code = 2; weiChat.msg = "优惠券已过期"; goto ERROR;
                    }
                    InsuranceFee += coupon.Money;
                    CouponType = coupon.CouponType;
                    couponIDList.Add(coupon.ID);
                }
                //int a = wXCoupons.Select(c => c.IsFollowQuantity.Equals("1")).Count();
                if (wXCoupons.Where(c => c.IsFollowQuantity.Equals("1")).Count() > Convert.ToInt32(YPTotalPiece))
                {
                    weiChat.code = 2; weiChat.msg = "优惠券使用数量有误"; goto ERROR;
                }
            }

            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
            List<CargoInterfaceEntity> goodList = new List<CargoInterfaceEntity>();
            int oNum = 0;
            decimal hzj = 0.00M;
            decimal originalPrice = 0M;
            foreach (Hashtable ht in rows)
            {
                if (string.IsNullOrEmpty(Convert.ToString(ht["BuyNum"]))) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
                if (Convert.ToInt32(ht["BuyNum"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
                if (string.IsNullOrEmpty(Convert.ToString(ht["BuyPrice"]))) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
                if (Convert.ToDecimal(ht["BuyPrice"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
                if (string.IsNullOrEmpty(Convert.ToString(ht["BatchYear"]))) { weiChat.code = 2; weiChat.msg = "商品周期有误"; goto ERROR; }
                pro.Add(new CargoProductShelvesEntity
                {
                    ProductCode = Convert.ToString(ht["ProductCode"]),//产品编码
                    BatchYear = Convert.ToInt32(ht["BatchYear"]),//商品周期批次年
                    OrderNum = Convert.ToInt32(ht["BuyNum"]),//商品购买数量
                    OrderPrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                    RuleID = mrule.ID,
                    CutEntry = mrule.CutEntry
                });
                goodList.Add(new CargoInterfaceEntity
                {
                    ProductCode = Convert.ToString(ht["ProductCode"]),
                    BatchYear = Convert.ToInt32(ht["BatchYear"]),
                    StockNum = Convert.ToInt32(ht["BuyNum"]),
                    ActSalePrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                    SpecsType = YPOrderType.Equals("23") ? "5" : "4",
                });
                //originalPrice += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["originalPrice"]);
                oNum += Convert.ToInt32(ht["BuyNum"]);
                hzj += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["BuyPrice"]);
            }
            if (!Convert.ToInt32(YPTotalPiece).Equals(oNum))
            {
                weiChat.code = 2; weiChat.msg = "订单总数量有误"; goto ERROR;
            }

            if (!Convert.ToDecimal(YPOrderMoney).Equals(hzj))
            {
                weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
            }
            //订单总金额=商品明细总金额+物流运费-优惠券
            if (!Convert.ToDecimal(YPTotalMoney).Equals(Convert.ToDecimal(YPOrderMoney) + Convert.ToDecimal(YPLogisMoney) - InsuranceFee))
            {
                weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
            }
            if (CheckOutType.Equals("10"))
            {
                //预收款支付，判断下预收款金额是否足够
                if (Convert.ToDecimal(YPTotalMoney) > wxUser.PreReceiveMoney)
                {
                    weiChat.code = 2; weiChat.msg = "预收款金额不足：" + wxUser.PreReceiveMoney.ToString(); goto ERROR;
                }
            }
            //汕头仓玲珑2155517一个门店限购两条
            //if (wxUser.HouseID.Equals(101) && pro[0].ProductCode.Equals("LTLL215551701") && mrule.ID > 0)
            //{
            //    //特价限购2条
            //    if (pro[0].OrderNum > 2)
            //    {
            //        weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
            //    }

            //    int OrderPiece = wbus.QueryYunSpecialTyreNum(new WXOrderEntity { ProductCode = pro[0].ProductCode, HouseID = wxUser.HouseID, ClientNum = wxUser.ClientNum, PayStatus = "1" });
            //    if (OrderPiece >= 2)
            //    {
            //        weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
            //    }
            //    if (pro[0].OrderNum + OrderPiece > 2)
            //    {
            //        weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
            //    }
            //}


            decimal wxZJ = Convert.ToDecimal(YPTotalMoney) * 100;
            string orderno = GetOrderNumber();//商城订单号
            decimal OverDayFee = 0.00M;//超期费用


            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "新增订单";
            log.UserID = wxUser.wxOpenID;
            log.Operate = "A";
            //同步修改收货人信息
            //wbus.UpdateWeixinUser(new List<WXUserEntity> { new WXUserEntity { AcceptCellphone = YPCellphone, AcceptName = YPName, wxOpenID = wxUser.wxOpenID, Sex = wxUser.Sex } });

            CargoOrderEntity ent = new CargoOrderEntity();
            List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
            //保存生成仓库订单
            List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();

            ent.Dep = houseEnt.DepCity;
            ent.Dest = wxUser.City;
            int OrderNum = 0;
            ent.HouseID = wxUser.HouseID;
            ent.LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34;
            ent.Rebate = 0;
            ent.CheckOutType = CheckOutType;// "5";//Convert.ToString(row["CheckOutType"]);
                                            //ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(row["ReturnAwb"])) ? 0 : Convert.ToInt32(row["ReturnAwb"]);
            ent.TrafficType = "0";// Convert.ToString(row["TrafficType"]);
            ent.DeliveryType = YPOrderType.Equals("23") ? "2" : Convert.ToString(context.Request["YPSendType"]);//Convert.ToString(row["DeliveryType"]);
            ent.AcceptUnit = !string.IsNullOrEmpty(YPCompany) ? YPCompany : wxUser.ClientName;//Convert.ToString(row["AcceptUnit"]);取公司名称
            ent.AcceptAddress = !string.IsNullOrEmpty(YPAddress) ? YPAddress : wxUser.Address;// Convert.ToString(row["AcceptAddress"]);取注册时填写的公司地址
            ent.AcceptPeople = YPName;//Convert.ToString(row["AcceptPeople"]);
            ent.AcceptTelephone = YPCellphone;//Convert.ToString(row["AcceptTelephone"]);
            ent.AcceptCellphone = YPCellphone;//Convert.ToString(row["AcceptCellphone"]);
            ent.CreateAwb = wxUser.Name;//开单人生成订单人取当前微信人
            ent.CreateAwbID = wxUser.ClientNum.ToString();//开单人ID取
            ent.CreateDate = DateTime.Now;
            ent.OP_ID = wxUser.ClientNum.ToString();
            ent.OP_Name = wxUser.Name;
            ent.SaleManID = wxUser.SaleManID;
            ent.SaleManName = wxUser.SaleManName;
            ent.SaleCellPhone = "";
            ent.Remark = YPRemark;
            //ent.CouponID = string.IsNullOrEmpty(CouponID) ? 0 : Convert.ToInt64(CouponID);
            ent.CouponIDList = couponIDList;
            //ent.ThrowGood = "0";
            ent.ThrowGood = YPOrderType;
            ent.BusinessID = "22";
            ent.MarketType = "2";
            ent.IsPrintPrice = 1;
            ent.TranHouse = "";
            ent.PostponeShip = "1";
            ent.ClientNum = wxUser.ClientNum;
            ent.PayClientNum = wxUser.ClientNum;
            ent.PayClientName = wxUser.ClientName;//付款人客户姓名
            ent.ClientID = wxUser.ClientID;
            string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
            ent.OrderNo = Common.GetMaxOrderNumByCurrentDate(wxUser.HouseID, houseEnt.HouseCode, out OrderNum); // Convert.ToString(row["OrderNo"]);//生成最新顺序订单号
            ent.OutHouseName = houseEnt.Name;
            int pieceSum = 0;
            string proStr = string.Empty;
            foreach (var itt in goodList)
            {
                pieceSum += itt.StockNum;
                int piece = itt.StockNum;
                List<CargoProductEntity> productBasic = house.QueryALLProductData(new CargoProductEntity { ProductCode = itt.ProductCode, SuppClientNum = Convert.ToInt64(SuppClientNum) });
                if (productBasic.Count <= 0)
                {
                    weiChat.code = 2; weiChat.msg = "商品基础数据有误"; goto ERROR;
                }
                proStr = productBasic[0].TypeName + " " + productBasic[0].Specs + " " + productBasic[0].Figure + " " + productBasic[0].LoadIndex + productBasic[0].SpeedLevel;
                CargoInterfaceEntity queryEntity = new CargoInterfaceEntity
                {
                    ProductCode = itt.ProductCode,
                    HouseID = wxUser.HouseID,
                    TypeID = productBasic[0].TypeID,
                    BatchYear = itt.BatchYear,
                    ParentID = productBasic[0].ParentID,
                    Regular = PromotionType,
                    SuppClientNum = SuppClientNum,
                    SpecsType = YPOrderType.Equals("23") ? "5" : "4",
                };
                List<CargoInterfaceEntity> stockList = interBus.queryCargoStock(queryEntity);
                if (stockList.Count <= 0)
                {
                    weiChat.code = 1006; weiChat.msg = "商品库存不足"; goto ERROR;
                }
                if (stockList.Sum(c => c.StockNum) < piece)
                {
                    weiChat.code = 1006; weiChat.msg = productBasic[0].Specs + " " + productBasic[0].Figure + "库存不足"; goto ERROR;
                }
                //减库存规则，一周期早的先出先进先出，二数量和库存数刚好一样的先出
                foreach (var it in stockList)
                {
                    if (it.StockNum <= 0) { continue; }
                    if (!it.ShareHouseID.Equals(0))
                    {
                        if (YPOrderType.Equals("23"))
                        {
                            //次日达 慧采云仓
                            ent.OpenOrderSource = "1";
                        }
                        ent.ShareHouseID = it.ShareHouseID;
                        ent.ShareHouseName = it.ShareHouseName;
                    }

                    CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                    cargo.OrderNo = ent.OrderNo;//订单号
                    cargo.OutCargoID = outID;
                    cargo.ContainerID = it.ContainerID;
                    cargo.TypeID = it.TypeID;
                    cargo.ProductID = it.ProductID;

                    cargo.ID = it.ContainerGoodsID;//库存表ID

                    //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
                    cargo.HouseName = houseEnt.Name;
                    //cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
                    cargo.ProductName = it.ProductName;
                    cargo.Model = it.Model;
                    cargo.Specs = it.Specs;
                    cargo.Figure = it.Figure;
                    int inHouseDay = (int)(DateTime.Now - it.InHouseTime).TotalDays;
                    int OverDay = 0;
                    decimal OnlyOverDayFee = 0;

                    #region 减库存逻辑
                    if (piece < it.StockNum)
                    {
                        if (inHouseDay > wxUser.OverDayNum)
                        {
                            //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
                            OverDay = inHouseDay - wxUser.OverDayNum;
                            OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * piece;
                            OverDayFee += OnlyOverDayFee;
                        }
                        //部分出
                        entDest.Add(new CargoOrderGoodsEntity
                        {
                            OrderNo = ent.OrderNo,
                            ProductID = it.ProductID,
                            HouseID = ent.HouseID,
                            AreaID = it.AreaID,
                            Piece = piece,
                            //ActSalePrice = it.SalePrice,
                            ActSalePrice = itt.ActSalePrice,
                            SupplySalePrice = it.InHousePrice,
                            ContainerCode = it.ContainerCode,
                            OutCargoID = outID,
                            RuleID = mrule.ID.ToString(),
                            RuleTitle = mrule.Title,
                            RuleType = mrule.RuleType,
                            OP_ID = log.UserID,
                            OverDayNum = OverDay,
                            OverDueFee = OnlyOverDayFee,
                        });

                        cargo.Piece = piece;
                        cargo.InPiece = it.StockNum;
                        originalPrice += piece * it.InHousePrice;//计算成本价
                        outHouseList.Add(cargo);
                        break;
                    }
                    if (piece.Equals(it.StockNum))
                    {
                        if (inHouseDay > wxUser.OverDayNum)
                        {
                            //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
                            OverDay = inHouseDay - wxUser.OverDayNum;
                            OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * piece;
                            OverDayFee += OnlyOverDayFee;
                        }
                        //要出库件数和第一条库存件数刚刚好，则就全部出
                        entDest.Add(new CargoOrderGoodsEntity
                        {
                            OrderNo = ent.OrderNo,
                            ProductID = it.ProductID,
                            HouseID = ent.HouseID,
                            AreaID = it.AreaID,
                            Piece = piece,
                            //ActSalePrice = it.SalePrice,
                            SupplySalePrice = it.InHousePrice,
                            ActSalePrice = itt.ActSalePrice,
                            ContainerCode = it.ContainerCode,
                            OutCargoID = outID,
                            RuleID = mrule.ID.ToString(),
                            RuleTitle = mrule.Title,
                            RuleType = mrule.RuleType,
                            OP_ID = log.UserID,
                            OverDayNum = OverDay,
                            OverDueFee = OnlyOverDayFee,
                        });
                        cargo.Piece = piece;
                        cargo.InPiece = it.StockNum;
                        originalPrice += piece * it.InHousePrice;//计算成本价

                        outHouseList.Add(cargo);
                        break;
                    }
                    if (piece > it.StockNum)
                    {
                        if (inHouseDay > wxUser.OverDayNum)
                        {
                            //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
                            OverDay = inHouseDay - wxUser.OverDayNum;
                            OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * it.StockNum;
                            OverDayFee += OnlyOverDayFee;
                        }
                        //全部出
                        entDest.Add(new CargoOrderGoodsEntity
                        {
                            OrderNo = ent.OrderNo,
                            ProductID = it.ProductID,
                            HouseID = ent.HouseID,
                            AreaID = it.AreaID,
                            Piece = it.StockNum,
                            //ActSalePrice = it.SalePrice,
                            SupplySalePrice = it.InHousePrice,
                            ActSalePrice = itt.ActSalePrice,
                            ContainerCode = it.ContainerCode,
                            OutCargoID = outID,
                            RuleID = mrule.ID.ToString(),
                            RuleTitle = mrule.Title,
                            RuleType = mrule.RuleType,
                            OP_ID = log.UserID,
                            OverDayNum = OverDay,
                            OverDueFee = OnlyOverDayFee,
                        });
                        cargo.Piece = it.StockNum;
                        cargo.InPiece = it.StockNum;
                        originalPrice += it.StockNum * it.InHousePrice;//计算成本价
                        outHouseList.Add(cargo);
                        piece = piece - it.StockNum;
                        continue;
                    }
                    #endregion
                }
            }
            //判断是否有赠品，如果有赠品，则将赠品加入订单
            if (GiftNum > 0)
            {
                List<CargoContainerShowEntity> showEntities = house.QueryALLHouseData(new CargoContainerShowEntity { ProductID = mrule.ProductID });
                CargoContainerShowEntity showEntity = new CargoContainerShowEntity();
                foreach (var item in showEntities)
                {
                    if (item.Piece < GiftNum)
                    {
                        weiChat.code = 2; weiChat.msg = "赠品库存不足"; goto ERROR;
                    }
                    showEntity = item; break;
                }
                //全部出
                entDest.Add(new CargoOrderGoodsEntity
                {
                    OrderNo = ent.OrderNo,
                    ProductID = mrule.ProductID,
                    HouseID = ent.HouseID,
                    AreaID = showEntity.AreaID,
                    Piece = GiftNum,
                    //ActSalePrice = it.SalePrice,
                    SupplySalePrice = showEntity.InHousePrice,
                    ActSalePrice = 0,
                    ContainerCode = showEntity.ContainerCode,
                    OutCargoID = outID,
                    RuleID = mrule.ID.ToString(),
                    RuleTitle = mrule.Title,
                    RuleType = mrule.RuleType,
                    OP_ID = log.UserID,
                    OverDayNum = 0,
                    OverDueFee = 0,
                });

                CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                cargo.OrderNo = ent.OrderNo;//订单号
                cargo.OutCargoID = outID;
                cargo.ContainerID = showEntity.ContainerID;
                cargo.TypeID = showEntity.TypeID;
                cargo.ProductID = mrule.ProductID;

                cargo.ID = showEntity.ID;//库存表ID

                //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
                cargo.HouseName = houseEnt.Name;
                //cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
                cargo.ProductName = showEntity.ProductName;
                cargo.Model = showEntity.Model;
                cargo.Specs = showEntity.Specs;
                cargo.Figure = showEntity.Figure;

                cargo.Piece = GiftNum;
                cargo.InPiece = GiftNum;
                outHouseList.Add(cargo);

                pieceSum += GiftNum;
            }

            //订单总数量
            ent.Piece = Convert.ToInt32(YPTotalPiece) + GiftNum;
            ent.Weight = 0;
            ent.Volume = 0;
            ent.InsuranceFee = InsuranceFee;// coupon.Money;//优惠券金额 
            ent.TransitFee = Convert.ToDecimal(YPLogisMoney);//即日达20*数量  即日达的运输费用
            ent.TransportFee = originalPrice;
            ent.OverDueFee = OverDayFee;//超期费用
            //ent.TransportFee = Convert.ToDecimal(YPOrderMoney);//订单的费用
            ent.DeliveryFee = 0;
            ent.OtherFee = Convert.ToDecimal(YPTotalMoney) - ent.TransitFee - ent.TransportFee + ent.InsuranceFee;//平台服服务费=总金额-配送费-销售金额+优惠券
            ent.TotalCharge = Convert.ToDecimal(YPTotalMoney);

            ent.CouponType = CouponType;
            ent.AwbStatus = "0";
            ent.OrderType = "4";
            ent.OrderNum = OrderNum;//最新订单顺序号
            //ent.FinanceSecondCheck = "1";
            //ent.FinanceSecondCheckName = wxUser.Name;
            //ent.FinanceSecondCheckDate = DateTime.Now;
            ent.goodsList = entDest;
            ent.FinanceSecondCheck = "0";
            ent.OrderModel = "0";
            ent.SuppClientNum = Convert.ToInt32(SuppClientNum);
            ent.WXOrderNo = orderno;//微信商城订单号
            if (!ent.Piece.Equals(pieceSum))
            {
                weiChat.code = 2; weiChat.msg = "购买数量不一致"; goto ERROR;
            }
            if (CheckOutType.Equals("10"))
            {
                ent.FinanceSecondCheck = "1";
                ent.FinanceSecondCheckName = wxUser.Name;
                ent.FinanceSecondCheckDate = DateTime.Now;
            }
            //保存生成商城订单
            wbus.SaveWeixinOrder(new WXOrderEntity
            {
                OrderNo = orderno,
                TotalCharge = Convert.ToDecimal(YPTotalMoney),
                SuppClientNum = Convert.ToInt32(SuppClientNum),
                TransitFee = Convert.ToDecimal(YPLogisMoney),
                WXID = wxUser.ID,
                PayStatus = CheckOutType.Equals("10") ? "1" : CheckOutType.Equals("10") ? "1" : "0",
                OrderStatus = "0",
                PayWay = CheckOutType.Equals("10") ? "3" : CheckOutType.Equals("6") ? "1" : "0",
                OrderType = "4",//小程序订单
                ThrowGood = YPOrderType,//订单类型即日达和次日达
                Piece = Convert.ToInt32(YPTotalPiece) + GiftNum,//商城订单总数量
                Address = wxUser.Address,
                Cellphone = YPCellphone,
                City = wxUser.City,
                Province = wxUser.Province,
                Country = wxUser.Country,
                Name = YPName,//wxUser.Name,
                HouseID = wxUser.HouseID,
                SaleManID = wxUser.SaleManID,
                Memo = YPRemark,
                CouponID = 0,//!string.IsNullOrEmpty(CouponID) ? Convert.ToInt64(CouponID) : 0,//优惠券ID
                LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34,
                LogicName = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? "新陆程" : wxUser.HouseID.Equals(136) ? "南宁好来运" : YPSendType.Equals("1") ? "自提" : "好来运速递",
                productList = pro
            }, log);

            //保存生成仓库出库订单
            orderbus.AddOrderInfo(ent, outHouseList, log);

            //支付成功，向客户发放优惠券根据订单明细返回的优惠规则ID，查询该规则的促销优惠内容，并向客户发放优惠券
            //如果是发放优惠券的规则，则向客户发放优惠券
            //CargoClientBus clientBus = new CargoClientBus();
            //if (mrule != null && mrule.IssueCoupon == 1)
            //{
            //    int couponNum = Convert.ToInt32(YPTotalPiece) / mrule.FullEntry;
            //    for (int i = 0; i < couponNum; i++)
            //    {
            //        clientBus.AddCoupon(new WXCouponEntity { WXID = wxUser.ID, Piece = 1, Money = mrule.CutEntry, UseStatus = "0", GainDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(mrule.ServiceTime), TypeID = mrule.UseTypeID,TypeName=mrule.UseTypeName, CouponType = mrule.CouponType.ToString(), SuppClientNum = mrule.SuppClientNum, IsSuperPosition = mrule.IsSuperPosition.ToString(), FromOrderNO = ent.WXOrderNo }, log);
            //    }
            //}

            if (!CheckOutType.Equals("6") && !CheckOutType.Equals("10"))
            {
                try
                {
                    SybWxPayService sybService = new SybWxPayService();
                    Dictionary<String, String> rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(YPTotalMoney) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/UnionPaySuccess.aspx", "");
                    Dictionary<String, String> payinfoDic = payinfo(rsp);
                    string jsonString = String.Join(",", rsp.Select(kvp => kvp.Key + "=" + kvp.Value));
                    Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：" + jsonString);

                    TenPayV3Info tenPayV3 = new TenPayV3Info(appid, appsecret, Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());

                    weiChat.msg = "{";
                    weiChat.msg += " \"appId\": \"" + tenPayV3.AppId + "\",";
                    weiChat.msg += " \"partnerId\": \"" + tenPayV3.MchId + "\",";
                    weiChat.msg += " \"prepayId\": \"" + payinfoDic["package"] + "\",";
                    weiChat.msg += " \"packageValue\": \"" + string.Format("prepay_id={0}", payinfoDic["package"]) + "\",";
                    weiChat.msg += " \"timeStamp\": \"" + payinfoDic["timeStamp"] + "\",";
                    weiChat.msg += " \"nonceStr\": \"" + payinfoDic["nonceStr"] + "\",";
                    weiChat.msg += " \"sign\": \"" + payinfoDic["paySign"] + "\",";
                    weiChat.msg += " \"orderNo\": \"" + orderno + "\"";
                    weiChat.msg += "}";
                }
                catch (Exception ex)
                {
                    Common.WriteTextLog("慧采云仓小程序 通联支付回调失败信息：" + ex.Message);
                    //ex.Message;
                    weiChat.code = 2; weiChat.msg = "订单推送失败"; goto ERROR;

                }
            }

            cor.orderno = orderno;
            weiChat.data = cor;

            //如果是次日达，并且库存是共享仓库库存，写入缓存
            if (!ent.ShareHouseID.Equals(0) && YPOrderType.Equals("23"))
            {
                //goodList
                RedisHelper.HashSet("NextDayOrderShareSync", ent.OrderNo + "_" + ent.HouseID.ToString() + "_" + ent.ShareHouseID.ToString(), JSON.Encode(goodList));
            }

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

            if (CheckOutType.Equals("3"))
            {
                #region 推送好来运系统

                if (ent.HouseID.Equals(93))
                {
                    //内部订单 || ent.HouseID.Equals(98)
                    orderbus.SaveHlyOrderData(outHouseList, ent);
                }
                else
                {
                    orderbus.InsertCargoOrderPush(new CargoOrderPushEntity
                    {
                        OrderNo = ent.OrderNo,
                        Dep = ent.Dep,
                        Dest = ent.Dest,
                        Piece = ent.Piece,
                        TransportFee = ent.TransportFee,
                        ClientNum = ent.ClientNum.ToString(),
                        AcceptAddress = ent.AcceptAddress,
                        AcceptCellphone = ent.AcceptCellphone,
                        AcceptTelephone = ent.AcceptTelephone,
                        AcceptPeople = ent.AcceptPeople,
                        AcceptUnit = ent.AcceptUnit,
                        HouseID = ent.HouseID.ToString(),
                        HouseName = ent.OutHouseName,
                        OP_ID = wxUser.Name,
                        PushType = "0",
                        PushStatus = "0",
                        LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34
                    }, log);

                }

                #endregion

                string fkfs = ent.CheckOutType.Equals("3") ? "货到付款" : ent.CheckOutType.Equals("10") ? "预付款" : "现付";
                string shf = ent.LogisID.Equals(46) ? "自提" : YPSendType.Equals("0") ? "急送" : "次日达";
                string tit = ent.ThrowGood.Equals("23") ? "次日达" : (YPSendType.Equals("2") ? "次日达" : "急速达") ;
                string go = ent.ThrowGood.Equals("23") ? "-不出库" :  (YPSendType.Equals("2") ? "" : "") ;
                CargoNewOrderNoticeEntity cargoNewOrder = new CargoNewOrderNoticeEntity();
                cargoNewOrder.HouseName = houseEnt.Name;
                cargoNewOrder.OrderNo = ent.WXOrderNo;
                cargoNewOrder.OrderNum = ent.Piece.ToString();
                cargoNewOrder.ClientInfo = ent.AcceptPeople + " " + ent.AcceptCellphone + " " + ent.AcceptAddress;// "泰乐 华笙 广东省广州市白云区东平加油站左侧";
                cargoNewOrder.ProductInfo = proStr;// "马牌 215/55R16 CC6 98V";
                cargoNewOrder.DeliveryName = shf;// "自提";
                cargoNewOrder.ReceivePeople = "";
                string hcno = JSON.Encode(cargoNewOrder);
                //Common.WriteTextLog(hcno);

                //如果是次日达，并且库存不是共享仓库库存，要推送给供应商
                if (ent.ShareHouseID.Equals(0) && ent.ThrowGood.Equals("23"))
                {
                    string Name = houseEnt.Name + " " + proStr;
                    // 若长度小于等于20，直接返回原字符串；否则截取前20个
                    Name = Name.Length <= 20 ? Name : Name.Substring(0, 20);
                    //供应商名称  物流单号   订单号    商品名称   金额   数量
                    Common.SendRePlaceAnOrderMsg(ent.SuppClientNum.ToString(), ent.LogisAwbNo, ent.OrderNo, Name, ent.TotalCharge, ent.Piece);
                }
                else
                {
                    //企业微信推送
                    List<CargoVoiceBroadEntity> voiceBroadList = house.GetVoiceBroadList(new CargoVoiceBroadEntity { HouseID = houseEnt.HouseID });
                    foreach (var voice in voiceBroadList)
                    {
                        RedisHelper.SetString("NewOrderNotice_" + voice.LoginName, hcno);
                        //mc.Add("NewOrderNotice_" + voice.LoginName, hcno);
                    }
                }
                //mc.Add("NewOrderNotice_1000", hcno);
                //mc.Add("NewOrderNotice_2215", hcno);
                //mc.Add("NewOrderNotice_2856", hcno);

                //RedisHelper.SetString("NewOrderNotice", hcno);
                //货到付款
                try
                {
                    QySendInfoEntity send = new QySendInfoEntity();
                    send.title = tit + " 有新订单";
                    //推送给提交人
                    send.msgType = msgType.textcard;
                    send.agentID = "1000003";//消息通知的应用
                    send.AgentSecret = "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM";
                    //send.toUser = qup.ApplyID;<div>订单金额：" + ord.TotalCharge.ToString("F2") + "</div>
                    //send.toTag = "19";
                    send.toTag = houseEnt.HCYCOrderPushTagID.ToString();
                    send.content = "<div></div><div>出库仓库：" + houseEnt.Name + go + "</div><div>商城订单号：" + ent.WXOrderNo + "</div><div>出库订单号：" + ent.OrderNo + "</div><div>订单数量：" + ent.Piece.ToString() + "</div><div>订单金额：" + ent.TotalCharge.ToString("F2") + "</div><div>货物信息：" + proStr + "</div><div>付款方式：" + fkfs + "</div><div>送货方式：" + shf + "</div><div>门店名称：" + ent.AcceptUnit + "</div><div>收货信息：" + ent.AcceptPeople + " " + ent.AcceptCellphone + "</div><div>收货地址：" + ent.AcceptAddress + "</div><div>请仓管人员留意尽快出库！</div>";
                    send.url = "http://dlt.neway5.com/QY/qyScanOrderSign.aspx?OrderNo=" + ent.OrderNo;
                    WxQYSendHelper.DLTQYPushInfo(send);

                }
                catch (ApplicationException ex)
                {

                }
                weiChat.msg = "提交成功，请收到货后在我的订单里支付货款";
            }
            else if (CheckOutType.Equals("6"))
            {
                weiChat.msg = "提交成功";
            }
            else if (CheckOutType.Equals("10"))
            {
                weiChat.msg = "保存成功";
            }
        ERROR:

            //JSON
            String result = JSON.Encode(weiChat);
            context.Response.Write(result);
        }
        /// <summary>
        ///  购物车生成订单
        /// </summary>
        /// <param name="context"></param>
        private void CreateMiniProOrderList(HttpContext context)
        {
            string requestBody = string.Empty;
            // 关键：使用 InputStream 而非 Body
            using (Stream stream = context.Request.InputStream)
            {
                // 重置流位置到开头（防止被提前读取）
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    requestBody = reader.ReadToEnd();
                }
            }

            CreateOrderEntity weiChat = new CreateOrderEntity();
            weiChat.code = 0;
            weiChat.msg = "成功";

            JObject requestJson = JObject.Parse(requestBody);


            CreateOrderInfo cor = new CreateOrderInfo();
            string DToken = requestJson["token"]?.ToString();
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken)) { weiChat.code = 1; weiChat.msg = "请求Token为空"; goto ERROR; }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0)) { weiChat.code = 1; weiChat.msg = "Token有误"; goto ERROR; }
            string json = requestJson["YPOrder"]?.ToString();
            //String json = context.Request["YPOrder"];//订单商品明细 主要是产品编码ProductCode，购买数量BuyNum，购买单价BuyPrice
            if (String.IsNullOrEmpty(json)) { weiChat.code = 2; weiChat.msg = "参数有误"; goto ERROR; }
            CargoInterfaceBus interBus = new CargoInterfaceBus();
            CargoHouseBus house = new CargoHouseBus();
            CargoOrderBus orderbus = new CargoOrderBus();
            CargoWeiXinBus wbus = new CargoWeiXinBus();
            CargoPriceBus price = new CargoPriceBus();

            // 从requestJson中获取其他参数
            string houseIdStr = requestJson["HouseID"]?.ToString();
            wxUser.HouseID = string.IsNullOrEmpty(houseIdStr) ? wxUser.HouseID : Convert.ToInt32(houseIdStr);

            int PromotionType = Convert.ToInt32(requestJson["PromotionType"]?.ToString() ?? "0");//0:正价1:特价促销
            CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxUser.HouseID);

            string YPSendType = requestJson["YPSendType"]?.ToString() ?? "0";//配送方式 0：急送，1：自提2：快递
            string YPOrderType = requestJson["YPOrderType"]?.ToString() ?? "";//订单类型 22：即日达，23：次日达
            if (YPOrderType.Equals("23"))
            {
                YPSendType = "2";
            }
            DateTime StartBusHours = DateTime.Now;
            DateTime now = DateTime.Now;
            DateTime EndBusHours = DateTime.Now;
            if (!string.IsNullOrEmpty(houseEnt.StartBusHours))
            {
                string[] sbh = houseEnt.StartBusHours.Split(':');
                StartBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
            }
            if (!string.IsNullOrEmpty(houseEnt.EndBusHours))
            {
                string[] sbh = houseEnt.EndBusHours.Split(':');
                EndBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
            }
            if (wxUser.HouseID.Equals(101) || wxUser.HouseID.Equals(91) || wxUser.HouseID.Equals(106) || wxUser.HouseID.Equals(93))
            {
                //汕头,龙华，东平，南沙单独设置 
                if (YPSendType.Equals("0"))
                {
                    if (now < StartBusHours || now > EndBusHours)
                    {
                        //送货和自提，判断时间是否在早上8点30和晚上19：30之间
                        weiChat.code = 2; weiChat.msg = "很抱歉，已经过了送货时间！"; goto ERROR;
                    }
                }
            }
            else
            {
                if (YPSendType.Equals("0"))
                {
                    if (now < StartBusHours || now > EndBusHours)
                    {
                        //送货和自提，判断时间是否在早上8点30和晚上19：30之间
                        weiChat.code = 2; weiChat.msg = "很抱歉，已经过了送货时间！"; goto ERROR;
                    }
                }
                if (YPSendType.Equals("1"))
                {
                    if (now < StartBusHours || now > EndBusHours)
                    {
                        //送货和自提，判断时间是否在早上8点30和晚上19：30之间
                        weiChat.code = 2; weiChat.msg = "很抱歉，已经过了自提时间！"; goto ERROR;
                    }
                }
            }
            string CheckOutType = requestJson["CheckOutType"]?.ToString() ?? "5";//付款方式 5：现付微信支付，3：货到付款 6：额度付款  10:预收款付款

            string YPCompany = requestJson["YPCompany"]?.ToString() ?? "";//收货单位
            string YPAddress = requestJson["YPAddress"]?.ToString() ?? "";//收货地址
            string YPName = requestJson["YPName"]?.ToString() ?? "";//收货人
            string YPCellphone = requestJson["YPCellphone"]?.ToString() ?? "";//手机号码
            string YPRemark = (requestJson["YPRemark"]?.ToString() ?? "").Replace("undefined", "");//备注
            string YPOrderMoney = requestJson["YPOrderMoney"]?.ToString() ?? "";//订单总金额
            string YPLogisMoney = requestJson["YPLogisMoney"]?.ToString() ?? "";//物流费
            string YPTotalMoney = requestJson["YPTotalMoney"]?.ToString() ?? "";//总金额
            string YPTotalPiece = requestJson["YPTotalPiece"]?.ToString() ?? "";//总数量 总条数
                                                                                //string SuppClientNum = requestJson["YPSuppClientNum"]?.ToString() ?? "";//供应商编码 - 不再使用，从商品明细中获取
            string RuleID = requestJson["RuleID"]?.ToString() ?? "";//促销规则ID    第一道防线
            if (String.IsNullOrEmpty(YPOrderType)) { weiChat.code = 2; weiChat.msg = "订单类型有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPName)) { weiChat.code = 2; weiChat.msg = "购买人不能为空"; goto ERROR; }
            if (String.IsNullOrEmpty(YPCellphone)) { weiChat.code = 2; weiChat.msg = "购买手机号不能为空"; goto ERROR; }
            if (String.IsNullOrEmpty(YPTotalPiece)) { weiChat.code = 2; weiChat.msg = "总数量不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPTotalPiece) <= 0) { weiChat.code = 2; weiChat.msg = "总数量数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPOrderMoney)) { weiChat.code = 2; weiChat.msg = "订单金额不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPOrderMoney) <= 0) { weiChat.code = 2; weiChat.msg = "订单金额数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPTotalMoney)) { weiChat.code = 2; weiChat.msg = "总金额不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPTotalMoney) <= 0) { weiChat.code = 2; weiChat.msg = "总金额数据有误"; goto ERROR; }

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "新增订单";
            log.UserID = wxUser.wxOpenID;
            log.Operate = "A";

            // 解析YPOrder为ArrayList
            ArrayList rows = (ArrayList)JSON.Decode(json);

            // 按供应商分组商品
            Dictionary<int, List<Hashtable>> supplierGoodsMap = new Dictionary<int, List<Hashtable>>();
            foreach (Hashtable ht in rows)
            {
                int suppClientNum = Convert.ToInt32(ht["SuppClientNum"]);
                if (!supplierGoodsMap.ContainsKey(suppClientNum))
                {
                    supplierGoodsMap.Add(suppClientNum, new List<Hashtable>());
                }
                supplierGoodsMap[suppClientNum].Add(ht);

            }

            // 验证分组后的数据
            if (supplierGoodsMap.Count == 0) { weiChat.code = 2; weiChat.msg = "供应商数据有误"; goto ERROR; }

            // 计算总数量和总金额（用于验证）
            int totalNum = 0;
            decimal totalAmount = 0.00M;
            foreach (var supplierEntry in supplierGoodsMap)
            {
                foreach (var ht in supplierEntry.Value)
                {
                    totalNum += Convert.ToInt32(ht["BuyNum"]);
                    totalAmount += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["BuyPrice"]);
                }
            }

            // 验证总数量和总金额
            if (!Convert.ToInt32(YPTotalPiece).Equals(totalNum))
            {
                weiChat.code = 2; weiChat.msg = "订单总数量有误"; goto ERROR;
            }
            if (!Convert.ToDecimal(YPOrderMoney).Equals(totalAmount))
            {
                weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
            }

            if ((YPOrderType.Equals("22") || YPOrderType.Equals("26")) && YPSendType.Equals("0"))
            {
                //即日达
                if (YPTotalPiece.Equals("1"))
                {
                    if (!wxUser.LogisFee.Equals(0))
                    {
                        if (!Convert.ToDecimal(YPLogisMoney).Equals(Convert.ToDecimal(YPTotalPiece) * wxUser.LogisFee))
                        {
                            weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
                        }
                    }
                }
                else if (YPTotalPiece.Equals("2"))
                {
                    if (!wxUser.TwoLogisFee.Equals(0))
                    {
                        if (!Convert.ToDecimal(YPLogisMoney).Equals(wxUser.TwoLogisFee))
                        {
                            weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
                        }
                    }
                }
                else if (Convert.ToInt32(YPTotalPiece) >= 3)
                {
                    if (!wxUser.ThreeLogisFee.Equals(0))
                    {
                        if (!Convert.ToDecimal(YPLogisMoney).Equals(wxUser.ThreeLogisFee))
                        {
                            weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
                        }
                    }
                }
            }
            if ((YPOrderType.Equals("22") || YPOrderType.Equals("26")) && YPSendType.Equals("2"))
            {
                //次日达
                decimal nextdayLogis = Convert.ToInt32(YPTotalPiece) * wxUser.NextDayLogisFee;
                if (!Convert.ToDecimal(YPLogisMoney).Equals(nextdayLogis))
                {
                    weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
                }
            }
            decimal InsuranceFee = 0; string CouponType = "0";
            List<long> couponIDList = new List<long>();
            List<WXCouponEntity> wXCoupons = new List<WXCouponEntity>();
            Dictionary<int, decimal> supplierCouponMap = new Dictionary<int, decimal>(); // 存储每个供应商的优惠券合计金额
            Dictionary<long, WXCouponEntity> couponCache = new Dictionary<long, WXCouponEntity>();
            foreach (Hashtable item in rows)
            {
                // 获取商品的供应商ID
                int suppClientNum = Convert.ToInt32(item["SuppClientNum"]);

                // 获取商品使用的优惠券ID字符串，格式如"105685,105684"
                string itemCouponIDs = item["CouponID"]?.ToString() ?? "";
                if (string.IsNullOrEmpty(itemCouponIDs))
                {
                    continue; // 该商品未使用优惠券，跳过
                }

                // 分割优惠券ID字符串为数组
                string[] itemCouponIDArr = itemCouponIDs.Split(',');
                foreach (string couponIDStr in itemCouponIDArr)
                {
                    if (string.IsNullOrEmpty(couponIDStr))
                    {
                        continue;
                    }

                    long couponID = Convert.ToInt64(couponIDStr);

                    // 检查优惠券是否已缓存，避免重复查询
                    WXCouponEntity coupon;
                    if (!couponCache.TryGetValue(couponID, out coupon))
                    {
                        // 查询优惠券信息
                        coupon = wbus.QueryCouponByID(new WXCouponEntity { ID = couponID });
                        couponCache.Add(couponID, coupon);

                        // 验证优惠券有效性
                        if (!coupon.UseStatus.Equals("0")) { weiChat.code = 2; weiChat.msg = "优惠券已使用"; goto ERROR; }
                        bool isExists = !string.IsNullOrWhiteSpace(coupon.ThrowGood) && coupon.ThrowGood.Split(',')
                           .Select(s => s.Trim())
                           .Any(s => int.TryParse(s, out int val) && val == Convert.ToInt32(YPOrderType));
                        if (coupon.ThrowGood != "0" && !isExists) { weiChat.code = 2; weiChat.msg = "此订单无法使用该优惠卷"; goto ERROR; }
                        if (PromotionType != 0) { weiChat.code = 2; weiChat.msg = "此订单无法使用该优惠卷"; goto ERROR; }
                        if (coupon.EndDate < DateTime.Now) { weiChat.code = 2; weiChat.msg = "优惠券已过期"; goto ERROR; }

                        // 记录优惠券信息
                        wXCoupons.Add(coupon);
                        couponIDList.Add(couponID);
                        InsuranceFee += coupon.Money;
                        CouponType = coupon.CouponType;
                    }

                    // 将优惠券金额累加到对应供应商的合计中
                    if (supplierCouponMap.ContainsKey(suppClientNum))
                    {
                        supplierCouponMap[suppClientNum] += coupon.Money;
                    }
                    else
                    {
                        supplierCouponMap.Add(suppClientNum, coupon.Money);
                    }
                }
            }
            // 验证总金额（订单总金额=商品明细总金额+物流运费-优惠券）
            if (!Convert.ToDecimal(YPTotalMoney).Equals(Convert.ToDecimal(YPOrderMoney) + Convert.ToDecimal(YPLogisMoney) - InsuranceFee))
            {
                weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
            }

            // 预收款支付验证
            if (CheckOutType.Equals("10"))
            {
                if (Convert.ToDecimal(YPTotalMoney) > wxUser.PreReceiveMoney)
                {
                    weiChat.code = 2; weiChat.msg = "预收款金额不足：" + wxUser.PreReceiveMoney.ToString(); goto ERROR;
                }
            }

            // 生成微信商城订单号
            string orderno = GetOrderNumber();//商城订单号

            // 构建微信商城订单的商品列表
            List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
            foreach (var supplierEntry in supplierGoodsMap)
            {
                foreach (var ht in supplierEntry.Value)
                {
                    pro.Add(new CargoProductShelvesEntity
                    {
                        ProductCode = Convert.ToString(ht["ProductCode"]),//产品编码
                        BatchYear = Convert.ToInt32(ht["BatchYear"]),//商品周期批次年
                        OrderNum = Convert.ToInt32(ht["BuyNum"]),//商品购买数量
                        OrderPrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                        RuleID = Convert.ToInt32(ht["RuleID"]), // 规则ID将在供应商循环中处理
                        CutEntry = 0
                    });
                }
            }
            Common.WriteTextLog("保存微信商城订单（一笔总数据）");
            // 保存微信商城订单（一笔总数据）
            wbus.SaveWeixinOrder(new WXOrderEntity
            {
                OrderNo = orderno,
                TotalCharge = Convert.ToDecimal(YPTotalMoney),
                SuppClientNum = 0, // 总订单不指定供应商
                TransitFee = Convert.ToDecimal(YPLogisMoney),
                WXID = wxUser.ID,
                PayStatus = CheckOutType.Equals("10") ? "1" : "0",
                OrderStatus = "0",
                PayWay = CheckOutType.Equals("10") ? "3" : CheckOutType.Equals("6") ? "1" : "0",
                OrderType = "4",//小程序订单
                ThrowGood = YPOrderType,//订单类型即日达和次日达
                Piece = Convert.ToInt32(YPTotalPiece),//商城订单总数量（赠品会在仓库订单中处理）
                Address = wxUser.Address,
                Cellphone = YPCellphone,
                City = wxUser.City,
                Province = wxUser.Province,
                Country = wxUser.Country,
                Name = YPName,
                HouseID = wxUser.HouseID,
                SaleManID = wxUser.SaleManID,
                Memo = YPRemark,
                CouponID = 0,
                LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34,
                LogicName = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? "新陆程" : wxUser.HouseID.Equals(136) ? "南宁好来运" : YPSendType.Equals("1") ? "自提" : "好来运速递",
                productList = pro
            }, log);

            // 计算每个供应商的商品总金额，用于优惠券分配
            //Dictionary<int, decimal> supplierAmountMap = new Dictionary<int, decimal>();
            //foreach (var supplierEntry in supplierGoodsMap)
            //{
            //    decimal supplierAmount = 0;
            //    foreach (var ht in supplierEntry.Value)
            //    {
            //        supplierAmount += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["BuyPrice"]);
            //    }
            //    supplierAmountMap.Add(supplierEntry.Key, supplierAmount);
            //}

            // 遍历每个供应商，生成仓库出库订单
            bool isFirstSupplier = true; // 标记是否为第一笔出库单
            string firstOrderNo = ""; // 第一笔订单号，用于其他订单备注
            foreach (var supplierEntry in supplierGoodsMap)
            {
                int suppClientNum = supplierEntry.Key;
                List<Hashtable> supplierGoods = supplierEntry.Value;

                // 促销规则处理（按供应商）
                List<CargoRuleBankEntity> mruleList = new List<CargoRuleBankEntity>();
                //CargoRuleBankEntity mrule = new CargoRuleBankEntity();
                if (!string.IsNullOrEmpty(RuleID))
                {
                    // 解析RuleID，找到当前供应商对应的规则
                    string[] ruleIds = RuleID.Split(',');
                    foreach (string ruleIdStr in ruleIds)
                    {
                        if (string.IsNullOrEmpty(ruleIdStr)) continue;
                        long ruleId = Convert.ToInt64(ruleIdStr);
                        CargoRuleBankEntity tempRule = price.QueryRuleBank(ruleId);
                        if (tempRule.SuppClientNum == suppClientNum)
                        {
                            mruleList.Add(tempRule);
                            //mrule = tempRule;
                            //break;
                        }
                    }
                }

                //// 第二道防线 再次查询
                //if (mruleList.Count()==0 && PromotionType != 1)
                //{
                //    CargoRuleBankEntity entity = new CargoRuleBankEntity();
                //    // 从商品中获取TypeID（假设同一供应商的商品TypeID相同，否则需要调整）
                //    int typeId = Convert.ToInt32(supplierGoods[0]["TypeID"]);
                //    entity.TypeID = typeId;
                //    entity.HouseID = wxUser.HouseID;
                //    entity.SuppClientNum = suppClientNum;
                //    entity.StartDate = DateTime.Now;
                //    mrule = price.QueryRuleBank(entity).FirstOrDefault();
                //    if (mrule == null)
                //    {
                //        mrule = new CargoRuleBankEntity();
                //    }
                //}

                // 判断促销规则，满送 赠送的数量
                //int GiftNum = 0;
                //if (mrule != null && mrule.RuleType != null && mrule.RuleType.Equals("8"))
                //{
                //    // 计算当前供应商商品的总数量
                //    int supplierTotalPiece = supplierGoods.Sum(ht => Convert.ToInt32(ht["BuyNum"]));
                //    // 取整 买一送几的数值
                //    GiftNum = (int)Math.Truncate(Convert.ToDecimal(supplierTotalPiece / mrule.FullEntry));
                //}

                // 构建当前供应商的商品列表
                List<CargoProductShelvesEntity> supplierPro = new List<CargoProductShelvesEntity>();
                List<CargoInterfaceEntity> goodList = new List<CargoInterfaceEntity>();
                int oNum = 0;
                decimal hzj = 0.00M;
                decimal originalPrice = 0M;

                foreach (Hashtable ht in supplierGoods)
                {

                    if (string.IsNullOrEmpty(Convert.ToString(ht["BuyNum"]))) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
                    if (Convert.ToInt32(ht["BuyNum"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
                    if (string.IsNullOrEmpty(Convert.ToString(ht["BuyPrice"]))) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
                    if (Convert.ToDecimal(ht["BuyPrice"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
                    if (string.IsNullOrEmpty(Convert.ToString(ht["BatchYear"]))) { weiChat.code = 2; weiChat.msg = "商品周期有误"; goto ERROR; }
                    var rule = mruleList.FirstOrDefault(w => w.TypeID == Convert.ToInt32(ht["TypeID"]));
                    supplierPro.Add(new CargoProductShelvesEntity
                    {
                        ProductCode = Convert.ToString(ht["ProductCode"]),//产品编码
                        BatchYear = Convert.ToInt32(ht["BatchYear"]),//商品周期批次年
                        OrderNum = Convert.ToInt32(ht["BuyNum"]),//商品购买数量
                        OrderPrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                        RuleID = rule?.ID ?? 0,
                        CutEntry = rule?.CutEntry ?? 0
                    });

                    goodList.Add(new CargoInterfaceEntity
                    {
                        ProductCode = Convert.ToString(ht["ProductCode"]),
                        BatchYear = Convert.ToInt32(ht["BatchYear"]),
                        StockNum = Convert.ToInt32(ht["BuyNum"]),
                        ActSalePrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                        SpecsType = Convert.ToString(ht["SpecsType"]),
                    });

                    oNum += Convert.ToInt32(ht["BuyNum"]);
                    hzj += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["BuyPrice"]);
                }

                // 库存查询与扣减
                List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();
                List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
                decimal OverDayFee = 0.00M;//超期费用
                int pieceSum = 0;
                string proStr = string.Empty;

                // 共享仓库处理
                int shareHouseID = 0;
                string shareHouseName = "";
                foreach (var itt in goodList)
                {
                    Common.WriteTextLog("要准备开始减库存了");
                    pieceSum += itt.StockNum;
                    int piece = itt.StockNum;

                    // 查询商品基础数据
                    List<CargoProductEntity> productBasic = house.QueryALLProductData(new CargoProductEntity { ProductCode = itt.ProductCode, SuppClientNum = suppClientNum });
                    if (productBasic.Count <= 0)
                    {
                        weiChat.code = 2; weiChat.msg = "商品基础数据有误"; goto ERROR;
                    }
                    proStr = productBasic[0].TypeName + " " + productBasic[0].Specs + " " + productBasic[0].Figure + " " + productBasic[0].LoadIndex + productBasic[0].SpeedLevel;

                    // 查询库存
                    CargoInterfaceEntity queryEntity = new CargoInterfaceEntity
                    {
                        ProductCode = itt.ProductCode,
                        HouseID = wxUser.HouseID,
                        TypeID = productBasic[0].TypeID,
                        BatchYear = itt.BatchYear,
                        ParentID = productBasic[0].ParentID,
                        Regular = PromotionType,
                        SuppClientNum = suppClientNum.ToString(),
                        SpecsType = itt.SpecsType,
                    };
                    List<CargoInterfaceEntity> stockList = interBus.queryCargoStock(queryEntity);
                    if (stockList.Count <= 0)
                    {
                        weiChat.code = 1006; weiChat.msg = "商品库存不足"; goto ERROR;
                    }
                    if (stockList.Sum(c => c.StockNum) < piece)
                    {
                        weiChat.code = 1006; weiChat.msg = productBasic[0].Specs + " " + productBasic[0].Figure + "库存不足"; goto ERROR;
                    }

                    // 减库存规则，一周期早的先出先进先出，二数量和库存数刚好一样的先出
                    
                    foreach (var it in stockList)
                    {
                        if (it.StockNum <= 0) { continue; }

                        
                        if (!it.ShareHouseID.Equals(0))
                        {
                            if (YPOrderType.Equals("23"))
                            {
                                // 次日达 慧采云仓
                                shareHouseID = it.ShareHouseID;
                                shareHouseName = it.ShareHouseName;
                            }
                        }
                        
                        CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                        cargo.OrderNo = ""; // 订单号稍后设置
                        cargo.OutCargoID = ""; // 出库单号稍后设置
                        cargo.ContainerID = it.ContainerID;
                        cargo.TypeID = it.TypeID;
                        cargo.ProductID = it.ProductID;
                        cargo.ID = it.ContainerGoodsID;//库存表ID
                        cargo.HouseName = houseEnt.Name;
                        cargo.ProductName = it.ProductName;
                        cargo.Model = it.Model;
                        cargo.Specs = it.Specs;
                        cargo.Figure = it.Figure;

                        int inHouseDay = (int)(DateTime.Now - it.InHouseTime).TotalDays;
                        int OverDay = 0;
                        decimal OnlyOverDayFee = 0;

                        var rule = mruleList.FirstOrDefault(w => w.TypeID == it.TypeID);
                        #region 减库存逻辑
                        if (piece < it.StockNum)
                        {
                            if (inHouseDay > wxUser.OverDayNum)
                            {
                                // 如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
                                OverDay = inHouseDay - wxUser.OverDayNum;
                                OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * piece;
                                OverDayFee += OnlyOverDayFee;
                            }
                            // 部分出
                            entDest.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = "", // 订单号稍后设置
                                ProductID = it.ProductID,
                                HouseID = wxUser.HouseID,
                                AreaID = it.AreaID,
                                Piece = piece,
                                ActSalePrice = itt.ActSalePrice,
                                SupplySalePrice = it.InHousePrice,
                                ContainerCode = it.ContainerCode,
                                OutCargoID = "", // 出库单号稍后设置
                                RuleID = rule?.ID.ToString() ?? "0" ,
                                RuleTitle = rule?.Title ??"",
                                RuleType = rule?.RuleType ??"",
                                OP_ID = wxUser.wxOpenID,
                                OverDayNum = OverDay,
                                OverDueFee = OnlyOverDayFee,
                            });

                            cargo.Piece = piece;
                            cargo.InPiece = it.StockNum;
                            originalPrice += piece * it.InHousePrice;//计算成本价
                            outHouseList.Add(cargo);
                            break;
                        }
                        if (piece.Equals(it.StockNum))
                        {
                            if (inHouseDay > wxUser.OverDayNum)
                            {
                                // 如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
                                OverDay = inHouseDay - wxUser.OverDayNum;
                                OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * piece;
                                OverDayFee += OnlyOverDayFee;
                            }
                            // 要出库件数和第一条库存件数刚刚好，则就全部出
                            entDest.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = "", // 订单号稍后设置
                                ProductID = it.ProductID,
                                HouseID = wxUser.HouseID,
                                AreaID = it.AreaID,
                                Piece = piece,
                                ActSalePrice = itt.ActSalePrice,
                                SupplySalePrice = it.InHousePrice,
                                ContainerCode = it.ContainerCode,
                                OutCargoID = "", // 出库单号稍后设置
                                RuleID = rule?.ID.ToString() ?? "0",
                                RuleTitle = rule?.Title ?? "",
                                RuleType = rule?.RuleType ?? "",
                                OP_ID = wxUser.wxOpenID,
                                OverDayNum = OverDay,
                                OverDueFee = OnlyOverDayFee,
                            });
                            cargo.Piece = piece;
                            cargo.InPiece = it.StockNum;
                            originalPrice += piece * it.InHousePrice;//计算成本价
                            outHouseList.Add(cargo);
                            break;
                        }
                        if (piece > it.StockNum)
                        {
                            if (inHouseDay > wxUser.OverDayNum)
                            {
                                // 如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
                                OverDay = inHouseDay - wxUser.OverDayNum;
                                OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * it.StockNum;
                                OverDayFee += OnlyOverDayFee;
                            }
                            // 全部出
                            entDest.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = "", // 订单号稍后设置
                                ProductID = it.ProductID,
                                HouseID = wxUser.HouseID,
                                AreaID = it.AreaID,
                                Piece = it.StockNum,
                                ActSalePrice = itt.ActSalePrice,
                                SupplySalePrice = it.InHousePrice,
                                ContainerCode = it.ContainerCode,
                                OutCargoID = "", // 出库单号稍后设置
                                RuleID = rule?.ID.ToString() ?? "0",
                                RuleTitle = rule?.Title ?? "",
                                RuleType = rule?.RuleType ?? "",
                                OP_ID = wxUser.wxOpenID,
                                OverDayNum = OverDay,
                                OverDueFee = OnlyOverDayFee,
                            });
                            cargo.Piece = it.StockNum;
                            cargo.InPiece = it.StockNum;
                            originalPrice += it.StockNum * it.InHousePrice;//计算成本价
                            outHouseList.Add(cargo);
                            piece = piece - it.StockNum;
                            continue;
                        }
                        #endregion
                    }
                }

                // 判断是否有赠品，如果有赠品，则将赠品加入订单
                int GiftNum = 0;
                //foreach (var mrule in mruleList)
                //{
                //    if (mrule != null && mrule.RuleType != null && mrule.RuleType.Equals("8"))
                //    {
                //        // 计算当前供应商商品的总数量
                //        //int supplierTotalPiece = supplierGoods.Sum(ht => Convert.ToInt32(ht["BuyNum"]));
                //        // 取整 买一送几的数值
                //        GiftNum = (int)Math.Truncate(Convert.ToDecimal(Convert.ToInt32(ht["BuyNum"]) / mrule.FullEntry));
                //    }
                //    if (GiftNum > 0)
                //    {
                //        List<CargoContainerShowEntity> showEntities = house.QueryALLHouseData(new CargoContainerShowEntity { ProductID = mrule.ProductID });
                //        CargoContainerShowEntity showEntity = new CargoContainerShowEntity();
                //        bool hasEnoughGift = false;
                //        foreach (var item in showEntities)
                //        {
                //            if (item.Piece >= GiftNum)
                //            {
                //                showEntity = item;
                //                hasEnoughGift = true;
                //                break;
                //            }
                //        }
                //        if (!hasEnoughGift)
                //        {
                //            weiChat.code = 2; weiChat.msg = "赠品库存不足"; goto ERROR;
                //        }

                //        // 全部出
                //        entDest.Add(new CargoOrderGoodsEntity
                //        {
                //            OrderNo = "", // 订单号稍后设置
                //            ProductID = mrule.ProductID,
                //            HouseID = wxUser.HouseID,
                //            AreaID = showEntity.AreaID,
                //            Piece = GiftNum,
                //            ActSalePrice = 0,
                //            SupplySalePrice = showEntity.InHousePrice,
                //            ContainerCode = showEntity.ContainerCode,
                //            OutCargoID = "", // 出库单号稍后设置
                //            RuleID = mrule.ID.ToString(),
                //            RuleTitle = mrule.Title,
                //            RuleType = mrule.RuleType,
                //            OP_ID = wxUser.wxOpenID,
                //            OverDayNum = 0,
                //            OverDueFee = 0,
                //        });

                //        CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                //        cargo.OrderNo = ""; // 订单号稍后设置
                //        cargo.OutCargoID = ""; // 出库单号稍后设置
                //        cargo.ContainerID = showEntity.ContainerID;
                //        cargo.TypeID = showEntity.TypeID;
                //        cargo.ProductID = mrule.ProductID;
                //        cargo.ID = showEntity.ID;//库存表ID
                //        cargo.HouseName = houseEnt.Name;
                //        cargo.ProductName = showEntity.ProductName;
                //        cargo.Model = showEntity.Model;
                //        cargo.Specs = showEntity.Specs;
                //        cargo.Figure = showEntity.Figure;
                //        cargo.Piece = GiftNum;
                //        cargo.InPiece = GiftNum;
                //        outHouseList.Add(cargo);

                //        pieceSum += GiftNum;
                //    }
                //}

                // 生成当前供应商的仓库订单
                int OrderNum = 0;
                string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
                string supplierOrderNo = Common.GetMaxOrderNumByCurrentDate(wxUser.HouseID, houseEnt.HouseCode, out OrderNum); // 生成最新顺序订单号

                // 更新订单号和出库单号到商品列表
                foreach (var item in entDest)
                {
                    item.OrderNo = supplierOrderNo;
                    item.OutCargoID = outID;
                }
                foreach (var item in outHouseList)
                {
                    item.OrderNo = supplierOrderNo;
                    item.OutCargoID = outID;
                }

                // 构建仓库订单实体
                CargoOrderEntity ent = new CargoOrderEntity();
                ent.Dep = houseEnt.DepCity;
                ent.Dest = wxUser.City;
                ent.HouseID = wxUser.HouseID;
                ent.LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34;
                ent.Rebate = 0;
                ent.CheckOutType = CheckOutType;
                ent.TrafficType = "0";
                ent.DeliveryType = YPOrderType.Equals("23") ? "2" : YPSendType;
                ent.AcceptUnit = !string.IsNullOrEmpty(YPCompany) ? YPCompany : wxUser.ClientName;
                ent.AcceptAddress = !string.IsNullOrEmpty(YPAddress) ? YPAddress : wxUser.Address;
                ent.AcceptPeople = YPName;
                ent.AcceptTelephone = YPCellphone;
                ent.AcceptCellphone = YPCellphone;
                ent.CreateAwb = wxUser.Name;
                ent.CreateAwbID = wxUser.ClientNum.ToString();
                ent.CreateDate = DateTime.Now;
                ent.OP_ID = wxUser.ClientNum.ToString();
                ent.OP_Name = wxUser.Name;
                ent.SaleManID = wxUser.SaleManID;
                ent.SaleManName = wxUser.SaleManName;
                ent.SaleCellPhone = "";
                ent.Remark = YPRemark;
                ent.CouponIDList = couponIDList;
                ent.ThrowGood = YPOrderType;
                ent.BusinessID = "22";
                ent.MarketType = "2";
                ent.IsPrintPrice = 1;
                ent.TranHouse = "";
                ent.PostponeShip = "1";
                ent.ClientNum = wxUser.ClientNum;
                ent.PayClientNum = wxUser.ClientNum;
                ent.PayClientName = wxUser.ClientName;
                ent.ClientID = wxUser.ClientID;
                ent.OrderNo = supplierOrderNo;
                ent.OutHouseName = houseEnt.Name;
                //共享仓库
                ent.ShareHouseID = shareHouseID;
                ent.ShareHouseName = shareHouseName;


                // 订单总数量
                ent.Piece = oNum + GiftNum;
                ent.Weight = 0;
                ent.Volume = 0;

                // 1. 优惠券计算：根据当前供应商商品金额占总金额比例分配优惠券
                decimal supplierInsuranceFee = 0;
                if (supplierCouponMap.ContainsKey(suppClientNum))
                {
                    supplierInsuranceFee = supplierCouponMap[suppClientNum];
                }
                ent.InsuranceFee = supplierInsuranceFee; // 优惠券金额

                // 2. 运费处理：第一笔订单承担全部运费，其他订单运费为0
                if (isFirstSupplier)
                {
                    ent.TransitFee = Convert.ToDecimal(YPLogisMoney); // 第一笔订单承担全部运费
                    firstOrderNo = supplierOrderNo; // 记录第一笔订单号
                }

                    if (!isFirstSupplier&& YPLogisMoney!="0") {
                        ent.TransitFee = 0; // 其他订单运费为0
                                            // 在备注中添加说明：运费计算在第一笔订单上
                        if (!string.IsNullOrEmpty(ent.Remark))
                        {
                            ent.Remark += "；";
                        }
                        ent.Remark += $"运费计算在订单{firstOrderNo}上";
                    }

                ent.TransportFee = originalPrice;
                ent.OverDueFee = OverDayFee;//超期费用
                ent.DeliveryFee = 0;
                ent.OtherFee = Convert.ToDecimal(hzj) - ent.TransitFee - ent.TransportFee + ent.InsuranceFee;//平台服服务费=总金额-配送费-销售金额+优惠券
                ent.OtherFee = (Convert.ToDecimal(YPTotalMoney) - ent.TransitFee - ent.TransportFee + ent.InsuranceFee) * (hzj / Convert.ToDecimal(YPOrderMoney)); // 按比例分配其他费用
                ent.TotalCharge = hzj + ent.TransitFee - ent.InsuranceFee; // 供应商订单总金额

                ent.CouponType = CouponType;
                ent.AwbStatus = "0";
                ent.OrderType = "4";
                ent.OrderNum = OrderNum;//最新订单顺序号
                ent.goodsList = entDest;
                ent.FinanceSecondCheck = "0";
                ent.OrderModel = "0";
                ent.SuppClientNum = suppClientNum;
                ent.WXOrderNo = orderno;//微信商城订单号

                if (!ent.Piece.Equals(pieceSum))
                {
                    weiChat.code = 2; weiChat.msg = "购买数量不一致"; goto ERROR;
                }
                if (CheckOutType.Equals("10"))
                {
                    ent.FinanceSecondCheck = "1";
                    ent.FinanceSecondCheckName = wxUser.Name;
                    ent.FinanceSecondCheckDate = DateTime.Now;
                }
                Common.WriteTextLog("生成出库单");
                // 保存生成仓库出库订单
                orderbus.AddOrderInfo(ent, outHouseList, log);

                // 仓库同步缓存
                //如果是次日达，并且库存是共享仓库库存，写入缓存
                if (!ent.ShareHouseID.Equals(0) && YPOrderType.Equals("23"))
                {
                    //goodList
                    RedisHelper.HashSet("NextDayOrderShareSync", ent.OrderNo + "_" + ent.HouseID.ToString() + "_" + ent.ShareHouseID.ToString(), JSON.Encode(goodList));

                }

                foreach (CargoContainerShowEntity time in outHouseList)
                {
                    CargoProductEntity syncProduct = house.SyncTypeProduct(time.ProductID.ToString());
                    //34 马牌  1 同步马牌  2 同步全部品牌
                    if (syncProduct.SyncType == "2" || (syncProduct.SyncType == "1" && syncProduct.TypeID == 34))
                    {
                        RedisHelper.HashSet("OpenSystemStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.GoodsCode);
                    }

                    //主仓缓存更改
                    if (house.IsAddCaching(syncProduct.HouseID, time.TypeID))
                    {
                        RedisHelper.HashSet("HCYCHouseStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.ProductCode);
                    }
                }

                // 标记已处理完第一笔订单
                isFirstSupplier = false;
            }

            // 支付处理
            if (!CheckOutType.Equals("6") && !CheckOutType.Equals("10"))
            {
                Common.WriteTextLog("进入通联调试阶段");
                try
                {
                    SybWxPayService sybService = new SybWxPayService();
                    Dictionary<String, String> rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(YPTotalMoney) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/UnionPaySuccess.aspx", "");
                    Dictionary<String, String> payinfoDic = payinfo(rsp);
                    string jsonString = String.Join(",", rsp.Select(kvp => kvp.Key + "=" + kvp.Value));
                    Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：" + jsonString);

                    TenPayV3Info tenPayV3 = new TenPayV3Info(appid, appsecret, Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());

                    weiChat.msg = "{";
                    weiChat.msg += " \"appId\": \"" + tenPayV3.AppId + "\",";
                    weiChat.msg += " \"partnerId\": \"" + tenPayV3.MchId + "\",";
                    weiChat.msg += " \"prepayId\": \"" + payinfoDic["package"] + "\",";
                    weiChat.msg += " \"packageValue\": \"" + string.Format("prepay_id={0}", payinfoDic["package"]) + "\",";
                    weiChat.msg += " \"timeStamp\": \"" + payinfoDic["timeStamp"] + "\",";
                    weiChat.msg += " \"nonceStr\": \"" + payinfoDic["nonceStr"] + "\",";
                    weiChat.msg += " \"sign\": \"" + payinfoDic["paySign"] + "\",";
                    weiChat.msg += " \"orderNo\": \"" + orderno + "\"";
                    weiChat.msg += "}";
                }
                catch (Exception ex)
                {
                    Common.WriteTextLog("慧采云仓小程序 通联支付回调失败信息：" + ex.Message);
                    weiChat.code = 2; weiChat.msg = "订单推送失败"; goto ERROR;
                }
            }

            cor.orderno = orderno;
            weiChat.data = cor;

            // 订单通知等后续处理...
            if (CheckOutType.Equals("3"))
            {
                // 推送好来运系统等逻辑...
                weiChat.msg = "提交成功，请收到货后在我的订单里支付货款";
            }
            else if (CheckOutType.Equals("6"))
            {
                weiChat.msg = "提交成功";
            }
            else if (CheckOutType.Equals("10"))
            {
                weiChat.msg = "保存成功";
            }

        ERROR:
            //JSON
            String result = JSON.Encode(weiChat);
            context.Response.Write(result);
        }
        //private void CreateMiniProOrderList(HttpContext context)
        //{
        //    //String returnString = string.Empty;

        //    string requestBody = string.Empty;
        //    // 关键：使用 InputStream 而非 Body
        //    using (Stream stream = context.Request.InputStream)
        //    {
        //        // 重置流位置到开头（防止被提前读取）
        //        stream.Position = 0;
        //        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        //        {
        //            requestBody = reader.ReadToEnd();
        //        }
        //    }

        //    CreateOrderEntity weiChat = new CreateOrderEntity();
        //    weiChat.code = 0;
        //    weiChat.msg = "成功";

        //    JObject requestJson = JObject.Parse(requestBody);


        //    CreateOrderInfo cor = new CreateOrderInfo();
        //    string DToken = Convert.ToString(context.Request["token"]);
        //    //WriteTextLog("绑定" + DToken);
        //    if (string.IsNullOrEmpty(DToken)) { weiChat.code = 1; weiChat.msg = "请求Token为空"; goto ERROR; }
        //    WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
        //    if (wxUser.ID.Equals(0)) { weiChat.code = 1; weiChat.msg = "Token有误"; goto ERROR; }
        //    string json = requestJson["YPOrder"]?.ToString();
        //    //String json = context.Request["YPOrder"];//订单商品明细 主要是产品编码ProductCode，购买数量BuyNum，购买单价BuyPrice
        //    if (String.IsNullOrEmpty(json)) { weiChat.code = 2; weiChat.msg = "参数有误"; goto ERROR; }
        //    CargoInterfaceBus interBus = new CargoInterfaceBus();
        //    CargoHouseBus house = new CargoHouseBus();
        //    CargoOrderBus orderbus = new CargoOrderBus();
        //    CargoWeiXinBus wbus = new CargoWeiXinBus();
        //    CargoPriceBus price = new CargoPriceBus();
        //    wxUser.HouseID = string.IsNullOrEmpty(Convert.ToString(requestJson["HouseID"])) ? wxUser.HouseID : Convert.ToInt32(requestJson["HouseID"]?.ToString());
        //    int PromotionType = Convert.ToInt32(requestJson["PromotionType"]?.ToString());//0:正价1:特价促销
        //    //仓库信息
        //    CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxUser.HouseID);
        //    //送货方式
        //    string YPSendType = string.IsNullOrEmpty(requestJson["YPSendType"]?.ToString()) ? "0" : Convert.ToString(requestJson["YPSendType"]?.ToString());//配送方式 0：急送，1：自提2：快递
        //    if (Convert.ToString(requestJson["YPOrderType"]).Equals("23"))
        //    {
        //        YPSendType = "2";
        //    }
        //    //判断仓库营业时间
        //    DateTime StartBusHours = DateTime.Now;
        //    DateTime now = DateTime.Now;
        //    DateTime EndBusHours = DateTime.Now;
        //    if (!string.IsNullOrEmpty(houseEnt.StartBusHours))
        //    {
        //        string[] sbh = houseEnt.StartBusHours.Split(':');
        //        StartBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
        //    }
        //    if (!string.IsNullOrEmpty(houseEnt.EndBusHours))
        //    {
        //        string[] sbh = houseEnt.EndBusHours.Split(':');
        //        EndBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
        //    }
        //    if (wxUser.HouseID.Equals(101) || wxUser.HouseID.Equals(91) || wxUser.HouseID.Equals(106) || wxUser.HouseID.Equals(93))
        //    {
        //        //汕头,龙华，东平，南沙单独设置 
        //        if (YPSendType.Equals("0"))
        //        {
        //            if (now < StartBusHours || now > EndBusHours)
        //            {
        //                //送货和自提，判断时间是否在早上8点30和晚上19：30之间
        //                weiChat.code = 2; weiChat.msg = "很抱歉，已经过了送货时间！"; goto ERROR;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (YPSendType.Equals("0"))
        //        {
        //            if (now < StartBusHours || now > EndBusHours)
        //            {
        //                //送货和自提，判断时间是否在早上8点30和晚上19：30之间
        //                weiChat.code = 2; weiChat.msg = "很抱歉，已经过了送货时间！"; goto ERROR;
        //            }
        //        }
        //        if (YPSendType.Equals("1"))
        //        {
        //            if (now < StartBusHours || now > EndBusHours)
        //            {
        //                //送货和自提，判断时间是否在早上8点30和晚上19：30之间
        //                weiChat.code = 2; weiChat.msg = "很抱歉，已经过了自提时间！"; goto ERROR;
        //            }
        //        }
        //    }
        //    string CheckOutType = string.IsNullOrEmpty(Convert.ToString(requestJson["CheckOutType"])) ? "5" : Convert.ToString(requestJson["CheckOutType"]);//付款方式 5：现付微信支付，3：货到付款 6：额度付款  10:预收款付款
        //    string YPOrderType = Convert.ToString(requestJson["YPOrderType"]);//订单类型 22：即日达，23：次日达
        //    string YPCompany = Convert.ToString(requestJson["YPCompany"]);//收货单位
        //    string YPAddress = Convert.ToString(requestJson["YPAddress"]);//收货地址
        //    string YPName = Convert.ToString(requestJson["YPName"]);//收货人
        //    string YPCellphone = Convert.ToString(requestJson["YPCellphone"]);//手机号码
        //    string YPRemark = Convert.ToString(requestJson["YPRemark"]).Replace("undefined", "");//备注
        //    string YPOrderMoney = Convert.ToString(requestJson["YPOrderMoney"]);//订单总金额
        //    string YPLogisMoney = Convert.ToString(requestJson["YPLogisMoney"]);//物流费
        //    string YPTotalMoney = Convert.ToString(requestJson["YPTotalMoney"]);//总金额
        //    string YPTotalPiece = Convert.ToString(requestJson["YPTotalPiece"]);//总数量 总条数
        //    string SuppClientNum = Convert.ToString(requestJson["YPSuppClientNum"]);//供应商编码
        //    string RuleID = Convert.ToString(requestJson["RuleID"]);//促销规则ID    第一道防线
        //    if (String.IsNullOrEmpty(YPOrderType)) { weiChat.code = 2; weiChat.msg = "订单类型有误"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPName)) { weiChat.code = 2; weiChat.msg = "购买人不能为空"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPCellphone)) { weiChat.code = 2; weiChat.msg = "购买手机号不能为空"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPTotalPiece)) { weiChat.code = 2; weiChat.msg = "总数量不能为空"; goto ERROR; }
        //    if (Convert.ToDecimal(YPTotalPiece) <= 0) { weiChat.code = 2; weiChat.msg = "总数量数据有误"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPOrderMoney)) { weiChat.code = 2; weiChat.msg = "订单金额不能为空"; goto ERROR; }
        //    if (Convert.ToDecimal(YPOrderMoney) <= 0) { weiChat.code = 2; weiChat.msg = "订单金额数据有误"; goto ERROR; }
        //    if (String.IsNullOrEmpty(YPTotalMoney)) { weiChat.code = 2; weiChat.msg = "总金额不能为空"; goto ERROR; }
        //    if (Convert.ToDecimal(YPTotalMoney) <= 0) { weiChat.code = 2; weiChat.msg = "总金额数据有误"; goto ERROR; }
        //    //if (String.IsNullOrEmpty(SuppClientNum)) { weiChat.code = 2; weiChat.msg = "供应商不能为空"; goto ERROR; }
        //    //if (Convert.ToInt32(SuppClientNum).Equals(0)) { weiChat.code = 2; weiChat.msg = "供应商数据有误"; goto ERROR; }
        //    if (YPOrderType.Equals("22") && YPSendType.Equals("0"))
        //    {
        //        //即日达
        //        if (YPTotalPiece.Equals("1"))
        //        {
        //            if (!wxUser.LogisFee.Equals(0))
        //            {
        //                if (!Convert.ToDecimal(YPLogisMoney).Equals(Convert.ToDecimal(YPTotalPiece) * wxUser.LogisFee))
        //                {
        //                    weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
        //                }
        //            }
        //        }
        //        else if (YPTotalPiece.Equals("2"))
        //        {
        //            if (!wxUser.TwoLogisFee.Equals(0))
        //            {
        //                if (!Convert.ToDecimal(YPLogisMoney).Equals(wxUser.TwoLogisFee))
        //                {
        //                    weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
        //                }
        //            }
        //        }
        //        else if (Convert.ToInt32(YPTotalPiece) >= 3)
        //        {
        //            if (!wxUser.ThreeLogisFee.Equals(0))
        //            {
        //                if (!Convert.ToDecimal(YPLogisMoney).Equals(wxUser.ThreeLogisFee))
        //                {
        //                    weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
        //                }
        //            }
        //        }
        //    }
        //    if (YPOrderType.Equals("22") && YPSendType.Equals("2"))
        //    {
        //        //次日达
        //        decimal nextdayLogis = Convert.ToInt32(YPTotalPiece) * wxUser.NextDayLogisFee;
        //        if (!Convert.ToDecimal(YPLogisMoney).Equals(nextdayLogis))
        //        {
        //            weiChat.code = 2; weiChat.msg = "物流费用有误"; goto ERROR;
        //        }
        //    }



        //    ArrayList rows = (ArrayList)JSON.Decode(json);
        //    var groupedBySupplier = rows
        //       .Cast<dynamic>() // 将 ArrayList 元素转换为 dynamic
        //       .GroupBy(item => item["SuppClientNum"]) // 按供应商编号分组
        //       .ToDictionary(g => g.Key, g => g.ToList()); // 转换为字典，键为供应商编号，值为商品列表

        //    // 遍历每个供应商   生成出库单
        //    foreach (var supplierGroup in groupedBySupplier)
        //    {
        //        int supplierNum = Convert.ToInt32(supplierGroup.Key); // 供应商编号
        //        List<dynamic> supplierProducts = supplierGroup.Value; // 该供应商的商品列表


        //        //促销规则
        //        CargoRuleBankEntity mrule = new CargoRuleBankEntity();
        //        if (!string.IsNullOrEmpty(RuleID))
        //        {
        //            mrule = price.QueryRuleBank(Convert.ToInt64(RuleID));
        //        }
        //        //第二道防线  再次查询
        //        //20250825  特价不查询优化信息
        //        if (mrule.ID == 0 && PromotionType != 1)
        //        {
        //            CargoRuleBankEntity entity = new CargoRuleBankEntity();
        //            entity.TypeID = Convert.ToInt32(context.Request["TypeID"]);
        //            entity.HouseID = wxUser.HouseID;
        //            entity.SuppClientNum = Convert.ToInt32(SuppClientNum);
        //            entity.StartDate = DateTime.Now;
        //            mrule = price.QueryRuleBank(entity).FirstOrDefault();
        //            if (mrule == null)
        //            {
        //                mrule = new CargoRuleBankEntity();
        //            }
        //        }
        //        //判断促销规则，满送  赠送的数量
        //        int GiftNum = 0;
        //        if (mrule != null && mrule.RuleType != null && mrule.RuleType.Equals("8"))
        //        {
        //            //取整 买一送几的数值
        //            GiftNum = (int)Math.Truncate(Convert.ToDecimal(Convert.ToInt32(YPTotalPiece) / mrule.FullEntry));

        //        }
        //        //优惠卷

        //        //物流费用


        //        // 遍历该供应商的每个商品
        //        foreach (dynamic product in supplierProducts)
        //        {
        //            string productCode = product.ProductCode; // 产品编码
        //            int buyNum = product.BuyNum; // 购买数量
        //            decimal buyPrice = product.BuyPrice; // 购买单价


        //        }
        //    }
        //    //string CouponID = Convert.ToString(context.Request["CouponID"]).Equals("0") ? "" : Convert.ToString(context.Request["CouponID"]).ToLower().Equals("undefined") ? "" : Convert.ToString(context.Request["CouponID"]);
        //    ////string RuleID = context.Request["RuleID"];
        //    ////促销规则
        //    //CargoRuleBankEntity mrule = new CargoRuleBankEntity();
        //    //if (!string.IsNullOrEmpty(RuleID))
        //    //{
        //    //    mrule = price.QueryRuleBank(Convert.ToInt64(RuleID));
        //    //}
        //    ////第二道防线  再次查询
        //    ////20250825  特价不查询优化信息
        //    //if (mrule.ID == 0 && PromotionType != 1)
        //    //{
        //    //    CargoRuleBankEntity entity = new CargoRuleBankEntity();
        //    //    entity.TypeID = Convert.ToInt32(context.Request["TypeID"]);
        //    //    entity.HouseID = wxUser.HouseID;
        //    //    entity.SuppClientNum = Convert.ToInt32(SuppClientNum);
        //    //    entity.StartDate = DateTime.Now;
        //    //    mrule = price.QueryRuleBank(entity).FirstOrDefault();
        //    //    if (mrule == null)
        //    //    {
        //    //        mrule = new CargoRuleBankEntity();
        //    //    }
        //    //}
        //    ////判断促销规则，满送  赠送的数量
        //    //int GiftNum = 0;
        //    //if (mrule != null && mrule.RuleType != null && mrule.RuleType.Equals("8"))
        //    //{
        //    //    //取整 买一送几的数值
        //    //    GiftNum = (int)Math.Truncate(Convert.ToDecimal(Convert.ToInt32(YPTotalPiece) / mrule.FullEntry));

        //    //}
        //    ////优惠券金额变量 优惠券类型，平台券或商家券
        //    //decimal InsuranceFee = 0; string CouponType = "0";
        //    //List<long> couponIDList = new List<long>();
        //    //List<WXCouponEntity> wXCoupons = new List<WXCouponEntity>();
        //    ////20240930 传多张优惠券叠加使用，优惠券ID用逗号隔开，如：1,2,3，拆分成数组再循环处理
        //    //if (!string.IsNullOrEmpty(CouponID))
        //    //{
        //    //    string[] couponArr = CouponID.Split(',');
        //    //    foreach (string couponID in couponArr)
        //    //    {
        //    //        WXCouponEntity coupon = wbus.QueryCouponByID(new WXCouponEntity { ID = Convert.ToInt64(couponID) });
        //    //        wXCoupons.Add(coupon);
        //    //        if (!coupon.UseStatus.Equals("0"))
        //    //        { weiChat.code = 2; weiChat.msg = "优惠券已使用"; goto ERROR; }
        //    //        bool isExists = !string.IsNullOrWhiteSpace(coupon.ThrowGood) && coupon.ThrowGood.Split(',')
        //    //           .Select(s => s.Trim())
        //    //           .Any(s => int.TryParse(s, out int val) && val == Convert.ToInt32(YPOrderType));
        //    //        if (coupon.ThrowGood != "0" && !isExists)
        //    //        {
        //    //            weiChat.code = 2; weiChat.msg = "此订单无法使用该优惠卷"; goto ERROR;
        //    //        }
        //    //        if (PromotionType != 0)
        //    //        {
        //    //            weiChat.code = 2; weiChat.msg = "此订单无法使用该优惠卷"; goto ERROR;
        //    //        }
        //    //        if (coupon.EndDate < DateTime.Now)
        //    //        {
        //    //            weiChat.code = 2; weiChat.msg = "优惠券已过期"; goto ERROR;
        //    //        }
        //    //        InsuranceFee += coupon.Money;
        //    //        CouponType = coupon.CouponType;
        //    //        couponIDList.Add(coupon.ID);
        //    //    }
        //    //    //int a = wXCoupons.Select(c => c.IsFollowQuantity.Equals("1")).Count();
        //    //    if (wXCoupons.Where(c => c.IsFollowQuantity.Equals("1")).Count() > Convert.ToInt32(YPTotalPiece))
        //    //    {
        //    //        weiChat.code = 2; weiChat.msg = "优惠券使用数量有误"; goto ERROR;
        //    //    }
        //    //}



        //    //List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
        //    //List<CargoInterfaceEntity> goodList = new List<CargoInterfaceEntity>();
        //    //int oNum = 0;
        //    //decimal hzj = 0.00M;
        //    //decimal originalPrice = 0M;
        //    //foreach (Hashtable ht in rows)
        //    //{

        //    //    if (string.IsNullOrEmpty(Convert.ToString(ht["BuyNum"]))) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
        //    //    if (Convert.ToInt32(ht["BuyNum"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
        //    //    if (string.IsNullOrEmpty(Convert.ToString(ht["BuyPrice"]))) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
        //    //    if (Convert.ToDecimal(ht["BuyPrice"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
        //    //    if (string.IsNullOrEmpty(Convert.ToString(ht["BatchYear"]))) { weiChat.code = 2; weiChat.msg = "商品周期有误"; goto ERROR; }
        //    //    pro.Add(new CargoProductShelvesEntity
        //    //    {
        //    //        ProductCode = Convert.ToString(ht["ProductCode"]),//产品编码
        //    //        BatchYear = Convert.ToInt32(ht["BatchYear"]),//商品周期批次年
        //    //        OrderNum = Convert.ToInt32(ht["BuyNum"]),//商品购买数量
        //    //        OrderPrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
        //    //        RuleID = mrule.ID,
        //    //        CutEntry = mrule.CutEntry
        //    //    });
        //    //    goodList.Add(new CargoInterfaceEntity
        //    //    {
        //    //        ProductCode = Convert.ToString(ht["ProductCode"]),
        //    //        BatchYear = Convert.ToInt32(ht["BatchYear"]),
        //    //        StockNum = Convert.ToInt32(ht["BuyNum"]),
        //    //        ActSalePrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
        //    //        SpecsType = YPOrderType.Equals("23") ? "5" : "4",
        //    //    });
        //    //    //originalPrice += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["originalPrice"]);
        //    //    oNum += Convert.ToInt32(ht["BuyNum"]);
        //    //    hzj += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["BuyPrice"]);
        //    //}
        //    //if (!Convert.ToInt32(YPTotalPiece).Equals(oNum))
        //    //{
        //    //    weiChat.code = 2; weiChat.msg = "订单总数量有误"; goto ERROR;
        //    //}

        //    //if (!Convert.ToDecimal(YPOrderMoney).Equals(hzj))
        //    //{
        //    //    weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
        //    //}
        //    ////订单总金额=商品明细总金额+物流运费-优惠券
        //    //if (!Convert.ToDecimal(YPTotalMoney).Equals(Convert.ToDecimal(YPOrderMoney) + Convert.ToDecimal(YPLogisMoney) - InsuranceFee))
        //    //{
        //    //    weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
        //    //}
        //    //if (CheckOutType.Equals("10"))
        //    //{
        //    //    //预收款支付，判断下预收款金额是否足够
        //    //    if (Convert.ToDecimal(YPTotalMoney) > wxUser.PreReceiveMoney)
        //    //    {
        //    //        weiChat.code = 2; weiChat.msg = "预收款金额不足：" + wxUser.PreReceiveMoney.ToString(); goto ERROR;
        //    //    }
        //    //}
        //    ////汕头仓玲珑2155517一个门店限购两条
        //    ////if (wxUser.HouseID.Equals(101) && pro[0].ProductCode.Equals("LTLL215551701") && mrule.ID > 0)
        //    ////{
        //    ////    //特价限购2条
        //    ////    if (pro[0].OrderNum > 2)
        //    ////    {
        //    ////        weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
        //    ////    }

        //    ////    int OrderPiece = wbus.QueryYunSpecialTyreNum(new WXOrderEntity { ProductCode = pro[0].ProductCode, HouseID = wxUser.HouseID, ClientNum = wxUser.ClientNum, PayStatus = "1" });
        //    ////    if (OrderPiece >= 2)
        //    ////    {
        //    ////        weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
        //    ////    }
        //    ////    if (pro[0].OrderNum + OrderPiece > 2)
        //    ////    {
        //    ////        weiChat.code = 2; weiChat.msg = "特价规格限购2条"; goto ERROR;
        //    ////    }
        //    ////}


        //    //decimal wxZJ = Convert.ToDecimal(YPTotalMoney) * 100;
        //    //string orderno = GetOrderNumber();//商城订单号
        //    //decimal OverDayFee = 0.00M;//超期费用


        //    //LogEntity log = new LogEntity();
        //    //log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
        //    //log.Moudle = "慧采云仓";
        //    //log.Status = "0";
        //    //log.NvgPage = "新增订单";
        //    //log.UserID = wxUser.wxOpenID;
        //    //log.Operate = "A";
        //    ////同步修改收货人信息
        //    ////wbus.UpdateWeixinUser(new List<WXUserEntity> { new WXUserEntity { AcceptCellphone = YPCellphone, AcceptName = YPName, wxOpenID = wxUser.wxOpenID, Sex = wxUser.Sex } });

        //    //CargoOrderEntity ent = new CargoOrderEntity();
        //    //List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
        //    ////保存生成仓库订单
        //    //List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();

        //    //ent.Dep = houseEnt.DepCity;
        //    //ent.Dest = wxUser.City;
        //    //int OrderNum = 0;
        //    //ent.HouseID = wxUser.HouseID;
        //    //ent.LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34;
        //    //ent.Rebate = 0;
        //    //ent.CheckOutType = CheckOutType;// "5";//Convert.ToString(row["CheckOutType"]);
        //    //                                //ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(row["ReturnAwb"])) ? 0 : Convert.ToInt32(row["ReturnAwb"]);
        //    //ent.TrafficType = "0";// Convert.ToString(row["TrafficType"]);
        //    //ent.DeliveryType = YPOrderType.Equals("23") ? "2" : Convert.ToString(context.Request["YPSendType"]);//Convert.ToString(row["DeliveryType"]);
        //    //ent.AcceptUnit = !string.IsNullOrEmpty(YPCompany) ? YPCompany : wxUser.ClientName;//Convert.ToString(row["AcceptUnit"]);取公司名称
        //    //ent.AcceptAddress = !string.IsNullOrEmpty(YPAddress) ? YPAddress : wxUser.Address;// Convert.ToString(row["AcceptAddress"]);取注册时填写的公司地址
        //    //ent.AcceptPeople = YPName;//Convert.ToString(row["AcceptPeople"]);
        //    //ent.AcceptTelephone = YPCellphone;//Convert.ToString(row["AcceptTelephone"]);
        //    //ent.AcceptCellphone = YPCellphone;//Convert.ToString(row["AcceptCellphone"]);
        //    //ent.CreateAwb = wxUser.Name;//开单人生成订单人取当前微信人
        //    //ent.CreateAwbID = wxUser.ClientNum.ToString();//开单人ID取
        //    //ent.CreateDate = DateTime.Now;
        //    //ent.OP_ID = wxUser.ClientNum.ToString();
        //    //ent.OP_Name = wxUser.Name;
        //    //ent.SaleManID = wxUser.SaleManID;
        //    //ent.SaleManName = wxUser.SaleManName;
        //    //ent.SaleCellPhone = "";
        //    //ent.Remark = YPRemark;
        //    ////ent.CouponID = string.IsNullOrEmpty(CouponID) ? 0 : Convert.ToInt64(CouponID);
        //    //ent.CouponIDList = couponIDList;
        //    ////ent.ThrowGood = "0";
        //    //ent.ThrowGood = YPOrderType;
        //    //ent.BusinessID = "22";
        //    //ent.MarketType = "2";
        //    //ent.IsPrintPrice = 1;
        //    //ent.TranHouse = "";
        //    //ent.PostponeShip = "1";
        //    //ent.ClientNum = wxUser.ClientNum;
        //    //ent.PayClientNum = wxUser.ClientNum;
        //    //ent.PayClientName = wxUser.ClientName;//付款人客户姓名
        //    //ent.ClientID = wxUser.ClientID;
        //    //string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
        //    //ent.OrderNo = Common.GetMaxOrderNumByCurrentDate(wxUser.HouseID, houseEnt.HouseCode, out OrderNum); // Convert.ToString(row["OrderNo"]);//生成最新顺序订单号
        //    //ent.OutHouseName = houseEnt.Name;
        //    //int pieceSum = 0;
        //    //string proStr = string.Empty;





        //    //foreach (var itt in goodList)
        //    //{
        //    //    pieceSum += itt.StockNum;
        //    //    int piece = itt.StockNum;
        //    //    List<CargoProductEntity> productBasic = house.QueryALLProductData(new CargoProductEntity { ProductCode = itt.ProductCode, SuppClientNum = Convert.ToInt64(SuppClientNum) });
        //    //    if (productBasic.Count <= 0)
        //    //    {
        //    //        weiChat.code = 2; weiChat.msg = "商品基础数据有误"; goto ERROR;
        //    //    }
        //    //    proStr = productBasic[0].TypeName + " " + productBasic[0].Specs + " " + productBasic[0].Figure + " " + productBasic[0].LoadIndex + productBasic[0].SpeedLevel;
        //    //    CargoInterfaceEntity queryEntity = new CargoInterfaceEntity
        //    //    {
        //    //        ProductCode = itt.ProductCode,
        //    //        HouseID = wxUser.HouseID,
        //    //        TypeID = productBasic[0].TypeID,
        //    //        BatchYear = itt.BatchYear,
        //    //        ParentID = productBasic[0].ParentID,
        //    //        Regular = PromotionType,
        //    //        SuppClientNum = itt.SuppClientNum,
        //    //        SpecsType = YPOrderType.Equals("22") ? "4" : "5",
        //    //    };
        //    //    List<CargoInterfaceEntity> stockList = interBus.queryCargoStock(queryEntity);
        //    //    if (stockList.Count <= 0)
        //    //    {
        //    //        weiChat.code = 1006; weiChat.msg = "商品库存不足"; goto ERROR;
        //    //    }
        //    //    if (stockList.Sum(c => c.StockNum) < piece)
        //    //    {
        //    //        weiChat.code = 1006; weiChat.msg = productBasic[0].Specs + " " + productBasic[0].Figure + "库存不足"; goto ERROR;
        //    //    }
        //    //    //减库存规则，一周期早的先出先进先出，二数量和库存数刚好一样的先出
        //    //    foreach (var it in stockList)
        //    //    {
        //    //        if (it.StockNum <= 0) { continue; }
        //    //        if (!it.ShareHouseID.Equals(0))
        //    //        {
        //    //            if (YPOrderType.Equals("23"))
        //    //            {
        //    //                //次日达 慧采云仓
        //    //                ent.OpenOrderSource = "1";
        //    //            }
        //    //            ent.ShareHouseID = it.ShareHouseID;
        //    //            ent.ShareHouseName = it.ShareHouseName;
        //    //        }

        //    //        CargoContainerShowEntity cargo = new CargoContainerShowEntity();
        //    //        cargo.OrderNo = ent.OrderNo;//订单号
        //    //        cargo.OutCargoID = outID;
        //    //        cargo.ContainerID = it.ContainerID;
        //    //        cargo.TypeID = it.TypeID;
        //    //        cargo.ProductID = it.ProductID;

        //    //        cargo.ID = it.ContainerGoodsID;//库存表ID

        //    //        //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
        //    //        cargo.HouseName = houseEnt.Name;
        //    //        //cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
        //    //        cargo.ProductName = it.ProductName;
        //    //        cargo.Model = it.Model;
        //    //        cargo.Specs = it.Specs;
        //    //        cargo.Figure = it.Figure;
        //    //        int inHouseDay = (int)(DateTime.Now - it.InHouseTime).TotalDays;
        //    //        int OverDay = 0;
        //    //        decimal OnlyOverDayFee = 0;

        //    //        #region 减库存逻辑
        //    //        if (piece < it.StockNum)
        //    //        {
        //    //            if (inHouseDay > wxUser.OverDayNum)
        //    //            {
        //    //                //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
        //    //                OverDay = inHouseDay - wxUser.OverDayNum;
        //    //                OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * piece;
        //    //                OverDayFee += OnlyOverDayFee;
        //    //            }
        //    //            //部分出
        //    //            entDest.Add(new CargoOrderGoodsEntity
        //    //            {
        //    //                OrderNo = ent.OrderNo,
        //    //                ProductID = it.ProductID,
        //    //                HouseID = ent.HouseID,
        //    //                AreaID = it.AreaID,
        //    //                Piece = piece,
        //    //                //ActSalePrice = it.SalePrice,
        //    //                ActSalePrice = itt.ActSalePrice,
        //    //                SupplySalePrice = it.InHousePrice,
        //    //                ContainerCode = it.ContainerCode,
        //    //                OutCargoID = outID,
        //    //                RuleID = mrule.ID.ToString(),
        //    //                RuleTitle = mrule.Title,
        //    //                RuleType = mrule.RuleType,
        //    //                OP_ID = log.UserID,
        //    //                OverDayNum = OverDay,
        //    //                OverDueFee = OnlyOverDayFee,
        //    //            });

        //    //            cargo.Piece = piece;
        //    //            cargo.InPiece = it.StockNum;
        //    //            originalPrice += piece * it.InHousePrice;//计算成本价
        //    //            outHouseList.Add(cargo);
        //    //            break;
        //    //        }
        //    //        if (piece.Equals(it.StockNum))
        //    //        {
        //    //            if (inHouseDay > wxUser.OverDayNum)
        //    //            {
        //    //                //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
        //    //                OverDay = inHouseDay - wxUser.OverDayNum;
        //    //                OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * piece;
        //    //                OverDayFee += OnlyOverDayFee;
        //    //            }
        //    //            //要出库件数和第一条库存件数刚刚好，则就全部出
        //    //            entDest.Add(new CargoOrderGoodsEntity
        //    //            {
        //    //                OrderNo = ent.OrderNo,
        //    //                ProductID = it.ProductID,
        //    //                HouseID = ent.HouseID,
        //    //                AreaID = it.AreaID,
        //    //                Piece = piece,
        //    //                //ActSalePrice = it.SalePrice,
        //    //                SupplySalePrice = it.InHousePrice,
        //    //                ActSalePrice = itt.ActSalePrice,
        //    //                ContainerCode = it.ContainerCode,
        //    //                OutCargoID = outID,
        //    //                RuleID = mrule.ID.ToString(),
        //    //                RuleTitle = mrule.Title,
        //    //                RuleType = mrule.RuleType,
        //    //                OP_ID = log.UserID,
        //    //                OverDayNum = OverDay,
        //    //                OverDueFee = OnlyOverDayFee,
        //    //            });
        //    //            cargo.Piece = piece;
        //    //            cargo.InPiece = it.StockNum;
        //    //            originalPrice += piece * it.InHousePrice;//计算成本价

        //    //            outHouseList.Add(cargo);
        //    //            break;
        //    //        }
        //    //        if (piece > it.StockNum)
        //    //        {
        //    //            if (inHouseDay > wxUser.OverDayNum)
        //    //            {
        //    //                //如果在库天数大于了用户设置的天数，则按照超期天数计算超期费用
        //    //                OverDay = inHouseDay - wxUser.OverDayNum;
        //    //                OnlyOverDayFee = OverDay * wxUser.OverDueUnitPrice * it.StockNum;
        //    //                OverDayFee += OnlyOverDayFee;
        //    //            }
        //    //            //全部出
        //    //            entDest.Add(new CargoOrderGoodsEntity
        //    //            {
        //    //                OrderNo = ent.OrderNo,
        //    //                ProductID = it.ProductID,
        //    //                HouseID = ent.HouseID,
        //    //                AreaID = it.AreaID,
        //    //                Piece = it.StockNum,
        //    //                //ActSalePrice = it.SalePrice,
        //    //                SupplySalePrice = it.InHousePrice,
        //    //                ActSalePrice = itt.ActSalePrice,
        //    //                ContainerCode = it.ContainerCode,
        //    //                OutCargoID = outID,
        //    //                RuleID = mrule.ID.ToString(),
        //    //                RuleTitle = mrule.Title,
        //    //                RuleType = mrule.RuleType,
        //    //                OP_ID = log.UserID,
        //    //                OverDayNum = OverDay,
        //    //                OverDueFee = OnlyOverDayFee,
        //    //            });
        //    //            cargo.Piece = it.StockNum;
        //    //            cargo.InPiece = it.StockNum;
        //    //            originalPrice += it.StockNum * it.InHousePrice;//计算成本价
        //    //            outHouseList.Add(cargo);
        //    //            piece = piece - it.StockNum;
        //    //            continue;
        //    //        }
        //    //        #endregion
        //    //    }
        //    //}
        //    ////判断是否有赠品，如果有赠品，则将赠品加入订单
        //    //if (GiftNum > 0)
        //    //{
        //    //    List<CargoContainerShowEntity> showEntities = house.QueryALLHouseData(new CargoContainerShowEntity { ProductID = mrule.ProductID });
        //    //    CargoContainerShowEntity showEntity = new CargoContainerShowEntity();
        //    //    foreach (var item in showEntities)
        //    //    {
        //    //        if (item.Piece < GiftNum)
        //    //        {
        //    //            weiChat.code = 2; weiChat.msg = "赠品库存不足"; goto ERROR;
        //    //        }
        //    //        showEntity = item; break;
        //    //    }
        //    //    //全部出
        //    //    entDest.Add(new CargoOrderGoodsEntity
        //    //    {
        //    //        OrderNo = ent.OrderNo,
        //    //        ProductID = mrule.ProductID,
        //    //        HouseID = ent.HouseID,
        //    //        AreaID = showEntity.AreaID,
        //    //        Piece = GiftNum,
        //    //        //ActSalePrice = it.SalePrice,
        //    //        SupplySalePrice = showEntity.InHousePrice,
        //    //        ActSalePrice = 0,
        //    //        ContainerCode = showEntity.ContainerCode,
        //    //        OutCargoID = outID,
        //    //        RuleID = mrule.ID.ToString(),
        //    //        RuleTitle = mrule.Title,
        //    //        RuleType = mrule.RuleType,
        //    //        OP_ID = log.UserID,
        //    //        OverDayNum = 0,
        //    //        OverDueFee = 0,
        //    //    });

        //    //    CargoContainerShowEntity cargo = new CargoContainerShowEntity();
        //    //    cargo.OrderNo = ent.OrderNo;//订单号
        //    //    cargo.OutCargoID = outID;
        //    //    cargo.ContainerID = showEntity.ContainerID;
        //    //    cargo.TypeID = showEntity.TypeID;
        //    //    cargo.ProductID = mrule.ProductID;

        //    //    cargo.ID = showEntity.ID;//库存表ID

        //    //    //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
        //    //    cargo.HouseName = houseEnt.Name;
        //    //    //cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
        //    //    cargo.ProductName = showEntity.ProductName;
        //    //    cargo.Model = showEntity.Model;
        //    //    cargo.Specs = showEntity.Specs;
        //    //    cargo.Figure = showEntity.Figure;

        //    //    cargo.Piece = GiftNum;
        //    //    cargo.InPiece = GiftNum;
        //    //    outHouseList.Add(cargo);

        //    //    pieceSum += GiftNum;
        //    //}

        //    ////订单总数量
        //    //ent.Piece = Convert.ToInt32(YPTotalPiece) + GiftNum;
        //    //ent.Weight = 0;
        //    //ent.Volume = 0;
        //    //ent.InsuranceFee = InsuranceFee;// coupon.Money;//优惠券金额 
        //    //ent.TransitFee = Convert.ToDecimal(YPLogisMoney);//即日达20*数量  即日达的运输费用
        //    //ent.TransportFee = originalPrice;
        //    //ent.OverDueFee = OverDayFee;//超期费用
        //    ////ent.TransportFee = Convert.ToDecimal(YPOrderMoney);//订单的费用
        //    //ent.DeliveryFee = 0;
        //    //ent.OtherFee = Convert.ToDecimal(YPTotalMoney) - ent.TransitFee - ent.TransportFee + ent.InsuranceFee;//平台服服务费=总金额-配送费-销售金额+优惠券
        //    //ent.TotalCharge = Convert.ToDecimal(YPTotalMoney);

        //    //ent.CouponType = CouponType;
        //    //ent.AwbStatus = "0";
        //    //ent.OrderType = "4";
        //    //ent.OrderNum = OrderNum;//最新订单顺序号
        //    ////ent.FinanceSecondCheck = "1";
        //    ////ent.FinanceSecondCheckName = wxUser.Name;
        //    ////ent.FinanceSecondCheckDate = DateTime.Now;
        //    //ent.goodsList = entDest;
        //    //ent.FinanceSecondCheck = "0";
        //    //ent.OrderModel = "0";
        //    //ent.SuppClientNum = Convert.ToInt32(SuppClientNum);
        //    //ent.WXOrderNo = orderno;//微信商城订单号
        //    //if (!ent.Piece.Equals(pieceSum))
        //    //{
        //    //    weiChat.code = 2; weiChat.msg = "购买数量不一致"; goto ERROR;
        //    //}
        //    //if (CheckOutType.Equals("10"))
        //    //{
        //    //    ent.FinanceSecondCheck = "1";
        //    //    ent.FinanceSecondCheckName = wxUser.Name;
        //    //    ent.FinanceSecondCheckDate = DateTime.Now;
        //    //}
        //    ////保存生成商城订单
        //    //wbus.SaveWeixinOrder(new WXOrderEntity
        //    //{
        //    //    OrderNo = orderno,
        //    //    TotalCharge = Convert.ToDecimal(YPTotalMoney),
        //    //    SuppClientNum = Convert.ToInt32(SuppClientNum),
        //    //    TransitFee = Convert.ToDecimal(YPLogisMoney),
        //    //    WXID = wxUser.ID,
        //    //    PayStatus = CheckOutType.Equals("10") ? "1" : CheckOutType.Equals("10") ? "1" : "0",
        //    //    OrderStatus = "0",
        //    //    PayWay = CheckOutType.Equals("10") ? "3" : CheckOutType.Equals("6") ? "1" : "0",
        //    //    OrderType = "4",//小程序订单
        //    //    ThrowGood = YPOrderType,//订单类型即日达和次日达
        //    //    Piece = Convert.ToInt32(YPTotalPiece) + GiftNum,//商城订单总数量
        //    //    Address = wxUser.Address,
        //    //    Cellphone = YPCellphone,
        //    //    City = wxUser.City,
        //    //    Province = wxUser.Province,
        //    //    Country = wxUser.Country,
        //    //    Name = YPName,//wxUser.Name,
        //    //    HouseID = wxUser.HouseID,
        //    //    SaleManID = wxUser.SaleManID,
        //    //    Memo = YPRemark,
        //    //    CouponID = 0,//!string.IsNullOrEmpty(CouponID) ? Convert.ToInt64(CouponID) : 0,//优惠券ID
        //    //    LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34,
        //    //    LogicName = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? "新陆程" : wxUser.HouseID.Equals(136) ? "南宁好来运" : YPSendType.Equals("1") ? "自提" : "好来运速递",
        //    //    productList = pro
        //    //}, log);

        //    ////保存生成仓库出库订单
        //    //orderbus.AddOrderInfo(ent, outHouseList, log);

        //    ////支付成功，向客户发放优惠券根据订单明细返回的优惠规则ID，查询该规则的促销优惠内容，并向客户发放优惠券
        //    ////如果是发放优惠券的规则，则向客户发放优惠券
        //    ////CargoClientBus clientBus = new CargoClientBus();
        //    ////if (mrule != null && mrule.IssueCoupon == 1)
        //    ////{
        //    ////    int couponNum = Convert.ToInt32(YPTotalPiece) / mrule.FullEntry;
        //    ////    for (int i = 0; i < couponNum; i++)
        //    ////    {
        //    ////        clientBus.AddCoupon(new WXCouponEntity { WXID = wxUser.ID, Piece = 1, Money = mrule.CutEntry, UseStatus = "0", GainDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(mrule.ServiceTime), TypeID = mrule.UseTypeID,TypeName=mrule.UseTypeName, CouponType = mrule.CouponType.ToString(), SuppClientNum = mrule.SuppClientNum, IsSuperPosition = mrule.IsSuperPosition.ToString(), FromOrderNO = ent.WXOrderNo }, log);
        //    ////    }
        //    ////}

        //    //if (!CheckOutType.Equals("6") && !CheckOutType.Equals("10"))
        //    //{
        //    //    try
        //    //    {
        //    //        SybWxPayService sybService = new SybWxPayService();
        //    //        Dictionary<String, String> rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(YPTotalMoney) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/UnionPaySuccess.aspx", "");
        //    //        Dictionary<String, String> payinfoDic = payinfo(rsp);
        //    //        string jsonString = String.Join(",", rsp.Select(kvp => kvp.Key + "=" + kvp.Value));
        //    //        Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：" + jsonString);

        //    //        TenPayV3Info tenPayV3 = new TenPayV3Info(appid, appsecret, Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());

        //    //        weiChat.msg = "{";
        //    //        weiChat.msg += " \"appId\": \"" + tenPayV3.AppId + "\",";
        //    //        weiChat.msg += " \"partnerId\": \"" + tenPayV3.MchId + "\",";
        //    //        weiChat.msg += " \"prepayId\": \"" + payinfoDic["package"] + "\",";
        //    //        weiChat.msg += " \"packageValue\": \"" + string.Format("prepay_id={0}", payinfoDic["package"]) + "\",";
        //    //        weiChat.msg += " \"timeStamp\": \"" + payinfoDic["timeStamp"] + "\",";
        //    //        weiChat.msg += " \"nonceStr\": \"" + payinfoDic["nonceStr"] + "\",";
        //    //        weiChat.msg += " \"sign\": \"" + payinfoDic["paySign"] + "\",";
        //    //        weiChat.msg += " \"orderNo\": \"" + orderno + "\"";
        //    //        weiChat.msg += "}";
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        Common.WriteTextLog("慧采云仓小程序 通联支付回调失败信息：" + ex.Message);
        //    //        //ex.Message;
        //    //        weiChat.code = 2; weiChat.msg = "订单推送失败"; goto ERROR;

        //    //    }
        //    //}

        //    //cor.orderno = orderno;
        //    //weiChat.data = cor;

        //    ////如果是次日达，并且库存是共享仓库库存，写入缓存
        //    //if (!ent.ShareHouseID.Equals(0) && YPOrderType.Equals("23"))
        //    //{
        //    //    //goodList
        //    //    RedisHelper.HashSet("NextDayOrderShareSync", ent.OrderNo + "_" + ent.HouseID.ToString() + "_" + ent.ShareHouseID.ToString(), JSON.Encode(goodList));

        //    //}

        //    ////仓库同步缓存
        //    //foreach (CargoContainerShowEntity time in outHouseList)
        //    //{
        //    //    CargoProductEntity syncProduct = house.SyncTypeProduct(time.ProductID.ToString());
        //    //    //34 马牌  1 同步马牌  2 同步全部品牌
        //    //    if (syncProduct.SyncType == "2" || (syncProduct.SyncType == "1" && syncProduct.TypeID == 34))
        //    //    {
        //    //        RedisHelper.HashSet("OpenSystemStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.GoodsCode);
        //    //    }

        //    //    //主仓缓存更改
        //    //    if (house.IsAddCaching(syncProduct.HouseID, time.TypeID))
        //    //    {
        //    //        RedisHelper.HashSet("HCYCHouseStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.ProductCode);
        //    //    }
        //    //}

        //    //if (CheckOutType.Equals("3"))
        //    //{
        //    //    #region 推送好来运系统

        //    //    if (ent.HouseID.Equals(93))
        //    //    {
        //    //        //内部订单 || ent.HouseID.Equals(98)
        //    //        orderbus.SaveHlyOrderData(outHouseList, ent);
        //    //    }
        //    //    else
        //    //    {
        //    //        orderbus.InsertCargoOrderPush(new CargoOrderPushEntity
        //    //        {
        //    //            OrderNo = ent.OrderNo,
        //    //            Dep = ent.Dep,
        //    //            Dest = ent.Dest,
        //    //            Piece = ent.Piece,
        //    //            TransportFee = ent.TransportFee,
        //    //            ClientNum = ent.ClientNum.ToString(),
        //    //            AcceptAddress = ent.AcceptAddress,
        //    //            AcceptCellphone = ent.AcceptCellphone,
        //    //            AcceptTelephone = ent.AcceptTelephone,
        //    //            AcceptPeople = ent.AcceptPeople,
        //    //            AcceptUnit = ent.AcceptUnit,
        //    //            HouseID = ent.HouseID.ToString(),
        //    //            HouseName = ent.OutHouseName,
        //    //            OP_ID = wxUser.Name,
        //    //            PushType = "0",
        //    //            PushStatus = "0",
        //    //            LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34
        //    //        }, log);

        //    //    }

        //    //    #endregion

        //    //    string fkfs = ent.CheckOutType.Equals("3") ? "货到付款" : ent.CheckOutType.Equals("10") ? "预付款" : "现付";
        //    //    string shf = ent.LogisID.Equals(46) ? "自提" : YPSendType.Equals("0") ? "急送" : "次日达";
        //    //    string tit = ent.ThrowGood.Equals("22") ? YPSendType.Equals("2") ? "次日达" : "急速达" : "次日达";
        //    //    string go = ent.ThrowGood.Equals("22") ? (YPSendType.Equals("2") ? "" : "") : "-不出库";
        //    //    CargoNewOrderNoticeEntity cargoNewOrder = new CargoNewOrderNoticeEntity();
        //    //    cargoNewOrder.HouseName = houseEnt.Name;
        //    //    cargoNewOrder.OrderNo = ent.WXOrderNo;
        //    //    cargoNewOrder.OrderNum = ent.Piece.ToString();
        //    //    cargoNewOrder.ClientInfo = ent.AcceptPeople + " " + ent.AcceptCellphone + " " + ent.AcceptAddress;// "泰乐 华笙 广东省广州市白云区东平加油站左侧";
        //    //    cargoNewOrder.ProductInfo = proStr;// "马牌 215/55R16 CC6 98V";
        //    //    cargoNewOrder.DeliveryName = shf;// "自提";
        //    //    cargoNewOrder.ReceivePeople = "";
        //    //    string hcno = JSON.Encode(cargoNewOrder);
        //    //    //Common.WriteTextLog(hcno);

        //    //    //如果是次日达，并且库存不是共享仓库库存，要推送给供应商
        //    //    if (ent.ShareHouseID.Equals(0) && ent.ThrowGood.Equals("23"))
        //    //    {
        //    //        string Name = houseEnt.Name + " " + proStr;
        //    //        // 若长度小于等于20，直接返回原字符串；否则截取前20个
        //    //        Name = Name.Length <= 20 ? Name : Name.Substring(0, 20);
        //    //        //供应商名称  物流单号   订单号    商品名称   金额   数量
        //    //        Common.SendRePlaceAnOrderMsg(ent.SuppClientNum.ToString(), ent.LogisAwbNo, ent.OrderNo, Name, ent.TotalCharge, ent.Piece);
        //    //    }
        //    //    else
        //    //    {
        //    //        //企业微信推送
        //    //        List<CargoVoiceBroadEntity> voiceBroadList = house.GetVoiceBroadList(new CargoVoiceBroadEntity { HouseID = houseEnt.HouseID });
        //    //        foreach (var voice in voiceBroadList)
        //    //        {
        //    //            RedisHelper.SetString("NewOrderNotice_" + voice.LoginName, hcno);
        //    //            //mc.Add("NewOrderNotice_" + voice.LoginName, hcno);
        //    //        }
        //    //    }
        //    //    //mc.Add("NewOrderNotice_1000", hcno);
        //    //    //mc.Add("NewOrderNotice_2215", hcno);
        //    //    //mc.Add("NewOrderNotice_2856", hcno);

        //    //    //RedisHelper.SetString("NewOrderNotice", hcno);
        //    //    //货到付款
        //    //    try
        //    //    {
        //    //        QySendInfoEntity send = new QySendInfoEntity();
        //    //        send.title = tit + " 有新订单";
        //    //        //推送给提交人
        //    //        send.msgType = msgType.textcard;
        //    //        send.agentID = "1000003";//消息通知的应用
        //    //        send.AgentSecret = "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM";
        //    //        //send.toUser = qup.ApplyID;<div>订单金额：" + ord.TotalCharge.ToString("F2") + "</div>
        //    //        //send.toTag = "19";
        //    //        send.toTag = houseEnt.HCYCOrderPushTagID.ToString();
        //    //        send.content = "<div></div><div>出库仓库：" + houseEnt.Name + go + "</div><div>商城订单号：" + ent.WXOrderNo + "</div><div>出库订单号：" + ent.OrderNo + "</div><div>订单数量：" + ent.Piece.ToString() + "</div><div>订单金额：" + ent.TotalCharge.ToString("F2") + "</div><div>货物信息：" + proStr + "</div><div>付款方式：" + fkfs + "</div><div>送货方式：" + shf + "</div><div>门店名称：" + ent.AcceptUnit + "</div><div>收货信息：" + ent.AcceptPeople + " " + ent.AcceptCellphone + "</div><div>收货地址：" + ent.AcceptAddress + "</div><div>请仓管人员留意尽快出库！</div>";
        //    //        send.url = "http://dlt.neway5.com/QY/qyScanOrderSign.aspx?OrderNo=" + ent.OrderNo;
        //    //        WxQYSendHelper.DLTQYPushInfo(send);

        //    //    }
        //    //    catch (ApplicationException ex)
        //    //    {

        //    //    }
        //    //    weiChat.msg = "提交成功，请收到货后在我的订单里支付货款";
        //    //}
        //    //else if (CheckOutType.Equals("6"))
        //    //{
        //    //    weiChat.msg = "提交成功";
        //    //}
        //    //else if (CheckOutType.Equals("10"))
        //    //{
        //    //    weiChat.msg = "保存成功";
        //    //}
        //ERROR:

        //    //JSON
        //    String result = JSON.Encode(weiChat);
        //    context.Response.Write(result);
        //}

        /// <summary>
        /// 通联支付预订单提交订单
        /// </summary>
        /// <param name="context"></param>
        private void CreateMiniAdvanceOrder(HttpContext context)
        {
            CreateOrderEntity weiChat = new CreateOrderEntity();
            weiChat.code = 0;
            weiChat.msg = "成功";

            CreateOrderInfo cor = new CreateOrderInfo();
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken)) { weiChat.code = 1; weiChat.msg = "请求Token为空"; goto ERROR; }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0)) { weiChat.code = 1; weiChat.msg = "Token有误"; goto ERROR; }
            String json = context.Request["YPOrder"];//订单商品明细 主要是产品编码ProductCode，购买数量BuyNum，购买单价BuyPrice
            if (String.IsNullOrEmpty(json)) { weiChat.code = 2; weiChat.msg = "参数有误"; goto ERROR; }
            CargoInterfaceBus interBus = new CargoInterfaceBus();
            CargoHouseBus house = new CargoHouseBus();
            CargoOrderBus orderbus = new CargoOrderBus();
            CargoWeiXinBus wbus = new CargoWeiXinBus();
            CargoPriceBus price = new CargoPriceBus();
            CargoProductBus productBus = new CargoProductBus();
            wxUser.HouseID = string.IsNullOrEmpty(Convert.ToString(context.Request["HouseID"])) || Convert.ToString(context.Request["HouseID"]) == "undefined" ? wxUser.HouseID : Convert.ToInt32(context.Request["HouseID"]);
            int PromotionType = Convert.ToInt32(context.Request["PromotionType"]);//0:正价1:特价促销2：预订单
            CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxUser.HouseID);
            string YPSendType = string.IsNullOrEmpty(Convert.ToString(context.Request["YPSendType"])) ? "0" : Convert.ToString(context.Request["YPSendType"]);//配送方式 0：急送，1：自提2：快递
            if (Convert.ToString(context.Request["YPOrderType"]).Equals("23"))
            {
                YPSendType = "2";
            }
            DateTime StartBusHours = DateTime.Now;
            DateTime now = DateTime.Now;
            DateTime EndBusHours = DateTime.Now;
            if (!string.IsNullOrEmpty(houseEnt.StartBusHours))
            {
                string[] sbh = houseEnt.StartBusHours.Split(':');
                StartBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
            }
            if (!string.IsNullOrEmpty(houseEnt.EndBusHours))
            {
                string[] sbh = houseEnt.EndBusHours.Split(':');
                EndBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
            }
            string CheckOutType = string.IsNullOrEmpty(Convert.ToString(context.Request["CheckOutType"])) ? "5" : Convert.ToString(context.Request["CheckOutType"]);//付款方式 5：现付微信支付，3：货到付款 6：额度付款  10:预收款付款
            string YPOrderType = Convert.ToString(context.Request["YPOrderType"]);//订单类型 22：即日达，23：次日达
            string YPCompany = Convert.ToString(context.Request["YPCompany"]);//收货单位
            string YPAddress = Convert.ToString(context.Request["YPAddress"]);//收货地址
            string YPName = Convert.ToString(context.Request["YPName"]);//收货人
            string YPCellphone = Convert.ToString(context.Request["YPCellphone"]);//手机号码
            string YPRemark = Convert.ToString(context.Request["YPRemark"]).Replace("undefined", "");//备注
            string YPOrderMoney = Convert.ToString(context.Request["YPOrderMoney"]);//订单总金额
            string actualAmounts = Convert.ToString(context.Request["YPOrderMoney"]);//订单总金额
            string YPLogisMoney = Convert.ToString(context.Request["YPLogisMoney"]);//物流费
            string YPTotalMoney = Convert.ToString(context.Request["YPTotalMoney"]);//总金额
            string YPTotalPiece = Convert.ToString(context.Request["YPTotalPiece"]);//总数量 总条数
            decimal YPProportion = Convert.ToDecimal(context.Request["Proportion"]);//总数量 总条数
            YPTotalMoney = actualAmounts;
            //string SuppClientNum = Convert.ToString(context.Request["YPSuppClientNum"]);//供应商编码
            string SuppClientNum = "551098";//供应商编码 供应商默认狄乐汽服OE
            string RuleID = Convert.ToString(context.Request["RuleID"]);//促销规则ID    第一道防线
            string PaymentType = Convert.ToString(context.Request["PaymentType"]);// (1:定金, 2:尾款 3:全款) 
            int TypeID = Convert.ToInt32(context.Request["TypeID"]);//品牌
            if (String.IsNullOrEmpty(YPOrderType)) { weiChat.code = 2; weiChat.msg = "订单类型有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPName)) { weiChat.code = 2; weiChat.msg = "购买人不能为空"; goto ERROR; }
            if (String.IsNullOrEmpty(YPCellphone)) { weiChat.code = 2; weiChat.msg = "购买手机号不能为空"; goto ERROR; }
            if (String.IsNullOrEmpty(YPTotalPiece)) { weiChat.code = 2; weiChat.msg = "总数量不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPTotalPiece) <= 0) { weiChat.code = 2; weiChat.msg = "总数量数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPOrderMoney)) { weiChat.code = 2; weiChat.msg = "订单金额不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPOrderMoney) <= 0) { weiChat.code = 2; weiChat.msg = "订单金额数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPTotalMoney)) { weiChat.code = 2; weiChat.msg = "总金额不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPTotalMoney) <= 0) { weiChat.code = 2; weiChat.msg = "总金额数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(SuppClientNum)) { weiChat.code = 2; weiChat.msg = "供应商不能为空"; goto ERROR; }
            if (Convert.ToInt32(SuppClientNum).Equals(0)) { weiChat.code = 2; weiChat.msg = "供应商数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(PaymentType)) { weiChat.code = 2; weiChat.msg = "支付类型不能为空"; goto ERROR; }

            //string CouponID = Convert.ToString(context.Request["CouponID"]).Equals("0") ? "" : Convert.ToString(context.Request["CouponID"]).ToLower().Equals("undefined") ? "" : Convert.ToString(context.Request["CouponID"]);
            //string RuleID = context.Request["RuleID"];
            //第二道防线  再次查询
            //优惠券金额变量 优惠券类型，平台券或商家券
            decimal InsuranceFee = 0; string CouponType = "0";
            List<long> couponIDList = new List<long>();

            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
            List<CargoInterfaceEntity> goodList = new List<CargoInterfaceEntity>();
            int oNum = 0;
            decimal hzj = 0.00M;
            decimal originalPrice = 0M;
            foreach (Hashtable ht in rows)
            {
                if (string.IsNullOrEmpty(Convert.ToString(ht["BuyNum"]))) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
                if (Convert.ToInt32(ht["BuyNum"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
                if (string.IsNullOrEmpty(Convert.ToString(ht["BuyPrice"]))) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
                if (Convert.ToDecimal(ht["BuyPrice"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
                if (string.IsNullOrEmpty(Convert.ToString(ht["BatchYear"]))) { weiChat.code = 2; weiChat.msg = "商品周期有误"; goto ERROR; }

                var pData = productBus.GetProductSpecEntity(new CargoProductSpecEntity { TypeID = TypeID, ProductCode = Convert.ToString(ht["ProductCode"]) });

                pro.Add(new CargoProductShelvesEntity
                {
                    ProductCode = Convert.ToString(ht["ProductCode"]),//产品编码
                    BatchYear = Convert.ToInt32(ht["BatchYear"]),//商品周期批次年
                    OrderNum = Convert.ToInt32(ht["BuyNum"]),//商品购买数量
                    OrderPrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                    RuleID = 0,
                    CutEntry = 0
                });
                goodList.Add(new CargoInterfaceEntity
                {
                    TypeID = TypeID,
                    ProductCode = pData.ProductCode,
                    GoodsCode = pData.GoodsCode,
                    ProductName = pData.ProductName,
                    Specs = pData.Specs,
                    Model = pData.Model,
                    Figure = pData.Figure,
                    StockNum = Convert.ToInt32(ht["BuyNum"]),
                    ActSalePrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                    SpecsType = YPOrderType.Equals("23") ? "5" : "4",
                });
                //originalPrice += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["originalPrice"]);
                oNum += Convert.ToInt32(ht["BuyNum"]);
                hzj += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["BuyPrice"]);
            }

            //if (YPProportion > 0)
            //{
            //    hzj = hzj - (hzj * YPProportion);
            //    YPOrderMoney = (Convert.ToDecimal(YPOrderMoney) - (Convert.ToDecimal(YPOrderMoney) * YPProportion)).ToString();
            //}
            if (!Convert.ToInt32(YPTotalPiece).Equals(oNum))
            {
                weiChat.code = 2; weiChat.msg = "订单总数量有误"; goto ERROR;
            }

            if (!Convert.ToDecimal(YPOrderMoney).Equals(hzj))
            {
                weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
            }
            //订单总金额=商品明细总金额+物流运费-优惠券
            if (!Convert.ToDecimal(YPTotalMoney).Equals(Convert.ToDecimal(YPOrderMoney) - InsuranceFee))
            {
                weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
            }
            //预收款支付，判断下预收款金额是否足够
            if (CheckOutType.Equals("10"))
            {
                //预收款支付，判断下预收款金额是否足够
                if (Convert.ToDecimal(YPTotalMoney) > wxUser.PreReceiveMoney)
                {
                    weiChat.code = 2; weiChat.msg = "预收款金额不足：" + wxUser.PreReceiveMoney.ToString(); goto ERROR;
                }
            }

            decimal wxZJ = Convert.ToDecimal(YPTotalMoney) * 100;
            string orderno = GetOrderNumber();//商城订单号
            decimal OverDayFee = 0.00M;//超期费用

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "新增订单";
            log.UserID = wxUser.wxOpenID;
            log.Operate = "A";

            //CargoOrderEntity ent = new CargoOrderEntity();
            CargoReserveOrderEntity ent = new CargoReserveOrderEntity();
            //订金/全款 生成订单数据
            if (PaymentType == "1" || PaymentType == "3")
            {
                List<CargoReserveOrderGoodsEntity> entDest = new List<CargoReserveOrderGoodsEntity>();
                //保存生成仓库订单
                List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();

                ent.Dep = houseEnt.DepCity;
                ent.Dest = wxUser.City;
                int OrderNum = 0;
                ent.HouseID = wxUser.HouseID;
                //var AreaDara = house.QueryAreaByHouseID(new CargoAreaEntity { HouseID = ent.HouseID });
                var AreaDara = house.QueryALLArea(new CargoAreaEntity { HouseID = ent.HouseID, ParentID = 0 }).FirstOrDefault();
                ent.LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34;
                ent.Rebate = 0;
                ent.CheckOutType = CheckOutType;// "5";//Convert.ToString(row["CheckOutType"]);
                                                //ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(row["ReturnAwb"])) ? 0 : Convert.ToInt32(row["ReturnAwb"]);
                ent.TrafficType = "0";// Convert.ToString(row["TrafficType"]);
                ent.DeliveryType = "2";//Convert.ToString(row["DeliveryType"]);
                                       //ent.CheckStatus = "1";//Convert.ToString(row["DeliveryType"]);
                ent.AcceptUnit = !string.IsNullOrEmpty(YPCompany) ? YPCompany : wxUser.ClientName;//Convert.ToString(row["AcceptUnit"]);取公司名称
                ent.AcceptAddress = !string.IsNullOrEmpty(YPAddress) ? YPAddress : wxUser.Address;// Convert.ToString(row["AcceptAddress"]);取注册时填写的公司地址
                ent.AcceptPeople = YPName;//Convert.ToString(row["AcceptPeople"]);
                ent.AcceptTelephone = YPCellphone;//Convert.ToString(row["AcceptTelephone"]);
                ent.AcceptCellphone = YPCellphone;//Convert.ToString(row["AcceptCellphone"]);
                ent.CreateAwb = wxUser.Name;//开单人生成订单人取当前微信人
                ent.CreateAwbID = wxUser.ClientNum.ToString();//开单人ID取
                ent.CreateDate = DateTime.Now;
                ent.OP_ID = wxUser.ClientNum.ToString();
                ent.OP_Name = wxUser.Name;
                ent.SaleManID = wxUser.SaleManID;
                ent.SaleManName = wxUser.SaleManName;
                ent.SaleCellPhone = "";
                ent.Remark = YPRemark;
                //ent.CouponID = string.IsNullOrEmpty(CouponID) ? 0 : Convert.ToInt64(CouponID);
                ent.CouponIDList = couponIDList;
                //ent.ThrowGood = "0";
                ent.ThrowGood = YPOrderType == "0" ? "23" : YPOrderType;
                ent.BusinessID = "22";
                ent.MarketType = "2";
                ent.IsPrintPrice = 1;
                ent.TranHouse = "";
                ent.PostponeShip = "1";
                ent.ClientNum = wxUser.ClientNum;
                ent.PayClientNum = wxUser.ClientNum;
                ent.PayClientName = wxUser.ClientName;//付款人客户姓名
                ent.ClientID = wxUser.ClientID;
                //string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
                ent.OrderNo = Common.GetMaxReserveOrderNumByCurrentDate(wxUser.HouseID, houseEnt.HouseCode, out OrderNum); // Convert.ToString(row["OrderNo"]);//生成最新顺序订单号
                ent.OutHouseName = houseEnt.Name;
                int pieceSum = 0;
                string proStr = string.Empty;
                foreach (var itt in goodList)
                {
                    pieceSum += itt.StockNum;
                    int piece = itt.StockNum;
                    List<CargoProductEntity> productBasic = house.QueryALLProductData(new CargoProductEntity { ProductCode = itt.ProductCode, SuppClientNum = Convert.ToInt64(SuppClientNum) });
                    if (productBasic.Count <= 0)
                    {
                        weiChat.code = 2; weiChat.msg = "商品基础数据有误"; goto ERROR;
                    }
                    proStr = productBasic[0].TypeName + " " + productBasic[0].Specs + " " + productBasic[0].Figure + " " + productBasic[0].LoadIndex + productBasic[0].SpeedLevel;

                    //部分出
                    entDest.Add(new CargoReserveOrderGoodsEntity
                    {
                        OrderNo = ent.OrderNo,
                        //ProductID = itt.ProductID,
                        ProductCode = itt.ProductCode,
                        ProductName = itt.ProductName,
                        GoodsCode = itt.GoodsCode,
                        Specs = itt.Specs,
                        Figure = itt.Figure,
                        TypeID = itt.TypeID,
                        Model = itt.Model,
                        HouseID = ent.HouseID,
                        AreaID = AreaDara == null ? 0 : AreaDara.AreaID,
                        Piece = piece,
                        //ActSalePrice = it.SalePrice,
                        ActSalePrice = itt.ActSalePrice,
                        SupplySalePrice = itt.InHousePrice,
                        //ContainerCode = itt.ContainerCode,
                        //OutCargoID = outID,
                        OP_ID = log.UserID,
                        OverDayNum = 0,
                        OverDueFee = 0,
                    });

                }

                //订单总数量
                ent.Piece = Convert.ToInt32(YPTotalPiece);
                ent.Weight = 0;
                ent.Volume = 0;
                ent.InsuranceFee = InsuranceFee;// coupon.Money;//优惠券金额 
                ent.TransitFee = Convert.ToDecimal(YPLogisMoney);//即日达20*数量  即日达的运输费用
                ent.TransportFee = originalPrice;
                ent.OverDueFee = OverDayFee;//超期费用
                                            //ent.TransportFee = Convert.ToDecimal(YPOrderMoney);//订单的费用
                ent.DeliveryFee = 0;
                ent.OtherFee = Convert.ToDecimal(YPTotalMoney) - ent.TransitFee - ent.TransportFee + ent.InsuranceFee;//平台服服务费=总金额-配送费-销售金额+优惠券
                ent.TotalCharge = Convert.ToDecimal(YPTotalMoney);
                ent.ActualAmounts = Convert.ToDecimal(actualAmounts);

                ent.CouponType = CouponType;
                ent.AwbStatus = "0";
                ent.OrderType = "4";
                ent.OrderNum = OrderNum;//最新订单顺序号
                                        //ent.FinanceSecondCheck = "1";
                                        //ent.FinanceSecondCheckName = wxUser.Name;
                                        //ent.FinanceSecondCheckDate = DateTime.Now;
                ent.goodsList = entDest;
                ent.FinanceSecondCheck = "0";
                ent.OrderModel = "0";
                ent.SuppClientNum = Convert.ToInt32(SuppClientNum);
                ent.WXOrderNo = orderno;//微信商城订单号
                ent.PaymentType = Convert.ToInt32(PaymentType) <= 2 ? "1" : "2";//支付方式(1:订尾款 2:全款)
                if (!ent.Piece.Equals(pieceSum))
                {
                    weiChat.code = 2; weiChat.msg = "购买数量不一致"; goto ERROR;
                }
                if (CheckOutType.Equals("10"))
                {
                    ent.FinanceSecondCheck = "1";
                    ent.FinanceSecondCheckName = wxUser.Name;
                    ent.FinanceSecondCheckDate = DateTime.Now;
                }
                //保存生成商城订单
                wbus.SaveWeixinOrder(new WXOrderEntity
                {
                    OrderNo = orderno,
                    TotalCharge = Convert.ToDecimal(YPTotalMoney),
                    SuppClientNum = Convert.ToInt32(SuppClientNum),
                    TransitFee = Convert.ToDecimal(YPLogisMoney),
                    WXID = wxUser.ID,
                    PayStatus = CheckOutType.Equals("10") ? "1" : "0",
                    OrderStatus = "0",
                    PayWay = CheckOutType.Equals("10") ? "3" : CheckOutType.Equals("6") ? "1" : "0",
                    OrderType = "4",//小程序订单
                    ThrowGood = YPOrderType,//订单类型即日达和次日达
                    Piece = Convert.ToInt32(YPTotalPiece),//商城订单总数量
                    Address = wxUser.Address,
                    Cellphone = YPCellphone,
                    City = wxUser.City,
                    Province = wxUser.Province,
                    Country = wxUser.Country,
                    Name = YPName,//wxUser.Name,
                    HouseID = wxUser.HouseID,
                    SaleManID = wxUser.SaleManID,
                    Memo = YPRemark,
                    CouponID = 0,//!string.IsNullOrEmpty(CouponID) ? Convert.ToInt64(CouponID) : 0,//优惠券ID
                    LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34,
                    LogicName = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? "新陆程" : wxUser.HouseID.Equals(136) ? "南宁好来运" : YPSendType.Equals("1") ? "自提" : "好来运速递",
                    productList = pro,
                    isReserve = true
                }, log);

                //保存生成仓库出库订单
                orderbus.AddReserveOrderInfo(ent, log);

            }

            try
            {


                if (!CheckOutType.Equals("6") && !CheckOutType.Equals("10"))
                {

                    if (YPProportion > 0)
                    {
                        YPTotalMoney = (Convert.ToDecimal(YPTotalMoney) - (Convert.ToDecimal(YPTotalMoney) * YPProportion)).ToString();
                    }
                    SybWxPayService sybService = new SybWxPayService();
                    orderno = orderno + "_" + PaymentType;
                    //Dictionary<String, String> rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(YPTotalMoney) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/AdvanceUnionPaySuccess.aspx", "");
                    Dictionary<String, String> rsp = new Dictionary<string, string>();
                    if (IsDebug)
                        rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(YPTotalMoney) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "http://b1e609e.r9.cpolar.cn/Interface/AdvanceUnionPaySuccess.aspx", "", paymenttype: PaymentType);
                    else
                        rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(YPTotalMoney) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/AdvanceUnionPaySuccess.aspx", "", paymenttype: PaymentType);
                    Dictionary<String, String> payinfoDic = payinfo(rsp);
                    string jsonString = String.Join(",", rsp.Select(kvp => kvp.Key + "=" + kvp.Value));
                    Common.WriteTextLog("慧采云仓小程序 预付款 通联支付回调信息：" + jsonString);

                    TenPayV3Info tenPayV3 = new TenPayV3Info(appid, appsecret, Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());

                    weiChat.msg = "{";
                    weiChat.msg += " \"appId\": \"" + tenPayV3.AppId + "\",";
                    weiChat.msg += " \"partnerId\": \"" + tenPayV3.MchId + "\",";
                    weiChat.msg += " \"prepayId\": \"" + payinfoDic["package"] + "\",";
                    weiChat.msg += " \"packageValue\": \"" + string.Format("prepay_id={0}", payinfoDic["package"]) + "\",";
                    weiChat.msg += " \"timeStamp\": \"" + payinfoDic["timeStamp"] + "\",";
                    weiChat.msg += " \"nonceStr\": \"" + payinfoDic["nonceStr"] + "\",";
                    weiChat.msg += " \"sign\": \"" + payinfoDic["paySign"] + "\",";
                    weiChat.msg += " \"orderNo\": \"" + orderno + "\"";
                    weiChat.msg += "}";
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("慧采云仓小程序 预付款 通联支付回调失败信息：" + ex.Message);
                //ex.Message;
                weiChat.code = 2; weiChat.msg = "订单推送失败"; goto ERROR;

            }


            cor.orderno = orderno;
            weiChat.data = cor;

        ////如果是次日达，并且库存是共享仓库库存，写入缓存
        //if (!ent.ShareHouseID.Equals(0) && YPOrderType.Equals("23"))
        //{
        //    //goodList
        //    RedisHelper.HashSet("NextDayOrderShareSync", ent.OrderNo + "_" + ent.HouseID.ToString() + "_" + ent.ShareHouseID.ToString(), JSON.Encode(goodList));

        //}

        ////仓库同步缓存
        //foreach (CargoContainerShowEntity time in outHouseList)
        //{
        //    CargoProductEntity syncProduct = house.SyncTypeProduct(time.ProductID.ToString());
        //    //34 马牌  1 同步马牌  2 同步全部品牌
        //    if (syncProduct.SyncType == "2" || (syncProduct.SyncType == "1" && syncProduct.TypeID == 34))
        //    {
        //        RedisHelper.HashSet("OpenSystemStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.GoodsCode);
        //    }

        //    //主仓缓存更改
        //    if (house.IsAddCaching(syncProduct.HouseID, time.TypeID))
        //    {
        //        RedisHelper.HashSet("HCYCHouseStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.ProductCode);
        //    }
        //}

        //weiChat.msg = "保存成功";
        ERROR:

            //JSON
            String result = JSON.Encode(weiChat);
            context.Response.Write(result);
        }
        private void CreateMiniAdvanceOrderBatch(HttpContext context)
        {
            string requestBody = string.Empty;
            // 关键：使用 InputStream 而非 Body
            using (Stream stream = context.Request.InputStream)
            {
                // 重置流位置到开头（防止被提前读取）
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    requestBody = reader.ReadToEnd();
                }
            }

            CreateOrderEntity weiChat = new CreateOrderEntity();
            weiChat.code = 0;
            weiChat.msg = "成功";

            JObject requestJson = JObject.Parse(requestBody);

            CreateOrderInfo cor = new CreateOrderInfo();
            string DToken = requestJson["token"]?.ToString();
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken)) { weiChat.code = 1; weiChat.msg = "请求Token为空"; goto ERROR; }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0)) { weiChat.code = 1; weiChat.msg = "Token有误"; goto ERROR; }
            string json = requestJson["YPOrder"]?.ToString();
            //String json = context.Request["YPOrder"];//订单商品明细 主要是产品编码ProductCode，购买数量BuyNum，购买单价BuyPrice
            if (String.IsNullOrEmpty(json)) { weiChat.code = 2; weiChat.msg = "参数有误"; goto ERROR; }
            CargoInterfaceBus interBus = new CargoInterfaceBus();
            CargoHouseBus house = new CargoHouseBus();
            CargoOrderBus orderbus = new CargoOrderBus();
            CargoWeiXinBus wbus = new CargoWeiXinBus();
            CargoPriceBus price = new CargoPriceBus();
            CargoProductBus productBus = new CargoProductBus();

            // 从requestJson中获取其他参数
            string houseIdStr = requestJson["HouseID"]?.ToString();
            wxUser.HouseID = string.IsNullOrEmpty(houseIdStr) || houseIdStr == "undefined" ? wxUser.HouseID : Convert.ToInt32(houseIdStr);

            int PromotionType = Convert.ToInt32(requestJson["PromotionType"]?.ToString() ?? "0");//0:正价1:特价促销2：预订单
            CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxUser.HouseID);

            string YPSendType = requestJson["YPSendType"]?.ToString() ?? "0";//配送方式 0：急送，1：自提2：快递
            string YPOrderType = requestJson["YPOrderType"]?.ToString() ?? "";//订单类型 22：即日达，23：次日达
            if (YPOrderType.Equals("23"))
            {
                YPSendType = "2";
            }
            DateTime StartBusHours = DateTime.Now;
            DateTime now = DateTime.Now;
            DateTime EndBusHours = DateTime.Now;
            if (!string.IsNullOrEmpty(houseEnt.StartBusHours))
            {
                string[] sbh = houseEnt.StartBusHours.Split(':');
                StartBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
            }
            if (!string.IsNullOrEmpty(houseEnt.EndBusHours))
            {
                string[] sbh = houseEnt.EndBusHours.Split(':');
                EndBusHours = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(sbh[0]), Convert.ToInt32(sbh[1]), 0);
            }
            string CheckOutType = requestJson["CheckOutType"]?.ToString() ?? "5";//付款方式 5：现付微信支付，3：货到付款 6：额度付款  10:预收款付款
            string YPCompany = requestJson["YPCompany"]?.ToString() ?? "";//收货单位
            string YPAddress = requestJson["YPAddress"]?.ToString() ?? "";//收货地址
            string YPName = requestJson["YPName"]?.ToString() ?? "";//收货人
            string YPCellphone = requestJson["YPCellphone"]?.ToString() ?? "";//手机号码
            string YPRemark = (requestJson["YPRemark"]?.ToString() ?? "").Replace("undefined", "");//备注
            string YPOrderMoney = requestJson["YPOrderMoney"]?.ToString() ?? "";//订单总金额
            string actualAmounts = requestJson["YPOrderMoney"]?.ToString() ?? "";//订单总金额
            string YPLogisMoney = requestJson["YPLogisMoney"]?.ToString() ?? "";//物流费
            string Deposit = requestJson["YPTotalMoney"]?.ToString() ?? "";//订金 前端计算后的价格
            string YPTotalMoney = requestJson["YPTotalMoney"]?.ToString() ?? "";//总金额
            string YPTotalPiece = requestJson["YPTotalPiece"]?.ToString() ?? "";//总数量 总条数
            decimal YPProportion = Convert.ToDecimal(requestJson["Proportion"]?.ToString() ?? "0");//总数量 总条数
            YPTotalMoney = actualAmounts;
            //string SuppClientNum = Convert.ToString(context.Request["YPSuppClientNum"]);//供应商编码
            string SuppClientNum = "551098";//供应商编码 供应商默认狄乐汽服OE
            string RuleID = requestJson["RuleID"]?.ToString() ?? "";//促销规则ID    第一道防线
            string PaymentType = requestJson["PaymentType"]?.ToString() ?? "";// (1:定金, 2:尾款 3:全款) 

            if (String.IsNullOrEmpty(YPOrderType)) { weiChat.code = 2; weiChat.msg = "订单类型有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPName)) { weiChat.code = 2; weiChat.msg = "购买人不能为空"; goto ERROR; }
            if (String.IsNullOrEmpty(YPCellphone)) { weiChat.code = 2; weiChat.msg = "购买手机号不能为空"; goto ERROR; }
            if (String.IsNullOrEmpty(YPTotalPiece)) { weiChat.code = 2; weiChat.msg = "总数量不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPTotalPiece) <= 0) { weiChat.code = 2; weiChat.msg = "总数量数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPOrderMoney)) { weiChat.code = 2; weiChat.msg = "订单金额不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPOrderMoney) <= 0) { weiChat.code = 2; weiChat.msg = "订单金额数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(YPTotalMoney)) { weiChat.code = 2; weiChat.msg = "总金额不能为空"; goto ERROR; }
            if (Convert.ToDecimal(YPTotalMoney) <= 0) { weiChat.code = 2; weiChat.msg = "总金额数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(SuppClientNum)) { weiChat.code = 2; weiChat.msg = "供应商不能为空"; goto ERROR; }
            if (Convert.ToInt32(SuppClientNum).Equals(0)) { weiChat.code = 2; weiChat.msg = "供应商数据有误"; goto ERROR; }
            if (String.IsNullOrEmpty(PaymentType)) { weiChat.code = 2; weiChat.msg = "支付类型不能为空"; goto ERROR; }

            //string CouponID = Convert.ToString(context.Request["CouponID"]).Equals("0") ? "" : Convert.ToString(context.Request["CouponID"]).ToLower().Equals("undefined") ? "" : Convert.ToString(context.Request["CouponID"]);
            //string RuleID = context.Request["RuleID"];
            //第二道防线  再次查询
            //优惠券金额变量 优惠券类型，平台券或商家券
            decimal InsuranceFee = 0; string CouponType = "0";
            List<long> couponIDList = new List<long>();

            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
            List<CargoInterfaceEntity> goodList = new List<CargoInterfaceEntity>();

            int oNum = 0;
            decimal hzj = 0.00M;
            decimal originalPrice = 0M;
            foreach (Hashtable ht in rows)
            {
                if (string.IsNullOrEmpty(Convert.ToString(ht["BuyNum"]))) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
                if (Convert.ToInt32(ht["BuyNum"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买数量有误"; goto ERROR; }
                if (string.IsNullOrEmpty(Convert.ToString(ht["BuyPrice"]))) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
                if (Convert.ToDecimal(ht["BuyPrice"]) <= 0) { weiChat.code = 2; weiChat.msg = "购买单价有误"; goto ERROR; }
                if (string.IsNullOrEmpty(Convert.ToString(ht["BatchYear"]))) { weiChat.code = 2; weiChat.msg = "商品周期有误"; goto ERROR; }

                int TypeID = Convert.ToInt32(ht["TypeID"]);//品牌
                //int TypeID = Convert.ToInt32(context.Request["TypeID"]);//品牌

                var pData = productBus.GetProductSpecEntity(new CargoProductSpecEntity { TypeID = TypeID, ProductCode = Convert.ToString(ht["ProductCode"]) });

                pro.Add(new CargoProductShelvesEntity
                {
                    ProductCode = Convert.ToString(ht["ProductCode"]),//产品编码
                    BatchYear = Convert.ToInt32(ht["BatchYear"]),//商品周期批次年
                    OrderNum = Convert.ToInt32(ht["BuyNum"]),//商品购买数量
                    OrderPrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                    RuleID = 0,
                    CutEntry = 0
                });
                goodList.Add(new CargoInterfaceEntity
                {
                    TypeID = TypeID,
                    ProductCode = pData.ProductCode,
                    GoodsCode = pData.GoodsCode,
                    ProductName = pData.ProductName,
                    Specs = pData.Specs,
                    Model = pData.Model,
                    Figure = pData.Figure,
                    StockNum = Convert.ToInt32(ht["BuyNum"]),
                    ActSalePrice = Convert.ToDecimal(ht["BuyPrice"]),//商品购买单价
                    SpecsType = YPOrderType.Equals("23") ? "5" : "4",
                });
                //originalPrice += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["originalPrice"]);
                oNum += Convert.ToInt32(ht["BuyNum"]);
                hzj += Convert.ToDecimal(ht["BuyNum"]) * Convert.ToDecimal(ht["BuyPrice"]);
            }

            //if (YPProportion > 0)
            //{
            //    hzj = hzj - (hzj * YPProportion);
            //    YPOrderMoney = (Convert.ToDecimal(YPOrderMoney) - (Convert.ToDecimal(YPOrderMoney) * YPProportion)).ToString();
            //}
            if (!Convert.ToInt32(YPTotalPiece).Equals(oNum))
            {
                weiChat.code = 2; weiChat.msg = "订单总数量有误"; goto ERROR;
            }

            if (!Convert.ToDecimal(YPOrderMoney).Equals(hzj))
            {
                weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
            }
            //订单总金额=商品明细总金额+物流运费-优惠券
            if (!Convert.ToDecimal(YPTotalMoney).Equals(Convert.ToDecimal(YPOrderMoney) - InsuranceFee))
            {
                weiChat.code = 2; weiChat.msg = "订单总金额有误"; goto ERROR;
            }
            //预收款支付，判断下预收款金额是否足够
            if (CheckOutType.Equals("10"))
            {
                //预收款支付，判断下预收款金额是否足够
                if (Convert.ToDecimal(YPTotalMoney) > wxUser.PreReceiveMoney)
                {
                    weiChat.code = 2; weiChat.msg = "预收款金额不足：" + wxUser.PreReceiveMoney.ToString(); goto ERROR;
                }
            }

            decimal wxZJ = Convert.ToDecimal(YPTotalMoney) * 100;
            string orderno = GetOrderNumber();//商城订单号
            decimal OverDayFee = 0.00M;//超期费用

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "新增订单";
            log.UserID = wxUser.wxOpenID;
            log.Operate = "A";

            //CargoOrderEntity ent = new CargoOrderEntity();
            CargoReserveOrderEntity ent = new CargoReserveOrderEntity();
            //订金/全款 生成订单数据
            if (PaymentType == "1" || PaymentType == "3")
            {
                List<CargoReserveOrderGoodsEntity> entDest = new List<CargoReserveOrderGoodsEntity>();
                //保存生成仓库订单
                List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();

                ent.Dep = houseEnt.DepCity;
                ent.Dest = wxUser.City;
                int OrderNum = 0;
                ent.HouseID = wxUser.HouseID;
                //var AreaDara = house.QueryAreaByHouseID(new CargoAreaEntity { HouseID = ent.HouseID });
                var AreaDara = house.QueryALLArea(new CargoAreaEntity { HouseID = ent.HouseID, ParentID = 0 }).FirstOrDefault();
                ent.LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34;
                ent.Rebate = 0;
                ent.CheckOutType = CheckOutType;// "5";//Convert.ToString(row["CheckOutType"]);
                                                //ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(row["ReturnAwb"])) ? 0 : Convert.ToInt32(row["ReturnAwb"]);
                ent.TrafficType = "0";// Convert.ToString(row["TrafficType"]);
                ent.DeliveryType = "2";//Convert.ToString(row["DeliveryType"]);
                                       //ent.CheckStatus = "1";//Convert.ToString(row["DeliveryType"]);
                ent.AcceptUnit = !string.IsNullOrEmpty(YPCompany) ? YPCompany : wxUser.ClientName;//Convert.ToString(row["AcceptUnit"]);取公司名称
                ent.AcceptAddress = !string.IsNullOrEmpty(YPAddress) ? YPAddress : wxUser.Address;// Convert.ToString(row["AcceptAddress"]);取注册时填写的公司地址
                ent.AcceptPeople = YPName;//Convert.ToString(row["AcceptPeople"]);
                ent.AcceptTelephone = YPCellphone;//Convert.ToString(row["AcceptTelephone"]);
                ent.AcceptCellphone = YPCellphone;//Convert.ToString(row["AcceptCellphone"]);
                ent.CreateAwb = wxUser.Name;//开单人生成订单人取当前微信人
                ent.CreateAwbID = wxUser.ClientNum.ToString();//开单人ID取
                ent.CreateDate = DateTime.Now;
                ent.OP_ID = wxUser.ClientNum.ToString();
                ent.OP_Name = wxUser.Name;
                ent.SaleManID = wxUser.SaleManID;
                ent.SaleManName = wxUser.SaleManName;
                ent.SaleCellPhone = "";
                ent.Remark = YPRemark;
                //ent.CouponID = string.IsNullOrEmpty(CouponID) ? 0 : Convert.ToInt64(CouponID);
                ent.CouponIDList = couponIDList;
                //ent.ThrowGood = "0";
                ent.ThrowGood = YPOrderType == "0" ? "23" : YPOrderType;
                ent.BusinessID = "22";
                ent.MarketType = "2";
                ent.IsPrintPrice = 1;
                ent.TranHouse = "";
                ent.PostponeShip = "1";
                ent.ClientNum = wxUser.ClientNum;
                ent.PayClientNum = wxUser.ClientNum;
                ent.PayClientName = wxUser.ClientName;//付款人客户姓名
                ent.ClientID = wxUser.ClientID;
                //string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
                ent.OrderNo = Common.GetMaxReserveOrderNumByCurrentDate(wxUser.HouseID, houseEnt.HouseCode, out OrderNum); // Convert.ToString(row["OrderNo"]);//生成最新顺序订单号
                ent.OutHouseName = houseEnt.Name;
                int pieceSum = 0;
                string proStr = string.Empty;
                foreach (var itt in goodList)
                {
                    pieceSum += itt.StockNum;
                    int piece = itt.StockNum;
                    List<CargoProductEntity> productBasic = house.QueryALLProductData(new CargoProductEntity { ProductCode = itt.ProductCode, SuppClientNum = Convert.ToInt64(SuppClientNum) });
                    if (productBasic.Count <= 0)
                    {
                        weiChat.code = 2; weiChat.msg = "商品基础数据有误"; goto ERROR;
                    }
                    proStr = productBasic[0].TypeName + " " + productBasic[0].Specs + " " + productBasic[0].Figure + " " + productBasic[0].LoadIndex + productBasic[0].SpeedLevel;

                    //部分出
                    entDest.Add(new CargoReserveOrderGoodsEntity
                    {
                        OrderNo = ent.OrderNo,
                        //ProductID = itt.ProductID,
                        ProductCode = itt.ProductCode,
                        ProductName = itt.ProductName,
                        GoodsCode = itt.GoodsCode,
                        Specs = itt.Specs,
                        Figure = itt.Figure,
                        TypeID = itt.TypeID,
                        Model = itt.Model,
                        HouseID = ent.HouseID,
                        AreaID = AreaDara == null ? 0 : AreaDara.AreaID,
                        Piece = piece,
                        //ActSalePrice = it.SalePrice,
                        ActSalePrice = itt.ActSalePrice,
                        SupplySalePrice = itt.InHousePrice,
                        //ContainerCode = itt.ContainerCode,
                        //OutCargoID = outID,
                        OP_ID = log.UserID,
                        OverDayNum = 0,
                        OverDueFee = 0,
                    });

                }

                //订单总数量
                ent.Piece = Convert.ToInt32(YPTotalPiece);
                ent.Weight = 0;
                ent.Volume = 0;
                ent.InsuranceFee = InsuranceFee;// coupon.Money;//优惠券金额 
                ent.TransitFee = Convert.ToDecimal(YPLogisMoney);//即日达20*数量  即日达的运输费用
                ent.TransportFee = originalPrice;
                ent.OverDueFee = OverDayFee;//超期费用
                                            //ent.TransportFee = Convert.ToDecimal(YPOrderMoney);//订单的费用
                ent.DeliveryFee = 0;
                ent.OtherFee = Convert.ToDecimal(YPTotalMoney) - ent.TransitFee - ent.TransportFee + ent.InsuranceFee;//平台服服务费=总金额-配送费-销售金额+优惠券
                ent.TotalCharge = Convert.ToDecimal(YPTotalMoney);
                ent.ActualAmounts = Convert.ToDecimal(actualAmounts);

                ent.CouponType = CouponType;
                ent.AwbStatus = "0";
                ent.OrderType = "4";
                ent.OrderNum = OrderNum;//最新订单顺序号
                                        //ent.FinanceSecondCheck = "1";
                                        //ent.FinanceSecondCheckName = wxUser.Name;
                                        //ent.FinanceSecondCheckDate = DateTime.Now;
                ent.goodsList = entDest;
                ent.FinanceSecondCheck = "0";
                ent.OrderModel = "0";
                ent.SuppClientNum = Convert.ToInt32(SuppClientNum);
                ent.WXOrderNo = orderno;//微信商城订单号
                ent.PaymentType = Convert.ToInt32(PaymentType) <= 2 ? "1" : "2";//支付方式(1:订尾款 2:全款)
                if (!ent.Piece.Equals(pieceSum))
                {
                    weiChat.code = 2; weiChat.msg = "购买数量不一致"; goto ERROR;
                }
                if (CheckOutType.Equals("10"))
                {
                    ent.FinanceSecondCheck = "1";
                    ent.FinanceSecondCheckName = wxUser.Name;
                    ent.FinanceSecondCheckDate = DateTime.Now;
                }
                //保存生成商城订单
                wbus.SaveWeixinOrder(new WXOrderEntity
                {
                    OrderNo = orderno,
                    TotalCharge = Convert.ToDecimal(YPTotalMoney),
                    SuppClientNum = Convert.ToInt32(SuppClientNum),
                    TransitFee = Convert.ToDecimal(YPLogisMoney),
                    WXID = wxUser.ID,
                    PayStatus = CheckOutType.Equals("10") ? "1" : "0",
                    OrderStatus = "0",
                    PayWay = CheckOutType.Equals("10") ? "3" : CheckOutType.Equals("6") ? "1" : "0",
                    OrderType = "4",//小程序订单
                    ThrowGood = YPOrderType,//订单类型即日达和次日达
                    Piece = Convert.ToInt32(YPTotalPiece),//商城订单总数量
                    Address = wxUser.Address,
                    Cellphone = YPCellphone,
                    City = wxUser.City,
                    Province = wxUser.Province,
                    Country = wxUser.Country,
                    Name = YPName,//wxUser.Name,
                    HouseID = wxUser.HouseID,
                    SaleManID = wxUser.SaleManID,
                    Memo = YPRemark,
                    CouponID = 0,//!string.IsNullOrEmpty(CouponID) ? Convert.ToInt64(CouponID) : 0,//优惠券ID
                    LogisID = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? 62 : wxUser.HouseID.Equals(136) ? 383 : YPSendType.Equals("1") ? 46 : 34,
                    LogicName = wxUser.HouseID.Equals(97) || wxUser.HouseID.Equals(95) || wxUser.HouseID.Equals(101) ? "新陆程" : wxUser.HouseID.Equals(136) ? "南宁好来运" : YPSendType.Equals("1") ? "自提" : "好来运速递",
                    productList = pro,
                    isReserve = true
                }, log);

                //保存生成仓库出库订单
                orderbus.AddReserveOrderInfo(ent, log);

            }

            try
            {


                if (!CheckOutType.Equals("6") && !CheckOutType.Equals("10"))
                {

                    //if (YPProportion > 0)
                    //{
                    //    YPTotalMoney = (Convert.ToDecimal(YPTotalMoney) - (Convert.ToDecimal(YPTotalMoney) * YPProportion)).ToString();
                    //}
                    SybWxPayService sybService = new SybWxPayService();
                    orderno = orderno + "_" + PaymentType;
                    //Dictionary<String, String> rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(YPTotalMoney) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/AdvanceUnionPaySuccess.aspx", "");
                    Dictionary<String, String> rsp = new Dictionary<string, string>();
                    if (IsDebug)
                        rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(Deposit) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "http://b1e609e.r9.cpolar.cn/Interface/AdvanceUnionPaySuccess.aspx", "", paymenttype: PaymentType);
                    else
                        rsp = sybService.pay(Convert.ToInt64(Convert.ToDecimal(Deposit) * 100), orderno, "W06", wxUser.HouseName + "-小程序支付", YPRemark, wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/AdvanceUnionPaySuccess.aspx", "", paymenttype: PaymentType);
                    Dictionary<String, String> payinfoDic = payinfo(rsp);
                    string jsonString = String.Join(",", rsp.Select(kvp => kvp.Key + "=" + kvp.Value));
                    Common.WriteTextLog("慧采云仓小程序 预付款 通联支付回调信息：" + jsonString);

                    TenPayV3Info tenPayV3 = new TenPayV3Info(appid, appsecret, Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());

                    weiChat.msg = "{";
                    weiChat.msg += " \"appId\": \"" + tenPayV3.AppId + "\",";
                    weiChat.msg += " \"partnerId\": \"" + tenPayV3.MchId + "\",";
                    weiChat.msg += " \"prepayId\": \"" + payinfoDic["package"] + "\",";
                    weiChat.msg += " \"packageValue\": \"" + string.Format("prepay_id={0}", payinfoDic["package"]) + "\",";
                    weiChat.msg += " \"timeStamp\": \"" + payinfoDic["timeStamp"] + "\",";
                    weiChat.msg += " \"nonceStr\": \"" + payinfoDic["nonceStr"] + "\",";
                    weiChat.msg += " \"sign\": \"" + payinfoDic["paySign"] + "\",";
                    weiChat.msg += " \"orderNo\": \"" + orderno + "\"";
                    weiChat.msg += "}";
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("慧采云仓小程序 预付款 通联支付回调失败信息：" + ex.Message);
                //ex.Message;
                weiChat.code = 2; weiChat.msg = "订单推送失败"; goto ERROR;

            }


            cor.orderno = orderno;
            weiChat.data = cor;

        ////如果是次日达，并且库存是共享仓库库存，写入缓存
        //if (!ent.ShareHouseID.Equals(0) && YPOrderType.Equals("23"))
        //{
        //    //goodList
        //    RedisHelper.HashSet("NextDayOrderShareSync", ent.OrderNo + "_" + ent.HouseID.ToString() + "_" + ent.ShareHouseID.ToString(), JSON.Encode(goodList));

        //}

        ////仓库同步缓存
        //foreach (CargoContainerShowEntity time in outHouseList)
        //{
        //    CargoProductEntity syncProduct = house.SyncTypeProduct(time.ProductID.ToString());
        //    //34 马牌  1 同步马牌  2 同步全部品牌
        //    if (syncProduct.SyncType == "2" || (syncProduct.SyncType == "1" && syncProduct.TypeID == 34))
        //    {
        //        RedisHelper.HashSet("OpenSystemStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.GoodsCode);
        //    }

        //    //主仓缓存更改
        //    if (house.IsAddCaching(syncProduct.HouseID, time.TypeID))
        //    {
        //        RedisHelper.HashSet("HCYCHouseStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.ProductCode);
        //    }
        //}

        //weiChat.msg = "保存成功";
        ERROR:

            //JSON
            String result = JSON.Encode(weiChat);
            context.Response.Write(result);
        }
        private string printRsp(Dictionary<String, String> rspDic)
        {
            string rsp = "请求返回数据:\n";
            foreach (var item in rspDic)
            {
                rsp += item.Key + "-----" + item.Value + ";\n";
            }
            return rsp;
        }


        private Dictionary<string, string> payinfo(Dictionary<String, String> rspDic)
        {
            String payinfo = rspDic["payinfo"];

            // 创建一个Dictionary<String, String>
            Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(payinfo);

            // 通过键获取值
            string rsp = "";
            if (dictionary.TryGetValue("package", out string packageValue))
            {
                // 输出 "prepay_id=wx090924477812138e451c0b06f676e00001"
                int equalIndex = packageValue.IndexOf('=');
                if (equalIndex != -1)
                {
                    rsp = packageValue.Substring(equalIndex + 1);
                    dictionary["package"] = rsp;
                }

            }
            return dictionary;

        }
        //微信统一下单获取prepay_id & 再次签名返回数据
        private static string Getprepay_id(string appid, string attach, string body, string mch_id, string nonce_str, string notify_url, string openid, string bookingNo, string total_fee)
        {
            var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";//微信统一下单请求地址
            string strA = "appid=" + appid + "&attach=" + attach + "&body=" + body + "&mch_id=" + mch_id + "&nonce_str=" + nonce_str + "&notify_url=" + notify_url + "&openid=" + openid + "&out_trade_no=" + bookingNo + "&spbill_create_ip=&total_fee=" + total_fee + "&trade_type=JSAPI";
            string strk = strA + "&key=" + _key; //key为商户平台设置的密钥key(假)
            string strMD5 = CMD5(strk).ToUpper();//MD5签名

            //string strHash=HmacSHA256("sha256",strmd5).ToUpper();  //签名方式只需一种(MD5 或 HmacSHA256   【支付文档需仔细看】)

            //签名
            var formData = "<xml>";
            formData += "<appid>" + appid + "</appid>";//appid 
            formData += "<attach>" + attach + "</attach>"; //附加数据(描述)
            formData += "<body>" + body + "</body>";//商品描述
            formData += "<mch_id>" + mch_id + "</mch_id>";//商户号 
            formData += "<nonce_str>" + nonce_str + "</nonce_str>";//随机字符串，不长于32位。 
            formData += "<notify_url>" + notify_url + "</notify_url>";//通知地址
            formData += "<openid>" + openid + "</openid>";//openid
            formData += "<out_trade_no>" + bookingNo + "</out_trade_no>";//商户订单号  --待
            formData += "<spbill_create_ip></spbill_create_ip>";//终端IP --用户ip
            formData += "<total_fee>" + total_fee + "</total_fee>";//支付金额单位为（分）
            formData += "<trade_type>JSAPI</trade_type>";//交易类型(JSAPI--公众号支付)
            formData += "<sign>" + strMD5 + "</sign>"; //签名
            formData += "</xml>";

            //请求数据
            var getdata = wxHttpUtility.SendHttpRequest(url, formData);

            //获取xml数据
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(getdata);
            //xml格式转json
            string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            string prepay_id = jo["xml"]["prepay_id"]["#cdata-section"].ToString();

            //时间戳
            string _time = getTime().ToString();

            //再次签名返回数据至小程序
            string strB = "appId=" + appid + "&nonceStr=" + nonce_str + "&package=prepay_id=" + prepay_id + "&signType=MD5&timeStamp=" + _time + "&key=" + _key;

            WeChatPayInfo w = new WeChatPayInfo();
            w.timeStamp = _time;
            w.nonceStr = nonce_str;
            w.package = "prepay_id=" + prepay_id;
            w.paySign = CMD5(strB).ToUpper(); ;
            w.signType = "MD5";

            //向小程序发送json数据
            return JsonConvert.SerializeObject(w);
        }
        /// <summary>
        /// 生成随机串  
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns></returns>
        private static string GetRandomString(int length)
        {
            const string key = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            if (length < 1)
                return string.Empty;

            Random rnd = new Random();
            byte[] buffer = new byte[8];

            ulong bit = 31;
            ulong result = 0;
            int index = 0;
            StringBuilder sb = new StringBuilder((length / 5 + 1) * 5);

            while (sb.Length < length)
            {
                rnd.NextBytes(buffer);

                buffer[5] = buffer[6] = buffer[7] = 0x00;
                result = BitConverter.ToUInt64(buffer, 0);

                while (result > 0 && sb.Length < length)
                {
                    index = (int)(bit & result);
                    sb.Append(key[index]);
                    result = result >> 5;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private static long getTime()
        {
            TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
            long t = (long)cha.TotalSeconds;
            return t;
        }
        /// <summary>
        /// MD5签名方法 
        /// </summary> 
        /// <param name="inputText">加密参数</param> 
        /// <returns></returns> 
        private static string CMD5(string inputText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(inputText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            return byte2String;
        }
        /// <summary>
        /// HMAC-SHA256签名方式
        /// </summary>
        /// <param name="message"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        private static string HmacSHA256(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        /// <summary>
        /// 微信预支付 返回预支付 ID
        /// </summary>
        /// <param name="attach"></param>
        /// <param name="body"></param>
        /// <param name="openid"></param>
        /// <param name="price"></param>
        /// <param name="orderNum"></param>
        /// <param name="tenPayV3"></param>
        /// <returns></returns>
        private string PayInfo(string attach, string body, string openid, string price, string orderNum, TenPayV3Info tenPayV3, string nonceStr)
        {
            string prepayId = string.Empty;
            RequestHandler requestHandler = new RequestHandler(HttpContext.Current);
            //微信分配的公众账号ID（企业号corpid即为此appId）
            requestHandler.SetParameter("appid", appid);
            //附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
            requestHandler.SetParameter("attach", attach);
            //商品或支付单简要描述
            requestHandler.SetParameter("body", body);
            //微信支付分配的商户号
            requestHandler.SetParameter("mch_id", tenPayV3.MchId);
            //随机字符串，不长于32位。
            requestHandler.SetParameter("nonce_str", nonceStr);
            //接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数。
            requestHandler.SetParameter("notify_url", tenPayV3.TenPayV3Notify);
            //trade_type=JSAPI，此参数必传，用户在商户公众号appid下的唯一标识。
            requestHandler.SetParameter("openid", openid);
            //商户系统内部的订单号,32个字符内、可包含字母，自己生成
            requestHandler.SetParameter("out_trade_no", "JSAPI" + orderNum);
            //APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP。
            //requestHandler.SetParameter("spbill_create_ip", "127.0.0.1");
            //订单总金额，单位为分，做过银联支付的朋友应该知道，代表金额为12位，末位分分
            requestHandler.SetParameter("total_fee", price);
            //取值如下：JSAPI，NATIVE，APP，我们这里使用JSAPI
            requestHandler.SetParameter("trade_type", Senparc.Weixin.MP.TenPayV3Type.JSAPI.ToString());
            //设置KEY
            //requestHandler.SetKey(tenPayV3.Key);
            string sign = requestHandler.CreateMd5Sign("key", tenPayV3.Key);
            requestHandler.SetParameter("sign", sign);

            string data = requestHandler.ParseXML();
            requestHandler.GetDebugInfo();

            //获取并返回预支付XML信息
            string result = TenPayV3.Unifiedorder(data);
            Common.WriteTextLog(result);
            var res = XDocument.Parse(result);
            prepayId = res.Element("xml").Element("prepay_id").Value;
            return prepayId;
        }
        ///// <summary>
        ///// 重新支付小程序商城订单
        ///// </summary>
        ///// <param name="context"></param>
        //private void AgainPayMiniProOrder(HttpContext context)
        //{
        //    CreateOrderEntity weiChat = new CreateOrderEntity();
        //    weiChat.code = 0;
        //    weiChat.msg = "成功";
        //    CreateOrderInfo cor = new CreateOrderInfo();
        //    string DToken = Convert.ToString(context.Request["token"]);
        //    Common.WriteTextLog("绑定" + DToken);
        //    if (string.IsNullOrEmpty(DToken))
        //    {
        //        weiChat.code = 1;
        //        weiChat.msg = "请求Token为空";
        //        goto ERROR;
        //    }
        //    WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
        //    if (wxUser.ID.Equals(0))
        //    {
        //        weiChat.code = 1;
        //        weiChat.msg = "Token有误";
        //        goto ERROR;
        //    }

        //    string OrderNo = Convert.ToString(context.Request["OrderNo"]);
        //    Common.WriteTextLog("绑定:" + OrderNo);
        //    if (string.IsNullOrEmpty(OrderNo))
        //    {
        //        weiChat.code = 2;
        //        weiChat.msg = "订单号不能为空";
        //        goto ERROR;
        //    }
        //    CargoWeiXinBus bus = new CargoWeiXinBus();
        //    List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(1, 2, new WXOrderEntity { OrderNo = OrderNo });
        //    if (result.Count <= 0)
        //    {
        //        weiChat.code = 1007;
        //        weiChat.msg = "订单不存在";
        //        goto ERROR;
        //    }
        //    WXOrderEntity wo = result[0];
        //    if (wo.TotalCharge <= 0)
        //    {
        //        weiChat.code = 1011;
        //        weiChat.msg = "订单价格有误";
        //        goto ERROR;
        //    }
        //    if (wo.ID.Equals(0))
        //    {
        //        weiChat.code = 1007;
        //        weiChat.msg = "订单不存在";
        //        goto ERROR;
        //    }
        //    if (wo.PayWay.Equals("0") && wo.PayStatus.Equals("1"))
        //    {
        //        weiChat.code = 1008;
        //        weiChat.msg = "订单已付款";
        //        goto ERROR;
        //    }
        //    //if (!wo.OrderStatus.Equals("0"))
        //    //{
        //    //    weiChat.code = 1009;
        //    //    weiChat.msg = "仓库已发货";
        //    //    goto ERROR;
        //    //}
        //    Common.WriteTextLog("开始支付");
        //    string tmst = TenPayV3Util.GetTimestamp();
        //    string nocs = TenPayV3Util.GetNoncestr();
        //    //TenPayV3Info tenPayV3 = new TenPayV3Info(Common.GetHCYCAppID(), Common.GetHCYCAppSecret(), Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());
        //    TenPayV3Info tenPayV3 = new TenPayV3Info(appid, appsecret, Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());
        //    int wxZJ = (int)(result[0].TotalCharge * 100);
        //    string prepayID = PayInfo("", "慧采云仓小程序支付", wxUser.wxOpenID, wxZJ.ToString(), OrderNo, tenPayV3, nocs);
        //    Common.WriteTextLog(prepayID);

        //    //设置支付参数
        //    RequestHandler paySignReqHandler = new RequestHandler();
        //    paySignReqHandler.SetParameter("appId", tenPayV3.AppId);
        //    paySignReqHandler.SetParameter("timeStamp", tmst);
        //    paySignReqHandler.SetParameter("nonceStr", nocs);
        //    //paySignReqHandler.SetParameter("partnerid", tenPayV3.MchId);
        //    //paySignReqHandler.SetParameter("prepayid", prepayID);
        //    paySignReqHandler.SetParameter("package", string.Format("prepay_id={0}", prepayID));
        //    paySignReqHandler.SetParameter("signType", "MD5");
        //    string paySign = paySignReqHandler.CreateMd5Sign("key", tenPayV3.Key);

        //    weiChat.msg = "{";
        //    weiChat.msg += " \"appId\": \"" + tenPayV3.AppId + "\",";
        //    weiChat.msg += " \"partnerId\": \"" + tenPayV3.MchId + "\",";
        //    weiChat.msg += " \"prepayId\": \"" + prepayID + "\",";
        //    weiChat.msg += " \"packageValue\": \"" + string.Format("prepay_id={0}", prepayID) + "\",";
        //    weiChat.msg += " \"timeStamp\": \"" + tmst + "\",";
        //    weiChat.msg += " \"nonceStr\": \"" + nocs + "\",";
        //    weiChat.msg += " \"sign\": \"" + paySign + "\",";
        //    weiChat.msg += " \"orderNo\": \"" + OrderNo + "\"";
        //    weiChat.msg += "}";

        //ERROR:
        //    //JSON
        //    String returnString = JSON.Encode(weiChat);
        //    context.Response.Write(returnString);
        //}


        /// <summary>
        /// 重新支付小程序商城订单
        /// </summary>
        /// <param name="context"></param>
        private void AgainPayMiniProOrder(HttpContext context)
        {
            CreateOrderEntity weiChat = new CreateOrderEntity();
            weiChat.code = 0;
            weiChat.msg = "成功";
            CreateOrderInfo cor = new CreateOrderInfo();
            string DToken = Convert.ToString(context.Request["token"]);
            Common.WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                weiChat.code = 1;
                weiChat.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                weiChat.code = 1;
                weiChat.msg = "Token有误";
                goto ERROR;
            }

            string OrderNo = Convert.ToString(context.Request["OrderNo"]);
            var IsReserve = Convert.ToInt32(context.Request["IsReserve"]);
            var PayMentType = Convert.ToString(context.Request["PayMentType"]);
            Common.WriteTextLog("绑定:" + OrderNo);
            if (string.IsNullOrEmpty(OrderNo))
            {
                weiChat.code = 2;
                weiChat.msg = "订单号不能为空";
                goto ERROR;
            }
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(1, 2, new WXOrderEntity { OrderNo = OrderNo, isReserve = IsReserve == 1 });
            if (result.Count <= 0)
            {
                weiChat.code = 1007;
                weiChat.msg = "订单不存在";
                goto ERROR;
            }
            WXOrderEntity wo = result[0];
            if (wo.TotalCharge <= 0)
            {
                weiChat.code = 1011;
                weiChat.msg = "订单价格有误";
                goto ERROR;
            }
            if (wo.ID.Equals(0))
            {
                weiChat.code = 1007;
                weiChat.msg = "订单不存在";
                goto ERROR;
            }
            if (!(IsReserve == 1 && PayMentType == "2"))
            {
                if (wo.PayWay.Equals("0") && wo.PayStatus.Equals("1"))
                {
                    weiChat.code = 1008;
                    weiChat.msg = "订单已付款";
                    goto ERROR;
                }
            }
            if (PayMentType == "0" && IsReserve == 1)
            {
                weiChat.code = 1008;
                weiChat.msg = "订单付款类型获取失败";
                goto ERROR;
            }

            Common.WriteTextLog("开始支付");

            TenPayV3Info tenPayV3 = new TenPayV3Info(appid, appsecret, Common.GetHCYCMachID(), Common.GetHCYCWxPayKey(), Common.GetHCYCWxPayTranUrl());
            if (PayMentType == "1" && IsReserve == 1)
            {
                //订金
                result[0].TotalCharge = Math.Round(result[0].TotalCharge * 0.3M, 2, MidpointRounding.AwayFromZero);
            }
            int wxZJ = (int)(result[0].TotalCharge * 100);

            SybWxPayService sybService = new SybWxPayService();
            //Dictionary<String, String> rsp = sybService.pay(Convert.ToInt64(wxZJ), OrderNo, "W06", "慧采云仓小程序支付", "再次支付", wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/UnionPaySuccess.aspx", "");
            Dictionary<String, String> rsp = new Dictionary<string, string>();
            if (IsDebug)
            {
                if (IsReserve == 1)
                {
                    OrderNo = OrderNo + "_" + PayMentType;
                    rsp = sybService.pay(Convert.ToInt64(wxZJ), OrderNo, "W06", "慧采云仓小程序支付", "再次支付", wxUser.wxOpenID, "", "http://b1e609e.r9.cpolar.cn/Interface/AdvanceUnionPaySuccess.aspx", "");
                }
                else
                {
                    rsp = sybService.pay(Convert.ToInt64(wxZJ), OrderNo, "W06", "慧采云仓小程序支付", "再次支付", wxUser.wxOpenID, "", "http://b1e609e.r9.cpolar.cn/Interface/UnionPaySuccess.aspx", "");
                }
            }
            else
            {
                if (IsReserve == 1)
                {
                    OrderNo = OrderNo + "_" + PayMentType;
                    rsp = sybService.pay(Convert.ToInt64(wxZJ), OrderNo, "W06", "慧采云仓小程序支付", "再次支付", wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/AdvanceUnionPaySuccess.aspx", "");
                }
                else
                {
                    rsp = sybService.pay(Convert.ToInt64(wxZJ), OrderNo, "W06", "慧采云仓小程序支付", "再次支付", wxUser.wxOpenID, "", "https://dlt.neway5.com/Interface/UnionPaySuccess.aspx", "");
                }
            }
            Dictionary<String, String> payinfoDic = payinfo(rsp);
            string jsonString = String.Join(",", rsp.Select(kvp => kvp.Key + "=" + kvp.Value));
            Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：" + jsonString);

            weiChat.msg = "{";
            weiChat.msg += " \"appId\": \"" + tenPayV3.AppId + "\",";
            weiChat.msg += " \"partnerId\": \"" + tenPayV3.MchId + "\",";
            weiChat.msg += " \"prepayId\": \"" + payinfoDic["package"] + "\",";
            weiChat.msg += " \"packageValue\": \"" + string.Format("prepay_id={0}", payinfoDic["package"]) + "\",";
            weiChat.msg += " \"timeStamp\": \"" + payinfoDic["timeStamp"] + "\",";
            weiChat.msg += " \"nonceStr\": \"" + payinfoDic["nonceStr"] + "\",";
            weiChat.msg += " \"sign\": \"" + payinfoDic["paySign"] + "\",";
            weiChat.msg += " \"orderNo\": \"" + OrderNo + "\"";
            weiChat.msg += "}";
        ERROR:
            //JSON
            String returnString = JSON.Encode(weiChat);
            context.Response.Write(returnString);
        }
        /// <summary>
        /// 我的订单详细列表
        /// </summary>
        /// <param name="context"></param>
        private void QueryMyMiniProOrder(HttpContext context)
        {
            ReturnMiniProOrderEntity result = new ReturnMiniProOrderEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXOrderEntity queryEntity = new WXOrderEntity();
            //queryEntity.HouseID = wxUser.HouseID;
            queryEntity.ClientNum = wxUser.ClientNum;
            queryEntity.PayStatus = Convert.ToString(context.Request["PayStatus"]);//查询条件 支付状态 0：未支付 1：已支付2：申请退款3：已退款
            queryEntity.OrderStatus = Convert.ToString(context.Request["OrderStatus"]);//查询条件 订单状态 空：查询全部  1：待发货 2：已发货
            queryEntity.OrderNo = Convert.ToString(context.Request["OrderNo"]);//查询条件 订单号
            int pageIndex = Convert.ToInt32(context.Request["page"]);//查询条件 分页 第几页
            int pageSize = Convert.ToInt32(context.Request["pageSize"]); //查询条件 分页 每页数量
            List<MiniProOrderEntity> list = bus.QueryMyMiniProOrder(pageIndex, pageSize, queryEntity);
            List<MiniProOrderEntity> miniProOrders = new List<MiniProOrderEntity>();
            miniProOrders.AddRange(list);
            result.data = miniProOrders;
        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);

        }

        /// <summary>
        /// 刪除小程序商城订单
        /// </summary>
        /// <param name="context"></param>
        private void DeleteMiniProOrder(HttpContext context)
        {
            ReturnMiniProOrderEntity result = new ReturnMiniProOrderEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "删除订单";
            log.UserID = wxUser.wxOpenID;
            log.Operate = "D";
            CargoWeiXinBus bus = new CargoWeiXinBus();
            CargoOrderBus orderBus = new CargoOrderBus();
            WXOrderEntity queryEntity = new WXOrderEntity();
            queryEntity.OrderNo = Convert.ToString(context.Request["OrderNo"]);
            queryEntity.OrderType = Convert.ToString(context.Request["OrderType"]);//0：系统订单 1：预订单
            //WXOrderEntity wo = bus.QueryWeixinOrderByOrderNo(queryEntity);
            List<WXOrderEntity> list = bus.QueryWeixinOrderInfo(1, 100, queryEntity);
            if (list == null)
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            if (list.Count <= 0)
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            WXOrderEntity wo = list[0];
            if (wo == null)
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            if (wo.ID.Equals(0))
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            if (wo.PayWay.Equals("0") && wo.PayStatus.Equals("1"))
            {
                result.code = 1008;
                result.msg = "订单已付款 不允许删除";
                goto ERROR;
            }
            if (!wo.OrderStatus.Equals("0"))
            {
                result.code = 1009;
                result.msg = "仓库已发货 不允许删除";
                goto ERROR;
            }

            //仓库同步缓存
            CargoHouseBus house = new CargoHouseBus();
            if (queryEntity.OrderType == "0")
            {
                List<CargoProductEntity> syncProduct = house.SyncTypeOrderNo(wo.CargoOrderNo);
                foreach (CargoProductEntity product in syncProduct)
                {
                    if (Common.IsAllSyncStock(product.HouseID, product.TypeID, "Cass"))
                    {
                        RedisHelper.HashSet("OpenSystemStockSyc", "" + product.HouseID + "_" + product.TypeID + "_" + product.ProductCode + "", product.GoodsCode);
                    }
                    if (Common.IsAllSyncStock(product.HouseID, product.TypeID, "DILE"))
                    {
                        RedisHelper.HashSet("HCYCHouseStockSyc", product.HouseID + "_" + product.TypeID + "_" + product.ProductCode, product.ProductCode);
                    }
                    if (Common.IsAllSyncStock(product.HouseID, product.TypeID, "Tuhu"))
                    {
                        RedisHelper.HashSet("TuhuStockSyc", product.HouseID + "_" + product.TypeID + "_" + product.ProductCode, product.ProductCode);
                    }
                }
                orderBus.DeleteOrderInfo(new List<CargoOrderEntity> { new CargoOrderEntity { OrderNo = wo.CargoOrderNo, OrderID = wo.OrderID, DeleteID = wxUser.wxOpenID, DeleteName = wxUser.Name, WXOrderNo = wo.OrderNo, CheckStatus = "0" } }, log);
            }
            else if (queryEntity.OrderType == "1")
            {
                orderBus.DeleteReserveOrderInfo(new List<CargoOrderEntity> { new CargoOrderEntity { OrderNo = wo.CargoOrderNo, OrderID = wo.OrderID, DeleteID = wxUser.wxOpenID, DeleteName = wxUser.Name, WXOrderNo = wo.OrderNo, CheckStatus = "0", IsReserve = 1 } }, log);
            }
            try
            {
                //250822 企业微信对删除订单不推送

                //CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wo.HouseID);
                //string tit = wo.ThrowGood.Equals("22") ? "急速达" : "次日达";
                //QySendInfoEntity send = new QySendInfoEntity();
                //send.title = tit + " 订单已删除";
                ////推送给提交人
                //send.msgType = msgType.textcard;
                //send.agentID = "1000003";//消息通知的应用
                //send.AgentSecret = "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM";
                ////send.toUser = qup.ApplyID;
                ////send.toTag = "19";//wo.HouseID.Equals("83"): "19";
                //send.toTag = houseEnt.HCYCOrderPushTagID.ToString();
                //send.content = "<div></div><div>商城订单号：" + wo.OrderNo + "</div><div>出库订单号：" + wo.CargoOrderNo + "</div><div>收货信息：" + wo.Name + " " + wo.Cellphone + "</div><div>收货地址：" + wo.Address + "</div><div>订单金额：" + wo.TotalCharge.ToString("F2") + "</div><div>请仓管人员留意勿出库！</div>";
                //send.url = "http://dlt.neway5.com/";
                //WxQYSendHelper.DLTQYPushInfo(send);
            }
            catch (ApplicationException ex)
            {

            }
        //bus.DeleteWeixinOrder(wo, log);

        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        /// <summary>
        /// 提交退款申请单
        /// </summary>
        /// <param name="context"></param>
        private void RefundApplyOrder(HttpContext context)
        {
            AppletResultData result = new AppletResultData();
            result.code = 0;
            result.msg = "提交成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser == null)
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "提交退款单";
            log.UserID = wxUser.wxOpenID;
            log.Operate = "U";
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXOrderEntity queryEntity = new WXOrderEntity();
            queryEntity.OrderNo = Convert.ToString(context.Request["OrderNo"]);
            queryEntity.RefundReason = Convert.ToString(context.Request["RefundReason"]);
            queryEntity.RefundMemo = Convert.ToString(context.Request["RefundMemo"]);
            queryEntity.RefundDate = DateTime.Now;
            queryEntity.PayStatus = "2";//申请退款
            queryEntity.ThrowGood = "22";
            WXOrderEntity wo = bus.QueryWeixinOrderByOrderNo(queryEntity);
            if (wo == null)
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            if (wo.ID.Equals(0))
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            if (wo.PayStatus.Equals("2"))
            {
                result.code = 1008;
                result.msg = "订单退款已申请";
                goto ERROR;
            }
            if (wo.PayStatus.Equals("3"))
            {
                result.code = 1008;
                result.msg = "订单已退款";
                goto ERROR;
            }
            int[] ints = { 130453, 891524, 551098, 542207 };
            if (wo.ThrowGood.Equals("23") && !ints.Contains(wo.SuppClientNum) && wo.ShareHouseID==0)
            {
                queryEntity.RefundCheckID = wxUser.wxOpenID;
                queryEntity.RefundCheckName = wxUser.Name;
                queryEntity.RefundCheckStatus = "1";
                queryEntity.RefundCheckDate = DateTime.Now;
                queryEntity.ThrowGood = "23";
            }
            bus.UpdateWxOrderRefund(queryEntity, log);
            try
            {


                //急送退款通知，推送到企业微信
                CargoHouseBus house = new CargoHouseBus();
                CargoHouseEntity houseEnt = house.QueryCargoHouseByID(wxUser.HouseID);
                string OrderStatus = "未发货";
                switch (wo.OrderStatus)
                {
                    case "0": OrderStatus = "未发货"; break;
                    case "1": OrderStatus = "未发货"; break;
                    case "2": OrderStatus = "已发货"; break;
                    case "3": OrderStatus = "已发货"; break;
                    case "4": OrderStatus = "已签收"; break;
                    default:
                        break;
                }
                string RefundReason = "";
                switch (queryEntity.RefundReason)
                {
                    case "1": RefundReason = "商品无货"; break;
                    case "2": RefundReason = "发货时间问题"; break;
                    case "3": RefundReason = "不想要了"; break;
                    case "4": RefundReason = "商品错选/多选"; break;
                    case "5": RefundReason = "地址信息错误"; break;
                    case "6": RefundReason = "商品价格太高"; break;
                    default:
                        break;
                }
                string tit = wo.ThrowGood.Equals("23") ? "次日达" : "急速达" ;
                QySendInfoEntity send = new QySendInfoEntity();
                send.title = tit + " 订单申请退款";
                //推送给提交人
                send.msgType = msgType.textcard;
                send.agentID = "1000003";//消息通知的应用
                send.AgentSecret = "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM";
                //send.toUser = qup.ApplyID;
                send.toTag = houseEnt.HCYCOrderPushTagID.ToString();
                send.content = "<div></div><div>出库仓库：" + houseEnt.Name + "</div><div>商城订单号：" + wo.OrderNo + "</div><div>出库订单号：" + wo.CargoOrderNo + "</div><div>发货状态：" + OrderStatus + "</div><div>订单金额：" + wo.TotalCharge.ToString("F2") + "</div><div>退款原因：" + RefundReason + "</div><div>退款说明：" + queryEntity.RefundMemo + "</div><div>请审核人员尽快审核！</div>";
                send.url = "http://dlt.neway5.com/";
                WxQYSendHelper.DLTQYPushInfo(send);

                if (wo.ThrowGood.Equals("23"))
                {
                    //次日达退款通知，推送到供应商关注的服务号
                    Common.SendRefundModelMsg(wo.SuppClientNum.ToString(), wo.OrderNo, wo.TotalCharge, queryEntity.RefundMemo);
                }

            }
            catch (ApplicationException ex)
            {

            }
        ERROR:
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }

        /// <summary>
        /// 确认收货完成
        /// </summary>
        /// <param name="contex"></param>
        private void setWeixinOrderOk(HttpContext context)
        {
            AppletResultData result = new AppletResultData();
            result.code = 0;
            result.msg = "确认成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken)) { result.code = 1; result.msg = "请求Token为空"; goto ERROR; }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser == null) { result.code = 1; result.msg = "Token有误"; goto ERROR; }
            if (wxUser.ID.Equals(0)) { result.code = 1; result.msg = "Token有误"; goto ERROR; }
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "确认收货";
            log.UserID = wxUser.wxOpenID;
            log.Operate = "U";
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXOrderEntity queryEntity = new WXOrderEntity();
            queryEntity.OrderNo = Convert.ToString(context.Request["OrderNo"]);
            queryEntity.OrderStatus = "4";//收货确认
            WXOrderEntity wo = bus.QueryWeixinOrderByOrderNo(queryEntity);
            if (wo == null)
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            if (wo.ID.Equals(0))
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            bus.setWeixinOrderOk(queryEntity, log);

        ERROR:
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 添加订单商品和物流评价数据
        /// </summary>
        /// <param name="context"></param>
        private void setOrderEvaluate(HttpContext context)
        {
            AppletResultData result = new AppletResultData();
            result.code = 0;
            result.msg = "评价成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken)) { result.code = 1; result.msg = "请求Token为空"; goto ERROR; }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser == null) { result.code = 1; result.msg = "Token有误"; goto ERROR; }
            if (wxUser.ID.Equals(0)) { result.code = 1; result.msg = "Token有误"; goto ERROR; }
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "订单评价";
            log.UserID = wxUser.wxOpenID;
            log.Operate = "U";
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXOrderEntity queryEntity = new WXOrderEntity();
            queryEntity.OrderNo = Convert.ToString(context.Request["OrderNo"]);
            queryEntity.GoodEvaluate = Convert.ToString(context.Request["GoodEvaluate"]);//商品评价1 2 3 4 5 颗星
            queryEntity.LogisEvaluate = Convert.ToString(context.Request["LogisEvaluate"]);//物流评价1 2 3 4 5 颗星
            queryEntity.EvaluateMemo = Convert.ToString(context.Request["EvaluateMemo"]);//评价内容
            WXOrderEntity wo = bus.QueryWeixinOrderByOrderNo(queryEntity);
            if (wo == null)
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            if (wo.ID.Equals(0))
            {
                result.code = 1007;
                result.msg = "订单不存在";
                goto ERROR;
            }
            bus.setOrderEvaluate(queryEntity, log);

        ERROR:
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }

        #endregion

        #region 收货地址管理
        /// <summary>
        /// 获取默认收货地址
        /// </summary>
        /// <param name="context"></param>
        private void GetDefaultAcceptAddressInfo(HttpContext context)
        {
            ShippingAddressListEntity result = new ShippingAddressListEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            CargoClientBus bus = new CargoClientBus();
            List<ShippingAddressInfo> shippingAddressInfos = new List<ShippingAddressInfo>();
            List<CargoClientAcceptAddressEntity> entity = bus.QueryAcceptAddress(new CargoClientAcceptAddressEntity() { ClientNum = wxUser.ClientNum, isDefault = 1 });
            if (entity.Count > 0)
            {
                foreach (var it in entity)
                {
                    shippingAddressInfos.Add(new ShippingAddressInfo
                    {
                        isDefault = it.isDefault.Equals(1) ? true : false,
                        provinceStr = it.AcceptProvince,
                        cityStr = it.AcceptCity,
                        areaStr = it.AcceptCountry,
                        linkMan = it.AcceptPeople,
                        mobile = it.AcceptCellphone,
                        address = it.AcceptProvince + it.AcceptCity + it.AcceptAddress,
                        company = it.AcceptCompany
                    });
                }
            }
            result.data = shippingAddressInfos;
        ERROR:
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 获取所有收货地址信息
        /// </summary>
        /// <param name="context"></param>
        private void GetAcceptAddressInfo(HttpContext context)
        {
            ShippingAddressListEntity result = new ShippingAddressListEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            CargoClientBus bus = new CargoClientBus();
            List<ShippingAddressInfo> shippingAddressInfos = new List<ShippingAddressInfo>();
            List<CargoClientAcceptAddressEntity> entity = bus.QueryAcceptAddress(new CargoClientAcceptAddressEntity() { ClientNum = wxUser.ClientNum });
            if (entity.Count > 0)
            {
                foreach (var it in entity)
                {
                    shippingAddressInfos.Add(new ShippingAddressInfo
                    {
                        isDefault = it.isDefault.Equals(1) ? true : false,
                        provinceStr = it.AcceptProvince,
                        cityStr = it.AcceptCity,
                        areaStr = it.AcceptCountry,
                        linkMan = it.AcceptPeople,
                        mobile = it.AcceptCellphone,
                        address = it.AcceptProvince + it.AcceptCity + it.AcceptAddress,
                        company = it.AcceptCompany
                    });
                }
            }
            result.data = shippingAddressInfos;
        ERROR:
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        #endregion

        #region 企业客户订单
        /// <summary>
        /// 我的企业客户订单详细列表
        /// </summary>
        /// <param name="context"></param>
        private void QueryMyEnterpriseOrder(HttpContext context)
        {
            ReturnMiniProOrderEntity result = new ReturnMiniProOrderEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            if (wxUser.ClientNum.Equals(0))
            {
                result.code = 1;
                result.msg = "未审核通过";
                goto ERROR;
            }
            CargoClientBus clientBus = new CargoClientBus();
            CargoWeiXinBus bus = new CargoWeiXinBus();

            CargoClientEntity cargoClient = clientBus.QueryCargoClientEntity(new CargoClientEntity { ClientNum = wxUser.ClientNum });

            if (string.IsNullOrEmpty(cargoClient.ShopCode))
            {
                result.data = new List<MiniProOrderEntity>();
                goto ERROR;
            }

            CargoOrderEntity queryEntity = new CargoOrderEntity();
            queryEntity.PayClientNum = wxUser.ClientNum;
            queryEntity.ShopCode = cargoClient.ShopCode;
            queryEntity.OrderNo = Convert.ToString(context.Request["OrderNo"]);//查询条件 订单号
            queryEntity.AwbStatus = Convert.ToString(context.Request["OrderStatus"]);//查询条件订单状态 空：查询全部  0：已下单和出库中 2：已出库 5：已签收

            queryEntity.BelongHouse = "6";//云仓订单
            queryEntity.OrderType = "4";//小程序订单
            queryEntity.OrderModel = "0";//订单不包括退货单
            int pageIndex = Convert.ToInt32(context.Request["page"]);//查询条件 分页 第几页
            int pageSize = Convert.ToInt32(context.Request["pageSize"]); //查询条件 分页 每页数量
            List<MiniProOrderEntity> list = bus.QueryMyEnterpriseOrder(pageIndex, pageSize, queryEntity);
            result.data = list;
        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);

        }

        /// <summary>
        /// 查询企业客户订单物流跟踪信息
        /// </summary>
        /// <param name="context"></param>
        private void QueryOrderLogisInfo(HttpContext context)
        {
            LogisticsInfoEntity result = new LogisticsInfoEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            CargoOrderBus bus = new CargoOrderBus();
            CargoOrderStatusEntity queryEntity = new CargoOrderStatusEntity();
            queryEntity.OrderNo = Convert.ToString(context.Request["OrderNo"]);//查询条件 订单号
            List<CargoOrderStatusEntity> list = bus.QueryOrderStatus(queryEntity);
            List<LogisticsEntity> logisticsList = new List<LogisticsEntity>();
            if (list.Count > 0)
            {
                foreach (var it in list)
                {
                    string img = string.Empty;
                    string lstatus = string.Empty;
                    switch (it.OrderStatus.Trim())
                    {
                        case "0": lstatus = "已下单"; break;
                        case "1": lstatus = "出库中"; break;
                        case "2": lstatus = "已出库"; break;
                        case "3": lstatus = "已发车"; break;
                        case "4": lstatus = "已到达"; break;
                        case "5": lstatus = "已签收"; img = it.SignImage; break;
                        case "6": lstatus = "已拣货"; break;
                        case "8": lstatus = "已接单"; break;
                        case "9": lstatus = "已提货"; break;
                        default:
                            break;
                    }
                    logisticsList.Add(new LogisticsEntity
                    {
                        OPDATE = it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss"),
                        Status = lstatus,
                        SignImage = img,
                    });
                }
            }
            result.data = logisticsList;
        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }

        #endregion

        #region 促销规则管理
        /// <summary>
        /// 查询促销规则数据
        /// </summary>
        /// <param name="context"></param>
        private void QueryRuleBankData(HttpContext context)
        {
            List<RuleBankInfo> coupons = new List<RuleBankInfo>();
            RuleBankEntity result = new RuleBankEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }

            CargoPriceBus bus = new CargoPriceBus();
            CargoRuleBankEntity queryEntity = new CargoRuleBankEntity();
            queryEntity.HouseID = string.IsNullOrEmpty(context.Request["HouseID"]) ? wxUser.HouseID : Convert.ToInt32(context.Request["HouseID"]);
            queryEntity.TypeID = string.IsNullOrEmpty(Convert.ToString(context.Request["TypeID"])) ? 0 : Convert.ToInt32(context.Request["TypeID"]);
            queryEntity.SuppClientNum = string.IsNullOrEmpty(Convert.ToString(context.Request["SuppClientNum"])) ? 0 : Convert.ToInt32(context.Request["SuppClientNum"]);
            //queryEntity.ThrowGood = Convert.ToString(context.Request["ThrowGood"]);//22 急 23 次  26  特 27 预
            List<CargoRuleBankEntity> list = bus.QueryRuleBank(queryEntity);

            //判断订单类型获取规则
            List<CargoRuleBankEntity> ruleBankList = new List<CargoRuleBankEntity>();
            if (!string.IsNullOrEmpty(context.Request["ThrowGood"]?.Trim())&&Convert.ToString(context.Request["ThrowGood"]) != "0")
            {
                foreach (var item in list)
                {
                    bool isExists = !string.IsNullOrWhiteSpace(item.ThrowGood) && item.ThrowGood.Split(',')
                           .Select(s => s.Trim())
                           .Any(s => int.TryParse(s, out int val) && val == Convert.ToInt32(context.Request["ThrowGood"]));
                    if (item.ThrowGood == "0" || isExists)
                    {
                        ruleBankList.Add(item);
                    }
                }
                if (ruleBankList.Count > 0)
                {
                    list = ruleBankList;
                }
            }


            foreach (CargoRuleBankEntity cou in list)
            {
                RuleBankInfo couponEntity = new RuleBankInfo();
                couponEntity.ID = cou.ID;
                couponEntity.Title = cou.Title;
                couponEntity.FullEntry = cou.FullEntry;
                couponEntity.CutEntry = cou.CutEntry;
                couponEntity.ServiceTime = cou.ServiceTime;
                couponEntity.SuppClientNum = cou.SuppClientNum;
                couponEntity.RuleType = cou.RuleType;
                couponEntity.IsSuperPosition = cou.IsSuperPosition;
                couponEntity.IsFollowQuantity = cou.IsFollowQuantity;
                couponEntity.ThrowGood = cou.ThrowGood;
                couponEntity.TypeID = cou.TypeID;

                coupons.Add(couponEntity);
            }
            result.data = coupons;

        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        #endregion

        #region 优惠券管理
        /// <summary>
        /// 查询我的优惠券或我的店代码下面的所有优惠券
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private void QueryCouponData(HttpContext context)
        {
            List<MiniProCouponEntity> coupons = new List<MiniProCouponEntity>();
            ReturnMiniProCouponEntity result = new ReturnMiniProCouponEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            //WriteTextLog("绑定" + DToken);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }

            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXCouponEntity queryEntity = new WXCouponEntity();
            queryEntity.WXID = wxUser.ID;
            //UseStatus传空值就是查询我的所有优惠券，传0就是查询我下面可用的优惠券
            queryEntity.UseStatus = Convert.ToString(context.Request["UseStatus"]);
            queryEntity.FromOrderNO = Convert.ToString(context.Request["FromOrderNO"]);
            queryEntity.TypeID = Convert.ToString(context.Request["TypeID"]);
            queryEntity.SuppClientNum = Convert.ToInt32(context.Request["SuppClientNum"]);
            //queryEntity.ThrowGood = Convert.ToString(context.Request["ThrowGood"]);//22 急 23 次  26  特 27 预
            int pageIndex = Convert.ToInt32(context.Request["page"]);//查询条件 分页 第几页
            int pageSize = Convert.ToInt32(context.Request["pageSize"]); //查询条件 分页 每页数量
            //List<WXCouponEntity> list = new List<WXCouponEntity>();
            //list = bus.QueryCouponData(queryEntity);
            List<WXCouponEntity> list = bus.QueryCouponData(queryEntity);
            if (list.Count <= 0)
            {
                result.code = 0;
                result.msg = "优惠券为空";
            }
            //判断订单类型获取优惠卷
            List<WXCouponEntity> couponsGoodList = new List<WXCouponEntity>();
            if (!string.IsNullOrEmpty(context.Request["ThrowGood"]?.Trim()) && Convert.ToString(context.Request["ThrowGood"]) != "0")
            {
                foreach (var item in list)
                {
                    bool isExists = !string.IsNullOrWhiteSpace(item.ThrowGood) && item.ThrowGood.Split(',')
                           .Select(s => s.Trim())
                           .Any(s => int.TryParse(s, out int val) && val == Convert.ToInt32(context.Request["ThrowGood"]));
                    if (item.ThrowGood== "0" || isExists) {
                        couponsGoodList.Add(item);
                    }
                }
                if (couponsGoodList.Count > 0)
                {
                    list = couponsGoodList;
                }
            }
            //赋值优惠卷
            List<WXCouponEntity> couponsList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            foreach (WXCouponEntity cou in couponsList)
            {
                MiniProCouponEntity couponEntity = new MiniProCouponEntity();
                couponEntity.ID = cou.ID;
                couponEntity.Money = cou.Money;
                couponEntity.StartDate = cou.StartDate.ToString("yyyy-MM-dd");
                couponEntity.EndDate = cou.EndDate.ToString("yyyy-MM-dd");
                couponEntity.UseStatus = cou.UseStatus;
                couponEntity.TypeName = cou.TypeName;
                couponEntity.IsSuperPositionName = cou.IsSuperPosition.Equals("0") ? "不可叠加" : "可叠加";
                couponEntity.CouponTypeName = cou.CouponType.Equals("0") ? "平台卷" : "供应商卷";
                couponEntity.IsFollowQuantity = cou.IsFollowQuantity.Equals("0") ? "不限制条数" : "限制条数";
                couponEntity.FollowQuantity = cou.IsFollowQuantity;
                couponEntity.SuperPositionName = cou.IsSuperPosition;
                couponEntity.ThrowGood = cou.ThrowGood;
                couponEntity.TypeID = cou.TypeID;
                couponEntity.SuppClientNum = cou.SuppClientNum.ToString();
                //if (string.IsNullOrEmpty(queryEntity.UseStatus)&& cou.UseStatus=="0") {
                //    if (DateTime.Now < cou.StartDate || DateTime.Now > cou.EndDate)
                //    {
                //        couponEntity.UseStatus = "2";
                //        //continue;
                //    }
                //}
                if (queryEntity.UseStatus != null && queryEntity.UseStatus.Equals("0"))
                {
                    if (DateTime.Now < cou.StartDate || DateTime.Now > cou.EndDate)
                    {
                        continue;
                    }
                }
                coupons.Add(couponEntity);
            }
            result.data = coupons;

        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        #endregion

        #region 小程序公告管理
        /// <summary>
        /// 小程序公告查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryMiniProNotice(HttpContext context)
        {
            NoticeEntity result = new NoticeEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }
            CargoStaticBus bus = new CargoStaticBus();

            List<NoticeListInfo> NoticeListInfo = new List<NoticeListInfo>();
            List<APPCargoNoticeEntity> noticeEntities = bus.QueryNoticeForAPP(new CargoNoticeEntity { HouseID = wxUser.HouseID, DelFlag = "0", NoticeType = "2" });

            for (int i = 0; i < noticeEntities.Count; i++)
            {
                if (i == 2) { break; }
                NoticeListInfo.Add(new NoticeListInfo
                {
                    id = noticeEntities[i].ID,
                    title = noticeEntities[i].Title,
                    NoticeUrl = noticeEntities[i].URL,
                });

            }

            NoticeInfo noticeInfo = new NoticeInfo();
            noticeInfo.dataList = NoticeListInfo;

            result.data = noticeInfo;
        ERROR:
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        #endregion

        #region 小程序购物车 Redis
        public static long GetCurrentUnixTimeSeconds()
        {
            var epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var now = DateTimeOffset.UtcNow;
            long milliseconds = (long)(now - epoch).TotalMilliseconds; // 直接取总毫秒数（13位）
            return milliseconds;
        }
        /// <summary>
        /// 新增 OR 修改
        /// </summary>
        /// <param name="context"></param>
        /// <param name="wxOpenId"></param>
        /// <param name="json"></param>
        private void InsertShoppingCartItem(HttpContext context)
        {
            NoticeEntity result = new NoticeEntity();
            string wxOpenId = Convert.ToString(context.Request["wxOpenId"]);
            string json = Convert.ToString(context.Request["json"]);
            string isAccumulation = string.IsNullOrEmpty(context.Request["IsAccumulation"]) ? "0" : Convert.ToString(context.Request["IsAccumulation"]);//是否累加  0 否 1是
            String returnString = string.Empty;
            long timestampMs = GetCurrentUnixTimeSeconds();
            var data = JsonConvert.DeserializeObject<MiniApplicationParam>(json);
            if (data != null)
            {
                var dataList = new List<MiniApplicationParam>();
                if (data.ThrowGood == null)
                {
                    result.code = -1;
                    result.msg = "未获取到订单类型";
                    json = JSON.Encode(result);
                    context.Response.Write(json);
                    return;
                }
                if (!Enum.IsDefined(typeof(CargoProductThrowGoodEnum), data.ThrowGood))
                {
                    result.code = -1;
                    result.msg = "传入类型有误";
                    json = JSON.Encode(result);
                    context.Response.Write(json);
                    return;
                }
                CargoProductThrowGoodEnum goodEnum = (CargoProductThrowGoodEnum)data.ThrowGood;
                //查询当前用户总下单数量
                var PlaceOrderList = RedisHelper.GetKeys($@"MiniProgram:ShoppingCartItem:{wxOpenId}:", _DbIndex: 4);
                if (PlaceOrderList.Count > 99)
                {
                    result.code = -1;
                    result.msg = "超出所限下单数量[99]，请清理购物车！";
                    json = JSON.Encode(result);
                    context.Response.Write(json);
                    return;
                }
                //查询现有数据购物车
                var redisList = RedisHelper.GetKeys($@"MiniProgram:ShoppingCartItem:{wxOpenId}:{goodEnum.ToString()}:", _DbIndex: 4);
                if (redisList.Count > 0)
                {
                    foreach (var rowStr in redisList)
                    {
                        var item = RedisHelper.GetStringDbIndex(rowStr, false, 4);
                        if (string.IsNullOrEmpty(item)) continue;
                        var list = JsonConvert.DeserializeObject<MiniApplicationParam>(item);
                        dataList.Add(list);
                    }
                }
                //有数据
                if (dataList.Count > 0)
                {
                    var dataItem = dataList.Where(a => a.BatchYear == data.BatchYear && a.HouseID == data.HouseID && a.ProductCode == data.ProductCode && a.SuppClientNum == data.SuppClientNum).FirstOrDefault();
                    if (dataItem != null)
                    {
                        //修改
                        data.CreateTimestampId = dataItem.CreateTimestampId;
                        if (isAccumulation == "1")
                        {
                            data.CartNumber += dataItem.CartNumber;
                        }
                        RedisHelper.SetStringDbIndex($@"MiniProgram:ShoppingCartItem:{wxOpenId}:{goodEnum.ToString()}:{dataItem.CreateTimestampId}", JsonConvert.SerializeObject(data), DbIndex_: 4);
                    }
                    else
                    {
                        //新增
                        data.CreateTimestampId = timestampMs;
                        RedisHelper.SetStringDbIndex($@"MiniProgram:ShoppingCartItem:{wxOpenId}:{goodEnum.ToString()}:{timestampMs}", JsonConvert.SerializeObject(data), DbIndex_: 4);
                    }
                }
                else
                {
                    //无数据
                    data.CreateTimestampId = timestampMs;
                    RedisHelper.SetStringDbIndex($@"MiniProgram:ShoppingCartItem:{wxOpenId}:{goodEnum.ToString()}:{timestampMs}", JsonConvert.SerializeObject(data), DbIndex_: 4);
                }
            }
            else
            {
                result.code = -1;
                result.msg = "未获取到传入数据";
                json = JSON.Encode(result);
                context.Response.Write(json);
                return;
            }
            returnString = JSON.Encode(new
            {
                code = 0,
                msg = "加入购物车成功"
            });
            context.Response.Write(returnString);
        }
        /// <summary>
        /// 修改商品详情数量等数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="wxOpenId"></param>
        /// <param name="json"></param>
        private void UpdateShoppingCartItem(HttpContext context)
        {
            String returnString = string.Empty;
            string wxOpenId = Convert.ToString(context.Request["wxOpenId"]);
            //string json = Convert.ToString(context.Request["json"]);

            string requestJson = string.Empty;
            // 关键：使用 InputStream 而非 Body
            using (Stream stream = context.Request.InputStream)
            {
                // 重置流位置到开头（防止被提前读取）
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    requestJson = reader.ReadToEnd();
                }
            }
            if (string.IsNullOrEmpty(requestJson))
            {
                returnString = JsonConvert.SerializeObject(new { code = -1, msg = "无可更改数据" });
                context.Response.Write(returnString);
                return;
            }
            long timestampMs = GetCurrentUnixTimeSeconds();
            List<MiniApplicationParam> dataList = JsonConvert.DeserializeObject<List<MiniApplicationParam>>(requestJson);
            if (dataList == null || dataList.Count == 0)
            {
                // 处理空列表的情况
                context.Response.Write(JSON.Encode(new { code = -1, msg = "未获取到有效数据" }));
                return;
            }
            foreach (var data in dataList)
            {
                if (data.CreateTimestampId == null)
                {
                    returnString = JSON.Encode(new
                    {
                        code = -1,
                        msg = "未获取到订单ID"
                    });
                    context.Response.Write(returnString);
                    return;
                }
                if (data.ThrowGood == null)
                {
                    returnString = JSON.Encode(new
                    {
                        code = -1,
                        msg = "未获取到订单类型"
                    });
                    context.Response.Write(returnString);
                    return;
                }
                if (!Enum.IsDefined(typeof(CargoProductThrowGoodEnum), data.ThrowGood))
                {
                    returnString = JSON.Encode(new
                    {
                        code = -1,
                        msg = "传入类型有误"
                    });
                    context.Response.Write(returnString);
                    return;
                }
                CargoProductThrowGoodEnum goodEnum = (CargoProductThrowGoodEnum)data.ThrowGood;
                //查询现有数据
                var rediDataItem = RedisHelper.GetStringDbIndex($@"MiniProgram:ShoppingCartItem:{wxOpenId}:{goodEnum.ToString()}:{data.CreateTimestampId}", DbIndex_: 4);
                //var dataItem = JsonConvert.DeserializeObject<MiniApplicationParam>(rediDataItem);

                if (rediDataItem != null)
                {
                    RedisHelper.SetStringDbIndex($@"MiniProgram:ShoppingCartItem:{wxOpenId}:{goodEnum.ToString()}:{data.CreateTimestampId}", JsonConvert.SerializeObject(data), DbIndex_: 4);
                }
                else
                {
                    returnString = JSON.Encode(new
                    {
                        code = -1,
                        msg = "未查询到数据"
                    });
                    context.Response.Write(returnString);
                    return;
                }
            }

            returnString = JSON.Encode(new
            {
                code = 0,
                msg = ""
            });
            context.Response.Write(returnString);
        }
        /// <summary>
        /// 商品详情删除
        /// </summary>
        /// <param name="context"></param>
        /// <param name="wxOpenId"></param>
        /// <param name="CreateTimestampId">详情ID 可空。</param>
        private void DeleteShoppingCartItem(HttpContext context)
        {
            String returnString = string.Empty;
            string wxOpenId = Convert.ToString(context.Request["wxOpenId"]);
            //string json = Convert.ToString(context.Request["json"]);


            string requestJson = string.Empty;
            // 关键：使用 InputStream 而非 Body
            using (Stream stream = context.Request.InputStream)
            {
                // 重置流位置到开头（防止被提前读取）
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    requestJson = reader.ReadToEnd();
                }
            }

            if (string.IsNullOrEmpty(requestJson))
            {
                //删除客户所有购物车数据
                //RedisHelper.DeleteKeys($@"MiniProgram:ShoppingCartItem:{wxOpenId}:", _DbIndex: 4);
                returnString = JSON.Encode(new
                {
                    code = -1,
                    msg = "无可删除数据"
                });
                context.Response.Write(returnString);
                return;
            }
            else
            {
                List<MiniApplicationParam> dataList = JsonConvert.DeserializeObject<List<MiniApplicationParam>>(requestJson);
                foreach (var item in dataList)
                {
                    CargoProductThrowGoodEnum goodEnum = (CargoProductThrowGoodEnum)item.ThrowGood;
                    RedisHelper.DeleteKeys($@"MiniProgram:ShoppingCartItem:{wxOpenId}:{goodEnum.ToString()}:{item.CreateTimestampId}", _DbIndex: 4);
                }
            }

            returnString = JSON.Encode(new
            {
                code = 0,
                msg = "删除购物车成功"
            });
            context.Response.Write(returnString);
        }

        private void GetMiniShoppingCartItems(HttpContext context)
        {
            string wxOpenId = Convert.ToString(context.Request["wxOpenId"]);
            String returnString = string.Empty;
            var dataList = new List<MiniApplicationParam>();
            var redisList = RedisHelper.GetKeys($@"MiniProgram:ShoppingCartItem:{wxOpenId}:", _DbIndex: 4);
            if (redisList.Count > 0)
            {
                foreach (var rowStr in redisList)
                {
                    var item = RedisHelper.GetStringDbIndex(rowStr, false, 4);
                    if (string.IsNullOrEmpty(item)) continue;
                    var list = JsonConvert.DeserializeObject<MiniApplicationParam>(item);
                    dataList.Add(list);
                }
            }
            //查询规则信息

            //优惠卷信息
            
            returnString = JSON.Encode(new
            {
                code = 0,
                msg = "",
                list = dataList,
            });
            context.Response.Write(returnString);
        }
        #endregion

        #region 预收款明细
        private void GetClientPreRecordList(HttpContext context)
        {
            ReturnPreRecordListEntity result = new ReturnPreRecordListEntity();
            result.code = 0;
            result.msg = "成功";
            string DToken = Convert.ToString(context.Request["token"]);
            if (string.IsNullOrEmpty(DToken))
            {
                result.code = 1;
                result.msg = "请求Token为空";
                goto ERROR;
            }
            WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            if (wxUser.ID.Equals(0))
            {
                result.code = 1;
                result.msg = "Token有误";
                goto ERROR;
            }

            string ClientNum = Convert.ToString(context.Request["ClientNum"]);
            int page = Convert.ToInt32(context.Request["page"]);
            int pageSize = Convert.ToInt32(context.Request["pageSize"]);

            if (string.IsNullOrEmpty(ClientNum))
            {
                result.code = 1;
                result.msg = $@"未获取到传入数据[ClientNum:{ClientNum}]";
                goto ERROR;
            }

            CargoClientBus clientBus = new CargoClientBus();
            List<CargoClientPreRecordEntity> clientPre = new List<CargoClientPreRecordEntity>();
            var dataList = clientBus.GetClientPreRecordList(page, pageSize, new CargoClientPreRecordEntity { ClientNum = Convert.ToInt32(ClientNum) });
            List<CargoClientPreRecordEntity> awbList = (List<CargoClientPreRecordEntity>)dataList["rows"];
            foreach (CargoClientPreRecordEntity cli in awbList)
            {
                CargoClientPreRecordEntity clientPreEntity = new CargoClientPreRecordEntity();
                clientPreEntity.RecordTypeStr = cli.RecordType.Equals("0") ? "充值余额" : cli.RecordType.Equals("1") ? "订单支出" : "退货收入";
                clientPreEntity.RecordType = cli.RecordType;// 0 收入  1 支出  2 退收
                clientPreEntity.ExID = cli.ExID; //订单号/报销单号
                clientPreEntity.Money = cli.RecordType.Equals("1") ? -cli.Money : cli.Money;//金额
                clientPreEntity.OPDATE = cli.OP_DATE.ToString("yyyy-MM-dd HH:mm");//操作时间
                clientPreEntity.ClientNum = cli.ClientNum;
                clientPre.Add(clientPreEntity);
            }
            result.data = clientPre;

        ERROR:
            //JSON
            String returnString = JSON.Encode(result);
            context.Response.Write(returnString);
        }
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            #region 缓存
            string[] serverlist = ConfigurationSettings.AppSettings["memcachedServer"].Split('/');
            SockIOPool pool = SockIOPool.GetInstance(ConfigurationSettings.AppSettings["PoolName"]);
            pool.SetServers(serverlist);
            pool.InitConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["InitConnections"]);//连接池初始容量
            pool.MinConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MinConnections"]);//最小容量
            pool.MaxConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MaxConnections"]);//最大容量
            pool.SocketConnectTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketConnectTimeout"]);//数据读取超时时间
            pool.SocketTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketTimeout"]);//Socket连接超时时间
            pool.MaintenanceSleep = Convert.ToInt64(ConfigurationSettings.AppSettings["MaintenanceSleep"]);//线程池维护线程之间的休眠时间
            pool.Failover = Convert.ToBoolean(ConfigurationSettings.AppSettings["Failover"]);//使用缓存服务器自动切换功能，当一台服务器死了可以自动切换到另外一台查找缓存

            pool.Nagle = Convert.ToBoolean(ConfigurationSettings.AppSettings["Nagle"]);//禁用Nagle算法
            pool.Initialize();
            mc.PoolName = ConfigurationSettings.AppSettings["PoolName"];
            mc.EnableCompression = true;
            mc.CompressionThreshold = 10240;
            #endregion

            context.Response.ContentType = "text/plain";
            string cmd = context.Request["cmd"];
            MethodInfo Method = this.GetType().GetMethod(cmd, BindingFlags.NonPublic | BindingFlags.Instance);//通过反射机制,直接对应到相应的方法
            if (Method != null)
            {
                Method.Invoke(this, new object[] { context });
            }
            else
            {
                context.Response.Write("传入参数不正确");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
    public class MiniApplicationParam : CargoProductEntity
    {
        public long? CreateTimestampId { get; set; }
        public int? ThrowGood { get; set; }
        public string WxOpenid { get; set; }
        public int IsMyHouseStock { get; set; }
        public bool IsMyHouseStockBool { get; set; }

        public decimal NextDayLogisFee { get; set; }

        public string PickUpAddress { get; set; }


        public int PromotionType { get; set; }


        public int StoreNum { get; set; }



        public int CategoryId { get; set; }

        public int ClickNum { get; set; }

        public bool HasAddition { get; set; }

        public int Id { get; set; }

        public decimal ListPrice { get; set; }
        public int MinBuyNumber { get; set; }

        public decimal MinPrice { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public int NumberOrders { get; set; }

        public int NumberSells { get; set; }

        public decimal OriginalPrice { get; set; }

        public string Pic { get; set; }

        public string Stores { get; set; }

        public string StoresStr { get; set; }

        public int CartNumber { get; set; }

        public decimal Proportion { get; set; }
        public bool Checked { get; set; }
    }
}