using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IdentityData.DataSeed
{
	
	public class IdentityDataSeed
	{
		
		
		public static async Task IdentitySeedAsync(UserManager<AppUser> userManager)
		{
			
			
			if(!userManager.Users.Any())
			{
				var user = new AppUser()
				{
					DisplayName = "mohamed husseiny",
					Email = "mohamed@gmail.com",
					UserName = "mohamed",
					PhoneNumber = "01245789635"
				};
				await userManager.CreateAsync(user,"P@ssw0rd");
			}
		}




    }
}
