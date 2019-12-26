using System.Collections;

namespace LeiHong.Tools.WeChat.Signature.Abstractions
{
    public interface IWeChatSignatureService
    {
        Hashtable GetSign(string url);
    }
}
