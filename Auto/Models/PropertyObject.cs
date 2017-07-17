using System;

namespace testAdmin.Models
{
    public class PropertyObject
    {
        public int Id { get; set; }

        public int Subject_id { get; set; }

        public string Properties { get; set; }

        public Nullable<int> VisualRepresentation_Id { get; set; }

        public Nullable<int> Description_Id { get; set; }

        public string Description { get; set; }
    }
}