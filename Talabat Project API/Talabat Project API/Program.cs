using Core.Entities.Identity;
using Core.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using Repository.Data.DataSeed;
using Repository.IdentityData;
using Repository.IdentityData.DataSeed;
using StackExchange.Redis;
using Talabat_Project_API.Errors;
using Talabat_Project_API.Extensions;
using Talabat_Project_API.Helper;
using Talabat_Project_API.MiddleWares;

namespace Talabat_Project_API
{
    public class Program
	{
		public async static Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});
			builder.Services.AddSingleton<IConnectionMultiplexer>(option=>
			{
				var connection = builder.Configuration["RedisConnection"];
				return ConnectionMultiplexer.Connect(connection);
			});
			builder.Services.AddDbContext<IdentityContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});
			builder.Services.applyServices(builder.Configuration);
			
			var app = builder.Build();

			var serviceScope = app.Services.CreateScope();
			var storeContext = serviceScope.ServiceProvider.GetRequiredService<StoreContext>();// getting store context by explicitly injecting parameters using CLR
			var IdentityContext = serviceScope.ServiceProvider.GetRequiredService<IdentityContext>();// getting Identity context by explicitly injecting parameters using CLR
			var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
			var loggerFactory = serviceScope.ServiceProvider.GetRequiredService<ILoggerFactory>();// getting i logger factory by explicitly injecting parameters using CLR
			var iLogger = loggerFactory.CreateLogger<Program>();// creating logger to this class ((program))
			try
			{
				await storeContext.Database.MigrateAsync();// updates database with the pending migrations 
				await StoredContextSeed.SeedAsync(storeContext);// fetching data from json files and insert it into database
				await IdentityContext.Database.MigrateAsync();
				await IdentityDataSeed.IdentitySeedAsync(userManager);
			}
			catch (Exception ex)
			{
				iLogger.LogError(ex,"Error occured in data base while updating or seeding data");
			}

			
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionHandlerMiddleWare>();
				app.UseSwagerMiddleWares();
			}
			
			app.UseStaticFiles();
			app.UseStatusCodePagesWithReExecute("/error/{0}");// if user entered any route not found clr will redirect him to error/code
			app.UseHttpsRedirection();
			app.UseCors("MyPolicy");
			app.UseAuthentication();
			app.UseAuthorization();
			

			app.MapControllers();

			app.Run();
		}
	}
}