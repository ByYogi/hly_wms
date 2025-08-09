using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 客户对账单数据 实体
    /// </summary>
    [Serializable]
    public class APPClientAccountEntity
    {
        /// <summary>
        /// 账单号
        /// </summary>
        public string AccountID { get; set; }
        /// <summary>
        /// 账单名称
        /// </summary>
        public string AccountTitle { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public long ClientID { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public int ClientNum { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 账单金额
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// 税费
        /// </summary>
        public decimal TaxFee { get; set; }
        /// <summary>
        /// 其它费用
        /// </summary>
        public decimal OtherFee { get; set; }
        /// <summary>
        /// 账单备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 是否签名 0:未签名1:已签名
        /// </summary>
        public string ElecSign { get; set; }
        /// <summary>
        /// 结算状态
        /// </summary>
        public string CheckStatus { get; set; }
        /// <summary>
        /// 账单类型
        /// </summary>
        public string AType { get; set; }
        /// <summary>
        /// 账单月
        /// </summary>
        public string AccountDate { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int HouseID { get; set; }
        /// <summary>
        /// 客户剩余返利总金额
        /// </summary>
        public decimal RebateMoney { get; set; }
        /// <summary>
        /// 客户上月来款总金额
        /// </summary>
        public decimal IncomeMoney { get; set; }
        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal NoPayMoney { get; set; }
        /// <summary>
        /// 查询条件 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 查询条件 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 客户订单数据列表
        /// </summary>
        public List<APPClientOrderEntity> OrderList { get; set; }
        /// <summary>
        /// 客户返利数据列表
        /// </summary>
        public List<APPClientRebateEntity> RebateList { get; set; }
        /// <summary>
        /// 客户来款数据列表
        /// </summary>
        public List<APPClientIncomeMoneyEntity> IncomeList { get; set; }
    }

    /// <summary>
    /// 客户返利数据实体
    /// </summary>
    [Serializable]
    public class APPClientRebateEntity
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public long ClientID { get; set; }
        /// <summary>
        /// 返利类型 0:报销返利1:延保返利2:补贴返利3:好评返利4:ROSS返利
        /// </summary>
        public string RebateType { get; set; }
        /// <summary>
        /// 返利金额
        /// </summary>
        public decimal RebateMoney { get; set; }
        /// <summary>
        /// 返利月份
        /// </summary>
        public DateTime RebateDate { get; set; }
        /// <summary>
        /// 查询条件 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 查询条件 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }

    /// <summary>
    /// 客户订单数据实体
    /// </summary>
    [Serializable]
    public class APPClientOrderEntity
    {
        /// <summary>
        /// 账单号
        /// </summary>
        public string AccountID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单总数量
        /// </summary>
        public int Piece { get; set; }
        /// <summary>
        /// 订单总金额 
        /// </summary>
        public decimal TotalCharge { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string AcceptPeople { get; set; }
        /// <summary>
        /// 收货人手机号码
        /// </summary>
        public string AcceptCellphone { get; set; }
        /// <summary>
        /// 开单时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 业务员名字
        /// </summary>
        public string SaleManName { get; set; }
        /// <summary>
        /// 订单类型0:客户订单1:退货单
        /// </summary>
        public string OrderModel { get; set; }
        /// <summary>
        /// 结算状态0:未结算1:已结清2:未结清
        /// </summary>
        public string CheckStatus { get; set; }
        /// <summary>
        /// 订单产品明细列表
        /// </summary>
        public List<APPClientOrderGoodsEntity> OrderGoodsList { get; set; }
    }

    /// <summary>
    /// 客户订单与产品关联数据实体
    /// </summary>
    [Serializable]
    public class APPClientOrderGoodsEntity
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 花纹
        /// </summary>
        public string Figure { get; set; }
        /// <summary>
        /// 载重指数
        /// </summary>
        public string LoadIndex { get; set; }
        /// <summary>
        /// 速度级别
        /// </summary>
        public string SpeedLevel { get; set; }
        /// <summary>
        /// 周期批次
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 周期年
        /// </summary>
        public int BatchYear { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int Piece { get; set; }
        /// <summary>
        /// 订单单价
        /// </summary>
        public decimal ActSalePrice { get; set; }

    }

    /// <summary>
    /// 客户来款数据实体
    /// </summary>
    [Serializable]
    public class APPClientIncomeMoneyEntity
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public long ClientID { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public int ClientNum { get; set; }
        /// <summary>
        /// 来款金额
        /// </summary>
        public decimal IncomeMoney { get; set; }
        /// <summary>
        /// 来款日期
        /// </summary>
        public DateTime IncomeDate { get; set; }
        /// <summary>
        /// 查询条件 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 查询条件 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }

}
