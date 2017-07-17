using System.Web.Mvc;
using testAdmin.Core;
using testAdmin.Models;
using testAdmin.Service;

namespace testAdmin.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;

        public PropertyController(IUnitOfWork unit)
        {
            _unitOfwork = unit;
        }

        #region Get Property 
        // GET: Property
        [HttpGet]
        public ActionResult Get(Property property)
        {
            var vr = new VisualRepresentation();
            if (property.Id != 0)
            {
                property = _unitOfwork.Properties.GetProperty(property.Id);
            }

            if (property.VisualRepresentation_Id == null)
                return View(property);


            return View(property);
        }
        #endregion


        #region Add|Update property
        [HttpPost]
        public ActionResult Add(PropertyObject propertyModel)
        {
            if (propertyModel.Subject_id == 0)
                return HttpNotFound();

            int DescriptionId;

            var description = AdminService.GetDescription(propertyModel.Description);
            _unitOfwork.Descriptions.Add(description);

            _unitOfwork.Complete();
            DescriptionId = description.Id;

            var propObject = AdminService.GetProperty(DescriptionId, propertyModel);

            if (propertyModel.Id != 0)
            {
                propObject.Id = propertyModel.Id;
                _unitOfwork.Properties.Update(propObject);
            }
            else
                _unitOfwork.Properties.Add(propObject);

            _unitOfwork.Complete();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Delete by id
        public ActionResult Delete(int id)
        {
            var property = new Property { Id = id };

            _unitOfwork.Properties.Delete(property);
            _unitOfwork.Complete();

            return Json(new { Url = "/Home/Find/" }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Get Properties

        public ActionResult GetPropertybyId(int id)
        {
            var result = _unitOfwork.Properties.GetPropertybyId(id);

            if (result == null)
                return HttpNotFound();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Clear Visual ID
        public ActionResult ClearVisualId(int id)
        {
            _unitOfwork.Properties.ClearVisualId(id);

            return RedirectToAction("DeleteVisual", "Subject", new { id = id });
        }
        #endregion

    }
}