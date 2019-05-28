using AdminTemplate.service.BaseServices;
using GlobalConfiguration.Utility;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class SysUserService : BaseService
    {
        public NetResult Login(string username, string password)
        {
            var model = DbContext.SysUser.FirstOrDefault(p => p.Username.Equals(username) && p.Password.Equals(password));
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
