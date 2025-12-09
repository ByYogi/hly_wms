using iText.IO.Source;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cargo.Interface.Utils
{
    /// <summary>
    /// 开放平台接口加密调用类，对应Java版本的OpenApiEncryptedService
    /// 使用AES256加密和解密，确保与Java版本的行为完全一致
    /// </summary>
    public class OpenApiEncryptedService
    {
        /// <summary>
        /// 沙箱环境URL
        /// </summary>
        private const string SandboxUrl = "https://opensandbox.tmallyc.com/api";

        /// <summary>
        /// 生产环境URL
        /// </summary>
        private const string ProductionUrl = "https://opengw.tmallyc.com/api";

        /// <summary>
        /// 开放平台API基础URL
        /// 沙箱环境: https://opensandbox.tmallyc.com/api
        /// 生产环境: https://opengw.tmallyc.com/api
        /// </summary>
        private readonly string _openUrl;

        /// <summary>
        /// HTTP客户端
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="useProduction">是否使用生产环境，默认为false（沙箱环境）</param>
        public OpenApiEncryptedService(bool useProduction = false)
        {
            _openUrl = useProduction ? ProductionUrl : SandboxUrl;
            _httpClient = new HttpClient();

            // 设置默认编码，对应Java版本的StringHttpMessageConverter(StandardCharsets.UTF_8)
            _httpClient.DefaultRequestHeaders.Add("Accept-Charset", "utf-8");
        }

        /// <summary>
        /// POST请求开放平台API
        /// 对应Java版本的post方法，实现完整的加密请求流程
        /// </summary>
        /// <param name="path">请求路径</param>
        /// <param name="requestObject">请求参数对象</param>
        /// <returns>解密后的响应结果</returns>
        public static string PostAsyncV2(object requestObject)
        {
            try
            {
                // 将请求参数转换为字典，对应Java的TypeUtils.convertBeanToMap
                var parameters = TypeUtils.ConvertObjectToDictionary(requestObject);

                // 设置AppKey和时间戳，与Java版本保持一致
                parameters["app_key"] = SignUtil.AppKey;
                parameters["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

                // 生成签名，与Java版本保持一致
                parameters["sign"] = SignUtil.GenerateSign(parameters);

                // 打印请求参数，与Java版本保持一致
                //Console.WriteLine($"入参: {JsonSerializer.Serialize(parameters)}");

                // 构建请求体，对应Java版本的MultiValueMap
                var formData = new List<KeyValuePair<string, string>>();
                foreach (var kvp in parameters)
                {
                    formData.Add(new KeyValuePair<string, string>(kvp.Key, kvp.Value));
                }

                // 对请求体进行加密，取AppSecret的前32位作为密钥
                string encryptedBody;
                try
                {
                    var bodyJson = JsonConvert.SerializeObject(formData);
                    encryptedBody = EncryptUtils.Encrypt(bodyJson);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"请求加密失败: {ex.Message}");
                    return "{\"code\":-1,\"msg\":\"请求加密失败\",\"info\":false}";
                }

                // 打印加密后的请求参数，与Java版本保持一致
                Console.WriteLine($"加密后的入参: {encryptedBody}");

                return encryptedBody;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"请求异常: {ex.Message}");
                return "{\"code\":-1,\"msg\":\"请求异常\",\"info\":false}";
            }
        }

        /// <summary>
        /// POST请求开放平台API
        /// 对应Java版本的post方法，实现完整的加密请求流程
        /// </summary>
        /// <param name="path">请求路径</param>
        /// <param name="requestObject">请求参数对象</param>
        /// <returns>解密后的响应结果</returns>
        public async Task<string> PostAsync(string path, object requestObject)
        {
            try
            {
                // 将请求参数转换为字典，对应Java的TypeUtils.convertBeanToMap
                var parameters = TypeUtils.ConvertObjectToDictionary(requestObject);

                // 设置AppKey和时间戳，与Java版本保持一致
                parameters["app_key"] = SignUtil.AppKey;
                parameters["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

                // 生成签名，与Java版本保持一致
                parameters["sign"] = SignUtil.GenerateSign(parameters);

                // 打印请求参数，与Java版本保持一致
                //Console.WriteLine($"入参: {JsonSerializer.Serialize(parameters)}");

                // 构建请求体，对应Java版本的MultiValueMap
                var formData = new List<KeyValuePair<string, string>>();
                foreach (var kvp in parameters)
                {
                    formData.Add(new KeyValuePair<string, string>(kvp.Key, kvp.Value));
                }

                // 对请求体进行加密，取AppSecret的前32位作为密钥
                string encryptedBody;
                try
                {
                    var bodyJson = JsonConvert.SerializeObject(formData);
                    encryptedBody = EncryptUtils.Encrypt(bodyJson);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"请求加密失败: {ex.Message}");
                    return "{\"code\":-1,\"msg\":\"请求加密失败\",\"info\":false}";
                }

                // 打印加密后的请求参数，与Java版本保持一致
                Console.WriteLine($"加密后的入参: {encryptedBody}");

                // 构建HTTP请求
                var content = new StringContent(encryptedBody, Encoding.UTF8, "application/x-www-form-urlencoded");

                // URL中需要指定为加密请求，传入app_key，与Java版本保持一致
                var requestUrl = $"{_openUrl}{path}?request_encrypted=true&app_key={SignUtil.AppKey}";

                // 发送POST请求
                var response = await _httpClient.PostAsync(requestUrl, content);
                var encryptedResult = await response.Content.ReadAsStringAsync();

                // 打印返回的加密result，与Java版本保持一致
                Console.WriteLine($"加密的result: {encryptedResult}");

                // 解密响应结果，取AppSecret的前32位作为密钥
                try
                {
                    return EncryptUtils.Decrypt(encryptedResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"响应解密失败: {ex.Message}");
                    return "{\"code\":-1,\"msg\":\"请求成功但是解密失败\",\"info\":true}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"请求异常: {ex.Message}");
                return "{\"code\":-1,\"msg\":\"请求异常\",\"info\":false}";
            }
        }

        /// <summary>
        /// 同步版本的POST请求
        /// </summary>
        /// <param name="path">请求路径</param>
        /// <param name="requestObject">请求参数对象</param>
        /// <returns>解密后的响应结果</returns>
        public string Post(string path, object requestObject)
        {
            return PostAsync(path, requestObject).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 验证配置是否正确
        /// </summary>
        /// <returns>配置验证结果</returns>
        public bool ValidateConfiguration()
        {
            var isValid = SignUtil.IsConfigValid();
            if (!isValid)
            {
                Console.WriteLine("配置验证失败，请检查AppKey和AppSecret配置");
                Console.WriteLine(SignUtil.GetConfigInfo());
            }
            return isValid;
        }

        /// <summary>
        /// 获取当前使用的环境信息
        /// </summary>
        /// <returns>环境信息</returns>
        public string GetEnvironmentInfo()
        {
            var environment = _openUrl == ProductionUrl ? "生产环境" : "沙箱环境";
            return $"当前环境: {environment}, URL: {_openUrl}";
        }

        /// <summary>
        /// 测试连接性
        /// </summary>
        /// <returns>连接测试结果</returns>
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_openUrl);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"连接测试失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~OpenApiEncryptedService()
        {
            Dispose();
        }
    }
}