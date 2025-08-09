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
    public class CargoWeiXinBus
    {
        private CargoWeiXinManager man = new CargoWeiXinManager();
        /// <summary>
        /// 根据OpenID判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistWeixinUser(WXUserEntity entity)
        {
            return man.IsExistWeixinUser(entity);
        }
        public void AddWeixinUser(WXUserEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            try
            {
                man.AddWeixinUser(entity);
                log.Memo = "新增微信用户";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增微信用户，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 查询微信用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXUserEntity> QueryWeixinUserInfo(WXUserEntity entity)
        {
            return man.QueryWeixinUserInfo(entity);
        }
        /// <summary>
        /// 修改微信用户信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWeixinUser(List<WXUserEntity> entity)
        {
            man.UpdateWeixinUser(entity);
        }
        /// <summary>
        /// 修改用户姓名和手机号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserNameCellphone(WXUserEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            try
            {
                man.UpdateWxUserNameCellphone(entity);
                log.Memo = "修改用户信息，姓名：" + entity.Name + "，手机号码：" + entity.Cellphone + "，ID：" + entity.ID.ToString();
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }

        /// <summary>
        /// 根据OPENID查询微信用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXUserEntity QueryWeixinUserByOpendID(WXUserEntity entity)
        {
            return man.QueryWeixinUserByOpendID(entity);
        }
        /// <summary>
        /// 根据ID查询微信 用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXUserEntity QueryWeixinUserByID(WXUserEntity entity)
        {
            return man.QueryWeixinUserByID(entity);
        }
        /// <summary>
        /// 微信用户地址管理
        /// </summary>
        /// <param name="entity"></param>
        public void AddAddress(WXUserAddressEntity entity, LogEntity log)
        {
            LogWrite<WXUserAddressEntity> lw = new LogWrite<WXUserAddressEntity>();
            try
            {
                man.AddAddress(entity);
                log.Memo = "新增微信地址";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增微信地址，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAddress(WXUserAddressEntity entity, LogEntity log)
        {
            LogWrite<WXUserAddressEntity> lw = new LogWrite<WXUserAddressEntity>();
            try
            {
                man.UpdateAddress(entity);
                log.Memo = "修改收货地址，收货人姓名：" + entity.Name + "，地址：" + entity.Address + "，省：" + entity.Province + " " + entity.City + " " + entity.Country + "，电话：" + entity.Cellphone;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改收货地址，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 查询用户地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXUserAddressEntity> QueryWxAddressByWXID(WXUserAddressEntity entity)
        {
            return man.QueryWxAddressByWXID(entity);
        }
        /// <summary>
        /// 更新用户的物流信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserLogicName(WXUserEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            try
            {
                man.UpdateWxUserLogicName(entity);
                log.Memo = "修改配送物流，用户ID：" + entity.ID + "，微信名：" + entity.wxName + "，OPENID：" + entity.wxOpenID + "，物流名称：" + entity.LogicName;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改配送物流，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteAddress(WXUserAddressEntity entity, LogEntity log)
        {
            LogWrite<WXUserAddressEntity> lw = new LogWrite<WXUserAddressEntity>();
            try
            {
                man.DeleteAddress(entity);
                log.Memo = "删除微信地址：ID" + entity.ID.ToString();
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "删除微信地址，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 设为默认地址
        /// </summary>
        /// <param name="addressid"></param>
        public void SetAddressDefault(WXUserAddressEntity entity, LogEntity log)
        {
            LogWrite<WXUserAddressEntity> lw = new LogWrite<WXUserAddressEntity>();
            try
            {
                man.SetAddressNotDefault(entity.WXID);
                man.SetAddressDefault(entity.ID);
                log.Memo = "设置默认地址：ID" + entity.ID.ToString() + "，微信ID：" + entity.WXID.ToString();
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "设置默认地址，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 查询用户的积分记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXUserPointEntity> QueryWXUserPoint(WXUserPointEntity entity)
        {
            return man.QueryWXUserPoint(entity);
        }
        /// <summary>
        /// 添加微信用户积分记录
        /// </summary>
        /// <param name="entity"></param>
        public void AddWeixinPoint(WXUserPointEntity entity, LogEntity log)
        {
            LogWrite<WXUserPointEntity> lw = new LogWrite<WXUserPointEntity>();
            try
            {
                man.AddWeixinPoint(entity);
                man.UpdateWxUserConsume(new WXUserEntity { ID = entity.WXID, ConsumerPoint = entity.Point });
                log.Memo = "今日签到：WXID" + entity.WXID.ToString() + "，积分：" + entity.Point.ToString() + "，积分类型：" + entity.PointType.ToString();
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "今日签到，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 根据ID查询上架的商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CargoProductEntity QueryOnShelvesByID(Int64 id)
        {
            return man.QueryOnShelvesByID(id);
        }
        /// <summary>
        /// 保存微信订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void SaveWeixinOrder(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.SaveWeixinOrder(entity);
                    log.Memo = "微信下单：订单号：" + entity.OrderNo + "，客户ID：" + entity.WXID.ToString() + "，下单数量：" + entity.Piece.ToString() + "，下单金额：" + entity.TotalCharge.ToString();
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "微信下单，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 修改微信订单支付状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWeixinOrderPayStatus(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateWeixinOrderPayStatus(entity);
                    log.Memo = "微信支付回调状态：订单号：" + entity.OrderNo + "，付款状态：" + entity.PayStatus.ToString() + "，微信系统订单号：" + entity.WXPayOrderNo + "，仓库订单号：" + entity.CargoOrderNo;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "微信下单，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 判断微信订单是否支付成功
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistWeixinOrderPay(WXOrderEntity entity)
        {
            return man.IsExistWeixinOrderPay(entity);
        }
        /// <summary>
        /// 根据用户ID查询该用户的订单状态
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public WXOrderEntity QueryWeixinUserOrderInfo(Int64 userid)
        {
            return man.QueryWeixinUserOrderInfo(userid);
        }
        public List<WXOrderEntity> QueryWeixinOrderInfo(int pIndex, int pNum, WXOrderEntity entity)
        {
            return man.QueryWeixinOrderInfo(pIndex, pNum, entity);
        }
        /// <summary>
        /// 确认收货修改状态
        /// </summary>
        /// <param name="entity"></param>
        public void setWeixinOrderOk(WXOrderEntity entity, LogEntity log)
        {
            CargoOrderManager order = new CargoOrderManager();
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    CargoOrderEntity ent = new CargoOrderEntity();
                    if (man.IsExistWeixinOrderPay(new WXOrderEntity { OrderNo = entity.OrderNo }))
                    {
                        man.setWeixinOrderOk(entity);
                        ent = order.QueryCargoInfoByWxOrderNo(new CargoOrderEntity { WXOrderNo = entity.OrderNo });
                    }
                    else
                    {
                        ent = order.QueryOrderInfo(new CargoOrderEntity { OrderNo = entity.OrderNo });
                    }
                    order.SaveOrderStatus(new CargoOrderStatusEntity
                    {
                        OrderID = ent.OrderID,
                        OrderNo = ent.OrderNo,
                        OrderStatus = "5",
                        Signer = ent.AcceptPeople,
                        DetailInfo = "微信确认收货签收",
                        SignTime = DateTime.Now,
                        OP_ID = log.UserID,
                        OP_Name = log.UserID,
                        OP_DATE = DateTime.Now
                    });
                    log.Memo = "确认收货：微信订单号：" + entity.OrderNo + ",系统订单号：" + ent.OrderNo;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "确认收货，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }

        /// <summary>
        /// 订单评价
        /// </summary>
        /// <param name="entity"></param>
        public void setOrderEvaluate(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.setOrderEvaluate(entity);
                    log.Memo = "订单评价：微信订单号：" + entity.OrderNo + ",商品评价：" + entity.GoodEvaluate + ",物流评价:" + entity.LogisEvaluate + ",评价内容：" + entity.EvaluateMemo;
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

        public void setWeixinOrderStatus(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.setWeixinOrderOk(entity);
                    log.Memo = "修改微信订单状态：订单号：" + entity.OrderNo + "，订单状态：" + entity.OrderStatus;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "修改微信订单状态，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        public void DeleteWeixinOrder(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    List<CargoProductShelvesEntity> proList = man.QueryWeixinOrderProductInfo(entity.ID);
                    man.DeleteWeixinOrder(entity);
                    foreach (var it in proList)
                    {
                        man.UpdateOnShelvesNumAdd(it);
                    }
                    if (entity.PayWay.Equals("1"))
                    {
                        //额度付款，则把客户的额度加回去
                        WXUserEntity wxUser = man.QueryWeixinUserByID(new WXUserEntity { ID = entity.WXID });
                        if (!wxUser.ClientNum.Equals(0))
                        {
                            man.UpdateClientLimitMoney(entity.TotalCharge, wxUser.ClientNum, "0");
                        }
                        //2.删除积分
                        man.AddWeixinPoint(new WXUserPointEntity
                        {
                            WXID = entity.WXID,
                            Point = Convert.ToInt32(Math.Round(entity.TotalCharge)),
                            PointType = "5",
                            CutPoint = "1",
                            WXOrderNo = entity.OrderNo
                        });
                        //3.减去客户积分
                        man.SubtractWxUserConsume(new WXUserEntity { ConsumerPoint = Convert.ToInt32(Math.Round(entity.TotalCharge)), ID = entity.WXID });
                    }
                    else if (entity.PayWay.Equals("2"))
                    {
                        //proList[0].ID  上架表主键ID
                        CargoProductShelvesEntity shelves = man.QueryProductShelvesByID(proList[0].ID);
                        //积分兑换
                        //积分再加回来
                        man.AddWeixinPoint(new WXUserPointEntity
                        {
                            WXID = entity.WXID,
                            Point = shelves.Consume,
                            PointType = "7",
                            CutPoint = "0",
                            WXOrderNo = entity.OrderNo
                        });
                        //3.加回客户积分
                        man.UpdateWxUserConsume(new WXUserEntity { ConsumerPoint = shelves.Consume, ID = entity.WXID });
                    }
                    //修改上架表数量 和 库存表数量
                    log.Memo = "删除订单：订单号：" + entity.ID.ToString() + "，订单号：" + entity.OrderNo;
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
        /// 积分兑换
        /// </summary>
        /// <param name="entity"></param>
        public void ConsumeConvert(WXOrderEntity entity)
        {
            //1.增加一条积分兑换记录
            man.AddWeixinPoint(new WXUserPointEntity
            {
                WXID = entity.WXID,
                Point = Convert.ToInt32(Math.Round(entity.TotalCharge)),
                PointType = "6",
                CutPoint = "1",
                WXOrderNo = entity.OrderNo
            });
            //2.减去客户积分
            man.SubtractWxUserConsume(new WXUserEntity { ConsumerPoint = Convert.ToInt32(Math.Round(entity.TotalCharge)), ID = entity.WXID });
        }
        /// <summary>
        /// 根据用户OpendID判断该用户是否绑定客户编码True：已经绑定，False：未绑定
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsBindingClientNum(WXUserEntity entity)
        {
            return man.IsBindingClientNum(entity);
        }
        /// <summary>
        /// 绑定客户店代码
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateClientForBindingClientNum(WXUserEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateClientForBindingClientNum(entity);
                    log.Memo = "客户绑定：店代码：" + entity.ClientNum.ToString() + "，手机号：" + entity.Cellphone + "，所在地区：" + entity.Province + " " + entity.City + " " + entity.Country;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "客户绑定，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        /// <summary>
        /// 修改微信 用户的上级人
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserParentID(WXUserEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateWxUserParentID(entity);
                    log.Memo = "绑定上级：上级ID：" + entity.ParentID.ToString() + "，用户ID：" + entity.ID.ToString() + "，OpenID：" + entity.wxOpenID;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "绑定上级，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        /// <summary>
        /// 保存用户注册
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxUserRegeist(WXUserEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateWxUserRegeist(entity);
                    log.Memo = "用户注册：公司名称：" + entity.CompanyName.Trim() + "，联系人：" + entity.Name.Trim() + "，手机号：" + entity.Cellphone + "，所在地区：" + entity.Province + " " + entity.City + " " + entity.Country + "，公司地址：" + entity.Address + "，营业执照照片：" + entity.BusLicenseImg + "，身份证照片：" + entity.IDCardImg;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "客户绑定，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        public bool IsMaxBindingNum(WXUserEntity entity)
        {
            return man.IsMaxBindingNum(entity);
        }
        /// <summary>
        /// 判断手机号码是否已经绑定 True:绑定过了False:未绑定
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsCellphoneBind(WXUserEntity entity)
        {
            return man.IsCellphoneBind(entity);
        }
        /// <summary>
        /// 通过微信订单号查询订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXOrderEntity QueryWeixinOrderByOrderNo(WXOrderEntity entity)
        {
            return man.QueryWeixinOrderByOrderNo(entity);
        }
        /// <summary>
        /// 修改客户的信用额度并修改微信商城订单的支付状态 
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateClientLimitAndWxOrderPayStatus(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    //1.修改微信商城订单的状态 
                    man.UpdateClientLimitAndWxOrderPayStatus(entity);
                    if (!man.IsSign(new WXUserPointEntity { WXID = entity.WXID, PointType = "3", WXOrderNo = entity.OrderNo }))
                    {
                        //2.增加客户的积分记录
                        man.AddWeixinPoint(new WXUserPointEntity
                        {
                            WXID = entity.WXID,
                            Point = Convert.ToInt32(Math.Round(entity.TotalCharge)),
                            PointType = "3",
                            WXOrderNo = entity.OrderNo
                        });
                        //3.增加客户的积分统计
                        man.UpdateWxUserConsume(new WXUserEntity { ID = entity.WXID, ConsumerPoint = Convert.ToInt32(Math.Round(entity.TotalCharge)) });
                    }
                    log.Memo = "信用额度支付：客户编码：" + entity.ClientNum.ToString() + "，支付金额：" + entity.TotalCharge.ToString("F2") + "，订单号：" + entity.OrderNo + "，微信ID：" + entity.WXID.ToString() + "，付款状态：" + entity.PayStatus + "，所属仓库：" + entity.HouseID.ToString() + "，地址：" + entity.Province + " " + entity.City + " " + entity.Country + " " + entity.Address;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "信用额度支付，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        /// <summary>
        /// 微信支付成功增加积分
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void UpdateWxClientPoint(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (!man.IsSign(new WXUserPointEntity { WXID = entity.WXID, PointType = "3", WXOrderNo = entity.OrderNo }))
                {
                    //2.增加客户的积分记录
                    man.AddWeixinPoint(new WXUserPointEntity
                    {
                        WXID = entity.WXID,
                        Point = Convert.ToInt32(Math.Round(entity.TotalCharge)),
                        PointType = "3",
                        WXOrderNo = entity.OrderNo
                    });
                    //3.增加客户的积分统计
                    man.UpdateWxUserConsume(new WXUserEntity { ID = entity.WXID, ConsumerPoint = Convert.ToInt32(Math.Round(entity.TotalCharge)) });
                    log.Memo = "微信支付成功增加积分：客户编码：" + entity.ClientNum.ToString() + "，增加积分：" + Convert.ToInt32(Math.Round(entity.TotalCharge)).ToString() + "，订单号：" + entity.OrderNo + "，微信ID：" + entity.WXID.ToString();
                    lw.WriteLog(log);
                    scope.Complete();
                }
            }
        }
        /// <summary>
        /// 修改商城订单总金额
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderTotalCharge(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateWxOrderTotalCharge(entity);
                    log.Memo = "修改订单总金额：订单号：" + entity.OrderNo + "，修改后金额：" + entity.TotalCharge.ToString("F2");
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "修改订单总金额，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        #region 优惠活动领取
        /// <summary>
        /// 新增领取记录
        /// </summary>
        /// <param name="entity"></param>
        public void AddSecKill(WXSecKillEntity entity, LogEntity log)
        {
            LogWrite<WXSecKillEntity> lw = new LogWrite<WXSecKillEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.AddSecKill(entity);
                    log.Memo = "领取记录：车牌号：" + entity.CarNum + "，手机号码：" + entity.Cellphone + "，微信用户ID：" + entity.WXID.ToString() + "，微信OPENID：" + entity.wxOpenID + "，微信名：" + entity.wxName;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "修改订单总金额，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        /// <summary>
        /// 车牌号是否已经领取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistReceive(WXSecKillEntity entity)
        {
            return man.IsExistReceive(entity);
        }
        /// <summary>
        /// 查询优惠领取情况 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXSecKillEntity> QuerySecKillData(WXSecKillEntity entity)
        {
            return man.QuerySecKillData(entity);
        }
        /// <summary>
        /// 新增统计数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void AddSecStatisData(WXSecStatisEntity entity)
        {
            man.AddSecStatisData(entity);
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXSecStatisEntity QuerySecStatisEntity(WXSecStatisEntity entity)
        {
            return man.QuerySecStatisEntity(entity);
        }
        #endregion
        /// <summary>
        /// 查询淘宝订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXTaobaoEntity> QueryTaobaoOrderInfo(WXTaobaoEntity entity)
        {
            return man.QueryTaobaoOrderInfo(entity);
        }
        /// <summary>
        /// 查询今日统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXTaobaoEntity QueryTodayTaobaoStatis(WXTaobaoEntity entity)
        {
            return man.QueryTodayTaobaoStatis(entity);
        }
        /// <summary>
        /// 保存淘宝订单
        /// </summary>
        /// <param name="entity"></param>
        public void SaveTaobaoOrder(WXTaobaoEntity entity, LogEntity log)
        {
            LogWrite<WXTaobaoEntity> lw = new LogWrite<WXTaobaoEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    CargoSecKillManager sec = new CargoSecKillManager();
                    if (sec.IsExistTaobaoOrder(new WXTaobaoEntity { TaobaoID = entity.TaobaoID }))
                    {
                        //修改
                        man.UpdateTaobaoOrderWxID(entity);
                        //3.修改上级和上上级人返利和提成金额
                        WXTaobaoEntity userLevel = sec.QueryWxUserLevel(new WXTaobaoEntity { WXID = entity.WXID });
                        //4.先删除返利表的返利数据 
                        sec.DeleteTaobaoCashBack(entity.TaobaoID);
                        WXTaobaoEntity tb = sec.QueryTaobaoOrderByID(entity.TaobaoID);
                        //5.再新增进来返利和提成数据
                        if (!userLevel.WXID.Equals(0))
                        {
                            sec.AddTaobaoCashBack(new WXTaobaoCashBackEntity
                            {
                                WXID = userLevel.WXID,
                                TaobaoID = entity.TaobaoID,
                                payment = tb.payment,
                                CashType = "0",
                                Cash = tb.num * 4
                            });
                        }
                        if (!userLevel.OneWxID.Equals(0))
                        {
                            sec.AddTaobaoCashBack(new WXTaobaoCashBackEntity
                            {
                                WXID = userLevel.OneWxID,
                                TaobaoID = entity.TaobaoID,
                                payment = tb.payment,
                                CashType = "1",
                                Cash = tb.num * 10
                            });
                        }
                        if (!userLevel.SecendWxID.Equals(0))
                        {
                            sec.AddTaobaoCashBack(new WXTaobaoCashBackEntity
                            {
                                WXID = userLevel.SecendWxID,
                                TaobaoID = entity.TaobaoID,
                                payment = tb.payment,
                                CashType = "1",
                                Cash = tb.num * 6
                            });
                        }
                    }
                    else
                    {
                        man.SaveTaobaoOrder(entity);
                    }
                    log.Memo = "保存淘宝订单号：订单号：" + entity.TaobaoID + "，微信用户ID：" + entity.WXID.ToString() + "，微信OPENID：" + log.UserID;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "保存淘宝订单号，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        /// <summary>
        /// 查询我的上级信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXUserEntity QueryMyParentEntity(WXUserEntity entity)
        {
            return man.QueryMyParentEntity(entity);
        }
        /// <summary>
        /// 判断是否存在淘宝订单号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistTaobaoOrder(WXTaobaoEntity entity)
        {
            return man.IsExistTaobaoOrder(entity);
        }
        /// <summary>
        /// 查询某个用户的所有微信商城订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXOrderManagerEntity> QueryWxOrderInfo(WXOrderManagerEntity entity)
        {
            return man.QueryWxOrderInfo(entity);
        }
        /// <summary>
        /// 修改绑定客户的微信商城订单的账单号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderAccountByID(List<WXOrderEntity> entity, LogEntity log)
        {
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateWxOrderAccountByID(entity);
                    foreach (var it in entity)
                    {
                        log.Memo += "批量支付账单号：订单ID：" + it.ID.ToString() + "，账单号：" + it.AccountNo;
                    }
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "绑定订单账单号，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        /// <summary>
        /// 通过省市数据查询负责该省市的业务员账号信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXUserAddressEntity QueryWxAreaClient(WXUserAddressEntity entity)
        {
            return man.QueryWxAreaClient(entity);
        }
        /// <summary>
        /// 查询客户当前月的订单情况 计算可以购买特价轮胎数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderGoodsEntity> QueryClientOrderData(CargoOrderEntity entity)
        {
            return man.QueryClientOrderData(entity);
        }
        /// <summary>
        /// 查询推广规格上架产品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoProductShelvesEntity> QueryShelvesData(CargoProductShelvesEntity entity)
        {
            return man.QueryShelvesData(entity);
        }
        /// <summary>
        /// 查询客户本月的特价轮胎数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int QueryPurchaseNum(WXOrderEntity entity)
        {
            return man.QueryPurchaseNum(entity);
        }
        /// <summary>
        /// 查询客户特价轮胎数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int QueryYunSpecialTyreNum(WXOrderEntity entity)
        {
            return man.QueryYunSpecialTyreNum(entity);
        }
        /// <summary>
        /// 查询最新的版本记录数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public APPVersionEntity QueryLatestAppVersionEntity(APPVersionEntity entity)
        {
            return man.QueryLatestAppVersionEntity(entity);
        }
        /// <summary>
        /// 通过手机号码查询当天发送验证码记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXMobileCodeEntity QueryMobileCodeEntity(WXMobileCodeEntity entity)
        {
            return man.QueryMobileCodeEntity(entity);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddMobileCodeEntity(WXMobileCodeEntity entity)
        {
            man.AddMobileCodeEntity(entity);
        }
        /// <summary>
        /// 修改记录计数
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateMobileCodeEntity(WXMobileCodeEntity entity)
        {
            man.UpdateMobileCodeEntity(entity);
        }
        public void AddAppCellphoneUser(WXUserEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            try
            {
                man.AddAppCellphoneUser(entity);
                log.Memo = "新增APP用户";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增APP用户，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        public void UpdateWeixinUserByUnionID(WXUserEntity entity)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                man.UpdateWeixinUserByUnionID(entity);
                scope.Complete();
            }
        }
        public void BindingClientNumAPP(WXUserEntity entity, LogEntity log)
        {
            LogWrite<WXUserEntity> lw = new LogWrite<WXUserEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.BindingClientNumAPP(entity);
                    log.Memo = "APP客户绑定：店代码：" + entity.ClientNum.ToString() + "，手机号：" + entity.Cellphone + "，所在地区：" + entity.Province + " " + entity.City + " " + entity.Country;
                    lw.WriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "客户绑定，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }

            }
        }
        /// <summary>
        /// 查询苹果提审数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public AppleCheckEntity QueryAppleCheckEntity(AppleCheckEntity entity)
        {
            return man.QueryAppleCheckEntity(entity);
        }
        /// <summary>
        /// 新增优惠券
        /// </summary>
        /// <param name="entity"></param>
        public void AddCoupon(List<WXCouponEntity> entity, LogEntity log)
        {
            LogWrite<WXCouponEntity> lw = new LogWrite<WXCouponEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    foreach (var it in entity)
                    {
                        man.AddCoupon(it);
                        log.Memo = "获取优惠券：用户ID：" + it.WXID.ToString() + "，用户名：" + it.Name + "，金额：" + it.Money.ToString() + "，有效期：" + it.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "-" + it.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                        lw.WriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "获取优惠券，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 设置优惠券已使用或已过期
        /// </summary>
        /// <param name="entity"></param>
        public void SetCouponUsed(List<WXCouponEntity> entity, LogEntity log)
        {
            LogWrite<WXCouponEntity> lw = new LogWrite<WXCouponEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    foreach (var it in entity)
                    {
                        it.EnSafe();
                        man.SetCouponUsed(it);
                        if (it.UseStatus.Equals("1"))
                        {
                            log.Memo = "优惠券已使用：主键：" + it.ID.ToString() + ",用户ID：" + it.WXID.ToString() + "，使用状态：" + it.UseStatus + "，金额：" + it.Money.ToString() + ",使用时间：" + it.UseDate.ToString("yyyy-MM-dd HH:mm:ss") + ",订单ID：" + it.OrderID.ToString() + ",下单方式：" + it.OrderType;
                        }
                        else
                        {
                            log.Memo = "优惠券已过期：主键：" + it.ID.ToString() + ",用户ID：" + it.WXID.ToString() + "，使用状态：" + it.UseStatus;
                        }

                        lw.WriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Status = "1";
                    log.Memo = "优惠券，失败信息：" + ex.Message;
                    lw.WriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 查询我的优惠券或我的店代码下面的所有优惠券
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXCouponEntity> QueryCouponData(WXCouponEntity entity)
        {
            return man.QueryCouponData(entity);
        }
        /// <summary>
        /// 通过ID查询优惠券信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXCouponEntity QueryCouponByID(WXCouponEntity entity)
        {
            return man.QueryCouponByID(entity);
        }
        #region 小程序 
        #region 购物车
        public List<ShippingCarItemsInfo> QueryShippingCarInfo(ShippingCarItemsInfo entity)
        {
            return man.QueryShippingCarInfo(entity);
        }
        public void AddShippingCarItem(ShippingCarItemsInfo entity, LogEntity log)
        {
            LogWrite<ShippingCarItemsInfo> lw = new LogWrite<ShippingCarItemsInfo>();
            try
            {
                man.AddShippingCarItem(entity);
                log.Memo = "添加购物车";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "添加购物车，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        public void ModifyShippingCarItem(ShippingCarItemsInfo entity, LogEntity log)
        {
            LogWrite<ShippingCarItemsInfo> lw = new LogWrite<ShippingCarItemsInfo>();
            try
            {
                man.ModifyShippingCarItem(entity);
                log.Memo = "修改购物车数量";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改购物车数量，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        public void SelectedShippingCarInfo(ShippingCarItemsInfo entity, LogEntity log)
        {
            LogWrite<ShippingCarItemsInfo> lw = new LogWrite<ShippingCarItemsInfo>();
            try
            {
                man.SelectedShippingCarInfo(entity);
                log.Memo = "修改购物车选中";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改购物车选中，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        public void DelShippingCarItem(ShippingCarItemsInfo entity, LogEntity log)
        {
            LogWrite<ShippingCarItemsInfo> lw = new LogWrite<ShippingCarItemsInfo>();
            try
            {
                man.DelShippingCarItem(entity);
                log.Memo = "删除购物车";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "删除购物车，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        #endregion
        #endregion

        #region 云配小程序
        /// <summary>
        /// 查询我的商城订单
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<MiniProOrderEntity> QueryMyMiniProOrder(int pIndex, int pNum, WXOrderEntity entity)
        {
            return man.QueryMyMiniProOrder(pIndex, pNum, entity);
        }

        /// <summary>
        /// 新增微信小程序商城订单退款申请单
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderRefund(WXOrderEntity entity, LogEntity log)
        {
            LogWrite<WXOrderEntity> lw = new LogWrite<WXOrderEntity>();

            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                man.UpdateWxOrderRefund(entity);
                List<WXCouponEntity> wxe = man.QueryCouponData(new WXCouponEntity { FromOrderNO = entity.OrderNo });
                foreach (WXCouponEntity item in wxe)
                {
                    if (item.UseStatus.Equals("0"))
                    {
                        man.DelCoupon(item);
                    }
                }
                log.Memo = "微信小程序订单号：" + entity.OrderNo + ",退款原因:" + entity.RefundReason + ",退款备注:" + entity.RefundMemo + ",用户OpenID:" + entity.wxOpenID + ",用户名:" + entity.Name;
                lw.WriteLog(log);

                scope.Complete();
            }
        }
        /// <summary>
        /// 更新微信订单退货来货入库单号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWxOrderFacOrderNo(WXOrderEntity entity)
        {
            man.UpdateWxOrderFacOrderNo(entity);
        }

        /// <summary>
        /// 查询我的企业订单 企业客户
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<MiniProOrderEntity> QueryMyEnterpriseOrder(int pIndex, int pNum, CargoOrderEntity entity)
        {
            return man.QueryMyEnterpriseOrder(pIndex, pNum, entity);
        }

        /// <summary>
        /// 查询供应商对应仓库销售数据
        /// </summary>
        /// <param name="entity">openid</param>
        /// <param name="days">天数</param>
        /// <returns></returns>
        public List<WXSerViceSettleHouseEntity> QueryServiceOrderData(string entity,string days,string currdate)
        {
            return man.QueryServiceHouseData(entity, days, currdate);
        }

        /// <summary>
        /// 获取退款数据
        /// </summary>
        /// <param name="entity">openid</param>
        /// <param name="days">天数</param>
        /// <returns></returns>
        public List<WXOrderEntity> QueryRefundOrderData(string entity)
        {
            return man.QueryRefundOrderData(entity);
        }

        /// <summary>
        /// 查询供应商个人信息
        /// </summary>
        /// <returns></returns>
        public List<WxClientUserEntity> QueryWxDetailData(string entity)
        {
            return man.QueryWxDetailData(entity);
        }

        /// <summary>
        /// 查询供应商账单信息
        /// </summary>
        /// <returns></returns>
        public List<CargoSuppClientAccountEntityParams> QueryWxSupplierBillData(string entity)
        {
            return man.QueryWxSupplierBillData(entity);
        }

        /// <summary>
        /// 查询服务号供应商信息
        /// </summary>
        /// <returns></returns>
        public List<WxClientUserEntity> QueryWxSupplierOpenID(string clientNum=null)
        {
            return man.QueryWxSupplierOpenID(clientNum);
        }


        /// <summary>
        /// 查询供应商销售数据
        /// </summary>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryWxSupplierSalesData(CargoOrderEntity entity, int days)
        {
            return man.QueryWxSupplierSalesData(entity,days);
        }
        /// <summary>
        /// 更新服务号数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void UpdateWxStoresData(WxClientUserEntity entity, LogEntity log)
        {
            LogWrite<WxClientUserEntity> lw = new LogWrite<WxClientUserEntity>();
            try
            {
                man.UpdateWxStoresData(entity);
                log.Memo = "更新用户数据，OpenID：" + entity.wxOpenID + "，微信名称：" + entity.wxName;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 更新签名数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void UpdateWxSuppSignature(CargoSuppClientAccountEntity entity, LogEntity log)
        {
            LogWrite<CargoSuppClientAccountEntity> lw = new LogWrite<CargoSuppClientAccountEntity>();
            try
            {
                man.UpdateWxSuppSignature(entity);
                log.Memo = "更新签名数据，AccountNO：" + entity.AccountNO;
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        #endregion
    }
}
