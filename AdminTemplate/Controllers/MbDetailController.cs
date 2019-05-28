using AdminTemplate.service.Dto.MbDetail;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers
{

    [Route("api/[controller]")]
    public class MbDetailController : ControllerBase
    {
        private readonly MbDetailService _service;
        public MbDetailController(MbDetailService service)
        {
            _service = service;
        }
        [HttpGet, Route("Get")]
        public NetResult Get(string id)
        {
            return _service.Get(id);
        }
        [HttpPost, Route("Update")]
        public NetResult Update([FromBody]MbDetailDto form)
        {
            return _service.Update(form);
        }

        [HttpGet, Route("GetList")]
        public NetResult GetList(string userId, PaginationStartAndLengthFilter filter)
        {
            return _service.GetList(userId, filter);
        }
        [HttpPost, Route("Save")]
        public NetResult Save([FromBody]MbDetailDto form)
        {

            return _service.Save(form);
        }
        [HttpGet, Route("GetListItem")]
        public NetResult GetListItem(string detailId)
        {
            return _service.GetListItem(detailId);
        }
        [HttpPost, Route("SaveItem")]
        public NetResult SaveItem([FromBody]MbDetailItemDto form)
        {

            return _service.SaveItem(form);
        }
    }
}
