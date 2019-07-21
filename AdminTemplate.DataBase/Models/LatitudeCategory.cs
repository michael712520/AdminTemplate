using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class LatitudeCategory
    {
        public string Id { get; set; }
        public string MbDetailId { get; set; }
        public string Name { get; set; }
        public string LatitudeDetailId { get; set; }

        public virtual LatitudeDetail LatitudeDetail { get; set; }
    }
}
