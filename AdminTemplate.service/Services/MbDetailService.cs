using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.MbDetail;
using AutoMapper;
using GlobalConfiguration.Utility;
using System;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class MbDetailService : BaseService
    {
        public NetResult GetList(string userId)
        {
            var list = DbContext.MbDetail.Where(p => p.UserId.Equals(userId)).ToList();

            return ResponseBodyEntity(list);
        }
        public NetResult Save(MbDetailDto form)
        {
            MbDetail model = Mapper.Map<MbDetail>(form);
            model.Id = Guid.NewGuid().ToString("N");
            DbContext.MbDetail.Add(model);
            DbContext.SaveChanges();
            return ResponseBodyEntity();
        }
        public NetResult GetListItem(string detailId)
        {
            var list = DbContext.MbDetailItem.Where(p => p.DetailId.Equals(detailId)).ToList();

            return ResponseBodyEntity(list);
        }
        public NetResult SaveItem(MbDetailItem form)
        {

            DbContext.MbDetailItem.Add(form);
            return ResponseBodyEntity();
        }
    }
}
