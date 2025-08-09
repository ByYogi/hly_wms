using House.Entity.Cargo;
using House.Manager.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Business.Cargo
{
    /// <summary>
    /// 数据可视化大数据 分析 系统业务逻辑类
    /// </summary>
    public class CargoBigDataBus
    {
        private CargoBigDataViewManager man = new CargoBigDataViewManager();
        /// <summary>
        /// 仓库总的实时统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoBigDataViewEntity> QueryALLBigDataStatis(CargoBigDataViewEntity entity)
        {
            return man.QueryALLBigDataStatis(entity);
        }
        /// <summary>
        /// 仓库总的实时统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoBigDataViewEntity> QueryALLBigOrderSum(CargoBigDataViewEntity entity)
        {
            return man.QueryALLBigOrderSum(entity);
        }
        /// <summary>
        /// 按品牌查询
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoBigDataViewEntity> QueryDataByTypeName(CargoBigDataViewEntity entity)
        {
            return man.QueryDataByTypeName(entity);
        }
        /// <summary>
        /// 实时统计查询
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoBigDataViewEntity> QueryCurBigDataStatis(CargoBigDataViewEntity entity)
        {
            return man.QueryCurBigDataStatis(entity);
        }
        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoBigDataViewEntity> QueryBigDataStatisList(CargoBigDataViewEntity entity)
        {
            return man.QueryBigDataStatisList(entity);
        }
        /// <summary>
        /// 按业务员销量排名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoBigDataViewEntity> QueryDataBySaleManRank(CargoBigDataViewEntity entity)
        {
            return man.QueryDataBySaleManRank(entity);
        }
    }
}
