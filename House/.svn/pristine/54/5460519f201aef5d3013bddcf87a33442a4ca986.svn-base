using House.DataAccess;
using House.Entity.Cargo;
using House.Entity.House;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace House.Manager.Cargo
{
    public class ArriveManager
    {
        SqlHelper NewaySql = new SqlHelper("Neway");
        #region 托运合同录入

        public List<ClientSessionEntity> QueryClientSession(ClientEntity entity)
        {
            List<ClientSessionEntity> result = new List<ClientSessionEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT * FROM Tbl_Client Where DelFlag=@DelFlag and BelongSystem=@BelongSystem ";

                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(command, "@DelFlag", DbType.String, entity.DelFlag);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new ClientSessionEntity
                            {
                                ClientID = Convert.ToInt32(idr["ClientID"]),
                                ClientName = Convert.ToString(idr["ClientName"]).Trim(),
                                ClientShortName = Convert.ToString(idr["ClientShortName"]).Trim(),
                                Boss = Convert.ToString(idr["Boss"]).Trim(),
                                Telephone = Convert.ToString(idr["Telephone"]).Trim(),
                                Cellphone = Convert.ToString(idr["Cellphone"]).Trim(),
                                Address = Convert.ToString(idr["Address"]).Trim(),
                                ClientNum = Convert.ToString(idr["ClientNum"]).Trim()
                                //AcceptAddress = listAcceptAddress
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 根据入库员代码查询用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SystemUserEntity GetUserNameByClerkNo(SystemUserEntity entity)
        {
            SystemUserEntity result = new SystemUserEntity();
            try
            {
                string strSQL = @"select LoginName,UserName from Tbl_SysUser Where ClerkNo=@ClerkNo and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@ClerkNo", DbType.String, entity.ClerkNo);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.NewLandBelongSystem);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.UserName = Convert.ToString(idr["UserName"]).Trim();
                            result.LoginName = Convert.ToString(idr["LoginName"]).Trim();
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询好来运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<HlyExchangeEntity> QueryHlyAwbInfo(HlyExchangeEntity entity)
        {
            List<HlyExchangeEntity> result = new List<HlyExchangeEntity>();
            try
            {
                string strSQL = @"SELECT * FROM Tbl_HLY_AwbInfo Where (1=1) ";
                if (!string.IsNullOrEmpty(entity.SaveStatus)) { strSQL += " and SaveStatus='" + entity.SaveStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.HlyFiveNo)) { strSQL += " and HlyFiveNo='" + entity.HlyFiveNo + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " order by SaveStatus asc ,OP_DATE desc ";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            result.Add(new HlyExchangeEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                HlyAwbNo = Convert.ToString(idr["HlyAwbNo"]).Trim(),
                                HlyFiveNo = Convert.ToString(idr["HlyFiveNo"]).Trim(),
                                TagCodeNo = Convert.ToString(idr["TagCodeNo"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                SaveStatus = Convert.ToString(idr["SaveStatus"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                Goods = Convert.ToString(idr["Goods"]).Trim()
                            });
                            #endregion
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 通过客户编号查询客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientEntity GetClientByID(int Clientid)
        {
            ClientEntity result = new ClientEntity();
            try
            {
                string strSQL = @"SELECT * from Tbl_Client Where ClientID=@ClientID";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@ClientID", DbType.Int32, Clientid);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new ClientEntity
                            {
                                ClientID = Convert.ToInt32(idr["ClientID"]),
                                ClientName = Convert.ToString(idr["ClientName"]).Trim(),
                                ClientShortName = Convert.ToString(idr["ClientShortName"]).Trim(),
                                Address = Convert.ToString(idr["Address"]).Trim(),
                                ZipCode = Convert.ToString(idr["ZipCode"]).Trim(),
                                Telephone = Convert.ToString(idr["Telephone"]),
                                Fax = Convert.ToString(idr["Fax"]).Trim(),
                                Boss = Convert.ToString(idr["Boss"]),
                                Position = Convert.ToString(idr["Position"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                Email = Convert.ToString(idr["Email"]).Trim(),
                                Invoice = Convert.ToString(idr["Invoice"]).Trim(),
                                TaxNum = Convert.ToString(idr["TaxNum"]).Trim(),
                                Bank = Convert.ToString(idr["Bank"]).Trim(),
                                BankAccount = Convert.ToString(idr["BankAccount"]).Trim(),
                                Business = Convert.ToString(idr["BankAccount"]).Trim(),
                                Product = Convert.ToString(idr["Product"]).Trim(),
                                CompanyRemark = Convert.ToString(idr["CompanyRemark"]).Trim(),
                                CompanyScope = Convert.ToInt32(idr["CompanyScope"]),
                                CompanyType = Convert.ToString(idr["CompanyType"]).Trim(),
                                LastModifyDate = string.IsNullOrEmpty(Convert.ToString(idr["LastModifyDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["LastModifyDate"]),
                                StartDate = string.IsNullOrEmpty(Convert.ToString(idr["StartDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["StartDate"]),
                                EndDate = string.IsNullOrEmpty(Convert.ToString(idr["EndDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["EndDate"]),
                                BelongDot = Convert.ToString(idr["BelongDot"]).Trim(),
                                ClientNum = Convert.ToString(idr["ClientNum"]).Trim(),
                                ReceiveDot = Convert.ToString(idr["ReceiveDot"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                ClientType = Convert.ToString(idr["ClientType"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            };
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询该客户下的所有收货地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<ClientAcceptAddressEntity> QueryClientAcceptAddress(ClientAcceptAddressEntity entity)
        {
            List<ClientAcceptAddressEntity> result = new List<ClientAcceptAddressEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT * FROM Tbl_ClientAcceptAddress Where 1=1 ";
                if (!entity.ClientID.Equals(0))
                {
                    strSQL += " and ClientID=@ClientID";
                }
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strSQL += " and AcceptPeople like '%" + entity.AcceptPeople + "%'";
                }
                if (!string.IsNullOrEmpty(entity.AcceptCity))
                {
                    strSQL += " and AcceptCity like '%" + entity.AcceptCity + "%'";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    if (!entity.ClientID.Equals(0))
                    {
                        NewaySql.AddInParameter(cmd, "@ClientID", DbType.Int32, entity.ClientID);
                    }
                    if (!string.IsNullOrEmpty(entity.AcceptCity))
                    {
                        NewaySql.AddInParameter(cmd, "@AcceptCity", DbType.String, entity.AcceptCity);
                    }
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new ClientAcceptAddressEntity
                            {
                                AID = Convert.ToInt64(idrCount["AID"]),
                                AcceptCompany = Convert.ToString(idrCount["AcceptCompany"]).Trim(),
                                ClientID = Convert.ToInt32(idrCount["ClientID"]),
                                AcceptCity = Convert.ToString(idrCount["AcceptCity"]).Trim(),
                                AcceptAddress = Convert.ToString(idrCount["AcceptAddress"]).Trim(),
                                AcceptCellphone = Convert.ToString(idrCount["AcceptCellphone"]).Trim(),
                                AcceptPeople = Convert.ToString(idrCount["AcceptPeople"]).Trim(),
                                AcceptTelephone = Convert.ToString(idrCount["AcceptTelephone"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idrCount["OP_DATE"])
                            });
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 判断运单号是否在所允许的区间内
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public bool IsRangeAwbNo(AwbEntity ent)
        {
            bool result = false;
            try
            {
                string strSQL = @"select b.StartAwbNo,b.EndAwbNo,b.CName, a.UserID from Tbl_SysUser as a left join Tbl_SysDepart as b on a.DepID=b.DepID where a.LoginName=@LoginName and b.StartAwbNo<=@AwbNo and b.EndAwbNo>=@AwbNo and a.BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, ent.BelongSystem.Trim());
                    NewaySql.AddInParameter(cmdAdd, "@LoginName", DbType.String, ent.OP_ID.Trim());
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.Int32, Convert.ToInt32(ent.AwbNo.Trim()));

                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 判断运单号是否存在
        /// True：存在
        /// False：不存在
        /// </summary>
        /// <param name="awb"></param>
        /// <returns></returns>
        public bool IsExistAwb(AwbEntity awb)
        {
            bool result = false;
            try
            {
                awb.EnSafe();
                string strSQL = @"SELECT Count(*) as AwbCount FROM Tbl_Awb_Basic WHERE AwbNo=@AwbNo and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awb.AwbNo.ToUpper().Trim());
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem.ToUpper().Trim());
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["AwbCount"]) > 0) { result = true; }
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增运单信息
        /// </summary>
        /// <param name="awb"></param>
        public void AddAwbInfo(AwbEntity awb)
        {
            try
            {
                awb.EnSafe();
                Int64 did = 0;
                #region 新增的SQL语句
                string strSQL = @"INSERT INTO Tbl_Awb_Basic(AwbNo,Dep,Dest,Transit,Piece,Weight,Volume,AwbPiece,AwbWeight,AwbVolume,Attach,InsuranceFee,TransitFee,TransportFee,DeliverFee,OtherFee,TotalCharge,Rebate,NowPay,PickPay,CheckOutType,CollectMoney,ReturnAwb,TrafficType,TimeLimit,DeliveryType,SteveDore,ShipperName,ShipperUnit,ShipperAddress,ShipperTelephone,ShipperCellphone,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,HandleTime,Remark,DelFlag,CreateAwb,CreateDate,OP_ID,HAwbNo,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,ClientNum,AwbStatus,ClerkNo,ClerkName,BelongSystem,HLY) VALUES (@AwbNo,@Dep,@Dest,@Transit,@Piece,@Weight,@Volume,@AwbPiece,@AwbWeight,@AwbVolume,@Attach,@InsuranceFee,@TransitFee,@TransportFee,@DeliverFee,@OtherFee,@TotalCharge,@Rebate,@NowPay,@PickPay,@CheckOutType,@CollectMoney,@ReturnAwb,@TrafficType,@TimeLimit,@DeliveryType,@SteveDore,@ShipperName,@ShipperUnit,@ShipperAddress,@ShipperTelephone,@ShipperCellphone,@AcceptUnit,@AcceptAddress,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@HandleTime,@Remark,@DelFlag,@CreateAwb,@CreateDate,@OP_ID,@HAwbNo,@FinanceFirstCheck,@FirstCheckName,@FirstCheckDate,@FinanceSecondCheck,@SecondCheckName,@SecondCheckDate,@ClientNum,@AwbStatus,@ClerkNo,@ClerkName,@BelongSystem,@HLY) SELECT @@IDENTITY";
                #endregion
                #region 新增实现
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awb.AwbNo.ToUpper());
                    NewaySql.AddInParameter(cmdAdd, "@Dep", DbType.String, awb.Dep);
                    NewaySql.AddInParameter(cmdAdd, "@Dest", DbType.String, awb.Dest);
                    NewaySql.AddInParameter(cmdAdd, "@Transit", DbType.String, string.IsNullOrEmpty(Convert.ToString(awb.Transit).Trim()) ? awb.Dest : awb.Transit.Trim());
                    NewaySql.AddInParameter(cmdAdd, "@Piece", DbType.Int32, awb.Piece);
                    NewaySql.AddInParameter(cmdAdd, "@Weight", DbType.Decimal, awb.Weight);
                    NewaySql.AddInParameter(cmdAdd, "@Volume", DbType.Decimal, awb.Volume);
                    NewaySql.AddInParameter(cmdAdd, "@AwbPiece", DbType.Int32, awb.Piece);
                    NewaySql.AddInParameter(cmdAdd, "@AwbWeight", DbType.Decimal, awb.Weight);
                    NewaySql.AddInParameter(cmdAdd, "@AwbVolume", DbType.Decimal, awb.Volume);
                    NewaySql.AddInParameter(cmdAdd, "@Attach", DbType.Int32, awb.Attach);
                    NewaySql.AddInParameter(cmdAdd, "@InsuranceFee", DbType.Decimal, awb.InsuranceFee);
                    NewaySql.AddInParameter(cmdAdd, "@TransitFee", DbType.Decimal, awb.TransitFee);
                    NewaySql.AddInParameter(cmdAdd, "@TransportFee", DbType.Decimal, awb.TransportFee);
                    NewaySql.AddInParameter(cmdAdd, "@DeliverFee", DbType.Decimal, awb.DeliverFee);
                    NewaySql.AddInParameter(cmdAdd, "@OtherFee", DbType.Decimal, awb.OtherFee);
                    NewaySql.AddInParameter(cmdAdd, "@TotalCharge", DbType.Decimal, awb.TotalCharge);
                    NewaySql.AddInParameter(cmdAdd, "@Rebate", DbType.Decimal, awb.Rebate);
                    NewaySql.AddInParameter(cmdAdd, "@NowPay", DbType.Decimal, awb.NowPay);
                    NewaySql.AddInParameter(cmdAdd, "@PickPay", DbType.Decimal, awb.PickPay);
                    NewaySql.AddInParameter(cmdAdd, "@CheckOutType", DbType.String, awb.CheckOutType);
                    NewaySql.AddInParameter(cmdAdd, "@CollectMoney", DbType.Decimal, awb.CollectMoney);
                    NewaySql.AddInParameter(cmdAdd, "@ReturnAwb", DbType.Int32, awb.ReturnAwb);
                    NewaySql.AddInParameter(cmdAdd, "@TrafficType", DbType.String, awb.TrafficType);
                    NewaySql.AddInParameter(cmdAdd, "@TimeLimit", DbType.Int32, awb.TimeLimit);
                    NewaySql.AddInParameter(cmdAdd, "@DeliveryType", DbType.String, awb.DeliveryType);
                    NewaySql.AddInParameter(cmdAdd, "@SteveDore", DbType.String, awb.SteveDore);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperName", DbType.String, awb.ShipperName);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperUnit", DbType.String, awb.ShipperUnit);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperAddress", DbType.String, awb.ShipperAddress);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperTelephone", DbType.String, awb.ShipperTelephone);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperCellphone", DbType.String, awb.ShipperCellphone);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptUnit", DbType.String, awb.AcceptUnit);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptAddress", DbType.String, awb.AcceptAddress);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptPeople", DbType.String, awb.AcceptPeople);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptTelephone", DbType.String, awb.AcceptTelephone);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptCellphone", DbType.String, awb.AcceptCellphone);
                    NewaySql.AddInParameter(cmdAdd, "@HandleTime", DbType.DateTime, awb.HandleTime);
                    NewaySql.AddInParameter(cmdAdd, "@Remark", DbType.String, awb.Remark);
                    NewaySql.AddInParameter(cmdAdd, "@DelFlag", DbType.String, awb.DelFlag);
                    NewaySql.AddInParameter(cmdAdd, "@AwbStatus", DbType.String, awb.AwbStatus);
                    NewaySql.AddInParameter(cmdAdd, "@CreateAwb", DbType.String, awb.CreateAwb);
                    NewaySql.AddInParameter(cmdAdd, "@CreateDate", DbType.DateTime, DateTime.Now);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, awb.OP_ID);
                    NewaySql.AddInParameter(cmdAdd, "@HAwbNo", DbType.String, awb.HAwbNo);
                    NewaySql.AddInParameter(cmdAdd, "@FinanceFirstCheck", DbType.String, awb.FinanceFirstCheck);
                    NewaySql.AddInParameter(cmdAdd, "@FirstCheckName", DbType.String, awb.FirstCheckName);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem);
                    if (awb.CheckOutType.Equals("0"))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DateTime.Now);
                        NewaySql.AddInParameter(cmdAdd, "@SecondCheckDate", DbType.DateTime, DateTime.Now);
                    }
                    else
                    {
                        NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DBNull.Value);
                        NewaySql.AddInParameter(cmdAdd, "@SecondCheckDate", DbType.DateTime, DBNull.Value);
                    }
                    NewaySql.AddInParameter(cmdAdd, "@SecondCheckName", DbType.String, awb.SecondCheckName);
                    NewaySql.AddInParameter(cmdAdd, "@FinanceSecondCheck", DbType.String, awb.FinanceSecondCheck);
                    NewaySql.AddInParameter(cmdAdd, "@ClientNum", DbType.String, awb.ClientNum);
                    NewaySql.AddInParameter(cmdAdd, "@ClerkName", DbType.String, awb.ClerkName);
                    NewaySql.AddInParameter(cmdAdd, "@ClerkNo", DbType.String, awb.ClerkNo);
                    NewaySql.AddInParameter(cmdAdd, "@HLY", DbType.String, awb.HLY);

                    //NewaySql.ExecuteNonQuery(cmdAdd);
                    did = Convert.ToInt64(NewaySql.ExecuteScalar(cmdAdd));

                }
                #endregion
                #region 2017-04-13 添加副单号逻辑
                if (!string.IsNullOrEmpty(awb.HAwbNo))
                {
                    string[] hw = awb.HAwbNo.Split(',');//拆分
                    List<HAwbNoEntity> hwList = new List<HAwbNoEntity>();
                    foreach (var it in hw)
                    {
                        if (!string.IsNullOrEmpty(it))
                        {
                            hwList.Add(new HAwbNoEntity { AwbNo = awb.AwbNo, HAwbNo = it, BelongSystem = awb.BelongSystem, OP_ID = awb.OP_ID });
                        }
                        AddHAwbNo(hwList);
                    }
                }
                #endregion
                //保存运单的货物品名数据
                AddAwbGoodsInfo(awb.AwbGoods);
                //判断托运人是否存在，如果不存在就添加进去，存在就过
                ClientEntity ce = new ClientEntity();
                ce.ClientShortName = ce.ClientName = awb.ShipperUnit;
                ce.Boss = awb.ShipperName;
                ce.Telephone = awb.ShipperTelephone;
                ce.Cellphone = awb.ShipperCellphone;
                ce.DelFlag = "0";
                ce.OP_ID = awb.OP_ID;
                ce.BelongDot = awb.Dep;
                ce.BelongSystem = awb.BelongSystem;
                if (awb.CheckOutType.Equals("0") || awb.CheckOutType.Equals("1"))//现付和回单
                {
                    ce.ReceiveDot = awb.Dep;
                }
                if (awb.CheckOutType.Equals("3"))//到付
                {
                    ce.ReceiveDot = awb.Dest;
                }
                int cID = 0;
                if (!IsExistClient(ce, ref cID))
                {
                    cID = AddClientReturnID(ce);
                }
                //判断收货人是否存在，如果不存在就添加进去，存在就过
                ClientAcceptAddressEntity caae = new ClientAcceptAddressEntity
                {
                    ClientID = cID,
                    AcceptCity = awb.Dest,
                    AcceptCompany = awb.AcceptUnit,
                    AcceptAddress = awb.AcceptAddress,
                    AcceptPeople = awb.AcceptPeople,
                    AcceptCellphone = awb.AcceptCellphone,
                    AcceptTelephone = awb.AcceptTelephone,
                    BelongSystem = awb.BelongSystem,
                    OP_ID = awb.OP_ID
                };
                if (!IsExistClientAcceptAddress(caae))
                {
                    SaveClientAcceptAddress(caae);
                }
                //运单状态
                InsertAwbStatus(new AwbStatus
                {
                    AwbID = did,
                    AwbNo = awb.AwbNo,
                    TruckFlag = "0",
                    CurrentLocation = awb.Dep,
                    LastHour = 0,
                    DetailInfo = "",
                    BelongSystem = awb.BelongSystem,
                    OP_ID = awb.OP_ID
                });
                //如果是现付并且存在代收款就保存数据到代收款审核表
                if (awb.CheckOutType.Equals("0") && awb.CollectMoney > 0)
                {
                    AddCollectAccount(new AwbEntity
                    {
                        AwbID = did,
                        AwbNo = awb.AwbNo,
                        ShouFlag = awb.ShouFlag,
                        ShouCheckName = awb.ShouCheckName,
                        ShouCheckDate = DateTime.Now,
                        BelongSystem = awb.BelongSystem,
                        AKind = "0"
                    });
                }
                //如果出发站和到达站相同，则直接到达状态
                if (awb.Dep.Equals(awb.Dest))
                {
                    //运单状态
                    InsertAwbStatus(new AwbStatus
                    {
                        AwbID = did,
                        AwbNo = awb.AwbNo,
                        TruckFlag = "3",
                        CurrentLocation = awb.Dep,
                        ArriveTime = DateTime.Now,
                        LastHour = 0,
                        DetailInfo = "",
                        BelongSystem = awb.BelongSystem,
                        OP_ID = awb.OP_ID
                    });
                    awb.AwbID = did;
                    AddArriveAwbInfo(awb);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 保存运单和副单号数据
        /// </summary>
        /// <param name="goods"></param>
        public void AddHAwbNo(List<HAwbNoEntity> Hawb)
        {
            try
            {
                foreach (var it in Hawb)
                {
                    it.EnSafe();
                    //判断如果存在运单号和副单号就跳过
                    if (IsExistHAwbNo(it)) { continue; }
                    string strSQL = @"INSERT INTO Tbl_Awb_HAwbNo(AwbNo ,HAwbNo ,OP_ID ,BelongSystem ) VALUES  (@AwbNo ,@HAwbNo ,@OP_ID ,@BelongSystem)";
                    using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                    {
                        NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, it.AwbNo.ToUpper().Trim());
                        NewaySql.AddInParameter(cmd, "@HAwbNo", DbType.String, it.HAwbNo.ToUpper().Trim());
                        NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, it.OP_ID);
                        NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, it.BelongSystem);
                        NewaySql.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 判断运单和副单号是否存在
        /// True：存在
        /// False：不存在
        /// </summary>
        /// <param name="awb"></param>
        /// <returns></returns>
        public bool IsExistHAwbNo(HAwbNoEntity Hawb)
        {
            bool result = false;
            try
            {
                Hawb.EnSafe();
                string strSQL = @"SELECT Count(*) as AwbCount FROM Tbl_Awb_HAwbNo WHERE AwbNo=@AwbNo and HAwbNo=@HAwbNo and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, Hawb.AwbNo.ToUpper().Trim());
                    NewaySql.AddInParameter(cmdAdd, "@HAwbNo", DbType.String, Hawb.HAwbNo.ToUpper().Trim());
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, Hawb.BelongSystem.ToUpper().Trim());
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["AwbCount"]) > 0) { result = true; }
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增运单的货物品名数据
        /// </summary>
        /// <param name="goods"></param>
        public void AddAwbGoodsInfo(List<AwbGoodsEntity> goods)
        {
            try
            {
                foreach (var it in goods)
                {
                    it.EnSafe();

                    string strSQL = @"INSERT INTO Tbl_Awb_Goods( AwbNo ,Package ,Goods ,Piece ,PiecePrice,Weight ,WeightPrice ,Volume ,VolumePrice,DeclareValue,OP_ID ,BelongSystem,ProductName,Model,GoodsCode,Specs,Figure,LoadIndex,SpeedLevel,Batch,ContainerCode,TypeName ) VALUES  (@AwbNo ,@Package ,@Goods ,@Piece ,@PiecePrice,@Weight ,@WeightPrice ,@Volume ,@VolumePrice,@DeclareValue,@OP_ID ,@BelongSystem,@ProductName,@Model,@GoodsCode,@Specs,@Figure,@LoadIndex,@SpeedLevel,@Batch,@ContainerCode,@TypeName)";
                    using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                    {
                        NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, it.AwbNo.ToUpper().Trim());
                        NewaySql.AddInParameter(cmd, "@Package", DbType.String, it.Package);
                        NewaySql.AddInParameter(cmd, "@Goods", DbType.String, it.Goods);
                        NewaySql.AddInParameter(cmd, "@Piece", DbType.Int32, it.Piece);
                        NewaySql.AddInParameter(cmd, "@PiecePrice", DbType.Decimal, it.PiecePrice);
                        NewaySql.AddInParameter(cmd, "@Weight", DbType.Decimal, it.Weight);
                        NewaySql.AddInParameter(cmd, "@WeightPrice", DbType.Decimal, it.WeightPrice);
                        NewaySql.AddInParameter(cmd, "@Volume", DbType.Decimal, it.Volume);
                        NewaySql.AddInParameter(cmd, "@VolumePrice", DbType.Decimal, it.VolumePrice);
                        NewaySql.AddInParameter(cmd, "@DeclareValue", DbType.String, it.DeclareValue);
                        NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, it.OP_ID);
                        NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, it.BelongSystem);
                        NewaySql.AddInParameter(cmd, "@ProductName", DbType.String, it.ProductName);
                        NewaySql.AddInParameter(cmd, "@Model", DbType.String, it.Model);
                        NewaySql.AddInParameter(cmd, "@GoodsCode", DbType.String, it.GoodsCode);
                        NewaySql.AddInParameter(cmd, "@Specs", DbType.String, it.Specs);
                        NewaySql.AddInParameter(cmd, "@Figure", DbType.String, it.Figure);
                        NewaySql.AddInParameter(cmd, "@LoadIndex", DbType.Int32, it.LoadIndex);
                        NewaySql.AddInParameter(cmd, "@SpeedLevel", DbType.String, it.SpeedLevel);
                        NewaySql.AddInParameter(cmd, "@Batch", DbType.String, it.Batch);
                        NewaySql.AddInParameter(cmd, "@ContainerCode", DbType.String, it.ContainerCode);
                        NewaySql.AddInParameter(cmd, "@TypeName", DbType.String, it.TypeName);
                        NewaySql.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 通过客户编号和客户名称查询客户信息 True 存在 False 不存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExistClient(ClientEntity entity, ref int CID)
        {
            bool result = false;
            try
            {
                string strSQL = @"SELECT ClientID from Tbl_Client Where (1=1) ";
                strSQL += " and BelongSystem=@BelongSystem";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strSQL += " and DelFlag = '" + entity.DelFlag + "'";
                }
                if (!string.IsNullOrEmpty(entity.ClientName))
                {
                    strSQL += " and ClientName=@ClientName ";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    if (!string.IsNullOrEmpty(entity.DelFlag))
                    {
                        NewaySql.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    }
                    if (!string.IsNullOrEmpty(entity.ClientName))
                    {
                        NewaySql.AddInParameter(cmd, "@ClientName", DbType.String, entity.ClientName);
                    }
                    using (DbDataReader idr = NewaySql.ExecuteReader(cmd))
                    {
                        while (idr.Read())
                        {
                            result = true;
                            CID = Convert.ToInt32(idr["ClientID"]);
                            break;
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增客户数据
        /// </summary>
        /// <param name="entity"></param>
        public int AddClientReturnID(ClientEntity entity)
        {
            Random rd = new Random();
            //采用递归算法 ，直到找到表中不存在的客户编号
            int crd = GetNotExistClientID(rd.Next(1000, 99999999));
            string strSQL = @"INSERT INTO Tbl_Client( ClientID ,ClientName ,ClientShortName ,Address ,ZipCode ,Telephone ,Fax ,Boss ,Position ,Cellphone ,Email ,Invoice ,TaxNum ,Bank ,BankAccount ,Business ,Product ,CompanyType ,CompanyScope ,CompanyRemark ,LastModifyDate ,DelFlag,ClientType,OP_ID,BelongDot,ReceiveDot,ClientNum,BelongSystem) VALUES  (@ClientID ,@ClientName ,@ClientShortName ,@Address ,@ZipCode ,@Telephone ,@Fax ,@Boss ,@Position ,@Cellphone ,@Email ,@Invoice ,@TaxNum ,@Bank ,@BankAccount ,@Business ,@Product ,@CompanyType ,@CompanyScope ,@CompanyRemark ,@LastModifyDate ,@DelFlag,@ClientType,@OP_ID,@BelongDot,@ReceiveDot,@ClientNum,@BelongSystem)";
            try
            {
                entity.EnSafe();
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@ClientID", DbType.Int32, crd);
                    NewaySql.AddInParameter(cmd, "@ClientName", DbType.String, entity.ClientName);
                    NewaySql.AddInParameter(cmd, "@ClientShortName", DbType.String, entity.ClientShortName);
                    NewaySql.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    NewaySql.AddInParameter(cmd, "@ZipCode", DbType.String, entity.ZipCode);
                    NewaySql.AddInParameter(cmd, "@Telephone", DbType.String, entity.Telephone);
                    NewaySql.AddInParameter(cmd, "@Fax", DbType.String, entity.Fax);
                    NewaySql.AddInParameter(cmd, "@Boss", DbType.String, entity.Boss);
                    NewaySql.AddInParameter(cmd, "@Position", DbType.String, entity.Position);
                    NewaySql.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    NewaySql.AddInParameter(cmd, "@Email", DbType.String, entity.Email);
                    NewaySql.AddInParameter(cmd, "@Invoice", DbType.String, entity.Invoice);
                    NewaySql.AddInParameter(cmd, "@TaxNum", DbType.String, entity.TaxNum);
                    NewaySql.AddInParameter(cmd, "@Bank", DbType.String, entity.Bank);
                    NewaySql.AddInParameter(cmd, "@BankAccount", DbType.String, entity.BankAccount);
                    NewaySql.AddInParameter(cmd, "@Business", DbType.String, entity.Business);
                    NewaySql.AddInParameter(cmd, "@Product", DbType.String, entity.Product);
                    NewaySql.AddInParameter(cmd, "@CompanyType", DbType.String, entity.CompanyType);
                    NewaySql.AddInParameter(cmd, "@CompanyScope", DbType.Int32, entity.CompanyScope);
                    NewaySql.AddInParameter(cmd, "@CompanyRemark", DbType.String, entity.CompanyRemark);
                    NewaySql.AddInParameter(cmd, "@LastModifyDate", DbType.DateTime, DateTime.Now);
                    NewaySql.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    NewaySql.AddInParameter(cmd, "@ClientType", DbType.String, entity.ClientType);
                    NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.AddInParameter(cmd, "@BelongDot", DbType.String, entity.BelongDot);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(cmd, "@ReceiveDot", DbType.String, entity.ReceiveDot);
                    NewaySql.AddInParameter(cmd, "@ClientNum", DbType.String, entity.ClientType.Trim().Equals("1") ? entity.ClientNum : "");
                    NewaySql.ExecuteNonQuery(cmd);
                }
                return crd;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 递归得到数据库中不存在的客户编号
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int GetNotExistClientID(int cid)
        {
            Random rd = new Random();
            int crd = cid;
            if (!string.IsNullOrEmpty(GetClientByID(cid).ClientName))
            {
                crd = GetNotExistClientID(rd.Next(1000, 99999999));
            }
            return crd;
        }
        /// <summary>
        /// 通过客户编号和收货单位名称查询客户的收货地址信息 True 存在 False 不存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExistClientAcceptAddress(ClientAcceptAddressEntity entity)
        {
            bool result = false;
            try
            {
                string strSQL = @"SELECT * from Tbl_ClientAcceptAddress Where ClientID=@ClientID ";

                if (!string.IsNullOrEmpty(entity.AcceptCompany))
                {
                    strSQL += " and AcceptCompany=@AcceptCompany ";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@ClientID", DbType.Int32, entity.ClientID);
                    if (!string.IsNullOrEmpty(entity.AcceptCompany))
                    {
                        NewaySql.AddInParameter(cmd, "@AcceptCompany", DbType.String, entity.AcceptCompany);
                    }
                    using (DbDataReader idr = NewaySql.ExecuteReader(cmd))
                    {
                        while (idr.Read())
                        {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 新增和修改客户的收货地址
        /// </summary>
        /// <param name="entity"></param>
        public void SaveClientAcceptAddress(ClientAcceptAddressEntity entity)
        {
            try
            {
                string strAdd = @"INSERT INTO Tbl_ClientAcceptAddress( ClientID ,AcceptCity ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone,AcceptCompany,OP_ID,BelongSystem) VALUES  ( @ClientID ,@AcceptCity , @AcceptAddress ,@AcceptPeople ,@AcceptTelephone ,@AcceptCellphone,@AcceptCompany,@OP_ID,@BelongSystem)";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strAdd))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ClientID", DbType.Int32, entity.ClientID);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptCity", DbType.String, entity.AcceptCity);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptCompany", DbType.String, entity.AcceptCompany);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 保存代收款审核表
        /// </summary>
        /// <param name="entity"></param>
        public void AddCollectAccount(AwbEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_CollectAccount(AwbID,AwbNo,ShouFlag,AKind,ShouCheckName,ShouCheckDate,FuFlag,FuCheckName,FuCheckDate,BelongSystem) VALUES (@AwbID,@AwbNo,@ShouFlag,@AKind,@ShouCheckName,@ShouCheckDate,@FuFlag,@FuCheckName,@FuCheckDate,@BelongSystem)";
            //判断是否存在，如果存在则直接跳出
            if (IsExistCollectAwb(entity))
            {
                strSQL = @"UPDATE Tbl_CollectAccount set ShouFlag=@ShouFlag,ShouCheckName=@ShouCheckName,ShouCheckDate=@ShouCheckDate,FuFlag=@FuFlag,FuCheckName=@FuCheckName,FuCheckDate=@FuCheckDate where AwbID=@AwbID and AwbNo=@AwbNo and AKind=@AKind and BelongSystem=@BelongSystem";
            }

            using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
            {
                NewaySql.AddInParameter(cmd, "@AwbID", DbType.Int64, entity.AwbID);
                NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, entity.AwbNo);
                NewaySql.AddInParameter(cmd, "@ShouFlag", DbType.String, entity.ShouFlag);
                NewaySql.AddInParameter(cmd, "@ShouCheckName", DbType.String, entity.ShouCheckName);
                NewaySql.AddInParameter(cmd, "@ShouCheckDate", DbType.DateTime, DateTime.Now);
                NewaySql.AddInParameter(cmd, "@FuFlag", DbType.String, entity.ShouFlag);
                NewaySql.AddInParameter(cmd, "@FuCheckName", DbType.String, entity.ShouCheckName);
                NewaySql.AddInParameter(cmd, "@FuCheckDate", DbType.DateTime, DateTime.Now);
                NewaySql.AddInParameter(cmd, "@AKind", DbType.String, entity.AKind);
                NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);

                NewaySql.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 判断是否存在代收款运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool IsExistCollectAwb(AwbEntity entity)
        {
            bool result = false;
            string strSQL = @"select COUNT(*) as ANum from Tbl_CollectAccount where AwbID=@AwbID and AwbNo=@AwbNo and AKind=@AKind and BelongSystem=@BelongSystem";
            using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
            {
                NewaySql.AddInParameter(cmd, "@AwbID", DbType.Int64, entity.AwbID);
                NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, entity.AwbNo);
                NewaySql.AddInParameter(cmd, "@AKind", DbType.String, entity.AKind);
                NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                using (DbDataReader idr = NewaySql.ExecuteReader(cmd))
                {
                    while (idr.Read()) { if (Convert.ToInt32(idr["ANum"]) > 0) { result = true; break; } } idr.Dispose();
                }
            }
            return result;
        }
        /// <summary>
        /// 新增到达运单数据
        /// </summary>
        /// <param name="awb"></param>
        private void AddArriveAwbInfo(AwbEntity awb)
        {
            #region 新增的SQL语句
            string strSQL = @"INSERT INTO Tbl_Awb_Arrive(AwbID,AwbNo,Dep,Dest,Transit,Piece,Weight,Volume,AwbPiece,AwbWeight,AwbVolume,Attach,InsuranceFee,TransitFee,TransportFee,DeliverFee,OtherFee,TotalCharge,Rebate,NowPay,PickPay,CheckOutType,CollectMoney,ReturnAwb,TrafficType,TimeLimit,DeliveryType,SteveDore,ShipperName,ShipperUnit,ShipperAddress,ShipperTelephone,ShipperCellphone,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,HandleTime,Remark,DelFlag,CreateAwb,CreateDate,OP_ID,HAwbNo,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,ClientNum,AwbStatus,BelongSystem,HLY) VALUES (@AwbID,@AwbNo,@Dep,@Dest,@Transit,@Piece,@Weight,@Volume,@AwbPiece,@AwbWeight,@AwbVolume,@Attach,@InsuranceFee,@TransitFee,@TransportFee,@DeliverFee,@OtherFee,@TotalCharge,@Rebate,@NowPay,@PickPay,@CheckOutType,@CollectMoney,@ReturnAwb,@TrafficType,@TimeLimit,@DeliveryType,@SteveDore,@ShipperName,@ShipperUnit,@ShipperAddress,@ShipperTelephone,@ShipperCellphone,@AcceptUnit,@AcceptAddress,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@HandleTime,@Remark,@DelFlag,@CreateAwb,@CreateDate,@OP_ID,@HAwbNo,@FinanceFirstCheck,@FirstCheckName,@FirstCheckDate,@FinanceSecondCheck,@SecondCheckName,@SecondCheckDate,@ClientNum,@AwbStatus,@BelongSystem,@HLY)";
            #endregion
            #region 新增实现
            using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
            {
                NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awb.AwbID);
                NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awb.AwbNo.ToUpper());
                NewaySql.AddInParameter(cmdAdd, "@Dep", DbType.String, awb.Dep);
                NewaySql.AddInParameter(cmdAdd, "@Dest", DbType.String, awb.Dest);
                NewaySql.AddInParameter(cmdAdd, "@Transit", DbType.String, string.IsNullOrEmpty(Convert.ToString(awb.Transit).Trim()) ? awb.Dest : awb.Transit);
                NewaySql.AddInParameter(cmdAdd, "@Piece", DbType.Int32, awb.Piece);
                NewaySql.AddInParameter(cmdAdd, "@Weight", DbType.Decimal, awb.Weight);
                NewaySql.AddInParameter(cmdAdd, "@Volume", DbType.Decimal, awb.Volume);
                NewaySql.AddInParameter(cmdAdd, "@AwbPiece", DbType.Int32, awb.Piece);
                NewaySql.AddInParameter(cmdAdd, "@AwbWeight", DbType.Decimal, awb.Weight);
                NewaySql.AddInParameter(cmdAdd, "@AwbVolume", DbType.Decimal, awb.Volume);
                NewaySql.AddInParameter(cmdAdd, "@Attach", DbType.Int32, awb.Attach);
                NewaySql.AddInParameter(cmdAdd, "@InsuranceFee", DbType.Decimal, awb.InsuranceFee);
                NewaySql.AddInParameter(cmdAdd, "@TransitFee", DbType.Decimal, awb.TransitFee);
                NewaySql.AddInParameter(cmdAdd, "@TransportFee", DbType.Decimal, awb.TransportFee);
                NewaySql.AddInParameter(cmdAdd, "@DeliverFee", DbType.Decimal, awb.DeliverFee);
                NewaySql.AddInParameter(cmdAdd, "@OtherFee", DbType.Decimal, awb.OtherFee);
                NewaySql.AddInParameter(cmdAdd, "@TotalCharge", DbType.Decimal, awb.TotalCharge);
                NewaySql.AddInParameter(cmdAdd, "@Rebate", DbType.Decimal, awb.Rebate);
                NewaySql.AddInParameter(cmdAdd, "@NowPay", DbType.Decimal, awb.NowPay);
                NewaySql.AddInParameter(cmdAdd, "@PickPay", DbType.Decimal, awb.PickPay);
                NewaySql.AddInParameter(cmdAdd, "@CheckOutType", DbType.String, awb.CheckOutType);
                NewaySql.AddInParameter(cmdAdd, "@CollectMoney", DbType.Decimal, awb.CollectMoney);
                NewaySql.AddInParameter(cmdAdd, "@ReturnAwb", DbType.Int32, awb.ReturnAwb);
                NewaySql.AddInParameter(cmdAdd, "@TrafficType", DbType.String, awb.TrafficType);
                NewaySql.AddInParameter(cmdAdd, "@TimeLimit", DbType.Int32, awb.TimeLimit);
                NewaySql.AddInParameter(cmdAdd, "@DeliveryType", DbType.String, awb.DeliveryType);
                NewaySql.AddInParameter(cmdAdd, "@SteveDore", DbType.String, awb.SteveDore);
                NewaySql.AddInParameter(cmdAdd, "@ShipperName", DbType.String, awb.ShipperName);
                NewaySql.AddInParameter(cmdAdd, "@ShipperUnit", DbType.String, awb.ShipperUnit);
                NewaySql.AddInParameter(cmdAdd, "@ShipperAddress", DbType.String, awb.ShipperAddress);
                NewaySql.AddInParameter(cmdAdd, "@ShipperTelephone", DbType.String, awb.ShipperTelephone);
                NewaySql.AddInParameter(cmdAdd, "@ShipperCellphone", DbType.String, awb.ShipperCellphone);
                NewaySql.AddInParameter(cmdAdd, "@AcceptUnit", DbType.String, awb.AcceptUnit);
                NewaySql.AddInParameter(cmdAdd, "@AcceptAddress", DbType.String, awb.AcceptAddress);
                NewaySql.AddInParameter(cmdAdd, "@AcceptPeople", DbType.String, awb.AcceptPeople);
                NewaySql.AddInParameter(cmdAdd, "@AcceptTelephone", DbType.String, awb.AcceptTelephone);
                NewaySql.AddInParameter(cmdAdd, "@AcceptCellphone", DbType.String, awb.AcceptCellphone);
                NewaySql.AddInParameter(cmdAdd, "@HandleTime", DbType.DateTime, awb.HandleTime);
                NewaySql.AddInParameter(cmdAdd, "@Remark", DbType.String, awb.Remark);
                NewaySql.AddInParameter(cmdAdd, "@DelFlag", DbType.String, awb.DelFlag);
                NewaySql.AddInParameter(cmdAdd, "@AwbStatus", DbType.String, awb.AwbStatus);
                NewaySql.AddInParameter(cmdAdd, "@CreateAwb", DbType.String, awb.CreateAwb);
                NewaySql.AddInParameter(cmdAdd, "@CreateDate", DbType.DateTime, DateTime.Now);
                NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, awb.OP_ID);
                NewaySql.AddInParameter(cmdAdd, "@HAwbNo", DbType.String, awb.HAwbNo);
                NewaySql.AddInParameter(cmdAdd, "@FinanceFirstCheck", DbType.String, awb.FinanceFirstCheck);
                NewaySql.AddInParameter(cmdAdd, "@FirstCheckName", DbType.String, awb.FirstCheckName);
                NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem);
                if (awb.CheckOutType.Equals("0"))
                {
                    NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DateTime.Now);
                    NewaySql.AddInParameter(cmdAdd, "@SecondCheckDate", DbType.DateTime, DateTime.Now);
                }
                else
                {
                    NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DBNull.Value);
                    NewaySql.AddInParameter(cmdAdd, "@SecondCheckDate", DbType.DateTime, DBNull.Value);
                }
                NewaySql.AddInParameter(cmdAdd, "@SecondCheckName", DbType.String, awb.SecondCheckName);
                NewaySql.AddInParameter(cmdAdd, "@FinanceSecondCheck", DbType.String, awb.FinanceSecondCheck);
                NewaySql.AddInParameter(cmdAdd, "@ClientNum", DbType.String, awb.ClientNum);
                NewaySql.AddInParameter(cmdAdd, "@HLY", DbType.String, awb.HLY);
                NewaySql.ExecuteNonQuery(cmdAdd);
            }
            #endregion
        }
        /// <summary>
        /// 修改好来运保存状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateHlyAwbStatus(HlyExchangeEntity entity)
        {
            try
            {
                string strDel = @"UPDATE Tbl_HLY_AwbInfo SET SaveStatus=@SaveStatus WHERE HlyFiveNo=@HlyFiveNo";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmdAdd, "@SaveStatus", DbType.String, entity.SaveStatus);
                    NewaySql.AddInParameter(cmdAdd, "@HlyFiveNo", DbType.String, entity.HlyFiveNo);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
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
            List<AwbEntity> result = new List<AwbEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.HandleTime DESC,a.AwbNo DESC) AS RowNumber,b.UserName,a.*,DATEADD(day,a.TimeLimit,a.HandleTime) as LatestTimeLimit FROM Tbl_Awb_Basic as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem Where (1=1) ";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind)) { strSQL += " and a.TransKind = '" + entity.TransKind + "'"; }
                if (entity.DelFlag.Equals("1")) { strSQL += " and a.DelFlag = '" + entity.DelFlag + "'"; }
                //副单号
                if (!string.IsNullOrEmpty(entity.HAwbNo)) { strSQL += " and a.HAwbNo like  '%" + entity.HAwbNo + "%'"; }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'"; }
                //发货单位，人
                if (!string.IsNullOrEmpty(entity.ShipperUnit)) { strSQL += " and (a.ShipperUnit like '%" + entity.ShipperUnit + "%' or a.ShipperName like '%" + entity.ShipperUnit + "%')"; }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')"; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dep in ('" + res + "')";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dest in ('" + res + "')";
                }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and a.AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //件数
                if (!entity.Piece.Equals(0))
                {
                    strSQL += " and a.Piece=" + entity.Piece + "";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                goodList.Add(new AwbGoodsEntity
                                {
                                    GoodsID = Convert.ToInt32(idrGoods["GoodsID"]),
                                    AwbNo = Convert.ToString(idrGoods["AwbNo"]),
                                    Package = Convert.ToString(idrGoods["Package"]),
                                    Goods = Convert.ToString(idrGoods["Goods"]),
                                    Weight = Convert.ToDecimal(idrGoods["Weight"]),
                                    WeightPrice = Convert.ToDecimal(idrGoods["WeightPrice"]),
                                    Volume = Convert.ToDecimal(idrGoods["Volume"]),
                                    VolumePrice = Convert.ToDecimal(idrGoods["VolumePrice"]),
                                    Piece = Convert.ToInt32(idrGoods["Piece"]),
                                    DeclareValue = Convert.ToString(idrGoods["DeclareValue"]),
                                    OP_ID = Convert.ToString(idrGoods["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idrGoods["OP_DATE"])
                                });

                                good += Convert.ToString(idrGoods["Goods"]).Trim() + ",";
                            }
                            #endregion
                            #region 获取运单数据

                            result.Add(new AwbEntity
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                Attach = Convert.ToInt32(idr["Attach"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                NowPay = Convert.ToDecimal(idr["NowPay"]),
                                PickPay = Convert.ToDecimal(idr["PickPay"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                SteveDore = Convert.ToString(idr["SteveDore"]).Trim(),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                ShipperAddress = Convert.ToString(idr["ShipperAddress"]).Trim(),
                                ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 0 : Convert.ToInt16(idr["PrintNum"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]).Trim(),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                HLY = Convert.ToString(idr["HLY"]).Trim(),
                                ArriveDate = string.IsNullOrEmpty(Convert.ToString(idr["ArriveDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ArriveDate"]),
                                AwbGoods = goodList,
                                Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Awb_Basic  Where (1=1)";
                strCount += " and BelongSystem='" + entity.BelongSystem + "'";
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind)) { strCount += " and TransKind = '" + entity.TransKind + "'"; }
                if (entity.DelFlag.Equals("1")) { strCount += " and DelFlag = '" + entity.DelFlag + "'"; }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strCount += " and CheckOutType = '" + entity.CheckOutType + "'"; }
                //副单号
                if (!string.IsNullOrEmpty(entity.HAwbNo)) { strCount += " and HAwbNo like  '%" + entity.HAwbNo + "%'"; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and Dep in ('" + res + "')";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and Dest in ('" + res + "')";
                }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strCount += " and AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strCount += " and CreateAwb = '" + entity.CreateAwb + "'"; }
                //发货单位，人
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strCount += " and (ShipperUnit like '%" + entity.ShipperUnit + "%' or ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strCount += " and (AcceptPeople like '%" + entity.AcceptPeople + "%' or AcceptUnit like '%" + entity.AcceptPeople + "%')";
                }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //件数
                if (!entity.Piece.Equals(0))
                {
                    strCount += " and Piece=" + entity.Piece + "";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 修改运单信息
        /// </summary>
        /// <param name="awb"></param>
        public void UpdateAwbInfo(AwbEntity awb)
        {
            try
            {
                awb.EnSafe();
                //判断运单是否分批
                bool FP = IsFenPiByAwbNo(awb.AwbNo, awb.BelongSystem);
                //List<AwbEntity> elist = QueryAwb(new AwbEntity { AwbNo = awb.AwbNo });
                if (!FP)//没有分批 分批件数=总件数
                {
                    awb.AwbPiece = awb.Piece;
                    awb.AwbWeight = awb.Weight;
                    awb.AwbVolume = awb.Volume;
                }
                //else//有分批
                //{
                //    bool su = false;
                //    foreach (var it in elist)
                //    {
                //        if (!it.DelFlag.Equals("0") && !it.DelFlag.Equals("1"))//说明有一批已经配载
                //        {
                //            su = true;
                //            break;
                //        }
                //    }
                //}
                #region 修改的SQL语句
                string strSQL = @"UPDATE Tbl_Awb_Basic set Dep=@Dep,Dest=@Dest,Transit=@Transit,Piece=@Piece,Weight=@Weight,Volume=@Volume,AwbPiece=@AwbPiece,AwbWeight=@AwbWeight,AwbVolume=@AwbVolume,Attach=@Attach,InsuranceFee=@InsuranceFee,TransitFee=@TransitFee,TransportFee=@TransportFee,DeliverFee=@DeliverFee,OtherFee=@OtherFee,TotalCharge=@TotalCharge,Rebate=@Rebate,NowPay=@NowPay,PickPay=@PickPay,CheckOutType=@CheckOutType,CollectMoney=@CollectMoney,ReturnAwb=@ReturnAwb,TimeLimit=@TimeLimit,TrafficType=@TrafficType,DeliveryType=@DeliveryType,SteveDore=@SteveDore,ShipperName=@ShipperName,ShipperUnit=@ShipperUnit,ShipperAddress=@ShipperAddress,ShipperTelephone=@ShipperTelephone,ShipperCellphone=@ShipperCellphone,AcceptUnit=@AcceptUnit,AcceptAddress=@AcceptAddress,AcceptPeople=@AcceptPeople,AcceptTelephone=@AcceptTelephone,AcceptCellphone=@AcceptCellphone,HandleTime=@HandleTime,Remark=@Remark,DelFlag=@DelFlag,OP_ID=@OP_ID,HAwbNo=@HAwbNo,FinanceFirstCheck=@FinanceFirstCheck,FirstCheckName=@FirstCheckName,FirstCheckDate=@FirstCheckDate,FinanceSecondCheck=@FinanceSecondCheck,SecondCheckName=@SecondCheckName,SecondCheckDate=@SecondCheckDate,ClientNum=@ClientNum,ClerkNo=@ClerkNo,ClerkName=@ClerkName,HLY=@HLY Where AwbID=@AwbID AND AwbNo=@AwbNo and BelongSystem=@BelongSystem";
                #endregion
                #region 修改实现
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awb.AwbID);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem.ToUpper());
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awb.AwbNo.ToUpper());
                    NewaySql.AddInParameter(cmdAdd, "@Dep", DbType.String, awb.Dep);
                    NewaySql.AddInParameter(cmdAdd, "@Transit", DbType.String, string.IsNullOrEmpty(Convert.ToString(awb.Transit).Trim()) ? awb.Dest : awb.Transit.Trim());
                    NewaySql.AddInParameter(cmdAdd, "@Dest", DbType.String, awb.Dest);
                    NewaySql.AddInParameter(cmdAdd, "@Piece", DbType.Int32, awb.Piece);
                    NewaySql.AddInParameter(cmdAdd, "@Weight", DbType.Decimal, awb.Weight);
                    NewaySql.AddInParameter(cmdAdd, "@Volume", DbType.Decimal, awb.Volume);
                    NewaySql.AddInParameter(cmdAdd, "@AwbPiece", DbType.Int32, awb.AwbPiece);
                    NewaySql.AddInParameter(cmdAdd, "@AwbWeight", DbType.Decimal, awb.AwbWeight);
                    NewaySql.AddInParameter(cmdAdd, "@AwbVolume", DbType.Decimal, awb.AwbVolume);
                    NewaySql.AddInParameter(cmdAdd, "@Attach", DbType.Int32, awb.Attach);
                    NewaySql.AddInParameter(cmdAdd, "@InsuranceFee", DbType.Decimal, awb.InsuranceFee);
                    NewaySql.AddInParameter(cmdAdd, "@TransitFee", DbType.Decimal, awb.TransitFee);
                    NewaySql.AddInParameter(cmdAdd, "@TransportFee", DbType.Decimal, awb.TransportFee);
                    NewaySql.AddInParameter(cmdAdd, "@DeliverFee", DbType.Decimal, awb.DeliverFee);
                    NewaySql.AddInParameter(cmdAdd, "@OtherFee", DbType.Decimal, awb.OtherFee);
                    NewaySql.AddInParameter(cmdAdd, "@TotalCharge", DbType.Decimal, awb.TotalCharge);
                    NewaySql.AddInParameter(cmdAdd, "@Rebate", DbType.Decimal, awb.Rebate);
                    NewaySql.AddInParameter(cmdAdd, "@NowPay", DbType.Decimal, awb.NowPay);
                    NewaySql.AddInParameter(cmdAdd, "@PickPay", DbType.Decimal, awb.PickPay);
                    NewaySql.AddInParameter(cmdAdd, "@CheckOutType", DbType.String, awb.CheckOutType);
                    NewaySql.AddInParameter(cmdAdd, "@CollectMoney", DbType.Decimal, awb.CollectMoney);
                    NewaySql.AddInParameter(cmdAdd, "@ReturnAwb", DbType.Int32, awb.ReturnAwb);
                    NewaySql.AddInParameter(cmdAdd, "@TimeLimit", DbType.Int32, awb.TimeLimit);
                    NewaySql.AddInParameter(cmdAdd, "@TrafficType", DbType.String, awb.TrafficType);
                    NewaySql.AddInParameter(cmdAdd, "@DeliveryType", DbType.String, awb.DeliveryType);
                    NewaySql.AddInParameter(cmdAdd, "@SteveDore", DbType.String, awb.SteveDore);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperName", DbType.String, awb.ShipperName);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperUnit", DbType.String, awb.ShipperUnit);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperAddress", DbType.String, awb.ShipperAddress);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperTelephone", DbType.String, awb.ShipperTelephone);
                    NewaySql.AddInParameter(cmdAdd, "@ShipperCellphone", DbType.String, awb.ShipperCellphone);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptUnit", DbType.String, awb.AcceptUnit);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptAddress", DbType.String, awb.AcceptAddress);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptPeople", DbType.String, awb.AcceptPeople);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptTelephone", DbType.String, awb.AcceptTelephone);
                    NewaySql.AddInParameter(cmdAdd, "@AcceptCellphone", DbType.String, awb.AcceptCellphone);
                    NewaySql.AddInParameter(cmdAdd, "@HandleTime", DbType.DateTime, awb.HandleTime);
                    NewaySql.AddInParameter(cmdAdd, "@Remark", DbType.String, awb.Remark);
                    NewaySql.AddInParameter(cmdAdd, "@DelFlag", DbType.String, awb.DelFlag);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, awb.OP_ID);
                    NewaySql.AddInParameter(cmdAdd, "@HAwbNo", DbType.String, awb.HAwbNo);
                    NewaySql.AddInParameter(cmdAdd, "@FinanceFirstCheck", DbType.String, awb.FinanceFirstCheck);
                    NewaySql.AddInParameter(cmdAdd, "@FirstCheckName", DbType.String, awb.FirstCheckName);
                    if (awb.CheckOutType.Equals("0"))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DateTime.Now);
                        NewaySql.AddInParameter(cmdAdd, "@SecondCheckDate", DbType.DateTime, DateTime.Now);
                    }
                    else
                    {
                        if (awb.FinanceFirstCheck.Equals("1"))
                        {
                            NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, awb.FirstCheckDate);
                        }
                        else
                        {
                            NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DBNull.Value);
                        }
                        if (awb.FinanceSecondCheck.Equals("1"))
                        {
                            NewaySql.AddInParameter(cmdAdd, "@SecondCheckDate", DbType.DateTime, awb.SecondCheckDate);
                        }
                        else
                        {
                            NewaySql.AddInParameter(cmdAdd, "@SecondCheckDate", DbType.DateTime, DBNull.Value);
                        }
                    }
                    NewaySql.AddInParameter(cmdAdd, "@SecondCheckName", DbType.String, awb.SecondCheckName);
                    NewaySql.AddInParameter(cmdAdd, "@FinanceSecondCheck", DbType.String, awb.FinanceSecondCheck);
                    NewaySql.AddInParameter(cmdAdd, "@ClientNum", DbType.String, awb.ClientNum);
                    NewaySql.AddInParameter(cmdAdd, "@ClerkName", DbType.String, awb.ClerkName);
                    NewaySql.AddInParameter(cmdAdd, "@ClerkNo", DbType.String, awb.ClerkNo);
                    NewaySql.AddInParameter(cmdAdd, "@HLY", DbType.String, awb.HLY);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
                #endregion
                #region 2017-04-13 添加副单号逻辑
                if (!string.IsNullOrEmpty(awb.HAwbNo))
                {
                    string[] hw = awb.HAwbNo.Split(',');//拆分
                    List<HAwbNoEntity> hwList = new List<HAwbNoEntity>();
                    foreach (var it in hw)
                    {
                        if (!string.IsNullOrEmpty(it))
                        {
                            hwList.Add(new HAwbNoEntity { AwbNo = awb.AwbNo, HAwbNo = it, BelongSystem = awb.BelongSystem, OP_ID = awb.OP_ID });
                        }
                        AddHAwbNo(hwList);
                    }
                }
                #endregion
                //保存运单的货物品名数据 ，先删除，后增加
                DeleteAwbGoodsInfo(awb.AwbGoods);
                AddAwbGoodsInfo(awb.AwbGoods);
                //判断托运人是否存在，如果不存在就添加进去，存在就过
                ClientEntity ce = new ClientEntity();
                ce.ClientShortName = ce.ClientName = awb.ShipperUnit;
                ce.Boss = awb.ShipperName;
                ce.Telephone = awb.ShipperTelephone;
                ce.Cellphone = awb.ShipperCellphone;
                ce.DelFlag = "0";
                ce.OP_ID = awb.OP_ID;
                ce.BelongSystem = awb.BelongSystem;
                int cID = 0;
                if (!IsExistClient(ce, ref cID))
                {
                    cID = AddClientReturnID(ce);
                }
                //判断收货人是否存在，如果不存在就添加进去，存在就过
                ClientAcceptAddressEntity caae = new ClientAcceptAddressEntity
                {
                    ClientID = cID,
                    AcceptCity = awb.Dest,
                    AcceptCompany = awb.AcceptUnit,
                    AcceptAddress = awb.AcceptAddress,
                    AcceptPeople = awb.AcceptPeople,
                    AcceptCellphone = awb.AcceptCellphone,
                    AcceptTelephone = awb.AcceptTelephone,
                    BelongSystem = awb.BelongSystem,
                    OP_ID = awb.OP_ID
                };
                if (!IsExistClientAcceptAddress(caae))
                {
                    SaveClientAcceptAddress(caae);
                }
                //如果是现付并且存在代收款就保存数据到代收款审核表
                if (awb.CheckOutType.Equals("0") && awb.CollectMoney > 0)
                {
                    AddCollectAccount(new AwbEntity
                    {
                        AwbID = awb.AwbID,
                        AwbNo = awb.AwbNo,
                        ShouFlag = awb.ShouFlag,
                        ShouCheckName = awb.ShouCheckName,
                        ShouCheckDate = DateTime.Now,
                        BelongSystem = awb.BelongSystem,
                        AKind = "0"
                    });
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 判断运单是否分批
        /// </summary>
        /// <param name="awbid"></param>
        /// <returns></returns>
        public bool IsFenPiByAwbNo(string awbno, string BelongSystem)
        {
            bool result = false;
            string strSQL = @"select COUNT(*) as Nm from Tbl_Awb_Basic where AwbNo=@AwbNo and BelongSystem=@BelongSystem";
            using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
            {
                NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, awbno);
                NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, BelongSystem);
                using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["Nm"]) > 1)
                        {
                            result = true;
                        }
                    }
                }

            }
            return result;
        }
        /// <summary>
        /// 修改运单的货物品名数据
        /// </summary>
        /// <param name="goods"></param>
        public void DeleteAwbGoodsInfo(List<AwbGoodsEntity> goods)
        {
            try
            {
                string strSQL = @"DELETE From Tbl_Awb_Goods Where AwbNo=@AwbNo and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, goods[0].AwbNo.ToUpper().Trim());
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, goods[0].BelongSystem.ToUpper().Trim());
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除或作废运单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ty">0:作废运单1:彻底删除运单</param>
        public void DelAwb(List<AwbEntity> entity, int ty)
        {
            try
            {
                foreach (var it in entity)
                {
                    it.EnSafe();
                    //设置订单表的删除标志为1 表示删除或作废
                    string strDel = @"UPDATE Tbl_Awb_Basic SET DelFlag=1 WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                    string strArrDel = @"UPDATE Tbl_Awb_Arrive SET DelFlag=1 WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                    if (ty.Equals(1))
                    {
                        strDel = @"Delete From Tbl_Awb_Basic WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                        strArrDel = @"Delete From Tbl_Awb_Arrive WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                    }
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, it.AwbID);
                        NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, it.BelongSystem);
                        NewaySql.ExecuteNonQuery(cmdAdd);
                    }
                    using (DbCommand cmdArrAdd = NewaySql.GetSqlStringCommond(strArrDel))
                    {
                        NewaySql.AddInParameter(cmdArrAdd, "@AwbID", DbType.Int64, it.AwbID);
                        NewaySql.AddInParameter(cmdArrAdd, "@BelongSystem", DbType.String, it.BelongSystem);
                        NewaySql.ExecuteNonQuery(cmdArrAdd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
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
            List<AwbEntity> result = new List<AwbEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句

                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.HandleTime DESC,a.AwbNo Asc) AS RowNumber,b.UserName,a.*,DATEADD(day,a.TimeLimit,a.HandleTime) as LatestTimeLimit  FROM Tbl_Awb_Basic as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem Where (1=1) ";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind))
                {
                    strSQL += " and a.TransKind = '" + entity.TransKind + "'";
                }
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strSQL += " and a.DelFlag = '" + entity.DelFlag + "'";
                }
                //副单号
                if (!string.IsNullOrEmpty(entity.HAwbNo))
                {
                    strSQL += " and a.HAwbNo like  '%" + entity.HAwbNo + "%'";
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType))
                {
                    strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'";
                }
                //发货单位，人
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strSQL += " and (a.ShipperUnit like '%" + entity.ShipperUnit + "%' or a.ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strSQL += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')";
                }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    strSQL += "and (";
                    string sd = string.Empty;
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        if (i == ccs.Length - 1)
                        {
                            sd += " ((a.Dep='" + ccs[i] + "' and (a.MidDest IS NULL or a.MidDest='')) or (a.Dep<>'" + ccs[i] + "' and a.MidDest='" + ccs[i] + "'))";
                        }
                        else
                        {
                            sd += " ((a.Dep='" + ccs[i] + "' and (a.MidDest IS NULL or a.MidDest='')) or (a.Dep<>'" + ccs[i] + "' and a.MidDest='" + ccs[i] + "')) or ";
                        }
                    }
                    strSQL += sd + ")";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dest in ('" + res + "')";
                }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strSQL += " and a.AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'";
                }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //件数
                if (!entity.Piece.Equals(0))
                {
                    strSQL += " and a.Piece=" + entity.Piece + "";
                }

                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                #endregion
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };

                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                goodList.Add(new AwbGoodsEntity
                                {
                                    GoodsID = Convert.ToInt32(idrGoods["GoodsID"]),
                                    AwbNo = Convert.ToString(idrGoods["AwbNo"]),
                                    Package = Convert.ToString(idrGoods["Package"]),
                                    Goods = Convert.ToString(idrGoods["Goods"]),
                                    Weight = Convert.ToDecimal(idrGoods["Weight"]),
                                    WeightPrice = Convert.ToDecimal(idrGoods["WeightPrice"]),
                                    Volume = Convert.ToDecimal(idrGoods["Volume"]),
                                    VolumePrice = Convert.ToDecimal(idrGoods["VolumePrice"]),
                                    Piece = Convert.ToInt32(idrGoods["Piece"]),
                                    DeclareValue = Convert.ToString(idrGoods["DeclareValue"]),
                                    OP_ID = Convert.ToString(idrGoods["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idrGoods["OP_DATE"])
                                });

                                good += Convert.ToString(idrGoods["Goods"]).Trim() + ",";
                            }
                            #endregion
                            #region 获取运单数据
                            result.Add(new AwbEntity
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                Attach = Convert.ToInt32(idr["Attach"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                NowPay = Convert.ToDecimal(idr["NowPay"]),
                                PickPay = Convert.ToDecimal(idr["PickPay"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                SteveDore = Convert.ToString(idr["SteveDore"]).Trim(),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                ShipperAddress = Convert.ToString(idr["ShipperAddress"]).Trim(),
                                ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 0 : Convert.ToInt16(idr["PrintNum"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                HLY = Convert.ToString(idr["HLY"]).Trim(),
                                ArriveDate = string.IsNullOrEmpty(Convert.ToString(idr["ArriveDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ArriveDate"]),
                                AwbGoods = goodList,
                                Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Awb_Basic  Where (1=1)";
                strCount += " and BelongSystem='" + entity.BelongSystem + "'";
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind))
                {
                    strCount += " and TransKind = '" + entity.TransKind + "'";
                }
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strCount += " and DelFlag = '" + entity.DelFlag + "'";
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType))
                {
                    strCount += " and CheckOutType = '" + entity.CheckOutType + "'";
                }
                //副单号
                if (!string.IsNullOrEmpty(entity.HAwbNo))
                {
                    strCount += " and HAwbNo like  '%" + entity.HAwbNo + "%'";
                }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    strCount += "and (";
                    string sd = string.Empty;
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        if (i == ccs.Length - 1)
                        {
                            sd += " ((Dep='" + ccs[i] + "' and (MidDest IS NULL or MidDest='')) or (Dep<>'" + ccs[i] + "' and MidDest='" + ccs[i] + "'))";
                        }
                        else
                        {
                            sd += " ((Dep='" + ccs[i] + "' and (MidDest IS NULL or MidDest='')) or (Dep<>'" + ccs[i] + "' and MidDest='" + ccs[i] + "')) or ";
                        }
                    }
                    strCount += sd + ")";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and Dest in ('" + res + "')";
                }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strCount += " and AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'";
                }
                //发货单位，人
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strCount += " and (ShipperUnit like '%" + entity.ShipperUnit + "%' or ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strCount += " and (AcceptPeople like '%" + entity.AcceptPeople + "%' or AcceptUnit like '%" + entity.AcceptPeople + "%')";
                }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //件数
                if (!entity.Piece.Equals(0))
                {
                    strCount += " and Piece=" + entity.Piece + "";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 根据单位ID查询该单位下的所有部门数据
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public List<SystemDepartEntity> GetDeptByUnitID(string UnitID, string BelongSystem)
        {
            List<SystemDepartEntity> result = new List<SystemDepartEntity>();
            string strSQL = @"SELECT DepID,CName,People,Telephone,Address FROM Tbl_SysDepart WHERE DelFlag=0";
            strSQL += " and BelongSystem='" + BelongSystem + "'";
            if (!string.IsNullOrEmpty(UnitID)) { strSQL += " and UnitID in (" + UnitID + ")"; }
            try
            {
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemDepartEntity
                            {
                                DepID = Convert.ToInt32(idrCount["DepID"]),
                                CName = Convert.ToString(idrCount["CName"]).Trim(),
                                People = Convert.ToString(idrCount["People"]).Trim(),
                                Address = Convert.ToString(idrCount["Address"]).Trim(),
                                Telephone = Convert.ToString(idrCount["Telephone"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 判断运单是否配载了
        /// </summary>
        /// <param name="awb"></param>
        /// <returns></returns>
        public bool IsManifestAwb(AwbEntity awb)
        {
            bool result = false;
            try
            {
                awb.EnSafe();
                string strSQL = @"SELECT ContractNum FROM Tbl_Awb_Basic WHERE AwbNo=@AwbNo and AwbID=@AwbID and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awb.AwbNo.ToUpper().Trim());
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem.ToUpper().Trim());
                    NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awb.AwbID);
                    using (DataTable idr = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        if (idr.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(idr.Rows[0]["ContractNum"]).Trim()))
                            {
                                result = true;
                            }
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }

        /// <summary>
        /// 保存配载数据信息
        /// </summary>
        /// <param name="awb"></param>
        public void SaveManifest(DepManifestEntity awb)
        {
            try
            {
                awb.EnSafe();
                string strSQL = @"INSERT INTO Tbl_DepManifest( ContractNum ,Dep ,Transit ,Dest ,TruckNum ,TransportFee ,PrepayFee ,ArriveFee ,PayMode ,StartTime ,PassTime ,Weight ,Volume ,Remark ,OP_ID ,DriverCellPhone,Driver,DriverIDNum,DriverIDAddress,UnLoadAddress,DestPeople,DestCellphone,DelFlag,PreArriveTime,Loader,Manifester,CreateTime,CardBank,CardName,CardNum,BelongSystem,Dispatcher) VALUES  (@ContractNum ,@Dep ,@Transit ,@Dest ,@TruckNum ,@TransportFee ,@PrepayFee ,@ArriveFee ,@PayMode ,@StartTime ,@PassTime ,@Weight ,@Volume ,@Remark ,@OP_ID ,@DriverCellPhone,@Driver,@DriverIDNum,@DriverIDAddress,@UnLoadAddress,@DestPeople,@DestCellphone,@DelFlag,@PreArriveTime,@Loader,@Manifester,@CreateTime,@CardBank,@CardName,@CardNum,@BelongSystem,@Dispatcher)";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    #region 赋值
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, awb.ContractNum);
                    NewaySql.AddInParameter(cmdAdd, "@Dep", DbType.String, awb.Dep);
                    NewaySql.AddInParameter(cmdAdd, "@Transit", DbType.String, awb.Transit);
                    NewaySql.AddInParameter(cmdAdd, "@Dest", DbType.String, awb.Dest);
                    NewaySql.AddInParameter(cmdAdd, "@TruckNum", DbType.String, awb.TruckNum);
                    NewaySql.AddInParameter(cmdAdd, "@Weight", DbType.Decimal, awb.Weight);
                    NewaySql.AddInParameter(cmdAdd, "@Volume", DbType.Decimal, awb.Volume);
                    NewaySql.AddInParameter(cmdAdd, "@TransportFee", DbType.Decimal, awb.TransportFee);
                    NewaySql.AddInParameter(cmdAdd, "@PrepayFee", DbType.Decimal, awb.PrepayFee);
                    NewaySql.AddInParameter(cmdAdd, "@ArriveFee", DbType.Decimal, awb.ArriveFee);
                    NewaySql.AddInParameter(cmdAdd, "@PassTime", DbType.Decimal, awb.PassTime);
                    NewaySql.AddInParameter(cmdAdd, "@PayMode", DbType.String, awb.PayMode);
                    NewaySql.AddInParameter(cmdAdd, "@StartTime", DbType.DateTime, awb.StartTime);
                    NewaySql.AddInParameter(cmdAdd, "@Remark", DbType.String, awb.Remark);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, awb.OP_ID);
                    NewaySql.AddInParameter(cmdAdd, "@DriverCellPhone", DbType.String, awb.DriverCellPhone);
                    NewaySql.AddInParameter(cmdAdd, "@Driver", DbType.String, awb.Driver);
                    NewaySql.AddInParameter(cmdAdd, "@DriverIDNum", DbType.String, awb.DriverIDNum);
                    NewaySql.AddInParameter(cmdAdd, "@DriverIDAddress", DbType.String, awb.DriverIDAddress);
                    NewaySql.AddInParameter(cmdAdd, "@UnLoadAddress", DbType.String, awb.UnLoadAddress);
                    NewaySql.AddInParameter(cmdAdd, "@DestPeople", DbType.String, awb.DestPeople);
                    NewaySql.AddInParameter(cmdAdd, "@DestCellphone", DbType.String, awb.DestCellphone);
                    NewaySql.AddInParameter(cmdAdd, "@DelFlag", DbType.String, awb.DelFlag);
                    NewaySql.AddInParameter(cmdAdd, "@PreArriveTime", DbType.DateTime, awb.PreArriveTime);
                    NewaySql.AddInParameter(cmdAdd, "@Dispatcher", DbType.String, awb.Dispatcher);
                    NewaySql.AddInParameter(cmdAdd, "@Loader", DbType.String, awb.Loader);
                    NewaySql.AddInParameter(cmdAdd, "@Manifester", DbType.String, awb.Manifester);
                    NewaySql.AddInParameter(cmdAdd, "@CreateTime", DbType.DateTime, DateTime.Now);
                    NewaySql.AddInParameter(cmdAdd, "@CardBank", DbType.String, awb.CardBank);
                    NewaySql.AddInParameter(cmdAdd, "@CardName", DbType.String, awb.CardName);
                    NewaySql.AddInParameter(cmdAdd, "@CardNum", DbType.String, awb.CardNum);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem);

                    #endregion
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
                ///更新运单的配载合同号
                SetAwbManifest(awb.AwbInfo);
                TruckEntity te = new TruckEntity
                {
                    TruckNum = awb.TruckNum.Trim(),
                    Driver = awb.Driver,
                    Length = awb.Length,
                    Model = awb.Model,
                    DriverCellPhone = awb.DriverCellPhone,
                    DriverIDAddress = awb.DriverIDAddress,
                    DriverIDNum = awb.DriverIDNum,
                    TripMark = "0",
                    BelongSystem = awb.BelongSystem,
                    OP_ID = awb.OP_ID
                };
                //更新车辆信息
                if (!IsExistTruck(te)) { SaveTruckInfo(te); }
                //如果银行卡号不为空更新司机银行卡号
                if (!string.IsNullOrEmpty(awb.CardNum.Trim()))
                {
                    if (!IsExistDriverCard(new DriverCardEntity { TruckNum = awb.TruckNum.Trim(), CardNum = awb.CardNum.Trim(), CardName = awb.CardName.Trim(), BelongSystem = awb.BelongSystem }))
                    {
                        SaveDriverCard(new DriverCardEntity
                        {
                            TruckNum = awb.TruckNum.Trim(),
                            CardBank = awb.CardBank.Trim(),
                            CardName = awb.CardName.Trim(),
                            CardNum = awb.CardNum.Trim(),
                            BelongSystem = awb.BelongSystem,
                            OP_ID = awb.OP_ID.Trim()
                        });
                    }
                }
                //增加运单跟踪状态
                foreach (var ie in awb.AwbInfo)
                {
                    InsertAwbStatus(new AwbStatus
                    {
                        AwbID = ie.AwbID,
                        AwbNo = ie.AwbNo,
                        TruckFlag = "1",
                        CurrentLocation = awb.Dep,
                        LastHour = awb.PassTime,
                        OP_ID = awb.OP_ID,
                        OP_DATE = awb.StartTime,
                        BelongSystem = awb.BelongSystem,
                        DetailInfo = ""
                    });
                }
                //更新车辆信息
                UpdateTruckCityStatus(new TruckEntity
                {
                    DelFlag = "2",
                    TruckNum = awb.TruckNum,
                    BelongSystem = awb.BelongSystem,
                    CurCity = awb.Dep
                });
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 设置运单的状态为配载
        /// </summary>
        /// <param name="entity"></param>
        public void SetAwbManifest(List<AwbEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    it.EnSafe();
                    //设置订单表的删除标志为2 表示配载了就不能再修改
                    string strDel = @"UPDATE Tbl_Awb_Basic SET DelFlag=2";
                    if (!string.IsNullOrEmpty(it.TransKind))
                    {
                        strDel += ",TransKind=@TransKind";
                    }
                    strDel += " ,ContractNum=@ContractNum WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, it.BelongSystem);
                        NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, it.ContractNum);
                        if (!string.IsNullOrEmpty(it.TransKind))
                        {
                            NewaySql.AddInParameter(cmdAdd, "@TransKind", DbType.String, it.TransKind);
                        }
                        NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, it.AwbID);
                        NewaySql.ExecuteNonQuery(cmdAdd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 判断是否存在司机账号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistDriverCard(DriverCardEntity entity)
        {
            bool result = false;
            try
            {
                string strSQL = @"SELECT TruckNum from Tbl_DriverCard Where TruckNum=@TruckNum and CardName=@CardName and CardNum=@CardNum and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@TruckNum", DbType.String, entity.TruckNum);
                    NewaySql.AddInParameter(cmd, "@CardName", DbType.String, entity.CardName);
                    NewaySql.AddInParameter(cmd, "@CardNum", DbType.String, entity.CardNum);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd)) { if (dt.Rows.Count > 0) { result = true; } }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 保存司机银行卡信息
        /// </summary>
        /// <param name="entity"></param>
        public void SaveDriverCard(DriverCardEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strAdd = @"INSERT INTO Tbl_DriverCard(TruckNum,CardBank,CardName,CardNum,OP_ID,BelongSystem) VALUES  (@TruckNum,@CardBank,@CardName,@CardNum,@OP_ID,@BelongSystem)";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strAdd))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@TruckNum", DbType.String, entity.TruckNum);
                    NewaySql.AddInParameter(cmdAdd, "@CardBank", DbType.String, entity.CardBank);
                    NewaySql.AddInParameter(cmdAdd, "@CardName", DbType.String, entity.CardName);
                    NewaySql.AddInParameter(cmdAdd, "@CardNum", DbType.String, entity.CardNum);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改车辆的当前城市和车辆状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateTruckCityStatus(TruckEntity entity)
        {
            entity.EnSafe();
            try
            {
                string strSQL = @"UPDATE Tbl_Truck SET CurCity=@CurCity,DelFlag=@DelFlag WHERE TruckNum=@TruckNum and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@CurCity", DbType.String, entity.CurCity);
                    NewaySql.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    NewaySql.AddInParameter(cmd, "@TruckNum", DbType.String, entity.TruckNum);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 合并运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        public void merge(AwbEntity entity, List<AwbEntity> list)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"INSERT INTO Tbl_Awb_Basic(AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume ,AwbPiece ,AwbWeight ,AwbVolume ,Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit ,ShipperAddress,  ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem,DelayReason,HLY) Select AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume,";
                strSQL += "" + entity.AwbPiece + ",";
                strSQL += "" + entity.AwbWeight + ",";
                strSQL += "" + entity.AwbVolume + ",";
                strSQL += @"Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit,ShipperAddress ,ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem,DelayReason,HLY from Tbl_Awb_Basic Where AwbID=@AwbID and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, list[0].BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, list[0].AwbID);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }

                foreach (var it in list)
                {
                    //删除原运单数据
                    string strDel = @"Delete from Tbl_Awb_Basic WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                    using (DbCommand cmddel = NewaySql.GetSqlStringCommond(strDel))
                    {
                        NewaySql.AddInParameter(cmddel, "@BelongSystem", DbType.String, it.BelongSystem);
                        NewaySql.AddInParameter(cmddel, "@AwbID", DbType.Int64, it.AwbID);
                        NewaySql.ExecuteNonQuery(cmddel);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 分批操作
        /// </summary>
        /// <param name="ent"></param>
        public void Tear(List<AwbEntity> entity)
        {
            try
            {
                foreach (var awb in entity)
                {
                    awb.EnSafe();
                    string strSQL = @"INSERT INTO Tbl_Awb_Basic( AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume ,AwbPiece ,AwbWeight ,AwbVolume ,Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit ,ShipperAddress,  ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem,DelayReason,HLY) Select AwbNo,HAwbNo,Dep,Transit,Dest,Piece,Weight,Volume,";
                    strSQL += "" + awb.AwbPiece + ",";
                    strSQL += "" + awb.AwbWeight + ",";
                    strSQL += "" + awb.AwbVolume + ",";
                    strSQL += @"Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit,ShipperAddress ,ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem,DelayReason,HLY from Tbl_Awb_Basic Where AwbID=@AwbID and BelongSystem=@BelongSystem";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awb.AwbID);
                        NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem);
                        NewaySql.ExecuteNonQuery(cmdAdd);
                    }
                }
                //删除原运单数据
                string strDel = @"Delete from Tbl_Awb_Basic WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                using (DbCommand cmddel = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmddel, "@BelongSystem", DbType.String, entity[0].BelongSystem);
                    NewaySql.AddInParameter(cmddel, "@AwbID", DbType.Int64, entity[0].AwbID);
                    NewaySql.ExecuteNonQuery(cmddel);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 根据车牌号获取司机的银行卡信息
        /// </summary>
        /// <returns></returns>
        public List<DriverCardEntity> GetDriverCardByTruckNum(string TruckNum)
        {
            List<DriverCardEntity> result = new List<DriverCardEntity>();
            try
            {
                string strSQL = @" SELECT CardBank,CardName,CardNum,TruckNum,DCID FROM Tbl_DriverCard Where TruckNum=@TruckNum ";

                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(command, "@TruckNum", DbType.String, TruckNum);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new DriverCardEntity
                            {
                                DCID = Convert.ToInt32(idr["DCID"]),
                                TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                CardBank = Convert.ToString(idr["CardBank"]).Trim(),
                                CardName = Convert.ToString(idr["CardName"]).Trim(),
                                CardNum = Convert.ToString(idr["CardNum"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        #endregion
        #region 车辆合同管理

        /// <summary>
        /// 查看所有已经生成司机合同的自发运单信息
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryDepManifest(int pIndex, int pNum, DepManifestEntity entity)
        {
            List<DepManifestEntity> result = new List<DepManifestEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.StartTime DESC) AS RowNumber,a.*,RTRIM(a.Dep)+'/'+RTRIM(a.Dest) AS Range,ISNULL(b.Length,0) as Length,ISNULL(b.Weight,0) as TruckWeight,b.Model,c.FilePath,c.TbFilePath FROM Tbl_DepManifest AS a LEFT JOIN Tbl_Truck AS b ON a.TruckNum=b.TruckNum left join Tbl_Dep_Files as c on a.ContractNum=c.ContractNum and a.BelongSystem=c.BelongSystem and c.FMode='0'  Where (1=1) ";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //自发和外协
                if (!string.IsNullOrEmpty(entity.TransKind)) { strSQL += " and a.TransKind = '" + entity.TransKind + "'"; }
                //合同状态
                if (!string.IsNullOrEmpty(entity.CancelFlag)) { strSQL += " and a.CancelFlag = '" + entity.CancelFlag + "'"; }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest)) { strSQL += " and a.Dest = '" + entity.Dest + "'"; }
                if (!string.IsNullOrEmpty(entity.Dep)) { strSQL += " and a.Dep = '" + entity.Dep + "'"; }
                //一审
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strSQL += " and a.DelFlag = '" + entity.DelFlag + "'"; }
                //二审
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strSQL += " and a.FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //司机合同号
                if (!string.IsNullOrEmpty(entity.ContractNum)) { strSQL += " and a.ContractNum like '%" + entity.ContractNum + "%'"; }
                //车牌照
                if (!string.IsNullOrEmpty(entity.TruckNum)) { strSQL += " and a.TruckNum like '%" + entity.TruckNum + "%'"; }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow idr in dt.Rows)
                            {
                                if (result.Where(c => c.ContractNum.Equals(Convert.ToString(idr["ContractNum"]).Trim())).Count() > 0)
                                {
                                    continue;
                                }
                                int AwbNum = 0;
                                int TotalPiece = 0;
                                decimal TotalCharge = 0.00M, noStopVolume = 0.00M, noStopFee = 0.00M, transitVolume = 0.00M, tranistFee = 0.00M;
                                DataTable awbdt = QueryAwbByContrctNo(Convert.ToString(idr["ContractNum"]).Trim());
                                if (awbdt.Rows.Count > 0)
                                {
                                    AwbNum = Convert.ToInt32(awbdt.Rows[0]["AwbNum"]);
                                    TotalPiece = Convert.ToInt32(awbdt.Rows[0]["TotalPiece"]);
                                    TotalCharge = Convert.ToDecimal(awbdt.Rows[0]["TotalCharge"]);
                                    noStopVolume = Convert.ToDecimal(awbdt.Rows[0]["noStopVolume"]);
                                    noStopFee = Convert.ToDecimal(awbdt.Rows[0]["noStopFee"]);
                                    transitVolume = Convert.ToDecimal(awbdt.Rows[0]["transitVolume"]);
                                    tranistFee = Convert.ToDecimal(awbdt.Rows[0]["tranistFee"]);
                                }
                                result.Add(new DepManifestEntity
                                {
                                    AwbNum = AwbNum,
                                    TotalAwbPiece = TotalPiece,
                                    TotalFee = TotalCharge,
                                    noStopVolume = noStopVolume,
                                    noStopFee = noStopFee,
                                    transitVolume = transitVolume,
                                    tranistFee = tranistFee,
                                    ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                    TruckNum = Convert.ToString(idr["TruckNum"]),
                                    Dep = Convert.ToString(idr["Dep"]).Trim(),
                                    Dest = Convert.ToString(idr["Dest"]).Trim(),
                                    Transit = Convert.ToString(idr["Transit"]).Trim(),
                                    Range = Convert.ToString(idr["Range"]).Trim(),
                                    TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                    PrepayFee = Convert.ToDecimal(idr["PrepayFee"]),
                                    ArriveFee = Convert.ToDecimal(idr["ArriveFee"]),
                                    PassTime = Convert.ToDecimal(idr["PassTime"]),
                                    Weight = Convert.ToDecimal(idr["Weight"]),
                                    Volume = Convert.ToDecimal(idr["Volume"]),
                                    Length = Convert.ToDecimal(idr["Length"]),
                                    TruckWeight = Convert.ToDecimal(idr["TruckWeight"]),
                                    Model = Convert.ToString(idr["Model"]).Trim(),
                                    StartTime = Convert.ToDateTime(idr["StartTime"]),
                                    PayMode = Convert.ToString(idr["PayMode"]).Trim(),
                                    Driver = Convert.ToString(idr["Driver"]).Trim(),
                                    DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                    DriverIDNum = Convert.ToString(idr["DriverIDNum"]).Trim(),
                                    DestCellphone = Convert.ToString(idr["DestCellphone"]).Trim(),
                                    DestPeople = Convert.ToString(idr["DestPeople"]).Trim(),
                                    UnLoadAddress = Convert.ToString(idr["UnLoadAddress"]).Trim(),
                                    Remark = Convert.ToString(idr["Remark"]).Trim(),
                                    OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                    DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                    TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                    FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]).Trim(),
                                    SecondCheckName = Convert.ToString(idr["SecondCheckName"]).Trim(),
                                    SecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["SecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SecondCheckDate"]),
                                    ContractStatus = Convert.ToString(idr["ContractStatus"]).Trim(),
                                    ContractURL = Convert.ToString(idr["ContractStatus"]).Trim() == "0" ? "" : Convert.ToString(idr["FilePath"]),
                                    //ContractURL = Convert.ToString(idr["ContractStatus"]).Trim() == "0" ? "" : "<a href=../" + Convert.ToString(idr["FilePath"]) + " target=\"_blank\">车辆合同</a>",
                                    Loader = Convert.ToString(idr["Loader"]).Trim(),
                                    Dispatcher = Convert.ToString(idr["Dispatcher"]).Trim(),
                                    Manifester = Convert.ToString(idr["Manifester"]).Trim(),
                                    CancelFlag = Convert.ToString(idr["CancelFlag"]).Trim(),
                                    CardBank = Convert.ToString(idr["CardBank"]).Trim(),
                                    CardName = Convert.ToString(idr["CardName"]).Trim(),
                                    CardNum = Convert.ToString(idr["CardNum"]).Trim(),
                                    CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim()
                                });
                            }
                        }
                    }
                }
                resHT["rows"] = result;
                string strCount = @"Select Count(*) as TotalCount from Tbl_DepManifest as a Where (1=1)";
                strCount += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //自发和外协
                if (!string.IsNullOrEmpty(entity.TransKind)) { strCount += " and a.TransKind = '" + entity.TransKind + "'"; }
                //合同状态
                if (!string.IsNullOrEmpty(entity.CancelFlag)) { strCount += " and a.CancelFlag = '" + entity.CancelFlag + "'"; }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest)) { strCount += " and a.Dest = '" + entity.Dest + "'"; }
                if (!string.IsNullOrEmpty(entity.Dep)) { strCount += " and a.Dep = '" + entity.Dep + "'"; }
                //一审
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strCount += " and a.DelFlag = '" + entity.DelFlag + "'"; }
                //二审
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strCount += " and a.FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //司机合同号
                if (!string.IsNullOrEmpty(entity.ContractNum)) { strCount += " and a.ContractNum like '%" + entity.ContractNum + "%'"; }
                //车牌照
                if (!string.IsNullOrEmpty(entity.TruckNum)) { strCount += " and a.TruckNum like '%" + entity.TruckNum + "%'"; }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 通过车辆合同号查询该车上的所有运单统计信息
        /// </summary>
        /// <param name="contractnum"></param>
        /// <returns></returns>
        public DataTable QueryAwbByContrctNo(string contractnum)
        {
            DataTable result = new DataTable();
            try
            {
                string strSQL = @"select a.*,ISNULL(b.noStopVolume,0) as noStopVolume,ISNULL(Convert(decimal(18,2),b.noStopFee) ,0) as noStopFee,ISNULL(c.transitVolume,0) as transitVolume,ISNULL(Convert(decimal(18,2),c.tranistFee),0) as tranistFee  from (select ContractNum,COUNT(ContractNum) as AwbNum,
ISNULL(SUM(AwbPiece),0) as TotalPiece,
ISNULL(SUM(CASE WHEN ISNULL(Piece,0)=0 THEN 0 ELSE AwbPiece*TotalCharge/Piece END),0) as TotalCharge 
from Tbl_Awb_Basic where ContractNum=@ContractNum group by ContractNum
) as a left join 
(
select ContractNum,
ISNULL(SUM(AwbVolume),0) as noStopVolume,
ISNULL(SUM(CASE WHEN ISNULL(Piece,0)=0 THEN 0 ELSE AwbPiece*TotalCharge/Piece END),0) as noStopFee 
 from Tbl_Awb_Basic 
where ContractNum=@ContractNum and Dest=Transit  group by ContractNum
) as b on a.ContractNum=b.ContractNum
left join (
select ContractNum,
ISNULL(SUM(AwbVolume),0) as transitVolume,
ISNULL(SUM(CASE WHEN ISNULL(Piece,0)=0 THEN 0 ELSE AwbPiece*TotalCharge/Piece END),0) as tranistFee 
 from Tbl_Awb_Basic 
where ContractNum=@ContractNum and Dest<>Transit  group by ContractNum
) as c on a.ContractNum=c.ContractNum";

                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, contractnum);
                    result = NewaySql.ExecuteDataTable(cmdAdd);
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 根据配载合同号查询配载信息和运单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DepManifestEntity QueryAwbInfoByContractNumForExport(DepManifestEntity entity)
        {
            DepManifestEntity result = new DepManifestEntity();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT ContractNum,TruckNum,CreateTime,StartTime,Dep,Dest,Transit,Driver,DriverCellPhone,DriverIDNum,TransportFee  FROM Tbl_DepManifest  WHERE ContractNum=@ContractNum ";
                strSQL += " and BelongSystem=@BelongSystem";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(command, "@ContractNum", DbType.String, entity.ContractNum.Trim());
                    NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, entity.BelongSystem.Trim());
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            List<AwbEntity> awb = QueryAwbByContractNum(new AwbEntity { ContractNum = entity.ContractNum, BelongSystem = entity.BelongSystem });
                            result = new DepManifestEntity
                            {
                                ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Transit = Convert.ToString(idr["Transit"]).Trim(),
                                StartTime = Convert.ToDateTime(idr["StartTime"]),
                                CreateTime = Convert.ToDateTime(idr["CreateTime"]),
                                Driver = Convert.ToString(idr["Driver"]).Trim(),
                                DriverIDNum = Convert.ToString(idr["DriverIDNum"]).Trim(),
                                DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                AwbInfo = awb
                            };
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
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
            List<AwbEntity> result = new List<AwbEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT * FROM Tbl_Awb_Basic WHERE (1=1)";
                if (!string.IsNullOrEmpty(entity.BelongSystem))
                {
                    strSQL += " and BelongSystem=@BelongSystem";
                }
                if (!string.IsNullOrEmpty(entity.ContractNum))
                {
                    strSQL += " and ContractNum=@ContractNum";
                }
                if (!entity.AwbID.Equals(0))
                {
                    strSQL += " and AwbID=@AwbID ";
                }
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    if (!string.IsNullOrEmpty(entity.BelongSystem))
                    {
                        NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, entity.BelongSystem);
                    }
                    if (!string.IsNullOrEmpty(entity.ContractNum))
                    {
                        NewaySql.AddInParameter(command, "@ContractNum", DbType.String, entity.ContractNum);
                    }
                    if (!entity.AwbID.Equals(0))
                    {
                        NewaySql.AddInParameter(command, "@AwbID", DbType.Int64, entity.AwbID);
                    }
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                good += Convert.ToString(idrGoods["Goods"]).Trim() + ",";
                            }
                            #endregion
                            #region 获取运单数据
                            result.Add(new AwbEntity
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                MidDest = Convert.ToString(idr["MidDest"]).Trim(),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                Transit = Convert.ToString(idr["Transit"]).Trim(),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                Goods = good,
                                HLY = Convert.ToString(idr["HLY"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim()
                            });
                            #endregion
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 长途车车辆合同信息查询供导出
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<DepManifestEntity> QueryDepManifestForExport(DepManifestEntity entity)
        {
            List<DepManifestEntity> result = new List<DepManifestEntity>();
            entity.EnSafe();
            string strSQL = @"SELECT a.*,RTRIM(a.Dep)+'/'+RTRIM(a.Dest) AS Range,ISNULL(b.Length,0) as Length,ISNULL(b.Weight,0) as TruckWeight,b.Model FROM Tbl_DepManifest AS a LEFT JOIN Tbl_Truck AS b ON a.TruckNum=b.TruckNum  Where (1=1) ";
            strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
            //自发和外协
            if (!string.IsNullOrEmpty(entity.TransKind)) { strSQL += " and a.TransKind = '" + entity.TransKind + "'"; }
            //合同状态
            if (!string.IsNullOrEmpty(entity.CancelFlag)) { strSQL += " and a.CancelFlag = '" + entity.CancelFlag + "'"; }
            //到达站
            if (!string.IsNullOrEmpty(entity.Dest)) { strSQL += " and a.Dest = '" + entity.Dest + "'"; }
            //一审
            if (!string.IsNullOrEmpty(entity.DelFlag)) { strSQL += " and a.DelFlag = '" + entity.DelFlag + "'"; }
            //二审
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strSQL += " and a.FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
            //司机合同号
            if (!string.IsNullOrEmpty(entity.ContractNum)) { strSQL += " and a.ContractNum like '%" + entity.ContractNum + "%'"; }
            //车牌照
            if (!string.IsNullOrEmpty(entity.TruckNum)) { strSQL += " and a.TruckNum like '%" + entity.TruckNum + "%'"; }
            //发车时间范围
            if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
            {
                strSQL += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
            {
                strSQL += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            try
            {
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow idr in dt.Rows)
                            {
                                if (result.Where(c => c.ContractNum.Equals(Convert.ToString(idr["ContractNum"]).Trim())).Count() > 0)
                                {
                                    continue;
                                }
                                int AwbNum = 0;
                                int TotalPiece = 0;
                                decimal TotalCharge = 0.00M;
                                DataTable awbdt = QueryAwbByContrctNo(Convert.ToString(idr["ContractNum"]).Trim());
                                if (awbdt.Rows.Count > 0)
                                {
                                    AwbNum = Convert.ToInt32(awbdt.Rows[0]["AwbNum"]);
                                    TotalPiece = Convert.ToInt32(awbdt.Rows[0]["TotalPiece"]);
                                    TotalCharge = Convert.ToDecimal(awbdt.Rows[0]["TotalCharge"]);
                                }
                                result.Add(new DepManifestEntity
                                {
                                    AwbNum = AwbNum,
                                    TotalAwbPiece = TotalPiece,
                                    TotalFee = TotalCharge,
                                    ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                    TruckNum = Convert.ToString(idr["TruckNum"]),
                                    Dep = Convert.ToString(idr["Dep"]).Trim(),
                                    Dest = Convert.ToString(idr["Dest"]).Trim(),
                                    Transit = Convert.ToString(idr["Transit"]).Trim(),
                                    Range = Convert.ToString(idr["Range"]).Trim(),
                                    TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                    PrepayFee = Convert.ToDecimal(idr["PrepayFee"]),
                                    ArriveFee = Convert.ToDecimal(idr["ArriveFee"]),
                                    PassTime = Convert.ToDecimal(idr["PassTime"]),
                                    Weight = Convert.ToDecimal(idr["Weight"]),
                                    Volume = Convert.ToDecimal(idr["Volume"]),
                                    Length = Convert.ToDecimal(idr["Length"]),
                                    TruckWeight = Convert.ToDecimal(idr["TruckWeight"]),
                                    Model = Convert.ToString(idr["Model"]).Trim(),
                                    StartTime = Convert.ToDateTime(idr["StartTime"]),
                                    CreateTime = Convert.ToDateTime(idr["CreateTime"]),
                                    ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"]),
                                    PayMode = Convert.ToString(idr["PayMode"]).Trim(),
                                    Driver = Convert.ToString(idr["Driver"]).Trim(),
                                    DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                    DriverIDNum = Convert.ToString(idr["DriverIDNum"]).Trim(),
                                    DestCellphone = Convert.ToString(idr["DestCellphone"]).Trim(),
                                    DestPeople = Convert.ToString(idr["DestPeople"]).Trim(),
                                    UnLoadAddress = Convert.ToString(idr["UnLoadAddress"]).Trim(),
                                    Remark = Convert.ToString(idr["Remark"]).Trim(),
                                    OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                    DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                    TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                    FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]).Trim(),
                                    SecondCheckName = Convert.ToString(idr["SecondCheckName"]).Trim(),
                                    SecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["SecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SecondCheckDate"]),
                                    Loader = Convert.ToString(idr["Loader"]).Trim(),
                                    Dispatcher = Convert.ToString(idr["Dispatcher"]).Trim(),
                                    Manifester = Convert.ToString(idr["Manifester"]).Trim(),
                                    CancelFlag = Convert.ToString(idr["CancelFlag"]).Trim(),
                                    CardBank = Convert.ToString(idr["CardBank"]).Trim(),
                                    CardName = Convert.ToString(idr["CardName"]).Trim(),
                                    CardNum = Convert.ToString(idr["CardNum"]).Trim(),
                                    CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim()
                                });
                            }
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
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
            List<AwbEntity> result = new List<AwbEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.HandleTime DESC,a.AwbNo Asc) AS RowNumber,b.UserName,a.*,DATEADD(day,a.TimeLimit,a.HandleTime) as LatestTimeLimit FROM Tbl_Awb_Basic as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem Where (1=1) ";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind))
                {
                    strSQL += " and a.TransKind = '" + entity.TransKind + "'";
                }
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strSQL += " and a.DelFlag = '" + entity.DelFlag + "'";
                }
                //运单号
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strSQL += " and a.AwbNo = '" + entity.AwbNo + "'";
                }
                //副单号
                if (!string.IsNullOrEmpty(entity.HAwbNo))
                {
                    strSQL += " and a.HAwbNo like  '%" + entity.HAwbNo + "%'";
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType))
                {
                    strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'";
                }
                //发货单位，人
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strSQL += " and (a.ShipperUnit like '%" + entity.ShipperUnit + "%' or a.ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strSQL += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')";
                }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    strSQL += "and (";
                    string sd = string.Empty;
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        if (i == ccs.Length - 1)
                        {
                            sd += " ((a.Dep='" + ccs[i] + "' and (a.MidDest IS NULL or a.MidDest='')) or (a.Dep<>'" + ccs[i] + "' and a.MidDest='" + ccs[i] + "'))";
                        }
                        else
                        {
                            sd += " ((a.Dep='" + ccs[i] + "' and (a.MidDest IS NULL or a.MidDest='')) or (a.Dep<>'" + ccs[i] + "' and a.MidDest='" + ccs[i] + "')) or ";
                        }
                    }
                    strSQL += sd + ")";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dest in ('" + res + "')";
                }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strSQL += " and a.AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'";
                }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //件数
                if (!entity.Piece.Equals(0))
                {
                    strSQL += " and a.Piece=" + entity.Piece + "";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                #endregion
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                goodList.Add(new AwbGoodsEntity
                                {
                                    GoodsID = Convert.ToInt32(idrGoods["GoodsID"]),
                                    AwbNo = Convert.ToString(idrGoods["AwbNo"]),
                                    Package = Convert.ToString(idrGoods["Package"]),
                                    Goods = Convert.ToString(idrGoods["Goods"]),
                                    Weight = Convert.ToDecimal(idrGoods["Weight"]),
                                    WeightPrice = Convert.ToDecimal(idrGoods["WeightPrice"]),
                                    Volume = Convert.ToDecimal(idrGoods["Volume"]),
                                    VolumePrice = Convert.ToDecimal(idrGoods["VolumePrice"]),
                                    Piece = Convert.ToInt32(idrGoods["Piece"]),
                                    DeclareValue = Convert.ToString(idrGoods["DeclareValue"]),
                                    OP_ID = Convert.ToString(idrGoods["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idrGoods["OP_DATE"])
                                });

                                good += Convert.ToString(idrGoods["Goods"]).Trim() + ",";
                            }
                            #endregion
                            #region 获取运单数据

                            result.Add(new AwbEntity
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                Attach = Convert.ToInt32(idr["Attach"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                NowPay = Convert.ToDecimal(idr["NowPay"]),
                                PickPay = Convert.ToDecimal(idr["PickPay"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                SteveDore = Convert.ToString(idr["SteveDore"]).Trim(),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                ShipperAddress = Convert.ToString(idr["ShipperAddress"]).Trim(),
                                ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 0 : Convert.ToInt16(idr["PrintNum"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                ArriveDate = string.IsNullOrEmpty(Convert.ToString(idr["ArriveDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ArriveDate"]),
                                AwbGoods = goodList,
                                Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Awb_Basic  Where (1=1)";
                strCount += " and BelongSystem='" + entity.BelongSystem + "'";
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind))
                {
                    strCount += " and TransKind = '" + entity.TransKind + "'";
                }
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strCount += " and DelFlag = '" + entity.DelFlag + "'";
                }
                //运单号
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strCount += " and AwbNo = '" + entity.AwbNo + "'";
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType))
                {
                    strCount += " and CheckOutType = '" + entity.CheckOutType + "'";
                }
                //副单号
                if (!string.IsNullOrEmpty(entity.HAwbNo))
                {
                    strCount += " and HAwbNo like  '%" + entity.HAwbNo + "%'";
                }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    strCount += "and (";
                    string sd = string.Empty;
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        if (i == ccs.Length - 1)
                        {
                            sd += " ((Dep='" + ccs[i] + "' and (MidDest IS NULL or MidDest='')) or (Dep<>'" + ccs[i] + "' and MidDest='" + ccs[i] + "'))";
                        }
                        else
                        {
                            sd += " ((Dep='" + ccs[i] + "' and (MidDest IS NULL or MidDest='')) or (Dep<>'" + ccs[i] + "' and MidDest='" + ccs[i] + "')) or ";
                        }
                    }
                    strCount += sd + ")";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and Dest in ('" + res + "')";
                }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strCount += " and AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'";
                }
                //发货单位，人
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strCount += " and (ShipperUnit like '%" + entity.ShipperUnit + "%' or ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strCount += " and (AcceptPeople like '%" + entity.AcceptPeople + "%' or AcceptUnit like '%" + entity.AcceptPeople + "%')";
                }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 通过车辆合同号查询配载信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DepManifestEntity QueryDepManifest(DepManifestEntity entity)
        {
            DepManifestEntity result = new DepManifestEntity();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT ISNULL(b.Length,0) as Length,ISNULL(b.Model,'') as Model,a.* FROM Tbl_DepManifest as a left join Tbl_Truck as b on a.TruckNum=b.TruckNum  WHERE a.ContractNum=@ContractNum ";
                if (!string.IsNullOrEmpty(entity.BelongSystem))
                {
                    strSQL += " and a.BelongSystem=@BelongSystem";
                }
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(command, "@ContractNum", DbType.String, entity.ContractNum.Trim());
                    if (!string.IsNullOrEmpty(entity.BelongSystem))
                    {
                        NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, entity.BelongSystem.Trim());
                    }
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new DepManifestEntity
                            {
                                ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Transit = Convert.ToString(idr["Transit"]).Trim(),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                PrepayFee = Convert.ToDecimal(idr["PrepayFee"]),
                                ArriveFee = Convert.ToDecimal(idr["ArriveFee"]),
                                PassTime = Convert.ToDecimal(idr["PassTime"]),
                                Model = Convert.ToString(idr["Model"]).Trim(),
                                Length = Convert.ToDecimal(idr["Length"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                StartTime = Convert.ToDateTime(idr["StartTime"]),
                                PayMode = Convert.ToString(idr["PayMode"]).Trim(),
                                Driver = Convert.ToString(idr["Driver"]).Trim(),
                                DriverIDNum = Convert.ToString(idr["DriverIDNum"]).Trim(),
                                DriverIDAddress = Convert.ToString(idr["DriverIDAddress"]).Trim(),
                                DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                DestPeople = Convert.ToString(idr["DestPeople"]).Trim(),
                                DestCellphone = Convert.ToString(idr["DestCellphone"]).Trim(),
                                UnLoadAddress = Convert.ToString(idr["UnLoadAddress"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                Loader = Convert.ToString(idr["Loader"]).Trim(),
                                Dispatcher = Convert.ToString(idr["Dispatcher"]).Trim(),
                                Manifester = Convert.ToString(idr["Manifester"]).Trim(),
                                TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                CancelFlag = Convert.ToString(idr["CancelFlag"]).Trim(),
                                CardNum = Convert.ToString(idr["CardNum"]).Trim(),
                                CardName = Convert.ToString(idr["CardName"]).Trim(),
                                CardBank = Convert.ToString(idr["CardBank"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim()
                            };
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改司机合同信息 审核与未审核
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateManifest(List<DepManifestEntity> entity)
        {
            try
            {
                foreach (var order in entity)
                {
                    order.EnSafe();
                    DepManifestEntity dme = QueryDepManifest(order);
                    //if (dme != null)
                    //{
                    //    if (!dme.TruckFlag.Trim().Equals("0") && !dme.TruckFlag.Trim().Equals("1")) { continue; }
                    //}
                    if (!order.DelFlag.Equals("0"))//审核
                    {
                        if (dme != null) { if (dme.DelFlag.Trim().Equals("1")) { continue; } }
                    }
                    else
                    {
                        if (dme != null) { if (dme.DelFlag.Trim().Equals("0")) { continue; } }
                    }

                    //设置订单表的删除标志为1 表示删除或作废
                    string strDel = @"UPDATE Tbl_DepManifest SET ";
                    //如果是审核的话，就设置车辆状态为出发 否则为原始在站
                    if (!dme.DelFlag.Trim().Equals("2"))
                    {
                        if (order.DelFlag.Equals("1"))
                        {
                            strDel += "TruckFlag=1,";
                        }
                        else
                        {
                            strDel += "TruckFlag=0,";
                        }
                    }
                    strDel += "DelFlag=@DelFlag,FinanceFirstCheck=@FinanceFirstCheck,FirstCheckName=@FirstCheckName,FirstCheckDate=@FirstCheckDate,OP_ID=@OP_ID,OP_DATE=@OP_DATE WHERE ContractNum=@ContractNum and BelongSystem=@BelongSystem";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, order.ContractNum);
                        NewaySql.AddInParameter(cmdAdd, "@DelFlag", DbType.String, order.DelFlag);
                        NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, order.OP_ID);
                        NewaySql.AddInParameter(cmdAdd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                        NewaySql.AddInParameter(cmdAdd, "@FinanceFirstCheck", DbType.String, order.FinanceFirstCheck);
                        NewaySql.AddInParameter(cmdAdd, "@FirstCheckName", DbType.String, order.FirstCheckName);
                        NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DateTime.Now);
                        NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, order.BelongSystem);
                        NewaySql.ExecuteNonQuery(cmdAdd);
                    }
                    if (!dme.DelFlag.Trim().Equals("2"))
                    {
                        ///更新运单的配载合同号
                        SetAwbStatus(new AwbEntity
                        {
                            ContractNum = order.ContractNum,
                            BelongSystem = order.BelongSystem,
                            AwbStatus = order.DelFlag
                        });
                        //如果是审核的话，就设置车辆状态为出发 否则为原始在站
                        if (order.DelFlag.Equals("1"))
                        {
                            //保存车辆状态跟踪信息
                            TruckStatusTrackEntity tste = new TruckStatusTrackEntity
                            {
                                ContractNum = order.ContractNum,
                                TruckNum = order.TruckNum,
                                TruckFlag = "1",
                                CurrentLocation = order.Dep,
                                BelongSystem = order.BelongSystem,
                                OP_DATE = order.StartTime,
                                OP_ID = order.OP_ID
                            };
                            InsertTruckStatusTrack(tste);
                            //更改车辆状态
                            UpdateTruckCityStatus(new TruckEntity
                            {
                                DelFlag = "2",
                                TruckNum = order.TruckNum,
                                BelongSystem = order.BelongSystem,
                                CurCity = order.Dest
                            });
                        }
                        else
                        {
                            //删除车辆状态跟踪信息
                            TruckStatusTrackEntity tste = new TruckStatusTrackEntity
                            {
                                ContractNum = order.ContractNum,
                                TruckNum = order.TruckNum,
                                TruckFlag = "0",
                                BelongSystem = order.BelongSystem,
                                OP_ID = order.OP_ID
                            };
                            DeleteTruckStatusTrack(tste);
                            //更改车辆状态
                            UpdateTruckCityStatus(new TruckEntity
                            {
                                DelFlag = "0",
                                TruckNum = order.TruckNum,
                                BelongSystem = order.BelongSystem,
                                CurCity = order.Dep
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 保存车辆状态跟踪信息
        /// </summary>
        /// <param name="ent"></param>
        private void InsertTruckStatusTrack(TruckStatusTrackEntity ent)
        {
            try
            {
                ent.EnSafe();
                string strSQL = @"INSERT INTO Tbl_TruckStatusTrack(ContractNum, TruckNum,TruckFlag,CurrentLocation ,LastHour,DetailInfo,OP_ID,BelongSystem,OP_DATE) VALUES (@ContractNum,@TruckNum,@TruckFlag,@CurrentLocation,@LastHour,@DetailInfo,@OP_ID,@BelongSystem,@OP_DATE)";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, ent.ContractNum);
                    NewaySql.AddInParameter(cmdAdd, "@TruckNum", DbType.String, ent.TruckNum);
                    NewaySql.AddInParameter(cmdAdd, "@TruckFlag", DbType.String, ent.TruckFlag);
                    NewaySql.AddInParameter(cmdAdd, "@CurrentLocation", DbType.String, ent.CurrentLocation);
                    NewaySql.AddInParameter(cmdAdd, "@LastHour", DbType.Decimal, ent.LastHour);
                    NewaySql.AddInParameter(cmdAdd, "@DetailInfo", DbType.String, ent.DetailInfo);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, ent.OP_ID);
                    if (ent.OP_DATE != null)
                    {
                        NewaySql.AddInParameter(cmdAdd, "@OP_DATE", DbType.DateTime, ent.OP_DATE);
                    }
                    else
                    {
                        NewaySql.AddInParameter(cmdAdd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    }
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, ent.BelongSystem);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除车辆状态跟踪信息
        /// </summary>
        /// <param name="ent"></param>
        private void DeleteTruckStatusTrack(TruckStatusTrackEntity ent)
        {
            try
            {
                ent.EnSafe();
                string strSQL = @"DELETE FROM Tbl_TruckStatusTrack WHERE TruckNum=@TruckNum AND ContractNum=@ContractNum and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, ent.ContractNum);
                    NewaySql.AddInParameter(cmdAdd, "@TruckNum", DbType.String, ent.TruckNum);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, ent.BelongSystem);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除配载司机合同
        /// </summary>
        /// <param name="entity"></param>
        public void DelManifest(List<DepManifestEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    it.EnSafe();
                    string strSQL = @"Update Tbl_DepManifest set CancelFlag=@CancelFlag where ContractNum=@ContractNum and BelongSystem=@BelongSystem";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, it.BelongSystem.Trim());
                        NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, it.ContractNum.Trim());
                        NewaySql.AddInParameter(cmdAdd, "@CancelFlag", DbType.String, it.CancelFlag);
                        NewaySql.ExecuteNonQuery(cmdAdd);
                    }

                    //通过司机合同号首先清空所有运单的配载信息
                    DelAwbManifest(it.ContractNum.Trim(), it.BelongSystem);
                    //删除运单跟踪状态
                    List<AwbEntity> awblist = QueryAwbByContractNum(new AwbEntity { ContractNum = it.ContractNum.Trim(), BelongSystem = it.BelongSystem });
                    foreach (var ie in awblist)
                    {
                        DeleteAwbStatus(ie.AwbNo, ie.AwbID, ie.BelongSystem);
                    }
                    //更新车辆信息
                    UpdateTruckCityStatus(new TruckEntity
                    {
                        DelFlag = "0",
                        TruckNum = it.TruckNum,
                        BelongSystem = it.BelongSystem,
                        CurCity = it.Dep
                    });
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除运单配载的状态
        /// </summary>
        /// <param name="entity"></param>
        public void DelAwbManifest(string ContractNum, string BelongSystem)
        {
            try
            {
                //设置订单表的删除标志为0 
                string strDel = @"UPDATE Tbl_Awb_Basic SET DelFlag=0,AwbStatus='0',TransKind='0', ContractNum='' WHERE ContractNum=@ContractNum and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, ContractNum);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 车辆合同取消审核时删除出发的运单状态
        /// </summary>
        /// <param name="awbno"></param>
        public void DeleteAwbStatus(string awbno, Int64 awbid, string BelongSystem)
        {
            try
            {
                string strSQL = @"DELETE FROM Tbl_AwbStatusTruck Where AwbNo=@AwbNo and AwbID=@AwbID and TruckFlag=@TruckFlag and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "BelongSystem", DbType.String, BelongSystem);
                    NewaySql.AddInParameter(cmd, "AwbNo", DbType.String, awbno);
                    NewaySql.AddInParameter(cmd, "AwbID", DbType.Int64, awbid);
                    NewaySql.AddInParameter(cmd, "TruckFlag", DbType.String, "1");
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 启用作废的合同
        /// </summary>
        /// <param name="entity"></param>
        public void RenewManifest(List<DepManifestEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    it.EnSafe();
                    string strSQL = @"Update Tbl_DepManifest set CancelFlag=@CancelFlag where ContractNum=@ContractNum and BelongSystem=@BelongSystem";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, it.BelongSystem.Trim());
                        NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, it.ContractNum.Trim());
                        NewaySql.AddInParameter(cmdAdd, "@CancelFlag", DbType.String, it.CancelFlag);
                        NewaySql.ExecuteNonQuery(cmdAdd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// 修改司机合同，配载信息，运单司机合同号
        /// </summary>
        /// <param name="order"></param>
        public void UpdateContractInfo(DepManifestEntity awb)
        {
            try
            {
                awb.EnSafe();
                string strSQL = @"Update Tbl_DepManifest set Dep=@Dep,Transit=@Transit,Dest=@Dest,TruckNum=@TruckNum,TransportFee=@TransportFee,PrepayFee=@PrepayFee,ArriveFee=@ArriveFee,PayMode=@PayMode,StartTime=@StartTime,PassTime= @PassTime,Weight=@Weight,Volume=@Volume,Remark=@Remark,OP_ID=@OP_ID,DriverCellPhone=@DriverCellPhone,DriverIDNum=@DriverIDNum,DriverIDAddress=@DriverIDAddress,Driver=@Driver,DestPeople=@DestPeople,DestCellphone=@DestCellphone,DelFlag= @DelFlag,PreArriveTime=@PreArriveTime,Loader=@Loader,CardBank=@CardBank,CardName=@CardName,CardNum=@CardNum,Dispatcher=@Dispatcher Where ContractNum=@ContractNum and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, awb.ContractNum);
                    NewaySql.AddInParameter(cmdAdd, "@Dep", DbType.String, awb.Dep);
                    NewaySql.AddInParameter(cmdAdd, "@Transit", DbType.String, awb.Transit);
                    NewaySql.AddInParameter(cmdAdd, "@Dest", DbType.String, awb.Dest);
                    NewaySql.AddInParameter(cmdAdd, "@TruckNum", DbType.String, awb.TruckNum);
                    NewaySql.AddInParameter(cmdAdd, "@Weight", DbType.Decimal, awb.Weight);
                    NewaySql.AddInParameter(cmdAdd, "@Volume", DbType.Decimal, awb.Volume);
                    NewaySql.AddInParameter(cmdAdd, "@TransportFee", DbType.Decimal, awb.TransportFee);
                    NewaySql.AddInParameter(cmdAdd, "@PrepayFee", DbType.Decimal, awb.PrepayFee);
                    NewaySql.AddInParameter(cmdAdd, "@ArriveFee", DbType.Decimal, awb.ArriveFee);
                    NewaySql.AddInParameter(cmdAdd, "@PassTime", DbType.Decimal, awb.PassTime);
                    NewaySql.AddInParameter(cmdAdd, "@PayMode", DbType.String, awb.PayMode);
                    NewaySql.AddInParameter(cmdAdd, "@StartTime", DbType.DateTime, awb.StartTime);
                    NewaySql.AddInParameter(cmdAdd, "@Remark", DbType.String, awb.Remark);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, awb.OP_ID);
                    NewaySql.AddInParameter(cmdAdd, "@DriverCellPhone", DbType.String, awb.DriverCellPhone);
                    NewaySql.AddInParameter(cmdAdd, "@Driver", DbType.String, awb.Driver);
                    NewaySql.AddInParameter(cmdAdd, "@DriverIDNum", DbType.String, awb.DriverIDNum);
                    NewaySql.AddInParameter(cmdAdd, "@DriverIDAddress", DbType.String, awb.DriverIDAddress);
                    NewaySql.AddInParameter(cmdAdd, "@DestPeople", DbType.String, awb.DestPeople);
                    NewaySql.AddInParameter(cmdAdd, "@DestCellphone", DbType.String, awb.DestCellphone);
                    NewaySql.AddInParameter(cmdAdd, "@DelFlag", DbType.String, awb.DelFlag);
                    NewaySql.AddInParameter(cmdAdd, "@PreArriveTime", DbType.DateTime, awb.PreArriveTime);
                    NewaySql.AddInParameter(cmdAdd, "@Loader", DbType.String, awb.Loader);
                    NewaySql.AddInParameter(cmdAdd, "@Dispatcher", DbType.String, awb.Dispatcher);
                    NewaySql.AddInParameter(cmdAdd, "@CardBank", DbType.String, awb.CardBank);
                    NewaySql.AddInParameter(cmdAdd, "@CardName", DbType.String, awb.CardName);
                    NewaySql.AddInParameter(cmdAdd, "@CardNum", DbType.String, awb.CardNum);
                    NewaySql.ExecuteNonQuery(cmdAdd);

                }
                //删除运单跟踪状态
                List<AwbEntity> awblist = QueryAwbByContractNum(new AwbEntity { ContractNum = awb.ContractNum.Trim(), BelongSystem = awb.BelongSystem });
                //通过司机合同号首先清空所有运单的配载信息
                DelAwbManifest(awb.ContractNum.Trim(), awb.BelongSystem);
                //更新运单的配载合同号
                SetAwbManifest(awb.AwbInfo);

                foreach (var ie in awblist)
                {
                    DeleteAwbStatus(ie.AwbNo, ie.AwbID, awb.BelongSystem);
                }
                //如果银行卡号不为空更新司机银行卡号
                if (!string.IsNullOrEmpty(awb.CardNum.Trim()))
                {
                    if (!IsExistDriverCard(new DriverCardEntity { TruckNum = awb.TruckNum.Trim(), CardNum = awb.CardNum.Trim(), CardName = awb.CardName.Trim(), BelongSystem = awb.BelongSystem }))
                    {
                        SaveDriverCard(new DriverCardEntity
                        {
                            TruckNum = awb.TruckNum.Trim(),
                            CardBank = awb.CardBank.Trim(),
                            CardName = awb.CardName.Trim(),
                            CardNum = awb.CardNum.Trim(),
                            BelongSystem = awb.BelongSystem,
                            OP_ID = awb.OP_ID.Trim()
                        });
                    }
                }
                //增加运单跟踪状态
                foreach (var ie in awb.AwbInfo)
                {
                    InsertAwbStatus(new AwbStatus
                    {
                        AwbID = ie.AwbID,
                        AwbNo = ie.AwbNo,
                        TruckFlag = "1",
                        CurrentLocation = awb.Dep,
                        LastHour = awb.PassTime,
                        OP_ID = awb.OP_ID,
                        BelongSystem = awb.BelongSystem,
                        DetailInfo = ""
                    });
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
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
            List<DepManifestEntity> result = new List<DepManifestEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.StartTime DESC) AS RowNumber,a.*,RTRIM(a.Dep)+'/'+RTRIM(a.Dest) AS Range,b.FilePath,b.TbFilePath  FROM Tbl_DepManifest AS a left join Tbl_Dep_Files as b on a.ContractNum=b.ContractNum and a.BelongSystem=b.BelongSystem and b.FMode='0'  Where (1=1) and a.DelFlag=1 and a.TransKind=0";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                if (!string.IsNullOrEmpty(entity.TruckFlag)) { strSQL += " and a.TruckFlag = '" + entity.TruckFlag + "'"; }
                if (!string.IsNullOrEmpty(entity.ContractNum)) { strSQL += " and a.ContractNum = '" + entity.ContractNum + "'"; }
                //if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and b.AwbNo = '" + entity.AwbNo + "'"; }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dest in ('" + res + "')";
                }
                //按车牌号查询
                if (!string.IsNullOrEmpty(entity.TruckNum)) { strSQL += " and a.TruckNum like '%" + entity.TruckNum + "%'"; }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            if (result.Where(c => c.ContractNum.Equals(Convert.ToString(idr["ContractNum"]).Trim())).Count() > 0) { continue; }
                            result.Add(new DepManifestEntity
                            {
                                ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                TruckNum = Convert.ToString(idr["TruckNum"]),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Range = Convert.ToString(idr["Range"]).Trim(),
                                //Transit = Convert.ToString(idr["TransitT"]).Trim(),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                PrepayFee = Convert.ToDecimal(idr["PrepayFee"]),
                                ArriveFee = Convert.ToDecimal(idr["ArriveFee"]),
                                PassTime = Convert.ToDecimal(idr["PassTime"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                //Length = Convert.ToDecimal(idr["Length"]),
                                //TruckWeight = Convert.ToDecimal(idr["TruckWeight"]),
                                StartTime = Convert.ToDateTime(idr["StartTime"]),
                                PayMode = Convert.ToString(idr["PayMode"]).Trim(),
                                Driver = Convert.ToString(idr["Driver"]).Trim(),
                                DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                DestPeople = Convert.ToString(idr["DestPeople"]).Trim(),
                                DestCellphone = Convert.ToString(idr["DestCellphone"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                PreArriveTime = Convert.ToString(idr["TruckFlag"]).Equals("3") ? Convert.ToDateTime(idr["ActArriveTime"]) : Convert.ToDateTime(idr["PreArriveTime"]),
                                TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                ContractStatus = Convert.ToString(idr["ContractStatus"]).Trim(),
                                ContractURL = Convert.ToString(idr["ContractStatus"]).Trim() == "0" ? "" : Convert.ToString(idr["FilePath"]),
                                //ContractURL = Convert.ToString(idr["ContractStatus"]).Trim() == "0" ? "" : "<a href=../" + Convert.ToString(idr["FilePath"]) + " target=\"_blank\">车辆合同</a>"
                            });
                        }
                    }
                }

                resHT["rows"] = result;
                string strCount = @"Select Count(a.ContractNum) as TotalCount from Tbl_DepManifest as a  Where (1=1) and a.DelFlag=1 and a.TransKind=0";
                strCount += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //查询所有出发和在途的
                if (!string.IsNullOrEmpty(entity.TruckFlag)) { strCount += " and a.TruckFlag = '" + entity.TruckFlag + "'"; }
                if (!string.IsNullOrEmpty(entity.ContractNum)) { strCount += " and a.ContractNum = '" + entity.ContractNum + "'"; }
                //if (!string.IsNullOrEmpty(entity.AwbNo)) { strCount += " and b.AwbNo = '" + entity.AwbNo + "'"; }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and a.Dest in ('" + res + "')";
                }
                //按车牌号查询
                if (!string.IsNullOrEmpty(entity.TruckNum)) { strCount += " and a.TruckNum like '%" + entity.TruckNum + "%'"; }
                //预计到达时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 查询车辆的跟踪信息
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public DataTable QueryTruckStatusTrack(TruckStatusTrackEntity ent)
        {
            DataTable result = new DataTable();
            try
            {
                string strSQL = @"SELECT a.*,b.UserName FROM Tbl_TruckStatusTrack as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem WHERE a.ContractNum=@ContractNum and a.BelongSystem=@BelongSystem ORDER BY a.OP_DATE DESC";

                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, ent.BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, ent.ContractNum);
                    result = NewaySql.ExecuteDataTable(cmdAdd);
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 保存车辆状态跟踪信息
        /// </summary>
        /// <param name="ent"></param>
        public void SaveTruckStatusTrack(TruckStatusTrackEntity ent)
        {
            try
            {
                ent.EnSafe();
                string strSQL = @"INSERT INTO Tbl_TruckStatusTrack(ContractNum,TruckNum,TruckFlag,CurrentLocation,LastHour,DetailInfo,OP_ID,ArriveTime,BelongSystem) VALUES (@ContractNum,@TruckNum,@TruckFlag,@CurrentLocation,@LastHour,@DetailInfo,@OP_ID,@ArriveTime,@BelongSystem)";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, ent.BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, ent.ContractNum);
                    NewaySql.AddInParameter(cmdAdd, "@TruckNum", DbType.String, ent.TruckNum);
                    NewaySql.AddInParameter(cmdAdd, "@TruckFlag", DbType.String, ent.TruckFlag);
                    NewaySql.AddInParameter(cmdAdd, "@CurrentLocation", DbType.String, ent.CurrentLocation);
                    NewaySql.AddInParameter(cmdAdd, "@LastHour", DbType.Decimal, ent.LastHour);
                    NewaySql.AddInParameter(cmdAdd, "@DetailInfo", DbType.String, ent.DetailInfo);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, ent.OP_ID);
                    NewaySql.AddInParameter(cmdAdd, "@ArriveTime", DbType.DateTime, ent.ArriveTime);

                    NewaySql.ExecuteNonQuery(cmdAdd);
                }

                ///更新运单的配载车辆状态
                UpdateManifest(new DepManifestEntity
                {
                    TruckFlag = ent.TruckFlag,
                    ContractNum = ent.ContractNum,
                    ActArriveTime = ent.ArriveTime,
                    BelongSystem = ent.BelongSystem,
                    TruckNum = ent.TruckNum
                });
                //修改运单状态设置运单的状态为完成和结束
                SetAwbStatus(new AwbEntity
                {
                    AwbStatus = ent.TruckFlag,
                    DelFlag = ent.TruckFlag,
                    ArriveDate = ent.ArriveTime,
                    BelongSystem = ent.BelongSystem,
                    ContractNum = ent.ContractNum
                });
                //修改车辆状态
                UpdateTruckStatus(new TruckEntity
                {
                    TruckNum = ent.TruckNum,
                    BelongSystem = ent.BelongSystem,
                    TruckFlag = "0"
                });

                //增加运单跟踪状态
                List<AwbEntity> awblist = QueryAwbByContractNum(new AwbEntity { ContractNum = ent.ContractNum.Trim(), BelongSystem = ent.BelongSystem });
                foreach (var ie in awblist)
                {
                    InsertAwbStatus(new AwbStatus
                    {
                        AwbID = ie.AwbID,
                        AwbNo = ie.AwbNo,
                        TruckFlag = ent.TruckFlag,
                        CurrentLocation = ent.CurrentLocation,
                        LastHour = ent.LastHour,
                        OP_ID = ent.OP_ID,
                        BelongSystem = ent.BelongSystem,
                        DetailInfo = ent.DetailInfo,
                        ArriveTime = ent.ArriveTime
                    });
                }
                //如果是到达就新增到达运单表
                if (ent.TruckFlag.Equals("3"))
                {
                    AddArriveAwbByAwbBasic(ent.ContractNum.Trim(), ent.BelongSystem);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 设置司机合同的状态
        /// </summary>
        /// <param name="ent"></param>
        public void UpdateManifest(DepManifestEntity order)
        {
            try
            {
                order.EnSafe();
                //设置订单表的删除标志为1 表示删除或作废
                string strDel = @"UPDATE Tbl_DepManifest SET ";
                strDel += "TruckFlag=@TruckFlag";
                if (order.TruckFlag.Trim().Equals("3"))
                {
                    strDel += " ,ActArriveTime=@ActArriveTime ";
                }
                strDel += @" WHERE ContractNum=@ContractNum and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, order.ContractNum);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, order.BelongSystem);
                    if (order.TruckFlag.Trim().Equals("3"))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@ActArriveTime", DbType.DateTime, order.ActArriveTime);
                    }
                    NewaySql.AddInParameter(cmdAdd, "@TruckFlag", DbType.String, order.TruckFlag);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 修改车辆状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateTruckStatus(TruckEntity entity)
        {
            entity.EnSafe();
            try
            {
                string strSQL = @"UPDATE Tbl_Truck SET DelFlag=@DelFlag WHERE TruckNum=@TruckNum and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    NewaySql.AddInParameter(cmd, "@TruckNum", DbType.String, entity.TruckNum);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 向到达运单表新增数据
        /// </summary>
        /// <param name="contractnum"></param>
        public void AddArriveAwbByAwbBasic(string contractnum, string BelongSystem)
        {
            string strSQL = @"insert into Tbl_Awb_Arrive(AwbID,AwbNo,HAwbNo,Dep,Dest,Piece,Weight,Volume,AwbPiece,AwbWeight,AwbVolume,Attach,InsuranceFee,TransitFee,TransportFee
,DeliverFee,OtherFee,TotalCharge,NowPay,PickPay,CheckOutType,CollectMoney,ReturnAwb,TrafficType,DeliveryType,SteveDore,ShipperName,ShipperUnit
,ShipperAddress,ShipperTelephone,ShipperCellphone,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,PrintNum
,HandleTime,Remark,OP_ID,OP_DATE,DelFlag,ContractNum,ReturnStatus,ReturnInfo,ArriveDate,Rebate,TransKind,AwbStatus,ActMoney
,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,CreateAwb,CreateDate,FinanceFirstCheck
,FirstCheckName,FirstCheckDate,SecondCheckName,SecondCheckDate,FinanceSecondCheck,AccountID,CheckStatus,Transit,Signer,SignTime,TimeLimit,MidDest,BelongSystem) select AwbID,AwbNo,HAwbNo,Dep,Dest,Piece,Weight,Volume,AwbPiece,AwbWeight,AwbVolume,Attach,InsuranceFee,TransitFee,TransportFee
,DeliverFee,OtherFee,TotalCharge,NowPay,PickPay,CheckOutType,CollectMoney,ReturnAwb,TrafficType,DeliveryType,SteveDore,ShipperName,ShipperUnit
,ShipperAddress,ShipperTelephone,ShipperCellphone,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,PrintNum
,HandleTime,Remark,OP_ID,OP_DATE,DelFlag,ContractNum,ReturnStatus,ReturnInfo,ArriveDate,Rebate,TransKind,AwbStatus,ActMoney
,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,CreateAwb,CreateDate,FinanceFirstCheck
,FirstCheckName,FirstCheckDate,SecondCheckName,SecondCheckDate,FinanceSecondCheck,AccountID,CheckStatus,Transit,Signer,SignTime,TimeLimit,MidDest,BelongSystem from Tbl_Awb_Basic where ContractNum=@ContractNum and BelongSystem=@BelongSystem";
            try
            {
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@ContractNum", DbType.String, contractnum);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, BelongSystem);
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
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
            List<DepManifestEntity> result = new List<DepManifestEntity>();
            

            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.*,RTRIM(a.Dep)+'/'+RTRIM(a.Dest) AS Range  FROM Tbl_DepManifest AS a left join Tbl_Awb_Basic as c on a.ContractNum=c.ContractNum Where a.DelFlag=1 and a.TransKind=0";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //查询所有已审核过的到达的车辆
                strSQL += " and (a.TruckFlag = '3' or a.TruckFlag = '4')";
                //司机合同号
                if (!string.IsNullOrEmpty(entity.ContractNum))
                {
                    strSQL += " and a.ContractNum='" + entity.ContractNum + "'";
                }
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strSQL += " and c.AwbNo='" + entity.AwbNo + "'";
                }
                //按车牌号查询
                if (!string.IsNullOrEmpty(entity.TruckNum))
                {
                    strSQL += " and a.TruckNum='" + entity.TruckNum + "'";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and ( a.Dest in ('" + res + "') or a.Transit in ('" + res + "') )";
                }
                //预计到达时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new DepManifestEntity
                            {
                                ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                TruckNum = Convert.ToString(idr["TruckNum"]),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Range = Convert.ToString(idr["Range"]).Trim(),
                                Transit = Convert.ToString(idr["Transit"]).Trim(),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                PrepayFee = Convert.ToDecimal(idr["PrepayFee"]),
                                ArriveFee = Convert.ToDecimal(idr["ArriveFee"]),
                                PassTime = Convert.ToDecimal(idr["PassTime"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                //Length = Convert.ToDecimal(idr["Length"]),
                                // TruckWeight = Convert.ToDecimal(idr["Length"]),
                                StartTime = Convert.ToDateTime(idr["StartTime"]),
                                PayMode = Convert.ToString(idr["PayMode"]).Trim(),
                                Driver = Convert.ToString(idr["Driver"]).Trim(),
                                DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                DestPeople = Convert.ToString(idr["DestPeople"]).Trim(),
                                DestCellphone = Convert.ToString(idr["DestCellphone"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"]),
                                TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim()
                            });
                        }
                    }
                }
                resHT["rows"] = result;
                string strCount = @"Select Count(*) as TotalCount from ( Select DISTINCT a.* from Tbl_DepManifest as a left join Tbl_Awb_Basic as c on a.ContractNum=c.ContractNum Where a.DelFlag=1 and a.TransKind=0";
                strCount += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //查询所有已审核过的到达和结束的车辆
                strCount += " and (a.TruckFlag = '3' or a.TruckFlag = '4')";
                //司机合同号
                if (!string.IsNullOrEmpty(entity.ContractNum))
                {
                    strCount += " and a.ContractNum='" + entity.ContractNum + "'";
                }
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strCount += " and c.AwbNo='" + entity.AwbNo + "'";
                }
                //按车牌号查询
                if (!string.IsNullOrEmpty(entity.TruckNum))
                {
                    strCount += " and a.TruckNum='" + entity.TruckNum + "'";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++) { res += ccs[i] + "','"; }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and a.Dest in ('" + res + "')";
                }
                //预计到达时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strCount += ") as d";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 更新回单信息
        /// </summary>
        /// <param name="awb"></param>
        public void AddReturnAwbInfo(List<AwbEntity> awblist)
        {
            
            try
            {
                foreach (var awb in awblist)
                {
                    awb.EnSafe();
                    string strSQL = @"UPDATE Tbl_Awb_Basic ";
                    if (awb.TransKind.Equals("1"))
                    {
                        strSQL += "SET ReturnInfo=@ReturnInfo,ReturnStatus=@ReturnStatus,ActMoney=@ActMoney,ReturnDate=@ReturnDate,AwbStatus=@AwbStatus,SendReturnAwbStatus=@SendReturnAwbStatus,SendReturnAwbDate=@SendReturnAwbDate,ConfirmReturnAwbStatus=@ConfirmReturnAwbStatus,ConfirmReturnAwbDate=@ConfirmReturnAwbDate,Confirmer=@Confirmer,ConfirmerName=@ConfirmerName";
                    }
                    else
                    {
                        strSQL += "SET ReturnInfo=@ReturnInfo,ReturnStatus=@ReturnStatus,ActMoney=@ActMoney,ReturnDate=@ReturnDate,AwbStatus=@AwbStatus,Signer=@Signer,SignTime=@SignTime,DelayReason=@DelayReason";
                    }
                    if (awb.UploadReturnPic.Equals("1"))
                    {
                        strSQL += " ,UploadReturnPic=@UploadReturnPic ";
                    }
                    strSQL += " WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                    {
                        if (awb.TransKind.Equals("1"))
                        {
                            NewaySql.AddInParameter(cmdAdd, "@ActMoney", DbType.Decimal, awb.ActMoney);
                            NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem);
                            NewaySql.AddInParameter(cmdAdd, "@ReturnInfo", DbType.String, awb.ReturnInfo);
                            NewaySql.AddInParameter(cmdAdd, "@ReturnStatus", DbType.String, awb.ReturnStatus);
                            NewaySql.AddInParameter(cmdAdd, "@ReturnDate", DbType.DateTime, DateTime.Now);
                            NewaySql.AddInParameter(cmdAdd, "@AwbStatus", DbType.String, awb.AwbStatus);
                            NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awb.AwbID);
                            NewaySql.AddInParameter(cmdAdd, "@SendReturnAwbStatus", DbType.String, "Y");
                            NewaySql.AddInParameter(cmdAdd, "@SendReturnAwbDate", DbType.DateTime, awb.SignTime);
                            NewaySql.AddInParameter(cmdAdd, "@ConfirmReturnAwbStatus", DbType.String, "Y");
                            NewaySql.AddInParameter(cmdAdd, "@ConfirmReturnAwbDate", DbType.DateTime, awb.SignTime);
                            NewaySql.AddInParameter(cmdAdd, "@Confirmer", DbType.String, awb.Confirmer);
                            NewaySql.AddInParameter(cmdAdd, "@ConfirmerName", DbType.String, awb.ConfirmerName);
                        }
                        else
                        {
                            NewaySql.AddInParameter(cmdAdd, "@ActMoney", DbType.Decimal, awb.ActMoney);
                            NewaySql.AddInParameter(cmdAdd, "@ReturnInfo", DbType.String, awb.ReturnInfo);
                            NewaySql.AddInParameter(cmdAdd, "@ReturnStatus", DbType.String, awb.ReturnStatus);
                            NewaySql.AddInParameter(cmdAdd, "@ReturnDate", DbType.DateTime, DateTime.Now);
                            NewaySql.AddInParameter(cmdAdd, "@AwbStatus", DbType.String, awb.AwbStatus);
                            NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awb.AwbID);
                            NewaySql.AddInParameter(cmdAdd, "@Signer", DbType.String, awb.Signer);
                            NewaySql.AddInParameter(cmdAdd, "@DelayReason", DbType.String, awb.DelayReason);
                            NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem);
                            NewaySql.AddInParameter(cmdAdd, "@SignTime", DbType.DateTime, awb.SignTime);
                        }
                        if (awb.UploadReturnPic.Equals("1"))
                        {
                            NewaySql.AddInParameter(cmdAdd, "@UploadReturnPic", DbType.String, awb.UploadReturnPic);
                        }
                        NewaySql.ExecuteNonQuery(cmdAdd);
                    }
                    //修改到达运单的状态
                    string strSQLArrive = @"UPDATE Tbl_Awb_Arrive  ";
                    if (awb.TransKind.Equals("1"))
                    {
                        strSQLArrive += "SET ReturnInfo=@ReturnInfo,ReturnStatus=@ReturnStatus,ActMoney=@ActMoney,ReturnDate=@ReturnDate,AwbStatus=@AwbStatus,SendReturnAwbStatus=@SendReturnAwbStatus,SendReturnAwbDate=@SendReturnAwbDate,ConfirmReturnAwbStatus=@ConfirmReturnAwbStatus,ConfirmReturnAwbDate=@ConfirmReturnAwbDate";
                    }
                    else
                    {
                        strSQLArrive += "SET ReturnInfo=@ReturnInfo,ReturnStatus=@ReturnStatus,ActMoney=@ActMoney,ReturnDate=@ReturnDate,AwbStatus=@AwbStatus,Signer=@Signer,SignTime=@SignTime,DelayReason=@DelayReason";
                    }
                    if (awb.UploadReturnPic.Equals("1"))
                    {
                        strSQLArrive += " ,UploadReturnPic=@UploadReturnPic ";
                    }
                    strSQLArrive += " WHERE AwbID=@AwbID and BelongSystem=@BelongSystem";
                    using (DbCommand cmdAddArrive = NewaySql.GetSqlStringCommond(strSQLArrive))
                    {
                        if (awb.TransKind.Equals("1"))
                        {
                            NewaySql.AddInParameter(cmdAddArrive, "@ActMoney", DbType.Decimal, awb.ActMoney);
                            NewaySql.AddInParameter(cmdAddArrive, "@ReturnInfo", DbType.String, awb.ReturnInfo);
                            NewaySql.AddInParameter(cmdAddArrive, "@ReturnStatus", DbType.String, awb.ReturnStatus);
                            NewaySql.AddInParameter(cmdAddArrive, "@ReturnDate", DbType.DateTime, DateTime.Now);
                            NewaySql.AddInParameter(cmdAddArrive, "@AwbStatus", DbType.String, awb.AwbStatus);
                            NewaySql.AddInParameter(cmdAddArrive, "@AwbID", DbType.Int64, awb.AwbID);
                            NewaySql.AddInParameter(cmdAddArrive, "@SendReturnAwbStatus", DbType.String, "Y");
                            NewaySql.AddInParameter(cmdAddArrive, "@SendReturnAwbDate", DbType.DateTime, awb.SignTime);
                            NewaySql.AddInParameter(cmdAddArrive, "@ConfirmReturnAwbStatus", DbType.String, "Y");
                            NewaySql.AddInParameter(cmdAddArrive, "@BelongSystem", DbType.String, awb.BelongSystem);
                            NewaySql.AddInParameter(cmdAddArrive, "@ConfirmReturnAwbDate", DbType.DateTime, awb.SignTime);
                        }
                        else
                        {
                            NewaySql.AddInParameter(cmdAddArrive, "@BelongSystem", DbType.String, awb.BelongSystem);
                            NewaySql.AddInParameter(cmdAddArrive, "@ActMoney", DbType.Decimal, awb.ActMoney);
                            NewaySql.AddInParameter(cmdAddArrive, "@ReturnInfo", DbType.String, awb.ReturnInfo);
                            NewaySql.AddInParameter(cmdAddArrive, "@ReturnStatus", DbType.String, awb.ReturnStatus);
                            NewaySql.AddInParameter(cmdAddArrive, "@ReturnDate", DbType.DateTime, DateTime.Now);
                            NewaySql.AddInParameter(cmdAddArrive, "@AwbStatus", DbType.String, awb.AwbStatus);
                            NewaySql.AddInParameter(cmdAddArrive, "@AwbID", DbType.Int64, awb.AwbID);
                            NewaySql.AddInParameter(cmdAddArrive, "@Signer", DbType.String, awb.Signer);
                            NewaySql.AddInParameter(cmdAddArrive, "@DelayReason", DbType.String, awb.DelayReason);
                            NewaySql.AddInParameter(cmdAddArrive, "@SignTime", DbType.DateTime, awb.SignTime);
                        }
                        if (awb.UploadReturnPic.Equals("1"))
                        {
                            NewaySql.AddInParameter(cmdAddArrive, "@UploadReturnPic", DbType.String, awb.UploadReturnPic);
                        }
                        NewaySql.ExecuteNonQuery(cmdAddArrive);
                    }
                    if (awb.TransKind.Equals("1"))
                    {
                        //运单状态
                        InsertAwbStatus(new AwbStatus
                        {
                            AwbID = awb.AwbID,
                            AwbNo = awb.AwbNo,
                            TruckFlag = "4",
                            Signer = awb.Signer,
                            SignTime = awb.SignTime,
                            ArriveTime = awb.SignTime,
                            CurrentLocation = awb.Transit,
                            LastHour = 0,
                            DetailInfo = "",
                            Longitude = awb.Longitude,
                            Latitude = awb.Latitude,
                            BelongSystem = awb.BelongSystem,
                            OP_ID = awb.OP_ID
                        });
                    }
                    else
                    {
                        //运单状态
                        InsertAwbStatus(new AwbStatus
                        {
                            AwbID = awb.AwbID,
                            AwbNo = awb.AwbNo,
                            TruckFlag = awb.AwbStatus,
                            Signer = awb.Signer,
                            SignTime = awb.SignTime,
                            ArriveTime = awb.SignTime,
                            CurrentLocation = awb.Transit,
                            LastHour = 0,
                            DetailInfo = "",
                            Longitude = awb.Longitude,
                            Latitude = awb.Latitude,
                            BelongSystem = awb.BelongSystem,
                            OP_DATE = DateTime.Now,
                            OP_ID = awb.OP_ID
                        });
                    }
                    //保存运单的回单照片
                    if (awb.AwbFiles != null && awb.AwbFiles.Count > 0) { SaveReturnAwbFile(awb.AwbFiles); }
                }
                string contract = awblist[0].ContractNum;
                if (!string.IsNullOrEmpty(contract))
                {
                    if (IsHasAllReturnAwb(contract, awblist[0].BelongSystem))
                    {
                        string strSQL = @"UPDATE Tbl_DepManifest SET ReturnStatus='Y' WHERE ContractNum=@ContractNum and BelongSystem=@BelongSystem";
                        using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                        {
                            NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awblist[0].BelongSystem);
                            NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, contract);
                            NewaySql.ExecuteNonQuery(cmdAdd);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 保存运单的回单照片文件
        /// </summary>
        /// <param name="entity"></param>
        public void SaveReturnAwbFile(List<AwbFilesEntity> entity)
        {
            
            try
            {
                foreach (var it in entity)
                {
                    it.EnSafe();
                    //设置订单表的删除标志为2 表示配载了就不能再修改
                    string strDel = @"INSERT INTO Tbl_Awb_Files(AwbID,AwbNo,FileName,FileType,FileSize,FilePath,TbFilePath,OP_ID,BelongSystem) VALUES(@AwbID,@AwbNo,@FileName,@FileType,@FileSize,@FilePath,@TbFilePath,@OP_ID,@BelongSystem)";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, it.AwbID);
                        NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, it.BelongSystem);
                        NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, it.AwbNo);
                        NewaySql.AddInParameter(cmdAdd, "@FileName", DbType.String, it.FileName);
                        NewaySql.AddInParameter(cmdAdd, "@FileType", DbType.String, it.FileType);
                        NewaySql.AddInParameter(cmdAdd, "@FileSize", DbType.Int32, it.FileSize);
                        NewaySql.AddInParameter(cmdAdd, "@FilePath", DbType.String, it.FilePath);
                        NewaySql.AddInParameter(cmdAdd, "@TbFilePath", DbType.String, it.TbFilePath);
                        NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, it.OP_ID);
                        NewaySql.ExecuteNonQuery(cmdAdd);
                    }
                }
                //修改运单的回单上传照片状态
                UpdateUploadReturnPic(new AwbEntity { AwbNo = entity[0].AwbNo, AwbID = entity[0].AwbID, UploadReturnPic = "1" });
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 修改运单的上传回单照片状态
        /// </summary>
        /// <param name="entity"></param>
        private void UpdateUploadReturnPic(AwbEntity entity)
        {
            
            try
            {
                entity.EnSafe();
                string strDel = @"UPDATE Tbl_Awb_Basic SET UploadReturnPic=@UploadReturnPic WHERE AwbID=@AwbID and AwbNo=@AwbNo";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, entity.AwbID);
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, entity.AwbNo);
                    NewaySql.AddInParameter(cmdAdd, "@UploadReturnPic", DbType.String, entity.UploadReturnPic);
                    NewaySql.ExecuteNonQuery(cmdAdd);

                }
                //修改到达运单的状态
                string strSQLArrive = @"UPDATE Tbl_Awb_Arrive SET UploadReturnPic=@UploadReturnPic WHERE AwbID=@AwbID and AwbNo=@AwbNo";
                using (DbCommand cmdAddArrive = NewaySql.GetSqlStringCommond(strSQLArrive))
                {
                    NewaySql.AddInParameter(cmdAddArrive, "@AwbID", DbType.Int64, entity.AwbID);
                    NewaySql.AddInParameter(cmdAddArrive, "@AwbNo", DbType.String, entity.AwbNo);
                    NewaySql.AddInParameter(cmdAddArrive, "@UploadReturnPic", DbType.String, entity.UploadReturnPic);
                    NewaySql.ExecuteNonQuery(cmdAddArrive);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增运单状态
        /// </summary>
        /// <param name="entity"></param>
        public void InsertAwbStatus(AwbStatus entity)
        {
            
            try
            {
                entity.EnSafe();
                string strSQL = @"INSERT INTO Tbl_AwbStatusTruck(AwbNo,AwbID,TruckFlag,CurrentLocation,LastHour,";
                if (entity.ArriveTime != null)
                {
                    strSQL += "ArriveTime,";
                }
                strSQL += "DetailInfo,OP_ID,Signer,BelongSystem,OP_DATE,Latitude,Longitude) VALUES(@AwbNo,@AwbID,@TruckFlag,@CurrentLocation,@LastHour,";
                if (entity.ArriveTime != null)
                {
                    strSQL += "@ArriveTime,";
                }
                strSQL += "@DetailInfo,@OP_ID,@Signer,@BelongSystem,@OP_DATE,@Latitude,@Longitude)";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "AwbNo", DbType.String, entity.AwbNo);
                    NewaySql.AddInParameter(cmd, "AwbID", DbType.Int64, entity.AwbID);
                    NewaySql.AddInParameter(cmd, "TruckFlag", DbType.String, entity.TruckFlag);
                    NewaySql.AddInParameter(cmd, "CurrentLocation", DbType.String, entity.CurrentLocation);
                    NewaySql.AddInParameter(cmd, "LastHour", DbType.Decimal, entity.LastHour);
                    if (entity.ArriveTime != null)
                    {
                        NewaySql.AddInParameter(cmd, "ArriveTime", DbType.DateTime, entity.ArriveTime);
                    }
                    NewaySql.AddInParameter(cmd, "DetailInfo", DbType.String, entity.DetailInfo);
                    NewaySql.AddInParameter(cmd, "OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.AddInParameter(cmd, "Signer", DbType.String, entity.Signer);
                    NewaySql.AddInParameter(cmd, "BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(cmd, "Latitude", DbType.String, entity.Latitude);
                    NewaySql.AddInParameter(cmd, "Longitude", DbType.String, entity.Longitude);
                    if (null != entity.OP_DATE && entity.OP_DATE.ToString("yyyy-MM-dd") != "0001-01-01" && entity.OP_DATE.ToString("yyyy-MM-dd") != "1900-01-01" && entity.OP_DATE.ToString("yyyy-MM-dd") != "1901-01-01")
                    {
                        NewaySql.AddInParameter(cmd, "OP_DATE", DbType.DateTime, entity.OP_DATE);
                    }
                    else
                    {
                        NewaySql.AddInParameter(cmd, "OP_DATE", DbType.DateTime, DateTime.Now);
                    }
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 通过司机合同号判断该合同下的所有运单是否有签过回单
        /// True:全都有回单，False：有的没有回单
        /// </summary>
        /// <param name="ContractNum"></param>
        /// <returns></returns>
        public bool IsHasAllReturnAwb(string ContractNum, string BelongSystem)
        {
            bool result = true;
            

            string strSQL = @"SELECT ReturnStatus FROM Tbl_Awb_Basic WHERE ContractNum=@ContractNum and BelongSystem=@BelongSystem";
            using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
            {
                NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, BelongSystem);
                NewaySql.AddInParameter(command, "@ContractNum", DbType.String, ContractNum);
                using (DataTable dt = NewaySql.ExecuteDataTable(command))
                {
                    if (dt == null)
                    {
                        return false;
                    }
                    foreach (DataRow idr in dt.Rows)
                    {
                        if (Convert.ToString(idr["ReturnStatus"]).Trim().ToUpper().Equals("N"))
                        {
                            result = false;
                            break;
                        }
                    }
                }

            }
            return result;
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
            List<AwbEntity> result = new List<AwbEntity>();
            

            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT a.*,DATEADD(day,a.TimeLimit,a.HandleTime) as LatestTimeLimit,b.UserName,case when Piece=0 then 0 else convert(decimal(18,2),(AwbPiece*TotalCharge/Piece)) end as FPCharge FROM Tbl_Awb_Basic as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem WHERE (1=1)";
                if (!string.IsNullOrEmpty(entity.BelongSystem))
                {
                    strSQL += " and a.BelongSystem=@BelongSystem";
                }
                if (!string.IsNullOrEmpty(entity.ContractNum))
                {
                    strSQL += " and a.ContractNum=@ContractNum";
                }
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strSQL += " and a.AwbNo=@AwbNo";
                }
                if (!string.IsNullOrEmpty(entity.TransKind))
                {
                    strSQL += " and a.TransKind=@TransKind";
                }
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    strSQL += " and a.AwbStatus=@AwbStatus";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++) { res += ccs[i] + "','"; }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dest in ('" + res + "')";
                }
                strSQL += " Order by a.HandleTime DESC,a.AwbNo Asc";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    if (!string.IsNullOrEmpty(entity.BelongSystem))
                    {
                        NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, entity.BelongSystem);
                    }
                    if (!string.IsNullOrEmpty(entity.ContractNum))
                    {
                        NewaySql.AddInParameter(command, "@ContractNum", DbType.String, entity.ContractNum);
                    }
                    if (!string.IsNullOrEmpty(entity.AwbNo))
                    {
                        NewaySql.AddInParameter(command, "@AwbNo", DbType.String, entity.AwbNo);
                    }
                    if (!string.IsNullOrEmpty(entity.TransKind))
                    {
                        NewaySql.AddInParameter(command, "@TransKind", DbType.String, entity.TransKind);
                    }
                    if (!string.IsNullOrEmpty(entity.AwbStatus))
                    {
                        NewaySql.AddInParameter(command, "@AwbStatus", DbType.String, entity.AwbStatus);
                    }
                    if (!string.IsNullOrEmpty(entity.Dest))
                    {
                        NewaySql.AddInParameter(command, "@Dest", DbType.String, entity.Dest);
                    }
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                goodList.Add(new AwbGoodsEntity
                                {
                                    GoodsID = Convert.ToInt32(idrGoods["GoodsID"]),
                                    AwbNo = Convert.ToString(idrGoods["AwbNo"]),
                                    Package = Convert.ToString(idrGoods["Package"]),
                                    Goods = Convert.ToString(idrGoods["Goods"]),
                                    Weight = Convert.ToDecimal(idrGoods["Weight"]),
                                    WeightPrice = Convert.ToDecimal(idrGoods["WeightPrice"]),
                                    Volume = Convert.ToDecimal(idrGoods["Volume"]),
                                    VolumePrice = Convert.ToDecimal(idrGoods["VolumePrice"]),
                                    Piece = Convert.ToInt32(idrGoods["Piece"]),
                                    DeclareValue = Convert.ToString(idrGoods["DeclareValue"]),
                                    OP_ID = Convert.ToString(idrGoods["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idrGoods["OP_DATE"])
                                });
                                good += Convert.ToString(idrGoods["Goods"]) + ",";
                                if (!string.IsNullOrEmpty(good)) { good = good.Substring(0, good.Length - 1); }
                            }

                            #endregion
                            #region 获取运单数据
                            AwbEntity e = new AwbEntity();
                            e.AwbID = Convert.ToInt64(idr["AwbID"]);
                            e.AwbNo = Convert.ToString(idr["AwbNo"]).Trim();
                            e.HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim();
                            e.Dep = Convert.ToString(idr["Dep"]).Trim();
                            e.Dest = Convert.ToString(idr["Dest"]).Trim();
                            e.Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]);
                            e.Weight = Convert.ToDecimal(idr["Weight"]);
                            e.Volume = Convert.ToDecimal(idr["Volume"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);
                            e.AwbWeight = Convert.ToDecimal(idr["AwbWeight"]);
                            e.AwbVolume = Convert.ToDecimal(idr["AwbVolume"]);
                            e.AwbPiece = Convert.ToInt32(idr["AwbPiece"]);
                            e.Attach = Convert.ToInt32(idr["Attach"]);
                            e.InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]);
                            e.TransitFee = Convert.ToDecimal(idr["TransitFee"]);
                            e.TransportFee = Convert.ToDecimal(idr["TransportFee"]);
                            e.DeliverFee = Convert.ToDecimal(idr["DeliverFee"]);
                            e.OtherFee = Convert.ToDecimal(idr["OtherFee"]);
                            e.TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                            e.SumCharge = Convert.ToDecimal(idr["FPCharge"]);
                            e.Rebate = Convert.ToDecimal(idr["Rebate"]);
                            e.NowPay = Convert.ToDecimal(idr["NowPay"]);
                            e.PickPay = Convert.ToDecimal(idr["PickPay"]);
                            e.CollectMoney = Convert.ToDecimal(idr["CollectMoney"]);
                            e.CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim();
                            e.ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]);
                            e.TimeLimit = Convert.ToInt32(idr["TimeLimit"]);
                            e.LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd");
                            e.TrafficType = Convert.ToString(idr["TrafficType"]).Trim();
                            e.DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim();
                            e.SteveDore = Convert.ToString(idr["SteveDore"]).Trim();
                            e.ShipperName = Convert.ToString(idr["ShipperName"]).Trim();
                            e.ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim();
                            e.ShipperAddress = Convert.ToString(idr["ShipperAddress"]).Trim();
                            e.ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim();
                            e.ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim();
                            e.ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim();
                            e.AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim();
                            e.AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim();
                            e.AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim();
                            e.AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim();
                            e.AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim();
                            e.AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim();
                            e.HandleTime = Convert.ToDateTime(idr["HandleTime"]);
                            e.Remark = Convert.ToString(idr["Remark"]).Trim();
                            e.PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 0 : Convert.ToInt16(idr["PrintNum"]);
                            e.DelFlag = Convert.ToString(idr["DelFlag"]).Trim();
                            e.CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim();
                            e.CreateDate = Convert.ToDateTime(idr["CreateDate"]);
                            e.OP_ID = Convert.ToString(idr["OP_ID"]).Trim();
                            e.UserName = Convert.ToString(idr["UserName"]).Trim();
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            e.ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim();
                            e.ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim();
                            e.ActMoney = Convert.ToDecimal(idr["ActMoney"]);
                            e.Signer = Convert.ToString(idr["Signer"]).Trim();
                            e.ContractNum = Convert.ToString(idr["ContractNum"]).Trim();
                            e.DelayReason = Convert.ToString(idr["DelayReason"]).Trim();
                            e.HLY = Convert.ToString(idr["HLY"]).Trim();
                            e.AwbGoods = goodList;
                            e.Goods = good;

                            e.SignTime = DateTime.Now;
                            if (!string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])))
                            {
                                e.SignTime = Convert.ToDateTime(idr["SignTime"]);
                            }
                            e.DelayDay = "0";
                            if (!Convert.ToInt32(idr["TimeLimit"]).Equals(9))
                            {
                                TimeSpan ts = new TimeSpan();
                                if (string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])))
                                {
                                    ts = DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd 23:59:58"));
                                }
                                else
                                {
                                    ts = Convert.ToDateTime(e.SignTime) - Convert.ToDateTime(Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd 23:59:58"));
                                }
                                if (ts.TotalHours > 0)
                                {
                                    for (int i = 1; i < 100; i++)
                                    {
                                        if (ts.TotalHours <= 12 * i && ts.TotalHours > 12 * (i - 1))
                                        {
                                            e.DelayDay = ((double)i / 2).ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                            if (IsFenPiByAwbID(Convert.ToInt64(idr["AwbID"]), entity.BelongSystem))
                            {
                                e.FP = "1";
                            }
                            result.Add(e);
                            #endregion
                        }
                    }

                }
                //result.Sort(CompareEntity);
                resHT["rows"] = result;
                resHT["total"] = result.Count.ToString();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
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
            List<AwbEntity> result = new List<AwbEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT a.*,DATEADD(day,a.TimeLimit,a.HandleTime) as LatestTimeLimit,case when Piece=0 then 0 else convert(decimal(18,2),(AwbPiece*TotalCharge/Piece)) end as FPCharge FROM Tbl_Awb_Basic as a  WHERE a.AwbStatus in ('3','8','9','6') ";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //查询运单状态为到达，配送和中转的运单
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and a.AwbNo='" + entity.AwbNo + "'"; }
                if (!string.IsNullOrEmpty(entity.ShipperUnit)) { strSQL += " and a.ShipperUnit like '%" + entity.ShipperUnit + "%'"; }
                if (!string.IsNullOrEmpty(entity.TransKind)) { strSQL += " and a.TransKind='" + entity.TransKind + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++) { res += ccs[i] + "','"; }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dest in ('" + res + "')";
                }
                strSQL += " Order by a.HandleTime DESC,a.AwbNo Asc";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                good += Convert.ToString(idrGoods["Goods"]) + ",";
                                if (!string.IsNullOrEmpty(good)) { good = good.Substring(0, good.Length - 1); }
                            }

                            #endregion
                            #region 获取运单数据
                            AwbEntity e = new AwbEntity();
                            e.AwbID = Convert.ToInt64(idr["AwbID"]);
                            e.AwbNo = Convert.ToString(idr["AwbNo"]).Trim();
                            e.HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim();
                            e.Dep = Convert.ToString(idr["Dep"]).Trim();
                            e.Dest = Convert.ToString(idr["Dest"]).Trim();
                            e.Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]);
                            e.Weight = Convert.ToDecimal(idr["Weight"]);
                            e.Volume = Convert.ToDecimal(idr["Volume"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);
                            e.AwbWeight = Convert.ToDecimal(idr["AwbWeight"]);
                            e.AwbVolume = Convert.ToDecimal(idr["AwbVolume"]);
                            e.AwbPiece = Convert.ToInt32(idr["AwbPiece"]);
                            e.InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]);
                            e.TransitFee = Convert.ToDecimal(idr["TransitFee"]);
                            e.TransportFee = Convert.ToDecimal(idr["TransportFee"]);
                            e.DeliverFee = Convert.ToDecimal(idr["DeliverFee"]);
                            e.OtherFee = Convert.ToDecimal(idr["OtherFee"]);
                            e.TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                            e.SumCharge = Convert.ToDecimal(idr["FPCharge"]);
                            e.CollectMoney = Convert.ToDecimal(idr["CollectMoney"]);
                            e.CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim();
                            e.ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]);
                            e.TimeLimit = Convert.ToInt32(idr["TimeLimit"]);
                            e.LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd");
                            e.TrafficType = Convert.ToString(idr["TrafficType"]).Trim();
                            e.DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim();
                            e.ShipperName = Convert.ToString(idr["ShipperName"]).Trim();
                            e.ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim();
                            e.ShipperAddress = Convert.ToString(idr["ShipperAddress"]).Trim();
                            e.ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim();
                            e.ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim();
                            e.ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim();
                            e.AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim();
                            e.AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim();
                            e.AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim();
                            e.AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim();
                            e.AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim();
                            e.AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim();
                            e.HandleTime = Convert.ToDateTime(idr["HandleTime"]);
                            e.Remark = Convert.ToString(idr["Remark"]).Trim();
                            e.PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 0 : Convert.ToInt16(idr["PrintNum"]);
                            e.DelFlag = Convert.ToString(idr["DelFlag"]).Trim();
                            e.CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim();
                            e.CreateDate = Convert.ToDateTime(idr["CreateDate"]);
                            e.OP_ID = Convert.ToString(idr["OP_ID"]).Trim();
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            e.ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim();
                            e.ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim();
                            e.ActMoney = Convert.ToDecimal(idr["ActMoney"]);
                            e.Signer = Convert.ToString(idr["Signer"]).Trim();
                            e.ContractNum = Convert.ToString(idr["ContractNum"]).Trim();
                            e.Goods = good;
                            e.HLY = Convert.ToString(idr["HLY"]).Trim();
                            e.SignTime = DateTime.Now;
                            if (!string.IsNullOrEmpty(Convert.ToString(idr["SignTime"]))) { e.SignTime = Convert.ToDateTime(idr["SignTime"]); }
                            if (IsFenPiByAwbID(Convert.ToInt64(idr["AwbID"]), entity.BelongSystem)) { e.FP = "1"; }
                            result.Add(e);
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                resHT["total"] = result.Count.ToString();
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 通过运单号查询货物品名数据
        /// </summary>
        /// <param name="AwbNo"></param>
        /// <returns></returns>
        public DataTable QueryAwbGoodsInfo(AwbGoodsEntity goods)
        {
            DataTable idr = new DataTable();
            
            try
            {
                goods.EnSafe();
                string strSQL = @"SELECT * FROM Tbl_Awb_Goods Where  AwbNo=@AwbNo and BelongSystem=@BelongSystem";
                DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL);
                NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, goods.AwbNo);
                NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, goods.BelongSystem);
                idr = NewaySql.ExecuteDataTable(cmd);

                return idr;
            }
            catch (ApplicationException ex)
            {
                if (idr != null)
                {
                    idr.Dispose();
                }
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 判断运单是否做到达分批
        /// </summary>
        /// <param name="awbid"></param>
        /// <returns></returns>
        private bool IsFenPiByAwbID(Int64 awbid, string BelongSystem)
        {
            
            bool result = false;
            string strSQL = @"select COUNT(*) as Nm from Tbl_Awb_Arrive where AwbID=@AwbID and BelongSystem=@BelongSystem";
            using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
            {
                NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, BelongSystem);
                NewaySql.AddInParameter(cmd, "@AwbID", DbType.Int64, awbid);
                using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["Nm"]) > 1)
                        {
                            result = true;
                        }
                    }
                }

            }
            return result;
        }
        #endregion
        #region 到达运单派送

        /// <summary>
        /// 运单信息查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryAwbRetrunStatus(int pIndex, int pNum, AwbEntity entity)
        {
            
            List<AwbEntity> result = new List<AwbEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.HandleTime DESC,a.AwbNo Asc) AS RowNumber,b.UserName,a.*,DATEADD(day,a.TimeLimit,a.HandleTime) as LatestTimeLimit   FROM Tbl_Awb_Arrive as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem Where (1=1)  ";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                #region 组装查询SQL语句
                //回单状态
                if (!string.IsNullOrEmpty(entity.ReturnStatus)) { strSQL += " and a.ReturnStatus='" + entity.ReturnStatus + "'"; }
                //回单发送状态
                if (!string.IsNullOrEmpty(entity.SendReturnAwbStatus)) { strSQL += " and a.SendReturnAwbStatus='" + entity.SendReturnAwbStatus + "'"; }
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind)) { strSQL += " and a.TransKind='" + entity.TransKind + "'"; }
                //运单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus)) { strSQL += " and ( a.AwbStatus = '3' or  a.AwbStatus ='13') "; }
                //运单状态
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strSQL += " and a.DelFlag = '" + entity.DelFlag + "'"; }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'"; }
                //运单号 
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and a.AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'"; }
                //按客户名称查询
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strSQL += " and (a.ShipperUnit like '%" + entity.ShipperUnit + "%' or a.ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                //按收货人查询
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strSQL += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')";
                }
                if (string.IsNullOrEmpty(entity.ReturnDateTip))
                {
                    //制单日期范围
                    if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strSQL += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                    }
                    if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strSQL += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                    }
                }
                else
                {
                    //发送回单日期范围
                    if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strSQL += " and a.SendReturnAwbDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                    }
                    if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strSQL += " and a.SendReturnAwbDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                    }
                }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dep in ('" + res + "')";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and (a.Dest in ('" + res + "') or a.Transit in ('" + res + "'))";
                }
                //中间站
                if (!string.IsNullOrEmpty(entity.MidDest)) { strSQL += " and a.MidDest='" + entity.MidDest + "'"; }
                //中转站
                if (!string.IsNullOrEmpty(entity.Transit)) { strSQL += " and a.Transit='" + entity.Transit + "'"; }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                #endregion
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };


                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                goodList.Add(new AwbGoodsEntity
                                {
                                    GoodsID = Convert.ToInt32(idrGoods["GoodsID"]),
                                    AwbNo = Convert.ToString(idrGoods["AwbNo"]),
                                    Package = Convert.ToString(idrGoods["Package"]),
                                    Goods = Convert.ToString(idrGoods["Goods"]),
                                    Weight = Convert.ToDecimal(idrGoods["Weight"]),
                                    WeightPrice = Convert.ToDecimal(idrGoods["WeightPrice"]),
                                    Volume = Convert.ToDecimal(idrGoods["Volume"]),
                                    VolumePrice = Convert.ToDecimal(idrGoods["VolumePrice"]),
                                    Piece = Convert.ToInt32(idrGoods["Piece"]),
                                    DeclareValue = Convert.ToString(idrGoods["DeclareValue"]),
                                    OP_ID = Convert.ToString(idrGoods["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idrGoods["OP_DATE"])
                                });

                                good += Convert.ToString(idrGoods["Goods"]).Trim() + ",";
                            }
                            #endregion
                            #region 获取运单数据

                            result.Add(new AwbEntity
                            {
                                ArriveID = Convert.ToInt64(idr["ArriveID"]),
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                BelongSystem = entity.BelongSystem,
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                Attach = Convert.ToInt32(idr["Attach"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                NowPay = Convert.ToDecimal(idr["NowPay"]),
                                PickPay = Convert.ToDecimal(idr["PickPay"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                SteveDore = Convert.ToString(idr["SteveDore"]).Trim(),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                ShipperAddress = Convert.ToString(idr["ShipperAddress"]).Trim(),
                                ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 0 : Convert.ToInt16(idr["PrintNum"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                ArriveDate = string.IsNullOrEmpty(Convert.ToString(idr["ArriveDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ArriveDate"]),
                                ActMoney = Convert.ToDecimal(idr["ActMoney"]),
                                SendReturnAwbStatus = Convert.ToString(idr["SendReturnAwbStatus"]).Trim(),
                                SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                ConfirmReturnAwbStatus = Convert.ToString(idr["ConfirmReturnAwbStatus"]).Trim(),
                                ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                HLY = Convert.ToString(idr["HLY"]).Trim(),
                                AwbGoods = goodList,
                                Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                            });
                            #endregion
                        }
                    }
                }
                //result.Sort(CompareEntity);
                resHT["rows"] = result;
                #region 总数

                string strCount = @"Select Count(*) as TotalCount from Tbl_Awb_Arrive  Where (1=1)  ";
                strCount += " and BelongSystem='" + entity.BelongSystem + "'";
                //回单状态
                if (!string.IsNullOrEmpty(entity.ReturnStatus)) { strCount += " and ReturnStatus='" + entity.ReturnStatus + "'"; }
                //回单发送状态
                if (!string.IsNullOrEmpty(entity.SendReturnAwbStatus)) { strCount += " and SendReturnAwbStatus='" + entity.SendReturnAwbStatus + "'"; }
                //运单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus)) { strCount += " and ( AwbStatus = '3' or  AwbStatus ='13') "; }
                //if (!string.IsNullOrEmpty(entity.AwbStatus)) { strCount += " and AwbStatus = '" + entity.AwbStatus + "'"; }
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind)) { strCount += " and TransKind='" + entity.TransKind + "'"; }
                //运单状态
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strCount += " and DelFlag = '" + entity.DelFlag + "'"; }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strCount += " and CheckOutType = '" + entity.CheckOutType + "'"; }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strCount += " and AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'"; }
                //按客户名称查询
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strCount += " and (ShipperUnit like '%" + entity.ShipperUnit + "%' or ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                //按收货人查询
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strCount += " and (AcceptPeople like '%" + entity.AcceptPeople + "%' or AcceptUnit like '%" + entity.AcceptPeople + "%')";
                }
                if (string.IsNullOrEmpty(entity.ReturnDateTip))
                {
                    //制单日期范围
                    if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strCount += " and HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                    }
                    if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strCount += " and HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                    }
                }
                else
                {
                    //发送回单日期范围
                    if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strCount += " and SendReturnAwbDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                    }
                    if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strCount += " and SendReturnAwbDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                    }
                }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and Dep in ('" + res + "')";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and (Dest in ('" + res + "') or Transit in ('" + res + "'))";
                }
                //中间站
                if (!string.IsNullOrEmpty(entity.MidDest)) { strCount += " and MidDest='" + entity.MidDest + "'"; }
                //中转站
                if (!string.IsNullOrEmpty(entity.Transit)) { strCount += " and Transit='" + entity.Transit + "'"; }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        //private SqlDatabase NewaySql = (SqlDatabase)DatabaseFactory.CreateDatabase();
        /// <summary>
        /// 判断到达运单是否存在
        /// </summary>
        /// <param name="awb"></param>
        /// <returns></returns>
        public bool IsExistArriveAwb(DeliveryEntity awb)
        {
            
            bool result = false;
            try
            {
                awb.EnSafe();
                string strSQL = @"SELECT COUNT(*) as num FROM Tbl_Awb_Arrive WHERE ArriveID=@ArriveID";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ArriveID", DbType.Int64, awb.ArriveID);
                    using (DataTable idr = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        if (idr.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(idr.Rows[0]["num"]) > 0)
                            {
                                result = true;
                            }
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 合并运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        public void DeliveryMerge(AwbEntity entity, List<AwbEntity> list)
        {
            
            try
            {
                entity.EnSafe();
                string strSQL = @"INSERT INTO Tbl_Awb_Arrive(AwbID,AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume ,AwbPiece ,AwbWeight ,AwbVolume ,Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit ,ShipperAddress,  ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem) Select AwbID,AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume,";
                strSQL += "" + entity.AwbPiece + ",";
                strSQL += "" + entity.AwbWeight + ",";
                strSQL += "" + entity.AwbVolume + ",";
                strSQL += @"Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit,ShipperAddress ,ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem from Tbl_Awb_Arrive Where ArriveID=@ArriveID";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ArriveID", DbType.Int64, list[0].ArriveID);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
                foreach (var it in list)
                {
                    //删除原运单数据
                    string strDel = @"Delete from Tbl_Awb_Arrive WHERE ArriveID=@ArriveID";
                    using (DbCommand cmddel = NewaySql.GetSqlStringCommond(strDel))
                    {
                        NewaySql.AddInParameter(cmddel, "@ArriveID", DbType.Int64, it.ArriveID);
                        NewaySql.ExecuteNonQuery(cmddel);
                        cmddel.Connection.Close();
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 分批操作
        /// </summary>
        /// <param name="ent"></param>
        public void DeliveryTear(List<AwbEntity> entity)
        {
            

            try
            {
                foreach (var awb in entity)
                {
                    awb.EnSafe();
                    string strSQL = @"INSERT INTO Tbl_Awb_Arrive( AwbID,AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume ,AwbPiece ,AwbWeight ,AwbVolume ,Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit ,ShipperAddress,  ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem) Select AwbID,AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume ,";
                    strSQL += "" + awb.AwbPiece + ",";
                    strSQL += "" + awb.AwbWeight + ",";
                    strSQL += "" + awb.AwbVolume + ",";
                    strSQL += @"Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit,ShipperAddress ,ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem from Tbl_Awb_Arrive Where ArriveID=@ArriveID";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@ArriveID", DbType.Int64, awb.ArriveID);
                        NewaySql.ExecuteNonQuery(cmdAdd);

                    }
                }
                //删除原运单数据
                string strDel = @"Delete from Tbl_Awb_Arrive WHERE ArriveID=@ArriveID";
                using (DbCommand cmddel = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmddel, "@ArriveID", DbType.Int64, entity[0].ArriveID);
                    NewaySql.ExecuteNonQuery(cmddel);
                    cmddel.Connection.Close();
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询所有的城市数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CityEntity> QueryAllCity(CityEntity entity)
        {
            List<CityEntity> result = new List<CityEntity>();
            

            string strSQL = @" SELECT* FROM Tbl_City WHERE DelFlag='0'";
            strSQL += " and BelongSystem='" + entity.BelongSystem + "'";
            //以中文名称为查询条件
            if (!string.IsNullOrEmpty(entity.CityName))
            {
                strSQL += " and CityName = '" + entity.CityName + "'";
            }
            using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = NewaySql.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new CityEntity
                        {
                            CityCode = Convert.ToString(idr["CityCode"]).Trim(),
                            CityName = Convert.ToString(idr["CityName"]).Trim()
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询所有具体承运资格的启用的承运商档案信息
        /// </summary>
        /// <returns></returns>
        public List<CarrierEntity> QueryCarrier(string BelongSystem)
        {
            List<CarrierEntity> result = new List<CarrierEntity>();
            try
            {
                string strSQL = @"SELECT * from Tbl_Carrier Where DelFlag=0 and HasCarrier=0 and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, BelongSystem);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CarrierEntity
                            {
                                CarrierID = Convert.ToInt64(idr["CarrierID"]),
                                BelongSystem = Convert.ToString(idr["BelongSystem"]),
                                CarrierName = Convert.ToString(idr["CarrierName"]),
                                CarrierShortName = Convert.ToString(idr["CarrierShortName"]),
                                Boss = Convert.ToString(idr["Boss"]),
                                Telephone = Convert.ToString(idr["Telephone"]),
                                Address = Convert.ToString(idr["Address"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]),
                                HasCarrier = Convert.ToString(idr["HasCarrier"]),
                                //HasCheck = Convert.ToString(idr["HasCheck"]),
                                //HasDelivery = Convert.ToString(idr["HasDelivery"])
                            });
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 查询所有用户名
        /// </summary>
        /// <returns></returns>
        public List<NewaySystemUserEntity> QueryALLUser(string sys)
        {
            List<NewaySystemUserEntity> result = new List<NewaySystemUserEntity>();
            string strSQL = @"select LoginName,UserName,CellPhone from Tbl_SysUser where DelFlag=0";
            strSQL += " and BelongSystem=@BelongSystem";

            try
            {
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, sys);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new NewaySystemUserEntity
                            {
                                LoginName = Convert.ToString(idrCount["LoginName"]).Trim(),
                                CellPhone = Convert.ToString(idrCount["CellPhone"]).Trim(),
                                UserName = Convert.ToString(idrCount["UserName"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 通过城市代码查询单位信息
        /// </summary>
        /// <param name="citycode"></param>
        /// <returns></returns>
        public SystemUnitEntity GetUnitByCityCode(string citycode, string sys)
        {
            SystemUnitEntity result = new SystemUnitEntity();
            try
            {
                string strSQL = @"SELECT CellPhone,Address,Boss,phone from Tbl_SysUnit Where CityCode=@CityCode and DelFlag=0 and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@CityCode", DbType.String, citycode);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, sys);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new SystemUnitEntity
                            {
                                CellPhone = Convert.ToString(idr["CellPhone"]).Trim(),
                                Address = Convert.ToString(idr["Address"]).Trim(),
                                Boss = Convert.ToString(idr["Boss"]).Trim(),
                                phone = Convert.ToString(idr["phone"]).Trim()
                            };
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 根据发车时间查询当天配送发的车辆数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ReturnDayTheTruckNum(DeliveryEntity entity)
        {
            string result = string.Empty;
            int n = 0;
            try
            {
                entity.EnSafe();
                string strSQL = @"select ISNULL(Max(DNum),0) as TNum from Tbl_Delivery where DType='0'";
                strSQL += " and BelongSystem='" + entity.BelongSystem + "'";
                //当前城市
                if (!string.IsNullOrEmpty(entity.Dest)) { strSQL += " and Dest = '" + entity.Dest + "'"; }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and CreateTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and CreateTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            n = Convert.ToInt32(idr["TNum"]);
                            break;
                        }
                    }

                }
                if (n == 0)
                {
                    result = "001";
                }
                if (n > 0 && n < 9)
                {
                    result = "00" + (n + 1).ToString();
                }
                if (n >= 9 && n < 99)
                {
                    result = "0" + (n + 1).ToString();
                }
                if (n >= 99)
                {
                    result = (n + 1).ToString();
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 根据发车时间查询当天发的车辆数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ReturnDayTheTruckNum(DepManifestEntity entity)
        {
            string result = string.Empty;
            int n = 0;
            try
            {
                entity.EnSafe();
                string strSQL = @"select COUNT(*) as TNum from Tbl_DepManifest where (1=1) ";
                //strSQL += " and BelongSystem='" + entity.BelongSystem + "'";
                //自发和外协
                if (!string.IsNullOrEmpty(entity.TransKind))
                {
                    strSQL += " and TransKind = '" + entity.TransKind + "'";
                }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    strSQL += " and Dep= '" + entity.Dep + "'";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    strSQL += " and Dest = '" + entity.Dest + "'";
                }
                //配载时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and CreateTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and CreateTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            n = Convert.ToInt32(idr["TNum"]);
                            break;
                        }
                    }
                }
                if (n == 0)
                {
                    result = "001";
                }
                if (n > 0 && n < 9)
                {
                    result = "00" + (n + 1).ToString();
                }
                if (n >= 9 && n < 99)
                {
                    result = "0" + (n + 1).ToString();
                }
                if (n >= 99)
                {
                    result = (n + 1).ToString();
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 通过车辆牌照号判断是否存在相同的车牌信息 True 存在 False 不存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExistTruck(TruckEntity entity)
        {
            
            bool result = false;
            try
            {
                string strSQL = @"SELECT TruckNum from Tbl_Truck Where TruckNum=@TruckNum and BelongSystem=@BelongSystem";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@TruckNum", DbType.String, entity.TruckNum);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd)) { if (dt.Rows.Count > 0) { result = true; } }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 判断是否存在当前城市的在站的车辆
        /// True：存在，可以配载，False：不可以配载
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string IsExistCurCityTruck(TruckEntity entity)
        {
            
            string result = string.Empty;
            try
            {
                string strSQL = @" select DelFlag from Tbl_Truck where TruckNum=@TruckNum and DelFlag<>1";
                strSQL += " and BelongSystem=@BelongSystem";
                if (!string.IsNullOrEmpty(entity.CurCity)) { strSQL += " and CurCity=@CurCity"; }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(cmd, "@TruckNum", DbType.String, entity.TruckNum);
                    if (!string.IsNullOrEmpty(entity.CurCity)) { NewaySql.AddInParameter(cmd, "@CurCity", DbType.String, entity.CurCity); }
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow it in dt.Rows)
                            {
                                result = Convert.ToString(it["DelFlag"]);
                            }
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增车辆信息
        /// </summary>
        /// <param name="entity"></param>
        public void SaveTruckInfo(TruckEntity entity)
        {
            
            try
            {
                entity.EnSafe();
                string strAdd = @"INSERT INTO Tbl_Truck(TruckNum,City,Model,Weight,Length,Driver,DriverIDNum,DriverIDAddress,DriverCellPhone,TruckType,License,OP_ID,CurCity,TripMark,BlackMemo,BelongSystem) VALUES  (@TruckNum,@City,@Model,@Weight,@Length,@Driver,@DriverIDNum,@DriverIDAddress,@DriverCellPhone,@TruckType,@License,@OP_ID,@CurCity,@TripMark,@BlackMemo,@BelongSystem)";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strAdd))
                {
                    NewaySql.AddInParameter(cmdAdd, "@TruckNum", DbType.String, entity.TruckNum);
                    NewaySql.AddInParameter(cmdAdd, "@City", DbType.String, entity.City);
                    NewaySql.AddInParameter(cmdAdd, "@CurCity", DbType.String, entity.City);
                    NewaySql.AddInParameter(cmdAdd, "@Model", DbType.String, entity.Model);
                    NewaySql.AddInParameter(cmdAdd, "@Weight", DbType.Decimal, entity.Weight);
                    NewaySql.AddInParameter(cmdAdd, "@Length", DbType.Decimal, entity.Length);
                    NewaySql.AddInParameter(cmdAdd, "@Driver", DbType.String, entity.Driver);
                    NewaySql.AddInParameter(cmdAdd, "@DriverIDNum", DbType.String, entity.DriverIDNum);
                    NewaySql.AddInParameter(cmdAdd, "@DriverIDAddress", DbType.String, entity.DriverIDAddress);
                    NewaySql.AddInParameter(cmdAdd, "@DriverCellPhone", DbType.String, entity.DriverCellPhone);
                    NewaySql.AddInParameter(cmdAdd, "@TruckType", DbType.String, entity.TruckType);
                    NewaySql.AddInParameter(cmdAdd, "@License", DbType.String, entity.License);
                    NewaySql.AddInParameter(cmdAdd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.AddInParameter(cmdAdd, "@TripMark", DbType.String, entity.TripMark);
                    NewaySql.AddInParameter(cmdAdd, "@BlackMemo", DbType.String, entity.BlackMemo);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 判断运单是否配送
        /// </summary>
        /// <param name="awb"></param>
        /// <returns></returns>
        public bool IsDeliveryAwb(DeliveryEntity awb)
        {
            
            bool result = false;
            try
            {
                awb.EnSafe();
                string strSQL = @"SELECT COUNT(*) as num FROM Tbl_DeliveryAwb WHERE AwbNo=@AwbNo and ArriveID=@ArriveID and Mode=@Mode and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, awb.BelongSystem.ToUpper().Trim());
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awb.AwbNo.ToUpper().Trim());
                    NewaySql.AddInParameter(cmdAdd, "@Mode", DbType.String, awb.Mode);
                    NewaySql.AddInParameter(cmdAdd, "@ArriveID", DbType.Int64, awb.ArriveID);
                    using (DataTable idr = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        if (idr.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(idr.Rows[0]["num"]) > 0)
                            {
                                result = true;
                            }
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增配送运单数据
        /// </summary>
        public Int64 AddDelivery(DeliveryEntity entity)
        {
            
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Delivery(DeliveryNum,DNum,TruckNum,Driver,DriverCellPhone,StartTime,PreArriveTime,TransportFee,OtherFee,OP_ID,CurCity,PieceTotal,CreateTime,Dest,DType,BelongSystem,Remark) VALUES (@DeliveryNum,@DNum,@TruckNum,@Driver,@DriverCellPhone,@StartTime,@PreArriveTime,@TransportFee,@OtherFee,@OP_ID,@CurCity,@PieceTotal,@CreateTime,@Dest,@DType,@BelongSystem,@Remark) SELECT @@IDENTITY";
            try
            {
                entity.EnSafe();

                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(cmd, "@DeliveryNum", DbType.String, entity.DeliveryNum);
                    NewaySql.AddInParameter(cmd, "@DNum", DbType.Int32, ReturnNumTheTruckNum(new DeliveryEntity { Dest = entity.Dest, StartDate = DateTime.Now, EndDate = DateTime.Now, BelongSystem = entity.BelongSystem }));
                    NewaySql.AddInParameter(cmd, "@TruckNum", DbType.String, entity.TruckNum.Trim());
                    NewaySql.AddInParameter(cmd, "@Driver", DbType.String, entity.Driver.Trim());
                    NewaySql.AddInParameter(cmd, "@DriverCellPhone", DbType.String, entity.DriverCellPhone.Trim());
                    NewaySql.AddInParameter(cmd, "@StartTime", DbType.DateTime, entity.StartTime);
                    NewaySql.AddInParameter(cmd, "@PreArriveTime", DbType.DateTime, entity.PreArriveTime);
                    NewaySql.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    NewaySql.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.AddInParameter(cmd, "@CurCity", DbType.String, entity.CurCity);
                    NewaySql.AddInParameter(cmd, "@PieceTotal", DbType.Int32, entity.PieceTotal);
                    NewaySql.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now);
                    NewaySql.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    NewaySql.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    NewaySql.AddInParameter(cmd, "@DType", DbType.String, entity.DType);

                    did = Convert.ToInt64(NewaySql.ExecuteScalar(cmd));

                }
                return did;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }
        /// <summary>
        /// 返回配送表的编号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int ReturnNumTheTruckNum(DeliveryEntity entity)
        {
            
            string result = string.Empty;
            int n = 0;
            try
            {
                entity.EnSafe();
                string strSQL = @"select ISNULL(Max(DNum),0) as TNum from Tbl_Delivery where DType='0'";
                strSQL += " and BelongSystem='" + entity.BelongSystem + "'";
                //当前城市
                if (!string.IsNullOrEmpty(entity.Dest)) { strSQL += " and Dest = '" + entity.Dest + "'"; }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and CreateTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and CreateTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            n = Convert.ToInt32(idr["TNum"]);
                            break;
                        }
                    }
                }
                return (n + 1);
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增配送单号(中转单号)与运单号的关联数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddDeliveryAwb(DeliveryAwbEntity entity)
        {

            

            string strSQL = @"INSERT INTO Tbl_DeliveryAwb(DeliveryID,AwbID,ArriveID,AwbNo,Mode,OP_ID,BelongSystem) VALUES (@DeliveryID,@AwbID,@ArriveID,@AwbNo,@Mode,@OP_ID,@BelongSystem)";
            try
            {
                entity.EnSafe();

                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@DeliveryID", DbType.Int64, entity.DeliveryID);
                    NewaySql.AddInParameter(cmd, "@AwbID", DbType.Int64, entity.AwbID);
                    NewaySql.AddInParameter(cmd, "@ArriveID", DbType.Int64, entity.ArriveID);
                    NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, entity.AwbNo);
                    NewaySql.AddInParameter(cmd, "@Mode", DbType.String, entity.Mode);
                    NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }
        /// <summary>
        /// 设置运单的状态为到达和结束
        /// </summary>
        /// <param name="entity"></param>
        public void SetAwbStatus(AwbEntity it)
        {
            
            try
            {
                it.EnSafe();
                //设置运单表的状态为3到达
                string strDel = @"UPDATE Tbl_Awb_Basic SET AwbStatus=@AwbStatus";
                if (!string.IsNullOrEmpty(it.DelFlag))
                {
                    strDel += " ,DelFlag=@DelFlag";
                }
                if (it.AwbStatus.Trim().Equals("3") && it.ArriveDate.ToString("yyyy-MM-dd") != "0001-01-01" && it.ArriveDate.ToString("yyyy-MM-dd") != "1900-01-01" && it.ArriveDate.ToString("yyyy-MM-dd") != "1901-01-01")
                {
                    strDel += ",ArriveDate=@ArriveDate ";
                }
                if (it.AwbStatus.Trim().Equals("6"))//送达状态，保存送达时间
                {
                    strDel += ",GiveTime=@GiveTime";
                }
                if (it.AwbStatus.Trim().Equals("13"))//异常状态，保存异常时间
                {
                    strDel += ",AbnormalTime=@AbnormalTime";
                }
                strDel += " Where BelongSystem=@BelongSystem";
                if (!string.IsNullOrEmpty(it.ContractNum))
                {
                    strDel += " and ContractNum=@ContractNum";
                }
                if (!it.AwbID.Equals(0))
                {
                    strDel += " and AwbID=@AwbID";
                }
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    if (!string.IsNullOrEmpty(it.ContractNum))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@ContractNum", DbType.String, it.ContractNum.Trim());
                    }
                    if (it.AwbStatus.Trim().Equals("3") && it.ArriveDate.ToString("yyyy-MM-dd") != "0001-01-01" && it.ArriveDate.ToString("yyyy-MM-dd") != "1900-01-01" && it.ArriveDate.ToString("yyyy-MM-dd") != "1901-01-01")
                    {
                        NewaySql.AddInParameter(cmdAdd, "@ArriveDate", DbType.DateTime, it.ArriveDate);
                    }
                    if (it.AwbStatus.Trim().Equals("6"))//送达状态，保存送达时间
                    {
                        NewaySql.AddInParameter(cmdAdd, "@GiveTime", DbType.DateTime, it.GiveTime);
                    }
                    if (it.AwbStatus.Trim().Equals("13"))//异常状态，保存异常时间
                    {
                        NewaySql.AddInParameter(cmdAdd, "@AbnormalTime", DbType.DateTime, it.GiveTime);
                    }
                    if (!it.AwbID.Equals(0))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, it.AwbID);
                    }
                    if (!string.IsNullOrEmpty(it.DelFlag))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@DelFlag", DbType.String, it.DelFlag.Trim());
                    }
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, it.BelongSystem.Trim());
                    NewaySql.AddInParameter(cmdAdd, "@AwbStatus", DbType.String, it.AwbStatus.Trim());
                    NewaySql.ExecuteNonQuery(cmdAdd);

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 设置运单的状态为到达和结束
        /// </summary>
        /// <param name="entity"></param>
        public void SetArriveAwbStatus(AwbEntity it)
        {
            
            try
            {
                it.EnSafe();
                //设置运单表的状态为3到达
                string strDel = @"UPDATE Tbl_Awb_Arrive SET AwbStatus=@AwbStatus ";
                if (it.AwbStatus.Trim().Equals("6"))//送达状态，保存送达时间
                {
                    strDel += ",GiveTime=@GiveTime";
                }
                if (it.AwbStatus.Trim().Equals("13"))//异常状态，保存异常时间
                {
                    strDel += ",AbnormalTime=@AbnormalTime";
                }
                strDel += " where ArriveID=@ArriveID";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ArriveID", DbType.Int64, it.ArriveID);
                    if (it.AwbStatus.Trim().Equals("6"))//送达状态，保存送达时间
                    {
                        NewaySql.AddInParameter(cmdAdd, "@GiveTime", DbType.DateTime, it.GiveTime);
                    }
                    if (it.AwbStatus.Trim().Equals("13"))//异常状态，保存异常时间
                    {
                        NewaySql.AddInParameter(cmdAdd, "@AbnormalTime", DbType.DateTime, it.GiveTime);
                    }
                    NewaySql.AddInParameter(cmdAdd, "@AwbStatus", DbType.String, it.AwbStatus.Trim());
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        #endregion
        #region 配送运单管理
        /// <summary>
        /// 配送运单管理查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryDeliveryOrder(int pIndex, int pNum, DeliveryEntity entity)
        {
            
            List<DeliveryEntity> result = new List<DeliveryEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += " (select ROW_NUMBER() OVER (ORDER BY b.StartTime DESC) AS RowNumber,b.* from (select distinct a.DeliveryID,a.DeliveryNum,a.TruckNum,a.Driver,a.DriverCellPhone,a.StartTime,a.TransportFee,a.PreArriveTime,a.OtherFee,a.OP_ID,a.OP_DATE,a.FinanceFirstCheck,a.FirstCheckName,a.FirstCheckDate,c.UserName,a.CurCity,a.Remark,ISNULL(d.Model,0) as Model,ISNULL(d.Length,0) as Length from Tbl_Delivery as a left join Tbl_DeliveryAwb as b on a.DeliveryID=b.DeliveryID  Left join Tbl_SysUser as c on a.OP_ID=c.LoginName and a.BelongSystem=c.BelongSystem left join Tbl_Truck as d on a.TruckNum=d.TruckNum where a.DType='0'";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //Mode
                if (!string.IsNullOrEmpty(entity.Mode)) { strSQL += " and b.Mode = '" + entity.Mode + "'"; }
                //操作员
                if (!string.IsNullOrEmpty(entity.OP_ID)) { strSQL += " and a.OP_ID = '" + entity.OP_ID + "'"; }
                //司机姓名
                if (!string.IsNullOrEmpty(entity.Driver)) { strSQL += " and a.Driver like '%" + entity.Driver + "%'"; }
                //运单号
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and b.AwbNo = '" + entity.AwbNo + "'"; }
                //车牌照
                if (!string.IsNullOrEmpty(entity.TruckNum)) { strSQL += " and a.TruckNum like '%" + entity.TruckNum + "%'"; }
                //配送合同号
                if (!string.IsNullOrEmpty(entity.DeliveryNum)) { strSQL += " and a.DeliveryNum = '" + entity.DeliveryNum + "'"; }
                //当前站点
                if (!string.IsNullOrEmpty(entity.CurCity))
                {
                    string[] ccs = entity.CurCity.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.CurCity in ('" + res + "')";
                }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += ") as b ) A ";
                strSQL += " WHERE A.RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new DeliveryEntity
                            {
                                DeliveryID = Convert.ToInt64(idr["DeliveryID"]),
                                DeliveryNum = Convert.ToString(idr["DeliveryNum"]),
                                TruckNum = Convert.ToString(idr["TruckNum"]),
                                Driver = Convert.ToString(idr["Driver"]).Trim(),
                                DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                StartTime = Convert.ToDateTime(idr["StartTime"]),
                                UserName = Convert.ToString(idr["UserName"]),
                                PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                FinanceFirstCheck = Convert.ToString(idr["FinanceFirstCheck"]).Trim(),
                                FirstCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FirstCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FirstCheckDate"]),
                                FirstCheckName = Convert.ToString(idr["FirstCheckName"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                Model = Convert.ToString(idr["Model"]).Trim(),
                                Length = Convert.ToDecimal(idr["Length"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                CurCity = Convert.ToString(idr["CurCity"]).Trim()
                            });
                        }
                    }
                }
                resHT["rows"] = result;
                #region 汇总
                string strCount = @"Select Count(*) as TotalCount from ( Select distinct a.DeliveryID from Tbl_Delivery as a left join Tbl_DeliveryAwb as b on a.DeliveryID=b.DeliveryID where (1=1) and a.DType='0'";
                strCount += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //Mode
                if (!string.IsNullOrEmpty(entity.Mode)) { strCount += " and b.Mode = '" + entity.Mode + "'"; }
                if (!string.IsNullOrEmpty(entity.OP_ID)) { strCount += " and a.OP_ID = '" + entity.OP_ID + "'"; }
                //司机姓名
                if (!string.IsNullOrEmpty(entity.Driver)) { strCount += " and a.Driver like '%" + entity.Driver + "%'"; }
                //运单号
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strCount += " and b.AwbNo = '" + entity.AwbNo + "'"; }
                //车牌照
                if (!string.IsNullOrEmpty(entity.TruckNum)) { strCount += " and a.TruckNum like '%" + entity.TruckNum + "%'"; }
                //配送合同号
                if (!string.IsNullOrEmpty(entity.DeliveryNum)) { strCount += " and a.DeliveryNum = '" + entity.DeliveryNum + "'"; }
                //当前站点
                if (!string.IsNullOrEmpty(entity.CurCity))
                {
                    string[] ccs = entity.CurCity.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and a.CurCity in ('" + res + "')";
                }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strCount += " ) as c";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 查询所有车辆信息
        /// </summary>
        /// <returns></returns>
        public List<TruckCacheEntity> QueryTruck(TruckEntity te)
        {
            
            List<TruckCacheEntity> result = new List<TruckCacheEntity>();
            string strSQL = @"SELECT TruckNum,Driver,DriverCellPhone,DriverIDNum,DriverIDAddress,TruckType,Model,Length FROM Tbl_Truck Where DelFlag<>1 ";
            if (!string.IsNullOrEmpty(te.BelongSystem)) { strSQL += " and BelongSystem=@BelongSystem"; }
            if (!string.IsNullOrEmpty(te.TripMark)) { strSQL += " and TripMark=@TripMark"; }
            //当前城市
            if (!string.IsNullOrEmpty(te.CurCity))
            {
                string[] ccs = te.CurCity.Split(',');
                string res = string.Empty;
                for (int i = 0; i < ccs.Length; i++)
                {
                    res += ccs[i] + "','";
                }
                res = res.Substring(0, res.Length - 3);
                strSQL += " and CurCity in ('" + res + "')";
            }
            try
            {
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    if (!string.IsNullOrEmpty(te.BelongSystem))
                    {
                        NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, te.BelongSystem);
                    }
                    if (!string.IsNullOrEmpty(te.TripMark))
                    {
                        NewaySql.AddInParameter(cmd, "@TripMark", DbType.String, te.TripMark);
                    }
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new TruckCacheEntity
                            {
                                TruckNum = Convert.ToString(idrCount["TruckNum"]).Trim(),
                                Driver = Convert.ToString(idrCount["Driver"]).Trim(),
                                DriverCellPhone = Convert.ToString(idrCount["DriverCellPhone"]).Trim(),
                                DriverIDAddress = Convert.ToString(idrCount["DriverIDAddress"]).Trim(),
                                DriverIDNum = Convert.ToString(idrCount["DriverIDNum"]).Trim(),
                                Length = Convert.ToDecimal(idrCount["Length"]),
                                Model = Convert.ToString(idrCount["Model"]).Trim()
                            });
                        }
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
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
            List<AwbEntity> result = new List<AwbEntity>();
            

            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @"select a.DeliveryID,b.ArriveID,b.AwbID,b.AwbNo,b.Dep,b.Dest,b.MidDest,b.Transit,b.HandleTime,b.ShipperUnit,b.AcceptPeople,b.AcceptCellphone,b.AcceptTelephone,b.AcceptAddress,b.Piece,b.AwbPiece,b.Weight,b.AwbWeight,b.Volume,b.AwbVolume,b.TransportFee,b.TotalCharge,b.CollectMoney,b.CheckOutType,b.ReturnAwb,b.TrafficType,b.DelFlag,b.TimeLimit,DATEADD(day,b.TimeLimit,b.HandleTime) as LatestTimeLimit,b.HLY from Tbl_DeliveryAwb as a inner join Tbl_Awb_Arrive as b on a.ArriveID=b.ArriveID where a.DeliveryID=@DeliveryID and a.Mode=@Mode and a.BelongSystem=@BelongSystem order by b.HandleTime DESC,b.AwbNo Asc";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(command, "@DeliveryID", DbType.Int64, entity.DeliveryID);
                    NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(command, "@Mode", DbType.String, entity.Mode);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows) { good += Convert.ToString(idrGoods["Goods"]) + ","; }
                            #endregion
                            result.Add(new AwbEntity
                            {
                                ArriveID = Convert.ToInt64(idr["ArriveID"]),
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                BelongSystem = entity.BelongSystem,
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                MidDest = Convert.ToString(idr["MidDest"]).Trim(),
                                Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                HLY = Convert.ToString(idr["HLY"]).Trim(),
                                Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                            });
                        }
                    }
                }
                //result.Sort(CompareEntity);
                resHT["rows"] = result;
                resHT["total"] = result.Count.ToString();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }
        /// <summary>
        /// 通过配送合同号查询短途配载数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DeliveryEntity QueryAwbByDeliveryID(Int64 did, string dty)
        {
            DeliveryEntity result = new DeliveryEntity();
            
            try
            {
                string strSQL = @"SELECT DeliveryNum,DeliveryID,ShortStatus,CheckStatus,FinanceFirstCheck,FinanceSecondCheck FROM Tbl_Delivery WHERE DeliveryID=@DeliveryID and DType=@DType";

                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(command, "@DeliveryID", DbType.Int64, did);
                    NewaySql.AddInParameter(command, "@DType", DbType.String, dty);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new DeliveryEntity
                            {
                                DeliveryNum = Convert.ToString(idr["DeliveryNum"]).Trim(),
                                DeliveryID = Convert.ToInt64(idr["DeliveryID"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim(),
                                FinanceFirstCheck = Convert.ToString(idr["FinanceFirstCheck"]).Trim(),
                                FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]).Trim(),
                                ShortStatus = Convert.ToString(idr["ShortStatus"]).Trim()
                            };
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 审核配送单
        /// </summary>
        /// <param name="entity"></param>
        public void CheckDelivery(DeliveryEntity entity)
        {
            
            try
            {
                string strSQL = @"update Tbl_Delivery set FinanceFirstCheck=@FinanceFirstCheck,FirstCheckName=@FirstCheckName,FirstCheckDate=@FirstCheckDate where DeliveryID=@DeliveryID";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@DeliveryID", DbType.Int64, entity.DeliveryID);
                    NewaySql.AddInParameter(cmdAdd, "@FinanceFirstCheck", DbType.String, entity.FinanceFirstCheck);
                    NewaySql.AddInParameter(cmdAdd, "@FirstCheckName", DbType.String, entity.FirstCheckName);
                    NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DateTime.Now);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
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
            
            bool result = false;
            try
            {
                string strSQL = @"select b.AwbStatus from Tbl_DeliveryAwb as a left join Tbl_Awb_Arrive as b on a.ArriveID=b.ArriveID where a.DeliveryID=@DeliveryID and a.Mode=@Mode and a.BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@Mode", DbType.String, mode);
                    NewaySql.AddInParameter(cmdAdd, "@DeliveryID", DbType.Int64, did);
                    using (DataTable idr = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        if (idr.Rows.Count > 0)
                        {
                            foreach (DataRow it in idr.Rows)
                            {
                                if (!Convert.ToString(it["AwbStatus"]).Trim().Equals(status))
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 根据配送和中转ID查询相关运单ID信息
        /// </summary>
        /// <param name="deliveryID"></param>
        /// <param name="md"></param>
        /// <returns></returns>
        public List<DeliveryAwbEntity> QueryAwbIDByDeliveryID(Int64 deliveryID, string md)
        {
            List<DeliveryAwbEntity> result = new List<DeliveryAwbEntity>();
            
            try
            {
                string strSQL = @"Select a.AwbID,a.AwbNo,a.DeliveryID,a.ArriveID,b.AwbStatus from Tbl_DeliveryAwb as a left join Tbl_Awb_Arrive as b on a.ArriveID=b.ArriveID where a.DeliveryID=@DeliveryID and a.Mode=@Mode";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@DeliveryID", DbType.Int64, deliveryID);
                    NewaySql.AddInParameter(cmdAdd, "@Mode", DbType.String, md);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new DeliveryAwbEntity
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]),
                                DeliveryID = Convert.ToInt64(idr["DeliveryID"]),
                                ArriveID = Convert.ToInt64(idr["ArriveID"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"])
                            });
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除配送运单
        /// </summary>
        /// <param name="entity"></param>
        public void DelDelivery(Int64 deliveryID)
        {
            
            try
            {
                string strSQL = @"delete from Tbl_Delivery where DeliveryID=@DeliveryID";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@DeliveryID", DbType.Int64, deliveryID);
                    NewaySql.ExecuteNonQuery(cmdAdd);

                }
                //删除关联表
                DelDeliveryTransitAwb(deliveryID, "0");
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除配送单号与运单的关联表
        /// </summary>
        /// <param name="deliveryID"></param>
        public void DelDeliveryTransitAwb(Int64 deliveryID, string md)
        {
            
            try
            {
                string strSQL = @"delete from Tbl_DeliveryAwb where DeliveryID=@DeliveryID and Mode=@Mode";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@DeliveryID", DbType.Int64, deliveryID);
                    NewaySql.AddInParameter(cmdAdd, "@Mode", DbType.String, md);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除运单状态，根据运单id和运单号，运单状态
        /// </summary>
        /// <param name="awbno"></param>
        public void DeleteAwbStatus(string awbno, long awbid, string tflag, string BelongSystem)
        {
            
            try
            {
                string strSQL = @"DELETE FROM Tbl_AwbStatusTruck Where AwbNo=@AwbNo and AwbID=@AwbID and TruckFlag=@TruckFlag and BelongSystem=@BelongSystem";

                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "BelongSystem", DbType.String, BelongSystem);
                    NewaySql.AddInParameter(cmd, "AwbNo", DbType.String, awbno);
                    NewaySql.AddInParameter(cmd, "AwbID", DbType.Int64, awbid);
                    NewaySql.AddInParameter(cmd, "TruckFlag", DbType.String, tflag);

                    NewaySql.ExecuteNonQuery(cmd);

                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 配送运单查询导出
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<DeliveryEntity> QueryDeliveryOrderForExport(DeliveryEntity entity)
        {
            
            List<DeliveryEntity> result = new List<DeliveryEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT a.DeliveryID,a.DeliveryNum,a.TruckNum,a.Driver,a.DriverCellPhone,a.StartTime,a.CreateTime,a.TransportFee,a.PreArriveTime,a.OtherFee,a.OP_ID,a.OP_DATE,a.FinanceFirstCheck,a.Remark,b.AwbID,b.AwbNo,ISNULL(c.CollectMoney,0) as CollectMoney,ISNULL(c.CheckOutType,'') as CheckOutType,ISNULL(c.TotalCharge,0) as TotalCharge,ISNULL(c.DeliverFee,0) as DeliverFee,ISNULL(c.Piece,0) as Piece,ISNULL(c.AwbPiece,0) as AwbPiece,ISNULL(c.Weight,0) as Weight,ISNULL(c.Volume,0) as Volume,c.HandleTime,c.ShipperUnit,c.ShipperName,c.AcceptUnit,c.AcceptPeople,c.AcceptAddress,ISNULL(d.Length,0) as Length,ISNULL(d.Model,0) as Model  from Tbl_Delivery as a left join Tbl_DeliveryAwb as b on a.DeliveryID=b.DeliveryID left join Tbl_Awb_Arrive as c on  b.ArriveID=c.ArriveID left join Tbl_Truck as d on a.TruckNum=d.TruckNum where (1=1) and a.DType='0'";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //Mode
                if (!string.IsNullOrEmpty(entity.Mode)) { strSQL += " and b.Mode = '" + entity.Mode + "'"; }
                //司机姓名
                if (!string.IsNullOrEmpty(entity.Driver)) { strSQL += " and a.Driver like '%" + entity.Driver + "%'"; }
                //运单号
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and b.AwbNo = '" + entity.AwbNo + "'"; }
                //车牌照
                if (!string.IsNullOrEmpty(entity.TruckNum)) { strSQL += " and a.TruckNum like '%" + entity.TruckNum + "%'"; }
                //配送合同号
                if (!string.IsNullOrEmpty(entity.DeliveryNum)) { strSQL += " and a.DeliveryNum = '" + entity.DeliveryNum + "'"; }
                //当前站点
                if (!string.IsNullOrEmpty(entity.CurCity))
                {
                    string[] ccs = entity.CurCity.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.CurCity in ('" + res + "')";
                }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                good += Convert.ToString(idrGoods["Goods"]).Trim() + ",";
                            }
                            #endregion
                            DeliveryEntity de = new DeliveryEntity();
                            de.DeliveryID = Convert.ToInt64(idr["DeliveryID"]);
                            de.DeliveryNum = Convert.ToString(idr["DeliveryNum"]);
                            de.TruckNum = Convert.ToString(idr["TruckNum"]);
                            de.Driver = Convert.ToString(idr["Driver"]).Trim();
                            de.DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim();
                            de.TransportFee = Convert.ToDecimal(idr["TransportFee"]);
                            de.OtherFee = Convert.ToDecimal(idr["OtherFee"]);
                            de.StartTime = Convert.ToDateTime(idr["StartTime"]);
                            de.CreateTime = Convert.ToDateTime(idr["CreateTime"]);
                            de.PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]);
                            de.OP_ID = Convert.ToString(idr["OP_ID"]).Trim();
                            de.AwbNo = Convert.ToString(idr["AwbNo"]).Trim();
                            de.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            de.Remark = Convert.ToString(idr["Remark"]).Trim();
                            de.FinanceFirstCheck = Convert.ToString(idr["FinanceFirstCheck"]).Trim();
                            de.Piece = Convert.ToInt32(idr["Piece"]);
                            de.AwbPiece = Convert.ToInt32(idr["AwbPiece"]);
                            de.Weight = Convert.ToDecimal(idr["Weight"]);
                            de.Volume = Convert.ToDecimal(idr["Volume"]);
                            de.ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim();
                            de.AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim();
                            de.AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim();
                            de.Model = Convert.ToString(idr["Model"]).Trim();
                            de.Length = Convert.ToDecimal(idr["Length"]);
                            de.Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1);
                            de.CollectMoney = Convert.ToDecimal(idr["CollectMoney"]);
                            de.TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                            de.DeliverFee = Convert.ToDecimal(idr["DeliverFee"]);
                            de.HandleTime = string.IsNullOrEmpty(Convert.ToString(idr["HandleTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["HandleTime"]);
                            //if (Convert.ToString(idr["CheckOutType"]).Trim().Equals("3"))
                            //{
                            //    de.TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                            //}
                            result.Add(de);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 修改配送运单信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateDelivery(DeliveryEntity entity)
        {
            
            string strSQL = @"UPDATE Tbl_Delivery SET TruckNum =@TruckNum,Driver=@Driver,DriverCellPhone= @DriverCellPhone,StartTime=@StartTime,PreArriveTime=@PreArriveTime,TransportFee=@TransportFee,OtherFee=@OtherFee,OP_ID=@OP_ID,OP_DATE=@OP_DATE,CurCity=@CurCity,PieceTotal=@PieceTotal,Dest=@Dest,MidDest=@MidDest,Remark=@Remark WHERE DeliveryID=@DeliveryID";
            try
            {
                entity.EnSafe();
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@TruckNum", DbType.String, entity.TruckNum);
                    NewaySql.AddInParameter(cmd, "@Driver", DbType.String, entity.Driver);
                    NewaySql.AddInParameter(cmd, "@DriverCellPhone", DbType.String, entity.DriverCellPhone);
                    NewaySql.AddInParameter(cmd, "@StartTime", DbType.DateTime, entity.StartTime);
                    NewaySql.AddInParameter(cmd, "@PreArriveTime", DbType.DateTime, entity.PreArriveTime);
                    NewaySql.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    NewaySql.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    NewaySql.AddInParameter(cmd, "@DeliveryID", DbType.Int64, entity.DeliveryID);
                    NewaySql.AddInParameter(cmd, "@CurCity", DbType.String, entity.CurCity);
                    NewaySql.AddInParameter(cmd, "@PieceTotal", DbType.Int32, entity.PieceTotal);
                    NewaySql.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    NewaySql.AddInParameter(cmd, "@MidDest", DbType.String, entity.MidDest);
                    NewaySql.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询运单的状态
        /// </summary>
        /// <param name="awbid"></param>
        /// <param name="awbno"></param>
        /// <returns></returns>
        public string QueryAwbStatusByAwbIDNo(long awbid, string awbno, string BelongSystem)
        {
            string result = string.Empty;
            
            try
            {
                string strDel = @"SELECT AwbStatus From Tbl_Awb_Basic WHERE AwbID=@AwbID and AwbNo=@AwbNo and BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awbno);
                    NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awbid);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = Convert.ToString(idr["AwbStatus"]).Trim();
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        #endregion
        #region 到达运单中转

        /// <summary>
        /// 合并运单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        public void TransitMerge(AwbEntity entity, List<AwbEntity> list)
        {
            
            try
            {
                entity.EnSafe();
                string strSQL = @"INSERT INTO Tbl_Awb_Arrive(AwbID,AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume ,AwbPiece ,AwbWeight ,AwbVolume ,Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit ,ShipperAddress,  ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem) Select AwbID,AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume,";
                strSQL += "" + entity.AwbPiece + ",";
                strSQL += "" + entity.AwbWeight + ",";
                strSQL += "" + entity.AwbVolume + ",";
                strSQL += @"Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit,ShipperAddress ,ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem from Tbl_Awb_Arrive Where ArriveID=@ArriveID";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@ArriveID", DbType.Int64, list[0].ArriveID);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
                foreach (var it in list)
                {
                    //删除原运单数据
                    string strDel = @"Delete from Tbl_Awb_Arrive WHERE ArriveID=@ArriveID";
                    using (DbCommand cmddel = NewaySql.GetSqlStringCommond(strDel))
                    {
                        NewaySql.AddInParameter(cmddel, "@ArriveID", DbType.Int64, it.ArriveID);
                        NewaySql.ExecuteNonQuery(cmddel);
                        cmddel.Connection.Close();
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 分批操作
        /// </summary>
        /// <param name="ent"></param>
        public void TransitTear(List<AwbEntity> entity)
        {
            

            try
            {
                foreach (var awb in entity)
                {
                    awb.EnSafe();
                    string strSQL = @"INSERT INTO Tbl_Awb_Arrive( AwbID,AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume ,AwbPiece ,AwbWeight ,AwbVolume ,Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit ,ShipperAddress,  ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem) Select AwbID,AwbNo,HAwbNo ,Dep,Transit ,Dest ,Piece ,Weight ,Volume ,";
                    strSQL += "" + awb.AwbPiece + ",";
                    strSQL += "" + awb.AwbWeight + ",";
                    strSQL += "" + awb.AwbVolume + ",";
                    strSQL += @"Attach ,InsuranceFee ,TransitFee ,TransportFee ,DeliverFee ,OtherFee,Rebate,TotalCharge ,NowPay ,PickPay ,CheckOutType ,CollectMoney ,ReturnAwb ,TrafficType ,DeliveryType ,SteveDore ,ShipperName ,ShipperUnit,ShipperAddress ,ShipperTelephone ,ShipperCellphone ,AcceptUnit ,AcceptAddress ,AcceptPeople ,AcceptTelephone ,AcceptCellphone ,HandleTime,PrintNum ,Remark ,DelFlag,OP_ID,CreateAwb,CreateDate,AwbStatus,ContractNum,TransKind,ReturnStatus,ReturnDate,SendReturnAwbStatus,SendReturnAwbDate,ConfirmReturnAwbStatus,ConfirmReturnAwbDate,ReturnInfo,ActMoney,ArriveDate,OP_DATE,FinanceFirstCheck,FirstCheckName,FirstCheckDate,FinanceSecondCheck,SecondCheckName,SecondCheckDate,AccountID,CheckStatus,Signer,SignTime,MidDest,TimeLimit,ClientNum,Sender,BelongSystem from Tbl_Awb_Arrive Where ArriveID=@ArriveID";
                    using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                    {
                        NewaySql.AddInParameter(cmdAdd, "@ArriveID", DbType.Int64, awb.ArriveID);
                        NewaySql.ExecuteNonQuery(cmdAdd);

                    }
                }
                //删除原运单数据
                string strDel = @"Delete from Tbl_Awb_Arrive WHERE ArriveID=@ArriveID";
                using (DbCommand cmddel = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmddel, "@ArriveID", DbType.Int64, entity[0].ArriveID);
                    NewaySql.ExecuteNonQuery(cmddel);
                    cmddel.Connection.Close();
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增中转运单数据
        /// </summary>
        public Int64 AddTransit(TransitEntity entity)
        {
            

            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_TransitAwb(CarrierID,StartTime,PreArriveTime,TransportFee,PrepayFee,ArriveFee,OtherFee,DeliveryFee,SendFee,CollectMoney,HandFee,Remark,CurCity,OP_ID,CheckOutType,AssistNum,PieceTotal,BelongSystem,Weight,WeightPrice,Volume,VolumePrice,Piece,PiecePrice) VALUES (@CarrierID,@StartTime,@PreArriveTime,@TransportFee,@PrepayFee,@ArriveFee,@OtherFee,@DeliveryFee,@SendFee,@CollectMoney,@HandFee,@Remark,@CurCity,@OP_ID,@CheckOutType,@AssistNum,@PieceTotal,@BelongSystem,@Weight,@WeightPrice,@Volume,@VolumePrice,@Piece,@PiecePrice) SELECT @@IDENTITY";
            try
            {
                entity.EnSafe();
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@CarrierID", DbType.Int64, entity.CarrierID);
                    NewaySql.AddInParameter(cmd, "@StartTime", DbType.DateTime, entity.StartTime);
                    NewaySql.AddInParameter(cmd, "@PreArriveTime", DbType.DateTime, entity.PreArriveTime);
                    NewaySql.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    NewaySql.AddInParameter(cmd, "@PrepayFee", DbType.Decimal, entity.PrepayFee);
                    NewaySql.AddInParameter(cmd, "@ArriveFee", DbType.Decimal, entity.ArriveFee);
                    NewaySql.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    NewaySql.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    NewaySql.AddInParameter(cmd, "@SendFee", DbType.Decimal, entity.SendFee);
                    NewaySql.AddInParameter(cmd, "@CollectMoney", DbType.Decimal, entity.CollectMoney);
                    NewaySql.AddInParameter(cmd, "@HandFee", DbType.Decimal, entity.HandFee);
                    NewaySql.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.AddInParameter(cmd, "@CurCity", DbType.String, entity.CurCity);
                    NewaySql.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    NewaySql.AddInParameter(cmd, "@AssistNum", DbType.String, entity.AssistNum);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(cmd, "@PieceTotal", DbType.Int32, entity.PieceTotal);
                    NewaySql.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    NewaySql.AddInParameter(cmd, "@PiecePrice", DbType.Decimal, entity.PiecePrice);
                    NewaySql.AddInParameter(cmd, "@WeightPrice", DbType.Decimal, entity.WeightPrice);
                    NewaySql.AddInParameter(cmd, "@Weight", DbType.Decimal, entity.Weight);
                    NewaySql.AddInParameter(cmd, "@Volume", DbType.Decimal, entity.Volume);
                    NewaySql.AddInParameter(cmd, "@VolumePrice", DbType.Decimal, entity.VolumePrice);

                    did = Convert.ToInt64(NewaySql.ExecuteScalar(cmd));

                }
                return did;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        #endregion
        #region 中转运单管理
        /// <summary>
        /// 中转运单管理查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Hashtable QueryTransitOrder(int pIndex, int pNum, TransitEntity entity)
        {
            List<TransitEntity> result = new List<TransitEntity>();
            
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += " (select ROW_NUMBER() OVER (ORDER BY b.StartTime DESC) AS RowNumber,b.* from (select distinct a.*,c.CarrierShortName,c.Boss,c.Telephone,c.Address,d.UserName from Tbl_TransitAwb as a left join Tbl_DeliveryAwb as b on a.TransitID=b.DeliveryID left join Tbl_Carrier as c on a.CarrierID=c.CarrierID  left join Tbl_SysUser as d on a.OP_ID=d.LoginName and a.BelongSystem=d.BelongSystem where (1=1)";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //Mode
                if (!string.IsNullOrEmpty(entity.Mode)) { strSQL += " and b.Mode = '" + entity.Mode + "'"; }
                //外协单号
                if (!string.IsNullOrEmpty(entity.AssistNum)) { strSQL += " and a.AssistNum = '" + entity.AssistNum + "'"; }
                //承运人简称
                if (!string.IsNullOrEmpty(entity.CarrierShortName)) { strSQL += " and c.CarrierShortName like '%" + entity.CarrierShortName + "%'"; }
                //运单号
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and b.AwbNo = '" + entity.AwbNo + "'"; }
                //一审
                if (!string.IsNullOrEmpty(entity.FinanceFirstCheck)) { strSQL += " and a.FinanceFirstCheck = '" + entity.FinanceFirstCheck + "'"; }
                //当前站点
                if (!string.IsNullOrEmpty(entity.CurCity))
                {
                    string[] ccs = entity.CurCity.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.CurCity in ('" + res + "')";
                }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += ") as b ) A ";
                strSQL += " WHERE A.RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new TransitEntity
                            {
                                TransitID = Convert.ToInt64(idr["TransitID"]),
                                CarrierID = Convert.ToInt64(idr["CarrierID"]),
                                CarrierShortName = Convert.ToString(idr["CarrierShortName"]),
                                Boss = Convert.ToString(idr["Boss"]).Trim(),
                                Address = Convert.ToString(idr["Address"]).Trim(),
                                Telephone = Convert.ToString(idr["Telephone"]).Trim(),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                StartTime = Convert.ToDateTime(idr["StartTime"]),
                                PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                ArriveFee = Convert.ToDecimal(idr["ArriveFee"]),
                                PrepayFee = Convert.ToDecimal(idr["PrepayFee"]),
                                DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                                SendFee = Convert.ToDecimal(idr["SendFee"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                HandFee = Convert.ToDecimal(idr["HandFee"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                PiecePrice = Convert.ToDecimal(idr["PiecePrice"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                WeightPrice = Convert.ToDecimal(idr["WeightPrice"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                VolumePrice = Convert.ToDecimal(idr["VolumePrice"]),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                AssistNum = Convert.ToString(idr["AssistNum"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                CurCity = Convert.ToString(idr["CurCity"]).Trim(),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                FinanceFirstCheck = Convert.ToString(idr["FinanceFirstCheck"]).Trim(),
                                FirstCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FirstCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FirstCheckDate"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim(),
                                FirstCheckName = Convert.ToString(idr["FirstCheckName"]).Trim(),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;
                #region 合计
                string strCount = @"Select Count(*) as TotalCount from (select distinct a.TransitID from Tbl_TransitAwb as a left join Tbl_DeliveryAwb as b on a.TransitID=b.DeliveryID left join Tbl_Carrier as c on a.CarrierID=c.CarrierID where (1=1)";
                strCount += " and  a.BelongSystem='" + entity.BelongSystem + "'";
                //Mode
                if (!string.IsNullOrEmpty(entity.Mode)) { strCount += " and b.Mode = '" + entity.Mode + "'"; }
                if (!string.IsNullOrEmpty(entity.AssistNum)) { strCount += " and a.AssistNum = '" + entity.AssistNum + "'"; }
                //司机姓名
                if (!string.IsNullOrEmpty(entity.CarrierShortName)) { strCount += " and c.CarrierShortName like '%" + entity.CarrierShortName + "%'"; }
                //运单号
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strCount += " and b.AwbNo = '" + entity.AwbNo + "'"; }
                //一审
                if (!string.IsNullOrEmpty(entity.FinanceFirstCheck)) { strCount += " and a.FinanceFirstCheck = '" + entity.FinanceFirstCheck + "'"; }
                //当前站点
                if (!string.IsNullOrEmpty(entity.CurCity))
                {
                    string[] ccs = entity.CurCity.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and a.CurCity in ('" + res + "')";
                }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strCount += " ) as c";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }

        /// <summary>
        /// 审核中转单
        /// </summary>
        /// <param name="entity"></param>
        public void CheckTransit(TransitEntity entity)
        {
            
            try
            {
                string strSQL = @"Update Tbl_TransitAwb set FinanceFirstCheck=@FinanceFirstCheck,FirstCheckName=@FirstCheckName,FirstCheckDate=@FirstCheckDate where TransitID=@TransitID";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@TransitID", DbType.Int64, entity.TransitID);
                    NewaySql.AddInParameter(cmdAdd, "@FinanceFirstCheck", DbType.String, entity.FinanceFirstCheck);
                    NewaySql.AddInParameter(cmdAdd, "@FirstCheckName", DbType.String, entity.FirstCheckName);
                    NewaySql.AddInParameter(cmdAdd, "@FirstCheckDate", DbType.DateTime, DateTime.Now);
                    NewaySql.ExecuteNonQuery(cmdAdd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除中转运单
        /// </summary>
        /// <param name="entity"></param>
        public void DelTransit(Int64 TransitID)
        {
            
            try
            {
                string strSQL = @"delete from Tbl_TransitAwb where TransitID=@TransitID";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmdAdd, "@TransitID", DbType.Int64, TransitID);
                    NewaySql.ExecuteNonQuery(cmdAdd);

                }
                //删除关联表
                DelDeliveryTransitAwb(TransitID, "1");
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 中转运单导出查询
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<TransitEntity> QueryTransitOrderForExport(TransitEntity entity)
        {
            List<TransitEntity> result = new List<TransitEntity>();
            
            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT a.*,c.CarrierShortName,c.Boss,c.Telephone,b.AwbID,b.AwbNo,ISNULL(d.CollectMoney,0) as ACollectMoney,ISNULL(d.CheckOutType,'') as AwbCheckOutType,ISNULL(d.TotalCharge,0) as TotalCharge,ISNULL(d.Piece,0) as AwbPiece,ISNULL(d.AwbPiece,0) as FenPiPiece,ISNULL(d.Weight,0) as AwbWeight,ISNULL(d.Volume,0) as AwbVolume,d.HandleTime,d.ShipperUnit,d.ShipperName,d.AcceptUnit,d.AcceptPeople,d.Dep,d.Dest,d.Transit  from Tbl_TransitAwb as a left join Tbl_DeliveryAwb as b on a.TransitID=b.DeliveryID left join Tbl_Awb_Arrive as d on  b.ArriveID=d.ArriveID left join Tbl_Carrier as c on a.CarrierID=c.CarrierID where (1=1)";
                strSQL += " and  a.BelongSystem='" + entity.BelongSystem + "'";
                //Mode
                if (!string.IsNullOrEmpty(entity.Mode)) { strSQL += " and b.Mode = '" + entity.Mode + "'"; }
                //外协单号
                if (!string.IsNullOrEmpty(entity.AssistNum)) { strSQL += " and a.AssistNum = '" + entity.AssistNum + "'"; }
                //承运人简称
                if (!string.IsNullOrEmpty(entity.CarrierShortName)) { strSQL += " and c.CarrierShortName like '%" + entity.CarrierShortName + "%'"; }
                //运单号
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and b.AwbNo = '" + entity.AwbNo + "'"; }
                //当前站点
                if (!string.IsNullOrEmpty(entity.CurCity))
                {
                    string[] ccs = entity.CurCity.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.CurCity in ('" + res + "')";
                }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.StartTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //一审
                if (!string.IsNullOrEmpty(entity.FinanceFirstCheck)) { strSQL += " and a.FinanceFirstCheck = '" + entity.FinanceFirstCheck + "'"; }
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            TransitEntity de = new TransitEntity();
                            de.TransitID = Convert.ToInt64(idr["TransitID"]);
                            de.CarrierID = Convert.ToInt64(idr["CarrierID"]);
                            de.CarrierShortName = Convert.ToString(idr["CarrierShortName"]);
                            de.Boss = Convert.ToString(idr["Boss"]).Trim();
                            de.Telephone = Convert.ToString(idr["Telephone"]).Trim();
                            de.TransportFee = Convert.ToDecimal(idr["TransportFee"]);
                            de.OtherFee = Convert.ToDecimal(idr["OtherFee"]);
                            de.StartTime = Convert.ToDateTime(idr["StartTime"]);
                            de.PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]);
                            // de.ArriveFee = Convert.ToDecimal(idr["ArriveFee"]);
                            //de.PrepayFee = Convert.ToDecimal(idr["PrepayFee"]);
                            de.DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]);
                            de.SendFee = Convert.ToDecimal(idr["SendFee"]);
                            de.CollectMoney = Convert.ToDecimal(idr["CollectMoney"]);
                            de.HandFee = Convert.ToDecimal(idr["HandFee"]);
                            de.Remark = Convert.ToString(idr["Remark"]).Trim();
                            de.AwbNo = Convert.ToString(idr["AwbNo"]).Trim();
                            de.AssistNum = Convert.ToString(idr["AssistNum"]).Trim();
                            de.OP_ID = Convert.ToString(idr["OP_ID"]).Trim();
                            de.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            de.CheckOutType = Convert.ToString(idr["CheckOutType"]);
                            de.FinanceFirstCheck = Convert.ToString(idr["FinanceFirstCheck"]);
                            de.Piece = Convert.ToInt32(idr["Piece"]);
                            de.Weight = Convert.ToDecimal(idr["Weight"]);
                            de.Volume = Convert.ToDecimal(idr["Volume"]);
                            de.AwbPiece = Convert.ToInt32(idr["AwbPiece"]);
                            de.FenPiPiece = Convert.ToInt32(idr["FenPiPiece"]);
                            de.AwbWeight = Convert.ToDecimal(idr["AwbWeight"]);
                            de.AwbVolume = Convert.ToDecimal(idr["AwbVolume"]);
                            de.PiecePrice = Convert.ToDecimal(idr["PiecePrice"]);
                            de.WeightPrice = Convert.ToDecimal(idr["WeightPrice"]);
                            de.VolumePrice = Convert.ToDecimal(idr["VolumePrice"]);
                            de.ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim();
                            de.AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim();
                            de.PrepayFee = Convert.ToDecimal(idr["ACollectMoney"]);//运单的代收款
                            de.HandleTime = string.IsNullOrEmpty(Convert.ToString(idr["HandleTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["HandleTime"]);
                            de.Dep = Convert.ToString(idr["Dep"]).Trim();
                            de.Dest = Convert.ToString(idr["Dest"]).Trim();
                            de.Transit = Convert.ToString(idr["Transit"]).Trim();
                            de.TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                            //if (Convert.ToString(idr["AwbCheckOutType"]).Trim().Equals("3"))
                            //{
                            //    de.TotalCharge = Convert.ToDecimal(idr["TotalCharge"]);
                            //}
                            ////如果到达库里没有该运单数据，就汇总代收款和到付款
                            //if (!IsExistArriveAwbByAwbNo(new AwbEntity { AwbNo = Convert.ToString(idr["AwbNo"]), AwbStatus = "3" }))
                            //{
                            //    de.CollectMoney += Convert.ToDecimal(idr["CollectMoney"]);
                            //    if (Convert.ToString(idr["AwbCheckOutType"]).Trim().Equals("3"))
                            //    {
                            //        de.TotalCharge += Convert.ToDecimal(idr["TotalCharge"]);
                            //    }
                            //}
                            result.Add(de);
                        }
                    }
                    //}
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 修改中转运单信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateTransit(TransitEntity entity)
        {
            
            string strSQL = @"UPDATE Tbl_TransitAwb SET CarrierID=@CarrierID,PrepayFee=@PrepayFee,ArriveFee=@ArriveFee,StartTime=@StartTime,PreArriveTime=@PreArriveTime,TransportFee=@TransportFee,OtherFee=@OtherFee,DeliveryFee=@DeliveryFee,SendFee=@SendFee,CollectMoney=@CollectMoney,HandFee=@HandFee,Remark=@Remark,OP_ID=@OP_ID,OP_DATE=@OP_DATE,CheckOutType=@CheckOutType,AssistNum=@AssistNum,PieceTotal=@PieceTotal,CurCity=@CurCity,Piece=@Piece,PiecePrice=@PiecePrice,Weight=@Weight,WeightPrice=@WeightPrice,Volume=@Volume,VolumePrice=@VolumePrice WHERE TransitID=@TransitID";
            try
            {
                entity.EnSafe();
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@CarrierID", DbType.String, entity.CarrierID);
                    NewaySql.AddInParameter(cmd, "@PrepayFee", DbType.Decimal, entity.PrepayFee);
                    NewaySql.AddInParameter(cmd, "@ArriveFee", DbType.Decimal, entity.ArriveFee);
                    NewaySql.AddInParameter(cmd, "@StartTime", DbType.DateTime, entity.StartTime);
                    NewaySql.AddInParameter(cmd, "@PreArriveTime", DbType.DateTime, entity.PreArriveTime);
                    NewaySql.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    NewaySql.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    NewaySql.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    NewaySql.AddInParameter(cmd, "@SendFee", DbType.Decimal, entity.SendFee);
                    NewaySql.AddInParameter(cmd, "@CollectMoney", DbType.Decimal, entity.CollectMoney);
                    NewaySql.AddInParameter(cmd, "@HandFee", DbType.Decimal, entity.HandFee);
                    NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    NewaySql.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    NewaySql.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    NewaySql.AddInParameter(cmd, "@TransitID", DbType.Int64, entity.TransitID);
                    NewaySql.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    NewaySql.AddInParameter(cmd, "@AssistNum", DbType.String, entity.AssistNum);
                    NewaySql.AddInParameter(cmd, "@PieceTotal", DbType.Int32, entity.PieceTotal);
                    NewaySql.AddInParameter(cmd, "@CurCity", DbType.String, entity.CurCity);
                    NewaySql.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    NewaySql.AddInParameter(cmd, "@PiecePrice", DbType.Decimal, entity.PiecePrice);
                    NewaySql.AddInParameter(cmd, "@WeightPrice", DbType.Decimal, entity.WeightPrice);
                    NewaySql.AddInParameter(cmd, "@Weight", DbType.Decimal, entity.Weight);
                    NewaySql.AddInParameter(cmd, "@Volume", DbType.Decimal, entity.Volume);
                    NewaySql.AddInParameter(cmd, "@VolumePrice", DbType.Decimal, entity.VolumePrice);
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        #endregion
        #region 中转状态跟踪
        /// <summary>
        /// 中转运单查询
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryTransitAwbStatusTrack(int pIndex, int pNum, TransitEntity entity)
        {
            List<AwbStatusTrackEntity> result = new List<AwbStatusTrackEntity>();
            
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询条件
                string strSQL = @" SELECT distinct TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT ROW_NUMBER() OVER (ORDER BY c.HandleTime DESC) AS RowNumber,b.AwbID,b.AwbNo,b.ArriveID,b.Mode,c.ShipperUnit,c.ShipperName,c.ShipperAddress,c.ShipperTelephone,c.ShipperCellphone,c.AcceptUnit,c.AcceptAddress,c.AcceptPeople,c.AcceptTelephone,c.AcceptCellphone,c.Dep,c.Dest,c.Transit,c.Piece,c.AwbPiece,c.Weight,c.Volume,c.HandleTime,c.CreateAwb,c.DeliveryType,c.CheckOutType,c.AwbStatus,DATEADD(day,c.TimeLimit,c.HandleTime) as LatestTimeLimit,c.ArriveDate from  Tbl_DeliveryAwb as b  left join Tbl_Awb_Arrive as c on b.ArriveID=c.ArriveID  WHERE (1=1) and  (b.Mode=1 or  b.Mode=0) and c.Piece is not null ";
                strSQL += " and c.BelongSystem='" + entity.BelongSystem + "'";
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and c.AwbNo='" + entity.AwbNo + "'"; }
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.FinanceFirstCheck)) { strSQL += " and c.AwbStatus='" + entity.FinanceFirstCheck + "'"; }
                //发货
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strSQL += " and (c.ShipperUnit like '%" + entity.ShipperUnit + "%' or c.ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                ////按承运商查询
                //if (!string.IsNullOrEmpty(entity.CarrierShortName))
                //{
                //    strSQL += " and d.CarrierShortName like'%" + entity.CarrierShortName + "%'";
                //}
                ////外协单号
                //if (!string.IsNullOrEmpty(entity.AssistNum))
                //{
                //    strSQL += " and a.AssistNum='" + entity.AssistNum + "'";
                //}
                ////中转单号
                //if (!entity.TransitID.Equals(0))
                //{
                //    strSQL += " and a.TransitID=" + entity.TransitID + "";
                //}
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++) { res += ccs[i] + "','"; }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and c.Dest in ('" + res + "')";
                }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and c.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and c.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                good += Convert.ToString(idrGoods["Goods"]) + ",";
                            }
                            #endregion
                            AwbStatusTrackEntity aste = new AwbStatusTrackEntity();
                            if (!string.IsNullOrEmpty(Convert.ToString(idr["Mode"])))
                            {
                                if (Convert.ToString(idr["Mode"]).Equals("0"))//配送
                                {
                                    List<DeliveryEntity> dEnt = QueryDeliveryByAwb(Convert.ToInt64(idr["AwbID"]), Convert.ToString(idr["AwbNo"]), entity.BelongSystem);
                                    if (dEnt.Count > 0)
                                    {
                                        aste.TTime = dEnt[0].CreateTime;
                                    }
                                }
                                if (Convert.ToString(idr["Mode"]).Equals("1"))//中转
                                {
                                    List<TransitEntity> dEnt = QueryTransitByAwb(Convert.ToInt64(idr["AwbID"]), Convert.ToString(idr["AwbNo"]), entity.BelongSystem);
                                    if (dEnt.Count > 0)
                                    {
                                        aste.TTime = dEnt[0].OP_DATE;
                                    }
                                }
                            }
                            #region 获取运单数据
                            result.Add(new AwbStatusTrackEntity
                            {
                                //TransitID = Convert.ToInt64(idr["TransitID"]),
                                //CarrierID = Convert.ToInt64(idr["CarrierID"]),
                                //AssistNum = Convert.ToString(idr["AssistNum"]).Trim(),
                                //CarrierShortName = Convert.ToString(idr["CarrierShortName"]).Trim(),
                                //CarrierName = Convert.ToString(idr["CarrierName"]).Trim(),
                                //DriverCellPhone = Convert.ToString(idr["Telephone"]).Trim(),//承运人联系电话
                                //StartTime = Convert.ToDateTime(idr["StartTime"]),
                                HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                ArriveID = Convert.ToInt64(idr["ArriveID"]),
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                LatestTimeLimit = Convert.ToString(idr["LatestTimeLimit"]),
                                Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                ArriveDate = Convert.ToDateTime(idr["ArriveDate"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                TruckFlag = Convert.ToString(idr["AwbStatus"]),
                                TTime = aste.TTime,
                                Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 查询总数
                string strCount = @"select COUNT(*) as TotalCount from Tbl_DeliveryAwb as b left join Tbl_Awb_Arrive as c on b.ArriveID=c.ArriveID  where 1=1 and ( b.Mode=1 or  b.Mode=0 ) and c.Piece is not null ";
                strCount += " and c.BelongSystem='" + entity.BelongSystem + "'";
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    strCount += " and c.AwbNo='" + entity.AwbNo + "'";
                }
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.FinanceFirstCheck))
                {
                    strCount += " and c.AwbStatus='" + entity.FinanceFirstCheck + "'";
                }
                //发货
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strCount += " and (c.ShipperUnit like '%" + entity.ShipperUnit + "%' or c.ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                ////按承运商查询
                //if (!string.IsNullOrEmpty(entity.CarrierShortName))
                //{
                //    strCount += " and d.CarrierShortName like'%" + entity.CarrierShortName + "%'";
                //}
                ////外协单号
                //if (!string.IsNullOrEmpty(entity.AssistNum))
                //{
                //    strCount += " and a.AssistNum='" + entity.AssistNum + "'";
                //}
                ////中转单号
                //if (!entity.TransitID.Equals(0))
                //{
                //    strCount += " and a.TransitID=" + entity.TransitID + "";
                //}
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++) { res += ccs[i] + "','"; }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and c.Dest in ('" + res + "')";
                }
                //发车时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and c.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and c.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 通过运单ID和运单号查询配送数据
        /// </summary>
        /// <param name="awbid"></param>
        /// <param name="awbno"></param>
        /// <returns></returns>
        public List<DeliveryEntity> QueryDeliveryByAwb(Int64 awbid, string awbno, string BelongSystem)
        {
            
            List<DeliveryEntity> result = new List<DeliveryEntity>();
            try
            {
                string strDel = @"select CreateTime,Driver,DriverCellPhone,TruckNum from Tbl_DeliveryAwb as a left join Tbl_Delivery as b on a.DeliveryID=b.DeliveryID where a.Mode=0 and a.AwbID=@AwbID and a.AwbNo=@AwbNo and a.BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awbid);
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awbno);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, BelongSystem);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new DeliveryEntity
                            {
                                CreateTime = string.IsNullOrEmpty(Convert.ToString(idr["CreateTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CreateTime"]),
                                Driver = Convert.ToString(idr["Driver"]).Trim(),
                                DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                TruckNum = Convert.ToString(idr["TruckNum"]).Trim()
                            });
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 通过运单号查询中转信息
        /// </summary>
        /// <param name="awbid"></param>
        /// <param name="awbno"></param>
        /// <returns></returns>
        public List<TransitEntity> QueryTransitByAwb(Int64 awbid, string awbno, string BelongSystem)
        {
            
            List<TransitEntity> result = new List<TransitEntity>();
            try
            {
                string strDel = @"select b.AssistNum,b.OP_DATE,c.CarrierShortName,c.Telephone,a.ArriveID from Tbl_DeliveryAwb as a left join Tbl_TransitAwb as b on a.DeliveryID=b.TransitID left join Tbl_Carrier as c on b.CarrierID=c.CarrierID where a.Mode=1  and a.AwbID=@AwbID and a.AwbNo=@AwbNo and a.BelongSystem=@BelongSystem";
                using (DbCommand cmdAdd = NewaySql.GetSqlStringCommond(strDel))
                {
                    NewaySql.AddInParameter(cmdAdd, "@AwbID", DbType.Int64, awbid);
                    NewaySql.AddInParameter(cmdAdd, "@BelongSystem", DbType.String, BelongSystem);
                    NewaySql.AddInParameter(cmdAdd, "@AwbNo", DbType.String, awbno);
                    using (DataTable dt = NewaySql.ExecuteDataTable(cmdAdd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new TransitEntity
                            {
                                ArriveID = Convert.ToInt64(idr["ArriveID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                AssistNum = Convert.ToString(idr["AssistNum"]).Trim(),
                                CarrierShortName = Convert.ToString(idr["CarrierShortName"]).Trim(),
                                Telephone = Convert.ToString(idr["Telephone"]).Trim()
                            });
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }

        /// <summary>
        /// 查询所有运单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AwbEntity> QueryAwb(AwbEntity entity)
        {
            List<AwbEntity> result = new List<AwbEntity>();
            

            try
            {
                entity.EnSafe();
                string strSQL = @" SELECT b.UserName,a.*,DATEADD(day,a.TimeLimit,a.HandleTime) as LatestTimeLimit FROM Tbl_Awb_Basic as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem Where (1=1) ";
                if (!string.IsNullOrEmpty(entity.BelongSystem)) { strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'"; }
                #region 组装查询SQL语句
                if (!string.IsNullOrEmpty(entity.HAwbNo))
                {
                    strSQL += " and a.HAwbNo = '" + entity.HAwbNo + "'";
                }
                //本站自发的运单
                if (!string.IsNullOrEmpty(entity.TransKind))
                {
                    strSQL += " and a.TransKind = '" + entity.TransKind + "'";
                }
                //回单状态
                if (!string.IsNullOrEmpty(entity.ReturnStatus))
                {
                    strSQL += " and a.ReturnStatus = '" + entity.ReturnStatus + "'";
                }
                //回单发送状态
                if (!string.IsNullOrEmpty(entity.SendReturnAwbStatus))
                {
                    strSQL += " and a.SendReturnAwbStatus='" + entity.SendReturnAwbStatus + "'";
                }
                //回单确认状态
                if (!string.IsNullOrEmpty(entity.ConfirmReturnAwbStatus))
                {
                    strSQL += " and a.ConfirmReturnAwbStatus='" + entity.ConfirmReturnAwbStatus + "'";
                }
                //回单发送人
                if (!string.IsNullOrEmpty(entity.Sender))
                {
                    strSQL += " and a.Sender='" + entity.Sender + "'";
                }
                //运单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    strSQL += " and a.AwbStatus = '" + entity.AwbStatus + "'";
                }
                if (entity.DelFlag.Equals("1")) { strSQL += " and a.DelFlag = '" + entity.DelFlag + "'"; }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType))
                {
                    strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'";
                }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dep in ('" + res + "')";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dest in ('" + res + "')";
                }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " and a.AwbNo = '" + entity.AwbNo.ToUpper().Trim() + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb = '" + entity.CreateAwb + "'"; }
                //发货单位，人
                if (!string.IsNullOrEmpty(entity.ShipperUnit))
                {
                    strSQL += " and (a.ShipperUnit like '%" + entity.ShipperUnit + "%' or a.ShipperName like '%" + entity.ShipperUnit + "%')";
                }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople))
                {
                    strSQL += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')";
                }
                //回单发送日期范围
                if ((entity.SStartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.SStartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.SendReturnAwbDate>='" + entity.SStartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.SEndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.SEndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.SendReturnAwbDate<'" + entity.SEndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (string.IsNullOrEmpty(entity.ReturnDateTip))
                {
                    //制单日期范围
                    if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strSQL += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                    }
                    if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strSQL += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                    }
                }
                else
                {
                    //发送回单日期范围
                    if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strSQL += " and a.SendReturnAwbDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                    }
                    if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        strSQL += " and a.SendReturnAwbDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                    }
                }
                //件数
                if (!entity.Piece.Equals(0))
                {
                    strSQL += " and a.Piece=" + entity.Piece + "";
                }
                strSQL += " Order By a.HandleTime DESC,a.ReturnStatus";
                #endregion
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };

                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                goodList.Add(new AwbGoodsEntity
                                {
                                    GoodsID = Convert.ToInt32(idrGoods["GoodsID"]),
                                    AwbNo = Convert.ToString(idrGoods["AwbNo"]).Trim(),
                                    Package = Convert.ToString(idrGoods["Package"]).Trim(),
                                    Goods = Convert.ToString(idrGoods["Goods"]).Trim(),
                                    Weight = Convert.ToDecimal(idrGoods["Weight"]),
                                    WeightPrice = Convert.ToDecimal(idrGoods["WeightPrice"]),
                                    Volume = Convert.ToDecimal(idrGoods["Volume"]),
                                    VolumePrice = Convert.ToDecimal(idrGoods["VolumePrice"]),
                                    Piece = Convert.ToInt32(idrGoods["Piece"]),
                                    PiecePrice = Convert.ToDecimal(idrGoods["PiecePrice"]),
                                    DeclareValue = Convert.ToString(idrGoods["DeclareValue"]).Trim(),
                                    OP_ID = Convert.ToString(idrGoods["OP_ID"]).Trim().Trim(),
                                    OP_DATE = Convert.ToDateTime(idrGoods["OP_DATE"])
                                });

                                good += Convert.ToString(idrGoods["Goods"]).Trim() + ",";
                            }
                            #endregion
                            #region 获取运单数据
                            result.Add(new AwbEntity
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]).Trim() : Convert.ToString(idr["Transit"]).Trim(),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                Attach = Convert.ToInt32(idr["Attach"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                NowPay = Convert.ToDecimal(idr["NowPay"]),
                                PickPay = Convert.ToDecimal(idr["PickPay"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                SteveDore = Convert.ToString(idr["SteveDore"]).Trim(),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                ShipperAddress = Convert.ToString(idr["ShipperAddress"]).Trim(),
                                ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 0 : Convert.ToInt16(idr["PrintNum"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]).Trim(),
                                BelongSystem = Convert.ToString(idr["BelongSystem"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                ArriveDate = string.IsNullOrEmpty(Convert.ToString(idr["ArriveDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ArriveDate"]),
                                ActMoney = Convert.ToDecimal(idr["ActMoney"]),
                                SendReturnAwbStatus = Convert.ToString(idr["SendReturnAwbStatus"]).Trim(),
                                Sender = Convert.ToString(idr["Sender"]).Trim(),
                                SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                ConfirmReturnAwbStatus = Convert.ToString(idr["ConfirmReturnAwbStatus"]).Trim(),
                                ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                FinanceFirstCheck = Convert.ToString(idr["FinanceFirstCheck"]).Trim(),
                                FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]).Trim(),
                                FirstCheckName = Convert.ToString(idr["FirstCheckName"]).Trim(),
                                FirstCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FirstCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FirstCheckDate"]),
                                SecondCheckName = Convert.ToString(idr["SecondCheckName"]).Trim(),
                                SecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["SecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SecondCheckDate"]),
                                ClientNum = Convert.ToString(idr["ClientNum"]).Trim(),
                                ClerkNo = Convert.ToString(idr["ClerkNo"]).Trim(),
                                ClerkName = Convert.ToString(idr["ClerkName"]).Trim(),
                                HLY = Convert.ToString(idr["HLY"]).Trim(),
                                AwbGoods = goodList,
                                Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                            });
                            #endregion
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询运单跟踪状态信息
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public List<AwbStatus> QueryAwbTrackInfo(AwbStatus ent)
        {
            List<AwbStatus> result = new List<AwbStatus>();
            

            try
            {
                //string strSQL = @"select a.*,b.UserName,c.FilePath,c.TbFilePath from Tbl_AwbStatusTruck as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem left join Tbl_Awb_Files as c on a.AwbID=c.AwbID and a.AwbNo=c.AwbNo where a.AwbNo=@AwbNo and a.BelongSystem=@BelongSystem";
                string strSQL = @"select a.*,b.UserName from Tbl_AwbStatusTruck as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem  where a.AwbNo=@AwbNo and a.BelongSystem=@BelongSystem";
                if (ent.AwbID != 0)
                {
                    strSQL += " and a.AwbID=@AwbID ";
                }
                strSQL += " order by a.OP_DATE Asc";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    if (ent.AwbID != 0)
                    {
                        NewaySql.AddInParameter(command, "@AwbID", DbType.Int64, ent.AwbID);
                    }
                    NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, ent.BelongSystem);
                    NewaySql.AddInParameter(command, "@AwbNo", DbType.String, ent.AwbNo);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            result = QueryMaxLateAwbInfo(ent);
                        }
                        if (result.Count > 0)
                        {
                            foreach (DataRow idr in dt.Rows)
                            {
                                if (Convert.ToString(idr["TruckFlag"]).Trim().Equals("0") || Convert.ToString(idr["TruckFlag"]).Trim().Equals("1") || Convert.ToString(idr["TruckFlag"]).Trim().Equals("2") || Convert.ToString(idr["TruckFlag"]).Trim().Equals("6") || Convert.ToString(idr["TruckFlag"]).Trim().Equals("11") || Convert.ToString(idr["TruckFlag"]).Trim().Equals("10") || Convert.ToString(idr["TruckFlag"]).Trim().Equals("4") || Convert.ToString(idr["TruckFlag"]).Trim().Equals("12"))
                                {
                                    if (result.Where(c => c.TruckFlag.Equals(Convert.ToString(idr["TruckFlag"]).Trim())).Count() > 0)
                                    {
                                        AwbStatus ast = result.Find(c => c.TruckFlag.Equals(Convert.ToString(idr["TruckFlag"]).Trim()));
                                        if (ast.OP_DATE <= Convert.ToDateTime(idr["OP_DATE"])) { continue; }
                                        result.Remove(ast);
                                    }
                                    #region 赋值
                                    result.Add(new AwbStatus
                                    {
                                        AwbID = Convert.ToInt64(idr["AwbID"]),
                                        AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                        TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                        CurrentLocation = Convert.ToString(idr["CurrentLocation"]).Trim(),
                                        ArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ArriveTime"]),
                                        DetailInfo = Convert.ToString(idr["DetailInfo"]).Trim(),
                                        OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                        OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                        Signer = Convert.ToString(idr["Signer"]).Trim(),
                                        BelongSystem = Convert.ToString(idr["BelongSystem"]).Trim(),
                                        UserName = Convert.ToString(idr["UserName"]).Trim(),
                                        Latitude = Convert.ToString(idr["Latitude"]),
                                        Longitude = Convert.ToString(idr["Longitude"])
                                        //FilePath = Convert.ToString(idr["FilePath"]).Trim(),
                                        //TbFilePath = Convert.ToString(idr["TbFilePath"]).Trim()
                                    });
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow idr in dt.Rows)
                            {
                                List<AwbFilesEntity> file = new List<AwbFilesEntity>();
                                if (Convert.ToString(idr["TruckFlag"]).Equals("7"))
                                {
                                    //签收 ，查询号签收照片
                                    file = QueryAwbFilesByAwbNo(new AwbEntity { AwbID = Convert.ToInt64(idr["AwbID"]), AwbNo = Convert.ToString(idr["AwbNo"]), BelongSystem = Convert.ToString(idr["BelongSystem"]) });
                                }
                                #region 赋值
                                result.Add(new AwbStatus
                                {
                                    AwbID = Convert.ToInt64(idr["AwbID"]),
                                    AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                    TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                    CurrentLocation = Convert.ToString(idr["CurrentLocation"]).Trim(),
                                    ArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ArriveTime"]),
                                    DetailInfo = Convert.ToString(idr["DetailInfo"]).Trim(),
                                    OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                    Signer = Convert.ToString(idr["Signer"]).Trim(),
                                    BelongSystem = Convert.ToString(idr["BelongSystem"]).Trim(),
                                    UserName = Convert.ToString(idr["UserName"]).Trim(),
                                    Latitude = Convert.ToString(idr["Latitude"]),
                                    Longitude = Convert.ToString(idr["Longitude"]),
                                    Files = file
                                    //FilePath = Convert.ToString(idr["FilePath"]).Trim(),
                                    //TbFilePath = Convert.ToString(idr["TbFilePath"]).Trim()
                                });
                                #endregion
                            }
                        }
                    }
                }
                result.Sort(delegate(AwbStatus p1, AwbStatus p2) { return Comparer<DateTime>.Default.Compare(p2.OP_DATE, p1.OP_DATE); });
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 查询最晚到达的一批运单数据
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        private List<AwbStatus> QueryMaxLateAwbInfo(AwbStatus ent)
        {
            List<AwbStatus> result = new List<AwbStatus>();
            

            try
            {
                string strSQL = @"select a.*,b.UserName from  (select * from Tbl_AwbStatusTruck where AwbID=(select top 1 AwbID from Tbl_AwbStatusTruck where OP_DATE=(select max(OP_DATE) from Tbl_AwbStatusTruck where AwbNo=@AwbNo and TruckFlag='3') and AwbNo=@AwbNo)) as a left join Tbl_SysUser as b on a.OP_ID=b.LoginName and a.BelongSystem=b.BelongSystem where a.AwbNo=@AwbNo and a.BelongSystem=@BelongSystem";
                if (ent.AwbID != 0)
                {
                    strSQL += " and a.AwbID=@AwbID ";
                }
                //strSQL += " order by a.OP_DATE Asc";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    if (ent.AwbID != 0)
                    {
                        NewaySql.AddInParameter(command, "@AwbID", DbType.Int64, ent.AwbID);
                    }
                    NewaySql.AddInParameter(command, "@AwbNo", DbType.String, ent.AwbNo);
                    NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, ent.BelongSystem);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            List<AwbFilesEntity> file = new List<AwbFilesEntity>();
                            if (Convert.ToString(idr["TruckFlag"]).Equals("7"))
                            {
                                //签收 ，查询号签收照片
                                file = QueryAwbFilesByAwbNo(new AwbEntity { AwbID = Convert.ToInt64(idr["AwbID"]), AwbNo = Convert.ToString(idr["AwbNo"]), BelongSystem = Convert.ToString(idr["BelongSystem"]) });
                            }
                            #region 赋值
                            result.Add(new AwbStatus
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                CurrentLocation = Convert.ToString(idr["CurrentLocation"]).Trim(),
                                ArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ArriveTime"]),
                                DetailInfo = Convert.ToString(idr["DetailInfo"]).Trim(),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                Signer = Convert.ToString(idr["Signer"]).Trim(),
                                Latitude = Convert.ToString(idr["Latitude"]),
                                Longitude = Convert.ToString(idr["Longitude"]),
                                BelongSystem = Convert.ToString(idr["BelongSystem"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                Files = file
                            });
                            #endregion
                        }
                    }

                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 通过运单号和运单ID查询上传的回单照片数据列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AwbFilesEntity> QueryAwbFilesByAwbNo(AwbEntity entity)
        {
            List<AwbFilesEntity> result = new List<AwbFilesEntity>();
            
            try
            {
                string strSQL = @" select * from Tbl_Awb_Files where AwbNo=@AwbNo and AwbID=@AwbID and BelongSystem=@BelongSystem";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(command, "@AwbID", DbType.Int64, entity.AwbID);
                    NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, entity.BelongSystem);
                    NewaySql.AddInParameter(command, "@AwbNo", DbType.String, entity.AwbNo);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new AwbFilesEntity
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                FileName = Convert.ToString(idr["FileName"]).Trim(),
                                FilePath = Convert.ToString(idr["FilePath"]).Trim(),
                                TbFilePath = Convert.ToString(idr["TbFilePath"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 保存中转运单状态跟踪信息
        /// </summary>
        /// <param name="ent"></param>
        public void SaveAwbTruckStatus(TruckStatusTrackEntity ent)
        {
            
            try
            {
                ent.EnSafe();
                //插入运单状态
                InsertAwbStatus(new AwbStatus
                {
                    AwbID = Convert.ToInt64(ent.TruckNum),
                    AwbNo = ent.ContractNum,
                    TruckFlag = ent.TruckFlag,
                    CurrentLocation = ent.CurrentLocation,
                    LastHour = ent.LastHour,
                    OP_ID = ent.OP_ID,
                    DetailInfo = ent.DetailInfo,
                    BelongSystem = ent.BelongSystem,
                    ArriveTime = ent.ArriveTime
                });
                //修改运单状态设置运单的状态为在途和客户
                SetAwbStatus(new AwbEntity
                {
                    AwbStatus = ent.TruckFlag,
                    DelFlag = ent.TruckFlag,
                    BelongSystem = ent.BelongSystem,
                    GiveTime = ent.ArriveTime,
                    AwbID = Convert.ToInt64(ent.TruckNum)
                });
                //修改到达运单的状态
                SetArriveAwbStatus(new AwbEntity
                {
                    AwbStatus = ent.TruckFlag,
                    DelFlag = ent.TruckFlag,
                    BelongSystem = ent.BelongSystem,
                    GiveTime = ent.ArriveTime,
                    ArriveID = ent.ArriveID
                });
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
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
            List<AwbStatusTrackEntity> result = new List<AwbStatusTrackEntity>();
            
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询条件
                string strSQL = @" SELECT distinct TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT ROW_NUMBER() OVER (ORDER BY a.HandleTime DESC) AS RowNumber,a.AwbID,a.AwbNo,a.HAwbNo,a.Dep,a.Dest,a.Piece,a.AwbPiece,a.AwbWeight,a.AwbVolume,a.HandleTime,a.ShipperUnit,a.ShipperName,a.ShipperTelephone,a.ShipperCellphone,a.AcceptUnit,a.AcceptAddress,a.AcceptPeople,a.AcceptTelephone,a.AcceptCellphone,a.DeliveryType,ISNULL(b.ContractNum,'') as ContractNum,ISNULL(b.TruckNum,'') as TruckNum,ISNULL(b.Driver,'') as Driver,ISNULL(b.DriverCellPhone,'') as DriverCellPhone,ISNULL((b.TransportFee+b.OtherFee+b.DeliveryFee+b.SendFee+b.HandFee),0) as ContractFee,ISNULL(a.AwbStatus,'') as TruckFlag,ISNULL(b.StartTime,'') as StartTime,ISNULL(b.PreArriveTime,'') as PreArriveTime,b.ActArriveTime,a.ReturnInfo,a.ReturnStatus,a.InsuranceFee,a.TransportFee,a.TransitFee,a.DeliverFee,a.OtherFee,a.TotalCharge,a.Rebate,a.CheckOutType,a.CollectMoney,a.Remark,a.TransKind,a.CreateAwb,a.CreateDate,a.TrafficType,a.CheckStatus,a.Signer,a.Transit,a.MidDest,a.ReturnAwb,a.SignTime,a.SendReturnAwbDate,a.ConfirmReturnAwbDate,a.TimeLimit,DATEADD(day,a.TimeLimit,a.HandleTime) as LatestTimeLimit,a.UploadReturnPic FROM Tbl_Awb_Basic AS a LEFT JOIN Tbl_DepManifest AS b ON a.ContractNum=b.ContractNum LEFT JOIN Tbl_Awb_Goods AS c ON a.AwbNo=c.AwbNo WHERE  a.DelFlag<>1 ";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //查询所有已审核过的出发和在途的，到达的，关注的b.ContractNum IS NOT NULL and b.DelFlag=1and (d.Mode<>2 or d.Mode is null)
                //strSQL += " and (b.TruckFlag = '1' or b.TruckFlag = '2' or b.TruckFlag='3' or b.TruckFlag='5')";
                //运输性质自发还是外协
                if (!string.IsNullOrEmpty(entity.TransKind)) { strSQL += " and a.TransKind = '" + entity.TransKind + "'"; }
                //查询所有已审核过的出发和在途的，到达的，关注的
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strSQL += " and a.AwbStatus = '" + entity.DelFlag + "'"; }
                //结算状态
                if (!string.IsNullOrEmpty(entity.CheckStatus)) { strSQL += " and a.CheckStatus = '" + entity.CheckStatus + "'"; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dep in ('" + res + "')";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest)) { strSQL += " and a.Dest='" + entity.Dest + "'"; }
                //中间站
                if (!string.IsNullOrEmpty(entity.MidDest)) { strSQL += " and a.MidDest='" + entity.MidDest + "'"; }
                //中转站
                if (!string.IsNullOrEmpty(entity.Transit)) { strSQL += " and a.Transit='" + entity.Transit + "'"; }
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    string[] ccs = entity.AwbNo.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.AwbNo in ('" + res + "')";
                }
                //按副单号查询
                if (!string.IsNullOrEmpty(entity.HAwbNo)) { strSQL += " and a.HAwbNo like '%" + entity.HAwbNo + "%'"; }
                //按客户名称查询
                if (!string.IsNullOrEmpty(entity.ShipperUnit)) { strSQL += " and (a.ShipperUnit like '%" + entity.ShipperUnit + "%' or a.ShipperName like '%" + entity.ShipperUnit + "%')"; }
                //按收货人查询
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')"; }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and a.Piece=" + entity.Piece + ""; }
                //送货方式
                if (!string.IsNullOrEmpty(entity.DeliveryType))
                {
                    strSQL += " and a.DeliveryType = '" + entity.DeliveryType + "'";
                }
                //货物品名
                if (!string.IsNullOrEmpty(entity.Goods))
                {
                    strSQL += " and c.Goods like '%" + entity.Goods + "%'";
                }
                //预计到达时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //开单时间
                if (!entity.CStartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.CStartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.CStartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.CEndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.CEndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.CEndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                strSQL += " order by HandleTime desc";
                #endregion
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            if (result.Where(c => c.AwbID.Equals(Convert.ToInt64(idr["AwbID"]))).Count() > 0) { continue; }
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };
                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                good += Convert.ToString(idrGoods["Goods"]) + ",";

                            }
                            #endregion

                            List<DeliveryEntity> delist = QueryDeliveryByAwb(Convert.ToInt64(idr["AwbID"]), Convert.ToString(idr["AwbNo"]), entity.BelongSystem);
                            foreach (var i in delist)
                            {
                                #region 获取运单数据
                                result.Add(new AwbStatusTrackEntity
                                {
                                    AwbID = Convert.ToInt64(idr["AwbID"]),
                                    AwbNo = Convert.ToString(idr["AwbNo"]),
                                    Dep = Convert.ToString(idr["Dep"]),
                                    Dest = Convert.ToString(idr["Dest"]),
                                    Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"]).Trim()) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                    MidDest = Convert.ToString(idr["MidDest"]),
                                    Piece = Convert.ToInt32(idr["Piece"]),
                                    AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                    AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                    AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                    HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                    ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                    ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                    ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                    ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                    ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                    AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                    AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                    AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                    AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                    AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                    AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                    ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                    DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                    TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                    Driver = Convert.ToString(idr["Driver"]).Trim(),
                                    DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                    TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                    StartTime = Convert.ToDateTime(idr["StartTime"]),
                                    PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                    ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"]),
                                    ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                    ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                    //UserName = Convert.ToString(idr["UserName"]).Trim(),
                                    InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                    TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                    TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                    DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                    OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                    TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                    Rebate = Convert.ToDecimal(idr["Rebate"]),
                                    CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                    CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                    Remark = Convert.ToString(idr["Remark"]).Trim(),
                                    TransKind = Convert.ToString(idr["TransKind"]).Trim(),
                                    CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                    CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                    TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                    TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                    LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                    CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim(),
                                    HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                    ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                    Signer = Convert.ToString(idr["Signer"]).Trim(),
                                    SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                    SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                    ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                    ContractFee = Convert.ToDecimal(idr["ContractFee"]),
                                    UploadReturnPic = Convert.ToString(idr["UploadReturnPic"]).Trim(),
                                    DTime = i.CreateTime,
                                    DDrive = i.Driver,
                                    DTruckNum = i.TruckNum,
                                    DPhone = i.DriverCellPhone,
                                    //AwbGoods = goodList,
                                    Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                                });
                                #endregion
                            }
                            if (delist.Count <= 0)
                            {
                                List<TransitEntity> telist = QueryTransitByAwb(Convert.ToInt64(idr["AwbID"]), Convert.ToString(idr["AwbNo"]), entity.BelongSystem);
                                if (telist.Count > 0)
                                {
                                    foreach (var i in telist)
                                    {
                                        #region 获取运单数据
                                        result.Add(new AwbStatusTrackEntity
                                        {
                                            AwbID = Convert.ToInt64(idr["AwbID"]),
                                            AwbNo = Convert.ToString(idr["AwbNo"]),
                                            HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                            Dep = Convert.ToString(idr["Dep"]),
                                            Dest = Convert.ToString(idr["Dest"]),
                                            ArriveID = i.ArriveID,
                                            Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"]).Trim()) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                            MidDest = Convert.ToString(idr["MidDest"]),
                                            Piece = Convert.ToInt32(idr["Piece"]),
                                            AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                            AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                            AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                            HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                            ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                            ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                            ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                            ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                            ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                            AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                            ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                            DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                            TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                            Driver = Convert.ToString(idr["Driver"]).Trim(),
                                            DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                            TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                            StartTime = Convert.ToDateTime(idr["StartTime"]),
                                            PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                            ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"]),
                                            ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                            ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                            //UserName = Convert.ToString(idr["UserName"]).Trim(),
                                            InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                            DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                            OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                            Rebate = Convert.ToDecimal(idr["Rebate"]),
                                            CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                            CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                            Remark = Convert.ToString(idr["Remark"]).Trim(),
                                            TransKind = Convert.ToString(idr["TransKind"]).Trim(),
                                            CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                            TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                            TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                            LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                            CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim(),
                                            ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                            Signer = Convert.ToString(idr["Signer"]).Trim(),
                                            SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                            SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                            ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                            ContractFee = Convert.ToDecimal(idr["ContractFee"]),
                                            UploadReturnPic = Convert.ToString(idr["UploadReturnPic"]).Trim(),
                                            TTime = i.OP_DATE,
                                            TAssistNum = i.AssistNum,
                                            TPhone = i.Telephone,
                                            TShortName = i.CarrierShortName,
                                            //AwbGoods = goodList,
                                            Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                                        });
                                        #endregion
                                    }
                                }
                                else
                                {
                                    #region 获取运单数据
                                    result.Add(new AwbStatusTrackEntity
                                    {
                                        AwbID = Convert.ToInt64(idr["AwbID"]),
                                        AwbNo = Convert.ToString(idr["AwbNo"]),
                                        HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                        Dep = Convert.ToString(idr["Dep"]),
                                        Dest = Convert.ToString(idr["Dest"]),
                                        Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"]).Trim()) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                        MidDest = Convert.ToString(idr["MidDest"]),
                                        Piece = Convert.ToInt32(idr["Piece"]),
                                        AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                        AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                        AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                        HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                        ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                        ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                        ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                        ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                        ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                        AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                        AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                        AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                        AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                        AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                        AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                        ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                        DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                        TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                        Driver = Convert.ToString(idr["Driver"]).Trim(),
                                        DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                        TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                        StartTime = Convert.ToDateTime(idr["StartTime"]),
                                        PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                        ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"]),
                                        ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                        ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                        //UserName = Convert.ToString(idr["UserName"]).Trim(),
                                        InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                        TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                        TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                        DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                        OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                        TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                        Rebate = Convert.ToDecimal(idr["Rebate"]),
                                        CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                        CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                        Remark = Convert.ToString(idr["Remark"]).Trim(),
                                        TransKind = Convert.ToString(idr["TransKind"]).Trim(),
                                        CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                        CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                        TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                        TimeLimit = Convert.ToInt32(idr["TimeLimit"]),
                                        LatestTimeLimit = Convert.ToInt32(idr["TimeLimit"]).Equals(9) ? "" : Convert.ToDateTime(idr["LatestTimeLimit"]).ToString("yyyy-MM-dd"),
                                        CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim(),
                                        ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                        Signer = Convert.ToString(idr["Signer"]).Trim(),
                                        SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                        SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                        ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                        UploadReturnPic = Convert.ToString(idr["UploadReturnPic"]).Trim(),
                                        ContractFee = Convert.ToDecimal(idr["ContractFee"]),
                                        //AwbGoods = goodList,
                                        Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                                    });
                                    #endregion
                                }
                            }
                        }
                    }
                }
                resHT["rows"] = result;
                #region 查询总数

                string strCount = @"SELECT Count(distinct a.AwbID) as TotalCount FROM Tbl_Awb_Basic AS a LEFT JOIN Tbl_DepManifest AS b ON a.ContractNum=b.ContractNum LEFT JOIN Tbl_Awb_Goods AS c ON a.AwbNo=c.AwbNo LEFT JOIN Tbl_DeliveryAwb as d on a.AwbID=d.AwbID and a.AwbNo=d.AwbNo  WHERE  a.DelFlag<>1 ";
                strCount += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //查询所有已审核过的出发和在途的，到达的，关注的b.ContractNum IS NOT NULL and b.DelFlag=1
                //strCount += " and (b.TruckFlag = '1' or b.TruckFlag = '2' or b.TruckFlag='3' or b.TruckFlag='5')";
                //运输性质自发还是外协
                if (!string.IsNullOrEmpty(entity.TransKind)) { strCount += " and a.TransKind = '" + entity.TransKind + "'"; }
                //查询所有已审核过的出发和在途的，到达的，关注的
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strCount += " and a.AwbStatus = '" + entity.DelFlag + "'"; }
                //结算状态
                if (!string.IsNullOrEmpty(entity.CheckStatus)) { strCount += " and a.CheckStatus = '" + entity.CheckStatus + "'"; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dep in ('" + res + "')";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest)) { strCount += " and a.Dest='" + entity.Dest + "'"; }
                //中间站
                if (!string.IsNullOrEmpty(entity.MidDest)) { strCount += " and a.MidDest='" + entity.MidDest + "'"; }
                //中转站
                if (!string.IsNullOrEmpty(entity.Transit)) { strCount += " and a.Transit='" + entity.Transit + "'"; }
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    string[] ccs = entity.AwbNo.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strCount += " and a.AwbNo in ('" + res + "')";
                }
                //按副单号查询
                if (!string.IsNullOrEmpty(entity.HAwbNo)) { strCount += " and a.HAwbNo like '%" + entity.HAwbNo + "%'"; }
                //按客户名称查询
                if (!string.IsNullOrEmpty(entity.ShipperUnit)) { strCount += " and (a.ShipperUnit like '%" + entity.ShipperUnit + "%' or a.ShipperName like '%" + entity.ShipperUnit + "%')"; }
                //按收货人查询
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strCount += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')"; }
                //件数
                if (!entity.Piece.Equals(0)) { strCount += " and a.Piece=" + entity.Piece + ""; }
                //送货方式
                if (!string.IsNullOrEmpty(entity.DeliveryType)) { strCount += " and a.DeliveryType = '" + entity.DeliveryType + "'"; }
                //货物品名
                if (!string.IsNullOrEmpty(entity.Goods)) { strCount += " and c.Goods like '%" + entity.Goods + "%'"; }
                //预计到达时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.CStartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.CStartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.CreateDate>='" + entity.CStartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.CEndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.CEndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strCount += " and a.CreateDate<'" + entity.CEndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = NewaySql.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 保存运单备注信息
        /// </summary>
        /// <param name="goods"></param>
        public void AddAwbRemarkInfo(AwbRemarkEntity remark)
        {
            
            try
            {
                remark.EnSafe();
                string strSQL = @"INSERT INTO Tbl_Awb_Remark( AwbNo ,OP_NAME,OP_ID ,BelongSystem,Remark ) VALUES  (@AwbNo ,@OP_NAME,@OP_ID ,@BelongSystem,@Remark)";
                using (DbCommand cmd = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(cmd, "@AwbNo", DbType.String, remark.AwbNo.ToUpper().Trim());
                    NewaySql.AddInParameter(cmd, "@OP_NAME", DbType.String, remark.OP_NAME);
                    NewaySql.AddInParameter(cmd, "@OP_ID", DbType.String, remark.OP_ID);
                    NewaySql.AddInParameter(cmd, "@BelongSystem", DbType.String, remark.BelongSystem);
                    NewaySql.AddInParameter(cmd, "@Remark", DbType.String, remark.Remark);
                    NewaySql.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 根据运单号查询运单的备注内容 并按备注的时间倒序排列
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AwbRemarkEntity> QueryAwbNoRemarkInfo(AwbRemarkEntity entity)
        {
            List<AwbRemarkEntity> result = new List<AwbRemarkEntity>();
            
            try
            {
                entity.EnSafe();
                string strSQL = @" select * from tbl_awb_remark where awbno=@awbno and BelongSystem=@BelongSystem";
                strSQL += " order by op_date desc";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    NewaySql.AddInParameter(command, "@awbno", DbType.String, entity.AwbNo);
                    NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, entity.BelongSystem);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            result.Add(new AwbRemarkEntity
                            {
                                id = Convert.ToInt32(idr["id"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                OP_ID = Convert.ToString(idr["OP_ID"]).Trim(),
                                OP_NAME = Convert.ToString(idr["OP_NAME"]).Trim(),
                                BelongSystem = Convert.ToString(idr["BelongSystem"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                            #endregion
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询运单跟踪状态信息
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public List<AwbRoadEntity> QueryAwbRoad(AwbRoadEntity ent)
        {
            List<AwbRoadEntity> result = new List<AwbRoadEntity>();
            
            try
            {
                string strSQL = @"select a.AwbID,a.AwbNo,a.Dep,a.Transit,a.Dest,a.CreateAwb,a.CreateDate,a.Signer,a.SignTime,a.ReturnStatus,a.ReturnDate,a.SendReturnAwbStatus,a.SendReturnAwbDate,a.ConfirmReturnAwbStatus,a.ConfirmReturnAwbDate,a.ShipperUnit,a.AcceptPeople,a.AcceptUnit,a.ShipperName,a.Piece,a.AwbPiece,a.Weight,a.Volume,a.ReturnInfo,b.ContractNum,b.CreateTime,b.StartTime,b.TruckNum,b.Driver,b.DriverCellPhone,b.DestPeople,b.DestCellphone,b.Loader,b.Manifester,b.ActArriveTime from tbl_awb_basic as a left join Tbl_DepManifest as b on a.ContractNum=b.ContractNum where a.AwbNo=@AwbNo and a.BelongSystem=@BelongSystem";
                if (ent.AwbID != 0)
                {
                    strSQL += " and a.AwbID=@AwbID ";
                }
                //strSQL += " order by a.OP_DATE desc";
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    if (ent.AwbID != 0)
                    {
                        NewaySql.AddInParameter(command, "@AwbID", DbType.Int64, ent.AwbID);
                    }
                    NewaySql.AddInParameter(command, "@BelongSystem", DbType.String, ent.BelongSystem);
                    NewaySql.AddInParameter(command, "@AwbNo", DbType.String, ent.AwbNo);
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new AwbRoadEntity
                            {
                                AwbID = Convert.ToInt64(idr["AwbID"]),
                                AwbNo = Convert.ToString(idr["AwbNo"]).Trim(),
                                Dep = Convert.ToString(idr["Dep"]).Trim(),
                                Dest = Convert.ToString(idr["Dest"]).Trim(),
                                Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                CreateDate = string.IsNullOrEmpty(Convert.ToString(idr["CreateDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CreateDate"]),
                                Signer = Convert.ToString(idr["Signer"]).Trim(),
                                SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                ReturnDate = string.IsNullOrEmpty(Convert.ToString(idr["ReturnDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ReturnDate"]),
                                SendReturnAwbStatus = Convert.ToString(idr["SendReturnAwbStatus"]).Trim(),
                                SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                ConfirmReturnAwbStatus = Convert.ToString(idr["ConfirmReturnAwbStatus"]).Trim(),
                                ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                CreateTime = string.IsNullOrEmpty(Convert.ToString(idr["CreateTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CreateTime"]),
                                StartTime = string.IsNullOrEmpty(Convert.ToString(idr["StartTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["StartTime"]),
                                TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                Driver = Convert.ToString(idr["Driver"]).Trim(),
                                DestCellphone = Convert.ToString(idr["DestCellphone"]).Trim(),
                                DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                DestPeople = Convert.ToString(idr["DestPeople"]).Trim(),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                Loader = Convert.ToString(idr["Loader"]).Trim(),
                                Manifester = Convert.ToString(idr["Manifester"]).Trim(),
                                ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"])
                            });
                        }
                    }

                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 根据运单查询货物跟踪信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<AwbStatusTrackEntity> QueryAwbStatusTrack(AwbEntity entity)
        {
            List<AwbStatusTrackEntity> result = new List<AwbStatusTrackEntity>();
            

            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询条件
                string strSQL = "SELECT distinct a.AwbID,a.AwbNo,a.HAwbNo,a.Dep,a.Dest,a.Piece,a.AwbPiece,a.AwbWeight,a.AwbVolume,a.HandleTime,a.ShipperUnit,a.ShipperName,a.ShipperTelephone,a.ShipperCellphone,a.AcceptUnit,a.AcceptAddress,a.AcceptPeople,a.AcceptTelephone,a.AcceptCellphone,a.DeliveryType,ISNULL(b.ContractNum,'') as ContractNum,ISNULL(b.TruckNum,'') as TruckNum,ISNULL(b.Driver,'') as Driver,ISNULL(b.DriverCellPhone,'') as DriverCellPhone,ISNULL(a.AwbStatus,'') as TruckFlag,ISNULL(b.StartTime,'') as StartTime,ISNULL(b.PreArriveTime,'') as PreArriveTime,b.ActArriveTime,a.ReturnInfo,a.ReturnStatus,a.TransitFee,a.InsuranceFee,a.TransportFee,a.DeliverFee,a.OtherFee,a.TotalCharge,a.Rebate,a.CheckOutType,a.CollectMoney,a.Remark,a.CreateDate,a.CreateAwb,a.TransKind,a.TrafficType,a.CheckStatus,a.Transit,a.MidDest,a.Signer,ISNULL(b.TransportFee,0) as ContractFee,a.ReturnAwb,a.SignTime,a.SendReturnAwbDate,a.ConfirmReturnAwbDate,d.Mode,a.ClientNum,a.UploadReturnPic FROM Tbl_Awb_Basic AS a LEFT JOIN Tbl_DepManifest AS b ON a.ContractNum=b.ContractNum LEFT JOIN Tbl_Awb_Goods AS c ON a.AwbNo=c.AwbNo LEFT JOIN Tbl_DeliveryAwb as d on a.AwbID=d.AwbID and a.AwbNo=d.AwbNo  WHERE  a.DelFlag<>1 ";
                strSQL += " and a.BelongSystem='" + entity.BelongSystem + "'";
                //查询所有已审核过的出发和在途的，到达的，关注的b.ContractNum IS NOT NULL and b.DelFlag=1
                //运输性质自发还是外协
                if (!string.IsNullOrEmpty(entity.TransKind)) { strSQL += " and a.TransKind = '" + entity.TransKind + "'"; }
                //查询所有已审核过的出发和在途的，到达的，关注的
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strSQL += " and a.AwbStatus = '" + entity.DelFlag + "'"; }
                //结算状态
                if (!string.IsNullOrEmpty(entity.CheckStatus)) { strSQL += " and a.CheckStatus = '" + entity.CheckStatus + "'"; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep))
                {
                    string[] ccs = entity.Dep.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++) { res += ccs[i] + "','"; }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dep in ('" + res + "')";
                }
                //到达站
                if (!string.IsNullOrEmpty(entity.Dest))
                {
                    string[] ccs = entity.Dest.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++) { res += ccs[i] + "','"; }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.Dest in ('" + res + "')";
                }
                //中间站
                if (!string.IsNullOrEmpty(entity.MidDest)) { strSQL += " and a.MidDest='" + entity.MidDest + "'"; }
                //中转站
                if (!string.IsNullOrEmpty(entity.Transit)) { strSQL += " and a.Transit='" + entity.Transit + "'"; }
                //按运单号查询
                if (!string.IsNullOrEmpty(entity.AwbNo))
                {
                    string[] ccs = entity.AwbNo.Split(',');
                    string res = string.Empty;
                    for (int i = 0; i < ccs.Length; i++)
                    {
                        res += ccs[i] + "','";
                    }
                    res = res.Substring(0, res.Length - 3);
                    strSQL += " and a.AwbNo in ('" + res + "')";
                }
                //按副单号查询
                if (!string.IsNullOrEmpty(entity.HAwbNo)) { strSQL += " and a.HAwbNo like '%" + entity.HAwbNo + "%'"; }
                //按客户名称查询
                if (!string.IsNullOrEmpty(entity.ShipperUnit)) { strSQL += " and (a.ShipperUnit like '%" + entity.ShipperUnit + "%' or a.ShipperName like '%" + entity.ShipperUnit + "%')"; }
                //按收货人查询
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')"; }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and a.Piece=" + entity.Piece + ""; }
                //送货方式
                if (!string.IsNullOrEmpty(entity.DeliveryType)) { strSQL += " and a.DeliveryType = '" + entity.DeliveryType + "'"; }
                //货物品名
                if (!string.IsNullOrEmpty(entity.Goods)) { strSQL += " and c.Goods like '%" + entity.Goods + "%'"; }
                //预计到达时间范围
                if (!entity.StartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.StartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.HandleTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.EndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.EndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.HandleTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.CEndDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.CEndDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.CEndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.CStartDate.ToString("yyyy-MM-dd").Equals("0001-01-01") && !entity.CStartDate.ToString("yyyy-MM-dd").Equals("1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.CStartDate.ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " Order by a.HandleTime desc";
                #endregion
                using (DbCommand command = NewaySql.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = NewaySql.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            if (result.Where(c => c.AwbID.Equals(Convert.ToInt64(idr["AwbID"]))).Count() > 0) { continue; }
                            string good = string.Empty;
                            #region 获取运单货物品名
                            List<AwbGoodsEntity> goodList = new List<AwbGoodsEntity>();
                            AwbGoodsEntity goods = new AwbGoodsEntity { AwbNo = Convert.ToString(idr["AwbNo"]).Trim(), BelongSystem = entity.BelongSystem };

                            DataTable dtt = QueryAwbGoodsInfo(goods);
                            foreach (DataRow idrGoods in dtt.Rows)
                            {
                                goodList.Add(new AwbGoodsEntity
                                {
                                    GoodsID = Convert.ToInt32(idrGoods["GoodsID"]),
                                    AwbNo = Convert.ToString(idrGoods["AwbNo"]),
                                    Package = Convert.ToString(idrGoods["Package"]),
                                    Goods = Convert.ToString(idrGoods["Goods"]),
                                    Weight = Convert.ToDecimal(idrGoods["Weight"]),
                                    WeightPrice = Convert.ToDecimal(idrGoods["WeightPrice"]),
                                    Volume = Convert.ToDecimal(idrGoods["Volume"]),
                                    VolumePrice = Convert.ToDecimal(idrGoods["VolumePrice"]),
                                    Piece = Convert.ToInt32(idrGoods["Piece"]),
                                    PiecePrice = Convert.ToDecimal(idrGoods["PiecePrice"]),
                                    DeclareValue = Convert.ToString(idrGoods["DeclareValue"]),
                                    OP_ID = Convert.ToString(idrGoods["OP_ID"]).Trim(),
                                    OP_DATE = Convert.ToDateTime(idrGoods["OP_DATE"])
                                });

                                good += Convert.ToString(idrGoods["Goods"]).Trim() + ",";
                            }
                            #endregion
                            List<DeliveryEntity> delist = QueryDeliveryByAwb(Convert.ToInt64(idr["AwbID"]), Convert.ToString(idr["AwbNo"]), entity.BelongSystem);
                            foreach (var i in delist)
                            {
                                #region 获取运单数据
                                result.Add(new AwbStatusTrackEntity
                                {
                                    AwbID = Convert.ToInt64(idr["AwbID"]),
                                    AwbNo = Convert.ToString(idr["AwbNo"]),
                                    Dep = Convert.ToString(idr["Dep"]),
                                    Dest = Convert.ToString(idr["Dest"]),
                                    Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                    MidDest = Convert.ToString(idr["MidDest"]),
                                    Piece = Convert.ToInt32(idr["Piece"]),
                                    AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                    AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                    AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                    HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                    ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                    ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                    ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                    ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                    ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                    AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                    AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                    AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                    AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                    AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                    AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                    ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                    DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                    TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                    Driver = Convert.ToString(idr["Driver"]).Trim(),
                                    DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                    TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                    StartTime = Convert.ToDateTime(idr["StartTime"]),
                                    PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                    ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"]),
                                    ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                    ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                    //UserName = Convert.ToString(idr["UserName"]).Trim(),
                                    InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                    TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                    TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                    DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                    OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                    TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                    Rebate = Convert.ToDecimal(idr["Rebate"]),
                                    CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                    CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                    Remark = Convert.ToString(idr["Remark"]).Trim(),
                                    TransKind = Convert.ToString(idr["TransKind"]).Trim(),
                                    CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                    CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                    TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                    CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim(),
                                    ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                    Signer = Convert.ToString(idr["Signer"]).Trim(),
                                    SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                    SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                    ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                    ContractFee = Convert.ToDecimal(idr["ContractFee"]),
                                    HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                    ClientNum = Convert.ToString(idr["ClientNum"]).Trim(),
                                    UploadReturnPic = Convert.ToString(idr["UploadReturnPic"]).Trim(),
                                    DTime = i.CreateTime,
                                    DDrive = i.Driver,
                                    DTruckNum = i.TruckNum,
                                    DPhone = i.DriverCellPhone,
                                    AwbGoods = goodList,
                                    Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                                });
                                #endregion
                            }
                            if (delist.Count <= 0)
                            {
                                List<TransitEntity> telist = QueryTransitByAwb(Convert.ToInt64(idr["AwbID"]), Convert.ToString(idr["AwbNo"]), entity.BelongSystem);
                                if (telist.Count > 0)
                                {
                                    foreach (var i in telist)
                                    {
                                        #region 获取运单数据
                                        result.Add(new AwbStatusTrackEntity
                                        {
                                            AwbID = Convert.ToInt64(idr["AwbID"]),
                                            AwbNo = Convert.ToString(idr["AwbNo"]),
                                            Dep = Convert.ToString(idr["Dep"]),
                                            Dest = Convert.ToString(idr["Dest"]),
                                            Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                            MidDest = Convert.ToString(idr["MidDest"]),
                                            Piece = Convert.ToInt32(idr["Piece"]),
                                            AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                            AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                            AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                            HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                            ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                            ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                            ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                            ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                            ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                            AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                            ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                            DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                            TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                            Driver = Convert.ToString(idr["Driver"]).Trim(),
                                            DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                            TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                            StartTime = Convert.ToDateTime(idr["StartTime"]),
                                            PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                            ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"]),
                                            ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                            ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                            //UserName = Convert.ToString(idr["UserName"]).Trim(),
                                            InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                            DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                            OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                            Rebate = Convert.ToDecimal(idr["Rebate"]),
                                            CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                            CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                            Remark = Convert.ToString(idr["Remark"]).Trim(),
                                            TransKind = Convert.ToString(idr["TransKind"]).Trim(),
                                            CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                            TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                            CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim(),
                                            ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                            Signer = Convert.ToString(idr["Signer"]).Trim(),
                                            SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                            SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                            ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                            ContractFee = Convert.ToDecimal(idr["ContractFee"]),
                                            HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                            ClientNum = Convert.ToString(idr["ClientNum"]).Trim(),
                                            UploadReturnPic = Convert.ToString(idr["UploadReturnPic"]).Trim(),
                                            TTime = i.OP_DATE,
                                            TAssistNum = i.AssistNum,
                                            TPhone = i.Telephone,
                                            TShortName = i.CarrierShortName,
                                            AwbGoods = goodList,
                                            Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                                        });
                                        #endregion
                                    }
                                }
                                else
                                {
                                    #region 获取运单数据
                                    result.Add(new AwbStatusTrackEntity
                                    {
                                        AwbID = Convert.ToInt64(idr["AwbID"]),
                                        AwbNo = Convert.ToString(idr["AwbNo"]),
                                        Dep = Convert.ToString(idr["Dep"]),
                                        Dest = Convert.ToString(idr["Dest"]),
                                        Transit = string.IsNullOrEmpty(Convert.ToString(idr["Transit"])) ? Convert.ToString(idr["Dest"]) : Convert.ToString(idr["Transit"]),
                                        MidDest = Convert.ToString(idr["MidDest"]),
                                        Piece = Convert.ToInt32(idr["Piece"]),
                                        AwbPiece = Convert.ToInt32(idr["AwbPiece"]),
                                        AwbWeight = Convert.ToDecimal(idr["AwbWeight"]),
                                        AwbVolume = Convert.ToDecimal(idr["AwbVolume"]),
                                        HandleTime = Convert.ToDateTime(idr["HandleTime"]),
                                        ShipperName = Convert.ToString(idr["ShipperName"]).Trim(),
                                        ShipperUnit = Convert.ToString(idr["ShipperUnit"]).Trim(),
                                        ShipperTelephone = Convert.ToString(idr["ShipperTelephone"]).Trim(),
                                        ShipperCellphone = Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                        ShipperPhone = string.IsNullOrEmpty(Convert.ToString(idr["ShipperTelephone"]).Trim()) ? Convert.ToString(idr["ShipperCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["ShipperCellphone"]).Trim()) ? Convert.ToString(idr["ShipperTelephone"]).Trim() : Convert.ToString(idr["ShipperTelephone"]).Trim() + "/" + Convert.ToString(idr["ShipperCellphone"]).Trim(),
                                        AcceptUnit = Convert.ToString(idr["AcceptUnit"]).Trim(),
                                        AcceptAddress = Convert.ToString(idr["AcceptAddress"]).Trim(),
                                        AcceptPeople = Convert.ToString(idr["AcceptPeople"]).Trim(),
                                        AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]).Trim(),
                                        AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                        AcceptPhone = string.IsNullOrEmpty(Convert.ToString(idr["AcceptTelephone"]).Trim()) ? Convert.ToString(idr["AcceptCellphone"]).Trim() : string.IsNullOrEmpty(Convert.ToString(idr["AcceptCellphone"]).Trim()) ? Convert.ToString(idr["AcceptTelephone"]).Trim() : Convert.ToString(idr["AcceptTelephone"]).Trim() + "/" + Convert.ToString(idr["AcceptCellphone"]).Trim(),
                                        ContractNum = Convert.ToString(idr["ContractNum"]).Trim(),
                                        DeliveryType = Convert.ToString(idr["DeliveryType"]).Trim(),
                                        TruckNum = Convert.ToString(idr["TruckNum"]).Trim(),
                                        Driver = Convert.ToString(idr["Driver"]).Trim(),
                                        DriverCellPhone = Convert.ToString(idr["DriverCellPhone"]).Trim(),
                                        TruckFlag = Convert.ToString(idr["TruckFlag"]).Trim(),
                                        StartTime = Convert.ToDateTime(idr["StartTime"]),
                                        PreArriveTime = Convert.ToDateTime(idr["PreArriveTime"]),
                                        ActArriveTime = string.IsNullOrEmpty(Convert.ToString(idr["ActArriveTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ActArriveTime"]),
                                        ReturnInfo = Convert.ToString(idr["ReturnInfo"]).Trim(),
                                        ReturnStatus = Convert.ToString(idr["ReturnStatus"]).Trim(),
                                        //UserName = Convert.ToString(idr["UserName"]).Trim(),
                                        InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                        TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                        TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                        DeliverFee = Convert.ToDecimal(idr["DeliverFee"]),
                                        OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                        TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                        Rebate = Convert.ToDecimal(idr["Rebate"]),
                                        CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                        CheckOutType = Convert.ToString(idr["CheckOutType"]).Trim(),
                                        Remark = Convert.ToString(idr["Remark"]).Trim(),
                                        TransKind = Convert.ToString(idr["TransKind"]).Trim(),
                                        CreateAwb = Convert.ToString(idr["CreateAwb"]).Trim(),
                                        CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                        TrafficType = Convert.ToString(idr["TrafficType"]).Trim(),
                                        CheckStatus = Convert.ToString(idr["CheckStatus"]).Trim(),
                                        ReturnAwb = Convert.ToInt32(idr["ReturnAwb"]),
                                        Signer = Convert.ToString(idr["Signer"]).Trim(),
                                        SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                        SendReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["SendReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendReturnAwbDate"]),
                                        ConfirmReturnAwbDate = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmReturnAwbDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ConfirmReturnAwbDate"]),
                                        ContractFee = Convert.ToDecimal(idr["ContractFee"]),
                                        HAwbNo = Convert.ToString(idr["HAwbNo"]).Trim(),
                                        ClientNum = Convert.ToString(idr["ClientNum"]).Trim(),
                                        UploadReturnPic = Convert.ToString(idr["UploadReturnPic"]).Trim(),
                                        AwbGoods = goodList,
                                        Goods = string.IsNullOrEmpty(good) ? "" : good.Substring(0, good.Length - 1)
                                    });
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }

        #endregion
    }
}
