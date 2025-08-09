using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    public class BannerListEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BannerInfo> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }
    public class BannerInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 首页轮播图
        /// </summary>
        public string title { get; set; }
    }
}
