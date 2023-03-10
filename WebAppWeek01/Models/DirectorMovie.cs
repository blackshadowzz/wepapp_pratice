using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppWeek01.Models
{
    public class DirectorMovie
    {
        [ForeignKey("Director")]
        public int? DirectorId { get; set; }
        public Person? Director { get; set; }

        [ForeignKey("Movie")]
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}