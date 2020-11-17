using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Enums;
using Domain.OrderAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configs.OrderConfigs
{
   public class OrderConfig:IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(c => c.Date);
            builder.OwnsOne(c => c.Detail);
            builder.HasOne(c => c.User).WithMany(c => c.Orders).HasForeignKey(c => c.UserId);

            builder.Property(f => f.OrderState)
                .HasColumnName("OrderState")
                .HasConversion(f => f.Id,
                    foodTypeId => Enumeration.GetAll<OrderState>().Single(s => s.Id == foodTypeId));
        }
    }
}
