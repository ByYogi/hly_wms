using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Cargo.Interface.Utils
{
    /// <summary>
    /// 类型转换工具类，对应Java版本的TypeUtils
    /// 实现对象与字典之间的相互转换
    /// </summary>
    public static class TypeUtils
    {
        /// <summary>
        /// 将对象转换为字典，对应Java的convertBeanToMap方法
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的字典</returns>
        public static Dictionary<string, string> ConvertObjectToDictionary(object obj)
        {
            var dictionary = new Dictionary<string, string>();

            if (obj == null)
                return dictionary;

            try
            {
                var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in properties)
                {
                    if (!property.CanRead)
                        continue;

                    var propertyName = property.Name;
                    var propertyValue = property.GetValue(obj);

                    if (propertyValue != null)
                    {
                        string valueStr = CastBaseObjectToString(propertyValue);

                        // 非基础类型，转换为JSON字符串
                        if (valueStr == null)
                        {
                            valueStr = JsonConvert.SerializeObject(propertyValue);
                        }

                        dictionary[propertyName] = valueStr;
                    }
                }
            }
            catch (Exception ex)
            {
                // 打印异常信息
                Console.WriteLine($"ConvertObjectToDictionary异常: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return dictionary;
        }

        /// <summary>
        /// 将字典转换为对象，对应Java的convertMapToBean方法
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="dictionary">源字典</param>
        /// <returns>转换后的对象</returns>
        public static T ConvertDictionaryToObject<T>(Dictionary<string, string> dictionary) where T : new()
        {
            try
            {
                var result = new T();
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in properties)
                {
                    if (!property.CanWrite)
                        continue;

                    var propertyName = property.Name;

                    if (dictionary.ContainsKey(propertyName))
                    {
                        var value = InitValue(property.PropertyType, propertyName, dictionary, typeof(T));
                        if (value != null)
                        {
                            property.SetValue(result, value);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // 记得替换成log日志打印
                Console.WriteLine($"ConvertDictionaryToObject异常: {ex.Message}");
                Console.WriteLine("dictionary转换成对象异常");
                return default(T);
            }
        }

        /// <summary>
        /// 将基础类型值转换为字符串，对应Java的castBaseObjectToString方法
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的字符串，如果不是基础类型则返回null</returns>
        public static string CastBaseObjectToString(object obj)
        {
            if (obj == null)
                return null;

            switch (obj)
            {
                case bool boolValue:
                    return boolValue.ToString().ToLower(); // Java的Boolean.toString()返回小写
                case char charValue:
                    return charValue.ToString();
                case byte byteValue:
                    return byteValue.ToString();
                case short shortValue:
                    return shortValue.ToString();
                case int intValue:
                    return intValue.ToString();
                case long longValue:
                    return longValue.ToString();
                case float floatValue:
                    return floatValue.ToString(CultureInfo.InvariantCulture);
                case double doubleValue:
                    return doubleValue.ToString(CultureInfo.InvariantCulture);
                case decimal decimalValue:
                    return decimalValue.ToString(CultureInfo.InvariantCulture);
                case string stringValue:
                    return stringValue;
                case DateTime dateTimeValue:
                    // 对应Java的Date.getTime()，返回毫秒时间戳
                    return ((long)(dateTimeValue - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds).ToString();
                case byte[] byteArrayValue:
                    return System.Text.Encoding.UTF8.GetString(byteArrayValue);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 初始化对象数据，不支持Dictionary类型属性，对应Java的initValue方法
        /// </summary>
        /// <param name="propertyType">属性类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="dictionary">参数字典</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>初始化后的值</returns>
        private static object InitValue(Type propertyType, string propertyName, Dictionary<string, string> dictionary, Type targetType)
        {
            if (!dictionary.TryGetValue(propertyName, out string value) || string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var typeName = propertyType.FullName;

            try
            {
                // 处理可空类型
                var underlyingType = Nullable.GetUnderlyingType(propertyType);
                if (underlyingType != null)
                {
                    propertyType = underlyingType;
                    typeName = propertyType.FullName;
                }

                switch (typeName)
                {
                    case "System.String":
                        return value;
                    case "System.Int64":
                        return long.Parse(value);
                    case "System.Double":
                        return double.Parse(value, CultureInfo.InvariantCulture);
                    case "System.Int32":
                        return int.Parse(value);
                    case "System.Decimal":
                        return decimal.Parse(value, CultureInfo.InvariantCulture);
                    case "System.DateTime":
                        // 对应Java的new Date(Long.parseLong(value))
                        var timestamp = long.Parse(value);
                        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp);
                    default:
                        // 处理List类型
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            return InitListValue(targetType, propertyName, value.Replace("\\", ""));
                        }
                        // 其他不支持的类型，尝试转对象，不影响结果
                        else
                        {
                            try
                            {
                                var cleanValue = value.Replace("\\", "");
                                return JsonConvert.DeserializeObject(cleanValue, propertyType);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"InitValue反序列化异常: {ex.Message}");
                                return value.Replace("\\", "");
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InitValue转换异常: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 初始化List数据，对应Java的initListValue方法
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">值</param>
        /// <returns>初始化后的List对象</returns>
        private static object InitListValue(Type targetType, string propertyName, object value)
        {
            try
            {
                if (value == null)
                    return null;

                var property = targetType.GetProperty(propertyName);
                if (property == null)
                    return null;

                var propertyType = property.PropertyType;
                if (!propertyType.IsGenericType || propertyType.GetGenericTypeDefinition() != typeof(List<>))
                    return null;

                var elementType = propertyType.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(elementType);

                // 使用JsonSerializer反序列化List
                return JsonConvert.DeserializeObject(value.ToString(), listType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InitListValue异常: {ex.Message}");
                return null;
            }
        }
    }
}