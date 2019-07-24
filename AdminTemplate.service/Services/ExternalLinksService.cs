using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AutoMapper;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminTemplate.service.Services
{
	public class ExternalLinksService : BaseService
	{
		public NetResult Add(string mbQuestionId, string teacherIdCard, string foreignType, string studentIdCard, string callBack)
		{

			var mj = DbContext.QtDetail.AsNoTracking()
				.Where(p => p.StudentIdCard.Equals(studentIdCard) && p.TeacherIdCard.Equals(teacherIdCard) && p.MbDetailId.Equals(mbQuestionId)).OrderByDescending(o => o.CreateTime)
				.FirstOrDefault();
			if (mj != null)
			{
				mj.CallBack = callBack;
				DbContext.QtDetail.Update(mj);
				callBack = HttpUtility.UrlEncode(callBack, System.Text.Encoding.GetEncoding(65001));
				DbContext.SaveChanges();
				return ResponseBodyEntity($"https://www.iu1314.com/#/ExternalLinks/wj?qtDetailId={mj.Id}&callBack={callBack}");
			}

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
			qtDetail.CallBack = callBack;
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
			callBack = HttpUtility.UrlEncode(callBack, System.Text.Encoding.GetEncoding(65001));
			return ResponseBodyEntity($"https://www.iu1314.com/#/ExternalLinks/wj?qtDetailId={qtDetail.Id}&callBack={callBack}");
		}
        public NetResult AddLocal(string mbQuestionId)
        {
            string guid = Guid.NewGuid().ToString("N");
            var model = DbContext.MbDetail.Include(o => o.MbDetailItem).FirstOrDefault(p => p.Id.Equals(mbQuestionId));
            if (model == null)
            {
                return ResponseBodyEntity("", EnumResult.Error, "没有找到对应的问卷");
            }
            QtDetail qtDetail = Mapper.Map<QtDetail>(model);
            qtDetail.Id = Guid.NewGuid().ToString("N");
            qtDetail.TeacherIdCard = guid;
            qtDetail.ForeignType = guid;
            qtDetail.StudentIdCard = guid;
            qtDetail.CallBack = guid;
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
            return ResponseBodyEntity();
        }
        public NetResult GetStudentList(string studentIdCard)
		{

			return ResponseBodyEntity($"https://www.iu1314.com/#/ExternalLinks/studentList?studentIdCard={studentIdCard}");
		}
		public NetResult StudentAndMbQuestion(string studentIdCard, string mbQuestionId)
		{

			return ResponseBodyEntity($"https://www.iu1314.com/#/ExternalLinks/studentAndMbQuestion?studentIdCard={studentIdCard}&mbQuestionId={mbQuestionId}");
		}

		public NetResult QuestionResult(string studentIdCard, string mbQuestionId)
		{
			var firstOrDefault = DbContext.QtDetail.Include(o => o.QtLatitudeDetail).AsNoTracking().FirstOrDefault(p => p.StudentIdCard.Equals(studentIdCard) && p.MbDetailId.Equals(mbQuestionId));
			if (firstOrDefault != null)
			{
				return ResponseBodyEntity(firstOrDefault);
			}
			else { return ResponseBodyEntity("", EnumResult.Error, "查询结果为空"); }


		}
	}
}
