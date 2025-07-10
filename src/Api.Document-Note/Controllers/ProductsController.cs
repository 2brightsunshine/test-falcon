using Api.Document_Note.Models;
using Api.Document_Note.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Document_Note;

public class ProductsController(IProductService productService) : Controller
{
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("api/products")]
    public async Task<IActionResult> GetProducts(string subAccountId)
    {
        if (string.IsNullOrWhiteSpace(subAccountId))
        {
            return await Task.FromResult<IActionResult>(BadRequest("SubAccountId cannot be null or empty."));
        }
        var result = await productService.GetProductsAsync(subAccountId);
       
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("api/products")]
    public async Task<IActionResult> PostProducts(string subAccountId, Product productRequest)
    {
        var result = await productService.CreateProductAsync(productRequest);
        
        return Ok(result);
    }
}