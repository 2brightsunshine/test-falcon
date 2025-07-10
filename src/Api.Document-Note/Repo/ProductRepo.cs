using Api.Document_Note.Data;
using Api.Document_Note.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Document_Note.Repo;

public class ProductRepo : IProductRepository
{
    private readonly ProductDbContext _context;

    /// <summary>
    /// Initializes a new instance of the ProductRepo class with the specified database context.
    /// </summary>
    public ProductRepo(ProductDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously retrieves all products from the database, ordered by creation time.
    /// </summary>
    /// <returns>A collection of products ordered by their creation timestamp.</returns>
    public async Task<IEnumerable<Product>> GetProductAsync(string subAccountId, string productId)
    {
        var query = _context.Products.AsQueryable();

        return await query.OrderBy(p => p.CreatedAt).ToListAsync();
    }

    /// <summary>
    /// Asynchronously creates a new product in the database and returns the created product.
    /// </summary>
    /// <param name="product">The product entity to add to the database.</param>
    /// <returns>The created product entity.</returns>
    public async Task<Product> CreateProductAsync(Product product)
    {
        product.UpdatedAt = null;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }
}