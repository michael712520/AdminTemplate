using AdminTemplate.service.Dto.SysUser;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AdminTemplate.Controllers
{
    [Route("api/[controller]")]
    public class SysUserController : ControllerBase
    {
        private readonly SysUserService _service;

        public SysUserController(SysUserService service)
        {
            _service = service;
        }
        [HttpGet, Route("GetInfoTest")]
        public NetResult GetInfoTest()
        {
            return _service.GetInfoTest();
        }
        [HttpPost, Route("Login")]
        public NetResult Login([FromBody]SysUserLogin sysUserLogin)
        {
            return _service.Login(sysUserLogin);
        }
    }
}
