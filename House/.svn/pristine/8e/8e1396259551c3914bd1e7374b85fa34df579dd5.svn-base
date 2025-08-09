using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.QY
{
    //QYBasePage
    //System.Web.UI.Page
    public partial class TypeStockQuery : QYBasePage
    {
        public string hStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            hStr = "[";
            string[] vs = QyUserInfo.CargoPermisID.Split(',');
            string[] vt = QyUserInfo.CargoPermisName.Split(',');
            for (int i = 0; i < vs.Length; i++)
            {
                if (!string.IsNullOrEmpty(vs[i]))
                {
                    hStr += "{value:" + vs[i] + ",text:\'" + vt[i] + "\'},";
                }
            }
            if (hStr.Length > 1)
            {
                hStr = hStr.Substring(0, hStr.Length - 1);
            }
            hStr += "]";
        }
    }
}