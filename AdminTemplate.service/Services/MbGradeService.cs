using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.MbGrade;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;

namespace AdminTemplate.service.Services
{
	public class MbGradeService : BaseService
	{
		public NetResult Get(string id)
		{
			var model=DbContext.MbGrade.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
			return ResponseBodyEntity(model);
		}
		public NetResult GetMbDetail(string id)
		{
			var model = DbContext.MbDetail.Include(o => o.MbGrade).AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
			return ResponseBodyEntity(model);
		}
		public NetResult SaveUpdate(MbGradeDto from)
		{
			if (from.Id != null)
			{
				var model = DbContext.MbGrade.AsNoTracking().FirstOrDefault(p => p.Id.Equals(from.Id));
				if (model == null)
				{

					return ResponseBodyEntity("", EnumResult.Error, "更新对象不存在");
				}

				if (from.Titile!=null)
				{
					model.Titile = from.Titile;

				}
				if (from.Content != null)
				{
					model.Content = from.Content;

				}
				if (from.UpScore != null)
				{
					model.UpScore = from.UpScore;

				}
				if (from.DownScore != null)
				{
					model.DownScore = from.DownScore;

				}
				DbContext.Update(model);
				DbContext.SaveChanges();
				return ResponseBodyEntity(model);
			}
			else
			{
				MbGrade model = new MbGrade();
				model.Id = Guid.NewGuid().ToString("N");
				model.Titile = from.Titile;
				model.Content = from.Content;
				model.UpScore = from.UpScore;
				model.DownScore = from.DownScore;
				model.MbDetailId = from.MbDetailId;
				DbContext.MbGrade.Add(model);
				DbContext.SaveChanges();
				return ResponseBodyEntity(model);
			}
		}
	}
}
