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
    public partial class ConsumeInfo : WXBasePage
    {
        public Int64 pid;
        public Int64 ProductID;
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = Convert.ToInt64(Request.QueryString["ID"]);
            ProductID = Convert.ToInt64(Request.QueryString["PID"]);
            //Int64 sid = 3;
            if (!IsPostBack)
            {
                CargoHouseBus house = new CargoHouseBus();
                CargoWeiXinBus wx = new CargoWeiXinBus();
                CargoProductEntity result = wx.QueryOnShelvesByID(pid);
                if (result != null)
                {
                    if (result.fileList.Count > 0)
                    {
                        foreach (var it in result.fileList)
                        {
                            ltlzhutu.Text += "<div class='swiper-slide'><img src='" + it.FileName + "' height='270px' /></div>";
                        }
                    }
                    ltlTitle.Text = "<h4 class='wy-media-box__title'>" + result.Title + "</h4><div class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + result.Consume + "&nbsp;积分</em><em style='color:red;font-weight:bold;'></em></div><p class='weui-media-box__desc'>正品保证 全国25座大型仓库发货 准时送达</p>";

                    ltlDetail.Text = result.Memo;
                    //ltlAddCart.Text = "<a href='javascript:addCart();' class='weui-tabbar__item yellow-color open-popup' data-target='#join_cart'><p class='promotion-foot-menu-label'>加入购物车</p></a>";
                    List<CargoContainerGoodsEntity> goods = house.QueryCargoContainerGoodsByProductID(new CargoContainerGoodsEntity { ProductID = ProductID });
                    if (goods.Count > 0)
                    {
                        ltlBuy.Text = "<a href='javascript:saveOrder();' class='weui-tabbar__item red-color open-popup'><p class='promotion-foot-menu-label'>积分兑换</p></a>";
                    }
                    else
                    {
                        ltlBuy.Text = "<a href='javascript:saveOrder();' class='weui-tabbar__item gray-color open-popup'><p class='promotion-foot-menu-label'>已兑完</p></a>";
                    }
                }
            }
        }
    }
}