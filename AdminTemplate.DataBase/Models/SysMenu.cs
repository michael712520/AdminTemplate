using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class SysMenu
    {
        public string Id { get; set; }
        public string MenuName { get; set; }
        public int? Level { get; set; }
        public string Pid { get; set; }
        public int? State { get; set; }
        public string Url { get; set; }
        public DateTime? Createtime { get; set; }
        public DateTime? Updatetime { get; set; }
    }
}
