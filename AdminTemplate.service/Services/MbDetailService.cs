using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.MbDetail;
using AutoMapper;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminTemplate.service.Services
{
	public class MbDetailService : BaseService
	{
		public NetResult Get(string id)
		{
			var query = DbContext.MbDetail.Include(o => o.MbDetailItem).AsNoTracking();
			var model = query.FirstOrDefault(p => p.Id.Equals(id));
			model.MbDetailItem = model.MbDetailItem.OrderBy(o => o.Order).ToList();
			return ResponseBodyEntity(model);
		}
		public NetResult SaveUpdate(MbDetailDto form)
		{
			MbDetail model;
			if (form.Id == null)
			{
				System.DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(new System.DateTime(1970, 1, 1));
				model = new MbDetail();
				model.Id = DateTime.Now.Ticks.ToString();
				model.UserId = form.UserId;
				model.Title = form.Title;
				model.Content = form.Content;

				model = DbContext.MbDetail.Add(model).Entity;
				DbContext.SaveChanges();
				return ResponseBodyEntity(model);
			}

			var query = DbContext.MbDetail.AsNoTracking();
			model = query.FirstOrDefault(p => p.Id.Equals(form.Id));
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

		public NetResult Delete(string id)
		{
			var model = DbContext.MbDetail.FirstOrDefault(p => p.Id.Equals(id));
			if (model != null)
			{
				DbContext.MbDetail.Remove(model);
				DbContext.SaveChanges();
				return ResponseBodyEntity();
			}
			return ResponseBodyEntity("", EnumResult.Error, "id未找到");
		}

		public NetResult UpdateMbDetail(UpdateMbDetailDto form)
		{
			if (form == null)
			{
				return ResponseBodyEntity("", EnumResult.Error, "提交的值为空");

			}
			var model = DbContext.MbDetailItem.FirstOrDefault(p =>
				p.DetailId.Equals(form.DetailId) && p.Order == form.Sort);
			MbDetailItem model2 = null;
			if (form.Type == 0)
			{
				model2 = DbContext.MbDetailItem.OrderByDescending(
					o => o.Order).FirstOrDefault(p =>
					p.DetailId.Equals(form.DetailId) && p.Order < form.Sort);
			}
			else if ((form.Type == 1))
			{
				model2 = DbContext.MbDetailItem.OrderBy(o => o.Order).FirstOrDefault(p =>
					p.DetailId.Equals(form.DetailId) && p.Order > form.Sort);
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
			return ResponseBodyEntity(new { model, model2 });

		}
		public NetResult DeleteItem(string id)
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
			var list = query.Skip(filter.Start).Take(filter.Length).OrderByDescending(
				o => o.Order).ToList();
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
			var list = DbContext.MbDetailItem.AsNoTracking().OrderBy(o => o.Order).Where(p => p.DetailId.Equals(detailId)).ToList();

			return ResponseBodyEntity(list);
		}

		public NetResult ListSaveItem(List<MbDetailItemDto> list)
		{
			if (list != null)
			{
				int i = 0;
				list.ForEach(d =>
				{
					d.Order = i;
					i++;
				});
				list.ForEach(this.UpdateSaveItem);
				DbContext.SaveChanges();
				return ResponseBodyEntity();
			}
			return ResponseBodyEntity();
		}

		public void UpdateSaveItem(MbDetailItemDto form)
		{
			MbDetailItem model = Mapper.Map<MbDetailItem>(form);
			if (form.Id == null)
			{
				model.Id = Guid.NewGuid().ToString("N");
				if (model.Order == null)
				{
					var m = DbContext.MbDetailItem.AsNoTracking().OrderByDescending(o => o.Order).FirstOrDefault();
					if (m != null) model.Order = m.Order + 1;
				}

				if (form.LatitudeDetailIds != null && form.LatitudeDetailIds.Count > 0)
				{
					model.LatitudeDetailIds = JsonConvert.SerializeObject(form.LatitudeDetailIds);
					var ls = form.LatitudeDetailIds[form.LatitudeDetailIds.Count - 1];
					if (ls != null)
					{
						var ml = DbContext.LatitudeDetail.FirstOrDefault(p => p.Id.Equals(ls));
						model.LatitudeDetailId = ml?.Id;
						model.LatitudeDetailName = ml?.Name;


					}
				}
				else
				{
					model.LatitudeDetailIds = null;
				}


				DbContext.MbDetailItem.Add(model);
			}
			else
			{
				if (form.LatitudeDetailIds != null && form.LatitudeDetailIds.Count > 0)
				{
					model.LatitudeDetailIds = JsonConvert.SerializeObject(form.LatitudeDetailIds);
					var ls = form.LatitudeDetailIds[form.LatitudeDetailIds.Count - 1];
					if (ls != null)
					{
						var ml = DbContext.LatitudeDetail.FirstOrDefault(p => p.Id.Equals(ls));
						model.LatitudeDetailId = ml?.Id;
						model.LatitudeDetailName = ml?.Name;
					}

				}
				DbContext.MbDetailItem.Update(model);
			}

		}
		public NetResult SaveItem(MbDetailItemDto form)
		{
			MbDetailItem model = Mapper.Map<MbDetailItem>(form);
			if (form.Id == null)
			{
				model.Id = Guid.NewGuid().ToString("N");
				var m = DbContext.MbDetailItem.AsNoTracking().OrderByDescending(o => o.Order).FirstOrDefault();
				if (m != null) model.Order = m.Order + 1;
				if (form.LatitudeDetailIds != null && form.LatitudeDetailIds.Count > 0)
				{
					model.LatitudeDetailIds = JsonConvert.SerializeObject(form.LatitudeDetailIds);
					var ls = form.LatitudeDetailIds[form.LatitudeDetailIds.Count - 1];
					if (ls != null)
					{
						var ml = DbContext.LatitudeDetail.FirstOrDefault(p => p.Id.Equals(ls));
						model.LatitudeDetailId = ml?.Id;
						model.LatitudeDetailName = ml?.Name;
					}
				}
				else
				{
					model.LatitudeDetailIds = null;
				}


				DbContext.MbDetailItem.Add(model);
			}
			else
			{
				if (form.LatitudeDetailIds != null && form.LatitudeDetailIds.Count > 0)
				{
					model.LatitudeDetailIds = JsonConvert.SerializeObject(form.LatitudeDetailIds);
					var ls = form.LatitudeDetailIds[form.LatitudeDetailIds.Count - 1];
					if (ls != null)
					{
						var ml = DbContext.LatitudeDetail.FirstOrDefault(p => p.Id.Equals(ls));
						model.LatitudeDetailId = ml?.Id;
						model.LatitudeDetailName = ml?.Name;
					}
					DbContext.MbDetailItem.Update(model);
				}


			}
			DbContext.SaveChanges();
			return ResponseBodyEntity();
		}

		/**
		 *
		 */
		public NetResult GetSetWd(string detailId)
		{

			DbContext.MbDetailItem.Where(p => p.DetailId.Equals(detailId)).OrderBy(o => o.Order).ToList();
			return ResponseBodyEntity();
		}


        public NetResult UpdateState(string id,int state)
        {
          var model=  DbContext.MbDetail.FirstOrDefault(p => p.Id.Equals(id));
          if (model!=null)
          {
              model.State = state;
              DbContext.MbDetail.Update(model);
              DbContext.SaveChanges();
          }
          return ResponseBodyEntity();
        }

        public NetResult WxList(PaginationStartAndLengthFilter filter)
        {
            var query = DbContext.MbDetail.AsNoTracking().Where(p => p.State == 1);
            var count = query.Count();
            var list = query.Skip(filter.Start).Take(filter.Length).ToList();
            return ResponseBodyEntity(list,count);
        }

        public NetResult UpdateFree(string id,double fee)
        {
            var model=DbContext.MbDetail.FirstOrDefault(p => p.Id.Equals(id));
            if (model!=null)
            {
                model.Fee = fee;
                DbContext.MbDetail.Update(model);

                IEnumerable<QtDetail> list = DbContext.QtDetail.Where(p => p.MbDetailId.Equals(id)&&p.IsFee==0);

                foreach (var item in list)
                {
                    item.Fee = fee;
                }
                DbContext.QtDetail.UpdateRange(list);
                
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }
            else
            {
                return ResponseBodyEntity("",EnumResult.Error,"id未查询到对象！");
            }
        }
        public NetResult BuyFree(string id, double fee)
        {
            var model = DbContext.QtDetail.FirstOrDefault(p => p.Id.Equals(id));
            if (model != null)
            {
                model.Fee = fee;
                model.IsFee = 1;
                DbContext.QtDetail.Update(model);
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }
            else
            {
                return ResponseBodyEntity("", EnumResult.Error, "id未查询到对象！");
            }
        }
    }
}
