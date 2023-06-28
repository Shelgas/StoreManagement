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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(category => category.Id);
            builder.HasIndex(category => new {category.Id, category.Name}).IsUnique();
            builder.Property(category => category.Name).HasMaxLength(255);
            builder.HasData(
                new Category { Id = 1, Name = "Электроника и гаджеты" },
                new Category { Id = 2, Name = "Мода и стиль" },
                new Category { Id = 3, Name = "Дом и сад" },
                new Category { Id = 4, Name = "Красота и уход" },
                new Category { Id = 5, Name = "Спорт и отдых" }
                );
        }
    }
}
