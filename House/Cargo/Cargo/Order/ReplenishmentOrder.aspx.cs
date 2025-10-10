using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Order
{
    public partial class ReplenishmentOrder : System.Web.UI.Page
    {
        public List<CargoStockTakeTagEntity> CargoStockTakeList
        {
            get
            {
                if (Session["CargoStockTakeList"] == null)
                {
                    Session["CargoStockTakeList"] = new List<CargoStockTakeTagEntity>();
                }
                return (List<CargoStockTakeTagEntity>)(Session["CargoStockTakeList"]);
            }
            set
            {
                Session["CargoStockTakeList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {


        }
    }
}