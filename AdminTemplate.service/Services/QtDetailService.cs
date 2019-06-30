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
    }
}
