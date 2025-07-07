using Api.Document_Note.Models;

namespace Api.Document_Note.Repo;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetProductAsync(string subAccountId, string productId);
    
    public Task<Product> CreateProductAsync(Product product);
}