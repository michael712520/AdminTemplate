using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminTemplate.service.Dto.MbGrade;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers
{
    [Route("api/[controller]")]
    public class MbGradeController : ControllerBase
    {
	    private readonly MbGradeService _service;

	    public MbGradeController(MbGradeService service)
	    {
		    _service = service;
	    }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
	    [HttpGet, Route("Get/{id}")]
		public NetResult Get(string id)
	    {
		   return _service.Get(id);

	    }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet, Route("GetMbDetail/{id}")]
		public NetResult GetMbDetail(string id)
		{
			return _service.GetMbDetail(id);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="from"></param>
		/// <returns></returns>
		[HttpPost, Route("GetMbDetail")]
		public NetResult SaveUpdate([FromBody]MbGradeDto from)
		{
			return _service.SaveUpdate(from);
		}

	}
}