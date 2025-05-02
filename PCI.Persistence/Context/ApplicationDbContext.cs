using Microsoft.EntityFrameworkCore;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using System.Reflection;

namespace PCI.Persistence.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserAccessorService userAccessor) : DbContext(options)
{

    public DbSet<Organisation> Organisations { get; init; }
    public DbSet<Tag> Tags { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<CategoryImage> CategoryImages { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<ProductCategory> ProductCategories { get; init; }
    public DbSet<ProductTag> ProductTags { get; init; }
    public DbSet<ProductVariant> ProductVariants { get; init; }
    public DbSet<ProductImage> ProductImages { get; init; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set schema for all tables
        modelBuilder.HasDefaultSchema("APP");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Apply filter based on IUserAccessor
        //var userId = userAccessor.GetCurrentUserId();
        //modelBuilder.Entity<YourEntity>().HasQueryFilter(e => e.UserId == _userAccessor.GetCurrentUserId());
    }
}