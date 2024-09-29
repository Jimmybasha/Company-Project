using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="UserName is required")]
        public String Username { get; set; }


		[Required(ErrorMessage = "FirstName is required")]
		public String FirstName { get; set; }
		

		[Required(ErrorMessage = "LastName is required")]
		public String LastName { get; set; }


		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public String Password { get; set; }


		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Passwords' Doesn't Match")]
		public String ConfirmPassword { get; set; }


		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage ="Invalid Email Form")]
		public string Email { get; set; }

		[Required(ErrorMessage ="You Can't Create Account without Accepting Terms And Conditions")]
		public bool isAgree { get; set; }


    }
}
