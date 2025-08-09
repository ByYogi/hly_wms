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
    public partial class wxClientCheck : BasePage
    {
        public List<WXUserEntity> WxClient
        {
            get
            {
                if (Session["WxClient"] == null)
                {
                    Session["WxClient"] = new List<WXUserEntity>();
                }
                return (List<WXUserEntity>)(Session["WxClient"]);
            }
            set
            {
                Session["WxClient"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (WxClient.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("用户姓名", typeof(string));
            table.Columns.Add("公司名称", typeof(string));
            table.Columns.Add("微信名称", typeof(string));
            table.Columns.Add("OpenID", typeof(string));
            table.Columns.Add("手机号码", typeof(string));
            table.Columns.Add("实名认证", typeof(string));
            table.Columns.Add("性别", typeof(string));
            table.Columns.Add("所在省", typeof(string));
            table.Columns.Add("所在市", typeof(string));
            table.Columns.Add("所在区", typeof(string));
            table.Columns.Add("公司地址", typeof(string));
            table.Columns.Add("营业执照照片", typeof(string));
            table.Columns.Add("身份证正面照片", typeof(string));
            table.Columns.Add("身份证反面照片", typeof(string));
            table.Columns.Add("注册时间", typeof(string));
            table.Columns.Add("绑定店代码", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("绑定时间", typeof(string));
            table.Columns.Add("拒审原因", typeof(string));
            int i = 0;
            foreach (var it in WxClient)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["用户姓名"] = it.Name.Trim();
                newRows["公司名称"] = it.CompanyName.Trim();
                newRows["微信名称"] = it.wxName;
                newRows["OpenID"] = it.wxOpenID;
                newRows["手机号码"] = it.Cellphone;
                newRows["实名认证"] = it.IsCertific.Trim().Equals("1") ? "是" : "否";
                newRows["性别"] = it.Sex.Equals("1") ? "男" : "女";
                newRows["所在省"] = it.Province.Trim();
                newRows["所在市"] = it.City.Trim();
                newRows["所在区"] = it.Country;
                newRows["公司地址"] = it.Address;
                newRows["营业执照照片"] = string.IsNullOrEmpty(it.BusLicenseImg) ? "无" : "有";
                newRows["身份证正面照片"] = string.IsNullOrEmpty(it.IDCardImg) ? "无" : "有";
                newRows["身份证反面照片"] = string.IsNullOrEmpty(it.IDCardBackImg) ? "无" : "有";
                newRows["注册时间"] = it.RegisterDate.ToString("yyyy-MM-dd") == "0001-01-01" || it.RegisterDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : it.RegisterDate.ToString("yyyy-MM-dd");
                newRows["绑定店代码"] = it.ClientNum;
                newRows["所属仓库"] = it.HouseName.Trim();
                newRows["绑定时间"] = it.BindDate.ToString("yyyy-MM-dd") == "0001-01-01" || it.BindDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : it.BindDate.ToString("yyyy-MM-dd");
                newRows["拒审原因"] = it.DenyReason;
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "微信用户绑定数据表");

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}