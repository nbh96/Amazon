﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Amazon.Models
{
    public class Basket
    {
        public int quantity;

        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        public virtual void AddItem (Book bk, int qty, double price)
        {
            BasketLineItem line = Items
                .Where(b => b.Book.BookId == bk.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = bk,
                    Quantity = qty,
                    Price = price
                });
            }
            else
            {
                line.Quantity += qty;
                line.Price += price;
            }
        }

        public double CalculateTotal()
        {
            double sum = Items.Sum(x =>  x.Price );

            return sum;
        }

        public virtual void ClearBasket()
        {
            Items.Clear();
        }

        public virtual void RemoveItems(Book bk)
        {
            Items.RemoveAll(x => x.Book.BookId == bk.BookId);
        }
    }

    public class BasketLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
