using Core.Entities.OrderAggregation;

namespace Talabat_Project_API.DTOS
{
	public class OrderDto
	{
		public string BasketId { get; set; }
		public int DeliveryMethodId { get; set;}
		public UserAddresseDto ShippingAddresse { get; set;}

	}
}
