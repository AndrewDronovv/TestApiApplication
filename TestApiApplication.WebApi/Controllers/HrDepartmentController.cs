using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApiApplication.Domain;
using TestApiApplication.Domain.Entities;
using TestApiApplication.Domain.Enums;
using TestApiApplication.WebApi.Dto.Employees;

namespace TestApiApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrDepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HrDepartmentController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost("[action]")]
        public IActionResult CreateEmployee(CreateEmployeeDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.LastName))
            {
                return BadRequest("Заполните необходимые поля");
            }

            _context.Employees.Add(new Employee()
            {
                Name = input.Name,
                LastName = input.LastName,
                MiddleName = input.MiddleName,
                Position = input.Position,
            });

            _context.SaveChanges();
            return Ok(input);
        }


        [HttpGet("[action]")]
        public IActionResult GetPositions()
        {
            var positions = Enum.GetValues<Position>();
            var result = positions.Select(p => new PositionDto
            {
                Name = p.AsString(EnumFormat.Description),
                Value = p,
            });

            return Ok(result);
        }


        [HttpGet("[action]")]
        public IActionResult GetEmployees(Position? position)
        {
            IQueryable<Employee> query = _context.Employees.Include(e => e.Shifts);
            if (position.HasValue)
            {
                query = query.Where(e => e.Position == position.Value);
            }

            IEnumerable<Employee> employees = query.ToList();

            IEnumerable<EmployeeDto> getEmployeeDto = employees.Select(e => new EmployeeDto()
            {
                Id = e.Id,
                Name = e.Name,
                LastName = e.LastName,
                MiddleName = e.MiddleName,
                Position = e.Position,
                Violations = e.Shifts
                    .Where(s => s.IsFailed)
                    .GroupBy(s => s.DateStartWork.ToString("MM.yyyy"))
                    .Select(group => new ViolationDto
                    {
                        Month = group.Key,
                        Count = group.Count(),
                    })
            });
            return Ok(getEmployeeDto);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            Employee employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Работник с id = {id} не существует");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult UpdateEmployee(UpdateEmployeeDto updateEmployee)
        {
            Employee employee = _context.Employees.Find(updateEmployee.Id);
            if (employee == null)
            {
                return BadRequest($"Работник с id = {updateEmployee.Id} не существует");
            }
            employee.Name = updateEmployee.Name;
            employee.LastName = updateEmployee.LastName;
            employee.Position = updateEmployee.Position;
            employee.MiddleName = updateEmployee.MiddleName;

            _context.Employees.Update(employee);
            _context.SaveChanges();
            return Ok(employee);
        }
    }
}
