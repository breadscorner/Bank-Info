using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModel
{
    public class UserRoleEditVM
    {
        [Key]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [Required]
        public string Role { get; set; }
    }

}
