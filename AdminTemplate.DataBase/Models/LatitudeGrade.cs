using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class LatitudeGrade
    {
        public string Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Titile { get; set; }
        public string Content { get; set; }
        public double? UpScore { get; set; }
        public double? DownScore { get; set; }
        public string LatitudeDetailId { get; set; }

        public virtual LatitudeDetail LatitudeDetail { get; set; }
    }
}
