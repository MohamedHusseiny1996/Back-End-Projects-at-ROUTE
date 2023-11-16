using Core.Entities;
using Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _dbContext;

		public BasketRepository(IConnectionMultiplexer connection)
        {
			_dbContext = connection.GetDatabase();
		}
        public async Task<CustomerBasket> AddUpdateBasketAsync(CustomerBasket basket)
		{
			var done = await _dbContext.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(1));
			if (!done) return null;

			return await GetBasketAsync(basket.Id);
		}

		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _dbContext.KeyDeleteAsync(basketId);
		}

		public async Task<CustomerBasket> GetBasketAsync(string basketId)
		{
		  var json = await _dbContext.StringGetAsync(basketId);
		  if (json.IsNull) return null;
		  var customerBasket = JsonSerializer.Deserialize<CustomerBasket>(json);
			return customerBasket;
		}
	}
}
