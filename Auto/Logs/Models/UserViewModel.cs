using System.Collections.Generic;

namespace Logs.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public Pager Pager { get; set; }
    }
}
