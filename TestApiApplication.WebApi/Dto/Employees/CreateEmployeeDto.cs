using TestApiApplication.Domain.Enums;

namespace TestApiApplication.WebApi.Dto.Employees
{
    public class CreateEmployeeDto
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string? MiddleName { get; set; }
        public Position Position { get; set; }
    }
}
