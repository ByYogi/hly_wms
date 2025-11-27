using House.Entity;
using House.Entity.Cargo;
using House.Entity.Dto.Purchase;
using House.Manager;
using House.Manager.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace House.Business.Cargo
{
    /// <summary>
    /// 采购订单操作逻辑类
    /// </summary>
    public class CargoRealFactoryPurchaseOrderBus
    {
        private CargoRealFactoryPurchaseOrderManager man = new CargoRealFactoryPurchaseOrderManager();
        #region 采购单管理操作方法集合
        /// <summary>
        /// 新增采购订单
        /// </summary>
        /// <param name="entity"></param>
        public void AddRealPurchaseOrder(CargoRealFactoryPurchaseOrderEntity entity, LogEntity log, CargoReserveParam Reserve =null)
        {
            LogWrite<CargoRealFactoryPurchaseOrderEntity> lw = new LogWrite<CargoRealFactoryPurchaseOrderEntity>();

            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.AddRealPurchaseOrder(entity, Reserve);

                    log.Memo = "";
                    lw.WriteLog(entity, log);
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

        public void UpdatePurchaseOrderApproval(CargoRealFactoryPurchaseOrderEntity entity, LogEntity log)
        {
            LogWrite<CargoRealFactoryPurchaseOrderEntity> lw = new LogWrite<CargoRealFactoryPurchaseOrderEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdatePurchaseOrderApproval(entity);
                    log.Memo = "修改采购单审批状态：采购ID：" + entity.PurOrderID.ToString() + ",审批状态：" + entity.ApplyStatus;
                    lw.WriteLog(entity, log);
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
        /// 获取每日最大的采购单号数据
        /// </summary>
        /// <returns></returns>
        public int GetMaxPurchaseOrderNum()
        {
            return man.GetMaxPurchaseOrderNum();
        }

        /// <summary>
        /// 查询采购订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CargoRealFactoryPurchaseOrderEntity QueryCargoRealFactoryPurchaseEntity(CargoRealFactoryPurchaseOrderEntity entity)
        {
            return man.QueryCargoRealFactoryPurchaseEntity(entity);
        }


        public void AddPurchaseOrderInfo(CargoRealFactoryPurchaseOrderEntity entity, List<CargoProductEntity> productList, List<CargoFactoryOrderEntity> factoryList, LogEntity log)
        {
            LogWrite<CargoOrderEntity> lw = new LogWrite<CargoOrderEntity>();
            CargoFactoryOrderManager facMan = new CargoFactoryOrderManager();
            CargoProductManager product = new CargoProductManager();
            CargoHouseManager house = new CargoHouseManager();
            try
            {
                facMan.SaveData(factoryList);
                if (entity.PurchaseInStoreType == "1")
                {
                    //使用事务流转单
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        foreach (var item in factoryList)
                        {
                            long ProductID = product.AddCargoProduct(new CargoProductEntity
                            {
                                ProductName = item.ProductName,
                                GoodsName = item.GoodsName,
                                TypeID = item.TypeID,
                                Model = item.Model,
                                GoodsCode = item.GoodsCode,
                                Specs = item.Specs,
                                Figure = item.Figure,
                                LoadIndex = item.LoadIndex,
                                SpeedLevel = item.SpeedLevel,
                                UnitPrice = Convert.ToDecimal(item.UnitPrice),
                                Numbers = item.OrderNum,
                                HubDiameter = item.HubDiameter,
                                FinalCostPrice = Convert.ToDecimal(item.UnitPrice),
                                CostPrice = Convert.ToDecimal(item.CostPrice),
                                TaxCostPrice = Convert.ToDecimal(item.CostPrice),
                                NoTaxCostPrice = Convert.ToDecimal(item.CostPrice),
                                //TradePrice = Convert.ToDecimal(item.TradePrice),
                                //SalePrice = Convert.ToDecimal(item.SalePrice),

                                TradePrice = Convert.ToDecimal(item.TradePrice),
                                SalePrice = Convert.ToDecimal(item.SalePrice),
                                InHousePrice = Convert.ToDecimal(item.InHousePrice),
                                NextDayPrice = Convert.ToDecimal(item.NextDayPrice),
                                WholesalePrice = Convert.ToDecimal(item.WholesalePrice),

                                Batch = item.Batch,
                                BatchWeek = item.BatchWeek,
                                BatchYear = item.BatchYear,
                                HouseID = item.HouseID,
                                Source = item.Source.ToString(),
                                SourceOrderNo = item.FacOrderNo,
                                BelongMonth = item.BelongMonth,
                                Born = item.Born,
                                Assort = item.Assort,
                                BelongDepart = item.BelongDepart,
                                Company = item.Company,
                                SpecsType = item.SpecsType,
                                OperaType = "0",
                                Package = item.ProductUnit,
                                PackageNum = item.BundleNum,
                                ProductCode = item.ProductCode,
                                Size = "",
                                Meridian = "",
                                Supplier = item.Supplier,
                                SuppClientNum = Convert.ToInt64(item.SuppClientNum),
                                SupplierAddress = item.SupplierAddress,
                                OwnerShip = item.OwnerShip,
                                GoodsClass = item.GoodsClass,
                            });

                            CargoContainerGoodsEntity containerGoodsEntity = new CargoContainerGoodsEntity();
                            containerGoodsEntity.ContainerID = item.ContainerID;
                            containerGoodsEntity.TypeID = item.TypeID;
                            containerGoodsEntity.ProductID = ProductID;
                            containerGoodsEntity.Piece = item.OrderNum;
                            containerGoodsEntity.Weight = 0;
                            containerGoodsEntity.IsPrintInCargo = false;
                            containerGoodsEntity.InHouseType = "0";
                            containerGoodsEntity.InCargoID = item.InCargoID;
                            house.AddContainerGoods(containerGoodsEntity);
                            //保存入库单表
                            house.AddInContainerGoods(containerGoodsEntity);
                            //修改货位上的产品件数
                            house.UpdateContainerInPiece(new CargoContainerEntity { ContainerID = item.ContainerID, InPiece = item.OrderNum, IsAdd = true });
                            //修改产品的入库状态 
                            product.UpdateCargoProductStatus(new CargoProductEntity { ProductID = ProductID, InCargoStatus = "1" });


                            log.Memo = "采购流转单产品入库，产品编码：" + item.ProductCode + "，数量：" + item.OrderNum + "，销售价：" + item.SalePrice + "，供应商名称：" + item.Supplier + "，供应商编码：" + item.SuppClientNum + "，产品ID：" + ProductID + "，产品类型ID：" + item.TypeID + "，仓库：" + item.HouseID + "，批次：" + item.Batch;
                            lw.WriteLog(log);
                        }
                        scope.Complete();
                    }
                }
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
        /// 采购单查询方法
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        public Hashtable QueryCargoRealPurchase(int pIndex, int pNum, CargoRealFactoryPurchaseOrderEntity entity)
        {
            return man.QueryCargoRealPurchase(pIndex, pNum, entity);
        }

        public List<CargoRealFactoryPurchaseHouseEntity> cargoRealFactoryPurchaseHouses(CargoRealFactoryPurchaseHouseEntity entity)
        {
            return man.cargoRealFactoryPurchaseHouses(entity);
        }

        /// <summary>
        /// 查询采购清单明细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoRealFactoryPurchaseOrderGoodsEntity> cargoRealFactoryPurchaseOrderGoods(CargoRealFactoryPurchaseOrderGoodsEntity entity)
        {
            return man.cargoRealFactoryPurchaseOrderGoods(entity);
        }
        public RealFactoryPODetailListDto QueryRealFactoryPODetail(RealFactoryPODetailParams entity)
        {
            return man.QueryRealFactoryPODetail(entity);
        }

        
        /// <summary>
        ///  查询采购清单明细 导出用
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoRealFactoryPurchaseOrderGoodsEntity> cargoRealFactoryPurchaseOrderGoodsExport(CargoRealFactoryPurchaseOrderGoodsEntity entity)
        {
            return man.cargoRealFactoryPurchaseOrderGoodsExport(entity);
        }
        public List<CargoRealFactoryPurchaseOrderGoodsEntity> cargoRealFactoryPurchaseOrderGoodsFactory(CargoRealFactoryPurchaseOrderGoodsEntity entity)
        {
            return man.cargoRealFactoryPurchaseOrderGoodsFactory(entity);
        }

        /// <summary>
        /// 修改到货时间和物流单号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateETRTimeLogisAwbNo(CargoRealFactoryPurchaseHouseEntity entity, LogEntity log)
        {
            LogWrite<CargoRealFactoryPurchaseHouseEntity> lw = new LogWrite<CargoRealFactoryPurchaseHouseEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdateETRTimeLogisAwbNo(entity);
                    //回写到“来货导入”的
                    man.UpdateFactoryOrder(entity);
                    log.Memo = "采购单ID：" + entity.PurOrderID.ToString() + ",到仓仓库:" + entity.HouseName + "物流单号：" + entity.LogisAwbNo + ",物流公司：" + entity.LogisID.ToString() + ",预计到货时间:" + entity.ETATime.ToString("yyyy-MM-dd HH:mm:ss");
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
        /// 删除采购明细
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void DelPurchaseGoods(List<CargoRealFactoryPurchaseOrderGoodsEntity> entity, LogEntity log)
        {
            LogWrite<CargoRealFactoryPurchaseHouseEntity> lw = new LogWrite<CargoRealFactoryPurchaseHouseEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    log.Memo = "删除采购单明细，";

                    long PurOrderID = 0; int ReplyNum = 0; int Piece = 0; decimal TransportFee = 0.00M;
                    string fpid = string.Empty;
                    foreach (var item in entity)
                    {
                        PurOrderID = item.PurOrderID;
                        man.DelPurchaseGoods(item);
                        log.Memo += "品牌：" + item.TypeName + "，规格：" + item.Specs + "，花纹：" + item.Figure + "，产品编码:" + item.ProductCode + ",采购数量：" + item.Piece.ToString() + ",回告数量:" + item.ReplyPiece.ToString() + "，采购价格:" + item.PurchasePrice.ToString();
                    }
                    List<CargoRealFactoryPurchaseOrderGoodsEntity> goodsEntities = man.QueryPurGoodsGroup(new CargoRealFactoryPurchaseOrderGoodsEntity { PurOrderID = PurOrderID });
                    if (goodsEntities.Count <= 0)
                    {
                        //删除House表和Order表
                        man.DeleteRealPurchaseOrder(new CargoRealFactoryPurchaseOrderEntity { PurOrderID = PurOrderID });
                        man.DeleteRealPurchaseHouseByPurOrderID(new CargoRealFactoryPurchaseHouseEntity { PurOrderID = PurOrderID });
                    }
                    else
                    {
                        foreach (var item in goodsEntities)
                        {
                            man.UpdatePurchaseHousePiece(new CargoRealFactoryPurchaseHouseEntity { PurOrderID = item.PurOrderID, FPID = item.FPID, OrderNum = item.Piece, ReplyNum = item.ReplyPiece });
                            Piece += item.Piece;
                            ReplyNum += item.ReplyPiece;
                            TransportFee += item.ReplyFee;
                            fpid += item.FPID.ToString() + ",";
                        }
                        if (!string.IsNullOrEmpty(fpid))
                        {
                            fpid = fpid.Substring(0, fpid.Length - 1);
                            man.DeleteRealPurchaseHouseData(fpid, PurOrderID);
                        }

                        man.UpdatePurchaseOrderPiece(new CargoRealFactoryPurchaseOrderEntity
                        {
                            PurOrderID = PurOrderID,
                            Piece = Piece,
                            ReplyNum = ReplyNum,
                            TransportFee = TransportFee,
                        });
                    }



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
        /// 修改采购单明细
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void UpdatePurchaseReplyNum(CargoRealFactoryPurchaseOrderGoodsEntity entity, LogEntity log)
        {
            LogWrite<CargoRealFactoryPurchaseHouseEntity> lw = new LogWrite<CargoRealFactoryPurchaseHouseEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdatePurchaseReplyNum(entity);
                    long PurOrderID = entity.PurOrderID; int ReplyNum = 0; int Piece = 0; decimal TransportFee = 0.00M;
                    List<CargoRealFactoryPurchaseOrderGoodsEntity> goodsEntities = man.QueryPurGoodsGroup(new CargoRealFactoryPurchaseOrderGoodsEntity { PurOrderID = PurOrderID });
                    foreach (var item in goodsEntities)
                    {
                        man.UpdatePurchaseHousePiece(new CargoRealFactoryPurchaseHouseEntity { PurOrderID = item.PurOrderID, FPID = item.FPID, OrderNum = item.Piece, ReplyNum = item.ReplyPiece });
                        Piece += item.Piece;
                        ReplyNum += item.ReplyPiece;
                        TransportFee += item.ReplyFee;
                    }
                    man.UpdatePurchaseOrderPiece(new CargoRealFactoryPurchaseOrderEntity
                    {
                        PurOrderID = PurOrderID,
                        Piece = Piece,
                        ReplyNum = ReplyNum,
                        TransportFee = TransportFee,
                    });
                    log.Memo = "修改采购单明细，GoodsID：" + entity.GoodsID.ToString() + "，采购ID：" + entity.PurOrderID.ToString();
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
        /// 删除采购单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void DelPurchaseOrder(List<CargoRealFactoryPurchaseOrderEntity> entity, LogEntity log)
        {
            LogWrite<CargoRealFactoryPurchaseHouseEntity> lw = new LogWrite<CargoRealFactoryPurchaseHouseEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    log.Memo = "删除采购单，";
                    foreach (var item in entity)
                    {
                        man.DeleteRealPurchaseOrder(item);
                        man.DeleteRealPurchaseHouseByPurOrderID(new CargoRealFactoryPurchaseHouseEntity { PurOrderID = item.PurOrderID });
                        man.DelPurchaseGoodsByPurOrderID(new CargoRealFactoryPurchaseOrderGoodsEntity { PurOrderID = item.PurOrderID });
                        log.Memo += "采购单号：" + item.PurOrderNo;
                    }

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
        /// 更改转账方式
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="log"></param>
        public void UpdatePurchaseOrderTransferType(CargoRealFactoryPurchaseOrderEntity entity, LogEntity log)
        {
            LogWrite<CargoRealFactoryPurchaseOrderEntity> lw = new LogWrite<CargoRealFactoryPurchaseOrderEntity>();

            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.UpdatePurchaseOrderTransferType(entity);

                    log.Memo = "";
                    lw.WriteLog(entity, log);
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
        /// 查询采购单与来货导入表相关数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoFactoryOrderEntity> QueryCargoRealFactoryData(CargoRealFactoryPurchaseOrderEntity entity)
        {
            return man.QueryCargoRealFactoryData(entity);
        }
        #endregion

        #region 采购账单管理操作方法集合
        public void AddPurchaseBillAccount(CargoRealPurchaseAccountEntity entity, LogEntity log)
        {
            LogWrite<CargoClientAccountEntity> lw = new LogWrite<CargoClientAccountEntity>();
            //使用事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    man.AddPurchaseBillAccount(entity);

                    foreach (var it in entity.goodsList)
                    {
                        //新增明细
                        man.AddPurchaseBillAccountGoods(it, entity.AccountNO);

                        //更改采购订单信息
                        man.UpdatePurchaseOrderApproval(new CargoRealFactoryPurchaseOrderEntity { ApplyStatus="3",CheckStatus = "2",PurOrderID= it.PurOrderID });
                    }

                    log.Memo = "新增采购账单,账单号:" + entity.AccountID + ",账单名称:" + entity.AccountTitle;
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
        #endregion
    }
}
