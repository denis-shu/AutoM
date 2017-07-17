using System.Collections.Generic;

namespace testAdmin.Models
{
    public class SubViewModel
    {
        public IEnumerable<Subject> Subs { get; set; }

        public Logs.Models.Pager Pager { get; set; }
    }


}