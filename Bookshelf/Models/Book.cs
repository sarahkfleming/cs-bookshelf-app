using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public int AuthorId { get; set; }

        // A book may have only one author
        public Author Author { get; set; }

        //[Required]
        public string OwnerId { get; set; }
        // User who created the book
        public ApplicationUser Owner { get; set; }
    }
}