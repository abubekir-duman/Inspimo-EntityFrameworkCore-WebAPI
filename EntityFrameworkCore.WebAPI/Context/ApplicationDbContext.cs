using EntityFrameworkCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.WebAPI.Context
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Personel> Personels { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Personel>().ToTable("Personels");
        //}



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer");
        //}
    }
}
