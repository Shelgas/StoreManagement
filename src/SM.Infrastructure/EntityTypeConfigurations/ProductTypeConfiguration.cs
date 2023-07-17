using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructure.EntityTypeConfigurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.HasIndex(product => product.Id).IsUnique();
            builder.Property(product => product.Price).HasColumnType("NUMERIC(18,2)").IsRequired();
            builder.HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Смартфон",
                    Description = "Мощный и современный смартфон с отличным качеством камеры.",
                    Price = 599.99m,
                    BrandName = "SomeBrand",
                    CategoryId = 1 // Предположим, что это ID категории "Электроника"
                },
                new Product
                {
                    Id = 2,
                    Name = "Ноутбук",
                    Description = "Легкий и портативный ноутбук с высокой производительностью.",
                    Price = 999.50m,
                    BrandName = "AnotherBrand",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Футболка",
                    Description = "Комфортная футболка с оригинальным дизайном.",
                    Price = 19.99m,
                    BrandName = "FashionBrand",
                    CategoryId = 2 // Предположим, что это ID категории "Мода и стиль"
                });
        }
    }
}
