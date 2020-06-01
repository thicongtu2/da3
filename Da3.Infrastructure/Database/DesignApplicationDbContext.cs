using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Da3.Infrastructure.Database
{
    public class DesignApplicationDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql("server=127.0.0.1;user id=root;password=password;port=13307;database=da3;");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}