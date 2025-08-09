using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class AddMyAddress : WXBasePage
    {
        public string xm = string.Empty;
        public string dh = string.Empty;
        public string address = string.Empty;
        public string city = string.Empty;
        public string isde = string.Empty;
        public string ID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cs = Convert.ToString(Request.QueryString["ID"]);
                if (!string.IsNullOrEmpty(cs))
                {
                    string[] cl = cs.Split('/');
                    if (cl.Length > 0)
                    {
                        ID = Convert.ToString(cl[0]);
                        address = Convert.ToString(cl[1]);
                        xm = Convert.ToString(cl[2]);
                        dh = Convert.ToString(cl[3]);
                        city = Convert.ToString(cl[4]) + " " + Convert.ToString(cl[5]) + " " + Convert.ToString(cl[6]);
                        isde = Convert.ToString(cl[7]);
                    }
                }

            }
        }
    }
}