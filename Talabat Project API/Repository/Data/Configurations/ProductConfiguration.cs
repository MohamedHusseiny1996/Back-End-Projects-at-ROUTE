using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(p => p.Id).IsRequired().UseIdentityColumn(0,1);
			builder.Property(p => p.Name).IsRequired().HasMaxLength(40);
			builder.Property(p => p.Price).IsRequired().HasColumnType("Dec(18,2)");
			builder.Property(p=>p.PictureUrl).IsRequired();

			builder.HasOne(x => x.ProductBrand).WithMany().HasForeignKey(x=>x.ProductBrandId);
			builder.HasOne(x => x.ProductType).WithMany().HasForeignKey(x => x.ProductTypeId);
		}
	}
}
