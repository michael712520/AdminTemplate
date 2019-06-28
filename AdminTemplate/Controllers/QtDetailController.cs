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
    }
}
