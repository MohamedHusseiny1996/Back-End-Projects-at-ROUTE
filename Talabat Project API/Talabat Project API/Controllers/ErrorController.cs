using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Project_API.Errors;

namespace Talabat_Project_API.Controllers
{
	[Route("error/{statusCode}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]//to tell swager not to document this class actions because we will not user routing here but we will be redirected here using clr only not routing
	public class ErrorController : ControllerBase
	{
		
		public ActionResult Error(int statusCode)
		{
			return NotFound(new ApiErrorResponse(statusCode));
		}
	}
}
