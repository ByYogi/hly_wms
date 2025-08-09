using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cargo
{
    /// <summary>
    /// 通联配置信息
    /// </summary>
    public class AppConstants
    {
        /// <summary>
        /// 接口版本号
        /// </summary>
        public static String APIVERSION = "11";

        #region 支付
        //public static String APPID = "00328929";
        public static String CUSID = "660581055322T0Z";
        public static String APPID = "00328929";
        //public static String CUSID = "660581055322T11";
        /// <summary>
        /// 接口地址：https://vsp.allinpay.com/apiweb/unitorder/pay
        /// 测试地址：https://syb-test.allinpay.com/apiweb/unitorder/pay
        /// </summary>
        public static String API_URL = "https://vsp.allinpay.com/apiweb/unitorder";
        /// <summary>
        /// RSA私钥
        /// </summary>
        public static String PRIKEY = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDX5QZ2CKRurOK1aEPFDIuz+3HCeXXm+ykgb9mSKfgJSGm5WcP6UkLNCCZC5+JtxQKoddiwrrIpg7OApFfjOQIiKygdUQum/+LfsOtGgmURRgWtLCvvc0NGCND3XIp26ze+EV7v2DmffUFmc0fpY/elna+FA6hB2Lew2EU4bGtEssq6DRnVYoUipIQbTnm0kIMaWrWfaJKrZsU/QNv6zRdtmpAqReVmDQ8FjYAGb8TGMkhxHrkZn2VdPrcI0ta1LDnp0rJ17cocXWVDrnVycp4g+BmyL8Y5+4srDFZkOV8Z9lIk2+buOn0kp/wY2Ebynyx35l++UDy3QRL3LvQlKGK9AgMBAAECggEAFkji6ZibRnxMe26rfiG9IuMbuzf32lOHC3zYH6z62BR0K0hvgP1wPUMv1dyjI/L/wv1FFHm31K6uPuNX2hG2nWnIfnww4HhNxgq4QmMvxQBuEhG66EDEyywaS8ou1bcSXiljXAz857BRaZ6RJ+9kbuEvrqJxwfg5f8TXIkuYp/K+6LQn/6zXhhjE52kk/hICNk9vAt7vzXDsXfakgBe54Kv5yWGx8hiuQXxe95c+KtGFNN3vx9nPuprNDB5pUKZCUqAtATfagVQWSYGSTkFXbOdgKHbM3MUeTlF2w53i/MDdWHB+h7zCgOcPjnTBwJ0gDR0J54dz+mDlGIhuKKI4EQKBgQD+VTn/QZUcysZgPWJD+OWg9eEM9JAdfzFc1KVQcvxmjGAOk05qbn86Ceu44sxab2ZDFAAcjZafhnMhZureq/qMA0m8ljRvkADvJOwIlWQapexQKIsL0x/FnoiiX7J7W3tQ6ONQz/VublM9HWDVETEdfy4cGWdpQG50TI6yoQhQEQKBgQDZT0x/S0W7aVSsrqoEK1LszV0ystBCEe1rG4sTPKZkjcot+wGIxM0te4SyeALnLA4t75r1cuS2826WpdV8J18JFCVGxJFCgs1hmJxOKQfnMp8jEkTBOtaL4z2uPlh9aSF0XFyIEdPIl4/jF/NfoEmUsOiqQOeYigYFdf6Z7zUT7QKBgQDIOTxBcdX8Jr7pwSl9LZNJloin9FkIf7vSwy2qGfwOl3B/yQQw0fCjNnA2y4XppE2zfSlhUS5FREmbADTjPC0w0N3Z874wHgQ+QKj5UPLP79QMfv6IPpuCrn4OppwSdUCtv8pFMpKbb4tkT56N3sz11cvtHyrEN489S5ADnLa4oQKBgFGYlQC3ItfcFQ9CBlTKb7i64+PFgK1OTfeJwA7ZFyFqnB/nwAu62c/aMVlR9sUWpbxZ27WODpMjlMK8Qgz8O5MNHXIVHPX8Z2HeC9LNVUpal6ZzlZ14mlasXNywTEnKz+UBST1OdFc9kamTtK6TYQ2T38kN1ehQ6zHhvFfQmf2BAoGBALtgVh5MzMHfvdEiQ7QfWThDqH8PitVEj1Vnpnn1tmf6kJHoshlkFxvQk/GgG/DiQ5Fel0OIkN51hYMUHENfal0cvXgjk9IyFgC6lXsVoDgTk6R/b3XHf/81f+xsJzVyHk5vXDJg0opUG+NZZew9XViVNd+GyRHIvQK/6EVgyShA";
        /// <summary>
        /// RSA公钥
        /// </summary>
        public static String PUBKEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA1+UGdgikbqzitWhDxQyLs/txwnl15vspIG/Zkin4CUhpuVnD+lJCzQgmQufibcUCqHXYsK6yKYOzgKRX4zkCIisoHVELpv/i37DrRoJlEUYFrSwr73NDRgjQ91yKdus3vhFe79g5n31BZnNH6WP3pZ2vhQOoQdi3sNhFOGxrRLLKug0Z1WKFIqSEG055tJCDGlq1n2iSq2bFP0Db+s0XbZqQKkXlZg0PBY2ABm/ExjJIcR65GZ9lXT63CNLWtSw56dKyde3KHF1lQ651cnKeIPgZsi/GOfuLKwxWZDlfGfZSJNvm7jp9JKf8GNhG8p8sd+ZfvlA8t0ES9y70JShivQIDAQAB";

        //public static String APPID = "00002811";
        //public static String CUSID = "5505810078000YD";
        //public static String APIVERSION = "11";
        //public static String API_URL = "https://syb-test.allinpay.com/apiweb/unitorder";
        //public static String PRIKEY = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCfw/st1+lYn9k5wDuIIdsKuk2Ju/lijbyL/jJVcQxfaiit5iTSeVrReoaANtl8QqJdnB0KxySCzvONR/J2xHRgh5OsVZxuuV4RHkg8tqCJB5AckeDf97T61wt7byrr/tMutFqe8Jjmvicx7c0KZrCScT7bBfxlKCcpXHkTgbnv5o4o8xiAJV3ei47oEaKcShCjMzGIwNjjBqhK2hBAA7R7z7iuOrLYQD4PATBw1SAnprRYGIecOI2HZBaiMoLvtITCwAAauGnUZ51DaUFZPkeMrMWjoi2vMaS33sFh9h3tDWr2P5XZoW8dmcKRpp92YbQckfUlnHOziyUdxmhpcEeBAgMBAAECggEAAg+ntmwyLPwG8+lIe1Wge09y/6NmsMBOXen+IT8Pn02Bz9iHwhVhuBEiGhZbEPDVImsIruJp1Kwx1TFH7gNT0wj8vTzvgzgt//+JhAsBIDNyRwQUyB7sfU337nQ9NAU6GUCnaKSG/HcYj1rXidpQTdtbKb02h+GQO8bfIwLJ8M/d7Y2EK2/9+jIAvmKSO4ZyGEVrzbgYQC0V8KCKuTx64fErYurQH/aBlhxN0GxTcxeX/y5RkUwfaW55JEhApwm+SpzhjLJJfri14bMcVT8av4yoDGnmLEPBpPnFxR1EQoBJLTtc7TS7u/51I/JJwAe/75OOPSYIQWYVnlOLDng7yQKBgQDTFNNgvwr9zwdInnQfiibUx7TJfKSJqvbEXhsIWXKBCfFyQdu3d9RLd2FgRE3vgsFx3PMaT/ILVs15jPEI/mP4tCwQdCXc+l1NoiuuabUc4UwgY290fa2S5oRrf7ltmuscrC/xCa0Iqaio5dqfZN2XaBdv97ETDvZ3ReUxGsX0HQKBgQDBw5n5Ky2gpbOG0EG79tViF+S5JlTwNzQJjm+YskauZZcFwPAAIzh1z9Kz8lodSoC1wb/rrFmr9PnAbk4Puv9NbNdoIifp9CshonQ3PJyykh/SMTn2wmrQpmIiJIjgOvUjSgyRad6ePzM3hh9bQpD9kkRg73b85eDSqHIhp5g7tQKBgC3e3riti5Pwg6KyXIXmHd4rsAwBPDh2oL23vaQo3AtSv9eWnErYjZgAz3Z+IXmlLqix3VqgePch2/FIQbE0p0EK1nCU7Q2Ckvgl/9wdOLCX/VUkhroH/cposeoyjXdWLTXD7X8yHRo+1Fov6TyuTMF0a3N3nlGH9OOimtX6/X25AoGBAIUv5o6XV22teJGaZRTGvjYHJnj+GDRmPaaz+ZGEOYF24zBZRp9RlmWkzLhURg0MQRyuaTWd6qWAZowXRiEZ8JNP1WEG2Vi/NUaRXED7sNouByF8JNOxH6r8M0g0xMEcxZPUOn9ZvmQYHSR1VOhuASvLdqUK9Ucw3DDxCEKJ6OJhAoGBAKZYojmufECdq4YL5bsHBKo9okdOxzbAguQF4D8R0y7xYOTtQlU5qKQADmfMUxQwD/m+xgGewCoqRk8fDl2CTZDE4pmOrK5LAG2ZMZoTLErX95SmR/9VQgkddfWWKwyBQ1AcxshhTjADT71EYIvq12TJfL8iAcZCqC+4DmFcaYRg";
        //public static String PUBKEY = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYXfu4b7xgDSmEGQpQ8Sn3RzFgl5CE4gL4TbYrND4FtCYOrvbgLijkdFgIrVVWi2hUW4K0PwBsmlYhXcbR+JSmqv9zviVXZiym0lK3glJGVCN86r9EPvNTusZZPm40TOEKMVENSYaUjCxZ7JzeZDfQ4WCeQQr2xirqn6LdJjpZ5wIDAQAB";

        #endregion

        #region 余额分账 BILL

        //正式
        /// <summary>
        /// 平台应用号
        /// </summary>
        public static String APPIDBILL = "JST_MA85ZSSYE2FB4";
        /// <summary>
        /// 平台bizUserId
        /// </summary>
        public static String USERIDBILL = "VA86F86LOSAGW";
        /// <summary>
        /// RSA私钥
        /// </summary>
        public static String PRIKEYBILL = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAPPu3qdFT1JGy7yaH8kbgO2CYwH7977CaRvwFukDpRDfD5oB0o9JYB6mT5kEuJjmA4vfdbdh6qh0UYa1ZRv99UOuDA04Ru8YaHqT9EweUAcN7OvdbJYoAEvaXbPYAmTc/qZikpsWVmahuHBdD/V0vrVeETpyafz9c/KdMYKTkz/7AgMBAAECgYBrt3PC0axxXKnjIvweRnLsbsNhwO5p4lef6qlzRBTEGnw9OcjNVU2Iu1Ay9g6+FfRC8+57iFWjBZpqrfd0IPUIjTz6yeA1CdYJXvR/g9X3zyzbXcVAiVw05zGE1BXsiys9z8koytrtg65mZwK9ODF4Kg1tVv6gi+CaNGnHCfNOIQJBAP25iLqrhBrBWvU1kIyNnKOvVzdR/MM7HiG8/g/o2iLOFBRhkob1La2aGE5eIrOiemBRJxCNE6TTqHtCJ6dHofUCQQD2Htt5OnoCwZjxB9lA0vpzimA4+b4rXxzHHRVEhPX3FjO72rFEyzFk0N72YEMbR3dJ/N66UnWwyrIvxSW+c/QvAkBgvItn+JruhIgEc77ACAIP2ntbSTQgz3pmjKMlN7dri7zWJHl0YShgRx87SeLbMHiOHoLRaahysIxNKmTp/4K9AkEAwElHayU2oeSjGtzo7W5n4dEgcCMeYkBC+YVNUmUWzd54uLGZgYfGpV9ScuauRyfEmPeJA8Tc0izqncvHO07YXQJAP1ix100Clum+p08NrLqgBXOa2Jm87FHb60cKhL6vZNmuxqsArc8ldBe6tu9969urZqsl6BhmuUwlooQhqGR6Xg==";
        /// <summary>
        /// RSA公钥
        /// </summary>
        public static String PUBKEYBILL = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDz7t6nRU9SRsu8mh/JG4DtgmMB+/e+wmkb8BbpA6UQ3w+aAdKPSWAepk+ZBLiY5gOL33W3YeqodFGGtWUb/fVDrgwNOEbvGGh6k/RMHlAHDezr3WyWKABL2l2z2AJk3P6mYpKbFlZmobhwXQ/1dL61XhE6cmn8/XPynTGCk5M/+wIDAQAB";
        /// <summary>
        /// 接口地址
        /// </summary>
        public static String API_URLBILL = "https://pay.xchjst.com/management/gateway.do";
        /// <summary>
        /// 通知地址
        /// </summary>
        public static String NOTIFYURL = "https://test.xchjst.com/api/notify";
        ////测试
        //public static String APPIDBILL = "JST_19GPPP545A5MO";
        //public static String USERIDBILL = "A9GR9JB0JJTOG";
        //public static String PRIKEYBILL = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBALkL6hVbgm+9QaDU/dActpUTfUGRt2/UDzPd3kOM/MsuK36IT8UF7sqiEI3/MCai+GPsTL2ntZ1an/a24p0YRigI4wU9Gf5S7EnQH8Uvc+xMrU1JJfFrdbZkSHEPcxIp1FrUAGd+DHiRG3CU5ZptWc8uftzl855hIHw6Xisvi82bAgMBAAECgYAJEEZU6XiIFJMEV6pe6SkgQCYgcgy0E4TzG2jpkhxHr2k991tA4TuC/VEmQ1uOaOkVq9tOZsqEfI3dPbP30dqNwgctxwpr1Y4GvYsFMUXR0o1QYiYVEKcTMpG93gt39zvgsl482c8oc8fPj/n2cSFBsAh4P3f1YOzTb4kN6/9ZwQJBAO8kiYlQJZLGHhjlztSczZ3zpLOPEn0/n3SkWG/WX0iFz0TG5ZAhx+lGcidquMvSY8+jnJSRec6yVqbb6hNNUEsCQQDGFy58bPJup6mYx9wsWDqQzswyLsds2Ce2ThcLpcocfo+HA/owlnPPhuP3pW9XOa1NI6R9Y6uZbpi1H+KZ/UXxAkA8Bo3HO6jSuIvhb/2EfH9YAEn9EBJyAcBChOX13Hc6OuwVtV712KTXNul8X1tXPc3z1nt9By7t5PG/HEAa7DMVAkBKazkWm6N0eN6ZPDR2IGtYLai/DZ30QTyiG7JCuPU2QUHQmmjqygsWIvoP9oHexhdaTJKmXMSB7u/F1AXAjksxAkEAzq7BCAcJjL5wplq+J91t1OdoSE7EPX2QPC95fdGThNjSr99dfFSgRWH/2YsHixGF0vKteqhZu5+iiXRKNZmvng==";
        //public static String PUBKEYBILL = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDz7t6nRU9SRsu8mh/JG4DtgmMB+/e+wmkb8BbpA6UQ3w+aAdKPSWAepk+ZBLiY5gOL33W3YeqodFGGtWUb/fVDrgwNOEbvGGh6k/RMHlAHDezr3WyWKABL2l2z2AJk3P6mYpKbFlZmobhwXQ/1dL61XhE6cmn8/XPynTGCk5M/+wIDAQAB";
        //public static String API_URLBILL = "https://test.xchjst.com/management/gateway.do";
        //public static String NOTIFYURL = "https://test.xchjst.com/api/notify";
        #endregion
    }
}