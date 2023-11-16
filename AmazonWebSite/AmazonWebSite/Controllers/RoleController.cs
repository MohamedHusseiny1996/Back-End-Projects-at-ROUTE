using AmazonWebSiteBLL.Interfaces;
using AmazonWebSitePL.Helper;
using AmazonWebSitePL.Models;
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
    public class RoleController : Controller
    {

        public IMapper _Mapper { get; set; }
        public UserManager<UserApplication> _UserManager { get; set; }
        public RoleManager<IdentityRole> _RoleManager { get; set; }

        public RoleController(IMapper mapper , UserManager<UserApplication> userManager , RoleManager<IdentityRole> roleManager)
        {
            _Mapper = mapper;
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        

        public async Task<IActionResult> Index(string SearchValue = "")
        {

           
			IEnumerable<RoleViewModel> roleViewModels = null;
			if (SearchValue.IsNullOrEmpty())
            {
                roleViewModels = await _RoleManager.Roles.Select( r => new RoleViewModel()
                {
                    Id = r.Id,
                    RoleName = r.Name
                   
                }).ToListAsync();
               
                
            }
            else
            {
                roleViewModels = await _RoleManager.Roles.
                    Where(r=>r.Name.Trim().ToLower().Contains(SearchValue.Trim().ToLower())==true).
                    Select(r=>new RoleViewModel()
                    {
                        Id = r.Id,
                        RoleName = r.Name
                    }).ToListAsync();
                
            }

            return View(roleViewModels);
        }


        public IActionResult Create()
        {


            return View(new RoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = roleViewModel.RoleName,
                    NormalizedName = roleViewModel.RoleName.ToUpper()
                };
               await _RoleManager.CreateAsync(role);
               
                return RedirectToAction("Index", "Role");

            }

            return View(roleViewModel);
        }




        [HttpGet]
        public async Task<IActionResult> Update([FromRoute] string id, string ViewName = "Update")
        {
            var role =await _RoleManager.FindByIdAsync(id);


            if (role is null)
            {
                return BadRequest();
            }

            RoleViewModel roleViewModel= new RoleViewModel()
           {
                Id = role.Id,
                RoleName = role.Name
            };


            return View(ViewName, roleViewModel);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(RoleViewModel roleViewModel)
        {


            if (ModelState.IsValid)
            {
               var role= await _RoleManager.FindByIdAsync (roleViewModel.Id);
                
                if (role.Name != roleViewModel.RoleName)
                    role.Name = roleViewModel.RoleName;
                

              await  _RoleManager.UpdateAsync(role);
                
                return RedirectToAction("Index");
            }
            return View(roleViewModel);

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
        public async Task<IActionResult> Delete(RoleViewModel roleViewModel)
        {

            if (roleViewModel == null)
            {
                return BadRequest();
            }
            var role=await _RoleManager.FindByIdAsync(roleViewModel.Id);
            await _RoleManager.DeleteAsync(role);
           
            return RedirectToAction("Index");



        }

        public async Task<IActionResult> AddRemoveUsers(string roleId)
        {
            List<UserInRoleViewModel> users=new List<UserInRoleViewModel>();

          var role = await _RoleManager.FindByIdAsync(roleId);
            ViewBag.RoleId = roleId;
            if(role is null)
            {
                return NotFound();
            }

            foreach(var user in await _UserManager.Users.ToListAsync())
            {
                UserInRoleViewModel userInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _UserManager.IsInRoleAsync(user, role.Name)) 
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                users.Add(userInRole);
            }
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddRemoveUsers(List<UserInRoleViewModel> users , string roleId)
        {
            var role = await _RoleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                foreach(var userInRole in users)
                {
                    var user = await _UserManager.FindByIdAsync(userInRole.UserId);
                    if(userInRole.IsSelected && ! await _UserManager.IsInRoleAsync(user,role.Name))
                    {
                        await _UserManager.AddToRoleAsync(user, role.Name);
                    }
                    if (! userInRole.IsSelected && await _UserManager.IsInRoleAsync(user, role.Name))
                    {
                        await _UserManager.RemoveFromRoleAsync(user, role.Name);
                    }

                }
                return RedirectToAction("Index");
            }

            return View(users);
        }

    }
}
