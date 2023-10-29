namespace User_Dashboard.Models
{
    public class UserAdminViewModel
    {
        public UserInformationAdmin InformationAdmin {get;set;}
        public UserPassword UserPassword { get; set; }
        public List<UserLevel> UserLevels { get; set; }
    }
}
