namespace EMSProject.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Designation { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string? DepartmentName { get; set; }  // Department.Name, DepartmentId nahi
        public bool IsActive { get; set; }

    }
}
