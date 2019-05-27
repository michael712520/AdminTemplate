using System;
using System.Collections.Generic;

namespace AdminTemplate.DataBase.Models
{
    public partial class QtDetail
    {
        public QtDetail()
        {
            QtDetailItem = new HashSet<QtDetailItem>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Order { get; set; }
        public int? State { get; set; }

        public virtual ICollection<QtDetailItem> QtDetailItem { get; set; }
    }
}
