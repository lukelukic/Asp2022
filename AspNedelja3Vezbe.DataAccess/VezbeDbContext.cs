using AspNedelja3Vezbe.DataAccess.Configurations;
using ASPNedelja3Vezbe.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspNedelja3Vezbe.DataAccess
{
    public class VezbeDbContext : DbContext
    {
        public VezbeDbContext(DbContextOptions options = null) : base(options)
        {

        }


        public IApplicationUser User { get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<CategorySpecification>().HasKey(x => new { x.CategoryId, x.SpecificationId });
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UseCaseId });

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=AspVezbe_3;Integrated Security=True")
        //        .UseLazyLoadingProxies();
        //}

        public override int SaveChanges()
        {
            foreach(var entry in this.ChangeTracker.Entries())
            {
                if(entry.Entity is Entity e)
                {
                    switch(entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = User?.Identity;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<CategorySpecification> CategorySpecifications { get; set; }
        public DbSet<SpecificationValue> SpecificationValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
    }
}
