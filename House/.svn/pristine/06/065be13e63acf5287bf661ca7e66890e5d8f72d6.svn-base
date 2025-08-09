    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    namespace House.Entity.Cargo
    {
        /// <summary>
        /// 商品名录管理实体
        /// </summary>
        [Serializable]
        public class CargoSupplierCoodEntity
        {

            [Description("表主键")]
            public Int64 ID { get; set; }
            [Description("供应商ID")]
            public Int64 SuppID { get; set; }
            [Description("富添盛编码")]
            public string FTSCode { get; set; }
            [Description("供应商产品名称")]
            public string GoodsName { get; set; }
            [Description("产品分类")]
            public Int64 TypeID { get; set; }

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
