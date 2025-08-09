using House.Entity.Cargo;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseServices
{
    /// <summary>
    /// 微信客服接口推送消息
    /// </summary>
    public class WxCustomSend
    {
        public void SendWeiXinInfo(string accessToken, string openid, QySendInfoEntity sendInfo, List<Article> articlelist)
        {
            switch (sendInfo.msgType)
            {
                case msgType.file:
                    break;
                case msgType.imgage:
                    CustomApi.SendImage(accessToken, openid, sendInfo.media_id);
                    break;
                case msgType.mpnews:
                    break;
                case msgType.news:
                    CustomApi.SendNews(accessToken, openid, articlelist);
                    break;
                case msgType.text:
                    CustomApi.SendText(accessToken, openid, sendInfo.content);
                    break;
                case msgType.textcard:
                    break;
                case msgType.video:
                    break;
                case msgType.voice:
                    break;
                default:
                    break;
            }

        }
    }
    /// <summary>
    /// 发送微信模板消息接口
    /// </summary>
    public class SendTempleteMessage
    {
        public string SendMessage(string accessToken, string openid, string TemplateId, string url, TemplateMsg entity)
        {
            WxJsonResult wxjson = Template.SendTemplateMessage<TemplateMsg>(accessToken, openid, TemplateId, "#173177", url, entity);
            return wxjson.errmsg;
        }
    }

    /// <summary>
    /// 模板消息接口
    /// </summary>
    public static class Template
    {
        public static WxJsonResult SendTemplateMessage<T>(string accessToken, string openId, string templateId, string topcolor, string url, T data)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
            var msgData = new TempleteModel()
            {
                template_id = templateId,
                topcolor = topcolor,
                touser = openId,
                url = url,
                data = data
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData);
        }
    }

    [Serializable]
    public partial class TemplateMsg
    {
        /// <summary>
        /// first
        /// </summary>
        public TemplateDataItem first { get; set; }
        /// <summary>
        /// keyword1
        /// </summary>
        public TemplateDataItem keyword1 { get; set; }
        /// <summary>
        /// keyword2
        /// </summary>
        public TemplateDataItem keyword2 { get; set; }
        /// <summary>
        /// keyword3
        /// </summary>
        public TemplateDataItem keyword3 { get; set; }
        /// <summary>
        /// keyword4
        /// </summary>
        public TemplateDataItem keyword4 { get; set; }
        /// <summary>
        /// keyword5
        /// </summary>
        public TemplateDataItem keyword5 { get; set; }
        /// <summary>
        /// keyword6
        /// </summary>
        public TemplateDataItem keyword6 { get; set; }
        /// <summary>
        /// remark
        /// </summary>
        public TemplateDataItem remark { get; set; }
    }
}
