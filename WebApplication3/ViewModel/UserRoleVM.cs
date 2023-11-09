using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModel
{
    public class UserRoleVM
    {
        [Key]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabet characters are allowed.")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabet characters are allowed.")]
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; }
    }

}
