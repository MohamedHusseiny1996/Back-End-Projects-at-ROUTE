using AutoMapper;
using Castle.Core.Internal;
using Deapartment_project_BLL.Interfaces;
using Department_prject_PL.Models;
using Department_prject_PL.Helper;
using Department_project_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Department_prject_PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {

        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWorkRepository UnitOfWork,IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string SearchValue = "")
        {
            
            IEnumerable<Employee> employees = null;
            IEnumerable<EmployeeViewModel> employeesViewModel = null;
            if (SearchValue.IsNullOrEmpty())
            {
                employees = _unitOfWork._Employee.GetAll();
                employeesViewModel = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
            else
            {
                employees = _unitOfWork._Employee.Search(SearchValue);
                employeesViewModel = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
            return View(employeesViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _unitOfWork._Department.GetAll();
            ViewBag.departments = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(new EmployeeViewModel());

        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel is null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                string FileName=DocumentSetting.UploadFile(employeeViewModel.image, "images");
                employeeViewModel.ImageUrl = FileName;
               var employee= _mapper.Map<Employee>(employeeViewModel);
                _unitOfWork._Employee.Create(employee);
                _unitOfWork.Complete();
                TempData["created"] = "Employee Added Successfully";
                return Redirect(nameof(Index));
            }

            var departments = _unitOfWork._Department.GetAll();
            ViewBag.departments = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(employeeViewModel);

        }



        [HttpGet]
        public IActionResult Update([FromRoute] int id, string ViewName = "Update")
        {
            var employee = _unitOfWork._Employee.GetById(id);
            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            if (employeeViewModel is null)
            {
                return BadRequest();
            }
            return View(ViewName, employeeViewModel);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Update(EmployeeViewModel employeeViewModel)
        {

            if (employeeViewModel == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeViewModel);
                _unitOfWork._Employee.Update(employee);
                _unitOfWork.Complete();
                return Redirect("/Employee/Index");
            }
            return View(employeeViewModel);

        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            

            return Update(id, "Details");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var employee = _unitOfWork._Employee.GetById(id);
            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            if (employeeViewModel is null)
            {
                return BadRequest();
            }
            return View(employeeViewModel);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeViewModel)
        {

            if (employeeViewModel == null)
            {
                return BadRequest();
            }
            var employee= _mapper.Map<Employee>(employeeViewModel);
            _unitOfWork._Employee.Delete(employee);
            _unitOfWork.Complete();
            return Redirect("/Employee/Index");



        }
    }
}
