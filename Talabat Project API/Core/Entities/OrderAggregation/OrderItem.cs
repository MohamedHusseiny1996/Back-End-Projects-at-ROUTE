using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregation
{
	public class OrderItem:BaseEntity
	{
        public OrderItem()
        {
            
        }
        public OrderItem(decimal price, int quantity, ProductItemOrder product)
		{
			Price = price;
			this.quantity = quantity;
			Product = product;
		}

		public decimal Price { get; set; }
		public int quantity { get; set; }
		public ProductItemOrder Product { get; set; }
	}
}
