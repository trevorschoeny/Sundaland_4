using System;
using System.Linq;

namespace Sundaland.Models.ViewModels
{
    public class BookListViewModel
    {
        public IQueryable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
