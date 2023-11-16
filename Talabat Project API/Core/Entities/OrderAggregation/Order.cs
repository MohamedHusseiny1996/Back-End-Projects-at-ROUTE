using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregation
{
	public class Order:BaseEntity
	{
        public Order()
        {
            
        }
        public Order(string buyerEmail, Addresse shippingAddresse, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal , string paymentIntentId)
		{
			BuyerEmail = buyerEmail;
			ShippingAddresse = shippingAddresse;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
			PaymentIntentId = paymentIntentId;
		}

		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;
		public OrderStatus Status { get; set; } = OrderStatus.Pending;
		public Addresse ShippingAddresse { get; set; }// this will'nt be db Table so it isn't nav prop but it is
		//public int DeliveryMethodId { get; set; }// this is one to many relation but we will tell EF to deal it as one to one total relation ship so there will be no table for delivery method table in DB but what we need is to represent it in each table of order so this is the trick and we will not create it because we don't need it so we will let EF create it in DB
		public DeliveryMethod DeliveryMethod { get; set; }//nav prop because this will be table in db
		public ICollection<OrderItem> Items { get; set; }=new HashSet<OrderItem>();
		public decimal SubTotal { get; set; }// product * quantity

		//[NotMapped]
		//public decimal Total { get => SubTotal + DeliveryMethod.Cost; } // subtotal + delivery method 

		public decimal GetTotal()
		{
			return SubTotal + DeliveryMethod.Cost;
		}

		public string PaymentIntentId { get; set; }

	}
}
