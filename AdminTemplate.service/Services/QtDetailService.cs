using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.LatitudeDetailItem;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public NetResult GetStudentAll(string studentIdCard, PaginationStartAndLengthFilter filter)
        {
            var query = DbContext.QtDetail.Where(p => p.StudentIdCard.Equals(studentIdCard));

            var count = query.Count();
            var list = query.Skip(filter.Start).Take(filter.Length).ToList();
            return ResponseBodyEntity(list, count);
        }
        public NetResult GetByStudentAndMbDetailId(string studentIdCard, string mbDetailId)
        {
            var model = DbContext.QtDetail.FirstOrDefault(p => p.StudentIdCard.Equals(studentIdCard) && p.MbDetailId.Equals(mbDetailId));
            return ResponseBodyEntity(model);
        }

        public NetResult UpdateQtDetailItem(List<LatitudeDetailItemDto> list)
        {
            list.ForEach(d =>
            {
                var model = DbContext.QtDetailItem.FirstOrDefault(p => p.Id.Equals(d.Id));
                if (model != null)
                {
                    model.SelectResult = d.SelectResult;
                    DbContext.QtDetailItem.Update(model);
                }
            });
            DbContext.SaveChanges();
            return ResponseBodyEntity();
        }
    }
}
