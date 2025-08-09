using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo
{
    public partial class NoticeShow : System.Web.UI.Page
    {
        public CargoNoticeEntity ent = new CargoNoticeEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = Convert.ToInt32(Request["ID"]);
                CargoStaticBus bus = new CargoStaticBus();
                ent = bus.QueryNoticeByID(new CargoNoticeEntity { ID = ID });
            }
        }
    }
}