using System;
using System.Net;
using testAdmin.Models;

namespace testAdmin.Service
{
    public class AdminService
    {
        public static Subject ModifySubject(Subject sub)
        {
            var modSub = new Subject();

            modSub.Id = sub.Id;
            modSub.Description = sub.Description;
            modSub.Key = sub.Key;
            modSub.Characteristics_Id = sub.Characteristics_Id;
            modSub.VisualRepresentation_Id = sub.VisualRepresentation_Id;

            return modSub;
        }

        public static Subject NewSubject(Subject sub)
        {
            var newSub = new Subject();

            newSub.Description = sub.Description;
            newSub.Key = sub.Key;
            newSub.Characteristics_Id = sub.Characteristics_Id;
            newSub.VisualRepresentation_Id = sub.VisualRepresentation_Id;

            return newSub;
        }

        public static byte[] GetImageArr(Uri url)
        {
            byte[] imageArr = null;

            using (var wc = new WebClient())
            {
                imageArr = wc.DownloadData(url);
            }

            return imageArr;
        }

        public static Property GetProperty(int id, PropertyObject propertyModel)
        {
            var propObject = new Property();

            propObject.Description_Id = id;

            propObject.Properties = propertyModel.Properties;
            propObject.Subject_id = propertyModel.Subject_id;
            propObject.VisualRepresentation_Id = propertyModel.VisualRepresentation_Id;

            return propObject;
        }

        public static Description GetDescription(string description)
        {
            var descriptionObj = new Description();

            descriptionObj.Description1 = description;

            return descriptionObj;
        }
    }
}