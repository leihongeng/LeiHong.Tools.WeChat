using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using LeiHong.Tools.WeChat.Signature.Abstractions;
using LeiHong.Tools.WeChat.Signature.Implements;
using Microsoft.AspNetCore.Http;

namespace LeiHong.Tools.WeChat.Signature
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWeChatSignatureService(this IServiceCollection services, Action<WeChatSignatureOptionsBuilder> options)
        {
            services.AddScoped<IWeChatSignatureService, WeChatSignatureService>();
            var data = new WeChatSignatureOptionsBuilder();
            options.Invoke(data);
            ApplicationSetting.WxAppId = data.WxAppId;
            ApplicationSetting.WxAppSecret = data.WxAppSecret;
            return services;
        }
    }
}
