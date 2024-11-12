using API2.Context;
using API2.Entities;
using API2.Repositories.Interfaces;
using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace API2.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PostgreContext context, ILogger logger) : base(context, logger) {}

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            using var connection = _context.DapperConnection();
            #region EF
            //try
            //{
            //    return await _dbSet.ToListAsync();
            //}
            #endregion
            #region Dapper
            try
            {
                var sql = """
                    SELECT * FROM dev."Categories" ORDER BY "Id" ASC
                    """;
                return await connection.QueryAsync<Category>(sql);
            }
            #endregion
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetCategoriesAsync function error", typeof(CategoryRepository));
                return new List<Category>();
            }
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid Id)
        {
            using var connection = _context.DapperConnection();
            #region Dapper
            try
            {
                var sql = """
                    SELECT * FROM dev."Categories" WHERE id = @Id
                    """;
                //var sql = "SELECT * FROM SomeTable WHERE id = ANY(@ids)";
                //var results = connection.Query(sql, new { ids = new[] { 1, 2, 3, 4, 5 } });
                return await connection.QueryFirstOrDefaultAsync<Category>(sql, Id);
            }
            #endregion
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetCategoryByIdAsync function error", typeof(CategoryRepository));
                return new Category();
            }
        }

        public override async Task<bool> Add(Category entity)
        {
            using var connection = _context.DapperConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                var sql = """
                    INSERT INTO dev."Categories" ("Id", "Name") VALUES (@Id, @Name)
                    """;
                await connection.QueryAsync<Category>(sql, 
                    new { Id = entity.Id, Name = entity.Name, });
                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add function error", typeof(CategoryRepository));
                transaction.Rollback();
                connection.Close();
                return false;
            }
        }

        public override async Task<bool> Upsert(Category entity)
        {
            using var connection = _context.DapperConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            #region EF
            //try
            //{
            //    var existingCategory = await _dbSet.Where(x => x.Id == entity.Id)
            //                                        .FirstOrDefaultAsync();

            //    if (existingCategory == null)
            //        return await Add(entity);

            //    existingCategory.Name = entity.Name;

            //    return true;
            //}
            #endregion
            #region Dapper
            try
            {
                var sql = """
                    UPDATE dev."Categories" SET "Name" = @Name WHERE "Id" = @Id
                    """;
                await connection.QueryAsync<Category>(sql, new { Id = entity.Id, Name = entity.Name });
                transaction.Commit();
                connection.Close();
                return true;
            }
            #endregion
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(CategoryRepository));
                transaction.Rollback();
                connection.Close();
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            using var connection = _context.DapperConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            #region EF
            //try
            //{
            //    var exist = await _dbSet.Where(x => x.Id == id)
            //                            .FirstOrDefaultAsync();

            //    if (exist == null) return false;

            //    _dbSet.Remove(exist);

            //    return true;
            //}
            #endregion
            #region Dapper
            try
            {
                var sql = """
                    DELETE FROM dev."Categories" WHERE "Id" = @Id
                    """;
                await connection.QueryAsync<Category>(sql, new { Id = id });
                transaction.Commit();
                connection.Close();
                return true;
            }
            #endregion
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CategoryRepository));
                transaction.Rollback();
                connection.Close();
                return false;
            }
        }
    }
}
