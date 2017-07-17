using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace testAdmin.Models
{
    [Serializable]
    public class VisualRepresentationModel
    {
        public int Id { get; set; }       
       
        public string Code { get; set; }

        public string ImageLink { get; set; }

        public HttpPostedFileBase Image { get; set; }
        
    }
}