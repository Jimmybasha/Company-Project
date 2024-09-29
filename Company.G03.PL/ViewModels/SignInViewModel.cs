using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
	public class SignInViewModel
	{

		[Required(ErrorMessage ="Email is required")]
        public string EmailAddress { get; set; }

		[Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
		public bool Remember { get; set; }
    }
}
