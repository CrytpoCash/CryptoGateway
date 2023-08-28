using CryptoGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoGateway.Infra.Mappings;

public class ExchangeMapping : IEntityTypeConfiguration<Exchange>
{
    public void Configure(EntityTypeBuilder<Exchange> builder)
    {
        builder.ToTable("Exchanges");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(250)");
        
        builder.Property(c => c.BaseURL)
            .IsRequired()
            .HasColumnType("varchar(250)");
    }
}