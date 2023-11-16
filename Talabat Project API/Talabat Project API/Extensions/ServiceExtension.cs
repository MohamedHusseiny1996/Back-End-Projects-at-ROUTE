using Core.Entities.Identity;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.IdentityData;
using Repository.UnitOfWork;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Talabat_Project_API.Errors;
using Talabat_Project_API.Helper;

namespace Talabat_Project_API.Extensions
{
	public static class ServiceExtension
	{
		public static IServiceCollection applyServices(this IServiceCollection services , IConfiguration configuration)
		{

			services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			services.AddCors(options =>
			{
				options.AddPolicy("MyPolicy", options =>
				{
					options.AllowAnyOrigin()
						   .WithOrigins(configuration["frontBaseUrl"])
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});

			//services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			services.AddScoped(typeof(IunitOfWork) , typeof(UnitOfWork));
			services.AddScoped(typeof(IOrderService), typeof(OrderService));
			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

			services.AddAutoMapper(typeof(Profiles));
			
			services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
			services.AddScoped(typeof(ITokenService), typeof(TokenService) );
			services.AddAuthentication(options=>options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)//this schema is used when the request comes to decrypt  the token using schema that he was generated with
			.AddJwtBearer(
				options =>  // after decription you will find some parameters to validate take them to validate
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["Jwt:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = configuration["Jwt:ValidAudience"],
					ValidateLifetime = true, // u don't have to send the expire date to validate it , it will be token from the token itself
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				};
			}
			);

			services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = (context) =>
				{
					var errors = context.ModelState.Where(p => p.Value.Errors.Count > 0)
					.SelectMany(p => p.Value.Errors).Select(e => e.ErrorMessage).ToArray();

					var response = new ValidationErrorResponse(400, null, errors);
					return new BadRequestObjectResult(response);
				};
			});
			return services;
		}
	}
}
