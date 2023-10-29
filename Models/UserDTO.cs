using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace User_Dashboard.Models
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(8, ErrorMessage = "Password requires 8 values as minimum")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
