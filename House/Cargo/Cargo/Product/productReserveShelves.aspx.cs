using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using House.Entity.Cargo;

namespace Cargo.Product
{
    public partial class productReserveShelves : BasePage
    {
        public int HouseID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            HouseID = UserInfor.HouseID;
        }

        /// <summary>
        /// 商品上架导出实体
        /// </summary>
        public List<CargoContainerShowEntity> ShelvesExportData
        {
            get
            {
                if (Session["ShelvesExportData"] == null)
                {
                    Session["ShelvesExportData"] = new List<CargoContainerShowEntity>();
                }
                return (List<CargoContainerShowEntity>)(Session["ShelvesExportData"]);
            }
            set
            {
                Session["ShelvesExportData"] = value;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (ShelvesExportData.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("产品ID", typeof(string));
            table.Columns.Add("货位代码", typeof(string));
            table.Columns.Add("上架数量", typeof(string));
            table.Columns.Add("在库数量", typeof(string));
            table.Columns.Add("销售类型", typeof(string));
            table.Columns.Add("积分数量", typeof(string));
            table.Columns.Add("商城价格", typeof(string));
            table.Columns.Add("产品类型", typeof(string));
            table.Columns.Add("型号", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载重指数", typeof(string));
            table.Columns.Add("速度级别", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("销售价", typeof(string));
            table.Columns.Add("门店价", typeof(string));
            table.Columns.Add("产品名称", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("所在仓库", typeof(string));
            table.Columns.Add("一级区域", typeof(string));
            table.Columns.Add("二级区域", typeof(string));
            table.Columns.Add("上架状态", typeof(string));
            table.Columns.Add("操作时间", typeof(string));
            foreach (var it in ShelvesExportData)
            {
                DataRow newRows = table.NewRow();
                newRows["产品ID"] = it.ProductID;
                newRows["货位代码"] = it.ContainerCode.Trim();
                newRows["上架数量"] = it.ShelvesNum;
                newRows["在库数量"] = it.Piece;
                newRows["销售类型"] = GetSaleType(it.SaleType.Trim());
                newRows["积分数量"] = it.Consume;
                newRows["商城价格"] = it.ProductPrice;
                newRows["产品类型"] = it.TypeName.Trim();
                newRows["型号"] = it.Model.Trim();
                newRows["规格"] = it.Specs.Trim();
                newRows["花纹"] = it.Figure.Trim();
                newRows["载重指数"] = it.LoadIndex;
                newRows["速度级别"] = it.SpeedLevel.Trim();
                newRows["批次"] = it.Batch;
                newRows["销售价"] = it.SalePrice;
                newRows["门店价"] = it.TradePrice;
                newRows["产品名称"] = it.ProductName;
                newRows["货品代码"] = it.GoodsCode;
                newRows["所在仓库"] = it.ParentAreaName;
                newRows["一级区域"] = it.FirstAreaName;
                newRows["二级区域"] = it.AreaName;
                newRows["上架状态"] = GetSaleType(it.OnShelves.Trim());
                newRows["操作时间"] = it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "商品上架导出数据");
        }

        public static string GetSaleType(string type)
        {
            string value="";
            switch (type)
            {
                case "0":
                    value = "正价销售";
                    break;
                case "1":
                    value = "天天特价";
                    break;
                case "3":
                    value = "限时促销";
                    break;
                case "4":
                    value = "积分兑换";
                    break;
            }
            return value;
        }
        public static string GetOnShelves(string type)
        {
            string value = "";
            switch (type)
            {
                case "0":
                    value = "已上架";
                    break;
                case "1":
                    value = "未上架";
                    break;
            }
            return value;
        }

    }
}