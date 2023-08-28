using CriptoGateway.Infra;
using CryptoGateway.API.ViewModels;
using CryptoGateway.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoGateway.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ExchangeController : ControllerBase
{
    private readonly ILogger<ExchangeController> _logger;
    private readonly CryptoGatewayContext _context;

    public ExchangeController(
        ILogger<ExchangeController> logger, 
        CryptoGatewayContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var list = await _context.Exchanges.AsNoTracking().ToListAsync();
        
        return Ok(list);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Exchange>> Get(Guid id)
    {
        var exchange = await _context.Exchanges.FindAsync(id);

        if (exchange is null)
        {
            return NotFound();
        }

        return exchange;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Post(ExchangeInputViewModel model)
    {
        var exchange = new Exchange(model.Name, model.BaseURL);
        
        await _context.Exchanges.AddAsync(exchange);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new { id = exchange.Id }, new ExchangeResultViewModel(exchange.Id));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ExchangeInputViewModel model)
    {
        var exchange = await _context.Exchanges.FindAsync(id);

        if (exchange is null)
        {
            return NotFound();
        }
        
        exchange.Name = model.Name;
        exchange.BaseURL = model.BaseURL;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var exchange = await _context.Exchanges.FindAsync(id);

        if (exchange is null)
        {
            return NotFound();
        }

        _context.Exchanges.Remove(exchange);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
