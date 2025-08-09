using House.Business.Cargo;
using House.Entity.Cargo;
using Senparc.Weixin.Entities;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace HouseServices
{
    public class WXQYSendHelper
    {
        /// <summary>
        /// 通过微信企业号向用户推送消息
        /// </summary>
        /// <param name="sendType">消息类型：0：推送</param>
        /// <param name="workType">业务分类：4:仓配订单</param>
        /// <param name="sendInfo"></param>
        public static void PushInfo(string sendType, string workType, QySendInfoEntity sendInfo)
        {
            QiyeBus bus = new QiyeBus();
            //查询好来运企业微信
            QyConfigEntity config = bus.QueryQyConfig(new QyConfigEntity { SendType = sendType, WorkClass = workType, QYKind = "1" });
            if (config == null || config.ID.Equals(0)) { return; }
            sendInfo.agentID = config.AgentID;
            string urlF = @"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";
            //消息通知应用的Secret和ApentID1000003
            // AccessTokenResult token = CommonApi.GetToken(ConfigurationSettings.AppSettings["HLYQyCorpID"], config.AppSecret.Trim());//VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM

            object data = new object(); ;
            switch (sendInfo.msgType)
            {
                case msgType.text:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "text",
                        agentid = sendInfo.agentID,
                        text = new
                        {
                            content = sendInfo.content
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.imgage:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "imgage",
                        agentid = sendInfo.agentID,
                        imgage = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.voice:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "voice",
                        agentid = sendInfo.agentID,
                        voice = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.video:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "video",
                        agentid = sendInfo.agentID,
                        video = new
                        {
                            media_id = sendInfo.media_id,
                            title = sendInfo.title,
                            description = sendInfo.content
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.file:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "file",
                        agentid = sendInfo.agentID,
                        file = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.textcard:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "textcard",
                        agentid = sendInfo.agentID,
                        textcard = new
                        {
                            title = sendInfo.title,
                            description = sendInfo.content,
                            url = sendInfo.url,
                            btntxt = "详情"
                        }
                    };
                    break;
                case msgType.news:
                    break;
                case msgType.mpnews:
                    break;
                default:
                    break;
            }
            string corpid = Convert.ToString(ConfigurationSettings.AppSettings["HLYQyCorpID"]);
            string token = GetWeixinQYToken(corpid, config.AppSecret.Trim());
            QyJsonResult res = CommonJsonSend.Send(token, urlF, data, Senparc.Weixin.CommonJsonSendType.POST);
        }
        public static string GetWeixinQYToken(string qyCorpID, string qyAgentSecret)
        {
            string token = string.Empty;
            try
            {
                token = Senparc.Weixin.QY.Containers.AccessTokenContainer.TryGetToken(qyCorpID, qyAgentSecret);
                GetCallBackIpResult bip = CommonApi.GetCallBackIp(token);
                if (bip.errcode.Equals("40001"))
                {
                    Senparc.Weixin.QY.Containers.AccessTokenContainer.Register(qyCorpID, qyAgentSecret);
                    token = Senparc.Weixin.QY.Containers.AccessTokenContainer.TryGetToken(qyCorpID, qyAgentSecret);
                }
            }
            catch (Exception ex)
            {
                Senparc.Weixin.QY.Containers.AccessTokenContainer.Register(qyCorpID, qyAgentSecret);
                token = Senparc.Weixin.QY.Containers.AccessTokenContainer.TryGetToken(qyCorpID, qyAgentSecret);
                //token = Senparc.Weixin.QY.Containers.AccessTokenContainer.TryGetToken(qyCorpID, qyAgentSecret, true);
            }
            return token;
        }
        /// <summary>
        /// 通过好来运微信企业号向用户推送消息
        /// </summary>
        /// <param name="sendType">消息类型：0：推送</param>
        /// <param name="workType">业务分类：0：下单推送，1：价格变动，2：状态跟踪推送</param>
        /// <param name="sendInfo"></param>
        public static void HLYQYPushInfo(QyConfigEntity agentEnt, QySendInfoEntity sendInfo)
        {
            sendInfo.agentID = agentEnt.AgentID;
            string urlF = @"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";
            //消息通知应用的Secret和ApentID1000003
            //string token = AccessTokenContainer.TryGetToken(Common.GetQYCorpID(), agentEnt.AgentSecret);
            string token = GetWeixinQYToken("wx5bf912a3b8774d14", agentEnt.AgentSecret);
            //AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), config.AppSecret.Trim());//VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM
            object data = new object(); ;
            switch (sendInfo.msgType)
            {
                case msgType.text:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "text",
                        agentid = sendInfo.agentID,
                        text = new
                        {
                            content = sendInfo.content
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.imgage:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "imgage",
                        agentid = sendInfo.agentID,
                        imgage = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.voice:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "voice",
                        agentid = sendInfo.agentID,
                        voice = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.video:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "video",
                        agentid = sendInfo.agentID,
                        video = new
                        {
                            media_id = sendInfo.media_id,
                            title = sendInfo.title,
                            description = sendInfo.content
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.file:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "file",
                        agentid = sendInfo.agentID,
                        file = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.textcard:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "textcard",
                        agentid = sendInfo.agentID,
                        textcard = new
                        {
                            title = sendInfo.title,
                            description = sendInfo.content,
                            url = sendInfo.url,
                            btntxt = "详情"
                        }
                    };
                    break;
                case msgType.news:
                    break;
                case msgType.mpnews:
                    break;
                default:
                    break;
            }

            QyJsonResult res = CommonJsonSend.Send(token, urlF, data, Senparc.Weixin.CommonJsonSendType.POST);
        }
        /// <summary>
        /// 通过微信企业号向用户推送消息
        /// </summary>
        /// <param name="sendInfo"></param>
        public static void DLTQYPushInfo(QySendInfoEntity sendInfo)
        {
            string urlF = @"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";
            //消息通知应用的Secret和ApentID1000003
            //AccessTokenResult token = new AccessTokenResult();
            string token = GetWeixinQYToken("ww4ee2174db697d479", sendInfo.AgentSecret);

            object data = new object(); ;
            switch (sendInfo.msgType)
            {
                case msgType.text:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "text",
                        agentid = sendInfo.agentID,
                        text = new
                        {
                            content = sendInfo.content
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.imgage:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "imgage",
                        agentid = sendInfo.agentID,
                        imgage = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.voice:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "voice",
                        agentid = sendInfo.agentID,
                        voice = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.video:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "video",
                        agentid = sendInfo.agentID,
                        video = new
                        {
                            media_id = sendInfo.media_id,
                            title = sendInfo.title,
                            description = sendInfo.content
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.file:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "file",
                        agentid = sendInfo.agentID,
                        file = new
                        {
                            media_id = sendInfo.media_id
                        },
                        safe = sendInfo.safe
                    };
                    break;
                case msgType.textcard:
                    data = new
                    {
                        touser = sendInfo.toUser,
                        toparty = sendInfo.toParty,
                        totag = sendInfo.toTag,
                        msgtype = "textcard",
                        agentid = sendInfo.agentID,
                        textcard = new
                        {
                            title = sendInfo.title,
                            description = sendInfo.content,
                            url = sendInfo.url,
                            btntxt = "详情"
                        }
                    };
                    break;
                case msgType.news:
                    break;
                case msgType.mpnews:
                    break;
                default:
                    break;
            }

            QyJsonResult res = CommonJsonSend.Send(token, urlF, data, Senparc.Weixin.CommonJsonSendType.POST);
        }
    }
}
