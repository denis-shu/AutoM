using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using testAdmin.Core;
using testAdmin.Models;
using testAdmin.Service;

namespace testAdmin.Controllers
{
    public class SubjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Upload Visual Representation

        [HttpPost]
        public ActionResult Upload(VisualRepresentationModel vr)
        {
            if (vr == null)
            {
                return new HttpNotFoundResult();
            }
            byte[] imageArr = null;
            int id = 0;

            var message = "";
            var visualRep = new VisualRepresentation();

            if (vr.ImageLink != null)
            {
                imageArr = AdminService.GetImageArr(new Uri(vr.ImageLink));
            }

            if (Request.Files.Count > 0)
            {
                using (var reader = new BinaryReader(Request.Files["Image"].InputStream))
                {
                    imageArr = reader.ReadBytes(Request.Files["Image"].ContentLength);
                }
            }

            if (vr.Id != 0)
            {
                visualRep = _unitOfWork.VisualRepresentations.Get(vr.Id);

                if (vr.Code != null)
                    visualRep.Code = (string.Format(@"{0}", vr.Code))
                        .Replace("\r", ""); ;
                // visualRep.Code = visualRep.Code.Replace("\r", "");
                visualRep.Id = vr.Id;

                visualRep.Image = imageArr != null
                    ? imageArr : visualRep.Image;


                _unitOfWork.VisualRepresentations.Update(visualRep);
                message = "Updated";
            }
            else
            {
                if (vr.Code != null)
                    visualRep.Code = (string.Format(@"{0}", vr.Code))
                        .Replace("\r", "");

                visualRep.Image = imageArr != null
                    ? imageArr : visualRep.Image;

                _unitOfWork.VisualRepresentations.Add(visualRep);
                message = "Added";
            }
            _unitOfWork.Complete();
            id = visualRep.Id;

            return Json(new { success = true, responseMessage = message, Id = id });
        }

        #endregion


        #region Get Image

        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var entity = _unitOfWork.VisualRepresentations.GetImage(id);

            var sd = entity.Image;

            return ((entity != null) && (entity.Image != null)) ?
                File(entity.Image, "image/jpeg") : null;
        }
        #endregion


        #region Delete Visual

        [HttpPost]
        public ActionResult DeleteVisual(int id)
        {
            _unitOfWork.Properties.ClearVisualId(id);
            _unitOfWork.Subs.ClearVisualId(id);

            var visualObj = new VisualRepresentation { Id = id };

            _unitOfWork.VisualRepresentations.Delete(visualObj);
            _unitOfWork.Complete();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Delete Image
        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            var visualObj = _unitOfWork.VisualRepresentations.Get(id);

            visualObj.Image = null;

            _unitOfWork.VisualRepresentations.Update(visualObj);
            _unitOfWork.Complete();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Add Tags
        [HttpPost]
        public ActionResult AddTags(SubTagModel tagObj)
        {
            if (tagObj == null)
            {
                return new HttpNotFoundResult();
            }

            List<Tag> tags = new List<Tag>();
            if (tagObj.Tag1.Length > 0)
            {
                for (int i = 0; i < tagObj.Tag1.Length; i++)
                {
                    var tag = new Tag
                    {
                        Sub_Id = tagObj.Sub_Id,
                        Tag1 = tagObj.Tag1[i]
                    };
                    tags.Add(tag);
                }
            }
            _unitOfWork.Tags.AddRange(tags);
            _unitOfWork.Complete();

            return Json(new { success = true, responseMessage = "Tag successfully added", Id = tagObj.Sub_Id }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Delete Tags
        //Delete tag object
        public JsonResult DeleteTag(int id)
        {
            var tag = _unitOfWork.Tags.GetTagWithSub(id);
            _unitOfWork.Tags.RemoveRange(tag);
            _unitOfWork.Complete();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //Delete one tag
        public ActionResult DeleteOneTag(int id)
        {
            var tag = _unitOfWork.Tags.Get(id);
            _unitOfWork.Tags.Delete(tag);
            _unitOfWork.Complete();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Add Characteristic

        [HttpPost]
        public ActionResult AddCharacteristic(Language langObj)
        {
            if (langObj == null)
            {
                return new HttpNotFoundResult();
            }

            var language = new Language();

            if (langObj.Id != 0)
            {
                language.Language1 = langObj.Language1;
                language.Id = langObj.Id;
                _unitOfWork.Languages.Update(language);
                _unitOfWork.Complete();

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //  language.Id = 0;
                language.Language1 = langObj.Language1;
                _unitOfWork.Languages.Add(language);
            }

            _unitOfWork.Complete();
            var ch_id = language.Id;

            var characteristic = new Characteristic();
            characteristic.Language_Id = ch_id;
            _unitOfWork.Characteristics.Add(characteristic);
            _unitOfWork.Complete();
            var char_id = characteristic.Id;

            return Json(new { success = true, id = char_id }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Delete Characteristic

        public ActionResult DeleteCharacteristic(int id)
        {
            _unitOfWork.Subs.ClearCharacteristic(id);
            var characteristic = _unitOfWork.Characteristics.Get(id);
            _unitOfWork.Characteristics.Delete(characteristic);
            _unitOfWork.Complete();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
