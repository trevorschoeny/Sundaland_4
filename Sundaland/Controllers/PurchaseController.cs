using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sundaland.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sundaland.Controllers
{
    public class PurchaseController : Controller
    {
        public IPurchaseRepository repo { get; set; }
        public Basket basket { get; set; }

        public PurchaseController(IPurchaseRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }

        [HttpPost]
        public IActionResult Checkout(Purchase purchase)
        {
            if (basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your basket is empty!");
            }

            if (ModelState.IsValid)
            {
                purchase.Lines = basket.Items.ToArray();
                repo.SavePurchase(purchase);
                basket.ClearBasket();

                return RedirectToPage("/PurchaseCompleted");
            }
            else
            {
                return View();
            }
        }
    }
}
