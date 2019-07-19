using AdminTemplate.DataBase.Models;
using AdminTemplate.service.BaseServices;
using AdminTemplate.service.Dto.QtDetailItem;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
		public NetResult GetResult(string id, string batchNumber)
		{
			var model = DbContext.QtDetail.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
			if (model != null)
			{
				model.QtLatitudeDetail = DbContext.QtLatitudeDetail.Include(o => o.LatitudeDetail)
					.Where(p => p.BatchNumber.Equals(batchNumber)).ToList();

				var listLatitudeGrade = DbContext.LatitudeGrade.Where(p => model.QtLatitudeDetail.Select(s => s.LatitudeDetailId).Contains(p.LatitudeDetailId)).ToList();

				List<QtDetailItemResult> lik = new List<QtDetailItemResult>();
				model.QtLatitudeDetail.ToList().ForEach(d =>
				{
					QtDetailItemResult kModel = new QtDetailItemResult();
					kModel.Id = d.LatitudeDetailId;
					kModel.Name = d.LatitudeDetail.Name;
					kModel.Score = d.Score;

					listLatitudeGrade.Where(p => p.LatitudeDetailId.Equals(d.LatitudeDetailId)).ToList().ForEach(e =>
					  {
						  if (e.UpScore <= d.Score && d.Score <= e.DownScore)
						  {
							  kModel.describe = e.Titile;
						  }
						  else if (d.Score >= e.DownScore)
						  {
							  kModel.describe = e.Titile;
						  }
					  });

					lik.Add(kModel);
				});
				return ResponseBodyEntity(new { bt = model.Title, context = model.Content, list = lik, qtLatitudeDetail = model.QtLatitudeDetail.ToList() });
			}
			return ResponseBodyEntity("", EnumResult.Error, "对象不存在");
		}
		public NetResult GetStudentAll(string studentIdCard, PaginationStartAndLengthFilter filter)
		{
			var query = DbContext.QtDetailbatch.Where(p => p.StudentIdCard.Equals(studentIdCard));
			var count = query.Count();
			var list = query.Skip(filter.Start).Take(filter.Length).ToList();
			return ResponseBodyEntity(list, count);
		}
		public NetResult GetByStudentAndMbDetailId(string studentIdCard, string mbDetailId)
		{
			var model = DbContext.QtDetail.Include(o => o.QtLatitudeDetail).ThenInclude(o => o.LatitudeDetail).FirstOrDefault(p => p.StudentIdCard.Equals(studentIdCard) && p.MbDetailId.Equals(mbDetailId));
			return ResponseBodyEntity(model);
		}

		/// <summary>
		/// 提交问题单以及保存结果
		/// </summary>
		/// <param name="listParam"></param>
		/// <returns></returns>
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
						model.State = 1;
						DbContext.QtDetailItem.Update(model);
					}

				});
				DbContext.SaveChanges();

				if (id != null && mbId != null)
				{
					var listQtDetailItem = DbContext.QtDetailItem.Where(p =>
						p.QtDetailId.Equals(id) && (p.Type.Equals("pfduoxuan") || p.Type.Equals("pfdanxuan"))).ToList();
					var listLatitude = DbContext.LatitudeDetail.Where(p => p.MbDetailId.Equals(mbId)).ToList();
					Dictionary<string, double> myDictionary = new Dictionary<string, double>();
					Dictionary<string, double> DictionaryDetailItem = new Dictionary<string, double>();
					listQtDetailItem.ForEach(d =>
					{
						try
						{

							if (d.LatitudeDetailIds != null)
							{
								dynamic key = JsonConvert.DeserializeObject(d.LatitudeDetailIds);
								dynamic Bcontemt = JsonConvert.DeserializeObject(d.Bcontemt);
								dynamic SelectResult = JsonConvert.DeserializeObject(d.SelectResult);
								double score = 0;
								if (d.Type.Equals("pfdanxuan"))
								{
									foreach (var item in Bcontemt)
									{
										if (SelectResult.value.Value.Equals(item.value.Value))
										{
											score = double.Parse(item.score.Value);
										}
									}


								}
								else if (d.Type.Equals("pfduoxuan"))
								{
									foreach (var item in Bcontemt)
									{
										foreach (var ik in SelectResult.value)
										{
											if (ik.Value.Contains(item.value.Value))
											{
												score += double.Parse(item.score.Value);
											}
										}

									}
								}
								DictionaryDetailItem.Add(key[0].Value, score);
							}

						}
						catch (Exception e)
						{
						}


					});
					listLatitude.ForEach(d =>
					{
						double total = 0;
						try
						{
							if (d.BaseScore != null)
							{
								total = total + (double)d.BaseScore;
							}
						}
						catch (Exception e)
						{

						}



						dynamic relationship = JsonConvert.DeserializeObject(d.Relationship);
						var ax = relationship.ax;
						try
						{


							if (relationship.ax.s != null && relationship.cx.s.Count > 0 && relationship.ax.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.ax.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.ax.v.Value);
								}


							}
							if (relationship.bx.s != null && relationship.bx.s.Count > 0 && relationship.bx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.bx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.bx.v.Value);
								}


							}
							if (relationship.cx.s != null && relationship.cx.s.Count > 0 && relationship.cx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.cx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.cx.v.Value);
								}


							}
							if (relationship.dx.s != null && relationship.dx.s.Count > 0 && relationship.dx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.dx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.dx.v.Value);
								}


							}
							if (relationship.ex.s != null && relationship.ex.s.Count > 0 && relationship.ex.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.ex.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.ex.v.Value);
								}


							}
							if (relationship.fx.s != null && relationship.fx.s.Count > 0 && relationship.fx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.fx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.fx.v.Value);
								}


							}
							if (relationship.gx.s != null && relationship.gx.s.Count > 0 && relationship.gx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.gx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.gx.v.Value);
								}


							}
							if (relationship.hx.s != null && relationship.hx.s.Count > 0 && relationship.hx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.hx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.hx.v.Value);
								}


							}
							if (relationship.ix.s != null && relationship.ix.s.Count > 0 && relationship.ix.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.ix.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.ix.v.Value);
								}


							}
							if (relationship.jx != null && relationship.jx.s != null && relationship.jx.s.Count > 0 && relationship.jx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.jx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.jx.v.Value);
								}


							}
							if (relationship.kx != null && relationship.kx.s != null && relationship.kx.s.Count > 0 && relationship.kx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.kx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.kx.v.Value);
								}


							}
							if (relationship.mx != null && relationship.mx.s != null && relationship.mx.s.Count > 0 && relationship.mx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.mx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.mx.v.Value);
								}


							}
							if (relationship.nx != null && relationship.nx.s != null && relationship.nx.s.Count > 0 && relationship.nx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.nx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.nx.v.Value);
								}


							}
							if (relationship.ox != null && relationship.ox.s != null && relationship.ox.s.Count > 0 && relationship.ox.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.ox.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.ox.v.Value);
								}


							}
							if (relationship.px != null && relationship.px.s != null && relationship.px.s.Count > 0 && relationship.px.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.px.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.px.v.Value);
								}


							}
							if (relationship.qx != null && relationship.qx.s != null && relationship.qx.s.Count > 0 && relationship.qx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.qx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.qx.v.Value);
								}


							}
							if (relationship.rx != null && relationship.rx.s != null && relationship.rx.s.Count > 0 && relationship.rx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.rx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.rx.v.Value);
								}


							}
							if (relationship.sx != null && relationship.sx.s != null && relationship.sx.s.Count > 0 && relationship.sx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.sx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.sx.v.Value);
								}


							}
							if (relationship.tx != null && relationship.tx.s != null && relationship.tx.s.Count > 0 && relationship.tx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.tx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.tx.v.Value);
								}


							}
							if (relationship.ux != null && relationship.ux.s != null && relationship.ux.s.Count > 0 && relationship.ux.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.ux.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.ux.v.Value);
								}


							}
							if (relationship.vx != null && relationship.vx.s != null && relationship.vx.s.Count > 0 && relationship.vx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.vx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.vx.v.Value);
								}


							}
							if (relationship.wx != null && relationship.wx.s != null && relationship.wx.s.Count > 0 && relationship.wx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.wx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.wx.v.Value);
								}


							}

							if (relationship.xx != null && relationship.xx.s != null && relationship.xx.s.Count > 0 && relationship.xx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.xx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.xx.v.Value);
								}


							}
							if (relationship.yx != null && relationship.yx.s != null && relationship.yx.s.Count > 0 && relationship.yx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.yx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.yx.v.Value);
								}


							}
							if (relationship.zx != null && relationship.zx.s != null && relationship.zx.s[0].Value != null && relationship.zx.v.Value != "0")
							{
								DictionaryDetailItem.TryGetValue(relationship.zx.s[0].Value, out double outStr);
								if (outStr > 0)
								{
									total = total + outStr * double.Parse(relationship.zx.v.Value);
								}


							}
						}
						catch (Exception e)
						{
							Console.WriteLine(e);

						}

						myDictionary.Add(d.Id, total);
					});

					var mk = DbContext.QtDetail.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
					if (mk != null)
					{
						mk.State = 1;
						mk = DbContext.QtDetail.Update(mk).Entity;
						DbContext.SaveChanges();
						QtDetailbatch qtDetailBatch = AutoMapper.Mapper.Map<QtDetailbatch>(mk);
						qtDetailBatch.Id = Guid.NewGuid().ToString("N");
						qtDetailBatch.QtDetailId = mk.Id;
						qtDetailBatch.BatchNumber = DateTime.Now.ToString("yyyyMMddHHmmss");
						foreach (var item in myDictionary)
						{
							QtLatitudeDetail model = new QtLatitudeDetail();
							model.Id = Guid.NewGuid().ToString("N");
							model.QtDetailId = id;
							model.LatitudeDetailId = item.Key;
							model.Score = item.Value;
							model.BatchNumber = qtDetailBatch.BatchNumber;
							qtDetailBatch.QtLatitudeDetail.Add(model);
						}
						DbContext.QtDetailbatch.Add(qtDetailBatch);
						DbContext.SaveChanges();
					}


				}


			}
			else
			{
				return ResponseBodyEntity("", EnumResult.Error, "数据对象为空");
			}
			return ResponseBodyEntity();

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="studentIdCard"></param>
		/// <param name="mbId"></param>
		/// <returns></returns>
		public NetResult SelectResultSimple(string id, string studentIdCard, string mbId)
		{
			QtDetail model = null;
			if (id != null)
			{
				model = DbContext.QtDetail.Include(o => o.QtLatitudeDetail).AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
			}
			else
			{

				model = DbContext.QtDetail.Include(o => o.QtLatitudeDetail).AsNoTracking().FirstOrDefault(p => p.MbDetailId.Equals(mbId) && p.StudentIdCard.Equals(studentIdCard));
			}

			if (model != null)
			{
				var cd = new
				{
					list = model.QtLatitudeDetail
						.GroupBy(g => new { g.CreateTime, g.BatchNumber },
						(k, g) => new { k.CreateTime, k.BatchNumber, name = model.Title, id = model.Id, count = g.Count() }).OrderByDescending(o => o.CreateTime).Select(s => new { s.BatchNumber, s.CreateTime, s.count, s.name, s.id }).ToList()
				};
				return ResponseBodyEntity(cd);
			}

			return ResponseBodyEntity();
		}

	}
}
