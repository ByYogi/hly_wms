using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Api
{
    public partial class ClientApi : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            string methodName = string.Empty;
            methodName = Request["method"];
            Debug.WriteLine($"{methodName} 开始请求");
            try
            {
                if (string.IsNullOrEmpty(methodName)) return;
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);

                object result = method.Invoke(this, null); // 返回的是 Task<string>

                if (result is Task task)
                {
                    await task;

                    if (method.ReturnType.IsGenericType)
                    {
                        var returnVal = ((dynamic)task).Result; // string 类型
                    }
                }
            }
            catch (Exception ex)
            {

            }
            Debug.WriteLine($"{methodName} 结束请求");
        }

        public async Task getasync()
        {
            await Task.Delay(3000);
            string resps = JsonConvert.SerializeObject(new string[] { "异步3s", "异步3s" });
            Response.Write(resps);
        }
        public void getsync()
        {
            Thread.Sleep(3000);
            string resps = JsonConvert.SerializeObject(new string[] { "同步3s", "同步3s" });
            Response.Write(resps);
        }
        public void gettest()
        {
            string resps = JsonConvert.SerializeObject(new string[] { "同步测试", "同步测试" });
            Response.Write(resps);
        }
    }
}