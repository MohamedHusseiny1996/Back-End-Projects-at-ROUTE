using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Project_API.Helper;

namespace Core.Specifications
{
	public class ProductSpecifications:SpecificationBase<Product> 
	{
        public ProductSpecifications(ProductParams param)//get all
		{
			Includes.Add(p => p.ProductBrand);
			Includes.Add(p => p.ProductType);
			Criteria = p =>  (string.IsNullOrEmpty(param.SearchValue) || p.Name.Trim().ToLower().Contains(param.SearchValue)    &&
			                 (!param.TypeId.HasValue|| p.ProductTypeId==param.TypeId) &&
			                 (!param.BrandId.HasValue || p.ProductBrandId == param.BrandId));
			if(!string.IsNullOrEmpty(param.orderby))
			{
				  switch (param.orderby)
				{
					case "Name":
						OrderBy = p => p.Name;
						break;
					case "NameDesc":
						OrderByDesc = p => p.Name;
						break;
					case "Price":
						OrderBy = p => p.Price;
						break;
					case "PriceDesc":
						OrderByDesc = p => p.Price;
						break;
				} 
			}

			
			

			Take = param.PageSize;
			Skip = (param.PageSize * (param.PageIndex - 1));
			IsPaginationEnabled = true;
        }

		public ProductSpecifications(int id):base(p=>p.Id==id) //get by id
		{
			
			Includes.Add(p => p.ProductBrand);
			Includes.Add(p => p.ProductType);
		}
	}
}
