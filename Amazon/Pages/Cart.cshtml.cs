using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Amazon.Infrastructure;

namespace Amazon.Pages
{
    public class CartModel : PageModel
    {
        private IBookRepository repo { get; set; }
        public string ReturnUrl { get; set; }
        public CartModel (IBookRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        public Basket basket { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(int bookID, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookID);

            basket.AddItem(b, 1, b.Price);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int bookID, string returnUrl)
        {
            basket.RemoveItems(basket.Items.First(x => x.Book.BookId == bookID).Book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
