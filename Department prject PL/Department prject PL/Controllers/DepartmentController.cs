using AutoMapper;
using Castle.Core.Internal;
using Deapartment_project_BLL.Interfaces;
using Department_prject_PL.Models;
using Department_project_DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Department_prject_PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {

        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWorkRepository UnitOfWork, IMapper mapper)
        {
            _unitOfWork=UnitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string SearchValue="")
        {
           
            IEnumerable<Department> departments=null;
            IEnumerable<DepartmentViewModel> departmentViewModels = null;
            if (SearchValue.IsNullOrEmpty())
            {
             departments=_unitOfWork._Department.GetAll();
                departmentViewModels=_mapper.Map<IEnumerable<DepartmentViewModel>>(departments); 
            }
            else
            {
                departments = _unitOfWork._Department.Search(SearchValue);
                departmentViewModels = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            }
            
            return View(departmentViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DepartmentViewModel());

        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if(departmentViewModel is null)
            {
                return BadRequest();
            }
           if(ModelState.IsValid)
            {
                var department= _mapper.Map<Department>(departmentViewModel);
                _unitOfWork._Department.Create(department);
                _unitOfWork.Complete();
                return Redirect(nameof(Index));
            }
            return View(departmentViewModel);

        }


        
        [HttpGet]
        public IActionResult Update([FromRoute]int id,string ViewName="Update")
        {
            var department = _unitOfWork._Department.GetById(id);
            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);
            if (departmentViewModel is null)
            {
                return BadRequest();
            }
            return View(ViewName,departmentViewModel);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Update(DepartmentViewModel departmentViewModel)
        {
           
            if (departmentViewModel == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(departmentViewModel);
                _unitOfWork._Department.Update(department);
                _unitOfWork.Complete();
                return Redirect("/Department/Index");
            }
            return View(departmentViewModel);

        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
           

            return Update(id,"Details");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var department = _unitOfWork._Department.GetById(id);

            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);

            if (departmentViewModel is null)
            {
                return BadRequest();
            }
            return View(departmentViewModel);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentViewModel)
        {

            if (departmentViewModel == null)
            {
                return BadRequest();
            }
            var department = _mapper.Map<Department>(departmentViewModel);
            _unitOfWork._Department.Delete(department);
            _unitOfWork.Complete();
            return Redirect("/Department/Index");
            
            

        }
    }
}
