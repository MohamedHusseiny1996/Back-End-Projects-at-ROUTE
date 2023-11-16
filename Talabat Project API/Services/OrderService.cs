using Core.Entities;
using Core.Entities.OrderAggregation;
using Core.Repositories;
using Core.Services;
using Core.Specifications;
using Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class OrderService : IOrderService
	{


		private readonly IunitOfWork _unitOfWork;
		private readonly IPaymentService _paymentService;
		private readonly IBasketRepository _basketRepository;
		//private readonly IGenericRepository<Product> _productrepository;
		//private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepository;
		//private readonly IGenericRepository<Order> _orderRepository;

		public OrderService(
			IBasketRepository basketRepository , 
			//IGenericRepository<Product> Productrepository ,
			//IGenericRepository<DeliveryMethod> DeliveryMethodRepository ,
			//IGenericRepository<Order> OrderRepository,

			IunitOfWork unitOfWork,
			 IPaymentService paymentService
			)

        {
			_unitOfWork = unitOfWork;
			this._paymentService = paymentService;
			_basketRepository = basketRepository;
			//_productrepository = Productrepository;
			//_deliveryMethodRepository = DeliveryMethodRepository;
			//_orderRepository = OrderRepository;
		}


        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliverlyMethodId, Addresse addresse )
		{
			 // 1.Get Basket From Basket Repo

			  var basket = await _basketRepository.GetBasketAsync(BasketId);


             // 2.Get Selected Items at Basket From Product Repo
			 var orderItems = new List<OrderItem>();
			if(basket?.BasketItems.Count > 0)
			{
				foreach(var item in basket.BasketItems)
				{
					var product =await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					var productItemOrdered = new ProductItemOrder(item.Id, product.Name, product.PictureUrl);
					var orderItem = new OrderItem(product.Price, item.Quantity, productItemOrdered );
					orderItems.Add(orderItem);
				}

			}

			// 3.Calculate SubTotal

			var subTotal = orderItems.Sum(item => item.Price * item.quantity);

			// 4.Get Delivery Method From DeliveryMethod Repo

			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliverlyMethodId);

			// 5.Create Order
			var spec = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);
			var exOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
			if(exOrder is not null)
			{
				 _unitOfWork.Repository<Order>().Delete(exOrder);
				_paymentService.CreateUpdatePaymentIntent(BasketId);
			}
			var order = new Order(BuyerEmail, addresse ,deliveryMethod ,orderItems ,subTotal ,basket.PaymentIntentId);

             // 6.Add Order Locally
			await _unitOfWork.Repository<Order>().AddAsync(order);

             // 7.Save Order To Database
			
			var num = await _unitOfWork.CompleteAsync();
			if(num > 0)
			return order;

			return null;
		}

		

		public async Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int orderId)
		{
			  var spec = new OrderSpecifications(BuyerEmail, orderId);
			var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
			return order;
		}


		public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
		{
			var spec = new OrderSpecifications(BuyerEmail);
			var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync( spec);
			return orders;
		}




		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsyn();
			return deliveryMethods;
		}


	}
}
