using System;
using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.QtDetailItem;
using AutoMapper;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AdminTemplate.service.Services
{
	public class QtDetailService : BaseService
	{
		public NetResult Get(string id)
		{
			var model = DbContext.QtDetail.AsNoTracking().Include(o => o.QtDetailItem).FirstOrDefault(p => p.Id.Equals(id));
			if (model != null)
			{
				model.QtDetailItem = model.QtDetailItem.OrderBy(o => o.Order).ToList();
				return ResponseBodyEntity(model);
			}
			return ResponseBodyEntity("", EnumResult.Error, "对象不存在");
		}

		public NetResult GetStudentAll(string studentIdCard, PaginationStartAndLengthFilter filter)
		{
			var query = DbContext.QtDetail.Where(p => p.StudentIdCard.Equals(studentIdCard));

			var count = query.Count();
			var list = query.Skip(filter.Start).Take(filter.Length).ToList();
			return ResponseBodyEntity(list, count);
		}
		public NetResult GetByStudentAndMbDetailId(string studentIdCard, string mbDetailId)
		{
			var model = DbContext.QtDetail.FirstOrDefault(p => p.StudentIdCard.Equals(studentIdCard) && p.MbDetailId.Equals(mbDetailId));
			return ResponseBodyEntity(model);
		}

		public NetResult UpdateSelectResult(List<QtDetailItemDto> listParam)
		{

			//            DbContext.QtDetailItem.UpdateRange(list);
			//            DbContext.SaveChanges();
			string id = null;
			string mbId = null;
			if (listParam != null && listParam.Count > 0)
			{
				listParam.ForEach(d =>
				{
					var model = DbContext.QtDetailItem.FirstOrDefault(p => p.Id.Equals(d.Id));
					if (model != null)
					{
						id = model.QtDetailId;
						mbId = model.MbDetailId;
						model.SelectResult = d.SelectResult;
						DbContext.QtDetailItem.Update(model);
					}

				});
				DbContext.SaveChanges();

				if (id != null && mbId != null)
				{
					var listQtDetailItem = DbContext.QtDetailItem.Where(p =>
						p.QtDetailId.Equals(id) && (p.Type.Equals("pfduoxuan") || p.Type.Equals("pfdanxuan"))).ToList();
					var listLatitude = DbContext.LatitudeDetail.Where(p => p.Id.Equals(mbId)).ToList();
					Dictionary<string, double> myDictionary = new Dictionary<string, double>();
					listLatitude.ForEach(d =>
					{
						double total = 0;
						dynamic relationship = JsonConvert.DeserializeObject(d.Relationship);
						var ax = relationship.ax;
						if (relationship.ax.s!=null&&relationship.ax.s[0]&& relationship.ax.v!=null)
						{
							total = total + 0 * double.Parse(relationship.ax.v);
						}
						var bx = relationship.bx;
						var cx = relationship.cx;
						var dx = relationship.dx;
						var ex = relationship.ex;
						var fx = relationship.fx;
						var gx = relationship.gx;
						var hx = relationship.hx;
						var ix = relationship.ix;
						var jx = relationship.jx;
						var kx = relationship.kx;
						var mx = relationship.mx;
						var nx = relationship.nx;
						var ox = relationship.ox;
						var px = relationship.px;
						var qx = relationship.qx;
						var rx = relationship.rx;
						var sx = relationship.sx;
						var tx = relationship.tx;
						var ux = relationship.ux;
						var vx = relationship.vx;
						var wx = relationship.wx;
						var xx = relationship.xx;
						var yx = relationship.yx;
						var zx = relationship.zx;

						myDictionary.Add(d.Name, total);
					});
				}


			}
			else
			{
				return ResponseBodyEntity("", EnumResult.Error, "数据对象为空");
			}
			return ResponseBodyEntity();

		}
	}
}
