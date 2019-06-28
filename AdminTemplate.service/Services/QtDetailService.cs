using AdminTemplate.service.BaseServices;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class QtDetailService : BaseService
    {
        public NetResult Get(string id)
        {
            var model = DbContext.QtDetail.Include(o => o.QtDetailItem).FirstOrDefault(p => p.Id.Equals(id));
            return ResponseBodyEntity(model);
        }
    }
}
