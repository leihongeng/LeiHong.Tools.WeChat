using System;
using System.Collections.Generic;
using System.Text;

namespace LeiHong.Tools.WeChat.Signature.WeChat
{
    public class TicketModel
    {
        public int? errcode { get; set; }

        public string errmsg { get; set; }

        public string ticket { get; set; }

        public int expires_in { get; set; }
    }
}
