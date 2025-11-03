using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceMarketData;

public class Catalog : _EntityBase
{
    public string? Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();//mant to many bağlantı (catalog listesi de product da var)

}

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.ToTable("Catalogs");
        builder.HasIndex(p => new { p.Name });

        builder.Property(p => p.Name).IsRequired();

    }
}
