using API2.Context;
using API2.Entities;
using API2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API2.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(PostgreContext context, ILogger logger) : base(context, logger) { }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetAllUsersAsync function error", typeof(ProductRepository));
                return new List<Product>();
            }
        }
    }
}
