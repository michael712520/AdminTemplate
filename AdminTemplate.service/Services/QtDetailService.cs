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
            var model = DbContext.QtDetail.AsNoTracking().Include(o => o.QtDetailItem).FirstOrDefault(p => p.Id.Equals(id));
            if (model != null)
            {
                model.QtDetailItem = model.QtDetailItem.OrderBy(o => o.Order).ToList();
                return ResponseBodyEntity(model);
            }
            return ResponseBodyEntity("", EnumResult.Error, "对象不存在");
        }
    }
}
