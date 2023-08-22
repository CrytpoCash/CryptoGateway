using CryptoGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptoPriceController : ControllerBase
{
    private readonly ILogger<CryptoPriceController> _logger;
    private readonly ICryptoPriceService _cryptoPriceService;

    public CryptoPriceController(
        ILogger<CryptoPriceController> logger, 
        ICryptoPriceService cryptoPriceService)
    {
        _logger = logger;
        _cryptoPriceService = cryptoPriceService;
    }

    [HttpGet(Name = "GetCryptoPrice")]
    public async Task<IActionResult> Get(string symbol)
    {
        var response = await _cryptoPriceService.GetCryptoPriceAsync(symbol);
        
        return Ok(response);
    }
}
