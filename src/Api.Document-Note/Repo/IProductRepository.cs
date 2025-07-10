using Api.Document_Note.Models;

namespace Api.Document_Note.Repo;

public interface IProductRepository
{
    /// <summary>
/// Asynchronously retrieves a collection of products matching the specified sub-account and product IDs.
/// </summary>
/// <param name="subAccountId">The identifier of the sub-account to filter products by.</param>
/// <param name="productId">The identifier of the product to retrieve.</param>
/// <returns>A task that represents the asynchronous operation, containing an enumerable collection of matching products.</returns>
public Task<IEnumerable<Product>> GetProductAsync(string subAccountId, string productId);
    
    /// <summary>
/// Asynchronously creates a new product and returns the created product.
/// </summary>
/// <param name="product">The product entity to be created.</param>
/// <returns>A task representing the asynchronous operation, containing the created <see cref="Product"/>.</returns>
public Task<Product> CreateProductAsync(Product product);
}