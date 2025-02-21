using System.Text.Json;
using Confluent.Kafka;
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

        public EmployeesController(EmployeeDbContext dbContext, ILogger<EmployeesController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            _logger.LogInformation("Requesting all employees");
            return await _dbContext.Employees.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(string name, string surname)
        {
            var employee = new Employee(Guid.NewGuid(), name, surname);
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            var message = new Message<string, string>()
            {
                Key = employee.Id.ToString(),
                Value = JsonSerializer.Serialize(employee)
            };

            //clients
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All
                //you can add it to be like Acks.All, Acks.None, Acks.Leader, Acks.Local 
                /*Acks.All: This means the leader will wait for the full set of in-sync replicas to acknowledge the record.
                This guarantees that the record will not be lost as long as at least one in-sync replica remains alive. This is the strongest available guarantee.
                Acks.None: This means the producer will not wait for any acknowledgment from the server at all. The record will be immediately added to the socket buffer and considered sent. No guarantee can be made that the server has received the record in this case, and the retries configuration will not take effect (as the client won't generally know of any failures).
                Acks.Leader: This means the leader will write the record to its local log but will respond without awaiting full acknowledgment from all followers. In this case should the leader fail immediately after acknowledging the record but before the followers have replicated it then the record will be lost.
                Acks.Local: This means the leader will wait for the full set of in-sync replicas to acknowledge the record only after all in-sync replicas have acknowledged the record. This guarantees that the record will not be lost as long as at least one in-sync replica remains alive and that the record will be acknowledged by the full set of in-sync replicas.
                */

            };

            var producer = new ProducerBuilder<string, string>(producerConfig) 
             .Build();
           await  producer.ProduceAsync("employeeTopic", message );
           producer.Dispose()
            ;

            //broker 1, broker 2, broker 3 (In sync replicas)

            return CreatedAtAction(nameof(CreateEmployee), new { id = employee.Id }, employee);

        }


    }
}