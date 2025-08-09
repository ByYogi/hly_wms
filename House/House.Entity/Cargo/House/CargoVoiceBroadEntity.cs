using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 仓库订单语音播报数据实体1.2.13.Tbl_Cargo_VoiceBroad（仓库订单语音播报表）
    /// </summary>
    [Serializable]
    public class CargoVoiceBroadEntity
    {
        public int VoiceID { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public int HouseID { get; set; }
        public DateTime OP_DATE { get; set; }
    }
}
