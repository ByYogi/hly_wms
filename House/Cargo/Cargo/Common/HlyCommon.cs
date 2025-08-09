using House.Business;
using House.Entity;
using House.Entity.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Cargo
{
    /// <summary>
    /// 与好来运系统对接的数据接口
    /// </summary>
    public class HlyCommon
    {
        protected readonly bool open = Convert.ToBoolean(ConfigurationSettings.AppSettings["HLYOpen"]);
        protected readonly string HLYAwbStatusURL = Convert.ToString(ConfigurationSettings.AppSettings["HLYAwbStatusURL"]);
        protected readonly string HLYToken = Convert.ToString(ConfigurationSettings.AppSettings["HLYToken"]);
        protected readonly string HLYAwbURL = Convert.ToString(ConfigurationSettings.AppSettings["HLYAwbURL"]);
        public void SaveXLCAwbNoToHLY(HlyExchangeEntity entity)
        {
            //if (string.IsNullOrEmpty(entity.HAwbNo) || string.IsNullOrEmpty(entity.HlyFiveNo))
            //{
            //    return;
            //}
            //// 

            //LogEntity log = new LogEntity();
            //log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            //log.Moudle = "客户管理";
            //log.NvgPage = "好来运单管理";
            //log.UserID = "HLY";
            //log.Operate = "A";
            //LogBus logBus = new LogBus();
            //HlyEntity en = new HlyEntity();
            //en.Token = HLYToken;
            //en.Hawbno = entity.HlyFiveNo;
            //en.Xawbno = entity.HAwbNo;
            //en.company = "新陆程";
            //string postData = "orderdata=" + JSON.Encode(en);
            //string hlyres = wxHttpUtility.SendHttpRequest("http://api.haolaiyun.com:9001/Order.ashx?action=InsertExternalAwbno", postData);
            ////string hlyres = wxHttpUtility.GetData(HLYAwbStatusURL + "&orderdata=" + JSON.Encode(entity));
            //Hashtable hlyRow = (Hashtable)JSON.Decode(hlyres);
            //if (hlyRow["state"].ToString().ToUpper().Equals("TRUE"))
            //{
            //    //保存成功
            //    log.Memo = "数据上传成功，新陆程运单号：" + entity.HAwbNo + "，好来运单号：" + entity.HlyFiveNo + "，操作时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //}
            //else
            //{
            //    //保存失败
            //    log.Memo = "数据上传失败，失败原因：" + Convert.ToString(hlyRow["errmsg"]) + "，新陆程运单号：" + entity.HAwbNo + "，好来运单号：" + entity.HlyFiveNo + "，操作时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //}
            //logBus.InsertLog(log);
        }
        /// <summary>
        /// 保存运单状态到好来运系统
        /// </summary>
        /// <param name="entity"></param>
        public void SaveAwbStatus(HlyEntity entity)
        {
            //entity.EnSafe();
            //if (open)
            //{
            //    if (string.IsNullOrEmpty(entity.Hawbno) || string.IsNullOrEmpty(entity.Xawbno))
            //    {
            //        return;
            //    }
            //    LogEntity log = new LogEntity();
            //    log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            //    log.Moudle = "营运管理";
            //    log.NvgPage = "长运配载操作";
            //    log.UserID = "HLY";
            //    //log.BelongSystem = "NW";
            //    log.Operate = "A";
            //    LogBus logBus = new LogBus();
            //    entity.Token = HLYToken;
            //    string postData = "orderdata=" + JSON.Encode(entity);
            //    string hlyres = wxHttpUtility.SendHttpRequest(HLYAwbStatusURL, postData);
            //    //string hlyres = wxHttpUtility.GetData(HLYAwbStatusURL + "&orderdata=" + JSON.Encode(entity));
            //    Hashtable hlyRow = (Hashtable)JSON.Decode(hlyres);
            //    if (hlyRow["state"].ToString().ToUpper().Equals("TRUE"))
            //    {
            //        //保存成功
            //        log.Memo = "状态保存成功，新陆程运单号：" + entity.Xawbno + "，好来运单号：" + entity.Hawbno + "，运单状态：" + entity.State + "，操作人：" + entity.Cuser + "，操作时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    }
            //    else
            //    {
            //        //保存失败
            //        log.Memo = "状态保存失败，失败原因：" + Convert.ToString(hlyRow["errmsg"]) + "，新陆程运单号：" + entity.Xawbno + "，好来运单号：" + entity.Hawbno + "，运单状态：" + entity.State + "，操作人：" + entity.Cuser + "，操作时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    }
            //    logBus.InsertLog(log);
            //}
        }
        /// <summary>
        /// 保存新陆程序单号
        /// </summary>
        /// <param name="entity"></param>
        public void SaveNewayAwb(HlyEntity entity)
        {
            //if (open)
            //{
            //    if (string.IsNullOrEmpty(entity.Hawbno) || string.IsNullOrEmpty(entity.Xawbno))
            //    {
            //        return;
            //    }
            //    LogEntity log = new LogEntity();
            //    log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            //    log.Moudle = "营运管理";
            //    log.NvgPage = "长运配载操作";
            //    log.UserID = "HLY";
            //    //log.BelongSystem = "NW";
            //    log.Operate = "A";
            //    LogBus logBus = new LogBus();
            //    entity.Token = HLYToken;
            //    string postData = "orderdata=" + JSON.Encode(entity);
            //    try
            //    {
            //        string hlyres = wxHttpUtility.SendHttpRequest(HLYAwbURL, postData);
            //        //string hlyres = wxHttpUtility.GetData(HLYAwbStatusURL + "&orderdata=" + JSON.Encode(entity));
            //        Hashtable hlyRow = (Hashtable)JSON.Decode(hlyres);
            //        if (hlyRow["state"].ToString().ToUpper().Equals("TRUE"))
            //        {
            //            //保存成功
            //            log.Memo = "运单保存成功，新陆程运单号：" + entity.Xawbno + "，好来运单号：" + entity.Hawbno + "，运单状态：" + entity.State + "，操作人：" + entity.Cuser + "，操作时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //        }
            //        else
            //        {
            //            //保存失败
            //            log.Memo = "运单保存失败，失败原因：" + Convert.ToString(hlyRow["errmsg"]) + "，新陆程运单号：" + entity.Xawbno + "，好来运单号：" + entity.Hawbno + "，运单状态：" + entity.State + "，操作人：" + entity.Cuser + "，操作时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //        }
            //        logBus.InsertLog(log);
            //    }
            //    catch (ApplicationException ex)
            //    {
            //        log.Memo = "好来运接口报错，运单号：" + entity.Xawbno;
            //        logBus.InsertLog(log);
            //    }

            //}
        }
    }
}