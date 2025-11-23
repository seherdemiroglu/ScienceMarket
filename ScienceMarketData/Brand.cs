using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ScienceMarketData;

public class Brand : _EntityBase
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "{0} field cannot be left blank")]
    public string? Name { get; set; }

    [Display(Name = "Logo")]
    public byte[]? Logo { get; set; }

    [NotMapped]
    public IFormFile? LogoFile { get; set; }//bunun db de karşılığı yok,kullanıcıdan bu fomatta alıcaz, byte a çevirip dbye koycaz
    public ICollection<Product> Products { get; set; } = new List<Product>();


}

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");
        builder.HasIndex(p => new { p.Name }); //builder.HasIndex(p => p.Name); de olur ama iki parametreli olsaydı bu kullanılamazdı.

        builder.Property(p => p.Name).IsRequired();

        builder.HasMany(p => p.Products)
            .WithOne(p => p.Brand)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}
