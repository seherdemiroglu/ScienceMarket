using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceMarketData;

public class Category : _EntityBase
{
    [Display(Name = "Ad")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
    public string? Name { get; set; }

    
    public byte[]? Image { get; set; } //kategoriye image ekle

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Specification> Specifications { get; set; } = new List<Specification>();
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder
        .HasIndex(p => new { p.Name });



        builder.Property(p => p.Name).IsRequired();

        builder
        .HasMany(p => p.Products)
        .WithOne(p => p.Category)
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(p => p.Specifications)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

