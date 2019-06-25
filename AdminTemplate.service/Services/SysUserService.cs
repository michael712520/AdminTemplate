using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.SysUser;
using GlobalConfiguration.@base;
using GlobalConfiguration.Utility;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class SysUserService : BaseService
    {
        public NetResult Login(SysUserLogin sysUserLogin)
        {
            var model = DbContext.SysUser.FirstOrDefault(p => p.Username.Equals(sysUserLogin.Username) && p.Password.Equals(CryptUtils.MD5(sysUserLogin.Password)));
            if (model == null)
            {
                return ResponseBodyEntity("", EnumResult.Error, "失败");
            }
            else
            {
                var data = AppConfig.GetToken();
                if (data.Code == EnumResult.Error)
                {
                    return ResponseBodyEntity("", EnumResult.Error, "获取toke错误");
                }
                else
                {
                    return ResponseBodyEntity(new { model, token = data.Data });
                }


                return ResponseBodyEntity(model);
            }


        }

        public NetResult Authorization(string kyeId)
        {
            var authorization = AppConfig.GetAppSettings("authorization");

            if (authorization.Value.Equals(kyeId))
            {
                var data = AppConfig.GetToken();
                return ResponseBodyEntity(data.Data);
            }
            else { return ResponseBodyEntity("", EnumResult.Error, "授权码不正确!"); }


        }


    }
}
