﻿using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class QtLatitudeDetail
    {
        public string Id { get; set; }
        public string LatitudeDetailId { get; set; }
        public double? Score { get; set; }
        public string QtDetailId { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string BatchNumber { get; set; }
        public string QtDetailbatchId { get; set; }

        public virtual LatitudeDetail LatitudeDetail { get; set; }
        public virtual QtDetail QtDetail { get; set; }
        public virtual QtDetailbatch QtDetailbatch { get; set; }
    }
}
