using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class LatitudeDetail
    {
        public LatitudeDetail()
        {
            InverseParent = new HashSet<LatitudeDetail>();
            LatitudeDetailTwo = new HashSet<LatitudeDetailTwo>();
            MbDetailItem = new HashSet<MbDetailItem>();
        }

        public string Id { get; set; }
        public string ParentId { get; set; }
        public double? Score { get; set; }
        public double? Coefficient { get; set; }
        public double? BaseScore { get; set; }
        public string Name { get; set; }
        public int? Sort { get; set; }

        public virtual LatitudeDetail Parent { get; set; }
        public virtual ICollection<LatitudeDetail> InverseParent { get; set; }
        public virtual ICollection<LatitudeDetailTwo> LatitudeDetailTwo { get; set; }
        public virtual ICollection<MbDetailItem> MbDetailItem { get; set; }
    }
}
