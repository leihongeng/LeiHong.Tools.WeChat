using System;
using System.Collections;
using System.Net.Http;
using Flurl.Http;
using LeiHong.Tools.WeChat.Signature.Abstractions;
using LeiHong.Tools.WeChat.Signature.WeChat;
using Microsoft.AspNetCore.Http;

namespace LeiHong.Tools.WeChat.Signature.Implements
{
    public class WeChatSignatureService : IWeChatSignatureService
    {
        public Hashtable GetSign(string url)
        {
            if (url.Contains('#')) url = url.Split('#')[0];
            var jsTicketTicket =  Helper.GetJsapiTicket();
            var timestamp = Convert.ToString(Helper.ConvertDateTimeInt(DateTime.Now));
            var nonceStr = Helper.CreateNonceStr();

            var ticket = $"jsapi_ticket={jsTicketTicket}&noncestr={nonceStr}&timestamp={timestamp}&url={url}";
            var signature = Helper.Sha1Encrypt(ticket);
            return new Hashtable
            {
                {"appId", ApplicationSetting.WxAppId},
                {"nonceStr", nonceStr},
                {"timestamp", timestamp},
                {"url", url},
                {"signature", signature},
                {"JSTicketTicket", jsTicketTicket}
            };
        }

        public WebAccessToken Login(string code)
        {
            var url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={ApplicationSetting.WxAppId}&secret={ApplicationSetting.WxAppSecret}&code={code}&grant_type=authorization_code";
            return url.GetAsync().Result.GetJsonAsync<WebAccessToken>().Result;
        }
    }
}
