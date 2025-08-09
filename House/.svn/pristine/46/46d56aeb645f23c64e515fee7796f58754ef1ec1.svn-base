using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.QY
{
    public partial class qyScanOrderSign : QYBasePage
    {
        public string OrderNo = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OrderNo = Convert.ToString(Request["OrderNo"]);

            }
        }
    }
}