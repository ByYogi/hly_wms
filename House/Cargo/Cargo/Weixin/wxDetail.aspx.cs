using House.Entity.Cargo;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class wxDetail :WXBasePage// System.Web.UI.Page
    {
        public WXUserEntity wxUser = new WXUserEntity();
        public string AccountNO { get; set; } 
        protected void Page_Load(object sender, EventArgs e)
        {
            wxUser = WxUserInfo;

            AccountNO = Convert.ToString(Request["accountno"]);
        }
    }
}