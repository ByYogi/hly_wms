using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.2.12.Tbl_Cargo_HouseBanner（慧采云仓广告Banner表）
    /// </summary>
    [Serializable]
    public class CargoBannerEntity
    {
        public int BID { get; set; }
        public int HouseID { get; set; }
        public string PicName { get; set; }
        public string Title { get; set; }
        public DateTime OPDATE { get; set; }
        public string DelFlag { get; set; }

    }
}
