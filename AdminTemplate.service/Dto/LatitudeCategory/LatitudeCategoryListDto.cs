using System;
using System.Collections.Generic;
using System.Text;

namespace AdminTemplate.service.Dto.LatitudeCategory
{
	public class LatitudeCategoryList
	{
		public string MbDetailId { get; set; }
		List<LatitudeCategoryDto> List { get; set; }
	}
}
