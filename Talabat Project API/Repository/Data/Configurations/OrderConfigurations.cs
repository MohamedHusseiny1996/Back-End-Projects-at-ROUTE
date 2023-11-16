using Core.Entities.OrderAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Configurations
{
	public class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
		
			builder.Property(p => p.Status)
				.HasConversion(oStatus => oStatus.ToString() , oStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), oStatus));

			builder.Property(p => p.SubTotal)
				.HasColumnType("decimal(18,2)");

			builder.OwnsOne(p => p.ShippingAddresse, sh=>sh.WithOwner());

			builder.HasOne(p => p.DeliveryMethod)
				.WithMany().OnDelete( DeleteBehavior.NoAction);
				
		} 
	}
}
