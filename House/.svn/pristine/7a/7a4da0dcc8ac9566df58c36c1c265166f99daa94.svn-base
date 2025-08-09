using House.Business.Cargo;
using House.Entity.Cargo;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Cargo.Weixin
{
    public partial class myOrderSubmit : WXBasePage
    {
        public int AllowBuyNum = 0;
        public int PLAllowBuyNum = 0;
        public int TeJiaNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int NormalPriceNum = 0, SpecialPriceNum = 0;
                CargoWeiXinBus bus = new CargoWeiXinBus();
                List<CargoOrderGoodsEntity> result = bus.QueryClientOrderData(new CargoOrderEntity { ClientNum = WxUserInfo.ClientNum, HouseID = WxUserInfo.HouseID, StartDate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)), EndDate = DateTime.Now, TypeID = 9, SaleType = "1" });
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
                    AllowBuyNum = NormalPriceNum - SpecialPriceNum;
                }

                int PLNormalPriceNum = 0, PLSpecialPriceNum = 0;
                List<CargoOrderGoodsEntity> res = bus.QueryClientOrderData(new CargoOrderEntity { ClientNum = WxUserInfo.ClientNum, HouseID = WxUserInfo.HouseID, StartDate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)), EndDate = DateTime.Now, TypeID = 18, SaleType = "1" });
                if (res.Count > 0)
                {
                    foreach (var it in res)
                    {
                        if (it.OrderModel.Equals("1")) { PLSpecialPriceNum += it.Piece; continue; }
                        if (!it.SpecialID.Equals(0)) { PLSpecialPriceNum += it.Piece; continue; }
                        PLNormalPriceNum += it.Piece;
                    }
                }
                if (PLNormalPriceNum > PLSpecialPriceNum)
                {
                    PLAllowBuyNum = PLNormalPriceNum - PLSpecialPriceNum;
                }
                //特价胎
                //List<CargoOrderGoodsEntity> tejia = bus.QueryClientOrderData(new CargoOrderEntity { ClientNum = WxUserInfo.ClientNum, HouseID = WxUserInfo.HouseID, StartDate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)), EndDate = DateTime.Now, SaleType = "3" });
                //if (tejia.Count > 0)
                //{
                //    foreach (var it in tejia)
                //    {
                //        TeJiaNum += it.Piece;
                //    }
                //}
            }
        }
    }
}