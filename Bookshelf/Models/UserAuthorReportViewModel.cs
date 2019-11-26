using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class UserAuthorReportViewModel
    {

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        [Display(Name = "Author Count")]
        public int AuthorCount { get; set; }
    }
}
