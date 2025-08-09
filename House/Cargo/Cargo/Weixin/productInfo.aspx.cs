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
    public partial class productInfo : WXBasePage
    {
        public Int64 pid;
        public decimal price = 0.00M;
        public string SaleT = "0";
        public string Assort = string.Empty;
        public string SaleType = string.Empty;
        public int TypeID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 sid = Convert.ToInt64(Request.QueryString["ID"]);
            //Int64 sid = 3;
            if (!IsPostBack)
            {
                CargoWeiXinBus wx = new CargoWeiXinBus();
                CargoProductEntity result = wx.QueryOnShelvesByID(sid);
                if (result != null)
                {
                    SaleT = result.SaleType;
                    //SaleT = "2";
                    pid = sid;
                    price = result.SalePrice;
                    Assort = result.Assort;
                    SaleType = result.SaleType;
                    TypeID = result.TypeID;
                    if (result.fileList.Count > 0)
                    {
                        foreach (var it in result.fileList)
                        {
                            ltlzhutu.Text += "<div class='swiper-slide'><img src='" + it.FileName + "'/></div>";
                        }
                    }
                    if (result.HouseID.Equals(12))
                    {
                        ltlTitle.Text = "<h4 class='wy-media-box__title'>" + result.Title + "</h4><div class='wy-pro-pri mg-tb-5'>¥<em class='num font-20'>" + result.SalePrice.ToString("F2") + "</em><em style='color:red;font-weight:bold;'></em></div><p class='weui-media-box__desc'>正品保证 全国30座大型仓库发货 准时送达</p>";
                    }
                    else
                    {
                        if (result.SaleType.Equals("1"))
                        {
                            ltlTitle.Text = "<h4 class='wy-media-box__title'>" + result.Title + "<em style='color:red;font-weight:bold;'></em></h4><div class='wy-pro-pri mg-tb-5'>¥<em class='num font-20'>" + result.SalePrice.ToString("F2") + "</em></div><p class='weui-media-box__desc'>正品保证 全国30座大型仓库发货 准时送达</p>";
                        }
                        else if (result.SaleType.Equals("3"))
                        {
                            ltlTitle.Text = "<h4 class='wy-media-box__title'>" + result.Title + "<em style='color:red;font-weight:bold;'></em></h4><div class='wy-pro-pri mg-tb-5'>¥<em class='num font-20'>" + result.SalePrice.ToString("F2") + "</em></div><p class='weui-media-box__desc'>正品保证 全国30座大型仓库发货 准时送达</p>";
                        }
                        else
                        {
                            ltlTitle.Text = "<h4 class='wy-media-box__title'>" + result.Title + "&nbsp;&nbsp;" + result.Assort + "</h4><div class='wy-pro-pri mg-tb-5'>¥<em class='num font-20'>" + result.SalePrice.ToString("F2") + "</em><em style='color:red;font-weight:bold;'></em></div><p class='weui-media-box__desc'>正品保证 全国30座大型仓库发货 准时送达</p>";
                        }
                    }
                    //ltlProduct.Text = "<div class='weui-media-box_appmsg'><div class='weui-media-box__bd'><div class='promotion-sku clear' style='font-size:13px;'>商品名称：" + result.ProductName + "&nbsp;&nbsp;&nbsp;规格：" + result.Specs + "&nbsp;&nbsp;&nbsp;花纹：" + result.Figure + "&nbsp;&nbsp;&nbsp;型号：" + result.Model + "&nbsp;&nbsp;&nbsp;尺寸：" + result.HubDiameter + "寸&nbsp;&nbsp;&nbsp;周期批次：" + result.BatchYear.ToString() + "年&nbsp;&nbsp;&nbsp;速率级别：" + result.SpeedLevel + "级</div></div></div>";
                    ltlProduct.Text = "<div class='weui-media-box_appmsg'><div class='weui-media-box__bd'><div class='promotion-sku clear' style='font-size:13px;'>规格：" + result.Specs + "&nbsp;&nbsp;&nbsp;花纹：" + result.Figure + "&nbsp;&nbsp;&nbsp;载速：" + result.LoadIndex + result.SpeedLevel + "&nbsp;&nbsp;&nbsp;型号：" + result.Model + "&nbsp;&nbsp;&nbsp;尺寸：" + result.HubDiameter + "寸&nbsp;&nbsp;&nbsp;周期批次：" + result.BatchYear.ToString() + "年&nbsp;&nbsp;&nbsp;速率级别：" + result.SpeedLevel + "级</div></div></div>";

                    ltlDetail.Text = result.Memo;
                    ltlAddCart.Text = "<a href='javascript:addCart();' class='weui-tabbar__item yellow-color open-popup' data-target='#join_cart'><p class='promotion-foot-menu-label'>加入购物车</p></a>";
                    ltlBuy.Text = "<a href='javascript:saveOrder();' class='weui-tabbar__item red-color open-popup'><p class='promotion-foot-menu-label'>立即购买</p></a>";
                    //if (result.OnSaleNum <= 0)
                    //{
                    //    ltlAddCart.Text = "<a href='javascript:;' class='weui-tabbar__item yellow-color open-popup'><p class='promotion-foot-menu-label'>缺货</p></a>";
                    //    ltlBuy.Text = "<a href='javascript:;' class='weui-tabbar__item red-color open-popup'><p class='promotion-foot-menu-label'>缺货</p></a>";
                    //}
                    //else
                    //{
                    //    ltlAddCart.Text = "<a href='javascript:addCart();' class='weui-tabbar__item yellow-color open-popup' data-target='#join_cart'><p class='promotion-foot-menu-label'>加入购物车</p></a>";
                    //    ltlBuy.Text = "<a href='javascript:saveOrder();' class='weui-tabbar__item red-color open-popup'><p class='promotion-foot-menu-label'>立即购买</p></a>";
                    //}
                }
            }
        }
    }
}