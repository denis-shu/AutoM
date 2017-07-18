using Logs.Models.ModelDTO;
using System.Collections.Generic;

namespace Logs.Models
{
    public class MessageViewModel
    {
        public List<MessageDTO> msgViewModel { get; set; }

        public PageInfo pageinfo { get; set; }

    }
}
