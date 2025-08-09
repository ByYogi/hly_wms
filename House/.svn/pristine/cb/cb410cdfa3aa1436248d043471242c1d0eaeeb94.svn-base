using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier
{
    public partial class Default : System.Web.UI.Page
    {
        public string un = string.Empty;
        public string pw = string.Empty;
        public bool isDebug = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "用户登录：" + Common.GetSystemName();

                HttpCookie Cookie = CookiesHelper.GetCookie("UserLoginInfo");
                if (Cookie != null)
                {
                    un = Cookie.Values["loginid"];
                    pw = Cookie.Values[un];
                }
#if DEBUG
                isDebug = true;
#endif
            }
        }
    }
}