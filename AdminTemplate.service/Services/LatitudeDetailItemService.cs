using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.baseReEntity;
using AdminTemplate.service.Dto.LatitudeDetailItem;
using AutoMapper;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class LatitudeDetailItemService : BaseService
    {

        public NetResult Add(LatitudeDetailItemDto from)
        {
            if (from.Id != null)
            {
                return Update(from);
            }
            else
            {
                var model = Mapper.Map<LatitudeDetailItem>(from);
                model.Id = Guid.NewGuid().ToString("N");
                model.MbDetailId = from.MbDetailId;
                DbContext.LatitudeDetailItem.Add(model);
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }


        }

        public NetResult Delete(string id)
        {

            var model = DbContext.LatitudeDetailItem.FirstOrDefault(p => p.Id.Equals(id));
            if (model != null)
            {
                DbContext.LatitudeDetailItem.Remove(model);
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }
            else
            {
                return ResponseBodyEntity("", EnumResult.Error, "id未找到对象");
            }


        }
        public NetResult Update(LatitudeDetailItemDto model)
        {
            var m = DbContext.LatitudeDetailItem.FirstOrDefault(p => p.Id.Equals(model.Id));
            if (m != null)
            {
                if (model.Name != null)
                {
                    m.Name = model.Name;
                }
                if (model.BaseScore != null)
                {
                    m.BaseScore = model.BaseScore;
                }
                if (model.Score != null)
                {
                    m.Score = model.Score;
                }
                if (model.Coefficient != null)
                {
                    m.Coefficient = model.Coefficient;
                }
                if (model.Sort != null)
                {
                    m.Sort = model.Sort;
                }
                DbContext.LatitudeDetailItem.Update(m);
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }

            return ResponseBodyEntity("", EnumResult.Error, "id获取对象为空");
        }
        public NetResult List(string id, PaginationStartAndLengthFilter filter)
        {
            var query = DbContext.LatitudeDetailItem.AsNoTracking();

            if (filter.Keywords != null)
            {
                query = query.Where(p => p.Name.Contains(filter.Keywords));
            }



            var count = query.Count();
            var list = query.Skip(filter.Start).Take(filter.Length).OrderByDescending(o => o.Sort).ToList();
            return ResponseBodyEntity(list, count);
        }

        public NetResult GetPicker(string id)
        {
            var list = DbContext.LatitudeDetailItem.AsNoTracking().Where(p => p.MbDetailId.Equals(id)).ToList();
            var data = Mapper.Map<List<PairReEntity>>(list);
            return ResponseBodyEntity(data);
        }

    }
}
