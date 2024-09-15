using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TestApiApplication.Domain.Common;
using TestApiApplication.Domain.Enums;

namespace TestApiApplication.Domain.Entities
{
    public class Employee : Entity
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string? MiddleName { get; set; }
        public string FullName => $"{LastName} {Name} {MiddleName}";
        public Position Position { get; set; }
        public ICollection<Shift>? Shifts { get; set; }
    }
}
