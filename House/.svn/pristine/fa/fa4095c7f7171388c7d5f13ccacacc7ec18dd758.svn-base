using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using House.Entity.Cargo;

namespace Cargo.Order
{
    public partial class PreOrderManager : BasePage
    {
        public string Un = string.Empty;
        public string Ln = string.Empty;
        public string HouseName = string.Empty;
        public string PickTitle = string.Empty;
        public string SendTitle = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Un = UserInfor.UserName.Trim();
            Ln = UserInfor.LoginName.Trim();
            HouseName = UserInfor.HouseName.Trim();
            PickTitle = UserInfor.PickTitle;
            SendTitle = UserInfor.SendTitle;
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<CargoContainerShowEntity> massExportOrder
        {
            get
            {
                if (Session["massExportOrder"] == null) { Session["massExportOrder"] = new List<CargoContainerShowEntity>(); } return (List<CargoContainerShowEntity>)(Session["massExportOrder"]);
            }
            set
            {
                Session["massExportOrder"] = value;
            }
        }
        /// <summary>
        /// 导出类型实体
        /// </summary>
        public string massExportOrderType
        {
            get
            {
                if (Session["massExportOrderType"] == null)
                {
                    Session["massExportOrderType"] = "";
                }
                return (string)(Session["massExportOrderType"]);
            }
            set
            {
                Session["massExportOrderType"] = value;
            }
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<CargoContainerShowEntity> tagCodeExport
        {
            get
            {
                if (Session["tagCodeExport"] == null) { Session["tagCodeExport"] = new List<CargoContainerShowEntity>(); } return (List<CargoContainerShowEntity>)(Session["tagCodeExport"]);
            }
            set
            {
                Session["tagCodeExport"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (massExportOrder.Count <= 0) { return; }
            if (string.IsNullOrEmpty(massExportOrderType)) { return; }

            if (massExportOrderType == "47")
            {
                string tname = string.Empty;
                DataTable table = new DataTable();
                table.Columns.Add("序号", typeof(int));
                table.Columns.Add("名称", typeof(string));
                table.Columns.Add("规格", typeof(string));
                table.Columns.Add("批次", typeof(string));
                table.Columns.Add("数量", typeof(int));
                table.Columns.Add("货位代码", typeof(string));
                table.Columns.Add("所在区域", typeof(string));
                table.Columns.Add("目的站", typeof(string));
                table.Columns.Add("收件人", typeof(string));
                table.Columns.Add("运单号", typeof(string));
                List<CargoOrderEntity> tot = new List<CargoOrderEntity>();
                int i = 0;
                foreach (var it in massExportOrder)
                {
                    if (tot.Exists(c => c.SaleManName == (it.AreaName + " " + it.ProductName + " " + it.Specs + " " + it.GoodsCode)))
                    {
                        CargoOrderEntity co = tot.Find(c => c.SaleManName == (it.AreaName + " " + it.ProductName + " " + it.Specs + " " + it.GoodsCode));
                        co.Piece += it.Piece;
                    }
                    else
                    {
                        tot.Add(new CargoOrderEntity { SaleManName = (it.AreaName + " " + it.ProductName + " " + it.Specs + " " + it.GoodsCode), Piece = it.Piece });
                    }
                    i++;
                    DataRow newRows = table.NewRow();
                    newRows["序号"] = i;
                    newRows["名称"] = it.ProductName.Trim();
                    newRows["规格"] = it.Specs.Trim();
                    newRows["批次"] = it.Batch;
                    newRows["数量"] = it.Piece;
                    newRows["货位代码"] = it.ContainerCode.Trim();
                    newRows["所在区域"] = it.AreaName.Trim();
                    newRows["目的站"] = it.Dest.Trim();
                    newRows["收件人"] = it.AcceptPeople.Trim();
                    newRows["运单号"] = it.LogisAwbNo;
                    table.Rows.Add(newRows);

                }
                DataRow cR1 = table.NewRow();
                table.Rows.Add(cR1);
                DataRow cR2 = table.NewRow();
                table.Rows.Add(cR2);
                foreach (var t in tot)
                {
                    DataRow newRows = table.NewRow();

                    newRows["规格"] = t.SaleManName;
                    newRows["数量"] = t.Piece;
                    table.Rows.Add(newRows);
                }
                DataRow cR3 = table.NewRow();
                cR3["规格"] = "总计";
                cR3["数量"] = tot.Sum(c => c.Piece).ToString();
                table.Rows.Add(cR3);
                ToExcel.DataTableToExcel(table, "", DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日" + massExportOrder[0].ProductName + "仓库拣货单");
            }
            else
            {
                string tname = string.Empty;
                DataTable table = new DataTable();
                table.Columns.Add("序号", typeof(int));
                table.Columns.Add("规格", typeof(string));
                table.Columns.Add("型号", typeof(string));
                table.Columns.Add("花纹", typeof(string));
                table.Columns.Add("批次", typeof(string));
                table.Columns.Add("数量", typeof(int));
                table.Columns.Add("货位代码", typeof(string));
                table.Columns.Add("所在区域", typeof(string));
                table.Columns.Add("目的站", typeof(string));
                table.Columns.Add("收件人", typeof(string));
                table.Columns.Add("运单号", typeof(string));
                List<CargoOrderEntity> tot = new List<CargoOrderEntity>();
                int i = 0;
                foreach (var it in massExportOrder)
                {
                    if (tot.Exists(c => c.SaleManName == (it.AreaName + " " + it.Specs + " " + it.Model + " " + it.Figure)))
                    {
                        CargoOrderEntity co = tot.Find(c => c.SaleManName == (it.AreaName + " " + it.Specs + " " + it.Model + " " + it.Figure));
                        co.Piece += it.Piece;
                    }
                    else
                    {
                        tot.Add(new CargoOrderEntity { SaleManName = (it.AreaName + " " + it.Specs + " " + it.Model + " " + it.Figure), Piece = it.Piece });
                    }
                    i++;
                    DataRow newRows = table.NewRow();
                    newRows["序号"] = i;
                    newRows["规格"] = it.Specs.Trim();
                    newRows["型号"] = it.Model.Trim();
                    newRows["花纹"] = it.Figure.Trim();
                    newRows["批次"] = it.Batch;
                    newRows["数量"] = it.Piece;
                    newRows["货位代码"] = it.ContainerCode.Trim();
                    newRows["所在区域"] = it.AreaName.Trim();
                    newRows["目的站"] = it.Dest.Trim();
                    newRows["收件人"] = it.AcceptPeople.Trim();
                    newRows["运单号"] = it.LogisAwbNo;
                    table.Rows.Add(newRows);

                }
                DataRow cR1 = table.NewRow();
                table.Rows.Add(cR1);
                DataRow cR2 = table.NewRow();
                table.Rows.Add(cR2);
                foreach (var t in tot)
                {
                    DataRow newRows = table.NewRow();

                    newRows["花纹"] = t.SaleManName;
                    newRows["数量"] = t.Piece;
                    table.Rows.Add(newRows);
                }
                DataRow cR3 = table.NewRow();
                cR3["花纹"] = "总计";
                cR3["数量"] = tot.Sum(c => c.Piece).ToString();
                table.Rows.Add(cR3);
                ToExcel.DataTableToExcel(table, "", DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日" + massExportOrder[0].ProductName + "仓库拣货单");
            }
        }
    }
}