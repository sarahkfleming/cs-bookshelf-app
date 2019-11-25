using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshelf.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(35)]
        [MinLength(1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Display(Name = "Pen Name")]
        public string PenName { get; set; }

        [Display(Name = "Preferred Genre")]
        public string PreferredGenre { get; set; }
        public List<Book> BooksWritten { get; set; } = new List<Book>();

        // ApplicationUser creating the author
        public string UserCreatingId { get; set; }
        [NotMapped]
        public ApplicationUser UserCreating { get; set; }

    }
}
