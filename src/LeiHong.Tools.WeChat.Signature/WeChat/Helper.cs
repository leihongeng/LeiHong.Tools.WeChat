using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Flurl.Http;

namespace LeiHong.Tools.WeChat.Signature.WeChat
{
    public class Helper
    {
        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            var filepath = Path.Join(Environment.CurrentDirectory, "Token.xml");
            var xml = new XmlDocument();
            xml.Load(filepath);
            var accessToken = xml.SelectSingleNode("xml")?.SelectSingleNode("AccessToken")?.InnerText;
            var accessTokenExpires = DateTime.Parse(xml.SelectSingleNode("xml")?.SelectSingleNode("AccessTokenExpires")?.InnerText);
            if (DateTime.Now >= accessTokenExpires)
            {
                var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={ApplicationSetting.WxAppId}&secret={ApplicationSetting.WxAppSecret}";

                var result = url.GetAsync().Result.GetJsonAsync<AccessToken>().Result;
                if (result.errcode.GetValueOrDefault() != 0) throw new Exception(result.errmsg);
                xml.SelectSingleNode("xml").SelectSingleNode("AccessToken").InnerText = result.access_token;
                xml.SelectSingleNode("xml").SelectSingleNode("AccessTokenExpires").InnerText = DateTime.Now.AddSeconds(result.expires_in).ToString("yyyy-MM-dd HH:mm:ss");
                xml.Save(filepath);
                return result.access_token;
            }

            return accessToken;
        }


        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        /// <returns></returns>
        public static string GetJsapiTicket()
        {
            var filepath = Path.Join(Environment.CurrentDirectory, "Token.xml");
            var xml = new XmlDocument();
            xml.Load(filepath);
            var jsapiTicket = xml.SelectSingleNode("xml")?.SelectSingleNode("JsapiTicket")?.InnerText;
            var jsapiTicketExpires = DateTime.Parse(xml.SelectSingleNode("xml")?.SelectSingleNode("JsapiTicketExpires")?.InnerText);
            if (DateTime.Now >= jsapiTicketExpires)
            {
                var accessToken = GetAccessToken();
                var url = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={accessToken}&type=jsapi";
                var result = url.GetAsync().Result.GetJsonAsync<TicketModel>().Result;
                if (result.errcode.GetValueOrDefault() != 0) throw new Exception(result.errmsg);
                xml.SelectSingleNode("xml").SelectSingleNode("JsapiTicket").InnerText = result.ticket;
                xml.SelectSingleNode("xml").SelectSingleNode("JsapiTicketExpires").InnerText = DateTime.Now.AddSeconds(result.expires_in).ToString("yyyy-MM-dd HH:mm:ss");
                xml.Save(filepath);
                return result.ticket;
            }

            return jsapiTicket;
        }

        public static string CreateNonceStr()
        {
            const int length = 16;
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var str = "";
            var rad = new Random();
            for (var i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }

        public static int ConvertDateTimeInt(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return Convert.ToInt32((time - startTime).TotalSeconds);
        }

        public static string Sha1Encrypt(string source, bool isReplace = true, bool isToLower = true)
        {
            var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(source));
            var shaStr = BitConverter.ToString(hash);
            if (isReplace)
            {
                shaStr = shaStr.Replace("-", "");
            }
            if (isToLower)
            {
                shaStr = shaStr.ToLower();
            }
            return shaStr;
        }
    }
}
