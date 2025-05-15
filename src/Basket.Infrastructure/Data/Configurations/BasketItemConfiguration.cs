using Basket.Domain.Entities;
using Basket.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Infrastructure.Data.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(bi => bi.Id);

            builder.Property(bi => bi.ProductCode)
                   .IsRequired();

            builder.Property(bi => bi.Color)
                   .IsRequired();

            builder.Property(bi => bi.Price)
                   .IsRequired();

            builder.Property(bi => bi.Quantity)
                   .IsRequired();

            builder.Property(bi => bi.BasketId)
                   .IsRequired();

            builder.Property(bi => bi.TotalPrice)
                  .IsRequired();

            builder.Property(o => o.Status)
             .IsRequired()
             .HasConversion(
                 status => status.Name,
                 name => BasketItemStatus.FromName(name, true)
             )
             .HasMaxLength(50)
             .HasColumnName("Status");

            builder.HasQueryFilter(i => i.Status != BasketItemStatus.Cancelled);
        }
    }
}