using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Basket.Domain.Entities;

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

            builder.Property(i => i.IsDeleted).IsRequired();

            builder.HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
