using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppWeek01.Models;

namespace WebAppWeek01.Models
{
    public class Employee
    {
      
        public int ID { get; set; }
        [Column(TypeName ="nvarchar(60)"),DisplayName("First Name")]
        public string FirstName { get; set; }  
        [Column(TypeName ="nvarchar(60)")]
        public string LastName { get; set; }

        public DateTime? DOB { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string? Gender { get; set; } 
        [Column(TypeName = "nvarchar(150)")]
        public string? Address { get; set; } 
        [Column(TypeName = "nvarchar(60)")]
        public string? City { get; set; } 
        [Column(TypeName = "nvarchar(60)")]
        public string? Province { get; set; }
        [Column(TypeName = "nvarchar(60)")]
        public string? Country { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? Phone { get; set; } 
        [Column(TypeName = "nvarchar(100)")]
        public string? Email { get; set; }   
        [Column("Image_path")]
        public string? Photo { get; set; }
        [DisplayName("Position"), ForeignKey("Position")] 
        public int PositionId { get; set; }
        [DisplayName("Department"), ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Position? Position { get; set; } 
        public Department? Department { get; set; }

    }
}
