using System;
using System.Collections.Generic;
using System.Text;

namespace AdminTemplate.service.Dto.LatitudeCategory
{
	public class LatitudeCategoryResult
	{
		public string Id { get; set; }
		public string Bt { get; set; }
		public string Context { get; set; }
		private List<ListLatitudeInfoResult> List { get; set; }
	}
}
