using API.Repositories.Implementations;
using API.Repositories.Interfaces;

namespace API.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ////Define the Specific Repositories
        UserRepository Users { get; }

        Task CompleteAsync();
    }
}
