using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class LatitudeDetail
    {
        public LatitudeDetail()
        {
            MbDetailItem = new HashSet<MbDetailItem>();
        }

        public string Id { get; set; }
        public string ParentId { get; set; }
        public double? Score { get; set; }
        public double? Coefficient { get; set; }
        public double? BaseScore { get; set; }

        public virtual ICollection<MbDetailItem> MbDetailItem { get; set; }
    }
}
