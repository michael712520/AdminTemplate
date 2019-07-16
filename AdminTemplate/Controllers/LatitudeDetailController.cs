using System.Collections.Generic;
using AdminTemplate.service.Dto.LatitudeDetail;
using AdminTemplate.service.Dto.MbDetail;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route("api/[controller]")]
	public class LatitudeDetailController : ControllerBase
	{
		private readonly LatitudeDetailService _service;



		/// <summary>
		/// 
		/// </summary>
		/// <param name="service"></param>
		public LatitudeDetailController(LatitudeDetailService service)
		{
			_service = service;
		}
		[HttpPost, Route("Add")]
		public NetResult Add([FromBody]LatitudeDetailDto model)
		{


			return _service.Add(model);
		}
		[HttpGet, Route("Get")]
		public NetResult Get(string id)
		{
			return _service.Get(id);

		}
		[HttpPost, Route("Delete/{id}")]
		public NetResult Delete(string id)
		{
			return _service.Delete(id);
		}
		[HttpPost, Route("Update")]
		public NetResult Update([FromBody]LatitudeDetailDto model)
		{
			return _service.Update(model);
		}
		[HttpGet, Route("List")]
		public NetResult List(string id, PaginationStartAndLengthFilter filter)
		{
			return _service.List(id, filter);
		}
		[HttpGet, Route("GetPicker")]
		public NetResult GetPicker()
		{
			return null;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mbDetailId"></param>
		/// <returns></returns>
		[HttpGet, Route("GetListLat")]
		public NetResult GetListLat(string mbDetailId)
		{
			return _service.GetListLat(mbDetailId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		[HttpPost, Route("UpdateItemMbDetailItem")]
		public NetResult UpdateItemMbDetailItem([FromBody]List<UpdateMbDetailItemDto> list)
		{
			return _service.UpdateItemMbDetailItem(list);

		}
	}
}
