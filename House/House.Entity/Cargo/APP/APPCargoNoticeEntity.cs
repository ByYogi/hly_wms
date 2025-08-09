using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 公告数据实体
    /// </summary>
    [Serializable]
    public class APPCargoNoticeEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string DelFlag { get; set; }
        public DateTime OPDATE { get; set; }
    }
}
