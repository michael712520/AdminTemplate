using AdminTemplate.service.Dto;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers
{
    /// <summary>
    /// 外部链接接口
    /// </summary>
    [Route("api/[controller]")]
    public class ExternalLinksController : ControllerBase
    {
        private readonly ExternalLinksService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ExternalLinksController(ExternalLinksService service)
        {
            _service = service;
        }
		/// <summary>
		/// 导师给某个学员做评估的链接
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost, Route("Add")]
        public NetResult Add([FromBody] ExternalLinksAddDto model)
        {
            return _service.Add(model.mbQuestionId, model.teacherIdCard, model.foreignType, model.studentIdCard, model.callBack);
        }

        /// <summary>
        /// 查看某个学员所有评估的列表链接
        /// </summary>
        /// <param name="studentIdCard">学生身份证</param>
        /// <returns></returns>
        [HttpGet, Route("GetStudentList")]
        public NetResult GetStudentList(string studentIdCard)
        {
            return _service.GetStudentList(studentIdCard);
        }
        /// <summary>
        /// 查看某个学员具体某个评估的接口链接
        /// </summary>
        /// <param name="studentIdCard">学生身份证</param>
        /// <param name="mbQuestionId">问卷id</param>
        /// <returns></returns>
        [HttpGet, Route("StudentAndMbQuestion")]
        public NetResult StudentAndMbQuestion(string studentIdCard, string mbQuestionId)
        {
            return _service.StudentAndMbQuestion(studentIdCard, mbQuestionId);
        }

        /// <summary>
        /// 调取某个学员具体某个评估的结果页面查询接口（非链接）
        /// </summary>
        /// <param name="studentIdCard"></param>
        /// <param name="mbQuestionId"></param>
        /// <returns></returns>
        [HttpGet, Route("QuestionResult")]
        public NetResult QuestionResult(string studentIdCard, string mbQuestionId)
        {
            return _service.QuestionResult(studentIdCard, mbQuestionId);
        }
    }
}
