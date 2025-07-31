using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCI.Domain.Models;

namespace PCI.Persistence.Configurations;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("States");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.StateCode)
            .HasMaxLength(10);

        builder.Property(s => s.Type)
            .HasMaxLength(50);

        builder.Property(s => s.Latitude)
            .HasColumnType("decimal(10,8)");

        builder.Property(s => s.Longitude)
            .HasColumnType("decimal(11,8)");

        builder.Property(s => s.CountryId)
            .IsRequired();

        // Indexes
        builder.HasIndex(s => s.StateCode)
            .HasDatabaseName("IX_States_StateCode");

        builder.HasIndex(s => s.Name)
            .HasDatabaseName("IX_States_Name");

        builder.HasIndex(s => s.CountryId)
            .HasDatabaseName("IX_States_CountryId");

        // Relationships
        builder.HasOne(s => s.Country)
            .WithMany(c => c.States)
            .HasForeignKey(s => s.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}