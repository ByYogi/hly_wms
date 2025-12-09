using Cargo.Interface.Utils;
using Cargo.QY;
using Cargo.systempage;
using DocumentFormat.OpenXml.Spreadsheet;
using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using House.Entity.Dto;
using House.Manager;
using iText.Layout.Element;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Formula.Functions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Interop;

namespace Cargo.Interface
{
    /// <summary>
    /// TMallApi 的摘要说明
    /// </summary>
    public class TMallApi : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            CargoInterfaceBus nwBus = new CargoInterfaceBus();
            LogBus logbus = new LogBus();

            LogEntity log = new LogEntity();
            log.IPAddress = "";
            log.Moudle = "服务管理";
            log.Status = "0";
            log.NvgPage = "天猫接收发货通知单";
            log.UserID = "2029";
            log.Operate = "A";

            // 读取请求体
            using (var reader = new StreamReader(context.Request.InputStream, Encoding.UTF8))
            {
                var dataStr = reader.ReadToEnd();
                log.Memo = $@"天猫 获取到加密数据：" + dataStr;
                logbus.InsertLog(log);
                #region test
                //CargoTMallEntity cargoData22 = ParseToCargoTMallEntityV2(dataStr);
                //var requestBody2 = OpenApiEncryptedService.PostAsyncV2(cargoData22);
                //var requestBody = EncryptUtils.Decrypt(requestBody2);
                #endregion
                var requestBody = EncryptUtils.Decrypt(dataStr);
                //var param = context.Request.QueryString["apitype"];
                var param = "outhouse";
                log.Memo = $@"天猫 接收数据成功，加密：{dataStr}数据 解密：" + requestBody;
                logbus.InsertLog(log);
                // 解析 JSON 数据
                var logData = new CargoCassMallEntity { SourceType = 0, SourceAction = "API", orderId = null, ResJson = requestBody };
                if (param == "outhouse")
                {
                    // 使用 HttpUtility.ParseQueryString 解析
                    // 它会返回一个 NameValueCollection
                    // 2. 调用解析函数
                    CargoTMallEntity cargoData = ParseToCargoTMallEntity(requestBody);
                    logData.orderId = cargoData.outboundNoticeNo.ToString();
                    nwBus.AddTMallDataLog(logData);
                    if (cargoData != null)
                    {
                        //保存
                        nwBus.SaveTMallOutHouseData(cargoData);
                        log.Memo = "天猫 接收数据并保存完成，数据：";
                        logbus.InsertLog(log);
                        // 返回 200 状态码
                        context.Response.StatusCode = 200;
                        context.Response.Write("'{\"code\":0,\"msg\":\"成功\",\"info\":{\"data\":\"1\"}}'");
                    }
                    else
                    {
                        log.Memo = "天猫 接收数据失败，数据：" + requestBody;
                        logbus.InsertLog(log);
                        // 返回 -1 失败 状态码
                        context.Response.StatusCode = -1;
                        context.Response.Write("'{\"code\":\"非0整数\",\"msg\":\"失败\",\"info\":null}'");
                    }

                }

            }

        }

        /// <summary>
        /// 将查询字符串解析为 CargoTMallEntity 对象
        /// </summary>
        /// <param name="queryString">查询字符串</param>
        /// <returns>解析后的 CargoTMallEntity 对象</returns>
        public static CargoTMallEntity ParseToCargoTMallEntity(string rawJson)
        {
            // 步骤1：反序列化JSON数组为Key-Value列表
            var keyValueList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(rawJson);
            // 步骤2：构建清理后的字典（Key=Key字段，Value=清理空格后的Value字段）
            var paramDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); // 忽略大小写
            foreach (var item in keyValueList)
            {
                if (item.TryGetValue("Key", out string key) && item.TryGetValue("Value", out string value))
                {
                    paramDict[key] = value?.Trim() ?? string.Empty; // 清理首尾空格
                }
            }

            // 步骤3：初始化OutboundNotice并赋值
            var notice = new CargoTMallEntity();

            // --- 解析简单属性 ---
            // 使用 TryParse 模式安全地转换，避免异常 + GetValueOrDefault 安全取值（避免键不存在抛异常）
            notice.id = long.TryParse(paramDict.GetValueOrDefault("id"), out long idVal) ? idVal : 0;
            notice.userId = long.TryParse(paramDict.GetValueOrDefault("userId"), out long userIdVal) ? userIdVal : 0;
            notice.consigneeCityId = long.TryParse(paramDict.GetValueOrDefault("consigneeCityId"), out long cityIdVal) ? cityIdVal : 0;
            notice.consigneeCountyId = long.TryParse(paramDict.GetValueOrDefault("consigneeCountyId"), out long countyIdVal) ? countyIdVal : 0;
            notice.consigneeProvinceId = long.TryParse(paramDict.GetValueOrDefault("consigneeProvinceId"), out long provinceIdVal) ? provinceIdVal : 0;

            notice.originBillFirstChannel = int.TryParse(paramDict.GetValueOrDefault("originBillFirstChannel"), out int firstChannelVal) ? firstChannelVal : 0;
            notice.originBillSecondChannel = int.TryParse(paramDict.GetValueOrDefault("originBillSecondChannel"), out int secondChannelVal) ? secondChannelVal : 0;
            notice.originBillThirdChannel = int.TryParse(paramDict.GetValueOrDefault("originBillThirdChannel"), out int thirdChannelVal) ? thirdChannelVal : 0;
            notice.originBillType = int.TryParse(paramDict.GetValueOrDefault("originBillType"), out int billTypeVal) ? billTypeVal : 0;
            notice.urgentLevel = int.TryParse(paramDict.GetValueOrDefault("urgentLevel"), out int urgentLevelVal) ? urgentLevelVal : 0;
            notice.isExpressSheetEncrypted = int.TryParse(paramDict.GetValueOrDefault("isExpressSheetEncrypted"), out int isEncryptedVal) ? isEncryptedVal : 0;
            notice.isAllowLack = int.TryParse(paramDict.GetValueOrDefault("isAllowLack"), out int isAllowLackVal) ? isAllowLackVal : 0;
            notice.isEncrypted = int.TryParse(paramDict.GetValueOrDefault("isEncrypted"), out int isMainEncryptedVal) ? isMainEncryptedVal : 0;

            // DateTime转换添加非空处理
            notice.requireArriveTime = ConvertMillisecondTimeStampToDateTime(paramDict.GetValueOrDefault("requireArriveTime"));
            notice.noticeCreateTime = ConvertMillisecondTimeStampToDateTime(paramDict.GetValueOrDefault("noticeCreateTime"));
            notice.originBillTime = ConvertMillisecondTimeStampToDateTime(paramDict.GetValueOrDefault("originBillTime"));
            notice.requireOutWarehouseTime = ConvertMillisecondTimeStampToDateTime(paramDict.GetValueOrDefault("requireOutWarehouseTime"));
            notice.payTime = ConvertMillisecondTimeStampToDateTime(paramDict.GetValueOrDefault("payTime"));

            // 字符串类型使用 GetValueOrDefault 安全取值（键不存在时返回 null，与原逻辑一致）
            // 注意：字符串属性已经通过 GetValueOrDefault 处理了null情况，返回null而不是空字符串
            notice.sendType = paramDict.GetValueOrDefault("sendType");
            notice.consigneePhone = paramDict.GetValueOrDefault("consigneePhone");
            notice.extend = paramDict.GetValueOrDefault("extend");
            notice.customerContact = paramDict.GetValueOrDefault("customerContact");
            notice.outboundNoticeNo = paramDict.GetValueOrDefault("outboundNoticeNo");
            notice.originBillNo = paramDict.GetValueOrDefault("originBillNo");
            notice.consigneeProvinceName = paramDict.GetValueOrDefault("consigneeProvinceName");
            notice.consigneeCityName = paramDict.GetValueOrDefault("consigneeCityName");
            notice.consigneeCountyName = paramDict.GetValueOrDefault("consigneeCountyName");
            notice.warehouseCode = paramDict.GetValueOrDefault("warehouseCode");
            notice.saleType = paramDict.GetValueOrDefault("saleType");
            notice.warehouseName = paramDict.GetValueOrDefault("warehouseName");
            notice.thirdWarehouseCode = paramDict.GetValueOrDefault("thirdWarehouseCode");
            notice.buyerRemark = paramDict.GetValueOrDefault("buyerRemark");
            notice.sellerRemark = paramDict.GetValueOrDefault("sellerRemark");
            notice.deliverRemark = paramDict.GetValueOrDefault("deliverRemark");
            notice.aliOaid = paramDict.GetValueOrDefault("aliOaid");
            notice.consigneeContacts = paramDict.GetValueOrDefault("consigneeContacts");
            notice.customerName = paramDict.GetValueOrDefault("customerName");
            notice.consigneeDetail = paramDict.GetValueOrDefault("consigneeDetail");
            notice.app_key = paramDict.GetValueOrDefault("app_key");
            notice.sign = paramDict.GetValueOrDefault("sign");
            notice.timestamp = paramDict.GetValueOrDefault("timestamp");
            notice.is_test = paramDict.GetValueOrDefault("is_test");
            notice.callback = paramDict.GetValueOrDefault("callback");
            // 3.3 解析noticeDetailList（JSON数组）
            if (paramDict.TryGetValue("noticeDetailList", out string noticeDetailJson) && !string.IsNullOrEmpty(noticeDetailJson))
            {
                notice.noticeDetailList = JsonConvert.DeserializeObject<List<OutboundNoticeDetailSpiPo>>(noticeDetailJson);
            }

            return notice;

        }

        /// <summary>
        /// 将查询字符串解析为 CargoTMallEntity 对象
        /// </summary>
        /// <param name="queryString">查询字符串</param>
        /// <returns>解析后的 CargoTMallEntity 对象</returns>
        public static CargoTMallEntity ParseToCargoTMallEntityV2(string queryString)
        {
            var callbackData = HttpUtility.ParseQueryString(queryString);
            var data = new CargoTMallEntity();

            // --- 解析简单属性 ---
            // 使用 TryParse 模式安全地转换，避免异常
            data.id = long.TryParse(callbackData["id"], out long idVal) ? idVal : 0;
            data.userId = long.TryParse(callbackData["userId"], out long userIdVal) ? userIdVal : 0;
            data.consigneeCityId = long.TryParse(callbackData["consigneeCityId"], out long cityIdVal) ? cityIdVal : 0;
            data.consigneeCountyId = long.TryParse(callbackData["consigneeCountyId"], out long countyIdVal) ? countyIdVal : 0;
            data.consigneeProvinceId = long.TryParse(callbackData["consigneeProvinceId"], out long provinceIdVal) ? provinceIdVal : 0;

            data.originBillFirstChannel = int.TryParse(callbackData["originBillFirstChannel"], out int firstChannelVal) ? firstChannelVal : 0;
            data.originBillSecondChannel = int.TryParse(callbackData["originBillSecondChannel"], out int secondChannelVal) ? secondChannelVal : 0;
            data.originBillThirdChannel = int.TryParse(callbackData["originBillThirdChannel"], out int thirdChannelVal) ? thirdChannelVal : 0;
            data.originBillType = int.TryParse(callbackData["originBillType"], out int billTypeVal) ? billTypeVal : 0;
            data.urgentLevel = int.TryParse(callbackData["urgentLevel"], out int urgentLevelVal) ? urgentLevelVal : 0;
            data.isExpressSheetEncrypted = int.TryParse(callbackData["isExpressSheetEncrypted"], out int isEncryptedVal) ? isEncryptedVal : 0;
            data.isAllowLack = int.TryParse(callbackData["isAllowLack"], out int isAllowLackVal) ? isAllowLackVal : 0;
            data.isEncrypted = int.TryParse(callbackData["isEncrypted"], out int isMainEncryptedVal) ? isMainEncryptedVal : 0;

            data.requireArriveTime = ConvertMillisecondTimeStampToDateTime(callbackData["requireArriveTime"]);
            data.noticeCreateTime = ConvertMillisecondTimeStampToDateTime(callbackData["noticeCreateTime"]);
            data.originBillTime = ConvertMillisecondTimeStampToDateTime(callbackData["originBillTime"]);
            data.requireOutWarehouseTime = ConvertMillisecondTimeStampToDateTime(callbackData["requireOutWarehouseTime"]);
            data.payTime = ConvertMillisecondTimeStampToDateTime(callbackData["payTime"]);

            // 字符串类型直接赋值，?.?.? 操作符可以安全地处理 null 值
            data.sendType = callbackData["sendType"] ?? "";
            data.consigneePhone = callbackData["consigneePhone"] ?? "";
            data.extend = callbackData["extend"] ?? "";
            data.customerContact = callbackData["customerContact"] ?? "";
            data.outboundNoticeNo = callbackData["outboundNoticeNo"] ?? "";
            data.originBillNo = callbackData["originBillNo"] ?? "";
            data.consigneeProvinceName = callbackData["consigneeProvinceName"] ?? "";
            data.consigneeCityName = callbackData["consigneeCityName"] ?? "";
            data.consigneeCountyName = callbackData["consigneeCountyName"] ?? "";
            data.warehouseCode = callbackData["warehouseCode"] ?? "";
            data.saleType = callbackData["saleType"] ?? "";
            data.warehouseName = callbackData["warehouseName"] ?? "";
            data.thirdWarehouseCode = callbackData["thirdWarehouseCode"] ?? "";
            data.buyerRemark = callbackData["buyerRemark"] ?? "";
            data.sellerRemark = callbackData["sellerRemark"] ?? "";
            data.deliverRemark = callbackData["deliverRemark"] ?? "";
            data.aliOaid = callbackData["aliOaid"] ?? "";
            data.consigneeContacts = callbackData["consigneeContacts"] ?? "";
            data.customerName = callbackData["customerName"] ?? "";
            data.consigneeDetail = callbackData["consigneeDetail"] ?? "";
            data.app_key = callbackData["app_key"] ?? "";
            data.sign = callbackData["sign"] ?? "";
            data.timestamp = callbackData["timestamp"] ?? "";
            data.is_test = callbackData["is_test"] ?? "";
            data.callback = callbackData["callback"] ?? "";

            // --- 解复杂的列表属性 noticeDetailList ---
            //data.noticeDetailList = ParseNoticeDetailList(callbackData);
            data.noticeDetailList = JsonConvert.DeserializeObject<List<OutboundNoticeDetailSpiPo>>(callbackData["noticeDetailList"]);

            return data;
        }

        /// <summary>
        /// 从 NameValueCollection 中解析出 noticeDetailList
        /// </summary>
        private static List<OutboundNoticeDetailSpiPo> ParseNoticeDetailList(NameValueCollection collection)
        {
            var detailList = new List<OutboundNoticeDetailSpiPo>();

            // 找出所有与 noticeDetailList 相关的键
            var listKeys = collection.AllKeys.Where(key => key != null && key.StartsWith("noticeDetailList")).ToList();

            if (!listKeys.Any())
            {
                return detailList; // 如果没有相关键，返回空列表
            }

            // 使用正则表达式找出所有用到的索引，并确定最大索引，以确定列表大小
            var indices = listKeys.Select(key =>
            {
                var match = Regex.Match(key, @"noticeDetailList\[(\d+)\]");
                return match.Success ? int.Parse(match.Groups[1].Value) : -1;
            }).Where(index => index != -1).Distinct().ToList();

            if (!indices.Any())
            {
                return detailList;
            }

            // 按索引循环，创建并填充每个对象
            foreach (var i in indices)
            {
                var detail = new OutboundNoticeDetailSpiPo();
                string prefix = $"noticeDetailList[{i}].";

                detail.skuId = long.TryParse(collection[$"{prefix}skuId"], out long skuId) ? skuId : 0;
                detail.needOutboundNum = int.TryParse(collection[$"{prefix}needOutboundNum"], out int num) ? num : 0;
                detail.goodsQuality = int.TryParse(collection[$"{prefix}goodsQuality"], out int quality) ? quality : 0;
                detail.guaranteePeriod = long.TryParse(collection[$"{prefix}guaranteePeriod"], out long period) ? period : 0;
                detail.skuName = collection[$"{prefix}skuName"];
                detail.skuOrderCode = collection[$"{prefix}skuOrderCode"];

                detailList.Add(detail);
            }

            return detailList;
        }
        /// <summary>
        /// 添加播报
        /// </summary>
        //public void Broadcast(CargoTMallEntity dto)
        //{
        //    CargoHouseBus house = new CargoHouseBus();
        //    var cassHouses = house.GetCassHouses();
        //    var cassProducts = house.GetCassProDucts();
        //    foreach (var item in dto.noticeDetailList)
        //    {
        //        //获取对应仓库
        //        var cHouses = dto.thirdWarehouseCode;
        //        var cassHouse = cassHouses.FirstOrDefault(a => cHouses.Contains(a.CassHouseCode));
        //        var cassProduct = cassProducts.FirstOrDefault(a => item.PartsNum == a.CassCode);
        //        CargoHouseEntity houseEnt = house.QueryCargoHouseByID(cassHouse.HouseID);

        //        string proStr = cassProduct?.TypeName + " " + cassProduct?.Specs + " " + cassProduct?.Figure + " " + cassProduct?.LoadIndex + cassProduct?.SpeedLevel;

        //        //添加播报
        //        CargoNewOrderNoticeEntity cargoNewOrder = new CargoNewOrderNoticeEntity();
        //        cargoNewOrder.HouseName = "开思订单-" + houseEnt.Name;
        //        cargoNewOrder.OrderNo = dto.OrderHeader.OrderId;
        //        cargoNewOrder.OrderNum = item.Quantity.ToString();
        //        cargoNewOrder.ClientInfo = dto.OrderHeader.Buyer.CompanyDisplayName + " " + dto.OrderPostalAddress.ContactNumber + " " + (dto.OrderPostalAddress.ProvinceGeoName + " " + dto.OrderPostalAddress.CityGeoName + " " + dto.OrderPostalAddress.Address);// "泰乐 华笙 广东省广州市白云区东平加油站左侧";
        //        cargoNewOrder.ProductInfo = proStr;// "马牌 215/55R16 CC6 98V";
        //        cargoNewOrder.DeliveryName = "急送";// "自提";
        //        cargoNewOrder.ReceivePeople = "";
        //        string hcno = JSON.Encode(cargoNewOrder);
        //        //Common.WriteTextLog(hcno);
        //        List<CargoVoiceBroadEntity> voiceBroadList = house.GetVoiceBroadList(new CargoVoiceBroadEntity { HouseID = houseEnt.HouseID });
        //        foreach (var voice in voiceBroadList)
        //        {
        //            RedisHelper.SetString("NewOrderNotice_" + voice.LoginName, hcno);
        //            //mc.Add("NewOrderNotice_" + voice.LoginName, hcno);
        //        }
        //    }

        //}
        /// <summary>
        /// 将毫秒级Unix时间戳字符串转换为DateTime（失败返回默认值）
        /// </summary>
        /// <param name="timeStampStr">13位毫秒级时间戳字符串（如"1764830781000"）</param>
        /// <returns>转换后的DateTime（UTC），失败返回default(DateTime)</returns>
        private static DateTime ConvertMillisecondTimeStampToDateTime(string timeStampStr)
        {
            // 1. 处理空值/空白字符串
            if (string.IsNullOrWhiteSpace(timeStampStr))
                return default(DateTime);

            // 2. 尝试转换为长整型（毫秒级时间戳）
            if (!long.TryParse(timeStampStr, out long timeStampMs))
                return default(DateTime);

            try
            {
                // 3. Unix时间戳起始时间（UTC）
                DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                // 4. 累加毫秒数得到目标时间
                DateTime targetTime = unixEpoch.AddMilliseconds(timeStampMs);

                // 【可选】如果需要转换为本地时间，取消下面注释
                // targetTime = targetTime.ToLocalTime();

                return targetTime;
            }
            catch
            {
                // 处理数值异常（如时间戳过大/过小）
                return default(DateTime);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    // 兼容所有.NET版本的字典扩展方法
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 获取字典值，不存在则返回默认值
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值（可选）</param>
        /// <returns>对应值或默认值</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            // 判空+检查是否包含键
            if (dictionary == null || !dictionary.ContainsKey(key))
            {
                return defaultValue;
            }
            return dictionary[key];
        }
    }
}