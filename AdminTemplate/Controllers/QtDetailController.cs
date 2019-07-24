using AdminTemplate.service.Dto.QtDetailItem;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AdminTemplate.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route("api/[controller]")]
	public class QtDetailController : ControllerBase
	{
		private readonly QtDetailService _service;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="service"></param>
		public QtDetailController(QtDetailService service)
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
		/// 根据id获取问卷结果
		/// </summary>
		/// <param name="id"></param>
		/// <param name="batchNumber"></param>
		/// <returns></returns>
		[HttpGet, Route("GetResult/{id}/{batchNumber}")]
		public NetResult GetResult(string id, string batchNumber)
		{
			return _service.GetResult(id, batchNumber);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="studentIdCard"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		[HttpGet, Route("GetStudentAll/{studentIdCard}")]
		public NetResult GetStudentAll(string studentIdCard, PaginationStartAndLengthFilter filter)
		{
			return _service.GetStudentAll(studentIdCard, filter);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="studentIdCard"></param>
		/// <param name="mbDetailId"></param>
		/// <returns></returns>
		[HttpGet, Route("GetByStudentAndMbDetailId/{studentIdCard}/{mbDetailId}")]
		public NetResult GetByStudentAndMbDetailId(string studentIdCard, string mbDetailId)
		{
			return _service.GetByStudentAndMbDetailId(studentIdCard, mbDetailId);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="listParam"></param>
		/// <returns></returns>
		[HttpPost, Route("UpdateSelectResult")]
		public NetResult UpdateSelectResult([FromBody]List<QtDetailItemDto> listParam)
		{
			return _service.UpdateSelectResult(listParam);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="studentIdCard"></param>
		/// <param name="mbId"></param>
		/// <returns></returns>
		[HttpGet, Route("SelectResultSimple")]
		public NetResult SelectResultSimple(string id, string studentIdCard, string mbId)
		{
			return _service.SelectResultSimple(id, studentIdCard, mbId);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mbDetailId"></param>
        /// <returns></returns>
        [HttpGet, Route("SelectResult")]
        public NetResult SelectResult(string mbDetailId, PaginationStartAndLengthFilter filter)
        {
            return _service.SelectResult(mbDetailId, filter);
        }


    }
}
