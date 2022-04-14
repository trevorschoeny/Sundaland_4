using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sundaland.Models;
using Sundaland.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sundaland.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController(IBookstoreRepository temp) => repo = temp;

        public IActionResult Index()
        {
            return RedirectToAction("Booklist");
        }

        public IActionResult Booklist(string category, int pageNum = 1)
        {
            int pageSize = 10;

            var bookListViewModel = new BookListViewModel
            {
                Books = repo.Books
                    .Where(b => b.Category == category || category == null)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = (category == null
                        ? repo.Books.Count()
                        : repo.Books.Where(b => b.Category == category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            return View(bookListViewModel);
        }
    }
}
