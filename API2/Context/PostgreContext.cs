using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API2.Context
{
    public class PostgreContext : IdentityDbContext<IdentityUser>
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
