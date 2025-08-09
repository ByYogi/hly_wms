using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace House.webSer
{
    /// <summary>
    /// 向外系统提供用户权限查询服务
    /// userServices 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class userServices : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 用户权限查询服务 返回Json字符串权限结果
        /// </summary>
        /// <param name="userID">用户登陆名</param>
        /// <param name="systemCode">系统代码</param>
        /// <returns></returns>
        [WebMethod]
        public string queryUserPermission(string userID, string systemCode)
        {
            string result = string.Empty;
            return result;
        }

        /// <summary>
        /// 系统代码查询
        /// </summary>
        /// <param name="key">用户授权Key</param>
        /// <returns></returns>
        [WebMethod]
        public string querySystemCode(string key)
        {
            return string.Empty;
        }
    }
}
