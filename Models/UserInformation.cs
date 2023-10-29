using System.ComponentModel.DataAnnotations;

namespace User_Dashboard.Models
{
    public class UserInformation
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
        [UniqueEmail(typeof(UserInformation))]
        public string Email { get; set; }
    }

}
