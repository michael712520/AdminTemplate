using System;
using System.Collections.Generic;
using System.Text;

namespace AdminTemplate.service.Dto.QtDetailItem
{
	public class QtDetailItemResult
	{
		public string Id { get; set; }
		public double? Score { get; set; }
		public double? maxScore { get; set; }

		public string Name { get; set; }
		public string describe { get; set; }
	}
}
