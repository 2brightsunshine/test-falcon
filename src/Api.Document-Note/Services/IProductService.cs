using Api.Document_Note.Models;

namespace Api.Document_Note.Services;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetProductsAsync(string subAccountId);
    
    public Task<Product> CreateProductAsync(Product product);
}