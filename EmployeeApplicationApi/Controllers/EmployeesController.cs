using EmployeeApplicationApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApplicationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetEmployees")]

        public IEnumerable<Employee> Get()
        {
            return new List<Employee>
            {
                new Employee(Guid.NewGuid(), "John", "Doe"),
                new Employee(Guid.NewGuid(), "Jane", "Doe")
            };
        }

    }
}