using AutoMapper;
using Logs;
using Logs.Business;
using Logs.Models;
using Logs.Models.ModelDTO;
using System.Web.Mvc;
using System.Threading.Tasks;


namespace testAdmin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDbActions _context;

        public UserController(IUserDbActions _context)
        {
            this._context = _context;

        }

        #region Index
        //  GET: User        
        public async Task<ActionResult> Index(int page = 1)
        {
            var model = await _context.GetUsers(page);

            return View(model);
        }
        #endregion


        #region Get Details

        public ActionResult GetDetails(int id, bool isAuthenticated)
        {

            var userDetail = _context.GetUserDetails(id);

            var uDt = Mapper.Map<UserDetail, UserDetailDTO>(userDetail);

            var udm = new UserDetailModel(
                 uDt,
                 _context.GetLoggins(id),
                 isAuthenticated,
                 id
                 );
            return View(udm);
        }

        #endregion


        #region Search User
        [HttpPost]
        public ActionResult SearchUser(string login)
        {
            var user = Mapper.Map<User, UserDTO>
                (_context.SearchUser(login));

            return Json(user, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Change Authentication

        public ActionResult ChangeAuth(string id)
        {
            var auth = _context.Authenticated(id);

            return Json(auth, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Delete User
        public ActionResult DeleteUser(int id)
        {
            var deleteUser = _context.DeleteUser(id);

            if (!deleteUser)
                return HttpNotFound();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
