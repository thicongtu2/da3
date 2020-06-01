using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Da3.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Da3.Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext<Da3User>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                                e.State == EntityState.Added
                                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedTime = DateTime.Now;
                }

                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    ((BaseEntity) entityEntry.Entity).DelFlag = 1;
                }
            }
            
            var result =  await base.SaveChangesAsync(acceptAllChangesOnSuccess,cancellationToken);
           
           return result;
        }
        
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                                e.State == EntityState.Added
                                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedTime = DateTime.Now;
                }

                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    ((BaseEntity) entityEntry.Entity).DelFlag = 1;
                }
            }

            return base.SaveChanges();
        }
        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()))
            {
                if (property.GetFieldName()=="Description")
                {
                    property.SetColumnType("text");
                }
                else if (property.ClrType == typeof(string))
                {
                    property.SetColumnType("varchar(255)");
                }
            }

            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                //Replace table names
                entity.SetTableName(entity.GetTableName().ToLower());
            }
            
            EntityBuilder.Builder(builder);
        }
    }
}