using Company.G03.DAL.Model;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Company.G03.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Index(string role)
        {
            var roles = Enumerable.Empty<RoleViewModel>();

            if (string.IsNullOrEmpty(role))
            {
                roles = await roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name

                }).ToListAsync();
            }
            else
            {
                roles = await roleManager.Roles.Where(R => R.Name.ToLower()
                    .Contains(role.ToLower())).Select(R => new RoleViewModel()
                    {
                        Id = R.Id,
                        RoleName = R.Name

                    }).ToListAsync();
              

            }
            return View(roles);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name=model.RoleName
                };

                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var rolesFromDB = await roleManager.FindByIdAsync(id);
            if (rolesFromDB is null)
            {
                return NotFound();
            }
            var roleMapped = new RoleViewModel()
            {
                Id = rolesFromDB.Id,
                RoleName= rolesFromDB.Name
            };
            return View(viewName, roleMapped);
        }


        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update([FromRoute] string? id, RoleViewModel model)
        {

            if (id is null) return BadRequest();
            if (model.Id != id) return BadRequest();

            if (ModelState.IsValid)
            {
                var roleFromDB = await roleManager.FindByIdAsync(id);

                if (roleFromDB is null)
                    return NotFound();

                roleFromDB.Name = model.RoleName;

                var Result = await roleManager.UpdateAsync(roleFromDB);
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
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)
        {
            if (id is null) return BadRequest();
            if (model.Id != id) return BadRequest();
            if (ModelState.IsValid)
            {
                var roleFromDB = await roleManager.FindByIdAsync(id);
                if (roleFromDB is null)
                    return NotFound();

                var Result = await roleManager.DeleteAsync(roleFromDB);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUserRoles(string id)
        {
            if(id is null) return BadRequest();
            ViewData["roleId"] = id;
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) return BadRequest();
            var usersInRole = new List<UserInRoleViewModel>();
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users) {
                var userInRole = new UserInRoleViewModel() {

                    Id = user.Id,
                    Name = user.UserName,
                };
                if( await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.isSelected = true;
                }
                else
                {
                    userInRole.isSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);

        }
        

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUserRoles(string id,List<UserInRoleViewModel> users)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null) return BadRequest();

            if (ModelState.IsValid)
            {
                foreach (var  user in users)
                {
                    var userInDB = await userManager.FindByIdAsync(user.Id);
                    if (userInDB is null) return BadRequest();

                    if(user.isSelected && ! await userManager.IsInRoleAsync(userInDB,role.Name) )
                    {
                        await userManager.AddToRoleAsync(userInDB, role.Name); 
                    }
                    else if (!user.isSelected && await userManager.IsInRoleAsync(userInDB, role.Name))
                    {
                       await userManager.RemoveFromRoleAsync(userInDB, role.Name); 
                    }
                }
                return RedirectToAction("Update", new { id=role.Id });

            }
            return View(users);

        }



    }
}
