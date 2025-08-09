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
    public partial class secKill : WXBasePage
    {
        public long ParentID = 0;
        public string company = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltlReg.Text = "0";
                ltlGet.Text = "0";
                ParentID = Convert.ToInt64(Request["ParentID"]);
                company = Convert.ToString(Request["Company"]);
                CargoWeiXinBus bus = new CargoWeiXinBus();
                List<WXSecKillEntity> result = bus.QuerySecKillData(new WXSecKillEntity { ParentID = WxUserInfo.ID, Company = company });
                if (result.Count > 0)
                {
                    ltlReg.Text = result.Where(c => c.UseStatus == "0").Count().Equals(0) ? "0" : result.Where(c => c.UseStatus == "0").Count().ToString();
                    ltlGet.Text = result.Where(c => c.UseStatus == "1").Count().Equals(0) ? "0" : result.Where(c => c.UseStatus == "1").Count().ToString();
                }
                switch (company)
                {
                    case "1":
                        //信达汽修
                        ltlTitle.Text = "全民战“疫”信达汽修在行动";
                        ltlShop.Text = "<table><tr><td>店名：</td><td>广州信达汽车维修中心&nbsp;&nbsp;<a href='https://apis.map.qq.com/tools/poimarker?type=1&keyword=广州信达汽车维修中心&center=23.226127,113.299532&radius=1000&key=7U4BZ-YDV3D-54E4H-PDUXL-G3TM3-EUBWZ&referer=myapp' style='border: 1px solid #d9d9d9; background-color: #f7f7f7; cursor: pointer; padding: 3px; color: black; font-size: 12px;'>打开地图</a></td></tr><tr><td>电话：</td><td><a href='tel:13631442958'>13711690902</a>，<a href='tel:13602759271'>13602759271</a></td></tr><tr><td>地址：</td><td>广州市白云区白云大道北黄边北路610号</td></tr></table>";
                        ltlActive.Text = "<table style='font-size: 13px; border: 1px solid #cad9ea; color: black;'><tr><th>套餐</th><th>活动方案</th><th>活动内容说明</th><th>备注</th></tr><tr style='background: #fff;'><td width='6%' rowspan='2' style='border: 1px solid #cad9ea;'>1</td><td rowspan='2' style='border: 1px solid #cad9ea;'>在微信注册登记并朋友圈转发活动链接</td><td style='border: 1px solid #cad9ea; text-align: left;'>A.可享受价值268元的车内杀菌消毒套餐特惠价服务:26.8元</td><td width='10%' rowspan='2' style='border: 1px solid #cad9ea;'>都可享受</td></tr><tr style='background: #fff; border: 1px solid #cad9ea; text-align: left;'><td>B.可免费享受24项爱车全面检测一次</td></tr><tr style='background: #fff;'><td rowspan='4' style='border: 1px solid #cad9ea;'>2</td><td rowspan='4' style='border: 1px solid #cad9ea;'>活动期间内做车辆保养维修</td><td style='border: 1px solid #cad9ea; text-align: left;'>A.可免费享受价值268元的车内杀菌消毒套餐服务</td><td rowspan='4' style='border: 1px solid #cad9ea;'>可选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>B.可享受维修工时8折优惠</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>C.可免费享受188元的四轮定位一次</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>D.可免费享受上门搭电一次</td></tr><tr style='background: #fff;'><td rowspan='4' style='border: 1px solid #cad9ea;'>3</td><td rowspan='4' style='border: 1px solid #cad9ea;'>活动期间内更换轮胎 蓄电池</td><td style='border: 1px solid #cad9ea; text-align: left;'>A.可免费享受价值268元的车内杀菌消毒套餐服务</td><td rowspan='4' style='border: 1px solid #cad9ea;'>可选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>B.可免费享受上门搭电一次</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>C.可免费赠送价值188元四轮定位一次</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>D.可免费享受24项爱车全面检测一次</td></tr><tr style='background: #fff;'><td rowspan='3' style='border: 1px solid #cad9ea;'>4</td><td rowspan='3' style='border: 1px solid #cad9ea;'>活动期间内购买保险</td><td style='border: 1px solid #cad9ea; text-align: left;'>A.可免费享受价值268元的车内杀菌消毒套餐服务</td><td rowspan='3' style='border: 1px solid #cad9ea;'>都可享受</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>B.可免费享受24项爱车全面检测一次</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>C.可免费故障救援服务：包括搭电、送水、更换备胎、应急拖车(两次不超过50公里）</td></tr></table>";
                        break;
                    case "2":
                        //长沙泉塘迪乐泰 113.127637,28.216226
                        ltlTitle.Text = "新老客户注意啦！免费福利来了！";
                        ltlShop.Text = "<table><tr><td width='20%' >店名：</td><td>长沙县泉塘迪乐泰汽车美容服务中心&nbsp;&nbsp;<a href='https://apis.map.qq.com/tools/poimarker?type=1&keyword=优科豪马轮胎(县一中南巷)&center=28.210552,113.121235&radius=1000&key=7U4BZ-YDV3D-54E4H-PDUXL-G3TM3-EUBWZ&referer=myapp' style='border: 1px solid #d9d9d9; background-color: #f7f7f7; cursor: pointer; padding: 3px; color: black; font-size: 12px;'>打开地图</a></td></tr><tr><td>电话：</td><td><a href='tel:15197999858'>15197999858</a></td></tr><tr><td>地址：</td><td>湖南省长沙市长沙县泉塘三期6栋优科豪马</td></tr></table>";
                        ltlActive.Text = "<table style='font-size: 13px; border: 1px solid #cad9ea; color: black;'><tr><th>套餐</th><th>活动方案</th><th>活动内容说明</th><th>备注</th></tr><tr style='background: #fff;'><td  width='6%' rowspan='3' style='border: 1px solid #cad9ea;'>1</td><td rowspan='4' style='border: 1px solid #cad9ea;'>在微信注册登记并朋友圈转发活动链接</td><td style='border: 1px solid #cad9ea; text-align: left;'>A.可享受价值128元的车内杀菌消毒一次</td><td rowspan='4' style='border: 1px solid #cad9ea;'>可选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>B.可享受价值128元 36项爱车全面检测一次</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>C.保养工时费抵扣券</td></tr></table>";
                        break;
                    case "3":
                        //广州车轮馆 113.127637,28.216226
                        ltlTitle.Text = "新冠病毒无情，迪乐泰有爱";
                        ltlShop.Text = "<table><tr><td width='20%' >店名：</td><td>广州车轮馆汽车服务有限公司&nbsp;&nbsp;<a href='https://apis.map.qq.com/tools/poimarker?type=1&keyword=广州车轮馆汽车服务有限公司&center=23.258037,113.317513&radius=1000&key=7U4BZ-YDV3D-54E4H-PDUXL-G3TM3-EUBWZ&referer=myapp' style='border: 1px solid #d9d9d9; background-color: #f7f7f7; cursor: pointer; padding: 3px; color: black; font-size: 12px;'>打开地图</a></td></tr><tr><td>电话：</td><td><a href='tel:15986386161'>15986386161</a>，<a href='tel:13631442958'>13631442958</a></td></tr><tr><td>地址：</td><td>广东省广州市白云区东平横岗东路自编3号</td></tr></table>";
                        ltlActive.Text = "<table style='font-size: 13px; border: 1px solid #cad9ea; color: black;'><tr><th>套餐</th><th>活动方案</th><th>活动内容说明</th><th>备注</th></tr><tr style='background: #fff;'><td width='6%' rowspan='3' style='border: 1px solid #cad9ea;'>A</td><td rowspan='3' style='border: 1px solid #cad9ea;'>朋友圈转发活动链接</td><td style='border: 1px solid #cad9ea; text-align: left;'>1:送爱车36项全车免费检测一次</td><td width='10%' rowspan='3' style='border: 1px solid #cad9ea;'>全部享受</td></tr><tr style='background: #fff; border: 1px solid #cad9ea; text-align: left;'><td>2：享受上门接车送车服务</td></tr><tr style='background: #fff; border: 1px solid #cad9ea; text-align: left;'><td>3：享受洗车和车内消毒原价138现只需48元优惠价一次</td></tr><tr style='background: #fff;'><td rowspan='3' style='border: 1px solid #cad9ea;'>B</td><td rowspan='3' style='border: 1px solid #cad9ea;'>车辆维修或保养</td><td style='border: 1px solid #cad9ea; text-align: left;'>1：送价值68元车内车外精洗一次</td><td rowspan='3' style='border: 1px solid #cad9ea;'>选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>2：享受车身打蜡抛光工时费6折一次</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>3：送店面价值49.8迪乐泰秒杀菌或安心醒神香囊等产品一样</td></tr><tr style='background: #fff;'><td rowspan='3' style='border: 1px solid #cad9ea;'>C</td><td rowspan='3' style='border: 1px solid #cad9ea;'>爱车美容或贴膜</td><td style='border: 1px solid #cad9ea; text-align: left;'>1：送价值88元四轮定位一次</td><td rowspan='3' style='border: 1px solid #cad9ea;'>选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>2:送公司引能仕机油5W-30一升</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>3：送店面价值49.8迪乐泰秒杀菌或安心醒神香囊等产品一样</td></tr><tr style='background: #fff;'><td rowspan='3' style='border: 1px solid #cad9ea;'>D</td><td rowspan='4' style='border: 1px solid #cad9ea;'>爱车购胎或轮毂</td><td style='border: 1px solid #cad9ea; text-align: left;'>1：送爱车保养美容工时费6折优惠一次</td><td rowspan='3' style='border: 1px solid #cad9ea;'>选其中一项</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>2：送轮胎6-12月免费补胎2-4次过期作废</td></tr><tr style='background: #fff;'><td style='border: 1px solid #cad9ea; text-align: left;'>3：送店面价值49.8迪乐泰秒杀菌或安心醒神香囊等产品一样</td></tr><tr><td colspan=4 style='color:red;'>备注：客户介绍或引流成功送店面优惠劵在店可当现金消费使用</td></tr></table>";
                        break;
                    default:
                        break;
                }
                bus.AddSecStatisData(new WXSecStatisEntity { SecID = Convert.ToInt32(company), BrowseNum = 1 });
            }
        }
    }
}