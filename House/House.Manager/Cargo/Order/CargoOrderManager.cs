using House.DataAccess;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using House.Entity.Cargo.Product;
using House.Entity.Dto;
using House.Entity.Dto.Base;
using House.Entity.Dto.Order;
using House.Entity.Dto.Order.CargoRpl;
using House.Entity.House;
using House.Manager.House;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace House.Manager.Cargo
{
    /// <summary>
    /// 订单数据操作类
    /// </summary>
    public class CargoOrderManager
    {
        private SqlHelper conn = new SqlHelper();
        #region 订单明细操作方法合计
        /// <summary>
        /// 分页查询订单明细
        /// </summary>
        public Hashtable QueryPageOrderDetailsData(int pIndex, int pNum, CargoOrderGoodsEntity entity)
        {
            List<CargoOrderGoodsEntity> result = new List<CargoOrderGoodsEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                #region 获取订单明细
                string strSQL = @" SELECT TOP " + pNum + "* FROM (SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY a.OP_DATE DESC) AS RowNumber, h.Name AS HouseName ,p.ProductCode,p.Born,p.Specs,p.Figure,p.LoadIndex,p.SpeedLevel,p.Batch,p.InHousePrice,p.CostPrice,p.SalePrice,p.GoodsCode,t.TypeName,t.TypeID,o.PayClientNum,o.AcceptUnit,o.ThrowGood,o.OrderModel,p.InHouseTime,a.* FROM Tbl_Cargo_OrderGoods AS a LEFT JOIN  Tbl_Cargo_Order AS o ON a.OrderNo = o.OrderNo LEFT JOIN  Tbl_Cargo_House AS h ON a.HouseID = h.HouseID  LEFT JOIN  Tbl_Cargo_Product AS p  ON  a.ProductID = p.ProductID  LEFT JOIN  Tbl_Cargo_ProductType AS t ON t.TypeID= p.TypeID WHERE (1=1)";

                if (!entity.PayClientNum.Equals(0)) { strSQL += " and o.PayClientNum =" + entity.PayClientNum; }
                if (!entity.SuppClientNum.Equals(0))
                {
                    strSQL += " and o.SuppClientNum=" + entity.SuppClientNum;
                }
                //查询订单类型【即日达订单22、次日达订单23、渠道订单24】
                if (!string.IsNullOrEmpty(entity.ThrowGood))
                {
                    strSQL += " and o.ThrowGood in (" + entity.ThrowGood + ")";
                }
                if (!string.IsNullOrEmpty(entity.OrderModel))
                {
                    strSQL += "and o.OrderModel=" + entity.OrderModel;
                }

                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                if (!entity.TypeID.Equals(0)) { strSQL += " and t.TypeID =" + entity.TypeID; }//品牌ID
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and o.AcceptUnit like '%" + entity.AcceptUnit + "%'"; }
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and o.OrderNo like '%" + entity.OrderNo + "%'"; }

                if (!string.IsNullOrEmpty(entity.Figure)) { strSQL += " and p.Figure like '%" + entity.Figure + "%'"; }
                if (!string.IsNullOrEmpty(entity.Batch)) { strSQL += " and p.Batch like '%" + entity.Batch + "%'"; }
                if (!string.IsNullOrEmpty(entity.Specs)) { strSQL += " and p.Specs like '%" + entity.Specs + "%'"; }

                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += ") as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))  order by RowNumber";
                #endregion

                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    #region 获取数据
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                ProductCode = Convert.ToString(idr["ProductCode"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                Born = Convert.ToString(idr["Born"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                LoadSpeed = Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                OutCargoID = Convert.ToString(idr["OutCargoID"]),
                                RuleType = Convert.ToString(idr["RuleType"]),
                                SuitClientNum = Convert.ToString(idr["SuitClientNum"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                InHouseTime = Convert.ToDateTime(idr["InHouseTime"]),
                                InHouseDay = Convert.ToDateTime(idr["OP_DATE"]).Subtract(Convert.ToDateTime(idr["InHouseTime"])).Days,
                                OrderModel = Convert.ToString(idr["OrderModel"]),
                                InHousePrice = Convert.ToDecimal(idr["InHousePrice"]),
                                CostPrice = Convert.ToDecimal(idr["SupplySalePrice"]),
                                TotalCostPrice = Convert.ToInt32(idr["Piece"]) * Convert.ToDecimal(idr["SupplySalePrice"]),
                                SalePrice = Convert.ToInt32(idr["Piece"]) * Convert.ToDecimal(idr["ActSalePrice"]),
                                OverDayNum = string.IsNullOrEmpty(Convert.ToString(idr["OverDayNum"])) ? 0 : Convert.ToInt32(idr["OverDayNum"]),
                                OverDueFee = string.IsNullOrEmpty(Convert.ToString(idr["OverDueFee"])) ? 0 : Convert.ToDecimal(idr["OverDueFee"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            });
                        }
                    }
                    #endregion
                }
                resHT["rows"] = result;
                #region 获取总数
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_OrderGoods as a LEFT JOIN Tbl_Cargo_Order AS o ON a.OrderNo = o.OrderNo LEFT JOIN  Tbl_Cargo_Product AS p  ON  a.ProductID = p.ProductID  LEFT JOIN  Tbl_Cargo_ProductType AS t ON t.TypeID= p.TypeID  Where (1=1)";

                if (!entity.PayClientNum.Equals(0)) { strCount += " and o.PayClientNum =" + entity.PayClientNum; }
                if (!entity.SuppClientNum.Equals(0))
                {
                    strCount += " and o.SuppClientNum=" + entity.SuppClientNum;
                }
                //查询订单类型【即日达订单22、次日达订单23、渠道订单24】
                if (!string.IsNullOrEmpty(entity.ThrowGood))
                {
                    strCount += " and o.ThrowGood in (" + entity.ThrowGood + ")";
                }
                if (!string.IsNullOrEmpty(entity.OrderModel))
                {
                    strCount += "and o.OrderModel=" + entity.OrderModel;
                }
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strCount += " and o.AcceptUnit like '%" + entity.AcceptUnit + "%'"; }
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strCount += " and o.OrderNo like '%" + entity.OrderNo + "%'"; }
                if (!string.IsNullOrEmpty(entity.Figure)) { strCount += " and p.Figure like '%" + entity.Figure + "%'"; }
                if (!string.IsNullOrEmpty(entity.Batch)) { strCount += " and p.Batch like '%" + entity.Batch + "%'"; }
                if (!string.IsNullOrEmpty(entity.Specs)) { strCount += " and p.Specs like '%" + entity.Specs + "%'"; }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                if (!entity.TypeID.Equals(0)) { strCount += " and t.TypeID =" + entity.TypeID; } //品牌ID
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }
        #endregion
        #region 销售订单操作方法集合
        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryPageOrderSaleInfo(int pIndex, int pNum, CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,b.LogisticName,c.WXPayOrderNo,c.PayWay ,h.Name AS HouseName from Tbl_Cargo_Order as a left join tbl_cargo_Logistic as b on a.LogisID=b.ID left join Tbl_WX_Order as c on a.WXOrderNo=c.OrderNo left JOIN Tbl_Cargo_House AS h ON a.HouseID = h.HouseID  Where (1=1) ";

                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                    }
                    else
                    {
                        strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                    }
                }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strSQL += " and a.PayClientName = '" + entity.PayClientName + "'"; }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.ThrowGood))
                {
                    strSQL += " and a.ThrowGood in (" + entity.ThrowGood + ")";
                }
                ////客户名称
                //if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and a.PayClientName like '%" + entity.AcceptUnit + "%'"; }
                //送货方式
                if (!string.IsNullOrEmpty(entity.DeliveryType)) { strSQL += " and a.DeliveryType = '" + entity.DeliveryType + "'"; }
                if (!entity.SuppClientNum.Equals(0)) { strSQL += " and a.SuppClientNum=" + entity.SuppClientNum; }

                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) order by A.RowNumber";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["TotalCharge"]) - Convert.ToDecimal(idr["TransitFee"]) - Convert.ToDecimal(idr["OtherFee"]) + Convert.ToDecimal(idr["InsuranceFee"]),
                                //Rebate = Convert.ToDecimal(idr["Rebate"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                LineID = string.IsNullOrEmpty(Convert.ToString(idr["LineID"])) ? 0 : Convert.ToInt32(idr["LineID"]),
                                LineName = Convert.ToString(idr["LineName"]),
                                LogisticName = Convert.ToString(idr["LogisticName"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                SaleCellPhone = Convert.ToString(idr["SaleCellPhone"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                OrderModel = Convert.ToString(idr["OrderModel"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                CheckDate = string.IsNullOrEmpty(Convert.ToString(idr["CheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckDate"]),
                                FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]),
                                FinanceSecondCheckName = Convert.ToString(idr["FinanceSecondCheckName"]),
                                FinanceSecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FinanceSecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FinanceSecondCheckDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                WXOrderNo = Convert.ToString(idr["OrderType"]).Equals("0") || Convert.ToString(idr["OrderType"]).Equals("1") ? "" : Convert.ToString(idr["WXOrderNo"]),
                                PayWay = Convert.ToString(idr["PayWay"]),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                TranHouse = Convert.ToString(idr["TranHouse"]),
                                Signer = Convert.ToString(idr["Signer"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                ModifyPriceStatus = Convert.ToString(idr["ModifyPriceStatus"]),
                                PayClientName = Convert.ToString(idr["PayClientName"]),
                                PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                                IsPrintPrice = Convert.ToInt32(idr["IsPrintPrice"]),
                                BelongHouse = Convert.ToString(idr["BelongHouse"]),
                                PostponeShip = string.IsNullOrEmpty(Convert.ToString(idr["PostponeShip"])) ? "0" : Convert.ToString(idr["PostponeShip"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                OutCargoTime = string.IsNullOrEmpty(Convert.ToString(idr["OutCargoTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["OutCargoTime"]),
                                DeliverySettlement = Convert.ToString(idr["DeliverySettlement"]),
                                OrderAging = Convert.ToString(idr["OrderAging"]),
                                PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 1 : Convert.ToInt32(idr["PrintNum"]),
                                SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                PickStatus = string.IsNullOrEmpty(Convert.ToString(idr["PickStatus"])) ? 0 : Convert.ToInt32(idr["PickStatus"]),
                                ShopCode = Convert.ToString(idr["ShopCode"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_Order  Where (1=1)";
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strCount += " and (AwbStatus='0' or AwbStatus='1')";
                    }
                    else
                    {
                        strCount += " and AwbStatus='" + entity.AwbStatus + "'";
                    }
                }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strCount += " and PayClientName = '" + entity.PayClientName + "'"; }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strCount += " and OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and HouseID in (" + entity.CargoPermisID + ")"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.ThrowGood))
                {
                    strCount += " and ThrowGood in (" + entity.ThrowGood + ")";
                }
                //送货方式
                if (!string.IsNullOrEmpty(entity.DeliveryType)) { strCount += " and DeliveryType = '" + entity.DeliveryType + "'"; }
                if (!entity.SuppClientNum.Equals(0)) { strCount += " and SuppClientNum=" + entity.SuppClientNum; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        #endregion
        #region 进货/仓单管理操作方法集合
        /// <summary>
        /// 分页查询所有进货单
        /// </summary>
        public Hashtable QueryAllPagePurcgaseOrders(int pIndex, int pNum, CargoPurchaseOrderEntity entity)
        {
            List<CargoPurchaseOrderEntity> result = new List<CargoPurchaseOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(select distinct ROW_NUMBER()   over (order by a.OP_DATE desc ) as RowNumber,a.*,h.Name AS HouseName   from  Tbl_Cargo_PurchaseOrder as a INNER JOIN Tbl_Cargo_House  AS h ON  a.HouseID = h.HouseID WHERE (1=1)";

                if (!entity.ClientNum.Equals(0)) { strSQL += " and a.ClientNum = " + entity.ClientNum; } //客户编码
                if (!entity.TrafficType.Equals(0))//订单类型，默认查询“进仓单”详见接口赋值4
                {
                    strSQL += "and a.TrafficType='" + entity.TrafficType + "'";
                }
                if (!string.IsNullOrEmpty(entity.FacOrderNo)) { strSQL += "and a.FacOrderNo like '%" + entity.FacOrderNo + "%'"; }//进货单号
                if (!entity.OrderID.Equals(0)) { strSQL += " and a.OrderID = " + entity.OrderID; } //订单编号 
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and a.AcceptUnit like '%" + entity.AcceptUnit + "%'"; } //客户名称
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb like '%" + entity.CreateAwb + "%'"; }//开单员
                if (!string.IsNullOrEmpty(entity.ReceivingStatus)) { strSQL += " and a.ReceivingStatus = '" + entity.ReceivingStatus + "'"; } //收货状态

                //开单时间范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }

                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) order by A.RowNumber";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取进仓单数据

                            result.Add(new CargoPurchaseOrderEntity
                            {
                                FacOrderNo = Convert.ToString(idr["FacOrderNo"]),
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TotalCharge = Convert.ToDouble(idr["TotalCharge"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                CheckStatus = Convert.ToInt32(idr["CheckStatus"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = string.IsNullOrEmpty(Convert.ToString(idr["LogisID"])) ? 0 : Convert.ToInt32(idr["LogisID"]),
                                ReceivingStatus = Convert.ToString(idr["ReceivingStatus"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_PurchaseOrder  Where (1=1)";
                if (!entity.ClientNum.Equals(0)) { strCount += " and ClientNum = " + entity.ClientNum; } //客户编码
                if (!entity.TrafficType.Equals(0))//订单类型，默认查询“进仓单”详见接口赋值4
                {
                    strCount += "and TrafficType='" + entity.TrafficType + "'";
                }
                if (!string.IsNullOrEmpty(entity.FacOrderNo)) { strCount += "and FacOrderNo like '%" + entity.FacOrderNo + "%'"; }//进货单号
                if (!entity.OrderID.Equals(0)) { strCount += " and OrderID = " + entity.OrderID; } //订单编号 
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strCount += " and AcceptUnit like '%" + entity.AcceptUnit + "%'"; } //客户名称
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strCount += " and CreateAwb like '%" + entity.CreateAwb + "%'"; }//开单员
                if (!string.IsNullOrEmpty(entity.ReceivingStatus)) { strCount += " and ReceivingStatus = '" + entity.ReceivingStatus + "'"; } //收货状态

                //开单时间范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and HouseID in (" + entity.CargoPermisID + ")"; }

                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }

        /// <summary>
        /// 查询该进货单下的明细商品
        /// </summary>
        public List<CargoPurchaseOrderGoodsEntity> QueryAllPurcgaseOrderGoods(CargoPurchaseOrderGoodsEntity entity)
        {
            List<CargoPurchaseOrderGoodsEntity> result = new List<CargoPurchaseOrderGoodsEntity>();
            try
            {
                var strSql = @"select o.*,t.TypeName,po.CreateDate,po.FacOrderNo from Tbl_Cargo_PurchaseOrderGoods as o inner join Tbl_Cargo_PurchaseOrder po on o.OrderID=po.OrderID INNER JOIN Tbl_Cargo_ProductType t ON o.TypeID=t.TypeID where (1=1)";
                if (!string.IsNullOrEmpty(entity.ClientNum))
                {
                    strSql += " and po.ClientNum=" + entity.ClientNum;
                }
                if (!string.IsNullOrEmpty(entity.TrafficType))
                {
                    strSql += " and po.TrafficType=" + entity.TrafficType;
                }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSql += " and po.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSql += " and po.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.OrderID.Equals(0))
                {
                    strSql += " and po.OrderID = " + entity.OrderID;
                }
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    string res = entity.Specs.ToUpper().Replace("/", "").Replace("R", "").Replace("C", "").Replace("F", "").Replace("Z", "");
                    if (res.Length <= 3)
                    {
                        if (!string.IsNullOrEmpty(res)) { strSql += " and o.Specs like '%" + res + "%'"; }
                    }
                    if (res.Length > 3 && res.Length <= 5)
                    {
                        strSql += " and (o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3) + "%' or o.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                    }
                    if (res.Length > 5)
                    {
                        strSql += " and (o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "RF" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZR" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZRF" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                    }
                }
                if (!entity.TypeID.Equals(0))
                {
                    strSql += " and o.TypeID='" + entity.TypeID + "'";
                }
                if (!string.IsNullOrEmpty(entity.Batch))
                {
                    strSql += " and o.Batch like '%" + entity.Batch + "%'";
                }
                if (!string.IsNullOrEmpty(entity.FacOrderNo))
                {
                    strSql += " and po.FacOrderNo ='" + entity.FacOrderNo + "'";
                }
                if (!entity.HouseID.Equals(0))
                {
                    strSql += " and po.HouseID=" + entity.HouseID;
                }
                strSql += " order by OP_DATE desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSql))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoPurchaseOrderGoodsEntity()
                            {
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                FacOrderNo = Convert.ToString(idr["FacOrderNo"]),
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                ReceivePiece = string.IsNullOrEmpty(Convert.ToString(idr["ReceivePiece"])) ? 0 : Convert.ToInt32(idr["ReceivePiece"]),
                                ReturnPiece = string.IsNullOrEmpty(Convert.ToString(idr["ReturnPiece"])) ? 0 : Convert.ToInt32(idr["ReturnPiece"]),
                                ProductCode = Convert.ToString(idr["ProductCode"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                Born = Convert.ToInt32(idr["Born"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                BatchYear = string.IsNullOrEmpty(Convert.ToString(idr["BatchYear"])) ? 0 : Convert.ToInt32(idr["BatchYear"]),
                                InHousePrice = Convert.ToDouble(idr["InHousePrice"]),
                                UnitPrice = Convert.ToDouble(idr["UnitPrice"]),
                                CostPrice = Convert.ToDouble(idr["CostPrice"]),
                                TradePrice = Convert.ToDouble(idr["TradePrice"]),
                                SalePrice = Convert.ToDouble(idr["SalePrice"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                Model = Convert.ToString(idr["Model"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                LoadSpeed = Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                            });
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

        public Hashtable QueryAllPurcgaseOrderGoods(int pIndex, int pNum, CargoPurchaseOrderGoodsEntity entity)
        {
            Hashtable resHT = new Hashtable();
            List<CargoPurchaseOrderGoodsEntity> result = new List<CargoPurchaseOrderGoodsEntity>();
            try
            {
                string strSql = @" SELECT TOP " + pNum + " *  FROM ";
                strSql += @"(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY po.CreateDate DESC) AS RowNumber,o.*,t.TypeName,po.CreateDate,po.FacOrderNo,ISNULL(d.InPiece,0) as InPiece from Tbl_Cargo_PurchaseOrderGoods as o inner join Tbl_Cargo_PurchaseOrder po on o.OrderID=po.OrderID INNER JOIN Tbl_Cargo_ProductType t ON o.TypeID=t.TypeID left join Tbl_Cargo_FactoryOrder as d on po.FacOrderNo=d.FacOrderNo and o.ProductCode=d.ProductCode where (1=1)";
                if (!string.IsNullOrEmpty(entity.ClientNum))
                {
                    strSql += " and po.ClientNum=" + entity.ClientNum;
                }
                if (!string.IsNullOrEmpty(entity.TrafficType))
                {
                    strSql += " and po.TrafficType=" + entity.TrafficType;
                }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSql += " and po.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSql += " and po.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.OrderID.Equals(0))
                {
                    strSql += " where po.OrderID = " + entity.OrderID;
                }
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    string res = entity.Specs.ToUpper().Replace("/", "").Replace("R", "").Replace("C", "").Replace("F", "").Replace("Z", "");
                    if (res.Length <= 3)
                    {
                        if (!string.IsNullOrEmpty(res)) { strSql += " and o.Specs like '%" + res + "%'"; }
                    }
                    if (res.Length > 3 && res.Length <= 5)
                    {
                        strSql += " and (o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3) + "%' or o.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                    }
                    if (res.Length > 5)
                    {
                        strSql += " and (o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "RF" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZR" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZRF" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                    }
                }
                if (!entity.TypeID.Equals(0))
                {
                    strSql += " and o.TypeID='" + entity.TypeID + "'";
                }
                if (!string.IsNullOrEmpty(entity.Batch))
                {
                    strSql += " and o.Batch like '%" + entity.Batch + "%'";
                }
                if (!string.IsNullOrEmpty(entity.FacOrderNo))
                {
                    strSql += " and po.FacOrderNo ='" + entity.FacOrderNo + "'";
                }
                if (!entity.HouseID.Equals(0))
                {
                    strSql += " and po.HouseID=" + entity.HouseID;
                }
                strSql += " ) A WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) order by A.RowNumber";
                using (DbCommand command = conn.GetSqlStringCommond(strSql))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoPurchaseOrderGoodsEntity()
                            {
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                FacOrderNo = Convert.ToString(idr["FacOrderNo"]),
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                InPiece = Convert.ToInt32(idr["InPiece"]),
                                ReceivePiece = string.IsNullOrEmpty(Convert.ToString(idr["ReceivePiece"])) ? 0 : Convert.ToInt32(idr["ReceivePiece"]),
                                ReturnPiece = string.IsNullOrEmpty(Convert.ToString(idr["ReturnPiece"])) ? 0 : Convert.ToInt32(idr["ReturnPiece"]),
                                ProductCode = Convert.ToString(idr["ProductCode"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                Born = Convert.ToInt32(idr["Born"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                InHousePrice = Convert.ToDouble(idr["InHousePrice"]),
                                UnitPrice = Convert.ToDouble(idr["UnitPrice"]),
                                CostPrice = Convert.ToDouble(idr["CostPrice"]),
                                TradePrice = Convert.ToDouble(idr["TradePrice"]),
                                SalePrice = Convert.ToDouble(idr["SalePrice"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                Model = Convert.ToString(idr["Model"]),
                                //ProductName = Convert.ToString(idr["ProductName"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                LoadSpeed = Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                            });
                        }
                    }
                }

                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_PurchaseOrderGoods as o inner join Tbl_Cargo_PurchaseOrder po on o.OrderID=po.OrderID where (1=1)";
                if (!string.IsNullOrEmpty(entity.ClientNum))
                {
                    strCount += " and po.ClientNum=" + entity.ClientNum;
                }
                if (!string.IsNullOrEmpty(entity.TrafficType))
                {
                    strCount += " and po.TrafficType=" + entity.TrafficType;
                }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and po.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and po.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (!entity.OrderID.Equals(0))
                {
                    strCount += " where po.OrderID = " + entity.OrderID;
                }
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    string res = entity.Specs.ToUpper().Replace("/", "").Replace("R", "").Replace("C", "").Replace("F", "").Replace("Z", "");
                    if (res.Length <= 3)
                    {
                        if (!string.IsNullOrEmpty(res)) { strCount += " and o.Specs like '%" + res + "%'"; }
                    }
                    if (res.Length > 3 && res.Length <= 5)
                    {
                        strCount += " and (o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3) + "%' or o.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                    }
                    if (res.Length > 5)
                    {
                        strCount += " and (o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "RF" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZR" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZRF" + res.Substring(5, res.Length - 5) + "%' or o.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                    }
                }
                if (!entity.TypeID.Equals(0))
                {
                    strCount += " and o.TypeID='" + entity.TypeID + "'";
                }
                if (!string.IsNullOrEmpty(entity.Batch))
                {
                    strCount += " and o.Batch like '%" + entity.Batch + "%'";
                }
                if (!string.IsNullOrEmpty(entity.FacOrderNo))
                {
                    strCount += " and po.FacOrderNo ='" + entity.FacOrderNo + "'";
                }
                if (!entity.HouseID.Equals(0))
                {
                    strCount += " and po.HouseID=" + entity.HouseID;
                }

                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }
        /// <summary>
        /// 修改进仓单明细来货件数、退货件数 
        /// </summary>
        /// <param name="entity">进仓单</param>
        public void UpdatePurchaseOrderGoodsPiece(CargoPurchaseOrderGoodsEntity entity)
        {
            string strSQL = "update Tbl_Cargo_PurchaseOrderGoods set ReceivePiece=@ReceivePiece , ReturnPiece = @ReturnPiece where  ProductCode=@ProductCode  and OrderID=@OrderID";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                if (!entity.OrderID.Equals(0))
                {
                    conn.AddInParameter(command, "@OrderID", DbType.Int64, entity.OrderID);
                }
                conn.AddInParameter(command, "@ReturnPiece", DbType.Int32, entity.ReturnPiece);
                conn.AddInParameter(command, "@ReceivePiece", DbType.Int32, entity.ReceivePiece);
                conn.AddInParameter(command, "@ProductCode", DbType.String, entity.ProductCode);
                conn.ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 更新进仓单主表 收货状态
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="clientNum">客户编码</param>
        /// <param name="facOrderNo">出库单号</param>
        /// <param name="receivingStatus">收货状态</param>
        public void UpdatePusrhOrderReceivingStatus(long orderID, int clientNum, string facOrderNo, string receivingStatus)
        {
            string updateSQL = "update Tbl_Cargo_PurchaseOrder set ReceivingStatus =@ReceivingStatus  where FacOrderNo=@FacOrderNo and OrderID=@OrderID and ClientNum=@ClientNum";
            using (DbCommand upCommand = conn.GetSqlStringCommond(updateSQL))
            {
                conn.AddInParameter(upCommand, "@ReceivingStatus", DbType.String, receivingStatus);
                conn.AddInParameter(upCommand, "@FacOrderNo", DbType.String, facOrderNo);
                conn.AddInParameter(upCommand, "@OrderID", DbType.Int64, orderID);
                conn.AddInParameter(upCommand, "@ClientNum", DbType.Int32, clientNum);
                conn.ExecuteNonQuery(upCommand);
            }
        }

        /// <summary>
        /// 新增【退货订单】进仓单主表及明细产品 
        /// </summary>
        /// <param name="entity">进仓订单</param>
        /// <param name="goodsList">明细产品</param>
        /// <param name="receivingStatus">收货状态</param>
        public void AddReturnPurschaseOrders(CargoPurchaseOrderEntity entity, List<CargoPurchaseOrderGoodsEntity> goodsList)
        {
            Int64 did = 0;//[退货订单]进仓单主表的[OrderID]
            string strSQL = @"INSERT INTO Tbl_Cargo_PurchaseOrder( Piece,TransportFee,TotalCharge ,TransitFee,HandFee,OtherFee,TrafficType,DeliveryType,ClientNum,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,CreateAwbID,CreateAwb,CreateDate ,HouseID,Remark,OP_DATE,OP_ID,FacOrderNo,ReceivingStatus) OUTPUT Inserted.OrderID VALUES(@Piece ,@TransportFee ,@TotalCharge,@TransitFee ,@HandFee,@OtherFee,@TrafficType ,@DeliveryType ,@ClientNum ,@AcceptUnit ,@AcceptAddress ,@AcceptPeople ,@AcceptTelephone ,@AcceptCellphone ,@CreateAwbID ,@CreateAwb ,@CreateDate  ,@HouseID ,@Remark ,@OP_DATE ,@OP_ID ,@FacOrderNo ,@ReceivingStatus)";
            try
            {
                #region 退货进仓单订单及明细产品保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.String, entity.TransitFee);
                    conn.AddInParameter(cmd, "@HandFee", DbType.String, entity.HandFee);
                    conn.AddInParameter(cmd, "@OtherFee", DbType.String, entity.OtherFee);
                    conn.AddInParameter(cmd, "@TrafficType", DbType.String, entity.TrafficType);
                    conn.AddInParameter(cmd, "@DeliveryType", DbType.String, entity.DeliveryType);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@CreateAwbID", DbType.String, entity.CreateAwbID);
                    conn.AddInParameter(cmd, "@CreateAwb", DbType.String, entity.CreateAwb);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, entity.FacOrderNo);
                    conn.AddInParameter(cmd, "@ReceivingStatus", DbType.String, entity.ReceivingStatus);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));//拿到主键OrderID
                }
                //新增退货单明细
                foreach (var it in goodsList)
                {
                    it.EnSafe();
                    string goodsSQL = @"INSERT INTO Tbl_Cargo_PurchaseOrderGoods(OrderID ,TypeID ,Piece ,ReceivePiece ,ReturnPiece ,ProductCode ,Specs ,Figure ,Model ,GoodsCode ,LoadIndex ,SpeedLevel ,Born ,Batch ,UnitPrice ,CostPrice ,TradePrice ,SalePrice ,OP_ID ,OP_DATE)  VALUES (@OrderID,@TypeID,@Piece,@ReceivePiece,@ReturnPiece,@ProductCode,@Specs,@Figure,@Model,@GoodsCode,@LoadIndex,@SpeedLevel,@Born,@Batch,@UnitPrice,@CostPrice,@TradePrice,@SalePrice,@OP_ID,@OP_DATE)";
                    using (DbCommand cmd = conn.GetSqlStringCommond(goodsSQL))
                    {
                        conn.AddInParameter(cmd, "@OrderID", DbType.Int64, did);
                        conn.AddInParameter(cmd, "@TypeID", DbType.Int32, it.TypeID);
                        conn.AddInParameter(cmd, "@Piece ", DbType.Int32, it.Piece);
                        conn.AddInParameter(cmd, "@ReceivePiece ", DbType.Int32, it.ReceivePiece);
                        conn.AddInParameter(cmd, "@ReturnPiece ", DbType.Int32, it.ReturnPiece);
                        conn.AddInParameter(cmd, "@ProductCode ", DbType.String, it.ProductCode);
                        conn.AddInParameter(cmd, "@Specs", DbType.String, it.Specs);
                        conn.AddInParameter(cmd, "@Figure", DbType.String, it.Figure);
                        conn.AddInParameter(cmd, "@Model", DbType.String, it.Model);
                        conn.AddInParameter(cmd, "@GoodsCode", DbType.String, it.GoodsCode);
                        conn.AddInParameter(cmd, "@LoadIndex ", DbType.String, it.LoadIndex);
                        conn.AddInParameter(cmd, "@SpeedLevel ", DbType.String, it.SpeedLevel);
                        conn.AddInParameter(cmd, "@Born ", DbType.String, it.Born);
                        conn.AddInParameter(cmd, "@Batch", DbType.String, it.Batch);
                        conn.AddInParameter(cmd, "@UnitPrice", DbType.Decimal, it.UnitPrice);
                        conn.AddInParameter(cmd, "@CostPrice", DbType.Decimal, it.CostPrice);
                        conn.AddInParameter(cmd, "@TradePrice", DbType.Decimal, it.TradePrice);
                        conn.AddInParameter(cmd, "@SalePrice", DbType.Decimal, it.SalePrice);
                        conn.AddInParameter(cmd, "@OP_ID", DbType.String, it.OP_ID);
                        conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }

        #endregion
        #region 订单管理操作方法集合
        /// <summary>
        /// 修改好来运新系统计划单号 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="Awbno"></param>
        public void UpdateHLYPlanOrderNo(string orderNo, string Awbno)
        {
            string strSQL = @"UPDATE Tbl_Cargo_Order Set LogisAwbNo=@LogisAwbNo Where OrderNo=@OrderNo";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, Awbno);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, orderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 判断 是否存在产品订单，如果存在则不允许删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistOrderInfo(CargoOrderGoodsEntity entity)
        {
            string strSQL = "Select ProductID from Tbl_Cargo_OrderGoods where (1=1)";
            if (!entity.ProductID.Equals(0))
            {
                strSQL += " and ProductID=@ProductID";
            }
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                if (!entity.ProductID.Equals(0))
                {
                    conn.AddInParameter(cmdQ, "@ProductID", DbType.Int64, entity.ProductID);
                }
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

       
        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryOrderInfo(int pIndex, int pNum, CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,b.LogisticName,c.WXPayOrderNo,c.PayWay,c.PayStatus ,h.Name AS HouseName,h.Cellphone as HouseCellphone,d.ClientShortName as SuppClientName,c.Trxid from Tbl_Cargo_Order as a left join tbl_cargo_Logistic as b on a.LogisID=b.ID left join Tbl_WX_Order as c on a.WXOrderNo=c.OrderNo inner JOIN Tbl_Cargo_House AS h ON a.HouseID = h.HouseID left join Tbl_Cargo_Client as d on a.SuppClientNum=d.ClientNum  Where (1=1) ";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and a.OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.OutHouseName)) { strSQL += " and a.OutHouseName = '" + entity.OutHouseName + "'"; }
                if (!string.IsNullOrEmpty(entity.TranHouse)) { strSQL += " and a.TranHouse = '" + entity.TranHouse + "'"; }
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                    }
                    else if (entity.AwbStatus.Equals("-5")) 
                    {
                        strSQL += " and a.AwbStatus<>'5'";
                    }
                    else
                    {
                        strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                    }
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'"; }
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strSQL += " and a.FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and a.AcceptPeople like '%" + entity.AcceptPeople + "%'"; }
                if (!string.IsNullOrEmpty(entity.HAwbNo)) { strSQL += " and a.HAwbNo like '%" + entity.HAwbNo + "%'"; }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strSQL += " and a.PayClientName = '" + entity.PayClientName + "'"; }
                if (!entity.PayClientNum.Equals(0)) { strSQL += " and a.PayClientNum=" + entity.PayClientNum; }
                if (!entity.LineID.Equals(0)) { strSQL += " and a.LineID=" + entity.LineID; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strSQL += " and a.Dep = '" + entity.Dep + "'"; }
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
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //物流单号
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strSQL += " and a.LogisAwbNo = '" + entity.LogisAwbNo + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID ='" + entity.SaleManID + "'"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.OrderModel)) { strSQL += " and a.OrderModel ='" + entity.OrderModel + "'"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.ThrowGood))
                {
                    strSQL += " and a.ThrowGood ='" + entity.ThrowGood + "'";
                }
                //第三方订单类型
                if (!string.IsNullOrEmpty(entity.OpenOrderSource))
                {
                    strSQL += " and a.OpenOrderSource ='" + entity.OpenOrderSource + "'";
                }
                //   if (!string.IsNullOrEmpty(entity.ThrowGood)) { strSQL += " and a.ThrowGood ='" + entity.ThrowGood + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and a.Piece=" + entity.Piece + ""; }
                //公司类型
                if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and a.BelongHouse = " + entity.BelongHouse; }
                //客户名称
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and a.PayClientName like '%" + entity.AcceptUnit + "%'"; }
                //店代码
                if (!string.IsNullOrEmpty(entity.ShopCode)) { strSQL += " and a.ShopCode ='" + entity.ShopCode + "'"; }
                //出库类型
                if (entity.OutCargoType == "1")
                {
                    strSQL += " and (a.OutCargoTime>CONVERT(varchar(100), a.CreateDate, 23)+' 23:59:59' or (a.AwbStatus=0 or a.AwbStatus=1)and a.CreateDate<DATEADD(DAY, 0, DATEDIFF(DAY, 0, GETDATE()))) and a.PostponeShip=0 and a.LogisID!=24 and a.LogisID!=46";
                }
                else if (entity.OutCargoType == "0")
                {
                    strSQL += " and (a.OutCargoTime<CONVERT(varchar(100), a.CreateDate, 23)+' 23:59:59' or (a.AwbStatus=0 or a.AwbStatus=1)and (a.CreateDate>DATEADD(SECOND, -1, DATEDIFF(DAY, 0, GETDATE())) or a.PostponeShip!=0))";
                }
                //推送类型
                if (entity.PostponeShip == "0")
                {
                    strSQL += " and a.PostponeShip=1";
                }
                else if (entity.PostponeShip == "1")
                {
                    strSQL += " and a.PostponeShip!=1";
                }
                if (!string.IsNullOrEmpty(entity.CheckStatus)) { strSQL += " and a.CheckStatus = '" + entity.CheckStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.TrafficType)) { strSQL += " and a.TrafficType = " + entity.TrafficType; }
                if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and c.PayStatus='" + entity.PayStatus + "'"; }
                if (!entity.SuppClientNum.Equals(0)) { strSQL += " and a.SuppClientNum='" + entity.SuppClientNum + "'"; }
                if (!string.IsNullOrEmpty(entity.BusinessID)) { strSQL += " and a.BusinessID=" + Convert.ToInt32(entity.BusinessID); }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) order by A.RowNumber";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            decimal ptf = Convert.ToDecimal(idr["OtherFee"]);
                            if (Convert.ToString(idr["OrderType"]).Equals("4"))
                            {
                                int dj = (int)(Convert.ToDecimal(idr["TotalCharge"]) + Convert.ToDecimal(idr["InsuranceFee"]) - Convert.ToDecimal(idr["TransitFee"])) / Convert.ToInt32(idr["Piece"]);
                                int djjg = (int)Math.Floor(dj / Convert.ToDecimal(1.009));
                                ptf = (dj - djjg) * Convert.ToInt32(idr["Piece"]);
                            }

                            #region 获取运单数据

                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                                OtherFee = ptf,
                                OverDueFee = string.IsNullOrEmpty(Convert.ToString(idr["OverDueFee"])) ? 0 : Convert.ToDecimal(idr["OverDueFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                HouseCellphone = Convert.ToString(idr["HouseCellphone"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                LineID = string.IsNullOrEmpty(Convert.ToString(idr["LineID"])) ? 0 : Convert.ToInt32(idr["LineID"]),
                                LineName = Convert.ToString(idr["LineName"]),
                                LogisticName = Convert.ToString(idr["LogisticName"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                SaleCellPhone = Convert.ToString(idr["SaleCellPhone"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                OrderModel = Convert.ToString(idr["OrderModel"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                CheckDate = string.IsNullOrEmpty(Convert.ToString(idr["CheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckDate"]),
                                FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]),
                                FinanceSecondCheckName = Convert.ToString(idr["FinanceSecondCheckName"]),
                                FinanceSecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FinanceSecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FinanceSecondCheckDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                WXOrderNo = Convert.ToString(idr["OrderType"]).Equals("0") || Convert.ToString(idr["OrderType"]).Equals("1") ? "" : Convert.ToString(idr["WXOrderNo"]),
                                PayWay = Convert.ToString(idr["PayWay"]),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                                PayStatus = Convert.ToString(idr["PayStatus"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                TranHouse = Convert.ToString(idr["TranHouse"]),
                                Signer = Convert.ToString(idr["Signer"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                ModifyPriceStatus = Convert.ToString(idr["ModifyPriceStatus"]),
                                PayClientName = Convert.ToString(idr["PayClientName"]),
                                PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                                IsPrintPrice = Convert.ToInt32(idr["IsPrintPrice"]),
                                BelongHouse = Convert.ToString(idr["BelongHouse"]),
                                PostponeShip = string.IsNullOrEmpty(Convert.ToString(idr["PostponeShip"])) ? "0" : Convert.ToString(idr["PostponeShip"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                OutCargoTime = string.IsNullOrEmpty(Convert.ToString(idr["OutCargoTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["OutCargoTime"]),
                                DeliverySettlement = Convert.ToString(idr["DeliverySettlement"]),
                                OrderAging = Convert.ToString(idr["OrderAging"]),
                                PrintNum = string.IsNullOrEmpty(Convert.ToString(idr["PrintNum"])) ? 1 : Convert.ToInt32(idr["PrintNum"]),
                                SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                PickStatus = string.IsNullOrEmpty(Convert.ToString(idr["PickStatus"])) ? 0 : Convert.ToInt32(idr["PickStatus"]),
                                ShopCode = Convert.ToString(idr["ShopCode"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                TakeOrderName = Convert.ToString(idr["TakeOrderName"]),
                                SuppClientNum = string.IsNullOrEmpty(Convert.ToString(idr["SuppClientNum"])) ? 0 : Convert.ToInt32(idr["SuppClientNum"]),
                                SuppClientName = Convert.ToString(idr["SuppClientName"]),
                                TakeOrderTime = string.IsNullOrEmpty(Convert.ToString(idr["TakeOrderTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["TakeOrderTime"]),
                                SendCarName = Convert.ToString(idr["SendCarName"]),
                                SendCarTime = string.IsNullOrEmpty(Convert.ToString(idr["SendCarTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SendCarTime"]),
                                OpenOrderSource = Convert.ToString(idr["OpenOrderSource"]),
                                OpenOrderNo = Convert.ToString(idr["OpenOrderNo"]),
                                CouponType = Convert.ToString(idr["CouponType"]),
                                DeliveryDriverName = Convert.ToString(idr["DeliveryDriverName"]),
                                DriverCarNum = Convert.ToString(idr["DriverCarNum"]),
                                DriverCellphone = Convert.ToString(idr["DriverCellphone"]),
                                DriverIDNum = Convert.ToString(idr["DriverIDNum"]),
                                Trxid = Convert.ToString(idr["Trxid"]),
                                BateAmount = string.IsNullOrEmpty(Convert.ToString(idr["BateAmount"]))?0: Convert.ToDecimal(idr["BateAmount"]),
                                OnlinePaidAmount = string.IsNullOrEmpty(Convert.ToString(idr["OnlinePaidAmount"]))?0: Convert.ToDecimal(idr["OnlinePaidAmount"]),

                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_Order  Where (1=1)";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strCount += " and OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.OutHouseName)) { strCount += " and OutHouseName = '" + entity.OutHouseName + "'"; }
                if (!string.IsNullOrEmpty(entity.TranHouse)) { strCount += " and TranHouse = '" + entity.TranHouse + "'"; }
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                    }
                    else if (entity.AwbStatus.Equals("-5"))
                    {
                        strSQL += " and a.AwbStatus<>'5'";
                    }
                    else
                    {
                        strCount += " and AwbStatus='" + entity.AwbStatus + "'";
                    }
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strCount += " and CheckOutType = '" + entity.CheckOutType + "'"; }
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strCount += " and FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.HAwbNo)) { strCount += " and HAwbNo like '%" + entity.HAwbNo + "%'"; }
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strCount += " and AcceptPeople like '%" + entity.AcceptPeople + "%'"; }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strCount += " and PayClientName = '" + entity.PayClientName + "'"; }
                if (!entity.PayClientNum.Equals(0)) { strCount += " and PayClientNum=" + entity.PayClientNum; }
                if (!entity.LineID.Equals(0)) { strCount += " and LineID=" + entity.LineID; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strCount += " and Dep = '" + entity.Dep + "'"; }
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
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strCount += " and OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //物流单号
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strCount += " and LogisAwbNo = '" + entity.LogisAwbNo + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strCount += " and CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strCount += " and SaleManID ='" + entity.SaleManID + "'"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.OrderModel)) { strCount += " and OrderModel ='" + entity.OrderModel + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood))
                {
                    strCount += " and ThrowGood ='" + entity.ThrowGood + "'";
                }
                //第三方订单类型
                if (!string.IsNullOrEmpty(entity.OpenOrderSource))
                {
                    strSQL += " and a.OpenOrderSource ='" + entity.OpenOrderSource + "'";
                }
                //件数
                if (!entity.Piece.Equals(0)) { strCount += " and Piece=" + entity.Piece + ""; }
                //公司类型
                if (!string.IsNullOrEmpty(entity.BelongHouse)) { strCount += " and BelongHouse = " + entity.BelongHouse; }
                //客户名称
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strCount += " and PayClientName like '%" + entity.AcceptUnit + "%'"; }
                //出库类型
                if (entity.OutCargoType == "1")
                {
                    strCount += " and (OutCargoTime>CONVERT(varchar(100), CreateDate, 23)+' 23:59:59' or (AwbStatus=0 or AwbStatus=1)and CreateDate<DATEADD(DAY, 0, DATEDIFF(DAY, 0, GETDATE()))) and PostponeShip=0 and LogisID!=24 and LogisID!=46";
                }
                else if (entity.OutCargoType == "0")
                {
                    strCount += " and (OutCargoTime<CONVERT(varchar(100), CreateDate, 23)+' 23:59:59' or (AwbStatus=0 or AwbStatus=1)and (CreateDate>DATEADD(SECOND, -1, DATEDIFF(DAY, 0, GETDATE())) or PostponeShip!=0))";
                }
                //推送类型
                if (entity.PostponeShip == "0") { strCount += " and PostponeShip=1"; }
                else if (entity.PostponeShip == "1") { strCount += " and PostponeShip!=1"; }
                if (!string.IsNullOrEmpty(entity.CheckStatus)) { strCount += " and CheckStatus = '" + entity.CheckStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.TrafficType)) { strCount += " and TrafficType = " + entity.TrafficType; }
                if (!entity.SuppClientNum.Equals(0)) { strCount += " and SuppClientNum='" + entity.SuppClientNum + "'"; }
                if (!string.IsNullOrEmpty(entity.BusinessID)) { strCount += " and BusinessID=" + Convert.ToInt32(entity.BusinessID); }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 通过OrderID查询订单数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public CargoOrderEntity QueryOrderInfoByOrderID(Int64 orderID)
        {
            CargoOrderEntity result = new CargoOrderEntity();
            try
            {
                string strSQL = @"Select * from Tbl_Cargo_Order Where OrderID=@OrderID";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderID", DbType.Int64, orderID);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                Weight = Convert.ToDecimal(idr["Weight"]),
                                Volume = Convert.ToDecimal(idr["Volume"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                TranHouse = Convert.ToString(idr["TranHouse"]),
                                IsPrintPrice = Convert.ToInt32(idr["IsPrintPrice"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                                PayClientName = Convert.ToString(idr["PayClientName"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                SaleCellPhone = Convert.ToString(idr["SaleCellPhone"]),
                                FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]),
                                FinanceSecondCheckName = Convert.ToString(idr["FinanceSecondCheckName"]),
                                FinanceSecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FinanceSecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FinanceSecondCheckDate"]),
                                OutCargoTime = string.IsNullOrEmpty(Convert.ToString(idr["OutCargoTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["OutCargoTime"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                WXOrderNo = Convert.ToString(idr["WXOrderNo"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                DeliveryDriverName = Convert.ToString(idr["DeliveryDriverName"]),
                                DriverCarNum = Convert.ToString(idr["DriverCarNum"]),
                                DriverCellphone = Convert.ToString(idr["DriverCellphone"]),
                                DriverIDNum = Convert.ToString(idr["DriverIDNum"]),
                                OpenOrderNo = Convert.ToString(idr["OpenOrderNo"]),
                                OpenOrderSource = Convert.ToString(idr["OpenOrderSource"]),
                                ShareHouseID = Convert.ToInt32(idr["ShareHouseID"]),
                                OrignHouseID = Convert.ToInt32(idr["OrignHouseID"]),
                            };
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CargoOrderEntity QueryOrderInfo(CargoOrderEntity entity)
        {
            CargoOrderEntity result = new CargoOrderEntity();
            try
            {
                string strSQL = @"Select top 10 a.*,ISNULL(b.PreReceiveMoney,0) as PreReceiveMoney,ISNULL(b.RebateMoney,0) as RebateMoney,ISNULL(b.ClientID,0) as ClientID,b.City,b.Province,b.ShopCode,b.UpClientID,b.UpClientShortName,c.LogisticName,h.HouseCode,h.Name as HouseName from Tbl_Cargo_Order as a left join Tbl_Cargo_Client as b on a.PayClientNum=b.ClientNum left join Tbl_Cargo_Logistic c on a.LogisID=c.ID inner join Tbl_Cargo_House h on a.HouseID=h.HouseID Where 1=1";
                if (!entity.HouseID.Equals(0))
                {
                    strSQL += " and a.HouseID=" + entity.HouseID;
                }
                if (!string.IsNullOrEmpty(entity.HouseIDStr))
                {
                    strSQL += " and a.HouseID in (" + entity.HouseIDStr + ")";

                }
                if (!string.IsNullOrEmpty(entity.OrderNo))
                {
                    //strSQL += " and a.OrderNo like '%" + entity.OrderNo + "%'";
                    strSQL += " and a.OrderNo=@OrderNo";
                }
                if (entity.OrderID != 0)
                {
                    strSQL += " and a.OrderID=@OrderID";
                }
                if (!string.IsNullOrEmpty(entity.HAwbNo))
                {
                    strSQL += " and CHARINDEX(','+'" + entity.HAwbNo + "'+',' , ','+a.HAwbNo+',') > 0";
                }
                if (!string.IsNullOrEmpty(entity.ShopCode))
                {
                    strSQL += " and b.ShopCode=@ShopCode";
                }
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {

                    if (!string.IsNullOrEmpty(entity.OrderNo))
                    {
                        conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);
                    }
                    if (entity.OrderID != 0)
                    {
                        conn.AddInParameter(command, "@OrderID", DbType.Int64, entity.OrderID);
                    }
                    if (!string.IsNullOrEmpty(entity.ShopCode))
                    {
                        conn.AddInParameter(command, "@ShopCode", DbType.String, entity.ShopCode);
                    }
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                LogisticName = Convert.ToString(idr["LogisticName"]),
                                HAwbNo = Convert.ToString(idr["HAwbNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                OrderModel = Convert.ToString(idr["OrderModel"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseCode = Convert.ToString(idr["HouseCode"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                ClientID = Convert.ToInt64(idr["ClientID"]),
                                ShopCode = Convert.ToString(idr["ShopCode"]),
                                PreReceiveMoney = Convert.ToDecimal(idr["PreReceiveMoney"]),
                                RebateMoney = Convert.ToDecimal(idr["RebateMoney"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                SaleCellPhone = Convert.ToString(idr["SaleCellPhone"]),
                                PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                                PayClientName = Convert.ToString(idr["PayClientName"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                BelongHouse = Convert.ToString(idr["BelongHouse"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]),
                                FinanceSecondCheckName = Convert.ToString(idr["FinanceSecondCheckName"]),
                                FinanceSecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FinanceSecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FinanceSecondCheckDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                WXOrderNo = Convert.ToString(idr["WXOrderNo"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                ModifyPriceStatus = Convert.ToString(idr["ModifyPriceStatus"]),
                                PostponeShip = string.IsNullOrEmpty(Convert.ToString(idr["PostponeShip"])) ? "0" : Convert.ToString(idr["PostponeShip"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]),
                                Province = Convert.ToString(idr["Province"]),
                                City = Convert.ToString(idr["City"]),
                                OrderAging = Convert.ToString(idr["OrderAging"]),
                                UpClientID = string.IsNullOrEmpty(Convert.ToString(idr["UpClientID"])) ? 0 : Convert.ToInt64(idr["UpClientID"]),
                                UpClientShortName = Convert.ToString(idr["UpClientShortName"]),
                                PlanOrderNo = Convert.ToString(idr["PlanOrderNo"]),
                                BusinessID = Convert.ToString(idr["BusinessID"]),
                                OpenExpressName = Convert.ToString(idr["OpenExpressName"]),
                                OpenExpressNum = Convert.ToString(idr["OpenExpressNum"]),
                                DeliveryDriverName = Convert.ToString(idr["DeliveryDriverName"]),
                                DriverCarNum = Convert.ToString(idr["DriverCarNum"]),
                                DriverCellphone = Convert.ToString(idr["DriverCellphone"]),
                                DriverIDNum = Convert.ToString(idr["DriverIDNum"]),
                                ShareHouseID = Convert.ToInt32(idr["ShareHouseID"]),
                                ShareHouseName = Convert.ToString(idr["ShareHouseName"]),
                                OrignHouseID = Convert.ToInt32(idr["OrignHouseID"]),
                                OrignHouseName = Convert.ToString(idr["OrignHouseName"]),
                                SuppClientNum = string.IsNullOrEmpty(Convert.ToString(idr["SuppClientNum"])) ? 0 : Convert.ToInt32(idr["SuppClientNum"]),
                                OpenOrderNo = Convert.ToString(idr["OpenOrderNo"]),
                                OpenOrderSource = Convert.ToString(idr["OpenOrderSource"]),
                            };
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public List<CargoOrderEntity> QueryWxOrderDataInfo(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            string strSQL = "select o.*,wo.PayStatus,wo.PayWay,wo.WXPayOrderNo,wo.OrderNo WXOrderNo from Tbl_WX_Order wo inner join Tbl_Cargo_Order o on wo.OrderNo=o.WXOrderNo and wo.HouseID=o.HouseID inner join Tbl_Cargo_House as a on o.HouseID=a.HouseID where (1=1) and o.FinanceSecondCheck=0 ";
            if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and a.BelongHouse='" + entity.BelongHouse + "'"; }
            if (!entity.HouseID.Equals(0)) { strSQL += " and wo.HouseID=" + entity.HouseID; }
            if (!string.IsNullOrEmpty(entity.HouseIDStr))
            {
                strSQL += " and wo.HouseID in (" + entity.HouseIDStr + ")";
            }
            if (!string.IsNullOrEmpty(entity.PayStatus))
            {
                strSQL += " and wo.PayStatus=" + entity.PayStatus;
            }
            if (!string.IsNullOrEmpty(entity.CheckOutType))
            {
                strSQL += " and o.CheckOutType=" + entity.CheckOutType;
            }
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and wo.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and wo.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.CreateDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.CreateDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and wo.CreateDate<'" + entity.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            strSQL += " order by wo.CreateDate desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        if (!Convert.ToString(idr["AwbStatus"]).Equals("0")) { continue; }
                        #region 获取运单数据

                        result.Add(new CargoOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                            DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                            OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            Rebate = Convert.ToDecimal(idr["Rebate"]),
                            CheckOutType = Convert.ToString(idr["CheckOutType"]),
                            TrafficType = Convert.ToString(idr["TrafficType"]),
                            DeliveryType = Convert.ToString(idr["DeliveryType"]),
                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                            CheckStatus = Convert.ToString(idr["CheckStatus"]),
                            Remark = Convert.ToString(idr["Remark"]),
                            LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                            LogisID = Convert.ToInt32(idr["LogisID"]),
                            FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]),
                            AwbStatus = Convert.ToString(idr["AwbStatus"]),
                            CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                            CreateAwb = Convert.ToString(idr["CreateAwb"]),
                            SaleManID = Convert.ToString(idr["SaleManID"]),
                            SaleManName = Convert.ToString(idr["SaleManName"]),
                            ClientNum = Convert.ToInt32(idr["ClientNum"]),
                            PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                            PayClientName = Convert.ToString(idr["PayClientName"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            OrderModel = Convert.ToString(idr["OrderModel"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            WXOrderNo = Convert.ToString(idr["WXOrderNo"]),
                            PayWay = Convert.ToString(idr["PayWay"]),
                            WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            PayStatus = Convert.ToString(idr["PayStatus"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            TranHouse = Convert.ToString(idr["TranHouse"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                        });
                        #endregion
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询优惠卷的条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryWxOrderRule(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            string strSQL = " SELECT DISTINCT a.OrderNo,a.SuppClientNum,a.HouseID,c.TypeID FROM Tbl_WX_Order  as a   LEFT JOIN Tbl_WX_OrderProduct AS b  ON a.ID = b.OrderID   LEFT JOIN Tbl_Cargo_Product AS c  ON b.ProductID = c.ProductID   where  a.OrderNo='" + entity.OrderNo + "'";

            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        #region 获取优惠卷的条件数据

                        result.Add(new CargoOrderEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            SuppClientNum = string.IsNullOrEmpty(Convert.ToString(idr["SuppClientNum"])) ? 0 : Convert.ToInt32(idr["SuppClientNum"]),
                            HouseID = string.IsNullOrEmpty(Convert.ToString(idr["HouseID"])) ? 0 : Convert.ToInt32(idr["HouseID"]),
                            TypeID = string.IsNullOrEmpty(Convert.ToString(idr["TypeID"])) ? 0 : Convert.ToInt32(idr["TypeID"]),
                        });
                        #endregion
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询订单数据列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryOrderDataInfo(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            string strSQL = "Select * From Tbl_Cargo_Order as a Where (1=1) and a.ModifyPriceStatus='0'";
            if (!entity.PayClientNum.Equals(0)) { strSQL += " and a.PayClientNum=" + entity.PayClientNum; }
            //订单状态
            if (!string.IsNullOrEmpty(entity.AwbStatus))
            {
                if (entity.AwbStatus.Equals("0"))
                {
                    strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                }
                else
                {
                    strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                }
            }
            //订单编号 
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.CreateDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.CreateDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate<'" + entity.CreateDate.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(entity.TrafficType)) { strSQL += " and a.TrafficType='" + entity.TrafficType + "'"; }
            if (!string.IsNullOrEmpty(entity.PostponeShip))
            {
                if (entity.PostponeShip.Equals("-1"))
                {
                    strSQL += " and a.PostponeShip!='2'";
                }
                else
                {
                    strSQL += " and a.PostponeShip='" + entity.PostponeShip + "'";
                }
            }
            //所属仓库ID
            if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
            if (!string.IsNullOrEmpty(entity.ThrowGood)) { strSQL += " and a.ThrowGood ='" + entity.ThrowGood + "'"; }
            if (!string.IsNullOrEmpty(entity.CheckStatus)) { strSQL += " and a.CheckStatus = '" + entity.CheckStatus + "'"; }
            //订单类型
            if (!string.IsNullOrEmpty(entity.OrderModel)) { strSQL += " and a.OrderModel ='" + entity.OrderModel + "'"; }
            if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and a.BelongHouse ='" + entity.BelongHouse + "'"; }
            strSQL += " order by a.CreateDate desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        #region 获取运单数据

                        result.Add(new CargoOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                            DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                            OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            Rebate = Convert.ToDecimal(idr["Rebate"]),
                            CheckOutType = Convert.ToString(idr["CheckOutType"]),
                            TrafficType = Convert.ToString(idr["TrafficType"]),
                            DeliveryType = Convert.ToString(idr["DeliveryType"]),
                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                            CheckStatus = Convert.ToString(idr["CheckStatus"]),
                            Remark = Convert.ToString(idr["Remark"]),
                            LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                            LogisID = Convert.ToInt32(idr["LogisID"]),
                            //LogisticName = Convert.ToString(idr["LogisticName"]),
                            FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]),
                            AwbStatus = Convert.ToString(idr["AwbStatus"]),
                            CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                            CreateAwb = Convert.ToString(idr["CreateAwb"]),
                            SaleManID = Convert.ToString(idr["SaleManID"]),
                            SaleManName = Convert.ToString(idr["SaleManName"]),
                            ClientNum = Convert.ToInt32(idr["ClientNum"]),
                            PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                            PayClientName = Convert.ToString(idr["PayClientName"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            OrderModel = Convert.ToString(idr["OrderModel"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            //WXOrderNo = Convert.ToString(idr["OrderType"]).Equals("0") || Convert.ToString(idr["OrderType"]).Equals("1") ? "" : Convert.ToString(idr["WXOrderNo"]),
                            //PayWay = Convert.ToString(idr["PayWay"]),
                            //WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            TranHouse = Convert.ToString(idr["TranHouse"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                        });
                        #endregion
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询客户订单用于账单对账
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryOrderAccount(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            string strSQL = "Select * From Tbl_Cargo_Order as a Where (1=1) and a.ModifyPriceStatus='0'";
            if (!entity.PayClientNum.Equals(0)) { strSQL += " and a.PayClientNum=" + entity.PayClientNum; }
            //订单状态
            if (!string.IsNullOrEmpty(entity.AwbStatus))
            {
                if (entity.AwbStatus.Equals("0"))
                {
                    strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                }
                else
                {
                    strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                }
            }
            //订单编号 
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            //所属仓库ID
            if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
            //订单类型
            if (!string.IsNullOrEmpty(entity.OrderModel)) { strSQL += " and a.OrderModel ='" + entity.OrderModel + "'"; }
            if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and a.BelongHouse ='" + entity.BelongHouse + "'"; }
            strSQL += " order by a.CreateDate desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        #region 获取运单数据

                        result.Add(new CargoOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                            DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                            OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            Rebate = Convert.ToDecimal(idr["Rebate"]),
                            CheckOutType = Convert.ToString(idr["CheckOutType"]),
                            TrafficType = Convert.ToString(idr["TrafficType"]),
                            DeliveryType = Convert.ToString(idr["DeliveryType"]),
                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                            CheckStatus = Convert.ToString(idr["CheckStatus"]),
                            Remark = Convert.ToString(idr["Remark"]),
                            LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                            LogisID = Convert.ToInt32(idr["LogisID"]),
                            //LogisticName = Convert.ToString(idr["LogisticName"]),
                            AwbStatus = Convert.ToString(idr["AwbStatus"]),
                            CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                            CreateAwb = Convert.ToString(idr["CreateAwb"]),
                            SaleManID = Convert.ToString(idr["SaleManID"]),
                            SaleManName = Convert.ToString(idr["SaleManName"]),
                            ClientNum = Convert.ToInt32(idr["ClientNum"]),
                            PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                            PayClientName = Convert.ToString(idr["PayClientName"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            OrderModel = Convert.ToString(idr["OrderModel"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            //WXOrderNo = Convert.ToString(idr["OrderType"]).Equals("0") || Convert.ToString(idr["OrderType"]).Equals("1") ? "" : Convert.ToString(idr["WXOrderNo"]),
                            //PayWay = Convert.ToString(idr["PayWay"]),
                            //WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            TranHouse = Convert.ToString(idr["TranHouse"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            ContainerShowEntity = QueryOrderByOrderNo(new CargoOrderEntity { OrderNo = Convert.ToString(idr["OrderNo"]) })
                        });
                        #endregion
                    }
                }
            }
            return result;
        }

        public Hashtable QueryOrderPickPlan(CargoOrderPickPlanEntity entity, int pageIndex, int pagsSize)
        {
            List<CargoOrderPickPlanEntity> result = new List<CargoOrderPickPlanEntity>();
            Hashtable resHT = new Hashtable();
            string strSQL = @" SELECT TOP " + pagsSize + " * from (select ROW_NUMBER()over(order by PickID desc) as RowNumber ,PickID,AY.PickPlanNo,TotalNum,CreateDate,PickStatus,PickType,AY.HouseID,PickNum,B.Name,Memo from Tbl_Cargo_OrderPickPlan AY inner join  Tbl_Cargo_House B ON AY.HouseID=B.HouseID where 1=1";
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CONVERT(varchar(10),CreateDate,120)>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and  CONVERT(varchar(10),CreateDate,120)<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            if (entity.PickStatus != "-1")
            {
                strSQL += " and PickStatus=" + entity.PickStatus;
            }
            if (!string.IsNullOrEmpty(entity.PickPlanNo))
            {
                strSQL += " and PickPlanNo='" + entity.PickPlanNo + "'";
            }
            if (!string.IsNullOrEmpty(Convert.ToString(entity.HouseID)))
            {
                strSQL += " and AY.HouseID IN(" + entity.HouseID + ")";
            }
            strSQL += "    group by PickID,ay.PickPlanNo,TotalNum,CreateDate,PickStatus,PickType,AY.HouseID,PickNum,B.Name,Memo ) as A where RowNumber > (" + pagsSize + "* (" + pageIndex + "-1))";
            try
            {
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CargoOrderPickPlanEntity
                            {
                                PickID = Convert.ToInt32(idr["PickID"]),
                                PickPlanNo = Convert.ToString(idr["PickPlanNo"]),
                                TotalNum = Convert.ToInt32(idr["TotalNum"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                Memo = Convert.ToString(idr["Memo"]),
                                PickStatus = Convert.ToString(idr["PickStatus"]),
                                PickType = Convert.ToString(idr["PickType"]),
                                HouseName = Convert.ToString(idr["Name"]),
                            });
                        }
                    }
                }
                resHT["rows"] = result;
                string strCount = @"Select Count(*) as TotalCount from  Tbl_Cargo_OrderPickPlan AY inner join  Tbl_Cargo_House B ON AY.HouseID=B.HouseID   where 1=1";
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CONVERT(varchar(10),CreateDate,120)>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and  CONVERT(varchar(10),CreateDate,120)<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (entity.PickStatus != "-1")
                {
                    strCount += " and PickStatus=" + entity.PickStatus;
                }
                if (!string.IsNullOrEmpty(Convert.ToString(entity.HouseID)))
                {
                    strCount += " and AY.HouseID IN(" + entity.HouseID + ")";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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

        public Hashtable QueryBundle(CargoOrderBundleEntity entity, int pageSize, int pageIndex)
        {
            Hashtable resHs = new Hashtable();
            List<CargoOrderBundleEntity> result = new List<CargoOrderBundleEntity>();
            string strSQL = " select top " + pageIndex + " * from (SELECT ROW_NUMBER()over(order by a.CreateDate desc) as RowNumber,a.*,b.Name as HouseName FROM  Tbl_Cargo_Bundle as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID where 1=1 ";
            if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and  a.HouseID in (" + entity.CargoPermisID + ")"; }
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CONVERT(varchar(10),a.CreateDate,120)>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and  CONVERT(varchar(10),a.CreateDate,120)<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            if (!string.IsNullOrEmpty(entity.BundleNo))
            {
                strSQL += " and a.BundleNo like %'" + entity.BundleNo + "'%";
            }
            if (!string.IsNullOrEmpty(entity.BundleStatus))
            {
                strSQL += " and a.BundleStatus='" + entity.BundleStatus + "'";
            }
            strSQL += ") as A where RowNumber > (" + pageIndex + "* (" + pageSize + "-1))";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new CargoOrderBundleEntity
                        {
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            TotalNum = Convert.ToInt32(idr["TotalNum"]),
                            BunID = Convert.ToInt64(idr["BunID"]),
                            OP_ID = Convert.ToString(idr["OP_ID"]),
                            BundleType = Convert.ToString(idr["BundleType"]),
                            Memo = Convert.ToString(idr["Memo"]),
                            BundleNo = Convert.ToString(idr["BundleNo"]),
                            BundleStatus = Convert.ToString(idr["BundleStatus"]),
                        });
                    }
                }
            }
            resHs["rows"] = result;
            string strCount = "Select count(*) TotalCount from Tbl_Cargo_Bundle where 1=1";
            if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and  HouseID in (" + entity.CargoPermisID + ")"; }

            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strCount += " and CONVERT(varchar(10),CreateDate,120)>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strCount += " and  CONVERT(varchar(10),CreateDate,120)<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            if (!string.IsNullOrEmpty(entity.BundleNo))
            {
                strCount += " and BundleNo like '%" + entity.BundleNo + "%'";
            }
            if (!string.IsNullOrEmpty(entity.BundleNo))
            {
                strCount += " and BundleStatus='" + entity.BundleStatus + "'";
            }
            using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
            {
                using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                {
                    if (idrCount.Rows.Count > 0)
                    {
                        resHs["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                    }
                }
            }
            return resHs;
        }

        public Hashtable QueryBundleGoods(CargoOrderBundleEntity entity)
        {
            Hashtable resHs = new Hashtable();
            List<CargoOrderBundleEntity> result = new List<CargoOrderBundleEntity>();
            string strSQL = " SELECT * FROM  Tbl_Cargo_BundleGoods where 1=1    ";
            strSQL += " and BundleNo='" + entity.BundleNo + "'";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new CargoOrderBundleEntity
                        {
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            ProductName = Convert.ToString(idr["ProductName"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            BunGoodsID = Convert.ToString(idr["BunGoodsID"]),
                            BundleNo = Convert.ToString(idr["BundleNo"]),

                        });
                    }
                }
            }
            resHs["rows"] = result;
            string strCount = "select count(*) as TotalCount from Tbl_Cargo_BundleGoods where 1=1";
            strCount += " and BundleNo='" + entity.BundleNo + "'";
            using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
            {
                using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                {
                    if (idrCount.Rows.Count > 0)
                    {
                        resHs["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                    }
                }
            }
            return resHs;
        }

        public List<CargoProductInfoEntity> QueryOrderTagInfo(CargoOrderEntity entity)
        {
            List<CargoProductInfoEntity> result = new List<CargoProductInfoEntity>();
            string strSQL = "select a.*,b.Specs,b.Figure,b.LoadIndex,b.SpeedLevel,b.Model,b.ProductName,b.BrandCode,b.CarModel,b.Manufacturer,b.ManufacturerAddress,\r\nb.Seller,b.SellerAddress,b.Servicer,b.ServicerAddress,b.UpClientID,b.UpClientShortName from (\r\nselect a.Piece,b.Batch,case when ISNULL(a.ShowGoodsCode,'')='' then b.GoodsCode when a.ShowGoodsCode='' then b.GoodsCode else a.ShowGoodsCode end as GoodsCode\r\n from Tbl_Cargo_OrderGoods as a inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID where (1=1)";
            if (!string.IsNullOrEmpty(entity.OrderNo))
            {
                strSQL += " and a.OrderNo='" + entity.OrderNo + "'";
            }
            strSQL += " ) as a inner join Tbl_Cargo_ProductSpec as b on a.GoodsCode=b.GoodsCode";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoProductInfoEntity
                        {
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            Model = Convert.ToString(idr["Model"]),
                            ProductName = Convert.ToString(idr["ProductName"]),
                            BrandCode = Convert.ToString(idr["BrandCode"]),
                            CarModel = Convert.ToString(idr["CarModel"]),
                            Manufacturer = Convert.ToString(idr["Manufacturer"]),
                            ManufacturerAddress = Convert.ToString(idr["ManufacturerAddress"]),
                            Seller = Convert.ToString(idr["Seller"]),
                            SellerAddress = Convert.ToString(idr["SellerAddress"]),
                            Servicer = Convert.ToString(idr["Servicer"]),
                            ServicerAddress = Convert.ToString(idr["ServicerAddress"]),
                            UpClientID = Convert.ToInt32(idr["UpClientID"]),
                            UpClientShortName = Convert.ToString(idr["UpClientShortName"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                        });
                    }
                }
            }
            return result;
        }
        public void AddBundleGoods(CargoOrderBundleEntity entity)
        {
            try
            {
                string strSQL = @"Insert into Tbl_Cargo_BundleGoods(BundleNo,GoodsCode,ProductName,Piece,OP_ID,OP_DATE) values(@BundleNo,@GoodsCode,@ProductName,@Piece,@OP_ID,GETDATE())";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@BundleNo", DbType.String, entity.BundleNo);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    conn.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName);
                    conn.AddInParameter(cmd, "@Piece", DbType.String, entity.Piece);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
                { }
            }
        }
        public void UpdataBundleGoods(CargoOrderBundleEntity entity)
        {
            //
            try
            {
                string strSQL = @"update Tbl_Cargo_BundleGoods set BundleNo=@BundleNo,GoodsCode=@GoodsCode,ProductName=@ProductName,Piece=@Piece where BunGoodsID=@BunGoodsID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@BundleNo", DbType.String, entity.BundleNo);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    conn.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName);
                    conn.AddInParameter(cmd, "@Piece", DbType.String, entity.Piece);
                    conn.AddInParameter(cmd, "@BunGoodsID", DbType.String, entity.BunGoodsID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
                { }
            }
        }
        public void DelBundleGoods(List<CargoOrderBundleEntity> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    string strSQL = @" DELETE FROM Tbl_Cargo_BundleGoods WHERE BunGoodsID=@BunGoodsID";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@BunGoodsID", DbType.Int32, item.BunGoodsID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CargoOrderPickPlanGoodsEntity> QueryOrderPickPlanOrderNo(string PickPlanNo)
        {
            List<CargoOrderPickPlanGoodsEntity> result = new List<CargoOrderPickPlanGoodsEntity>();
            string strSQL = $"select OrderNo,HouseID from Tbl_Cargo_OrderPickPlanGoods where PickPlanNo=@PickPlanNo group by OrderNo,HouseID";
            try
            {
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@PickPlanNo", DbType.String, PickPlanNo);
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderPickPlanGoodsEntity
                            {
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                HouseID = Convert.ToString(idr["HouseID"]),
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public void DelOrderPickPlan(CargoOrderPickPlanEntity entity)
        {
            try
            {
                List<CargoOrderPickPlanGoodsEntity> OrderPickPlanOrderNo = QueryOrderPickPlanOrderNo(entity.PickPlanNo);
                string strSQL = @"Delete from Tbl_Cargo_OrderPickPlan where PickID=@PickID and PickPlanNo=@PickPlanNo";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@PickID", DbType.String, entity.PickID);
                    conn.AddInParameter(cmd, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                    conn.ExecuteNonQuery(cmd);
                }
                strSQL = @"Delete from Tbl_Cargo_OrderPickPlanGoods where PickPlanNo=@PickPlanNo ";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                    conn.ExecuteNonQuery(cmd);
                }
                foreach (var item in OrderPickPlanOrderNo)
                {
                    strSQL = @"update Tbl_Cargo_Order set PickStatus=0 where OrderNo=@OrderNo and HouseID=@HouseID";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@OrderNo", DbType.String, item.OrderNo);
                        conn.AddInParameter(cmd, "@HouseID", DbType.String, item.HouseID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public List<CargoOrderPickPlanGoodsEntity> QueryOrderPickPlanGoods(CargoOrderPickPlanGoodsEntity entity)
        {
            List<CargoOrderPickPlanGoodsEntity> result = new List<CargoOrderPickPlanGoodsEntity>();
            string strSQL = " SELECT A.*,B.ProductName,C.Name FROM Tbl_Cargo_OrderPickPlanGoods A JOIN  Tbl_Cargo_Product B ON A.ProductID=B.ProductID  JOIN Tbl_Cargo_House C ON A.HouseID=C.HouseID WHERE PickPlanNo=@PickPlanNo";
            try
            {
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderPickPlanGoodsEntity
                            {
                                PickPlanNo = entity.PickPlanNo,
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                ProductID = Convert.ToString(idr["ProductID"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                HouseName = Convert.ToString(idr["Name"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToString(idr["Piece"]),
                                PitNum = Convert.ToInt32(idr["PitNum"]),
                                ScanStatus = Convert.ToString(idr["ScanStatus"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                ScanUserID = Convert.ToString(idr["ScanUserID"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                ScanPiece = string.IsNullOrEmpty(Convert.ToString(idr["ScanPiece"])) ? 0 : Convert.ToInt32(idr["ScanPiece"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryOrder(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            string strSQL = "Select * From Tbl_Cargo_Order as a Where (1=1) and a.ModifyPriceStatus='0'";
            if (!entity.PayClientNum.Equals(0)) { strSQL += " and a.PayClientNum=" + entity.PayClientNum; }
            //订单状态
            if (!string.IsNullOrEmpty(entity.AwbStatus))
            {
                if (entity.AwbStatus.Equals("0"))
                {
                    strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                }
                else
                {
                    strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                }
            }
            //订单编号 
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            //所属仓库ID
            if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
            //出库仓库
            if (!string.IsNullOrEmpty(entity.OutHouseName)) { strSQL += " and a.OutHouseName ='" + entity.OutHouseName + "'"; }
            //订单类型
            if (!string.IsNullOrEmpty(entity.OrderModel)) { strSQL += " and a.OrderModel ='" + entity.OrderModel + "'"; }
            if (!string.IsNullOrEmpty(entity.TrafficType)) { strSQL += " and a.TrafficType ='" + entity.TrafficType + "'"; }
            if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and a.BelongHouse ='" + entity.BelongHouse + "'"; }
            strSQL += " order by CreateDate desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        #region 获取运单数据

                        result.Add(new CargoOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                            DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                            CollectMoney = Convert.ToDecimal(idr["CollectMoney"]),
                            OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            Rebate = Convert.ToDecimal(idr["Rebate"]),
                            CheckOutType = Convert.ToString(idr["CheckOutType"]),
                            TrafficType = Convert.ToString(idr["TrafficType"]),
                            DeliveryType = Convert.ToString(idr["DeliveryType"]),
                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                            CheckStatus = Convert.ToString(idr["CheckStatus"]),
                            Remark = Convert.ToString(idr["Remark"]),
                            LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                            LogisID = Convert.ToInt32(idr["LogisID"]),
                            //LogisticName = Convert.ToString(idr["LogisticName"]),
                            AwbStatus = Convert.ToString(idr["AwbStatus"]),
                            CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                            CreateAwb = Convert.ToString(idr["CreateAwb"]),
                            SaleManID = Convert.ToString(idr["SaleManID"]),
                            SaleManName = Convert.ToString(idr["SaleManName"]),
                            SaleCellPhone = Convert.ToString(idr["SaleCellPhone"]),
                            ClientNum = Convert.ToInt32(idr["ClientNum"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            OrderModel = Convert.ToString(idr["OrderModel"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            WXOrderNo = Convert.ToString(idr["WXOrderNo"]),
                            //WXOrderNo = Convert.ToString(idr["OrderType"]).Equals("0") || Convert.ToString(idr["OrderType"]).Equals("1") ? "" : Convert.ToString(idr["WXOrderNo"]),
                            //PayWay = Convert.ToString(idr["PayWay"]),
                            //WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            TranHouse = Convert.ToString(idr["TranHouse"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            ContainerShowEntity = QueryUnScanProductTag(new CargoOrderEntity { OrderNo = Convert.ToString(idr["OrderNo"]) }),
                        });
                        #endregion
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询订单标签扫描出库数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoContainerShowEntity> QueryUnScanProductTag(CargoOrderEntity entity)
        {
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            string strSQL = "select ROW_NUMBER() OVER (ORDER BY a.OrderNo DESC) AS RowNumber,a.OrderNo,a.ProductID,a.ContainerCode,a.Piece,a.ActSalePrice,b.ContainerID,c.Specs,c.Figure,c.Batch,c.LoadIndex,c.SpeedLevel,d.TypeName From Tbl_Cargo_OrderGoods as a inner join Tbl_Cargo_Container as b on a.ContainerCode=b.ContainerCode and a.AreaID=b.AreaID inner join Tbl_Cargo_Product as c on a.ProductID=c.ProductID and a.HouseID=c.HouseID inner join Tbl_Cargo_ProductType as d on c.TypeID=d.TypeID where a.OrderNo=@OrderNo";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoContainerShowEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            ProductID = Convert.ToInt64(idr["ProductID"]),
                            ContainerCode = Convert.ToString(idr["ContainerCode"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                            ContainerID = Convert.ToInt32(idr["ContainerID"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            ContainerNum = Convert.ToInt32(idr["RowNumber"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            OldPiece = QueryOrderScanNum(new CargoProductTagEntity { OrderNo = Convert.ToString(idr["OrderNo"]), ContainerID = Convert.ToInt32(idr["ContainerID"]), ProductID = Convert.ToInt64(idr["ProductID"]) })
                        });
                    }
                }
            }
            return result;
        }
        public int QueryOrderScanNum(CargoProductTagEntity entity)
        {
            int result = 0;
            string strSQL = "select count(TagCode) as ScanNum From Tbl_Cargo_ProductTag where OrderNo=@OrderNo";
            if (!entity.ContainerID.Equals(0)) { strSQL += " and ContainerID=" + entity.ContainerID; }
            if (!entity.ProductID.Equals(0)) { strSQL += " and ProductID=" + entity.ProductID; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);
                using (DataTable dd = conn.ExecuteDataTable(command))
                {
                    if (dd.Rows.Count > 0) { result = Convert.ToInt32(dd.Rows[0]["ScanNum"]); }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询客户未付款订单信息
        /// </summary>
        /// <param name="clientNum"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public CargoOrderEntity QueryUnpaidOrder(string clientNum)
        {
            CargoOrderEntity result = new CargoOrderEntity();
            try
            {
                string strSQL = @"select count(*)Piece,SUM(TotalCharge)TotalCharge,(select RebateMoney from Tbl_Cargo_Client where ClientNum=@ClientNum)as RebateMoney from Tbl_Cargo_Order where PayClientNum=@ClientNum and CheckStatus!=1 and ThrowGood=21";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@ClientNum", DbType.Int32, clientNum);
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Piece = string.IsNullOrEmpty(Convert.ToString(idr["Piece"])) ? 0 : Convert.ToInt32(idr["Piece"]);
                            result.TotalCharge = string.IsNullOrEmpty(Convert.ToString(idr["TotalCharge"])) ? 0 : Convert.ToDecimal(idr["TotalCharge"]);
                            result.RebateMoney = string.IsNullOrEmpty(Convert.ToString(idr["RebateMoney"])) ? 0 : Convert.ToDecimal(idr["RebateMoney"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询用户订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderEntity> queryWxUserOrderInfo(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            try
            {
                string strSQL = @"select a.OrderID,a.OrderNo,a.LogisAwbNo,a.Dep,a.Dest,a.Piece,a.TransportFee,a.TotalCharge,a.TransitFee,a.AcceptPeople,a.AcceptCellphone,a.AcceptAddress,a.AcceptTelephone,a.ClientNum,b.LogisticName,a.CreateDate,a.OrderType,a.CheckStatus,a.WXOrderNo from Tbl_Cargo_Order as a left join Tbl_Cargo_Logistic as b on a.LogisID=b.ID where (1=1) and a.OrderModel='0'";
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID=@SaleManID"; }
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and a.AcceptPeople='" + entity.AcceptPeople + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " order by a.CreateDate desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    if (!string.IsNullOrEmpty(entity.SaleManID))
                    {
                        conn.AddInParameter(command, "@SaleManID", DbType.String, entity.SaleManID);
                    }
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                WXOrderNo = Convert.ToString(idr["WXOrderNo"]),
                                LogisticName = Convert.ToString(idr["LogisticName"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 通过订单号查询订单数据
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public List<CargoContainerShowEntity> QueryOrderByOrderNo(CargoOrderEntity entity)
        {
            CargoProductManager pro = new CargoProductManager();
            CargoHouseManager house = new CargoHouseManager();
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"select a.OrderNo,a.RelateOrderNo,a.ContainerCode,a.ProductID,a.Piece,ISNULL(a.ActSalePrice,0) as ActSalePrice,a.OutCargoID,a.ShowGoodsCode,a.ShowTypeName,b.HouseCode,b.HouseID,b.Name as HouseName,c.AreaID,c.Name as AreaName,d.ProductName,d.TypeID,e.TypeName,d.GoodsCode,d.Model,d.Specs,d.Figure,d.FlatRatio,d.Meridian,d.HubDiameter,d.LoadIndex,d.TreadWidth,d.SpeedLevel,d.SpeedMax,d.Size,d.SalePrice,d.CostPrice,d.TradePrice,d.UnitPrice,d.OP_DATE,d.Batch,d.Package,d.PackageNum,d.BatchYear,d.BatchWeek,d.BelongDepart,d.Born,d.Assort,d.Company,d.Supplier,d.ProductCode,f.ContainerID,e.ParentID,c.ParentID as AreaParentID,d.Source,d.SpecsType,a.RuleType,a.RuleID,a.RuleTitle,m.SourceName,m.SourceCode,e.TypeParentName,h.Name as FirstAreaName from Tbl_Cargo_OrderGoods as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID inner join Tbl_Cargo_Area as c on a.AreaID=c.AreaID inner join Tbl_Cargo_Product as d on a.ProductID=d.ProductID left join Tbl_Cargo_ProductSource as m on d.Source=m.Source inner join Tbl_Cargo_ProductType as e on d.TypeID=e.TypeID inner join Tbl_Cargo_Container as f on a.ContainerCode=f.ContainerCode and f.AreaID=a.AreaID inner join Tbl_Cargo_Area as g on c.ParentID=g.AreaID inner join Tbl_Cargo_Area h on g.ParentID=h.AreaID WHERE a.OrderNo=@OrderNo order by a.OP_DATE asc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            // List<CargoProductTypeEntity> proTypeList = pro.QueryProductType(new CargoProductTypeEntity { TypeID = Convert.ToInt32(idr["ParentID"]) });
                            //CargoAreaEntity cargoArea = new CargoAreaEntity();
                            //CargoAreaEntity hArea = new CargoAreaEntity();
                            ////如果父ID不为0，则查询父ID的区域名称
                            //if (!string.IsNullOrEmpty(idr["AreaParentID"].ToString()))
                            //{
                            //    if (!Convert.ToInt32(idr["AreaParentID"]).Equals(0))
                            //    {
                            //        cargoArea = house.QueryAreaByAreaID(new CargoAreaEntity { AreaID = Convert.ToInt32(idr["AreaParentID"]) });
                            //        if (!cargoArea.ParentID.Equals(0))
                            //        {
                            //            hArea = house.QueryAreaByAreaID(new CargoAreaEntity { AreaID = cargoArea.ParentID });
                            //        }
                            //    }
                            //}
                            #region 获取运单数据
                            CargoContainerShowEntity e = new CargoContainerShowEntity();
                            e.OrderNo = Convert.ToString(idr["OrderNo"]);
                            e.ContainerCode = Convert.ToString(idr["ContainerCode"]);
                            if (!string.IsNullOrEmpty(idr["ContainerID"].ToString()))
                                e.ContainerID = Convert.ToInt32(idr["ContainerID"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);//货位上件数
                            e.ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]);
                            if (!string.IsNullOrEmpty(idr["AreaID"].ToString()))
                                e.AreaID = Convert.ToInt32(idr["AreaID"]);
                            e.AreaName = Convert.ToString(idr["AreaName"]);
                            e.FirstAreaName = Convert.ToString(idr["FirstAreaName"]);
                            //if (!string.IsNullOrEmpty(idr["AreaParentID"].ToString()))
                            //{
                            //    e.FirstAreaName = hArea.Name;
                            //}
                            e.Born = Convert.ToString(idr["Born"]);
                            e.Assort = Convert.ToString(idr["Assort"]);
                            e.Company = Convert.ToString(idr["Company"]);
                            e.Supplier = Convert.ToString(idr["Supplier"]);
                            e.HouseID = Convert.ToInt32(idr["HouseID"]);
                            e.HouseName = Convert.ToString(idr["HouseName"]);
                            e.HouseCode = Convert.ToString(idr["HouseCode"]);
                            e.ProductID = Convert.ToInt64(idr["ProductID"]);
                            e.ProductName = Convert.ToString(idr["ProductName"]);
                            e.TypeID = Convert.ToInt32(idr["TypeID"]);
                            e.TypeName = Convert.ToString(idr["TypeName"]);
                            e.TypeParentID = Convert.ToInt32(idr["ParentID"]);
                            e.TypeParentName = Convert.ToString(idr["TypeParentName"]);
                            //if (proTypeList.Count > 0)
                            //{
                            //    e.TypeParentName = proTypeList[0].TypeName;
                            //}
                            e.Model = Convert.ToString(idr["Model"]);
                            e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            e.ShowGoodsCode = Convert.ToString(idr["ShowGoodsCode"]);
                            e.ShowTypeName = Convert.ToString(idr["ShowTypeName"]);
                            e.Figure = Convert.ToString(idr["Figure"]);
                            e.Specs = Convert.ToString(idr["Specs"]);
                            e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            //if (!string.IsNullOrEmpty(idr["LoadIndex"].ToString()))
                            //    e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            if (!string.IsNullOrEmpty(idr["TreadWidth"].ToString()))
                                e.TreadWidth = Convert.ToInt32(idr["TreadWidth"]);
                            if (!string.IsNullOrEmpty(idr["FlatRatio"].ToString()))
                                e.FlatRatio = Convert.ToInt32(idr["FlatRatio"]);
                            e.Meridian = Convert.ToString(idr["Meridian"]);
                            if (!string.IsNullOrEmpty(idr["HubDiameter"].ToString()))
                                e.HubDiameter = Convert.ToInt32(idr["HubDiameter"]);
                            e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            if (!string.IsNullOrEmpty(idr["SpeedMax"].ToString()))
                                e.SpeedMax = Convert.ToInt32(idr["SpeedMax"]);
                            e.Size = Convert.ToString(idr["Size"]);
                            e.OutCargoID = Convert.ToString(idr["OutCargoID"]);//出库单号
                            e.UnitPrice = Convert.ToDecimal(idr["UnitPrice"]);
                            e.CostPrice = Convert.ToDecimal(idr["CostPrice"]);
                            e.TradePrice = Convert.ToDecimal(idr["TradePrice"]);
                            e.SalePrice = Convert.ToDecimal(idr["SalePrice"]);
                            e.Source = Convert.ToString(idr["Source"]);
                            e.SourceName = Convert.ToString(idr["SourceName"]);
                            e.SourceCode = Convert.ToString(idr["SourceCode"]);
                            e.Batch = Convert.ToString(idr["Batch"]);
                            e.BatchYear = Convert.ToInt32(idr["BatchYear"]);
                            e.BatchWeek = Convert.ToInt32(idr["BatchWeek"]);
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            e.Package = Convert.ToString(idr["Package"]);
                            e.PackageNum = Convert.ToInt32(idr["PackageNum"]);
                            e.RelateOrderNo = Convert.ToString(idr["RelateOrderNo"]);
                            e.RuleType = Convert.ToString(idr["RuleType"]);
                            e.RuleID = Convert.ToString(idr["RuleID"]);
                            e.RuleTitle = Convert.ToString(idr["RuleTitle"]);
                            e.BelongDepart = Convert.ToString(idr["BelongDepart"]);
                            e.SpecsType = Convert.ToString(idr["SpecsType"]);
                            e.ProductCode = Convert.ToString(idr["ProductCode"]);
                            result.Add(e);
                            #endregion
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public List<CargoContainerShowEntity> QueryAppletOrderByOrderNo(CargoOrderEntity entity)
        {
            CargoProductManager pro = new CargoProductManager();
            CargoHouseManager house = new CargoHouseManager();
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT a.OrderNo, a.RelateOrderNo, a.ContainerCode, a.ProductID, a.Piece, ISNULL(a.ActSalePrice, 0) AS ActSalePrice, a.OutCargoID, d.ProductName, d.TypeID, e.TypeName, d.GoodsCode, d.Model, d.Specs, d.Figure, d.FlatRatio, d.Meridian, d.HubDiameter, d.LoadIndex, d.TreadWidth, d.SpeedLevel, d.SpeedMax, d.Size, d.SalePrice, d.CostPrice, d.TradePrice, d.UnitPrice, d.OP_DATE, d.Batch, d.Package, d.PackageNum, d.BatchYear, d.BatchWeek, d.BelongDepart, e.ParentID, d.Source, d.SpecsType, a.RuleType, a.RuleID, a.RuleTitle,c.Thumbnail FROM Tbl_Cargo_OrderGoods AS a INNER JOIN Tbl_Cargo_Product AS d ON a.ProductID=d.ProductID left join Tbl_Cargo_ProductSpec c on d.TypeID=c.TypeID and d.Specs=c.Specs and d.Figure=c.Figure and d.GoodsCode=c.GoodsCode LEFT JOIN Tbl_Cargo_ProductType AS e ON d.TypeID=e.TypeID WHERE a.OrderNo=@OrderNo";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            CargoContainerShowEntity e = new CargoContainerShowEntity();
                            e.OrderNo = Convert.ToString(idr["OrderNo"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);//货位上件数
                            e.ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]);
                            e.ProductID = Convert.ToInt64(idr["ProductID"]);
                            e.ProductName = Convert.ToString(idr["ProductName"]);
                            e.TypeID = Convert.ToInt32(idr["TypeID"]);
                            e.TypeName = Convert.ToString(idr["TypeName"]);
                            e.TypeParentID = Convert.ToInt32(idr["ParentID"]);
                            e.Model = Convert.ToString(idr["Model"]);
                            e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            e.Figure = Convert.ToString(idr["Figure"]);
                            e.Specs = Convert.ToString(idr["Specs"]);
                            if (!string.IsNullOrEmpty(idr["LoadIndex"].ToString()))
                                e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            if (!string.IsNullOrEmpty(idr["TreadWidth"].ToString()))
                                e.TreadWidth = Convert.ToInt32(idr["TreadWidth"]);
                            if (!string.IsNullOrEmpty(idr["FlatRatio"].ToString()))
                                e.FlatRatio = Convert.ToInt32(idr["FlatRatio"]);
                            e.Meridian = Convert.ToString(idr["Meridian"]);
                            if (!string.IsNullOrEmpty(idr["HubDiameter"].ToString()))
                                e.HubDiameter = Convert.ToInt32(idr["HubDiameter"]);
                            if (!string.IsNullOrEmpty(idr["LoadIndex"].ToString()))
                                e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            if (!string.IsNullOrEmpty(idr["SpeedMax"].ToString()))
                                e.SpeedMax = Convert.ToInt32(idr["SpeedMax"]);
                            e.Size = Convert.ToString(idr["Size"]);
                            e.OutCargoID = Convert.ToString(idr["OutCargoID"]);//出库单号
                            e.UnitPrice = Convert.ToDecimal(idr["UnitPrice"]);
                            e.CostPrice = Convert.ToDecimal(idr["CostPrice"]);
                            e.TradePrice = Convert.ToDecimal(idr["TradePrice"]);
                            e.SalePrice = Convert.ToDecimal(idr["SalePrice"]);
                            e.Source = Convert.ToString(idr["Source"]);
                            e.Batch = Convert.ToString(idr["Batch"]);
                            e.BatchYear = Convert.ToInt32(idr["BatchYear"]);
                            e.BatchWeek = Convert.ToInt32(idr["BatchWeek"]);
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            e.Package = Convert.ToString(idr["Package"]);
                            e.PackageNum = Convert.ToInt32(idr["PackageNum"]);
                            e.RelateOrderNo = Convert.ToString(idr["RelateOrderNo"]);
                            e.RuleType = Convert.ToString(idr["RuleType"]);
                            e.RuleID = Convert.ToString(idr["RuleID"]);
                            e.RuleTitle = Convert.ToString(idr["RuleTitle"]);
                            e.BelongDepart = Convert.ToString(idr["BelongDepart"]);
                            e.SpecsType = Convert.ToString(idr["SpecsType"]);
                            e.Thumbnail = Convert.ToString(idr["Thumbnail"]);
                            e.Born = Convert.ToString(idr["Born"]);
                            e.ProductCode = Convert.ToString(idr["ProductCode"]);
                            e.BelongDepart = Convert.ToString(idr["BelongDepart"]);
                            result.Add(e);
                            #endregion
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public void UpdatePrintNum(CargoOrderEntity entity)
        {
            string strSQL = @"Update Tbl_Cargo_Order set PrintNum=PrintNum+1 Where OrderNo=@OrderNo";
            entity.EnSafe();
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 新增订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddOrderInfo(CargoOrderEntity entity)
        {
            entity.EnSafe();
            CargoClientManager client = new CargoClientManager();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_Order(OrderNo,OrderNum,HAwbNo,LogisAwbNo,LogisID,Dep,Dest,Piece,Weight,Volume,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,TrafficType,DeliveryType,AcceptUnit,AcceptPeople,AcceptTelephone,AcceptCellphone,AcceptAddress,CreateAwb,CreateDate,OP_ID,OP_DATE,AwbStatus,Remark,SaleManID,SaleManName,SaleCellPhone,HouseID,CreateAwbID,OrderType,OrderModel,ClientNum,ThrowGood,TranHouse,OutHouseName,ModifyPriceStatus,PayClientNum,PayClientName,BelongHouse,IsPrintPrice,PostponeShip,DeliverySettlement,LineID,LineName,SuppClientNum,WXOrderNo,CollectMoney,OpenOrderNo,OpenOrderSource,BusinessID,OverDueFee,CouponType,ShareHouseID,ShareHouseName,OrignHouseID,OrignHouseName,CheckStatus,BateAmount,OnlinePaidAmount";
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheck))
            {
                strSQL += ",FinanceSecondCheck";
            }
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheckName))
            {
                strSQL += ",FinanceSecondCheckName";
            }
            if (entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                strSQL += ",FinanceSecondCheckDate";
            }
            if (!string.IsNullOrEmpty(entity.ShopCode))
            {
                strSQL += ",ShopCode";
            }
            if (entity.OutCargoTime.ToString("yyyy-MM-dd") != "0001-01-01" && entity.OutCargoTime.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                strSQL += ",OutCargoTime";
            }
            strSQL += ") VALUES (@OrderNo,@OrderNum,@HAwbNo,@LogisAwbNo,@LogisID,@Dep,@Dest,@Piece,@Weight,@Volume,@InsuranceFee,@TransitFee,@TransportFee,@DeliveryFee,@OtherFee,@TotalCharge,@Rebate,@CheckOutType,@TrafficType,@DeliveryType,@AcceptUnit,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@AcceptAddress,@CreateAwb,@CreateDate,@OP_ID,@OP_DATE,@AwbStatus,@Remark,@SaleManID,@SaleManName,@SaleCellPhone,@HouseID,@CreateAwbID,@OrderType,@OrderModel,@ClientNum,@ThrowGood,@TranHouse,@OutHouseName,@ModifyPriceStatus,@PayClientNum,@PayClientName,@BelongHouse,@IsPrintPrice,@PostponeShip,@DeliverySettlement,@LineID,@LineName,@SuppClientNum,@WXOrderNo,@CollectMoney,@OpenOrderNo,@OpenOrderSource,@BusinessID,@OverDueFee,@CouponType,@ShareHouseID,@ShareHouseName,@OrignHouseID,@OrignHouseName,@CheckStatus,@BateAmount,@OnlinePaidAmount";
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheck))
            {
                strSQL += ",@FinanceSecondCheck";
            }
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheckName))
            {
                strSQL += ",@FinanceSecondCheckName";
            }
            if (entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                strSQL += ",@FinanceSecondCheckDate";
            }

            if (!string.IsNullOrEmpty(entity.ShopCode))
            {
                strSQL += ",@ShopCode";
            }
            if (entity.OutCargoTime.ToString("yyyy-MM-dd") != "0001-01-01" && entity.OutCargoTime.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                strSQL += ",@OutCargoTime";
            }
            strSQL += ") SELECT SCOPE_IDENTITY()";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OpenOrderNo", DbType.String, entity.OpenOrderNo);
                    conn.AddInParameter(cmd, "@OpenOrderSource", DbType.String, entity.OpenOrderSource);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                    conn.AddInParameter(cmd, "@HAwbNo", DbType.String, entity.HAwbNo);
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@Weight", DbType.Decimal, entity.Weight);
                    conn.AddInParameter(cmd, "@Volume", DbType.Decimal, entity.Volume);
                    conn.AddInParameter(cmd, "@InsuranceFee", DbType.Decimal, entity.InsuranceFee);
                    conn.AddInParameter(cmd, "@CollectMoney", DbType.Decimal, entity.CollectMoney);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    conn.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@Rebate", DbType.Decimal, entity.Rebate);
                    conn.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    conn.AddInParameter(cmd, "@TrafficType", DbType.String, entity.TrafficType);
                    conn.AddInParameter(cmd, "@DeliveryType", DbType.String, entity.DeliveryType);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@CreateAwbID", DbType.String, entity.CreateAwbID);
                    conn.AddInParameter(cmd, "@CreateAwb", DbType.String, entity.CreateAwb);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, entity.CreateDate);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@AwbStatus", DbType.String, entity.AwbStatus);
                    conn.AddInParameter(cmd, "@CheckStatus", DbType.String, entity.CheckStatus);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                    conn.AddInParameter(cmd, "@SaleCellPhone", DbType.String, entity.SaleCellPhone);
                    conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                    conn.AddInParameter(cmd, "@OrderModel", DbType.String, entity.OrderModel);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@ThrowGood", DbType.String, entity.ThrowGood);
                    conn.AddInParameter(cmd, "@TranHouse", DbType.String, entity.TranHouse);
                    conn.AddInParameter(cmd, "@OutHouseName", DbType.String, entity.OutHouseName);
                    conn.AddInParameter(cmd, "@ModifyPriceStatus", DbType.String, entity.ModifyPriceStatus);
                    conn.AddInParameter(cmd, "@PayClientNum", DbType.Int32, entity.PayClientNum);
                    conn.AddInParameter(cmd, "@PayClientName", DbType.String, entity.PayClientName);
                    conn.AddInParameter(cmd, "@BelongHouse", DbType.String, entity.BelongHouse);
                    conn.AddInParameter(cmd, "@IsPrintPrice", DbType.String, entity.IsPrintPrice);
                    conn.AddInParameter(cmd, "@PostponeShip", DbType.String, entity.PostponeShip);
                    conn.AddInParameter(cmd, "@DeliverySettlement", DbType.String, entity.DeliverySettlement);
                    conn.AddInParameter(cmd, "@LineID", DbType.Int32, entity.LineID);
                    conn.AddInParameter(cmd, "@LineName", DbType.String, entity.LineName);
                    conn.AddInParameter(cmd, "@WXOrderNo", DbType.String, entity.WXOrderNo);
                    conn.AddInParameter(cmd, "@SuppClientNum", DbType.Int32, entity.SuppClientNum);
                    conn.AddInParameter(cmd, "@BusinessID", DbType.Int32, Convert.ToInt32(entity.BusinessID));
                    conn.AddInParameter(cmd, "@BateAmount", DbType.Decimal, Convert.ToDecimal(entity.BateAmount));
                    conn.AddInParameter(cmd, "@OnlinePaidAmount", DbType.Decimal, Convert.ToDecimal(entity.OnlinePaidAmount));
                    if (!string.IsNullOrEmpty(entity.FinanceSecondCheck))
                    {
                        conn.AddInParameter(cmd, "@FinanceSecondCheck", DbType.String, entity.FinanceSecondCheck);
                    }
                    if (!string.IsNullOrEmpty(entity.FinanceSecondCheckName))
                    {
                        conn.AddInParameter(cmd, "@FinanceSecondCheckName", DbType.String, entity.FinanceSecondCheckName);
                    }
                    if (entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "1900-01-01")
                    {
                        conn.AddInParameter(cmd, "@FinanceSecondCheckDate", DbType.DateTime, entity.FinanceSecondCheckDate);
                    }
                    if (!string.IsNullOrEmpty(entity.ShopCode))
                    {
                        conn.AddInParameter(cmd, "@ShopCode", DbType.String, entity.ShopCode);
                    }
                    if (entity.OutCargoTime.ToString("yyyy-MM-dd") != "0001-01-01" && entity.OutCargoTime.ToString("yyyy-MM-dd") != "1900-01-01")
                    {
                        conn.AddInParameter(cmd, "@OutCargoTime", DbType.DateTime, entity.OutCargoTime);
                    }
                    conn.AddInParameter(cmd, "@OverDueFee", DbType.Decimal, entity.OverDueFee);
                    conn.AddInParameter(cmd, "@CouponType", DbType.String, entity.CouponType);
                    conn.AddInParameter(cmd, "@ShareHouseID", DbType.Int32, entity.ShareHouseID);
                    conn.AddInParameter(cmd, "@ShareHouseName", DbType.String, entity.ShareHouseName);
                    conn.AddInParameter(cmd, "@OrignHouseID", DbType.Int32, entity.OrignHouseID);
                    conn.AddInParameter(cmd, "@OrignHouseName", DbType.String, entity.OrignHouseName);

                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                #endregion

                //新增产品与订单关联数据
                AddOrderGoodsInfo(entity.goodsList);

                #region 判断客户数据是否存在
                //判断客户数据是否存在，如果不存在就添加进去，存在就过
                //CargoClientEntity ce = new CargoClientEntity();
                //ce.ClientShortName = ce.ClientName = entity.AcceptUnit;
                //ce.Boss = entity.AcceptPeople;
                //ce.Telephone = entity.AcceptTelephone;
                //ce.Cellphone = entity.AcceptCellphone;
                //ce.ClientNum = entity.ClientNum;
                //ce.Address = entity.AcceptAddress;
                //ce.ClientType = "0";
                //ce.DelFlag = "0";
                //ce.HouseID = entity.HouseID;
                //ce.UserID = entity.SaleManID;
                //ce.UserName = entity.SaleManName;
                //if (!client.IsExistCargoClient(ce))
                //{
                //    client.AddCargoClient(ce);
                //}
                //client.UpdateCargoClientUserID(ce);
                //if (entity.BelongHouse == "0" || entity.BelongHouse == "")
                //{
                //    CargoClientAcceptAddressEntity ca = new CargoClientAcceptAddressEntity();
                //    ca.ClientNum = entity.ClientNum;
                //    //ca.AcceptProvince=entity
                //    //ca.AcceptCity=entity
                //    //ca.AcceptCountry=entity
                //    ca.AcceptCompany = entity.AcceptUnit;
                //    ca.AcceptAddress = entity.AcceptAddress;
                //    ca.AcceptPeople = entity.AcceptPeople;
                //    ca.AcceptTelephone = entity.AcceptTelephone;
                //    ca.AcceptCellphone = entity.AcceptCellphone;
                //    ca.HouseID = entity.HouseID;

                //    if (!client.IsExistAcceptAddress(ca))
                //    {
                //        CargoClientEntity clientEntity = client.QueryCargoClient(entity.ClientNum);
                //        ca.ClientID = clientEntity.ClientID;

                //        client.AddAcceptAddress(ca);
                //    }
                //}
                #endregion

                #region 新增订单已下单状态
                CargoOrderStatusEntity status = new CargoOrderStatusEntity();
                status.OrderID = did;
                status.OrderNo = entity.OrderNo;
                status.OrderStatus = "0";
                status.OP_ID = entity.OP_ID;
                status.OP_Name = entity.CreateAwb;
                status.OP_DATE = DateTime.Now;
                status.Longitude = entity.Longitude;
                status.Latitude = entity.Latitude;
                AddOrderStaus(status);
                #endregion

                #region 修改优惠券使用状态
                CargoWeiXinManager wx = new CargoWeiXinManager();
                if (!entity.CouponID.Equals(0))
                {
                    wx.SetCouponUsed(new WXCouponEntity { ID = entity.CouponID, UseStatus = "1", UseDate = DateTime.Now, OrderID = did, OrderType = "0" });
                }
                if (entity.CouponIDList != null)
                {
                    foreach (var coupon in entity.CouponIDList)
                    {
                        wx.SetCouponUsed(new WXCouponEntity { ID = coupon, UseStatus = "1", UseDate = DateTime.Now, OrderID = did, OrderType = "0" });
                    }
                }

                #endregion
                #region 订货单
                if (entity.ThrowGood == "21")
                {
                    if (entity.UserRulePiece > 0)
                    {
                        UpdateClientDiscountPiece(new CargoClientEntity { ClientNum = entity.PayClientNum, LimitMoney = Math.Round(Convert.ToDecimal(entity.Piece - entity.UserRulePiece) * 10 / 85, 3) - entity.UserRulePiece });
                    }
                    else
                    {
                        UpdateClientDiscountPiece(new CargoClientEntity { ClientNum = entity.PayClientNum, LimitMoney = Math.Round(Convert.ToDecimal(entity.Piece) * 10 / 85, 3) });
                    }
                    if (entity.InsuranceFee > 0)
                    {
                        CargoClientEntity clientEntity = client.QueryCargoClient(entity.ClientNum, 0);
                        if (!clientEntity.ClientID.Equals(0))
                        {
                            client.UpdateCargoClientRebateMoney(new CargoClientEntity { ClientID = clientEntity.ClientID, RebateMoney = entity.InsuranceFee * -1 });
                            if (entity.TotalCharge > 0)
                            {
                                client.AddClientPreRecord(new CargoClientPreRecordEntity { ClientID = clientEntity.ClientID, ExID = entity.OrderNo, Money = entity.InsuranceFee, RecordType = "1", OP_ID = entity.SaleManID, OperaType = "1", RebateType = "0", TryeClientCode = clientEntity.TryeClientCode, TypeID = 0, Remark = "" });
                            }
                        }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }

        public void AddOrderInfo11(CargoOrderEntity entity)
        {
            CargoClientManager client = new CargoClientManager();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_Order(OrderNo,OrderNum,HAwbNo,LogisAwbNo,LogisID,Dep,Dest,Piece,Weight,Volume,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,TrafficType,DeliveryType,AcceptUnit,AcceptPeople,AcceptTelephone,AcceptCellphone,AcceptAddress,CreateAwb,CreateDate,OP_ID,OP_DATE,AwbStatus,Remark,SaleManID,SaleManName,SaleCellPhone,HouseID,CreateAwbID,OrderType,OrderModel,ClientNum,ThrowGood,TranHouse,OutHouseName,ModifyPriceStatus,PayClientNum,PayClientName,BelongHouse,IsPrintPrice,PostponeShip,DeliverySettlement,LineID,LineName,SuppClientNum,WXOrderNo,CollectMoney,OpenOrderNo,OpenOrderSource,BusinessID,OverDueFee,CouponType";
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheck))
            {
                strSQL += ",FinanceSecondCheck";
            }
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheckName))
            {
                strSQL += ",FinanceSecondCheckName";
            }
            if (entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                strSQL += ",FinanceSecondCheckDate";
            }
            if (!string.IsNullOrEmpty(entity.ShopCode))
            {
                strSQL += ",ShopCode";
            }
            strSQL += ") VALUES ('" + entity.OrderNo + "',@OrderNum,@HAwbNo,@LogisAwbNo,@LogisID,@Dep,@Dest,@Piece,@Weight,@Volume,@InsuranceFee,@TransitFee,@TransportFee,@DeliveryFee,@OtherFee,@TotalCharge,@Rebate,@CheckOutType,@TrafficType,@DeliveryType,@AcceptUnit,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@AcceptAddress,@CreateAwb,@CreateDate,@OP_ID,@OP_DATE,@AwbStatus,@Remark,@SaleManID,@SaleManName,@SaleCellPhone,@HouseID,@CreateAwbID,@OrderType,@OrderModel,@ClientNum,@ThrowGood,@TranHouse,@OutHouseName,@ModifyPriceStatus,@PayClientNum,@PayClientName,@BelongHouse,@IsPrintPrice,@PostponeShip,@DeliverySettlement,@LineID,@LineName,@SuppClientNum,@WXOrderNo,@CollectMoney,@OpenOrderNo,@OpenOrderSource,@BusinessID,@OverDueFee,@CouponType";
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheck))
            {
                strSQL += ",@FinanceSecondCheck";
            }
            if (!string.IsNullOrEmpty(entity.FinanceSecondCheckName))
            {
                strSQL += ",@FinanceSecondCheckName";
            }
            if (entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                strSQL += ",@FinanceSecondCheckDate";
            }
            if (!string.IsNullOrEmpty(entity.ShopCode))
            {
                strSQL += ",@ShopCode";
            }
            strSQL += ") SELECT @@IDENTITY";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OpenOrderNo", DbType.String, entity.OpenOrderNo);
                    conn.AddInParameter(cmd, "@OpenOrderSource", DbType.String, entity.OpenOrderSource);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                    conn.AddInParameter(cmd, "@HAwbNo", DbType.String, entity.HAwbNo);
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@Weight", DbType.Decimal, entity.Weight);
                    conn.AddInParameter(cmd, "@Volume", DbType.Decimal, entity.Volume);
                    conn.AddInParameter(cmd, "@InsuranceFee", DbType.Decimal, entity.InsuranceFee);
                    conn.AddInParameter(cmd, "@CollectMoney", DbType.Decimal, entity.CollectMoney);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    conn.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@Rebate", DbType.Decimal, entity.Rebate);
                    conn.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    conn.AddInParameter(cmd, "@TrafficType", DbType.String, entity.TrafficType);
                    conn.AddInParameter(cmd, "@DeliveryType", DbType.String, entity.DeliveryType);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@CreateAwbID", DbType.String, entity.CreateAwbID);
                    conn.AddInParameter(cmd, "@CreateAwb", DbType.String, entity.CreateAwb);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, entity.CreateDate);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@AwbStatus", DbType.String, entity.AwbStatus);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                    conn.AddInParameter(cmd, "@SaleCellPhone", DbType.String, entity.SaleCellPhone);
                    conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                    conn.AddInParameter(cmd, "@OrderModel", DbType.String, entity.OrderModel);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@ThrowGood", DbType.String, entity.ThrowGood);
                    conn.AddInParameter(cmd, "@TranHouse", DbType.String, entity.TranHouse);
                    conn.AddInParameter(cmd, "@OutHouseName", DbType.String, entity.OutHouseName);
                    conn.AddInParameter(cmd, "@ModifyPriceStatus", DbType.String, entity.ModifyPriceStatus);
                    conn.AddInParameter(cmd, "@PayClientNum", DbType.Int32, entity.PayClientNum);
                    conn.AddInParameter(cmd, "@PayClientName", DbType.String, entity.PayClientName);
                    conn.AddInParameter(cmd, "@BelongHouse", DbType.String, entity.BelongHouse);
                    conn.AddInParameter(cmd, "@IsPrintPrice", DbType.String, entity.IsPrintPrice);
                    conn.AddInParameter(cmd, "@PostponeShip", DbType.String, entity.PostponeShip);
                    conn.AddInParameter(cmd, "@DeliverySettlement", DbType.String, entity.DeliverySettlement);
                    conn.AddInParameter(cmd, "@LineID", DbType.Int32, entity.LineID);
                    conn.AddInParameter(cmd, "@LineName", DbType.String, entity.LineName);
                    conn.AddInParameter(cmd, "@WXOrderNo", DbType.String, entity.WXOrderNo);
                    conn.AddInParameter(cmd, "@SuppClientNum", DbType.Int32, entity.SuppClientNum);
                    conn.AddInParameter(cmd, "@BusinessID", DbType.Int32, Convert.ToInt32(entity.BusinessID));
                    if (!string.IsNullOrEmpty(entity.FinanceSecondCheck))
                    {
                        conn.AddInParameter(cmd, "@FinanceSecondCheck", DbType.String, entity.FinanceSecondCheck);
                    }
                    if (!string.IsNullOrEmpty(entity.FinanceSecondCheckName))
                    {
                        conn.AddInParameter(cmd, "@FinanceSecondCheckName", DbType.String, entity.FinanceSecondCheckName);
                    }
                    if (entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.FinanceSecondCheckDate.ToString("yyyy-MM-dd") != "1900-01-01")
                    {
                        conn.AddInParameter(cmd, "@FinanceSecondCheckDate", DbType.DateTime, entity.FinanceSecondCheckDate);
                    }
                    if (!string.IsNullOrEmpty(entity.ShopCode))
                    {
                        conn.AddInParameter(cmd, "@ShopCode", DbType.String, entity.ShopCode);
                    }
                    conn.AddInParameter(cmd, "@OverDueFee", DbType.Decimal, entity.OverDueFee);
                    conn.AddInParameter(cmd, "@CouponType", DbType.String, entity.CouponType);

                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                #endregion

                //新增产品与订单关联数据
                AddOrderGoodsInfo(entity.goodsList);

                #region 判断客户数据是否存在
                //判断客户数据是否存在，如果不存在就添加进去，存在就过
                //CargoClientEntity ce = new CargoClientEntity();
                //ce.ClientShortName = ce.ClientName = entity.AcceptUnit;
                //ce.Boss = entity.AcceptPeople;
                //ce.Telephone = entity.AcceptTelephone;
                //ce.Cellphone = entity.AcceptCellphone;
                //ce.ClientNum = entity.ClientNum;
                //ce.Address = entity.AcceptAddress;
                //ce.ClientType = "0";
                //ce.DelFlag = "0";
                //ce.HouseID = entity.HouseID;
                //ce.UserID = entity.SaleManID;
                //ce.UserName = entity.SaleManName;
                //if (!client.IsExistCargoClient(ce))
                //{
                //    client.AddCargoClient(ce);
                //}
                //client.UpdateCargoClientUserID(ce);
                //if (entity.BelongHouse == "0" || entity.BelongHouse == "")
                //{
                //    CargoClientAcceptAddressEntity ca = new CargoClientAcceptAddressEntity();
                //    ca.ClientNum = entity.ClientNum;
                //    //ca.AcceptProvince=entity
                //    //ca.AcceptCity=entity
                //    //ca.AcceptCountry=entity
                //    ca.AcceptCompany = entity.AcceptUnit;
                //    ca.AcceptAddress = entity.AcceptAddress;
                //    ca.AcceptPeople = entity.AcceptPeople;
                //    ca.AcceptTelephone = entity.AcceptTelephone;
                //    ca.AcceptCellphone = entity.AcceptCellphone;
                //    ca.HouseID = entity.HouseID;

                //    if (!client.IsExistAcceptAddress(ca))
                //    {
                //        CargoClientEntity clientEntity = client.QueryCargoClient(entity.ClientNum);
                //        ca.ClientID = clientEntity.ClientID;

                //        client.AddAcceptAddress(ca);
                //    }
                //}
                #endregion

                #region 新增订单已下单状态
                CargoOrderStatusEntity status = new CargoOrderStatusEntity();
                status.OrderID = did;
                status.OrderNo = entity.OrderNo;
                status.OrderStatus = "0";
                status.OP_ID = entity.OP_ID;
                status.OP_Name = entity.CreateAwb;
                status.OP_DATE = DateTime.Now;
                status.Longitude = entity.Longitude;
                status.Latitude = entity.Latitude;
                AddOrderStaus(status);
                #endregion

                #region 修改优惠券使用状态
                CargoWeiXinManager wx = new CargoWeiXinManager();
                if (!entity.CouponID.Equals(0))
                {
                    wx.SetCouponUsed(new WXCouponEntity { ID = entity.CouponID, UseStatus = "1", UseDate = DateTime.Now, OrderID = did, OrderType = "0" });
                }
                if (entity.CouponIDList != null)
                {
                    foreach (var coupon in entity.CouponIDList)
                    {
                        wx.SetCouponUsed(new WXCouponEntity { ID = coupon, UseStatus = "1", UseDate = DateTime.Now, OrderID = did, OrderType = "0" });
                    }
                }

                #endregion
                #region 订货单
                if (entity.ThrowGood == "21")
                {
                    if (entity.UserRulePiece > 0)
                    {
                        UpdateClientDiscountPiece(new CargoClientEntity { ClientNum = entity.PayClientNum, LimitMoney = Math.Round(Convert.ToDecimal(entity.Piece - entity.UserRulePiece) * 10 / 85, 3) - entity.UserRulePiece });
                    }
                    else
                    {
                        UpdateClientDiscountPiece(new CargoClientEntity { ClientNum = entity.PayClientNum, LimitMoney = Math.Round(Convert.ToDecimal(entity.Piece) * 10 / 85, 3) });
                    }
                    if (entity.InsuranceFee > 0)
                    {
                        CargoClientEntity clientEntity = client.QueryCargoClient(entity.ClientNum, 0);
                        if (!clientEntity.ClientID.Equals(0))
                        {
                            client.UpdateCargoClientRebateMoney(new CargoClientEntity { ClientID = clientEntity.ClientID, RebateMoney = entity.InsuranceFee * -1 });
                            if (entity.TotalCharge > 0)
                            {
                                client.AddClientPreRecord(new CargoClientPreRecordEntity { ClientID = clientEntity.ClientID, ExID = entity.OrderNo, Money = entity.InsuranceFee, RecordType = "1", OP_ID = entity.SaleManID, OperaType = "1", RebateType = "0", TryeClientCode = clientEntity.TryeClientCode, TypeID = 0, Remark = "" });
                            }
                        }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }

        /// <summary>
        /// 修改客户优惠额度数量
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ApplicationException"></exception>
        public void UpdateClientDiscountPiece(CargoClientEntity entity)
        {
            try
            {
                string strSQL = @"UPDATE Tbl_Cargo_Client SET LimitMoney=LimitMoney+@LimitMoney";
                strSQL += " Where ClientNum=@ClientNum ";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ClientNum", DbType.String, entity.ClientNum);
                    conn.AddInParameter(cmd, "@LimitMoney", DbType.String, entity.LimitMoney);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateOrderInfo(CargoOrderEntity entity)
        {
            CargoClientManager client = new CargoClientManager();
            string strSQL = @"UPDATE Tbl_Cargo_Order set HAwbNo=@HAwbNo,LogisID=@LogisID,Dep=@Dep,Dest=@Dest,Piece=@Piece,Weight=@Weight,Volume=@Volume,InsuranceFee=@InsuranceFee,TransitFee=@TransitFee,TransportFee=@TransportFee,DeliveryFee=@DeliveryFee,OtherFee=@OtherFee,TotalCharge=@TotalCharge,Rebate=@Rebate,CheckOutType=@CheckOutType,TrafficType=@TrafficType,DeliveryType=@DeliveryType,AcceptUnit=@AcceptUnit,AcceptPeople=@AcceptPeople,AcceptTelephone=@AcceptTelephone,AcceptCellphone=@AcceptCellphone,AcceptAddress=@AcceptAddress,OP_ID=@OP_ID,OP_DATE=@OP_DATE,Remark=@Remark,SaleManID=@SaleManID,SaleManName=@SaleManName,SaleCellPhone=@SaleCellPhone,HouseID=@HouseID,ClientNum=@ClientNum,ThrowGood=@ThrowGood,TranHouse=@TranHouse,PayClientName=@PayClientName,PayClientNum=@PayClientNum,IsPrintPrice=@IsPrintPrice,DeliverySettlement=@DeliverySettlement,CollectMoney=@CollectMoney,OpenOrderNo=@OpenOrderNo,BateAmount=@BateAmount Where OrderID=@OrderID and OrderNo=@OrderNo";
            try
            {
                //,LogisAwbNo=@LogisAwbNo
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@HAwbNo", DbType.String, entity.HAwbNo);
                    //conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@Weight", DbType.Decimal, entity.Weight);
                    conn.AddInParameter(cmd, "@Volume", DbType.Decimal, entity.Volume);
                    conn.AddInParameter(cmd, "@CollectMoney", DbType.Decimal, entity.CollectMoney);
                    conn.AddInParameter(cmd, "@InsuranceFee", DbType.Decimal, entity.InsuranceFee);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    conn.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@Rebate", DbType.Decimal, entity.Rebate);
                    conn.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    conn.AddInParameter(cmd, "@TrafficType", DbType.String, entity.TrafficType);
                    conn.AddInParameter(cmd, "@DeliveryType", DbType.String, entity.DeliveryType);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    //conn.AddInParameter(cmd, "@AwbStatus", DbType.String, entity.AwbStatus);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                    conn.AddInParameter(cmd, "@SaleCellPhone", DbType.String, entity.SaleCellPhone);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@ThrowGood", DbType.String, entity.ThrowGood);
                    conn.AddInParameter(cmd, "@TranHouse", DbType.String, entity.TranHouse);
                    conn.AddInParameter(cmd, "@PayClientName", DbType.String, entity.PayClientName);
                    conn.AddInParameter(cmd, "@PayClientNum", DbType.String, entity.PayClientNum);
                    conn.AddInParameter(cmd, "@IsPrintPrice", DbType.Int32, entity.IsPrintPrice);
                    conn.AddInParameter(cmd, "@DeliverySettlement", DbType.String, entity.DeliverySettlement);
                    conn.AddInParameter(cmd, "@OpenOrderNo", DbType.String, entity.OpenOrderNo);
                    conn.AddInParameter(cmd, "@BateAmount", DbType.Decimal, entity.BateAmount);
                    conn.ExecuteNonQuery(cmd);
                }
                #endregion

                //先删除订单与产品关联数据
                //DeleteOrderGoodsInfo(new CargoOrderGoodsEntity { OrderNo = entity.OrderNo });
                //新增产品与订单关联数据
                //AddOrderGoodsInfo(entity.goodsList);

                #region 判断客户数据是否存在
                ////判断客户数据是否存在，如果不存在就添加进去，存在就过
                //CargoClientEntity ce = new CargoClientEntity();
                //ce.ClientShortName = ce.ClientName = entity.AcceptUnit;
                //ce.Boss = entity.AcceptPeople;
                //ce.Telephone = entity.AcceptTelephone;
                //ce.Cellphone = entity.AcceptCellphone;
                //ce.ClientNum = entity.ClientNum;
                //ce.ClientType = "0";
                //ce.DelFlag = "0";
                //ce.HouseID = entity.HouseID;
                //if (!client.IsExistCargoClient(ce))
                //{
                //    client.AddCargoClient(ce);
                //}
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 删除订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteOrderInfo(CargoOrderEntity entity)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_Order Where OrderID=@OrderID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 通过订单号删除订单与产品关联表数据
        /// </summary>
        /// <param name="good"></param>
        public void DeleteOrderGoodsInfo(CargoOrderGoodsEntity good)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_OrderGoods Where OrderNo=@OrderNo";
            if (!good.ProductID.Equals(0)) { strSQL += " and ProductID=@ProductID"; }
            if (!string.IsNullOrEmpty(good.ContainerCode)) { strSQL += " and ContainerCode=@ContainerCode"; }
            if (!string.IsNullOrEmpty(good.OutCargoID)) { strSQL += " and OutCargoID=@OutCargoID"; }
            if (!good.Piece.Equals(0)) { strSQL += " and Piece=@Piece"; }

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, good.OrderNo.ToUpper());
                if (!good.ProductID.Equals(0))
                {
                    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, good.ProductID);
                }
                if (!string.IsNullOrEmpty(good.ContainerCode))
                {
                    conn.AddInParameter(cmd, "@ContainerCode", DbType.String, good.ContainerCode.ToUpper());
                }
                if (!string.IsNullOrEmpty(good.OutCargoID))
                {
                    conn.AddInParameter(cmd, "@OutCargoID", DbType.String, good.OutCargoID.ToUpper());
                }
                if (!good.Piece.Equals(0))
                {
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, good.Piece);
                }
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改订单产品关联表件数
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateOrderGoodPieceByID(CargoContainerShowEntity entity)
        {
            try
            {
                //string strSQL = @"UPDATE Tbl_Cargo_OrderGoods SET ";
                //if (entity.IsAdd) { strSQL += " Piece=Piece+@Piece"; }
                //else { strSQL += " Piece=Piece-@Piece"; }
                //strSQL += " Where OrderNo=@OrderNo and OutCargoID=@OutCargoID and ContainerCode=@ContainerCode and ProductID=@ProductID";
                //using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                //{
                //    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                //    conn.AddInParameter(cmd, "@OutCargoID", DbType.String, entity.OutCargoID);
                //    conn.AddInParameter(cmd, "@ContainerCode", DbType.String, entity.ContainerCode);
                //    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                //    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                //    conn.ExecuteNonQuery(cmd);
                //}
                string strSQL = "";
                if (entity.IsAdd)
                {
                    strSQL = "UPDATE Tbl_Cargo_OrderGoods SET Piece=Piece+@Piece Where OrderNo=@OrderNo and OutCargoID=@OutCargoID and ContainerCode=@ContainerCode and ProductID=@ProductID";
                }
                else
                {
                    strSQL = "if((select Piece-@Piece from Tbl_Cargo_OrderGoods where OrderNo=@OrderNo and OutCargoID=@OutCargoID and ContainerCode=@ContainerCode and ProductID=@ProductID)<=0) begin delete from Tbl_Cargo_OrderGoods where OrderNo=@OrderNo and OutCargoID=@OutCargoID and ContainerCode=@ContainerCode and ProductID=@ProductID end else begin UPDATE Tbl_Cargo_OrderGoods SET Piece=Piece-@Piece Where OrderNo=@OrderNo and OutCargoID=@OutCargoID and ContainerCode=@ContainerCode and ProductID=@ProductID end";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@OutCargoID", DbType.String, entity.OutCargoID);
                    conn.AddInParameter(cmd, "@ContainerCode", DbType.String, entity.ContainerCode);
                    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改订单中产品价格
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateOrderSalePrice(CargoContainerShowEntity entity)
        {
            try
            {
                string strSQL = @"UPDATE Tbl_Cargo_OrderGoods SET ActSalePrice=@ActSalePrice";
                if (entity.SupplySalePrice > 0)
                {
                    strSQL += ",SupplySalePrice=@SupplySalePrice";
                }
                strSQL += " Where OrderNo=@OrderNo and OutCargoID=@OutCargoID and ContainerCode=@ContainerCode and ProductID=@ProductID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@OutCargoID", DbType.String, entity.OutCargoID);
                    conn.AddInParameter(cmd, "@ContainerCode", DbType.String, entity.ContainerCode);
                    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                    conn.AddInParameter(cmd, "@ActSalePrice", DbType.Decimal, entity.ActSalePrice);
                    if (entity.SupplySalePrice > 0)
                    {
                        conn.AddInParameter(cmd, "@SupplySalePrice", DbType.Decimal, entity.SupplySalePrice);
                    }
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 新增订单与产品关联表数据
        /// </summary>
        /// <param name="goods"></param>
        public void AddOrderGoodsInfo(List<CargoOrderGoodsEntity> goods)
        {
            try
            {
                foreach (var it in goods)
                {
                    it.EnSafe();
                    string strSQL = @"INSERT INTO Tbl_Cargo_OrderGoods(OrderNo,ProductID,HouseID,AreaID,ContainerCode,Piece,ActSalePrice,OP_ID,OutCargoID,RelateOrderNo,RuleType,RuleID,RuleTitle,SuitClientNum,SupplySalePrice,ShowGoodsCode,OverDueFee,OverDayNum,ShowProductCode,ShowTypeName) VALUES  (@OrderNo,@ProductID,@HouseID,@AreaID,@ContainerCode,@Piece,@ActSalePrice,@OP_ID,@OutCargoID,@RelateOrderNo,@RuleType,@RuleID,@RuleTitle,@SuitClientNum,@SupplySalePrice,@ShowGoodsCode,@OverDueFee,@OverDayNum,@ShowProductCode,@ShowTypeName)";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@OrderNo", DbType.String, it.OrderNo.ToUpper());
                        conn.AddInParameter(cmd, "@ProductID", DbType.Int64, it.ProductID);
                        conn.AddInParameter(cmd, "@HouseID", DbType.Int32, it.HouseID);
                        conn.AddInParameter(cmd, "@AreaID", DbType.Int32, it.AreaID);
                        conn.AddInParameter(cmd, "@ContainerCode", DbType.String, it.ContainerCode);
                        conn.AddInParameter(cmd, "@Piece", DbType.Int32, it.Piece);
                        conn.AddInParameter(cmd, "@ActSalePrice", DbType.Decimal, it.ActSalePrice);
                        conn.AddInParameter(cmd, "@OP_ID", DbType.String, it.OP_ID);
                        conn.AddInParameter(cmd, "@OutCargoID", DbType.String, it.OutCargoID);
                        conn.AddInParameter(cmd, "@RelateOrderNo", DbType.String, it.RelateOrderNo);
                        conn.AddInParameter(cmd, "@RuleType", DbType.String, it.RuleType);
                        conn.AddInParameter(cmd, "@RuleID", DbType.String, it.RuleID);
                        conn.AddInParameter(cmd, "@RuleTitle", DbType.String, it.RuleTitle);
                        conn.AddInParameter(cmd, "@SuitClientNum", DbType.String, it.SuitClientNum);
                        conn.AddInParameter(cmd, "@SupplySalePrice", DbType.Decimal, it.SupplySalePrice);
                        conn.AddInParameter(cmd, "@ShowGoodsCode", DbType.String, it.ShowGoodsCode);
                        conn.AddInParameter(cmd, "@OverDayNum", DbType.Int32, it.OverDayNum);
                        conn.AddInParameter(cmd, "@OverDueFee", DbType.Decimal, it.OverDueFee);
                        conn.AddInParameter(cmd, "@ShowProductCode", DbType.String, it.ShowProductCode);
                        conn.AddInParameter(cmd, "@ShowTypeName", DbType.String, it.ShowTypeName);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 根据订单号查询订单与产品关联表数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderGoodsEntity> QueryOrderGoodsInfo(CargoOrderGoodsEntity entity)
        {
            List<CargoOrderGoodsEntity> result = new List<CargoOrderGoodsEntity>();
            try
            {
                string strSQL = @"select a.*,b.Specs,b.Figure,b.Model,b.GoodsCode,b.Born,b.Batch,b.TypeID,b.SpeedLevel,b.LoadIndex,c.ContainerID,b.ProductName,b.ProductCode,d.TypeName,ar.Name as AreaName from Tbl_Cargo_OrderGoods a inner join Tbl_Cargo_Product b on a.ProductID=b.ProductID inner join Tbl_Cargo_Container c on a.ContainerCode=c.ContainerCode inner join tbl_cargo_Area ar on c.AreaID=ar.AreaID and a.HouseID=ar.HouseID inner join Tbl_Cargo_ProductType as d on b.TypeID=d.TypeID where OrderNo=@OrderNo";
                if (!entity.ProductID.Equals(0)) { strSQL += " and a.ProductID=" + entity.ProductID; }
                if (!string.IsNullOrEmpty(entity.ContainerCode)) { strSQL += " and a.ContainerCode='" + entity.ContainerCode + "'"; }
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                AreaID = Convert.ToInt32(idr["AreaID"]),
                                AreaName = Convert.ToString(idr["AreaName"]),
                                ContainerID = Convert.ToInt32(idr["ContainerID"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                                SupplySalePrice = Convert.ToDecimal(idr["SupplySalePrice"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                ProductCode = Convert.ToString(idr["ProductCode"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Model = Convert.ToString(idr["Model"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                Born = Convert.ToString(idr["Born"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                OutCargoID = Convert.ToString(idr["OutCargoID"]),
                                RuleType = Convert.ToString(idr["RuleType"]),
                                SuitClientNum = Convert.ToString(idr["SuitClientNum"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                ShowGoodsCode = Convert.ToString(idr["ShowGoodsCode"]),
                                ShowProductCode = Convert.ToString(idr["ShowProductCode"]),
                                LoadSpeed = Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }

        /// <summary>
        /// 判断 是否为退货单  
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsModelOrderInfo(string orderNo)
        {
            if (string.IsNullOrEmpty(orderNo)) {
                return false;
            }
            string strSQL = "Select OrderModel from Tbl_Cargo_Order where (1=1) AND OrderNo=@OrderNo";

            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@OrderNo", DbType.String, orderNo);
                using (DataTable dt = conn.ExecuteDataTable(cmdQ))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        int OrderModel = Convert.ToInt32(idr["OrderModel"]);
                        if (OrderModel == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 查询当前日期当前仓库表中最大顺序号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetMaxOrderNumByCurrentDate(CargoOrderEntity entity)
        {
            int result = 0;
            try
            {
                string strSQL = @"select ISNULL(MAX(OrderNum),0) as OrderNum from Tbl_Cargo_Order where HouseID=@HouseID ";
                strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                strSQL += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@HouseID", DbType.Int32, entity.HouseID);
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        if (dd.Rows.Count > 0) { result = Convert.ToInt32(dd.Rows[0]["OrderNum"]); }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 修改物流快递单号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateLogisAwbNo(CargoOrderEntity entity)
        {
            string strSQL = @"UPDATE Tbl_Cargo_Order Set LogisAwbNo=@LogisAwbNo,DeliveryFee=@DeliveryFee,LogisID=@LogisID,Dest=@Dest,SaleManID=@SaleManID,SaleManName=@SaleManName,SaleCellPhone=@SaleCellPhone,DeliverySettlement=@DeliverySettlement,OpenExpressName=@OpenExpressName,OpenExpressNum=@OpenExpressNum";
            if (!string.IsNullOrEmpty(entity.HAwbNo))
            {
                strSQL += ",HAwbNo=@HAwbNo";
            }
            strSQL += " Where OrderID=@OrderID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                    conn.AddInParameter(cmd, "@SaleCellPhone", DbType.String, entity.SaleCellPhone);
                    conn.AddInParameter(cmd, "@DeliverySettlement", DbType.String, entity.DeliverySettlement);
                    conn.AddInParameter(cmd, "@OpenExpressName", DbType.String, entity.OpenExpressName);
                    conn.AddInParameter(cmd, "@OpenExpressNum", DbType.String, entity.OpenExpressNum);
                    if (!string.IsNullOrEmpty(entity.HAwbNo))
                    {
                        conn.AddInParameter(cmd, "@HAwbNo", DbType.String, entity.HAwbNo);
                    }
                    conn.ExecuteNonQuery(cmd);
                }

                //CargoOrderEntity order = QueryOrderInfoByOrderID(entity.OrderID);
                //if (order != null)
                //{
                //    if (!order.ClientNum.Equals(0))
                //    {
                //        CargoStaticManager staticman = new CargoStaticManager();
                //        CargoLogisticEntity logisticEnt = staticman.QueryLogisticByID(new CargoLogisticEntity { ID = entity.LogisID });
                //        CargoWeiXinManager weixin = new CargoWeiXinManager();
                //        weixin.UpdateWxUserLogicNameByClientNum(new WXUserEntity { ClientNum = order.ClientNum, LogisID = entity.LogisID, LogicName = logisticEnt.LogisticName });
                //    }
                //}
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void SaveOrderDeliveryInfo(CargoOrderEntity entity)
        {
            string strSQL = @"UPDATE Tbl_Cargo_Order Set DeliveryDriverName=@DeliveryDriverName,DriverIDNum=@DriverIDNum,DriverCellphone=@DriverCellphone,DriverCarNum=@DriverCarNum Where OrderID=@OrderID";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.AddInParameter(cmd, "@DeliveryDriverName", DbType.String, entity.DeliveryDriverName);
                conn.AddInParameter(cmd, "@DriverIDNum", DbType.String, entity.DriverIDNum);
                conn.AddInParameter(cmd, "@DriverCellphone", DbType.String, entity.DriverCellphone);
                conn.AddInParameter(cmd, "@DriverCarNum", DbType.String, entity.DriverCarNum);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 通过订单号修改物流 单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="logisNo"></param>
        public void UpdateLogisAwbNo(string orderNo, string logisNo, string OrderAging)
        {
            string strSQL = @"UPDATE Tbl_Cargo_Order Set LogisAwbNo=@LogisAwbNo,OrderAging=@OrderAging Where OrderNo=@OrderNo";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, logisNo);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, orderNo);
                    conn.AddInParameter(cmd, "@OrderAging", DbType.String, OrderAging);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改副单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="HAwbNo"></param>
        public void UpdateHAwbNo(string orderNo, string HAwbNo)
        {
            string strSQL = @"UPDATE Tbl_Cargo_Order Set HAwbNo=@HAwbNo Where OrderNo=@OrderNo";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@HAwbNo", DbType.String, HAwbNo);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, orderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改副单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="HAwbNo"></param>
        public void UpdateOrderPieceCharge(CargoOrderEntity entity)
        {
            //string strSQL = @"UPDATE Tbl_Cargo_Order Set Piece=Piece-@Piece,TransportFee=TransportFee-@TransportFee,TotalCharge=TotalCharge-@TotalCharge Where OrderNo=@OrderNo";

            string strSQL = @"if((select Piece from Tbl_Cargo_Order where OrderNo=@OrderNo)-@Piece<=0)
                                begin
                                UPDATE Tbl_Cargo_Order Set Piece=Piece-@Piece,TransportFee=TransportFee-@TransportFee,TotalCharge=TotalCharge-@TotalCharge,AwbStatus=0,OutCargoTime=null Where OrderNo=@OrderNo
                                end
                                else
                                begin
                                UPDATE Tbl_Cargo_Order Set Piece=Piece-@Piece,TransportFee=TransportFee-@TransportFee,TotalCharge=TotalCharge-@TotalCharge Where OrderNo=@OrderNo
                                end";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改订单数量和金额
        /// </summary>
        /// <param name="entity"></param>
        public void DrawUpOrderPieceCharge(CargoOrderEntity entity)
        {
            string strSQL = @"UPDATE Tbl_Cargo_Order Set Piece=Piece+@Piece,TransportFee=TransportFee+@TransportFee,TotalCharge=TotalCharge+@TotalCharge";
            if (!entity.TransitFee.Equals(0))
            {
                strSQL += ",TransitFee=TransitFee+@TransitFee";
            }
            strSQL += " Where OrderNo=@OrderNo";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    if (!entity.TransitFee.Equals(0))
                    {
                        conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    }
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 通过订单号修改微信支付生成的单 号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateOrderWxNoByOrderNo(CargoOrderEntity entity)
        {
            string strSQL = @"UPDATE Tbl_Cargo_Order Set WXOrderNo=@WXOrderNo Where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@WXOrderNo", DbType.String, entity.WXOrderNo);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 通过订单号修改订单的结算状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="CheckStatus"></param>
        public void UpdateCheckStatusByOrderNo(string orderNo, string CheckStatus)
        {
            CargoWeiXinManager wx = new CargoWeiXinManager();
            try
            {
                string strDel = @"UPDATE Tbl_Cargo_Order SET CheckStatus=@CheckStatus WHERE OrderNo=@OrderNo";
                using (DbCommand cmdAdd = conn.GetSqlStringCommond(strDel))
                {
                    conn.AddInParameter(cmdAdd, "@CheckStatus", DbType.String, CheckStatus);
                    conn.AddInParameter(cmdAdd, "@OrderNo", DbType.String, orderNo);
                    conn.ExecuteNonQuery(cmdAdd);
                }

                CargoOrderEntity order = QueryOrderInfo(new CargoOrderEntity { OrderNo = orderNo });
                if (!string.IsNullOrEmpty(order.WXOrderNo) && order.CheckStatus.Equals("1") && order.CheckOutType.Equals("6"))
                {
                    wx.UpdateClientLimitMoney(order.TotalCharge, order.ClientNum, "0");
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 通过订单号修改订单状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="AwbStatus"></param>
        public void UpdateAwbStatusByOrderNo(long OrderID, string orderNo, string AwbStatus, string SignTime, string Signer, string AbSignStatus)
        {
            string strDel = @"UPDATE Tbl_Cargo_Order SET AwbStatus=@AwbStatus WHERE OrderNo=@OrderNo";
            if (AwbStatus.Equals("2"))
            {
                strDel = @"UPDATE Tbl_Cargo_Order SET AwbStatus=@AwbStatus,OutCargoTime=@OutCargoTime WHERE OrderNo=@OrderNo";
            }
            else if (AwbStatus.Equals("8"))//接单
            {
                strDel = @"UPDATE Tbl_Cargo_Order SET AwbStatus=@AwbStatus,TakeOrderName=@TakeOrderName,TakeOrderTime=@TakeOrderTime WHERE OrderNo=@OrderNo";
            }
            else if (AwbStatus.Equals("3"))//发车配送
            {
                strDel = @"UPDATE Tbl_Cargo_Order SET AwbStatus=@AwbStatus,SendCarName=@SendCarName,SendCarTime=@SendCarTime WHERE OrderNo=@OrderNo";
            }
            else if (AwbStatus.Equals("5"))
            {
                strDel = @"UPDATE Tbl_Cargo_Order SET AwbStatus=@AwbStatus,SignTime='" + SignTime + "',Signer='" + Signer + "',AbSignStatus=@AbSignStatus WHERE OrderNo=@OrderNo";
            }
            using (DbCommand cmdAdd = conn.GetSqlStringCommond(strDel))
            {
                conn.AddInParameter(cmdAdd, "@AwbStatus", DbType.String, AwbStatus);
                conn.AddInParameter(cmdAdd, "@OrderNo", DbType.String, orderNo);
                if (AwbStatus.Equals("2"))
                {
                    conn.AddInParameter(cmdAdd, "@OutCargoTime", DbType.DateTime, DateTime.Now);
                }
                else if (AwbStatus.Equals("8"))//接单
                {
                    conn.AddInParameter(cmdAdd, "@TakeOrderName", DbType.String, Signer);
                    conn.AddInParameter(cmdAdd, "@TakeOrderTime", DbType.DateTime, DateTime.Now);
                }
                else if (AwbStatus.Equals("3"))//发车配送
                {
                    conn.AddInParameter(cmdAdd, "@SendCarName", DbType.String, Signer);
                    conn.AddInParameter(cmdAdd, "@SendCarTime", DbType.DateTime, DateTime.Now);
                }
                else if (AwbStatus.Equals("5"))
                {
                    conn.AddInParameter(cmdAdd, "@AbSignStatus", DbType.String, AbSignStatus);
                }
                conn.ExecuteNonQuery(cmdAdd);
            }
        }
        /// <summary>
        /// 新增订单跟踪状态数据
        /// </summary>
        /// <param name="entity"></param>
        public void InsertCargoOrderStatus(CargoOrderStatusEntity entity)
        {
            entity.EnSafe();
            //添加订单状态跟踪记录表
            string strSQL = @"INSERT INTO Tbl_Cargo_OrderStatus(OrderID,OrderNo,OrderStatus";
            if (!string.IsNullOrEmpty(entity.Signer))
            {
                strSQL += ",Signer,SignTime";
            }
            strSQL += ",SignImage,DetailInfo,OP_ID,OP_Name,OP_DATE,Longitude,Latitude) VALUES (@OrderID,@OrderNo,@OrderStatus";
            if (!string.IsNullOrEmpty(entity.Signer))
            {
                strSQL += ",@Signer,@SignTime";
            }
            strSQL += ",@SignImage,@DetailInfo,@OP_ID,@OP_Name,@OP_DATE,@Longitude,@Latitude)";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.AddInParameter(cmd, "@OrderStatus", DbType.String, entity.OrderStatus);
                if (!string.IsNullOrEmpty(entity.Signer))
                {
                    conn.AddInParameter(cmd, "@Signer", DbType.String, entity.Signer);
                    conn.AddInParameter(cmd, "@SignTime", DbType.DateTime, entity.SignTime);
                }
                conn.AddInParameter(cmd, "@SignImage", DbType.String, entity.SignImage);
                conn.AddInParameter(cmd, "@DetailInfo", DbType.String, entity.DetailInfo);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.AddInParameter(cmd, "@OP_Name", DbType.String, entity.OP_Name);
                conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, entity.OP_DATE);
                conn.AddInParameter(cmd, "@Longitude", DbType.String, entity.Longitude);
                conn.AddInParameter(cmd, "@Latitude", DbType.String, entity.Latitude);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 判断是否有签收数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistCargoOrderCheckStatus(CargoOrderStatusEntity entity)
        {
            string strSQL = "Select OrderNo from Tbl_Cargo_OrderStatus where OrderNo=@OrderNo and OrderStatus=@OrderStatus";

            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@OrderNo", DbType.String, entity.OrderNo);
                conn.AddInParameter(cmdQ, "@OrderStatus", DbType.String, entity.OrderStatus);

                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 查询订单的跟踪状态信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderStatusEntity> QueryOrderStatus(CargoOrderStatusEntity entity)
        {
            List<CargoOrderStatusEntity> result = new List<CargoOrderStatusEntity>();
            try
            {
                string strSQL = @"select * from Tbl_Cargo_OrderStatus where 1=1 ";
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and OrderNo=@OrderNo"; }
                if (!entity.OrderID.Equals(0)) { strSQL += " and OrderID=@OrderID"; }
                if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and OrderStatus=@OrderStatus"; }

                strSQL += " Order By OP_DATE desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    if (!string.IsNullOrEmpty(entity.OrderNo))
                    {
                        conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);
                    }
                    if (!entity.OrderID.Equals(0))
                    {
                        conn.AddInParameter(command, "@OrderID", DbType.String, entity.OrderID);
                    }
                    if (!string.IsNullOrEmpty(entity.OrderStatus))
                    {
                        conn.AddInParameter(command, "@OrderStatus", DbType.String, entity.OrderStatus);
                    }
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderStatusEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                OP_Name = Convert.ToString(idr["OP_Name"]),
                                DetailInfo = Convert.ToString(idr["DetailInfo"]),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]),
                                Signer = Convert.ToString(idr["Signer"]),
                                SignTime = string.IsNullOrEmpty(Convert.ToString(idr["SignTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["SignTime"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                SignImage = Convert.ToString(idr["SignImage"]),
                                Latitude = Convert.ToString(idr["Latitude"]),
                                Longitude = Convert.ToString(idr["Longitude"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;

        }

        public void AddOrderStaus(CargoOrderStatusEntity entity)
        {
            entity.EnSafe();
            //添加订单状态跟踪记录表
            string strSQL = @"INSERT INTO Tbl_Cargo_OrderStatus(OrderID,OrderNo,OrderStatus";
            if (!string.IsNullOrEmpty(entity.Signer) && entity.OrderStatus.Equals("5"))
            {
                strSQL += ",Signer,SignTime,SignImage";
            }
            strSQL += ",DetailInfo,OP_ID,OP_Name,OP_DATE,Longitude,Latitude) VALUES (@OrderID,@OrderNo,@OrderStatus";
            if (!string.IsNullOrEmpty(entity.Signer) && entity.OrderStatus.Equals("5"))
            {
                strSQL += ",@Signer,@SignTime,@SignImage";
            }
            strSQL += ",@DetailInfo,@OP_ID,@OP_Name,@OP_DATE,@Longitude,@Latitude)";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.AddInParameter(cmd, "@OrderStatus", DbType.String, entity.OrderStatus);
                if (!string.IsNullOrEmpty(entity.Signer) && entity.OrderStatus.Equals("5"))
                {
                    conn.AddInParameter(cmd, "@Signer", DbType.String, entity.Signer);
                    conn.AddInParameter(cmd, "@SignTime", DbType.DateTime, entity.SignTime);
                    conn.AddInParameter(cmd, "@SignImage", DbType.String, entity.SignImage);
                }
                conn.AddInParameter(cmd, "@DetailInfo", DbType.String, entity.DetailInfo);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.AddInParameter(cmd, "@OP_Name", DbType.String, entity.OP_Name);
                conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, entity.OP_DATE);
                conn.AddInParameter(cmd, "@Longitude", DbType.String, entity.Longitude);
                conn.AddInParameter(cmd, "@Latitude", DbType.String, entity.Latitude);
                conn.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 保存订单跟踪状态
        /// </summary>
        /// <param name="entity"></param>
        public void SaveOrderStatus(CargoOrderStatusEntity entity)
        {
            entity.EnSafe();
            CargoOrderEntity orderEnt = QueryOrderInfo(new CargoOrderEntity { OrderNo = entity.OrderNo });
            if (!orderEnt.AwbStatus.Equals("5"))
            {
                string signTime = entity.SignTime.ToString("yyyy-MM-dd").Equals("0001-01-01") || entity.SignTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : entity.SignTime.ToString("yyyy-MM-dd HH:mm:ss");
                //修改订单状态
                UpdateAwbStatusByOrderNo(orderEnt.OrderID, orderEnt.OrderNo, entity.OrderStatus, signTime, entity.Signer, entity.AbSignStatus);
                if (!string.IsNullOrEmpty(entity.WXOrderNo))
                {
                    if (entity.OrderStatus.Equals("2"))
                    {
                        //表示已出库
                        UpdateWeixinOrderStatusByOrderNo(new WXOrderEntity { OrderStatus = "2", OrderNo = entity.WXOrderNo });
                    }
                    if (entity.OrderStatus.Equals("3"))
                    {
                        //表示已装车
                        UpdateWeixinOrderStatusByOrderNo(new WXOrderEntity { OrderStatus = "3", OrderNo = entity.WXOrderNo });
                    }
                    if (entity.OrderStatus.Equals("5"))
                    {
                        //表示签收
                        UpdateWeixinOrderStatusByOrderNo(new WXOrderEntity { OrderStatus = "4", OrderNo = entity.WXOrderNo });
                    }
                }
            }

            //添加订单状态跟踪记录表
            string strSQL = @"INSERT INTO Tbl_Cargo_OrderStatus(OrderID,OrderNo,OrderStatus";
            if (!string.IsNullOrEmpty(entity.Signer) && entity.OrderStatus.Equals("5"))
            {
                strSQL += ",Signer,SignTime,SignImage";
            }
            strSQL += ",DetailInfo,OP_ID,OP_Name,OP_DATE,Longitude,Latitude) VALUES (@OrderID,@OrderNo,@OrderStatus";
            if (!string.IsNullOrEmpty(entity.Signer) && entity.OrderStatus.Equals("5"))
            {
                strSQL += ",@Signer,@SignTime,@SignImage";
            }
            strSQL += ",@DetailInfo,@OP_ID,@OP_Name,@OP_DATE,@Longitude,@Latitude)";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, orderEnt.OrderID);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, orderEnt.OrderNo);
                conn.AddInParameter(cmd, "@OrderStatus", DbType.String, entity.OrderStatus);
                if (!string.IsNullOrEmpty(entity.Signer) && entity.OrderStatus.Equals("5"))
                {
                    conn.AddInParameter(cmd, "@Signer", DbType.String, entity.Signer);
                    conn.AddInParameter(cmd, "@SignTime", DbType.DateTime, entity.SignTime);
                    conn.AddInParameter(cmd, "@SignImage", DbType.String, entity.SignImage);
                }
                conn.AddInParameter(cmd, "@DetailInfo", DbType.String, entity.DetailInfo);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.AddInParameter(cmd, "@OP_Name", DbType.String, entity.OP_Name);
                conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, entity.OP_DATE);
                conn.AddInParameter(cmd, "@Longitude", DbType.String, entity.Longitude);
                conn.AddInParameter(cmd, "@Latitude", DbType.String, entity.Latitude);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改微信商城 订单物流 跟踪状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWeixinOrderStatusByOrderNo(WXOrderEntity entity)
        {
            string strSQL = @"Update Tbl_WX_Order Set OrderStatus=@OrderStatus where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderStatus", DbType.String, entity.OrderStatus);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询订单数据用以批量打印
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public List<CargoContainerShowEntity> QueryOrderDataForMassPrint(string orderno)
        {
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            try
            {
                string ar = string.Empty;
                string[] arr = orderno.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (string.IsNullOrEmpty(arr[i])) { continue; }
                    if (i == arr.Length - 1) { ar += arr[i]; break; }
                    ar += arr[i] + "','";
                }
                string strSQL = @"select a.OrderNo,a.HouseID,a.ProductID,a.AreaID,a.ContainerCode,a.Piece,a.OP_DATE,a.OutCargoID,
b.Specs,b.Figure,b.Model,b.Batch,b.ProductName,b.Package,b.PackageNum,b.GoodsCode,c.Name,d.Dep,d.Dest,d.AcceptUnit,d.AcceptAddress,d.AcceptPeople,d.AcceptTelephone,d.AcceptCellphone,d.LogisAwbNo,d.PayClientNum,e.ParentID from Tbl_Cargo_OrderGoods as a inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID and a.HouseID=b.HouseID left join Tbl_Cargo_Area as c on a.AreaID=c.AreaID left join Tbl_Cargo_Order as d on a.OrderNo=d.OrderNo and a.HouseID=d.HouseID left join Tbl_Cargo_ProductType as e on b.TypeID=e.TypeID where a.OrderNo in ('" + ar + "')  order by d.LogisAwbNo asc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoContainerShowEntity
                            {
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                AreaID = Convert.ToInt32(idr["AreaID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Model = Convert.ToString(idr["Model"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                AreaName = Convert.ToString(idr["Name"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                TypeParentID = Convert.ToInt32(idr["ParentID"]),
                                Package = Convert.ToString(idr["Package"]),
                                PackageNum = Convert.ToInt32(idr["PackageNum"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                PayClientNum = string.IsNullOrEmpty(Convert.ToString(idr["PayClientNum"])) ? 0 : Convert.ToInt32(idr["PayClientNum"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 批量查询订单数据 导出
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoContainerShowEntity> QueryOrderDataForMassExport(CargoOrderEntity entity)
        {
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            try
            {
                string strSQL = @"select a.OrderNo,a.ProductID,a.AreaID,a.ContainerCode,a.Piece,a.OP_DATE,a.OutCargoID,b.Specs,b.Figure,b.Model,b.Batch,b.ProductName,b.Package,b.PackageNum,c.Name,d.Dest,d.AcceptPeople,d.LogisAwbNo,pd.ParentID from Tbl_Cargo_OrderGoods as a left join Tbl_Cargo_Product as b on a.ProductID=b.ProductID left join Tbl_Cargo_Area as c on a.AreaID=c.AreaID left join Tbl_Cargo_ProductType as pd on b.TypeID=pd.TypeID ";
                if (!entity.FirstAreaID.Equals(0))
                {
                    strSQL += " left join Tbl_Cargo_Area as e on c.ParentID=e.AreaID ";
                }
                strSQL += " left join Tbl_Cargo_Order as d on a.OrderNo=d.OrderNo Where (1=1)";
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and d.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and d.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                if (!entity.FirstAreaID.Equals(0))
                {
                    strSQL += " and e.ParentID=" + entity.FirstAreaID + " ";
                }
                if (!entity.TypeID.Equals(0))
                {
                    strSQL += " and b.TypeID=" + entity.TypeID + " ";
                }
                strSQL += " order by d.LogisAwbNo asc,c.Name asc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoContainerShowEntity
                            {
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                AreaID = Convert.ToInt32(idr["AreaID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Model = Convert.ToString(idr["Model"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                AreaName = Convert.ToString(idr["Name"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                Package = Convert.ToString(idr["Package"]),
                                PackageNum = Convert.ToInt32(idr["PackageNum"]),
                                TypeParentID = Convert.ToInt32(idr["ParentID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public List<CargoOrderGoodsEntity> QueryOrderPicekPieceExport(CargoOrderEntity entity)
        {
            List<CargoOrderGoodsEntity> result = new List<CargoOrderGoodsEntity>();
            try
            {
                string strSQL = @"select max( c.ClientShortName)ClientName,o.ClientNum, p.GoodsCode,SUM(og.Piece)Piece,sum(opp.ScanPiece)ScanPiece from Tbl_Cargo_OrderGoods og inner join Tbl_Cargo_Order o on og.OrderNo=o.OrderNo inner join Tbl_Cargo_Product p on og.ProductID=p.ProductID inner join Tbl_Cargo_OrderPickPlanGoods opp on o.OrderNo=opp.OrderNo and p.GoodsCode=opp.GoodsCode inner join Tbl_Cargo_Client c on o.ClientNum=c.ClientNum where (1=1)   ";
                if (!string.IsNullOrEmpty(entity.OrderNoStr))
                {
                    strSQL += " and o.OrderNo in (" + entity.OrderNoStr + ") ";
                }
                strSQL += " group by p.GoodsCode,o.ClientNum";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderGoodsEntity
                            {
                                PayClientName = Convert.ToString(idr["ClientName"]),
                                PayClientNum = Convert.ToInt32(idr["ClientNum"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                ScanPiece = Convert.ToInt32(idr["ScanPiece"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询 没有签收 图片的数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderStatusEntity> QueryOrderStatusNoPic(CargoOrderStatusEntity entity)
        {
            List<CargoOrderStatusEntity> result = new List<CargoOrderStatusEntity>();
            try
            {
                string strSQL = @"select a.ID,a.OrderID,a.OrderNo,a.OrderStatus,a.DetailInfo,a.Signer,b.LogisID,b.LogisAwbNo,b.ThrowGood,b.OutHouseName,b.OpenOrderNo,b.ShareHouseID From Tbl_Cargo_OrderStatus as a inner join Tbl_Cargo_Order as b on a.OrderID=b.OrderID where (a.SignImage='' or a.SignImage is NULL) ";
                if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and a.OrderStatus='" + entity.OrderStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
                //开始日期
                if ((entity.OP_DATE.ToString("yyyy-MM-dd") != "0001-01-01" && entity.OP_DATE.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE>='" + entity.OP_DATE.ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderStatusEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                Signer = Convert.ToString(idr["Signer"]),
                                DetailInfo = Convert.ToString(idr["DetailInfo"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                OpenOrderNo = Convert.ToString(idr["OpenOrderNo"]),
                                ShareHouseID = Convert.ToInt32(idr["ShareHouseID"]),
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;

        }
        /// <summary>
        /// 修改订单跟踪状态的签收照片
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateOrderStatusPic(CargoOrderStatusEntity entity)
        {
            string strSQL = "Update Tbl_Cargo_OrderStatus set SignImage=@SignImage Where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@SignImage", DbType.String, entity.SignImage);
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询近10天内的梅州 仓库 的未签收的订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryCargoOrderTen(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();

            string strSQL = @"select OrderID,OrderNo,LogisAwbNo,WXOrderNo,Dest,Dep,AwbStatus,ThrowGood,OutHouseName from Tbl_Cargo_Order where AwbStatus!=5 and LogisAwbNo<>''";
            if (!entity.LogisID.Equals(0)) { strSQL += " and LogisID=" + entity.LogisID; }
            if (!entity.HouseID.Equals(0)) { strSQL += " and HouseID=" + entity.HouseID; }
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and OrderNo='" + entity.OrderNo + "'"; }
            if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and HouseID in (" + entity.CargoPermisID + ")"; }

            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            strSQL += " order by CreateDate asc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoOrderEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            AwbStatus = Convert.ToString(idr["AwbStatus"]),
                            WXOrderNo = Convert.ToString(idr["WXOrderNo"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"])
                        });
                    }
                }
            }
            return result;
        }

        public List<CargoOrderEntity> QueryCargoOrderKuaidi100(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();

            string strSQL = @"select a.OrderID,a.OrderNo,a.LogisAwbNo,a.LogisID,a.Dep,a.Dest,a.AwbStatus,a.OutHouseName,b.LogisticName,b.ComCode,a.ThrowGood From Tbl_Cargo_Order as a inner join Tbl_Cargo_HouseLogisPoll as b on a.LogisID=b.LogisID and a.HouseID=b.HouseID where (1=1) and b.DelFlag=0 ";
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
            if (!string.IsNullOrEmpty(entity.PollStatus)) { strSQL += " and a.PollStatus='" + entity.PollStatus + "'"; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoOrderEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            AwbStatus = Convert.ToString(idr["AwbStatus"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            LogisticName = Convert.ToString(idr["LogisticName"]),
                            ComCode = Convert.ToString(idr["ComCode"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"])
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 通过微信订单号查询订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CargoOrderEntity QueryCargoInfoByWxOrderNo(CargoOrderEntity entity)
        {
            CargoOrderEntity result = new CargoOrderEntity();
            string strSQL = "select OrderID,OrderNo,WXOrderNo,Dest,AcceptPeople from Tbl_Cargo_Order where WXOrderNo='" + entity.WXOrderNo + "'";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.OrderID = Convert.ToInt64(idr["OrderID"]);
                        result.OrderNo = Convert.ToString(idr["OrderNo"]);
                        result.WXOrderNo = Convert.ToString(idr["WXOrderNo"]);
                        result.Dest = Convert.ToString(idr["Dest"]);
                        result.AcceptPeople = Convert.ToString(idr["AcceptPeople"]);
                    }
                }
            }
            return result;
        }
        public CargoHouseEntity QueryCargoHouse(CargoHouseEntity entity)
        {
            CargoHouseEntity result = new CargoHouseEntity();
            string strSQL = @"SELECT * FROM Tbl_Cargo_House WHERE DelFlag='0'";
            if (!string.IsNullOrEmpty(entity.DeliveryArea)) { strSQL += " and DeliveryArea like '%" + entity.DeliveryArea + "%'"; }
            if (!entity.HouseID.Equals(0)) { strSQL += " and HouseID='" + entity.HouseID + "'"; }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.HouseID = Convert.ToInt32(idrCount["HouseID"]);
                            result.Name = Convert.ToString(idrCount["Name"]);
                            result.HouseCode = Convert.ToString(idrCount["HouseCode"]);
                            result.Person = Convert.ToString(idrCount["Person"]);
                            result.Cellphone = Convert.ToString(idrCount["Cellphone"]);
                            result.Remark = Convert.ToString(idrCount["Remark"]);
                            result.OP_DATE = Convert.ToDateTime(idrCount["OP_DATE"]);
                            result.SendTitle = Convert.ToString(idrCount["SendTitle"]);
                            result.PickTitle = Convert.ToString(idrCount["PickTitle"]);
                            result.DeliveryArea = Convert.ToString(idrCount["DeliveryArea"]);
                            result.CargoDepart = Convert.ToString(idrCount["CargoDepart"]);
                            result.OESCargoDepart = Convert.ToString(idrCount["OESCargoDepart"]);
                            result.DepCity = Convert.ToString(idrCount["DepCity"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 保存订单数据 到好来运系统
        /// </summary>
        /// <param name="entity"></param>
        public void SaveHlyOrderData(List<CargoContainerShowEntity> entity, CargoOrderEntity order)
        {
            //好来运数据库的数据连接
            SqlHelper hlySql = new SqlHelper("HLYSql");
            string area = "好来运迪乐泰广州仓业务部";
            if (order.HouseID.Equals(49)) { area = "好来运迪乐泰杭州仓业务部"; }
            if (order.HouseID.Equals(50)) { area = "好来运迪乐泰贵阳仓业务部"; }
            if (order.HouseID.Equals(51)) { area = "好来运迪乐泰太原仓业务部"; }
            if (order.HouseID.Equals(52)) { area = "好来运迪乐泰长春仓业务部"; }
            if (order.HouseID.Equals(53)) { area = "好来运迪乐泰昆明仓业务部"; }
            if (order.HouseID.Equals(54)) { area = "好来运迪乐泰新疆仓业务部"; }


            if (order.TrafficType.Equals("2"))
            {
                area = QueryCargoHouse(new CargoHouseEntity { HouseID = order.HouseID }).OESCargoDepart;
            }
            else
            {
                //OES仓库旧货写入为OES业务部，以下仓库无货后可弃用
                if (order.ThrowGood.Equals("12"))
                {
                    if (order.HouseID.Equals(9)) { area = "好来运迪乐泰OES广州仓业务部"; }
                    if (order.HouseID.Equals(10)) { area = "好来运迪乐泰OES西安仓业务部"; }
                    if (order.HouseID.Equals(13)) { area = "好来运迪乐泰OES天津仓业务部"; }
                    if (order.HouseID.Equals(14)) { area = "好来运迪乐泰OES成都仓业务部"; }
                    if (order.HouseID.Equals(15)) { area = "好来运迪乐泰OES上海仓业务部"; }
                    if (order.HouseID.Equals(23)) { area = "好来运迪乐泰OES沈阳仓业务部"; }
                    if (order.HouseID.Equals(30)) { area = "好来运迪乐泰OES武汉仓业务部"; }
                }
                else
                {
                    area = QueryCargoHouse(new CargoHouseEntity { HouseID = order.HouseID }).CargoDepart;
                }
            }
            //if (order.ThrowGood.Equals("12") && order.HouseID.Equals(9)) { area = "狄乐汽服广州仓业务部"; }
            if (order.ThrowGood.Equals("15") && order.HouseID.Equals(9)) { area = "好来运迪乐泰城配广州仓业务部"; }
            if (entity[0].TypeID.Equals(244)) { area = "迪乐泰马牌机油广州仓业务部"; }
            if (entity[0].ProductName.Equals("迪乐泰马牌机油广州仓业务部")) { area = "迪乐泰马牌机油广州仓业务部"; }
            if (entity[0].TypeParentID.Equals(10) || entity[0].TypeParentID.Equals(35))
            {
                area = entity[0].ProductName;
            }
            if (entity[0].TypeID.Equals(258) && order.HouseID.Equals(10))
            {
                area = "好来运安泰路斯轮胎西安仓业务部";
            }
            int ONum = GetHLYOrderNum(area);

            foreach (var it in entity)
            {
                it.EnSafe();
                string strSQL = "insert into Tbl_ProductOrder(guestid,pname,Fpno,model,Specification,TyreModel,Figure,Sname,number,orderdate,area,state,Inuser,Indate,UID,Batch,address,guestname,Phone,Fdest,ZhongFdest,other,TypeID,ThrowGood,DeliveryFee,LogisID) values (@guestid,@pname,@Fpno,@model,@Specification,@TyreModel,@Figure,@Sname,@number,@orderdate,@area,@state,@Inuser,@Indate,@UID,@Batch,@address,@guestname,@Phone,@Fdest,@ZhongFdest,@other,@TypeID,@ThrowGood,@DeliveryFee,@LogisID)";

                using (DbCommand cmd = hlySql.GetSqlStringCommond(strSQL))
                {
                    hlySql.AddInParameter(cmd, "@guestid", DbType.String, string.IsNullOrEmpty(order.ShopCode) ? order.ClientNum.ToString() : order.ShopCode);
                    hlySql.AddInParameter(cmd, "@pname", DbType.String, order.AcceptUnit);
                    hlySql.AddInParameter(cmd, "@Fpno", DbType.String, order.OrderNo);
                    hlySql.AddInParameter(cmd, "@model", DbType.String, it.GoodsCode);
                    hlySql.AddInParameter(cmd, "@Specification", DbType.String, it.Specs);
                    hlySql.AddInParameter(cmd, "@TyreModel", DbType.String, it.Model);
                    hlySql.AddInParameter(cmd, "@Figure", DbType.String, it.Figure);
                    hlySql.AddInParameter(cmd, "@Sname", DbType.String, it.ProductName + "/" + it.TypeName);
                    hlySql.AddInParameter(cmd, "@Batch", DbType.String, it.Batch);
                    hlySql.AddInParameter(cmd, "@number", DbType.Decimal, it.Piece);
                    hlySql.AddInParameter(cmd, "@orderdate", DbType.DateTime, order.CreateDate);
                    hlySql.AddInParameter(cmd, "@area", DbType.String, area);
                    hlySql.AddInParameter(cmd, "@state", DbType.String, "未开单");
                    hlySql.AddInParameter(cmd, "@Inuser", DbType.String, order.CreateAwb);
                    hlySql.AddInParameter(cmd, "@Indate", DbType.DateTime, DateTime.Now);
                    hlySql.AddInParameter(cmd, "@UID", DbType.Decimal, ONum);
                    hlySql.AddInParameter(cmd, "@address", DbType.String, order.AcceptAddress);
                    hlySql.AddInParameter(cmd, "@guestname", DbType.String, order.AcceptPeople);
                    hlySql.AddInParameter(cmd, "@Phone", DbType.String, order.AcceptCellphone);
                    hlySql.AddInParameter(cmd, "@Fdest", DbType.String, order.Dep);
                    hlySql.AddInParameter(cmd, "@ZhongFdest", DbType.String, order.Dest);
                    hlySql.AddInParameter(cmd, "@other", DbType.String, order.Remark);
                    hlySql.AddInParameter(cmd, "@TypeID", DbType.String, it.TypeID);
                    hlySql.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, it.DeliveryFee);
                    hlySql.AddInParameter(cmd, "@ThrowGood", DbType.String, it.ThrowGood);
                    hlySql.AddInParameter(cmd, "@LogisID", DbType.String, it.LogisID);
                    hlySql.ExecuteNonQuery(cmd);
                }
            }
        }
        public string QueryHlyArriveToDest(string Arrive)
        {
            SqlHelper hlySql = new SqlHelper("HLYSql");
            string Dest = string.Empty;
            string strSQL = "Select top 1 * From Tbl_FdestList Where Arrive=@Arrive";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(command, "@Arrive", DbType.String, Arrive);
                using (DataTable dt = hlySql.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        Dest = Convert.ToString(idr["fDest"]);
                    }
                }
            }
            return Dest;
        }
        /// <summary>
        /// 查询好来运订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CargoOrderEntity QueryHlyOrderData(string orderNo)
        {
            SqlHelper hlySql = new SqlHelper("HLYSql");
            CargoOrderEntity result = new CargoOrderEntity();
            string strSQL = "Select top 1 * From Tbl_ProductOrder Where Fpno=@Fpno";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(command, "@Fpno", DbType.String, orderNo);
                using (DataTable dt = hlySql.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.AwbStatus = Convert.ToString(idr["state"]);
                        result.HAwbNo = Convert.ToString(idr["awbno"]);
                        result.OrderNo = Convert.ToString(idr["Fpno"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 删除 好来运订单
        /// </summary>
        /// <param name="orderNo"></param>
        public void DelHlyOrderData(string orderNo)
        {
            SqlHelper hlySql = new SqlHelper("HLYSql");
            string strSQL = "Delete From Tbl_ProductOrder where Fpno =@Fpno";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(command, "@Fpno", DbType.String, orderNo);
                hlySql.ExecuteNonQuery(command);
            }
        }
        /// <summary>
        /// 返回好来运最大订单序列号
        /// </summary>
        /// <returns></returns>
        private int GetHLYOrderNum(string area)
        {
            int Num = 0;
            SqlHelper hlySql = new SqlHelper("HLYSql");
            string strSQL = "select MAX(UID)+1 as ONum From  Tbl_ProductOrder where area = '" + area + "'";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = hlySql.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        Num = string.IsNullOrEmpty(Convert.ToString(idr["ONum"])) ? 1 : Convert.ToInt32(idr["ONum"]);
                    }
                }
            }
            return Num;
        }
        /// <summary>
        /// 根据运单号查询好来运物流 跟踪状态
        /// </summary>
        /// <param name="awbno"></param>
        /// <returns></returns>
        public List<CargoOrderStatusEntity> QueryHlyOrderStatus(string awbno)
        {
            SqlHelper hlySql = new SqlHelper("HLYSql");
            List<CargoOrderStatusEntity> result = new List<CargoOrderStatusEntity>();
            string strSQL = "select * From tbl_state where awbno=@awbno order by Ftime desc";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(command, "@awbno", DbType.String, awbno);
                using (DataTable dt = hlySql.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoOrderStatusEntity
                        {
                            DetailInfo = Convert.ToString(idr["State"]),
                            OP_Name = Convert.ToString(idr["Puser"]),
                            OP_DATE = Convert.ToDateTime(idr["Ftime"]),
                            Signer = Convert.ToString(idr["pic"]),
                            WXOrderNo = awbno
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 保存好来运外采订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void SaveHlyOutOrderData(CargoOrderEntity entity)
        {
            //好来运数据库的数据连接
            SqlHelper hlySql = new SqlHelper("HLYSql");
            string strSQL = "Insert into Tbl_ApiGuestAwbno(Guestid,FrmnAme,Frmaddress,Frmtel,Mobile,Toname,Toaddress,ToMobile,Totel,Hangtag,InsuranceVal,Post,Packing,Pc,FDep,FDest,State,Dfee,Inuser,Idate,IdNumber,CorporateName,FrmPlace,transfe,UnitName,Tcategory,Submit,Input,WXOpenId,TranStep,AwbnoType,TypeExplain,CargoOrderNo) values (@Guestid,@FrmnAme,@Frmaddress,@Frmtel,@Mobile,@Toname,@Toaddress,@ToMobile,@Totel,@Hangtag,@InsuranceVal,@Post,@Packing,@Pc,@FDep,@FDest,@State,@Dfee,@Inuser,@Idate,@IdNumber,@CorporateName,@FrmPlace,@transfe,@UnitName,@Tcategory,@Submit,@Input,@WXOpenId,@TranStep,@AwbnoType,@TypeExplain,@CargoOrderNo)";
            using (DbCommand cmd = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(cmd, "@Guestid", DbType.String, "12930");
                hlySql.AddInParameter(cmd, "@FrmnAme", DbType.String, "广州迪乐泰");
                hlySql.AddInParameter(cmd, "@Frmaddress", DbType.String, "广州迪乐泰");
                hlySql.AddInParameter(cmd, "@Frmtel", DbType.String, "19928313473");
                hlySql.AddInParameter(cmd, "@Mobile", DbType.String, "19928313473");
                hlySql.AddInParameter(cmd, "@Toname", DbType.String, entity.AcceptPeople);
                hlySql.AddInParameter(cmd, "@Toaddress", DbType.String, entity.AcceptAddress);
                hlySql.AddInParameter(cmd, "@ToMobile", DbType.String, entity.AcceptTelephone);
                hlySql.AddInParameter(cmd, "@Totel", DbType.String, entity.AcceptCellphone);
                hlySql.AddInParameter(cmd, "@Hangtag", DbType.String, entity.Piece.ToString() + "条轮胎");
                hlySql.AddInParameter(cmd, "@InsuranceVal", DbType.Decimal, 0);
                hlySql.AddInParameter(cmd, "@Post", DbType.String, "月结");
                hlySql.AddInParameter(cmd, "@Packing", DbType.String, "纸箱");
                hlySql.AddInParameter(cmd, "@Pc", DbType.Int32, entity.Piece);
                hlySql.AddInParameter(cmd, "@FDep", DbType.String, entity.Dep);
                hlySql.AddInParameter(cmd, "@FDest", DbType.String, entity.Dest);
                hlySql.AddInParameter(cmd, "@State", DbType.String, "预录单");
                hlySql.AddInParameter(cmd, "@Dfee", DbType.Decimal, 0);
                hlySql.AddInParameter(cmd, "@Inuser", DbType.String, "广州迪乐泰");
                hlySql.AddInParameter(cmd, "@Idate", DbType.DateTime, DateTime.Now);
                hlySql.AddInParameter(cmd, "@IdNumber", DbType.String, "0");
                hlySql.AddInParameter(cmd, "@CorporateName", DbType.String, "广州迪乐泰");
                hlySql.AddInParameter(cmd, "@FrmPlace", DbType.String, "天平架营业部");
                hlySql.AddInParameter(cmd, "@transfe", DbType.String, "汽运");
                hlySql.AddInParameter(cmd, "@UnitName", DbType.String, entity.AcceptUnit);
                hlySql.AddInParameter(cmd, "@Tcategory", DbType.String, "普通运价");
                hlySql.AddInParameter(cmd, "@Submit", DbType.String, "否");
                hlySql.AddInParameter(cmd, "@Input", DbType.String, "否");
                hlySql.AddInParameter(cmd, "@WXOpenId", DbType.String, "oo1LEt4wPmZk3Dot-J__CoMtAly8");
                hlySql.AddInParameter(cmd, "@TranStep", DbType.String, "");
                hlySql.AddInParameter(cmd, "@AwbnoType", DbType.String, "普通单");
                hlySql.AddInParameter(cmd, "@TypeExplain", DbType.String, "");
                hlySql.AddInParameter(cmd, "@CargoOrderNo", DbType.String, entity.OrderNo);
                hlySql.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询外采表好来运运单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public CargoOrderEntity QueryHlyOutOrderData(string orderNo)
        {
            SqlHelper hlySql = new SqlHelper("HLYSql");
            CargoOrderEntity result = new CargoOrderEntity();
            string strSQL = "Select top 1 * From Tbl_ApiGuestAwbno Where CargoOrderNo=@CargoOrderNo";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(command, "@CargoOrderNo", DbType.String, orderNo);
                using (DataTable dt = hlySql.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //result.AwbStatus = Convert.ToString(idr["state"]);
                        result.HAwbNo = Convert.ToString(idr["Awbno"]);
                        result.OrderNo = Convert.ToString(idr["CargoOrderNo"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询外采订单好来运运单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public CargoOrderEntity QueryHlyOutOrderAwbno(string orderNo)
        {
            SqlHelper hlySql = new SqlHelper("HLYSql");
            CargoOrderEntity result = new CargoOrderEntity();
            string strSQL = "Select top 1 * From Tbl_FPNO Where FPNO=@CargoOrderNo";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(command, "@CargoOrderNo", DbType.String, orderNo);
                using (DataTable dt = hlySql.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //result.AwbStatus = Convert.ToString(idr["state"]);
                        result.HAwbNo = Convert.ToString(idr["Awbno"]);
                        result.OrderNo = Convert.ToString(idr["FPNO"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="orderNo"></param>
        public void DelHlyOutOrderData(string orderNo)
        {
            SqlHelper hlySql = new SqlHelper("HLYSql");
            string strSQL = "Delete From Tbl_ApiGuestAwbno where CargoOrderNo =@CargoOrderNo";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(command, "@CargoOrderNo", DbType.String, orderNo);
                hlySql.ExecuteNonQuery(command);
            }
        }
        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="order"></param>
        public void UpdateHlyOrderData(CargoOrderEntity order)
        {

        }
        /// <summary>
        /// 新增订单推送数据记录表
        /// </summary>
        /// <param name="entity"></param>
        public void InsertCargoOrderPush(CargoOrderPushEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_Cargo_OrderPush(OrderNo,AwbNo,Dep,Dest,Piece,TransportFee,ClientNum,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,HouseID,HouseName,OP_ID,PushType,PushStatus,OP_DATE,LogisID,HLYSendUnit,Type) VALUES  (@OrderNo,@AwbNo,@Dep,@Dest,@Piece,@TransportFee,@ClientNum,@AcceptUnit,@AcceptAddress,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@HouseID,@HouseName,@OP_ID,@PushType,@PushStatus,@OP_DATE,@LogisID,@HLYSendUnit,@Type)";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@HLYSendUnit", DbType.String, entity.HLYSendUnit);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.AddInParameter(cmd, "@AwbNo", DbType.String, entity.AwbNo);
                conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                conn.AddInParameter(cmd, "@ClientNum", DbType.String, entity.ClientNum);
                conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                conn.AddInParameter(cmd, "@HouseID", DbType.String, entity.HouseID);
                conn.AddInParameter(cmd, "@HouseName", DbType.String, entity.HouseName);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.AddInParameter(cmd, "@PushType", DbType.String, entity.PushType);
                conn.AddInParameter(cmd, "@PushStatus", DbType.String, entity.PushStatus);
                conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                conn.AddInParameter(cmd, "@LogisID", DbType.String, entity.LogisID);
                conn.AddInParameter(cmd, "@Type", DbType.Int32, entity.Type);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCargoOrderPush(CargoOrderPushEntity entity)
        {
            if (IsExistCargoOrderPush(entity))
            {
                string strSQL = @"INSERT INTO Tbl_Cargo_OrderPush(OrderNo,AwbNo,Dep,Dest,TransportFee,ClientNum,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,
AcceptCellphone,HouseID,HouseName,OP_ID,Piece,PushType,PushStatus,LogisID) select OrderNo,AwbNo,Dep,@Dest,TransportFee,@ClientNum,@AcceptUnit,@AcceptAddress,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,HouseID,HouseName,OP_ID,@Piece,@PushType,@PushStatus,@LogisID From Tbl_Cargo_OrderPush where OrderNo=@OrderNo and PushType='0'";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@PushType", DbType.String, entity.PushType);
                    conn.AddInParameter(cmd, "@PushStatus", DbType.String, entity.PushStatus);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.String, entity.ClientNum);
                    conn.AddInParameter(cmd, "@LogisID", DbType.String, entity.LogisID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
        }
        /// <summary>
        /// 判断是否存在新增 的数据 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool IsExistCargoOrderPush(CargoOrderPushEntity entity)
        {
            string strSQL = "Select OrderNo from Tbl_Cargo_OrderPush where OrderNo=@OrderNo and PushType='0'";

            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@OrderNo", DbType.String, entity.OrderNo);

                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 修改订单号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCargoOrderPushAwbNo(CargoOrderPushEntity entity)
        {
            string strSQL = "Update Tbl_Cargo_OrderPush set PushStatus=@PushStatus  ";
            if (!string.IsNullOrEmpty(entity.AwbNo)) { strSQL += " ,AwbNo=@AwbNo "; }
            strSQL += " Where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                if (!string.IsNullOrEmpty(entity.AwbNo)) { conn.AddInParameter(cmd, "@AwbNo", DbType.String, entity.AwbNo); }
                conn.AddInParameter(cmd, "@PushStatus", DbType.String, entity.PushStatus);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改订单改价申请状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCargoOrderModifyPriceStatus(CargoOrderEntity entity)
        {
            string strSQL = "Update Tbl_Cargo_Order set ModifyPriceStatus=@ModifyPriceStatus Where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ModifyPriceStatus", DbType.String, entity.ModifyPriceStatus);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询所有未推送数据 
        /// </summary>
        /// <returns></returns>
        public List<CargoOrderPushEntity> QueryCargoOrderPushInfoList(CargoOrderPushEntity entity)
        {
            List<CargoOrderPushEntity> result = new List<CargoOrderPushEntity>();
            string strSQL = "select * From Tbl_Cargo_OrderPush Where (1=1)";
            if (!string.IsNullOrEmpty(entity.PushStatus))
            {
                strSQL += " and PushStatus='" + entity.PushStatus + "'";
            }
            if (!string.IsNullOrEmpty(entity.OrderNo))
            {
                strSQL += " and OrderNo='" + entity.OrderNo + "'";
            }
            //strSQL += "ORDER BY OP_DATE DESC";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(idr["OrderNo"]))) { continue; }
                        result.Add(new CargoOrderPushEntity
                        {
                            ID = Convert.ToInt64(idr["ID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            AwbNo = Convert.ToString(idr["AwbNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                            ClientNum = Convert.ToString(idr["ClientNum"]),
                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                            HouseID = Convert.ToString(idr["HouseID"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            PushStatus = Convert.ToString(idr["PushStatus"]),
                            PushType = Convert.ToString(idr["PushType"]),
                            HLYSendUnit = Convert.ToString(idr["HLYSendUnit"]),
                            LogisID = string.IsNullOrEmpty(Convert.ToString(idr["LogisID"])) ? 0 : Convert.ToInt32(idr["LogisID"]),
                            OP_ID = Convert.ToString(idr["OP_ID"]),
                            Type = string.IsNullOrEmpty(Convert.ToString(idr["LogisID"])) ? 0 : Convert.ToInt32(idr["Type"]),
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询订单出库标签码列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoContainerShowEntity> QueryTagByOrderNo(CargoOrderEntity entity)
        {
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            string strSQL = "select c.HouseID,a.ContainerID,c.OutCargoID,c.ActSalePrice,c.OrderNo,c.ContainerCode,c.Piece,a.TagCode,a.TyreCode,a.InCargoTime,a.OutCargoOperID,a.OutCargoTime,b.ProductID,b.Specs,b.Figure,b.Model,b.GoodsCode,b.LoadIndex,b.SpeedLevel,b.Batch,g.TypeName,d.Name as AreaName,f.Name as ParentAreaName,b.PackageNum,b.ProductCode From Tbl_Cargo_OrderGoods as c inner join Tbl_Cargo_Container as h on c.ContainerCode=h.ContainerCode and c.AreaID=h.AreaID inner join Tbl_Cargo_ProductTag as a on c.ProductID=a.ProductID and c.OrderNo=a.OrderNo and h.ContainerID=a.ContainerID inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID left join Tbl_Cargo_Area as d on c.AreaID=d.AreaID left join Tbl_Cargo_Area as e on d.ParentID=e.AreaID left join Tbl_Cargo_Area as f on e.ParentID=f.AreaID left join Tbl_Cargo_ProductType as g on b.TypeID=g.TypeID where c.OrderNo='" + entity.OrderNo + "' and a.OutCargoStatus='1'";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //if (result.Exists(c => c.TagCode.Equals(Convert.ToString(idr["TagCode"])))) { continue; }
                        result.Add(new CargoContainerShowEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            ContainerCode = Convert.ToString(idr["ContainerCode"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            TagCode = Convert.ToString(idr["TagCode"]),
                            InCargoTime = Convert.ToDateTime(idr["InCargoTime"]),
                            OutCargoTime = Convert.ToDateTime(idr["OutCargoTime"]),
                            OutCargoOperID = Convert.ToString(idr["OutCargoOperID"]),
                            ProductID = Convert.ToInt64(idr["ProductID"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            Model = Convert.ToString(idr["Model"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            AreaName = Convert.ToString(idr["AreaName"]),
                            TyreCode = Convert.ToString(idr["TyreCode"]),
                            OutCargoID = Convert.ToString(idr["OutCargoID"]),
                            ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                            ContainerID = Convert.ToInt32(idr["ContainerID"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            ProductCode = Convert.ToString(idr["ProductCode"]),
                            PackageNum = Convert.ToInt32(idr["PackageNum"]),
                            ParentAreaName = Convert.ToString(idr["ParentAreaName"])
                        });
                    }
                }
            }
            return result;
        }
        public List<CargoContainerShowEntity> QueryTyreCodeByOrderNo(List<string> OrderNo)
        {
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            string strSQL = "select * from Tbl_Cargo_ProductTag where OrderNo in (";
            strSQL = "select c.HouseID,a.ContainerID,c.OutCargoID,c.ActSalePrice,c.OrderNo,c.ContainerCode,c.Piece,a.TagCode,a.TyreCode,a.InCargoTime,a.OutCargoOperID,a.OutCargoTime,b.ProductID,b.Specs,b.Figure,b.Model,b.GoodsCode,b.LoadIndex,b.SpeedLevel,b.Batch,b.SuppClientNum,b.Supplier,g.TypeName,d.Name as AreaName,f.Name as ParentAreaName,o.PayClientName From Tbl_Cargo_OrderGoods as c inner join Tbl_Cargo_Order o on c.OrderNo=o.OrderNo inner join Tbl_Cargo_Container as h on c.ContainerCode=h.ContainerCode and c.AreaID=h.AreaID inner join Tbl_Cargo_ProductTag as a on c.ProductID=a.ProductID and c.OrderNo=a.OrderNo and h.ContainerID=a.ContainerID inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID left join Tbl_Cargo_Area as d on c.AreaID=d.AreaID left join Tbl_Cargo_Area as e on d.ParentID=e.AreaID left join Tbl_Cargo_Area as f on e.ParentID=f.AreaID left join Tbl_Cargo_ProductType as g on b.TypeID=g.TypeID where c.OrderNo in (";
            string orderNoStr = "";
            foreach (var item in OrderNo)
            {
                orderNoStr += "'" + item + "',";
            }
            orderNoStr = orderNoStr.Substring(0, orderNoStr.Length - 1);
            strSQL += orderNoStr + ") and a.OutCargoStatus='1' order by c.OrderNo desc";

            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //if (result.Exists(c => c.TagCode.Equals(Convert.ToString(idr["TagCode"])))) { continue; }
                        result.Add(new CargoContainerShowEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            ContainerCode = Convert.ToString(idr["ContainerCode"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            TagCode = Convert.ToString(idr["TagCode"]),
                            InCargoTime = Convert.ToDateTime(idr["InCargoTime"]),
                            OutCargoTime = Convert.ToDateTime(idr["OutCargoTime"]),
                            OutCargoOperID = Convert.ToString(idr["OutCargoOperID"]),
                            ProductID = Convert.ToInt64(idr["ProductID"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            Model = Convert.ToString(idr["Model"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            AreaName = Convert.ToString(idr["AreaName"]),
                            TyreCode = Convert.ToString(idr["TyreCode"]),
                            OutCargoID = Convert.ToString(idr["OutCargoID"]),
                            ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                            ContainerID = Convert.ToInt32(idr["ContainerID"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            ParentAreaName = Convert.ToString(idr["ParentAreaName"]),
                            PayClientName = Convert.ToString(idr["PayClientName"]),
                            SuppClientNum = Convert.ToInt32(idr["SuppClientNum"]),
                            Supplier = Convert.ToString(idr["Supplier"]),
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// APP查询我的订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXOrderEntity> QueryMyOrderInfoForAPP(WXOrderEntity entity)
        {
            List<WXOrderEntity> result = new List<WXOrderEntity>();
            entity.EnSafe();
            string strSQL = "select OrderID,OrderNo,AwbStatus,Piece,AcceptPeople,AcceptCellphone,AcceptAddress,TotalCharge,CreateDate From Tbl_Cargo_Order where (1=1) and OrderType in (0,1) ";
            if (!string.IsNullOrEmpty(entity.OrderStatus))
            {
                if (entity.OrderStatus.Equals("0")) { strSQL += " and AwbStatus in (0,1)"; }
                else { strSQL += " and AwbStatus='" + entity.OrderStatus + "'"; }
            }
            if ((entity.CreateDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.CreateDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CreateDate>='" + entity.CreateDate.ToString("yyyy-MM-dd") + "'";
            }
            else { strSQL += " and AwbStatus>=2"; }
            if (!entity.ClientNum.Equals(0)) { strSQL += " and PayClientNum=" + entity.ClientNum; }
            strSQL += " order by CreateDate desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new WXOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            Name = Convert.ToString(idr["AcceptPeople"]),
                            Cellphone = Convert.ToString(idr["AcceptCellphone"]),
                            Address = Convert.ToString(idr["AcceptAddress"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            OrderStatus = Convert.ToString(idr["AwbStatus"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            productList = queryOrderProductForAPP(Convert.ToString(idr["OrderNo"]))
                        });
                    }
                }
            }
            return result;
        }
        public List<CargoProductShelvesEntity> queryOrderProductForAPP(string OrderNo)
        {
            CargoPriceManager price = new CargoPriceManager();
            List<CargoProductShelvesEntity> result = new List<CargoProductShelvesEntity>();
            try
            {
                string strSQL = @"select a.OrderNo,a.ProductID,a.Piece,a.ActSalePrice,ISNULL(a.SupplySalePrice,0) as originalPrice,b.TypeID,b.ProductName,c.TypeName,b.Model,b.GoodsCode,b.Specs,b.Figure,b.LoadIndex,b.SpeedLevel,b.Batch,b.BatchWeek,b.BatchYear,a.HouseID,b.TradePrice,b.SalePrice,b.TreadWidth,b.HubDiameter,InHouseTime,InCargoStatus From Tbl_Cargo_OrderGoods as a inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID inner join Tbl_Cargo_ProductType as c on b.TypeID=c.TypeID where a.OrderNo=@OrderNo";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            string batch = Convert.ToString(idr["BatchYear"]) + Convert.ToString(idr["BatchWeek"]);
                            List<CargoRuleBankEntity> rule = price.QueryRuleBank(new CargoRuleBankEntity { HouseID = Convert.ToInt32(idr["HouseID"]), TypeID = Convert.ToInt32(idr["TypeID"]), Specs = Convert.ToString(idr["Specs"]), Figure = Convert.ToString(idr["Figure"]), DelFlag = "0", StartBatch = Convert.ToInt32(batch) });
                            CargoRuleBankEntity rb = new CargoRuleBankEntity();
                            if (rule.Count > 0)
                            {
                                rb = rule.Find(c => c.RuleType.Equals("6"));
                            }
                            result.Add(new CargoProductShelvesEntity
                            {
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                OrderNum = Convert.ToInt32(idr["Piece"]),
                                OrderPrice = Convert.ToDecimal(idr["ActSalePrice"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                Title = Convert.ToString(idr["TypeName"]) + Convert.ToString(idr["Specs"]) + " " + Convert.ToString(idr["Figure"]) + " " + Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                                FileName = "/upload/trye/mapai/CC6.png",
                                ProductName = Convert.ToString(idr["ProductName"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                Model = Convert.ToString(idr["Model"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                TreadWidth = Convert.ToInt32(idr["TreadWidth"]),
                                HubDiameter = Convert.ToInt32(idr["HubDiameter"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                TradePrice = Convert.ToDecimal(idr["TradePrice"]),
                                originalPrice = Convert.ToDecimal(idr["originalPrice"]),
                                CutEntry = rb.CutEntry,
                                IsCut = rb.ID.Equals(0) ? false : true,
                                RuleID = rb.ID,
                                SalePrice = Convert.ToDecimal(idr["SalePrice"]),
                                InHouseTime = Convert.ToDateTime(idr["InHouseTime"]),
                                InCargoStatus = Convert.ToInt32(idr["InCargoStatus"]),
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }

        /// <summary>
        /// 修改订单延迟发货状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCargoOrderPostponeShipStatus(CargoOrderEntity entity)
        {
            string strSQL = "Update Tbl_Cargo_Order set OrderNo=@OrderNo ";
            if (!string.IsNullOrEmpty(entity.PostponeShip))
            {
                strSQL += ",PostponeShip=@PostponeShip";
            }
            if ((entity.CheckDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.CheckDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += ",CheckDate=@CheckDate";
            }
            if (!string.IsNullOrEmpty(entity.PollStatus))
            {
                strSQL += ",PollStatus=@PollStatus";
            }
            strSQL += " Where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                if ((entity.CheckDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.CheckDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    conn.AddInParameter(cmd, "@CheckDate", DbType.DateTime, entity.CheckDate);
                }
                if (!string.IsNullOrEmpty(entity.PostponeShip))
                {
                    conn.AddInParameter(cmd, "@PostponeShip", DbType.String, entity.PostponeShip);
                }
                if (!string.IsNullOrEmpty(entity.PollStatus))
                {
                    conn.AddInParameter(cmd, "@PollStatus", DbType.String, entity.PollStatus);
                }
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 查询已出库的小程序订单推送至小程序
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXOrderEntity> QueryMiniOrderSendWx(WXOrderEntity entity)
        {
            List<WXOrderEntity> result = new List<WXOrderEntity>();
            string strSQL = "select b.ID,a.OrderNo,a.WXOrderNo,c.OrderNo,c.WXPayOrderNo,d.OrderNum,e.wxOpenID,f.Specs,f.Figure,f.LoadIndex,f.SpeedLevel,g.TypeName from Tbl_Cargo_Order as a inner join Tbl_Cargo_OrderStatus as b on a.OrderNo=b.OrderNo and a.OrderID=b.OrderID and b.WxSendStatusPush=0 inner join Tbl_Cargo_House as h on a.HouseID=h.HouseID and h.BelongHouse=6 ";
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            //if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
            if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and b.OrderStatus='" + entity.OrderStatus + "'"; }
            strSQL += " inner join Tbl_WX_Order as c on a.WXOrderNo=c.OrderNo and a.HouseID=c.HouseID and c.WXPayOrderNo!='' inner join Tbl_WX_OrderProduct as d on c.ID=d.OrderID inner join Tbl_WX_Client as e on c.WXID=e.ID inner join Tbl_Cargo_ProductSpec as f on d.ProductCode=f.ProductCode inner join Tbl_Cargo_ProductType as g on f.TypeID=g.TypeID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new WXOrderEntity
                        {
                            ID = Convert.ToInt64(idr["ID"]),
                            CargoOrderNo = Convert.ToString(idr["OrderNo"]),
                            OrderNo = Convert.ToString(idr["WXOrderNo"]),
                            Piece = Convert.ToInt32(idr["OrderNum"]),
                            WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            wxOpenID = Convert.ToString(idr["wxOpenID"]),
                            Memo = Convert.ToString(idr["TypeName"]) + " " + Convert.ToString(idr["Specs"]) + " " + Convert.ToString(idr["Figure"]) + " " + Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 更新推送状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateOrderStatusWxPushStatus(CargoOrderStatusEntity entity)
        {
            string strSQL = "Update Tbl_Cargo_OrderStatus set WxSendStatusPush=@WxSendStatusPush Where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@WxSendStatusPush", DbType.String, entity.WxSendStatusPush);
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        #endregion
        #region 退货管理操作方法集合
        /// <summary>
        /// 查询订单数据用以退货管理
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderReturnOrderEntity> QueryOrderInfoForReturn(CargoOrderReturnOrderEntity entity)
        {
            CargoHouseManager house = new CargoHouseManager();
            List<CargoOrderReturnOrderEntity> result = new List<CargoOrderReturnOrderEntity>();
            try
            {
                string strSQL = @"select a.OrderID,a.OrderNo,a.Piece as TotalPiece,a.TransportFee,a.TotalCharge,a.AcceptPeople,a.AcceptCellphone,a.ClientNum,
a.SaleManID,a.SaleManName,a.CreateAwbID,a.CreateAwb,a.CreateDate,b.ProductID,b.HouseID,b.AreaID,b.ContainerCode,b.Piece,b.ActSalePrice,b.OutCargoID,c.ContainerID,d.Supplier,d.UnitPrice,d.SalePrice,d.CostPrice,d.TradePrice,d.Figure,d.Specs,d.GoodsCode,d.SpeedLevel,d.LoadIndex,d.Model,d.Batch,d.BatchYear,d.Born,d.ProductName,d.TypeID,d.ProductCode,d.SuppClientNum,d.SupplierAddress,d.InHousePrice,d.SpecsType,d.BelongDepart,e.TypeName,f.Name as AreaName,g.Name as HouseName,g.HouseCode,f.ParentID from Tbl_Cargo_Order as a inner join Tbl_Cargo_OrderGoods as b on a.OrderNo=b.OrderNo inner join Tbl_Cargo_Container as c on b.ContainerCode=c.ContainerCode and c.AreaID=b.AreaID inner join Tbl_Cargo_Product as d on b.ProductID=d.ProductID left join Tbl_Cargo_ProductType as e on d.TypeID=e.TypeID left join Tbl_Cargo_Area as f on c.AreaID=f.AreaID left join Tbl_Cargo_House as g on b.HouseID=g.HouseID Where (1=1) ";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and a.OrderType = '" + entity.OrderType + "'"; }
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
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID ='" + entity.SaleManID + "'"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.OrderModel)) { strSQL += " and a.OrderModel ='" + entity.OrderModel + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and a.Piece=" + entity.Piece + ""; }
                if (!string.IsNullOrEmpty(entity.BelongDepart)) { strSQL += " and d.BelongDepart = '" + entity.BelongDepart + "'"; }
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    string res = entity.Specs.ToUpper().Replace("/", "").Replace("R", "").Replace("C", "").Replace("F", "");
                    if (res.Length <= 3)
                    {
                        if (!string.IsNullOrEmpty(res)) { strSQL += " and d.Specs like '%" + res + "%'"; }
                    }
                    if (res.Length > 3 && res.Length <= 5)
                    {
                        strSQL += " and d.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3) + "%'";
                    }
                    if (res.Length > 5)
                    {
                        strSQL += " and (d.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5) + "%' or d.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "RF" + res.Substring(5, res.Length - 5) + "%' or d.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZR" + res.Substring(5, res.Length - 5) + "%')";
                    }
                }
                if (!entity.ClientNum.Equals(0))
                {
                    strSQL += " and a.PayClientNum ='" + entity.ClientNum + "'";
                }
                strSQL += " order by a.CreateDate desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            CargoAreaEntity cargoArea = new CargoAreaEntity();
                            CargoAreaEntity hArea = new CargoAreaEntity();
                            if (!string.IsNullOrEmpty(idr["ParentID"].ToString()))
                            {
                                //如果父ID不为0，则查询父ID的区域名称
                                if (!Convert.ToInt32(idr["ParentID"]).Equals(0))
                                {
                                    cargoArea = house.QueryAreaByAreaID(new CargoAreaEntity { AreaID = Convert.ToInt32(idr["ParentID"]) });
                                    if (!cargoArea.ParentID.Equals(0))
                                    {
                                        hArea = house.QueryAreaByAreaID(new CargoAreaEntity { AreaID = cargoArea.ParentID });
                                    }
                                }
                                #region 获取运单数据

                                result.Add(new CargoOrderReturnOrderEntity
                                {
                                    OrderID = Convert.ToInt64(idr["OrderID"]),
                                    OrderNo = Convert.ToString(idr["OrderNo"]),
                                    TotalPiece = Convert.ToInt32(idr["TotalPiece"]),
                                    Piece = Convert.ToInt32(idr["Piece"]),
                                    TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                    TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                    ProductID = Convert.ToInt64(idr["ProductID"]),
                                    HouseID = Convert.ToInt32(idr["HouseID"]),
                                    HouseCode = Convert.ToString(idr["HouseCode"]),
                                    AreaID = Convert.ToInt32(idr["AreaID"]),
                                    ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                    ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                                    ContainerID = Convert.ToInt32(idr["ContainerID"]),
                                    Figure = Convert.ToString(idr["Figure"]),
                                    Specs = Convert.ToString(idr["Specs"]),
                                    Model = Convert.ToString(idr["Model"]),
                                    Batch = Convert.ToString(idr["Batch"]),
                                    BatchYear = Convert.ToString(idr["BatchYear"]),
                                    Born = Convert.ToString(idr["Born"]),
                                    ProductName = Convert.ToString(idr["ProductName"]),
                                    TypeID = Convert.ToInt32(idr["TypeID"]),
                                    TypeName = Convert.ToString(idr["TypeName"]),
                                    AreaName = Convert.ToString(idr["AreaName"]),
                                    AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                    AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                    ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                    SaleManID = Convert.ToString(idr["SaleManID"]),
                                    SaleManName = Convert.ToString(idr["SaleManName"]),
                                    CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                    CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                    CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                    FirstAreaName = hArea.Name,
                                    BelongDepart = Convert.ToString(idr["BelongDepart"]),
                                    HouseName = Convert.ToString(idr["HouseName"]),
                                    GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                    SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                    LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                    UnitPrice = Convert.ToDecimal(idr["UnitPrice"]),
                                    SalePrice = Convert.ToDecimal(idr["SalePrice"]),
                                    CostPrice = Convert.ToDecimal(idr["CostPrice"]),
                                    TradePrice = Convert.ToDecimal(idr["TradePrice"]),
                                    InHousePrice = string.IsNullOrEmpty(Convert.ToString(idr["InHousePrice"])) ? 0 : Convert.ToDecimal(idr["InHousePrice"]),
                                    Supplier = Convert.ToString(idr["Supplier"]),
                                    ProductCode = Convert.ToString(idr["ProductCode"]),
                                    SuppClientNum = string.IsNullOrEmpty(Convert.ToString(idr["SuppClientNum"])) ? 0 : Convert.ToInt32(idr["SuppClientNum"]),
                                    SupplierAddress = Convert.ToString(idr["SupplierAddress"]),
                                    SpecsType = Convert.ToString(idr["SpecsType"]),
                                });
                                #endregion
                            }
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;

        }
        /// <summary>
        /// 保存退货单
        /// </summary>
        /// <param name="entity"></param>
        public void AddReturnOrderInfo(CargoOrderEntity entity)
        {

            CargoClientManager client = new CargoClientManager();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_Order(OrderNo,OrderNum,HAwbNo,Dep,Dest,Piece,TransportFee,TotalCharge,CheckOutType,TrafficType,DeliveryType,AcceptUnit,AcceptPeople,AcceptTelephone,AcceptCellphone,AcceptAddress,CreateAwb,CreateDate,OP_ID,OP_DATE,AwbStatus,Remark,SaleManID,SaleManName,HouseID,CreateAwbID,OrderType,OrderModel,ClientNum,ThrowGood,PayClientNum,PayClientName,BelongHouse,OutHouseName,LogisID) VALUES (@OrderNo,@OrderNum,@HAwbNo,@Dep,@Dest,@Piece,@TransportFee,@TotalCharge,@CheckOutType,@TrafficType,@DeliveryType,@AcceptUnit,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@AcceptAddress,@CreateAwb,@CreateDate,@OP_ID,@OP_DATE,@AwbStatus,@Remark,@SaleManID,@SaleManName,@HouseID,@CreateAwbID,@OrderType,@OrderModel,@ClientNum,@ThrowGood,@PayClientNum,@PayClientName,@BelongHouse,@OutHouseName,@LogisID) SELECT @@IDENTITY";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                    conn.AddInParameter(cmd, "@HAwbNo", DbType.String, entity.HAwbNo);
                    conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    conn.AddInParameter(cmd, "@TrafficType", DbType.String, entity.TrafficType);
                    conn.AddInParameter(cmd, "@DeliveryType", DbType.String, entity.DeliveryType);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@CreateAwbID", DbType.String, entity.CreateAwbID);
                    conn.AddInParameter(cmd, "@CreateAwb", DbType.String, entity.CreateAwb);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, entity.CreateDate);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@AwbStatus", DbType.String, entity.AwbStatus);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                    conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                    conn.AddInParameter(cmd, "@OrderModel", DbType.String, entity.OrderModel);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@ThrowGood", DbType.String, entity.ThrowGood);
                    conn.AddInParameter(cmd, "@PayClientName", DbType.String, entity.PayClientName);
                    conn.AddInParameter(cmd, "@PayClientNum", DbType.Int32, entity.PayClientNum);
                    conn.AddInParameter(cmd, "@BelongHouse", DbType.String, entity.BelongHouse);
                    conn.AddInParameter(cmd, "@OutHouseName", DbType.String, entity.OutHouseName);
                    conn.AddInParameter(cmd, "@LogisID", DbType.String, entity.LogisID);
                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                #endregion

                //新增产品与订单关联数据
                AddOrderGoodsInfo(entity.goodsList);
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 保存退货单的物流数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateReturnLogisAwbNo(CargoOrderEntity entity)
        {
            string strSQL = @"UPDATE Tbl_Cargo_Order Set LogisID=@LogisID,LogisAwbNo=@LogisAwbNo,TransitFee=@TransitFee Where OrderID=@OrderID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询退货订单详情
        /// </summary>
        public List<CargoOrderEntity> QuerReturnOrderInfo(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            try
            {
                string strSQL = @"select *,ar.Name as AreaName,ps.SourceName,b.Remark from Tbl_Cargo_OrderGoods a inner join Tbl_Cargo_Order b on a.OrderNo=b.OrderNo inner join Tbl_Cargo_Product p on p.ProductID=a.ProductID left join Tbl_Cargo_ProductSource ps on p.Source=ps.Source left join Tbl_Cargo_Area ar on a.AreaID=ar.AreaID left join tbl_cargo_Logistic c on b.LogisID=c.ID left join Tbl_WX_Order d on b.WXOrderNo=d.OrderNo where b.OrderModel = '1' and b.ThrowGood = '5' ";

                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and b.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //客户名称
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and b.PayClientName = '" + entity.AcceptUnit + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and b.Piece=" + entity.Piece + ""; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strSQL += " and b.Dep = '" + entity.Dep + "'"; }
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
                    strSQL += " and b.Dest in ('" + res + "')";
                }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and b.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and b.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and b.OrderType = '" + entity.OrderType + "'"; }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and b.HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and b.SaleManID ='" + entity.SaleManID + "'"; }
                strSQL += " order by b.CreateDate desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                RelateOrderNo = Convert.ToString(idr["RelateOrderNo"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Model = Convert.ToString(idr["Model"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                ContainerCode = Convert.ToString(idr["BelongDepart"]),
                                HouseName = Convert.ToString(idr["BelongDepart"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                SpeedMax = Convert.ToInt32(idr["SpeedMax"]),
                                Size = Convert.ToString(idr["Size"]),
                                UnitPrice = Convert.ToDecimal(idr["UnitPrice"]),
                                BelongDepart = Convert.ToString(idr["BelongDepart"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                AreaName = Convert.ToString(idr["AreaName"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                LogisticName = Convert.ToString(idr["LogisticName"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                OrderModel = Convert.ToString(idr["OrderModel"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                PayWay = Convert.ToString(idr["PayWay"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                TranHouse = Convert.ToString(idr["TranHouse"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                PayClientName = Convert.ToString(idr["PayClientName"]),
                                PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                                BelongHouse = Convert.ToString(idr["BelongHouse"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                SourceName = Convert.ToString(idr["SourceName"]),
                                OutCargoTime = string.IsNullOrEmpty(Convert.ToString(idr["OutCargoTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["OutCargoTime"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 未签收订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public List<CargoOrderEntity> QueryNotSignedOrderData(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @"select OrderID,OrderNo,LogisAwbNo,l.LogisticName,AwbStatus,Dep,Dest,Piece,AcceptUnit,AcceptPeople,AcceptCellphone,AcceptTelephone,AcceptAddress,CreateDate,OutCargoTime,Remark,OutHouseName from Tbl_Cargo_Order o left join tbl_cargo_Logistic as l on o.LogisID=l.ID Where (1=1) ";
                //开单日期
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and OrderNo like '%" + entity.OrderNo.ToUpper() + "%'"; }
                //物流单号
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strSQL += " and LogisAwbNo like '%" + entity.LogisAwbNo + "%'"; }
                //客户名称
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and PayClientName like '%" + entity.AcceptUnit + "%'"; }
                //收货人
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and AcceptPeople like '%" + entity.AcceptPeople + "%'"; }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and HouseID in (" + entity.CargoPermisID + ")"; }

                strSQL += " and BelongHouse=0 and AwbStatus!=5  and PostponeShip!=1 and OrderModel=0 and LogisID !=24 and LogisID !=46 order by CreateDate desc";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisticName = Convert.ToString(idr["LogisticName"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                OutCargoTime = string.IsNullOrEmpty(Convert.ToString(idr["OutCargoTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["OutCargoTime"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"])
                            });
                            #endregion
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        #endregion
        #region 微信商城操作方法集合
        /// <summary>
        /// 查询微信商城订单数据
        /// </summary>
        public List<WXOrderManagerEntity> QueryWeixinOrderInfo(WXOrderManagerEntity entity)
        {
            CargoWeiXinManager wx = new CargoWeiXinManager();
            List<WXOrderManagerEntity> result = new List<WXOrderManagerEntity>();
            try
            {
                string strSQL = @"select a.ID,a.OrderNo,a.WXID,a.Piece,a.CouponID,a.RuleTitle,a.TotalCharge,a.TransitFee,a.CreateDate,a.PayStatus,a.PayWay,a.OrderStatus,a.OrderType,a.WXPayOrderNo,a.Province,a.City,a.Country,a.Address,(a.Province+' '+a.City+' '+a.Country+' '+a.Address) as AcceptAddress,a.Name,a.Cellphone,b.ShelvesID,b.OrderNum,b.OrderPrice,b.CutEntry,c.ProductID,c.TypeID,c.ProductName,c.OnSaleNum,c.Title,c.Memo,c.FileName,c.SaleType,ISNULL(c.Consume,0) as Consume,d.ID as InContainID,d.ContainerID,d.Piece as InCargoPiece,e.ContainerCode,f.Name as AreaName,a.HouseID,g.wxOpenID,g.wxName,ISNULL(g.ClientNum,0) as ClientNum,h.Specs,h.Figure,h.GoodsCode,h.Model,h.Batch,h.BatchYear,f.AreaID,h.SpeedLevel from Tbl_WX_Order as a left join Tbl_WX_OrderProduct as b on a.ID=b.OrderID left join Tbl_WX_Client as g on a.WXID=g.ID left join Tbl_Cargo_Shelves as c on b.ShelvesID=c.ID left join Tbl_Cargo_InContainerGoods AS d on c.ProductID=d.ProductID left join Tbl_Cargo_Product as h on c.ProductID=h.ProductID and h.HouseID=a.HouseID left join Tbl_Cargo_Container as e on d.ContainerID=e.ContainerID left join Tbl_Cargo_Area as f on e.AreaID=f.AreaID and a.HouseID=f.HouseID where (1=1) AND b.ShelvesID IS NOT NULL ";// and f.AreaType='1' 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
                if (!string.IsNullOrEmpty(entity.Name)) { strSQL += " and a.Name like '%" + entity.Name + "%'"; }
                if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and a.PayStatus='" + entity.PayStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID='" + entity.SaleManID + "'"; }
                if (!string.IsNullOrEmpty(entity.PayWay)) { strSQL += " and a.PayWay='" + entity.PayWay + "'"; }
                //if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID=" + entity.HouseID + ""; }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and a.OrderStatus='" + entity.OrderStatus + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " Order by a.OrderStatus asc ,a.CreateDate desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            if (result.Exists(c => c.OrderNo.Equals(Convert.ToString(idr["OrderNo"])) && c.ShelvesID.Equals(Convert.ToInt64(idr["ShelvesID"]))))
                            {
                                continue;
                            }
                            WXCouponEntity coupon = new WXCouponEntity();
                            if (!Convert.ToInt64(idr["CouponID"]).Equals(0))
                            {
                                coupon = wx.QueryCouponByID(new WXCouponEntity { ID = Convert.ToInt64(idr["CouponID"]) });
                            }
                            result.Add(new WXOrderManagerEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                CouponID = Convert.ToInt64(idr["CouponID"]),
                                CouponMoney = Convert.ToInt32(coupon.Money),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                PayStatus = Convert.ToString(idr["PayStatus"]),
                                PayWay = Convert.ToString(idr["PayWay"]),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                                Province = Convert.ToString(idr["Province"]),
                                City = Convert.ToString(idr["City"]),
                                Country = Convert.ToString(idr["Country"]),
                                Address = Convert.ToString(idr["Address"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                Name = Convert.ToString(idr["Name"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                ShelvesID = Convert.ToInt64(idr["ShelvesID"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                OnSaleNum = Convert.ToInt32(idr["OnSaleNum"]),
                                Title = Convert.ToString(idr["Title"]),
                                Memo = Convert.ToString(idr["Memo"]),
                                FileName = Convert.ToString(idr["FileName"]),
                                InContainID = Convert.ToInt64(idr["InContainID"]),
                                ContainerID = Convert.ToInt32(idr["ContainerID"]),
                                InCargoPiece = Convert.ToInt32(idr["InCargoPiece"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                AreaName = Convert.ToString(idr["AreaName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                wxOpenID = Convert.ToString(idr["wxOpenID"]),
                                wxName = Convert.ToString(idr["wxName"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Model = Convert.ToString(idr["Model"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                BatchYear = Convert.ToInt32(idr["BatchYear"]),
                                AreaID = Convert.ToInt32(idr["AreaID"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                Consume = Convert.ToInt32(idr["Consume"]),
                                CutEntry = Convert.ToInt32(idr["CutEntry"]),
                                RuleTitle = Convert.ToString(idr["RuleTitle"]),
                                SaleType = Convert.ToString(idr["SaleType"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 按照ProductCode联表查询的方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public List<WXOrderManagerEntity> QueryWeixinOrderInfoNew(WXOrderManagerEntity entity)
        {
            CargoWeiXinManager wx = new CargoWeiXinManager();
            List<WXOrderManagerEntity> result = new List<WXOrderManagerEntity>();
            try
            {
                string strSQL = @"select b.ProductCode,a.ID,a.OrderNo,a.WXID,a.Memo,a.Piece,a.CouponID,a.RuleTitle,a.TotalCharge,a.TransitFee,a.CreateDate,a.PayStatus,a.PayWay,a.OrderStatus,a.OrderType,a.WXPayOrderNo,a.Province,a.City,a.Country,a.Address,(a.Province+' '+a.City+' '+a.Country+' '+a.Address) as AcceptAddress,a.Name,a.Cellphone,b.ShelvesID,b.OrderNum,b.OrderPrice,b.CutEntry,c.TypeID,c.ProductName,a.HouseID,g.wxOpenID,g.wxName,ISNULL(g.ClientNum,0) as ClientNum,c.Specs,c.Figure,c.GoodsCode,c.Model,c.LoadIndex,c.SpeedLevel,d.TypeName,a.GoodEvaluate,a.LogisEvaluate,a.EvaluateMemo from Tbl_WX_Order as a inner join Tbl_WX_OrderProduct as b on a.ID=b.OrderID inner join Tbl_WX_Client as g on a.WXID=g.ID left join Tbl_Cargo_ProductSpec as c on b.ProductCode=c.ProductCode left join Tbl_Cargo_ProductType as d on c.TypeID=d.TypeID where (1=1)";// and f.AreaType='1' 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
                if (!string.IsNullOrEmpty(entity.Name)) { strSQL += " and a.Name like '%" + entity.Name + "%'"; }
                if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and a.PayStatus='" + entity.PayStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID='" + entity.SaleManID + "'"; }
                if (!string.IsNullOrEmpty(entity.PayWay)) { strSQL += " and a.PayWay='" + entity.PayWay + "'"; }
                //if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID=" + entity.HouseID + ""; }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and a.OrderStatus='" + entity.OrderStatus + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " Order by a.OrderStatus asc ,a.CreateDate desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new WXOrderManagerEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                CouponID = Convert.ToInt64(idr["CouponID"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                PayStatus = Convert.ToString(idr["PayStatus"]),
                                PayWay = Convert.ToString(idr["PayWay"]),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                                Province = Convert.ToString(idr["Province"]),
                                City = Convert.ToString(idr["City"]),
                                Country = Convert.ToString(idr["Country"]),
                                Address = Convert.ToString(idr["Address"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                Name = Convert.ToString(idr["Name"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                wxOpenID = Convert.ToString(idr["wxOpenID"]),
                                wxName = Convert.ToString(idr["wxName"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Model = Convert.ToString(idr["Model"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                CutEntry = Convert.ToInt32(idr["CutEntry"]),
                                GoodEvaluate = Convert.ToString(idr["GoodEvaluate"]),
                                LogisEvaluate = Convert.ToString(idr["LogisEvaluate"]),
                                EvaluateMemo = Convert.ToString(idr["EvaluateMemo"]),
                                Title = Convert.ToString(idr["TypeName"]) + " " + Convert.ToString(idr["Specs"]) + " " + Convert.ToString(idr["Figure"]) + " " + Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                                RuleTitle = Convert.ToString(idr["RuleTitle"]),
                                Memo = Convert.ToString(idr["Memo"]),
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询仓库库存用于确认微信商城订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXOrderManagerEntity> QueryContainerGoodsMakeSureWeixinOrder(WXOrderManagerEntity entity)
        {
            List<WXOrderManagerEntity> result = new List<WXOrderManagerEntity>();
            try
            {
                string strSQL = @"select a.ID,a.OrderNo,a.WXID,a.Piece,a.TotalCharge,a.CouponID,a.TransitFee,a.CreateDate,a.PayStatus,a.PayWay,a.OrderStatus,a.OrderType,a.WXPayOrderNo,a.IsAppFirstOrder,a.Province,a.City,a.Country,a.Address,(a.Province+' '+a.City+' '+a.Country+' '+a.Address) as AcceptAddress,a.Name,a.Cellphone,ISNULL(a.LogisID,0) as LogisID,b.ShelvesID,b.OrderNum,b.OrderPrice,c.ProductID,c.TypeID,c.ProductName,c.OnSaleNum,c.Title,a.Memo,c.FileName,c.SaleType,ISNULL(c.Consume,0) as Consume,d.ID as InContainID,d.ContainerID,d.Piece as InCargoPiece,e.ContainerCode,f.Name as AreaName,a.HouseID,g.wxOpenID,g.wxName,ISNULL(g.ClientNum,0) as ClientNum,h.Specs,h.Figure,h.GoodsCode,h.Model,h.Batch,h.BatchYear,f.AreaID,h.SpeedLevel from Tbl_WX_Order as a left join Tbl_WX_OrderProduct as b on a.ID=b.OrderID left join Tbl_WX_Client as g on a.WXID=g.ID left join Tbl_Cargo_Shelves as c on b.ShelvesID=c.ID left join Tbl_Cargo_ContainerGoods AS d on c.ProductID=d.ProductID left join Tbl_Cargo_Product as h on c.ProductID=h.ProductID and h.HouseID=a.HouseID left join Tbl_Cargo_Container as e on d.ContainerID=e.ContainerID left join Tbl_Cargo_Area as f on e.AreaID=f.AreaID and a.HouseID=f.HouseID where (1=1) ";// and f.AreaType='1' 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
                if (!string.IsNullOrEmpty(entity.Name)) { strSQL += " and a.Name like '%" + entity.Name + "%'"; }
                if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and a.PayStatus='" + entity.PayStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.PayWay)) { strSQL += " and a.PayWay='" + entity.PayWay + "'"; }
                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID=" + entity.HouseID + ""; }
                if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and a.OrderStatus='" + entity.OrderStatus + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " Order by a.OrderStatus asc ,a.CreateDate desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            if (result.Exists(c => c.OrderNo.Equals(Convert.ToString(idr["OrderNo"])) && c.ShelvesID.Equals(Convert.ToInt64(idr["ShelvesID"]))))
                            {
                                continue;
                            }
                            result.Add(new WXOrderManagerEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                PayStatus = Convert.ToString(idr["PayStatus"]),
                                PayWay = Convert.ToString(idr["PayWay"]),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                                Province = Convert.ToString(idr["Province"]),
                                City = Convert.ToString(idr["City"]),
                                Country = Convert.ToString(idr["Country"]),
                                Address = Convert.ToString(idr["Address"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                Name = Convert.ToString(idr["Name"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                ShelvesID = Convert.ToInt64(idr["ShelvesID"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                OnSaleNum = Convert.ToInt32(idr["OnSaleNum"]),
                                Title = Convert.ToString(idr["Title"]),
                                Memo = Convert.ToString(idr["Memo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                FileName = Convert.ToString(idr["FileName"]),
                                InContainID = Convert.ToInt64(idr["InContainID"]),
                                ContainerID = Convert.ToInt32(idr["ContainerID"]),
                                InCargoPiece = Convert.ToInt32(idr["InCargoPiece"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                AreaName = Convert.ToString(idr["AreaName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                wxOpenID = Convert.ToString(idr["wxOpenID"]),
                                wxName = Convert.ToString(idr["wxName"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Model = Convert.ToString(idr["Model"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                BatchYear = Convert.ToInt32(idr["BatchYear"]),
                                AreaID = Convert.ToInt32(idr["AreaID"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                Consume = Convert.ToInt32(idr["Consume"]),
                                SaleType = Convert.ToString(idr["SaleType"]),
                                CouponID = Convert.ToInt64(idr["CouponID"]),
                                IsAppFirstOrder = Convert.ToString(idr["IsAppFirstOrder"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 新增订单数据
        /// </summary>
        /// <param name="entity"></param>
        public long AddOrderInfoFromWeiXin(CargoOrderEntity entity)
        {
            CargoClientManager client = new CargoClientManager();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_Order(OrderNo,OrderNum,HAwbNo,LogisAwbNo,LogisID,Dep,Dest,Piece,Weight,Volume,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,TrafficType,DeliveryType,AcceptUnit,AcceptPeople,AcceptTelephone,AcceptCellphone,AcceptAddress,CreateAwb,CreateDate,OP_ID,OP_DATE,AwbStatus,Remark,SaleManID,SaleManName,HouseID,CreateAwbID,OrderType,OrderModel,ClientNum,WXOrderNo,FinanceSecondCheck,FinanceSecondCheckName,FinanceSecondCheckDate,OutHouseName,PayClientNum,PayClientName) VALUES (@OrderNo,@OrderNum,@HAwbNo,@LogisAwbNo,@LogisID,@Dep,@Dest,@Piece,@Weight,@Volume,@InsuranceFee,@TransitFee,@TransportFee,@DeliveryFee,@OtherFee,@TotalCharge,@Rebate,@CheckOutType,@TrafficType,@DeliveryType,@AcceptUnit,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@AcceptAddress,@CreateAwb,@CreateDate,@OP_ID,@OP_DATE,@AwbStatus,@Remark,@SaleManID,@SaleManName,@HouseID,@CreateAwbID,@OrderType,@OrderModel,@ClientNum,@WXOrderNo,@FinanceSecondCheck,@FinanceSecondCheckName,@FinanceSecondCheckDate,@OutHouseName,@PayClientNum,@PayClientName) SELECT @@IDENTITY";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                    conn.AddInParameter(cmd, "@HAwbNo", DbType.String, entity.HAwbNo);
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@Weight", DbType.Decimal, entity.Weight);
                    conn.AddInParameter(cmd, "@Volume", DbType.Decimal, entity.Volume);
                    conn.AddInParameter(cmd, "@InsuranceFee", DbType.Decimal, entity.InsuranceFee);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    conn.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@Rebate", DbType.Decimal, entity.Rebate);
                    conn.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    conn.AddInParameter(cmd, "@TrafficType", DbType.String, entity.TrafficType);
                    conn.AddInParameter(cmd, "@DeliveryType", DbType.String, entity.DeliveryType);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@CreateAwbID", DbType.String, entity.CreateAwbID);
                    conn.AddInParameter(cmd, "@CreateAwb", DbType.String, entity.CreateAwb);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@AwbStatus", DbType.String, entity.AwbStatus);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                    conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                    conn.AddInParameter(cmd, "@OrderModel", DbType.String, entity.OrderModel);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@WXOrderNo", DbType.String, entity.WXOrderNo);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@OutHouseName", DbType.String, entity.OutHouseName);
                    conn.AddInParameter(cmd, "@FinanceSecondCheck", DbType.String, entity.FinanceSecondCheck);
                    conn.AddInParameter(cmd, "@FinanceSecondCheckName", DbType.String, entity.FinanceSecondCheckName);
                    conn.AddInParameter(cmd, "@FinanceSecondCheckDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@PayClientNum", DbType.Int32, entity.PayClientNum);
                    conn.AddInParameter(cmd, "@PayClientName", DbType.String, entity.PayClientName);
                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                #endregion

                //新增产品与订单关联数据
                AddOrderGoodsInfo(entity.goodsList);

                #region 新增订单已下单状态
                CargoOrderStatusEntity status = new CargoOrderStatusEntity();
                status.OrderID = did;
                status.OrderNo = entity.OrderNo;
                status.OrderStatus = "0";
                status.OP_ID = entity.OP_ID;
                status.OP_Name = entity.CreateAwb;
                status.OP_DATE = DateTime.Now;
                AddOrderStaus(status);
                CargoOrderStatusEntity sta = new CargoOrderStatusEntity();
                sta.OrderID = did;
                sta.OrderNo = entity.OrderNo;
                sta.OrderStatus = "1";
                sta.OP_ID = entity.OP_ID;
                sta.OP_Name = entity.CreateAwb;
                sta.OP_DATE = DateTime.Now;
                AddOrderStaus(sta);
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return did;
        }
        /// <summary>
        /// 通过微信商城订单号查询 该订单的微信用户OpendID
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public string QueryWeixinOpenIDByOrderNo(string orderno)
        {
            string result = string.Empty;
            try
            {
                string strSQL = @"select top 1 b.wxOpenID from Tbl_WX_Order as a inner join Tbl_WX_Client as b on a.WXID=b.ID where a.OrderNo=@OrderNo";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, orderno);
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        if (dd.Rows.Count > 0) { result = Convert.ToString(dd.Rows[0]["wxOpenID"]); }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询微信订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<WXOrderEntity> QueryWeixinOrder(WXOrderEntity entity)
        {
            List<WXOrderEntity> result = new List<WXOrderEntity>();
            string strSQL = "select * from Tbl_WX_Order Where (1=1)";
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and OrderNo='" + entity.OrderNo + "'"; }
            if (!string.IsNullOrEmpty(entity.Name)) { strSQL += " and Name like '%" + entity.Name + "%'"; }
            if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and PayStatus='" + entity.PayStatus + "'"; }
            if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and SaleManID='" + entity.SaleManID + "'"; }
            if (!string.IsNullOrEmpty(entity.PayWay)) { strSQL += " and PayWay='" + entity.PayWay + "'"; }
            //所属仓库ID
            if (!entity.HouseID.Equals(0)) { strSQL += " and HouseID =" + entity.HouseID; }
            if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and OrderStatus='" + entity.OrderStatus + "'"; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            strSQL += " Order by OrderStatus asc ,CreateDate desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new WXOrderEntity
                        {
                            ID = Convert.ToInt64(idr["ID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            WXID = Convert.ToInt64(idr["WXID"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            PayStatus = Convert.ToString(idr["PayStatus"]),
                            PayWay = Convert.ToString(idr["PayWay"]),
                            OrderStatus = Convert.ToString(idr["OrderStatus"]),
                            WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            Province = Convert.ToString(idr["Province"]),
                            City = Convert.ToString(idr["City"]),
                            Country = Convert.ToString(idr["Country"]),
                            Address = Convert.ToString(idr["Address"]),
                            Name = Convert.ToString(idr["Name"]),
                            Cellphone = Convert.ToString(idr["Cellphone"]),
                            Memo = Convert.ToString(idr["Memo"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            LogisID = Convert.ToInt32(idr["LogisID"]),
                            SaleManID = Convert.ToString(idr["SaleManID"]),
                            OrderType = Convert.ToString(idr["OrderType"])
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询单个微信订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXOrderEntity QueryWxOrderData(WXOrderEntity entity)
        {
            WXOrderEntity result = new WXOrderEntity();
            string strSQL = "select * from Tbl_WX_Order Where (1=1) and ID=@ID ";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@ID", DbType.Int64, entity.ID);
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result = new WXOrderEntity
                        {
                            ID = Convert.ToInt64(idr["ID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            WXID = Convert.ToInt64(idr["WXID"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            PayStatus = Convert.ToString(idr["PayStatus"]),
                            PayWay = Convert.ToString(idr["PayWay"]),
                            OrderStatus = Convert.ToString(idr["OrderStatus"]),
                            WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                            Province = Convert.ToString(idr["Province"]),
                            City = Convert.ToString(idr["City"]),
                            Country = Convert.ToString(idr["Country"]),
                            Address = Convert.ToString(idr["Address"]),
                            Name = Convert.ToString(idr["Name"]),
                            Cellphone = Convert.ToString(idr["Cellphone"]),
                            Memo = Convert.ToString(idr["Memo"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            LogisID = Convert.ToInt32(idr["LogisID"]),
                            SaleManID = Convert.ToString(idr["SaleManID"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                            RefundCheckStatus = Convert.ToString(idr["RefundCheckStatus"]),
                            FacOrderNo = Convert.ToString(idr["FacOrderNo"]),
                        };
                    }
                }
            }
            return result;
        }
        #endregion
        #region 改价申请审批方法集合
        /// <summary>
        /// 删除改价申请审批数据记录表
        /// </summary>
        /// <param name="entity"></param>
        public void DelUpdatePriceApply(QyOrderUpdatePriceEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Delete From Tbl_QY_OrderUpdatePrice Where OID=@OID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OID", DbType.Int64, entity.OID);
                conn.ExecuteNonQuery(cmd);
            }
            DelUpdatePriceGood(new QyOrderUpdateGoodsEntity { OID = entity.OID });
        }
        private void DelUpdatePriceGood(QyOrderUpdateGoodsEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Delete From Tbl_QY_OrderUpdateGoods Where OID=@OID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OID", DbType.Int64, entity.OID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询改价申请审批数据表
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryUpdatePriceInfo(int pIndex, int pNum, QyOrderUpdatePriceEntity entity)
        {
            List<QyOrderUpdatePriceEntity> result = new List<QyOrderUpdatePriceEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.ApplyDate DESC) AS RowNumber,a.*,b.Name as HouseName From Tbl_QY_OrderUpdatePrice as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID  Where (1=1) ";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and a.OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo + "'"; }
                if (!string.IsNullOrEmpty(entity.ApplyID)) { strSQL += " and a.ApplyID = '" + entity.ApplyID + "'"; }
                if (!string.IsNullOrEmpty(entity.ApplyName)) { strSQL += " and a.ApplyName like '%" + entity.ApplyName + "%'"; }
                if (!string.IsNullOrEmpty(entity.CheckID)) { strSQL += " and CHARINDEX(','+a.CheckID+',',','+'" + entity.CheckID + "'+',') > 0"; }
                //if (!string.IsNullOrEmpty(entity.CheckHouseID)) { strSQL += " and CHARINDEX(convert(varchar(3),a.HouseID),','+'" + entity.CheckHouseID + "'+',') > 0"; }
                if (!string.IsNullOrEmpty(entity.CheckHouseID)) { strSQL += " and a.HouseID in (" + entity.CheckHouseID + ")"; }
                if (!string.IsNullOrEmpty(entity.SaleManName)) { strSQL += " and a.SaleManName like '%" + entity.SaleManName + "%'"; }
                //所属仓库ID
                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }

                if (!string.IsNullOrEmpty(entity.ApplyStatus))
                {
                    if (entity.ApplyStatus.Equals("-1"))
                    {
                        strSQL += " and a.ApplyStatus !='0'";
                    }
                    else
                    {
                        strSQL += " and a.ApplyStatus in (" + entity.ApplyStatus + ")";
                    }
                }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.ApplyDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.ApplyDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new QyOrderUpdatePriceEntity
                            {
                                OID = Convert.ToInt64(idr["OID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                ApplyID = Convert.ToString(idr["ApplyID"]),
                                ApplyName = Convert.ToString(idr["ApplyName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                ApplyDate = Convert.ToDateTime(idr["ApplyDate"]),
                                ApplyStatus = Convert.ToString(idr["ApplyStatus"]),
                                CheckID = Convert.ToString(idr["CheckID"]),
                                CheckName = Convert.ToString(idr["CheckName"]),
                                CheckResult = Convert.ToString(idr["CheckResult"]),
                                Reason = Convert.ToString(idr["Reason"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                OrderCheckType = Convert.ToString(idr["OrderCheckType"]),
                                CheckTime = string.IsNullOrEmpty(Convert.ToString(idr["CheckTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckTime"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_QY_OrderUpdatePrice as a Where (1=1)";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strCount += " and a.OrderNo = '" + entity.OrderNo + "'"; }
                if (!string.IsNullOrEmpty(entity.OrderType)) { strCount += " and a.OrderType = '" + entity.OrderType + "'"; }
                // if (!string.IsNullOrEmpty(entity.CheckID)) { strCount += " and CHARINDEX(','+'" + entity.CheckID + "'+',' , ','+a.CheckID+',') > 0"; }
                if (!string.IsNullOrEmpty(entity.CheckID)) { strCount += " and CHARINDEX(','+a.CheckID+',',','+'" + entity.CheckID + "'+',') > 0"; }
                //if (!string.IsNullOrEmpty(entity.CheckHouseID)) { strCount += " and CHARINDEX(convert(varchar(3),a.HouseID),','+'" + entity.CheckHouseID + "'+',') > 0"; }
                if (!string.IsNullOrEmpty(entity.CheckHouseID)) { strSQL += " and a.HouseID in (" + entity.CheckHouseID + ")"; }
                if (!string.IsNullOrEmpty(entity.ApplyID)) { strCount += " and a.ApplyID = '" + entity.ApplyID + "'"; }
                if (!string.IsNullOrEmpty(entity.ApplyName)) { strCount += " and a.ApplyName like '%" + entity.ApplyName + "%'"; }
                if (!string.IsNullOrEmpty(entity.SaleManName)) { strCount += " and a.SaleManName like '%" + entity.SaleManName + "%'"; }
                //所属仓库ID
                if (!entity.HouseID.Equals(0)) { strCount += " and a.HouseID =" + entity.HouseID; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                if (!string.IsNullOrEmpty(entity.ApplyStatus))
                {
                    if (entity.ApplyStatus.Equals("-1"))
                    {
                        strCount += " and a.ApplyStatus !='0'";
                    }
                    else
                    {
                        strCount += " and a.ApplyStatus in (" + entity.ApplyStatus + ")";
                    }
                }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.ApplyDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.ApplyDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 查询我的审批 已审批过的
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryMyCheckUpdatePriceInfo(int pIndex, int pNum, QyOrderUpdatePriceEntity entity)
        {
            List<QyOrderUpdatePriceEntity> result = new List<QyOrderUpdatePriceEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.CheckType,a.ReadStatus,a.ApproveType,a.OP_DATE as ODate,b.*,c.Name as HouseName From Tbl_Cargo_ApproveCheck as a inner join Tbl_QY_OrderUpdatePrice as b on a.ApproveID=b.OID inner join Tbl_Cargo_House as c on c.HouseID=b.HouseID Where (1=1) and a.ApproveType=1";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and b.OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.ApplyID)) { strSQL += " and b.ApplyID = '" + entity.ApplyID + "'"; }
                if (!string.IsNullOrEmpty(entity.CheckID)) { strSQL += " and a.CheckUserID = '" + entity.CheckID + "'"; }
                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                if (!string.IsNullOrEmpty(entity.ApplyStatus))
                {
                    if (entity.ApplyStatus.Equals("-1"))
                    {
                        strSQL += " and b.ApplyStatus !='0'";
                    }
                    else
                    {
                        strSQL += " and b.ApplyStatus in (" + entity.ApplyStatus + ")";
                    }
                }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new QyOrderUpdatePriceEntity
                            {
                                OID = Convert.ToInt64(idr["OID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                ApplyID = Convert.ToString(idr["ApplyID"]),
                                ApplyName = Convert.ToString(idr["ApplyName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                ApplyDate = Convert.ToDateTime(idr["ApplyDate"]),
                                ApplyStatus = Convert.ToString(idr["ApplyStatus"]),
                                CheckID = Convert.ToString(idr["CheckID"]),
                                CheckName = Convert.ToString(idr["CheckName"]),
                                CheckResult = Convert.ToString(idr["CheckResult"]),
                                Reason = Convert.ToString(idr["Reason"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                CheckType = Convert.ToString(idr["CheckType"]),
                                ApproveType = Convert.ToString(idr["ApproveType"]),
                                ReadStatus = Convert.ToString(idr["ReadStatus"]),
                                CheckTime = string.IsNullOrEmpty(Convert.ToString(idr["CheckTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckTime"]),
                                OP_DATE = Convert.ToDateTime(idr["ODate"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount From Tbl_Cargo_ApproveCheck as a inner join Tbl_QY_OrderUpdatePrice as b on a.ApproveID=b.OID Where (1=1) and a.ApproveType=1";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strCount += " and b.OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.CheckID)) { strCount += " and a.CheckUserID = '" + entity.CheckID + "'"; }
                if (!string.IsNullOrEmpty(entity.ApplyID)) { strCount += " and b.ApplyID = '" + entity.ApplyID + "'"; }

                if (!string.IsNullOrEmpty(entity.ApplyStatus))
                {
                    if (entity.ApplyStatus.Equals("-1"))
                    {
                        strCount += " and b.ApplyStatus !='0'";
                    }
                    else
                    {
                        strCount += " and b.ApplyStatus in (" + entity.ApplyStatus + ")";
                    }
                }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 查询统计
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyOrderUpdatePriceEntity> QueryUpdatePriceStatis(QyOrderUpdatePriceEntity entity)
        {
            List<QyOrderUpdatePriceEntity> result = new List<QyOrderUpdatePriceEntity>();
            string strSQL = "select a.HouseID,b.Name as HouseName,a.OrderType,COUNT(*) as TNum From Tbl_QY_OrderUpdatePrice as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID where (1=1) ";
            if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID=" + entity.HouseID; }

            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.ApplyDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.ApplyDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            strSQL += " group by a.HouseID,b.Name,a.OrderType order by a.HouseID, COUNT(*) desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new QyOrderUpdatePriceEntity
                        {
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            TNum = Convert.ToInt32(idr["TNum"])
                        });
                    }
                }
            }
            return result;
        }
        public List<QyOrderUpdatePriceEntity> QueryUpdatePriceStatisSaleMan(QyOrderUpdatePriceEntity entity)
        {
            List<QyOrderUpdatePriceEntity> result = new List<QyOrderUpdatePriceEntity>();
            string strSQL = "select a.HouseID,b.Name as HouseName,a.SaleManName,COUNT(*) as TNum From Tbl_QY_OrderUpdatePrice as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID where (1=1) ";
            if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID=" + entity.HouseID; }

            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.ApplyDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.ApplyDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            strSQL += " group by a.HouseID,b.Name,a.SaleManName order by a.HouseID, COUNT(*) desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new QyOrderUpdatePriceEntity
                        {
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            SaleManName = Convert.ToString(idr["SaleManName"]),
                            TNum = Convert.ToInt32(idr["TNum"])
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询订单是否经过审批
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool QueryOrderApprovalOrNot(string OrderNo)
        {
            bool returnType = false;
            try
            {
                string strSQL = @"select top 1 * from Tbl_QY_OrderUpdatePrice where OrderNo=@OrderNo order by OP_DATE desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, OrderNo);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            if (idr["ApplyStatus"].ToString() == "1")
                            {
                                returnType = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return returnType;
        }
        /// <summary>
        /// 根据用户登录名获取上级负责人登录名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<SystemUserEntity> QueryUserBossLoginName(SystemUserEntity entity)
        {
            List<SystemUserEntity> resList = new List<SystemUserEntity>();
            string strSQL = @"select us.UserID,us.LoginName,us.UserName,us.DepID,us.DepCode,us2.LoginName as BossLoginName, org.BossID,org.Boss from Tbl_SysUser us inner join Tbl_SysOrganize org on us.DepID=org.ID left join Tbl_SysUser us2 on us2.UserID=org.BossID where us.LoginName=@LoginName";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LoginName", DbType.String, entity.LoginName);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            SystemUserEntity result = new SystemUserEntity();
                            result.UserID = Convert.ToInt32(idr["BossID"]);
                            result.LoginName = Convert.ToString(idr["BossLoginName"]);
                            result.UserName = Convert.ToString(idr["Boss"]);
                            resList.Add(result);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resList;
        }
        /// <summary>
        /// 根据员工账号查询分公司领导或部门领导信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SystemUserEntity QueryBossLeaderByLoginName(SystemUserEntity entity)
        {
            SystemUserEntity result = new SystemUserEntity();
            string strSQL = "select a.UserID,a.LoginName,a.UserName,b.OrgaType as OneOrgaType,b.Boss as OneBoss,b.BossLoginName as OneBossLoginName,c.OrgaType as TwoOrgaType,c.Boss as TwoBoss,c.BossLoginName as TwoBossLoginName From Tbl_SysUser as a inner join Tbl_SysOrganize as b on a.DepID=b.ID inner join Tbl_SysOrganize as c on b.ParentID=c.ID where LoginName=@LoginName";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@LoginName", DbType.String, entity.LoginName);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        if (Convert.ToString(idr["OneOrgaType"]).Equals("1"))
                        {
                            result.LoginName = Convert.ToString(idr["OneBossLoginName"]);
                            result.UserName = Convert.ToString(idr["OneBoss"]);
                        }
                        else if (Convert.ToString(idr["TwoOrgaType"]).Equals("1"))
                        {
                            result.LoginName = Convert.ToString(idr["TwoBossLoginName"]);
                            result.UserName = Convert.ToString(idr["TwoBoss"]);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询我的审批 未审批的报销
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryUpExpenseCheckInfo(int pIndex, int pNum, CargoExpenseEntity entity)
        {
            List<CargoExpenseEntity> result = new List<CargoExpenseEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.ExDate DESC) AS RowNumber,a.*,b.Name as HouseName,c.HappenDate,c.SName,c.SID,c.FID,c.ZID,c.Memo,c.Summary,c.DetailCharge,d.FName From Tbl_Cargo_Expense as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID inner join tbl_Cargo_ExpenseDetail as c on c.ExID=a.ExID inner join tbl_Cargo_FirstSubject d on d.FID=c.FID Where (1=1) ";
                if (!string.IsNullOrEmpty(entity.ExType)) { strSQL += " and a.ExType = '" + entity.ExType + "'"; }
                if (!string.IsNullOrEmpty(entity.NextCheckID)) { strSQL += " and CHARINDEX(','+'" + entity.NextCheckID + "'+',' , ','+a.NextCheckID+',') > 0"; }
                //所属仓库ID
                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.ExDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.ExDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoExpenseEntity
                            {
                                ExID = Convert.ToInt64(idr["ExID"]),
                                ExName = Convert.ToString(idr["ExName"]),
                                ExDepart = Convert.ToString(idr["ExDepart"]),
                                ExDate = Convert.ToDateTime(idr["ExDate"]),
                                ReceiveName = Convert.ToString(idr["ReceiveName"]),
                                ReceiveNumber = Convert.ToString(idr["ReceiveNumber"]),
                                ChargeType = Convert.ToString(idr["ChargeType"]),
                                ExCharge = Convert.ToDecimal(idr["ExCharge"]),
                                OperaID = Convert.ToString(idr["OperaID"]),
                                OperaName = Convert.ToString(idr["OperaName"]),
                                ExpenseDate = Convert.ToDateTime(idr["ExpenseDate"]),
                                Status = Convert.ToString(idr["Status"]),
                                DenyReason = Convert.ToString(idr["DenyReason"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Reason = Convert.ToString(idr["Reason"]),
                                NextCheckID = Convert.ToString(idr["NextCheckID"]),
                                NextCheckName = Convert.ToString(idr["NextCheckName"]),
                                UserID = Convert.ToString(idr["UserID"]),
                                UserName = Convert.ToString(idr["UserName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                ExType = Convert.ToString(idr["ExType"]),
                                ClientName = Convert.ToString(idr["ClientName"]),
                                ClientID = Convert.ToInt32(idr["ClientID"]),
                                CheckTime = string.IsNullOrEmpty(Convert.ToString(idr["CheckTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckTime"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_Expense as a Where (1=1)";
                //下单方式
                if (!string.IsNullOrEmpty(entity.ExType)) { strCount += " and a.ExType = '" + entity.ExType + "'"; }
                if (!string.IsNullOrEmpty(entity.NextCheckID)) { strCount += " and CHARINDEX(','+'" + entity.NextCheckID + "'+',' , ','+a.NextCheckID+',') > 0"; }
                //所属仓库ID
                if (!entity.HouseID.Equals(0)) { strCount += " and a.HouseID =" + entity.HouseID; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and a.HouseID in (" + entity.CargoPermisID + ")"; }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.ExDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.ExDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 查询我的审批 已审批过的报销
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryExpenseCheckInfo(int pIndex, int pNum, CargoExpenseEntity entity)
        {
            List<CargoExpenseEntity> result = new List<CargoExpenseEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                //SELECT ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber, * FROM ( SELECT DISTINCT a.*, c.Name AS HouseName FROM Tbl_Cargo_ApproveCheck b INNER JOIN Tbl_Cargo_Expense a ON b.ApproveID = a.ExID INNER JOIN Tbl_Cargo_House c ON c.HouseID = a.HouseID WHERE 1 = 1 AND b.ApproveType = 0 ) a
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(select DISTINCT ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber, a.ExID,a.ExName,a.ExDepart,a.ExDate,a.DenyReason,a.ReceiveName,a.ReceiveNumber,a.ChargeType,a.ExCharge,a.OperaID,a.OperaName,a.ExpenseDate,a.Status,a.CheckStatus,a.Reason,a.NextCheckID,a.NextCheckName,a.UserID,a.UserName,a.HouseID,c.Name as HouseName,a.ExType,a.ClientID,a.ClientName,a.CheckTime from Tbl_Cargo_Expense a inner join Tbl_Cargo_ExpenseApproveRout b on a.ExID = b.ExID inner join Tbl_Cargo_House c on a.HouseID = c.HouseID where 1 = 1 AND b.ApproveType = 0 ";
                if (!string.IsNullOrEmpty(entity.ExType)) { strSQL += " and a.ExType = '" + entity.ExType + "'"; }
                if (!string.IsNullOrEmpty(entity.NextCheckID)) { strSQL += " and CHARINDEX(','+'" + entity.NextCheckID + "'+',' , ','+a.NextCheckID+',') > 0"; }
                //所属仓库ID
                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.ExDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.ExDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoExpenseEntity
                            {
                                ExID = Convert.ToInt64(idr["ExID"]),
                                ExName = Convert.ToString(idr["ExName"]),
                                ExDepart = Convert.ToString(idr["ExDepart"]),
                                ExDate = Convert.ToDateTime(idr["ExDate"]),
                                ReceiveName = Convert.ToString(idr["ReceiveName"]),
                                ReceiveNumber = Convert.ToString(idr["ReceiveNumber"]),
                                ChargeType = Convert.ToString(idr["ChargeType"]),
                                ExCharge = Convert.ToDecimal(idr["ExCharge"]),
                                OperaID = Convert.ToString(idr["OperaID"]),
                                OperaName = Convert.ToString(idr["OperaName"]),
                                ExpenseDate = Convert.ToDateTime(idr["ExpenseDate"]),
                                Status = Convert.ToString(idr["Status"]),
                                DenyReason = Convert.ToString(idr["DenyReason"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Reason = Convert.ToString(idr["Reason"]),
                                NextCheckID = Convert.ToString(idr["NextCheckID"]),
                                NextCheckName = Convert.ToString(idr["NextCheckName"]),
                                UserID = Convert.ToString(idr["UserID"]),
                                UserName = Convert.ToString(idr["UserName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                ExType = Convert.ToString(idr["ExType"]),
                                ClientName = Convert.ToString(idr["ClientName"]),
                                ClientID = Convert.ToInt32(idr["ClientID"]),
                                CheckTime = string.IsNullOrEmpty(Convert.ToString(idr["CheckTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckTime"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount From Tbl_Cargo_ApproveCheck as b inner join Tbl_Cargo_Expense as a on b.ApproveID=a.ExID Where (1=1) and b.ApproveType=0";

                if (!string.IsNullOrEmpty(entity.ExType)) { strSQL += " and a.ExType = '" + entity.ExType + "'"; }
                if (!string.IsNullOrEmpty(entity.NextCheckID)) { strSQL += " and CHARINDEX(','+'" + entity.NextCheckID + "'+',' , ','+a.NextCheckID+',') > 0"; }
                //所属仓库ID
                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }

                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.ExDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.ExDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        #endregion
        #region 移库订单操作方法集合
        /// <summary>
        /// 移库新增
        /// </summary>
        /// <param name="entity"></param>
        public void AddMoveOrderData(CargoMoveOrderEntity entity)
        {
            CargoClientManager client = new CargoClientManager();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_MoveOrder(MoveNo,OldHouseName,OldHouseID,NewHouseName,NewHouseID,MoveNum,MoveStatus,Memo,OPID,NewAreaID,LogisID) VALUES (@MoveNo,@OldHouseName,@OldHouseID,@NewHouseName,@NewHouseID,@MoveNum,@MoveStatus,@Memo,@OPID,@NewAreaID,@LogisID) SELECT @@IDENTITY";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@MoveNo", DbType.String, entity.MoveNo);
                    conn.AddInParameter(cmd, "@OldHouseID", DbType.Int32, entity.OldHouseID);
                    conn.AddInParameter(cmd, "@NewAreaID", DbType.Int32, entity.NewAreaID);
                    conn.AddInParameter(cmd, "@OldHouseName", DbType.String, entity.OldHouseName);
                    conn.AddInParameter(cmd, "@NewHouseName", DbType.String, entity.NewHouseName);
                    conn.AddInParameter(cmd, "@NewHouseID", DbType.Int32, entity.NewHouseID);
                    conn.AddInParameter(cmd, "@MoveNum", DbType.Int32, entity.MoveNum);
                    conn.AddInParameter(cmd, "@MoveStatus", DbType.String, entity.MoveStatus);
                    conn.AddInParameter(cmd, "@Memo", DbType.String, entity.Memo);
                    conn.AddInParameter(cmd, "@OPID", DbType.String, entity.OPID);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID.Equals(0) ? 34 : entity.LogisID);
                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                #endregion

                //新增产品与订单关联数据
                AddMoveOrderGoodsInfo(entity.MoveGoodsList);
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void AddMoveOrderGoodsInfo(List<CargoMoveOrderGoodsEntity> goods)
        {
            try
            {
                foreach (var it in goods)
                {
                    it.EnSafe();
                    string strSQL = @"INSERT INTO Tbl_Cargo_MoveOrderGood(MoveNo,ProductID,ContainerGoodsID,ContainerID,Piece,OPID) VALUES (@MoveNo,@ProductID,@ContainerGoodsID,@ContainerID,@Piece,@OPID)";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@MoveNo", DbType.String, it.MoveNo);
                        conn.AddInParameter(cmd, "@ProductID", DbType.Int64, it.ProductID);
                        conn.AddInParameter(cmd, "@ContainerGoodsID", DbType.Int64, it.ContainerGoodsID);
                        conn.AddInParameter(cmd, "@ContainerID", DbType.Int32, it.ContainerID);
                        conn.AddInParameter(cmd, "@Piece", DbType.Int32, it.Piece);
                        conn.AddInParameter(cmd, "@OPID", DbType.String, it.OPID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 修改移库的新产品和新货位
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateMoveOrderGood(CargoMoveOrderGoodsEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_Cargo_MoveOrderGood Set NewProductID=@NewProductID,NewContainerID=@NewContainerID ";
            if (!entity.NewPiece.Equals(0)) { strSQL += " ,NewPiece=NewPiece+@NewPiece"; }
            strSQL += " Where MoveNo=@MoveNo and ProductID=@ProductID and ContainerID=@ContainerID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@MoveNo", DbType.String, entity.MoveNo);
                conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                conn.AddInParameter(cmd, "@ContainerID", DbType.Int32, entity.ContainerID);
                conn.AddInParameter(cmd, "@NewProductID", DbType.Int64, entity.NewProductID);
                conn.AddInParameter(cmd, "@NewContainerID", DbType.Int32, entity.NewContainerID);
                if (!entity.NewPiece.Equals(0))
                {
                    conn.AddInParameter(cmd, "@NewPiece", DbType.Int32, entity.NewPiece);
                }
                conn.ExecuteNonQuery(cmd);
            }
        }
        public void UpdateMoveOrderData(CargoMoveOrderEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_Cargo_MoveOrder Set MoveNum=MoveNum+@MoveNum,MoveStatus=@MoveStatus";
            string moveStatus = entity.MoveStatus;
            if (moveStatus == "2") //如果状态是已完成，设置完成时间
            {
                strSQL = @",CompDate=GETDATE()";
            }
            strSQL += " Where MoveNo=@MoveNo and ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@MoveNo", DbType.String, entity.MoveNo);
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.AddInParameter(cmd, "@MoveNum", DbType.Int32, entity.MoveNum);
                conn.AddInParameter(cmd, "@MoveStatus", DbType.String, entity.MoveStatus);
                conn.ExecuteNonQuery(cmd);
            }
        }
        public void UpdateMoveOrderGoodsData(CargoMoveOrderGoodsEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_Cargo_MoveOrderGood Set Piece=Piece+@Piece";
            strSQL += " Where MoveNo=@MoveNo and ProductID=@ProductID and ContainerGoodsID=@ContainerGoodsID and ContainerID=@ContainerID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@MoveNo", DbType.String, entity.MoveNo);
                conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                conn.AddInParameter(cmd, "@ContainerGoodsID", DbType.Int64, entity.ContainerGoodsID);
                conn.AddInParameter(cmd, "@ContainerID", DbType.Int32, entity.ContainerID);
                conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                conn.ExecuteNonQuery(cmd);
            }
        }
        public void DeleteMoveOrderGoodsData(CargoMoveOrderGoodsEntity entity)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_MoveOrderGood Where MoveNo=@MoveNo and ProductID=@ProductID and ContainerGoodsID=@ContainerGoodsID and ContainerID=@ContainerID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@MoveNo", DbType.String, entity.MoveNo);
                conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                conn.AddInParameter(cmd, "@ContainerGoodsID", DbType.Int64, entity.ContainerGoodsID);
                conn.AddInParameter(cmd, "@ContainerID", DbType.Int32, entity.ContainerID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改数量 
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateMoveOrderGoodPiece(CargoMoveOrderGoodsEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_Cargo_MoveOrderGood SET ";
            if (entity.IsAdd) { strSQL += " NewPiece=NewPiece+@NewPiece"; }
            else { strSQL += " NewPiece=NewPiece-@NewPiece"; }
            strSQL += " Where NewProductID=@NewProductID and MoveNo=@MoveNo and ProductID=@ProductID and ContainerID=@ContainerID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@NewPiece", DbType.Int32, entity.NewPiece);
                    conn.AddInParameter(cmd, "@NewProductID", DbType.Int64, entity.NewProductID);
                    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                    conn.AddInParameter(cmd, "@ContainerID", DbType.Int32, entity.ContainerID);
                    conn.AddInParameter(cmd, "@MoveNo", DbType.String, entity.MoveNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询移库订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoMoveOrderEntity> QueryMoveOrderList(CargoMoveOrderEntity entity)
        {
            List<CargoMoveOrderEntity> result = new List<CargoMoveOrderEntity>();
            string strSQL = "Select * From Tbl_Cargo_MoveOrder WHere (1=1)";
            if (!string.IsNullOrEmpty(entity.OldHouseID)) { strSQL += " and (OldHouseID=" + entity.OldHouseID + " or NewHouseID=" + entity.OldHouseID + ")"; }
            if (!entity.NewHouseID.Equals(0)) { strSQL += " and NewHouseID=" + entity.NewHouseID; }
            if (!string.IsNullOrEmpty(entity.MoveStatus))
            {
                if (entity.MoveStatus.Equals("0"))
                {
                    strSQL += " and MoveStatus in ('0','3')";
                }
                else
                {
                    strSQL += " and MoveStatus='" + entity.MoveStatus + "'";
                }
            }
            if (!string.IsNullOrEmpty(entity.MoveNo)) { strSQL += " and MoveNo='" + entity.MoveNo + "'"; }
            if (!string.IsNullOrEmpty(entity.NewHouseName)) { strSQL += " and NewHouseName ='" + entity.NewHouseName + "'"; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            strSQL += " Order by OP_DATE Desc ";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoMoveOrderEntity
                        {
                            ID = Convert.ToInt64(idr["ID"]),
                            MoveNo = Convert.ToString(idr["MoveNo"]),
                            OldHouseID = Convert.ToString(idr["OldHouseID"]),
                            OldHouseName = Convert.ToString(idr["OldHouseName"]),
                            NewHouseID = Convert.ToInt32(idr["NewHouseID"]),
                            NewHouseName = Convert.ToString(idr["NewHouseName"]),
                            MoveStatus = Convert.ToString(idr["MoveStatus"]),
                            MoveNum = Convert.ToInt32(idr["MoveNum"]),
                            Memo = Convert.ToString(idr["Memo"]),
                            OPID = Convert.ToString(idr["OPID"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            MoveGoodsList = QueryMoveOrderGoodsList(new CargoMoveOrderGoodsEntity { MoveNo = Convert.ToString(idr["MoveNo"]) })
                        });
                    }
                }
            }
            return result;
        }
        public List<CargoMoveOrderGoodsEntity> QueryMoveOrderGoodsList(CargoMoveOrderGoodsEntity entity)
        {
            List<CargoMoveOrderGoodsEntity> result = new List<CargoMoveOrderGoodsEntity>();
            string strSQL = "select a.*,ISNULL(b.ScanNum,0) as ScanNum from ( select a.*,mo.MoveStatus,mo.NewHouseName,mo.Memo,mo.ID as MoveID,c.ProductName,c.TypeID,pt.TypeName,c.Model,c.GoodsCode,c.Specs,c.Figure,c.SalePrice,c.UnitPrice,c.LoadIndex,c.SpeedLevel,c.Batch,c.BelongDepart,d.ContainerCode,h.Name as HouseName,f.Name as FirstAreaName,ps.SourceName From Tbl_Cargo_MoveOrderGood as a inner join Tbl_Cargo_ContainerGoods as b on a.ContainerGoodsID=b.ID inner join Tbl_Cargo_Product as c on a.ProductID=c.ProductID left join Tbl_Cargo_ProductSource ps on c.Source=ps.Source inner join Tbl_Cargo_Container as d on b.ContainerID=d.ContainerID left join Tbl_Cargo_Area as e on d.AreaID=e.AreaID left join Tbl_Cargo_Area as f on e.ParentID=f.AreaID inner join Tbl_Cargo_MoveOrder mo on a.MoveNo=mo.MoveNo left join Tbl_Cargo_ProductType pt on c.TypeID=pt.TypeID left join Tbl_Cargo_House h on h.HouseID=e.HouseID where (1=1)";
            if (!string.IsNullOrEmpty(entity.MoveNo)) { strSQL += " and a.MoveNo='" + entity.MoveNo + "'"; }
            if (!entity.ProductID.Equals(0)) { strSQL += " and a.ProductID=" + entity.ProductID; }
            if (!entity.ContainerID.Equals(0)) { strSQL += " and a.ContainerID=" + entity.ContainerID; }
            strSQL += ") as a left join (select ProductID,ContainerID,COUNT(*) as ScanNum From Tbl_Cargo_ProductTag where (1=1) ";
            if (!string.IsNullOrEmpty(entity.MoveNo)) { strSQL += " and MoveOrderNo='" + entity.MoveNo + "'"; }
            if (!string.IsNullOrEmpty(entity.MoveStatus)) { strSQL += " and MoveStatus='" + entity.MoveStatus + "'"; }
            strSQL += " group by ProductID,ContainerID ) as b on a.ProductID=b.ProductID and a.ContainerID=b.ContainerID";
            strSQL += " Order by a.ContainerCode asc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoMoveOrderGoodsEntity
                        {
                            MoveNo = Convert.ToString(idr["MoveNo"]),
                            MoveID = Convert.ToString(idr["MoveID"]),
                            ProductID = Convert.ToInt64(idr["ProductID"]),
                            ContainerGoodsID = Convert.ToInt64(idr["ContainerGoodsID"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            NewContainerID = Convert.ToInt32(idr["NewContainerID"]),
                            NewProductID = Convert.ToInt64(idr["NewProductID"]),
                            ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                            NewPiece = Convert.ToInt32(idr["NewPiece"]),
                            OPID = Convert.ToString(idr["OPID"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            ContainerID = Convert.ToInt32(idr["ContainerID"]),
                            MoveStatus = Convert.ToString(idr["MoveStatus"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            Model = Convert.ToString(idr["Model"]),
                            ProductName = Convert.ToString(idr["ProductName"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            TypeID = Convert.ToInt32(idr["TypeID"]),
                            ScanNum = Convert.ToInt32(idr["ScanNum"]),
                            ContainerCode = Convert.ToString(idr["ContainerCode"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            SalePrice = Convert.ToString(idr["SalePrice"]),
                            UnitPrice = Convert.ToString(idr["UnitPrice"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            FirstAreaName = Convert.ToString(idr["FirstAreaName"]),
                            BelongDepart = Convert.ToString(idr["BelongDepart"]),
                            NewHouseName = Convert.ToString(idr["NewHouseName"]),
                            Memo = Convert.ToString(idr["Memo"]),
                            SourceName = Convert.ToString(idr["SourceName"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                        });
                    }
                }
            }
            return result;

        }

        /// <summary>
        /// 根据移库单号查询移库单明细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public List<CargoContainerShowEntity> QueryMoveOrderByMoveOrderNo(CargoMoveOrderGoodsEntity entity)
        {
            CargoProductManager pro = new CargoProductManager();
            CargoHouseManager house = new CargoHouseManager();
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"select b.MoveStatus,a.ProductID,a.Piece,c.ProductName,c.Model,c.Specs,c.GoodsCode,c.Figure,c.LoadIndex,c.SpeedLevel,c.TypeID,c.SalePrice,c.CostPrice,c.TradePrice,c.Batch,c.BatchWeek,c.BatchYear,c.Source,c.Born,c.BelongDepart,c.Assort,c.Company,c.Supplier,c.SpecsType,c.HouseID,c.TreadWidth,c.FlatRatio,c.Meridian,c.HubDiameter,c.SpeedMax,c.Size,c.UnitPrice From Tbl_Cargo_MoveOrderGood as a inner join Tbl_Cargo_MoveOrder as b on a.MoveNo=b.MoveNo inner join Tbl_Cargo_Product as c on a.ProductID=c.ProductID and b.OldHouseID=c.HouseID where a.MoveNo=@MoveNo";
                if (!entity.ProductID.Equals(0))
                {
                    strSQL += " and a.ProductID=@ProductID";
                }
                if (!entity.ContainerGoodsID.Equals(0))
                {
                    strSQL += " and a.ContainerGoodsID=@ContainerGoodsID";
                }
                if (!entity.ContainerID.Equals(0))
                {
                    strSQL += " and a.ContainerID=@ContainerID";
                }
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@MoveNo", DbType.String, entity.MoveNo);
                    if (!entity.ProductID.Equals(0))
                    {
                        conn.AddInParameter(command, "@ProductID", DbType.Int32, entity.ProductID);
                    }
                    if (!entity.ContainerGoodsID.Equals(0))
                    {
                        conn.AddInParameter(command, "@ContainerGoodsID", DbType.Int32, entity.ContainerGoodsID);
                    }
                    if (!entity.ContainerID.Equals(0))
                    {
                        conn.AddInParameter(command, "@ContainerID", DbType.Int32, entity.ContainerID);
                    }
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            CargoContainerShowEntity e = new CargoContainerShowEntity();
                            e.InCargoStatus = Convert.ToString(idr["MoveStatus"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);//货位上件数
                            e.Born = Convert.ToString(idr["Born"]);
                            e.Assort = Convert.ToString(idr["Assort"]);
                            e.Company = Convert.ToString(idr["Company"]);
                            e.Supplier = Convert.ToString(idr["Supplier"]);
                            e.HouseID = Convert.ToInt32(idr["HouseID"]);
                            e.ProductID = Convert.ToInt64(idr["ProductID"]);
                            e.ProductName = Convert.ToString(idr["ProductName"]);
                            e.TypeID = Convert.ToInt32(idr["TypeID"]);
                            e.Model = Convert.ToString(idr["Model"]);
                            e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            e.Figure = Convert.ToString(idr["Figure"]);
                            e.Specs = Convert.ToString(idr["Specs"]);
                            e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            //if (!string.IsNullOrEmpty(idr["LoadIndex"].ToString()))
                            //    e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            if (!string.IsNullOrEmpty(idr["TreadWidth"].ToString()))
                                e.TreadWidth = Convert.ToInt32(idr["TreadWidth"]);
                            if (!string.IsNullOrEmpty(idr["FlatRatio"].ToString()))
                                e.FlatRatio = Convert.ToInt32(idr["FlatRatio"]);
                            e.Meridian = Convert.ToString(idr["Meridian"]);
                            if (!string.IsNullOrEmpty(idr["HubDiameter"].ToString()))
                                e.HubDiameter = Convert.ToInt32(idr["HubDiameter"]);
                            e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            if (!string.IsNullOrEmpty(idr["SpeedMax"].ToString()))
                                e.SpeedMax = Convert.ToInt32(idr["SpeedMax"]);
                            e.Size = Convert.ToString(idr["Size"]);
                            e.UnitPrice = Convert.ToDecimal(idr["UnitPrice"]);
                            e.CostPrice = Convert.ToDecimal(idr["CostPrice"]);
                            e.TradePrice = Convert.ToDecimal(idr["TradePrice"]);
                            e.SalePrice = Convert.ToDecimal(idr["SalePrice"]);
                            e.Source = Convert.ToString(idr["Source"]);
                            e.Batch = Convert.ToString(idr["Batch"]);
                            e.BatchYear = Convert.ToInt32(idr["BatchYear"]);
                            e.BatchWeek = Convert.ToInt32(idr["BatchWeek"]);
                            e.BelongDepart = Convert.ToString(idr["BelongDepart"]);
                            e.SpecsType = Convert.ToString(idr["SpecsType"]);
                            result.Add(e);
                            #endregion
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public int QueryMoveOrderScanPiece(CargoMoveOrderGoodsEntity entity)
        {
            int ScanPiece = 0;
            string strSQL = "select COUNT(*) ScanPiece from Tbl_Cargo_ProductTag where MoveOrderNo=@MoveOrderNo";
            if (!entity.ProductID.Equals(0))
            {
                strSQL += " and ProductID=@ProductID";
            }
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@MoveOrderNo", DbType.String, entity.MoveNo);
                if (!entity.ProductID.Equals(0))
                {
                    conn.AddInParameter(cmdQ, "@ProductID", DbType.String, entity.ProductID);
                }
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    ScanPiece = Convert.ToInt32(idr.Rows[0]["ScanPiece"]);
                }
            }
            return ScanPiece;
        }
        public int QueryMoveOrderMoveNum(CargoMoveOrderEntity entity)
        {
            int MoveNum = 0;
            string strSQL = "select MoveNum from Tbl_Cargo_MoveOrder where MoveNo=@MoveNo";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@MoveNo", DbType.String, entity.MoveNo);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    MoveNum = Convert.ToInt32(idr.Rows[0]["MoveNum"]);
                }
            }
            return MoveNum;
        }
        /// <summary>
        /// 查询移库单 
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryMoveOrder(int pIndex, int pNum, CargoMoveOrderEntity entity)
        {
            List<CargoMoveOrderEntity> result = new List<CargoMoveOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += @"(
  SELECT
  DISTINCT ROW_NUMBER() OVER (
    ORDER BY
      a.OP_DATE DESC
  ) AS RowNumber,
  a.*,
  b.UserName,
  c.TypeNames
FROM
  Tbl_Cargo_MoveOrder AS a
  LEFT JOIN Tbl_SysUser AS b ON a.OPID = b.LoginName
  LEFT JOIN (
    SELECT
      t.MoveNo,
      STUFF(
        (
          SELECT
            DISTINCT ',' + pt.TypeName
          FROM
            Tbl_Cargo_MoveOrderGood mog
            INNER JOIN Tbl_Cargo_Product p ON mog.ProductID = p.ProductID
            INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
          WHERE
            mog.MoveNo = t.MoveNo FOR XML PATH(''),
            TYPE
        ).value('.', 'NVARCHAR(MAX)'),
        1,
        1,
        ''
      ) AS TypeNames
    FROM
      Tbl_Cargo_MoveOrderGood t
    GROUP BY
      t.MoveNo
  ) c ON a.MoveNo = c.MoveNo
  WHERE
    (1 = 1) ";
                //下单方式
                if (!string.IsNullOrEmpty(entity.MoveNo)) { strSQL += " and a.MoveNo = '" + entity.MoveNo + "'"; }
                if (!string.IsNullOrEmpty(entity.MoveStatus)) { strSQL += " and a.MoveStatus = '" + entity.MoveStatus + "'"; }

                if (!string.IsNullOrEmpty(entity.OldHouseID)) { strSQL += " and a.OldHouseID in (" + entity.OldHouseID + ")"; }
                if (!entity.NewHouseID.Equals(0)) { strSQL += " and a.NewHouseID=" + entity.NewHouseID; }
                if (!string.IsNullOrEmpty(entity.NewHouseName)) { strSQL += " and a.NewHouseName='" + entity.NewHouseName + "'"; }
                if (!string.IsNullOrEmpty(entity.UserName)) { strSQL += "and b.UserName like '%" + entity.UserName + "%'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //按品牌查询
                if (!entity.TypeID.Equals(0) || !entity.ParentID.Equals(0))
                {
                    strSQL += " AND EXISTS ( SELECT 1 FROM Tbl_Cargo_MoveOrderGood AS c1  LEFT JOIN Tbl_Cargo_Product AS p ON c1.ProductID = p.ProductID LEFT JOIN Tbl_Cargo_ProductType AS t ON p.TypeID = t.TypeID  WHERE ( 1 = 1 )";
                    if (!entity.TypeID.Equals(0)) { strSQL += "and t.TypeID =" + entity.TypeID; }
                    if (!entity.ParentID.Equals(0)) { strSQL += " and  t.ParentID = " + entity.ParentID; }
                    strSQL += " AND c1.MoveNo = a.MoveNo )";
                }

                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoMoveOrderEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OldHouseID = Convert.ToString(idr["OldHouseID"]),
                                OldHouseName = Convert.ToString(idr["OldHouseName"]),
                                NewHouseID = Convert.ToInt32(idr["NewHouseID"]),
                                NewAreaID = string.IsNullOrEmpty(Convert.ToString(idr["NewAreaID"])) ? 0 : Convert.ToInt32(idr["NewAreaID"]),
                                NewHouseName = Convert.ToString(idr["NewHouseName"]),
                                MoveNo = Convert.ToString(idr["MoveNo"]),
                                MoveNum = Convert.ToInt32(idr["MoveNum"]),
                                MoveStatus = Convert.ToString(idr["MoveStatus"]),
                                Memo = Convert.ToString(idr["Memo"]),
                                OPID = Convert.ToString(idr["OPID"]),
                                UserName = Convert.ToString(idr["UserName"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                TypeNames = Convert.ToString(idr["TypeNames"]),
                                CompDate = idr.Field<DateTime?>("CompDate"),
                                SpendDays = idr.Field<DateTime?>("CompDate") != null
                                ? (int)Math.Ceiling(((DateTime)idr["CompDate"] - (DateTime)idr["OP_DATE"]).TotalDays)
                                : default(int?)
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_MoveOrder as a Where (1=1)";
                //下单方式
                if (!string.IsNullOrEmpty(entity.MoveNo)) { strCount += " and a.MoveNo = '" + entity.MoveNo + "'"; }
                if (!string.IsNullOrEmpty(entity.MoveStatus)) { strCount += " and a.MoveStatus = '" + entity.MoveStatus + "'"; }

                if (!string.IsNullOrEmpty(entity.OldHouseID)) { strCount += " and a.OldHouseID in (" + entity.OldHouseID + ")"; }
                //if (!entity.OldHouseID.Equals(0)) { strCount += " and a.OldHouseID in (" + entity.OldHouseID + ")"; }
                if (!entity.NewHouseID.Equals(0)) { strCount += " and a.NewHouseID=" + entity.NewHouseID; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 查询移库单导出
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoMoveOrderEntity> QueryMoveOrderForExport(CargoMoveOrderEntity entity)
        {
            List<CargoMoveOrderEntity> result = new List<CargoMoveOrderEntity>();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @"SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.* From Tbl_Cargo_MoveOrder as a Where (1=1) ";
                //下单方式
                if (!string.IsNullOrEmpty(entity.MoveNo)) { strSQL += " and a.MoveNo = '" + entity.MoveNo + "'"; }
                if (!string.IsNullOrEmpty(entity.MoveStatus)) { strSQL += " and a.MoveStatus = '" + entity.MoveStatus + "'"; }

                if (!entity.OldHouseID.Equals(0)) { strSQL += " and a.OldHouseID=" + entity.OldHouseID; }
                if (!entity.NewHouseID.Equals(0)) { strSQL += " and a.NewHouseID=" + entity.NewHouseID; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoMoveOrderEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OldHouseID = Convert.ToString(idr["OldHouseID"]),
                                OldHouseName = Convert.ToString(idr["OldHouseName"]),
                                NewHouseID = Convert.ToInt32(idr["NewHouseID"]),
                                NewHouseName = Convert.ToString(idr["NewHouseName"]),
                                MoveNo = Convert.ToString(idr["MoveNo"]),
                                MoveNum = Convert.ToInt32(idr["MoveNum"]),
                                MoveStatus = Convert.ToString(idr["MoveStatus"]),
                                Memo = Convert.ToString(idr["Memo"]),
                                OPID = Convert.ToString(idr["OPID"]),
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
        public void DeleteMoveOrderInfo(CargoMoveOrderEntity entity)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_MoveOrder Where ID=@ID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ID", DbType.Int64, entity.ID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改移库单移库状态
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateMoveOrderStatus(CargoMoveOrderEntity entity)
        {
            string strSQL = @"Update Tbl_Cargo_MoveOrder set MoveStatus=@MoveStatus Where MoveNo=@MoveNo";
            string moveStatus = entity.MoveStatus;
            if (moveStatus == "2") //如果状态是已完成，设置完成时间
            {
                strSQL = @"Update Tbl_Cargo_MoveOrder set MoveStatus=@MoveStatus, CompDate=GETDATE()  Where MoveNo=@MoveNo";
            }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@MoveStatus", DbType.String, entity.MoveStatus);
                conn.AddInParameter(cmd, "@MoveNo", DbType.String, entity.MoveNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改移库单物流号
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateMoveOrderLogis(CargoMoveOrderEntity entity)
        {
            string strSQL = @"Update Tbl_Cargo_MoveOrder set LogisAwbNo=@LogisAwbNo Where MoveNo=@MoveNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                conn.AddInParameter(cmd, "@MoveNo", DbType.String, entity.MoveNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 通过订单号删除订单与产品关联表数据
        /// </summary>
        /// <param name="good"></param>
        public void DeleteMoveOrderGoodsInfo(CargoMoveOrderGoodsEntity good)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_MoveOrderGood Where MoveNo=@MoveNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@MoveNo", DbType.String, good.MoveNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 判断该移库单是否入库完成
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsMoveInHouseComplete(CargoMoveOrderGoodsEntity entity)
        {
            string strSQL = "Select SUM(Piece) as Piece,SUM(NewPiece) as NewPiece from Tbl_Cargo_MoveOrderGood where MoveNo=@MoveNo";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@MoveNo", DbType.String, entity.MoveNo);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null) { return false; }
                    if (idr.Rows.Count <= 0) { return false; }
                    if (!Convert.ToInt32(idr.Rows[0]["Piece"]).Equals(Convert.ToInt32(idr.Rows[0]["NewPiece"])))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 查询移库扫描状态和入库状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CargoMoveOrderEntity QueryMoveOrderByMoveNo(CargoMoveOrderEntity entity)
        {
            CargoMoveOrderEntity result = new CargoMoveOrderEntity();
            string strSQL = "select * from Tbl_Cargo_MoveOrder where MoveNo=@MoveNo";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@MoveNo", DbType.String, entity.MoveNo);
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result = new CargoMoveOrderEntity
                        {
                            MoveNo = Convert.ToString(idr["MoveNo"]),
                            MoveNum = Convert.ToInt32(idr["MoveNum"]),
                            NewHouseID = Convert.ToInt32(idr["NewHouseID"]),
                        };
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询移库扫描状态和入库状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CargoMoveOrderEntity QueryMoveOrderEntity(CargoMoveOrderEntity entity)
        {
            CargoMoveOrderEntity result = new CargoMoveOrderEntity();
            string strSQL = "select a.MoveNo,a.MoveNum,b.ScanNum from (select MoveNo, MoveNum From Tbl_Cargo_MoveOrder where MoveNo =@MoveNo ) as a inner join(select MoveOrderNo, ISNULL(COUNT(*),0) as ScanNum From Tbl_Cargo_ProductTag where MoveOrderNo =@MoveNo and MoveStatus = @MoveStatus group by MoveOrderNo) as b on a.MoveNo = b.MoveOrderNo ";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@MoveNo", DbType.String, entity.MoveNo);
                conn.AddInParameter(command, "@MoveStatus", DbType.String, entity.MoveStatus);
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result = new CargoMoveOrderEntity
                        {
                            MoveNo = Convert.ToString(idr["MoveNo"]),
                            MoveNum = Convert.ToInt32(idr["MoveNum"]),
                            ScanNum = Convert.ToInt32(idr["ScanNum"]),
                        };
                    }
                }
            }
            return result;
        }
        #endregion
        #region 删除订单管理操作方法集合

        /// <summary>
        /// 插入订单数据至订单删除数据表
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrderInfoToTemp(CargoOrderEntity entity)
        {
            string strSQL = @"insert into Tbl_Cargo_OrderTemp (OrderNo,OrderNum,HAwbNo,LogisAwbNo,LogisID,Dep,Dest,Piece,Weight,Volume,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,TrafficType,DeliveryType,AcceptUnit,AcceptPeople,AcceptTelephone,AcceptCellphone,AcceptAddress,CreateAwb,CreateDate,CheckStatus,Signer,SignTime,OP_ID,OP_DATE,AwbStatus,Remark,SaleManID,SaleManName,HouseID,CreateAwbID,OrderType,FinanceSecondCheck,FinanceSecondCheckName,FinanceSecondCheckDate,OrderModel,ClientNum,WXOrderNo,ThrowGood,TranHouse,OutHouseName,ModifyPriceStatus,PayClientNum,PayClientName,BelongHouse,IsPrintPrice,DeliverySettlement,DeleteID,DeleteName) select OrderNo,OrderNum,HAwbNo,LogisAwbNo,LogisID,Dep,Dest,Piece,Weight,Volume,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,TrafficType,DeliveryType,AcceptUnit,AcceptPeople,AcceptTelephone,AcceptCellphone,AcceptAddress,CreateAwb,CreateDate,CheckStatus,Signer,SignTime,OP_ID,GETDATE(),AwbStatus,Remark,SaleManID,SaleManName,HouseID,CreateAwbID,OrderType,FinanceSecondCheck,FinanceSecondCheckName,FinanceSecondCheckDate,OrderModel,ClientNum,WXOrderNo,ThrowGood,TranHouse,OutHouseName,ModifyPriceStatus,PayClientNum,PayClientName,BelongHouse,IsPrintPrice,DeliverySettlement,@DeleteID,@DeleteName from Tbl_Cargo_Order where OrderID=@OrderID";

            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@DeleteID", DbType.String, string.IsNullOrEmpty(entity.DeleteID) ? "" : entity.DeleteID);
                    conn.AddInParameter(cmd, "@DeleteName", DbType.String, string.IsNullOrEmpty(entity.DeleteName) ? "" : entity.DeleteName);
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 插入订单数据至订单删除数据表
        /// </summary>
        /// <param name="entity"></param>
        public void InsertOrderGoodsInfoToTemp(CargoOrderGoodsEntity entity)
        {
            string strSQL = @"insert into Tbl_Cargo_OrderGoodsTemp (OrderNo,ProductID,HouseID,AreaID,ContainerCode,Piece,OP_ID,OP_DATE,ActSalePrice,OutCargoID,RelateOrderNo,RuleType,RuleID,RuleTitle) select OrderNo,ProductID,HouseID,AreaID,ContainerCode,Piece,OP_ID,GETDATE(),ActSalePrice,OutCargoID,RelateOrderNo,RuleType,RuleID,RuleTitle from Tbl_Cargo_OrderGoods where OrderNo=@OrderNo";
            if (!entity.ProductID.Equals(0)) { strSQL += " and ProductID=@ProductID"; }
            if (!string.IsNullOrEmpty(entity.ContainerCode)) { strSQL += " and ContainerCode=@ContainerCode"; }
            if (!string.IsNullOrEmpty(entity.OutCargoID)) { strSQL += " and OutCargoID=@OutCargoID"; }
            if (!entity.Piece.Equals(0)) { strSQL += " and Piece=@Piece"; }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo.ToUpper());
                    if (!entity.ProductID.Equals(0))
                    {
                        conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                    }
                    if (!string.IsNullOrEmpty(entity.ContainerCode))
                    {
                        conn.AddInParameter(cmd, "@ContainerCode", DbType.String, entity.ContainerCode.ToUpper());
                    }
                    if (!string.IsNullOrEmpty(entity.OutCargoID))
                    {
                        conn.AddInParameter(cmd, "@OutCargoID", DbType.String, entity.OutCargoID.ToUpper());
                    }
                    if (!entity.Piece.Equals(0))
                    {
                        conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    }
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询删除订单记录表
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryOrderTempInfo(int pIndex, int pNum, CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,b.LogisticName,c.WXPayOrderNo,c.PayWay from Tbl_Cargo_OrderTemp as a left join tbl_cargo_Logistic as b on a.LogisID=b.ID left join Tbl_WX_Order as c on a.WXOrderNo=c.OrderNo Where (1=1) ";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and a.OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.OutHouseName)) { strSQL += " and a.OutHouseName = '" + entity.OutHouseName + "'"; }
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                    }
                    else
                    {
                        strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                    }
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'"; }
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strSQL += " and a.FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and a.AcceptPeople = '" + entity.AcceptPeople + "'"; }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strSQL += " and a.PayClientName = '" + entity.PayClientName + "'"; }
                if (!entity.PayClientNum.Equals(0)) { strSQL += " and a.PayClientNum=" + entity.PayClientNum; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strSQL += " and a.Dep = '" + entity.Dep + "'"; }
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
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //物流单号
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strSQL += " and a.LogisAwbNo = '" + entity.LogisAwbNo + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID ='" + entity.SaleManID + "'"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.OrderModel)) { strSQL += " and a.OrderModel ='" + entity.OrderModel + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood)) { strSQL += " and a.ThrowGood ='" + entity.ThrowGood + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and a.Piece=" + entity.Piece + ""; }
                //公司类型
                if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and a.BelongHouse = " + entity.BelongHouse; }
                //客户名称
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and a.AcceptUnit = '" + entity.AcceptUnit + "'"; }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                LogisticName = Convert.ToString(idr["LogisticName"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                OrderModel = Convert.ToString(idr["OrderModel"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                FinanceSecondCheck = Convert.ToString(idr["FinanceSecondCheck"]),
                                FinanceSecondCheckName = Convert.ToString(idr["FinanceSecondCheckName"]),
                                FinanceSecondCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["FinanceSecondCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["FinanceSecondCheckDate"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                WXOrderNo = Convert.ToString(idr["OrderType"]).Equals("0") || Convert.ToString(idr["OrderType"]).Equals("1") ? "" : Convert.ToString(idr["WXOrderNo"]),
                                PayWay = Convert.ToString(idr["PayWay"]),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                TranHouse = Convert.ToString(idr["TranHouse"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                ModifyPriceStatus = Convert.ToString(idr["ModifyPriceStatus"]),
                                PayClientName = Convert.ToString(idr["PayClientName"]),
                                PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                                IsPrintPrice = Convert.ToInt32(idr["IsPrintPrice"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                DeleteID = Convert.ToString(idr["DeleteID"]),
                                DeleteName = Convert.ToString(idr["DeleteName"]),
                                DeleteDate = string.IsNullOrEmpty(Convert.ToString(idr["DeleteDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["DeleteDate"]),
                                DeliverySettlement = Convert.ToString(idr["DeliverySettlement"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_OrderTemp  Where (1=1)";
                if (!string.IsNullOrEmpty(entity.OutHouseName)) { strCount += " and OutHouseName = '" + entity.OutHouseName + "'"; }
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strCount += " and OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood)) { strSQL += " and ThrowGood ='" + entity.ThrowGood + "'"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.OrderModel)) { strCount += " and OrderModel ='" + entity.OrderModel + "'"; }
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strCount += " and (AwbStatus='0' or AwbStatus='1')";
                    }
                    else
                    {
                        strCount += " and AwbStatus='" + entity.AwbStatus + "'";
                    }
                    //strCount += " and AwbStatus = '" + entity.AwbStatus + "'";
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strCount += " and CheckOutType = '" + entity.CheckOutType + "'"; }
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strCount += " and FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //收货单位,人
                //if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strCount += " and (AcceptPeople like '%" + entity.AcceptPeople + "%' or AcceptUnit like '%" + entity.AcceptPeople + "%')"; }
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strCount += " and AcceptPeople = '" + entity.AcceptPeople + "'"; }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strCount += " and PayClientName = '" + entity.PayClientName + "'"; }
                if (!entity.PayClientNum.Equals(0)) { strCount += " and PayClientNum=" + entity.PayClientNum; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strCount += " and Dep='" + entity.Dep + "'"; }
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
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strCount += " and LogisAwbNo = '" + entity.LogisAwbNo + "'"; }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strCount += " and OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strCount += " and CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strCount += " and SaleManID ='" + entity.SaleManID + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strCount += " and Piece=" + entity.Piece + ""; }
                //公司类型
                if (!string.IsNullOrEmpty(entity.BelongHouse)) { strCount += " and BelongHouse = " + entity.BelongHouse; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 通过订单号查询订单数据
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public List<CargoContainerShowEntity> QueryOrderTempByOrderNo(CargoOrderEntity entity)
        {
            CargoProductManager pro = new CargoProductManager();
            CargoHouseManager house = new CargoHouseManager();
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"select a.OrderNo,a.RelateOrderNo,a.ContainerCode,a.ProductID,a.Piece,ISNULL(a.ActSalePrice,0) as ActSalePrice,a.OutCargoID,b.HouseCode,b.HouseID,b.Name as HouseName,c.AreaID,c.Name as AreaName,d.ProductName,d.TypeID,e.TypeName,d.GoodsCode,d.Model,d.Specs,d.Figure,d.FlatRatio,d.Meridian,d.HubDiameter,d.LoadIndex,d.TreadWidth,d.SpeedLevel,d.SpeedMax,d.Size,d.SalePrice,d.CostPrice,d.TradePrice,d.UnitPrice,d.OP_DATE,d.Batch,d.Package,d.BatchYear,d.BatchWeek,d.BelongDepart,f.ContainerID,e.ParentID,c.ParentID as AreaParentID,d.Source,a.RuleType,a.RuleID,a.RuleTitle from Tbl_Cargo_OrderGoodsTemp as a left join Tbl_Cargo_House as b on a.HouseID=b.HouseID left join Tbl_Cargo_Area as c on a.AreaID=c.AreaID left join Tbl_Cargo_Product as d on a.ProductID=d.ProductID left join Tbl_Cargo_ProductType as e on d.TypeID=e.TypeID left join Tbl_Cargo_Container as f on a.ContainerCode=f.ContainerCode and f.AreaID=a.AreaID WHERE a.OrderNo=@OrderNo";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            List<CargoProductTypeEntity> proTypeList = pro.QueryProductType(new CargoProductTypeEntity { TypeID = Convert.ToInt32(idr["ParentID"]) });
                            CargoAreaEntity cargoArea = new CargoAreaEntity();
                            CargoAreaEntity hArea = new CargoAreaEntity();
                            //如果父ID不为0，则查询父ID的区域名称
                            if (!Convert.ToInt32(idr["AreaParentID"]).Equals(0))
                            {
                                cargoArea = house.QueryAreaByAreaID(new CargoAreaEntity { AreaID = Convert.ToInt32(idr["AreaParentID"]) });
                                if (!cargoArea.ParentID.Equals(0))
                                {
                                    hArea = house.QueryAreaByAreaID(new CargoAreaEntity { AreaID = cargoArea.ParentID });
                                }
                            }
                            #region 获取运单数据
                            CargoContainerShowEntity e = new CargoContainerShowEntity();
                            e.ContainerCode = Convert.ToString(idr["ContainerCode"]);
                            e.ContainerID = Convert.ToInt32(idr["ContainerID"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);//货位上件数
                            e.ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]);
                            e.AreaID = Convert.ToInt32(idr["AreaID"]);
                            e.AreaName = Convert.ToString(idr["AreaName"]);
                            e.FirstAreaName = hArea.Name;
                            e.HouseID = Convert.ToInt32(idr["HouseID"]);
                            e.HouseName = Convert.ToString(idr["HouseName"]);
                            e.HouseCode = Convert.ToString(idr["HouseCode"]);
                            e.ProductID = Convert.ToInt64(idr["ProductID"]);
                            e.ProductName = Convert.ToString(idr["ProductName"]);
                            e.TypeID = Convert.ToInt32(idr["TypeID"]);
                            e.TypeName = Convert.ToString(idr["TypeName"]);
                            e.TypeParentID = Convert.ToInt32(idr["ParentID"]);
                            if (proTypeList.Count > 0)
                            {
                                e.TypeParentName = proTypeList[0].TypeName;
                            }
                            e.Model = Convert.ToString(idr["Model"]);
                            e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            e.Figure = Convert.ToString(idr["Figure"]);
                            e.Specs = Convert.ToString(idr["Specs"]);
                            e.TreadWidth = Convert.ToInt32(idr["TreadWidth"]);
                            e.FlatRatio = Convert.ToInt32(idr["FlatRatio"]);
                            e.Meridian = Convert.ToString(idr["Meridian"]);
                            e.HubDiameter = Convert.ToInt32(idr["HubDiameter"]);
                            e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            e.SpeedMax = Convert.ToInt32(idr["SpeedMax"]);
                            e.Size = Convert.ToString(idr["Size"]);
                            e.OutCargoID = Convert.ToString(idr["OutCargoID"]);//出库单号
                            e.UnitPrice = Convert.ToDecimal(idr["UnitPrice"]);
                            e.CostPrice = Convert.ToDecimal(idr["CostPrice"]);
                            e.TradePrice = Convert.ToDecimal(idr["TradePrice"]);
                            e.SalePrice = Convert.ToDecimal(idr["SalePrice"]);
                            e.Source = Convert.ToString(idr["Source"]);
                            e.Batch = Convert.ToString(idr["Batch"]);
                            e.BatchYear = Convert.ToInt32(idr["BatchYear"]);
                            e.BatchWeek = Convert.ToInt32(idr["BatchWeek"]);
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            e.Package = Convert.ToString(idr["Package"]);
                            e.RelateOrderNo = Convert.ToString(idr["RelateOrderNo"]);
                            e.RuleType = Convert.ToString(idr["RuleType"]);
                            e.RuleID = Convert.ToString(idr["RuleID"]);
                            e.RuleTitle = Convert.ToString(idr["RuleTitle"]);
                            e.BelongDepart = Convert.ToString(idr["BelongDepart"]);
                            result.Add(e);
                            #endregion
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }

        public void UpdateGtmcOrderStatus(CargoGtmcProOrderEntity entity)
        {
            string strSQL = @"update Tbl_GTMC_ProOrder set OrderNo='',OrderStatus='0',ExterOrderAlloNum = NULL, OrderAlloType = NULL where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }

        public void UpdateExterOrderAlloGTMC(CargoExterOrderAlloEntity entity)
        {
            string strSQL = @"UPDATE Tbl_Cargo_ExterOrderAllo
SET OPLoginName = @OPLoginName,
    OPName = @OPName,
    OPDATE = @OPDATE,
    AlloPiece = CASE
                    WHEN AlloPiece - @AlloPiece >= 0 THEN AlloPiece - @AlloPiece
                    ELSE AlloPiece
                END
WHERE ExterOrderAlloNum = (
    SELECT TOP 1  ExterOrderAlloNum
    FROM Tbl_GTMC_ProOrder
    WHERE OrderNo = @OrderNo
) AND ExterOrderAlloNum > 0;";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@AlloPiece", DbType.Int32, Math.Abs(entity.AlloPiece));


                conn.AddInParameter(cmd, "@OPLoginName", DbType.String, string.IsNullOrEmpty(entity.OPLoginName) ? "" : entity.OPLoginName);
                conn.AddInParameter(cmd, "@OPName", DbType.String, string.IsNullOrEmpty(entity.OPName) ? "" : entity.OPName);
                conn.AddInParameter(cmd, "@OPDATE", DbType.DateTime, DateTime.Now);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }

        #endregion
        #region 预订单方法

        /// <summary>
        /// 查询当前日期当前仓库表中最大顺序号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetMaxPreOrderNumByCurrentDate(CargoOrderEntity entity)
        {
            int result = 0;
            try
            {
                string strSQL = @"select max(OrderNum) as OrderNum from (select ISNULL(MAX(OrderNum),0) as OrderNum from Tbl_Cargo_PreOrder where HouseID=@HouseID  and CreateDate>=@StartCreateDate and CreateDate<@EndCreateDate union select ISNULL(MAX(OrderNum),0) as OrderNum from Tbl_Cargo_Order where HouseID=@HouseID  and CreateDate>=@StartCreateDate and CreateDate<@EndCreateDate)a";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(command, "@StartCreateDate", DbType.String, entity.StartDate.ToString("yyyy-MM-dd"));
                    conn.AddInParameter(command, "@EndCreateDate", DbType.String, entity.EndDate.AddDays(1).ToString("yyyy-MM-dd"));
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        if (dd.Rows.Count > 0) { result = Convert.ToInt32(dd.Rows[0]["OrderNum"]); }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 新增订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddPreOrderInfo(CargoOrderEntity entity)
        {
            CargoClientManager client = new CargoClientManager();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_PreOrder(OrderNo,OrderNum,LogisAwbNo,LogisID,Dep,Dest,Piece,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,ClientNum,AcceptUnit,AcceptPeople,AcceptTelephone,AcceptCellphone,AcceptAddress,CreateAwb,CreateDate,OP_ID,OP_DATE,AwbStatus,Remark,SaleManID,SaleManName,SaleCellPhone,HouseID,CreateAwbID,OrderType,ThrowGood,OutHouseName,PayClientNum,PayClientName,IsMakeSure,IsPrintPrice,PurchaseHouseID) VALUES (@OrderNo,@OrderNum,@LogisAwbNo,@LogisID,@Dep,@Dest,@Piece,@InsuranceFee,@TransitFee,@TransportFee,@DeliveryFee,@OtherFee,@TotalCharge,@Rebate,@CheckOutType,@ClientNum,@AcceptUnit,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@AcceptAddress,@CreateAwb,@CreateDate,@OP_ID,@OP_DATE,@AwbStatus,@Remark,@SaleManID,@SaleManName,@SaleCellPhone,@HouseID,@CreateAwbID,@OrderType,@ThrowGood,@OutHouseName,@PayClientNum,@PayClientName,@IsMakeSure,@IsPrintPrice,@PurchaseHouseID) SELECT @@IDENTITY";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@InsuranceFee", DbType.Decimal, entity.InsuranceFee);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    conn.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@Rebate", DbType.Decimal, entity.Rebate);
                    conn.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@CreateAwbID", DbType.String, entity.CreateAwbID);
                    conn.AddInParameter(cmd, "@CreateAwb", DbType.String, entity.CreateAwb);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, entity.CreateDate);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@AwbStatus", DbType.String, entity.AwbStatus);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                    conn.AddInParameter(cmd, "@SaleCellPhone", DbType.String, entity.SaleCellPhone);
                    conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@ThrowGood", DbType.String, entity.ThrowGood);
                    conn.AddInParameter(cmd, "@OutHouseName", DbType.String, entity.OutHouseName);
                    conn.AddInParameter(cmd, "@PayClientNum", DbType.Int32, entity.PayClientNum);
                    conn.AddInParameter(cmd, "@PayClientName", DbType.String, entity.PayClientName);
                    conn.AddInParameter(cmd, "@IsMakeSure", DbType.Int32, entity.IsMakeSure);
                    conn.AddInParameter(cmd, "@IsPrintPrice", DbType.String, entity.IsPrintPrice);
                    conn.AddInParameter(cmd, "@PurchaseHouseID", DbType.Int32, entity.PurchaseHouseID);

                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                #endregion

                //新增预订单与产品关联数据
                AddPreOrderGoodsInfo(entity.goodsList);

                #region 新增订单已下单状态
                //CargoOrderStatusEntity status = new CargoOrderStatusEntity();
                //status.OrderID = did;
                //status.OrderNo = entity.OrderNo;
                //status.OrderStatus = "0";
                //status.OP_ID = entity.OP_ID;
                //status.OP_Name = entity.CreateAwb;
                //status.OP_DATE = DateTime.Now;
                //SaveOrderStatus(status);
                #endregion

            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 修改预订单出库数量
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="GoodsCode"></param>
        /// <param name="Piece"></param>
        public void UpdatePreOrderPiece(string OrderNo, string GoodsCode, int Piece)
        {
            string strSQL = @"UPDATE Tbl_Cargo_PreOrder set Piece=Piece+@Piece Where OrderNo=@OrderNo ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                conn.AddInParameter(cmd, "@Piece", DbType.Int32, Piece);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改预订单下单明细出库数量
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="GoodsCode"></param>
        /// <param name="Piece"></param>
        public void UpdatePreOrderGoodsPiece(string OrderNo, string GoodsCode, int Piece)
        {
            string strSQL = @"UPDATE Tbl_Cargo_PreOrderGoods set Piece=Piece+@Piece Where OrderNo=@OrderNo and GoodsCode=@GoodsCode";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                conn.AddInParameter(cmd, "@GoodsCode", DbType.String, GoodsCode);
                conn.AddInParameter(cmd, "@Piece", DbType.Int32, Piece);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 删除预订单下单明细
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="GoodsCode"></param>
        public void UDeletePreOrderGoodsPiece(string OrderNo, string GoodsCode)
        {
            string strSQL = @"DELETE from Tbl_Cargo_PreOrderGoods Where OrderNo=@OrderNo and GoodsCode=@GoodsCode";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                conn.AddInParameter(cmd, "@GoodsCode", DbType.String, GoodsCode);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 新增预订单与产品关联数据
        /// </summary>
        /// <param name="goods"></param>
        public void AddPreOrderGoodsInfo(List<CargoOrderGoodsEntity> goods)
        {
            try
            {
                foreach (var it in goods)
                {
                    it.EnSafe();
                    string strSQL = @"INSERT INTO Tbl_Cargo_PreOrderGoods(OrderNo,ProductID,GoodsCode,ProductName,HouseID,Piece,ActSalePrice,OP_ID,Remark,OP_DATE,Specs,Figure,Batch,TypeID,Born,Model,LoadIndex,SpeedLevel,PurchaserID,PurchaserName,ConfirmSalePrice,DeliveryBoss,DeliveryAddress,DeliveryCellphone) VALUES  (@OrderNo,@ProductID,@GoodsCode,@ProductName,@HouseID,@Piece,@ActSalePrice,@OP_ID,@Remark,@OP_DATE,@Specs,@Figure,@Batch,@TypeID,@Born,@Model,@LoadIndex,@SpeedLevel,@PurchaserID,@PurchaserName,@ConfirmSalePrice,@DeliveryBoss,@DeliveryAddress,@DeliveryCellphone)";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@OrderNo", DbType.String, it.OrderNo.ToUpper());
                        conn.AddInParameter(cmd, "@ProductID", DbType.Int32, it.ProductID);
                        conn.AddInParameter(cmd, "@GoodsCode", DbType.String, it.GoodsCode);
                        conn.AddInParameter(cmd, "@ProductName", DbType.String, it.ProductName);
                        conn.AddInParameter(cmd, "@HouseID", DbType.Int32, it.HouseID);
                        conn.AddInParameter(cmd, "@Piece", DbType.Int32, it.Piece);
                        conn.AddInParameter(cmd, "@ActSalePrice", DbType.Decimal, it.ActSalePrice);
                        conn.AddInParameter(cmd, "@OP_ID", DbType.String, it.OP_ID);
                        conn.AddInParameter(cmd, "@Remark", DbType.String, it.RuleTitle);
                        conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                        conn.AddInParameter(cmd, "@Specs", DbType.String, it.Specs);
                        conn.AddInParameter(cmd, "@Figure", DbType.String, it.Figure);
                        conn.AddInParameter(cmd, "@Batch", DbType.String, it.Batch);
                        conn.AddInParameter(cmd, "@TypeID", DbType.String, it.TypeID);
                        conn.AddInParameter(cmd, "@Born", DbType.String, it.Born);
                        conn.AddInParameter(cmd, "@Model", DbType.String, it.Model);
                        conn.AddInParameter(cmd, "@LoadIndex", DbType.String, it.LoadIndex);
                        conn.AddInParameter(cmd, "@SpeedLevel", DbType.String, it.SpeedLevel);
                        conn.AddInParameter(cmd, "@PurchaserID", DbType.Int32, it.PurchaserID);
                        conn.AddInParameter(cmd, "@PurchaserName", DbType.String, it.PurchaserName);
                        conn.AddInParameter(cmd, "@ConfirmSalePrice", DbType.Decimal, it.CostPrice);
                        conn.AddInParameter(cmd, "@DeliveryBoss", DbType.String, it.DeliveryBoss);
                        conn.AddInParameter(cmd, "@DeliveryAddress", DbType.String, it.DeliveryAddress);
                        conn.AddInParameter(cmd, "@DeliveryCellphone", DbType.String, it.DeliveryCellphone);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryPreOrderInfo(int pIndex, int pNum, CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,b.LogisticName from Tbl_Cargo_PreOrder as a left join tbl_cargo_Logistic as b on a.LogisID=b.ID Where (1=1) ";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and a.OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.OutHouseName)) { strSQL += " and a.OutHouseName = '" + entity.OutHouseName + "'"; }
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                    }
                    else
                    {
                        strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                    }
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'"; }
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strSQL += " and a.FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and a.AcceptPeople = '" + entity.AcceptPeople + "'"; }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strSQL += " and a.PayClientName = '" + entity.PayClientName + "'"; }
                if (!entity.PayClientNum.Equals(0)) { strSQL += " and a.PayClientNum=" + entity.PayClientNum; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strSQL += " and a.Dep = '" + entity.Dep + "'"; }
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
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //物流单号
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strSQL += " and a.LogisAwbNo = '" + entity.LogisAwbNo + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID ='" + entity.SaleManID + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood)) { strSQL += " and a.ThrowGood ='" + entity.ThrowGood + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and a.Piece=" + entity.Piece + ""; }
                //公司类型
                if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and a.BelongHouse = " + entity.BelongHouse; }
                //客户名称
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and a.AcceptUnit = '" + entity.AcceptUnit + "'"; }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                LogisticName = Convert.ToString(idr["LogisticName"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                PayClientName = Convert.ToString(idr["PayClientName"]),
                                PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                                IsMakeSure = Convert.ToInt32(idr["IsMakeSure"]),
                                PurchaserID = Convert.ToString(idr["PurchaserID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_Order  Where (1=1)";
                if (!string.IsNullOrEmpty(entity.OutHouseName)) { strCount += " and OutHouseName = '" + entity.OutHouseName + "'"; }
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strCount += " and OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood)) { strSQL += " and ThrowGood ='" + entity.ThrowGood + "'"; }
                //订单类型
                if (!string.IsNullOrEmpty(entity.OrderModel)) { strCount += " and OrderModel ='" + entity.OrderModel + "'"; }
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strCount += " and (AwbStatus='0' or AwbStatus='1')";
                    }
                    else
                    {
                        strCount += " and AwbStatus='" + entity.AwbStatus + "'";
                    }
                    //strCount += " and AwbStatus = '" + entity.AwbStatus + "'";
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strCount += " and CheckOutType = '" + entity.CheckOutType + "'"; }
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strCount += " and FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //收货单位,人
                //if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strCount += " and (AcceptPeople like '%" + entity.AcceptPeople + "%' or AcceptUnit like '%" + entity.AcceptPeople + "%')"; }
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strCount += " and AcceptPeople = '" + entity.AcceptPeople + "'"; }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strCount += " and PayClientName = '" + entity.PayClientName + "'"; }
                if (!entity.PayClientNum.Equals(0)) { strCount += " and PayClientNum=" + entity.PayClientNum; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strCount += " and Dep='" + entity.Dep + "'"; }
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
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strCount += " and LogisAwbNo = '" + entity.LogisAwbNo + "'"; }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strCount += " and OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strCount += " and CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strCount += " and SaleManID ='" + entity.SaleManID + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strCount += " and Piece=" + entity.Piece + ""; }
                //公司类型
                if (!string.IsNullOrEmpty(entity.BelongHouse)) { strCount += " and BelongHouse = " + entity.BelongHouse; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 通过订单号查询订单数据
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public List<CargoContainerShowEntity> QueryPreOrderByOrderNo(CargoOrderEntity entity)
        {
            CargoProductManager pro = new CargoProductManager();
            CargoHouseManager house = new CargoHouseManager();
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"select a.OrderNo,a.GoodsCode,a.Piece,ISNULL(a.ActSalePrice,0) as ActSalePrice,a.Remark,b.HouseCode,b.HouseID,b.Name as HouseName,d.ProductName,d.TypeID,e.TypeName,d.ProductID,d.GoodsCode,d.Model,d.Specs,d.Figure,d.FlatRatio,d.Meridian,d.HubDiameter,d.LoadIndex,d.TreadWidth,d.SpeedLevel,d.SpeedMax,d.Size,d.SalePrice,d.CostPrice,d.TradePrice,d.UnitPrice,d.OP_DATE,d.Batch,d.Package,d.BatchYear,d.BatchWeek,e.ParentID,d.Source from Tbl_Cargo_PreOrderGoods as a left join Tbl_Cargo_House as b on a.HouseID=b.HouseID  left join Tbl_Cargo_Product as d on a.ProductID=d.ProductID left join Tbl_Cargo_ProductType as e on d.TypeID=e.TypeID  WHERE a.OrderNo=@OrderNo";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            CargoContainerShowEntity e = new CargoContainerShowEntity();
                            e.Piece = Convert.ToInt32(idr["Piece"]);//货位上件数
                            e.ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]);
                            e.ProductID = Convert.ToInt64(idr["ProductID"]);
                            e.HouseID = Convert.ToInt32(idr["HouseID"]);
                            e.HouseName = Convert.ToString(idr["HouseName"]);
                            e.HouseCode = Convert.ToString(idr["HouseCode"]);
                            e.ProductName = Convert.ToString(idr["ProductName"]);
                            e.TypeID = Convert.ToInt32(idr["TypeID"]);
                            e.TypeName = Convert.ToString(idr["TypeName"]);
                            e.TypeParentID = Convert.ToInt32(idr["ParentID"]);
                            e.Model = Convert.ToString(idr["Model"]);
                            e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            e.Figure = Convert.ToString(idr["Figure"]);
                            e.Specs = Convert.ToString(idr["Specs"]);
                            e.TreadWidth = Convert.ToInt32(idr["TreadWidth"]);
                            e.FlatRatio = Convert.ToInt32(idr["FlatRatio"]);
                            e.Meridian = Convert.ToString(idr["Meridian"]);
                            e.HubDiameter = Convert.ToInt32(idr["HubDiameter"]);
                            e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            e.SpeedMax = Convert.ToInt32(idr["SpeedMax"]);
                            e.Size = Convert.ToString(idr["Size"]);
                            e.UnitPrice = Convert.ToDecimal(idr["UnitPrice"]);
                            e.CostPrice = Convert.ToDecimal(idr["CostPrice"]);
                            e.TradePrice = Convert.ToDecimal(idr["TradePrice"]);
                            e.SalePrice = Convert.ToDecimal(idr["SalePrice"]);
                            e.Source = Convert.ToString(idr["Source"]);
                            e.Batch = Convert.ToString(idr["Batch"]);
                            e.BatchYear = Convert.ToInt32(idr["BatchYear"]);
                            e.BatchWeek = Convert.ToInt32(idr["BatchWeek"]);
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            e.Package = Convert.ToString(idr["Package"]);
                            e.RuleTitle = Convert.ToString(idr["Remark"]);
                            result.Add(e);
                            #endregion
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 修改预订单为已确认
        /// </summary>
        /// <param name="OrderNo"></param>
        public void UpdatePreOrderConfirm(string OrderNo)
        {
            string strSQL = @"UPDATE Tbl_Cargo_PreOrder SET IsMakeSure=1,ConfirmTime=@ConfirmTime Where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                conn.AddInParameter(cmd, "@ConfirmTime", DbType.DateTime, DateTime.Now);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 删除预订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void DeletePreOrderInfo(CargoOrderEntity entity)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_PreOrder Where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 通过订单号删除订单与产品关联表数据
        /// </summary>
        /// <param name="good"></param>
        public void DeletePreOrderGoodsInfo(CargoOrderGoodsEntity good)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_PreOrderGoods Where OrderNo=@OrderNo";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, good.OrderNo.ToUpper());
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 通过订单号与富添盛编码删除订单与产品关联表数据
        /// </summary>
        /// <param name="good"></param>
        public void DeletePreOrderGoodsInfo(string OrderNo, string GoodsCode)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_PreOrderGoods Where OrderNo=@OrderNo and GoodsCode=@GoodsCode";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo.ToUpper());
                conn.AddInParameter(cmd, "@GoodsCode", DbType.String, GoodsCode);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 转移预订单信息至订单表
        /// </summary>
        /// <param name="entity"></param>
        public void PreOrderConfirm(CargoOrderEntity entity)
        {
            string strSQL = @"insert into Tbl_Cargo_Order (OrderNo,OrderNum,LogisAwbNo,LogisID,Dep,Dest,Piece,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,ClientNum,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,CreateAwbID,CreateAwb,CreateDate,CheckStatus,SaleManID,SaleManName,HouseID,OutHouseName,OP_ID,OP_DATE,AwbStatus,Remark,OrderType,ThrowGood,PayClientNum,PayClientName) select OrderNo,OrderNum,LogisAwbNo,LogisID,Dep,Dest,Piece,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,ClientNum,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,CreateAwbID,CreateAwb,CreateDate,CheckStatus,SaleManID,SaleManName,HouseID,OutHouseName,OP_ID,OP_DATE,AwbStatus,Remark,OrderType,ThrowGood,PayClientNum,PayClientName from Tbl_Cargo_PreOrder where OrderNo=@OrderNo";

            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.ExecuteNonQuery(cmd);
                }

                DeletePreOrderInfo(entity);
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 转移预下单明细信息至订单明细表
        /// </summary>
        /// <param name="entity"></param>
        public void PreOrderGoodsConfirm(CargoOrderGoodsEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"insert into Tbl_Cargo_OrderGoods (OrderNo,ProductID,HouseID,AreaID,ContainerCode,Piece ,ActSalePrice,OP_ID,OP_DATE) select OrderNo,@ProductID,@HouseID,@AreaID,@ContainerCode,@Piece,ActSalePrice,OP_ID,OP_DATE from Tbl_Cargo_PreOrderGoods where OrderNo=@OrderNo and GoodsCode=@GoodsCode";

            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@AreaID", DbType.String, entity.AreaID);
                    conn.AddInParameter(cmd, "@ContainerCode", DbType.String, entity.ContainerCode);
                    conn.AddInParameter(cmd, "@Piece", DbType.String, entity.Piece);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    conn.ExecuteNonQuery(cmd);
                }
                DeletePreOrderGoodsInfo(entity.OrderNo, entity.GoodsCode);
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }

        /// <summary>
        /// 根据订单号查询订单与产品关联表数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderGoodsEntity> QueryPreOrderGoodsInfo(string OrderNo)
        {
            List<CargoOrderGoodsEntity> result = new List<CargoOrderGoodsEntity>();
            try
            {
                string strSQL = @"select * from Tbl_Cargo_PreOrderGoods where OrderNo=@OrderNo";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, OrderNo);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                ActSalePrice = Convert.ToDecimal(idr["ActSalePrice"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Model = Convert.ToString(idr["Model"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Born = Convert.ToString(idr["Born"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                PurchaserID = Convert.ToInt32(idr["PurchaserID"]),
                                PurchaserName = Convert.ToString(idr["PurchaserName"]),
                                ConfirmSalePrice = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmSalePrice"])) ? 0 : Convert.ToDecimal(idr["ConfirmSalePrice"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 根据预订单号查询产品库存数量
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public int QueryPreOrderProductNum(CargoOrderGoodsEntity entity)
        {
            int num = 0;
            try
            {
                string strSQL = @"select ISNULL(sum(cg.Piece),0) from Tbl_Cargo_Product p inner join Tbl_Cargo_ContainerGoods cg on p.ProductID=cg.ProductID where cg.Piece>0 ";
                if (entity.TypeID != 0)
                {
                    strSQL += " and p.TypeID=@TypeID";
                }
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    strSQL += " and p.Specs=@Specs";
                }
                if (!string.IsNullOrEmpty(entity.Figure))
                {
                    strSQL += " and p.Figure=@Figure";
                }
                if (!string.IsNullOrEmpty(entity.Model))
                {
                    strSQL += " and p.Model=@Model";
                }
                if (!string.IsNullOrEmpty(entity.GoodsCode))
                {
                    strSQL += " and p.GoodsCode=@GoodsCode";
                }
                if (!string.IsNullOrEmpty(entity.Batch))
                {
                    if (entity.Batch.Length == 2)
                    {
                        strSQL += " and RIGHT(p.Batch,2)=@Batch";
                    }
                    else if (entity.Batch.Length == 4)
                    {
                        strSQL += " and p.Batch=@Batch";
                    }
                }
                if (!string.IsNullOrEmpty(entity.Born))
                {
                    strSQL += " and p.Born=@Born";
                }
                if (!string.IsNullOrEmpty(entity.LoadIndex))
                {
                    strSQL += " and p.LoadIndex=@LoadIndex";
                }
                if (!string.IsNullOrEmpty(entity.SpeedLevel))
                {
                    strSQL += " and p.SpeedLevel=@SpeedLevel";
                }
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    if (entity.TypeID != 0)
                    {
                        conn.AddInParameter(command, "@TypeID", DbType.Int32, entity.TypeID);
                    }
                    if (!string.IsNullOrEmpty(entity.Specs))
                    {
                        conn.AddInParameter(command, "@Specs", DbType.String, entity.Specs);
                    }
                    if (!string.IsNullOrEmpty(entity.Figure))
                    {
                        conn.AddInParameter(command, "@Figure", DbType.String, entity.Figure);
                    }
                    if (!string.IsNullOrEmpty(entity.Model))
                    {
                        conn.AddInParameter(command, "@Model", DbType.String, entity.Model);
                    }
                    if (!string.IsNullOrEmpty(entity.Batch))
                    {
                        conn.AddInParameter(command, "@Batch", DbType.String, entity.Batch);
                    }
                    if (!string.IsNullOrEmpty(entity.Born))
                    {
                        conn.AddInParameter(command, "@Born", DbType.String, entity.Born);
                    }
                    if (!string.IsNullOrEmpty(entity.LoadIndex))
                    {
                        conn.AddInParameter(command, "@LoadIndex", DbType.String, entity.LoadIndex);
                    }
                    if (!string.IsNullOrEmpty(entity.SpeedLevel))
                    {
                        conn.AddInParameter(command, "@SpeedLevel", DbType.String, entity.SpeedLevel);
                    }
                    num = Convert.ToInt32(conn.ExecuteScalar(command));
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return num;
        }
        /// <summary>
        /// 根据条件查询所有产品
        /// </summary>
        /// <param name="GoodsCode"></param>
        /// <returns></returns>
        public List<CargoProductEntity> QueryPreOrderProductInfo(CargoProductEntity entity)
        {
            List<CargoProductEntity> result = new List<CargoProductEntity>();
            try
            {
                string strSQL = @"select cg.ID as ContainerGoodsID,*,ar.Name as AreaName from Tbl_Cargo_Product p inner join Tbl_Cargo_ContainerGoods cg on p.ProductID=cg.ProductID inner join Tbl_Cargo_Container c on cg.ContainerID=c.ContainerID inner join Tbl_Cargo_ProductType pt on p.TypeID=pt.TypeID inner join Tbl_Cargo_Area ar on c.AreaID=ar.AreaID where cg.Piece>0 ";
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    strSQL += " and p.Specs='" + entity.Specs + "'";
                }
                if (!string.IsNullOrEmpty(entity.Figure))
                {
                    strSQL += " and p.Figure='" + entity.Figure + "'";
                }
                if (!string.IsNullOrEmpty(entity.Model))
                {
                    strSQL += " and p.Model='" + entity.Model + "'";
                }
                if (!string.IsNullOrEmpty(entity.GoodsCode))
                {
                    strSQL += " and p.GoodsCode='" + entity.GoodsCode + "'";
                }
                if (!string.IsNullOrEmpty(entity.Batch))
                {
                    strSQL += " and p.Batch='" + entity.Batch + "'";
                }
                if (!string.IsNullOrEmpty(entity.Born))
                {
                    strSQL += " and p.Born='" + entity.Born + "'";
                }
                if (!entity.TypeID.Equals(0))
                {
                    strSQL += " and p.TypeID='" + entity.TypeID + "'";
                }
                if (!string.IsNullOrEmpty(entity.SpeedLevel))
                {
                    strSQL += " and p.SpeedLevel='" + entity.SpeedLevel + "'";
                }
                if (!string.IsNullOrEmpty(entity.LoadIndex))
                {
                    strSQL += " and p.LoadIndex='" + entity.LoadIndex + "'";
                }
                strSQL += " order by Batch";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new CargoProductEntity
                            {
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                Model = Convert.ToString(idr["Model"]),
                                AreaID = Convert.ToInt32(idr["AreaID"]),
                                AreaName = Convert.ToString(idr["AreaName"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                TreadWidth = Convert.ToInt32(idr["TreadWidth"]),
                                FlatRatio = Convert.ToInt32(idr["FlatRatio"]),
                                HubDiameter = Convert.ToInt32(idr["HubDiameter"]),
                                Meridian = Convert.ToString(idr["Meridian"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                Numbers = Convert.ToInt32(idr["Piece"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                UnitPrice = Convert.ToDecimal(idr["UnitPrice"]),
                                CostPrice = Convert.ToDecimal(idr["CostPrice"]),
                                FinalCostPrice = Convert.ToDecimal(idr["FinalCostPrice"]),
                                TaxCostPrice = Convert.ToDecimal(idr["TaxCostPrice"]),
                                NoTaxCostPrice = Convert.ToDecimal(idr["NoTaxCostPrice"]),
                                TradePrice = Convert.ToDecimal(idr["TradePrice"]),
                                SalePrice = Convert.ToDecimal(idr["SalePrice"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                BatchWeek = Convert.ToInt32(idr["BatchWeek"]),
                                BatchYear = Convert.ToInt32(idr["BatchYear"]),
                                Born = Convert.ToString(idr["Born"]),
                                Assort = Convert.ToString(idr["Assort"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                ContainerID = Convert.ToInt32(idr["ContainerID"]),
                                InPiece = Convert.ToInt32(idr["InPiece"]),
                                ContainerGoodsID = Convert.ToInt32(idr["ContainerGoodsID"]),
                                InCargoStatus = Convert.ToString(idr["InCargoStatus"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 修改预订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdatePreOrderInfo(CargoOrderEntity entity)
        {
            CargoClientManager client = new CargoClientManager();
            string strSQL = @"UPDATE Tbl_Cargo_PreOrder set LogisAwbNo=@LogisAwbNo,LogisID=@LogisID,Dep=@Dep,Dest=@Dest,Piece=@Piece,InsuranceFee=@InsuranceFee,TransitFee=@TransitFee,TransportFee=@TransportFee,DeliveryFee=@DeliveryFee,OtherFee=@OtherFee,TotalCharge=@TotalCharge,Rebate=@Rebate,CheckOutType=@CheckOutType,AcceptUnit=@AcceptUnit,AcceptPeople=@AcceptPeople,AcceptTelephone=@AcceptTelephone,AcceptCellphone=@AcceptCellphone,AcceptAddress=@AcceptAddress,OP_ID=@OP_ID,OP_DATE=@OP_DATE,Remark=@Remark,SaleManID=@SaleManID,SaleManName=@SaleManName,HouseID=@HouseID,ClientNum=@ClientNum,ThrowGood=@ThrowGood,PayClientName=@PayClientName,PayClientNum=@PayClientNum,PurchaserID=@PurchaserID,OutHouseName=@OutHouseName,IsPrintPrice=@IsPrintPrice Where OrderID=@OrderID and OrderNo=@OrderNo";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@InsuranceFee", DbType.Decimal, entity.InsuranceFee);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@DeliveryFee", DbType.Decimal, entity.DeliveryFee);
                    conn.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@Rebate", DbType.Decimal, entity.Rebate);
                    conn.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    //conn.AddInParameter(cmd, "@AwbStatus", DbType.String, entity.AwbStatus);
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                    conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@ThrowGood", DbType.String, entity.ThrowGood);
                    conn.AddInParameter(cmd, "@PayClientName", DbType.String, entity.PayClientName);
                    conn.AddInParameter(cmd, "@PayClientNum", DbType.String, entity.PayClientNum);
                    conn.AddInParameter(cmd, "@PurchaserID", DbType.String, entity.PurchaserID);
                    conn.AddInParameter(cmd, "@OutHouseName", DbType.String, entity.OutHouseName);
                    conn.AddInParameter(cmd, "@IsPrintPrice", DbType.String, entity.IsPrintPrice);
                    conn.ExecuteNonQuery(cmd);
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 修改订单数量和金额
        /// </summary>
        /// <param name="entity"></param>
        public void DrawUpPreOrderPieceCharge(CargoOrderEntity entity)
        {
            string strSQL = @"UPDATE Tbl_Cargo_PreOrder Set Piece=Piece+@Piece,TransportFee=TransportFee+@TransportFee,TotalCharge=TotalCharge+@TotalCharge Where OrderNo=@OrderNo";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改预订单中产品价格
        /// </summary>
        /// <param name="entity"></param>
        public void UpdatePreOrderSalePrice(CargoContainerShowEntity entity)
        {
            try
            {
                string strSQL = @"UPDATE Tbl_Cargo_PreOrderGoods SET ActSalePrice=@ActSalePrice";
                strSQL += " Where OrderNo=@OrderNo and GoodsCode=@GoodsCode ";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    conn.AddInParameter(cmd, "@ActSalePrice", DbType.Decimal, entity.ActSalePrice);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        #endregion
        #region 来货订单条码
        /// <summary>
        /// 查询来货订单条码
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryBarcodeData(int pIndex, int pNum, CargoFactoryOrderBarcodeEntity entity)
        {
            List<CargoFactoryOrderBarcodeEntity> result = new List<CargoFactoryOrderBarcodeEntity>();
            Hashtable resHT = new Hashtable();

            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.* from Tbl_Cargo_FactoryOrderBarcode a WHERE (1=1)";

                if (!string.IsNullOrEmpty(entity.VehicleNo)) { strSQL += " and a.VehicleNo like '%" + entity.VehicleNo + "%'"; }
                if (!string.IsNullOrEmpty(entity.Specs)) { strSQL += " and a.Specs like '%" + entity.Specs + "%'"; }
                if (!string.IsNullOrEmpty(entity.Figure)) { strSQL += " and a.Figure like '%" + entity.Figure + "%'"; }
                if (!string.IsNullOrEmpty(entity.Batch)) { strSQL += " and a.Batch like '%" + entity.Batch + "%'"; }
                if (!string.IsNullOrEmpty(entity.GoodsCode)) { strSQL += " and a.GoodsCode like '%" + entity.GoodsCode + "%'"; }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += ") as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    #region 获取数据
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CargoFactoryOrderBarcodeEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                VehicleNo = Convert.ToString(idr["VehicleNo"]),
                                Barcode = Convert.ToString(idr["Barcode"]),
                                RDC = Convert.ToString(idr["RDC"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                Description = Convert.ToString(idr["Description"]),
                                OutTime = Convert.ToDateTime(idr["OutTime"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])

                            });
                        }
                    }
                    #endregion
                }
                resHT["rows"] = result;
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_FactoryOrderBarcode as a Where (1=1)";
                if (!string.IsNullOrEmpty(entity.VehicleNo)) { strCount += " and a.VehicleNo like '%" + entity.VehicleNo + "%'"; }
                if (!string.IsNullOrEmpty(entity.Specs)) { strCount += " and a.Specs like '%" + entity.Specs + "%'"; }
                if (!string.IsNullOrEmpty(entity.Figure)) { strCount += " and a.Figure like '%" + entity.Figure + "%'"; }
                if (!string.IsNullOrEmpty(entity.Batch)) { strCount += " and a.Batch like '%" + entity.Batch + "%'"; }
                if (!string.IsNullOrEmpty(entity.GoodsCode)) { strCount += " and a.GoodsCode like '%" + entity.GoodsCode + "%'"; }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }

                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }

        /// <summary>
        /// 查询是否存在指定船运单号数据
        /// </summary>
        /// <param name="VehicleNo"></param>
        /// <returns></returns>
        public int IsExistBarcodeData(string VehicleNo)
        {
            string strQ = @"select * from Tbl_Cargo_FactoryOrderBarcode where VehicleNo='" + VehicleNo + "'";
            int count = 0;
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        count = 0;
                    }
                    if (idr.Rows.Count >= 0)
                    {
                        count = idr.Rows.Count;
                    }
                }
            }
            return count;
        }
        /// <summary>
        /// 新增来货订单条码
        /// </summary>
        /// <param name="entity"></param>
        public void InsertBarcodeData(CargoFactoryOrderBarcodeEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_Cargo_FactoryOrderBarcode(VehicleNo,Barcode,RDC,GoodsCode,Batch,Description,OutTime,Specs,Figure,LoadIndex,SpeedLevel,OP_DATE)VALUES(@VehicleNo,@Barcode,@RDC,@GoodsCode,@Batch,@Description,@OutTime,@Specs,@Figure,@LoadIndex,@SpeedLevel,@OP_DATE)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@VehicleNo", DbType.String, entity.VehicleNo);
                    conn.AddInParameter(cmd, "@Barcode", DbType.String, entity.Barcode);
                    conn.AddInParameter(cmd, "@RDC", DbType.String, entity.RDC);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    conn.AddInParameter(cmd, "@Batch", DbType.String, entity.Batch);
                    conn.AddInParameter(cmd, "@Description", DbType.String, entity.Description);
                    conn.AddInParameter(cmd, "@OutTime", DbType.DateTime, entity.OutTime);
                    conn.AddInParameter(cmd, "@Specs", DbType.String, entity.Specs);
                    conn.AddInParameter(cmd, "@Figure", DbType.String, entity.Figure);
                    conn.AddInParameter(cmd, "@LoadIndex", DbType.String, entity.LoadIndex);
                    conn.AddInParameter(cmd, "@SpeedLevel", DbType.String, entity.SpeedLevel);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 通过条件查询船运数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoFactoryOrderBarcodeEntity> QueryBarcodeInfo(CargoFactoryOrderBarcodeEntity entity)
        {
            List<CargoFactoryOrderBarcodeEntity> result = new List<CargoFactoryOrderBarcodeEntity>();
            string strSQL = "select * from Tbl_Cargo_FactoryOrderBarcode where 1=1";
            if (!string.IsNullOrEmpty(entity.VehicleNo)) { strSQL += " and VehicleNo like '%" + entity.VehicleNo + "%'"; }
            if (!string.IsNullOrEmpty(entity.Barcode)) { strSQL += " and Barcode like '%" + entity.Barcode + "%'"; }
            if (!string.IsNullOrEmpty(entity.Specs)) { strSQL += " and Specs like '%" + entity.Specs + "%'"; }
            if (!string.IsNullOrEmpty(entity.Figure)) { strSQL += " and Figure like '%" + entity.Figure + "%'"; }
            if (!string.IsNullOrEmpty(entity.Batch)) { strSQL += " and Batch like '%" + entity.Batch + "%'"; }
            if (!string.IsNullOrEmpty(entity.GoodsCode)) { strSQL += " and GoodsCode like '%" + entity.GoodsCode + "%'"; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoFactoryOrderBarcodeEntity
                        {
                            ID = Convert.ToInt64(idr["ID"]),
                            VehicleNo = Convert.ToString(idr["VehicleNo"]),
                            Barcode = Convert.ToString(idr["Barcode"]),
                            RDC = Convert.ToString(idr["RDC"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            Description = Convert.ToString(idr["Description"]),
                            OutTime = Convert.ToDateTime(idr["OutTime"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                        });
                    }
                }
            }
            return result;
        }
        #endregion
        #region 处理好来运系统订单签收
        /// <summary>
        /// 处理好来运系统的运单签收状态，同步修改智能系统订单物流状态
        /// </summary>
        /// <param name="entity"></param>
        public void SetOrderSignByHLY(CargoOrderEntity entity)
        {
            string strSQL = "select OrderID,OrderNo,LogisAwbNo,AwbStatus,OpenOrderNo,ShareHouseID From Tbl_Cargo_Order where (1=1) and AwbStatus!=5 and LogisAwbNo!=''";
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and HouseID in (" + entity.CargoPermisID + ")"; }
            //公司类型
            if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and BelongHouse = " + entity.BelongHouse; }
            if (entity.LogisID.Equals(0)) { strSQL += " and LogisID=" + entity.LogisID; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        if (IsHLYSign("签收", Convert.ToString(idr["LogisAwbNo"]), out string SignTime, out string Puser))
                        {
                            UpdateAwbStatusByOrderNo(Convert.ToInt64(idr["OrderID"]), Convert.ToString(idr["OrderNo"]), "5", SignTime, Puser, "0");
                            if (!IsExistCargoOrderCheckStatus(new CargoOrderStatusEntity { OrderNo = Convert.ToString(idr["OrderNo"]), OrderStatus = "5" }))
                            {
                                InsertCargoOrderStatus(new CargoOrderStatusEntity { OrderID = Convert.ToInt64(idr["OrderID"]), OrderNo = Convert.ToString(idr["OrderNo"]), OrderStatus = "5", OP_DATE = Convert.ToDateTime(SignTime), SignTime = Convert.ToDateTime(SignTime), Signer = Puser });
                            }
                            //处理共享前置仓的订单签收数据
                            if (!string.IsNullOrEmpty(Convert.ToString(idr["OpenOrderNo"])) && !Convert.ToInt32(idr["ShareHouseID"]).Equals(0))
                            {
                                UpdateAwbStatusByOrderNo(0, Convert.ToString(idr["OpenOrderNo"]), "5", SignTime, Puser, "0");
                                if (!IsExistCargoOrderCheckStatus(new CargoOrderStatusEntity { OrderNo = Convert.ToString(idr["OpenOrderNo"]), OrderStatus = "5" }))
                                {
                                    List<CargoOrderEntity> coe = QueryOrderDataInfo(new CargoOrderEntity { OrderNo = Convert.ToString(idr["OpenOrderNo"]), HouseID = Convert.ToInt32(idr["ShareHouseID"]) });
                                    if (coe.Count > 0)
                                    {
                                        InsertCargoOrderStatus(new CargoOrderStatusEntity { OrderID = coe[0].OrderID, OrderNo = Convert.ToString(idr["OpenOrderNo"]), OrderStatus = "5", OP_DATE = Convert.ToDateTime(SignTime), SignTime = Convert.ToDateTime(SignTime), Signer = Puser });
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void SetOrderStatusByHLY(CargoOrderEntity entity)
        {
            string strSQL = "select OrderID,OrderNo,LogisAwbNo,AwbStatus From Tbl_Cargo_Order where (1=1) and AwbStatus=2 and LogisAwbNo!=''";
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            else
            {
                strSQL += " and CreateDate>=dateadd(day,-1,getdate())";
            }
            if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and HouseID in (" + entity.CargoPermisID + ")"; }
            //公司类型
            if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and BelongHouse = " + entity.BelongHouse; }
            if (entity.LogisID.Equals(0)) { strSQL += " and LogisID=" + entity.LogisID; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        if (IsHLYSign("配载", Convert.ToString(idr["LogisAwbNo"]), out string SignTime, out string Puser))
                        {
                            UpdateAwbStatusByOrderNo(Convert.ToInt64(idr["OrderID"]), Convert.ToString(idr["OrderNo"]), "3", SignTime, "", "0");
                            InsertCargoOrderStatus(new CargoOrderStatusEntity { OrderID = Convert.ToInt64(idr["OrderID"]), OrderNo = Convert.ToString(idr["OrderNo"]), OrderStatus = "3", OP_DATE = DateTime.Now });
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 判断好来运系统是否拥有对应状态
        /// </summary>
        /// <param name="awbno">好来运运单号</param>
        /// <returns>True:签收 False:未签收</returns>
        private bool IsHLYSign(string type, string awbno, out string SignTime, out string Puser)
        {
            SignTime = ""; Puser = "";
            SqlHelper hlySql = new SqlHelper("HLYSql");
            string strSQL = "select Id,Indate,Puser From tbl_state where awbno=@awbno and ptype='" + type + "'";
            using (DbCommand command = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(command, "@awbno", DbType.String, awbno);
                using (DataTable idr = hlySql.ExecuteDataTable(command))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                    SignTime = idr.Rows[0]["Indate"].ToString();
                    Puser = idr.Rows[0]["Puser"].ToString();
                }
            }
            return true;
        }
        #endregion
        #region 大屏可视化方法集合

        public bool IsExistOwlOrderInfo(string OrderNo)
        {
            string strSQL = "Select OrderNo from Tbl_Owl_Order where OrderNo=@OrderNo";

            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@OrderNo", DbType.String, OrderNo);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 新增订单数据
        /// </summary>
        /// <param name="OrderNo"></param>
        public void AddOwlOrderData(string OrderNo)
        {
            if (!IsExistOwlOrderInfo(OrderNo))
            {
                string strSQL = @"Insert into dbo.Tbl_Owl_Order([OrderNo],[Dep],[Dest],[Piece],[TransportFee],[TotalCharge],[CheckOutType],[TrafficType],[DeliveryType],[AcceptUnit],[AcceptPeople],[CreateAwb],[CreateDate],[AwbStatus],[SaleManName],[OrderType],[OrderModel],[HouseName],[PayClientName],[BelongHouse],CreateDateTime,ThrowGood) select a.OrderNo,a.Dep,a.Dest,a.Piece,a.TransportFee,a.TotalCharge,a.CheckOutType,a.TrafficType,a.DeliveryType,a.AcceptUnit,a.AcceptPeople,a.CreateAwb,a.CreateDate,a.AwbStatus,a.SaleManName,a.OrderType,a.OrderModel,case when a.HouseID=44 then a.OutHouseName when a.HouseID=45 then a.OutHouseName else b.Name end as HouseName,a.PayClientName,a.BelongHouse,a.CreateDate,a.ThrowGood From Tbl_Cargo_Order as a inner join Tbl_Cargo_House as b on a.HouseID=b.HouseID where a.OrderNo=@OrderNo";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                    conn.ExecuteNonQuery(cmd);
                }
                string strGoodSQL = @"INSERT INTO [Tbl_Owl_OrderGoods]([OrderNo],[ProductID],[Piece],[ActSalePrice],[TypeParentName],[TypeName],[GoodsCode],[Specs],[Figure],[TreadWidth],[FlatRatio],[HubDiameter],[LoadIndex],[SpeedLevel],[Batch],[BatchYear],[BatchWeek],[Source],[BelongDepart],[OperaType],[Born]) select a.OrderNo,a.ProductID,a.Piece,a.ActSalePrice,d.TypeParentName,d.TypeName,c.GoodsCode,c.Specs,c.Figure,c.TreadWidth,c.FlatRatio,c.HubDiameter,c.LoadIndex,c.SpeedLevel,c.Batch,c.BatchYear,c.BatchWeek,c.Source,c.BelongDepart,c.OperaType,c.Born From Tbl_Cargo_OrderGoods as a inner join Tbl_Cargo_Product as c on a.ProductID=c.ProductID and a.HouseID=c.HouseID inner join Tbl_Cargo_ProductType as d on c.TypeID=d.TypeID where a.OrderNo=@OrderNo";
                using (DbCommand cmdGood = conn.GetSqlStringCommond(strGoodSQL))
                {
                    conn.AddInParameter(cmdGood, "@OrderNo", DbType.String, OrderNo);
                    conn.ExecuteNonQuery(cmdGood);
                }
            }
        }
        /// <summary>
        /// 删除大屏可视化订单数据
        /// </summary>
        /// <param name="OrderNo"></param>
        public void DeleteOwlOrderData(string OrderNo)
        {
            string strSQL = @"Delete from Tbl_Owl_Order Where OrderNo=@OrderNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
            string strGoodSQL = @"Delete from Tbl_Owl_OrderGoods Where OrderNo=@OrderNo";
            using (DbCommand cmdGood = conn.GetSqlStringCommond(strGoodSQL))
            {
                conn.AddInParameter(cmdGood, "@OrderNo", DbType.String, OrderNo);
                conn.ExecuteNonQuery(cmdGood);
            }
        }
        #endregion
        #region 采购订单方法集合

        /// <summary>
        /// 查询采购订单数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryOesPreOrderInfo(int pIndex, int pNum, CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,b.LogisticName,c.Name as PurchaserName from Tbl_Cargo_PreOrder as a inner join Tbl_Cargo_House c on a.PurchaseHouseID=c.HouseID left join tbl_cargo_Logistic as b on a.LogisID=b.ID Where (1=1) ";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and a.OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.OutHouseName)) { strSQL += " and a.OutHouseName = '" + entity.OutHouseName + "'"; }
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strSQL += " and (a.AwbStatus='0' or a.AwbStatus='1')";
                    }
                    else
                    {
                        strSQL += " and a.AwbStatus='" + entity.AwbStatus + "'";
                    }
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strSQL += " and a.CheckOutType = '" + entity.CheckOutType + "'"; }
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strSQL += " and a.FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and a.AcceptPeople = '" + entity.AcceptPeople + "'"; }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strSQL += " and a.PayClientName = '" + entity.PayClientName + "'"; }
                if (!entity.PayClientNum.Equals(0)) { strSQL += " and a.PayClientNum=" + entity.PayClientNum; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strSQL += " and a.Dep = '" + entity.Dep + "'"; }
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
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //物流单号
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strSQL += " and a.LogisAwbNo = '" + entity.LogisAwbNo + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID ='" + entity.SaleManID + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood)) { strSQL += " and a.ThrowGood ='" + entity.ThrowGood + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and a.Piece=" + entity.Piece + ""; }
                //公司类型
                if (!string.IsNullOrEmpty(entity.BelongHouse)) { strSQL += " and a.BelongHouse = " + entity.BelongHouse; }
                //客户名称
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and a.AcceptUnit = '" + entity.AcceptUnit + "'"; }

                if (Convert.ToString(entity.IsMakeSure) != "-1")
                {
                    strSQL += " and a.IsMakeSure = '" + entity.IsMakeSure + "'";
                }
                if (!string.IsNullOrEmpty(entity.PurchaserID)) { strSQL += " and a.PurchaseHouseID in (" + entity.PurchaserID + ")"; }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                InsuranceFee = Convert.ToDecimal(idr["InsuranceFee"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                DeliveryFee = Convert.ToDecimal(idr["DeliveryFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Rebate = Convert.ToDecimal(idr["Rebate"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                LogisticName = Convert.ToString(idr["LogisticName"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                SaleManID = Convert.ToString(idr["SaleManID"]),
                                SaleManName = Convert.ToString(idr["SaleManName"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                OrderType = Convert.ToString(idr["OrderType"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),
                                OutHouseName = Convert.ToString(idr["OutHouseName"]),
                                PayClientName = Convert.ToString(idr["PayClientName"]),
                                PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                                IsMakeSure = Convert.ToInt32(idr["IsMakeSure"]),
                                IsPrintPrice = Convert.ToInt32(idr["IsPrintPrice"]),
                                PurchaserID = Convert.ToString(idr["PurchaseHouseID"]),
                                PurchaserName = Convert.ToString(idr["PurchaserName"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_PreOrder  Where (1=1)";
                //下单方式
                if (!string.IsNullOrEmpty(entity.OrderType)) { strCount += " and OrderType = '" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.OutHouseName)) { strCount += " and OutHouseName = '" + entity.OutHouseName + "'"; }
                //订单状态
                if (!string.IsNullOrEmpty(entity.AwbStatus))
                {
                    if (entity.AwbStatus.Equals("0"))
                    {
                        strCount += " and (AwbStatus='0' or AwbStatus='1')";
                    }
                    else
                    {
                        strCount += " and AwbStatus='" + entity.AwbStatus + "'";
                    }
                }
                //结款方式
                if (!string.IsNullOrEmpty(entity.CheckOutType)) { strCount += " and CheckOutType = '" + entity.CheckOutType + "'"; }
                if (!string.IsNullOrEmpty(entity.FinanceSecondCheck)) { strCount += " and FinanceSecondCheck = '" + entity.FinanceSecondCheck + "'"; }
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strCount += " and AcceptPeople = '" + entity.AcceptPeople + "'"; }
                if (!string.IsNullOrEmpty(entity.PayClientName)) { strCount += " and PayClientName = '" + entity.PayClientName + "'"; }
                if (!entity.PayClientNum.Equals(0)) { strCount += " and PayClientNum=" + entity.PayClientNum; }
                //出发站
                if (!string.IsNullOrEmpty(entity.Dep)) { strCount += " and Dep = '" + entity.Dep + "'"; }
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
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strCount += " and OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //物流单号
                if (!string.IsNullOrEmpty(entity.LogisAwbNo)) { strCount += " and LogisAwbNo = '" + entity.LogisAwbNo + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strCount += " and CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strCount += " and SaleManID ='" + entity.SaleManID + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood)) { strCount += " and ThrowGood ='" + entity.ThrowGood + "'"; }
                //件数
                if (!entity.Piece.Equals(0)) { strCount += " and Piece=" + entity.Piece + ""; }
                //公司类型
                if (!string.IsNullOrEmpty(entity.BelongHouse)) { strCount += " and BelongHouse = " + entity.BelongHouse; }
                //客户名称
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strCount += " and AcceptUnit = '" + entity.AcceptUnit + "'"; }

                if (Convert.ToString(entity.IsMakeSure) != "-1")
                {
                    strCount += " and IsMakeSure = '" + entity.IsMakeSure + "'";
                }
                if (!string.IsNullOrEmpty(entity.PurchaserID)) { strCount += " and PurchaseHouseID in (" + entity.PurchaserID + ")"; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 查询采购订单产品详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public List<CargoContainerShowEntity> QueryOesPreOrderByOrderNo(CargoOrderEntity entity)
        {
            CargoProductManager pro = new CargoProductManager();
            CargoHouseManager house = new CargoHouseManager();
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT a.OrderNo, a.HouseID, a.Piece, ISNULL(a.ActSalePrice, 0) AS ActSalePrice, a.GoodsCode, a.Remark, a.specs, a.batch,a.TypeID,a.Figure,a.SpeedLevel,a.LoadIndex,a.Born,a.Model,a.PurchaserID,a.PurchaserName,a.DeliveryBoss as PurchaserBoss,a.DeliveryCellphone as PurchaserCellphone,a.DeliveryAddress as PurchaserAddress,a.ConfirmSalePrice, e.TypeName, e.ParentID,a.OP_DATE FROM Tbl_Cargo_PreOrderGoods AS a LEFT join tbl_cargo_Purchaser c on a.PurchaserID=c.PurchaserID LEFT JOIN Tbl_Cargo_ProductType AS e ON a.TypeID=e.TypeID  WHERE a.OrderNo=@OrderNo";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            CargoContainerShowEntity e = new CargoContainerShowEntity();
                            e.OrderNo = Convert.ToString(idr["OrderNo"]);
                            e.HouseID = Convert.ToInt32(idr["HouseID"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);
                            e.Figure = Convert.ToString(idr["Figure"]);
                            e.UnitPrice = Convert.ToDecimal(idr["ActSalePrice"]);
                            e.OldUnitPrice = e.UnitPrice;
                            e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            e.RuleTitle = Convert.ToString(idr["Remark"]);
                            e.Specs = Convert.ToString(idr["Specs"]);
                            e.Batch = Convert.ToString(idr["Batch"]);
                            e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            e.TypeID = Convert.ToInt32(idr["TypeID"]);
                            e.TypeName = Convert.ToString(idr["TypeName"]);
                            e.Born = Convert.ToString(idr["Born"]);
                            e.Model = Convert.ToString(idr["Model"]);
                            e.TypeParentID = Convert.ToInt32(idr["ParentID"]);
                            e.PurchaserID = Convert.ToString(idr["PurchaserID"]);
                            e.PurchaserName = Convert.ToString(idr["PurchaserName"]);
                            e.PurchaserBoss = Convert.ToString(idr["PurchaserBoss"]);
                            e.PurchaserCellphone = Convert.ToString(idr["PurchaserCellphone"]);
                            e.PurchaserAddress = Convert.ToString(idr["PurchaserAddress"]);
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            e.ConfirmSalePrice = string.IsNullOrEmpty(Convert.ToString(idr["ConfirmSalePrice"])) ? 0 : Convert.ToDecimal(idr["ConfirmSalePrice"]);
                            result.Add(e);
                            #endregion
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public CargoOrderEntity QueryOesPreOrderInfo(CargoOrderEntity entity)
        {
            CargoOrderEntity result = new CargoOrderEntity();
            string strSQL = "select * from Tbl_Cargo_PreOrder where 1=1 ";
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and OrderNo='" + entity.OrderNo + "'"; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result = new CargoOrderEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            OrderNum = Convert.ToInt32(idr["OrderNum"]),
                            LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                            LogisID = Convert.ToInt32(idr["LogisID"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                            TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                            OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                            TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                            CheckOutType = Convert.ToString(idr["CheckOutType"]),
                            ClientNum = Convert.ToInt32(idr["ClientNum"]),
                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                            AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                            AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                            AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                            CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                            CreateAwb = Convert.ToString(idr["CreateAwb"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            SaleManID = Convert.ToString(idr["SaleManID"]),
                            SaleManName = Convert.ToString(idr["SaleManName"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            OutHouseName = Convert.ToString(idr["OutHouseName"]),
                            OP_ID = Convert.ToString(idr["OP_ID"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            AwbStatus = Convert.ToString(idr["AwbStatus"]),
                            Remark = Convert.ToString(idr["Remark"]),
                            OrderType = Convert.ToString(idr["OrderType"]),
                            ThrowGood = Convert.ToString(idr["ThrowGood"]),
                            PayClientNum = Convert.ToInt32(idr["PayClientNum"]),
                            PayClientName = Convert.ToString(idr["PayClientName"]),
                            IsMakeSure = string.IsNullOrEmpty(Convert.ToString(idr["PayClientName"])) ? -1 : Convert.ToInt32(idr["PayClientName"]),
                            IsPrintPrice = Convert.ToInt32(idr["IsPrintPrice"]),
                            PurchaseRemark = Convert.ToString(idr["PurchaseRemark"])

                        };
                    }
                }
            }
            return result;
        }
        public CargoOrderGoodsEntity QueryOesPreOrderInfo(CargoOrderGoodsEntity entity)
        {
            CargoOrderGoodsEntity result = new CargoOrderGoodsEntity();
            string strSQL = "select * from Tbl_Cargo_PreOrderGoods where 1=1 ";
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and OrderNo='" + entity.OrderNo + "'"; }
            if (!string.IsNullOrEmpty(entity.Specs)) { strSQL += " and Specs='" + entity.Specs + "'"; }
            if (!string.IsNullOrEmpty(entity.Figure)) { strSQL += " and Figure='" + entity.Figure + "'"; }
            if (!string.IsNullOrEmpty(entity.Model)) { strSQL += " and Model='" + entity.Model + "'"; }
            if (!string.IsNullOrEmpty(entity.GoodsCode)) { strSQL += " and GoodsCode='" + entity.GoodsCode + "'"; }
            if (!string.IsNullOrEmpty(entity.Batch)) { strSQL += " and Batch='" + entity.Batch + "'"; }
            if (entity.TypeID != 0) { strSQL += " and TypeID=" + entity.TypeID + ""; }
            if (!string.IsNullOrEmpty(entity.Born)) { strSQL += " and Born='" + entity.Born + "'"; }
            if (!string.IsNullOrEmpty(entity.LoadIndex)) { strSQL += " and LoadIndex='" + entity.LoadIndex + "'"; }
            if (!string.IsNullOrEmpty(entity.SpeedLevel)) { strSQL += " and SpeedLevel='" + entity.SpeedLevel + "'"; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result = new CargoOrderGoodsEntity
                        {
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            Model = Convert.ToString(idr["Model"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            TypeID = Convert.ToInt32(idr["TypeID"]),
                            Born = Convert.ToString(idr["Born"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            PurchaserID = Convert.ToInt32(idr["PurchaserID"]),
                            PurchaserName = Convert.ToString(idr["PurchaserName"])
                        };
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 采购单确认数据移至订单明细表
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ApplicationException"></exception>
        public void OesPreOrderGoodsConfirm(CargoOrderGoodsEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"insert into Tbl_Cargo_OrderGoods (OrderNo,ProductID,HouseID,AreaID,ContainerCode,Piece ,ActSalePrice,OP_ID,OP_DATE,OutCargoID) select OrderNo,@ProductID,@HouseID,@AreaID,@ContainerCode,@Piece,@ActSalePrice,OP_ID,OP_DATE,@OutCargoID from Tbl_Cargo_PreOrderGoods where OrderNo=@OrderNo and GoodsCode=@GoodsCode";

            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, entity.ProductID);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@AreaID", DbType.String, entity.AreaID);
                    conn.AddInParameter(cmd, "@ContainerCode", DbType.String, entity.ContainerCode);
                    conn.AddInParameter(cmd, "@Piece", DbType.String, entity.Piece);
                    conn.AddInParameter(cmd, "@ActSalePrice", DbType.String, entity.ActSalePrice);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    conn.AddInParameter(cmd, "@OutCargoID", DbType.String, entity.OutCargoID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void OesPreOrderNewConfirm(CargoOrderEntity entity)
        {
            string strSQL = "update Tbl_Cargo_PreOrder set IsMakeSure=@IsMakeSure where OrderNo=@OrderNo";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "IsMakeSure", DbType.String, entity.IsMakeSure);
                    conn.AddInParameter(cmd, "OrderNo", DbType.String, entity.OrderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 采购单确认数据移至订单表
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ApplicationException"></exception>
        public void OesPreOrderConfirm(CargoOrderEntity entity)
        {
            string strSQL = @"insert into Tbl_Cargo_Order (OrderNo,OrderNum,LogisAwbNo,LogisID,Dep,Dest,Piece,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,ClientNum,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,CreateAwbID,CreateAwb,CreateDate,CheckStatus,SaleManID,SaleManName,SaleCellPhone,HouseID,OutHouseName,OP_ID,OP_DATE,AwbStatus,Remark,OrderType,ThrowGood,PayClientNum,PayClientName,TrafficType,IsPrintPrice,PostponeShip,BusinessID,ShopCode,HAwbNo) select OrderNo,@OrderNum,LogisAwbNo,LogisID,Dep,Dest,Piece,InsuranceFee,TransitFee,TransportFee,DeliveryFee,OtherFee,TotalCharge,Rebate,CheckOutType,ClientNum,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,CreateAwbID,CreateAwb,@CreateDate,CheckStatus,SaleManID,SaleManName,SaleCellPhone,HouseID,OutHouseName,OP_ID,@OP_DATE,AwbStatus,Remark,OrderType,ThrowGood,PayClientNum,PayClientName,2,IsPrintPrice,@PostponeShip,@BusinessID,@ShopCode,@HAwbNo from Tbl_Cargo_PreOrder where OrderNo=@OrderNo";

            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@PostponeShip", DbType.String, entity.PostponeShip);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@BusinessID", DbType.Int32, entity.BusinessID);
                    conn.AddInParameter(cmd, "@ShopCode", DbType.String, entity.ShopCode);
                    conn.AddInParameter(cmd, "@HAwbNo", DbType.String, entity.HAwbNo);
                    conn.ExecuteNonQuery(cmd);
                }

                UpdatePreOrderConfirm(entity.OrderNo);
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 保存数据至好来运采购单表
        /// </summary>
        /// <param name="entity"></param>
        public void SaveHlyOesOrderData(CargoPurchaserEntity entity)
        {
            //好来运数据库的数据连接
            SqlHelper hlySql = new SqlHelper("HLYSql");
            entity.EnSafe();
            string strSQL = "insert into Tbl_TakeGoods(OrderNO,Piece,CheckOutType,PurchaserUnit,PurchaserAddress,PurchaserPeople,PurchaserTelephone,CreateDate,PurchaserStatus,Remark) values (@OrderNO,@Piece,@CheckOutType,@PurchaserUnit,@PurchaserAddress,@PurchaserPeople,@PurchaserTelephone,@CreateDate,@PurchaserStatus,@Remark)";

            using (DbCommand cmd = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(cmd, "@OrderNO", DbType.String, entity.OrderNo);
                hlySql.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                hlySql.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutName);
                hlySql.AddInParameter(cmd, "@PurchaserUnit", DbType.String, entity.PurchaserName);
                hlySql.AddInParameter(cmd, "@PurchaserAddress", DbType.String, entity.Address);
                hlySql.AddInParameter(cmd, "@PurchaserPeople", DbType.String, entity.Boss);
                hlySql.AddInParameter(cmd, "@PurchaserTelephone", DbType.String, entity.Telephone);
                hlySql.AddInParameter(cmd, "@CreateDate", DbType.DateTime, DateTime.Now);
                hlySql.AddInParameter(cmd, "@PurchaserStatus", DbType.Int32, entity.PurchaserStatus);
                hlySql.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                hlySql.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 保存数据至好来运采购单明细表
        /// </summary>
        /// <param name="entity"></param>
        public void SaveHlyOesOrderGoodsData(CargoProductEntity entity)
        {
            //好来运数据库的数据连接
            SqlHelper hlySql = new SqlHelper("HLYSql");
            entity.EnSafe();
            string strSQL = "insert into Tbl_TakeGoodsList(OrderNO,TypeID,TypeName,Specs,Figure,GoodsCode,LoadIndex,SpeedLevel,Born,Batch,Piece,ActSalePrice,Model) values (@OrderNO,@TypeID,@TypeName,@Specs,@Figure,@GoodsCode,@LoadIndex,@SpeedLevel,@Born,@Batch,@Piece,@ActSalePrice,@Model)";

            using (DbCommand cmd = hlySql.GetSqlStringCommond(strSQL))
            {
                hlySql.AddInParameter(cmd, "@OrderNO", DbType.String, entity.OrderNo);
                hlySql.AddInParameter(cmd, "@TypeID", DbType.Int32, entity.TypeID);
                hlySql.AddInParameter(cmd, "@TypeName", DbType.String, entity.TypeName);
                hlySql.AddInParameter(cmd, "@Specs", DbType.String, entity.Specs);
                hlySql.AddInParameter(cmd, "@Figure", DbType.String, entity.Figure);
                hlySql.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                hlySql.AddInParameter(cmd, "@LoadIndex", DbType.String, entity.LoadIndex);
                hlySql.AddInParameter(cmd, "@SpeedLevel", DbType.String, entity.SpeedLevel);
                hlySql.AddInParameter(cmd, "@Born", DbType.String, entity.Born);
                hlySql.AddInParameter(cmd, "@Batch", DbType.String, entity.Batch);
                hlySql.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Numbers);
                hlySql.AddInParameter(cmd, "@ActSalePrice", DbType.Decimal, entity.UnitPrice);
                hlySql.AddInParameter(cmd, "@Model", DbType.String, entity.Model);
                hlySql.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改采购订单订单号为正式订单号
        /// </summary>
        /// <param name="NewOrderNo"></param>
        /// <param name="OldOrderNo"></param>
        /// <exception cref="ApplicationException"></exception>
        public void UpdatePurchaserOrderNo(string NewOrderNo, string OldOrderNo, decimal TransportFee, string PurchaserID = null)
        {
            string strSQL = @"update Tbl_Cargo_PreOrder set OrderNo=@NewOrderNo,PurchaseNo=@PurchaseNo,TransportFee=@TransportFee,TotalCharge=(InsuranceFee+TransitFee+@TransportFee+DeliveryFee+OtherFee) ";
            if (!string.IsNullOrEmpty(PurchaserID))
            {
                strSQL += " ,PurchaserID='" + PurchaserID + "'";
            }
            strSQL += " where OrderNo=@OldOrderNo";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@NewOrderNo", DbType.String, NewOrderNo);
                    conn.AddInParameter(cmd, "@OldOrderNo", DbType.String, OldOrderNo);
                    conn.AddInParameter(cmd, "@PurchaseNo", DbType.String, OldOrderNo);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, TransportFee);
                    conn.ExecuteNonQuery(cmd);
                }
                strSQL = @"update Tbl_Cargo_PreOrderGoods set OrderNo=@NewOrderNo where OrderNo=@OldOrderNo";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@NewOrderNo", DbType.String, NewOrderNo);
                    conn.AddInParameter(cmd, "@OldOrderNo", DbType.String, OldOrderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除好来运数据库采购单数据
        /// </summary>
        /// <param name="OrderNo"></param>
        public void DeleteHlyPurchaserOrder(string OrderNo)
        {
            //好来运数据库的数据连接
            SqlHelper hlySql = new SqlHelper("HLYSql");
            string strSQL = "delete from Tbl_TakeGoods where OrderNo=@OrderNo";

            using (DbCommand cmd = hlySql.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
            strSQL = "delete from Tbl_TakeGoodsList where OrderNo=@OrderNo";

            using (DbCommand cmd = hlySql.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                conn.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 修改采购订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdatePurchaserOrderInfo(CargoOrderEntity entity)
        {
            CargoClientManager client = new CargoClientManager();
            string strSQL = @"UPDATE Tbl_Cargo_PreOrder set TransportFee=@TransportFee,TotalCharge=@TotalCharge,IsMakeSure=@IsMakeSure Where OrderID=@OrderID and OrderNo=@OrderNo";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@IsMakeSure", DbType.Int32, entity.IsMakeSure);
                    conn.ExecuteNonQuery(cmd);
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void UpdatePurchaserOrderGoodsInfo(List<CargoOrderGoodsEntity> goods)
        {
            try
            {
                foreach (var it in goods)
                {
                    it.EnSafe();
                    string strSQL = @"UPDATE Tbl_Cargo_PreOrderGoods set Batch=@Batch,ActSalePrice=@ActSalePrice,PurchaserID=@PurchaserID,PurchaserName=@PurchaserName,OP_ID=@OP_ID,OP_DATE=@OP_DATE where HouseID=@HouseID and OrderNo=@OrderNo and Specs=@Specs and Figure=@Figure and Model=@Model and GoodsCode=@GoodsCode and TypeID=@TypeID and Born=@Born and LoadIndex=@LoadIndex and SpeedLevel=@SpeedLevel";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@Batch", DbType.String, it.Batch);
                        conn.AddInParameter(cmd, "@ActSalePrice", DbType.Decimal, it.ActSalePrice);
                        conn.AddInParameter(cmd, "@PurchaserID", DbType.String, it.PurchaserID);
                        conn.AddInParameter(cmd, "@PurchaserName", DbType.String, it.PurchaserName);
                        conn.AddInParameter(cmd, "@OP_ID", DbType.String, it.OP_ID);
                        conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                        conn.AddInParameter(cmd, "@HouseID", DbType.Int32, it.HouseID);
                        conn.AddInParameter(cmd, "@OrderNo", DbType.String, it.OrderNo.ToUpper());
                        conn.AddInParameter(cmd, "@Specs", DbType.String, it.Specs);
                        conn.AddInParameter(cmd, "@Figure", DbType.String, it.Figure);
                        conn.AddInParameter(cmd, "@Model", DbType.String, it.Model);
                        conn.AddInParameter(cmd, "@GoodsCode", DbType.String, it.GoodsCode);
                        conn.AddInParameter(cmd, "@TypeID", DbType.String, it.TypeID);
                        conn.AddInParameter(cmd, "@Born", DbType.String, it.Born);
                        conn.AddInParameter(cmd, "@LoadIndex", DbType.String, it.LoadIndex);
                        conn.AddInParameter(cmd, "@SpeedLevel", DbType.String, it.SpeedLevel);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 修改采购订单产品确认销售价
        /// </summary>
        /// <param name="goods"></param>
        /// <exception cref="ApplicationException"></exception>
        public void UpdatePurchaserOrderGoodsSalePrice(List<CargoOrderGoodsEntity> goods)
        {
            try
            {
                foreach (var it in goods)
                {
                    it.EnSafe();
                    string strSQL = @"UPDATE Tbl_Cargo_PreOrderGoods set ConfirmSalePrice=@ConfirmSalePrice,OP_ID=@OP_ID,OP_DATE=@OP_DATE where HouseID=@HouseID and OrderNo=@OrderNo and Specs=@Specs and Figure=@Figure and Model=@Model and GoodsCode=@GoodsCode and TypeID=@TypeID and Born=@Born and LoadIndex=@LoadIndex and SpeedLevel=@SpeedLevel";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@OP_ID", DbType.String, it.OP_ID);
                        conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                        conn.AddInParameter(cmd, "@HouseID", DbType.Int32, it.HouseID);
                        conn.AddInParameter(cmd, "@OrderNo", DbType.String, it.OrderNo.ToUpper());
                        conn.AddInParameter(cmd, "@Specs", DbType.String, it.Specs);
                        conn.AddInParameter(cmd, "@Figure", DbType.String, it.Figure);
                        conn.AddInParameter(cmd, "@Model", DbType.String, it.Model);
                        conn.AddInParameter(cmd, "@GoodsCode", DbType.String, it.GoodsCode);
                        conn.AddInParameter(cmd, "@TypeID", DbType.String, it.TypeID);
                        conn.AddInParameter(cmd, "@Born", DbType.String, it.Born);
                        conn.AddInParameter(cmd, "@LoadIndex", DbType.String, it.LoadIndex);
                        conn.AddInParameter(cmd, "@SpeedLevel", DbType.String, it.SpeedLevel);
                        conn.AddInParameter(cmd, "@ConfirmSalePrice", DbType.String, it.ConfirmSalePrice);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 取消采购订单
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ApplicationException"></exception>
        public void CancelPurchaserOrder(CargoOrderEntity entity)
        {
            CargoClientManager client = new CargoClientManager();
            string strSQL = @"UPDATE Tbl_Cargo_PreOrder set IsMakeSure=@IsMakeSure,PurchaseRemark=@PurchaseRemark Where OrderID=@OrderID and OrderNo=@OrderNo";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@PurchaseRemark", DbType.String, entity.PurchaseRemark);
                    conn.AddInParameter(cmd, "@IsMakeSure", DbType.Int32, entity.IsMakeSure);
                    conn.ExecuteNonQuery(cmd);
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        #endregion
        #region 补货单方法集合
        public string GetNewRplNo()
        {
            string strSql = @"
DECLARE @prefix NVARCHAR(20);
DECLARE @maxNo NVARCHAR(50);
DECLARE @newNo NVARCHAR(50);
DECLARE @seq INT;

-- 前缀：RO + yyMMdd
SET @prefix = 'RO' + RIGHT(CONVERT(CHAR(6), GETDATE(), 12), 6);  

-- 找当日最大流水
SELECT @maxNo = MAX(RplNo)
FROM Tbl_Cargo_RplOrder
WHERE RplNo LIKE @prefix + '%';

-- 截取最后四位并+1
IF @maxNo IS NULL
    SET @seq = 1;
ELSE
    SET @seq = CAST(RIGHT(@maxNo, 4) AS INT) + 1;

-- 生成新的单号
SET @newNo = @prefix + RIGHT('0000' + CAST(@seq AS VARCHAR(4)), 4);

SELECT @newNo AS NewRplNo;
";
            using (DbCommand command = conn.GetSqlStringCommond(strSql))
            {
                string newRplNo = conn.ExecuteScalar(command)?.ToString();
                if (string.IsNullOrWhiteSpace(newRplNo))
                {
                    throw new ApplicationException("获取最新补货单号失败");
                }
                return newRplNo;
            }
        }

        //更新缺货清单
        public List<CargoOOSLogDtlDto> UpdtOutOfStock(UpdateOOSParam entity)
        {
            SqlHelper conn = new SqlHelper();
            var goodslist = entity.Rows;
            List<CargoOOSLogDtlDto> result = new List<CargoOOSLogDtlDto>();
            if (!goodslist.Any())
            {
                return result;
            }
            string createPrdctTempSql = @"
CREATE TABLE 
	##productTemp(
		ProductID BIGINT,
		AreaID INT
	);
";
            string insertPrdctTempSql = @"
INSERT ##productTemp(ProductID, AreaID)
VALUES @{productValues};
";
            string queryOutofStockSql = @"
PRINT('------------ 区域仓库 ------------');
WITH ChildAreaCTE AS (
	SELECT
		HouseID,
		AreaID AS RootArea,
		ParentID AS ParentArea,
		AreaID AS AreaID,
		Name AS RootName,
		CAST('' AS varchar(50)) AS ParentName,
		Name AS AreaName,
		1 AS Level
	FROM
		Tbl_Cargo_Area a
	WHERE (1=1)
		AND ParentID = 0
	UNION ALL
	
	SELECT
		c.HouseID,
		c.RootArea,
		a.ParentID AS ParentArea,
		a.AreaID,
		c.RootName,
		c.AreaName,
		a.Name AS AreaName,
		c.Level + 1
	FROM
		Tbl_Cargo_Area a
		INNER JOIN ChildAreaCTE c ON a.ParentID = c.AreaID
    WHERE (1=1)
)
SELECT
	* INTO #childArea
FROM
	ChildAreaCTE 
OPTION (MAXRECURSION 2); --只查到2级子仓库，如有3级子仓库就报错，防止无限递归。业务逻辑也只允许最大2级子仓（注：根仓库是0级）
CREATE UNIQUE INDEX IX_#childArea
ON #childArea (HouseID,AreaID)
INCLUDE(RootArea);

-- 产品
WITH 
pCTE as (
SELECT 
	DISTINCT
	p.ProductID,
	p.ProductName,
	p.ProductCode,
	p.GoodsCode,
	p.Specs,
	p.Figure,
	P.LoadIndex,
	p.SpeedLevel,
	pt.TypeID,
	pt.TypeName,
	pt.ParentID,
	pt2.TypeName ParentName,
    h.HouseID,
	h.Name HouseName,
	h.ParentID HouseParentID,
	h.ParentName HouseParentName,
	cg.Piece InPiece,
	ca.RootArea AreaID
FROM
	Tbl_Cargo_Product p
	INNER JOIN ##productTemp pTemp ON p.ProductID = pTemp.ProductID
	INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
	INNER JOIN Tbl_Cargo_ContainerGoods cg ON p.ProductID = cg.ProductID
	INNER JOIN #childArea ca ON ca.AreaID = pTemp.AreaID
	LEFT JOIN Tbl_Cargo_ProductType pt2 ON pt.ParentID = pt2.TypeID
	LEFT JOIN Tbl_Cargo_House h ON p.HouseID = h.HouseID
WHERE
	ISNULL(p.ProductCode, '') <> ''
)
,
--在途
iti AS (
	SELECT
		fo.ProductCode,
		fo.HouseID,
		SUM(ReplyNumber - fo.InPiece) Piece
	FROM
		Tbl_Cargo_FactoryOrder fo
	WHERE
		(1 = 1)
		AND fo.InCargoStatus IN (0,2)
	GROUP BY
		fo.ProductCode,
		fo.HouseID
)
--select * from iti
,

--在补货
ro as (
SELECT
	rog.ProductCode, 
	ro.FromHouse HouseID,
	SUM(rog.Piece - rog.DonePiece) Piece
FROM
	Tbl_Cargo_RplOrderGoods rog 
	INNER JOIN Tbl_Cargo_RplOrder ro ON rog.RplID = ro.RplID
WHERE
	ro.Status NOT IN (2,3)
GROUP BY 
	rog.ProductCode, ro.FromHouse
)

--安全库存配置
SELECT
	ss.SID,
	ss.MinStock,
	ss.MaxStock,
	ISNULL(mss.WeightedMonthSale, 0) WeightedMonthSale,
	ISNULL(iti.Piece, 0) InTransitPiece,
	ISNULL(ro.Piece, 0) RestockingPiece,
	(ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) + ISNULL(ro.Piece, 0)) RealPiece,
	ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) + ISNULL(ro.Piece, 0)) Piece,
	p.*
FROM
	Tbl_Cargo_SafeStock ss
	INNER JOIN pCTE p ON p.ProductCode = ss.ProductCode AND p.AreaID = ss.AreaID
	LEFT JOIN iti ON iti.ProductCode = p.ProductCode AND iti.HouseID = p.HouseID
	LEFT JOIN Tbl_Cargo_MonthSaleStatic mss ON mss.ProductCode = p.ProductCode AND mss.AreaID = p.AreaID AND mss.YearMonth = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) -1 ,0)
	LEFT JOIN ro ON ro.ProductCode = p.ProductCode AND ro.HouseID = p.HouseID
	LEFT JOIN Tbl_Cargo_OutOfStock oos ON oos.ProductCode = ss.ProductCode AND oos.HouseID = ss.HouseID
WHERE 
	ss.MinStock > 0 AND MaxStock > 0
	AND (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) - ISNULL(ro.Piece, 0)) < ss.MinStock 
	AND ISNULL(oos.Piece, 0) <> ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) + ISNULL(ro.Piece, 0))
ORDER BY ss.MaxStock - (ISNULL(p.InPiece, 0) + ISNULL(iti.Piece, 0) + ISNULL(ro.Piece, 0)) DESC

";
            using (Trans trans = new Trans())
            {
                try
                {
                    int index = 1;
                    List<string> productValues = new List<string>();
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    foreach (var goods in goodslist)
                    {
                        productValues.Add($"(@ProductID{index}, @AreaID{index})");
                        sqlParameters.AddRange(new List<SqlParameter>
                        {
                            new SqlParameter($"@ProductID{index}", goods.ProductID),
                            new SqlParameter($"@AreaID{index}", goods.AreaID),

                        });
                        index++;
                    }
                    if (productValues.Any())
                    {
                        insertPrdctTempSql = insertPrdctTempSql.Replace("@{productValues}", string.Join("," + Environment.NewLine, productValues));
                    }
                    else
                    {
                        insertPrdctTempSql = insertPrdctTempSql.Replace("@{productValues}", "");
                    }

                    List<CargoOOSLogGoodsDto> toAddRows = new List<CargoOOSLogGoodsDto>();
                    List<CargoOOSLogDtlDto> toAddEntities = new List<CargoOOSLogDtlDto>();
                    //插入临时表数据
                    using (DbCommand command = conn.GetSqlStringCommond(createPrdctTempSql + insertPrdctTempSql))
                    {
                        command.Parameters.AddRange(sqlParameters.ToArray());
                        conn.ExecuteNonQuery(command, trans);

                        command.CommandText = queryOutofStockSql;
                        //获取库存低于安全库存的产品
                        using (var dt = conn.ExecuteDataTable(command, trans))
                        {
                            if (dt.Rows.Count == 0) return result;
                            foreach (DataRow dr in dt.Rows)
                            {
                                toAddRows.Add(new CargoOOSLogGoodsDto()
                                {
                                    ProductID = dr.Field<long>("ProductID"),
                                    ProductName = dr.Field<string>("ProductName"),
                                    ProductCode = dr.Field<string>("ProductCode"),
                                    GoodsCode = dr.Field<string>("GoodsCode"),
                                    TypeCate = dr.Field<int>("ParentID"),
                                    TypeCateName = dr.Field<string>("ParentName"),
                                    TypeID = dr.Field<int>("TypeID"),
                                    TypeName = dr.Field<string>("TypeName"),
                                    Specs = dr.Field<string>("Specs"),
                                    Figure = dr.Field<string>("Figure"),
                                    LoadIndex = dr.Field<string>("LoadIndex"),
                                    SpeedLevel = dr.Field<string>("SpeedLevel"),
                                    Piece = dr.Field<int>("Piece"),
                                    SysPiece = dr.Field<int>("Piece"),
                                    HouseID = dr.Field<int>("HouseID"),
                                    AreaID = dr.Field<int>("AreaID"),
                                    HouseName = dr.Field<string>("HouseName"),
                                    ParentHouseID = dr.Field<int?>("HouseParentID"),
                                    ParentHouseName = dr.Field<string>("HouseParentName"),

                                    MinStock = dr.Field<int>("MinStock"),
                                    MaxStock = dr.Field<int>("MaxStock"),
                                    SrcPiece = dr.Field<int>("InPiece"),
                                    InTransitQty = dr.Field<int>("InTransitPiece"),
                                    RestockingQty = dr.Field<int>("RestockingPiece"),
                                    AvgSalSUM = dr.Field<int>("WeightedMonthSale"),
                                    SID = dr.Field<long>("SID")
                                });
                            }
                            toAddRows.Where(c => c.Piece < 0).ToList().ForEach(c => c.Piece = 0);
                        }
                        //按照大仓分组
                        foreach (var item in toAddRows.GroupBy(x => x.HouseID))
                        {
                            int HouseID = item.Key.Value;
                            var firstData = item.FirstOrDefault();
                            int totalReplPiece = item.Select(x => x.Piece).OfType<int>().Sum();
                            CargoOOSLogDtlDto toAddEntity = new CargoOOSLogDtlDto()
                            {
                                HouseID = HouseID,
                                HouseName = firstData.HouseName,
                                FromHouse = firstData.ParentHouseID,
                                FromHouseName = firstData.ParentHouseName,
                                ReqBy = entity.UserID,
                                ReqByName = entity.UserName,
                                SrcType = entity.SrcType,
                                ReasonTag = entity.ReasonTag,
                                SrcCode = entity.SrcCode,
                                SrcID = entity.SrcID,
                                Piece = totalReplPiece,
                                Reason = "",
                                Remark = "",
                                Rows = item.ToList()
                            };
                            toAddEntities.Add(toAddEntity);
                        }

                    }
                    trans.Commit();

                    //将分组后的数据创建补货单
                    foreach (var toAddEntity in toAddEntities)
                    {
                        var resultAddedData = UpdateOutOfStockWithLog(toAddEntity);
                        result.Add(resultAddedData);
                    }
                }
                catch (Exception)
                {
                    trans.RollBack();
                    throw;
                }
            }

            return result;
        }


        public CargoRplOrderDto QueryRplOrderFirst(CargoRplOrderParams queryParams)
        {
            var listData = QueryRplOrder(queryParams)?.Data ?? new List<CargoRplOrderDto>();
            return listData.FirstOrDefault();
        }
        public CargoRplOrderListDto QueryRplOrder(CargoRplOrderParams queryParams)
        {
            CargoRplOrderListDto result = new CargoRplOrderListDto();
            try
            {
                #region 组装查询SQL语句
                StringBuilder strbld = new StringBuilder();
                List<string> conditions = new List<string>();
                List<SqlParameter> parameters = new List<SqlParameter>();
                strbld.AppendLine("------------------查询补货单列表-----------------");

                //补货单详情表
                strbld.AppendLine(@"
WITH rog AS (
SELECT
	RplID
FROM Tbl_Cargo_RplOrderGoods
WHERE (1=1) @{conditions}
GROUP BY RplID
)
"); 
                if (queryParams.TypeID.HasValue)
                {
                    conditions.Add("TypeID = @TypeID");
                    parameters.Add(new SqlParameter("@TypeID", SqlDbType.Int) { Value = queryParams.TypeID.Value });
                }
                if (queryParams.TypeCate.HasValue)
                {
                    conditions.Add("TypeCate = @TypeCate");
                    parameters.Add(new SqlParameter("@TypeCate", SqlDbType.Int) { Value = queryParams.TypeCate.Value });
                }
                if (conditions.Count > 0)
                {
                    strbld = strbld.Replace("@{conditions}", "AND " + string.Join(" AND ", conditions));
                }
                else
                {
                    strbld = strbld.Replace("@{conditions}", "");
                }

                strbld.AppendLine(@"
SELECT 
	@{pagesize}
	* ,
    COUNT(1) OVER() AS TotalCount
FROM (
    SELECT
	    DISTINCT ROW_NUMBER() OVER (ORDER BY ro.RplID DESC) AS RowNumber,
	    ro.*,
	    ro2.TypeNames
    FROM
	    Tbl_Cargo_RplOrder AS ro
	    LEFT JOIN (
		    SELECT
			    ro.RplID,
			    STUFF(
				    (
					    SELECT
						    DISTINCT ',' + rog.TypeName
					    FROM
						    Tbl_Cargo_RplOrderGoods rog
					    WHERE
						    ro.RplID = rog.RplID FOR XML PATH(''),
						    TYPE
				    ).value('.', 'NVARCHAR(MAX)'),
				    1,
				    1,
				    ''
			    ) AS TypeNames
		    FROM
			    Tbl_Cargo_RplOrder ro
	    ) ro2 ON ro2.RplID = ro.RplID
		INNER JOIN rog ON rog.RplID = ro.RplID
    WHERE 1 = 1 AND rog.RplID IS NOT NULL AND ro.Status <> 3 @{conditions}
) a
WHERE RowNumber > @{startindex}
ORDER BY RowNumber ASC
");
                conditions = new List<string>();
                strbld.Replace("@{pagesize}", queryParams.PageSize <= 0 ? "" : "TOP " + queryParams.PageSize.ToString());
                strbld.Replace("@{startindex}", queryParams.StartIndex.ToString());

                if (queryParams.RplIDs != null && queryParams.RplIDs.Any())
                {
                    conditions.Add($"ro.RplID IN ({string.Join(",", queryParams.RplIDs)})");
                }
                else if (queryParams.RplID.HasValue)
                {
                    conditions.Add("ro.RplID = @RplID");
                    parameters.Add(new SqlParameter("@RplID", SqlDbType.Int) { Value = queryParams.RplID });
                }
                if (!string.IsNullOrEmpty(queryParams.RplNo))
                {
                    conditions.Add("ro.RplNO LIKE @RplNO");
                    parameters.Add(new SqlParameter("@RplNO", SqlDbType.NVarChar, 50) { Value = '%' + queryParams.RplNo + '%' });
                }
                if (queryParams.Status.HasValue)
                {
                    conditions.Add("ro.Status = @Status");
                    parameters.Add(new SqlParameter("@Status", SqlDbType.TinyInt) { Value = queryParams.Status.Value });
                }
                if (queryParams.HouseID.HasValue)
                {
                    conditions.Add("ro.HouseID = @HouseID");
                    parameters.Add(new SqlParameter("@HouseID", SqlDbType.Int) { Value = queryParams.HouseID.Value });
                }
                if (queryParams.FromHouse.HasValue)
                {
                    conditions.Add("ro.FromHouse = @FromHouse");
                    parameters.Add(new SqlParameter("@FromHouse", SqlDbType.Int) { Value = queryParams.FromHouse.Value });
                }
                if (queryParams.UserName != null)
                {
                    conditions.Add("ro.ReqByName LIKE N'%'+@ReqByName+'%'");
                    parameters.Add(new SqlParameter("@ReqByName", SqlDbType.NVarChar, 50) { Value = queryParams.UserName });
                }
                if (queryParams.StartDate.HasValue)
                {
                    conditions.Add("ro.CreateDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = queryParams.StartDate.Value.Date });
                }
                if (queryParams.EndDate.HasValue)
                {
                    conditions.Add("ro.CreateDate < @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = queryParams.EndDate.Value.Date.AddDays(1) });
                }

                if (conditions.Count > 0)
                {
                    strbld = strbld.Replace("@{conditions}", "AND " + string.Join(" AND ", conditions));
                }
                else
                {
                    strbld = strbld.Replace("@{conditions}", "");
                }

                #endregion
                string strSQL = strbld.ToString();
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    string paramsStr = string.Join(", ", command.Parameters.Cast<SqlParameter>().Select(p => $"{p.ParameterName}={p.Value}"));
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        List<CargoRplOrderDto> sqlData = new List<CargoRplOrderDto>();
                        bool isFirst = true;
                        foreach (DataRow idr in dt.Rows)
                        {
                            if (isFirst)
                            {
                                result.DataTotal = idr.Field<int>("TotalCount");
                                isFirst = false;
                            }
                            result.DataTotal = idr.Field<int>("TotalCount");
                            sqlData.Add(new CargoRplOrderDto
                            {
                                RplID = idr.Field<int>("RplID"),
                                RplNo = idr.Field<string>("RplNo"),
                                FromHouse = idr.Field<int?>("FromHouse"),
                                HouseID = idr.Field<int?>("HouseID"),
                                HouseName = idr.Field<string>("HouseName"),
                                FromHouseName = idr.Field<string>("FromHouseName"),
                                UserID = idr.Field<string>("UserID"),
                                UserName = idr.Field<string>("UserName"),
                                Piece = idr.Field<int?>("Piece"),
                                DonePiece = idr.Field<int?>("DonePiece"),
                                Status = idr.Field<byte?>("Status"),
                                TypeNames = idr.Field<string>("TypeNames"),
                                Remark = idr.Field<string>("Remark"),
                                CreateDate = idr.Field<DateTime>("CreateDate"),
                                CompletedDate = idr.Field<DateTime?>("CompletedDate")
                            });
                        }
                        result.Data = sqlData;
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public CargoRplOrderGoodsListDto QueryRplOrderGoods(CargoRplOrderGoodsListDto entity)
        {
            CargoRplOrderGoodsListDto result = new CargoRplOrderGoodsListDto();
            var queryentity = entity.QueryEntity;
            try
            {
                //entity.EnSafe();
                #region 组装查询SQL语句
                StringBuilder strbld = new StringBuilder();
                List<string> conditions = new List<string>();
                List<SqlParameter> parameters = new List<SqlParameter>();
                strbld.AppendLine("------------------查询补货单详情列表-----------------");
                strbld.AppendLine(@"
SELECT @{pagesize}
	* ,
    COUNT(1) OVER() AS TotalCount
FROM (
    SELECT
	    DISTINCT ROW_NUMBER() OVER (ORDER BY rog.RplID DESC,rog.ID ASC) AS RowNumber,
	    rog.*,
		p.Specs,
		p.Figure,
		p.LoadIndex,
		p.SpeedLevel
    FROM
	    Tbl_Cargo_RplOrderGoods AS rog
		INNER JOIN Tbl_Cargo_Product p ON rog.ProductID = p.ProductID
    WHERE 1 = 1 @{conditions}
) a
WHERE 1 = 1 @{startindex}
ORDER BY RowNumber ASC
");
                conditions = new List<string>();

                if(entity.IsPaging)
                {
                    strbld.Replace("@{pagesize}", "TOP " + entity.PageSize.ToString());
                    strbld.Replace("@{startindex}", "RowNumber > " + entity.PageSize.ToString());
                }
                else
                {
                    strbld.Replace("@{pagesize}", "");
                    strbld.Replace("@{startindex}", "");
                }


                if (queryentity.RplID.HasValue)
                {
                    conditions.Add("rog.RplID = @RplID");
                    parameters.Add(new SqlParameter("@RplID", SqlDbType.Int) { Value = queryentity.RplID.Value });
                }

                if (conditions.Count > 0)
                {
                    strbld.Replace("@{conditions}", "AND " + string.Join(" AND ", conditions));
                }
                else
                {
                    strbld.Replace("@{conditions}", "");
                }

                #endregion
                string strSQL = strbld.ToString();
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    string paramsStr = string.Join(", ", command.Parameters.Cast<SqlParameter>().Select(p => $"{p.ParameterName}={p.Value}"));
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        List<CargoRplOrderGoodsDto> sqlData = new List<CargoRplOrderGoodsDto>();
                        bool isFirst = true;
                        foreach (DataRow idr in dt.Rows)
                        {
                            if (isFirst)
                            {   
                                result.DataTotal = idr.Field<int>("TotalCount");
                                isFirst = false;
                            }
                            result.DataTotal = idr.Field<int>("TotalCount");
                            sqlData.Add(new CargoRplOrderGoodsDto
                            {
                                ID = idr.Field<int>("ID"),
                                RplID = idr.Field<int>("RplID"),
                                ProductID = idr.Field<long>("ProductID"),
                                ProductName = idr.Field<string>("ProductName"),
                                ProductCode = idr.Field<string>("ProductCode"),
                                GoodsCode = idr.Field<string>("GoodsCode"),
                                HouseID = idr.Field<int?>("HouseID"),
                                HouseName = idr.Field<string>("HouseName"),
                                AreaID = idr.Field<int?>("AreaID"),
                                AreaName = idr.Field<string>("AreaName"),
                                TypeCate = idr.Field<int?>("TypeCate"),
                                TypeCateName = idr.Field<string>("TypeCateName"),
                                TypeID = idr.Field<int?>("TypeID"),
                                TypeName = idr.Field<string>("TypeName"),
                                Piece = idr.Field<int?>("Piece"),
                                DonePiece = idr.Field<int?>("DonePiece"),
                                Specs = idr.Field<string>("Specs"),
                                Figure = idr.Field<string>("Figure"),
                                LoadIndex = idr.Field<string>("LoadIndex"),
                                SpeedLevel = idr.Field<string>("SpeedLevel")
                            });
                        }
                        result.Data = sqlData;
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public List<CargoRplOrderExcelModel> GetRplOrderExcel(CargoRplOrderParams queryParams)
        {
            List<CargoRplOrderExcelModel> result = new List<CargoRplOrderExcelModel>();
            try
            {
                #region 组装查询SQL语句
                StringBuilder strbld = new StringBuilder();
                List<string> conditions = new List<string>();
                List<SqlParameter> parameters = new List<SqlParameter>();
                var roData = QueryRplOrder(queryParams);
                var rplIDs = roData.Data.Select(c => c.RplID).OfType<int>().ToList();
                string createRplIDsTempTblSql = @"
CREATE TABLE 
	##rplIDsTemp(
		RplID INT
	);


INSERT INTO ##rplIDsTemp (RplID)
Values @{tempTblVals}
";
                string queryRODataStr = @"
SELECT 
       ro.[RplID]
      ,ro.[RplNo]
      ,ro.[HouseID]
      ,ro.[HouseName]
      ,ro.[FromHouse]
      ,ro.[FromHouseName]
      ,ro.[UserID]
      ,ro.[UserName]
      ,ro.[Piece] TotalPiece
      ,ro.[DonePiece] TotalDonePiece
      ,ro.[Status]
      ,ro.[ProcessingDate]
      ,ro.[CompletedDate]
      ,ro.[CancelledDate]
      ,ro.[Remark]

      ,rog.[ID]
      ,rog.[ProductID]
      ,rog.[ProductName]
      ,rog.[ProductCode]
      ,rog.[GoodsCode]
      ,rog.[AreaID]
      ,rog.[AreaName]
      ,rog.[TypeCate]
      ,rog.[TypeCateName]
      ,rog.[TypeID]
      ,rog.[TypeName]
      ,rog.[Piece]
      ,rog.[SysPiece]
      ,rog.[DonePiece]
      ,rog.[CreateDate]
      ,rog.[UpdateDate]
      ,p.Specs
      ,p.Figure
      ,p.LoadIndex
      ,p.SpeedLevel
FROM Tbl_Cargo_RplOrder ro
LEFT JOIN Tbl_Cargo_RplOrderGoods rog ON ro.RplID = rog.RplID
INNER JOIN ##rplIDsTemp rit ON rit.RplID = ro.RplID
INNER JOIN Tbl_Cargo_Product p ON rog.ProductID = p.ProductID
";

                int index = 1;
                List<string> tempTblVals = new List<string>();
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                foreach (var rplID in rplIDs)
                {
                    tempTblVals.Add($"(@RplID{index})");
                    sqlParameters.AddRange(new List<SqlParameter>
                        {
                            new SqlParameter($"@RplID{index}", rplID)
                        });
                    index++;
                }

                if (rplIDs.Any())
                {
                    createRplIDsTempTblSql = createRplIDsTempTblSql.Replace("@{tempTblVals}", string.Join("," + Environment.NewLine, tempTblVals));
                }
                else
                {
                    return result;
                }
                #endregion
                string strSQL = createRplIDsTempTblSql + queryRODataStr;
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    command.Parameters.AddRange(sqlParameters.ToArray());
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        List<CargoRplOrderExcelModel> sqlData = new List<CargoRplOrderExcelModel>();
                        foreach (DataRow idr in dt.Rows)
                        {
                            sqlData.Add(new CargoRplOrderExcelModel
                            {
                                RplID = idr.Field<int>("RplID"),
                                RplNo = idr.Field<string>("RplNo"),
                                FromHouse = idr.Field<int?>("FromHouse"),
                                HouseID = idr.Field<int?>("HouseID"),
                                HouseName = idr.Field<string>("HouseName"),
                                FromHouseName = idr.Field<string>("FromHouseName"),
                                UserID = idr.Field<string>("UserID"),
                                UserName = idr.Field<string>("UserName"),
                                TotalPiece = idr.Field<int>("TotalPiece"),
                                TotalDonePiece = idr.Field<int?>("TotalDonePiece"),
                                Status = idr.Field<byte?>("Status"),
                                Remark = idr.Field<string>("Remark"),
                                CreateDate = idr.Field<DateTime>("CreateDate"),
                                UpdateDate = idr.Field<DateTime>("UpdateDate"),


                                ID = idr.Field<int>("ID"),
                                ProductID = idr.Field<long>("ProductID"),
                                ProductName = idr.Field<string>("ProductName"),
                                ProductCode = idr.Field<string>("ProductCode"),
                                GoodsCode = idr.Field<string>("GoodsCode"),
                                AreaID = idr.Field<int?>("AreaID"),
                                AreaName = idr.Field<string>("AreaName"),
                                TypeCate = idr.Field<int?>("TypeCate"),
                                TypeCateName = idr.Field<string>("TypeCateName"),
                                TypeID = idr.Field<int?>("TypeID"),
                                TypeName = idr.Field<string>("TypeName"),
                                Piece = idr.Field<int>("Piece"),
                                DonePiece = idr.Field<int?>("DonePiece"),
                                Specs = idr.Field<string>("Specs"),
                                Figure = idr.Field<string>("Figure"),
                                LoadIndex = idr.Field<string>("LoadIndex"),
                                SpeedLevel = idr.Field<string>("SpeedLevel")
                            });
                        }
                        result = sqlData;
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            
            return result;
        }
        public CargoRplOrderDtlDto AddRplOrder(CargoRplOrderDtlDto entity)
        {
            SqlHelper conn = new SqlHelper();
            CargoRplOrderDtlDto result = new CargoRplOrderDtlDto();
            CargoRplOrderDtlDto headData = entity;
;
            CargoRplOrderDtlDto rtData = new CargoRplOrderDtlDto();
            using (Trans trns = new Trans())
            {
                try
                {
                    int? FromHouseID = null;
                    List<CargoRplOrderGoodsDto> passRowsData = entity.Rows;
                    List<CargoRplOrderGoodsDto> rowsData = new List<CargoRplOrderGoodsDto>();
                    var oosIDs = passRowsData.Select(x => x.OOSID).ToList();
                    //获取原始缺货数据
                    string queryOOS = @"
SELECT * FROM Tbl_Cargo_OutOfStock
WHERE Piece > 0 AND OOSID IN (@{OOSIDs})
";
                    queryOOS = queryOOS.Replace("@{OOSIDs}", string.Join(", ", oosIDs));
                    using (DbCommand command = conn.GetSqlStringCommond(queryOOS))
                    {
                        using (DataTable dt = conn.ExecuteDataTable(command, trns))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                bool isFirst = true;
                                foreach (DataRow dr in dt.Rows)
                                {
                                    int oosID = dr.Field<int>("OOSID");
                                    var matchedPassRow = passRowsData.FirstOrDefault(c => c.OOSID == oosID);
                                    if (isFirst)
                                    {
                                        isFirst = false;
                                        FromHouseID = dr.Field<int?>("HouseID");
                                    }
                                    rowsData.Add(new CargoRplOrderGoodsDto()
                                    {
                                        ProductID = dr.Field<long>("ProductID"),
                                        ProductCode = dr.Field<string>("ProductCode"),
                                        ProductName = dr.Field<string>("ProductName"),
                                        GoodsCode = dr.Field<string>("GoodsCode"),
                                        TypeCate = dr.Field<int>("TypeCate"),
                                        TypeCateName = dr.Field<string>("TypeCateName"),
                                        TypeID = dr.Field<int>("TypeID"),
                                        TypeName = dr.Field<string>("TypeName"),
                                        HouseID = dr.Field<int>("HouseID"),
                                        HouseName = dr.Field<string>("HouseName"),
                                        AreaID = dr.Field<int>("AreaID"),
                                        AreaName = dr.Field<string>("AreaName"),
                                        Piece = matchedPassRow.Piece, //手动补货数量
                                        SysPiece = dr.Field<int?>("Piece") //系统建议补货数
                                    });
                                }
                            }
                            else
                            {
                                throw new ApplicationException("查询缺货单原数据失败");
                            }
                        }
                    }

                    CargoRplOrderGoodsDto firstRow = rowsData.FirstOrDefault();
                    int? HouseID = headData.HouseID;
                    int? FromHouse = FromHouseID;
                    string ReqBy = headData.UserID;
                    string RplNo = "";
                    string HouseName = "";
                    string FromHouseName = "";
                    string UserName = "";
                    CargoHouseManager houseMan = new CargoHouseManager();
                    //验证目标仓库是否有效
                    if (!HouseID.HasValue) throw new ApplicationException("请传入目标仓库ID");
                    var houseData = houseMan.QueryCargoHouseByID(HouseID.Value);
                    bool isExistHouse = (houseData?.HouseID ?? 0) > 0;
                    if (!isExistHouse) throw new ApplicationException($"目标仓库ID({HouseID.Value})不存在");
                    HouseName = houseData.Name;
                    //验证来源仓库是否有效
                    if (!FromHouse.HasValue) throw new ApplicationException("请传入来源仓库ID");
                    var fromhouseData = houseMan.QueryCargoHouseByID(FromHouse.Value);
                    isExistHouse = (fromhouseData?.HouseID ?? 0) > 0;
                    if (!isExistHouse) throw new ApplicationException($"来源仓库ID({FromHouse.Value})不存在");
                    FromHouseName = fromhouseData.Name;
                    //验证请求人ID是否有效
                    if (string.IsNullOrWhiteSpace(ReqBy))
                    {
                        throw new ApplicationException("请传入请求人ID");
                    }
                    SystemManager sysMan = new SystemManager();
                    var userData = sysMan.ReturnUserName(ReqBy);
                    bool isExistUser = (userData?.LoginName ?? "") != "";
                    if (!isExistHouse) throw new ApplicationException($"请求人ID({ReqBy})不存在");
                    UserName = userData.UserName;
                    //获取新补货单号
                    RplNo = GetNewRplNo();

                    //------------ 插入头数据 ------------
                    string insertHeadSQL = @"
INSERT INTO
	Tbl_Cargo_RplOrder (
		RplNo,
		HouseID,
		HouseName,
		FromHouse,
		FromHouseName,
		UserID,
		UserName,
		Piece,
		DonePiece,
		Status,
		ProcessingDate,
		CompletedDate,
		CancelledDate,
		Remark,
		CreateDate,
		UpdateDate
	)
OUTPUT 
    INSERTED.*
VALUES
	(
		@RplNo,
		@HouseID,
		@HouseName,
		@FromHouse,
		@FromHouseName,
		@UserID,
		@UserName,
		@Piece,
		@DonePiece,
		@Status,
		@ProcessingDate,
		@CompletedDate,
		@CancelledDate,
		@Remark,
		@CreateDate,
		@UpdateDate
	)
SELECT
	SCOPE_IDENTITY()";
                    using (DbCommand command = conn.GetSqlStringCommond(insertHeadSQL))
                    {
                        List<SqlParameter> hdsqlParameters = new List<SqlParameter>()
                        {
                            //计算值
                            new SqlParameter("@RplNo", SqlDbType.NVarChar, 50) { Value = RplNo },
                            new SqlParameter("@HouseID", SqlDbType.Int) { Value = HouseID },
                            new SqlParameter("@HouseName", SqlDbType.NVarChar, 50) { Value = HouseName},
                            new SqlParameter("@FromHouse", SqlDbType.Int) { Value = FromHouse },
                            new SqlParameter("@FromHouseName", SqlDbType.NVarChar, 50) { Value = FromHouseName },
                            new SqlParameter("@UserID", SqlDbType.VarChar, 10) { Value = headData.UserID },
                            new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = UserName },

                            //传值
                            new SqlParameter("@Piece", SqlDbType.Int) { Value = headData.Piece },
                            new SqlParameter("@Remark", SqlDbType.NVarChar, 500) { Value = (object)headData.Remark ?? DBNull.Value },

                            //默认值
                            new SqlParameter("@DonePiece", SqlDbType.Int) { Value = 0 },
                            new SqlParameter("@Status", SqlDbType.TinyInt) { Value = 0 },
                            new SqlParameter("@ProcessingDate", SqlDbType.DateTime) { Value = DBNull.Value },
                            new SqlParameter("@CompletedDate", SqlDbType.DateTime) { Value = DBNull.Value },
                            new SqlParameter("@CancelledDate", SqlDbType.DateTime) { Value = DBNull.Value },
                            new SqlParameter("@CreateDate", SqlDbType.DateTime) { Value = DateTime.Now },
                            new SqlParameter("@UpdateDate", SqlDbType.DateTime) { Value = DateTime.Now }
                        };
                        command.Parameters.AddRange(hdsqlParameters.ToArray());

                        using (DataTable dt = conn.ExecuteDataTable(command, trns))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                rtData = new CargoRplOrderDtlDto()
                                {
                                    RplID = dr.Field<int>("RplID"),
                                    RplNo = dr.Field<string>("RplNo"),
                                    HouseID = dr.Field<int>("HouseID"),
                                    HouseName = dr.Field<string>("HouseName"),
                                    FromHouse = dr.Field<int>("FromHouse"),
                                    FromHouseName = dr.Field<string>("FromHouseName"),
                                    UserID = dr.Field<string>("UserID"),
                                    UserName = dr.Field<string>("UserName"),
                                    Piece = dr.Field<int>("Piece"),
                                    DonePiece = dr.Field<int?>("DonePiece"),
                                    Status = dr.Field<byte?>("Status"),
                                    Remark = dr.Field<string>("Remark"),
                                    ProcessingDate = dr.Field<DateTime?>("ProcessingDate"),
                                    CompletedDate = dr.Field<DateTime?>("CompletedDate"),
                                    CancelledDate = dr.Field<DateTime?>("CancelledDate"),
                                    CreateDate = dr.Field<DateTime>("CreateDate"),
                                    UpdateDate = dr.Field<DateTime>("UpdateDate")
                                };
                            }
                            else
                            {
                                throw new ApplicationException("补货单头表数据保存失败");
                            }
                        }
                    }


                    //------------ 插入行数据 ------------
                    string insertRowsSQL = @"
INSERT INTO
	Tbl_Cargo_RplOrderGoods (
		RplID,
		ProductID,
		ProductName,
		ProductCode,
		GoodsCode,
        HouseID,
        HouseName,
        AreaID,
        AreaName,
		TypeCate,
        TypeCateName,
		TypeID,
		TypeName,
		Piece,
		SysPiece,
		DonePiece,
		CreateDate,
        UpdateDate
	)
OUTPUT 
    INSERTED.*
VALUES
@{sqlvalues}
";
                    List<string> sqlvaluesStrList = new List<string>();
                    List<SqlParameter> rowssqlParameters = new List<SqlParameter>();
                    int rowIndex = 0;
                    CargoProductManager prdctMan = new CargoProductManager();
                    foreach (var row in rowsData)
                    {
                        long? ProductID = row.ProductID;
                        int? TypeID = row.TypeID;
                        int? TypeCate = row.TypeCate;
                        int? AreaID = row.AreaID;
                        string AreaName = "";
                        int? typeCate = null;


                        //验证产品是否存在
                        if (!ProductID.HasValue) throw new ApplicationException("请传入产品ID");
                        var productData = prdctMan.QueryProductInfoByProductID(ProductID.Value);
                        bool isExistProduct = (productData?.ProductID ?? 0) > 0;
                        if (!isExistProduct) throw new ApplicationException($"产品ID({ProductID.Value})不存在");

                        //验证来源区域仓库是否有效
                        if (!AreaID.HasValue) throw new ApplicationException("请传入来源区域仓库ID");
                        var areaData = houseMan.QueryAreaByAreaID(new CargoAreaEntity() { AreaID = AreaID.Value });
                        bool isExistArea = (areaData?.AreaID ?? 0) > 0;
                        if (!isExistArea) throw new ApplicationException($"来源区域仓库ID({AreaID.Value})不存在");
                        AreaName = areaData.Name;

                        //验证品牌是否存在
                        if (!TypeID.HasValue) throw new ApplicationException("请传入产品ID");
                        if (!TypeCate.HasValue) throw new ApplicationException("请传入产品品类ID");
                        var typeData = prdctMan.QueryProductType(new CargoProductTypeEntity() { TypeID = TypeID.Value, ParentID = TypeCate.Value })?.FirstOrDefault();
                        bool isExistType = (typeData?.TypeID ?? 0) > 0;
                        if (!isExistType) throw new ApplicationException($"产品品牌不存在。相关数据：TypeID({TypeID.Value})，TypeCate({typeCate.Value})");

                        typeCate = typeData.ParentID;

                        string rplIDParam = "@RplID" + rowIndex;
                        string productIDParam = "@ProductID" + rowIndex;
                        string productNameParam = "@ProductName" + rowIndex;
                        string productCodeParam = "@ProductCode" + rowIndex;
                        string goodsCodeParam = "@GoodsCode" + rowIndex;
                        string houseIDParam = "@HouseID" + rowIndex;
                        string houseNameParam = "@HouseName" + rowIndex;
                        string areaIDParam = "@AreaID" + rowIndex;
                        string areaNameParam = "@AreaName" + rowIndex;
                        string typeCateParam = "@TypeCate" + rowIndex;
                        string typeCateNameParam = "@TypeCateName" + rowIndex;
                        string typeIDParam = "@TypeID" + rowIndex;
                        string typeNameParam = "@TypeName" + rowIndex;
                        string sysPieceParam = "@SysPiece" + rowIndex;
                        string pieceParam = "@Piece" + rowIndex;
                        string donePieceParam = "@DonePiece" + rowIndex;
                        string createDateParam = "@CreateDate" + rowIndex;
                        string updateDateParam = "@UpdateDate" + rowIndex;

                        sqlvaluesStrList.Add($"({rplIDParam}, {productIDParam}, {productNameParam}, {productCodeParam}, {goodsCodeParam}, " +
                            $"{houseIDParam}, {houseNameParam}, {areaIDParam}, {areaNameParam}, " +
                            $"{typeCateParam}, {typeCateNameParam}, {typeIDParam}, {typeNameParam}, {pieceParam}," +
                            $"{sysPieceParam}, {donePieceParam}, {createDateParam}, {updateDateParam})");

                        rowssqlParameters.AddRange(new List<SqlParameter>()
                        {
                            //计算
                            new SqlParameter(rplIDParam, SqlDbType.Int) { Value = rtData.RplID },
                            new SqlParameter(productIDParam, SqlDbType.BigInt) { Value = ProductID },
                            new SqlParameter(productNameParam, SqlDbType.NVarChar, 100) { Value = productData.ProductName },
                            new SqlParameter(productCodeParam, SqlDbType.NVarChar, 50) { Value = productData.ProductCode },
                            new SqlParameter(goodsCodeParam, SqlDbType.NVarChar, 50) { Value = productData.GoodsCode },
                            new SqlParameter(areaIDParam, SqlDbType.Int) { Value = AreaID },
                            new SqlParameter(areaNameParam, SqlDbType.NVarChar, 50) { Value = AreaName },
                            new SqlParameter(typeIDParam, SqlDbType.Int) { Value = TypeID },
                            new SqlParameter(typeNameParam, SqlDbType.NVarChar, 50) { Value = typeData.TypeName },
                            new SqlParameter(sysPieceParam, SqlDbType.Int) { Value = row.SysPiece },

                            //传值
                            new SqlParameter(donePieceParam, SqlDbType.Int) { Value = 0 },
                            new SqlParameter(pieceParam, SqlDbType.Int) { Value = row.Piece },
                            new SqlParameter(typeCateParam, SqlDbType.Int) { Value = row.TypeCate },
                            new SqlParameter(typeCateNameParam, SqlDbType.NVarChar, 50) { Value = row.TypeCateName },
                            new SqlParameter(houseIDParam, SqlDbType.Int) { Value = HouseID},
                            new SqlParameter(houseNameParam, SqlDbType.NVarChar, 50) { Value = HouseName},

                            //默认值
                            new SqlParameter(createDateParam, SqlDbType.DateTime) { Value = DateTime.Now },
                            new SqlParameter(updateDateParam, SqlDbType.DateTime) { Value = DateTime.Now }
                        });

                        rowIndex++;
                    }

                    insertRowsSQL = insertRowsSQL.Replace("@{sqlvalues}", string.Join(("," + Environment.NewLine), sqlvaluesStrList));
                    using (DbCommand command = conn.GetSqlStringCommond(insertRowsSQL))
                    {
                        command.Parameters.AddRange(rowssqlParameters.ToArray());
                        using (DataTable dt = conn.ExecuteDataTable(command, trns))
                        {
                            var rtRows = new List<CargoRplOrderGoodsDto>();
                            if (dt.Rows.Count > 0) 
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    rtRows.Add(new CargoRplOrderGoodsDto()
                                    {
                                        ID = dr.Field<int>("ID"),
                                        RplID = dr.Field<int>("RplID"),
                                        ProductID = dr.Field<long>("ProductID"),
                                        ProductCode = dr.Field<string>("ProductCode"),
                                        ProductName = dr.Field<string>("ProductName"),
                                        GoodsCode = dr.Field<string>("GoodsCode"),
                                        TypeCate = dr.Field<int>("TypeCate"),
                                        TypeCateName = dr.Field<string>("TypeCateName"),
                                        TypeID = dr.Field<int>("TypeID"),
                                        TypeName = dr.Field<string>("TypeName"),
                                        HouseID = dr.Field<int>("HouseID"),
                                        HouseName = dr.Field<string>("HouseName"),
                                        AreaID = dr.Field<int>("AreaID"),
                                        AreaName = dr.Field<string>("AreaName"),
                                        Piece = dr.Field<int>("Piece"),
                                        SysPiece = dr.Field<int?>("SysPiece"),
                                        DonePiece = dr.Field<int?>("DonePiece"),
                                        CreateDate = dr.Field<DateTime>("CreateDate"),
                                        UpdateDate = dr.Field<DateTime>("UpdateDate")
                                    });
                                }
                            }
                            rtData.Rows = rtRows;
                        }

                        trns.Commit();
                    }

                    return rtData;
                }
                catch (ApplicationException ex)
                {
                    trns.RollBack();
                    throw new ApplicationException(ex.Message);
                }
            }
        }

        public CargoOOSLogDtlDto UpdateOutOfStockWithLog(CargoOOSLogDtlDto entity)
        {
            SqlHelper conn = new SqlHelper();
            CargoOOSLogDtlDto result = new CargoOOSLogDtlDto();
            CargoOOSLogDtlDto headData = entity;
            List<CargoOOSLogGoodsDto> rowsData = entity.Rows;
            CargoOOSLogDtlDto oosLogData = new CargoOOSLogDtlDto();
            using (Trans trns = new Trans())
            {
                try
                {
                    byte CreateMethod = headData.CreateMethod ?? 0;

                    int? HouseID = headData.HouseID;
                    int? FromHouse = headData.FromHouse;
                    int? ParentRplID = headData.ParentRplID;
                    string ReqBy = headData.ReqBy;
                    string RplNo = "";
                    string HouseName = "";
                    string FromHouseName = "";
                    string ReqByName = "";
                    string AreaName = "";
                    CargoHouseManager houseMan = new CargoHouseManager();
                    //验证目标仓库是否有效
                    if (!HouseID.HasValue) throw new ApplicationException("请传入目标仓库ID");
                    var houseData = houseMan.QueryCargoHouseByID(HouseID.Value);
                    bool isExistHouse = (houseData?.HouseID ?? 0) > 0;
                    if (!isExistHouse) throw new ApplicationException($"目标仓库ID({HouseID.Value})不存在");
                    HouseName = houseData.Name;
                    //验证来源仓库是否有效
                    if (!FromHouse.HasValue) throw new ApplicationException("请传入来源仓库ID");
                    var fromhouseData = houseMan.QueryCargoHouseByID(FromHouse.Value);
                    isExistHouse = (fromhouseData?.HouseID ?? 0) > 0;
                    if (!isExistHouse) throw new ApplicationException($"来源仓库ID({FromHouse.Value})不存在");
                    FromHouseName = fromhouseData.Name;
                    //验证请求人ID是否有效
                    if (string.IsNullOrWhiteSpace(ReqBy))
                    {
                        throw new ApplicationException("请传入请求人ID");
                    }
                    SystemManager sysMan = new SystemManager();
                    var userData = sysMan.ReturnUserName(ReqBy);
                    bool isExistUser = (userData?.LoginName ?? "") != "";
                    if (!isExistHouse) throw new ApplicationException($"请求人ID({ReqBy})不存在");
                    ReqByName = userData.UserName;
                    //获取新补货单号
                    RplNo = GetNewRplNo();

                    //------------ 插入审计头数据 ------------
                    string insertHeadSQL = @"
INSERT INTO
	Tbl_Cargo_RplOrder (
		RplNo,
		HouseID,
		HouseName,
		FromHouse,
		FromHouseName,
		ReqBy,
		ReqByName,
		AppBy,
		AppByName,
		ParentRplID,
		ScrType,
		SrcCode,
		SrcID,
		CreateMethod,
		Piece,
		DonePiece,
		Status,
		Reason,
		ProcessingDate,
		CompletedDate,
		CancelledDate,
		Remark,
		CreateDate,
		UpdateDate
	)
OUTPUT 
    INSERTED.*
VALUES
	(
		@RplNo,
		@HouseID,
		@HouseName,
		@FromHouse,
		@FromHouseName,
		@ReqBy,
		@ReqByName,
		@AppBy,
		@AppByName,
		@ParentRplID,
		@ScrType,
		@SrcCode,
		@SrcID,
		@CreateMethod,
		@Piece,
		@DonePiece,
		@Status,
		@Reason,
		@ProcessingDate,
		@CompletedDate,
		@CancelledDate,
		@Remark,
		@CreateDate,
		@UpdateDate
	)
SELECT
	SCOPE_IDENTITY()";
                    insertHeadSQL = @"
INSERT INTO
	Tbl_Cargo_OutOfStockLog (
		HouseID,
		HouseName,
		FromHouse,
		FromHouseName,
		UserID,
		UserName,
		SrcType,
		SrcCode,
		SrcID,
		Piece,
		ReasonTag,
		Reason,
		CreateDate
	)
OUTPUT 
    INSERTED.*
VALUES
	(
		@HouseID,
		@HouseName,
		@FromHouse,
		@FromHouseName,
		@UserID,
		@UserName,
		@SrcType,
		@SrcCode,
		@SrcID,
		@Piece,
        @ReasonTag,
		@Reason,
		@CreateDate
	)
SELECT
	SCOPE_IDENTITY()
";
                    using (DbCommand command = conn.GetSqlStringCommond(insertHeadSQL))
                    {
                        List<SqlParameter> hdsqlParameters = new List<SqlParameter>()
                        {
                            //计算值
                            new SqlParameter("@HouseID", SqlDbType.Int) { Value = HouseID },
                            new SqlParameter("@HouseName", SqlDbType.NVarChar, 50) { Value = HouseName},
                            new SqlParameter("@FromHouse", SqlDbType.Int) { Value = FromHouse },
                            new SqlParameter("@FromHouseName", SqlDbType.NVarChar, 50) { Value = FromHouseName },
                            new SqlParameter("@UserID", SqlDbType.VarChar, 10) { Value = headData.ReqBy },
                            new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = ReqByName },

                            //传值
                            new SqlParameter("@SrcType", SqlDbType.TinyInt) { Value = (object)headData.SrcType ?? DBNull.Value },
                            new SqlParameter("@ReasonTag", SqlDbType.NVarChar, 10) { Value = (object)headData.ReasonTag ?? DBNull.Value },
                            new SqlParameter("@Reason", SqlDbType.NVarChar, 500) { Value = (object)headData.Reason ?? DBNull.Value },
                            new SqlParameter("@SrcCode", SqlDbType.NVarChar, 50) { Value = (object)headData.SrcCode ?? DBNull.Value },
                            new SqlParameter("@SrcID", SqlDbType.Int) { Value = (object)headData.SrcID ?? DBNull.Value },
                            new SqlParameter("@Piece", SqlDbType.Int) { Value = headData.Piece },
                            new SqlParameter("@CreateDate", SqlDbType.DateTime) { Value = DateTime.Now }
                        };
                        command.Parameters.AddRange(hdsqlParameters.ToArray());

                        using (DataTable dt = conn.ExecuteDataTable(command, trns))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                oosLogData = new CargoOOSLogDtlDto()
                                {
                                    RplID = dr.Field<int>("OOSLogID"),
                                    HouseID = dr.Field<int>("HouseID"),
                                    HouseName = dr.Field<string>("HouseName"),
                                    FromHouse = dr.Field<int>("FromHouse"),
                                    FromHouseName = dr.Field<string>("FromHouseName"),
                                    ReqBy = dr.Field<string>("UserID"),
                                    ReqByName = dr.Field<string>("UserName"),
                                    SrcType = dr.Field<byte?>("SrcType"),
                                    ReasonTag = dr.Field<string>("ReasonTag"),
                                    SrcID = dr.Field<int?>("SrcID"),
                                    Piece = dr.Field<int>("Piece"),
                                    Reason = dr.Field<string>("Reason"),
                                    CreateDate = dr.Field<DateTime>("CreateDate"),
                                };
                            }
                            else
                            {
                                throw new ApplicationException("补货单头表数据保存失败");
                            }
                        }
                    }

                    //------------ 插入审计行数据 ------------
                    string insertRowsSQL = @"
INSERT INTO
	Tbl_Cargo_OutOfStockLogGoods (
		OOSLogID,
		ProductID,
        SID,
		ProductName,
		ProductCode,
		GoodsCode,
        HouseID,
        HouseName,
        AreaID,
        AreaName,
		TypeCate,
        TypeCateName,
		TypeID,
		TypeName,
		Piece,
		MinStock,
		MaxStock,
		SrcPiece,
		RestockingQty,
		InTransitQty,
		AvgSalSUM,
		CreateDate
	)
OUTPUT 
    INSERTED.*
VALUES
@{sqlvalues}
";
                    List<string> sqlvaluesStrList = new List<string>();
                    List<SqlParameter> rowssqlParameters = new List<SqlParameter>();
                    int rowIndex = 0;
                    CargoProductManager prdctMan = new CargoProductManager();
                    foreach (var row in rowsData)
                    {
                        long? ProductID = row.ProductID;
                        int? TypeID = row.TypeID;
                        int? TypeCate = row.TypeCate;
                        int? AreaID = row.AreaID;
                        int? typeCate = null;
                        int sysPiece = 0;
                        AreaName = "";

                        int minstock, maxstock, srcpiece, restockingqty, intransiqty, avgSalsum;
                        minstock = row.MinStock.GetValueOrDefault();
                        maxstock = row.MaxStock.GetValueOrDefault();
                        srcpiece = row.SrcPiece.GetValueOrDefault();
                        restockingqty = row.RestockingQty.GetValueOrDefault();
                        intransiqty = row.InTransitQty.GetValueOrDefault();
                        avgSalsum = row.AvgSalSUM.GetValueOrDefault();

                        sysPiece = Math.Max(0, maxstock - row.SrcRealPiece);

                        //验证产品是否存在
                        if (!ProductID.HasValue) throw new ApplicationException("请传入产品ID");
                        var productData = prdctMan.QueryProductInfoByProductID(ProductID.Value);
                        bool isExistProduct = (productData?.ProductID ?? 0) > 0;
                        if (!isExistProduct) throw new ApplicationException($"产品ID({ProductID.Value})不存在");

                        //验证来源区域仓库是否有效
                        if (!AreaID.HasValue) throw new ApplicationException("请传入来源区域仓库ID");
                        var areaData = houseMan.QueryAreaByAreaID(new CargoAreaEntity() { AreaID = AreaID.Value });
                        bool isExistArea = (areaData?.AreaID ?? 0) > 0;
                        if (!isExistArea) throw new ApplicationException($"来源区域仓库ID({AreaID.Value})不存在");
                        AreaName = areaData.Name;

                        //验证品牌是否存在
                        if (!TypeID.HasValue) throw new ApplicationException("请传入产品ID");
                        if (!TypeCate.HasValue) throw new ApplicationException("请传入产品品类ID");
                        var typeData = prdctMan.QueryProductType(new CargoProductTypeEntity() { TypeID = TypeID.Value, ParentID = TypeCate.Value })?.FirstOrDefault();
                        bool isExistType = (typeData?.TypeID ?? 0) > 0;
                        if (!isExistType) throw new ApplicationException($"产品品牌不存在。相关数据：TypeID({TypeID.Value})，TypeCate({typeCate.Value})");

                        typeCate = typeData.ParentID;

                        string rplIDParam = "@RplID" + rowIndex;
                        string productIDParam = "@ProductID" + rowIndex;
                        string sidParam = "@SID" + rowIndex;
                        string productNameParam = "@ProductName" + rowIndex;
                        string productCodeParam = "@ProductCode" + rowIndex;
                        string goodsCodeParam = "@GoodsCode" + rowIndex;
                        string houseIDParam = "@HouseID" + rowIndex;
                        string houseNameParam = "@HouseName" + rowIndex;
                        string areaIDParam = "@AreaID" + rowIndex;
                        string areaNameParam = "@AreaName" + rowIndex;
                        string typeCateParam = "@TypeCate" + rowIndex;
                        string typeCateNameParam = "@TypeCateName" + rowIndex;
                        string typeIDParam = "@TypeID" + rowIndex;
                        string typeNameParam = "@TypeName" + rowIndex;
                        string pieceParam = "@Piece" + rowIndex;
                        string minstockParam = "@MinStock" + rowIndex;
                        string maxstockParam = "@MaxStock" + rowIndex;
                        string srcPieceParam = "@SrcPiece" + rowIndex;
                        string restockingqtyParam = "@RestockingQty" + rowIndex;
                        string intransiQtyParam = "@InTransitQty" + rowIndex;
                        string avgSalSumParam = "@AvgSalSUM" + rowIndex;
                        string createDateParam = "@CreateDate" + rowIndex;

                        sqlvaluesStrList.Add($"({rplIDParam}, {productIDParam}, {sidParam}, {productNameParam}, {productCodeParam}, {goodsCodeParam}, " +
                            $"{houseIDParam}, {houseNameParam}, {areaIDParam}, {areaNameParam}, " +
                            $"{typeCateParam}, {typeCateNameParam}, {typeIDParam}, {typeNameParam}, {pieceParam}," +
                            $"{minstockParam}, {maxstockParam}, {srcPieceParam}, " +
                            $"{restockingqtyParam}, {intransiQtyParam}, {avgSalsum}, {createDateParam})");

                        rowssqlParameters.AddRange(new List<SqlParameter>()
                        {
                            //计算
                            new SqlParameter(rplIDParam, SqlDbType.Int) { Value = oosLogData.RplID },
                            new SqlParameter(productIDParam, SqlDbType.BigInt) { Value = ProductID },
                            new SqlParameter(productNameParam, SqlDbType.NVarChar, 100) { Value = productData.ProductName },
                            new SqlParameter(productCodeParam, SqlDbType.NVarChar, 50) { Value = productData.ProductCode },
                            new SqlParameter(goodsCodeParam, SqlDbType.NVarChar, 50) { Value = productData.GoodsCode },
                            new SqlParameter(areaIDParam, SqlDbType.Int) { Value = AreaID },
                            new SqlParameter(areaNameParam, SqlDbType.NVarChar, 50) { Value = AreaName },
                            new SqlParameter(typeIDParam, SqlDbType.Int) { Value = TypeID },
                            new SqlParameter(typeNameParam, SqlDbType.NVarChar, 50) { Value = typeData.TypeName },

                            //传值
                            new SqlParameter(sidParam, SqlDbType.BigInt) { Value = row.SID },
                            new SqlParameter(pieceParam, SqlDbType.Int) { Value = row.Piece },
                            new SqlParameter(typeCateParam, SqlDbType.Int) { Value = row.TypeCate },
                            new SqlParameter(typeCateNameParam, SqlDbType.NVarChar, 50) { Value = row.TypeCateName },
                            new SqlParameter(houseIDParam, SqlDbType.Int) { Value = HouseID},
                            new SqlParameter(houseNameParam, SqlDbType.NVarChar, 50) { Value = HouseName},
                            new SqlParameter(minstockParam, SqlDbType.Int) { Value = minstock },
                            new SqlParameter(maxstockParam, SqlDbType.Int) { Value = maxstock },
                            new SqlParameter(srcPieceParam, SqlDbType.Int) { Value = srcpiece },
                            new SqlParameter(restockingqtyParam, SqlDbType.Int) { Value = restockingqty },
                            new SqlParameter(intransiQtyParam, SqlDbType.Int) { Value = intransiqty },
                            new SqlParameter(avgSalSumParam, SqlDbType.Int) { Value = avgSalsum },

                            //默认值
                            new SqlParameter(createDateParam, SqlDbType.DateTime) { Value = DateTime.Now },
                        });

                        rowIndex++;
                    }

                    insertRowsSQL = insertRowsSQL.Replace("@{sqlvalues}", string.Join(("," + Environment.NewLine), sqlvaluesStrList));
                    using (DbCommand command = conn.GetSqlStringCommond(insertRowsSQL))
                    {
                        command.Parameters.AddRange(rowssqlParameters.ToArray());
                        using (DataTable dt = conn.ExecuteDataTable(command, trns))
                        {
                            var rtRows = new List<CargoOOSLogGoodsDto>();
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    rtRows.Add(new CargoOOSLogGoodsDto()
                                    {
                                        ID = dr.Field<int>("ID"),
                                        RplID = dr.Field<int>("OOSLogID"),
                                        ProductID = dr.Field<long>("ProductID"),
                                        SID = dr.Field<long>("SID"),
                                        ProductCode = dr.Field<string>("ProductCode"),
                                        ProductName = dr.Field<string>("ProductName"),
                                        GoodsCode = dr.Field<string>("GoodsCode"),
                                        TypeCate = dr.Field<int>("TypeCate"),
                                        TypeCateName = dr.Field<string>("TypeCateName"),
                                        TypeID = dr.Field<int>("TypeID"),
                                        TypeName = dr.Field<string>("TypeName"),
                                        HouseID = dr.Field<int>("HouseID"),
                                        HouseName = dr.Field<string>("HouseName"),
                                        ParentHouseID = FromHouse,
                                        ParentHouseName = FromHouseName,
                                        AreaID = dr.Field<int>("AreaID"),
                                        AreaName = dr.Field<string>("AreaName"),
                                        Piece = dr.Field<int>("Piece"),
                                        MinStock = dr.Field<int?>("MinStock"),
                                        MaxStock = dr.Field<int?>("MaxStock"),
                                        SrcPiece = dr.Field<int?>("SrcPiece"),
                                        RestockingQty = dr.Field<int?>("RestockingQty"),
                                        InTransitQty = dr.Field<int?>("InTransitQty"),
                                        AvgSalSUM = dr.Field<int?>("AvgSalSUM"),
                                        CreateDate = dr.Field<DateTime>("CreateDate"),
                                    });
                                }
                            }
                            oosLogData.Rows = rtRows;
                        }
                    }

                    //------------ 插入缺货数据 ------------
                    string insertOutOfStock = @"
PRINT('Merge语法实现插入或更新自动判断，语法建议来自GPT');
MERGE INTO Tbl_Cargo_OutOfStock AS target
USING (
    VALUES
    @{sqlvalues}
) AS source (
    OOSLogID,
    OOSLogRowID,
    ProductID,
    SID,
    ProductName,
    ProductCode,
    GoodsCode,
    HouseID,
    HouseName,
    ParentHouse,
    ParentHouseName,
    AreaID,
    AreaName,
    TypeCate,
    TypeCateName,
    TypeID,
    TypeName,
    Piece,
    CreateDate,
    UpdateDate
)
ON target.AreaID = source.AreaID AND target.ProductCode = source.ProductCode

WHEN MATCHED AND target.Piece <> source.Piece THEN
    UPDATE SET
        target.OldPiece = target.Piece,
        target.Piece = source.Piece,
        target.OOSLogID = source.OOSLogID,
        target.OOSLogRowID = source.OOSLogRowID,
        target.UpdateDate = GETDATE()

WHEN NOT MATCHED THEN
    INSERT (
        OOSLogID,
        OOSLogRowID,
        ProductID,
        SID,
        ProductName,
        ProductCode,
        GoodsCode,
        HouseID,
        HouseName,
        ParentHouse,
        ParentHouseName,
        AreaID,
        AreaName,
        TypeCate,
        TypeCateName,
        TypeID,
        TypeName,
        Piece,
        CreateDate,
        UpdateDate
    )
    VALUES (
        source.OOSLogID,
        source.OOSLogRowID,
        source.ProductID,
        source.SID,
        source.ProductName,
        source.ProductCode,
        source.GoodsCode,
        source.HouseID,
        source.HouseName,
        source.ParentHouse,
        source.ParentHouseName,
        source.AreaID,
        source.AreaName,
        source.TypeCate,
        source.TypeCateName,
        source.TypeID,
        source.TypeName,
        source.Piece,
        source.CreateDate,
        source.UpdateDate
    )

OUTPUT 
    inserted.*;
";
                    rowIndex = 0;
                    sqlvaluesStrList.Clear();
                    rowssqlParameters.Clear();
                    foreach (var row in oosLogData.Rows)
                    {
                        string oosLogIDParam = "@OOSLogID" + rowIndex;
                        string oosLogRowIDParam = "@OOSLogRowID" + rowIndex;
                        string productIDParam = "@ProductID" + rowIndex;
                        string sidParam = "@SID" + rowIndex;
                        string productNameParam = "@ProductName" + rowIndex;
                        string productCodeParam = "@ProductCode" + rowIndex;
                        string goodsCodeParam = "@GoodsCode" + rowIndex;
                        string parentHouseIDParam = "@ParentHouse" + rowIndex;
                        string parentHouseNameParam = "@ParentHouseName" + rowIndex;
                        string houseIDParam = "@HouseID" + rowIndex;
                        string houseNameParam = "@HouseName" + rowIndex;
                        string areaIDParam = "@AreaID" + rowIndex;
                        string areaNameParam = "@AreaName" + rowIndex;
                        string typeCateParam = "@TypeCate" + rowIndex;
                        string typeCateNameParam = "@TypeCateName" + rowIndex;
                        string typeIDParam = "@TypeID" + rowIndex;
                        string typeNameParam = "@TypeName" + rowIndex;
                        string pieceParam = "@Piece" + rowIndex;
                        string createDateParam = "@CreateDate" + rowIndex;
                        string updateDateParam = "@UpdateDate" + rowIndex;

                        sqlvaluesStrList.Add($"({oosLogIDParam}, {oosLogRowIDParam}, {productIDParam}, {sidParam}, {productNameParam}, {productCodeParam}, {goodsCodeParam}, " +
                            $"{houseIDParam}, {houseNameParam}, {parentHouseIDParam}, {parentHouseNameParam}, {areaIDParam}, {areaNameParam}, " +
                            $"{typeCateParam}, {typeCateNameParam}, {typeIDParam}, {typeNameParam}, {pieceParam}," +
                            $"{createDateParam}, {updateDateParam})");

                        rowssqlParameters.AddRange(new List<SqlParameter>()
                        {
                            //计算
                            new SqlParameter(oosLogIDParam, SqlDbType.Int) { Value = row.RplID },
                            new SqlParameter(oosLogRowIDParam, SqlDbType.Int) { Value = row.ID },
                            new SqlParameter(productIDParam, SqlDbType.BigInt) { Value = row.ProductID },
                            new SqlParameter(productNameParam, SqlDbType.NVarChar, 100) { Value = row.ProductName },
                            new SqlParameter(productCodeParam, SqlDbType.NVarChar, 50) { Value = row.ProductCode },
                            new SqlParameter(goodsCodeParam, SqlDbType.NVarChar, 50) { Value = row.GoodsCode },
                            new SqlParameter(areaIDParam, SqlDbType.Int) { Value = row.AreaID },
                            new SqlParameter(areaNameParam, SqlDbType.NVarChar, 50) { Value = row.AreaName },
                            new SqlParameter(typeIDParam, SqlDbType.Int) { Value = row.TypeID },
                            new SqlParameter(typeNameParam, SqlDbType.NVarChar, 50) { Value = row.TypeName },

                            //传值
                            new SqlParameter(sidParam, SqlDbType.BigInt) { Value = row.SID },
                            new SqlParameter(pieceParam, SqlDbType.Int) { Value = row.Piece },
                            new SqlParameter(typeCateParam, SqlDbType.Int) { Value = row.TypeCate },
                            new SqlParameter(typeCateNameParam, SqlDbType.NVarChar, 50) { Value = row.TypeCateName },
                            new SqlParameter(parentHouseIDParam, SqlDbType.Int) { Value = row.ParentHouseID.HasValue ? (object)row.ParentHouseID : DBNull.Value},
                            new SqlParameter(parentHouseNameParam, SqlDbType.NVarChar, 50) { Value = row.ParentHouseName != null ? (object)row.ParentHouseName : DBNull.Value},
                            new SqlParameter(houseIDParam, SqlDbType.Int) { Value = row.HouseID},
                            new SqlParameter(houseNameParam, SqlDbType.NVarChar, 50) { Value = row.HouseName},

                            //默认值
                            new SqlParameter(createDateParam, SqlDbType.DateTime) { Value = DateTime.Now },
                            new SqlParameter(updateDateParam, SqlDbType.DateTime) { Value = DateTime.Now  },
                        });
                        rowIndex++;
                    }

                    insertOutOfStock = insertOutOfStock.Replace("@{sqlvalues}", string.Join(("," + Environment.NewLine), sqlvaluesStrList));
                    var oosList = new List<CargoOutOfStock>();
                    using (DbCommand command = conn.GetSqlStringCommond(insertOutOfStock))
                    {
                        command.Parameters.AddRange(rowssqlParameters.ToArray());
                        using (DataTable dt = conn.ExecuteDataTable(command, trns))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    oosList.Add(new CargoOutOfStock()
                                    {
                                        OOSID = dr.Field<int>("OOSID"),
                                        OOSLogID = dr.Field<int>("OOSLogID"),
                                        OOSLogRowID = dr.Field<int>("OOSLogRowID"),
                                        ProductID = dr.Field<long>("ProductID"),
                                        SID = dr.Field<long>("SID"),
                                        ProductCode = dr.Field<string>("ProductCode"),
                                        ProductName = dr.Field<string>("ProductName"),
                                        GoodsCode = dr.Field<string>("GoodsCode"),
                                        TypeCate = dr.Field<int>("TypeCate"),
                                        TypeCateName = dr.Field<string>("TypeCateName"),
                                        TypeID = dr.Field<int>("TypeID"),
                                        TypeName = dr.Field<string>("TypeName"),
                                        HouseID = dr.Field<int>("HouseID"),
                                        HouseName = dr.Field<string>("HouseName"),
                                        ParentHouse = dr.Field<int?>("ParentHouse"),
                                        ParentHouseName = dr.Field<string>("ParentHouseName"),
                                        AreaID = dr.Field<int>("AreaID"),
                                        AreaName = dr.Field<string>("AreaName"),
                                        OldPiece = dr.Field<int?>("OldPiece"),
                                        Piece = dr.Field<int>("Piece"),
                                        CreateDate = dr.Field<DateTime>("CreateDate"),
                                        UpdateDate = dr.Field<DateTime>("UpdateDate"),
                                    });
                                }
                            }
                        }
                    }

                    //------------ 记录缺货变更日志 ------------
                    string insertOOSChange = @"
INSERT INTO
	Tbl_Cargo_OutOfStockChange (
        OOSID,
		OOSLogID,
        OOSLogRowID,
        OldPiece,
        NewPiece,
        CreateDate
	)
OUTPUT 
    INSERTED.*
VALUES
@{sqlvalues}
";
                    rowIndex = 0;
                    sqlvaluesStrList.Clear();
                    rowssqlParameters.Clear();
                    foreach (var row in oosList)
                    {
                        string oosIDParam = "@OOSID" + rowIndex;
                        string oosLogIDParam = "@OOSLogID" + rowIndex;
                        string oosLogRowIDParam = "@OOSLogRowID" + rowIndex;
                        string oldPieceParam = "@OldPiece" + rowIndex;
                        string newPieceParam = "@NewPiece" + rowIndex;
                        string createDateParam = "@CreateDate" + rowIndex;

                        sqlvaluesStrList.Add($"({oosIDParam}, {oosLogIDParam}, {oosLogRowIDParam}, {oldPieceParam}, {newPieceParam}, {createDateParam})");

                        rowssqlParameters.AddRange(new List<SqlParameter>()
                        {
                            //计算
                            new SqlParameter(oosIDParam, SqlDbType.Int) { Value = row.OOSID },
                            new SqlParameter(oosLogIDParam, SqlDbType.Int) { Value = row.OOSLogID },
                            new SqlParameter(oosLogRowIDParam, SqlDbType.Int) { Value = row.OOSLogRowID },
                            new SqlParameter(oldPieceParam, SqlDbType.Int) { Value = row.OldPiece.HasValue ? (object)row.OldPiece.Value : DBNull.Value },
                            new SqlParameter(newPieceParam, SqlDbType.Int) { Value = row.Piece },

                            //默认值
                            new SqlParameter(createDateParam, SqlDbType.DateTime) { Value = DateTime.Now },
                        });

                        rowIndex++;
                    }

                    insertOOSChange = insertOOSChange.Replace("@{sqlvalues}", string.Join(("," + Environment.NewLine), sqlvaluesStrList));

                    using (DbCommand command = conn.GetSqlStringCommond(insertOOSChange))
                    {
                        command.Parameters.AddRange(rowssqlParameters.ToArray());
                        conn.ExecuteNonQuery(command, trns);
                    }

                    //------------ 提交所有变更 ------------
                    trns.Commit();
                    return oosLogData;
                }
                catch (ApplicationException ex)
                {
                    trns.RollBack();
                    throw new ApplicationException(ex.Message);
                }
            }
        }
        public void CancelRplOrder(int[] RplIDs, string UserID, string UserName)
        {
            SqlHelper conn = new SqlHelper();
            if (RplIDs.Any())
            {
                var rplListData = QueryRplOrder(new CargoRplOrderParams { RplIDs = new List<int>(RplIDs) })?.Data;
                if (rplListData == null || !rplListData.Any())
                {
                    throw new ApplicationException($"未找到对应的补货单数据。参数：RplIDs:{string.Join(",", RplIDs)}");
                }
                foreach (var RplID in RplIDs)
                {
                    var matchedRpl = rplListData.FirstOrDefault(c => c.RplID == RplID);
                    if (matchedRpl == null)
                    {
                        throw new ApplicationException($"未找到对应的补货单数据。参数：RplID:{RplID}");
                    }
                    //检查补货单为 待处理|已取消 状态
                    if (!new byte[] { 0, 4 }.Contains(matchedRpl.Status.GetValueOrDefault()))
                    {
                        throw new ApplicationException($"补货单({matchedRpl.RplNo})已经开始补货，不能作废");
                    }
                    //检查已完成补货数为0
                    var anyDonePiece = matchedRpl.DonePiece.GetValueOrDefault() > 0;
                    if (anyDonePiece)
                    {
                        throw new ApplicationException($"补货单({matchedRpl.RplNo})已经有过收货数量，不能作废");
                    }
                }

                string cancelRplSqlStr = @"
UPDATE Tbl_Cargo_RplOrder SET Status = 3 WHERE RplID IN @{RplIDs};
";
                cancelRplSqlStr = cancelRplSqlStr.Replace("@{RplIDs}", "(" + string.Join(",", RplIDs) + ")");
                using (var comm = conn.GetSqlStringCommond(cancelRplSqlStr))
                {
                    conn.ExecuteNonQuery(comm);
                }


                //刷新缺货数据
                UpdateOOSParam oosParams = new UpdateOOSParam()
                {
                    UserID = UserID,
                    UserName = UserName,
                    ReasonTag = "RO", //补货单标记
                    Reason = "作废补货单"
                };
                var oosRows = new List<UpdateOOSGoodsParam>();
                string queryOOSDataStr = @"
SELECT ProductID, AreaID FROM Tbl_Cargo_RplOrderGoods WHERE RplID IN @{RplIDs}
";
                queryOOSDataStr = queryOOSDataStr.Replace("@{RplIDs}", "(" + string.Join(",", RplIDs) + ")");
                using (DbCommand command = conn.GetSqlStringCommond(queryOOSDataStr))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            oosRows.Add(new UpdateOOSGoodsParam()
                            {
                                ProductID = dr.Field<long>("ProductID"),
                                AreaID = dr.Field<int>("AreaID"),
                            });
                        }
                        oosParams.Rows = oosRows;
                    }
                }

                if (oosParams.Rows.Count > 0)
                {
                    UpdtOutOfStock(oosParams);
                }
            }
        }

        public CargoOutOfStockListDto QueryOutOfStocks(CargoOutOfStockParams queryParams)
        {
            CargoOutOfStockListDto result = new CargoOutOfStockListDto();
            try
            {
                #region 组装查询SQL语句
                StringBuilder strbld = new StringBuilder();
                List<string> conditions = new List<string>();
                List<SqlParameter> parameters = new List<SqlParameter>();
                strbld.AppendLine("------------------ 查询缺货清单 -----------------");

                //补货单详情表
                strbld.AppendLine(@"
WITH iti AS (
	SELECT
		fo.ProductCode,
		fo.HouseID,
		SUM(ReplyNumber - fo.InPiece) Piece
	FROM
		Tbl_Cargo_FactoryOrder fo
	WHERE
		(1 = 1)
		AND fo.InCargoStatus IN (0,2)
	GROUP BY
		fo.ProductCode,
		fo.HouseID
)
,prdctGrp AS (
SELECT 
	MAX(p.ProductID) ProductID,
	p.ProductCode,
	p.HouseID,
	SUM(cg.Piece) Piece
FROM
	Tbl_Cargo_Product p
	INNER JOIN Tbl_Cargo_ProductType pt ON p.TypeID = pt.TypeID
	INNER JOIN Tbl_Cargo_ContainerGoods cg ON p.ProductID = cg.ProductID
WHERE
	ISNULL(p.ProductCode, '') <> ''
GROUP BY
	p.ProductCode, p.HouseID
)
,ro as (
SELECT
	rog.ProductCode, 
	ro.FromHouse HouseID,
	SUM(rog.Piece - rog.DonePiece) Piece
FROM
	Tbl_Cargo_RplOrderGoods rog 
	INNER JOIN Tbl_Cargo_RplOrder ro ON rog.RplID = ro.RplID
WHERE
	ro.Status NOT IN (2,3)
GROUP BY 
	rog.ProductCode, ro.FromHouse
)

SELECT
	oos.*,
    ISNULL(iti.Piece, 0) InTransitStock,
    ISNULL(cs.Piece, 0) CurStock,
	ISNULL(ro.Piece, 0) RestockingPiece,
    p.Model,
    p.Specs,
    p.Figure,
    p.LoadIndex,
    p.SpeedLevel,
    p.Batch,
    oosLg.MinStock,
    oosLg.MaxStock
FROM Tbl_Cargo_OutOfStock oos
LEFT JOIN  iti ON oos.ProductCode = iti.ProductCode AND oos.HouseID = iti.HouseID
LEFT JOIN prdctGrp cs ON oos.ProductCode = cs.ProductCode AND oos.HouseID = cs.HouseID
LEFT JOIN Tbl_Cargo_Product p ON oos.ProductID = p.ProductID
LEFT JOIN Tbl_Cargo_OutOfStockLogGoods oosLg ON oos.OOSLogRowID = oosLg.ID 
LEFT JOIN ro ON ro.ProductCode = p.ProductCode AND ro.HouseID = p.HouseID
WHERE (1=1) AND oos.Piece > 0
@{conditions}
");
                if (queryParams.TypeID.HasValue)
                {
                    conditions.Add("oos.TypeID = @TypeID");
                    parameters.Add(new SqlParameter("@TypeID", SqlDbType.Int) { Value = queryParams.TypeID.Value });
                }
                if (queryParams.TypeCate.HasValue)
                {
                    conditions.Add("oos.TypeCate = @TypeCate");
                    parameters.Add(new SqlParameter("@TypeCate", SqlDbType.Int) { Value = queryParams.TypeCate.Value });
                }
                if (queryParams.HouseID.HasValue)
                {
                    conditions.Add("oos.HouseID = @HouseID");
                    parameters.Add(new SqlParameter("@HouseID", SqlDbType.Int) { Value = queryParams.HouseID.Value });
                }
                if (queryParams.AreaID.HasValue)
                {
                    conditions.Add("oos.AreaID = @AreaID");
                    parameters.Add(new SqlParameter("@AreaID", SqlDbType.Int) { Value = queryParams.AreaID.Value });
                }
                if (queryParams.ParentHouse.HasValue)
                {
                    conditions.Add("ParentHouse = @ParentHouse");
                    parameters.Add(new SqlParameter("@ParentHouse", SqlDbType.Int) { Value = queryParams.ParentHouse.Value });
                }

                if (conditions.Count > 0)
                {
                    strbld = strbld.Replace("@{conditions}", "AND " + string.Join(" AND ", conditions));
                }
                else
                {
                    strbld = strbld.Replace("@{conditions}", "");
                }

                #endregion
                string strSQL = strbld.ToString();
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    //string paramsStr = string.Join(", ", command.Parameters.Cast<SqlParameter>().Select(p => $"{p.ParameterName}={p.Value}"));
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        List<CargoOutOfStockDto> sqlData = new List<CargoOutOfStockDto>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sqlData.Add(new CargoOutOfStockDto
                            {
                                OOSID = dr.Field<int?>("OOSID"),
                                OOSLogID = dr.Field<int?>("OOSLogID"),
                                OOSLogRowID = dr.Field<int?>("OOSLogRowID"),
                                ProductID = dr.Field<long?>("ProductID"),
                                SID = dr.Field<long?>("SID"),
                                ProductName = dr.Field<string>("ProductName"),
                                ProductCode = dr.Field<string>("ProductCode"),
                                GoodsCode = dr.Field<string>("GoodsCode"),
                                HouseID = dr.Field<int?>("HouseID"),
                                HouseName = dr.Field<string>("HouseName"),
                                ParentHouse = dr.Field<int?>("ParentHouse"),
                                ParentHouseName = dr.Field<string>("ParentHouseName"),
                                AreaID = dr.Field<int?>("AreaID"),
                                AreaName = dr.Field<string>("AreaName"),
                                TypeCate = dr.Field<int?>("TypeCate"),
                                TypeCateName = dr.Field<string>("TypeCateName"),
                                TypeID = dr.Field<int?>("TypeID"),
                                TypeName = dr.Field<string>("TypeName"),
                                OldPiece = dr.Field<int?>("OldPiece"),
                                Piece = dr.Field<int?>("Piece"),
                                CreateDate = dr.Field<DateTime?>("CreateDate"),
                                UpdateDate = dr.Field<DateTime?>("UpdateDate"),

                                InTransitStock = dr.Field<int?>("InTransitStock"),
                                CurStock = dr.Field<int?>("CurStock"),
                                RestockingPiece = dr.Field<int?>("RestockingPiece"),
                                Model = dr.Field<string>("Model"),
                                Specs = dr.Field<string>("Specs"),
                                Figure = dr.Field<string>("Figure"),
                                LoadIndex = dr.Field<string>("LoadIndex"),
                                SpeedLevel = dr.Field<string>("SpeedLevel"),
                                Batch = dr.Field<string>("Batch"),
                                MaxStock = dr.Field<int?>("MaxStock"),
                                MinStock = dr.Field<int?>("MinStock"),
                            });
                        }
                        result.Data = sqlData;
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }

        /// <summary>
        /// 更新缺货数据
        /// </summary>
        /// <param name="HouseID">更新范围</param>
        /// <returns>返回受影响行数</returns>
        public UpdateOOSParam UpdtOOSByHouse(int? HouseID, string UserID, string UserName)
        {
            //验证仓库是否有效
            if (!HouseID.HasValue) throw new ApplicationException("请传入仓库ID");
            if(HouseID > 0)
            {
                CargoHouseManager houseMan = new CargoHouseManager();
                var houseData = houseMan.QueryCargoHouseByID(HouseID.Value);
                bool isExistHouse = (houseData?.HouseID ?? 0) > 0;
                if (!isExistHouse) throw new ApplicationException($"仓库ID({HouseID.Value})不存在");
            }

            UpdateOOSParam oosParams = new UpdateOOSParam()
            {
                UserID = UserID,
                UserName = UserName,
                ReasonTag = "RF" //刷新标记
            };
            var conn = new SqlHelper();
            //获取仓库缺货数据
            var oosRows = new List<UpdateOOSGoodsParam>();
            
            string queryOOSDataStr = @"
SELECT ProductID, AreaID FROM Tbl_Cargo_OutOfStock WHERE (1=1) @{conditions}
";
            if (HouseID == -1)
            {
                queryOOSDataStr = queryOOSDataStr.Replace("@{conditions}", "");
            }
            else
            {
                queryOOSDataStr = queryOOSDataStr.Replace("@{conditions}", "AND HouseID=" + HouseID);
            }
            using (DbCommand command = conn.GetSqlStringCommond(queryOOSDataStr))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        oosRows.Add(new UpdateOOSGoodsParam()
                        {
                            ProductID = dr.Field<long>("ProductID"),
                            AreaID = dr.Field<int>("AreaID"),
                        });
                    }
                    oosParams.Rows = oosRows;
                }
            }
            
            if(oosParams.Rows.Count > 0)
            {
                UpdtOutOfStock(oosParams);
            }
            return oosParams;
        }
        #endregion
        #region 来货单 
        public void AddPurchaseOrderInfo(CargoOrderEntity entity, List<CargoProductEntity> productList)
        {
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_PurchaseOrder(LogisAwbNo,LogisID,Dep,Dest,Piece,TransitFee,TransportFee,HandFee,OtherFee,TotalCharge,CheckOutType,TrafficType,DeliveryType,ClientNum,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,CreateAwbID,CreateAwb,CreateDate,HouseID,OP_ID,OP_DATE,Remark,FacOrderNo) VALUES (@LogisAwbNo,@LogisID,@Dep,@Dest,@Piece,@TransitFee,@TransportFee,@HandFee,@OtherFee,@TotalCharge,@CheckOutType,@TrafficType,@DeliveryType,@ClientNum,@AcceptUnit,@AcceptAddress,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@CreateAwbID,@CreateAwb,@CreateDate,@HouseID,@OP_ID,@OP_DATE,@Remark,@FacOrderNo) SELECT @@IDENTITY";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, entity.LogisAwbNo);
                    conn.AddInParameter(cmd, "@LogisID", DbType.Int32, entity.LogisID);
                    conn.AddInParameter(cmd, "@Dep", DbType.String, entity.Dep);
                    conn.AddInParameter(cmd, "@Dest", DbType.String, entity.Dest);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                    conn.AddInParameter(cmd, "@HandFee", DbType.Decimal, entity.HandFee);
                    conn.AddInParameter(cmd, "@OtherFee", DbType.Decimal, entity.OtherFee);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@CheckOutType", DbType.String, entity.CheckOutType);
                    conn.AddInParameter(cmd, "@TrafficType", DbType.String, entity.TrafficType);
                    conn.AddInParameter(cmd, "@DeliveryType", DbType.String, entity.DeliveryType);
                    conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                    conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                    conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                    conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                    conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                    conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                    conn.AddInParameter(cmd, "@CreateAwbID", DbType.String, entity.CreateAwbID);
                    conn.AddInParameter(cmd, "@CreateAwb", DbType.String, entity.CreateAwb);
                    conn.AddInParameter(cmd, "@CreateDate", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, entity.FacOrderNo);
                    ////马牌来货单号
                    //if (!string.IsNullOrEmpty(entity.HorseFacOrderNo))
                    //{
                    //    conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, entity.HorseFacOrderNo);
                    //}
                    //else
                    //{
                    //    conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, entity.FacOrderNo);
                    //}
                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                #endregion

                //新增产品与订单关联数据
                AddPurchaseOrderGoodsInfo(productList, did);

            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        public void AddPurchaseOrderGoodsInfo(List<CargoProductEntity> productList, long OrderID)
        {
            foreach (var it in productList)
            {
                it.EnSafe();
                string strSQL = @"INSERT INTO Tbl_Cargo_PurchaseOrderGoods(OrderID,TypeID,Piece ,Specs,ProductCode,Figure,Model,GoodsCode,LoadIndex,SpeedLevel,Born,Batch,UnitPrice,CostPrice,TradePrice,SalePrice,OP_ID,OP_DATE,InHousePrice,BatchYear) VALUES  (@OrderID,@TypeID,@Piece ,@Specs,@ProductCode,@Figure,@Model,@GoodsCode,@LoadIndex,@SpeedLevel,@Born,@Batch,@UnitPrice,@CostPrice,@TradePrice,@SalePrice,@OP_ID,@OP_DATE,@InHousePrice,@BatchYear)";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, OrderID);
                    conn.AddInParameter(cmd, "@TypeID", DbType.Int32, it.TypeID);
                    conn.AddInParameter(cmd, "@Piece ", DbType.Int32, it.InPiece);
                    conn.AddInParameter(cmd, "@ProductCode ", DbType.String, it.ProductCode);
                    conn.AddInParameter(cmd, "@Specs", DbType.String, it.Specs);
                    conn.AddInParameter(cmd, "@Figure", DbType.String, it.Figure);
                    conn.AddInParameter(cmd, "@Model", DbType.String, it.Model);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, it.GoodsCode);
                    conn.AddInParameter(cmd, "@LoadIndex ", DbType.String, it.LoadIndex);
                    conn.AddInParameter(cmd, "@SpeedLevel ", DbType.String, it.SpeedLevel);
                    conn.AddInParameter(cmd, "@Born ", DbType.String, it.Born);
                    conn.AddInParameter(cmd, "@Batch", DbType.String, it.Batch);
                    conn.AddInParameter(cmd, "@BatchYear", DbType.String, it.BatchYear);
                    conn.AddInParameter(cmd, "@InHousePrice", DbType.Decimal, it.InHousePrice);
                    conn.AddInParameter(cmd, "@UnitPrice", DbType.Decimal, it.UnitPrice);
                    conn.AddInParameter(cmd, "@CostPrice", DbType.Decimal, it.CostPrice);
                    conn.AddInParameter(cmd, "@TradePrice", DbType.Decimal, it.TradePrice);
                    conn.AddInParameter(cmd, "@SalePrice", DbType.Decimal, it.SalePrice);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, it.OPID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }

                UpdateSupplierProductPrice(it);
            }
        }
        /// <summary>
        /// 修改供应商产品单价
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateSupplierProductPrice(CargoProductEntity entity)
        {
            string strSQL = " UPDATE Tbl_Cargo_Product  SET SalePrice =@SalePrice,InHousePrice=@InHousePrice where ProductCode=@ProductCode AND HouseID=@HouseID and SuppClientNum=@SuppClientNum and BatchYear=@BatchYear";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@SuppClientNum", DbType.Int64, entity.SuppClientNum);
                conn.AddInParameter(command, "@HouseID", DbType.Int32, entity.HouseID);
                conn.AddInParameter(command, "@BatchYear", DbType.Int32, entity.BatchYear);
                conn.AddInParameter(command, "@ProductCode", DbType.String, entity.ProductCode);
                conn.AddInParameter(command, "@InHousePrice", DbType.Decimal, entity.InHousePrice);
                conn.AddInParameter(command, "@SalePrice", DbType.Decimal, entity.SalePrice);
                conn.ExecuteNonQuery(command);
            }

        }

        /// <summary>
        /// 查询进货单数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryPurchaseOrderInfo(int pIndex, int pNum, CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(Select ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,* from (SELECT DISTINCT a.*,c.Name as HouseName,case when ISNULL(sum(b.InCargoStatus) over (partition by b.FacOrderNo),-1)=-1 then '0' when ISNULL(sum(b.InCargoStatus) over (partition by b.FacOrderNo),-1)= 0 then '0' when ISNULL(sum(b.InCargoStatus) over (partition by b.FacOrderNo),-1)= COUNT(*) over(partition by b.FacOrderNo) then '1' else '2' end as AwbStatus  from Tbl_Cargo_PurchaseOrder as a inner join tbl_Cargo_house as c on a.HouseID=c.HouseID left join Tbl_Cargo_FactoryOrder as b on a.FacOrderNo=b.FacOrderNo Where (1=1) ";

                //string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                //strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,h.Name as HouseName from Tbl_Cargo_PurchaseOrder as a inner join Tbl_Cargo_House h on a.HouseID=h.HouseID Where (1=1) ";
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and (a.AcceptPeople like '%" + entity.AcceptUnit + "%' or a.AcceptUnit like '%" + entity.AcceptUnit + "%')"; }
                //订单编号 
                if (!entity.OrderID.Equals(0)) { strSQL += " and a.OrderID = " + entity.OrderID; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (!string.IsNullOrEmpty(entity.TrafficType)) { strSQL += " and a.TrafficType='" + entity.TrafficType + "'"; }
                if (!string.IsNullOrEmpty(entity.FacOrderNo)) { strSQL += " and a.FacOrderNo='" + entity.FacOrderNo + "'"; }
                if (!entity.ClientNum.Equals(0)) { strSQL += " and a.ClientNum = " + entity.ClientNum; }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                strSQL += " ) as a";

                if (!string.IsNullOrEmpty(entity.AwbStatus)) { strSQL += " where a.AwbStatus in (" + entity.AwbStatus + ")"; }
                strSQL += ") A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) order by A.RowNumber";
                ////所属仓库ID
                //if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                //strSQL += " ) A ";
                //strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) order by A.RowNumber";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                HandFee = Convert.ToDecimal(idr["HandFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                FacOrderNo = Convert.ToString(idr["FacOrderNo"]),
                                AwbStatus = Convert.ToString(idr["AwbStatus"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                ReceivingStatus = Convert.ToString(idr["ReceivingStatus"]),
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_PurchaseOrder as a Where (1=1)";
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strCount += " and (a.AcceptPeople like '%" + entity.AcceptUnit + "%' or a.AcceptUnit like '%" + entity.AcceptUnit + "%')"; }
                //订单编号 
                if (!entity.OrderID.Equals(0)) { strCount += " and a.OrderID = " + entity.OrderID; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 删除来货订单数据
        /// </summary>
        /// <param name="entity"></param>
        public void DeletePurchaseOrderInfo(CargoOrderEntity entity)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_PurchaseOrder Where OrderID=@OrderID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 通过订单号删除来货订单与产品关联表数据
        /// </summary>
        /// <param name="good"></param>
        public void DeletePurchaseOrderGoodsInfo(List<CargoFactoryOrderEntity> entity)
        {
            foreach (var it in entity)
            {
                string strSQL = @"Delete from Tbl_Cargo_FactoryOrder where FacOrderNo=@FacOrderNo";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, it.FacOrderNo);
                    //if (!it.ID.Equals(0)) { conn.AddInParameter(cmd, "@ID", DbType.Int64, it.ID); }
                    //if (!it.OrderID.Equals(0)) { conn.AddInParameter(cmd, "@OrderID", DbType.Int64, it.OrderID); }
                    conn.ExecuteNonQuery(cmd);
                }
            }
        }
        public List<CargoProductEntity> QueryPurchaseOrderByOrderID(CargoOrderEntity entity)
        {
            List<CargoProductEntity> result = new List<CargoProductEntity>();
            //string strSQL = "select a.*,b.TypeName From Tbl_Cargo_PurchaseOrderGoods as a inner join Tbl_Cargo_ProductType as b on a.TypeID = b.TypeID Where (1=1)";and a.LoadIndex = d.LoadIndex and a.SpeedLevel = d.SpeedLevel
            string strSQL = "select a.*,c.FacOrderNo,c.HouseID,b.TypeName,ISNULL(d.InCargoStatus,0) as InCargoStatus,ISNULL(d.InPiece,a.Piece) as InPiece,ISNULL(d.ID,0) as FacID From Tbl_Cargo_PurchaseOrderGoods as a inner join Tbl_Cargo_ProductType as b on a.TypeID = b.TypeID inner join Tbl_Cargo_PurchaseOrder as c on a.OrderID=c.OrderID left join Tbl_Cargo_FactoryOrder as d on a.TypeID = d.TypeID and a.Specs = d.Specs and a.Figure = d.Figure and a.GoodsCode = d.GoodsCode and c.FacOrderNo = d.FacOrderNo Where (1=1)";
            if (!entity.OrderID.Equals(0)) { strSQL += " and a.OrderID=" + entity.OrderID; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoProductEntity
                        {
                            FacID = Convert.ToInt64(idr["FacID"]),
                            GoodsID = Convert.ToInt64(idr["GoodsID"]),
                            ProductID = Convert.ToInt64(idr["OrderID"]),
                            InPiece = Convert.ToInt32(idr["Piece"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            Model = Convert.ToString(idr["Model"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            InHousePrice = Convert.ToDecimal(idr["InHousePrice"]),
                            UnitPrice = Convert.ToDecimal(idr["UnitPrice"]),
                            SalePrice = Convert.ToDecimal(idr["SalePrice"]),
                            CostPrice = Convert.ToDecimal(idr["CostPrice"]),
                            TradePrice = Convert.ToDecimal(idr["TradePrice"]),
                            OPID = Convert.ToString(idr["OP_ID"]),
                            Born = Convert.ToString(idr["Born"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            SourceOrderNo = Convert.ToString(idr["FacOrderNo"]),
                            //Memo = Convert.ToString(idr["Memo"]),
                            InCargoStatus = Convert.ToString(idr["InCargoStatus"]),
                            Numbers = Convert.ToInt32(idr["InPiece"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            ReturnPiece = string.IsNullOrEmpty(Convert.ToString(idr["ReturnPiece"])) ? 0 : Convert.ToInt32(idr["ReturnPiece"]),
                            ReceivePiece = string.IsNullOrEmpty(Convert.ToString(idr["ReceivePiece"])) ? 0 : Convert.ToInt32(idr["ReceivePiece"]),
                            ProductCode = Convert.ToString(idr["ProductCode"]),
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 修改来货单
        /// </summary>
        /// <param name="entity"></param>
        public void UpdatePurchaseOrderInfo(CargoOrderEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_Cargo_PurchaseOrder set Piece=@Piece,TransportFee=@TransportFee,TotalCharge=@TotalCharge,ClientNum=@ClientNum,AcceptUnit=@AcceptUnit,AcceptAddress=@AcceptAddress,AcceptPeople=@AcceptPeople,AcceptTelephone=@AcceptTelephone,AcceptCellphone=@AcceptCellphone,OP_ID=@OP_ID,OP_DATE=@OP_DATE,Remark=@Remark Where OrderID=@OrderID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                conn.AddInParameter(cmd, "@TransportFee", DbType.Decimal, entity.TransportFee);
                conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                conn.AddInParameter(cmd, "@ClientNum", DbType.Int32, entity.ClientNum);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, entity.AcceptUnit);
                conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, entity.AcceptAddress);
                conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, entity.AcceptPeople);
                conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, entity.AcceptTelephone);
                conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, entity.AcceptCellphone);
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改来货明细
        /// </summary>
        /// <param name="entity"></param>
        public void UpdatePurchaseOrderGoods(CargoProductEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_Cargo_PurchaseOrderGoods set Piece=@Piece,UnitPrice=@UnitPrice,CostPrice=@CostPrice,TradePrice=@TradePrice,SalePrice=@SalePrice Where GoodsID=@GoodsID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@GoodsID", DbType.Int64, entity.GoodsID);
                conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.InPiece);
                conn.AddInParameter(cmd, "@UnitPrice", DbType.Decimal, entity.UnitPrice);
                conn.AddInParameter(cmd, "@CostPrice", DbType.Decimal, entity.CostPrice);
                conn.AddInParameter(cmd, "@TradePrice", DbType.Decimal, entity.TradePrice);
                conn.AddInParameter(cmd, "@SalePrice", DbType.Decimal, entity.SalePrice);
                conn.ExecuteNonQuery(cmd);
            }
            CargoFactoryOrderManager factoryOrderManager = new CargoFactoryOrderManager();

            factoryOrderManager.UpdateFactoryOrderNum(new CargoFactoryOrderEntity { ID = entity.FacID, ReplyNumber = entity.InPiece, OrderNum = entity.InPiece });
        }
        /// <summary>
        /// 删除来货明细
        /// </summary>
        /// <param name="entity"></param>
        public void DeletePurchaseOrderGood(CargoProductEntity entity)
        {
            string strSQL = @"Delete from Tbl_Cargo_PurchaseOrderGoods where GoodsID=@GoodsID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@GoodsID", DbType.Int64, entity.GoodsID);
                conn.ExecuteNonQuery(cmd);
            }
            if (entity.FacID > 0)
            {
                CargoFactoryOrderManager factoryOrderManager = new CargoFactoryOrderManager();
                List<CargoFactoryOrderEntity> cargoFactories = new List<CargoFactoryOrderEntity>();
                cargoFactories.Add(new CargoFactoryOrderEntity { ID = entity.FacID });
                factoryOrderManager.DeleteData(cargoFactories);
            }
        }
        /// <summary>
        /// 导出进货单数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable ExportPurchaseOrderDto(int pIndex, int pNum, CargoOrderEntity entity)
        {
            List<ExportPurchaseOrderDto> result = new List<ExportPurchaseOrderDto>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,h.Name as HouseName,   g.ProductCode,g.Specs,g.Figure,g.GoodsCode,g.LoadIndex,g.SpeedLevel,g.Batch,g.Piece as InHousePice,g.ReceivePiece,g.CostPrice,SalePrice,b.TypeName from Tbl_Cargo_PurchaseOrder as a inner join Tbl_Cargo_House h on a.HouseID=h.HouseID inner join Tbl_Cargo_PurchaseOrderGoods g on  a.OrderID= g.OrderID inner join Tbl_Cargo_ProductType as b on g.TypeID = b.TypeID " +
                    "Where (1=1) ";
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strSQL += " and (a.AcceptPeople like '%" + entity.AcceptUnit + "%' or a.AcceptUnit like '%" + entity.AcceptUnit + "%')"; }
                //订单编号 
                if (!entity.OrderID.Equals(0)) { strSQL += " and a.OrderID = " + entity.OrderID; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                if (!string.IsNullOrEmpty(entity.TrafficType))
                {
                    strSQL += " and a.TrafficType = " + entity.TrafficType;
                }
                if (!entity.ClientNum.Equals(0))
                {
                    strSQL += " and a.ClientNum = " + entity.ClientNum;
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) order by A.RowNumber";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new ExportPurchaseOrderDto
                            {
                                Specs = Convert.ToString(idr["Specs"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                ReceivingStatus = Convert.ToString(idr["ReceivingStatus"]),
                                InPiece = Convert.ToInt32(idr["Piece"]),
                                ReceivePiece = string.IsNullOrEmpty(Convert.ToString(idr["ReceivePiece"])) ? 0 : Convert.ToInt32(idr["ReceivePiece"]),
                                ProductCode = Convert.ToString(idr["ProductCode"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                SalePrice = Convert.ToDecimal(idr["SalePrice"]),
                                CostPrice = Convert.ToDecimal(idr["CostPrice"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                FacOrderNo = Convert.ToString(idr["FacOrderNo"]),
                                Batch = Convert.ToString(idr["Batch"]),

                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_PurchaseOrder as a Where (1=1)";
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptUnit)) { strCount += " and (a.AcceptPeople like '%" + entity.AcceptUnit + "%' or a.AcceptUnit like '%" + entity.AcceptUnit + "%')"; }
                //订单编号 
                if (!entity.OrderID.Equals(0)) { strCount += " and a.OrderID = " + entity.OrderID; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strCount += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        public void UpdatePurchaseOrderFee(CargoOrderEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"UPDATE Tbl_Cargo_PurchaseOrder set TransitFee=@TransitFee,HandFee=@HandFee,TotalCharge=TransportFee+@TransitFee+@HandFee,OP_ID=@OP_ID,OP_DATE=@OP_DATE where HouseID=@HouseID and OrderID=@OrderID ";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@TransitFee", DbType.Decimal, entity.TransitFee);
                    conn.AddInParameter(cmd, "@HandFee", DbType.Decimal, entity.HandFee);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@OrderID", DbType.String, entity.OrderID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public void DelPurchaseOrder(CargoOrderEntity entity)
        {
            try
            {
                string strSQL = @"Delete from Tbl_Cargo_PurchaseOrder where OrderID=@OrderID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.String, entity.OrderID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void UpdatePurchaseOrderProductCostPrice(CargoProductEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"UPDATE Tbl_Cargo_Product set FinalCostPrice=UnitPrice+@CostPrice,CostPrice=UnitPrice+@CostPrice,TaxCostPrice=UnitPrice+@CostPrice,NoTaxCostPrice=UnitPrice+@CostPrice,OP_DATE=@OP_DATE where HouseID=@HouseID and SourceOrderNo=@SourceOrderNo ";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CostPrice", DbType.Decimal, entity.CostPrice);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@SourceOrderNo", DbType.String, entity.SourceOrderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public void UpdatePurchaseOrderFactoryOrderCostPrice(CargoFactoryOrderEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"UPDATE Tbl_Cargo_FactoryOrder set CostPrice=UnitPrice+@CostPrice,TaxCostPrice=UnitPrice+@CostPrice,NoTaxCostPrice=UnitPrice+@CostPrice,OP_Name=@OP_Name,OP_DATE=@OP_DATE where HouseID=@HouseID and FacOrderNo=@FacOrderNo ";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CostPrice", DbType.Decimal, entity.CostPrice);
                    conn.AddInParameter(cmd, "@OP_Name", DbType.String, entity.OP_Name);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, entity.FacOrderNo);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public List<CargoOrderReturnOrderEntity> QueryOesOrderInfoForReturn(CargoOrderReturnOrderEntity entity)
        {
            CargoHouseManager house = new CargoHouseManager();
            List<CargoOrderReturnOrderEntity> result = new List<CargoOrderReturnOrderEntity>();
            try
            {
                string strSQL = @"select a.OrderID,a.CreateAwb,a.CreateDate,a.FacOrderNo,a.ClientNum,a.Piece as TotalPiece,a.TransportFee as TotalCharge,b.Piece,b.UnitPrice,b.CostPrice,b.TradePrice,b.SalePrice,b.Specs,b.Figure,b.Model,b.GoodsCode,b.Batch,b.Born,b.LoadIndex,b.SpeedLevel,b.TypeID,c.TypeName,a.AcceptUnit,a.AcceptPeople,a.AcceptCellphone,a.AcceptAddress,a.OP_DATE from Tbl_Cargo_PurchaseOrder a inner join Tbl_Cargo_PurchaseOrderGoods b on a.OrderID=b.OrderID inner join Tbl_Cargo_ProductType c on b.TypeID=c.TypeID Where (1=1) ";
                //收货单位,人
                if (!string.IsNullOrEmpty(entity.AcceptPeople)) { strSQL += " and (a.AcceptPeople like '%" + entity.AcceptPeople + "%' or a.AcceptUnit like '%" + entity.AcceptPeople + "%')"; }
                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderID = '" + entity.OrderNo.ToUpper() + "'"; }
                //制单人
                if (!string.IsNullOrEmpty(entity.CreateAwb)) { strSQL += " and a.CreateAwb = '" + entity.CreateAwb + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //所属仓库ID
                if (!string.IsNullOrEmpty(entity.CargoPermisID)) { strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")"; }
                //以业务员为查询条件
                if (!string.IsNullOrEmpty(entity.SaleManID)) { strSQL += " and a.SaleManID ='" + entity.SaleManID + "'"; }
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    string res = entity.Specs.ToUpper().Replace("/", "").Replace("R", "").Replace("C", "").Replace("F", "");
                    if (res.Length <= 3)
                    {
                        if (!string.IsNullOrEmpty(res)) { strSQL += " and d.Specs like '%" + res + "%'"; }
                    }
                    if (res.Length > 3 && res.Length <= 5)
                    {
                        strSQL += " and d.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3) + "%'";
                    }
                    if (res.Length > 5)
                    {
                        strSQL += " and (d.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5) + "%' or d.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "RF" + res.Substring(5, res.Length - 5) + "%' or d.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZR" + res.Substring(5, res.Length - 5) + "%')";
                    }
                }
                strSQL += " order by a.CreateDate desc";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据

                            result.Add(new CargoOrderReturnOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                UnitPrice = Convert.ToDecimal(idr["UnitPrice"]),
                                CostPrice = Convert.ToDecimal(idr["CostPrice"]),
                                TradePrice = Convert.ToDecimal(idr["TradePrice"]),
                                SalePrice = Convert.ToDecimal(idr["SalePrice"]),
                                TotalPiece = Convert.ToInt32(idr["TotalPiece"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Model = Convert.ToString(idr["Model"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Batch = Convert.ToString(idr["Batch"]),
                                Born = Convert.ToString(idr["Born"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                FacOrderNo = Convert.ToString(idr["FacOrderNo"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"])
                            });
                            #endregion
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;

        }
        public CargoOrderEntity QueryOesOrderInfoByOrderID(Int64 orderID)
        {
            CargoOrderEntity result = new CargoOrderEntity();
            try
            {
                string strSQL = @"Select * from Tbl_Cargo_PurchaseOrder Where OrderID=@OrderID";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderID", DbType.Int64, orderID);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new CargoOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TransportFee = Convert.ToDecimal(idr["TransportFee"]),
                                HandFee = Convert.ToDecimal(idr["HandFee"]),
                                OtherFee = Convert.ToDecimal(idr["OtherFee"]),
                                CheckOutType = Convert.ToString(idr["CheckOutType"]),
                                TrafficType = Convert.ToString(idr["TrafficType"]),
                                DeliveryType = Convert.ToString(idr["DeliveryType"]),
                                ClientNum = Convert.ToInt32(idr["ClientNum"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                CreateAwbID = Convert.ToString(idr["CreateAwbID"]),
                                CreateAwb = Convert.ToString(idr["CreateAwb"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                AccountNo = Convert.ToString(idr["AccountNo"]),
                                FacOrderNo = Convert.ToString(idr["FacOrderNo"])
                            };
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public CargoContainerGoodsEntity SelectOesrderProductPiece(CargoProductEntity entity)
        {
            CargoContainerGoodsEntity result = new CargoContainerGoodsEntity();
            string strQ = @"Select * from Tbl_Cargo_ContainerGoods a inner join Tbl_Cargo_Product b on a.ProductID=b.ProductID where (1=1)";
            if (!string.IsNullOrEmpty(entity.SourceOrderNo)) { strQ += " and SourceOrderNo=@SourceOrderNo "; }
            if (!string.IsNullOrEmpty(entity.Specs)) { strQ += " and Specs=@Specs "; }
            if (!string.IsNullOrEmpty(entity.Figure)) { strQ += " and Figure=@Figure "; }
            if (!string.IsNullOrEmpty(entity.Model)) { strQ += " and model=@model "; }
            if (!string.IsNullOrEmpty(entity.GoodsCode)) { strQ += " and GoodsCode=@GoodsCode "; }
            if (!string.IsNullOrEmpty(entity.Batch)) { strQ += " and Batch=@Batch "; }
            if (!string.IsNullOrEmpty(entity.LoadIndex)) { strQ += " and LoadIndex=@LoadIndex "; }
            if (!string.IsNullOrEmpty(entity.SpeedLevel)) { strQ += " and SpeedLevel=@SpeedLevel "; }
            if (!string.IsNullOrEmpty(entity.Born)) { strQ += " and Born=@Born "; }
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                if (!string.IsNullOrEmpty(entity.SourceOrderNo))
                {
                    conn.AddInParameter(cmdQ, "@SourceOrderNo", DbType.String, entity.SourceOrderNo);
                }
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    conn.AddInParameter(cmdQ, "@Specs", DbType.String, entity.Specs);
                }
                if (!string.IsNullOrEmpty(entity.Figure))
                {
                    conn.AddInParameter(cmdQ, "@Figure", DbType.String, entity.Figure);
                }
                if (!string.IsNullOrEmpty(entity.Model))
                {
                    conn.AddInParameter(cmdQ, "@Model", DbType.String, entity.Model);
                }
                if (!string.IsNullOrEmpty(entity.GoodsCode))
                {
                    conn.AddInParameter(cmdQ, "@GoodsCode", DbType.String, entity.GoodsCode);
                }
                if (!string.IsNullOrEmpty(entity.Batch))
                {
                    conn.AddInParameter(cmdQ, "@Batch", DbType.String, entity.Batch);
                }
                if (!string.IsNullOrEmpty(entity.LoadIndex))
                {
                    conn.AddInParameter(cmdQ, "@LoadIndex", DbType.String, entity.LoadIndex);
                }
                if (!string.IsNullOrEmpty(entity.SpeedLevel))
                {
                    conn.AddInParameter(cmdQ, "@SpeedLevel", DbType.String, entity.SpeedLevel);
                }
                if (!string.IsNullOrEmpty(entity.Born))
                {
                    conn.AddInParameter(cmdQ, "@Born", DbType.String, entity.Born);
                }
                using (DataTable dd = conn.ExecuteDataTable(cmdQ))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.ID = Convert.ToInt64(idr["ID"]);
                        result.ContainerID = Convert.ToInt32(idr["ContainerID"]);
                        result.TypeID = Convert.ToInt32(idr["TypeID"]);
                        result.ProductID = Convert.ToInt64(idr["ProductID"]);
                        result.InCargoID = Convert.ToString(idr["InCargoID"]);
                        result.Piece = Convert.ToInt32(idr["Piece"]);
                        result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                        result.Specs = Convert.ToString(idr["Specs"]);
                        result.Figure = Convert.ToString(idr["Figure"]);
                        result.Model = Convert.ToString(idr["Model"]);
                    }
                }
            }
            return result;
        }
        #endregion
        #region 缺货列表

        /// <summary>
        /// 查询缺货列表数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryShortageListInfo(int pIndex, int pNum, ShortageListEntity entity)
        {
            List<ShortageListEntity> result = new List<ShortageListEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.*,c.ClientName,h.Name as HouseName from Tbl_Cargo_ShortageList as a inner join Tbl_Cargo_Client c on a.ClientNum=c.ClientNum inner join Tbl_Cargo_House h on a.HouseID=h.HouseID Where (1=1) ";
                string res = entity.Specs.ToUpper().Replace("/", "").Replace("R", "").Replace("C", "").Replace("F", "").Replace("Z", "");
                if (res.Length <= 3)
                {
                    if (!string.IsNullOrEmpty(res)) { strSQL += " and a.Specs like '%" + res + "%'"; }
                }
                if (res.Length > 3 && res.Length <= 5)
                {
                    strSQL += " and (a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3) + "%' or a.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                }
                if (res.Length > 5)
                {
                    strSQL += " and (a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5) + "%' or a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "RF" + res.Substring(5, res.Length - 5) + "%' or a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZR" + res.Substring(5, res.Length - 5) + "%' or a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZRF" + res.Substring(5, res.Length - 5) + "%' or a.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                }
                if (!string.IsNullOrEmpty(entity.Figure)) { strSQL += " and a.Figure like '%" + entity.Figure + "%'"; }
                if (!string.IsNullOrEmpty(entity.Model)) { strSQL += " and a.Model like '%" + entity.Model + "%'"; }
                if (!string.IsNullOrEmpty(entity.GoodsCode)) { strSQL += " and a.GoodsCode like '%" + entity.GoodsCode + "%'"; }
                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID='" + entity.HouseID + "'"; }
                //客户编码
                if (!string.IsNullOrEmpty(entity.ClientNum)) { strSQL += " and a.ClientNum='" + entity.ClientNum + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //件数
                if (!entity.Piece.Equals(0)) { strSQL += " and a.Piece=" + entity.Piece + ""; }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) ";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取数据
                            result.Add(new ShortageListEntity
                            {
                                ShortageID = Convert.ToInt64(idr["ShortageID"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                ClientNum = Convert.ToString(idr["ClientNum"]),
                                ClientName = Convert.ToString(idr["ClientName"]),
                                TypeID = Convert.ToInt32(idr["Piece"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Model = Convert.ToString(idr["Model"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Born = Convert.ToString(idr["Born"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_ShortageList a Where (1=1)";
                if (res.Length <= 3)
                {
                    if (!string.IsNullOrEmpty(res)) { strCount += " and a.Specs like '%" + res + "%'"; }
                }
                if (res.Length > 3 && res.Length <= 5)
                {
                    strCount += " and (a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3) + "%' or a.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                }
                if (res.Length > 5)
                {
                    strCount += " and (a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5) + "%' or a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "RF" + res.Substring(5, res.Length - 5) + "%' or a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZR" + res.Substring(5, res.Length - 5) + "%' or a.Specs like '%" + res.Substring(0, 3) + "/" + res.Substring(3, 2) + "ZRF" + res.Substring(5, res.Length - 5) + "%' or a.Specs like '%" + res.Substring(0, 3) + "R" + res.Substring(3, res.Length - 3) + "%')";
                }
                if (!string.IsNullOrEmpty(entity.Figure)) { strCount += " and a.Figure like '%" + entity.Figure + "%'"; }
                if (!string.IsNullOrEmpty(entity.Model)) { strCount += " and a.Model like '%" + entity.Model + "%'"; }
                if (!string.IsNullOrEmpty(entity.GoodsCode)) { strCount += " and a.GoodsCode like '%" + entity.GoodsCode + "%'"; }
                if (!entity.HouseID.Equals(0)) { strCount += " and a.HouseID='" + entity.HouseID + "'"; }
                //客户编码
                if (!string.IsNullOrEmpty(entity.ClientNum)) { strCount += " and a.ClientNum='" + entity.ClientNum + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //件数
                if (!entity.Piece.Equals(0)) { strCount += " and a.Piece=" + entity.Piece + ""; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
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
        /// 通过客户编码查询缺货列表数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public List<ShortageListEntity> QueryShortageListByClientNum(ShortageListEntity entity)
        {
            List<ShortageListEntity> result = new List<ShortageListEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"Select * from Tbl_Cargo_ShortageList Where 1=1";
                if (!string.IsNullOrEmpty(entity.ClientNum))
                {
                    strSQL += " and ClientNum=@ClientNum";
                }
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    if (!string.IsNullOrEmpty(entity.ClientNum))
                    {
                        conn.AddInParameter(command, "@ClientNum", DbType.String, entity.ClientNum);
                    }
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            ShortageListEntity e = new ShortageListEntity();
                            e.ShortageID = Convert.ToInt64(idr["ShortageID"]);
                            e.HouseID = Convert.ToInt32(idr["HouseID"]);
                            e.ClientNum = Convert.ToString(idr["ClientNum"]);
                            e.TypeID = Convert.ToInt32(idr["Piece"]);
                            e.TypeName = Convert.ToString(idr["TypeName"]);
                            e.Specs = Convert.ToString(idr["Specs"]);
                            e.Figure = Convert.ToString(idr["Figure"]);
                            e.Model = Convert.ToString(idr["Model"]);
                            e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            e.Born = Convert.ToString(idr["Born"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);
                            e.OP_ID = Convert.ToString(idr["OP_ID"]);
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            result.Add(e);
                            #endregion
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 新增缺货数据
        /// </summary>
        /// <param name="entity"></param>
        public void InsertShortageList(ShortageListEntity entity)
        {
            entity.EnSafe();
            //添加订单状态跟踪记录表
            string strSQL = @"INSERT INTO Tbl_Cargo_ShortageList(ClientNum,HouseID,TypeID,TypeName,Specs,Figure,Model,GoodsCode,LoadIndex,SpeedLevel,Born,Piece,OP_ID) VALUES (@ClientNum,@HouseID,@TypeID,@TypeName,@Specs,@Figure,@Model,@GoodsCode,@LoadIndex,@SpeedLevel,@Born,@Piece,@OP_ID)";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                conn.AddInParameter(cmd, "@ClientNum", DbType.String, entity.ClientNum);
                conn.AddInParameter(cmd, "@TypeID", DbType.String, entity.TypeID);
                conn.AddInParameter(cmd, "@TypeName", DbType.String, entity.TypeName);
                conn.AddInParameter(cmd, "@Specs", DbType.String, entity.Specs);
                conn.AddInParameter(cmd, "@Figure", DbType.String, entity.Figure);
                conn.AddInParameter(cmd, "@Model", DbType.String, entity.Model);
                conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                conn.AddInParameter(cmd, "@LoadIndex", DbType.String, entity.LoadIndex);
                conn.AddInParameter(cmd, "@SpeedLevel", DbType.String, entity.SpeedLevel);
                conn.AddInParameter(cmd, "@Born", DbType.String, entity.Born);
                conn.AddInParameter(cmd, "@Piece", DbType.String, entity.Piece);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        public void DeleteShortageList(ShortageListEntity entity)
        {
            string strSQL = "Delete from Tbl_Cargo_ShortageList Where ClientNum=@ClientNum";
            if (!entity.ShortageID.Equals(0)) { strSQL += " and ShortageID=@ShortageID"; }
            if (!entity.HouseID.Equals(0))
            {
                strSQL += " and HouseID=@HouseID";
            }
            if (!string.IsNullOrEmpty(entity.Specs))
            {
                strSQL += " and Specs=@Specs";
            }
            if (!string.IsNullOrEmpty(entity.Figure))
            {
                strSQL += " and Figure=@Figure";
            }
            if (!string.IsNullOrEmpty(entity.Model))
            {
                strSQL += " and Model=@Model";
            }
            if (!string.IsNullOrEmpty(entity.GoodsCode))
            {
                strSQL += " and GoodsCode=@GoodsCode";
            }
            if (!string.IsNullOrEmpty(entity.SpeedLevel))
            {
                strSQL += " and SpeedLevel=@SpeedLevel";
            }
            if (!string.IsNullOrEmpty(entity.LoadIndex))
            {
                strSQL += " and LoadIndex=@LoadIndex";
            }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ClientNum", DbType.String, entity.ClientNum);
                if (!entity.ShortageID.Equals(0))
                {
                    conn.AddInParameter(cmd, "@ShortageID", DbType.Int64, entity.ShortageID);
                }
                if (!entity.HouseID.Equals(0))
                {
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                }
                if (!string.IsNullOrEmpty(entity.Specs))
                {
                    conn.AddInParameter(cmd, "@Specs", DbType.String, entity.Specs);
                }
                if (!string.IsNullOrEmpty(entity.Figure))
                {
                    conn.AddInParameter(cmd, "@Figure", DbType.String, entity.Figure);
                }
                if (!string.IsNullOrEmpty(entity.Model))
                {
                    conn.AddInParameter(cmd, "@Model", DbType.String, entity.Model);
                }
                if (!string.IsNullOrEmpty(entity.GoodsCode))
                {
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                }
                if (!string.IsNullOrEmpty(entity.SpeedLevel))
                {
                    conn.AddInParameter(cmd, "@SpeedLevel", DbType.String, entity.SpeedLevel);
                }
                if (!string.IsNullOrEmpty(entity.LoadIndex))
                {
                    conn.AddInParameter(cmd, "@LoadIndex", DbType.String, entity.LoadIndex);
                }
                conn.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 查詢當天到货入库数据与缺货清单匹配
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<ShortageListEntity> QueryCurrDayArrivalData(ShortageListEntity entity)
        {
            entity.EnSafe();
            List<ShortageListEntity> result = new List<ShortageListEntity>();
            string strSQL = "select a.*,c.ClientShortName,c.ClientName,c.UserID,c.UserName From Tbl_Cargo_ShortageList as a inner join (select distinct b.TypeID,b.GoodsCode,b.Specs,b.Figure,b.SpeedLevel,b.LoadIndex,b.HouseID From Tbl_Cargo_InContainerGoods as a inner join Tbl_Cargo_Product as b on a.ProductID=b.ProductID where b.HouseID in (" + entity.PermissionHouseID + ") and a.InHouseTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "') as b on a.TypeID=b.TypeID and a.Specs=b.Specs and a.Figure=b.Figure and a.GoodsCode=b.GoodsCode and a.LoadIndex=b.LoadIndex and a.SpeedLevel=b.SpeedLevel and a.HouseID=b.HouseID inner join Tbl_Cargo_Client as c on a.ClientNum=c.ClientNum order by c.UserID asc,a.ClientNum asc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        #region 获取运单数据
                        ShortageListEntity e = new ShortageListEntity();
                        e.ShortageID = Convert.ToInt64(idr["ShortageID"]);
                        e.HouseID = Convert.ToInt32(idr["HouseID"]);
                        e.ClientNum = Convert.ToString(idr["ClientNum"]);
                        e.TypeID = Convert.ToInt32(idr["Piece"]);
                        e.TypeName = Convert.ToString(idr["TypeName"]);
                        e.Specs = Convert.ToString(idr["Specs"]);
                        e.Figure = Convert.ToString(idr["Figure"]);
                        e.Model = Convert.ToString(idr["Model"]);
                        e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                        e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                        e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                        e.Born = Convert.ToString(idr["Born"]);
                        e.Piece = Convert.ToInt32(idr["Piece"]);
                        e.OP_ID = Convert.ToString(idr["OP_ID"]);
                        e.ClientName = Convert.ToString(idr["ClientName"]);
                        e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                        e.SaleManID = Convert.ToInt32(idr["UserID"]);
                        e.SaleManName = Convert.ToString(idr["UserName"]);
                        result.Add(e);
                        #endregion
                    }
                }
            }
            return result;
        }
        #endregion
        #region 工厂进货单
        public void AddFactoryPurchaseOrderInfo(CargoFactoryPurchaseOrderEntity entity, List<CargoFactoryPurchaseOrderGoodsEntity> productList)
        {
            CargoClientManager client = new CargoClientManager();
            string OrderID = string.Empty;
            string strSQL = @"INSERT INTO Tbl_Cargo_FactoryPurchaseOrder(OrderNo,Piece,TotalCharge,HouseID,CheckStatus,Remark,OP_ID,OP_DATE) VALUES (@OrderNo,@Piece,@TotalCharge,@HouseID,@CheckStatus,@Remark,@OP_ID,@OP_DATE) SELECT @@IDENTITY";
            try
            {
                #region 订单保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@CheckStatus", DbType.Decimal, entity.CheckStatus);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    OrderID = Convert.ToString(conn.ExecuteScalar(cmd));
                }
                #endregion

                //新增产品与订单关联数据
                AddFactoryPurchaseOrderGoodsInfo(productList, OrderID);

            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        public void AddFactoryPurchaseOrderGoodsInfo(List<CargoFactoryPurchaseOrderGoodsEntity> productList, string OrderID)
        {
            try
            {
                foreach (var it in productList)
                {
                    it.EnSafe();
                    string strSQL = @"INSERT INTO Tbl_Cargo_FactoryPurchaseOrderGoods(OrderID,TypeID,Specs,Figure,Model,GoodsCode,LoadIndex,SpeedLevel,Born,Piece,UnitPrice,WhetherTax,OP_DATE) VALUES  (@OrderID,@TypeID,@Specs,@Figure,@Model,@GoodsCode,@LoadIndex,@SpeedLevel,@Born,@Piece,@UnitPrice,@WhetherTax,@OP_DATE)";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@OrderID", DbType.String, OrderID);
                        conn.AddInParameter(cmd, "@TypeID", DbType.Int32, it.TypeID);
                        conn.AddInParameter(cmd, "@Specs", DbType.String, it.Specs);
                        conn.AddInParameter(cmd, "@Figure", DbType.String, it.Figure);
                        conn.AddInParameter(cmd, "@Model", DbType.String, it.Model);
                        conn.AddInParameter(cmd, "@GoodsCode", DbType.String, it.GoodsCode);
                        conn.AddInParameter(cmd, "@LoadIndex ", DbType.String, it.LoadIndex);
                        conn.AddInParameter(cmd, "@SpeedLevel ", DbType.String, it.SpeedLevel);
                        conn.AddInParameter(cmd, "@Born ", DbType.String, it.Born);
                        conn.AddInParameter(cmd, "@Piece ", DbType.Int32, it.Piece);
                        conn.AddInParameter(cmd, "@UnitPrice", DbType.Decimal, it.UnitPrice);
                        conn.AddInParameter(cmd, "@WhetherTax", DbType.Int32, it.WhetherTax);
                        conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public Hashtable QueryFactoryPurchaseOrderListInfo(int pIndex, int pNum, CargoFactoryPurchaseOrderEntity entity)
        {
            List<CargoFactoryPurchaseOrderEntity> result = new List<CargoFactoryPurchaseOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.*,h.Name as HouseName from Tbl_Cargo_FactoryPurchaseOrder as a inner join Tbl_Cargo_House h on a.HouseID=h.HouseID Where (1=1) ";

                if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID='" + entity.HouseID + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) ";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取数据
                            result.Add(new CargoFactoryPurchaseOrderEntity
                            {
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TotalCharge = Convert.ToDouble(idr["TotalCharge"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                CheckStatus = Convert.ToString(idr["CheckStatus"]),
                                ApplyStatus = Convert.ToString(idr["ApplyStatus"]),
                                CheckResult = Convert.ToString(idr["CheckResult"]),
                                Remark = Convert.ToString(idr["Remark"]),
                                ApplyID = Convert.ToString(idr["ApplyID"]),
                                ApplyName = Convert.ToString(idr["ApplyName"]),
                                ApplyDate = string.IsNullOrEmpty(Convert.ToString(idr["ApplyDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ApplyDate"]),
                                NextCheckID = Convert.ToString(idr["NextCheckID"]),
                                NextCheckName = Convert.ToString(idr["NextCheckName"]),
                                CheckTime = string.IsNullOrEmpty(Convert.ToString(idr["CheckTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckTime"]),
                                OP_ID = Convert.ToString(idr["OP_ID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_FactoryPurchaseOrder a Where (1=1)";
                if (!entity.HouseID.Equals(0)) { strCount += " and a.HouseID='" + entity.HouseID + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //件数
                if (!entity.Piece.Equals(0)) { strCount += " and a.Piece=" + entity.Piece + ""; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        public CargoFactoryPurchaseOrderEntity QueryFactoryPurchaseOrderInfo(CargoFactoryPurchaseOrderEntity entity)
        {
            CargoFactoryPurchaseOrderEntity result = new CargoFactoryPurchaseOrderEntity();
            string strSQL = @"Select a.*,h.Name as HouseName from Tbl_Cargo_FactoryPurchaseOrder a inner join Tbl_Cargo_House h on a.HouseID=h.HouseID where (1=1)";
            if (!entity.OrderID.Equals(0)) { strSQL += " and a.OrderID='" + entity.OrderID + "'"; }
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo='" + entity.OrderNo + "'"; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmdQ))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.OrderID = Convert.ToInt64(idr["OrderID"]);
                        result.OrderNo = Convert.ToString(idr["OrderNo"]);
                        result.Piece = Convert.ToInt32(idr["Piece"]);
                        result.TotalCharge = Convert.ToDouble(idr["TotalCharge"]);
                        result.HouseID = Convert.ToInt32(idr["HouseID"]);
                        result.HouseName = Convert.ToString(idr["HouseName"]);
                        result.CheckStatus = Convert.ToString(idr["CheckStatus"]);
                        result.Remark = Convert.ToString(idr["Remark"]);
                        result.OP_ID = Convert.ToString(idr["OP_ID"]);
                        result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                        result.ApplyID = Convert.ToString(idr["ApplyID"]);
                        result.ApplyName = Convert.ToString(idr["ApplyName"]);
                        result.ApplyDate = string.IsNullOrEmpty(Convert.ToString(idr["ApplyDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["ApplyDate"]);
                        result.NextCheckID = Convert.ToString(idr["NextCheckID"]);
                        result.NextCheckName = Convert.ToString(idr["NextCheckName"]);
                        result.CheckTime = string.IsNullOrEmpty(Convert.ToString(idr["CheckTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckTime"]);
                        result.CheckResult = Convert.ToString(idr["CheckResult"]);
                        result.ApplyStatus = Convert.ToString(idr["ApplyStatus"]);
                    }
                }
            }
            return result;
        }
        public void DeleteFactoryPurchaseOrderInfo(CargoFactoryPurchaseOrderEntity entity)
        {
            string strSQL = @"DELETE FROM Tbl_Cargo_FactoryPurchaseOrder Where OrderID=@OrderID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.ExecuteNonQuery(cmd);
            }
            DeleteFactoryPurchaseOrderGoods(new CargoFactoryPurchaseOrderGoodsEntity { OrderID = entity.OrderID });
        }
        public List<CargoFactoryPurchaseOrderGoodsEntity> QueryFactoryPurchaseOrderGoods(CargoFactoryPurchaseOrderEntity entity)
        {
            List<CargoFactoryPurchaseOrderGoodsEntity> result = new List<CargoFactoryPurchaseOrderGoodsEntity>();
            try
            {
                entity.EnSafe();
                string strSQL = @"SELECT a.*,pt.TypeName FROM Tbl_Cargo_FactoryPurchaseOrderGoods AS a LEFT JOIN Tbl_Cargo_ProductType AS pt ON a.TypeID=pt.TypeID WHERE a.OrderID=@OrderID";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@OrderID", DbType.String, entity.OrderID);

                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取产品数据
                            CargoFactoryPurchaseOrderGoodsEntity e = new CargoFactoryPurchaseOrderGoodsEntity();
                            e.ID = Convert.ToInt32(idr["ID"]);
                            e.OrderID = Convert.ToInt64(idr["OrderID"]);
                            e.TypeID = Convert.ToInt32(idr["TypeID"]);
                            e.TypeName = Convert.ToString(idr["TypeName"]);
                            e.Specs = Convert.ToString(idr["Specs"]);
                            e.Figure = Convert.ToString(idr["Figure"]);
                            e.Model = Convert.ToString(idr["Model"]);
                            e.GoodsCode = Convert.ToString(idr["GoodsCode"]);
                            e.SpeedLevel = Convert.ToString(idr["SpeedLevel"]);
                            e.LoadIndex = Convert.ToString(idr["LoadIndex"]);
                            e.Born = Convert.ToString(idr["Born"]);
                            e.Piece = Convert.ToInt32(idr["Piece"]);
                            e.UnitPrice = Convert.ToDouble(idr["UnitPrice"]);
                            e.WhetherTax = Convert.ToInt32(idr["WhetherTax"]);
                            e.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            result.Add(e);
                            #endregion
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public void AddFactoryPurchaseOrderGoods(CargoFactoryPurchaseOrderGoodsEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"INSERT INTO Tbl_Cargo_FactoryPurchaseOrderGoods(OrderID,TypeID,Specs,Figure,Model,GoodsCode,LoadIndex,SpeedLevel,Born,Piece,UnitPrice,WhetherTax,OP_DATE) VALUES  (@OrderID,@TypeID,@Specs,@Figure,@Model,@GoodsCode,@LoadIndex,@SpeedLevel,@Born,@Piece,@UnitPrice,@WhetherTax,@OP_DATE)";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                    conn.AddInParameter(cmd, "@TypeID", DbType.Int32, entity.TypeID);
                    conn.AddInParameter(cmd, "@Specs", DbType.String, entity.Specs);
                    conn.AddInParameter(cmd, "@Figure", DbType.String, entity.Figure);
                    conn.AddInParameter(cmd, "@Model", DbType.String, entity.Model);
                    conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    conn.AddInParameter(cmd, "@LoadIndex ", DbType.String, entity.LoadIndex);
                    conn.AddInParameter(cmd, "@SpeedLevel ", DbType.String, entity.SpeedLevel);
                    conn.AddInParameter(cmd, "@Born ", DbType.String, entity.Born);
                    conn.AddInParameter(cmd, "@Piece ", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@UnitPrice", DbType.Decimal, entity.UnitPrice);
                    conn.AddInParameter(cmd, "@WhetherTax", DbType.Decimal, entity.WhetherTax);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public void UpdateFactoryPurchaseOrderGoodsPiece(CargoFactoryPurchaseOrderGoodsEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"UPDATE Tbl_Cargo_FactoryPurchaseOrderGoods set Piece=@Piece,OP_DATE=@OP_DATE where OrderID=@OrderID and ID=@ID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int32, entity.ID);
                    conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@OrderID", DbType.String, entity.OrderID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public void DeleteFactoryPurchaseOrderGoods(CargoFactoryPurchaseOrderGoodsEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"DELETE from Tbl_Cargo_FactoryPurchaseOrderGoods where (1=1) and OrderID=@OrderID";
                if (!entity.ID.Equals(0))
                {
                    strSQL += " and ID=@ID";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderID", DbType.String, entity.OrderID);
                    if (!entity.ID.Equals(0))
                    {
                        conn.AddInParameter(cmd, "@ID", DbType.Int32, entity.ID);
                    }
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public void UpdateFactoryPurchaseOrder(CargoFactoryPurchaseOrderEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"UPDATE Tbl_Cargo_FactoryPurchaseOrder set OP_DATE=@OP_DATE";
                if (!entity.Piece.Equals(0))
                {
                    strSQL += ",Piece=Piece+@Piece";
                }
                if (!entity.TotalCharge.Equals(0))
                {
                    strSQL += ",TotalCharge=TotalCharge+@TotalCharge";
                }
                if (!string.IsNullOrEmpty(entity.Remark))
                {
                    strSQL += ",Remark=@Remark";
                }
                if (!string.IsNullOrEmpty(entity.FacOrderNo))
                {
                    strSQL += ",OrderNo=@FacOrderNo";
                }
                strSQL += " where OrderNo=@OrderNo and OrderID=@OrderID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    if (!entity.Piece.Equals(0))
                    {
                        conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                    }
                    if (!entity.TotalCharge.Equals(0))
                    {
                        conn.AddInParameter(cmd, "@TotalCharge", DbType.Decimal, entity.TotalCharge);
                    }
                    if (!string.IsNullOrEmpty(entity.Remark))
                    {
                        conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    }
                    if (!string.IsNullOrEmpty(entity.FacOrderNo))
                    {
                        conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, entity.FacOrderNo);
                    }
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int32, entity.OrderID);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
                if (!string.IsNullOrEmpty(entity.FacOrderNo))
                {
                    UpdateFactoryOrderFacOrderNo(entity.OrderNo, entity.FacOrderNo);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        private void UpdateFactoryOrderFacOrderNo(string OrderNo, string FacOrderNo)
        {
            try
            {
                string strSQL = @"UPDATE Tbl_Cargo_FactoryOrder set OP_DATE=@OP_DATE,FacOrderNo=@FacOrderNo where FacOrderNo=@OrderNo";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, OrderNo);
                    conn.AddInParameter(cmd, "@FacOrderNo", DbType.String, FacOrderNo);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public void SubmitFactoryPurchaseOrderReview(CargoFactoryPurchaseOrderEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"UPDATE Tbl_Cargo_FactoryPurchaseOrder set CheckStatus=@CheckStatus,ApplyID=@ApplyID,ApplyName=@ApplyName,ApplyDate=@ApplyDate,NextCheckID=@NextCheckID,NextCheckName=@NextCheckName,ApplyStatus=@ApplyStatus where OrderNo=@OrderNo and OrderID=@OrderID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CheckStatus", DbType.String, entity.CheckStatus);
                    conn.AddInParameter(cmd, "@ApplyID", DbType.String, entity.ApplyID);
                    conn.AddInParameter(cmd, "@ApplyName", DbType.String, entity.ApplyName);
                    conn.AddInParameter(cmd, "@NextCheckID", DbType.String, entity.NextCheckID);
                    conn.AddInParameter(cmd, "@NextCheckName", DbType.String, entity.NextCheckName);
                    conn.AddInParameter(cmd, "@ApplyStatus", DbType.String, entity.ApplyStatus);
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int32, entity.OrderID);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@ApplyDate", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public void CheckFactoryPurchaseOrder(CargoFactoryPurchaseOrderEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"UPDATE Tbl_Cargo_FactoryPurchaseOrder set CheckStatus=@CheckStatus,NextCheckID=@NextCheckID,NextCheckName=@NextCheckName,ApplyStatus=@ApplyStatus,CheckTime=@CheckTime,CheckResult=@CheckResult";
                strSQL += " where OrderNo=@OrderNo and OrderID=@OrderID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CheckStatus", DbType.String, entity.CheckStatus);
                    conn.AddInParameter(cmd, "@ApplyStatus", DbType.String, entity.ApplyStatus);
                    conn.AddInParameter(cmd, "@NextCheckID", DbType.String, entity.NextCheckID);
                    conn.AddInParameter(cmd, "@NextCheckName", DbType.String, entity.NextCheckName);
                    conn.AddInParameter(cmd, "@CheckResult", DbType.String, entity.CheckResult);
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int32, entity.OrderID);
                    conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                    conn.AddInParameter(cmd, "@CheckTime", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        #endregion
        #region 拣货计划方法集合
        /// <summary>
        /// 查询拣货计划数据列表 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderPickPlanEntity> QueryOrderPickPlanList(CargoOrderPickPlanEntity entity)
        {
            List<CargoOrderPickPlanEntity> result = new List<CargoOrderPickPlanEntity>();
            string strSQL = "select *,h.Name as HouseName From Tbl_Cargo_OrderPickPlan a inner join Tbl_Cargo_House h on a.HouseID=h.HouseID Where (1=1)";
            if (!entity.HouseID.Equals(0)) { strSQL += " and a.HouseID =" + entity.HouseID; }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            if (!string.IsNullOrEmpty(entity.PickPlanNo))
            {
                strSQL += " and a.PickPlanNo like '%" + entity.PickPlanNo + "%'";
            }
            strSQL += " order by a.CreateDate desc";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //List<CargoOrderPickPlanGoodsEntity> OrderPickPlanGoodsList = GetOrderPickPlanGoodsList(Convert.ToString(idr["PickPlanNo"]));
                        #region 获取运单数据
                        result.Add(new CargoOrderPickPlanEntity
                        {
                            HouseID = Convert.ToString(idr["HouseID"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            PickID = Convert.ToInt64(idr["PickID"]),
                            PickPlanNo = Convert.ToString(idr["PickPlanNo"]),
                            TotalNum = Convert.ToInt32(idr["TotalNum"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            Memo = Convert.ToString(idr["Memo"]),
                            PickStatus = Convert.ToString(idr["PickStatus"]),
                            PickType = Convert.ToString(idr["PickType"]),
                            OP_ID = Convert.ToString(idr["OP_ID"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            //PickPlanGoodsList = OrderPickPlanGoodsList
                        });
                        #endregion
                    }
                }
            }
            return result;
        }
        public CargoOrderPickPlanEntity QueryOrderPickPlanInfo(string PickPlanNo)
        {
            CargoOrderPickPlanEntity result = new CargoOrderPickPlanEntity();
            string strSQL = "select opp.*,h.Name as HouseName From Tbl_Cargo_OrderPickPlan opp inner join Tbl_Cargo_House h on opp.HouseID=h.HouseID where opp.PickPlanNo =@PickPlanNo ";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@PickPlanNo", DbType.String, PickPlanNo);
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result = new CargoOrderPickPlanEntity
                        {
                            PickID = Convert.ToInt64(idr["PickID"]),
                            PickPlanNo = Convert.ToString(idr["PickPlanNo"]),
                            HouseID = Convert.ToString(idr["HouseID"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            TotalNum = Convert.ToInt32(idr["TotalNum"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                            Memo = Convert.ToString(idr["Memo"]),
                            PickStatus = Convert.ToString(idr["PickStatus"]),
                            PickType = Convert.ToString(idr["PickType"]),
                            OP_ID = Convert.ToString(idr["OP_ID"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                        };
                    }
                }
            }
            return result;
        }
        public List<CargoOrderPickPlanGoodsEntity> GetOrderPickPlanGoodsList(string PickPlanNo)
        {
            List<CargoOrderPickPlanGoodsEntity> result = new List<CargoOrderPickPlanGoodsEntity>();
            try
            {
                string strSQL = @"select opg.*,p.ProductName from tbl_cargo_OrderPickPlanGoods opg inner join Tbl_Cargo_Product p on opg.ProductID=p.ProductID where opg.PickPlanNo=@PickPlanNo";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@PickPlanNo", DbType.String, PickPlanNo);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CargoOrderPickPlanGoodsEntity
                            {
                                PickGoodsID = Convert.ToInt32(idr["PickGoodsID"]),
                                PickPlanNo = Convert.ToString(idr["PickPlanNo"]),
                                ProductID = Convert.ToString(idr["ProductID"]),
                                ProductName = Convert.ToString(idr["ProductName"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                HouseID = Convert.ToString(idr["HouseID"]),
                                AreaID = Convert.ToString(idr["AreaID"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Piece = Convert.ToString(idr["Piece"]),
                                ScanPiece = string.IsNullOrEmpty(Convert.ToString(idr["ScanPiece"])) ? 0 : Convert.ToInt32(idr["ScanPiece"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                ScanStatus = Convert.ToString(idr["ScanStatus"]),
                                AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                PitNum = string.IsNullOrEmpty(Convert.ToString(idr["PitNum"])) ? 0 : Convert.ToInt32(idr["PitNum"]),
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public List<CargoOrderPickPlanGoodsEntity> GetOrderPickPlanGoodsInfoList(CargoOrderPickPlanGoodsEntity entity)
        {
            List<CargoOrderPickPlanGoodsEntity> result = new List<CargoOrderPickPlanGoodsEntity>();
            try
            {
                string strSQL = @"Select * From tbl_cargo_OrderPickPlanGoods Where PickPlanNo=@PickPlanNo ";
                if (!string.IsNullOrEmpty(entity.ContainerCode))
                {
                    strSQL += " and ContainerCode=@ContainerCode";
                }
                if (!string.IsNullOrEmpty(entity.GoodsCode))
                {
                    strSQL += " and GoodsCode=@GoodsCode";
                }
                if (!string.IsNullOrEmpty(entity.HouseID))
                {
                    strSQL += " and HouseID=@HouseID";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                    if (!string.IsNullOrEmpty(entity.ContainerCode))
                    {
                        conn.AddInParameter(cmd, "@ContainerCode", DbType.String, entity.ContainerCode);
                    }
                    if (!string.IsNullOrEmpty(entity.GoodsCode))
                    {
                        conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                    }
                    if (!string.IsNullOrEmpty(entity.HouseID))
                    {
                        conn.AddInParameter(cmd, "@HouseID", DbType.String, entity.HouseID);
                    }
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CargoOrderPickPlanGoodsEntity
                            {
                                PickGoodsID = Convert.ToInt32(idr["PickGoodsID"]),
                                PickPlanNo = Convert.ToString(idr["PickPlanNo"]),
                                ProductID = Convert.ToString(idr["ProductID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                HouseID = Convert.ToString(idr["HouseID"]),
                                AreaID = Convert.ToString(idr["AreaID"]),
                                ContainerCode = Convert.ToString(idr["ContainerCode"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                Piece = Convert.ToString(idr["Piece"]),
                                LogisAwbNo = Convert.ToString(idr["LogisAwbNo"]),
                                Dep = Convert.ToString(idr["Dep"]),
                                Dest = Convert.ToString(idr["Dest"]),
                                ScanStatus = Convert.ToString(idr["ScanStatus"]),
                                AcceptUnit = GetSpellCode(Convert.ToString(idr["AcceptUnit"])).ToUpper(),
                                AcceptUnitCN = Convert.ToString(idr["AcceptUnit"]),
                                AcceptAddress = Convert.ToString(idr["AcceptAddress"]),
                                AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                                AcceptTelephone = Convert.ToString(idr["AcceptTelephone"]),
                                AcceptCellphone = Convert.ToString(idr["AcceptCellphone"]),
                                ScanPiece = string.IsNullOrEmpty(idr["ScanPiece"].ToString()) ? 0 : Convert.ToInt32(idr["ScanPiece"]),
                                PitNum = string.IsNullOrEmpty(Convert.ToString(idr["PitNum"])) ? 0 : Convert.ToInt32(idr["PitNum"])
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
        public bool GetOrderPickPlanGoodsScanNum(CargoOrderPickPlanGoodsEntity entity)
        {
            bool result = false;
            string strSQL = @"Select ISNULL(SUM(ScanPiece),0)ScanPiece,SUM(Piece)Piece From tbl_cargo_OrderPickPlanGoods Where PickPlanNo=@PickPlanNo and OrderNo=@OrderNo ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    if (dd.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dd.Rows[0]["ScanPiece"]) + 1 >= Convert.ToInt32(dd.Rows[0]["Piece"]);
                    }
                }
            }
            return result;
        }
        public void UpdateOrderPickPlanGoodsScanStatus(CargoOrderPickPlanGoodsEntity entity)
        {
            string strSQL = "update Tbl_Cargo_OrderPickPlanGoods set ScanStatus=@ScanStatus,ScanPiece=@ScanPiece,ScanUserID=@ScanUserID where PickPlanNo=@PickPlanNo and ContainerCode=@ContainerCode and GoodsCode=@GoodsCode and ProductID=@ProductID";
            if (!string.IsNullOrEmpty(entity.OrderNo))
            {
                strSQL += " and OrderNo=@OrderNo";
            }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@ScanStatus", DbType.String, entity.ScanStatus);
                conn.AddInParameter(command, "@ScanPiece", DbType.String, entity.ScanPiece);
                conn.AddInParameter(command, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                conn.AddInParameter(command, "@ContainerCode", DbType.String, entity.ContainerCode);
                conn.AddInParameter(command, "@GoodsCode", DbType.String, entity.GoodsCode);
                conn.AddInParameter(command, "@ScanUserID", DbType.String, entity.OP_ID);
                conn.AddInParameter(command, "@ProductID", DbType.String, entity.ProductID);
                if (!string.IsNullOrEmpty(entity.OrderNo))
                {
                    conn.AddInParameter(command, "@OrderNo", DbType.String, entity.OrderNo);
                }
                conn.ExecuteNonQuery(command);
            }
            strSQL = "select * from Tbl_Cargo_OrderPickPlan opp inner join tbl_cargo_OrderPickPlanGoods oppg on opp.PickPlanNo=oppg.PickPlanNo where opp.PickPlanNo=@PickPlanNo and (oppg.ScanStatus=0 or oppg.ScanStatus=2)";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                using (DataTable dd = conn.ExecuteDataTable(command))
                {
                    if (dd.Rows.Count <= 0)
                    {
                        UpdateOrderPickPlanPickStatus(new CargoOrderPickPlanEntity { PickPlanNo = entity.PickPlanNo, HouseID = entity.HouseID, PickStatus = "2" });
                    }
                    else
                    {
                        UpdateOrderPickPlanPickStatus(new CargoOrderPickPlanEntity { PickPlanNo = entity.PickPlanNo, HouseID = entity.HouseID, PickStatus = "1" });
                    }
                }
            }
        }
        public void UpdateOrderPickPlanPickStatus(CargoOrderPickPlanEntity entity)
        {
            string strSQL = "update Tbl_Cargo_OrderPickPlan set PickStatus=@PickStatus where PickPlanNo=@PickPlanNo and HouseID=@HouseID";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@PickStatus", DbType.String, entity.PickStatus);
                conn.AddInParameter(command, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                conn.AddInParameter(command, "@HouseID", DbType.String, entity.HouseID);
                conn.ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 查询当前日期当前仓库表中最大顺序号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetMaxPickNumByCurrentDate(CargoOrderEntity entity)
        {
            int result = 0;
            try
            {
                string strSQL = @"select ISNULL(MAX(PickNum),0) as PickNum from Tbl_Cargo_OrderPickPlan where HouseID=@HouseID ";
                strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                strSQL += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@HouseID", DbType.Int32, entity.HouseID);
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        if (dd.Rows.Count > 0) { result = Convert.ToInt32(dd.Rows[0]["PickNum"]); }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        public void AddOrderPickPlan(CargoOrderPickPlanEntity entity)
        {
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_OrderPickPlan(PickNum,PickPlanNo,HouseID,TotalNum,Memo,OP_ID,PickType) VALUES (@PickNum,@PickPlanNo,@HouseID,@TotalNum,@Memo,@OP_ID,@PickType) SELECT @@IDENTITY";
            try
            {
                #region 拣货计划保存
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@PickNum", DbType.Int32, entity.PickNum);
                    conn.AddInParameter(cmd, "@PickPlanNo", DbType.String, entity.PickPlanNo);
                    conn.AddInParameter(cmd, "@HouseID", DbType.String, entity.HouseID);
                    conn.AddInParameter(cmd, "@TotalNum", DbType.Int32, entity.TotalNum);
                    conn.AddInParameter(cmd, "@Memo", DbType.String, entity.Memo);
                    conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                    //conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@PickType", DbType.Int32, entity.PickType);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                #endregion

                AddOrderPickPlanGoods(entity.PickPlanGoodsList);
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void AddOrderPickPlanGoods(List<CargoOrderPickPlanGoodsEntity> PickPlanGoodsList)
        {
            try
            {
                string orderNoStr = "";
                string strSQL = string.Empty;
                foreach (var it in PickPlanGoodsList)
                {
                    orderNoStr += "'" + it.OrderNo + "',";
                    it.EnSafe();
                    strSQL = @"INSERT INTO Tbl_Cargo_OrderPickPlanGoods(PickPlanNo,OrderNo,ProductID,HouseID,AreaID,ContainerCode,GoodsCode,Piece,OP_ID,LogisAwbNo,Dep,Dest,AcceptUnit,AcceptAddress,AcceptPeople,AcceptTelephone,AcceptCellphone,PitNum) VALUES  (@PickPlanNo,@OrderNo,@ProductID,@HouseID,@AreaID,@ContainerCode,@GoodsCode,@Piece,@OP_ID,@LogisAwbNo,@Dep,@Dest,@AcceptUnit,@AcceptAddress,@AcceptPeople,@AcceptTelephone,@AcceptCellphone,@PitNum)";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@PickPlanNo", DbType.String, it.PickPlanNo.ToUpper());
                        conn.AddInParameter(cmd, "@OrderNo", DbType.String, it.OrderNo);
                        conn.AddInParameter(cmd, "@ProductID", DbType.Int64, it.ProductID);
                        conn.AddInParameter(cmd, "@HouseID", DbType.Int32, it.HouseID);
                        conn.AddInParameter(cmd, "@AreaID", DbType.Int32, it.AreaID);
                        conn.AddInParameter(cmd, "@ContainerCode", DbType.String, it.ContainerCode);
                        conn.AddInParameter(cmd, "@GoodsCode", DbType.String, it.GoodsCode);
                        conn.AddInParameter(cmd, "@Piece", DbType.Int32, it.Piece);
                        conn.AddInParameter(cmd, "@OP_ID", DbType.String, it.OP_ID);
                        conn.AddInParameter(cmd, "@LogisAwbNo", DbType.String, it.LogisAwbNo);
                        conn.AddInParameter(cmd, "@Dep", DbType.String, it.Dep);
                        conn.AddInParameter(cmd, "@Dest", DbType.String, it.Dest);
                        conn.AddInParameter(cmd, "@AcceptUnit", DbType.String, it.AcceptUnit);
                        conn.AddInParameter(cmd, "@AcceptAddress", DbType.String, it.AcceptAddress);
                        conn.AddInParameter(cmd, "@AcceptPeople", DbType.String, it.AcceptPeople);
                        conn.AddInParameter(cmd, "@AcceptTelephone", DbType.String, it.AcceptTelephone);
                        conn.AddInParameter(cmd, "@AcceptCellphone", DbType.String, it.AcceptCellphone);
                        conn.AddInParameter(cmd, "@PitNum", DbType.Int32, it.PitNum);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
                orderNoStr = orderNoStr.Substring(0, orderNoStr.Length - 1);
                strSQL = "Update Tbl_Cargo_Order set PickStatus=1 where OrderNo in (" + orderNoStr + ")";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        #endregion
        #region 获取字符串拼音

        /// <summary>
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串
        /// </summary>
        /// <param name="CnStr">汉字字符串</param>
        /// <returns>相对应的汉语拼音首字母串</returns>
        public static string GetSpellCode(string CnStr)
        {
            string strTemp = "";
            for (int i = 0; i < CnStr.Length; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }
            return strTemp;
        }
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="str">被截取的字符串</param>
        /// <param name="len">所截取的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int len)
        {
            if (str == null || str.Length == 0 || len <= 0)
            {
                return string.Empty;
            }
            int l = str.Length;
            #region 计算长度
            int clen = 0;
            while (clen < len && clen < l)
            {
                //每遇到一个中文，则将目标长度减一。
                if ((int)str[clen] > 128) { len--; }
                clen++;
            }
            #endregion

            if (clen < l)
            {
                return str.Substring(0, clen) + "...";
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="CnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>
        private static string GetCharSpellCode(string CnChar)
        {

            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回首字母

            if (ZW.Length == 1)
            {

                return CutString(CnChar.ToUpper(), 1);

            }
            else
            {

                // get the array of byte from the single char

                int i1 = (short)(ZW[0]);

                int i2 = (short)(ZW[1]);

                iCnChar = i1 * 256 + i2;

            }

            // iCnChar match the constant

            if ((iCnChar >= 45217) && (iCnChar <= 45252)) { return "a"; }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760)) { return "B"; }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {

                return "C";

            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {

                return "D";

            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {

                return "E";

            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {

                return "F";

            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {

                return "G";

            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {

                return "H";

            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {

                return "J";

            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {

                return "K";

            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {

                return "L";

            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {

                return "M";

            }
            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {

                return "N";

            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {

                return "O";

            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {

                return "P";

            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {

                return "Q";

            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {

                return "R";

            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {

                return "S";

            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {

                return "T";

            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {

                return "W";

            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {

                return "X";

            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {

                return "Y";

            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {

                return "Z";

            }
            else

                return ("?");

        }
        #endregion

        #region 捆包管理方法集合
        /// <summary>
        /// 获取当天捆包单的最大序号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public int GetMaxBundleOrderNumByCurrentDate(CargoOrderBundleEntity entity)
        {
            int result = 0;
            try
            {
                string strSQL = @"select ISNULL(MAX(OrderNum),0) as OrderNum from tbl_Cargo_bundle where HouseID=@HouseID ";
                strSQL += " and CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                strSQL += " and CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@HouseID", DbType.Int32, entity.HouseID);
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        if (dd.Rows.Count > 0) { result = Convert.ToInt32(dd.Rows[0]["OrderNum"]); }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 新增捆包单
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ApplicationException"></exception>
        public void AddBundleOrder(CargoOrderBundleEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_Cargo_Bundle(BundleNo,OrderNum,HouseID,TotalNum,Memo,OP_ID,BundleStatus,BundleType) VALUES (@BundleNo,@OrderNum,@HouseID,@TotalNum,@Memo,@OP_ID,@BundleStatus,@BundleType)";

            entity.EnSafe();
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, entity.OrderNum);
                conn.AddInParameter(cmd, "@BundleNo", DbType.String, entity.BundleNo);
                conn.AddInParameter(cmd, "@HouseID", DbType.String, entity.HouseID);
                conn.AddInParameter(cmd, "@TotalNum", DbType.Int32, entity.TotalNum);
                conn.AddInParameter(cmd, "@Memo", DbType.String, entity.Memo);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.AddInParameter(cmd, "@BundleStatus", DbType.String, entity.BundleStatus);
                conn.AddInParameter(cmd, "@BundleType", DbType.String, entity.BundleType);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询捆包单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CargoOrderBundleEntity QueryBundleOrderEntity(CargoOrderBundleEntity entity)
        {
            CargoOrderBundleEntity result = new CargoOrderBundleEntity();

            string strSQL = "Select * from tbl_Cargo_bundle where (1=1)";
            if (!string.IsNullOrEmpty(entity.BundleNo)) { strSQL += " and BundleNo='" + entity.BundleNo + "'"; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.BundleNo = Convert.ToString(idr["BundleNo"]);
                        result.BunID = Convert.ToInt64(idr["BunID"]);
                        result.HouseID = Convert.ToInt32(idr["HouseID"]);
                        result.TotalNum = Convert.ToInt32(idr["TotalNum"]);
                        result.CreateDate = Convert.ToDateTime(idr["CreateDate"]);
                        result.BundleStatus = Convert.ToString(idr["BundleStatus"]);
                        result.BundleType = Convert.ToString(idr["BundleType"]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 判断捆包明细是否已经存在零件 true：存在，False：不存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsExistBundleOrderGoods(CargoOrderBundleEntity entity)
        {
            string strSQL = "Select BundleNo from tbl_Cargo_bundleGoods where BundleNo=@BundleNo and GoodsCode=@GoodsCode";

            using (DbCommand cmdQ = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmdQ, "@BundleNo", DbType.String, entity.BundleNo);
                conn.AddInParameter(cmdQ, "@GoodsCode", DbType.String, entity.GoodsCode);

                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 更新捆包明细数量 
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateBundleOrderGoodsPiece(CargoOrderBundleEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE tbl_Cargo_bundleGoods SET Piece=Piece+@Piece,OP_DATE=@OP_DATE,OP_ID=@OP_ID Where BundleNo=@BundleNo and GoodsCode=@GoodsCode";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                conn.AddInParameter(cmd, "@BundleNo", DbType.String, entity.BundleNo);
                conn.AddInParameter(cmd, "@GoodsCode", DbType.String, entity.GoodsCode);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 修改捆包单状态和数量
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateBundleOrderStatus(CargoOrderBundleEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE tbl_Cargo_bundle SET TotalNum=TotalNum+@Piece,BundleStatus=@BundleStatus,OP_DATE=@OP_DATE,OP_ID=@OP_ID Where BundleNo=@BundleNo";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@Piece", DbType.Int32, entity.Piece);
                conn.AddInParameter(cmd, "@BundleNo", DbType.String, entity.BundleNo);
                conn.AddInParameter(cmd, "@OP_ID", DbType.String, entity.OP_ID);
                conn.AddInParameter(cmd, "@BundleStatus", DbType.String, entity.BundleStatus);
                conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                conn.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 外部订单分配清单操作方法
        /// <summary>
        /// 查询外部订单分配清单数据方法
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public Hashtable QueryExterOrderAlloList(int pIndex, int pNum, CargoExterOrderAlloEntity entity)
        {
            List<CargoExterOrderAlloEntity> result = new List<CargoExterOrderAlloEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                #region 获取订单明细
                string strSQL = @" SELECT TOP " + pNum + "* FROM (SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY a.OPDATE DESC) AS RowNumber,a.* FROM Tbl_Cargo_ExterOrderAllo AS a WHERE (1=1)";
                if (!entity.ExterOrderAlloNum.Equals(0)) { strSQL += " and a.ExterOrderAlloNum =" + entity.ExterOrderAlloNum; }
                if (!string.IsNullOrEmpty(entity.AcceptName)) { strSQL += " and a.AcceptName like '%" + entity.AcceptName + "%'"; }
                if (!string.IsNullOrEmpty(entity.OrderAlloType)) { strSQL += " and a.OrderAlloType ='" + entity.OrderAlloType + "'"; }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OPDATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.OPDATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += ") as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))  order by RowNumber";
                #endregion

                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    #region 获取数据
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CargoExterOrderAlloEntity
                            {
                                OrderAlloType = Convert.ToString(idr["OrderAlloType"]),
                                ExterOrderAlloNum = Convert.ToInt32(idr["ExterOrderAlloNum"]),
                                AcceptName = Convert.ToString(idr["AcceptName"]),
                                AcceptLoginName = Convert.ToString(idr["AcceptLoginName"]),
                                AcceptDATE = string.IsNullOrEmpty(Convert.ToString(idr["AcceptDATE"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["AcceptDATE"]),
                                AlloPiece = Convert.ToInt32(idr["AlloPiece"]),
                                OPName = Convert.ToString(idr["OPName"]),
                                OPLoginName = Convert.ToString(idr["OPLoginName"]),
                                OPDATE = string.IsNullOrEmpty(Convert.ToString(idr["OPDATE"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["OPDATE"]),
                            });
                        }
                    }
                    #endregion
                }
                resHT["rows"] = result;
                #region 获取总数
                string strCount = @"Select Count(*) as TotalCount from Tbl_Cargo_ExterOrderAllo as a Where (1=1)";
                if (!entity.ExterOrderAlloNum.Equals(0)) { strCount += " and a.ExterOrderAlloNum =" + entity.ExterOrderAlloNum; }
                if (!string.IsNullOrEmpty(entity.AcceptName)) { strCount += " and a.AcceptName like '%" + entity.AcceptName + "%'"; }
                if (!string.IsNullOrEmpty(entity.OrderAlloType)) { strCount += " and a.OrderAlloType ='" + entity.OrderAlloType + "'"; }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OPDATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.OPDATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }

        /// <summary>
        /// 新增外部订单分配清单
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ApplicationException"></exception>
        public void AddExterOrderAllo(CargoExterOrderAlloEntity entity)
        {
            entity.EnSafe();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_Cargo_ExterOrderAllo(OrderAlloType, AlloPiece, OPLoginName, OPName, OPDATE) VALUES (@OrderAlloType, @AlloPiece,@OPLoginName, @OPName, @OPDATE) SELECT @@IDENTITY;";
            try
            {
                entity.EnSafe();
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OrderAlloType", DbType.Int32, Convert.ToInt32(entity.OrderAlloType));
                    conn.AddInParameter(cmd, "@AlloPiece", DbType.Int32, entity.AlloPiece);
                    conn.AddInParameter(cmd, "@OPLoginName", DbType.String, entity.OPLoginName);
                    conn.AddInParameter(cmd, "@OPName", DbType.String, entity.OPName);
                    conn.AddInParameter(cmd, "@OPDATE", DbType.DateTime, DateTime.Now);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }

                //修改外部订单
                if (entity.TID.Count() > 0)
                {
                    UpdateCargoFactory(new CargoGtmcProOrderEntity()
                    {
                        ID = string.Join(",", entity.TID),
                        ExterOrderAlloNum = Convert.ToInt32(did)
                    });
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }



        }

        ///批量修改外部订单
        public void UpdateCargoFactory(CargoGtmcProOrderEntity entity)
        {
            string strSQL = @"UPDATE Tbl_GTMC_ProOrder SET ExterOrderAlloNum=@ExterOrderAlloNum WHERE TID in (" + entity.ID + ");";

            entity.EnSafe();
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ExterOrderAlloNum", DbType.Int32, entity.ExterOrderAlloNum);
                //conn.AddInParameter(cmd, "@TID", DbType.String, entity.ID);
                conn.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 修改外部订单分配清单
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateExterOrderAllo(CargoExterOrderAlloEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_Cargo_ExterOrderAllo SET OPLoginName=@OPLoginName,OPName=@OPName,OPDATE=@OPDATE";
            //正数增加
            if (entity.AlloPiece > 0)
            {
                strSQL += " ,AlloPiece=AlloPiece+@AlloPiece";
            }
            //负数减少
            if (entity.AlloPiece < 0)
            {
                strSQL += " ,AlloPiece=AlloPiece-@AlloPiece";
            }

            strSQL += @" Where ExterOrderAlloNum=@ExterOrderAlloNum";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                if (!entity.AlloPiece.Equals(0))
                {

                    conn.AddInParameter(cmd, "@AlloPiece", DbType.Int32, Math.Abs(entity.AlloPiece));
                }

                conn.AddInParameter(cmd, "@OPLoginName", DbType.String, entity.OPLoginName);
                conn.AddInParameter(cmd, "@OPName", DbType.String, entity.OPName);
                conn.AddInParameter(cmd, "@OPDATE", DbType.DateTime, DateTime.Now);
                conn.AddInParameter(cmd, "@ExterOrderAlloNum", DbType.Int32, entity.ExterOrderAlloNum);
                conn.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 次日达数据操作方法集合

        public Hashtable QueryNextDayOrderInfo(int pIndex, int pNum, WXOrderEntity entity)
        {
            List<WXOrderEntity> result = new List<WXOrderEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                entity.EnSafe();
                #region 组装查询SQL语句
                string strSQL = @" SELECT TOP " + pNum + " *  FROM ";
                strSQL += "(SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY a.CreateDate DESC) AS RowNumber,a.*,b.Name as HouseName,b.Cellphone as HouseCellphone,c.CompanyName from Tbl_WX_Order as a inner join tbl_Cargo_house as b on a.HouseID=b.HouseID inner join Tbl_WX_Client as c on a.WXID=c.ID Where (1=1) ";

                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //订单类型
                if (!string.IsNullOrEmpty(entity.OrderType)) { strSQL += " and a.OrderType='" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood)) { strSQL += " and a.ThrowGood='" + entity.ThrowGood + "'"; }
                if (!string.IsNullOrEmpty(entity.OrderStatus)) { strSQL += " and a.OrderStatus='" + entity.OrderStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.PayStatus)) { strSQL += " and a.PayStatus='" + entity.PayStatus + "'"; }
                ////客户名称
                if (!string.IsNullOrEmpty(entity.Name)) { strSQL += " and a.Name like '%" + entity.Name + "%'"; }
                //送货方式
                if (!entity.SuppClientNum.Equals(0)) { strSQL += " and a.SuppClientNum=" + entity.SuppClientNum; }

                strSQL += " ) A ";
                strSQL += " WHERE RowNumber > (" + pNum + "* (" + pIndex + "-1)) order by A.RowNumber";
                #endregion
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            #region 获取运单数据
                            result.Add(new WXOrderEntity
                            {
                                ID = Convert.ToInt64(idr["ID"]),
                                OrderNo = Convert.ToString(idr["OrderNo"]),
                                WXID = Convert.ToInt64(idr["WXID"]),
                                Piece = Convert.ToInt32(idr["Piece"]),
                                TransitFee = Convert.ToDecimal(idr["TransitFee"]),
                                TotalCharge = Convert.ToDecimal(idr["TotalCharge"]),
                                CreateDate = Convert.ToDateTime(idr["CreateDate"]),
                                PayStatus = Convert.ToString(idr["PayStatus"]),
                                OrderStatus = Convert.ToString(idr["OrderStatus"]),
                                Province = Convert.ToString(idr["Province"]),
                                City = Convert.ToString(idr["City"]),
                                Country = Convert.ToString(idr["Country"]),
                                Address = Convert.ToString(idr["Address"]),
                                Name = Convert.ToString(idr["Name"]),
                                CompanyName = Convert.ToString(idr["CompanyName"]),
                                Cellphone = Convert.ToString(idr["Cellphone"]),
                                HouseCellphone = Convert.ToString(idr["HouseCellphone"]),
                                Memo = Convert.ToString(idr["Memo"]),
                                LogisID = Convert.ToInt32(idr["LogisID"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                OrderType = Convert.ToString(idr["OrderType"]),

                                PayWay = Convert.ToString(idr["PayWay"]),
                                WXPayOrderNo = Convert.ToString(idr["WXPayOrderNo"]),
                                ThrowGood = Convert.ToString(idr["ThrowGood"]),

                                SuppClientNum = Convert.ToInt32(idr["SuppClientNum"]),

                                RefundDate = string.IsNullOrEmpty(Convert.ToString(idr["RefundDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["RefundDate"]),
                                RefundMemo = Convert.ToString(idr["RefundMemo"]),
                                RefundReason = Convert.ToString(idr["RefundReason"]),

                                RefundCheckID = Convert.ToString(idr["RefundCheckID"]),
                                RefundCheckName = Convert.ToString(idr["RefundCheckName"]),
                                RefundCheckDate = string.IsNullOrEmpty(Convert.ToString(idr["RefundCheckDate"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["RefundCheckDate"]),
                                RefundCheckStatus = Convert.ToString(idr["RefundCheckStatus"]),
                                //ReturnOrderNO = Convert.ToString(idr["ReturnOrderNO"]),
                                Trxid = Convert.ToString(idr["Trxid"]),

                            });
                            #endregion
                        }
                    }
                }
                resHT["rows"] = result;
                #region 总数目
                string strCount = @"Select Count(*) as TotalCount from Tbl_WX_Order as a Where (1=1)";


                //订单编号 
                if (!string.IsNullOrEmpty(entity.OrderNo)) { strCount += " and a.OrderNo = '" + entity.OrderNo.ToUpper() + "'"; }
                //制单日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and a.CreateDate<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                //订单类型
                if (!string.IsNullOrEmpty(entity.OrderType)) { strCount += " and a.OrderType='" + entity.OrderType + "'"; }
                if (!string.IsNullOrEmpty(entity.ThrowGood)) { strCount += " and a.ThrowGood='" + entity.ThrowGood + "'"; }
                if (!string.IsNullOrEmpty(entity.OrderStatus)) { strCount += " and a.OrderStatus='" + entity.OrderStatus + "'"; }
                if (!string.IsNullOrEmpty(entity.PayStatus)) { strCount += " and a.PayStatus='" + entity.PayStatus + "'"; }
                ////客户名称
                if (!string.IsNullOrEmpty(entity.Name)) { strCount += " and a.Name like '%" + entity.Name + "%'"; }
                //送货方式
                if (!entity.SuppClientNum.Equals(0)) { strCount += " and a.SuppClientNum=" + entity.SuppClientNum; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0) { resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]); }
                    }
                }
                #endregion
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

            return resHT;

        }

        public List<CargoProductShelvesEntity> QueryNextDayOrderGoodsList(WXOrderEntity entity)
        {
            List<CargoProductShelvesEntity> result = new List<CargoProductShelvesEntity>();
            string strSQL = "select a.OrderID,a.OrderNum,a.OrderPrice,a.ProductCode,a.Batch,a.BatchYear,b.Specs,b.Figure,b.GoodsCode,b.LoadIndex,b.SpeedLevel,c.TypeName from Tbl_WX_OrderProduct as a inner join Tbl_Cargo_ProductSpec as b on a.ProductCode=b.ProductCode inner join Tbl_Cargo_ProductType as c on b.TypeID=c.TypeID where a.OrderID=@OrderID";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(command, "@OrderID", DbType.String, entity.OrderID);

                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.Add(new CargoProductShelvesEntity
                        {
                            OrderID = Convert.ToInt64(idr["OrderID"]),
                            OrderNum = Convert.ToInt32(idr["OrderNum"]),
                            OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                            ProductCode = Convert.ToString(idr["ProductCode"]),
                            Batch = Convert.ToString(idr["Batch"]),
                            BatchYear = Convert.ToInt32(idr["BatchYear"]),
                            Specs = Convert.ToString(idr["Specs"]),
                            Figure = Convert.ToString(idr["Figure"]),
                            GoodsCode = Convert.ToString(idr["GoodsCode"]),
                            LoadIndex = Convert.ToString(idr["LoadIndex"]),
                            SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                        });
                    }
                }

            }
            return result;
        }
        #endregion

        #region 

        public void InsertDayWxOrder(string st, string et, string opid = null)
        {
            var result = new List<CargoOrderDayStatisticsEntity>();
            try
            {
                /*
                 
                           --先修改覆盖现有数据
                     update a3 set Piece=a4.Piece ,TotalCharge=a4.TotalCharge
                     from Tbl_Cargo_OrderDayStatistics as a3
                     inner join (
                        select * from (
 select TypeID,HouseID,Piece,TotalCharge,throwgood,OrderModel,Year,Month,Day,opid,DayStatisticsTime,SuppClientNum,ProductCode from (
 select orderGoods.RelateOrderNo, h.TypeID,e.HouseID,sum(orderGoods.Piece) Piece,sum(orders.TotalCharge) TotalCharge,(case when orders.OrderModel=1 then (select top 1 ThrowGood from Tbl_Cargo_Order where OrderNo=orderGoods.RelateOrderNo) else orders.throwgood end) throwgood,orders.OrderModel
                        ,datename(YYYY,(convert(varchar(10),orders.CreateDate,23)))as Year,datename(month,(convert(varchar(10),orders.CreateDate,23))) as Month,datename(day ,(convert(varchar(10),orders.CreateDate,23))) as Day
                             ,'' as opid,(convert(varchar(10),orders.CreateDate,23)) DayStatisticsTime,orders.SuppClientNum,ProductCode
                        from Tbl_Cargo_Order orders
                                 inner join Tbl_Cargo_OrderGoods orderGoods
                                            on orders.OrderNo = orderGoods.OrderNo and orders.HouseID = orderGoods.HouseID
                                 left join Tbl_Cargo_Area as c on orderGoods.AreaID = c.AreaID
                                 inner join Tbl_Cargo_House as e on e.HouseID = orders.HouseID
                                 inner join Tbl_Cargo_Product as h on h.ProductID = orderGoods.ProductID
                                 inner join Tbl_Cargo_ProductType as f on h.TypeID = f.TypeID
                                 left join Tbl_Cargo_ProductSource ps on h.Source = ps.Source
                                 left join Tbl_Cargo_Area as j on c.ParentID = j.AreaID
                                 left join Tbl_Cargo_Area as k on j.ParentID = k.AreaID
                        where convert(varchar(10), orders.CreateDate, 23) >='{st}'
                          and convert(varchar(10), orders.CreateDate, 23) <='{et}' and CheckStatus=1 and orders.FinanceSecondCheck=1 and orders.OrderType = '4'
                          group by orderGoods.RelateOrderNo, h.TypeID,e.HouseID,ProductCode,orders.throwgood,orders.OrderModel,orders.SuppClientNum,convert(varchar(10),orders.CreateDate,23)
                                ) a3
group by TypeID,HouseID,Piece,TotalCharge,throwgood,OrderModel,Year,Month,Day,opid,DayStatisticsTime,SuppClientNum,ProductCode
) a2
 where exists (select * from Tbl_Cargo_OrderDayStatistics as aa where aa.TypeID=TypeID and aa.HouseID=HouseID and aa.DayStatisticsTime=a2.DayStatisticsTime)
 ) a4 on a3.TypeID=a4.TypeID and a3.HouseID=a4.HouseID and a3.ProductCode=a4.ProductCode and a3.SuppID=a4.SuppClientNum and a3.ThrowGood=a4.ThrowGood and a3.OrderModel=a4.OrderModel and a3.DayStatisticsTime=a4.DayStatisticsTime
                    --再新增

                 */

                                string strSQL = $@"
                    --先删除现有数据
delete from Tbl_Cargo_OrderDayStatistics where DayStatisticsTime>='{st}' and DayStatisticsTime<='{et}'
                    --再新增
                    ;insert into Tbl_Cargo_OrderDayStatistics(typeid, houseid, piece, totalcharge, throwgood, ordermodel, year, month, day, opid,DayStatisticsTime,SuppID,ProductCode)
                   select  * from (
 select TypeID,HouseID,sum(Piece) Piece,TotalCharge,throwgood,OrderModel,Year,Month,Day,opid,DayStatisticsTime,SuppClientNum,ProductCode from (
 select orderGoods.RelateOrderNo, h.TypeID,e.HouseID,sum(orderGoods.Piece) Piece,sum(orders.TotalCharge) TotalCharge,(case when orders.OrderModel=1 then (select top 1 ThrowGood from Tbl_Cargo_Order where OrderNo=orderGoods.RelateOrderNo) else orders.throwgood end) throwgood,orders.OrderModel
                        ,datename(YYYY,(convert(varchar(10),orders.CreateDate,23)))as Year,datename(month,(convert(varchar(10),orders.CreateDate,23))) as Month,datename(day ,(convert(varchar(10),orders.CreateDate,23))) as Day
                             ,'' as opid,(convert(varchar(10),orders.CreateDate,23)) DayStatisticsTime,orders.SuppClientNum,ProductCode
                        from Tbl_Cargo_Order orders
                                 inner join Tbl_Cargo_OrderGoods orderGoods
                                            on orders.OrderNo = orderGoods.OrderNo and orders.HouseID = orderGoods.HouseID
                                 left join Tbl_Cargo_Area as c on orderGoods.AreaID = c.AreaID
                                 inner join Tbl_Cargo_House as e on e.HouseID = orders.HouseID
                                 inner join Tbl_Cargo_Product as h on h.ProductID = orderGoods.ProductID
                                 inner join Tbl_Cargo_ProductType as f on h.TypeID = f.TypeID
                                 left join Tbl_Cargo_ProductSource ps on h.Source = ps.Source
                                 left join Tbl_Cargo_Area as j on c.ParentID = j.AreaID
                                 left join Tbl_Cargo_Area as k on j.ParentID = k.AreaID
                        where convert(varchar(10), orders.CreateDate, 23) >='{st}'
                          and convert(varchar(10), orders.CreateDate, 23) <='{et}' and (AwbStatus>1 or (CheckStatus = 1 and orders.FinanceSecondCheck = 1)) and orders.OrderType = '4'
                          group by orderGoods.RelateOrderNo, h.TypeID,e.HouseID,ProductCode,orders.throwgood,orders.OrderModel,orders.SuppClientNum,convert(varchar(10),orders.CreateDate,23)
                                ) a3
group by TypeID,HouseID,TotalCharge,throwgood,OrderModel,Year,Month,Day,opid,DayStatisticsTime,SuppClientNum,ProductCode                      
) a2";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@opid", DbType.String, opid);
                    conn.ExecuteScalar(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }

        #endregion


        #region 每月和每日销量 生成静态化销量数据
        public int GenerateDailySalesSnapshot()
        {
            int effectedRows = 0;
            //检查过去7天是否已经生成过静态数据
            string isGeneratedSqlTemp = @"
SELECT TOP 1 1 FROM Tbl_Cargo_DailySaleStatic WHERE CAST(SalesDate AS DATE) = CAST(DATEADD(DAY, @{dayfactor}, GETDATE()) AS DATE) --检查今日是否执行过
";
            string generateSqlTemp = @"
PRINT('------------ 区域仓库 ------------');
WITH ChildAreaCTE AS (
	SELECT
		HouseID,
		AreaID AS RootArea,
		ParentID AS ParentArea,
		AreaID AS AreaID,
		Name AS RootName,
		CAST('' AS varchar(50)) AS ParentName,
		Name AS AreaName,
		1 AS Level
	FROM
		Tbl_Cargo_Area a
	WHERE (1=1)
		AND ParentID = 0
	UNION ALL
	
	SELECT
		c.HouseID,
		c.RootArea,
		a.ParentID AS ParentArea,
		a.AreaID,
		c.RootName,
		c.AreaName,
		a.Name AS AreaName,
		c.Level + 1
	FROM
		Tbl_Cargo_Area a
		INNER JOIN ChildAreaCTE c ON a.ParentID = c.AreaID
)
SELECT
	* INTO #childArea
FROM
	ChildAreaCTE 
OPTION (MAXRECURSION 2); --只查到2级子仓库，如有3级子仓库就报错，防止无限递归。业务逻辑也只允许最大2级子仓（注：根仓库是0级）
CREATE UNIQUE INDEX IX_#childArea
ON #childArea (HouseID,AreaID)
INCLUDE(RootArea);
    
DECLARE @YesterdayDate DATE = CAST(DATEADD(DAY, @{dayfactor}, GETDATE()) AS DATE)
PRINT('------------ 删除昨日数据 ------------')
DELETE Tbl_Cargo_DailySaleStatic WHERE SalesDate = @YesterdayDate

PRINT('------------ 静态化保存产品昨日总销量 ------------')
INSERT INTO Tbl_Cargo_DailySaleStatic
(
    SalesDate,
    ProductCode,
    TypeID,
    HouseID,
    AreaID,
    Piece,
    WXPiece,
    LastUpdateTime
)
SELECT
	CAST(a.CreateDate AS DATE) SalesDate,
    c.ProductCode,
    c.TypeID,
    c.HouseID,
	ca.RootArea AreaID,
    SUM(b.Piece) AS Piece,
    SUM(CASE WHEN a.OrderType = 4 THEN b.Piece ELSE 0 END) AS WXPiece,
	GETDATE() LastUpdateTime
FROM Tbl_Cargo_OrderGoods AS b
INNER JOIN Tbl_Cargo_Order AS a ON a.OrderNo = b.OrderNo
INNER JOIN Tbl_Cargo_Product AS c ON b.ProductID = c.ProductID
INNER JOIN #childArea ca ON b.HouseID = ca.HouseID AND b.AreaID = ca.AreaID
WHERE a.ThrowGood != 25 --非退仓单
    AND a.OrderModel = 0  --订单类型为客户单，非退货单
    AND c.SpecsType != 5  --非次日达
    AND ISNULL(c.ProductCode, '') <> '' 
	AND CAST(a.CreateDate AS DATE) = @YesterdayDate
GROUP BY CAST(a.CreateDate AS DATE), c.ProductCode, c.TypeID, c.HouseID, ca.RootArea
ORDER BY CAST(a.CreateDate AS DATE), c.ProductCode, c.TypeID, c.HouseID, ca.RootArea

PRINT('------------ 数据插入完毕 ------------')
";

            for (int i = 0; i < 7; i++)
            {
                bool isGenerated = false;
                int dayFactor = (i + 1) * -1; //生成过去7天的静态数据
                string isGeneratedSql = isGeneratedSqlTemp.Replace("@{dayfactor}", dayFactor.ToString());
                string generateSql = generateSqlTemp.Replace("@{dayfactor}", dayFactor.ToString());

                using (var comm = conn.GetSqlStringCommond(isGeneratedSql))
                {
                    isGenerated = conn.ExecuteScalar(comm) != null;
                    if (!isGenerated)
                    {
                        comm.CommandText = generateSql;
                        effectedRows = conn.ExecuteNonQuery(comm);
                    }
                }
            }
            return effectedRows;
        }
        public int GenerateMonthlySalesSnapshot()
        {
            int effectedRows = 0;
            string isGeneratedSql = @"SELECT TOP 1 1 FROM Tbl_Cargo_MonthSaleStatic WHERE DATEADD(MONTH, DATEDIFF(MONTH, 0, YearMonth), 0) =  DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)";
            string generateSql = @"

PRINT('------------ 区域仓库 ------------');
WITH ChildAreaCTE AS (
	SELECT
		HouseID,
		AreaID AS RootArea,
		ParentID AS ParentArea,
		AreaID AS AreaID,
		Name AS RootName,
		CAST('' AS varchar(50)) AS ParentName,
		Name AS AreaName,
		1 AS Level
	FROM
		Tbl_Cargo_Area a
	WHERE (1=1)
		AND ParentID = 0
		AND a.IsShowStock = 0 AND a.HouseID IN (1,3,9,45,65,82,84,93,96,97,98,99,100,101,102,105,106,107,108,111,109,110,112,113,114,115,116,117,118,119,121,120,122,123,124,125,126,127,13,14,15,10,23,24,25,27,128,30,64,32,131,129,130,29,132,135,134,133,94,90,91,89,88,87,86,83,81,80,79,78,77,76,75,73,74,72,71,70,69,68,67,63,60,58,59,55,57,56,49,50,53,48,47,46,44,43,33,31,136,137,138)
	UNION ALL
	
	SELECT
		c.HouseID,
		c.RootArea,
		a.ParentID AS ParentArea,
		a.AreaID,
		c.RootName,
		c.AreaName,
		a.Name AS AreaName,
		c.Level + 1
	FROM
		Tbl_Cargo_Area a
		INNER JOIN ChildAreaCTE c ON a.ParentID = c.AreaID
    WHERE (1=1)
		AND a.IsShowStock = 0 AND a.HouseID IN (1,3,9,45,65,82,84,93,96,97,98,99,100,101,102,105,106,107,108,111,109,110,112,113,114,115,116,117,118,119,121,120,122,123,124,125,126,127,13,14,15,10,23,24,25,27,128,30,64,32,131,129,130,29,132,135,134,133,94,90,91,89,88,87,86,83,81,80,79,78,77,76,75,73,74,72,71,70,69,68,67,63,60,58,59,55,57,56,49,50,53,48,47,46,44,43,33,31,136,137,138)
)
SELECT
	* INTO #childArea
FROM
	ChildAreaCTE 
OPTION (MAXRECURSION 2); --只查到2级子仓库，如有3级子仓库就报错，防止无限递归。业务逻辑也只允许最大2级子仓（注：根仓库是0级）
CREATE UNIQUE INDEX IX_#childArea
ON #childArea (HouseID,AreaID)
INCLUDE(RootArea);


PRINT('------------ 删除上月数据 ------------');
DECLARE @LastMonth DATE = DATEADD(MONTH, -1, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0));
DELETE FROM Tbl_Cargo_MonthSaleStatic
WHERE YearMonth = @LastMonth;

PRINT('------------ 重新计算上月销量（近3个月） ------------');
PRINT('------------ 获取产品每月销量 ------------')
;WITH 
-- 获取从第一个销售单的创建时间到今天所有年月时间
DateRange AS (
    SELECT DATEADD(MONTH, -2, @LastMonth) AS MinMonth,
           @LastMonth AS MaxMonth
)
--SELECT * FROM DateRange
,MonthDim AS (
    SELECT MinMonth AS YearMonth
    FROM DateRange
    UNION ALL
    SELECT DATEADD(MONTH, 1, YearMonth)
    FROM MonthDim
    CROSS JOIN DateRange
    WHERE YearMonth < DateRange.MaxMonth
)
--SELECT * FROM MonthDim
,MonthlySalesGroup AS (
    SELECT
        p.ProductCode,
        p.TypeID,
        p.HouseID,
        ca.RootArea AreaID,
        DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0) YearMonth,
        SUM(ISNULL(og.Piece,0)) AS Piece
    FROM Tbl_Cargo_Product AS p
        INNER JOIN Tbl_Cargo_OrderGoods AS og ON og.ProductID = p.ProductID
        INNER JOIN #childArea ca ON ca.AreaID = og.AreaID
    WHERE ISNULL(p.ProductCode, '') <> ''
        AND DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0) BETWEEN DATEADD(MONTH, -2, @LastMonth) AND @LastMonth
    GROUP BY 
        p.ProductCode, p.TypeID, p.HouseID, ca.RootArea, DATEADD(MONTH, DATEDIFF(MONTH, 0, og.OP_DATE), 0)
)

,MonthlySalesOrg AS (
    SELECT
        p.*,
        ISNULL(m.Piece,0) AS Piece
    FROM (
            SELECT * FROM
            (SELECT ProductCode, TypeID, HouseID, AreaID FROM MonthlySalesGroup GROUP BY ProductCode, TypeID, HouseID, AreaID) p
            CROSS JOIN MonthDim d
        ) p
        LEFT JOIN MonthlySalesGroup m ON m.ProductCode = p.ProductCode AND m.TypeID = p.TypeID AND m.HouseID = p.HouseID AND m.AreaID = p.AreaID AND m.YearMonth = p.YearMonth
)

SELECT * 
    INTO #MonthlySales FROM MonthlySalesOrg;
CREATE UNIQUE INDEX IDX_#MonthlySales ON #MonthlySales (ProductCode,
        TypeID,
        HouseID,
        AreaID,
        YearMonth)
        INCLUDE(Piece)
        ;
-- select * from #MonthlySales
-- drop table #MonthlySales

PRINT('------------ 加权近3个月的月均销量 ------------')
PRINT('------------ 持久化插入计算数据 ------------')
;WITH MonthlyRanked AS
(
    SELECT a.*,
           x.SumLast3Months
    FROM #MonthlySales a
    OUTER APPLY
    (
        SELECT SUM(b.Piece) AS SumLast3Months
        FROM #MonthlySales b
        WHERE b.ProductCode = a.ProductCode
          AND b.TypeID = a.TypeID
          AND b.HouseID = a.HouseID
          AND b.AreaID = a.AreaID
          AND b.YearMonth BETWEEN DATEADD(MONTH, -2, a.YearMonth) AND a.YearMonth
    ) x
),
Weighted AS
(
    SELECT a.*,
           ISNULL(b.Piece * 0.2, 0) +
           ISNULL(c.Piece * 0.3, 0) +
           a.Piece * 0.5 AS WeightedMonthSale
    FROM MonthlyRanked a
    OUTER APPLY
    (
        SELECT TOP 1 b.Piece
        FROM #MonthlySales b
        WHERE b.ProductCode = a.ProductCode
          AND b.TypeID = a.TypeID
          AND b.HouseID = a.HouseID
          AND b.AreaID = a.AreaID
          AND b.YearMonth = DATEADD(MONTH, -2, a.YearMonth)
    ) b
    OUTER APPLY
    (
        SELECT TOP 1 c.Piece
        FROM #MonthlySales c
        WHERE c.ProductCode = a.ProductCode
          AND c.TypeID = a.TypeID
          AND c.HouseID = a.HouseID
          AND c.AreaID = a.AreaID
          AND c.YearMonth = DATEADD(MONTH, -1, a.YearMonth)
    ) c
    WHERE a.SumLast3Months > 0
)

INSERT INTO Tbl_Cargo_MonthSaleStatic
(
    YearMonth,
    ProductCode,
    TypeID,
    HouseID,
    AreaID,
    Piece,
    SumLast3Months,
    WeightedMonthSale,
    LastUpdateTime
)
SELECT
    YearMonth,
    ProductCode,
    TypeID,
    HouseID,
    AreaID,
    Piece,--当月总销量
    CEILING(SumLast3Months) SumLast3Months, --近3月总销量
    CEILING(WeightedMonthSale) WeightedMonthSale, --近3月加权总销量
    GETDATE() LastUpdateTime
FROM Weighted
WHERE YearMonth = @LastMonth
ORDER BY YearMonth,ProductCode, HouseID, AreaID;

PRINT('------------ 完成上月销量入库 ------------');

";
            bool isGenerated = false;
            using (var comm = conn.GetSqlStringCommond(isGeneratedSql))
            {
                isGenerated = conn.ExecuteScalar(comm) != null;
                if (!isGenerated)
                {
                    comm.CommandText = generateSql;
                    effectedRows = conn.ExecuteNonQuery(comm);
                }
            }
            return effectedRows;
        }
        #endregion

        public List<CargoProductTypeEntity> QueryTypeData()
        {
            List<CargoProductTypeEntity> result = new List<CargoProductTypeEntity>();
            string strSQL = $@"
            select TypeID,TypeName from Tbl_Cargo_ProductType where ParentID=1
order by TypeID
            ";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //List<CargoOrderPickPlanGoodsEntity> OrderPickPlanGoodsList = GetOrderPickPlanGoodsList(Convert.ToString(idr["PickPlanNo"]));
                        #region 获取运单数据
                        result.Add(new CargoProductTypeEntity
                        {
                            TypeID = Convert.ToInt32(idr["TypeID"]),
                            TypeName = Convert.ToString(idr["TypeName"]),
                            //PickPlanGoodsList = OrderPickPlanGoodsList
                        });
                        #endregion
                    }
                }
            }
            return result;
        }

        public List<SystemUserEntity> GetSysUsers()
        {
            List<SystemUserEntity> result = new List<SystemUserEntity>();
            string strSQL = $@"select * from Tbl_SysUser";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //List<CargoOrderPickPlanGoodsEntity> OrderPickPlanGoodsList = GetOrderPickPlanGoodsList(Convert.ToString(idr["PickPlanNo"]));
                        #region 获取运单数据
                        result.Add(new SystemUserEntity
                        {
                            UserID = Convert.ToInt32(idr["UserID"]),
                            UserName = Convert.ToString(idr["UserName"]),
                            LoginName = Convert.ToString(idr["LoginName"]),
                            CellPhone = Convert.ToString(idr["CellPhone"]),
                            //PickPlanGoodsList = OrderPickPlanGoodsList
                        });
                        #endregion
                    }
                }
            }
            return result;
        }

        public List<HouseEntity> QueryCassMallHouse()
        {
            List<HouseEntity> result = new List<HouseEntity>();
            string strSQL = $@"
                select facilityId,facilityName from Tbl_Cargo_CassMallOrderItemInventories
                group by facilityId,facilityName
                ";
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        //List<CargoOrderPickPlanGoodsEntity> OrderPickPlanGoodsList = GetOrderPickPlanGoodsList(Convert.ToString(idr["PickPlanNo"]));
                        #region 获取运单数据
                        result.Add(new HouseEntity
                        {
                            Value = Convert.ToString(idr["facilityId"]),
                            Name = Convert.ToString(idr["facilityName"]),
                            //PickPlanGoodsList = OrderPickPlanGoodsList
                        });
                        #endregion
                    }
                }
            }
            return result;
        }
    }
}
