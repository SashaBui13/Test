using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TESTPROJECT.Models;

namespace TESTPROJECT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
