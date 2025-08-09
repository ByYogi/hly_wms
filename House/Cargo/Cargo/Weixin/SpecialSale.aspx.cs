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
    public partial class SpecialSale : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int NormalPriceNum = 0, SpecialPriceNum = 0;
                CargoWeiXinBus bus = new CargoWeiXinBus();
                List<CargoOrderGoodsEntity> result = bus.QueryClientOrderData(new CargoOrderEntity { ClientNum = WxUserInfo.ClientNum, HouseID = WxUserInfo.HouseID, StartDate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)), EndDate = DateTime.Now, TypeID = 18, SaleType = "1" });
                if (result.Count > 0)
                {
                    foreach (var it in result)
                    {
                        if (it.OrderModel.Equals("1")) { SpecialPriceNum += it.Piece; continue; }
                        if (!it.SpecialID.Equals(0)) { SpecialPriceNum += it.Piece; continue; }
                        NormalPriceNum += it.Piece;
                    }
                }
                if (NormalPriceNum > SpecialPriceNum)
                {
                    ltlSpecial.Text = "您能购买普利司通" + (NormalPriceNum - SpecialPriceNum).ToString() + "条特价轮胎";
                }
                int YKNormalPriceNum = 0, YKSpecialPriceNum = 0;

                List<CargoOrderGoodsEntity> res = bus.QueryClientOrderData(new CargoOrderEntity { ClientNum = WxUserInfo.ClientNum, HouseID = WxUserInfo.HouseID, StartDate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)), EndDate = DateTime.Now, TypeID = 9, SaleType = "1" });
                if (res.Count > 0)
                {
                    foreach (var it in res)
                    {
                        if (it.OrderModel.Equals("1")) { YKSpecialPriceNum += it.Piece; continue; }
                        if (!it.SpecialID.Equals(0)) { YKSpecialPriceNum += it.Piece; continue; }
                        YKNormalPriceNum += it.Piece;
                    }
                }
                if (NormalPriceNum > SpecialPriceNum)
                {
                    ltlSpecial.Text = "您能购买优科豪马" + (YKNormalPriceNum - YKSpecialPriceNum).ToString() + "条特价轮胎";
                }
            }
        }
    }
}