using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class QtDetailItem
    {
        public string Id { get; set; }
        public string QtDetailId { get; set; }
        public string MbDetailId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Order { get; set; }
        public int? State { get; set; }
        public string Bcontemt { get; set; }
        public double? Score { get; set; }
        public string Type { get; set; }
        public int? Display { get; set; }
        public string LatitudeDetailItemId { get; set; }
        public string LatitudeDetailItemName { get; set; }
        public string LatitudeDetailIds { get; set; }
        public string PageInfo { get; set; }

        public virtual LatitudeDetailItem LatitudeDetailItem { get; set; }
        public virtual MbDetail MbDetail { get; set; }
        public virtual QtDetail QtDetail { get; set; }
    }
}
