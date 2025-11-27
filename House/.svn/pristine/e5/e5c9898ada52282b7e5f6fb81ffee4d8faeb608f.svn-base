using House.Entity.Cargo;
using House.Entity.House;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo
{
    public partial class main : BasePage
    {
        // 常量定义
        private const string HUANG_SHU_CAI = "黄书才";
        private const string HUANG_SHU_CAI_PHONE = "13250520143";
        private const string DUTY_ROSTER_REDIS_KEY = "main-dutyRoster";
        
        public string Un = string.Empty;
        public string Ln = string.Empty;
        public string ArgoBarPrintName = string.Empty;
        public string CityCode = string.Empty;
        public string SystemName = string.Empty;
        public string res = string.Empty;
        public string dutyRosterJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Server.Transfer("/Default.aspx");
            }
            if (UserInfor == null)
            {
                Server.Transfer("/Default.aspx");
            }
            if (UserInfor.UserName == null || UserInfor.LoginName == null)
            {
                Server.Transfer("/Default.aspx");
            }
            if (!IsPostBack)
            {

                welcome.Text = "所属仓库：<a href='javascript:changeHouse();'>" + UserInfor.HouseName + "</a>"
                + "&nbsp;&nbsp;&nbsp;&nbsp;"
                // + 当前日期：" + Common.GetSystemDate()
                // + "    欢迎您："
                + UserInfor.UserName.Trim() + "，" + GetCurrentTime();
            }
            Un = UserInfor.UserName.Trim();
            Ln = UserInfor.LoginName.Trim();
            ArgoBarPrintName = "";
            QueryItemByLoginName();

            // 初始化值班排班表
            InitializeDutyRoster();
        }

        /// <summary>
        /// 初始化值班排班表
        /// </summary>
        private void InitializeDutyRoster()
        {
            int currentWeek = GetCurrentWeekNumber();
            dutyRosterJson = RedisHelper.GetString(DUTY_ROSTER_REDIS_KEY);
            
            if (string.IsNullOrEmpty(dutyRosterJson))
            {
                var initialDutyRoster = CreateInitialDutyRoster(currentWeek);
                dutyRosterJson = JsonConvert.SerializeObject(initialDutyRoster);
                RedisHelper.SetString(DUTY_ROSTER_REDIS_KEY, dutyRosterJson);
            }
            //else
            //{
            //    var root = JsonConvert.DeserializeObject<Root>(dutyRosterJson);
            //    if (root?.DutyRoster != null && root.DutyRoster.CurrWeeks != currentWeek)
            //    {
            //        AdjustDutyRosterForNewWeek(root.DutyRoster, currentWeek);
            //        dutyRosterJson = JsonConvert.SerializeObject(root);
            //        RedisHelper.SetString(DUTY_ROSTER_REDIS_KEY, dutyRosterJson);
            //    }
            //}
        }

        /// <summary>
        /// 获取当前周数
        /// </summary>
        /// <returns></returns>
        private int GetCurrentWeekNumber()
        {
            var today = DateTime.Today;
            var culture = CultureInfo.CurrentCulture;
            var calendar = culture.Calendar;
            return calendar.GetWeekOfYear(today, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// 创建初始值班排班表
        /// </summary>
        /// <param name="weekNumber">周数</param>
        /// <returns></returns>
        private Root CreateInitialDutyRoster(int weekNumber)
        {
            var defaultSaturdayPersons = new List<Person> 
            {
                new Person { Name = "周贝", Phone = "18122471591" },
                new Person { Name = "胡忠俊", Phone = "17369390670" },
                new Person { Name = HUANG_SHU_CAI, Phone = HUANG_SHU_CAI_PHONE }
            };
            
            var defaultSundayPersons = new List<Person> 
            {
                new Person { Name = "郑能峰", Phone = "15074594269" }
            };

            return new Root 
            { 
                DutyRoster = new DutyRoster 
                { 
                    Saturday = defaultSaturdayPersons, 
                    Sunday = defaultSundayPersons, 
                    CurrWeeks = weekNumber 
                } 
            };
        }

        /// <summary>
        /// 调整值班排班表到新的周
        /// </summary>
        /// <param name="duty">值班排班表</param>
        /// <param name="newWeek">新周数</param>
        private void AdjustDutyRosterForNewWeek(DutyRoster duty, int newWeek)
        {
            // 移除特定人员
            duty.Saturday = RemovePersonFromList(duty.Saturday, HUANG_SHU_CAI);

            // 执行轮换逻辑
            var firstSaturdayPerson = duty.Saturday?.FirstOrDefault();
            
            // 将Sunday人员加到Saturday末尾
            if (duty.Sunday?.Any() == true)
            {
                duty.Saturday.AddRange(duty.Sunday);
            }

            // 重新设置Sunday人员
            duty.Sunday = firstSaturdayPerson != null 
                ? new List<Person> { firstSaturdayPerson } 
                : new List<Person>();

            // 移除Saturday第一个人员（已移到Sunday）
            if (duty.Saturday.Any())
            {
                duty.Saturday.RemoveAt(0);
            }

            // 更新周数
            duty.CurrWeeks = newWeek;

            // 重新添加特定人员（黄书才）
            duty.Saturday.Add(new Person { Name = HUANG_SHU_CAI, Phone = HUANG_SHU_CAI_PHONE });
        }

        /// <summary>
        /// 从人员列表中移除指定姓名的人员
        /// </summary>
        /// <param name="personList">人员列表</param>
        /// <param name="nameToRemove">要移除的姓名</param>
        /// <returns></returns>
        private List<Person> RemovePersonFromList(List<Person> personList, string nameToRemove)
        {
            return personList?.Where(person => person.Name != nameToRemove).ToList() ?? new List<Person>();
        }

        /// <summary>
        /// 按登陆名查询所有导航链接并格式化
        /// </summary>
        public void QueryItemByLoginName()
        {
            List<SystemItemEntity> result = (List<SystemItemEntity>)Session["List"];
            res = "{basic:[";
            foreach (var it in result)
            {
                if (it.ParentID.Equals(-1))
                {
                    continue;
                }
                if (it.ParentID.Equals(0))
                {
                    res += "{'icon': '" + it.ItemIcon.Trim() + "', 'menuname': '" + it.CName.Trim() + "','menus':[ ";
                    List<SystemItemEntity> secList = result.FindAll(c => c.ParentID.Equals(it.ItemID));
                    foreach (var e in secList)
                    {
                        res += "{'url': '" + e.ItemSrc.ToString() + "', 'menuname': '" + e.CName.Trim() + "', 'icon': '" + e.ItemIcon.Trim() + "','menuid':'" + e.CName.Trim() + "'},";
                    }
                    res = res.Substring(0, res.Length - 1);
                    res += "]}, ";
                }
            }
            res = res.Substring(0, res.Length - 1);
            res += "]}";
        }
        /// <summary>
        /// 返回时间欢迎语
        /// </summary>
        /// <returns></returns>
        private string GetCurrentTime()
        {
            int currentHour = Convert.ToInt32(DateTime.Now.ToString("HH"));//取24小时制的当前小时数
            if (currentHour < 6 && currentHour >= 0)
            {
                return "早上好！";
            }
            if (currentHour < 11 && 6 <= currentHour)
            {
                return "上午好！";
            }
            if (11 <= currentHour && currentHour < 13)
            {
                return "中午好！";
            }
            if (13 <= currentHour && currentHour < 17)
            {
                return "下午好！";
            }
            if (17 <= currentHour && currentHour < 23)
            {
                return "晚上好！";
            }
            return "上午好！";
        }
        public List<CargoSafeStockEntity> CargoSafeStockData
        {
            get
            {
                if (Session["CargoSafeStockData"] == null)
                {
                    Session["CargoSafeStockData"] = new List<CargoSafeStockEntity>();
                }
                return (List<CargoSafeStockEntity>)(Session["CargoSafeStockData"]);
            }
            set
            {
                Session["CargoSafeStockData"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoSafeStockData.Count <= 0) { return; }
            string tname = string.Empty;
            DataTable table = new DataTable();

            if (!CargoSafeStockData.Any()) return;

            // 定义列名与类型

            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("区域大仓", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载重", typeof(string));
            table.Columns.Add("速级", typeof(string));
            table.Columns.Add("最后销售时间", typeof(string));
            table.Columns.Add("告警库存", typeof(int));
            table.Columns.Add("当前库存", typeof(int));
            table.Columns.Add("在途库存", typeof(int));
            table.Columns.Add("相差数量", typeof(int));
            int i = 0;
            foreach (var it in CargoSafeStockData)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["区域大仓"] = it.HouseName;
                newRows["所属仓库"] = it.AreaName;
                newRows["产品编码"] = it.ProductCode;
                newRows["品牌"] = it.TypeName;
                newRows["货品代码"] = it.GoodsCode;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载重"] = it.LoadIndex;
                newRows["速级"] = it.SpeedLevel;
                newRows["最后销售时间"] = it.LastSalDate;
                newRows["告警库存"] = it.StockNum;
                newRows["当前库存"] = it.CurNum;
                newRows["在途库存"] = it.MoveNum;
                newRows["相差数量"] = it.LessNum;
                table.Rows.Add(newRows);
            }
            var footRow = table.NewRow();
            footRow["区域大仓"] = "汇总：";
            footRow["告警库存"] = CargoSafeStockData.Sum(x => x.StockNum);
            footRow["当前库存"] = CargoSafeStockData.Sum(x => x.CurNum);
            footRow["在途库存"] = CargoSafeStockData.Sum(x => x.MoveNum.GetValueOrDefault());
            footRow["相差数量"] = CargoSafeStockData.Sum(x => x.LessNum);
            table.Rows.Add(footRow);

            string tName = CargoSafeStockData[0].AreaName + "库存告警缺货数据表";
            if (CargoSafeStockData[0].HouseName.Equals("广州仓库"))
            {
                tName = "广州仓库库存告警缺货数据表";
            }
            ToExcel.DataTableToExcel(table, "", tName);

        }

        //protected void btnSaveDutyRoster_Click(object sender, EventArgs e)
        //{

        //}
        [WebMethod]
        public static object SaveDutyRoster(string json)
        {
            var dutyRosterObjData = JSON.Encode(json);

            RedisHelper.SetString("main-dutyRoster", dutyRosterObjData);
            var savedData = RedisHelper.GetString(("main-dutyRoster"));
            return new
            {
                success = true,
                data = dutyRosterObjData
            };
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class DutyRoster
    {
        public List<Person> Saturday { get; set; }
        public List<Person> Sunday { get; set; }
        public int CurrWeeks { get; set; }
    }
    public class Root
    {
        public DutyRoster DutyRoster { get; set; }
    }
}