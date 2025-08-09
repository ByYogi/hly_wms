using House.Entity;
using House.Entity.Cargo;
using House.Entity.House;
using House.Manager;
using House.Manager.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace House.Business.Cargo
{
    public class ArriveBus
    {
        private ArriveManager man = new ArriveManager();
        #region 托运合同录入
        public List<ClientSessionEntity> QueryClientSession(ClientEntity entity)
        {
            return man.QueryClientSession(entity);
        }
        /// <summary>
        /// 根据入库员代码查询用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SystemUserEntity GetUserNameByClerkNo(SystemUserEntity entity)
        {
            return man.GetUserNameByClerkNo(entity);
        }
        /// <summary>
        /// 查询好来运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<HlyExchangeEntity> QueryHlyAwbInfo(HlyExchangeEntity entity)
        {
            return man.QueryHlyAwbInfo(entity);
        }
        /// <summary>
        /// 通过客户编号查询客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientEntity GetClientByID(int Clientid)
        {
            return man.GetClientByID(Clientid);
        }
        /// <summary>
        /// 查询该客户下的所有收货地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<ClientAcceptAddressEntity> QueryClientAcceptAddress(ClientAcceptAddressEntity entity)
        {
            return man.QueryClientAcceptAddress(entity);
        }
        /// <summary>
        /// 判断运单号是否在所允许的区间内
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public bool IsRangeAwbNo(AwbEntity ent)
        {
            return man.IsRangeAwbNo(ent);
        }
        /// <summary>
        /// 判断运单号是否存在
        /// </summary>
        /// <param name="awb"></param>
        /// <returns></returns>
        public bool IsExistAwb(AwbEntity awb)
        {
            return man.IsExistAwb(awb);
        }
        /// <summary>
        /// 新增运单信息
        /// </summary>
        /// <param name="awb"></param>
        public void AddAwbInfo(AwbEntity awb, LogEntity log)
        {
            LogWrite<AwbEntity> lw = new LogWrite<AwbEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.AddAwbInfo(awb);
                    lw.NewayWriteLog(awb, log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "新增运单失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 修改好来运保存状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateHlyAwbStatus(HlyExchangeEntity entity, LogEntity log)
        {
            LogWrite<HlyExchangeEntity> lw = new LogWrite<HlyExchangeEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.UpdateHlyAwbStatus(entity);
                    log.Memo = "好来运五联单号：" + entity.HlyFiveNo + "，保存状态：" + entity.SaveStatus;
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "保存失败，失败信息：" + ex.InnerException.Message;
                    lw.NewayWriteLog(log);
                }
            }
        }
        #endregion
        #region 托运合同维护
        /// <summary>
        /// 运单信息查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryAwb(int pIndex, int pNum, AwbEntity entity)
        {
            return man.QueryAwb(pIndex, pNum, entity);
        }
        /// <summary>
        /// 修改运单信息
        /// </summary>
        /// <param name="awb"></param>
        public void UpdateAwbInfo(AwbEntity awb, LogEntity log)
        {
            LogWrite<AwbEntity> lw = new LogWrite<AwbEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.UpdateAwbInfo(awb);
                    lw.NewayWriteLog(awb, log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "修改运单失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 删除或作废运单
        /// </summary>
        /// <param name="entity"></param>
        public void DelAwb(List<AwbEntity> entity, int ty, LogEntity log)
        {
            LogWrite<AwbEntity> lw = new LogWrite<AwbEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.DelAwb(entity, ty);
                    foreach (var awb in entity)
                    {
                        lw.NewayWriteLog(awb, log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "作废运单失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        #endregion
        #region 长运配载操作
        /// <summary>
        /// 运单信息查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryAwbToManifest(int pIndex, int pNum, AwbEntity entity)
        {
            return man.QueryAwbToManifest(pIndex, pNum, entity);
        }
        /// <summary>
        /// 根据单位ID查询该单位下的所有部门数据
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public List<SystemDepartEntity> GetDeptByUnitID(string UnitID, string BelongSystem)
        {
            return man.GetDeptByUnitID(UnitID, BelongSystem);
        }
        #endregion
        #region 回单录入
        /// <summary>
        /// 查询所有到达的配载卡车
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryArriveManifestFromReturnAwb(int pIndex, int pNum, DepManifestEntity entity)
        {
            return man.QueryArriveManifestFromReturnAwb(pIndex, pNum, entity);
        }
        /// <summary>
        /// 更新回单信息
        /// </summary>
        /// <param name="awb"></param>
        public void AddReturnAwbInfo(List<AwbEntity> awblist, LogEntity log)
        {
            LogWrite<AwbEntity> lw = new LogWrite<AwbEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.AddReturnAwbInfo(awblist);
                    foreach (var it in awblist)
                    {
                        log.Memo = "回单录入，回单信息：" + it.ReturnInfo.ToString() + "，实收金额：" + it.ActMoney.ToString() + "，运单号：" + it.AwbNo + "，司机合同号：" + it.ContractNum + "，操作人：" + log.UserID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        lw.NewayWriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "回单录入，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 查询所有运单通过司机合同号
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryAwbByContractNum(int pIndex, int pNum, AwbEntity entity)
        {
            return man.QueryAwbByContractNum(pIndex, pNum, entity);
        }
        /// <summary>
        /// 查询所有运单
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryAwbForReturn(int pIndex, int pNum, AwbEntity entity)
        {
            return man.QueryAwbForReturn(pIndex, pNum, entity);
        }
        #endregion
        #region 到达运单派送

        /// <summary>
        /// 到达运单信息查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryAwbRetrunStatus(int pIndex, int pNum, AwbEntity entity)
        {
            return man.QueryAwbRetrunStatus(pIndex, pNum, entity);
        }
        /// <summary>
        /// 判断到达运单是否存在
        /// </summary>
        /// <param name="awb"></param>
        /// <returns></returns>
        public bool IsExistArriveAwb(DeliveryEntity awb)
        {
            return man.IsExistArriveAwb(awb);
        }
        /// <summary>
        /// 合并运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        public void DeliveryMerge(AwbEntity entity, List<AwbEntity> list, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.DeliveryMerge(entity, list);
                    log.Memo = "到达运单合并成功,";
                    for (int i = 0; i < list.Count; i++)
                    {
                        log.Memo += "运单" + (i + 1).ToString() + "运单号：" + list[i].AwbNo + "运单ID：" + list[i].AwbID.ToString() + "件数：" + list[i].AwbPiece.ToString() + "重量：" + list[i].AwbWeight.ToString() + "体积：" + list[i].AwbVolume + "  ";
                    }

                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "到达运单合并失败，失败信息：" + ex.Message + "运单号：" + entity.AwbNo;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 分批操作
        /// </summary>
        /// <param name="ent"></param>
        public void DeliveryTear(List<AwbEntity> entity, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.DeliveryTear(entity);
                    log.Memo = "到达运单分批成功，运单号：" + entity[0].AwbNo + "运单ID：" + entity[0].AwbID.ToString() + " 运单件数：" + entity[0].Piece.ToString() + " 重量：" + entity[0].Weight.ToString("F2") + "体积：" + entity[0].Volume.ToString("F2") + "分批件数：" + entity[0].AwbPiece.ToString() + "," + entity[1].AwbPiece.ToString() + "分批重量：" + entity[0].AwbWeight.ToString() + "," + entity[1].AwbWeight.ToString() + "分批体积：" + entity[0].AwbVolume.ToString() + "," + entity[1].AwbVolume.ToString();
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "到达运单分批失败，失败信息：" + ex.Message + "运单号：" + entity[0].AwbNo + "运单ID：" + entity[0].AwbID.ToString();
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 查询所有城市数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CityEntity> QueryAllCity(CityEntity entity)
        {
            return man.QueryAllCity(entity);
        }
        /// <summary>
        /// 查询所有具体承运资格的启用的承运商档案信息
        /// </summary>
        /// <returns></returns>
        public List<CarrierEntity> QueryCarrier(string BelongSystem)
        {
            return man.QueryCarrier(BelongSystem);
        }
        /// <summary>
        /// 查询所有用户名
        /// </summary>
        /// <returns></returns>
        public List<NewaySystemUserEntity> QueryALLUser(string sys)
        {
            return man.QueryALLUser(sys);
        }
        /// <summary>
        /// 根据发车时间查询当天配送发的车辆数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ReturnDayTheTruckNum(DeliveryEntity entity)
        {
            return man.ReturnDayTheTruckNum(entity);
        }
        /// <summary>
        /// 根据发车时间查询当天发的车辆数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ReturnDayTheTruckNum(DepManifestEntity entity)
        {
            return man.ReturnDayTheTruckNum(entity);
        }
        /// <summary>
        /// 新增配送运单信息
        /// </summary>
        public int AddDelivery(List<DeliveryEntity> entity, LogEntity log)
        {
            int result = 0;
            LogWrite<DeliveryEntity> lw = new LogWrite<DeliveryEntity>();

            if (man.IsExistTruck(new TruckEntity { TruckNum = entity[0].TruckNum, BelongSystem = entity[0].BelongSystem }))
            {
                string pz = man.IsExistCurCityTruck(new TruckEntity { TruckNum = entity[0].TruckNum, BelongSystem = entity[0].BelongSystem });
                if (pz.Equals("2"))//配载
                {
                    return 2;
                }
                if (pz.Equals("3"))//拉黑
                {
                    return 3;
                }
            }
            else
            {
                TruckEntity te = new TruckEntity
                {
                    TruckNum = entity[0].TruckNum.Trim(),
                    Driver = entity[0].Driver,
                    DriverCellPhone = entity[0].DriverCellPhone,
                    DriverIDAddress = "",
                    DriverIDNum = "",
                    TripMark = "1",
                    DelFlag = "0",
                    City = entity[0].CurCity,
                    BelongSystem = entity[0].BelongSystem,
                    CurCity = entity[0].CurCity,
                    OP_ID = entity[0].OP_ID
                };
                man.SaveTruckInfo(te);
            }
            foreach (var it in entity)
            {
                if (man.IsDeliveryAwb(it))
                {
                    return 1;
                }
            }
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    string awbno = string.Empty;
                    Int64 did = man.AddDelivery(entity[0]);
                    foreach (var it in entity)
                    {
                        man.AddDeliveryAwb(new DeliveryAwbEntity
                        {
                            ArriveID = it.ArriveID,
                            AwbID = it.AwbID,
                            AwbNo = it.AwbNo,
                            DeliveryID = did,
                            BelongSystem = entity[0].BelongSystem,
                            Mode = "0",
                            OP_ID = it.OP_ID
                        });

                        //更新运单跟踪状态
                        man.InsertAwbStatus(new AwbStatus
                        {
                            AwbID = it.AwbID,
                            AwbNo = it.AwbNo,
                            TruckFlag = "8",
                            CurrentLocation = it.Dest,
                            BelongSystem = entity[0].BelongSystem,
                            LastHour = 0,
                            DetailInfo = "",
                            OP_DATE = it.StartTime,
                            OP_ID = it.OP_ID
                        });
                        if (it.AwbStatus.Equals("3"))
                        {
                            //更新运单状态
                            man.SetAwbStatus(new AwbEntity
                            {
                                AwbID = it.AwbID,
                                BelongSystem = entity[0].BelongSystem,
                                AwbStatus = "8"
                            });
                            //更新到达运单状态
                            man.SetArriveAwbStatus(new AwbEntity
                            {
                                ArriveID = it.ArriveID,
                                BelongSystem = entity[0].BelongSystem,
                                AwbStatus = "8"
                            });
                        }
                        awbno += it.AwbNo.Trim();
                    }
                    log.Memo = "新增配送单成功，配载合同号：" + entity[0].DeliveryNum + "，运单号：" + awbno + "，车牌号码：" + entity[0].TruckNum + "，司机姓名：" + entity[0].Driver + "，司机手机号码：" + entity[0].DriverCellPhone + "，发车时间：" + entity[0].StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "，运费：" + entity[0].TransportFee.ToString() + "，其它费用：" + entity[0].OtherFee.ToString() + "，操作人：" + entity[0].OP_ID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "新增到达运单配送失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
            return result;
        }
        /// <summary>
        /// 保存配载数据信息
        /// </summary>
        /// <param name="awb"></param>
        public int SaveManifest(DepManifestEntity awb, LogEntity log)
        {
            log.Status = "1";
            int result = 0;
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            try
            {
                if (man.IsExistTruck(new TruckEntity { TruckNum = awb.TruckNum, BelongSystem = awb.BelongSystem }))
                {
                    string pz = man.IsExistCurCityTruck(new TruckEntity { TruckNum = awb.TruckNum, BelongSystem = awb.BelongSystem });
                    if (pz.Equals("2"))//配载
                    {
                        return 2;
                    }
                    if (pz.Equals("3"))//拉黑
                    {
                        return 3;
                    }
                }
                foreach (var it in awb.AwbInfo)
                {
                    if (man.IsManifestAwb(it)) { result = 1; break; }
                }
                //使用事务
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (result.Equals(0))
                    {
                        man.SaveManifest(awb);
                        lw.NewayWriteLog(awb, log);
                        scope.Complete();
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                log.Memo = "新增司机合同失败，失败信息：" + ex.Message;
                lw.NewayWriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 合并运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        public void merge(AwbEntity entity, List<AwbEntity> list, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.merge(entity, list);
                    log.Memo = "运单合并成功,";
                    for (int i = 0; i < list.Count; i++)
                    {
                        log.Memo += "运单" + (i + 1).ToString() + "运单号：" + list[i].AwbNo + "运单ID：" + list[i].AwbID.ToString() + "件数：" + list[i].AwbPiece.ToString() + "重量：" + list[i].AwbWeight.ToString() + "体积：" + list[i].AwbVolume + "  ";
                    }

                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "运单合并失败，失败信息：" + ex.Message + "运单号：" + entity.AwbNo;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 分批操作
        /// </summary>
        /// <param name="ent"></param>
        public void Tear(List<AwbEntity> entity, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.Tear(entity);
                    log.Memo = "运单分批成功，运单号：" + entity[0].AwbNo + "运单ID：" + entity[0].AwbID.ToString() + " 运单件数：" + entity[0].Piece.ToString() + " 重量：" + entity[0].Weight.ToString("F2") + "体积：" + entity[0].Volume.ToString("F2") + "分批件数：" + entity[0].AwbPiece.ToString() + "," + entity[1].AwbPiece.ToString() + "分批重量：" + entity[0].AwbWeight.ToString() + "," + entity[1].AwbWeight.ToString() + "分批体积：" + entity[0].AwbVolume.ToString() + "," + entity[1].AwbVolume.ToString();
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "运单分批失败，失败信息：" + ex.Message + "运单号：" + entity[0].AwbNo + "运单ID：" + entity[0].AwbID.ToString();
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 根据车牌号获取司机的银行卡信息
        /// </summary>
        /// <returns></returns>
        public List<DriverCardEntity> GetDriverCardByTruckNum(string TruckNum)
        {
            return man.GetDriverCardByTruckNum(TruckNum);
        }
        #endregion
        #region 车辆合同管理

        /// <summary>
        /// 查看所有已经生成司机合同的运单信息
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryDepManifest(int pIndex, int pNum, DepManifestEntity entity)
        {
            return man.QueryDepManifest(pIndex, pNum, entity);
        }
        /// <summary>
        /// 根据配载合同号查询配载信息和运单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DepManifestEntity QueryAwbInfoByContractNumForExport(DepManifestEntity entity)
        {
            return man.QueryAwbInfoByContractNumForExport(entity);
        }
        /// <summary>
        /// 长途车车辆合同信息查询供导出
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<DepManifestEntity> QueryDepManifestForExport(DepManifestEntity entity)
        {
            return man.QueryDepManifestForExport(entity);
        }
        /// <summary>
        /// 查询待运库运单信息
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryAwbFromUpdate(int pIndex, int pNum, AwbEntity entity)
        {
            return man.QueryAwbFromUpdate(pIndex, pNum, entity);
        }
        /// <summary>
        /// 通过车辆合同号查询配载信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DepManifestEntity QueryDepManifest(DepManifestEntity entity)
        {
            return man.QueryDepManifest(entity);
        }
        /// <summary>
        /// 修改司机合同信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateManifest(List<DepManifestEntity> entity, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.UpdateManifest(entity);
                    foreach (var it in entity)
                    {
                        log.Memo += "，合同号：" + it.ContractNum + "，操作人：" + it.OP_ID;
                        lw.NewayWriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo += "失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 删除配载司机合同
        /// </summary>
        /// <param name="entity"></param>
        public void DelManifest(List<DepManifestEntity> entity, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.DelManifest(entity);
                    foreach (var it in entity)
                    {
                        log.Memo = "作废配载合同成功，司机合同号：" + it.ContractNum + "，操作人：" + log.UserID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        lw.NewayWriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "作废配载合同失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 启用作废的配载司机合同
        /// </summary>
        /// <param name="entity"></param>
        public void RenewManifest(List<DepManifestEntity> entity, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.RenewManifest(entity);
                    foreach (var it in entity)
                    {
                        log.Memo = "启用作废的配载合同成功，司机合同号：" + it.ContractNum + "，操作人：" + log.UserID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        lw.NewayWriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "启用作废的配载合同失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 修改配载数据信息
        /// </summary>
        /// <param name="awb"></param>
        public void UpdateContractInfo(DepManifestEntity awb, LogEntity log)
        {

            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.UpdateContractInfo(awb);
                    lw.NewayWriteLog(awb, log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "修改司机合同失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        #endregion
        #region 车辆在途跟踪

        /// <summary>
        /// 查看所有已经发往到达站的所有配载数据 预先到达查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryArriveManifest(int pIndex, int pNum, DepManifestEntity entity)
        {
            return man.QueryArriveManifest(pIndex, pNum, entity);
        }
        /// <summary>
        /// 查询车辆的跟踪信息
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public DataTable QueryTruckStatusTrack(TruckStatusTrackEntity ent)
        {
            return man.QueryTruckStatusTrack(ent);
        }
        /// <summary>
        /// 保存车辆状态跟踪信息
        /// </summary>
        /// <param name="ent"></param>
        public void SaveTruckStatusTrack(TruckStatusTrackEntity ent, LogEntity log)
        {
            LogWrite<TruckStatusTrackEntity> lw = new LogWrite<TruckStatusTrackEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.SaveTruckStatusTrack(ent);
                    log.Memo = "车辆合同号：" + ent.ContractNum.Trim() + "，车牌号：" + ent.TruckNum.Trim() + "，到达时间：" + ent.ArriveTime.ToString("yyyy-MM-dd HH:mm:ss") + "，当前位置：" + ent.CurrentLocation + "，详细内容：" + ent.DetailInfo + "，操作人：" + ent.OP_ID;
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "修改车辆跟踪状态失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 查询所有运单通过司机合同号
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public List<AwbEntity> QueryAwbByContractNum(AwbEntity entity)
        {
            return man.QueryAwbByContractNum(entity);
        }
        #endregion
        #region 配送运单管理

        /// <summary>
        /// 配送运单管理查询
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryDeliveryOrder(int pIndex, int pNum, DeliveryEntity entity)
        {
            return man.QueryDeliveryOrder(pIndex, pNum, entity);
        }
        /// <summary>
        /// 查询所有车辆信息
        /// </summary>
        /// <returns></returns>
        public List<TruckCacheEntity> QueryTruck(TruckEntity te)
        {
            return man.QueryTruck(te);
        }
        /// <summary>
        /// 通过城市代码查询单位信息
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public SystemUnitEntity GetUnitByCityCode(string citycode, string sys)
        {
            return man.GetUnitByCityCode(citycode, sys);
        }
        /// <summary>
        /// 根据配送单号查询所有运单
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryAwbInfoByDeliveryID(int pIndex, int pNum, DeliveryEntity entity)
        {
            return man.QueryAwbInfoByDeliveryID(pIndex, pNum, entity);
        }
        /// <summary>
        /// 通过配送合同号查询短途配载数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DeliveryEntity QueryAwbByDeliveryID(Int64 did, string dty)
        {
            return man.QueryAwbByDeliveryID(did, dty);
        }
        /// <summary>
        /// 配送单审核成功
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void CheckDelivery(List<DeliveryEntity> entity, LogEntity log)
        {
            LogWrite<DeliveryEntity> lw = new LogWrite<DeliveryEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    foreach (var it in entity)
                    {
                        man.CheckDelivery(it);
                        log.Memo += " 配送单号：" + it.DeliveryID.ToString();
                    }
                    log.Memo += " 操作人：" + log.UserID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "审核配送单失败，失败信息：" + ex.Message; lw.NewayWriteLog(log); throw;
                }
            }
        }
        /// <summary>
        /// 通过配送(中转)合同号查询配送单中的运单是否都是配送状态
        /// </summary>
        /// <param name="did">配送中转单号</param>
        /// <param name="mode">类型0配送1中转</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool IsAllDeliveryByDeliverID(Int64 did, string mode, string status, string BelongSystem)
        {
            return man.IsAllDeliveryByDeliverID(did, mode, status, BelongSystem);
        }
        /// <summary>
        /// 删除配送运单
        /// </summary>
        /// <param name="entity"></param>
        public void DelDelivery(List<DeliveryEntity> entity, LogEntity log)
        {
            LogWrite<DeliveryEntity> lw = new LogWrite<DeliveryEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    foreach (var it in entity)
                    {
                        List<DeliveryAwbEntity> dlist = man.QueryAwbIDByDeliveryID(it.DeliveryID, "0");
                        man.DelDelivery(it.DeliveryID);
                        foreach (var awb in dlist)
                        {
                            if (awb.AwbStatus.Equals("8"))
                            {
                                //更新运单状态
                                man.SetAwbStatus(new AwbEntity { AwbID = awb.AwbID, AwbStatus = "3", BelongSystem = it.BelongSystem });
                                //更新到达运单状态
                                man.SetArriveAwbStatus(new AwbEntity { ArriveID = awb.ArriveID, AwbStatus = "3", BelongSystem = it.BelongSystem });
                                //删除运单跟踪状态
                                man.DeleteAwbStatus(awb.AwbNo, awb.AwbID, "8", it.BelongSystem);
                            }
                            log.Memo = "删除配送单成功，配送单号：" + it.DeliveryID.ToString() + "，运单ID：" + awb.AwbID.ToString() + "，运单号：" + awb.AwbNo.Trim() + "，操作人：" + log.UserID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            lw.NewayWriteLog(log);
                        }
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "删除配送单失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 配送运单导出查询
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<DeliveryEntity> QueryDeliveryOrderForExport(DeliveryEntity entity)
        {
            return man.QueryDeliveryOrderForExport(entity);
        }
        /// <summary>
        /// 修改配送运单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void UpdateDeliveryOrder(List<DeliveryEntity> entity, LogEntity log)
        {
            LogWrite<DeliveryEntity> lw = new LogWrite<DeliveryEntity>();
            string awbno = string.Empty;
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    Int64 did = entity[0].DeliveryID;
                    List<DeliveryAwbEntity> dae = man.QueryAwbIDByDeliveryID(did, "0");
                    //修改配送信息表
                    man.UpdateDelivery(entity[0]);
                    //删除配送单号与运单的关联表
                    man.DelDeliveryTransitAwb(did, "0");
                    foreach (var de in dae)
                    {
                        if (de.AwbStatus.Equals("8"))
                        {
                            //更新运单状态
                            man.SetAwbStatus(new AwbEntity { AwbID = de.AwbID, AwbStatus = "3", BelongSystem = entity[0].BelongSystem });
                            //更新到达运单状态
                            man.SetArriveAwbStatus(new AwbEntity { ArriveID = de.ArriveID, AwbStatus = "3", BelongSystem = entity[0].BelongSystem });
                            //删除运单状态
                            man.DeleteAwbStatus(de.AwbNo, de.AwbID, "8", entity[0].BelongSystem);
                        }
                    }
                    foreach (var it in entity)
                    {
                        man.AddDeliveryAwb(new DeliveryAwbEntity { ArriveID = it.ArriveID, AwbID = it.AwbID, AwbNo = it.AwbNo, DeliveryID = did, Mode = "0", OP_ID = it.OP_ID, BelongSystem = it.BelongSystem });
                        string awbstatus = man.QueryAwbStatusByAwbIDNo(it.AwbID, it.AwbNo, it.BelongSystem);
                        if (awbstatus == "3")
                        {
                            //运单状态
                            man.InsertAwbStatus(new AwbStatus { AwbID = it.AwbID, AwbNo = it.AwbNo, TruckFlag = "8", CurrentLocation = it.Dest, LastHour = 0, DetailInfo = "", OP_ID = it.OP_ID, BelongSystem = it.BelongSystem });
                            //更新运单状态
                            man.SetAwbStatus(new AwbEntity { AwbID = it.AwbID, AwbStatus = "8", BelongSystem = it.BelongSystem });
                            //更新到达运单状态
                            man.SetArriveAwbStatus(new AwbEntity { ArriveID = it.ArriveID, AwbStatus = "8", BelongSystem = it.BelongSystem });
                        }
                        awbno += it.AwbNo;
                    }
                    log.Memo = "修改配送单成功，配载合同号：" + entity[0].DeliveryNum + "，运单号：" + awbno + "，车牌号码：" + entity[0].TruckNum + "，司机姓名：" + entity[0].Driver + "，司机手机号码：" + entity[0].DriverCellPhone + "，发车时间：" + entity[0].StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "，运费：" + entity[0].TransportFee.ToString() + "，其它费用：" + entity[0].OtherFee.ToString() + "，操作人：" + entity[0].OP_ID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "修改配送单失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        #endregion
        #region 到达运单中转

        /// <summary>
        /// 合并运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        public void TransitMerge(AwbEntity entity, List<AwbEntity> list, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.TransitMerge(entity, list);
                    log.Memo = "到达运单合并成功,";
                    for (int i = 0; i < list.Count; i++)
                    {
                        log.Memo += "运单" + (i + 1).ToString() + "运单号：" + list[i].AwbNo + "运单ID：" + list[i].AwbID.ToString() + "件数：" + list[i].AwbPiece.ToString() + "重量：" + list[i].AwbWeight.ToString() + "体积：" + list[i].AwbVolume + "  ";
                    }

                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "到达运单合并失败，失败信息：" + ex.Message + "运单号：" + entity.AwbNo;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 分批操作
        /// </summary>
        /// <param name="ent"></param>
        public void TransitTear(List<AwbEntity> entity, LogEntity log)
        {
            LogWrite<DepManifestEntity> lw = new LogWrite<DepManifestEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.TransitTear(entity);
                    log.Memo = "到达运单分批成功，运单号：" + entity[0].AwbNo + "运单ID：" + entity[0].AwbID.ToString() + " 运单件数：" + entity[0].Piece.ToString() + " 重量：" + entity[0].Weight.ToString("F2") + "体积：" + entity[0].Volume.ToString("F2") + "分批件数：" + entity[0].AwbPiece.ToString() + "," + entity[1].AwbPiece.ToString() + "分批重量：" + entity[0].AwbWeight.ToString() + "," + entity[1].AwbWeight.ToString() + "分批体积：" + entity[0].AwbVolume.ToString() + "," + entity[1].AwbVolume.ToString();
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "到达运单分批失败，失败信息：" + ex.Message + "运单号：" + entity[0].AwbNo + "运单ID：" + entity[0].AwbID.ToString();
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 判断运单是否配送
        /// </summary>
        /// <param name="awb"></param>
        /// <returns></returns>
        public bool IsDeliveryAwb(DeliveryEntity awb)
        {
            return man.IsDeliveryAwb(awb);
        }
        /// <summary>
        /// 新增中转运单信息
        /// </summary>
        public void AddTransit(List<TransitEntity> entity, LogEntity log)
        {
            LogWrite<TransitEntity> lw = new LogWrite<TransitEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    Int64 did = man.AddTransit(entity[0]);
                    foreach (var it in entity)
                    {
                        man.AddDeliveryAwb(new DeliveryAwbEntity
                        {
                            ArriveID = it.ArriveID,
                            AwbID = it.AwbID,
                            AwbNo = it.AwbNo,
                            DeliveryID = did,
                            Mode = "1",
                            BelongSystem = it.BelongSystem,
                            OP_ID = it.OP_ID
                        });
                        //运单状态
                        man.InsertAwbStatus(new AwbStatus
                        {
                            AwbID = it.AwbID,
                            AwbNo = it.AwbNo,
                            TruckFlag = "9",
                            CurrentLocation = it.Dest,
                            BelongSystem = it.BelongSystem,
                            LastHour = 0,
                            DetailInfo = "",
                            OP_ID = it.OP_ID
                        });
                        //更新运单状态
                        man.SetAwbStatus(new AwbEntity
                        {
                            AwbID = it.AwbID,
                            BelongSystem = it.BelongSystem,
                            AwbStatus = "9"
                        });
                        //更新到达运单状态
                        man.SetArriveAwbStatus(new AwbEntity
                        {
                            ArriveID = it.ArriveID,
                            BelongSystem = it.BelongSystem,
                            AwbStatus = "9"
                        });
                        log.Memo = "新增运单中转成功";
                        lw.NewayWriteLog(it, log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "新增运单中转失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        #endregion
        #region 中转运单管理
        /// <summary>
        /// 中转运单管理查询
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryTransitOrder(int pIndex, int pNum, TransitEntity entity)
        {
            return man.QueryTransitOrder(pIndex, pNum, entity);
        }

        /// <summary>
        /// 审核中转单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void CheckTransit(List<TransitEntity> entity, LogEntity log)
        {
            LogWrite<TransitEntity> lw = new LogWrite<TransitEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    foreach (var it in entity)
                    {
                        man.CheckTransit(it);
                        log.Memo += "，中转单号：" + it.TransitID.ToString() + "，操作人：" + log.UserID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        lw.NewayWriteLog(log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "审核中转单失败，失败信息：" + ex.Message; lw.NewayWriteLog(log); throw;
                }
            }
        }
        /// <summary>
        /// 删除中转单
        /// </summary>
        /// <param name="entity"></param>
        public void DelTransit(List<TransitEntity> entity, LogEntity log)
        {
            LogWrite<TransitEntity> lw = new LogWrite<TransitEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    foreach (var it in entity)
                    {
                        List<DeliveryAwbEntity> dlist = man.QueryAwbIDByDeliveryID(it.TransitID, "1");
                        man.DelTransit(it.TransitID);
                        foreach (var awb in dlist)
                        {
                            if (awb.AwbStatus.Equals("9"))
                            {
                                //更新运单状态
                                man.SetAwbStatus(new AwbEntity { AwbID = awb.AwbID, AwbStatus = "3", BelongSystem = it.BelongSystem });
                                //更新到达运单状态
                                man.SetArriveAwbStatus(new AwbEntity { ArriveID = awb.ArriveID, AwbStatus = "3", BelongSystem = it.BelongSystem });
                                //删除运单跟踪状态
                                man.DeleteAwbStatus(awb.AwbNo, awb.AwbID, "9", it.BelongSystem);
                            }
                            log.Memo = "删除中转单成功，中转单号：" + it.TransitID.ToString() + "，运单ID：" + awb.AwbID.ToString() + "，运单号：" + awb.AwbNo.Trim() + "，操作人：" + log.UserID + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            lw.NewayWriteLog(log);
                        }
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "删除中转单失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 中转运单导出查询
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<TransitEntity> QueryTransitOrderForExport(TransitEntity entity)
        {
            return man.QueryTransitOrderForExport(entity);
        }
        /// <summary>
        /// 修改中转运单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void UpdateTransitOrder(List<TransitEntity> entity, LogEntity log)
        {
            LogWrite<TransitEntity> lw = new LogWrite<TransitEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1"; 
                try
                {
                    Int64 did = entity[0].TransitID;
                    List<DeliveryAwbEntity> dae = man.QueryAwbIDByDeliveryID(did, "1");
                    //修改中转信息表
                    man.UpdateTransit(entity[0]);
                    //删除中转单号与运单的关联表
                    man.DelDeliveryTransitAwb(did, "1");
                    foreach (var de in dae)
                    {
                        if (de.AwbStatus.Equals("9"))
                        {
                            //更新运单状态
                            man.SetAwbStatus(new AwbEntity { AwbID = de.AwbID, AwbStatus = "3", BelongSystem = entity[0].BelongSystem });
                            //更新到达运单状态
                            man.SetArriveAwbStatus(new AwbEntity { ArriveID = de.ArriveID, AwbStatus = "3", BelongSystem = entity[0].BelongSystem });
                            //删除运单状态
                            man.DeleteAwbStatus(de.AwbNo, de.AwbID, "9", entity[0].BelongSystem);
                        }
                    }
                    foreach (var it in entity)
                    {
                        man.AddDeliveryAwb(new DeliveryAwbEntity { ArriveID = it.ArriveID, AwbID = it.AwbID, AwbNo = it.AwbNo, DeliveryID = did, Mode = "1", OP_ID = it.OP_ID, BelongSystem = it.BelongSystem });
                        string awbstatus = man.QueryAwbStatusByAwbIDNo(it.AwbID, it.AwbNo, it.BelongSystem);
                        if (awbstatus == "3")
                        {
                            //新增运单状态
                            man.InsertAwbStatus(new AwbStatus { AwbID = it.AwbID, AwbNo = it.AwbNo, TruckFlag = "9", CurrentLocation = it.Dest, LastHour = 0, DetailInfo = "", OP_ID = it.OP_ID, BelongSystem = it.BelongSystem });
                            //更新运单状态
                            man.SetAwbStatus(new AwbEntity { AwbID = it.AwbID, AwbStatus = "9", BelongSystem = it.BelongSystem });
                            //更新到达运单状态
                            man.SetArriveAwbStatus(new AwbEntity { ArriveID = it.ArriveID, AwbStatus = "9", BelongSystem = it.BelongSystem });
                        }
                        log.Memo = "修改中转单成功"; lw.NewayWriteLog(it, log);
                    }
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "修改中转单失败，失败信息：" + ex.Message; lw.NewayWriteLog(log); throw;
                }
            }
        }
        #endregion
        #region 中转状态跟踪

        /// <summary>
        /// 配送运单管理查询
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryTransitAwbStatusTrack(int pIndex, int pNum, TransitEntity entity)
        {
            return man.QueryTransitAwbStatusTrack(pIndex, pNum, entity);
        }
        /// <summary>
        /// 查询所有运单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AwbEntity> QueryAwb(AwbEntity entity)
        {
            return man.QueryAwb(entity);
        }
        /// <summary>
        /// 查询运单跟踪状态信息
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public List<AwbStatus> QueryAwbTrackInfo(AwbStatus ent)
        {
            return man.QueryAwbTrackInfo(ent);
        }
        /// <summary>
        /// 保存中转运单状态跟踪信息
        /// </summary>
        /// <param name="ent"></param>
        public void SaveAwbTruckStatus(TruckStatusTrackEntity ent, LogEntity log)
        {
            LogWrite<TruckStatusTrackEntity> lw = new LogWrite<TruckStatusTrackEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.SaveAwbTruckStatus(ent);
                    lw.NewayWriteLog(ent, log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "修改到达出库运单跟踪状态失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        #endregion
        #region 客户货物查询
        /// <summary>
        /// 根据运单查询货物跟踪信息
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryClientAwbStatusTrack(int pIndex, int pNum, AwbEntity entity)
        {
            return man.QueryClientAwbStatusTrack(pIndex, pNum, entity);
        }
        /// <summary>
        /// 保存运单备注信息
        /// </summary>
        /// <param name="goods"></param>
        public void AddAwbRemarkInfo(AwbRemarkEntity remark, LogEntity log)
        {
            LogWrite<AwbRemarkEntity> lw = new LogWrite<AwbRemarkEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                log.Status = "1";
                try
                {
                    man.AddAwbRemarkInfo(remark);
                    log.Memo = "新增运单备注成功，运单号：" + remark.AwbNo + "，操作人：" + log.UserID + "，操作姓名：" + remark.OP_NAME + "，操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "，所属系统：" + remark.BelongSystem + "，备注内容：" + remark.Remark;
                    lw.NewayWriteLog(log);
                    scope.Complete();
                }
                catch (ApplicationException ex)
                {
                    log.Memo = "新增运单备注失败，失败信息：" + ex.Message;
                    lw.NewayWriteLog(log);
                    throw;
                }
            }
        }
        /// <summary>
        /// 根据运单号查询运单的备注内容 并按备注的时间倒序排列
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AwbRemarkEntity> QueryAwbNoRemarkInfo(AwbRemarkEntity entity)
        {
            return man.QueryAwbNoRemarkInfo(entity);
        }
        /// <summary>
        /// 通过运单号和运单ID查询上传的回单照片数据列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AwbFilesEntity> QueryAwbFilesByAwbNo(AwbEntity entity)
        {
            return man.QueryAwbFilesByAwbNo(entity);
        }
        /// <summary>
        /// 查询运单跟踪状态信息
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public List<AwbRoadEntity> QueryAwbRoad(AwbRoadEntity ent)
        {
            return man.QueryAwbRoad(ent);
        }
        /// <summary>
        /// 根据运单查询货物跟踪信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AwbStatusTrackEntity> QueryAwbStatusTrack(AwbEntity entity)
        {
            return man.QueryAwbStatusTrack(entity);
        }
        #endregion
    }
}
