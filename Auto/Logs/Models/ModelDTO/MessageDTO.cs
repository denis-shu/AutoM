using System;

namespace Logs.Models.ModelDTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public Nullable<int> SenderId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Application { get; set; }
        public int MessageType { get; set; }
    }
}