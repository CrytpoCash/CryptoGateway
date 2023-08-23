using CryptoGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoGateway.Infra;

public class CryptoGatewayContext : DbContext
{
    public CryptoGatewayContext(DbContextOptions<CryptoGatewayContext> options)
        : base(options)
    { }
    
}