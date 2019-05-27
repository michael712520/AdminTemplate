namespace AdminTemplate.service.Dto.SysUser
{
    public class SysUserLogin
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Userpwd { get; set; }
        public int? State { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string[] Companyid { get; set; }
        public string[] Companyname { get; set; }
     
    }
}
