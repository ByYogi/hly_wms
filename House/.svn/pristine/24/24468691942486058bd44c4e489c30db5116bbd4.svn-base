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
    public partial class myAddress : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QueryWxUserAddress();
            }
        }

        private void QueryWxUserAddress()
        {
            CargoWeiXinBus bus = new CargoWeiXinBus();
            List<WXUserAddressEntity> result = bus.QueryWxAddressByWXID(new WXUserAddressEntity { WXID = WxUserInfo.ID });
            foreach (var it in result)
            {
                //ltlShipAddress.Text += "<div class=\"weui-cell weui-cell_access\"><div class=\"weui-cell__bd\"><p style=\"margin-bottom: 5px;\">" + it.Name + "</p><p>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p></div><div class=\"weui-cell__hd\"><p style=\"margin-bottom: 5px;\">" + it.Cellphone + "</p></div></div>";
                string add = it.ID.ToString() + "/" + it.Address + "/" + it.Name + "/" + it.Cellphone + "/" + it.Province + "/" + it.City + "/" + it.Country + "/" + it.IsDefault;
                if (it.IsDefault.Equals("1"))
                {
                    ltlShipAddress.Text += "<div class='weui-panel__bd'><div class='weui-media-box weui-media-box_text address-list-box'><a href='AddMyAddress.aspx?ID=" + add + "' class='address-edit' title='点击修改地址'></a><a href='AddMyAddress.aspx?ID=" + add + "' title='点击修改地址'><h4 class='weui-media-box__title'><span>" + it.Name + "</span>&nbsp;&nbsp;<span>" + it.Cellphone + "</span></h4><p class='weui-media-box__desc address-txt'>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p></a><span class='default-add'>默认地址</span></div></div>";
                    //ltlShipAddress.Text += "<div class=\"weui-cell weui-cell_swiped\" style=\"color:red\" onclick='inputAddSession(\"" + add + "\")'><div class=\"weui-cell__bd\"><div class=\"weui-cell\"><div class=\"weui-cell__bd\"><p>" + it.Name + "</p><p>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p></div><div class=\"weui-cell__ft\">" + it.Cellphone + "</div></div></div><div class=\"weui-cell__ft\"><a class=\"weui-swiped-btn weui-swiped-btn_warn delete-swipeout\" href=\"javascript:Del(" + it.ID + ")\">删除</a><a class=\"weui-swiped-btn weui-swiped-btn_default close-swipeout\" href=\"javascript:Set(" + it.ID + ")\">设为默认</a></div></div>";
                }
                else
                {
                    ltlShipAddress.Text += "<div class='weui-panel__bd'><div class='weui-media-box weui-media-box_text address-list-box'><a href='AddMyAddress.aspx?ID=" + add + "' class='address-edit' title='点击修改地址'></a><a href='AddMyAddress.aspx?ID=" + add + "' title='点击修改地址'><h4 class='weui-media-box__title'><span>" + it.Name + "</span>&nbsp;&nbsp;<span>" + it.Cellphone + "</span></h4><p class='weui-media-box__desc address-txt'>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p></a></div></div>";
                    //ltlShipAddress.Text += "<div class=\"weui-cell weui-cell_swiped\" onclick='inputAddSession(\"" + add + "\")'><div class=\"weui-cell__bd\"><div class=\"weui-cell\"><div class=\"weui-cell__bd\"><p>" + it.Name + "</p><p>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p></div><div class=\"weui-cell__ft\">" + it.Cellphone + "</div></div></div><div class=\"weui-cell__ft\"><a class=\"weui-swiped-btn weui-swiped-btn_warn delete-swipeout\" href=\"javascript:Del(" + it.ID + ")\">删除</a><a class=\"weui-swiped-btn weui-swiped-btn_default close-swipeout\" href=\"javascript:Set(" + it.ID + ")\">设为默认</a></div></div>";
                }
            }

        }
    }
}