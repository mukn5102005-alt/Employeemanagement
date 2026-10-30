using EMSProject.Data;
using EMSProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesApiController(ApplicationDbContext context)
        {
            _context = context;
        }
         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Where(e => e.IsActive)
                .Include(e => e.Department)
                .ToListAsync();

            return Ok(employees);
        }

        // 2. GET: api/EmployeesApi/5 (Kisi ek employee ko ID se fetch karne ke liye)
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new { message = "Employee nahi mila!" });
            }

            return Ok(employee);
        }

        // 3. POST: api/EmployeesApi (Naya employee add karne ke liye)
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // 4. PUT: api/EmployeesApi/5 (Existing employee ka data update karne ke liye)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            if (id != updatedEmployee.Id)
            {
                return BadRequest(new { message = "ID match nahi ho rahi hai!" });
            }

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound(new { message = "Update karne ke liye employee nahi mila!" });
            }

            // Fields update karein
            existingEmployee.FullName = updatedEmployee.FullName;
            existingEmployee.Email = updatedEmployee.Email;
            existingEmployee.Salary = updatedEmployee.Salary;
            existingEmployee.DepartmentId = updatedEmployee.DepartmentId;
            existingEmployee.IsActive = updatedEmployee.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // 5. DELETE: api/EmployeesApi/5 (Employee ko delete ya soft-delete karne ke liye)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Delete karne ke liye employee nahi mila!" });
            }

            // Chahein toh database se poori tarah remove kar sakte hain:
            // _context.Employees.Remove(employee);

            // Ya phir IsActive false karke soft-delete kar sakte hain (Best Practice):
            employee.IsActive = false;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Employee successfully deactivate kar diya gaya hai!" });
        }
    }
}
