using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.QY
{
    public partial class qyAddClient : QYBasePage
    {
        public CargoClientEntity client = new CargoClientEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            //ltlHeader.Text = "新增客户";
            if (!IsPostBack)
            {
                string cid = Convert.ToString(Request["ClientID"]);
                if (!string.IsNullOrEmpty(cid))
                {
                    client.ClientID = Convert.ToInt64(cid);
                    client.ClientName = client.ClientShortName = Convert.ToString(Request["ClientName"]);
                    client.Address = Convert.ToString(Request["Address"]);
                    client.Cellphone = Convert.ToString(Request["Cellphone"]);
                    client.Telephone = Convert.ToString(Request["Telephone"]);
                    client.Boss = Convert.ToString(Request["Boss"]);
                    //ltlHeader.Text = "修改客户";
                }
            }
        }
    }
}