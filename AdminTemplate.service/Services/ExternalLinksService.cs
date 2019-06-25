using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AutoMapper;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminTemplate.service.Services
{
    public class ExternalLinksService : BaseService
    {
        public NetResult Add(string mbQuestionId, string teacherIdCard, string foreignType, string studentIdCard)
        {
            var model = DbContext.MbDetail.Include(o => o.MbDetailItem).FirstOrDefault(p => p.Id.Equals(mbQuestionId));
            if (model == null)
            {
                return ResponseBodyEntity("", EnumResult.Error, "没有找到对应的问卷");
            }

            if (teacherIdCard == null || foreignType == null || studentIdCard == null)
            {
                return ResponseBodyEntity("", EnumResult.Error, "确实对应的信息");
            }

            QtDetail qtDetail = Mapper.Map<QtDetail>(model);
            qtDetail.Id = Guid.NewGuid().ToString("N");
            qtDetail.TeacherIdCard = teacherIdCard;
            qtDetail.ForeignType = foreignType;
            qtDetail.StudentIdCard = studentIdCard;
            List<QtDetailItem> items = Mapper.Map<List<QtDetailItem>>(model.MbDetailItem);
            items.ForEach(d =>
            {
                d.Id = Guid.NewGuid().ToString("N");
                d.QtDetailId = qtDetail.Id;
                d.MbDetailId = model.Id;
                qtDetail.QtDetailItem.Add(d);
            });

            DbContext.QtDetail.Add(qtDetail);
            DbContext.SaveChanges();
            return ResponseBodyEntity("https://www.iu1314.com/#/ExternalLinks/wj?");
        }
    }
}
