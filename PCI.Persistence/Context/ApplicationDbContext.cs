using Microsoft.EntityFrameworkCore;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using System.Reflection;

namespace PCI.Persistence.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserAccessorService userAccessor) : DbContext(options)
{
    public DbSet<Organisation> Organisations { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<ProductImage> ProductImages { get; init; }
    public DbSet<ProductTag> ProductTags { get; init; }
    public DbSet<ProductTagAssignment> ProductTagAssignments { get; init; }
    public DbSet<ProductItemGroup> ProductItemGroups { get; init; }
    public DbSet<Customer> Customers { get; init; }
    public DbSet<CustomerContact> CustomerContacts { get; init; }
    public DbSet<CustomerAddress> CustomerAddresses { get; init; }
    public DbSet<CustomerTaxInfo> CustomerTaxInfos { get; init; }
    public DbSet<CustomerBankInfo> CustomerBankInfos { get; init; }
    public DbSet<CustomerDocument> CustomerDocuments { get; init; }
    public DbSet<Vendor> Vendors { get; init; }
    public DbSet<BusinessAddress> BusinessAddresses { get; init; }
    public DbSet<BusinessContact> BusinessContacts { get; init; }
    public DbSet<BusinessTaxInfo> BusinessTaxInfos { get; init; }
    public DbSet<CustomerFinancial> CustomerFinancials { get; init; }
    public DbSet<VendorFinancial> VendorFinancials { get; init; }
    public DbSet<BusinessBankInfo> BusinessBankInfos { get; init; }
    public DbSet<VendorPerformance> VendorPerformances { get; init; }
    public DbSet<VendorDocument> VendorDocuments { get; init; }
    public DbSet<SalesOrder> SalesOrders { get; init; }
    public DbSet<Invoice> Invoices { get; init; }
    public DbSet<Currency> Currencies { get; init; }
    public DbSet<TaxClassification> TaxClassifications { get; init; }
    public DbSet<Country> Countries { get; init; }
    public DbSet<State> States { get; init; }

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