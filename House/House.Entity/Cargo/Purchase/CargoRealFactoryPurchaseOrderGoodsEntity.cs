using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 采购订单明细数据表 Tbl_Cargo_RealFactoryPurchaseOrderGoods
    /// 描述：记录订单里的多份品名记录信息
    /// </summary>
    [Serializable]
    public class CargoRealFactoryPurchaseOrderGoodsEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID")]
        public long GoodsID { get; set; }

        /// <summary>
        /// 分配仓库表主键
        /// </summary>
        [Description("分配仓库表主键")]
        public long FPID { get; set; }

        /// <summary>
        /// 采购订单表主键
        /// </summary>
        [Description("采购订单表主键")]
        public long PurOrderID { get; set; }

        /// <summary>
        /// 产品类型ID
        /// </summary>
        [Description("产品类型ID")]
        public int TypeID { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int HouseID { get; set; }


        /// <summary>
        /// 产品编码
        /// </summary>
        [Description("产品编码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>
        [Description("采购数量")]
        public int Piece { get; set; }

        /// <summary>
        /// 回告数量
        /// </summary>
        [Description("回告数量")]
        public int ReplyPiece { get; set; }

        /// <summary>
        /// 采购价
        /// </summary>
        [Description("采购价")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 来货导入表ID
        /// </summary>
        [Description("来货导入表ID")]
        public long FacID { get; set; }

        public decimal PieceFee {  get; set; }
        public decimal ReplyFee {  get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }

        public int OrderNum { get; set; }
        public string FacOrderNo { get; set; }
        public DateTime ETATime { get; set; }
        public string HouseName { get; set; }
        public string PurDepart { get; set; }
        public string Specs { get; set; }
        public string Model { get; set; }
        public string Figure { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string GoodsCode { get; set; }
        public string TypeName { get; set; }
        public string Batch { get; set; }

        public string PurOrderNo { get; set; }
        public string Supplier { get; set; }
        public string SuppClientNum { get; set; }
        public string SupplierAddress { get; set; }
        public string ReceiveName { get; set; }
        public string ReceiveCity { get; set; }
        public string ReceiveMobile { get; set; }
        public string ProductName { get; set; }
        public string Source { get; set; }
        public string SourceName { get; set; }
        public string Born { get; set; }
        public int WhetherTax { get; set; }
        public int PurchaseType { get; set; }
        public int PaymentMethod { get; set; }
        public int TransferAccount { get; set; }
        public int ApplyStatus { get; set; }
        public DateTime InHouseTime { get; set; }
        public int PurchaseInStoreType { get; set; }
        public string GoodsName { get; set; }
        public string PurchaserName { get; set; }
        public string CreateAwb { get; set; }
        public string CreateDate { get; set; }
        public int InPiece { get; set; }
        public int InCargoStatus { get; set; }
        public int IsDocument { get; set; }
        /// <summary>
        /// 父表id集合
        /// </summary>
        public List<long> PurOrderArrID { get; set; }
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