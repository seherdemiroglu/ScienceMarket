using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceMarketData;

public class Specification : _EntityBase
{

    public string? Name { get; set; }
    public Guid CategoryId { get; set; }


    public Category? Category { get; set; }


}

public class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder
            .ToTable("Specifications");

        builder
            .HasIndex(p => new { p.Name });


        builder
            .Property(p => p.Name)
            .IsRequired();


    }
}
