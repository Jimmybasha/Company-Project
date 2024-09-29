using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
	public class ResetPasswordViewModel
	{

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Passwords' Doesn't Match")]
		public string ConfirmPassword { get; set; }
	}
}
