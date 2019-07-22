using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.LatitudeCategory;
using GlobalConfiguration.Utility;

namespace AdminTemplate.service.Services
{
	public class LatitudeCategoryService : BaseService
	{
		public NetResult List(string mbDetailId)
		{
			var data = DbContext.LatitudeCategory.Where(p => p.MbDetailId.Equals(mbDetailId)).ToList();
			return ResponseBodyEntity(data);


		}


		public NetResult SaveOrUpdate(LatitudeCategoryDto form)
		{
			if (form.Id != null)
			{
				var data = DbContext.LatitudeCategory.FirstOrDefault(p => p.Id.Equals(form.Id));
				if (data != null)
				{
					data.LatitudeDetails = form.LatitudeDetails;
					data.Name = form.Name;
					data.MbDetailId = form.MbDetailId;
					DbContext.LatitudeCategory.Update(data);
				}
				else { return ResponseBodyEntity("id查询对象为空", EnumResult.Error); }

				DbContext.SaveChanges();
				return ResponseBodyEntity(data);
			}
			else
			{
				LatitudeCategory model = new LatitudeCategory();
				model.Id = Guid.NewGuid().ToString("N");
				model.LatitudeDetails = form.LatitudeDetails;
				model.Name = form.Name;
				model.MbDetailId = form.MbDetailId;
				model = DbContext.LatitudeCategory.Add(model).Entity;
				DbContext.SaveChanges();
				return ResponseBodyEntity(model);
			}
		}

		public NetResult Delete(string id)
		{
			var data = DbContext.LatitudeCategory.FirstOrDefault(p => p.Id.Equals(id));
			if (data != null)
			{
				DbContext.LatitudeCategory.Remove(data);
				DbContext.SaveChanges();
				return ResponseBodyEntity();
			}
			else
			{
				return ResponseBodyEntity("id查询对象不存在无法删除", EnumResult.Error);
			}
		}

	}
}
