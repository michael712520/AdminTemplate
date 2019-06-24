using AdminTemplate.service.Dto.MbDetail;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        [HttpPost, Route("SaveUpdate")]
        public NetResult SaveUpdate([FromBody]MbDetailDto form)
        {
            return _service.SaveUpdate(form);
        }
        [HttpPost, Route("UpdateMbDetail")]
        public NetResult UpdateMbDetail([FromBody]UpdateMbDetailDto form)
        {
            return _service.UpdateMbDetail(form);
        }

        [HttpPost, Route("Delete/{id}")]
        public NetResult Delete(string id)
        {
            return _service.Delete(id);
        }
        [HttpPost, Route("DeleteItem/{id}")]
        public NetResult DeleteItem(string id)
        {
            return _service.DeleteItem(id);
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
        [HttpPost, Route("ListSaveItem")]
        public NetResult ListSaveItem([FromBody]List<MbDetailItemDto> list)
        {

            return _service.ListSaveItem(list);
        }
        [HttpPost, Route("SaveItem")]
        public NetResult SaveItem([FromBody]MbDetailItemDto form)
        {

            return _service.SaveItem(form);
        }
    }
}
