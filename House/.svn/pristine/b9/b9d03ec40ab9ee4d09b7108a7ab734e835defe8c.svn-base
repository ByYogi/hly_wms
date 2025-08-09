using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using House.Business.Cargo;
using House.Entity.Cargo;
namespace Cargo.Weixin
{
    public partial class chelunguan : WXBasePage
    {
        public long ParentID = 0;
        public string company = "3";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltlReg.Text = "0";
                ltlGet.Text = "0";
                ParentID = Convert.ToInt64(Request["ParentID"]);
                //company = Convert.ToString(Request["Company"]);
                CargoWeiXinBus bus = new CargoWeiXinBus();
                List<WXSecKillEntity> result = bus.QuerySecKillData(new WXSecKillEntity { ParentID = WxUserInfo.ID, Company = company });
                if (result.Count > 0)
                {
                    ltlReg.Text = result.Where(c => c.UseStatus == "0").Count().Equals(0) ? "0" : result.Where(c => c.UseStatus == "0").Count().ToString();
                    ltlGet.Text = result.Where(c => c.UseStatus == "1").Count().Equals(0) ? "0" : result.Where(c => c.UseStatus == "1").Count().ToString();
                }

                //广州车轮馆 113.127637,28.216226
                //ltlTitle.Text = "新冠病毒无情，迪乐泰有爱";
                //ltlShop.Text = "";
                //ltlActive.Text = "<table style='font-size: 13px; border: 1px solid #cad9ea; color: black;'><tr><th>套餐</th><th>活动方案</th><th>活动内容说明</th><th>备注</th></tr><tr style='background: #fff;'><td width='6%' rowspan='3' style='border: 1px solid #cad9ea;'>A</td><td rowspan='3' style='border: 1px solid #cad9ea;'>朋友圈转发活动链接</td><td style='border: 1px solid #cad9ea; text-align: left;'>1:送爱车36项全车免费检测一次</td><td width='10%' rowspan='3' style='border: 1px solid #cad9ea;'>全部享受</td></tr><tr style='background: #fff; border: 1px solid #cad9ea; text-align: left;'><td>2：享受上门接车送车服务</td></tr><tr style='background: #fff; border: 1px solid #cad9ea; text-align: left;'><td>3：享受洗车和车内消毒原价138现只需48元优惠价一次</td></tr><tr style='background: #fff;'><td rowspan='3' style='border: 1px solid #cad9ea;'>B</td><td rowspan='3' style='border: 1px solid #cad9ea;'>车辆维修或保养</td><td style='border: 1px solid #cad9ea; text-align: left;'>1：送价值68元车内车外精洗一次</td><td rowspan='3' style='border: 1px solid #cad9ea;'>选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>2：享受车身打蜡抛光工时费6折一次</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>3：送店面价值49.8迪乐泰秒杀菌或安心醒神香囊等产品一样</td></tr><tr style='background: #fff;'><td rowspan='3' style='border: 1px solid #cad9ea;'>C</td><td rowspan='3' style='border: 1px solid #cad9ea;'>爱车美容或贴膜</td><td style='border: 1px solid #cad9ea; text-align: left;'>1：送价值88元四轮定位一次</td><td rowspan='3' style='border: 1px solid #cad9ea;'>选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>2:送公司引能仕机油5W-30一升</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>3：送店面价值49.8迪乐泰秒杀菌或安心醒神香囊等产品一样</td></tr><tr style='background: #fff;'><td rowspan='3' style='border: 1px solid #cad9ea;'>D</td><td rowspan='4' style='border: 1px solid #cad9ea;'>爱车购胎或轮毂</td><td style='border: 1px solid #cad9ea; text-align: left;'>1：送爱车保养美容工时费6折优惠一次</td><td rowspan='3' style='border: 1px solid #cad9ea;'>选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>2：送轮胎6-12月免费补胎2-4次过期作废</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>3：送店面价值49.8迪乐泰秒杀菌或安心醒神香囊等产品一样</td></tr><tr><td colspan=4 style='color:red;'>备注：客户介绍或引流成功送店面优惠劵在店可当现金消费使用</td></tr></table>";

                bus.AddSecStatisData(new WXSecStatisEntity { SecID = Convert.ToInt32(company), BrowseNum = 1 });
            }
        }
    }
}