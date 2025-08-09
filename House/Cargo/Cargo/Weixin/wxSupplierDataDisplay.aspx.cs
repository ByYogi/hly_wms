using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class wxSupplierDataDisplay : WXBasePage// System.Web.UI.Page
    {
        public WXUserEntity wxUser = new WXUserEntity();
        public string CurrDate { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrDate = Convert.ToString(Request["currdate"]);
            wxUser = WxUserInfo;
        }
    }
}