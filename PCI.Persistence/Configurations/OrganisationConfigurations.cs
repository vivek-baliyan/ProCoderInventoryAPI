using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
{
    public void Configure(EntityTypeBuilder<Organisation> builder)
    {
        builder.Property(e => e.UserId).HasMaxLength(36);
        builder.Property(e => e.Country).HasMaxLength(100);
        builder.Property(e => e.Address).HasMaxLength(200);
        builder.Property(e => e.City).HasMaxLength(100);
        builder.Property(e => e.State).HasMaxLength(100);
        builder.Property(e => e.PostalCode).HasMaxLength(20);
        builder.Property(e => e.CompanyName).HasMaxLength(200);
        builder.Property(e => e.ContactPerson).HasMaxLength(200);
        builder.Property(e => e.WebsiteUrl).HasMaxLength(255);
    }

}
