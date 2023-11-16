using AutoMapper;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Project_API.DTOS;
using Talabat_Project_API.Errors;

namespace Talabat_Project_API.Controllers
{
	[Authorize]
	public class PaymentsController :ApiBaseController
	{
		private readonly IPaymentService _paymentService;
		private readonly IMapper _mapper;

		public PaymentsController(IPaymentService paymentService , IMapper mapper)
        {
			_paymentService = paymentService;
			_mapper = mapper;
		}

		

		[HttpPost]
		[ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
		public async  Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
		{
			var basket = _paymentService.CreateUpdatePaymentIntent(BasketId);
			if (basket == null) return BadRequest(new ApiErrorResponse(400));

			var mappedBasket= _mapper.Map<CustomerBasketDto>(basket);
			return mappedBasket;
		}
	}
}
