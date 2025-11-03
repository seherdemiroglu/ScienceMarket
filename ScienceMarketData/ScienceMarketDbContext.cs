using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScienceMarketData;

public class ScienceMarketDbContext(DbContextOptions options) : IdentityDbContext<User,
Role,
Guid,
IdentityUserClaim<Guid>,
UserRole,
IdentityUserLogin<Guid>,//google, facebook gibi araçlarla login için kullanılır
IdentityRoleClaim<Guid>,
IdentityUserToken<Guid>
>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ScienceMarketDbContext).Assembly);
    }

    public required DbSet<Address> Addresses { get; set; }
    public required DbSet<Brand> Brands { get; set; }
    public required DbSet<Catalog> Catalogs { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<City> Cities { get; set; }
    public required DbSet<Order> Orders { get; set; }
    public required DbSet<OrderItem> OrderItems { get; set; }
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Province> Provinces { get; set; }
    public required DbSet<ProductImage> ProductImages { get; set; }
    public required DbSet<ProductSpecification> ProductSpecifications { get; set; }
    public required DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public required DbSet<Specification> Specifications { get; set; }

}

