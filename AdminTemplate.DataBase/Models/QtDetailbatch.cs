using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class QtDetailbatch
    {
        public QtDetailbatch()
        {
            QtLatitudeDetail = new HashSet<QtLatitudeDetail>();
        }

        public string Id { get; set; }
        public string QtDetailId { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string BatchNumber { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string TeacherIdCard { get; set; }
        public string ForeignType { get; set; }
        public string StudentIdCard { get; set; }

        public virtual QtDetail QtDetail { get; set; }
        public virtual ICollection<QtLatitudeDetail> QtLatitudeDetail { get; set; }
    }
}
