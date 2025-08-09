using House.Business.Cargo;
using House.Entity.Cargo;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.QY
{
    public partial class MyWorkFloor : QYBasePage
    {
        public QyUserStatisEntity ent = new QyUserStatisEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            ltlCurDate.Text = DateTime.Today.ToLongDateString();
            //1.根据回传的URL获取 Code
            //string code = Request["code"];
            try
            {
                //WriteTextLog(QyUserInfo.UserID, DateTime.Now);
                //string secret = ConfigurationSettings.AppSettings["MyWorkSecret"];
                ////2.获取Token
                //AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), secret);
                ////3.根据Token和Code获取用户UserID
                //GetUserInfoResult user = OAuth2Api.GetUserId(token.access_token, code);
                QiyeBus bus = new QiyeBus();
                QyUserStatisEntity entity = new QyUserStatisEntity();
                entity.UserID = QyUserInfo.UserID;
                entity.OP_DATE = DateTime.Now;
                ent = bus.QueryWXQyUserStatis(entity);
            }
            catch (Exception ex)
            {
                WriteTextLog(ex.Message, DateTime.Now);
            }
            //WriteTextLog("2：" + code, DateTime.Now);
        }
    }
}