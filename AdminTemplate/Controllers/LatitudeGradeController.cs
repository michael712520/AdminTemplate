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
	/// <summary>
	/// 
	/// </summary>
	[Route("api/[controller]")]
	public class LatitudeGradeController : ControllerBase
	{
		private readonly LatitudeGradeService _service;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="service"></param>
		public LatitudeGradeController(LatitudeGradeService service)
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
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost, Route("Delete/{id}")]
		public NetResult Delete(string id)
		{
			return _service.Delete(id);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="from"></param>
		/// <returns></returns>
		[HttpPost, Route("SaveUpdate")]
		public NetResult SaveUpdate([FromBody]MbGradeDto from)
		{
			return _service.SaveUpdate(from);
		}

	}
}