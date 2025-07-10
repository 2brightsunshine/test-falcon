using Api.Document_Note.Models;
using Api.Document_Note.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Document_Note;

public class ProductsController(IProductService productService) : Controller
{
    /// <summary>
    /// Returns the default view for the products page.
    /// </summary>
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Retrieves the list of products associated with the specified sub-account identifier.
    /// </summary>
    /// <param name="subAccountId">The identifier of the sub-account whose products are to be retrieved.</param>
    /// <returns>An <see cref="IActionResult"/> containing the list of products if the sub-account ID is valid; otherwise, a bad request response.</returns>
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
    
    /// <summary>
    /// Creates a new product using the provided product data.
    /// </summary>
    /// <param name="subAccountId">The sub-account identifier (not used in this method).</param>
    /// <param name="productRequest">The product information to create.</param>
    /// <returns>An Ok result containing the created product.</returns>
    [Authorize]
    [HttpPost("api/products")]
    public async Task<IActionResult> PostProducts(string subAccountId, Product productRequest)
    {
        var result = await productService.CreateProductAsync(productRequest);
        
        return Ok(result);
    }
}