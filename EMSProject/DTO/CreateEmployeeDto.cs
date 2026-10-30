using System.ComponentModel.DataAnnotations;

namespace EMSProject.DTO
{
    public class CreateEmployeeDto
    {
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public string Designation { get; set; } = string.Empty;

        [Required, Range(0, 10000000)]
        public decimal Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
