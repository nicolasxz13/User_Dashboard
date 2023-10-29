using System.ComponentModel.DataAnnotations;

namespace User_Dashboard.Models
{
    public class UserInformationAdmin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name requires 2 values as minimum")]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        [MinLength(2, ErrorMessage = "Last name requires 2 values as minimum")]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [UniqueEmail(typeof(UserInformationAdmin))]
        public string Email { get; set; }
        [Required(ErrorMessage = "User level is required")]
        public int IdUser_level { get; set; }
    }
}
