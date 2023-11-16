using AutoMapper;
using Core.Entities.OrderAggregation;
using Core.Services;
using Core.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;
using Talabat_Project_API.DTOS;
using Talabat_Project_API.Errors;
namespace Talabat_Project_API.Controllers
{
	[Authorize]
	public class ordersController : ApiBaseController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;
		private readonly IunitOfWork _unitOfWork;
		private readonly IPaymentService _paymentService;

		public ordersController(IOrderService orderService,IMapper mapper,IunitOfWork unitOfWork , IPaymentService paymentService)
        {
			_orderService = orderService;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			this._paymentService = paymentService;
		}

        [HttpPost("CreateUpdateOrder")]
		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Order>> CraeteOrUpdateOrderAsync(OrderDto orderDto)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
		    var addresse = _mapper.Map<Addresse>(orderDto.ShippingAddresse);
			var deliveryMethod = _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);
			var order = await _orderService.CreateOrderAsync(email, orderDto.BasketId, orderDto.DeliveryMethodId, addresse);
			if (order is null)
				return BadRequest(new ApiErrorResponse(400));

			return Ok(order);
		}



		[HttpGet("GetOrderById")]
		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Order>> GetOrderByIdForSpecificUserAsync(int orderId)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderService.GetOrderByIdForSpecificUserAsync(email, orderId);
			if (order == null)
				return NotFound(new ApiErrorResponse(404));

			
			return Ok(order);
		}

		
		[HttpGet("GetAllOrders")]
		[ProducesResponseType(typeof(IReadOnlyList<Order>),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrdersForSpecificUserAsync()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var orders = await _orderService.GetOrdersForSpecificUserAsync(email);
			if (orders == null)
				return NotFound(new ApiErrorResponse(404));


			return Ok(orders);
		}




		[HttpGet("GetAllDeliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethodsAsync()
		{
			
			var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();

			return Ok(deliveryMethods);
		}




	}
}
