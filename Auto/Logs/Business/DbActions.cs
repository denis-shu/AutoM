using Logs.Models;

using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logs.Business
{
    public class DbActions
    {
        private readonly CXZAdminContext _context;

        public DbActions(CXZAdminContext _context)
        {
            this._context = _context;
        }
        //public Task<MessageViewModel> Get(int? totalItems, int page, int pageSize, bool order)
        //{
        //    return Task.Run(()=> {
        //        return GetMessages(totalItems, page, pageSize,  order);
        //    });
        //}

        #region Get Messages

        public async Task<ListMessageModel> GetMessages(int? totalItems, int page, int pageSize, bool order)
        {
           // var messages = new List<Message>();
            int count = 0;


           var messages = _context.Messages.AsQueryable();

            if (totalItems != null)
            {
                count = (int)totalItems;

                if (!order)

                    messages =  messages
                    .OrderByDescending(x => x.TimeStamp)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                else
                {
                    messages = messages
                      .OrderBy(x => x.TimeStamp)
                      .Skip((page - 1) * pageSize)
                      .Take(pageSize);
                }

            }
            else
            {
                count = await _context.Messages.CountAsync();

                messages = messages
               .OrderByDescending(x => x.TimeStamp)
               .Take(pageSize);
            }
           var  msgs = await messages.ToListAsync();

            List<Message> messagess = await Task.Run(() => 
            Service.SortHelper.GetSortedMessage(msgs));

            var listMessages = new ListMessageModel(messagess, count);

            return listMessages;
        }

        #endregion


        #region Search Messages

        public IEnumerable<Message> SearchMessages(string sender_id)
        {
            int senderId = int.Parse(sender_id);


            return _context.Messages
                .OrderBy(x => x.TimeStamp)
                .Where(x => x.SenderId == senderId)
                .ToList();
        }

        #endregion


        #region Get Logs

        public LogViewModel GetLogs(int? totalItems, int page, int pageSize, bool order)
        {
            var logs = new List<LogEntry>();

            int count = 0;

            if (totalItems != null)
            {
                count = (int)totalItems;

                if (!order)
                    logs = _context.LogEntries
                        .OrderByDescending(x => x.TimeStamp)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                else
                {
                    logs = _context.LogEntries
                           .OrderBy(x => x.TimeStamp)
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
                }
            }

            else
            {
                count = _context.LogEntries.Count();
                logs = _context.LogEntries
                    .OrderByDescending(x => x.TimeStamp)
                    .Take(pageSize)
                    .ToList();
            }

            PageInfo pageinfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = count };
            var lvm = new LogViewModel { logViewModel = logs, pageinfo = pageinfo };

            return lvm;
        }

        #endregion


        #region Search Logs

        public IEnumerable<LogEntry> SearchLogs(string messageId)
        {

            return _context.LogEntries
                    .Where(x => x.MessageId == messageId)
                    .OrderBy(x => x.TimeStamp)
                    .ToList();
        }

        #endregion


        #region Get Exception

        public Exception GetExp(int id)
        {
            var byteArr = _context.LogEntries
                .Where(x => x.Id == id)
                .Select(x => x.Exception)
                .FirstOrDefault();

            if (byteArr == null)
                return null;

            var exception = Service.Service.Deserialize(byteArr);

            return exception;
        }

        #endregion

    }
}
