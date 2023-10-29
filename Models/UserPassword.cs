using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace User_Dashboard.Models
{
    public class UserPassword
    {
        [Required(ErrorMessage = "Password is required!")]
        [MinLength(8, ErrorMessage = "Password requires 8 values as minimum")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [NotMapped]
        [Required(ErrorMessage = "Password is required!")]
        [MinLength(8, ErrorMessage = "Password requires 8 values as minimum")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirm")]
        public string ConfirmPassword { get; set; }
    }
}
