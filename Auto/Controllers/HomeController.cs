using System.Threading.Tasks;
using System.Web.Mvc;
using testAdmin.Core;
using testAdmin.Models;
using testAdmin.Service;

namespace testAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Index

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            var model = await _unitOfWork.Subs.GetSubAtPage(page);

            return View(model);
        }
        #endregion


        #region Update|Add Subject

        [HttpPost]
        public ActionResult Update(Subject sub)
        {
            var modSub = new Subject();

            if (sub.Id != 0)
            {
                modSub = _unitOfWork.Subs.Get(sub.Id);
                modSub = AdminService.ModifySubject(sub);
                _unitOfWork.Subs.Update(modSub);
            }
            else
            {
                modSub = AdminService.NewSubject(sub);
                _unitOfWork.Subs.Add(modSub);

            }
            _unitOfWork.Complete();
            var id = modSub.Id;
            var vId = modSub.VisualRepresentation_Id;


            ViewData["Message"] = "Success";
            return Json(new { Url = "/Home/Find/", Id = id }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Delete
        public ActionResult Delete(int id)
        {
            var sub = _unitOfWork.Subs.Get(id);
            if (sub != null)
            {
                _unitOfWork.Subs.Remove(sub);
                _unitOfWork.Complete();

                return Json(new { success = true, responseMessage = "Deleted" }, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }
        #endregion


        #region Find by id

        public ActionResult Find(int id = 0)
        {
            var sub = new Subject();
            if (id != 0)
            {
                var subExist = _unitOfWork.Subs.GetSubWith(id);
                return View(subExist);
            }
            else
                return View(sub);
        }

        #endregion


        #region Search subjects

        public ActionResult Search(string text)
        {
            var result = _unitOfWork.Subs.Search(text);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}