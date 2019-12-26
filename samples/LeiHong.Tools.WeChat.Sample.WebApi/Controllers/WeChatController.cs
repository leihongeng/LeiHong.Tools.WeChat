using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LeiHong.Tools.WeChat.Signature.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace LeiHong.Tools.WeChat.Sample.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeChatController : ControllerBase
    {
        private readonly IWeChatSignatureService _chatSignatureService;

        public WeChatController(IWeChatSignatureService chatSignatureService)
        {
            _chatSignatureService = chatSignatureService;
        }

        [HttpGet]
        public IActionResult GetSign(string url)
        {
            if (string.IsNullOrEmpty(url)) url = HttpContext.Request.Host.Host;
            return Ok(_chatSignatureService.GetSign(url));
        }

        [HttpGet]
        public IActionResult Login(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new Exception("code不能为空！");
            return Ok(_chatSignatureService.Login(code));
        }
    }
}
