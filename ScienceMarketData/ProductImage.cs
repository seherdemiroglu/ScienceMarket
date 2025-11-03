using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceMarketData;

public class ProductImage : _EntityBase
{

    public Guid ProductId { get; set; }
    public byte[] Image { get; set; }

    public Product? Product { get; set; }
}

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");
        builder.Property(p => p.Image).IsRequired();

    }
}

