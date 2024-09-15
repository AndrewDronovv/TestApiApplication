using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApiApplication.Domain;
using TestApiApplication.Domain.Entities;
using TestApiApplication.Domain.Enums;

namespace TestApiApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KppController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KppController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("start")]
        public IActionResult StartShift(int employeeId, DateTime startTime)
        {
            var employee = _context.Employees
                .Include(e => e.Shifts)
                .FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                return BadRequest("Сотрудник не найден");
            }

            if (employee.Shifts != null && employee.Shifts.Any(s => s.DateEndWork == null))
            {
                return BadRequest("Предыдущая смена не закрыта");
            }

            if (startTime == DateTime.MinValue)
            {
                return BadRequest("Введите дату и время");
            }

            var shift = new Shift { EmployeeId = employeeId, DateStartWork = startTime };

            if (startTime.Hour > 9)
            {
                shift.IsFailed = true;
            }

            _context.Shifts.Add(shift);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("end")]
        public IActionResult EndShift(int employeeId, DateTime endTime)
        {
            var employee = _context.Employees
                .Include(e => e.Shifts)
                .FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                return BadRequest("Сотрудник не найден");
            }

            if (endTime == DateTime.MinValue)
            {
                return BadRequest("Введите дату и время");
            }
            var shift = employee.Shifts.FirstOrDefault(s => s.DateEndWork == null);
            if (shift == null)
            {
                return BadRequest("Открытая смена не найдена");
            }

            shift.DateEndWork = endTime;
            shift.TotalHours = (endTime - shift.DateStartWork).TotalHours;

            if ((shift.DateEndWork.Value.Hour < 18 && employee.Position != Position.Tester)
                || (shift.DateEndWork.Value.Hour < 21 && employee.Position == Position.Tester))
            {
                shift.IsFailed = true;
            }

            _context.Shifts.Update(shift);
            _context.SaveChanges();

            return Ok();
        }
    }
}


