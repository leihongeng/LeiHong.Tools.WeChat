using System;
using System.Collections.Generic;
using System.Text;

namespace LeiHong.Tools.WeChat.Signature.WeChat
{
    public class AccessToken
    {
        public string access_token { get; set; }

        public double expires_in { get; set; }

        public int? errcode { get; set; }

        public string errmsg { get; set; }
    }
}
