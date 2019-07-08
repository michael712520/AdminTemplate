using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class LatitudeDetailItem
    {
        public LatitudeDetailItem()
        {
            QtDetailItem = new HashSet<QtDetailItem>();
        }

        public string Id { get; set; }
        public string MbDetailId { get; set; }
        public string Name { get; set; }
        public double? Score { get; set; }
        public double? Coefficient { get; set; }
        public double? BaseScore { get; set; }
        public int? Sort { get; set; }
        public string ReturnInfo { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<QtDetailItem> QtDetailItem { get; set; }
    }
}
