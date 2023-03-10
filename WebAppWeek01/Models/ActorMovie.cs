using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppWeek01.Models
{
    public class ActorMovie
    {

        public int? ActorId { get; set; }
        public int? MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }
        [ForeignKey("ActorId")]
        public Person? Actor { get; set; }
    }
}