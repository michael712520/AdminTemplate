﻿namespace AdminTemplate.service.Dto.LatitudeDetailItem
{
    public class LatitudeDetailItemDto
    {
        public string Id { get; set; }
        public string MbDetailId { get; set; }
        public string Name { get; set; }
        public double? Score { get; set; }
        public double? Coefficient { get; set; }
        public double? BaseScore { get; set; }
        public int? Sort { get; set; }
        public string SelectResult { get; set; }
    }
}
