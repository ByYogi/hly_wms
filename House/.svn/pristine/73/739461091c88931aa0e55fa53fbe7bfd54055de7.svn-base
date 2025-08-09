using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 采购订单表 Tbl_Cargo_RealFactoryPurchaseOrder
    /// 描述：系统订单数据表，维护客户订单数据信息
    /// </summary>
    [Serializable]
    public class CargoRealFactoryPurchaseOrderEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID")]
        public long PurOrderID { get; set; }

        /// <summary>
        /// 采购单号
        /// </summary>
        [Description("采购单号")]
        public string PurOrderNo { get; set; }
        /// <summary>
        /// 采购部门编码
        /// </summary>
        [Description("采购部门编码")]
        public int PurDepartID { get; set; }
        [Description("采购部门名称")]
        public string PurDepart { get; set; }

        /// <summary>
        /// 采购总数量
        /// </summary>
        [Description("采购总数量")]
        public int Piece { get; set; }

        /// <summary>
        /// 工厂回告总数量
        /// </summary>
        [Description("工厂回告总数量")]
        public int ReplyNum { get; set; }

        /// <summary>
        /// 采购单费用
        /// </summary>
        [Description("采购单费用")]
        public decimal TransportFee { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        [Description("其他费用")]
        public decimal OtherFee { get; set; }

        /// <summary>
        /// 合计费用
        /// </summary>
        [Description("合计费用")]
        public decimal TotalCharge { get; set; }

        /// <summary>
        /// 是否含税
        /// 0：不含税
        /// 1：含税
        /// </summary>
        [Description("是否含税")]
        public string WhetherTax { get; set; }

        /// <summary>
        /// 审批状态
        /// 0未审批1已审批2审批中
        /// </summary>
        [Description("审批状态")]
        public string CheckStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 供应商表ID
        /// </summary>
        [Description("供应商表ID")]
        public int PurchaserID { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string PurchaserName { get; set; }
        /// <summary>
        /// 提货地址
        /// </summary>
        [Description("提货地址")]
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// 提货电话
        /// </summary>
        [Description("提货电话")]
        public string DeliveryTelephone { get; set; }

        /// <summary>
        /// 提货负责人
        /// </summary>
        [Description("提货负责人")]
        public string DeliveryBoss { get; set; }

        /// <summary>
        /// 提货手机号码
        /// </summary>
        [Description("提货手机号码")]
        public string DeliveryCellphone { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        [Description("所在城市")]
        public string DeliveryCity { get; set; }

        /// <summary>
        /// 所在区
        /// </summary>
        [Description("所在区")]
        public string DeliveryCountry { get; set; }

        /// <summary>
        /// 所在省份
        /// </summary>
        [Description("所在省份")]
        public string DeliveryProvince { get; set; }

        /// <summary>
        /// 采购单类型
        /// 0:工厂采购单1:市场采购单
        /// </summary>
        [Description("采购单类型")]
        public string PurchaseType { get; set; }

        /// <summary>
        /// 采购单入库类型
        /// 0:入仓单1:流转单2:提送单
        /// </summary>
        [Description("采购单入库类型")]
        public string PurchaseInStoreType { get; set; }

        /// <summary>
        /// 开单员ID
        /// </summary>
        [Description("开单员ID")]
        public string CreateAwbID { get; set; }

        /// <summary>
        /// 开单员
        /// </summary>
        [Description("开单员")]
        public string CreateAwb { get; set; }

        /// <summary>
        /// 开单时间
        /// </summary>
        [Description("开单时间")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        [Description("操作员")]
        public string OP_ID { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }

        /// <summary>
        /// 申请人ID
        /// </summary>
        [Description("申请人ID")]
        public string ApplyID { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        [Description("申请人姓名")]
        public string ApplyName { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [Description("申请时间")]
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// 下一审批人ID
        /// </summary>
        [Description("下一审批人ID")]
        public string NextCheckID { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        [Description("审批人姓名")]
        public string NextCheckName { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        [Description("审批时间")]
        public DateTime CheckTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [Description("审批意见")]
        public string CheckResult { get; set; }

        /// <summary>
        /// 申请审批状态
        /// 0:提交申请
        /// 1:审批通过
        /// 2:审批拒绝
        /// 3:审批结束
        /// </summary>
        [Description("申请审批状态")]
        public string ApplyStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long TID { get; set; }
        public string BusinessID { get; set; }
        /// <summary>
        /// 付款方式 0:月结 1:周结 2:现结
        /// </summary>
        public int? PaymentMethod { get; set; }
        /// <summary>
        /// 转账账户 0： 对公、1：对私
        /// </summary>
        public int? TransferAccount { get; set; }
        public string PurchaseInStoreState { get; set; }
        public string HouseID { get; set; }
        public string Telephone { get; set; }
        public string PurchaseUploadDoc { get; set; }
        /// <summary>
        /// 入库总数 查询用
        /// </summary>
        public int InCargoQty { get; set; }
        /// <summary>
        /// 已入库总数 查询用
        /// </summary>
        public int fInCargoQty { get; set; }
        /// <summary>
        /// 上传单据否
        /// </summary>
        public int IsDocument { get; set; }
       
        /// <summary>
        /// 按分仓汇总数据列表
        /// </summary>
        public List<CargoRealFactoryPurchaseHouseEntity> purchaseHouseEntities { get; set; }
        /// <summary>
        /// 采购订单明细数据列表
        /// </summary>
        public List<CargoRealFactoryPurchaseOrderGoodsEntity> purchaseOrderGoodsEntities { get; set; }
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


    public class CargoRealFactoryPurchaseOrderEntityFile
    {
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int HouseID { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurOrderNo { get; set; }
        /// <summary>
        /// 来源ID
        /// </summary>
        public int Source { get; set; }
        /// <summary>
        /// 来源名称
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 品牌ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public int Batch { get; set; }
       /// <summary>
       /// 产地
       /// </summary>
        public string Born { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public string LoadIndex { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string SpeedLevel { get; set; }
        /// <summary>
        /// 花纹
        /// </summary>
        public string Figure { get; set; }
        /// <summary>
        /// 货品代码
        /// </summary>
        public string GoodsCode { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Piece { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal TotalCharge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int WhetherTax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PurDepart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PurchaserID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PurchaserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PurchaseType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PurchaseInStoreType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ApproveID { get; set; }
        public int BusinessID { get; set; }
        public string GtmcNo { get; set; }
        public string TID { get; set; }
        public string DAID { get; set; }
        public string PurDepartID{ get; set; }
        public string PurDepartName{ get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryTelephone { get; set; }
        public string DeliveryBoss { get; set; }
        public string DeliveryCellphone { get; set; }
        public string PurchaserBoss { get; set; }
        public string PurchaserAddress { get; set; }
        public string PurchaserCellphone { get; set; }
        public string PaymentMethod { get; set; }
        public string TransferAccount { get; set; }
    }
}