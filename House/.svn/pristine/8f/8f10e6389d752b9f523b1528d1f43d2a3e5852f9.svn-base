using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tencent;

namespace Cargo.QY
{
    public partial class qyApi : System.Web.UI.Page
    {

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
        /// 企业号ID
        /// </summary>
        private string Corpid = Common.GetQYCorpID();// "wxb81ad57f0579ad8e";
        private string QYToken = Common.GetQYToken();//"qyt";
        private string QYAesKey = Common.GetQYEncodingAESKey();//"LBRqF9WaicTFfxqAAldiK4GeE2q4vRSe3vu0S9XoBLx";

        protected void Page_Load(object sender, EventArgs e)
        {
            string signature = Request.QueryString["msg_signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            string echoString = Request.QueryString["echostr"];
            if (Request.HttpMethod.ToUpper() == "GET")//验证签名
            {
                string decryptEchoString = string.Empty;
                if (CheckSignature(QYToken, signature, timestamp, nonce, Corpid, QYAesKey, echoString, ref decryptEchoString))
                {
                    if (!string.IsNullOrEmpty(decryptEchoString))
                    {
                        WriteContent(decryptEchoString);
                    }
                }
            }
            else
            {
                WriteContent("");
            }
        }
        private void WriteContent(string str)
        {
            Response.Clear();
            Response.Charset = "UTF-8";
            Response.Write(str);
            Response.End();
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="corpId"></param>
        /// <param name="encodingAESKey"></param>
        /// <param name="echostr"></param>
        /// <param name="retEchostr"></param>
        /// <returns></returns>
        public bool CheckSignature(string token, string signature, string timestamp, string nonce, string corpId, string encodingAESKey, string echostr, ref string retEchostr)
        {
            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(token, encodingAESKey, corpId);
            int result = wxcpt.VerifyURL(signature, timestamp, nonce, echostr, ref retEchostr);
            //WriteTextLog(result.ToString(), DateTime.Now);
            if (result != 0)
            {
                return false;
            }
            return true;
        }
    }

}