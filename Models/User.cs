using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User_Dashboard.Data;

namespace User_Dashboard.Models
{
    public class User
    {
        [Key]
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
        [UniqueEmail(typeof(User))]
        public string Email { get; set; }

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

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserLevel? UserLevel { get; set; }
        public List<Comment>? Comments { get; set; }

        [InverseProperty("UserMessage")]
        public List<Message>? Messages { get; set; }

        [InverseProperty("RecipientMessage")]
        public List<Message>? ReceivedMessages { get; set; }
    }

    public class UniqueEmailAttribute : ValidationAttribute
    {
        private readonly Type _CastType;
        public UniqueEmailAttribute(Type castType)
        {
            _CastType = castType;
        }
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            if (value == null)
            {
                return new ValidationResult("Email is required!");
            }

            LoginContext _context = (LoginContext)
                validationContext.GetService(typeof(LoginContext));
            //Casteo dinamico para poder obtener el tipo en especifico del modelo en cuestion
            var currentObject = Convert.ChangeType(validationContext.ObjectInstance, _CastType);
            //Predefinimos el 0 en caso que el registro aun no exista entonces no tiene caso buscar un valor que aun no existe en estos casos solo tiene sentido ver si el email existe.
            int Id = 0;
            if (currentObject != null)
            {
                //Obtenemos la propiedad Id suponiendo que todos los que usen esta validación tienen esta propiedad.
                var valueId = currentObject.GetType().GetProperty("Id");
                if (valueId != null && valueId.PropertyType == typeof(int))
                {
                    Id = (int)valueId.GetValue(currentObject);
                }
            }

            if (Id != 0)
            {
                //Validamos si el email existe y verificamos que el id del usuario sea distinto al del email en caso que uno quiera actualizar no tener problemas de colision por duplicidad
                if (_context.Users.Any(e => e.Email == value.ToString() && e.Id != Id))
                {
                    return new ValidationResult("Email must be unique!");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                //validamos si existe un email.
                if (_context.Users.Any(e => e.Email == value.ToString()))
                {
                    return new ValidationResult("Email must be unique!");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}
