using System.Collections;
using LeiHong.Tools.WeChat.Signature.WeChat;

namespace LeiHong.Tools.WeChat.Signature.Abstractions
{
    public interface IWeChatSignatureService
    {
        Hashtable GetSign(string url);

        WebAccessToken Login(string code);
    }
}
