using Core.Entities.OrderAggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class OrderWithPaymentIntentSpecifications:SpecificationBase<Order>
	{
        public OrderWithPaymentIntentSpecifications(string paymentIntentId):base(p=>p.PaymentIntentId==paymentIntentId)
        {
           
        }
    }
}
