using API.Context;
using API.Repositories.Implementations;
using API.Repositories.Interfaces;
using API.UnitOfWork.Interfaces;

namespace API.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        //public UserRepository Users => throw new NotImplementedException();

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        private readonly PostgreContext _context;
        private readonly ILogger _logger;

        public UserRepository Users { get; private set; }
        //public UserRepository Users => new

        public UnitOfWork(PostgreContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            //using var transaction = _context.Database.BeginTransaction();
            await _context.SaveChangesAsync();
            // Commit transaction if all commands succeed, transaction will auto-rollback
            // when disposed if either commands fails
            //    transaction.Commit();
            //    Dispose();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //public Task CompleteAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
