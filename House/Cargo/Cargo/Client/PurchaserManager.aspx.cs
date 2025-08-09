using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Client
{
    public partial class PurchaserManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoPurchaserEntity> QueryCargoPurchaserList
        {
            get
            {
                if (Session["QueryCargoPurchaserList"] == null)
                {
                    Session["QueryCargoPurchaserList"] = new List<CargoPurchaserEntity>();
                }
                return (List<CargoPurchaserEntity>)(Session["QueryCargoPurchaserList"]);
            }
            set
            {
                Session["QueryCargoPurchaserList"] = value;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (QueryCargoPurchaserList.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("名称", typeof(string));
            table.Columns.Add("类型", typeof(string));
            table.Columns.Add("负责人", typeof(string));
            table.Columns.Add("手机", typeof(string));
            table.Columns.Add("电话", typeof(string));
            table.Columns.Add("所在省", typeof(string));
            table.Columns.Add("所在市", typeof(string));
            table.Columns.Add("地址", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            int i = 0;
            string OrderNo = string.Empty;
            foreach (var it in QueryCargoPurchaserList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["名称"] = it.PurchaserName;
                newRows["类型"] = it.PurchaserType == 0 ? "普通采购商" : "月结采购商";
                newRows["负责人"] = it.Boss;
                newRows["手机"] = it.Cellphone;
                newRows["电话"] = it.Telephone;
                newRows["所在省"] = it.Province;
                newRows["所在市"] = it.City;
                newRows["地址"] = it.Address;
                newRows["所属仓库"] = it.HouseName;
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "采购商信息");
        }
    }
}