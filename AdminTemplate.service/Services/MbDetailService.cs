using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.MbDetail;
using AutoMapper;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class MbDetailService : BaseService
    {
        public NetResult Get(string id)
        {
            var query = DbContext.MbDetail.AsNoTracking();
            var model = query.FirstOrDefault(p => p.Id.Equals(id));
            return ResponseBodyEntity(model);
        }
        public NetResult Update(MbDetailDto form)
        {
            var query = DbContext.MbDetail.AsNoTracking();
            var model = query.FirstOrDefault(p => p.Id.Equals(form.Id));
            if (model != null)
            {
                model.Title = form.Title;
                model.Content = form.Content;
                model = DbContext.MbDetail.Update(model).Entity;
                DbContext.SaveChanges();
                return ResponseBodyEntity(model);
            }
            else
            {
                return ResponseBodyEntity("", EnumResult.Error, "id未找到");
            }
        }
        public NetResult UpdateMbDetail(UpdateMbDetailDto form)
        {
            var model = DbContext.MbDetailItem.FirstOrDefault(p =>
                p.DetailId.Equals(form.DetailId) && p.Order == form.Sort);
            MbDetailItem model2;
            if (form.Type == 0)
            {
                model2 = DbContext.MbDetailItem.FirstOrDefault(p =>
                  p.DetailId.Equals(form.DetailId) && p.Order == (form.Sort - 1));
            }
            else
            {
                model2 = DbContext.MbDetailItem.FirstOrDefault(p =>
                  p.DetailId.Equals(form.DetailId) && p.Order == (form.Sort + 1));
            }

            if (model != null && model2 != null)
            {
                var i = model2.Order;
                model2.Order = model.Order;
                model.Order = i;
                DbContext.MbDetailItem.Update(model2);
                DbContext.MbDetailItem.Update(model);
            }

            DbContext.SaveChanges();
            return ResponseBodyEntity("", EnumResult.Error, "id未找到");

        }
        public NetResult Delete(string id)
        {

            var model = DbContext.MbDetailItem.FirstOrDefault(p => p.Id.Equals(id));
            if (model == null) return ResponseBodyEntity("", EnumResult.Error, "id不存在");
            DbContext.MbDetailItem.Remove(model);
            DbContext.SaveChanges();
            return ResponseBodyEntity();

        }
        public NetResult GetList(string userId, PaginationStartAndLengthFilter filter)
        {
            var query = DbContext.MbDetail.AsNoTracking();
            query = query.Where(p => p.UserId.Equals(userId));
            query = query.Where(p => p.UserId.Equals(userId));
            int count = query.Count();
            var list = query.Skip(filter.Start).Take(filter.Length).ToList();
            return ResponseBodyEntity(list, count);
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
        public NetResult SaveItem(MbDetailItemDto form)
        {
            MbDetailItem model = Mapper.Map<MbDetailItem>(form);
            if (form.Id == null)
            {
                model.Id = Guid.NewGuid().ToString("N");
                DbContext.MbDetailItem.Add(model);
            }
            else
            {
                DbContext.MbDetailItem.Update(model);

            }
            DbContext.SaveChanges();
            return ResponseBodyEntity();
        }
    }
}
