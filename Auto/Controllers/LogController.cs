using AutoMapper;
using Logs;
using Logs.Business;
using Logs.Models;
using Logs.Models.ModelDTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace testAdmin.Controllers
{


    public class LogController : Controller
    {
        private readonly DbActions Context;

        public LogController(DbActions Context)
        {
            this.Context = Context;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region GetMessage

        public async Task<ActionResult> GetMessage(int? totalItems, int page = 1, int pageSize = 20, bool order = false)
        {

            var listMsgModel = await Context.GetMessages(totalItems, page, pageSize, order);

            var messages = listMsgModel.messages;

            if (messages == null)
                return HttpNotFound();

            var msg = Mapper.Map<List<Message>, List<MessageDTO>>(messages);



            PageInfo pageinfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = listMsgModel.Count };
            var mvw = new MessageViewModel { msgViewModel = msg, pageinfo = pageinfo };

            return Json(mvw, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Search Message by sender_id

        public ActionResult SearchMessage(string sender_id)
        {
            // Mapper.Initialize(x => x.CreateMap<List<Message>, List<MessageDTO>>());

            var searchMsg = Mapper.Map<IEnumerable<Message>, List<MessageDTO>>
                (Context.SearchMessages(sender_id));

            if (searchMsg.Count < 1)
                return HttpNotFound();

            return Json(searchMsg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Get Logs

        public ActionResult GetLogs(int? totalItems, int page = 1, int pageSize = 20, bool order = false)
        {
            var logs = Context.GetLogs(totalItems, page, pageSize, order);

            if (logs == null)
                return HttpNotFound();

            return Json(logs, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #region Search Logs

        public ActionResult SearchLogs(string messageId)
        {
            var searchLogs = Context.SearchLogs(messageId);

            if (searchLogs == null)
                return HttpNotFound();

            return Json(searchLogs, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Get Exception
        //get exception by LogEntry.Id

        public ActionResult GetException(int id)
        {
            var exception = Context.GetExp(id);

            if (exception == null)
                return HttpNotFound();

            return Json(exception, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}