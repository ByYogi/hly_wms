using House.Business.Cargo;
using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using House.Entity.House;
using House.Manager.Cargo;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.Record.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Cargo
{
    /// <summary>
    /// 静态类
    /// </summary>
    public static class Common
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
        public static decimal GetHCYCTodayOrderTransitFee()
        {
            decimal TodayOrderTransitFee = 0M;
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["TodayOrderTransitFee"]))
            {
                TodayOrderTransitFee = Convert.ToDecimal(ConfigurationSettings.AppSettings["TodayOrderTransitFee"]);
            }
            return TodayOrderTransitFee;
        }
        /// <summary>
        /// 狄乐汽服商户号ID
        /// </summary>
        /// <returns></returns>
        public static string GetHCYCMachID_DLQF()
        {
            return ConfigurationSettings.AppSettings["HCYCMachID_DLQF"];
        }
        /// <summary>
        /// 慧采云仓微信支付APIV3密钥key
        /// </summary>
        /// <returns></returns>
        public static string GetHCYCWxPayAPIV3Key()
        {
            return ConfigurationSettings.AppSettings["HCYCWxPayAPIV3Key"];
        }
        /// <summary>
        /// 慧采云仓小程序支付证书序列号
        /// </summary>
        /// <returns></returns>
        public static string GetHCYCWxPaySerialNumber()
        {
            return ConfigurationSettings.AppSettings["HCYCWxPaySerialNumber"];
        }
        public static string GetHCYCMachID()
        {
            //HCYCMachID_DLQF狄乐汽服商户号ID
            return ConfigurationSettings.AppSettings["HCYCMachID_DLQF"];
        }
        public static string GetHCYCWxPayKey()
        {
            return ConfigurationSettings.AppSettings["HCYCWxPayKey"];
        }
        public static string GetHCYCWxPayTranUrl()
        {
            return ConfigurationSettings.AppSettings["HCYCWxPayTranUrl"];
        }
        public static string GetHCYCAppID()
        {
            return ConfigurationSettings.AppSettings["HCYCAppID"];
        }
        public static string GetHCYCAppSecret()
        {
            return ConfigurationSettings.AppSettings["HCYCAppSecret"];
        }
        public static string GetGTMCFile()
        {
            return ConfigurationSettings.AppSettings["GTMCFile"];
        }
        public static string GetContiUrl()
        {
            return ConfigurationSettings.AppSettings["ContiUrl"];
        }
        public static string GetContiOrderCheckSync()
        {
            return ConfigurationSettings.AppSettings["ContiOrderCheckSync"];
        }
        /// <summary>
        /// 马牌系统接口Continoise
        /// </summary>
        /// <returns></returns>
        public static string GetContinoise()
        {
            return ConfigurationSettings.AppSettings["Continoise"];
        }
        /// <summary>
        /// 湖北仓库马牌系统接口Continoise
        /// </summary>
        /// <returns></returns>
        public static string GetContinoiseHouseID3()
        {
            return ConfigurationSettings.AppSettings["ContinoiseHouseID3"];
        }
        /// <summary>
        /// 马牌系统接口ContiappSecret
        /// </summary>
        /// <returns></returns>
        public static string GetContiappSecret()
        {
            return ConfigurationSettings.AppSettings["ContiappSecret"];
        }
        /// <summary>
        /// 湖北仓库马牌系统接口ContiappSecret
        /// </summary>
        /// <returns></returns>
        public static string GetContiappSecretHouseID3()
        {
            return ConfigurationSettings.AppSettings["ContiappSecretHouseID3"];
        }
        /// <summary>
        /// 马牌系统接口Key
        /// </summary>
        /// <returns></returns>
        public static string GetContiappKey()
        {
            return ConfigurationSettings.AppSettings["ContiappKey"];
        }
        /// <summary>
        /// 湖北仓库马牌系统接口Key
        /// </summary>
        /// <returns></returns>
        public static string GetContiappKeyHouseID3()
        {
            return ConfigurationSettings.AppSettings["ContiappKeyHouseID3"];
        }
        /// <summary>
        /// 好来运系统API
        /// </summary>
        /// <returns></returns>
        public static string GetHLYAPI()
        {
            return ConfigurationSettings.AppSettings["HLYAPI"];
        }
        /// <summary>
        /// 电脑端保存照片文件
        /// </summary>
        /// <param name="imgFile"></param>
        /// <param name="modifyFileName"></param>
        /// <param name="IsChangeName"></param>
        public static void SaveComputerPic(HttpPostedFile imgFile, ref string modifyFileName, bool IsChangeName)
        {
            string imgname = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            string[] fs = imgFile.FileName.Split('.');
            if (IsChangeName)
            {
                modifyFileName = imgname;
                #region 保存图片
                switch (fs[fs.Length - 1].ToUpper().Trim())
                {
                    case "WPS": modifyFileName += ".wps"; break;
                    case "TXT": modifyFileName += ".txt"; break;
                    case "DOC": modifyFileName += ".doc"; break;
                    case "DOCX": modifyFileName += ".docx"; break;
                    case "XLS": modifyFileName += ".xls"; break;
                    case "XLSX": modifyFileName += ".xlsx"; break;
                    case "PPT": modifyFileName += ".ppt"; break;
                    case "PPTX": modifyFileName += ".pptx"; break;
                    case "PDF": modifyFileName += ".pdf"; break;
                    case "JPG": modifyFileName += ".jpg"; break;
                    case "GIF": modifyFileName += ".gif"; break;
                    case "PNG": modifyFileName += ".png"; break;
                    case "BMP": modifyFileName += ".bmp"; break;
                    case "JPEG": modifyFileName += ".jpeg"; break;
                    case "ZIP": modifyFileName += ".zip"; break;
                    case "RAR": modifyFileName += ".rar"; break;
                    default: modifyFileName += ".txt"; break;
                }
                #endregion
            }
            else
            {
                modifyFileName = imgname = imgFile.FileName;
            }


            string savePath = System.Web.HttpContext.Current.Server.MapPath("../upload/Approve/" + modifyFileName);
            imgFile.SaveAs(savePath);
        }
        /// <summary>
        /// 获取微信 公众服务号的Token 
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static string GetWeixinToken(string appID, string appSecret)
        {
            string token = string.Empty;
            //string token = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appid, appsecret);
            try
            {
                token = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appID, appSecret);
                Senparc.Weixin.MP.Entities.GetCallBackIpResult bip = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetCallBackIp(token);
                if (bip.errcode.Equals("40001"))
                {
                    //WriteTextLog(bip.errcode.ToString());
                    Senparc.Weixin.MP.Containers.AccessTokenContainer.Register(appID, appSecret, "DLTWxToken");
                    token = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appID, appSecret);
                }
            }
            catch (Exception ex)
            {
                //WriteTextLog("Token:" + ex.Message);
                Senparc.Weixin.MP.Containers.AccessTokenContainer.Register(appID, appSecret, "DLTWxToken");
                token = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(appID, appSecret);
            }
            return token;
        }
        /// <summary>
        /// 接受迪乐泰支付成功通知的列表
        /// </summary>
        /// <returns></returns>
        public static string GetdltPayOrderSuccessNotice()
        {
            return ConfigurationSettings.AppSettings["dltPayOrderSuccessNotice"];
        }
        /// <summary>
        /// 接受迪乐泰下单成功通知的列表
        /// </summary>
        /// <returns></returns>
        public static string GetdltGetOrderNotice()
        {
            return ConfigurationSettings.AppSettings["dltGetOrderNotice"];
        }
        /// <summary>
        /// 湖北仓库接受商城下单通知
        /// </summary>
        /// <returns></returns>
        public static string GetdltGetHuBeiOrderNotice()
        {
            return ConfigurationSettings.AppSettings["dltGetHuBeiOrderNotice"];
        }
        /// <summary>
        /// 西安迪乐泰接受商城下单 通知
        /// </summary>
        /// <returns></returns>
        public static string GetdltGetXiAnOrderNotice()
        {
            return ConfigurationSettings.AppSettings["dltGetXiAnOrderNotice"];
        }
        /// <summary>
        /// 西安迪乐泰接受商城下单 通知
        /// </summary>
        /// <returns></returns>
        public static string GetdltGetMeiZhouOrderNotice()
        {
            return ConfigurationSettings.AppSettings["dltGetMeiZhouOrderNotice"];
        }
        /// <summary>
        /// 广州仓库迪乐泰 接受商城下单通知
        /// </summary>
        /// <returns></returns>
        public static string GetdltGetGuangZhouOrderNotice()
        {
            return ConfigurationSettings.AppSettings["dltGetGuangZhouOrderNotice"];
        }
        /// <summary>
        /// 海南仓库 接受商城下单通知
        /// </summary>
        /// <returns></returns>
        public static string GetdltGetHaiNanOrderNotice()
        {
            return ConfigurationSettings.AppSettings["dltGetHaiNanOrderNotice"];
        }
        /// <summary>
        /// 揭阳仓库商城 下单通知
        /// </summary>
        /// <returns></returns>
        public static string GetdltGetJieYangOrderNotice()
        {
            return ConfigurationSettings.AppSettings["dltGetJieYangOrderNotice"];
        }
        /// <summary>
        /// 四川 仓库商城 下单通知
        /// </summary>
        /// <returns></returns>
        public static string GetdltGetSiChuanOrderNotice()
        {
            return ConfigurationSettings.AppSettings["dltGetSiChuanOrderNotice"];
        }
        /// <summary>
        /// APP开发者平台
        /// </summary>
        /// <returns></returns>
        public static string GetdltOpenAPPID()
        {
            return ConfigurationSettings.AppSettings["dltOpenAPPID"];
        }
        public static string GetdltOpenAppSecret()
        {
            return ConfigurationSettings.AppSettings["dltOpenAppSecret"];
        }
        /// <summary>
        /// 获取迪乐泰中国微信服务号的Token
        /// </summary>
        /// <returns></returns>
        public static string GetdltToken()
        {
            return ConfigurationSettings.AppSettings["dltToken"];
        }
        public static string GetdltTransfer_Url()
        {
            return ConfigurationSettings.AppSettings["dltTransfer_Url"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetdltAPPID()
        {
            return ConfigurationSettings.AppSettings["dltAPPID"];
        }
        public static string GetdltSalesModelID()
        {
            return ConfigurationSettings.AppSettings["dltSalesModelID"];
        }
        public static string GetdltBillModelID()
        {
            return ConfigurationSettings.AppSettings["dltBillModelID"];
        }
        public static string GetdltServiceToUrl()
        {
            return ConfigurationSettings.AppSettings["dltServiceToUrl"];
        }
        public static string GetdltServiceBillToUrl()
        {
            return ConfigurationSettings.AppSettings["dltServiceBillToUrl"];
        }
        /// <summary>
        /// 服务号供应商获取天数
        /// </summary>
        /// <returns></returns>
        public static string GetSerViceSupplierDay()
        {
            return ConfigurationSettings.AppSettings["SerViceSupplierDay"];
        }
        public static string GetdltAppSecret()
        {
            return ConfigurationSettings.AppSettings["dltAppSecret"];
        }
        public static string GetdltEncodingAESKey()
        {
            return ConfigurationSettings.AppSettings["dltEncodingAESKey"];
        }
        /// <summary>
        /// 微信企业号Token
        /// </summary>
        /// <returns></returns>
        public static string GetQYToken()
        {
            return ConfigurationSettings.AppSettings["Token"];
        }
        /// <summary>
        /// 微信企业号AesKey
        /// </summary>
        /// <returns></returns>
        public static string GetQYEncodingAESKey()
        {
            return ConfigurationSettings.AppSettings["EncodingAESKey"];
        }
        /// <summary>
        /// 微信企业号CorpID
        /// </summary>
        /// <returns></returns>
        public static string GetQYCorpID()
        {
            return ConfigurationSettings.AppSettings["CorpID"];
        }
        /// <summary>
        /// 获取回调URL路径
        /// </summary>
        /// <returns></returns>
        public static string GetRedirect_Url()
        {
            return ConfigurationSettings.AppSettings["redirect_Url"];
        }

        /// <summary>
        /// 微信企业号CorpSecret
        /// </summary>
        /// <returns></returns>
        public static string GetQYCorpSecret()
        {
            return ConfigurationSettings.AppSettings["CorpSecret"];
        }
        /// <summary>
        /// 获取系统名称
        /// </summary>
        public static string GetSystemName()
        {
            return ConfigurationSettings.AppSettings["SystemName"];
        }
        /// <summary>
        /// 获取系统域名
        /// </summary>
        /// <returns></returns>
        public static string GetSystemDomain()
        {
            return ConfigurationSettings.AppSettings["SystemDomain"];
        }
        /// <summary>
        /// 获取系统版本
        /// </summary>
        /// <returns></returns>
        public static string GetSystemVersion()
        {
            return ConfigurationSettings.AppSettings["SystemVersion"];
        }
        /// <summary>
        /// 获取系统版本
        /// </summary>
        /// <returns></returns>
        public static string GetMemcacheInfo()
        {
            return ConfigurationSettings.AppSettings["memcache"];
        }
        /// <summary>
        /// 获取狄乐汽服快速配送范围
        /// </summary>
        /// <returns></returns>
        public static int GetFastDeliveryRange()
        {
            return string.IsNullOrEmpty(ConfigurationSettings.AppSettings["FastDeliveryRange"]) ? 0 : Convert.ToInt32(ConfigurationSettings.AppSettings["FastDeliveryRange"]);
        }
        /// <summary>
        /// 获取狄乐汽服快速配送注释
        /// </summary>
        /// <returns></returns>
        public static string GetFastDeliveryNote()
        {
            return ConfigurationSettings.AppSettings["FastDeliveryNote"];
        }
        /// <summary>
        /// 员工资料 保存照片文件
        /// </summary>
        /// <param name="imgFile"></param>
        /// <param name="modifyFileName"></param>
        /// <param name="IsSaveHLY">是否保存好来运服务器</param>
        public static void SaveUserPic(HttpPostedFile imgFile, ref string FileName, ref string FilePath)
        {
            string imgname = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            string[] fs = imgFile.FileName.Split('.');
            string originExtension = Path.GetExtension(FileName);
            FileName = imgname;
            #region 保存图片
            switch (fs[fs.Length - 1].ToUpper().Trim())
            {
                case "WPS": FileName += ".wps"; break;
                case "TXT": FileName += ".txt"; break;
                case "DOC": FileName += ".doc"; break;
                case "DOCX": FileName += ".docx"; break;
                case "XLS": FileName += ".xls"; break;
                case "XLSX": FileName += ".xlsx"; break;
                case "PPT": FileName += ".ppt"; break;
                case "PPTX": FileName += ".pptx"; break;
                case "PDF": FileName += ".pdf"; break;
                case "JPG": FileName += ".jpg"; break;
                case "GIF": FileName += ".gif"; break;
                case "PNG": FileName += ".png"; break;
                case "BMP": FileName += ".bmp"; break;
                case "JPEG": FileName += ".jpeg"; break;
                case "ZIP": FileName += ".zip"; break;
                case "RAR": FileName += ".rar"; break;
                default: FileName += originExtension; break;
            }
            #endregion
            if (string.IsNullOrEmpty(FilePath))
            {
                FilePath = Path.Combine("../upload/ClientFile/", FileName);
            }
            else
            {
                FilePath = Path.Combine(FilePath, FileName);
            }
            string savePath = System.Web.HttpContext.Current.Server.MapPath(FilePath);
            imgFile.SaveAs(savePath);
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="imgFile"></param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="FilePath">文件路径</param>
        public static void SavePic(HttpPostedFile imgFile, string SavePath, ref string FileName, ref string FilePath)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(SavePath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(SavePath));//不存在就创建文件夹
            }
            if (!SavePath.EndsWith("/"))
            {
                SavePath += "/";
            }
            string imgname = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            string[] fs = imgFile.FileName.Split('.');
            FileName = imgname;
            #region 保存图片
            switch (fs[fs.Length - 1].ToUpper().Trim())
            {
                case "WPS": FileName += ".wps"; break;
                case "TXT": FileName += ".txt"; break;
                case "DOC": FileName += ".doc"; break;
                case "DOCX": FileName += ".docx"; break;
                case "XLS": FileName += ".xls"; break;
                case "XLSX": FileName += ".xlsx"; break;
                case "PPT": FileName += ".ppt"; break;
                case "PPTX": FileName += ".pptx"; break;
                case "PDF": FileName += ".pdf"; break;
                case "JPG": FileName += ".jpg"; break;
                case "GIF": FileName += ".gif"; break;
                case "PNG": FileName += ".png"; break;
                case "BMP": FileName += ".bmp"; break;
                case "JPEG": FileName += ".jpeg"; break;
                case "ZIP": FileName += ".zip"; break;
                case "RAR": FileName += ".rar"; break;
                default:
                    return;
                    break;
            }
            #endregion
            FilePath = SavePath + FileName;
            string savePath = System.Web.HttpContext.Current.Server.MapPath(SavePath + FileName);
            imgFile.SaveAs(savePath);
        }
        /// <summary>
        /// 获取系统名称和版本号
        /// </summary>
        /// <returns></returns>
        public static string GetSystemNameAndVersion()
        {
            return ConfigurationSettings.AppSettings["SystemName"] + " " + ConfigurationSettings.AppSettings["SystemVersion"];
        }
        /// <summary>
        /// 获取统一账号平台的API
        /// </summary>
        public static string GethouseAPI()
        {
            return ConfigurationSettings.AppSettings["houseAPI"];
        }
        /// <summary>
        /// 获取迪乐泰销售部门代码
        /// </summary>
        /// <returns></returns>
        public static string GetDLTDepCode()
        {
            return ConfigurationSettings.AppSettings["dltDepCode"];
        }
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUserIP(HttpRequest request)
        {
            //另外一种取电脑IP的方法，通过Request
            string x = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string y = request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(x))
            {
                return y;
            }
            return x;
            //一种取电脑IP的方法，通过System.Net.Dns
            //IPAddress[] ip = Dns.GetHostAddresses(Dns.GetHostName());
            //if (ip.Length > 0)
            //{
            //    return ip[0].ToString();
            //}
        }

        /// <summary>
        /// 加密密码返回加密后的密文
        /// 【1】前两位与第七，八位进行替换
        /// 【2】转换成字节数据，将salt添加至未位
        /// 【3】用SHA256加密，返回
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncodePassword(string password)
        {
            string passOne = password.Substring(0, 2);
            string passTwo = password.Substring(6, 2);
            string passThree = password.Substring(2, 4);
            string passFour = password.Substring(8, password.Length - 8);
            string result = passTwo + passThree + passOne + passFour;

            byte[] pass = Encoding.Unicode.GetBytes(result);
            //byte[] saltByte = Convert.FromBase64String(salt);
            //byte[] dst = new byte[pass.Length + saltByte.Length];
            byte[] returnPassword = null;

            //Buffer.BlockCopy(pass, 0, dst, 0, pass.Length);
            //Buffer.BlockCopy(saltByte, 0, dst, pass.Length, saltByte.Length);

            SHA256 sha = SHA256Cng.Create();
            //SHA256 sha = new SHA256Managed();
            returnPassword = sha.ComputeHash(pass);
            sha.Clear();

            return Convert.ToBase64String(returnPassword);
        }
        /// <summary>
        /// 获取系统日期，带星期
        /// </summary>
        public static string GetSystemDate()
        {
            return DateTime.Today.ToLongDateString() + " 星期" + getWeekday(DateTime.Today.DayOfWeek);
        }

        /// <summary>
        /// 返回星期几
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string getWeekday(DayOfWeek d)
        {
            string result;
            switch (d)
            {
                case DayOfWeek.Friday:
                    result = "五";
                    break;
                case DayOfWeek.Monday:
                    result = "一";
                    break;
                case DayOfWeek.Saturday:
                    result = "六";
                    break;
                case DayOfWeek.Sunday:
                    result = "天";
                    break;
                case DayOfWeek.Thursday:
                    result = "四";
                    break;
                case DayOfWeek.Tuesday:
                    result = "二";
                    break;
                case DayOfWeek.Wednesday:
                    result = "三";
                    break;
                default:
                    result = "一";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 返回当前日期，格式是：yyMMdd
        /// </summary>
        /// <returns></returns>
        public static string GetNowDate()
        {
            return DateTime.Now.ToString("yyMMdd");
        }
        /// <summary>
        /// 生成随机的四位数字 返回字符类型，不够四位前面补0
        /// </summary>
        /// <returns></returns>
        public static string GetRandomFourNumString()
        {
            string result = string.Empty;
            Random rd = new Random((int)DateTime.Now.Ticks);
            int sd = rd.Next(1, 9999);
            switch (sd.ToString().Length)
            {
                case 1:
                    result += "000" + sd.ToString();
                    break;
                case 2:
                    result += "00" + sd.ToString();
                    break;
                case 3:
                    result += "0" + sd.ToString();
                    break;
                default:
                    result = sd.ToString();
                    break;
            }
            return result;
        }
        /// <summary>
        /// 生成随机的四位数字
        /// </summary>
        /// <returns></returns>
        public static int GetRandomFourNumInt()
        {
            int sd = 1000;

            string key = DateTime.Now.ToString("yyyyMMdd");

            if (!RedisHelper.HashExists("HouseMoveOrderNo", key))
            {
                //不存在缓存
                RedisHelper.HashSet("HouseMoveOrderNo", key, "1000");
            }
            else
            {
                sd = Convert.ToInt32(RedisHelper.HashGet("HouseMoveOrderNo", key));
                RedisHelper.HashSet("HouseMoveOrderNo", key, (sd + 1).ToString());
                sd = sd + 1;
            }

            return sd;
        }
        /// <summary>
        /// 生成随机的六位数字
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSixNum()
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            int sd = rd.Next(100000, 999999);
            return sd;
        }
        /// <summary>
        /// 生成客户编码 随机的不重复的6位数字
        /// </summary>
        /// <returns></returns>
        public static int GetClientNum()
        {
            int sd = GetRandomSixNum();
            CargoClientBus bus = new CargoClientBus();
            if (bus.IsExistCargoClientNum(new CargoClientEntity { ClientNum = sd }))
            {
                sd = GetClientNum();
            }
            return sd;
        }
        /// <summary>  
        /// 转半角的函数(DBC case)  
        /// </summary>  
        /// <param name="input">任意字符串</param>  
        /// <returns>半角字符串</returns>  
        ///<remarks>  
        ///全角空格为12288，半角空格为32  
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248  
        ///</remarks>  
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;

                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)

                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 地球表面 计算两个坐标点的直线距离 采用Haversine公式
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns>公里</returns>
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double EarthRadius = 6371; // 地球半径，单位为公里
            double deltaLat = ToRadians(lat2 - lat1);
            double deltaLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadius * c;
        }
        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        #region 一个用hash实现的加密解密方法
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string EncryptStrByHash(string src)
        {
            if (src.Length == 0)
            {
                return "";
            }
            byte[] HaKey = System.Text.Encoding.ASCII.GetBytes((src + "Test").ToCharArray());
            byte[] HaData = new byte[20];
            HMACSHA1 Hmac = new HMACSHA1(HaKey);
            CryptoStream cs = new CryptoStream(Stream.Null, Hmac, CryptoStreamMode.Write);
            try
            {
                cs.Write(HaData, 0, HaData.Length);
            }
            finally
            {
                cs.Close();
            }
            string HaResult = System.Convert.ToBase64String(Hmac.Hash).Substring(0, 16);
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(HaResult.ToCharArray());
            byte[] RiDataBuf = System.Text.Encoding.ASCII.GetBytes(src.ToCharArray());
            byte[] EncodedBytes = { };
            MemoryStream ms = new MemoryStream();
            RijndaelManaged rv = new RijndaelManaged();
            cs = new CryptoStream(ms, rv.CreateEncryptor(RiKey, RiKey), CryptoStreamMode.Write);
            try
            {
                cs.Write(RiDataBuf, 0, RiDataBuf.Length);
                cs.FlushFinalBlock();
                EncodedBytes = ms.ToArray();
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            return HaResult + System.Convert.ToBase64String(EncodedBytes);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string DecrypStrByHash(string src)
        {
            if (src.Length < 40) return "";
            byte[] SrcBytes = System.Convert.FromBase64String(src.Substring(16));
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(src.Substring(0, 16).ToCharArray());
            byte[] InitialText = new byte[SrcBytes.Length];
            RijndaelManaged rv = new RijndaelManaged();
            MemoryStream ms = new MemoryStream(SrcBytes);
            CryptoStream cs = new CryptoStream(ms, rv.CreateDecryptor(RiKey, RiKey), CryptoStreamMode.Read);
            try
            {
                cs.Read(InitialText, 0, InitialText.Length);
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            System.Text.StringBuilder Result = new System.Text.StringBuilder();
            for (int i = 0; i < InitialText.Length; ++i) if (InitialText[i] > 0) Result.Append((char)InitialText[i]);
            return Result.ToString();
        }

        #endregion
        /// <summary>
        /// 读取JSON文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetFileJson(string filepath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312")))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }
        /// <summary>
        /// 返回轮胎最高速度
        /// </summary>
        /// <param name="filepath">Json文件地址</param>
        /// <param name="speedLevel">速度级别</param>
        /// <returns></returns>
        public static int ReturnTyreSpeed(string filepath, string speedLevel)
        {
            int result = 0;
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312")))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            ArrayList rows = (ArrayList)JSON.Decode(json);
            foreach (Hashtable row in rows)
            {
                if (speedLevel.Equals(Convert.ToString(row["id"])))
                {
                    result = Convert.ToInt32(row["text"]);//最高速度
                    break;
                }
            }
            return result;
        }
        public static string GetMaxAccountNo(string hCode, int hID)
        {
            string result = string.Empty;
            CargoClientBus bus = new CargoClientBus();

            int oNum = bus.GetMaxAccountNumByCurDate(new CargoClientAccountEntity { HouseID = hID, StartDate = DateTime.Now, EndDate = DateTime.Now });
            string oStr = string.Empty;
            switch ((oNum + 1).ToString().Length)
            {
                case 1:
                    oStr = "000" + (oNum + 1).ToString();
                    break;
                case 2:
                    oStr = "00" + (oNum + 1).ToString();
                    break;
                case 3:
                    oStr = "0" + (oNum + 1).ToString();
                    break;
                default:
                    oStr = (oNum + 1).ToString();
                    break;
            }
            result = hCode + DateTime.Now.ToString("yyMMdd") + oStr;
            return result;
        }
        public static string GetMaxAccountNoNum()
        {
            string result = string.Empty;
            CargoClientBus bus = new CargoClientBus();
            string key = DateTime.Now.ToString("yyyyMMdd");
            int oNum = 0;
            if (!RedisHelper.HashExists("HouseAccountNo", key))
            {
                //不存在缓存
                int oMaxNum = bus.GetMaxHouseAccountNoNum();
                RedisHelper.HashSet("HouseAccountNo", key, (oMaxNum + 1).ToString());

            }
            else
            {
                oNum = Convert.ToInt32(RedisHelper.HashGet("HouseAccountNo", key));
                RedisHelper.HashSet("HouseAccountNo", key, (oNum + 1).ToString());
            }

            string oStr = string.Empty;
            switch ((oNum + 1).ToString().Length)
            {
                case 1:
                    oStr = "000" + (oNum + 1).ToString();
                    break;
                case 2:
                    oStr = "00" + (oNum + 1).ToString();
                    break;
                case 3:
                    oStr = "0" + (oNum + 1).ToString();
                    break;
                default:
                    oStr = (oNum + 1).ToString();
                    break;
            }
            result = "B" + DateTime.Now.ToString("yyMMdd") + oStr;
            return result;
        }
        public static string GetMaxPurchaseOrderNum()
        {
            string result = string.Empty;
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            string key = DateTime.Now.ToString("yyyyMMdd");
            int oNum = 0;
            if (!RedisHelper.HashExists("HousePurchaseOrderNo", key))
            {
                //不存在缓存
                int oMaxNum = bus.GetMaxPurchaseOrderNum();
                RedisHelper.HashSet("HousePurchaseOrderNo", key, (oMaxNum + 1).ToString());

            }
            else
            {
                oNum = Convert.ToInt32(RedisHelper.HashGet("HousePurchaseOrderNo", key));
                RedisHelper.HashSet("HousePurchaseOrderNo", key, (oNum + 1).ToString());
            }

            string oStr = string.Empty;
            switch ((oNum + 1).ToString().Length)
            {
                case 1:
                    oStr = "000" + (oNum + 1).ToString();
                    break;
                case 2:
                    oStr = "00" + (oNum + 1).ToString();
                    break;
                case 3:
                    oStr = "0" + (oNum + 1).ToString();
                    break;
                default:
                    oStr = (oNum + 1).ToString();
                    break;
            }
            result = "P" + DateTime.Now.ToString("yyMMdd") + oStr;
            return result;
        }
        /// <summary>
        /// 获取当前仓库当前日期的最大顺序号
        /// </summary>
        /// <param name="hID"></param>
        /// <param name="hCode"></param>
        /// <returns></returns>
        public static string GetMaxOrderNumByCurrentDate(int hID, string hCode, out int OrderNum, int nextNum = 0)
        {
            string result = string.Empty;
            CargoOrderBus bus = new CargoOrderBus();

            //string key = "91_GZ_20250107";//91_GZ_20250107
            //string key = hID.ToString() + "_" + hCode + "_" + DateTime.Now.ToString("yyyyMMdd");
            //int oNum = 0;
            //if (!RedisHelper.HashExists("HouseOrderNo", key))
            //{
            //    //不存在缓存
            //    int oMaxNum = bus.GetMaxOrderNumByCurrentDate(new CargoOrderEntity { HouseID = hID, StartDate = DateTime.Now, EndDate = DateTime.Now }) + nextNum;
            //    RedisHelper.HashSet("HouseOrderNo", key, (oMaxNum+1).ToString());

            //}
            //else
            //{
            //    oNum = Convert.ToInt32(RedisHelper.HashGet("HouseOrderNo", key));
            //    RedisHelper.HashSet("HouseOrderNo", key, (oNum + 1).ToString());
            //}


            //string key = "91_20250107";//91_20250107  仓库ID+当天日期
            string key = hID.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd");
            int oNum = 0;
            if (!RedisHelper.HashExists("HouseOrderNo", key))
            {
                //不存在缓存
                int oMaxNum = bus.GetMaxOrderNumByCurrentDate(new CargoOrderEntity { HouseID = hID, StartDate = DateTime.Now, EndDate = DateTime.Now }) + nextNum;
                RedisHelper.HashSet("HouseOrderNo", key, (oMaxNum + 1).ToString());

            }
            else
            {
                oNum = Convert.ToInt32(RedisHelper.HashGet("HouseOrderNo", key));
                RedisHelper.HashSet("HouseOrderNo", key, (oNum + 1).ToString());
            }

            //int oNum = bus.GetMaxOrderNumByCurrentDate(new CargoOrderEntity { HouseID = hID, StartDate = DateTime.Now, EndDate = DateTime.Now }) + nextNum;
            string oStr = string.Empty;
            switch ((oNum + 1).ToString().Length)
            {
                case 1:
                    oStr = "00" + (oNum + 1).ToString();
                    break;
                case 2:
                    oStr = "0" + (oNum + 1).ToString();
                    break;
                default:
                    oStr = (oNum + 1).ToString();
                    break;
            }
            result = hCode.Trim() + DateTime.Now.ToString("yyMMdd") + oStr;
            OrderNum = oNum + 1;
            return result;
        }
        /// <summary>
        /// 获取当前仓库当前日期的最大顺序号
        /// </summary>
        /// <param name="hID"></param>
        /// <param name="hCode"></param>
        /// <returns></returns>
        public static string GetMaxBundleOrderNumByCurrentDate(int hID, string hCode, out int OrderNum, int nextNum = 0)
        {
            string result = string.Empty;
            CargoOrderBus bus = new CargoOrderBus();

            int oNum = bus.GetMaxBundleOrderNumByCurrentDate(new CargoOrderBundleEntity { HouseID = hID, StartDate = DateTime.Now, EndDate = DateTime.Now }) + nextNum;
            string oStr = string.Empty;
            switch ((oNum + 1).ToString().Length)
            {
                case 1:
                    oStr = "00" + (oNum + 1).ToString();
                    break;
                case 2:
                    oStr = "0" + (oNum + 1).ToString();
                    break;
                default:
                    oStr = (oNum + 1).ToString();
                    break;
            }
            result = hCode.Trim() + DateTime.Now.ToString("yyMMdd") + oStr;
            OrderNum = oNum + 1;
            return result;
        }
        public static string GetMaxPickNumByCurrentDate(int hID, string hCode, out int PickNum, int nextNum = 0)
        {
            string result = string.Empty;
            CargoOrderBus bus = new CargoOrderBus();

            int oNum = bus.GetMaxPickNumByCurrentDate(new CargoOrderEntity { HouseID = hID, StartDate = DateTime.Now, EndDate = DateTime.Now }) + nextNum;
            string oStr = string.Empty;
            switch ((oNum + 1).ToString().Length)
            {
                case 1:
                    oStr = "00" + (oNum + 1).ToString();
                    break;
                case 2:
                    oStr = "0" + (oNum + 1).ToString();
                    break;
                default:
                    oStr = (oNum + 1).ToString();
                    break;
            }
            result = "P" + hCode.Trim() + DateTime.Now.ToString("yyMMdd") + oStr;
            PickNum = oNum + 1;
            return result;
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
        /// 判断字符串是否与指定正则表达式匹配
        /// 要验证的字符串
        /// 正则表达式
        /// 验证通过返回true
        public static bool IsMatch(string input, string regularExp)
        {
            return Regex.IsMatch(input, regularExp);
        }
        /// 

        /// 验证非负整数（正整数 + 0）
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsUnMinusInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.UnMinusInteger);
        }
        /// 验证正整数
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsPlusInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.PlusInteger);
        }
        /// 验证非正整数（负整数 + 0） 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsUnPlusInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.UnPlusInteger);
        }
        /// 验证负整数
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsMinusInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.MinusInteger);
        }
        /// 验证整数
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsInt(string input)
        {
            return Regex.IsMatch(input, RegularExp.Integer);
        }
        /// 验证非负浮点数（正浮点数 + 0）
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsUnMinusFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.UnMinusFloat);
        }
        /// 验证正浮点数
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsPlusFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.PlusFloat);
        }
        /// 验证非正浮点数（负浮点数 + 0）
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsUnPlusFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.UnPlusFloat);
        }
        /// 验证负浮点数
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsMinusFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.MinusFloat);
        }
        /// 验证浮点数
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsFloat(string input)
        {
            return Regex.IsMatch(input, RegularExp.Float);
        }

        /// 验证由26个英文字母组成的字符串
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsLetter(string input)
        {
            return Regex.IsMatch(input, RegularExp.Letter);
        }
        /// 验证由中文组成的字符串
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsChinese(string input)
        {
            return Regex.IsMatch(input, RegularExp.Chinese);
        }
        /// 

        /// 验证由26个英文字母的大写组成的字符串
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsUpperLetter(string input)
        {
            return Regex.IsMatch(input, RegularExp.UpperLetter);
        }

        /// 

        /// 验证由26个英文字母的小写组成的字符串
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsLowerLetter(string input)
        {
            return Regex.IsMatch(input, RegularExp.LowerLetter);
        }

        /// 

        /// 验证由数字和26个英文字母组成的字符串
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsNumericOrLetter(string input)
        {
            return Regex.IsMatch(input, RegularExp.NumericOrLetter);
        }

        /// 

        /// 验证由数字组成的字符串
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, RegularExp.Numeric);
        }
        /// 
        /// 验证由数字和26个英文字母或中文组成的字符串
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsNumericOrLetterOrChinese(string input)
        {
            return Regex.IsMatch(input, RegularExp.NumbericOrLetterOrChinese);
        }

        /// 

        /// 验证由数字、26个英文字母或者下划线组成的字符串
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsNumericOrLetterOrUnderline(string input)
        {
            return Regex.IsMatch(input, RegularExp.NumericOrLetterOrUnderline);
        }

        /// 

        /// 验证email地址
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsEmail(string input)
        {
            return Regex.IsMatch(input, RegularExp.Email);
        }

        /// 

        /// 验证URL
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsUrl(string input)
        {
            return Regex.IsMatch(input, RegularExp.Url);
        }

        /// 

        /// 验证电话号码
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsTelephone(string input)
        {
            return Regex.IsMatch(input, RegularExp.Telephone);
        }

        /// 

        /// 验证手机号码
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsMobile(string input)
        {
            return Regex.IsMatch(input, RegularExp.Mobile);
        }

        /// 

        /// 通过文件扩展名验证图像格式
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsImageFormat(string input)
        {
            return Regex.IsMatch(input, RegularExp.ImageFormat);
        }

        /// 

        /// 验证IP
        /// 
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsIP(string input)
        {
            return Regex.IsMatch(input, RegularExp.IP);
        }
        /// 验证日期（YYYY-MM-DD）
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsDate(string input)
        {
            return Regex.IsMatch(input, RegularExp.Date);
        }
        /// 验证日期和时间（YYYY-MM-DD HH:MM:SS）
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsDateTime(string input)
        {
            return Regex.IsMatch(input, RegularExp.DateTime);
        }
        /// 验证颜色（#ff0000）
        /// 要验证的字符串
        /// 验证通过返回true
        public static bool IsColor(string input)
        {
            return Regex.IsMatch(input, RegularExp.Color);
        }
        /// <summary> 
        /// 取得HTML中所有图片的 URL。 
        /// </summary> 
        /// <param name="sHtmlText">HTML代码</param> 
        /// <returns>图片的URL列表</returns> 
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (System.Text.RegularExpressions.Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        /// <summary>
        /// 解密判断
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得运单总收入的百分比
        /// </summary>
        /// <returns></returns>
        public static decimal GetProfitPCT()
        {
            return Convert.ToDecimal(ConfigurationSettings.AppSettings["ProfitPCT"]);
        }
        /// <summary>
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串
        /// </summary>
        /// <param name="CnStr">汉字字符串</param>
        /// <returns>相对应的汉语拼音首字母串</returns>
        public static string GetSpellCode(string CnStr)
        {
            string strTemp = "";
            for (int i = 0; i < CnStr.Length; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }
            return strTemp;
        }
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="str">被截取的字符串</param>
        /// <param name="len">所截取的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int len)
        {
            if (str == null || str.Length == 0 || len <= 0)
            {
                return string.Empty;
            }
            int l = str.Length;
            #region 计算长度
            int clen = 0;
            while (clen < len && clen < l)
            {
                //每遇到一个中文，则将目标长度减一。
                if ((int)str[clen] > 128) { len--; }
                clen++;
            }
            #endregion

            if (clen < l)
            {
                return str.Substring(0, clen) + "...";
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="CnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>
        private static string GetCharSpellCode(string CnChar)
        {

            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回首字母

            if (ZW.Length == 1)
            {

                return CutString(CnChar.ToUpper(), 1);

            }
            else
            {

                // get the array of byte from the single char

                int i1 = (short)(ZW[0]);

                int i2 = (short)(ZW[1]);

                iCnChar = i1 * 256 + i2;

            }

            // iCnChar match the constant

            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {

                return "A";

            }

            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {

                return "B";

            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {

                return "C";

            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {

                return "D";

            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {

                return "E";

            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {

                return "F";

            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {

                return "G";

            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {

                return "H";

            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {

                return "J";

            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {

                return "K";

            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {

                return "L";

            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {

                return "M";

            }
            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {

                return "N";

            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {

                return "O";

            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {

                return "P";

            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {

                return "Q";

            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {

                return "R";

            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {

                return "S";

            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {

                return "T";

            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {

                return "W";

            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {

                return "X";

            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {

                return "Y";

            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {

                return "Z";

            }
            else

                return ("?");

        }

        #region 服务号模板推送通用接口
        public static string ServiceRequests(string RequestUrl, string RequestMethod)
        {
            string RequestUrls = RequestUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestUrls);
            request.Method = RequestMethod;
            request.ContentType = "textml;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string jsonData = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            string jsonString = jsonData;
            return jsonString;
        }

        /// <summary>
        /// 服务号 退款推送
        /// </summary>
        /// <param name="clientNum">门店编码(客户编码)</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="amount">金额</param>
        /// <param name="remark">备注</param>
        public static void SendRefundModelMsg(string clientNum, string orderNo, decimal amount, string remark)
        {
            CargoWeiXinBus cargoWeiXinBus = new CargoWeiXinBus();

            string strReturn = "";
            try
            {
                //获取 access_token
                string AppID = GetdltAPPID();
                string AppSecret = GetdltAppSecret();
                string AccessTokenUrl = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID, AppSecret);//构建请求的url，获取access_token
                string AccessToken = ServiceRequests(AccessTokenUrl, "get");
                JObject json1 = JObject.Parse(AccessToken);
                var accessToken = json1["access_token"];

                //LogHelper.WriteLog("服务号模板信息推送 access_token:" + accessToken);
                if (accessToken != null)
                {
                    //获取供应商数据 ，获取推送者的 openid
                    var SupplierList = cargoWeiXinBus.QueryWxSupplierOpenID(clientNum);
                    for (int j = 0; j < SupplierList.Count; j++)
                    {
                        string openId = SupplierList[j].wxOpenID;
                        //string templateId = GetdltBillModelID(); //设置的模板ID
                        string templateId = "Qkn4a3wwECN-0xeMWh1hBHbHipQfcH8-cW1Afv8wTE4"; //设置的模板ID
                        string toUrl = "http://dlt.neway5.com/Weixin/wxRefundConfirmation.aspx?orderno=" + orderNo; //跳转url
                        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
                        //设置推送的模板，包括推送的openid、模板ID-templateId 、推送的信息：dt1.Rows[i]["XXX"]等
                        string temp = "{\"touser\": \"" + openId + "\"," +

                                      "\"template_id\": \"" + templateId + "\", " +

                                      "\"topcolor\": \"#00ffea\", " +

                                       (string.IsNullOrWhiteSpace(toUrl) ? "" : ("\"url\": \"" + toUrl + "\", ")) +

                                      "\"data\": " +

                                      "{\"number2\": {\"value\": \"" + (orderNo) + "\"}," +

                                      "\"amount4\": { \"value\": \"" + (amount) + "\"}," +

                                      "\"const13\": {\"value\": \"" + (remark) + "\" }," +

                                      "\"time3\": { \"value\": \"" + Convert.ToDateTime(DateTime.Now) + "\"}" +

                                      "}}";
                        //核心：进行推送请求，并返回相应的反馈信息，如：{"errcode":0,"errmsg":"ok","msgid":XXXXX}
                        string results = GetResponseData(temp, url);
                        strReturn = SupplierList[j].Name + "——" + openId + "——" + "推送成功";
                        Console.WriteLine(strReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
                //Console.WriteLine(strReturn);
            }

        }


        /// <summary>
        /// 服务号 下单推送
        /// </summary>
        /// <param name="clientNum">门店编码(客户编码)</param>
        /// <param name="logisAwbNo">运单号</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="skuName">商品名称</param>
        /// <param name="amount">金额</param>
        /// <param name="number">数量</param>
        public static void SendRePlaceAnOrderMsg(string clientNum, string logisAwbNo, string orderNo, string skuName, decimal amount, int number)
        {
            CargoWeiXinBus cargoWeiXinBus = new CargoWeiXinBus();

            string strReturn = "";
            try
            {
                //获取 access_token
                string AppID = GetdltAPPID();
                string AppSecret = GetdltAppSecret();
                string AccessTokenUrl = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID, AppSecret);//构建请求的url，获取access_token
                string AccessToken = ServiceRequests(AccessTokenUrl, "get");
                JObject json1 = JObject.Parse(AccessToken);
                var accessToken = json1["access_token"];

                //LogHelper.WriteLog("服务号模板信息推送 access_token:" + accessToken);
                if (accessToken != null)
                {
                    //获取供应商数据 ，获取推送者的 openid
                    var SupplierList = cargoWeiXinBus.QueryWxSupplierOpenID(clientNum);
                    for (int j = 0; j < SupplierList.Count; j++)
                    {
                        string openId = SupplierList[j].wxOpenID;
                        //string templateId = GetdltBillModelID(); //设置的模板ID
                        string templateId = "2OJWkM7g_VqdofGGTBQ5vnbdJSNSCfTO_LLY4KG4iv0"; //设置的模板ID
                        //string toUrl = ToUrl; //跳转url
                        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
                        //设置推送的模板，包括推送的openid、模板ID-templateId 、推送的信息：dt1.Rows[i]["XXX"]等
                        string temp = "{\"touser\": \"" + openId + "\"," +

                                      "\"template_id\": \"" + templateId + "\", " +

                                      "\"topcolor\": \"#00ffea\", " +

                                      //(string.IsNullOrWhiteSpace(toUrl)?"":("\"url\": \"" + toUrl + "\", ")) +

                                      "\"data\": " +

                                      "{\"character_string1\": {\"value\": \"" + (logisAwbNo) + "\"}," +

                                      "\"character_string7\": {\"value\": \"" + (orderNo) + "\"}," +

                                      "\"thing6\": {\"value\": \"" + (skuName) + "\"}," +

                                      "\"character_string12\": { \"value\": \"" + (number) + "\"}," +

                                      "\"amount8\": { \"value\": \"" + (amount) + "\"}}}";
                        //核心：进行推送请求，并返回相应的反馈信息，如：{"errcode":0,"errmsg":"ok","msgid":XXXXX}
                        string results = GetResponseData(temp, url);
                        strReturn = SupplierList[j].Name + "——" + openId + "——" + "推送成功";
                        Console.WriteLine(strReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
                //Console.WriteLine(strReturn);
            }

        }


        /// <summary>
        /// 服务号 订单返货
        /// </summary>
        /// <param name="clientNum">门店编码(客户编码)</param>
        /// <param name="logisAwbNo">运单号</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="skuName">商品名称</param>
        /// <param name="amount">金额</param>
        /// <param name="number">数量</param>
        public static void SendOrderReturnMsg(string clientNum, string clientName, string ProductName, int number)
        {
            CargoWeiXinBus cargoWeiXinBus = new CargoWeiXinBus();

            string strReturn = "";
            try
            {
                //获取 access_token
                string AppID = GetdltAPPID();
                string AppSecret = GetdltAppSecret();
                string AccessTokenUrl = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID, AppSecret);//构建请求的url，获取access_token
                string AccessToken = ServiceRequests(AccessTokenUrl, "get");
                JObject json1 = JObject.Parse(AccessToken);
                var accessToken = json1["access_token"];

                //LogHelper.WriteLog("服务号模板信息推送 access_token:" + accessToken);
                if (accessToken != null)
                {
                    //获取供应商数据 ，获取推送者的 openid
                    var SupplierList = cargoWeiXinBus.QueryWxSupplierOpenID(clientNum);
                    for (int j = 0; j < SupplierList.Count; j++)
                    {
                        string openId = SupplierList[j].wxOpenID;
                        //string templateId = GetdltBillModelID(); //设置的模板ID
                        string templateId = "LdjdsRLFBatmmWQyWfw_VrSXLA8MuqpkKeauFRQ167g"; //设置的模板ID
                        //string toUrl = ToUrl; //跳转url
                        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
                        //设置推送的模板，包括推送的openid、模板ID-templateId 、推送的信息：dt1.Rows[i]["XXX"]等
                        string temp = "{\"touser\": \"" + openId + "\"," +

                                      "\"template_id\": \"" + templateId + "\", " +

                                      "\"topcolor\": \"#00ffea\", " +

                                      //(string.IsNullOrWhiteSpace(toUrl)?"":("\"url\": \"" + toUrl + "\", ")) +

                                      "\"data\": " +

                                      "{\"thing1\": {\"value\": \"" + (clientName) + "\"}," +

                                      "\"thing2\": {\"value\": \"" + (ProductName) + "\"}," +

                                      "\"character_string3\": { \"value\": \"" + (number) + "\"}}}";
                        //核心：进行推送请求，并返回相应的反馈信息，如：{"errcode":0,"errmsg":"ok","msgid":XXXXX}
                        string results = GetResponseData(temp, url);
                        strReturn = SupplierList[j].Name + "——" + openId + "——" + "推送成功";
                        Console.WriteLine(strReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
                //Console.WriteLine(strReturn);
            }

        }


        /// <summary>
        /// 服务号 账单推送
        /// </summary>
        public static void SendBillPushMsg(string clientNum, string AccountNO, decimal amount)
        {
            CargoWeiXinBus cargoWeiXinBus = new CargoWeiXinBus();

            string strReturn = "";
            try
            {
                //获取 access_token
                string AppID = GetdltAPPID();
                string AppSecret = GetdltAppSecret();
                string AccessTokenUrl = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID, AppSecret);//构建请求的url，获取access_token
                string AccessToken = ServiceRequests(AccessTokenUrl, "get");
                JObject json1 = JObject.Parse(AccessToken);
                var accessToken = json1["access_token"];

                //LogHelper.WriteLog("服务号模板信息推送 access_token:" + accessToken);
                if (accessToken != null)
                {
                    //获取供应商数据 ，获取推送者的 openid
                    var SupplierList = cargoWeiXinBus.QueryWxSupplierOpenID(clientNum);
                    for (int j = 0; j < SupplierList.Count; j++)
                    {
                        //if (SupplierList[j].wxOpenID != "oluY8vksVHoHiGsYBBsCwnPpNz38") continue;
                        string openId = SupplierList[j].wxOpenID;
                        //string templateId = GetdltBillModelID(); //设置的模板ID
                        string templateId = "rn8QmHgeDN9UCJ74sVOnMfi7QbtxihjHv3lYypCOP3w"; //设置的模板ID
                        //string toUrl = GetdltServiceBillToUrl(); //跳转url
                        string toUrl = "http://dlt.neway5.com/Weixin/wxDetail.aspx?accountno=" + AccountNO; //跳转url
                        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
                        //设置推送的模板，包括推送的openid、模板ID-templateId 、推送的信息：dt1.Rows[i]["XXX"]等
                        string temp = "{\"touser\": \"" + openId + "\"," +

                                      "\"template_id\": \"" + templateId + "\", " +

                                      "\"topcolor\": \"#00ffea\", " +

                                       "\"url\": \"" + toUrl + "\", " +

                                      "\"data\": " +

                                      "{\"character_string11\": {\"value\": \"" + (AccountNO) + "\"}," +

                                      "\"amount2\": { \"value\": \"" + (amount) + "\"}," +

                                      "\"time3\": {\"value\": \"" + Convert.ToDateTime(DateTime.Now) + "\" }}}";
                        //核心：进行推送请求，并返回相应的反馈信息，如：{"errcode":0,"errmsg":"ok","msgid":XXXXX}
                        string results = GetResponseData(temp, url);
                        strReturn = SupplierList[j].Name + "——" + openId + "——" + "推送成功";
                        Console.WriteLine(strReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
                //Console.WriteLine(strReturn);
            }

        }


        /// <summary>
        /// 服务号 付款成功推送
        /// </summary>
        /// <param name="clientNum">门店编码(客户编码)</param>
        /// <param name="payerName">付款方</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="amount">金额</param>
        /// <param name="paymentDate">付款日期</param>
        public static void SendPaymentSuccessfulModelMsg(string clientNum, string payerName, string orderNo, decimal amount, DateTime paymentDate)
        {
            CargoWeiXinBus cargoWeiXinBus = new CargoWeiXinBus();

            string strReturn = "";
            try
            {
                //获取 access_token
                string AppID = GetdltAPPID();
                string AppSecret = GetdltAppSecret();
                string AccessTokenUrl = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID, AppSecret);//构建请求的url，获取access_token
                string AccessToken = ServiceRequests(AccessTokenUrl, "get");
                JObject json1 = JObject.Parse(AccessToken);
                var accessToken = json1["access_token"];

                //LogHelper.WriteLog("服务号模板信息推送 access_token:" + accessToken);
                if (accessToken != null)
                {
                    //获取供应商数据 ，获取推送者的 openid
                    var SupplierList = cargoWeiXinBus.QueryWxSupplierOpenID(clientNum);
                    for (int j = 0; j < SupplierList.Count; j++)
                    {
                        string openId = SupplierList[j].wxOpenID;
                        //string templateId = GetdltBillModelID(); //设置的模板ID
                        string templateId = "Qkn4a3wwECN-0xeMWh1hBHbHipQfcH8-cW1Afv8wTE4"; //设置的模板ID
                        string toUrl = ""; //跳转url
                        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
                        //设置推送的模板，包括推送的openid、模板ID-templateId 、推送的信息：dt1.Rows[i]["XXX"]等
                        string temp = "{\"touser\": \"" + openId + "\"," +

                                      "\"template_id\": \"" + templateId + "\", " +

                                      "\"topcolor\": \"#00ffea\", " +

                                       (string.IsNullOrWhiteSpace(toUrl) ? "" : ("\"url\": \"" + toUrl + "\", ")) +

                                      "\"data\": " +

                                      "{\"character_string7\": {\"value\": \"" + (orderNo) + "\"}," +

                                      "\"amount3\": {\"value\": \"" + (payerName) + "\" }," +

                                      "\"thing2\": { \"value\": \"" + (amount) + "\"}," +

                                      "\"time4\": { \"value\": \"" + paymentDate + "\"}" +

                                      "}}";
                        //核心：进行推送请求，并返回相应的反馈信息，如：{"errcode":0,"errmsg":"ok","msgid":XXXXX}
                        string results = GetResponseData(temp, url);
                        strReturn = SupplierList[j].Name + "——" + openId + "——" + "推送成功";
                        Console.WriteLine(strReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
                //Console.WriteLine(strReturn);
            }

        }

        //获取请求的数据并返回相应的提示信息
        public static string GetResponseData(string JSONData, string Url)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "json";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);
            //声明一个HttpWebRequest请求
            request.Timeout = 90000;
            //设置连接超时时间
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string strResult = streamReader.ReadToEnd();
            streamReceive.Dispose();
            streamReader.Dispose();
            return strResult;
        }
        #endregion

        #region redis 文件夹通用

        public static string HuiCaiCommon = "HuiCai:";
        public static string HuiCaiCloudWarehouse = HuiCaiCommon+"HuiCaiCloudWarehouseExport:";

        #endregion

        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public static SystemUserEntity CurrUser { get; set; }

        /// <summary>
        /// 遍历DataTable，null转换
        /// </summary>
        /// <param name="dataTable"></param>
        public static void ReplaceDBNullWithDefault(DataTable dataTable)
        {
            // 遍历所有行
            foreach (DataRow row in dataTable.Rows)
            {
                // 遍历所有列 
                foreach (DataColumn column in dataTable.Columns)
                {
                    // 检查是否为 DBNull
                    if (row[column] == DBNull.Value)
                    {
                        // 根据列数据类型替换
                        switch (Type.GetTypeCode(column.DataType))
                        {
                            case TypeCode.String:
                                row[column] = string.Empty;  // 字符串替换为空
                                break;
                            case TypeCode.Int32:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                                row[column] = 0;  // 数值类型替换为0
                                break;
                            // 其他类型可扩展
                            default:
                                row[column] = string.Empty;  // 默认替换为空 
                                break;
                        }
                    }
                }
            }
        }
    }

    
}
public static class CassMallOrderStatus
{
    public const string ORDER_SENT = "已发货";
    public const string ORDER_APPROVED = "已确认";
    public const string ORDER_COMPLETED = "已完成";
    public const string ORDER_WAIT_PAYED = "等待付款";
    public const string ORDER_CANCELLED = "已取消";
    public const string ORDER_CANCELLING = "取消中";
    public const string ORDER_WAIT_APPROVED = "等待客户确认";
    public const string ORDER_CREATED = "等待商家确认";
    public const string ORDER_RETURN_APPROVE = "退货审核中";
    public const string ORDER_SPLIT = "已拆分";
    public const string ORDER_GROUP_BUYING = "预售中";
    public const string GROUP_BUYING_SUCCESS = "待付尾款";
    public const string ORDER_BREACH = "已违约";
    public const string GROUP_BUYING_FAIL = "预售失败";
    public const string ORDER_SENT_HAS_RETURN = "已发货(有退货)";
    public const string ORDER_COMPLETED_HAS_RETURN = "已完成(有退货)";
}

public struct RegularExp
{
    public const string Chinese = @"^[\u4E00-\u9FA5\uF900-\uFA2D]+$";
    public const string Color = "^#[a-fA-F0-9]{6}";
    public const string Date = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
    public const string DateTime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";
    public const string Email = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
    public const string Float = @"^(-?\d+)(\.\d+)?$";
    public const string ImageFormat = @"\.(?i:jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$";
    public const string Integer = @"^-?\d+$";
    public const string IP = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
    public const string Letter = "^[A-Za-z]+$";
    public const string LowerLetter = "^[a-z]+$";
    public const string MinusFloat = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
    public const string MinusInteger = "^-[0-9]*[1-9][0-9]*$";
    public const string Mobile = "^0{0,1}13[0-9]{9}$";
    public const string NumbericOrLetterOrChinese = @"^[A-Za-z0-9\u4E00-\u9FA5\uF900-\uFA2D]+$";
    public const string Numeric = "^[0-9]+$";
    public const string NumericOrLetter = "^[A-Za-z0-9]+$";
    public const string NumericOrLetterOrUnderline = @"^\w+$";
    public const string PlusFloat = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
    public const string PlusInteger = "^[0-9]*[1-9][0-9]*$";
    public const string Telephone = @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?";
    public const string UnMinusFloat = @"^\d+(\.\d+)?$";
    public const string UnMinusInteger = @"\d+$";
    public const string UnPlusFloat = @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$";
    public const string UnPlusInteger = @"^((-\d+)|(0+))$";
    public const string UpperLetter = "^[A-Z]+$";
    public const string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
}
