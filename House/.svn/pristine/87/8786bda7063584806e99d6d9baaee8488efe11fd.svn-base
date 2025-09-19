using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cargo.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 获取字符串（不去空格，原样返回）
        /// </summary>
        public static string GetString(this HttpRequest request, string key)
        {
            var value = request[key];
            return value == null ? null : value.ToString();
        }

        /// <summary>
        /// 获取去掉前后空格的字符串，空字符串返回 null
        /// </summary>
        public static string GetTrimmedString(this HttpRequest request, string key)
        {
            var value = request[key];
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        /// <summary>
        /// 获取可空 int
        /// </summary>
        public static int? GetInt(this HttpRequest request, string key)
        {
            return int.TryParse(request[key]?.ToString(), out var result) ? result : (int?)null;
        }

        /// <summary>
        /// 获取可空 byte
        /// </summary>
        public static byte? GetByte(this HttpRequest request, string key)
        {
            return byte.TryParse(request[key]?.ToString(), out var result) ? result : (byte?)null;
        }

        /// <summary>
        /// 获取可空 DateTime
        /// </summary>
        public static DateTime? GetDateTime(this HttpRequest request, string key)
        {
            return DateTime.TryParse(request[key]?.ToString(), out var result) ? result : (DateTime?)null;
        }

    }
}