using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Core.Entities;
using System.Linq.Expressions;
using System.Reflection;
using Talabat_Project_API.DTOS;

namespace Talabat_Project_API.Helper
{
	public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _configuration;

		public PictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if(source.PictureUrl != null)
			return $"{_configuration["BaseUrl"]}{source.PictureUrl}";

			return string.Empty ;
		}
	}
}
