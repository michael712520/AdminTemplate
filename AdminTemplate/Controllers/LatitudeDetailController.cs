using AdminTemplate.service.Dto.LatitudeDetail;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers
{
    [Route("api/[controller]")]
    public class LatitudeDetailController : ControllerBase
    {
        private readonly LatitudeDetailService _service;



        public LatitudeDetailController(LatitudeDetailService service)
        {
            _service = service;
        }
        [HttpPost, Route("Add")]
        public NetResult Add([FromBody]LatitudeDetailDto model)
        {


            return _service.Add(model);
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
            return _service.GetPicker();
        }
    }
}
