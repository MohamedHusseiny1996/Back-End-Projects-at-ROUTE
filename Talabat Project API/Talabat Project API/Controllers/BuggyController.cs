using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Talabat_Project_API.DTOS;
using Talabat_Project_API.Errors;

namespace Talabat_Project_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : ControllerBase
	{
		private readonly StoreContext _context;
		private readonly IMapper _mapper;

		public BuggyController(StoreContext context , IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
        [HttpGet("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			var product = _context.products.Find(100);
			if(product == null) 
				return NotFound(new ApiErrorResponse(404));

			return Ok(product);
		}


		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			var product = _context.products.Find(100);
			var productToReturn = product.ToString();// throw exception

			return Ok(productToReturn);
		}


		[HttpGet("validationerror/{id}")]
		public ActionResult GetValidationError(int id)
		{
			var product = _context.products.Find(id);
			var productToReturn = _mapper.Map<ProductToReturnDto>(product);

			return Ok(productToReturn);
		}


	}
}
