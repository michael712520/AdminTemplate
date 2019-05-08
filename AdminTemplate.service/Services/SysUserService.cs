using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.SysUser;
using GlobalConfiguration.@base;
using GlobalConfiguration.Utility;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class SysUserService : BaseService
    {
        public NetResult GetInfoTest()
        {
            var query = DbContext.SysUser;
            return ResponseBodyEntity(query.ToList());
        }
        public NetResult Login(SysUserLogin sysUserLogin)
        {
            var sysUser = DbContext.SysUser.FirstOrDefault(p => p.UserName.Equals(sysUserLogin.UserName) && p.Userpwd.Equals(sysUserLogin.Password));
            if (sysUser != null)
            {
                var data = AppConfig.GetToken();
                if (data.Code == EnumResult.Error)
                {
                    return ResponseBodyEntity("", EnumResult.Error, "获取toke错误");
                }
                else
                {
                    return ResponseBodyEntity(new { sysUser, token = data.Data });
                }


            }
            else
            {
                return ResponseBodyEntity("", EnumResult.Error, "用户名或者密码错误");
            }


        }
    }
}
