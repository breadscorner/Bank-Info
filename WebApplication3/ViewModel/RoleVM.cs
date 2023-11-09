using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModel
{
    public class RoleVM
    {
        // Create nullable string so automated form template won’t expect value for ID
        private string _Id;
        public string Id
        {
            get { return _Id ?? String.Empty; }
            set { _Id = value; }
        }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
