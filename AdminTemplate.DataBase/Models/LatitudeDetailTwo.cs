using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class LatitudeDetailTwo
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public double? Score { get; set; }
        public double? Coefficient { get; set; }
        public double? BaseScore { get; set; }
        public string Name { get; set; }
        public int? Sort { get; set; }
    }
}
