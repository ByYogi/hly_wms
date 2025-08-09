using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Order
{
    public partial class QuickAddOrder : BasePage
    {
        public string Un = string.Empty;
        public string Ln = string.Empty;
        public string HouseName = string.Empty;
        public string PickTitle = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Un = UserInfor.UserName.Trim();
            Ln = UserInfor.LoginName.Trim();
            HouseName = UserInfor.HouseName;
            PickTitle = UserInfor.PickTitle;

        }
    }
}