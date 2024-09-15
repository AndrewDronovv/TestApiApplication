using TestApiApplication.Domain.Enums;

namespace TestApiApplication.WebApi.Dto.Employees
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string? MiddleName { get; set; }
        public Position Position { get; set; }
    }
}
