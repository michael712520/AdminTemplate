using System;
using System.Collections.Generic;
using System.Text;

namespace AdminTemplate.service.Dto.MbGrade
{
	public class MbGradeDto
	{
		public string Id { get; set; }
	 
		public string Titile { get; set; }
		public string Content { get; set; }
		public double? UpScore { get; set; }
		public double? DownScore { get; set; }
		public string MbDetailId { get; set; }
	}
}
