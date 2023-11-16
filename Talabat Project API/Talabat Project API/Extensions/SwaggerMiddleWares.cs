using System.Runtime.CompilerServices;

namespace Talabat_Project_API.Extensions
{
	public static class SwaggerMiddleWares
	{
		public static WebApplication UseSwagerMiddleWares(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}
	}
}
