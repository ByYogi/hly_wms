using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 领取优惠活动数据实体1.3.9.Tbl_WX_SecKill（活动领取表）
    /// </summary>
    [Serializable]
    public class WXSecKillEntity
    {
        public Int64 ID { get; set; }
        public Int64 WXID { get; set; }
        public string CarNum { get; set; }
        public string Cellphone { get; set; }
        public string UseStatus { get; set; }
        public DateTime OP_DATE { get; set; }
        public string wxOpenID { get; set; }
        public string wxName { get; set; }
        public string ReceiveTime { get; set; }
        public Int64 ParentID { get; set; }
        public string AvatarBig { get; set; }
        public string OneWxName { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 去NULL,替换危险字符
        /// </summary>
        public void EnSafe()
        {
            PropertyInfo[] pSource = this.GetType().GetProperties();

            foreach (PropertyInfo s in pSource)
            {
                if (s.PropertyType.Name.ToUpper().Contains("STRING"))
                {
                    if (s.GetValue(this, null) == null)
                        s.SetValue(this, "", null);
                    else
                        s.SetValue(this, s.GetValue(this, null).ToString().Replace("'", "’"), null);
                }
            }
        }
    }

    /// <summary>
    /// 活动统计数据表
    /// </summary>
    [Serializable]
    public class WXSecStatisEntity
    {
        public int SecID { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 浏览数
        /// </summary>
        public int BrowseNum { get; set; }
        /// <summary>
        /// 分享给好友数
        /// </summary>
        public int ShareAppNum { get; set; }
        /// <summary>
        /// 分享到朋友圈数
        /// </summary>
        public int ShareTimNum { get; set; }
        /// <summary>
        /// 分享总数转发
        /// </summary>
        public int ShareNum { get; set; }
        /// <summary>
        /// 登记总数
        /// </summary>
        public int RegNum { get; set; }
        /// <summary>
        /// 领取数
        /// </summary>
        public int ReceiveNum { get; set; }
    }
}
