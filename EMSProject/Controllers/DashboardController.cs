using EMSProject.Models;
using EMSProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSProject.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var activeEmployees = _context.Employees.Where(e => e.IsActive);

            var totalHeadcount = await activeEmployees.CountAsync();

            var totalPayroll = await activeEmployees.SumAsync(e => e.Salary);

            var departmentBreakdown = await activeEmployees
                .Include(e => e.Department)
                .GroupBy(e => e.Department != null ? e.Department.Name : "Unassigned")
                .Select(g => new DepartmentStatViewModel
                {
                    DepartmentName = g.Key!,
                    EmployeeCount = g.Count(),
                    TotalSalary = g.Sum(e => e.Salary)
                })
                .OrderByDescending(d => d.EmployeeCount)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                TotalHeadcount = totalHeadcount,
                TotalPayroll = totalPayroll,
                DepartmentBreakdown = departmentBreakdown
            };

            return View(viewModel);
        }
    }
}