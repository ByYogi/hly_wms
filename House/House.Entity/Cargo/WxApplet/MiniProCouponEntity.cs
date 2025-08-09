using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 返回前端小程序优惠券数据实体
    /// </summary>
    [Serializable]
    public class ReturnMiniProCouponEntity
    {
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MiniProCouponEntity> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    /// 返回前端小程序优惠券数据实体
    /// </summary>
    [Serializable]
    public class MiniProCouponEntity
    {
        public long ID { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 使用状态
        /// </summary>
        public string UseStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 优惠券类型名称
        /// </summary>
        public string CouponTypeName { get; set; }
        /// <summary>
        /// 是否可叠加名称
        /// </summary>
        public string IsSuperPositionName { get; set; }
        /// <summary>
        /// 是否跟随订单数量限制条数
        /// </summary>
        public string IsFollowQuantity { get; set; }
        /// <summary>
        /// 是否跟随订单数量限制条数  int
        /// </summary>
        public string FollowQuantity { get; set; }
        /// <summary>
        /// 是否可叠加名称  int
        /// </summary>
        public string SuperPositionName { get; set; }
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
}
