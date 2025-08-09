using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace House
{
    /// <summary>
    /// 公共类
    /// </summary>
    public static class Common
    {
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
        /// 获取公司基础提成数
        /// </summary>
        /// <returns></returns>
        public static decimal GetBaseCommision()
        {
            return Convert.ToDecimal(ConfigurationSettings.AppSettings["BaseCommision"]);
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
        /// 微信企业号Token
        /// </summary>
        /// <returns></returns>
        public static string GetQYToken()
        {
            return ConfigurationSettings.AppSettings["QYToken"];
        }
        /// <summary>
        /// 微信企业号AesKey
        /// </summary>
        /// <returns></returns>
        public static string GetQYAesKey()
        {
            return ConfigurationSettings.AppSettings["QYAesKey"];
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
        /// 微信企业号CorpSecret
        /// </summary>
        /// <returns></returns>
        public static string GetQYCorpSecret()
        {
            return ConfigurationSettings.AppSettings["CorpSecret"];
        }
        /// <summary>
        /// 获取微信日志开头
        /// </summary>
        /// <returns></returns>
        public static string GetWeixinLog()
        {
            return ConfigurationSettings.AppSettings["weixinLog"];
        }
        /// <summary>
        /// 获取打印机端口名称
        /// </summary>
        /// <returns></returns>
        public static string GetPrintPort()
        {
            return ConfigurationSettings.AppSettings["POSTEK"];
        }
        /// <summary>
        /// 获取打印标签宽度
        /// </summary>
        /// <returns></returns>
        public static string GetTagWidth()
        {
            return ConfigurationSettings.AppSettings["TagWidth"];
        }
        /// <summary>
        /// 获取打印标签高度
        /// </summary>
        /// <returns></returns>
        public static string GetTagHeight()
        {
            return ConfigurationSettings.AppSettings["TagHeight"];
        }
        /// <summary>
        /// 获取打印标签黑度
        /// </summary>
        /// <returns></returns>
        public static string GetTagDarkness()
        {
            return ConfigurationSettings.AppSettings["TagDarkness"];
        }
        /// <summary>
        /// 获取打印标签打印速度
        /// </summary>
        /// <returns></returns>
        public static string GetTagSpeed()
        {
            return ConfigurationSettings.AppSettings["TagSpeed"];
        }

        /// <summary>
        /// 上传文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetInputsPath()
        {
            return ConfigurationSettings.AppSettings["sPath"];
        }
        /// <summary>
        /// 上传图片缩略图宽
        /// </summary>
        /// <returns></returns>
        public static string GetInputFileTbWidth()
        {
            return ConfigurationSettings.AppSettings["intThumbWidth"];
        }
        /// <summary>
        /// 上传图片缩略图高
        /// </summary>
        /// <returns></returns>
        public static string GetInputFileTbHeight()
        {
            return ConfigurationSettings.AppSettings["intThumbHeight"];
        }
        /// <summary>
        /// 保存在数据库中的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetInputFilePath()
        {
            return ConfigurationSettings.AppSettings["FilePath"];
        }
        /// <summary>
        /// 保存在数据库中的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetInputTBFilePath()
        {
            return ConfigurationSettings.AppSettings["TBFilePath"];
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
        /// 获取公司名称
        /// </summary>
        /// <returns></returns>
        public static string GetCompanyName()
        {
            return ConfigurationSettings.AppSettings["CompanyName"];
        }
        /// <summary>
        /// 获取当前站点城市
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentCity()
        {
            return ConfigurationSettings.AppSettings["CurrentCity"];
        }
        /// <summary>
        /// 更改当前城市站点
        /// </summary>
        public static void WriteCurrentCity(string city)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            //获取appSettings节点
            AppSettingsSection appSection = (AppSettingsSection)config.GetSection("appSettings");
            //修改appSettings节点中的元素
            appSection.Settings["CurrentCity"].Value = city.Trim();
            config.Save();
        }
        /// <summary>
        /// 返回当天日报表编码
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDayReport()
        {
            string result = string.Empty;
            string sd = DateTime.Now.ToString("yyyyMMdd");
            Random rd = new Random((int)DateTime.Now.Ticks);
            result = sd + rd.Next(1000, 9999).ToString();
            return result;
        }
        /// <summary>
        /// 生成随机的账单号
        /// </summary>
        /// <returns></returns>
        public static string GetAccountNum(string accountTag)
        {
            //string tag = ConfigurationSettings.AppSettings["AccountTag"];
            Random rd = new Random((int)DateTime.Now.Ticks);
            string aid = accountTag + rd.Next(10000000, 99999999).ToString();
            return aid;
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
    }
}