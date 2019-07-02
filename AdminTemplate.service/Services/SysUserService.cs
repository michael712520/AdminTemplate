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
                return ResponseBodyEntity(new
                {
                    model,
                    token = new
                    {
                        access_token = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImNiZTJkZjg5MWQwYjAwZDYxYjBkMzVmNzMyMzFiY2ZmIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NjIwNzcxNTIsImV4cCI6MTU2MjA4MDc1MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MTAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTEwMC9yZXNvdXJjZXMiLCJhcGkiLCJBcHBBcGkiLCJXZWJBcGkiLCJXeEFwaSJdLCJjbGllbnRfaWQiOiIxMjMiLCJzY29wZSI6WyJhcGkiLCJBcHBBcGkiLCJXZWJBcGkiLCJXeEFwaSJdfQ.YTTiXrTJ3BCOmUgQoKNnA3AyLw_qefmWt42H14rWHjOFFf_EqzypDcSA5lNMBbx5mMVAbrtzr6xLYF2K-yPVMCZwZf8QV_3xj0hFTTIAozft5HbrySsNL9yIXaUS4FI3ddV6KOfTxGzG5EU05cjzktTxMsYBkVZ04spkwblWzZZliTJNzpQFxnhse2T6VJ9LasfeGUPxiDZ4LfFFqwYf3EdqzIlIxjS_Jvp7SVnqZchhYpraQhPEEfwSAh1-_pwtdJNJxV2QUo8DqILwjaGaxPPCh2wSr3YK_JUjoaFsYoMEWf3V6uMYbbI-R6Yud9gWnhN16Ql4zWdhWl9QPxdq4A",
                        expires_in = 3600,
                        token_type = "Bearer"
                    }
                });
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
