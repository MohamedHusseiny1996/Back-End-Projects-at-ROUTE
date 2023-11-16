using Core.Entities;
using Core.Entities.OrderAggregation;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Core.Entities.Product;
namespace Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketRepository _basketRepository;
		private readonly IunitOfWork _unitOfWork;

		public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IunitOfWork unitOfWork)
		{
			_configuration = configuration;
			_basketRepository = basketRepository;
			_unitOfWork = unitOfWork;
		}



		public async Task<CustomerBasket> CreateUpdatePaymentIntent(string BasketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripSetting:Secretkey"];
			var basket = await _basketRepository.GetBasketAsync(BasketId);
			if (basket is null)
				return null;

			decimal ShippingPrice = 0m;
			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);//value is used 
																																//to over come the conversion from nullable int to int if the value has value . so we check first if it has value then 
																																// access the integer value of this nullable int 
				ShippingPrice = deliveryMethod.Cost;
				basket.ShippingCost = deliveryMethod.Cost;
			}

			if (basket?.BasketItems?.Count > 0)
			{
				foreach (var item in basket.BasketItems)
				{
					var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					if (item.Price != product.Price)
						item.Price = product.Price;
				}
			}

			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;

			if (string.IsNullOrEmpty(basket.PaymentIntentId))// create payment intent
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long)basket.BasketItems.Sum(item => item.Quantity * item.Price * 100) + (long)(ShippingPrice * 100), // *100 to over come decimal fractions
					Currency = "usd",
					PaymentMethodTypes = new List<string>() { "card" }
				};

			    paymentIntent = await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;
			}

			else // Update payment intent
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)basket.BasketItems.Sum(item => item.Quantity * item.Price * 100) + (long)(ShippingPrice * 100),
					
				};
				await service.UpdateAsync(basket.PaymentIntentId, options);
			}

			await _basketRepository.AddUpdateBasketAsync(basket);
			return basket;
		}

			
			
		
	}
}
