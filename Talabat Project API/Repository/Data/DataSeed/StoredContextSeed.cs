using Core.Entities;
using Core.Entities.OrderAggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.Data.DataSeed
{
    public static class StoredContextSeed
	{
		public static async Task SeedAsync(StoreContext context)
		{
			//seeding brands
			if(!context.brands.Any())
			{
				var json = File.ReadAllText("../Repository/Data/DataSeed/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(json);

				if(brands?.Count >0 )
				{
					foreach (var brand in brands)
					{
						await context.brands.AddAsync(brand);

					}
					await context.SaveChangesAsync();
				}
				
			}


			//seeding Types
			if (!context.types.Any())
			{
				var json = File.ReadAllText("../Repository/Data/DataSeed/types.json");
				var Types = JsonSerializer.Deserialize<List<ProductType>>(json);

				if (Types?.Count > 0)
				{
					foreach (var type in Types)
					{
						await context.types.AddAsync(type);

					}
					await context.SaveChangesAsync();
				}

			}



			//seeding Products
			if (!context.products.Any())
			{
				var json = File.ReadAllText("../Repository/Data/DataSeed/products.json");
				var Products = JsonSerializer.Deserialize<List<Product>>(json);

				if (Products?.Count > 0)
				{
					foreach (var product in Products)
					{
						await context.products.AddAsync(product);

					}
					await context.SaveChangesAsync();
				}

			}


			//seeding Delivery Methods
			if (!context.deliveryMethods.Any())
			{
				var json = File.ReadAllText("../Repository/Data/DataSeed/delivery.json");
				var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(json);

				if (DeliveryMethods?.Count > 0)
				{
					foreach (var DeliveryMethod in DeliveryMethods)
					{
						await context.deliveryMethods.AddAsync(DeliveryMethod);

					}
					await context.SaveChangesAsync();
				}

			}




		}
	}
}
