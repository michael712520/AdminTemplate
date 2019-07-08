using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers
{
    [Route("api/[controller]")]
     public class WeChatController : ControllerBase
     {
	     private readonly WeChatService _service;

	     public WeChatController(WeChatService service)
	     {
		     _service = service;
	     }
	     [HttpGet, Route("jscode2session/{appid}/{secret}/{js_code}")]
		public NetResult jscode2session(string appid, string secret, string js_code,
		     string grant_type = "authorization_code")
	     {
		     return _service.jscode2session(appid, secret, js_code, grant_type);

	     }

	 }
}