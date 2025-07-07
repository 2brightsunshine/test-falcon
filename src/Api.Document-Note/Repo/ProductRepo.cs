using Api.Document_Note.Data;
using Api.Document_Note.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Document_Note.Repo;

public class ProductRepo : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepo(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductAsync(string subAccountId, string productId)
    {
        var query = _context.Products.AsQueryable();

        return await query.OrderBy(p => p.CreatedAt).ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        product.UpdatedAt = null;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }
}