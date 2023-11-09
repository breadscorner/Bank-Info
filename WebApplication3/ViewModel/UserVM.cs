using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModel
{
    public class UserVM
    {
        [Key]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
