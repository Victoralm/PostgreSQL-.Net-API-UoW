using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class PostgreContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public PostgreContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //connect to postgres with connection string from app settings
            var ops = options.UseNpgsql(Configuration.GetConnectionString("PostgreSql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("dev");
        }

        // DbSets
        public DbSet<User> Users { get; set; }

    }
}
