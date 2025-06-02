using Microsoft.EntityFrameworkCore;
using shoppingAppRazor_temp.Models;

namespace shoppingAppRazor_temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  //Whatever options we configure here will be passed to the base class of DbContext
        {

        }
        public DbSet<Category> Categories { get; set; } //Table name Categories from model Category

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Fiction", DisplayOrder = 4 }
                );
        }
    }
}
