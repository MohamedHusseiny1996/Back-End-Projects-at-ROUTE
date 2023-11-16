using Core.Entities.OrderAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Configurations
{
	internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
	{
		public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			builder.Property(p=>p.Cost).HasColumnType("decimal(18,2)");
		}
	}
}
