using CryptoGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CriptoGateway.Infra.Mappings;

public class CryptocurrencyMapping : IEntityTypeConfiguration<Cryptocurrency>
{
    public void Configure(EntityTypeBuilder<Cryptocurrency> builder)
    {
        builder.ToTable("Cryptocurrencys");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(c => c.Symbol)
            .IsRequired()
            .HasColumnType("varchar(50)");
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(250)");
    }
}