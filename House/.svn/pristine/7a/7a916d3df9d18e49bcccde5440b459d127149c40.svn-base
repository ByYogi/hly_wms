using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 产品标签数据实体1.2.3.Tbl_Cargo_ProductTag（产品标签出入库关系表）
    /// </summary>
    [Serializable]
    public class CargoProductTagEntity
    {
        public Int64 ProductID { get; set; }
        public string TagCode { get; set; }
        public string InCargoStatus { get; set; }
        public string InCargoType { get; set; }
        public string InCargoOperID { get; set; }
        public DateTime InCargoTime { get; set; }
        public string OutCargoStatus { get; set; }
        public string OutCargoOperID { get; set; }
        public DateTime OutCargoTime { get; set; }
        public string OrderNo { get; set; }
        public string ProductName { get; set; }
        public string Specs { get; set; }
        public string Model { get; set; }
        public string GoodsCode { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string Batch { get; set; }
        public string Figure { get; set; }
        public int ContainerID { get; set; }
        public int OldContainerID { get; set; }
        public string TyreCode { get; set; }
        public string LogisAwbNo { get; set; }
        public string ContainerCode { get; set; }
        public string MoveOrderNo { get; set; }
        /// <summary>
        /// 移库状态和移库入库状态  默认为空，0：移库标签中1：移库入库成功
        /// </summary>
        public string MoveStatus { get; set; }
        /// <summary>
        /// 捆包数量
        /// </summary>
        public int BundleNum { get; set; }
        public string ProductCode { get; set; }
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
