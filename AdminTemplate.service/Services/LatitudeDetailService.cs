using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.LatitudeDetail;
using AutoMapper;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AdminTemplate.service.Dto.MbDetail;
using Newtonsoft.Json;

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
			if (id != null)
			{
				query = query.Where(p => p.MbDetailId.Equals(id));
			}
			if (filter.Keywords != null)
			{
				query = query.Where(p => p.Name.Contains(filter.Keywords));
			}



			var count = query.Count();
			var list = query.Skip(filter.Start).Take(filter.Length).OrderByDescending(o => o.Sort).ToList();
			return ResponseBodyEntity(list, count);
		}

		public NetResult GetListLat(string mbDetailId)
		{
			var model = DbContext.MbDetail.Include(o => o.MbDetailItem).FirstOrDefault(p => p.Id.Equals(mbDetailId));
			if (model == null)
			{
				return ResponseBodyEntity("", EnumResult.Error, "对象不存在");
			}

			var listMbDetailItem = model.MbDetailItem.ToList();
			listMbDetailItem.ForEach(d =>
			{
				if (d.Type == "pfdanxuan" || d.Type == "pfduoxuan")
				{
					if (d.LatitudeDetailIds != null)
					{
						try
						{
							dynamic key = JsonConvert.DeserializeObject(d.LatitudeDetailIds);
							d.LatitudeDetailIds = key[0].Value;
						}
						catch (Exception e)
						{

						}
					}
				}
			});
			var latitudeDetailIds = listMbDetailItem.Select(s => s.LatitudeDetailIds);
			var latitudeDetailItems = DbContext.LatitudeDetailItem.Where(p => latitudeDetailIds.Contains(p.Id)).ToList();
			var enumerable = listMbDetailItem.Select((s, i) => new
			{
				rowKey = i + 1,
				id = s.Id,
				titleTag = s.Title,
				type = s.Type,
				latitudeDetailId = latitudeDetailItems.FirstOrDefault(p => p.Id.Equals(s.LatitudeDetailIds))?.Id,
				name = latitudeDetailItems.FirstOrDefault(p => p.Id.Equals(s.LatitudeDetailIds))?.Name
			}).OrderBy(o => o.rowKey).ToList();
			var latitudeDetailItem = DbContext.LatitudeDetailItem.Where(p => p.MbDetailId.Equals(mbDetailId)).ToList();
			return ResponseBodyEntity(new
			{
				id = model.Id,
				title = model.Title,
				latitudeDetailItem = latitudeDetailItem,
				MbDetailItems = enumerable
			});
		}

		public NetResult UpdateItemMbDetailItem(List<UpdateMbDetailItemDto> list)
		{
         
            if (list!=null&&list.Count>0)
            {
                var ids = list.Select(s => s.Id).ToList();
               var listMbDetailItem= DbContext.MbDetailItem.AsNoTracking().Where(p => ids.Contains(p.Id)).ToList();
               List<MbDetailItem> lists=new List<MbDetailItem>();
                list.ForEach(d =>
                {
                    var model = listMbDetailItem.FirstOrDefault(p => p.Id.Equals(d.Id));
                    if (model != null)
                    {
                        if (d.LatitudeDetailIds != null)
                        {
                            model.LatitudeDetailIds = d.LatitudeDetailIds;
                            lists.Add(model);
                           
                        }
                    }
                 
                });
                DbContext.MbDetailItem.UpdateRange(lists);
                DbContext.SaveChanges();
                return ResponseBodyEntity();
            }
            else
            {
                return ResponseBodyEntity("",EnumResult.Error,"获取对象为空");
            }




        }
	}
}
