using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Project_API.Helper;

namespace Core.Specifications
{
	public class ProductSpecificationForCount:SpecificationBase<Product>
	{
		public ProductSpecificationForCount(ProductParams param)//get all
		{
			Includes.Add(p => p.ProductBrand);
			Includes.Add(p => p.ProductType);
			Criteria = p => (string.IsNullOrEmpty(param.SearchValue) || p.Name.Trim().ToLower().Contains(param.SearchValue) &&
			                (!param.TypeId.HasValue || p.ProductTypeId == param.TypeId) &&
						    (!param.BrandId.HasValue || p.ProductBrandId == param.BrandId));
			

			
		}

		
	}
}
