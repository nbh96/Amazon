using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amazon.Models
{
    public class EFCartRepository : ICartRepository
    {
        private BookstoreContext context;
        public EFCartRepository(BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Cart> Items => context.Items.Include(x => x.Lines).ThenInclude(x => x.Book);

        public void SaveCart(Cart cart)
        {
            context.AttachRange(cart.Lines.Select(x => x.Book));

            if (cart.ObjectId == 0)
            {
                context.Items.Add(cart);
            }

            context.SaveChanges();
        }
    }
}
