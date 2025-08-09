using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    public class ProvincesEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProvincesInfo> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }
    public class ProvincesInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string firstLetter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jianpin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 北京市
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nameEn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pinyin { get; set; }
    }
}
