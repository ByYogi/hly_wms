using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Cargo.Interface.Utils
{
    /// <summary>
    /// 加密解密工具类，对应Java版本的EncryptUtils
    /// 使用AES-256-ECB加密算法，确保与Java版本兼容
    /// </summary>
    public static class EncryptUtils
    {
        /// <summary>
        /// 密钥算法
        /// </summary>
        private const string KeyAlgorithm = "AES";

        /// <summary>
        /// 加密/解密算法/工作模式/填充方式
        /// 对应Java的"AES/ECB/PKCS5Padding"
        /// </summary>
        private const string CipherAlgorithm = "AES/ECB/PKCS5Padding";

        /// <summary>
        /// 加密数据
        /// 使用AppSecret的前32位作为密钥
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <returns>Base64编码的加密结果</returns>
        /// <exception cref="Exception">加密失败时抛出异常</exception>
        public static string Encrypt(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                    throw new ArgumentException("加密数据不能为空", nameof(data));

                // 获取AppSecret的前32位作为密钥，与Java版本保持一致
                var keyBytes = Encoding.UTF8.GetBytes(SignUtil.AppSecret.Substring(0, 32));
                var dataBytes = Encoding.UTF8.GetBytes(data);

                using (var aes = Aes.Create())
                {
                    // 设置为ECB模式，与Java版本保持一致
                    aes.Mode = CipherMode.ECB;
                    // 设置PKCS7填充（.NET中的PKCS7等同于Java中的PKCS5）
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = keyBytes;

                    using (var encryptor = aes.CreateEncryptor())
                    {
                        var encryptedBytes = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"加密失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 解密数据
        /// 使用AppSecret的前32位作为密钥
        /// </summary>
        /// <param name="data">Base64编码的加密数据</param>
        /// <returns>解密后的原始数据</returns>
        /// <exception cref="Exception">解密失败时抛出异常</exception>
        public static string Decrypt(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                    throw new ArgumentException("解密数据不能为空", nameof(data));

                // 获取AppSecret的前32位作为密钥，与Java版本保持一致
                var keyBytes = Encoding.UTF8.GetBytes(SignUtil.AppSecret.Substring(0, 32));
                var encryptedBytes = Convert.FromBase64String(data);

                using (var aes = Aes.Create())
                {
                    // 设置为ECB模式，与Java版本保持一致
                    aes.Mode = CipherMode.ECB;
                    // 设置PKCS7填充（.NET中的PKCS7等同于Java中的PKCS5）
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = keyBytes;

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"解密失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 生成AES密钥（256位）
        /// 对应Java版本的generateKey方法
        /// </summary>
        /// <returns>Base64编码的密钥</returns>
        /// <exception cref="Exception">密钥生成失败时抛出异常</exception>
        public static string GenerateKey()
        {
            try
            {
                using (var aes = Aes.Create())
                {
                    // 设置密钥长度为256位，与Java版本保持一致
                    aes.KeySize = 256;
                    aes.GenerateKey();

                    // 打印32位包含不可见字符密钥（对应Java版本的行为）
                    Console.WriteLine(Encoding.UTF8.GetString(aes.Key));

                    // 转换为Base64可见字符密钥
                    return Convert.ToBase64String(aes.Key);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"密钥生成失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证密钥长度是否符合要求
        /// </summary>
        /// <param name="key">要验证的密钥</param>
        /// <returns>密钥是否有效</returns>
        public static bool IsValidKey(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    return false;

                // 检查密钥长度是否至少32字符（对应Java版本的要求）
                return key.Length >= 32;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取加密算法信息
        /// </summary>
        /// <returns>算法信息字符串</returns>
        public static string GetAlgorithmInfo()
        {
            return $"算法: {KeyAlgorithm}, 模式: ECB, 填充: PKCS7, 密钥长度: 256位";
        }
    }
}