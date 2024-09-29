using AutoMapper;
using Company.G03.BLL;
using Company.G03.DAL.Model;
using Company.G03.PL.Helper;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL.Controllers
{
    public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;

		public UserController(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}
		public async Task<IActionResult> Index(string email)
		{
			var users = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(email))
			{
				users = await userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					Username = U.UserName,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
			
			}).ToListAsync();
                foreach (var user in users)
                {
                    var UserFromDB = await userManager.FindByIdAsync(user.Id);
                    user.Roles = await userManager.GetRolesAsync(UserFromDB);
                }
            } 
			else
			{
				users = await userManager.Users.Where(U => U.Email.ToLower()
					.Contains(email.ToLower())).Select(U => new UserViewModel()
					{
						Id = U.Id,
						FirstName = U.FirstName,
						LastName = U.LastName,
						Email = U.Email,
					
					}).ToListAsync();
                foreach (var user in users)
                {
                    var UserFromDB = await userManager.FindByIdAsync(user.Id);
                    user.Roles = await userManager.GetRolesAsync(UserFromDB);
                }

            }
			return View(users);

		}

		[HttpGet]
		public async Task<IActionResult> Details(string? id, string viewName = "Details")
		{
			if (id is null) return BadRequest();
			var userFromDB = await userManager.FindByIdAsync(id);
			if (userFromDB is null)
			{
				return NotFound();
			}
			var userMapped = new UserViewModel()
			{
				Id = userFromDB.Id,
				Email = userFromDB.Email,
				FirstName = userFromDB.FirstName,
				LastName = userFromDB.LastName,
				Username = userFromDB.UserName,
			};
			return View(viewName, userMapped);
		}


		[HttpGet]
		public async Task<IActionResult> Update(string? id)
		{
			return await Details(id, "Update");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		
		public async Task<IActionResult> Update([FromRoute] string? id, UserViewModel model)
		{

			if (id is null) return BadRequest();
			if (model.Id != id) return BadRequest();

			if (ModelState.IsValid)
			{
				var userFromDB = await userManager.FindByIdAsync(id);

				if (userFromDB is null)
					return NotFound();

				userFromDB.FirstName = model.FirstName;
				userFromDB.LastName = model.LastName;
				userFromDB.Email = model.Email;
				userFromDB.UserName = model.Username;

				var Result = await userManager.UpdateAsync(userFromDB);
				if (Result.Succeeded)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);

		}


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            if (id is null) return BadRequest();
            if (model.Id != id) return BadRequest();
            if (ModelState.IsValid)
            {
				var userFromDb= await userManager.FindByIdAsync(id);
				if (userFromDb is null)
					return NotFound();

                var Result = await userManager.DeleteAsync(userFromDb);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

		
    }
}
