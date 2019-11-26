using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Display(Name = "Books Written")]
        public List<Book> BooksWritten { get; set; } = new List<Book>();

        // ApplicationUser creating the author
        [Required]
        public string UserCreatingId { get; set; }
        public ApplicationUser UserCreating { get; set; }

    }
}
