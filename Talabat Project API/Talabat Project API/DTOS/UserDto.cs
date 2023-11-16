using System.ComponentModel.DataAnnotations;

namespace Talabat_Project_API.DTOS
{
	public class UserDto
	{
		
		public string Email { get; set; }

		
		public string DisplayName { get; set; }

		public string Token { get; set; }
	}
}
