using Api.Document_Note.Models;

namespace Api.Document_Note.Services;

public interface IProductService
{
    /// <summary>
/// Asynchronously retrieves all products associated with the specified sub-account ID.
/// </summary>
/// <param name="subAccountId">The identifier of the sub-account whose products are to be retrieved.</param>
/// <returns>A task representing the asynchronous operation, containing a collection of products for the given sub-account.</returns>
public Task<IEnumerable<Product>> GetProductsAsync(string subAccountId);
    
    /// <summary>
/// Asynchronously creates a new product based on the provided product information.
/// </summary>
/// <param name="product">The product details to use for creation.</param>
/// <returns>A task that represents the asynchronous operation, containing the created product.</returns>
public Task<Product> CreateProductAsync(Product product);
}