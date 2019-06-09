namespace AdminTemplate.service.Dto.LatitudeDetail
{
    public class LatitudeDetailDto
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
