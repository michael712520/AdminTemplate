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
        /// <summary>
        /// 讲openid和填写试题进行绑定
        /// </summary>
        /// <param name="qtDetailId"></param>
        /// <param name="batchNumber"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet, Route("UpdateQtDetail/{qtDetailId}/{batchNumber}/{openid}")]
        public NetResult UpdateQtDetail(string qtDetailId, string batchNumber, string openid)
        {
            return _service.UpdateQtDetail(qtDetailId, batchNumber, openid);
        }
        /// <summary>
        /// 根据openid查询问卷列表
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ListQtDetailbatch/{openid}")]
        public NetResult ListQtDetailbatch(string openid, PaginationStartAndLengthFilter filter)
        {
            return _service.ListQtDetailbatch(openid, filter);
        }
     }
}