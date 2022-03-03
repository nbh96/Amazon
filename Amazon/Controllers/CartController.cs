using Amazon.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amazon.Controllers
{
    public class CartController : Controller
    {
        private ICartRepository repo { get; set; }
        private Basket basket { get; set; }
        public CartController (ICartRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Cart());
        }

        [HttpPost]
        public IActionResult Checkout(Cart cart)
        {
            if (basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your basket is empty");
            }

            if(ModelState.IsValid)
            {
                cart.Lines = basket.Items.ToArray();
                repo.SaveCart(cart);
                basket.ClearBasket();

                return RedirectToPage("/OrderComplete");
            }
            else
            {
                return View();
            }
        }
    }
}
