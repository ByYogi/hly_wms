using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 移库数据实体Tbl_Cargo_MoveOrder（移库订单表）
    /// </summary>
    [Serializable]
    public class CargoMoveOrderEntity
    {
        public long ID { get; set; }
        [Description("移库单号")]
        public string MoveNo { get; set; }
        [Description("原仓库")]
        public string OldHouseName { get; set; }
        public string OldHouseID { get; set; }
        [Description("目标仓库")]
        public string NewHouseName { get; set; }
        public int NewHouseID { get; set; }
        public int NewAreaID { get; set; }
        [Description("移库数量")]
        public int MoveNum { get; set; }
        [Description("移库状态")]
        public string MoveStatus { get; set; }
        [Description("备注")]
        public string Memo { get; set; }
        [Description("操作人")]
        public string OPID { get; set; }
        public string UserName { get; set; }
        public DateTime OP_DATE { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ScanNum { get; set; }
        public string IsSaveIncomming { get; set; }
        public string Specs { get; set; }

        /// <summary>
        /// 物流公司运单号
        /// </summary>
        public string LogisAwbNo { get; set; }
        /// <summary>
        /// 物流公司ID
        /// </summary>
        public int LogisID { get; set; }
        public int ParentID { get; set; }
        public int TypeID { get; set; }
        public List<CargoMoveOrderGoodsEntity> MoveGoodsList { get; set; }
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
    /// 移库数据实体明细
    /// </summary>
    [Serializable]
    public class CargoMoveOrderGoodsEntity
    {
        public string MoveID { get; set; }
        public string MoveNo { get; set; }
        public long ProductID { get; set; }
        public long ContainerGoodsID { get; set; }
        public int Piece { get; set; }
        public decimal ActSalePrice { get; set; }
        public string OPID { get; set; }
        public DateTime OP_DATE { get; set; }
        public long NewProductID { get; set; }
        public int NewContainerID { get; set; }
        public int NewPiece { get; set; }
        public int OldPiece { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public string GoodsCode { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string Batch { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int ContainerID { get; set; }
        public string MoveStatus { get; set; }
        /// <summary>
        /// 已扫描数量
        /// </summary>
        public int ScanNum { get; set; }
        /// <summary>
        /// 未扫描数量
        /// </summary>
        public int NoScanNum { get; set; }
        public int MoveNum { get; set; }
        public bool IsAdd { get; set; }
        public string ContainerCode { get; set; }
        public string AreaName { get; set; }
        public string FirstAreaName { get; set; }
        public string HouseName { get; set; }
        public string SalePrice { get; set; }
        public string UnitPrice { get; set; }
        public string BelongDepart { get; set; }
        public string NewHouseName { get; set; }
        public string Memo { get; set; }
        public string SourceName { get; set; }
        public string TagCode { get; set; }
        public string TyreCode { get; set; }
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
