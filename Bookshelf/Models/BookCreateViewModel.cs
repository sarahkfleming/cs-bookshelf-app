using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshelf.Models
{
    public class BookCreateViewModel
    {
        public Book Book { get; set; }
        public List<Author> Authors { get; set; }
        public List<SelectListItem> AuthorOptions
        {
            get
            {
                return Authors?.Select(a => new SelectListItem(a.FullName, a.Id.ToString())).ToList();
            }
        }
    }
}
