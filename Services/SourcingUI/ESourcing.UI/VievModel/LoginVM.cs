using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.VievModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email is required")]
        [Display(Name ="Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage ="Password contains at least 4 characters")]
        public string Password { get; set; }
    }
}
