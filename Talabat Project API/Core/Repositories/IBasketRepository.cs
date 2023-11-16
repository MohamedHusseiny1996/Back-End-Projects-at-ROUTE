using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
	public interface IBasketRepository
	{
		public Task<CustomerBasket> AddUpdateBasketAsync(CustomerBasket basket);
		public Task<bool> DeleteBasketAsync(string basketId);
		public Task<CustomerBasket> GetBasketAsync(string basketId);
	}
}
