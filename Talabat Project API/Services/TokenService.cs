using Core.Entities.Identity;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public async Task<string> CreateTokenAsync(AppUser user)
		{
		
			//pay loads

			//1- provate claims

			var AuthClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName,user.DisplayName),
				new Claim(ClaimTypes.Email,user.Email)
			};

			//2- register claims > they are in app settings >> like  ValidIssuer , ValidAudience , DurationOfTokenInDayes



			// adding security key to add it to signature of token
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));// we used encoding to convert string to array of bytes as it is needed by symmetric key ,, this key will be shared between front and back end to validate or generate tokens

			//generating token
			var token = new JwtSecurityToken(issuer: _configuration["Jwt:ValidIssuer"],  // the source that will generate the token token
											 audience: _configuration["Jwt:ValidAudience"],
											 expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationOfTokenInDayes"])), // expiration date will be date of now + the duration in app setting
											  claims: AuthClaims,
											 signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256));
											  
			return new JwtSecurityTokenHandler().WriteToken(token); // it works syncronus
		}                  
	}
}
