using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required,StringLength(1000)]
        public string FullName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public string Designation { get; set; } = string.Empty;
        [Required]
        [Range(0, 10000000)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; } = DateTime.UtcNow;

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }



    }
}
