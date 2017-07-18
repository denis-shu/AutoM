using System.Collections.Generic;

namespace Logs.Service
{
    public static class SortHelper
    {
        public static List<Message> GetSortedMessage(List<Message> messages)
        {
            var mes = new Message();
            var mes1 = new Message();
            int i = 0;
            List<Message> messagess = new List<Message>();
            for (i = 0; i < messages.Count - 1; ++i)
            {
                if ((messages[i].SenderId == messages[i + 1].SenderId)
                    && (messages[i].MessageType == 1) && (messages[i + 1].MessageType == 0))
                {
                    messagess.Add(messages[i + 1]);
                    messagess.Add(messages[i]);
                    i++;
                }
                else
                {
                    messagess.Add(messages[i]);

                }
            }

            return messagess; ;
        }
    }
}
