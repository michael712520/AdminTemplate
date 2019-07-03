using AdminTemplate.service.Dto.QtDetailItem;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;

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
        /// <param name="from"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateAll")]
        public NetResult UpdateAll([FromBody]QtDetailItemFrom from)
        {
            return _service.UpdateAll(from);
        }
    }
}
