using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.SysUser;
using GlobalConfiguration.Utility;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class SysUserService : BaseService
    {
        public NetResult Login(SysUserLogin sysUserLogin)
        {
            var model = DbContext.SysUser.FirstOrDefault(p => p.Username.Equals(sysUserLogin.Username) && p.Password.Equals(sysUserLogin.Password));
            if (model == null)
            {
                return ResponseBodyEntity("", EnumResult.Error, "失败");
            }
            else
            {
                return ResponseBodyEntity(model);
            }


        }
    }
}
