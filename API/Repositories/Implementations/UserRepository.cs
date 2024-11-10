using API.Context;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PostgreContext context, ILogger logger) : base(context, logger) { }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetAllUsersAsync function error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public Task<User?> GetUserByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public Task<IEnumerable<User>> GetAllAsync()
        //{
        //    return await _context.Users.Include(e => e.Department).ToListAsync();
        //}

        //public Task<IEnumerable<User>> GetAllUsersAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<User?> GetByIdAsync(object Id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<User?> GetUserByIdAsync(Guid Id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task InsertAsync(User Entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateAsync(User Entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteAsync(object Id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> SaveAsync(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
