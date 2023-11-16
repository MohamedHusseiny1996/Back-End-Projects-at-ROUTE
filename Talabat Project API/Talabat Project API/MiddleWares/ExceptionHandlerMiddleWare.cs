using System.Net;
using System.Text.Json;
using Talabat_Project_API.Errors;

namespace Talabat_Project_API.MiddleWares
{
	public class ExceptionHandlerMiddleWare
	{
		private readonly ILogger<ExceptionHandlerMiddleWare> _logger;
		private readonly IHostEnvironment _env;
		private readonly RequestDelegate _Next;

		public ExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<ExceptionHandlerMiddleWare> logger ,  IHostEnvironment env  )
        {
			_logger = logger;
			_env = env;
			_Next = Next;
		}


		public async Task InvokeAsync(HttpContext context)// the name must be InvokeAsync or Invoke 
		{
			try
			{
				await _Next.Invoke(context);
			}
			catch(Exception ex) 
			{
				ExceptionResponse response;
				if(_env.IsDevelopment())
				{
					_logger.LogError(ex,ex.Message);
					response = new ExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
					
				}
				else
				{
					response = new ExceptionResponse((int)HttpStatusCode.InternalServerError);
				}

				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";
				var options = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				};
				var json = JsonSerializer.Serialize(response, options);
				context.Response.WriteAsync(json);
			}
			
		}



    }
}
