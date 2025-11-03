using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScienceMarketData;

public enum Genders
{
    Male, Female
}
public class User : IdentityUser<Guid>
{
    public required string GivenName { get; set; }
    public required DateTime Date { get; set; }
    public required Genders Gender { get; set; }

    public bool IsEnabled { get; set; } = true;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

    }
}
