using System.Collections.Generic;

namespace Logs.Models
{
    public class ListMessageModel
    {
        public List<Message> messages { get; set; }

        public int Count { get; set; }

        public ListMessageModel(List<Message> messages, int count)
        {
            this.messages = messages;
            this.Count = count;
             
        }
    }
}
