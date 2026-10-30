namespace EMSProject.Models
{
    public class DashboardViewModel
    {
        public int TotalHeadcount { get; set; }
        public decimal TotalPayroll { get; set; }
        public List<DepartmentStatViewModel> DepartmentBreakdown { get; set; } = new();
    }

    public class DepartmentStatViewModel
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
        public decimal TotalSalary { get; set; }
    }
}