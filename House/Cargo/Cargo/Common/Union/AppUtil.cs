using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Cargo
{
    public class AppUtil
    {
        /// <summary>
        /// 将参数排序组装
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static String BuildParamStr(Dictionary<String, String> param)
        {
            if (param == null || param.Count == 0)
            {
                return "";
            }
            Dictionary<String, String> ascDic = param.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            StringBuilder sb = new StringBuilder();
            foreach (var item in ascDic)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    sb.Append(item.Key).Append("=").Append(item.Value).Append("&");
                }
                
            }

            return sb.ToString().Substring(0,sb.ToString().Length-1);
        }

        /// <summary>
        /// 将 sign 字符串中的每个字符转换为UTF-8编码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static String signParam(Dictionary<String, String> param)
        {
            if (param == null || param.Count == 0)
            {
                return "";
            }
            String blankStr = BuildParamStr(param);
            String prikey = RSAPrivateKeyJava2DotNet(AppConstants.PRIKEY);
            String sign = RSASignCSharp(blankStr, prikey, "SHA1");//私钥加签
            return System.Web.HttpUtility.UrlEncode(sign, System.Text.Encoding.UTF8);
        }

        public static String signParamBILL(Dictionary<String, String> param)
        {
            if (param == null || param.Count == 0)
            {
                return "";
            }
            //String blankStr = "appId=JST_19GPPP545A5MO&bizContent={\"bizUserId\":\"A9GR9JB0JJTOG\",\"outOrderNo\":\"1747884145863\",\"amount\":4,\"splitRule\":\"{\\\"feeTakeMchId\\\":\\\"A9GR9JB0JJTOG\\\",\\\"type\\\":\\\"0\\\",\\\"splitRuleList\\\":[{\\\"transMessage\\\":\\\"测试余额分账\\\",\\\"subOutOrderNo\\\":\\\"1747884145863-1\\\",\\\"value\\\":\\\"4\\\",\\\"bizUserId\\\":\\\"99GPQ7D6C7SHS\\\"}]}\",\"notifyUrl\":\"https://test.xchjst.com/api/notify\"}&charset=utf-8&format=json&method=jst.kernel.BizSettlementService.drawCommission&signType=RSA&timestamp=20250522112226&version=11";
            String blankStr = BuildParamStr(param);
            String prikey = RSAPrivateKeyJava2DotNet(AppConstants.PRIKEYBILL);
            String sign = RSASignCSharp(blankStr, prikey, "SHA1");//私钥加签
            return sign;
        }
        /// <summary>
        /// 公钥验签
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool validSign(Dictionary<String, String> param)
        {
            //  Dictionary<String, String> param = (Dictionary<String, String>)JsonConvert.DeserializeObject(rspDic, typeof(Dictionary<String, String>));
            String signRsp = param["sign"];
            param.Remove("sign");
            String blankStr = BuildParamStr(param);
            String pubkey = RSAPublicKeyJava2DotNet(AppConstants.PUBKEY);
            bool flag = VerifyCSharp(blankStr, pubkey, signRsp, "SHA1");//公钥验签
            return flag;

        }

        /// <summary>
        /// 将实体转化为json
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ObjectToJson(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        public static String RSASignCSharp(String data, String privateKeyCSharp, String hashAlgorithm = "SHA1", String encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyCSharp); //加载私钥   
            var dataBytes = Encoding.GetEncoding(encoding).GetBytes(data);
            var HashbyteSignature = rsa.SignData(dataBytes, hashAlgorithm);
            return Convert.ToBase64String(HashbyteSignature);
        }

        public static string RSAPrivateKeyJava2DotNet(string privateKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
        }


        public static string RSAPublicKeyJava2DotNet(string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));

        }

        public static bool VerifyCSharp(String data, String publicKeyCSharp, String signature, String hashAlgorithm = "SHA1", String encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //导入公钥，准备验证签名
            rsa.FromXmlString(publicKeyCSharp);
            //返回数据验证结果
            Byte[] Data = Encoding.GetEncoding(encoding).GetBytes(data);
            Byte[] rgbSignature = Convert.FromBase64String(signature);

            return rsa.VerifyData(Data, hashAlgorithm, rgbSignature);
        }
    }
}