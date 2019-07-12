using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class MbDetail
    {
        public MbDetail()
        {
            MbDetailItem = new HashSet<MbDetailItem>();
            QtDetailItem = new HashSet<QtDetailItem>();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Order { get; set; }
        public int? State { get; set; }
        public double? Score { get; set; }
        public int? Display { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<MbDetailItem> MbDetailItem { get; set; }
        public virtual ICollection<QtDetailItem> QtDetailItem { get; set; }
    }
}
