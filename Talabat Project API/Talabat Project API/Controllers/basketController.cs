using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Project_API.DTOS;
using Talabat_Project_API.Errors;

namespace Talabat_Project_API.Controllers
{

	public class basketController : ApiBaseController
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public basketController(IBasketRepository basketRepository, IMapper mapper)
		{
			_basketRepository = basketRepository;
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
		{
			var basket = await _basketRepository.GetBasketAsync(basketId);
			if (basket == null) return NotFound(new ApiErrorResponse(404));
			return Ok(basket);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> CreateUpdateBasket(CustomerBasketDto basket)
		{
			var basketItem= _mapper.Map<CustomerBasket>(basket);
			var item = await _basketRepository.AddUpdateBasketAsync(basketItem);
			if (item == null) return BadRequest(new ApiErrorResponse(400));
			return Ok(item);
		}

		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string basketId)
		{
			var Done = await _basketRepository.DeleteBasketAsync(basketId);
			return Ok(Done);
		}
	}
}
