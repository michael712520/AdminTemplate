using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class QtDetail
    {
        public QtDetail()
        {
            QtDetailItem = new HashSet<QtDetailItem>();
            QtDetailbatch = new HashSet<QtDetailbatch>();
            QtLatitudeDetail = new HashSet<QtLatitudeDetail>();
        }

        public string Id { get; set; }
        public string MbDetailId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Order { get; set; }
        public int? State { get; set; }
        public double? Score { get; set; }
        public int? Display { get; set; }
        public string TeacherIdCard { get; set; }
        public string ForeignType { get; set; }
        public string StudentIdCard { get; set; }
        public string CallBack { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<QtDetailItem> QtDetailItem { get; set; }
        public virtual ICollection<QtDetailbatch> QtDetailbatch { get; set; }
        public virtual ICollection<QtLatitudeDetail> QtLatitudeDetail { get; set; }
    }
}
