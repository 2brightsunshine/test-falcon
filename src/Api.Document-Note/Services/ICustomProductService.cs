using Api.Document_Note.Models;

namespace Api.Document_Note.Services;

public interface ICustomProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(string subAccountId);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<IEnumerable<Product>> GetProductsBySubAccountAsync(string subAccountId);
    Task<Product> CreateProductAsync(Product product);
}