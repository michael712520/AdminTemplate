using System;
using System.Collections.Generic;
using System.Text;

namespace AdminTemplate.service.Dto
{
	public class ExternalLinksAddDto
	{
		public string mbQuestionId { get; set; }
		public string teacherIdCard { get; set; }
		public string foreignType { get; set; }
		public string studentIdCard { get; set; }
		public string callBack { get; set; }
	}
}
