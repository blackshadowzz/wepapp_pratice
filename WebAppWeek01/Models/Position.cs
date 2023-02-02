using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppWeek01.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }
        public string? Description { get; set; }
        
        public ICollection<Employee> Employees { get; set;} =new List<Employee>();
    }
}
