using Microsoft.EntityFrameworkCore;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;

namespace PCI.Persistence.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserAccessorService userAccessor) : DbContext(options)
{

    public DbSet<Organisation> Organisations { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<CategoryImage> CategoryImages { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set schema for all tables
        modelBuilder.HasDefaultSchema("APP");

        modelBuilder.Entity<Organisation>(profile =>
        {
            profile.Property(e => e.UserId).HasMaxLength(36);
            profile.Property(e => e.Country).HasMaxLength(100);
            profile.Property(e => e.Address).HasMaxLength(200);
            profile.Property(e => e.City).HasMaxLength(100);
            profile.Property(e => e.State).HasMaxLength(100);
            profile.Property(e => e.PostalCode).HasMaxLength(20);
            profile.Property(e => e.CompanyName).HasMaxLength(200);
            profile.Property(e => e.ContactPerson).HasMaxLength(200);
            profile.Property(e => e.WebsiteUrl).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(category =>
        {
            category.Property(e => e.Name).IsRequired().HasMaxLength(100);
            category.Property(e => e.PageTitle).IsRequired().HasMaxLength(100);
            category.Property(e => e.UrlIdentifier).IsRequired().HasMaxLength(255);
            category.Property(e => e.Description).HasMaxLength(1000);

            category.HasMany(e => e.ChildCategories)
                .WithOne(e => e.ParentCategory)
                .HasForeignKey(e => e.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            category.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            category.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

            // Add indexes for frequently queried columns if needed
            category.HasIndex(e => new { e.Name, e.UrlIdentifier });
        });

        modelBuilder.Entity<CategoryImage>(categoryImage =>
        {
            categoryImage.Property(e => e.ImagePath).IsRequired().HasMaxLength(255);
            categoryImage.Property(e => e.CategoryId).IsRequired();
            categoryImage.Property(e => e.IsPrimary).IsRequired();

            // Configure relationships
            categoryImage.HasOne(e => e.Category)
                .WithMany(e => e.CategoryImages)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Apply filter based on IUserAccessor
        //var userId = userAccessor.GetCurrentUserId();
        //modelBuilder.Entity<YourEntity>().HasQueryFilter(e => e.UserId == _userAccessor.GetCurrentUserId());
    }
}