﻿using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.LatitudeDetail;
using AutoMapper;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class LatitudeDetailService : BaseService
    {
        public NetResult Add(LatitudeDetailDto from)
        {
            if (from.Id != null)
            {
                return Update(from);
            }
            else
            {
                var model = Mapper.Map<LatitudeDetail>(from);
                model.Id = Guid.NewGuid().ToString("N");
                model.MbDetailId = from.MbDetailId;
                DbContext.LatitudeDetail.Add(model);
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }


        }
        public NetResult Get(string id)
        {
            var model = DbContext.LatitudeDetail.Include(o => o.MbDetailItem).FirstOrDefault(p => p.Id.Equals(id));
            return ResponseBodyEntity(model);

        }
        public NetResult Delete(string id)
        {

            var model = DbContext.LatitudeDetail.FirstOrDefault(p => p.Id.Equals(id));
            if (model != null)
            {
                DbContext.LatitudeDetail.Remove(model);
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }
            else
            {
                return ResponseBodyEntity("", EnumResult.Error, "id未找到对象");
            }


        }
        public NetResult Update(LatitudeDetailDto model)
        {
            var m = DbContext.LatitudeDetail.FirstOrDefault(p => p.Id.Equals(model.Id));
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
                if (model.Relationship != null)
                {
                    m.Relationship = model.Relationship;
                }
                DbContext.LatitudeDetail.Update(m);
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }

            return ResponseBodyEntity("", EnumResult.Error, "id获取对象为空");
        }
        public NetResult List(string id, PaginationStartAndLengthFilter filter)
        {
            var query = DbContext.LatitudeDetail.AsNoTracking();

            if (filter.Keywords != null)
            {
                query = query.Where(p => p.Name.Contains(filter.Keywords));
            }



            var count = query.Count();
            var list = query.Skip(filter.Start).Take(filter.Length).OrderByDescending(o => o.Sort).ToList();
            return ResponseBodyEntity(list, count);
        }

    }
}
