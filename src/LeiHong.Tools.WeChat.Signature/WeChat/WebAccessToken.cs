using System;
using System.Collections.Generic;
using System.Text;

namespace LeiHong.Tools.WeChat.Signature.WeChat
{
    public class WebAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
}
