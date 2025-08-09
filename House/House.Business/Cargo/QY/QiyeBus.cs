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
    /// <summary>
    /// 微信企业号业务逻辑操作类
    /// </summary>
    public class QiyeBus
    {
        private QiyeManager man = new QiyeManager();
        /// <summary>
        /// 部门查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strCountWhere">查询总数的查询条件</param>
        /// <returns></returns>
        public Hashtable queryDepart(int pIndex, int pNum, QyDepartEntity entity)
        {
            return man.queryDepart(pIndex, pNum, entity);
        }
        /// <summary>
        /// 部门查询
        /// </summary>
        /// <returns></returns>
        public List<QyDepartEntity> queryDepart()
        {
            return man.queryDepart();
        }
        /// <summary>
        /// 根据父ID查询所有部门数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyDepartEntity> QueryDepartList(QyDepartEntity entity)
        {
            return man.QueryDepartList(entity);
        }
        /// <summary>
        /// 根据部门ID查询所有部门人员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyUserEntity> QueryDepartAllUserList(QyUserEntity entity)
        {
            return man.QueryDepartAllUserList(entity);
        }
        /// <summary>
        /// 查询多个部门的所有人员
        /// </summary>
        /// <param name="departStr"></param>
        /// <returns></returns>
        public List<QyUserEntity> QueryDepartAllUserList(string departStr)
        {
            return man.QueryDepartAllUserList(departStr);
        }
        /// <summary>
        /// 获取所有组织架构
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyDepartEntity> QueryDepartList()
        {
            return man.QueryDepartList();
        }
        /// <summary>
        /// 同步部门数据 
        /// </summary>
        /// <param name="entity"></param>
        public void SyncDepart(List<QyDepartEntity> entity)
        {
            man.SyncDepart(entity);
        }
        /// <summary>
        /// 删除部门数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelQYDepart(List<QyDepartEntity> entity, LogEntity log)
        {
            LogWrite<CargoHouseEntity> lw = new LogWrite<CargoHouseEntity>();
            try
            {
                man.DelQYDepart(entity);
                foreach (var it in entity)
                {
                    log.Memo = "删除部门：部门ID：" + it.Id.ToString() + "，部门名称：" + it.Name.Trim() + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    lw.WriteLog(log);
                }
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "删除部门，失败信息：" + ex.Message;
                lw.WriteLog(log);
            }
        }
        /// <summary>
        /// 保存部门数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddQYDepart(QyDepartEntity entity, LogEntity log)
        {
            LogWrite<QyDepartEntity> lw = new LogWrite<QyDepartEntity>();
            try
            {
                man.AddQYDepart(entity);
                log.Memo = "新增部门，部门ID" + entity.Id.ToString() + "，部门名称：" + entity.Name + "，上级部门ID：" + entity.Parentid.ToString();
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增部门，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateQYDepart(QyDepartEntity entity, LogEntity log)
        {
            LogWrite<QyDepartEntity> lw = new LogWrite<QyDepartEntity>();
            try
            {
                man.UpdateQYDepart(entity);
                log.Memo = "修改部门，部门ID" + entity.Id.ToString() + "，部门名称：" + entity.Name + "，上级部门ID：" + entity.Parentid.ToString();
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改部门，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 同步用户数据
        /// </summary>
        /// <param name="entity"></param>
        public void SyncUser(List<QyUserEntity> entity, int type)
        {
            man.SyncUser(entity, type);
        }
        /// <summary>
        /// 查询微信用户数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable queryUser(int pIndex, int pNum, QyUserEntity entity)
        {
            return man.queryUser(pIndex, pNum, entity);
        }
        /// <summary>
        /// 获取所有微信用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyUserEntity> QueryAllUserList()
        {
            return man.QueryAllUserList();
        }
        /// <summary>
        /// 删除微信用户数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelQYUser(List<QyUserEntity> entity, LogEntity log)
        {
            LogWrite<QyUserEntity> lw = new LogWrite<QyUserEntity>();
            try
            {
                man.DelQYUser(entity);
                foreach (var it in entity)
                {
                    log.Memo = "删除用户：UserID：" + it.UserID.ToString() + "，微信名：" + it.WxName.Trim() + "，OPENID：" + it.OpenID + "，手机号码 ：" + it.CellPhone + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    lw.WriteLog(log);
                }
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "删除用户，失败信息：" + ex.Message;
                lw.WriteLog(log);
            }
        }
        /// <summary>
        /// 保存微信用户数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddQYUser(QyUserEntity entity, LogEntity log)
        {
            LogWrite<QyUserEntity> lw = new LogWrite<QyUserEntity>();
            try
            {
                man.AddQYUser(entity);
                log.Memo = "新增微信企业用户，UserID" + entity.UserID.ToString() + "，微信名称：" + entity.WxName + "，OpenID：" + entity.OpenID + "，手机号码：" + entity.CellPhone;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增微信企业用户，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改微信用户
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateQYUser(QyUserEntity entity, LogEntity log)
        {
            LogWrite<QyUserEntity> lw = new LogWrite<QyUserEntity>();
            try
            {
                man.UpdateQYUser(entity);
                log.Memo = "修改微信企业用户，UserID" + entity.UserID.ToString() + "，微信名称：" + entity.WxName + "，OpenID：" + entity.OpenID + "，手机号码：" + entity.CellPhone;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改微信企业用户，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 根据UserID判断是否存在用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsExistQYUser(string userID)
        {
            return man.IsExistQYUser(userID);
        }
        /// <summary>
        /// 查询微信企业用户的信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyUserStatisEntity QueryWXQyUserStatis(QyUserStatisEntity entity)
        {
            return man.QueryWXQyUserStatis(entity);
        }
        /// <summary>
        /// 查询微信用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyUserEntity QueryUser(QyUserEntity entity)
        {
            return man.QueryUser(entity);
        }
        /// <summary>
        /// 同步标签列表数据
        /// </summary>
        /// <param name="entity"></param>
        public void SyncTag(List<QyTagEntity> entity)
        {
            man.SyncTag(entity);
        }
        /// <summary>
        /// 保存标签
        /// </summary>
        /// <param name="entity"></param>
        public void AddQYTag(QyTagEntity entity, LogEntity log)
        {
            LogWrite<QyTagEntity> lw = new LogWrite<QyTagEntity>();
            try
            {
                man.AddQYTag(entity);
                log.Memo = "新增标签，标签ID" + entity.Id.ToString() + "，标签名称：" + entity.Name;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增标签，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateQYTag(QyTagEntity entity, LogEntity log)
        {
            LogWrite<QyTagEntity> lw = new LogWrite<QyTagEntity>();
            try
            {
                man.UpdateQYTag(entity);
                log.Memo = "修改标签，标签ID" + entity.Id.ToString() + "，标签名称：" + entity.Name + "，标签类型：" + entity.TagType;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改标签，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 查询标签
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable queryTag(int pIndex, int pNum, QyTagEntity entity)
        {
            return man.queryTag(pIndex, pNum, entity);
        }
        /// <summary>
        /// 查询标签
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyTagEntity> QueryTagList(QyTagEntity entity)
        {
            return man.QueryTagList(entity);
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="entity"></param>
        public void DelQYTag(List<QyTagEntity> entity, LogEntity log)
        {
            LogWrite<QyTagEntity> lw = new LogWrite<QyTagEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.DelQYTag(entity);
                    foreach (var it in entity)
                    {
                        log.Memo = "删除标签：ID：" + it.Id.ToString() + "，标签名：" + it.Name.Trim() + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        lw.WriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "删除标签，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                }
            }
        }
        /// <summary>
        /// 查询未发货的订单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryUnSendOrderInfo(CargoOrderEntity entity)
        {
            return man.QueryUnSendOrderInfo(entity);
        }
        /// <summary>
        /// 查询配置推送数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyPushConfigEntity> QueryPushConfigList(QyPushConfigEntity entity)
        {
            return man.QueryPushConfigList(entity);
        }
        /// <summary>
        /// 查询所有企业用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyUserEntity> QueryUserList(QyUserEntity entity)
        {
            return man.QueryUserList(entity);
        }
        #region 企业号配置管理
        /// <summary>
        /// 查询企业号配置信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyConfigEntity QueryQyConfig(QyConfigEntity entity)
        {
            return man.QueryQyConfig(entity);
        }
        /// <summary>
        /// 查询企业号配置数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryQyConfig(int pIndex, int pNum, QyConfigEntity entity)
        {
            return man.QueryQyConfig(pIndex, pNum, entity);
        }
        /// <summary>
        /// 删除企业微信配置
        /// </summary>
        /// <param name="entity"></param>
        public void DelQYConfig(List<QyConfigEntity> entity, LogEntity log)
        {
            LogWrite<QyConfigEntity> lw = new LogWrite<QyConfigEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.DelQYConfig(entity);
                    log.Memo = "删除企业微信配置";
                    foreach (var it in entity)
                    {
                        log.Memo += "企业微信：" + it.QYKind + "，AgentID：" + it.AgentID + ",应用Secret：" + it.AppSecret;
                    }
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "删除企业微信配置失败，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 新增企业号配置数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddQYConfig(QyConfigEntity entity, LogEntity log)
        {
            LogWrite<QyConfigEntity> lw = new LogWrite<QyConfigEntity>();
            try
            {
                man.AddQYConfig(entity);
                log.Memo = "新增配置,消息类型" + entity.SendType + "，业务分类：" + entity.WorkClass + "，推送应用ID：" + entity.AgentID + "，推送应用Secret：" + entity.AppSecret;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增配置，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        #endregion
        #region 订单价格修改
        /// <summary>
        /// 保存商城 订单修改价格申请
        /// </summary>
        /// <param name="entity"></param>
        public long AddOrderUpdatePrice(QyOrderUpdatePriceEntity entity, LogEntity log)
        {
            LogWrite<QyOrderUpdatePriceEntity> lw = new LogWrite<QyOrderUpdatePriceEntity>();
            //使用事务
            long OID = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    OID = man.AddOrderUpdatePrice(entity);
                    log.Memo = "提交修改单价申请：订单号：" + entity.OrderNo + "，申请人：" + entity.ApplyID + "，申请人姓名：" + entity.ApplyName + "，申请原因：" + entity.Reason + "，订单类型：" + entity.OrderType + ",下一审批人：" + entity.CheckName;
                    foreach (var it in entity.UpdatePriceGoodsList)
                    {
                        log.Memo += "，订单ID：" + it.OrderID.ToString() + "，上架表ID：" + it.ShelvesID.ToString() + "，订单数量：" + it.OrderNum.ToString() + "，原价格：" + it.OrderPrice.ToString("F2") + "，修改后价格：" + it.ModifyPrice.ToString("F2");
                    }
                    lw.WriteLog(log);
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "提交修改单价申请，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                }
                scope.Complete();
            }
            return OID;
        }
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="entity"></param>
        public void CheckUpdatePrice(QyOrderUpdatePriceEntity entity, LogEntity log)
        {
            LogWrite<QyOrderUpdatePriceEntity> lw = new LogWrite<QyOrderUpdatePriceEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.CheckUpdatePrice(entity);
                    string appy = "通过";
                    if (entity.ApplyStatus.Equals("2")) { appy = "拒审"; }
                    else if (entity.ApplyStatus.Equals("1")) { appy = "通过"; }
                    else if (entity.ApplyStatus.Equals("3")) { appy = "结束"; }
                    log.Memo = "改价审批 订单号：" + entity.OrderNo + ",当前审批人：" + log.UserID + ",审批意见：" + entity.CheckResult + ",审批时间：" + entity.CheckTime.ToString("yyyy-MM-dd HH:mm:ss") + ",审批结果：" + appy;
                    lw.WriteLog(log);
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "修改单价审批，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                }
                scope.Complete();
            }
        }
        /// <summary>
        /// 查询修改订单申请数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyOrderUpdatePriceEntity QueryOrderUpdatePriceEntity(QyOrderUpdatePriceEntity entity)
        {
            return man.QueryOrderUpdatePriceEntity(entity);
        }
        /// <summary>
        /// 根据订单ID查询该订单的产品数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<QyOrderUpdateGoodsEntity> queryOrderUpdateGoodsByOID(long orderID)
        {
            return man.queryOrderUpdateGoodsByOID(orderID);
        }
        /// <summary>
        /// 根据订单ID查询该订单的产品数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<QyOrderUpdateGoodsEntity> queryOrderUpdateGoodsByOIDComputer(long orderID)
        {
            return man.queryOrderUpdateGoodsByOIDComputer(orderID);
        }
        /// <summary>
        /// 修改商城 订单价格
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxMallOrderPrice(QyOrderUpdatePriceEntity entity, LogEntity log)
        {
            CargoWeiXinManager wx = new CargoWeiXinManager();
            LogWrite<QyOrderUpdatePriceEntity> lw = new LogWrite<QyOrderUpdatePriceEntity>();
            List<QyOrderUpdateGoodsEntity> qyGoods = man.queryOrderUpdateGoodsByOID(entity.OID);
            WXOrderEntity wxorder = wx.QueryWeixinOrderByOrderNo(new WXOrderEntity { OrderNo = entity.OrderNo });
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                long OID = 0;
                decimal cp = 0.00M;
                foreach (var it in qyGoods)
                {
                    OID = it.OrderID;
                    wx.UpdateWxOrderGoodsPrice(new QyOrderUpdateGoodsEntity { OrderPrice = it.ModifyPrice, ShelvesID = it.ShelvesID, OrderID = it.OrderID });
                    cp += it.OrderNum * (it.OrderPrice - it.ModifyPrice);
                }
                wx.UpdateWxOrderTotalChargeByID(new WXOrderEntity { ID = OID, TotalCharge = cp });

                log.Memo = "修改商城订单价格：订单号：" + entity.OrderNo + ",修改前金额：" + wxorder.TotalCharge.ToString("F2") + ",修改金额：" + cp.ToString("F2");
                lw.WriteLog(log);
                scope.Complete();
            }
        }
        #endregion

        #region 群聊管理
        /// <summary>
        /// 新增群聊
        /// </summary>
        /// <param name="entity"></param>
        public void AddGroupChat(QyGroupChatEntity entity, LogEntity log)
        {
            LogWrite<QyGroupChatEntity> lw = new LogWrite<QyGroupChatEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.AddGroupChat(entity);

                    log.Memo = "新增群聊：ChatID：" + entity.ChatID.ToString() + ",群聊名：" + entity.ChatName.Trim() + ",所属仓库:" + entity.HouseID.ToString() + ",群聊类型:" + entity.ChatType + ",群主:" + entity.Owner;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "新增群聊，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                }
            }
        }
        /// <summary>
        /// 查询群聊数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyGroupChatEntity QueryGroupChatEntity(QyGroupChatEntity entity)
        {
            return man.QueryGroupChatEntity(entity);
        }
        #endregion

        #region 考勤管理
        /// <summary>
        /// 查询考勤数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryCheckinData(int pIndex, int pNum, QYCheckinDataEntity entity)
        {
            return man.QueryCheckinData(pIndex, pNum, entity);
        }
        /// <summary>
        /// 查询导出考勤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QYCheckinDataEntity> QueryCheckinList(QYCheckinDataEntity entity)
        {
            return man.QueryCheckinList(entity);
        }
        /// <summary>
        /// 同步考勤记录
        /// </summary>
        /// <param name="entity"></param>
        public void SyncCheckinData(List<QYCheckinDataEntity> entity)
        {
            man.SyncCheckinData(entity);
        }
        /// <summary>
        /// 同步考勤日度汇总记录
        /// </summary>
        /// <param name="entity"></param>
        public void SyncCheckinDayReport(List<QYCheckinDayReportEntity> entity)
        {
            man.SyncCheckinDayReport(entity);
        }
        /// <summary>
        /// 同步考勤月度汇总记录
        /// </summary>
        /// <param name="entity"></param>
        public void SyncCheckinReport(List<QYCheckinMonthlyReportEntity> entity)
        {
            man.SyncCheckinReport(entity);
        }
        /// <summary>
        /// 查询月度汇总数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QYCheckinMonthlyReportEntity> QueryCheckinReportData(QYCheckinMonthlyReportEntity entity)
        {
            return man.QueryCheckinReportData(entity);
        }
        #endregion
        #region 打卡规则
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryCheckinRule(int pIndex, int pNum, CheckinRuleEntity entity)
        {
            return man.QueryCheckinRule(pIndex, pNum, entity);
        }
        /// <summary>
        /// 新增考勤规则
        /// </summary>
        /// <param name="entity"></param>
        public void AddCheckinRule(CheckinRuleEntity entity, LogEntity log)
        {
            LogWrite<CheckinRuleEntity> lw = new LogWrite<CheckinRuleEntity>();
            try
            {
                entity.RuleID = Convert.ToInt32(man.AddCheckinRule(entity));
                man.AddCheckinRuleTime(entity);
                man.AddCheckinRulePersonnel(entity);
                log.Memo = "新增考勤规则";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增考勤规则，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改考勤规则
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCheckinRule(CheckinRuleEntity entity, LogEntity log)
        {
            LogWrite<CheckinRuleEntity> lw = new LogWrite<CheckinRuleEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateCheckinRule(entity);
                    man.UpdateCheckinRuleTime(entity);
                    man.UpdateCheckinRulePersonnel(entity);
                    log.Memo = "修改考勤规则";
                    lw.WriteLog(entity, log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "修改考勤规则，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }

        /// <summary>
        /// 删除考勤规则
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void DeleteCheckinRule(List<CheckinRuleEntity> entity, LogEntity log)
        {
            LogWrite<CheckinRuleEntity> lw = new LogWrite<CheckinRuleEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.DeleteCheckinRule(entity);
                    man.DeleteCheckinRuleTime(entity);
                    man.DeleteCheckinRulePersonnel(entity);
                    foreach (var it in entity)
                    {
                        log.Memo = "删除考勤规则数据：规则ID" + it.RuleID + "，规则名称：" + it.RuleName + "，打卡位置：" + it.CheckinLocation + "，打卡范围:" + it.CheckinScope;
                    }
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "删除考勤规则数据失败，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        #endregion
        #region  

        public CargoExpenseEntity QueryExpenseCheckEntity(CargoExpenseEntity entity)
        {
            return man.QueryExpenseCheckEntity(entity);
        }
        public void CheckExpense(CargoExpenseEntity entity, LogEntity log)
        {
            LogWrite<QyOrderUpdatePriceEntity> lw = new LogWrite<QyOrderUpdatePriceEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.CheckExpense(entity);
                    string appy = "通过";
                    if (entity.Status.Equals("2")) { appy = "拒审"; }
                    else if (entity.Status.Equals("1")) { appy = "通过"; }
                    else if (entity.Status.Equals("3")) { appy = "结束"; }
                    log.Memo = "报销审批 报销单号：" + entity.ExID + ",当前审批人：" + log.UserID + ",审批意见：" + entity.DenyReason + ",审批时间：" + entity.CheckTime.ToString("yyyy-MM-dd HH:mm:ss") + ",审批结果：" + appy;
                    lw.WriteLog(log);
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "报销审批，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                }
                scope.Complete();
            }
        }
        public string QueryWeixinOpenIDByUserID(string UserID)
        {
            return man.QueryWeixinOpenIDByUserID(UserID);
        }
        #endregion
    }
}
