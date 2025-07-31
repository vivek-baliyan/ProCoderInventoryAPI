using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Iso2)
            .HasMaxLength(2);

        builder.Property(c => c.Iso3)
            .HasMaxLength(3);

        builder.Property(c => c.NumericCode)
            .HasMaxLength(3);

        builder.Property(c => c.PhoneCode)
            .HasMaxLength(10);

        builder.Property(c => c.Capital)
            .HasMaxLength(100);

        builder.Property(c => c.Currency)
            .HasMaxLength(3);

        builder.Property(c => c.CurrencyName)
            .HasMaxLength(50);

        builder.Property(c => c.CurrencySymbol)
            .HasMaxLength(5);

        builder.Property(c => c.Tld)
            .HasMaxLength(10);

        builder.Property(c => c.Native)
            .HasMaxLength(100);

        builder.Property(c => c.Region)
            .HasMaxLength(50);

        builder.Property(c => c.Subregion)
            .HasMaxLength(50);

        builder.Property(c => c.Latitude)
            .HasColumnType("decimal(10,8)");

        builder.Property(c => c.Longitude)
            .HasColumnType("decimal(11,8)");

        builder.Property(c => c.Emoji)
            .HasMaxLength(10);

        builder.Property(c => c.EmojiU)
            .HasMaxLength(20);

        // Indexes
        builder.HasIndex(c => c.Iso2)
            .IsUnique()
            .HasDatabaseName("IX_Countries_Iso2");

        builder.HasIndex(c => c.Iso3)
            .IsUnique()
            .HasDatabaseName("IX_Countries_Iso3");

        builder.HasIndex(c => c.Name)
            .HasDatabaseName("IX_Countries_Name");
    }
}