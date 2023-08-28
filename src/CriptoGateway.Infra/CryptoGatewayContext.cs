using CryptoGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CriptoGateway.Infra;

public class CryptoGatewayContext : DbContext
{
    public CryptoGatewayContext(DbContextOptions<CryptoGatewayContext> options)
        : base(options)
    { }
    
    public DbSet<Cryptocurrency> Cryptocurrencys { get; set; }
    public DbSet<Exchange> Exchanges { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CryptoGatewayContext).Assembly);
    }
}