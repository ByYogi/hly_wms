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
    public partial class address : WXBasePage
    {
        public string cn = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QueryWxUserAddress();
            }
            cn = WxUserInfo.ClientNum.ToString();
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
                    ltlShipAddress.Text += "<div class='weui-panel__bd'><div class='weui-media-box weui-media-box_text address-list-box' style='padding:5px;'><a href='AddAddress.aspx?ID=" + add + "' style='height: 24px;display: block;position: absolute;right: 15px;top: 15px;background-size: 24px;color: #6a6565;border-left: #ffffff 1px groove;padding-left: 10px;font-size:14px;' title='点击修改地址'>编辑</a><h4 class='weui-media-box__title' style='margin-bottom: 0px;text-decoration: underline;    color: crimson;' onclick='inputAddSession(\"" + add + "\")'><span>" + it.Name + "</span>&nbsp;&nbsp;<span>" + it.Cellphone + "</span></h4><p class='weui-media-box__desc address-txt'  onclick='inputAddSession(\"" + add + "\")'>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p> <span class='default-add'>默认地址</span></div></div>";
                    //ltlShipAddress.Text += "<div class=\"weui-cell weui-cell_swiped\" style=\"color:red\" onclick='inputAddSession(\"" + add + "\")'><div class=\"weui-cell__bd\"><div class=\"weui-cell\"><div class=\"weui-cell__bd\"><p>" + it.Name + "</p><p>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p></div><div class=\"weui-cell__ft\">" + it.Cellphone + "</div></div></div><div class=\"weui-cell__ft\"><a class=\"weui-swiped-btn weui-swiped-btn_warn delete-swipeout\" href=\"javascript:Del(" + it.ID + ")\">删除</a><a class=\"weui-swiped-btn weui-swiped-btn_default close-swipeout\" href=\"javascript:Set(" + it.ID + ")\">设为默认</a></div></div>";
                }
                else
                {
                    ltlShipAddress.Text += "<div class='weui-panel__bd'><div class='weui-media-box weui-media-box_text address-list-box' style='padding:5px;'><a href='AddAddress.aspx?ID=" + add + "' style='height: 24px;display: block;position: absolute;right: 15px;top: 15px;background-size: 24px;color: #6a6565;border-left: #ffffff 1px groove;padding-left: 10px;font-size:14px;' title='点击修改地址'>编辑</a><h4 class='weui-media-box__title' style='margin-bottom: 0px;text-decoration: underline;    color: crimson;'  onclick='inputAddSession(\"" + add + "\")'><span>" + it.Name + "</span>&nbsp;&nbsp;<span>" + it.Cellphone + "</span></h4><p class='weui-media-box__desc address-txt'  onclick='inputAddSession(\"" + add + "\")'>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p></div></div>";
                    //ltlShipAddress.Text += "<div class=\"weui-cell weui-cell_swiped\" onclick='inputAddSession(\"" + add + "\")'><div class=\"weui-cell__bd\"><div class=\"weui-cell\"><div class=\"weui-cell__bd\"><p>" + it.Name + "</p><p>" + it.Province + " " + it.City + " " + it.Country + " " + it.Address + "</p></div><div class=\"weui-cell__ft\">" + it.Cellphone + "</div></div></div><div class=\"weui-cell__ft\"><a class=\"weui-swiped-btn weui-swiped-btn_warn delete-swipeout\" href=\"javascript:Del(" + it.ID + ")\">删除</a><a class=\"weui-swiped-btn weui-swiped-btn_default close-swipeout\" href=\"javascript:Set(" + it.ID + ")\">设为默认</a></div></div>";
                }
            }

        }
    }
}