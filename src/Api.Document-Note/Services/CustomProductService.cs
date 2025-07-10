using Api.Document_Note.Models;
using Api.Document_Note.Repo;

namespace Api.Document_Note.Services;

public class CustomProductService : ICustomProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CustomProductService> _logger;

    public CustomProductService(IProductRepository productRepository, ILogger<CustomProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string subAccountId)
    {
        _logger.LogInformation("Getting products for subAccountId: {SubAccountId}", subAccountId);
        return await _productRepository.GetProductAsync(subAccountId, null);
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        _logger.LogInformation("Getting product by ID: {ProductId}", productId);
        var products = await _productRepository.GetProductAsync(null, productId.ToString());
        return products.FirstOrDefault();
    }

    public async Task<IEnumerable<Product>> GetProductsBySubAccountAsync(string subAccountId)
    {
        _logger.LogInformation("Getting products by subAccountId: {SubAccountId}", subAccountId);
        return await _productRepository.GetProductAsync(subAccountId, null);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _logger.LogInformation("Creating new product with code: {Code}", product.Code);
        return await _productRepository.CreateProductAsync(product);
    }
}