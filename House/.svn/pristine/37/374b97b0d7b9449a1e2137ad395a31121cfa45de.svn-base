using House.Entity.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity
{
    [Serializable]
    public class userAPIMessage
    {
        /// <summary>
        /// True：返回成功，False：返回失败
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 成功或失败的信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回权限列表
        /// </summary>
        public List<SystemItemEntity> itemList { get; set; }
        public SystemUserEntity userEnt { get; set; }
        public List<SystemUserEntity> userList { get; set; } 
    }
}
