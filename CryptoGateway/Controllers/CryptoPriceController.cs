using CryptoGateway.Adapter.Binance;
using CryptoGateway.Adapter.Kucoin;
using CryptoGateway.Services;
using CryptoGateway.Shared;
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
    public IActionResult Get(string symbol)
    {
        var response =  _cryptoPriceService.GetCryptoPrice(symbol);
        
        return Ok(response);
    }
}
