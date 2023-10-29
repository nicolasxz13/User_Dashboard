using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User_Dashboard.Data;

namespace User_Dashboard.Models
{
    public class UserLevel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User_level")]
        public string Name { get; set; }
        public int Userlevel { get; set; }

        List<User>? Users{get;set;}
    }
}
