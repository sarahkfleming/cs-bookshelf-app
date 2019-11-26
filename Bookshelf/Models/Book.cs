using System;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }
        
        [Display(Name = "Page Count")]
        public int? PageCount { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        // A book may have only one author
        public Author Author { get; set; }

        [Required]
        public string OwnerId { get; set; }
        // User who created the book
        public ApplicationUser Owner { get; set; }
    }
}