using Basket.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Infrastructure.Data.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Domain.Entities.Basket>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Basket> builder)
        {
            builder.ToTable("Basket");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Currency)
                .IsRequired();

            builder.Property(b => b.Country)
                .IsRequired();

            builder.Property(b => b.SessionId)
                .IsRequired();

            builder.Property(b => b.TotalPrice)
                .IsRequired();

            builder.Navigation(b => b.BasketItems)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasField("_items"); // Explicitly specify the field name

            builder.HasMany(b => b.BasketItems)
            .WithOne()
             .HasForeignKey(bi => bi.BasketId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion(
                status => status.Name,
                name => BasketStatus.FromName(name, true)
            )
            .HasMaxLength(50)
            .HasColumnName("Status");
        }
    }
}