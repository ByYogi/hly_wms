using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 订单拣货计划数据实体
    /// </summary>
    [Serializable]
    public class CargoOrderPickPlanEntity
    {
        [Description("表主键")]
        public long PickID { get; set; }
        [Description("拣货顺序号")]
        public int PickNum { get; set; }
        [Description("拣货计划单号")]
        public string PickPlanNo { get; set; }
        [Description("仓库ID")]
        public string HouseID { get; set; }
        [Description("总数量")]
        public int TotalNum { get; set; }
        [Description("开单时间")]
        public DateTime CreateDate { get; set; }
        public string Memo { get; set; }
        [Description("拣货状态")]
        public string PickStatus { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("拣货类型")]
        public string PickType { get; set; }

        public string HouseName { get; set; }
        public string AcceptUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderNoStr { get; set; }
        public List<CargoOrderPickPlanGoodsEntity> PickPlanGoodsList { get; set; }
        public List<CargoOrderPickPlanGoodsEntity> AcceptUnitGoodsList { get; set; }
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
