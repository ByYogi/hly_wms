
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
    public partial class mall : WXBasePage
    {
        public string AdvertEndDate = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoWeiXinBus bus = new CargoWeiXinBus();
                List<CargoProductShelvesEntity> result = bus.QueryShelvesData(new CargoProductShelvesEntity { SaleType = "3", ShelveStatus = "0", HouseID = WxUserInfo.HouseID });
                if (result.Count > 0)
                {
                    string ls = "<div class='wy-Module'><div class='wy-Module-tit' style='font-size: 16px; font-weight: bolder;'>限时促销<span id='fixT' style='font-size: 14px;'></span></div><div class='wy-Module-con'><div class='swiper-container swiper-jingxuan' style='padding-top: 34px;'><div class='swiper-wrapper'>";
                    foreach (var it in result)
                    {
                        string imgsrc = string.Empty;
                        if (it.TypeID.Equals(9)) { imgsrc = "wxPic/YKG055.jpg"; }
                        else if (it.TypeID.Equals(18)) { imgsrc = "wxPic/plst.jpg"; }
                        else if (it.TypeID.Equals(66)) { imgsrc = "wxPic/gubo.jpg"; }
                        else if (it.TypeID.Equals(34)) { imgsrc = "wxPic/mapai.jpg"; }
                        ls += "<div class='swiper-slide' style='text-align: center;'><a href='AdvertFixTime.aspx'><img src='" + imgsrc + "' /></a><div style='font-size: 13px;'>" + it.Specs + " " + it.Figure + "</div><div style='font-size: 16px; color: red; font-weight: bolder;'>¥" + it.SalePrice + "</div></div>";
                    }
                    ls += "</div><div style='top: 0; right: 10px; padding-right: 10px; bottom: auto; left: auto; text-align: right; position: absolute; z-index: 10; font-size: 12px; line-height: 34px; color: #999;'><a href='AdvertFixTime.aspx' style='color: #999;'>进入查看更多...</a></div></div></div></div>";
                    ltlLimitTimeSale.Text = ls;
                    if (result[0].AdvertEndDate > DateTime.Now)
                    {
                        AdvertEndDate = result[0].AdvertEndDate.ToString("yyyy-MM-dd");
                    }
                }
            }
        }
    }
}