using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool isRoleConfirmed { get; set; }
    }

    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Kjo fushë eshte obligative")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Kjo fushë eshte obligative")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kjo fushë eshte obligative")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kjo fushë eshte obligative")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Kjo fushë është obligative")]
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Kjo fushë është obligative")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Fjalekalimet ndryshojne")]
        public string ConfirmPassword { get; set; }
    }

    public class UserEditViewModel
    {
        public string Id { get; set; }

		[Required(ErrorMessage = "Kjo fushë eshte obligative")]
		[Display(Name = "First name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Kjo fushë eshte obligative")]
		[Display(Name = "Last name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Kjo fushë eshte obligative")]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Kjo fushë eshte obligative")]
		[Display(Name = "Username")]
		public string UserName { get; set; }
		[Display(Name = "Phone number")]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Kjo fushë është obligative")]
        [Display(Name = "Roli")]
        public string RoleId { get; set; }

		[Required(ErrorMessage = "Kjo fushë është obligative")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Fjalekalimet ndryshojne")]
		public string ConfirmPassword { get; set; }
	}
}
