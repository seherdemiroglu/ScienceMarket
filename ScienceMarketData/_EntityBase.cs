using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceMarketData;

public abstract class _EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Aktive")]
    public bool IsEnabled { get; set; } = true;

    public User? User { get; set; }
}

public class _EntityBaseConfiguration : IEntityTypeConfiguration<_EntityBase>
{
    public void Configure(EntityTypeBuilder<_EntityBase> builder)
    {
       
    }
}
