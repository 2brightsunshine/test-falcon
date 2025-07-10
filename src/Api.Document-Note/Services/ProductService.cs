using Api.Document_Note.Models;
using Api.Document_Note.Repo;

namespace Api.Document_Note.Services;

public class ProductService(ILogger<ProductService> logger, IProductRepository repository) : IProductService
{
  
    /// <summary>
    /// Asynchronously retrieves all products associated with the specified sub-account ID.
    /// </summary>
    /// <param name="subAccountId">The identifier of the sub-account whose products are to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, containing a collection of products for the given sub-account.</returns>
    public async Task<IEnumerable<Product>> GetProductsAsync(string subAccountId)
    {
        
         logger.LogInformation("GetProductsAsync called with subAccountId: {SubAccountId}", subAccountId);
         return await repository.GetProductAsync(subAccountId, null);
    }

    /// <summary>
    /// Asynchronously creates a new product using the repository.
    /// </summary>
    /// <param name="product">The product to be created.</param>
    /// <returns>The created product.</returns>
    public async Task<Product> CreateProductAsync(Product product)
    {
        return await repository.CreateProductAsync(product);
    }
}