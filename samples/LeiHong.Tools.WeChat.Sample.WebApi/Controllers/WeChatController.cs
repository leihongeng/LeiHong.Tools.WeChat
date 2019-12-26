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
        public IActionResult GetSign()
        {
            return Ok(_chatSignatureService.GetSign(HttpContext.Request.Host.Host));
        }

        [HttpGet]
        public IActionResult GetSign(string url)
        {
            return Ok(_chatSignatureService.GetSign(url));
        }
    }
}
