using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Talabat_Project_API.Extensions
{
	public static class UserManagerExtensions
	{

		public async static Task<AppUser?> FindWithAddresseByEmailAsync( this UserManager<AppUser> userManager , string email)
		{
			var user = await userManager.Users.Include(u => u.addresse).FirstOrDefaultAsync(u => u.Email == email);
			return user;
		}
	}
}
