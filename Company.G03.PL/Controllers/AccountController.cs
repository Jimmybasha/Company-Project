using Company.G03.DAL.Model;
using Company.G03.PL.Helper;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;

namespace Company.G03.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager
			
			)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		} 

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{

				var user = await userManager.FindByNameAsync(model.Username);

				if (user is null)
				{
					user = await userManager.FindByEmailAsync(model.Email);
					if (user is null)
					{
					user = new ApplicationUser()
					{
						UserName = model.Username,
						Email = model.Email,
						FirstName = model.FirstName,
						LastName = model.LastName,
						IsAgree = model.isAgree,
					};
					var result = await userManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
					{
						return RedirectToAction("SignIn");
					}
					else
					{
                            foreach (var err in result.Errors)
                            {
								ModelState.AddModelError(string.Empty,err.Description );
                            }
                        }
				
					}
					ModelState.AddModelError(string.Empty, "Email is already used");
				}

				ModelState.AddModelError(string.Empty, "Username is already used");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{

			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.EmailAddress);
				if(user is not null)
				{
					var flag = await userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var Result = await signInManager.PasswordSignInAsync(user,model.Password, model.Remember,false);
						if (Result.Succeeded)
						{
						return RedirectToAction("Index", "Home");
						}
					}
				}
			}
			ModelState.AddModelError(string.Empty, "Invalid Login Information");
			return View();
		}


		public new async Task<IActionResult> SignOut()
		{

			await signInManager.SignOutAsync();

			return RedirectToAction("SignIn");
			
		}

		public IActionResult ForgetPassword()
		{
			return View();
		}
		public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
		{
			var user = await userManager.FindByEmailAsync(model.EmailAddress);


			if (user is not null)
			{
				var token = await userManager.GeneratePasswordResetTokenAsync(user);

				var url =Url.Action("ResetPassword", "Account", new {email=model.EmailAddress,token= token},Request.Scheme);

				var email = new DAL.Model.Email()
				{
					To = model.EmailAddress,
					Subject = "Reset password",
					Body = url,

				};
				EmailSettings.SendEmail(email);
				return RedirectToAction("CheckUrInbox");
			}

			ModelState.AddModelError(string.Empty, "Invalid");

			return View();
		}

		[HttpGet]
		public IActionResult CheckUrInbox()
		{
			return View();
		}


		[HttpGet]

		public IActionResult ResetPassword(string email,string token)
		{
			TempData["Email"] = email;
			TempData["token"] = token;
			return View();
		}



		[HttpPost]

		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{


			if (ModelState.IsValid)
			{

				var email = TempData["Email"]  as string;
				var token = TempData["token"]  as string;

				var user = await userManager.FindByEmailAsync(email);
				if(user is not null)
				{
					var result =  await userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
					{
						return	RedirectToAction("SignIn");
					}
				}
			}

			ModelState.AddModelError(string.Empty, "Invalid");
			return View();
		}
        public IActionResult AccessDenied()
        {
            return View();
        }

    }

}
 