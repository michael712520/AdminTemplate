using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class QtDetailItem
    {
        public string Id { get; set; }
        public string QtDetailId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Order { get; set; }
        public int? State { get; set; }
        public string Bcontemt { get; set; }
        public double? Score { get; set; }
        public string MbDetailItemId { get; set; }
        public string Type { get; set; }

        public virtual MbDetailItem MbDetailItem { get; set; }
        public virtual QtDetail QtDetail { get; set; }
    }
}
