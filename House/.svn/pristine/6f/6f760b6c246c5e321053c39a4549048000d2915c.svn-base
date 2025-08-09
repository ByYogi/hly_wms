using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Report
{
    public partial class BigDataApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string methodName = string.Empty;
            try
            {
                methodName = Request["method"];
                if (String.IsNullOrEmpty(methodName)) return;
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                method.Invoke(this, null);
            }
            catch (Exception ex)
            {
            }

        }
        #region 大数据分析统计
        /// <summary>
        /// 所有仓库所有时间的数据统计
        /// </summary>
        public void QueryALLBigDataStatis()
        {
            CargoBigDataViewEntity entity = new CargoBigDataViewEntity();
            entity.BelongHouse = "0";//查询迪乐泰的订单
            entity.OrderModel = "0";//查询订单，不查退货单 
            entity.StartDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            //entity.StartDate = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 1);
            CargoBigDataBus bus = new CargoBigDataBus();
            List<CargoBigDataViewEntity> list = bus.QueryALLBigDataStatis(entity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 仓库总的实时统计
        /// </summary>
        public void QueryALLBigOrderSum()
        {
            CargoBigDataViewEntity entity = new CargoBigDataViewEntity();
            entity.BelongHouse = "0";//查询迪乐泰的订单
            entity.OrderModel = "0";//查询订单，不查退货单 
            CargoBigDataBus bus = new CargoBigDataBus();
            List<CargoBigDataViewEntity> list = bus.QueryALLBigOrderSum(entity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 按品牌查询
        /// </summary>
        public void QueryDataByTypeName()
        {
            CargoBigDataViewEntity entity = new CargoBigDataViewEntity();
            entity.BelongHouse = "0";//查询迪乐泰的订单
            entity.OrderModel = "0";//查询订单，不查退货单 
            entity.TypeParentName = "轮胎";
            entity.StartDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            //entity.StartDate = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 1);
            CargoBigDataBus bus = new CargoBigDataBus();
            List<CargoBigDataViewEntity> list = bus.QueryDataByTypeName(entity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 按业务员销量排名查询
        /// </summary>
        public void QueryDataBySaleManRank()
        {
            CargoBigDataViewEntity entity = new CargoBigDataViewEntity();
            entity.BelongHouse = "0";//查询迪乐泰的订单
            entity.OrderModel = "0";//查询订单，不查退货单 
            entity.TypeParentName = "轮胎";
            entity.ThrowGood = Convert.ToString(Request["ThrowGood"]);//OE 和RE
            entity.StartDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            //entity.StartDate = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 1);
            CargoBigDataBus bus = new CargoBigDataBus();
            List<CargoBigDataViewEntity> list = bus.QueryDataBySaleManRank(entity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 实时数据查询
        /// </summary>
        public void QueryCurBigDataStatis()
        {
            CargoBigDataViewEntity entity = new CargoBigDataViewEntity();
            entity.BelongHouse = "0";//查询迪乐泰的订单
            entity.OrderModel = "0";//查询订单，不查退货单 
            entity.StartDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            CargoBigDataBus bus = new CargoBigDataBus();
            List<CargoBigDataViewEntity> list = bus.QueryCurBigDataStatis(entity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询订单列表
        /// </summary>
        public void QueryBigDataStatisList()
        {
            CargoBigDataViewEntity entity = new CargoBigDataViewEntity();
            entity.BelongHouse = "0";//查询迪乐泰的订单
            entity.OrderModel = "0";//查询订单，不查退货单 
            entity.StartDate = DateTime.Now;
            entity.OrderNo = Request["OrderNo"];
            entity.CreateDateTime = Convert.ToDateTime(Request["CreateDateTime"]);
            CargoBigDataBus bus = new CargoBigDataBus();
            List<CargoBigDataViewEntity> list = bus.QueryBigDataStatisList(entity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 大数据可视化平台-所有云仓信息
        /// </summary>
        public void BigDataQueryCargoHouse()
        {
            CargoHouseBus bus = new CargoHouseBus();
            List<CargoHouseEntity> list = bus.BigDataQueryCargoHouse();

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        #endregion
    }
}