using ImageManipulation.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageManipulation.Data.Repositories;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<IEnumerable<Product>> GetProductsAsync(Product product);
    Task<Product?> FindProductByIdAsync(int id);
    Task DeleteProductAsync(Product product);
}
public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<Product> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;  // returning created product, it will automatically fetch `Id`
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> FindProductByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(Product product)
    {
        return await _context.Products.ToListAsync();
    }


}