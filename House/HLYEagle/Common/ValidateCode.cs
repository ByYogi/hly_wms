using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace HLYEagle
{
    public class ValidateCode
    {
        public static void CreateCheckCodeImage()
        {
            CreateCheckCodeImage(GenerateCheckCode());
        }
        private static void CreateCheckCodeImage(string checkCode)
        {
            if ((checkCode != null) && (checkCode.Trim() != string.Empty))
            {
                Bitmap bitmap = new Bitmap((int)Math.Ceiling(checkCode.Length * 12.5), 0x16);
                Graphics graphics = Graphics.FromImage(bitmap);
                try
                {
                    int num;
                    Random random = new Random();
                    graphics.Clear(Color.White);
                    for (num = 0; num < 0x19; num++)
                    {
                        int num2 = random.Next(bitmap.Width);
                        int num3 = random.Next(bitmap.Width);
                        int num4 = random.Next(bitmap.Width);
                        int num5 = random.Next(bitmap.Width);
                        graphics.DrawLine(new Pen(Color.Silver), num2, num4, num3, num5);
                    }
                    //Font font = new Font("Arial", 12f, 3)
                    Font font = new Font("Arial", 18, GraphicsUnit.Pixel);
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bitmap.Width, bitmap.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                    graphics.DrawString(checkCode, font, brush, 2f, 2f);
                    for (num = 0; num < 100; num++)
                    {
                        int num6 = random.Next(bitmap.Width);
                        int num7 = random.Next(bitmap.Height);
                        bitmap.SetPixel(num6, num7, Color.FromArgb(random.Next()));
                    }
                    graphics.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
                    MemoryStream stream = new MemoryStream();
                    bitmap.Save(stream, ImageFormat.Gif);
                    HttpContext.Current.Response.ClearContent();
                    //HttpContext.Current.Response.ContentType("image/Gif");
                    HttpContext.Current.Response.ContentType = "image/Gif";
                    HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                }
                catch (Exception exception)
                {
                    //HttpContext.get_Current().get_Response().Redirect("ErrorMessage.aspx@Error=" + exception);
                }
                finally
                {
                    graphics.Dispose();
                    bitmap.Dispose();
                }
            }

        }
        private static string GenerateCheckCode()
        {
            string str = getRodomNum();
            HttpContext.Current.Session.Add("CheckCode", str);
            return str;

        }
        private static string GetEngLish()
        {
            string[] strArray = new string[] {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "M", "L", "N", "O", "P",
        "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
     };
            return strArray[Random(0, 0x19)];

        }
        private static string getRodomNum()
        {
            Random random = new Random();
            return Random(0x3e8, 0x270f).ToString();
        }
        public static int Random(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        /// <summary>
        /// 判断验证码
        /// </summary>
        /// <param name="strCheckKey"></param>
        /// <param name="strInPut"></param>
        /// <returns></returns>
        public static bool GetCheckResult(string strCheckKey, string strInPut)
        {
            return GetSession(strCheckKey).Equals(strInPut);
        }

        public static string GetSession(string strKey)
        {
            string str = (HttpContext.Current.Session[strKey] != null) ? HttpContext.Current.Session[strKey].ToString() : "";
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (!string.IsNullOrEmpty(GetCookies(strKey)))
            {
                SetSession(strKey, joecrown(GetCookies(strKey), 6));
                return HttpContext.Current.Session[strKey].ToString();
            }
            return "";
        }
        public static string joecrown(string str, int t)
        {
            string str2 = "";
            try
            {
                char[] chArray = str.ToCharArray();
                string[] strArray = ArrayListstr(str, t);
                for (int i = 0; i < strArray.Length; i++)
                {
                    byte[] buffer = Convert.FromBase64String(strArray[i].ToString().Substring(0, 4));
                    str2 = str2 + Encoding.UTF8.GetString(buffer);
                }
                return str2;
            }
            catch
            {
                return "异常信息";
            }
        }
        public static string[] ArrayListstr(string str, int n)
        {
            string[] strArray;
            try
            {
                if (n <= 0)
                {
                    n = str.Length;
                }
                int num = 0;
                num = str.Length / n;
                if (num <= 0)
                {
                    num = 1;
                }
                strArray = new string[num];
                for (int i = 0; i < num; i++)
                {
                    strArray[i] = str.Substring(n * i, n);
                }
            }
            catch
            {
                strArray = new string[] { "异常信息" };
            }
            return strArray;
        }
        public static void SetSession(string strKey, string strValue)
        {
            HttpContext.Current.Session.Add(strKey, strValue);
        }
        public static string GetCookies(string strKey)
        {
            return ((HttpContext.Current.Request.Cookies.Get(strKey) != null) ? HttpContext.Current.Request.Cookies.Get(strKey).Value.ToString().Trim() : "");
        }
    }
}