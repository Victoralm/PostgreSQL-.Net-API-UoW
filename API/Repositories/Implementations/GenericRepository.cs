using API.Context;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;

namespace API.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //protected readonly PostgreContext _configuration;
        //protected readonly IPostgreContext _context;
        //The following variable is going to hold the EFCoreDbContext instance
        protected readonly PostgreContext _context;
        protected readonly DbSet<T> _dbSet;
        public readonly ILogger _logger;

        public GenericRepository(PostgreContext context, ILogger logger)
        {
            _context = context;

            //Whatever Entity name we specify while creating the instance of GenericRepository
            //That Entity name  will be stored in the DbSet<T> variable
            _dbSet = context.Set<T>();

            _logger = logger;
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        //public GenericRepository(PostgreContext Configuration)
        //{
        //    _configuration = Configuration;

        //    _dbSet = _configuration.Set<T>();
        //}

        //public GenericRepository(IPostgreContext configuration)
        //{
        //    this.configuration = configuration;
        //}

        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}

        //public async Task<T?> GetByIdAsync(object Id)
        //{
        //    return await _dbSet.FindAsync(Id);
        //}

        //public async Task InsertAsync(T Entity)
        //{
        //    await _dbSet.AddAsync(Entity);
        //}

        //public async Task UpdateAsync(T Entity)
        //{
        //    _dbSet.Update(Entity);
        //}

        //public async Task DeleteAsync(object Id)
        //{
        //    var entity = await _dbSet.FindAsync(Id);
        //    if (entity != null)
        //        _dbSet.Remove(entity);
        //}

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
