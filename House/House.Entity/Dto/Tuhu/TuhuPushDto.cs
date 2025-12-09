using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto.Tuhu
{
    public class TuhuPushDto
    {
        public string platform { get; set; }
        public string authCode { get; set; }
        public List<string> erpSkuCodes { get; set; }
        public List<Tuhu_Sku> skuInfos { get; set; }
        public List<Tuhu_Stock> stockInfos { get; set; }
        public List<Tuhu_Price> priceInfos { get; set; }

        public TuhuPushDto() { }
        public TuhuPushDto(string platform, string authCode, List<Tuhu_Sku> skuInfos = null)
        {
            this.platform = platform;
            this.authCode = authCode;
            this.skuInfos = skuInfos;
        }
    }
    public class Tuhu_Sku
    {
        public int version { get; set; }
        public string erpSkuCode { get; set; } //我方SKU编码（ProductCode）
        public string tuhuSkuCode { get; set; } //途虎SKU编码
        public string ProductCode { get; set; }
        public int Piece { get; set; }
        public decimal SalePrice { get; set; }
        public string GoodsCode { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string Model { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public int HubDiameter { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string Born { get; set; }

    }

    public class Tuhu_Stock
    {
        public int version { get; set; }
        public string erpSkuCode { get; set; } //我方SKU编码（ProductCode）
        public string tuhuSkuCode { get; set; } //途虎SKU编码
        public int Piece { get; set; }
    }
    public class Tuhu_Price
    {
        public int version { get; set; }
        public string erpSkuCode { get; set; } //我方SKU编码（ProductCode）
        public string tuhuSkuCode { get; set; } //途虎SKU编码
        public decimal SalePrice { get; set; }
    }
}
