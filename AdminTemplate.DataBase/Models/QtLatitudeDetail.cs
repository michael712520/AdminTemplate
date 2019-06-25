using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class QtLatitudeDetail
    {
        public string Id { get; set; }
        public string LatitudeDetailId { get; set; }
        public double? Score { get; set; }
        public string QtDetailId { get; set; }

        public virtual LatitudeDetail LatitudeDetail { get; set; }
        public virtual QtDetail QtDetail { get; set; }
    }
}
