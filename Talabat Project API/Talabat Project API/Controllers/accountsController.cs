using AutoMapper;
using Core.Entities.Identity;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Talabat_Project_API.DTOS;
using Talabat_Project_API.Errors;
using Talabat_Project_API.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Talabat_Project_API.Controllers
{
	
	public class accountsController : ApiBaseController
	{
		public UserManager<AppUser> _UserManager { get; set; }
		public SignInManager<AppUser> _SignInManager { get; set; }
		public IMapper _Mapper { get; set; }
		public ITokenService TokenService { get; set; }

		public accountsController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , IMapper mapper , ITokenService tokenService)
        {
			_UserManager = userManager;
			_SignInManager = signInManager;
			_Mapper = mapper;
			TokenService = tokenService;
		}


        [HttpPost("login")]// we only use http post because in api we dont need to return view as in httpget of Login in mvc
		public async Task<ActionResult<UserDto>> LogIn(LoginDto model)
		{
			 var user =await _UserManager.FindByEmailAsync(model.Email);
			if (user is null) return Unauthorized(new ApiErrorResponse(401));
			var result = await _SignInManager.CheckPasswordSignInAsync(user, model.Password,false);
			if(!result.Succeeded) return Unauthorized(new ApiErrorResponse(401));
			//await _SignInManager.SignInAsync(user,true); // we don't use SignInAsync in API because it will generate weak token based on default token provider for microsoft asp but we will use JWT to create our token and front end will use it in the browser 
			var userDto = _Mapper.Map<UserDto>(model);
			userDto.Token = await TokenService.CreateTokenAsync(user);
			return Ok(userDto);
		}

		[HttpPost("resister")] // we only use http post because in api we dont need to return view as in httpget of register in mvc
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			if(CheckEmailExist(model.Email).Result.Value)// we used Result to make it work synchronous not async because we need it to block the following code then we used value to conver task<bool> to bool
			{
				return BadRequest(new ApiErrorResponse(400, "This Email Is Already Exist"));
			}
			var user = new AppUser()
			{
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				DisplayName = model.DisplayName,
				UserName = model.Email.Split('@')[0]
			};

			var result = await _UserManager.CreateAsync(user,model.Password);
			if (!result.Succeeded) return BadRequest(new ApiErrorResponse(400));
			 var userDto = _Mapper.Map<UserDto>(model);
			userDto.Token = await TokenService.CreateTokenAsync(user);
			return Ok(userDto);
		}

		[HttpGet("getuser")]
		[Authorize]
		public async  Task<ActionResult<UserDto>> GetCurrentUser()
		{
			 var email =  User.FindFirstValue(ClaimTypes.Email);
			 var user = await _UserManager.FindByEmailAsync(email);
			if (user == null) return BadRequest(new ApiErrorResponse(400));
			 var userDto =  _Mapper.Map<UserDto>(user);
			userDto.Token = await TokenService.CreateTokenAsync(user);
			return Ok(userDto);

		}

		[HttpGet("GetAddresse")]
		[Authorize]
		public async Task<ActionResult<UserAddresseDto>> GetCurrentUserAddresse()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);

			var user = await _UserManager.FindWithAddresseByEmailAsync(email);

			if (user == null) return BadRequest(new ApiErrorResponse(400));
			var userAddresseDto = _Mapper.Map<UserAddresseDto>(user.addresse);
			return Ok(userAddresseDto);

		}

		[HttpPut("UpdateAddresse")]
		[Authorize]
		public async Task<ActionResult<UserAddresseDto>> UpdateCurrentUserAddresse(UserAddresseDto addresseDto)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);

			var user = await _UserManager.FindWithAddresseByEmailAsync(email);

			if (user == null) return Unauthorized(new ApiErrorResponse(401));
			var addresse = _Mapper.Map<Addresse>(addresseDto);
			addresse.AppUserId = user.Id;//to tell EF that this object has the Fk that you are tracking so accept the updating in data
			addresse.id = user.addresse.id;//to tell EF that this object has the id that you are tracking so accept the updating in data
			user.addresse = addresse;
			var result = await _UserManager.UpdateAsync(user);
			if(!result.Succeeded)
			{
				return BadRequest(new ApiErrorResponse(401));
			}
			

			return Ok(addresseDto);

		}


		[HttpGet("emailexists")]
		public async Task<ActionResult<bool>> CheckEmailExist(string email)
		{
			var result = await _UserManager.FindByEmailAsync(email);
			if (result is null) return false;
			return true;
		}


	}
}
