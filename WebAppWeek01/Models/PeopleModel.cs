using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppWeek01.Models
{
    public class PeopleModel
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? dob { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? gender { get; set; }
        public string? Address { get; set; }
    }
}
