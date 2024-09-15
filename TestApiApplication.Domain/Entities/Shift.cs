using System.ComponentModel.DataAnnotations;
using TestApiApplication.Domain.Common;

namespace TestApiApplication.Domain.Entities
{
    public class Shift : Entity
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public DateTime DateStartWork { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public DateTime? DateEndWork { get; set; }
        
        public double TotalHours { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        public bool IsFailed { get; set; }
    }
}
