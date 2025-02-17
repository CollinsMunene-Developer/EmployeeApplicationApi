using Microsoft.EntityFrameworkCore;
using EmployeeApplicationApi.Models;

namespace EmployeeApplicationApi.Database

{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions ) : base(dbContextOptions)
        {

        }
        public DbSet<Employee> Employees { get; set; }

    }
}