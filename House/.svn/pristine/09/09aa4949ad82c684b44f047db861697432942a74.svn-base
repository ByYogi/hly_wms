using House.Entity;
using House.Entity.Cargo;
using House.Manager;
using House.Manager.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;

namespace House.Business.Cargo
{
    /// <summary>
    /// 仓库品牌加价模块业务逻辑类
    /// </summary>
    public class CargoHouseBrandPriceBus
    {
        private CargoHouseBrandPriceManeger man = new CargoHouseBrandPriceManeger();

        /// <summary>
        /// 仓库品牌加价列表查询
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryHouseBrandData(int pIndex, int pNum, CargoHouseBrandPriceEntity entity)
        {
            return man.QueryHouseBrandData(pIndex, pNum, entity);
        }

        /// <summary>
        /// 判断仓库品牌是否存在 true:表示存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true:表示存在</returns>
        public bool IsExistHouseBrandPrice(CargoHouseBrandPriceEntity entity)
        {
            return man.IsExistHouseBrandPrice(entity);
        }
        /// <summary>
        /// 判断仓库品牌加价是否可修改 true:表示存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true:表示存在</returns>
        public bool IsExistUpdateHouseBrandPrice(CargoHouseBrandPriceEntity entity)
        {
            return man.IsExistUpdateHouseBrandPrice(entity);
        }
        

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddHouseBrandPrice(CargoHouseBrandPriceEntity entity, LogEntity log)
        {
            LogWrite<CargoHouseBrandPriceEntity> lw = new LogWrite<CargoHouseBrandPriceEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.AddHouseBrandPrice(entity);
                    log.Memo = "新增仓库品牌加价";
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void UpdateHouseBrandPrice(CargoHouseBrandPriceEntity entity, LogEntity log)
        {
            LogWrite<CargoHouseBrandPriceEntity> lw = new LogWrite<CargoHouseBrandPriceEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateHouseBrandPrice(entity);
                    log.Memo = "修改仓库品牌加价";
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelHouseBrandPrice(List<CargoHouseBrandPriceEntity> entity, LogEntity log)
        {
            LogWrite<CargoHouseBrandPriceEntity> lw = new LogWrite<CargoHouseBrandPriceEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.DelHouseBrandPrice(entity);
                    foreach (var it in entity)
                    {
                        log.Memo = "删除仓库品牌加价";
                        lw.WriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "删除失败，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }

        public List<CargoBannerEntity> QueryHouseFile(CargoBannerEntity entity)
        {
            return man.QueryHouseFile(entity);
        }

    

        public void DelHousePic(List<CargoBannerEntity> entity,LogEntity log)
        {
            LogWrite<CargoHouseEntity> lw = new LogWrite<CargoHouseEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.DelHousePic(entity);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "作废图片，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                }
            }
        }

        public void AddCargoHouseBrandFile(List<CargoBannerEntity> entity, LogEntity log)
        {
            LogWrite<CargoFactoryFileEntity> lw = new LogWrite<CargoFactoryFileEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    foreach (var it in entity)
                    {
                        man.AddCargoHouseBrandFile(it);
                        log.Memo = "新增小程序轮播图，仓库id：" + it.HouseID + ",文件地址：" + it.PicName;
                        lw.WriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
    }
}
