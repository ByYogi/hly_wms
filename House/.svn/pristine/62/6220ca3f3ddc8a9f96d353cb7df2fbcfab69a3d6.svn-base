using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Cargo.Weixin
{
    public class EventHandler : IHandler
    {

        /// <summary>
        /// 请求的xml
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml"></param>
        public EventHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        public string HandleRequest()
        {
            string response = string.Empty;
            EventMessage em = EventMessage.LoadFromXml(RequestXml);
            if (em != null)
            {
                switch (em.Event.ToLower())
                {
                    case ("subscribe"):
                        response = SubscribeEventHandler(em);
                        break;
                    case "click":
                        response = ClickEventHandler(em);
                        break;
                }
            }

            return response;
        }
        /// <summary>
        /// 添加关注
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string SubscribeEventHandler(EventMessage em)
        {
            //回复欢迎消息
            TextMessage tm = new TextMessage();
            tm.ToUserName = em.FromUserName;
            tm.FromUserName = em.ToUserName;
            tm.CreateTime = WeiXinService.GetNowTime();
            tm.Content = "您好，欢迎您关注迪乐泰-中国服务号！迪乐泰中国专营优科豪马、普利司通、德国马牌、万达宝通和美国固铂等知名轮胎！迪乐泰，您身边的轮胎专家！";
            WriteTextLog(tm.ToUserName + "|" + tm.FromUserName, DateTime.Now);
            return tm.GenerateContent();
        }

        public static void WriteTextLog(string strMessage, DateTime time)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"System\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + time.ToString("yyyy-MM-dd") + ".System.txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
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
        /// <summary>
        /// 点击运单查询按钮
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string QueryAwbInfo(EventMessage em)
        {
            //回复欢迎消息
            TextMessage tm = new TextMessage();
            tm.ToUserName = em.FromUserName;
            tm.FromUserName = em.ToUserName;
            tm.CreateTime = WeiXinService.GetNowTime();
            tm.Content = "请输入8位数字运单号！";
            return tm.GenerateContent();
        }
        /// <summary>
        /// 处理点击事件
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string ClickEventHandler(EventMessage em)
        {
            string result = string.Empty;
            if (em != null && em.EventKey != null)
            {
                switch (em.EventKey.ToUpper())
                {
                    case "AWBQUERY":
                        result = QueryAwbInfo(em);
                        break;
                    default:
                        result = "";
                        break;
                }
            }

            return result;
        }
    }
}