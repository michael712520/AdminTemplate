using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers
{
    [Route("api/[controller]")]
    public class ExternalLinksController : ControllerBase
    {
        private readonly ExternalLinksService _service;

        public ExternalLinksController(ExternalLinksService service)
        {
            _service = service;
        }
        [HttpPost, Route("Add/{mbQuestionId}/{teacherIdCard}/{foreignType}/{studentIdCard}")]
        public NetResult Add(string mbQuestionId, string teacherIdCard, string foreignType, string studentIdCard)
        {
            return _service.Add(mbQuestionId, teacherIdCard, foreignType, studentIdCard);
        }

    }
}
