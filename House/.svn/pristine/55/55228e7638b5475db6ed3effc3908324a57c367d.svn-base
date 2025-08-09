using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class category : WXBasePage
    {
        public int ProductTypeID = 0;
        public string searchText = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ProductTypeID"])))
            {
                ProductTypeID = Convert.ToInt32(Request["ProductTypeID"]);
                searchText = Convert.ToString(Request["searchText"]);
            }
        }
    }
}