using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class SysUser
    {
        public string UserName { get; set; }
        public string Userpwd { get; set; }
        public int? State { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Companyid { get; set; }
        public string Companyname { get; set; }
        public DateTime? Createtime { get; set; }
        public DateTime? Updatetime { get; set; }
        public string Id { get; set; }
    }
}
