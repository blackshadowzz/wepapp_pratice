using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebAppWeek01.Models;

namespace WebAppWeek01.Data
{
    public class appDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WebWeek01;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        //public DbSet<WebAppWeek01.Models.PeopleModel> PeopleModel { get; set; } = default!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>()
        //        .HasOne<Department>(e=>e.Department)
        //        .WithMany(p => p.Employees)
        //        .HasForeignKey(e=>e.DepartmentId);
        //    modelBuilder.Entity<Employee>()
        //        .HasOne<Position>(e=>e.Position)
        //        .WithMany(p => p.Employees)
        //        .HasForeignKey(e=>e.PositionId);

        //    //modelBuilder.Entity<Department>()
        //    //    .HasMany(p => p.Employees);

        //}
    }
}
