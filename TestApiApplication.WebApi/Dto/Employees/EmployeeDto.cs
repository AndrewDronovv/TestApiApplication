using TestApiApplication.Domain.Entities;
using TestApiApplication.Domain.Enums;

namespace TestApiApplication.WebApi.Dto.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string? MiddleName { get; set; }
        public string FullName => $"{LastName} {Name} {MiddleName}";
        public Position Position { get; set; }
        public IEnumerable<ViolationDto> Violations { get; set; }  
    }
}