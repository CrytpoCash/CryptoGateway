using CryptoGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CriptoGateway.Infra.Mappings;

public class CryptocurrencyExchangeItemMapping : IEntityTypeConfiguration<CryptocurrencyExchangeItem>
{
    public void Configure(EntityTypeBuilder<CryptocurrencyExchangeItem> builder)
    {
        builder.ToTable("CryptocurrencyExchangeItems");
        
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Cryptocurrency)
            .WithMany(x => x.ExchangeItems);
        
        builder.HasOne(x => x.Exchange)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x=> x.ExchangeId);
        
        builder.Property(c => c.CryptoSymbolExternal)
            .IsRequired()
            .HasColumnType("varchar(50)");
    }
}