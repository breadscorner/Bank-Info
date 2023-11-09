using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModel
{
    public class ClientAccountVM
    {
        [Key]
        public int AccountNum { get; set; }

        [Display(Name = "Client ID")]
        public int ClientID { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetical characters are allowed.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabetical characters are allowed.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public IList<ClientAccountVM> ClientAccounts { get; set; } = new List<ClientAccountVM>();
    }
}
