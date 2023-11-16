using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Core.Specifications;
using Core.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat_Project_API.DTOS;
using Talabat_Project_API.Errors;
using Talabat_Project_API.Helper;

namespace Talabat_Project_API.Controllers
{
	
	public class productsController : ApiBaseController
	{
		private readonly IunitOfWork _unitOfWork;

		//private readonly IGenericRepository<ProductBrand> _brandRepository;
		//private readonly IGenericRepository<ProductType> _typeRepository;
		//public IGenericRepository<Product> _ProductRepository { get; set; }
		private readonly IMapper _mapper;

		public productsController(
			//IGenericRepository<Product> ProductRepository , 
			//IGenericRepository<ProductBrand> BrandRepository , 
			//IGenericRepository<ProductType> TypeRepository,
			IunitOfWork unitOfWork,
			IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			//_ProductRepository = ProductRepository;
			//_brandRepository = BrandRepository;
			//_typeRepository = TypeRepository;
			_mapper = mapper;
		}

		

		[HttpGet]
		[Authorize]//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductParams param)// from query means that the class will take his parameters from the query in the url
		{
			
			var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(new ProductSpecifications(param));
			var mappedProducts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
			var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(new ProductSpecificationForCount(param));
			return Ok(new Pagination<ProductToReturnDto>(param.PageSize,param.PageIndex,count,mappedProducts));
		}
		

		[HttpGet("{id}")]//u will get variable of id
		[ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(new ProductSpecifications(id));
			if(product != null)
			{
				var mappedProduct = _mapper.Map<ProductToReturnDto>(product);
				return Ok(mappedProduct);

			}
			return NotFound(new ApiErrorResponse(404));	
		}

		[HttpGet("brands")]
		public async Task<ActionResult<ProductBrand>> GetBrands()
		{
		 	var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsyn();
			return Ok(brands);
		}


		[HttpGet("types")]
		public async Task<ActionResult<ProductBrand>> GetTypes()
		{
			var types = await _unitOfWork.Repository<ProductType>().GetAllAsyn();
			return Ok(types);
		}

	}
}
