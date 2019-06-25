using AdminTemplate.service.Dto.SysUser;
using AdminTemplate.service.Services;
using GlobalConfiguration.Utility;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost, Route("Login")]
        public NetResult Login([FromBody]SysUserLogin sysUserLogin)
        {
            return _service.Login(sysUserLogin);
        }
        [HttpPost, Route("authorization")]
        public NetResult Authorization(string kyeId)
        {
            return _service.Authorization(kyeId);
        }
    }
}
