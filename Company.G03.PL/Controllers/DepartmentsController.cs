using Microsoft.AspNetCore.Mvc;
using Company.G03.DAL.Interfaces;
using Company.G03.DAL.Model;
using Company.G03.BLL;
using Microsoft.AspNetCore.Authorization;
namespace Company.G03.PL.Controllers
{
	[Authorize]
	public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentsController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        [HttpGet]
        public IActionResult Create() {
        return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department entity)
        {
            if(entity.DateOfCreation == DateTime.MinValue)
            {
                ModelState.AddModelError("DateOfCreation", "Date Of Creation is Required");
            }

            if (ModelState.IsValid)
            {
                var Count = await _unitOfWork.DepartmentRepository.AddAsync(entity);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }

        [HttpGet]

        public async Task<IActionResult> Details(int? id,string ViewName= "Details")
        {
            if(id is null)
            {
                return BadRequest();
            }
            var dept = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (dept is null) return NotFound(); //Error404
            return View(ViewName,dept);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            return await Details(id,"Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id,Department entity)
        {
            try
            {
                if (id != entity.Id) BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = await _unitOfWork.DepartmentRepository.UpdateAsync(entity);
                    if (Count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(entity);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id,Department entity)
        {
            if(id != entity.Id) BadRequest();

            if(entity is null) return BadRequest();
            var Count= await _unitOfWork.DepartmentRepository.DeleteAsync(entity);
            if (Count > 0)
            {
                return RedirectToAction("Index");
            }
            return View(entity);
        }
    }
}