using Api.Document_Note.Models;
using Api.Document_Note.Repo;

namespace Api.Document_Note.Services;

public class ProductService(ILogger<ProductService> logger, IProductRepository repository) : IProductService
{
  
    public async Task<IEnumerable<Product>> GetProductsAsync(string subAccountId)
    {
        
         logger.LogInformation("GetProductsAsync called with subAccountId: {SubAccountId}", subAccountId);
         return await repository.GetProductAsync(subAccountId, null);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        return await repository.CreateProductAsync(product);
    }
}