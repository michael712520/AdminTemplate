using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminTemplate.DataBase.Models;
using AdminTemplate.service.Dto.LatitudeCategory;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminTemplate.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route("api/[controller]")]

	public class LatitudeCategoryController : ControllerBase
	{
		private LatitudeCategoryService _service;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="service"></param>
		public LatitudeCategoryController(LatitudeCategoryService service)
		{
			_service = service;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mbDetailId"></param>
		/// <returns></returns>
		[HttpGet, Route("List")]
		public NetResult List(string mbDetailId)
		{
			return _service.List(mbDetailId);


		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="form"></param>
		/// <returns></returns>
		[HttpPost, Route("SaveOrUpdate")]
		public NetResult SaveOrUpdate([FromBody]LatitudeCategoryDto form)
		{
			return _service.SaveOrUpdate(form);
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
	}
}