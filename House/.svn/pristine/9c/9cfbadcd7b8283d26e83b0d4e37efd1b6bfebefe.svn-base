using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Cargo.Interface.Utils
{
    /// <summary>
    /// 签名工具类，对应Java版本的SignUtil
    /// 实现MD5签名生成和验证功能，确保与Java版本的签名算法完全一致
    /// </summary>
    public static class SignUtil
    {
        ///// <summary>
        ///// AppKey配置
        ///// TODO: 替换为你的AppKey
        ///// </summary>
        //public const string AppKey = "2025120243201";

        ///// <summary>
        ///// AppSecret配置
        ///// TODO: 替换为你的AppSecret
        ///// 取AppSecret的前32位作为加密密钥
        ///// </summary>
        //public const string AppSecret = "1d6c1fb8c356b5b405c674f4c572426d57362a9b";

        /// <summary>
        /// AppKey配置
        /// 从配置文件中读取
        /// </summary>
        public static string AppKey
        {
            get
            {
                var appKey = ConfigurationManager.AppSettings["TMall_AppKey"];
                if (string.IsNullOrWhiteSpace(appKey))
                {
                    return "2025120243201";
                }
                return appKey;
            }
        }

        /// <summary>
        /// AppSecret配置
        /// 从配置文件中读取
        /// 取AppSecret的前32位作为加密密钥
        /// </summary>
        public static string AppSecret
        {
            get
            {
                var appSecret = ConfigurationManager.AppSettings["TMall_AppSecret"];
                if (string.IsNullOrWhiteSpace(appSecret))
                {
                     return "1d6c1fb8c356b5b405c674f4c572426d57362a9b";
                }
                return appSecret;
            }
        }

        /// <summary>
        /// 生成签名
        /// 严格按照Java版本的逻辑实现：参数排序 -> 拼接字符串 -> MD5哈希
        /// </summary>
        /// <param name="requestParams">请求参数字典</param>
        /// <returns>MD5签名字符串（小写）</returns>
        public static string GenerateSign(Dictionary<string, string> requestParams)
        {
            try
            {
                if (requestParams == null)
                    throw new ArgumentNullException(nameof(requestParams));

                // 获取所有键并排序，与Java版本的Arrays.sort(keys)保持一致
                var keys = requestParams.Keys.ToArray();
                Array.Sort(keys, StringComparer.Ordinal);

                // 构建签名字符串，与Java版本的逻辑完全一致
                var queryBuilder = new StringBuilder();
                queryBuilder.Append(AppSecret);

                foreach (var key in keys)
                {
                    var value = requestParams[key];
                    if (!string.IsNullOrEmpty(value))
                    {
                        queryBuilder.Append(key).Append(value);
                    }
                    else
                    {
                        queryBuilder.Append(key);
                    }
                }

                queryBuilder.Append(AppSecret);
                var queryString = queryBuilder.ToString();

                // 打印签名参数，与Java版本保持一致
                Console.WriteLine($"签名param：{queryString}");

                // 计算MD5哈希，返回小写十六进制字符串
                return ComputeMd5Hash(queryString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"生成签名异常: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 验证签名
        /// 对应Java版本的verifySign方法
        /// </summary>
        /// <param name="requestParams">请求参数字典</param>
        /// <returns>签名验证结果</returns>
        public static bool VerifySign(Dictionary<string, string> requestParams)
        {
            try
            {
                if (requestParams == null || !requestParams.ContainsKey("sign"))
                    return false;

                var paramSign = requestParams["sign"];

                // 生成签名时将sign去掉，与Java版本保持一致
                var paramsForSign = new Dictionary<string, string>(requestParams);
                paramsForSign.Remove("sign");

                // 生成本地签名
                var localSign = GenerateSign(paramsForSign);

                // 打印签名对比信息，与Java版本保持一致
                Console.WriteLine($"远程签名：{paramSign}  本地生成签名：{localSign}");

                // 验证签名是否匹配
                return !string.IsNullOrWhiteSpace(paramSign) &&
                       !string.IsNullOrWhiteSpace(localSign) &&
                       paramSign.Equals(localSign, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"验证签名异常: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 计算字符串的MD5哈希值
        /// 对应Java版本的DigestUtils.md5DigestAsHex方法
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>MD5哈希值（小写十六进制）</returns>
        private static string ComputeMd5Hash(string input)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    var inputBytes = Encoding.UTF8.GetBytes(input);
                    var hashBytes = md5.ComputeHash(inputBytes);

                    // 转换为小写十六进制字符串，与Java版本保持一致
                    var sb = new StringBuilder();
                    foreach (var b in hashBytes)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"MD5哈希计算失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证AppKey和AppSecret配置是否有效
        /// </summary>
        /// <returns>配置是否有效</returns>
        public static bool IsConfigValid()
        {
            return !string.IsNullOrWhiteSpace(AppKey) &&
                   !string.IsNullOrWhiteSpace(AppSecret) &&
                   AppKey != "your_app_key" &&
                   AppSecret != "your_app_secret_secret_secret_secret_sec" &&
                   AppSecret.Length >= 32;
        }

        /// <summary>
        /// 获取配置信息（用于调试）
        /// </summary>
        /// <returns>配置信息字符串</returns>
        public static string GetConfigInfo()
        {
            return $"AppKey: {(string.IsNullOrWhiteSpace(AppKey) ? "未配置" : AppKey)}, " +
                   $"AppSecret长度: {(string.IsNullOrWhiteSpace(AppSecret) ? 0 : AppSecret.Length)}";
        }

        /// <summary>
        /// 创建带有基础参数的请求参数字典
        /// </summary>
        /// <param name="additionalParams">额外参数</param>
        /// <returns>包含AppKey和时间戳的参数字典</returns>
        public static Dictionary<string, string> CreateRequestParams(Dictionary<string, string> additionalParams = null)
        {
            var requestParams = new Dictionary<string, string>
            {
                ["app_key"] = AppKey,
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()
            };

            if (additionalParams != null)
            {
                foreach (var kvp in additionalParams)
                {
                    requestParams[kvp.Key] = kvp.Value;
                }
            }

            return requestParams;
        }
    }
}