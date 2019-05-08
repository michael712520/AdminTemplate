using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class MagFiedmanage
    {
        public string Id { get; set; }
        public string Fiedid { get; set; }
        public string Fiedname { get; set; }
        public string FiedsNo { get; set; }
        public string Year { get; set; }
        public string Namefied { get; set; }
        public string Fiedpostfix { get; set; }
        public string Fiedurl { get; set; }
        public string Fiedmenuid { get; set; }
        public string Fiedgenres { get; set; }
        public string ShareUrl { get; set; }
        public string Username { get; set; }
        public string Userid { get; set; }
        public int? Fiedisopen { get; set; }
        public DateTime? Createtime { get; set; }
        public DateTime? Update { get; set; }
    }
}
