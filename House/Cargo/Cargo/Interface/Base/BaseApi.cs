using House.Business.Cargo;
using House.Entity.Cargo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Cargo.Interface.Base
{
    public abstract class BaseApi: IHttpHandler
    {
        private StockApiResponse result = new StockApiResponse();
        public virtual void ProcessRequest(HttpContext context)
        {
            string expectedKey = string.Empty;
            CargoInterfaceBus bus=new CargoInterfaceBus();
            string sig = context.Request.Headers["signature"];
            //StockApiResponse result = new StockApiResponse();
            result.Success = false;
            string appKey = context.Request.Headers["appKey"];
            string apiName = context.Request.Headers["apiName"];
            string timestamp = context.Request.Headers["timestamp"];
            DateTime date = DateTime.Now;
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Convert.ToDouble(timestamp)).ToLocalTime();
            if ((date-dt)>TimeSpan.FromMinutes(15))
            {
                result.Message = "请求超时";
                goto ErrEND;
            }
            if (!bus.keyExists(new StockApiEntity() { SystemKey= appKey }))
            {
                result.Message = "错误的key";
                goto ErrEND;
            }
            if (string.IsNullOrEmpty(sig))
            {
                result.Message = "签名不能为空";
                goto ErrEND;
            }
            using (MD5 md5 = MD5.Create())
            {
                string sign = (apiName + appKey + timestamp).ToUpper();
                byte[] inputBytes = Encoding.UTF8.GetBytes(sign);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                expectedKey = sb.ToString();
            }
            if (expectedKey != sig)
            {
                // 校验失败，执行相应的操作
                result.Message = "签名错误";
                goto ErrEND;
            }
            else
            {
                // 校验成功，执行相应的操作
                HandleRequest(context);
            }
        ErrEND:
            string re = JSON.Encode(Result1);
            context.Response.Write(re);
        }

        protected abstract void HandleRequest(HttpContext context);

        public bool IsReusable => false;

        public StockApiResponse Result1 { get => result; set => result = value; }
    }
}