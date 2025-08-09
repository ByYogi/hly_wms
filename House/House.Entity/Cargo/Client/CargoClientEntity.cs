using House.Entity.Cargo.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 客户数据实体
    /// </summary>
    [Serializable]
    public class CargoClientEntity
    {
        [Description("表主键")]
        public long ClientID { get; set; }
        [Description("客户编码")]
        public int ClientNum { get; set; }
        /// <summary>
        /// 客户编码集合
        /// </summary>
        public string ClientNums { get; set; }
        [Description("客户名称")]
        public string ClientName { get; set; }
        [Description("客户简称")]
        public string ClientShortName { get; set; }
        [Description("客户类型")]
        public string ClientType { get; set; }
        [Description("公司地址")]
        public string Address { get; set; }
        [Description("公司邮编")]
        public string ZipCode { get; set; }
        [Description("公司电话")]
        public string Telephone { get; set; }
        [Description("公司传真号码")]
        public string Fax { get; set; }
        [Description("公司负责人")]
        public string Boss { get; set; }
        [Description("联系手机")]
        public string Cellphone { get; set; }
        [Description("公司邮件地址")]
        public string Email { get; set; }
        [Description("经营产品")]
        public string Product { get; set; }
        [Description("删除标识")]
        public string DelFlag { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("店代码")]
        public string ShopCode { get; set; }
        /// <summary>
        /// 客户经营产品ID
        /// </summary>
        public string ClientTypeID { get; set; }
        /// <summary>
        /// 客户经营产品名称
        /// </summary>
        public string ClientTypeName { get; set; }
        /// <summary>
        /// 入驻仓库ID
        /// </summary>
        public string SettleHouseID { get; set; }
        /// <summary>
        /// 入驻仓库名称
        /// </summary>
        public string SettleHouseName { get; set; }
        /// <summary>
        /// 地推人员
        /// </summary>
        public string GroundPushName { get; set; }

        /// <summary>
        /// 归属业务员
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 归属业务员姓名 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 市场业务员
        /// </summary>
        public string BusUserID { get; set; }
        /// <summary>
        /// 市场业务员姓名 
        /// </summary>
        public string BusUserName { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 所在省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 所在区
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 所在仓库
        /// </summary>
        public int HouseID { get; set; }
        public string HouseIDStr { get; set; }
        /// <summary>
        /// 所在仓库
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 查询所属仓库订单
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 开户名
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string CardNum { get; set; }
        /// <summary>
        /// 预存款
        /// </summary>
        public decimal PreReceiveMoney { get; set; }
        /// <summary>
        /// 返利款
        /// </summary>
        public decimal RebateMoney { get; set; }
        /// <summary>
        /// 月结透支额度 
        /// </summary>
        public decimal LimitMoney { get; set; }
        /// <summary>
        /// 我的申请额度
        /// </summary>
        public decimal QuotaMoney { get; set; }

        /// <summary>
        /// 目标销量
        /// </summary>
        public int TargetNum { get; set; }
        /// <summary>
        /// 额度付款权限 
        /// </summary>
        public string QuotaLimit { get; set; }
        /// <summary>
        /// 轮胎公司客户编码
        /// </summary>
        [Description("轮胎公司客户编码")]
        public string TryeClientCode { get; set; }
        /// <summary>
        /// 轮胎公司名称
        /// </summary>
        [Description("轮胎公司代码")]
        public string TryeCompany { get; set; }
        [Description("门店类型")]
        public string TryeClientType { get; set; }

        [Description("配送物流")]
        public string LogisName { get; set; }
        [Description("配送物流ID")]
        public int LogisID { get; set; }
        /// <summary>
        /// 路线承运商ID
        /// </summary>
        public int LogisLineLogisID { get; set; }
        /// <summary>
        /// 物流配送费，每条多少钱
        /// </summary>
        public decimal LogisFee { get; set; }
        [Description("地址经度")]
        public string Longitude { get; set; }
        [Description("地址纬度")]
        public string Latitude { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string AcceptPeople { get; set; }
        /// <summary>
        /// 是否推送账单
        /// </summary>
        public string PushAccount { get; set; }
        /// <summary>
        /// 客户资料审核
        /// </summary>
        public string BookCheck { get; set; }
        public string AddData { get; set; }
        [Description("付款方式")]
        public string CheckOutType { get; set; }
        public decimal SelFee { get; set; }
        public decimal PurFee { get; set; }
        public decimal SelAffectTotal { get; set; }
        public decimal PurAffectTotal { get; set; }
        public decimal Weight { get; set; }
        public int RowNumber { get; set; }
        public int SumPiece { get; set; }
        public int UserRulePiece { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LoginPwd { get; set; }
        public string UpClientShortName { get; set; }
        public int UpClientID { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal Discount { get; set; }

        public string LineName { get; set; }
        public string BelongHouse { get; set; }
        public string ArrivePayLimit { get; set; }
        public int LineID { get; set; }
        /// <summary>
        /// 分账客户编码（通联)
        /// </summary>
        public string BizUserId { get; set; }
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
    /// 客户收货地址数据实体1.3.2.Tbl_Cargo_AcceptAddress（客户收货地址管理）
    /// </summary>
    [Serializable]
    public class CargoClientAcceptAddressEntity
    {
        public long ADID { get; set; }
        public long ClientID { get; set; }
        [Description("客户编码")]
        public int ClientNum { get; set; }
        [Description("收货省")]
        public string AcceptProvince { get; set; }
        [Description("收货市")]
        public string AcceptCity { get; set; }
        [Description("收货区")]
        public string AcceptCountry { get; set; }
        [Description("收货单位")]
        public string AcceptCompany { get; set; }
        [Description("收货地址")]
        public string AcceptAddress { get; set; }
        [Description("收货人")]
        public string AcceptPeople { get; set; }
        [Description("收货电话")]
        public string AcceptTelephone { get; set; }
        [Description("手机号码")]
        public string AcceptCellphone { get; set; }
        /// <summary>
        /// 轮胎公司客户编码
        /// </summary>
        [Description("轮胎公司客户编码")]
        public string TryeClientCode { get; set; }
        /// <summary>
        /// 轮胎公司名称
        /// </summary>
        [Description("轮胎公司代码")]
        public string TryeCompany { get; set; }
        [Description("门店类型")]
        public string TryeClientType { get; set; }
        public int TargetNum { get; set; }
        [Description("地址经度")]
        public string Longitude { get; set; }
        [Description("地址纬度")]
        public string Latitude { get; set; }
        public int isDefault { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 所在仓库
        /// </summary>
        [Description("所属仓库")]
        public int HouseID { get; set; }
        /// <summary>
        /// 所在仓库
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 查询所属仓库订单
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 配送物流名称
        /// </summary>
        public string LogisName { get; set; }
        /// <summary>
        /// 配送物流ID
        /// </summary>
        public int LogisID { get; set; }
        /// <summary>
        /// 收货人与电话
        /// </summary>
        public string AcceptPeopleCellphone { get; set; }
        public string ClientShortName { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
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
    /// 客户预存款收支记录表
    /// </summary>
    [Serializable]
    public class CargoClientPreRecordEntity
    {
        public string Boss { get; set; }
        public string ClientName { get; set; }
        public int ClientNum { get; set; }
        public int ID { get; set; }
        public long ClientID { get; set; }
        public string ExID { get; set; }
        public decimal Money { get; set; }
        public string RecordType { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAdd { get; set; }
        public string RebateType { get; set; }
        public string TryeClientCode { get; set; }
        public int TypeID { get; set; }
        public DateTime RebateDate { get; set; }
        public string RebateMonth { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string OperaType { get; set; }
        /// <summary>
        /// 延保返利
        /// </summary>
        public decimal YBMoney { get; set; }
        /// <summary>
        /// 补贴返利
        /// </summary>
        public decimal BTMoney { get; set; }
        /// <summary>
        /// 好评返利
        /// </summary>
        public decimal HPMoney { get; set; }
        /// <summary>
        /// ROSS返利
        /// </summary>
        public decimal ROSSMoney { get; set; }
        /// <summary>
        /// 返利
        /// </summary>
        public decimal RebateMoney { get; set; }
        /// <summary>
        /// 月度达成返利
        /// </summary>
        public decimal MonthMoney { get; set; }
        /// <summary>
        /// 18寸达成返利
        /// </summary>
        public decimal ShiBaMoney { get; set; }
        /// <summary>
        /// 早期订单返利
        /// </summary>
        public decimal StageOrderMoney { get; set; }
        /// <summary>
        /// 季度达成返利
        /// </summary>
        public decimal QuaterMoney { get; set; }
        /// <summary>
        /// 追加促销返利
        /// </summary>
        public decimal PromotionMoney { get; set; }
        /// <summary>
        /// 预收款
        /// </summary>
        public decimal PreReceiveMoney { get; set; }
        public string CargoPermisID { get; set; }
        public string Remark { get; set; }
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
    /// 客户入驻仓库表
    /// </summary>
    [Serializable]
    public class CargoSettleHouseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Description("ID")]
        public int ID { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        [Description("客户ID")]
        public int ClientID { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        [Description("客户编码")]
        public int ClientNum { get; set; }
        /// <summary>
        /// 入驻品牌ID
        /// </summary>
        [Description("入驻品牌ID")]
        public string ClientTypeID { get; set; }
        /// <summary>
        /// 入驻品牌名称
        /// </summary>
        [Description("入驻品牌名称")]
        public string ClientTypeName { get; set; }
        /// <summary>
        /// 入驻仓库ID
        /// </summary>
        [Description("入驻仓库ID")]
        public string SettleHouseID { get; set; }
        /// <summary>
        /// 入驻仓库名称
        /// </summary>
        [Description("入驻仓库名称")]
        public string SettleHouseName { get; set; }

        /// <summary>
        /// 分账客户编码（通联）
        /// </summary>
        [Description("分账客户编码")]
        public string BizUserId { get; set; }

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
    public class CargoSettleHouseUpdateEntity
    {
        public string OldSettleHouseID { get; set; }
        public List<CargoSupplierProductPrice> ProductPrices { get; set; }
        public string DelClientTypeID { get; set; }
        public List<CargoSupplierProductPrice> AddProductPrices { get; set; }
    }
}
