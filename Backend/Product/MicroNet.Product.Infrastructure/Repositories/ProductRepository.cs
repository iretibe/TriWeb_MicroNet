using MicroNet.Product.Core.Repositories;
using MicroNet.Product.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Product.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Core.Entities.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Core.Entities.Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string productCode)
        {
            return await _context.Products.AnyAsync(p => p.ProductCode == productCode);
        }

        public async Task<List<Core.Entities.Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Core.Entities.Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        public async Task UpdateAsync(Core.Entities.Product product)
        {
            var existingproduct = await _context.Products.FindAsync(product.Id);
            if (existingproduct == null)
            {
                throw new KeyNotFoundException($"Loan with ID {product.Id} not found.");
            }

            existingproduct.Update(product.Id, product.ProductName, product.Description, product.Note, product.AuditInfo.UpdatedBy!);
            
            await _context.SaveChangesAsync();
        }
    }
}
