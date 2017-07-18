using Logs.Models.ModelDTO;


namespace Logs.Models
{
    public class UserDetailModel
    {
        //public Nullable<DateTime> DateOfRegistration { get; set; }
        public UserDetailModel(UserDetailDTO userDetail, string[] Logins, 
            bool isAuth, int UserId)
        {
            this.userDetail = userDetail;
            this.Logins = Logins;
            this.isAuth = isAuth;
            this.UserId = UserId;
        }
        public bool isAuth { get; set; }

        public UserDetailDTO userDetail { get; set; }

        public string[] Logins { get; set; }

        public int UserId { get; set; }
    }
}
