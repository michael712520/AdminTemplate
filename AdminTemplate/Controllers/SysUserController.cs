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
        public NetResult Login(string username, string password)
        {
            return _service.Login(username, password);
        }
    }
}
