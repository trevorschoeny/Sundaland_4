using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sundaland.Models;
using Sundaland.Infrastructure;

namespace Sundaland.Pages
{
    public class CartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }
        public Basket Basket { get; set; }
        public string ReturnUrl { get; set; }

        public CartModel (IBookstoreRepository temp, Basket b)
        {
            repo = temp;
            Basket = b;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int bookID, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookID);

            // Add item to basket
            Basket.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            Basket.RemoveItem(Basket.Items.First(x => x.Book.BookId == bookId).Book);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
