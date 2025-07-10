using Api.Document_Note.Models;
using Api.Document_Note.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Document_Note.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ICustomProductService _customProductService;

    public ProductsController(ICustomProductService customProductService)
    {
        _customProductService = customProductService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Products API is running");
    }

    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts(string subAccountId)
    {
        if (string.IsNullOrWhiteSpace(subAccountId))
        {
            return BadRequest("SubAccountId cannot be null or empty.");
        }
        
        var result = await _customProductService.GetProductsAsync(subAccountId);
        return Ok(result);
    }
    
    [HttpPost("PostProducts")]
    [Authorize]
    public async Task<IActionResult> PostProducts(string subAccountId, [FromBody] Product productRequest)
    {
        var result = await _customProductService.CreateProductAsync(productRequest);
        return Ok(result);
    }
}