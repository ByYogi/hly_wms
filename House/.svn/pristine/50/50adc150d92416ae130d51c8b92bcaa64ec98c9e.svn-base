using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.QY
{
    public partial class qyClientManager : QYBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoClientBus bus = new CargoClientBus();
                List<CargoClientEntity> result = bus.QueryClientAddress(new CargoClientEntity { DelFlag = "0", UserID = "1000" });
                foreach (var it in result)
                {
                    ltlShipAddress.Text += "<li class=\"mui-table-view-cell\"><a class=\"mui-navigate-right\" href=\"qyAddClient.aspx?ClientID=" + it.ClientID + "&ClientName=" + it.ClientName + "&Address=" + it.Address.Trim() + "&Cellphone=" + it.Cellphone.Trim() + "&Boss=" + it.Boss + "\" target=\"_self\"><div class=\"mui-table-cell mui-col-xs-10\"><h5 class=\"mui-ellipsis\">" + it.Address + "</h5><p class=\"mui-h6 mui-ellipsis\">" + it.Boss + "&nbsp;&nbsp;&nbsp;&nbsp;" + it.Cellphone + "&nbsp;&nbsp;(点击查看详情)</p></div></a></li>";
                }
            }
        }
    }
}