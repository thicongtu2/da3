using Da3.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Da3.Infrastructure.Database
{
    public class EntityBuilder
    {
        public static void Builder(ModelBuilder builder)
        {
            builder.Entity<Job>(e =>
            {
                e.HasIndex(x => x.MinSalary);
                e.HasIndex(y => y.Title);
            });
            
            builder.Entity<Employee>().Property(x => x.Locations).HasColumnType("json");
            
            builder.Entity<JobCategory>().HasKey(sc => new { sc.JobId, sc.CategoryId });
           
        }
    }
}