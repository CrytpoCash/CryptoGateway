﻿using CryptoGateway.Domain.Entities;
using CryptoGateway.Infra;
using CryptoGateway.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptocurrencyController : ControllerBase
{
    private readonly ILogger<CryptocurrencyController> _logger;
    private readonly CryptoGatewayContext _context;

    public CryptocurrencyController(
        ILogger<CryptocurrencyController> logger, 
        CryptoGatewayContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var list = await _context.Cryptocurrencys.AsNoTracking().ToListAsync();
        
        return Ok(list);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Crypto>> Get(Guid id)
    {
        var crypto = await _context.Cryptocurrencys
            .Include(x => x.CryptoSymbolExchanges)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (crypto is null)
        {
            return NotFound();
        }

        return crypto;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Post(CryptocurrencyInputViewModel model)
    {
        var cryptocurrency = new Crypto(model.Name, model.Symbol);

        foreach (var item in model.Exchanges)
        {
            var exchange = await _context.Exchanges.FindAsync(item.ExchangeId);

            if (exchange is null)
            {
                continue;
            }
            
            cryptocurrency.AddCryptoSymbolExchange(new CryptoSymbolExchange(exchange, item.Symbol));
        }
        
        await _context.Cryptocurrencys.AddAsync(cryptocurrency);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new { id = cryptocurrency.Id }, cryptocurrency);
    }
}