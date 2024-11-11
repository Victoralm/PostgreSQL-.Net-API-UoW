using API2.Context;
using API2.Entities;
using API2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API2.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(PostgreContext context, ILogger logger) : base(context, logger) { }

        public Task<Product?> GetProductByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

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

        public override async Task<bool> Upsert(Product entity)
        {
            try
            {
                var existingProduct = await _dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingProduct == null)
                    return await Add(entity);

                existingProduct.Name = entity.Name;
                existingProduct.Description = entity.Description;
                existingProduct.CategoryId = entity.CategoryId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(ProductRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ProductRepository));
                return false;
            }
        }
    }
}
