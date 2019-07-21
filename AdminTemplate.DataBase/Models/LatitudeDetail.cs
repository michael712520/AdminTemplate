using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class LatitudeDetail
    {
        public LatitudeDetail()
        {
            LatitudeCategory = new HashSet<LatitudeCategory>();
            LatitudeGrade = new HashSet<LatitudeGrade>();
            MbDetailItem = new HashSet<MbDetailItem>();
            QtLatitudeDetail = new HashSet<QtLatitudeDetail>();
        }

        public string Id { get; set; }
        public string MbDetailId { get; set; }
        public string Name { get; set; }
        public double? Score { get; set; }
        public double? Coefficient { get; set; }
        public double? BaseScore { get; set; }
        public int? Sort { get; set; }
        public string Relationship { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Pattern { get; set; }

        public virtual ICollection<LatitudeCategory> LatitudeCategory { get; set; }
        public virtual ICollection<LatitudeGrade> LatitudeGrade { get; set; }
        public virtual ICollection<MbDetailItem> MbDetailItem { get; set; }
        public virtual ICollection<QtLatitudeDetail> QtLatitudeDetail { get; set; }
    }
}
