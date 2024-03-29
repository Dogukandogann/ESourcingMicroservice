using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.VievModel
{
    public class AppUserVM
    {
        [Required(ErrorMessage ="UserName is required")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "IsBuyer is required")]
        [Display(Name = "IsBuyer")]
        public bool IsBuyer { get; set; }
        [Required(ErrorMessage = "IsSeller is required")]
        [Display(Name = "IsSeller")]
        public bool IsSeller { get; set; }
        public int UserSelectTypeId { get; set; }
    }
}
