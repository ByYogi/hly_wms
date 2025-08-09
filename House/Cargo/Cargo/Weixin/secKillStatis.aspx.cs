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
    public partial class secKillStatis : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int company = 1;
                if (!string.IsNullOrEmpty(Convert.ToString(Request["Company"])))
                {
                    company = Convert.ToInt32(Request["Company"]);
                }
                CargoWeiXinBus bus = new CargoWeiXinBus();
                WXSecStatisEntity entity = bus.QuerySecStatisEntity(new WXSecStatisEntity { SecID = company });
                string stat = string.Empty;
                switch (company)
                {
                    case 1:
                        stat = "<table><tr><th>信达汽修店活动数据统计</th></tr><tr><td>总浏览数</td></tr><tr><td>" + entity.BrowseNum.ToString() + "</td></tr><tr><td>总转发数</td></tr><tr><td>" + entity.ShareNum.ToString() + "</td></tr><tr><td>总登记数</td></tr><tr><td>" + entity.RegNum.ToString() + "</td></tr><tr><td>总领取数</td></tr><tr><td>" + entity.ReceiveNum.ToString() + "</td></tr></table>";
                        break;
                    case 2:
                        stat = "<table><tr><th>长沙迪乐泰活动数据统计</th></tr><tr><td>总浏览数</td></tr><tr><td>" + entity.BrowseNum.ToString() + "</td></tr><tr><td>总转发数</td></tr><tr><td>" + entity.ShareNum.ToString() + "</td></tr><tr><td>总登记数</td></tr><tr><td>" + entity.RegNum.ToString() + "</td></tr><tr><td>总领取数</td></tr><tr><td>" + entity.ReceiveNum.ToString() + "</td></tr></table>";
                        break;
                    case 3:
                        stat = "<table><tr><th>广州车轮馆活动数据统计</th></tr><tr><td>总浏览数</td></tr><tr><td>" + entity.BrowseNum.ToString() + "</td></tr><tr><td>总转发数</td></tr><tr><td>" + entity.ShareNum.ToString() + "</td></tr><tr><td>总登记数</td></tr><tr><td>" + entity.RegNum.ToString() + "</td></tr><tr><td>总领取数</td></tr><tr><td>" + entity.ReceiveNum.ToString() + "</td></tr></table>";
                        break;
                    default:
                        break;
                }

                ltlSta.Text = stat;
            }
        }
    }
}