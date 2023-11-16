using AutoMapper;
using Castle.Core.Internal;
using Department_prject_PL.Models;
using Department_project_DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Department_prject_PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {

        public IMapper _Mapper { get; set; }
        public UserManager<UserApplication> _UserManager { get; set; }
        public RoleManager<IdentityRole> _RoleManager { get; set; }

        public UserController(IMapper mapper , UserManager<UserApplication> userManager , RoleManager<IdentityRole> roleManager)
        {
            _Mapper = mapper;
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        

        public async Task<IActionResult> Index(string SearchValue = "")
        {

           
			IEnumerable<UsersViewModel> usersViewModel = null;
			if (SearchValue.IsNullOrEmpty())
            {
                usersViewModel = await _UserManager.Users.Select( u => new UsersViewModel()
                {
                    Id = u.Id,
                    Name = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    DateOfCreation = u.DateOfCreation,
                    Roles =  _UserManager.GetRolesAsync(u).Result


                }).ToListAsync();
               
                
            }
            else
            {
				usersViewModel = await _UserManager.Users.
                    Where(u=>u.Email.Trim().ToLower().Contains(SearchValue.Trim().ToLower())==true).
                    Select(u=>new UsersViewModel()
                    {
                        Id= u.Id,
						Name = u.UserName,
						Email = u.Email,
						PhoneNumber = u.PhoneNumber,
                         DateOfCreation = u.DateOfCreation
                    }).ToListAsync();
                
            }

            return View(usersViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> Update([FromRoute] string id, string ViewName = "Update")
        {
            var user =await _UserManager.FindByIdAsync(id);


            if (user is null)
            {
                return BadRequest();
            }

            UsersViewModel usersViewModel= new UsersViewModel()
           {
               Id = user.Id,
               Name = user.UserName,
               Email = user.Email,
               PhoneNumber = user.PhoneNumber,
               DateOfCreation = user.DateOfCreation
           };


            return View(ViewName, usersViewModel);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(UsersViewModel usersViewModel)
        {


            if (ModelState.IsValid)
            {
               var user= await _UserManager.FindByIdAsync (usersViewModel.Id);
                if(user.PhoneNumber!=usersViewModel.PhoneNumber)
                    user.PhoneNumber = usersViewModel.PhoneNumber;
                if (user.UserName != usersViewModel.Name)
                    user.UserName = usersViewModel.Name;
                if (user.Email != usersViewModel.Email)
                    user.Email = usersViewModel.Email;

              await  _UserManager.UpdateAsync(user);
                
                return RedirectToAction("Index");
            }
            return View(usersViewModel);

        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] string id)
        {


            return await Update(id, "Details");
        }


        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
           
            return await Update(id,"Delete");

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(UsersViewModel usersViewModel)
        {

            if (usersViewModel == null)
            {
                return BadRequest();
            }
            var user=await _UserManager.FindByIdAsync(usersViewModel.Id);
            await _UserManager.DeleteAsync(user);
           
            return RedirectToAction("Index");



        }

    }
}
