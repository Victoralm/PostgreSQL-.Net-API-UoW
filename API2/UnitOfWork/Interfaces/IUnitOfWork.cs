using API2.Repositories.Implementations;

namespace API2.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        ////Define the Specific Repositories
        ProductRepository Products { get; }

        Task CompleteAsync();
    }
}
