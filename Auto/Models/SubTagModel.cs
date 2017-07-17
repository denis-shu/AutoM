using System;

namespace testAdmin.Models
{
    [Serializable]
    public class SubTagModel
    {
        public Nullable<int> Sub_Id { get; set; }

        public string [] Tag1 { get; set; }
    }
}