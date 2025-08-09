using House.Entity;
using House.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Business
{
    public class LogBus
    {
        private LogWrite<LogEntity> lw = new LogWrite<LogEntity>();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="entity"></param>
        public void InsertLog(LogEntity entity)
        {
            lw.WriteLog(entity);
        }
    }
}
