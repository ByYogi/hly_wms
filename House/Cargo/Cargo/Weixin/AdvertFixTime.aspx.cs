using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class AdvertFixTime : WXBasePage
    {
        public string AdvertEndDate = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CargoWeiXinBus bus = new CargoWeiXinBus();
                //List<CargoProductShelvesEntity> result = bus.QueryShelvesData(new CargoProductShelvesEntity { SaleType = "3", ShelveStatus = "0", HouseID = WxUserInfo.HouseID });
                //if (result.Count > 0)
                //{
                //    if (result[0].AdvertEndDate > DateTime.Now)
                //    {
                //        AdvertEndDate = result[0].AdvertEndDate.ToString("yyyy-MM-dd");
                //    }
                //}
            }
        }
    }
}