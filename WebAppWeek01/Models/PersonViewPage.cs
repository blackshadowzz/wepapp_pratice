using System.Reflection.Metadata;

namespace WebAppWeek01.Models
{
    public class PersonViewPage
    {
        public IEnumerable<Person> People { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(People.Count() / (double)PerPage));
        }
        public IEnumerable<Person> PaginatedPerson()
        {
            int start = (CurrentPage - 1) * PerPage;
            return People.OrderBy(b => b.PersonId).Skip(start).Take(PerPage);
        }
    }
}
