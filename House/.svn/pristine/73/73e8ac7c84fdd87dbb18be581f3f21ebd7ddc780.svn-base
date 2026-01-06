using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace Cargo
{
    /// <summary>
    /// 计算平台的费的费率
    /// </summary>
    public class OtherFeeRatio
    {
        public static decimal Ratio(int id)
        {
            string dutyRosterJson = RedisHelper.GetString("OtherFeeRatio");

            // 缓存为空时，返回默认的0.025（与原代码默认值一致）
            if (string.IsNullOrWhiteSpace(dutyRosterJson))
            {
                return 0.025m;
            }

            try
            {
                RatioRootModel ratioModel = JsonConvert.DeserializeObject<RatioRootModel>(dutyRosterJson);
                if (ratioModel == null || ratioModel.Data == null || ratioModel.Data.Count == 0)
                {
                    return ratioModel?.OtherRatio ?? 0.025m;
                }
                RatioItemModel matchedItem = ratioModel.Data.Find(item => item.Id == id);
                return matchedItem != null ? matchedItem.Ratio : ratioModel.OtherRatio;
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("获取通道费比例失败：" + ex);
                // LogHelper.Error("Ratio方法反序列化JSON异常", ex); // 建议添加日志
                return 0.025m;
            }
        }
    }
    public class RatioRootModel
    {
        /// <summary>
        /// 默认比率（未匹配到id时返回）
        /// </summary>
        public decimal OtherRatio { get; set; }

        /// <summary>
        /// 具体id对应的比率列表
        /// </summary>
        public List<RatioItemModel> Data { get; set; }
    }

    public class RatioItemModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 匹配id对应的比率
        /// </summary>
        public decimal Ratio { get; set; }
    }
}