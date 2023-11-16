using Core.Entities.OrderAggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
	public interface IOrderService
	{
		public Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId , int DeliverlyMethodId , Addresse addresse);
		public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail);
		public Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail , int orderId);
		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
	}
}
