using API2.Entities;

namespace API2.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(Guid Id);

    }
}
