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

namespace House.Business.Cargo
{
    public class CargoSecKillBus
    {
        private CargoSecKillManager man = new CargoSecKillManager();
        /// <summary>
        /// 查询淘宝订单数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryCargoHouse(int pIndex, int pNum, WXTaobaoEntity entity)
        {
            return man.QueryCargoHouse(pIndex, pNum, entity);
        }
        /// <summary>
        /// 保存淘宝订单
        /// </summary>
        /// <param name="entity"></param>
        public void SaveTaobaoOrderData(List<WXTaobaoEntity> entity, LogEntity log)
        {
            LogWrite<WXTaobaoEntity> lw = new LogWrite<WXTaobaoEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.SaveTaobaoOrderData(entity);
                    log.Memo = "";
                    foreach (var it in entity)
                    {
                        lw.WriteLog(it, log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "新增淘宝订单失败，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 删除 淘宝订单
        /// </summary>
        public void DelTaobaoOrder(List<WXTaobaoEntity> entity, LogEntity log)
        {
            LogWrite<WXTaobaoEntity> lw = new LogWrite<WXTaobaoEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.DelTaobaoOrder(entity);
                    log.Memo = "删除淘宝订单号：";
                    foreach (var it in entity)
                    {
                        log.Memo += it.TaobaoID + "，用户昵称：" + it.buyer_nick + "，用户支付宝账号:" + it.buyer_alipay;
                    }
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "删除淘宝订单号失败，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 查询关注汽修推广数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryAutoCarSpread(int pIndex, int pNum, WXSecKillEntity entity)
        {
            return man.QueryAutoCarSpread(pIndex, pNum, entity);
        }
        /// <summary>
        /// 设置使用状态
        /// </summary>
        /// <param name="entity"></param>
        public void setUseStatus(List<WXSecKillEntity> entity, LogEntity log)
        {
            LogWrite<WXSecKillEntity> lw = new LogWrite<WXSecKillEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.setUseStatus(entity);
                    log.Memo = "设置使用状态：";
                    foreach (var it in entity)
                    {
                        log.Memo += it.ID.ToString() + "，微信名：" + it.wxName + "，车牌号码:" + it.CarNum + "，手机号码：" + it.Cellphone + "，使用状态：" + it.UseStatus;
                    }
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "设置使用状态失败，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
    }
}
