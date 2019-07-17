using System.Collections.Generic;

namespace AdminTemplate.service.Dto.MbDetail
{
	public class MbDetailItemDto
	{
		public string Id { get; set; }
		public string DetailId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int? Order { get; set; }
		public int? State { get; set; }
		public string Bcontemt { get; set; }
		public double? Score { get; set; }
		public string Type { get; set; }
		public string PageInfo { get; set; }
		public List<string> LatitudeDetailIds { get; set; }
	}
}
