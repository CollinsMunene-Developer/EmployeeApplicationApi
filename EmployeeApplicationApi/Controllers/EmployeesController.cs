using EmployeeApplicationApi.Database;
using EmployeeApplicationApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApplicationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly EmployeeDbContext _dbContext;

        public EmployeesController(EmployeeDbContext dbContext , ILogger<EmployeesController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }   

        [HttpGet]

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            _logger. LogInformation("Requestin g all employees");
           return await  _dbContext.Employees.ToListAsync();

        }

    }
}