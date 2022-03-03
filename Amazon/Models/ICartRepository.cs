using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amazon.Models
{
    public interface ICartRepository
    {
        IQueryable<Cart> Items { get; }

        void SaveCart(Cart cart);
    }
}
