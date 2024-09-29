using AutoMapper;
using Company.G03.BLL;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Interfaces;
using Company.G03.DAL.Model;
using Company.G03.PL.Helper;
using Company.G03.PL.Mapping;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace Company.G03.PL.Controllers
{
	[Authorize]

	public class EmployeesController : Controller
    {
        //private readonly IEmployeeRepository employeeRepo;
        //private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public EmployeesController
            (
            IMapper mapper,
            IUnitOfWork unitOfWork

            )
        {
            //employeeRepo = employeeRepository; 
            //this.departmentRepository = departmentRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork; 
        }
        public async Task<IActionResult> Index(string name)
        {

            var Employees = Enumerable.Empty<Employee>();
            //ViewData["Message"] = "Hello world From ViewData";
            //ViewBag.Message = "Hello from ViewBag";
            //TempData["Message"] = "Hello From Temp Data";
            if (string.IsNullOrEmpty(name)) {

             Employees= await unitOfWork.EmployeeRepository.GetAllAsync();

            }
            else
            {
                Employees = await unitOfWork.EmployeeRepository.GetByNameAsync(name);
            }
            var Result = mapper.Map<IEnumerable<EmployeeViewModel>>(Employees);
            return View(Result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var Departments  = await unitOfWork.DepartmentRepository.GetAllAsync();

            ViewData["Departments"] = Departments;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
         

            if (ModelState.IsValid)
            {
                if(model.Image is not null)
                model.ImageName = DocumentSettings.UploadFile(model.Image,"images");


              var employee = mapper.Map<Employee>(model);

                var Count = await unitOfWork.EmployeeRepository.AddAsync(employee);
                if (Count > 0)
                {
                    TempData["Message"] = "Employee Created Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Employee isn't Created";
                }
            }
            TempData["Message"] = "Employee isn't Created";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id , string viewName="Details")
        {
            if (id is null) return BadRequest();
            var Employee= await unitOfWork.EmployeeRepository.GetAsync(id);
            if (Employee == null)
            {
                return NotFound();
            }
            var EmployeeViewModels = mapper.Map<EmployeeViewModel>(Employee);
            return View(viewName, EmployeeViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewData["Departments"] = await unitOfWork.DepartmentRepository.GetAllAsync();
            return await Details(id, "Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute]int? id,EmployeeViewModel model)
        {

            if (id is null) return BadRequest();
            if (model.Id != id) return BadRequest();
            if (ModelState.IsValid)
            {
                //AutoMapping
                if(model.ImageName is not null)
                {
                    DocumentSettings.deleteFile(model.ImageName, "images");
                }
                if(model.Image is not null)
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

                var employee = mapper.Map<Employee>(model);

                var Employee = await unitOfWork.EmployeeRepository.UpdateAsync(employee);
                if (Employee > 0)
                {
                    return RedirectToAction("Index");
                }
            }
           
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id is null) return BadRequest();
            if (model.Id != id) return BadRequest();
            if (ModelState.IsValid)
            {
                //Auto Mapping
                var employee = mapper.Map<Employee>(model);

                var Employee = await unitOfWork.EmployeeRepository.DeleteAsync(employee);
                if (Employee > 0)
                {
                    if(model.ImageName is not null)
                    DocumentSettings.deleteFile(model.ImageName,"images");
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

    }
}
