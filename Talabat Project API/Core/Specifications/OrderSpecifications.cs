using Core.Entities.OrderAggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class OrderSpecifications:SpecificationBase<Order>
	{

		public OrderSpecifications(string buyerEmail):base(p=>p.BuyerEmail==buyerEmail)
		{
			Includes.Add(p=>p.DeliveryMethod);
			Includes.Add(p=>p.Items);
			OrderByDesc = p=>p.OrderDate;
		}
		public OrderSpecifications(string buyerEmail,int OrderId):base(p => p.BuyerEmail == buyerEmail && p.Id==OrderId)
		{
			Includes.Add(p => p.DeliveryMethod);
			Includes.Add(p => p.Items);
			OrderByDesc = p => p.OrderDate;
		}
		
	}
}
