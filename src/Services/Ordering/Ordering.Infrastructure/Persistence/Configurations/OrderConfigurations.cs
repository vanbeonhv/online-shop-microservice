using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Shared.Enums;

namespace Ordering.Infrastructure.Persistence.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Status)
            .HasDefaultValue(EOrderStatus.New)
            .IsRequired();
    }
}