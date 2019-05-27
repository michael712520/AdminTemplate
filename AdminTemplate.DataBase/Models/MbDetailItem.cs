using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class MbDetailItem
    {
        public MbDetailItem()
        {
            QtDetailItem = new HashSet<QtDetailItem>();
        }

        public string Id { get; set; }
        public string DetailId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Order { get; set; }
        public int? State { get; set; }
        public string Bcontemt { get; set; }
        public double? Score { get; set; }
        public string Type { get; set; }

        public virtual MbDetail Detail { get; set; }
        public virtual ICollection<QtDetailItem> QtDetailItem { get; set; }
    }
}
