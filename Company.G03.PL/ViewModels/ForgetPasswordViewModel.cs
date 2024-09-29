using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{
	
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Form")]
		public string EmailAddress { get; set; }

	}
}
