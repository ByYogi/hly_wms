using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 微信企业号公共配置数据实体1.6.1.Tbl_QY_Config（企业号配置数据表）
    /// </summary>
    [Serializable]
    public class QyConfigEntity
    {
        public int ID { get; set; }
        public string SendType { get; set; }
        public string WorkClass { get; set; }
        public string AgentID { get; set; }
        public string AgentSecret { get; set; }
        public string AppSecret { get; set; }
        public DateTime OP_DATE { get; set; }
        public string QYKind { get; set; }
        public string Token { get; set; }
    }
    /// <summary>
    /// 消息推送的格式 数据实体
    /// </summary>
    [Serializable]
    public class QySendInfoEntity
    {
        /// <summary>
        /// 接受成员UserID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送
        /// </summary>
        public string toUser { get; set; }
        /// <summary>
        /// 部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// </summary>
        public string toParty { get; set; }
        /// <summary>
        /// 标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// </summary>
        public string toTag { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public msgType msgType { get; set; }
        /// <summary>
        /// 企业应用的id
        /// </summary>
        public string agentID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 点击后跳转的链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 消息内容，最长不超过2048个字节
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图片媒体文件id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 表示是否是保密消息，0表示否，1表示是，默认0
        /// </summary>
        public int safe { get; set; }
        /// <summary>
        /// 群聊天会话ID
        /// </summary>
        public string ChatID { get; set; }

        public string AgentSecret { get; set; }
    }

    /// <summary>
    /// 消息推送枚举
    /// </summary>
    public enum pushType
    {
        order,
        price
    }
    /// <summary>
    /// 消息推送类型枚举
    /// </summary>
    public enum msgType
    {
        text,//文本消息
        imgage,//图片消息
        voice,//语音消息
        video,//视频消息
        file,//文件消息
        textcard,//文本卡片形式消息
        news,//图文消息
        mpnews,//图文消息
        markdown//
    }
}
