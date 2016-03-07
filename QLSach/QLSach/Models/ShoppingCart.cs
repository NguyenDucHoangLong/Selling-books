using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLSach.Models
{
    public class ShoppingCartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ListItem = new List<ShoppingCartItem>();
        }

        public List<ShoppingCartItem> ListItem { get; set; }

        public bool AddToCart(ShoppingCartItem item)
        {
            bool alreadyExists = ListItem.Any(x => x.ProductID == item.ProductID);
            if (alreadyExists)
            {
                ShoppingCartItem existsItem = ListItem.Where(x => x.ProductID == item.ProductID).SingleOrDefault();
                if (existsItem != null)
                {
                    existsItem.Quantity = existsItem.Quantity + 1;
                    existsItem.Total = existsItem.Quantity * existsItem.Price;
                }
            }
            else
            {
                ListItem.Add(item);
            }
            return true;
        }

        public bool RemoveFromCart(int lngProductSellID)
        {
            ShoppingCartItem existsItem = ListItem.Where(x => x.ProductID == lngProductSellID).SingleOrDefault();
            if (existsItem != null)
            {
                ListItem.Remove(existsItem);
            }
            return true;
        }

        public bool UpdateQuantity(int lngProductSellID, int intQuantity)
        {
            ShoppingCartItem existsItem = ListItem.Where(x => x.ProductID == lngProductSellID).SingleOrDefault();
            if (existsItem != null)
            {
                existsItem.Quantity = intQuantity;
                existsItem.Total = existsItem.Quantity * existsItem.Price;
            }
            return true;
        }

        public decimal GetTotal()
        {
            return ListItem.Sum(x => x.Total);
        }

        public int GetItem()
        {
            return ListItem.Count();
        }

        public bool EmptyCart()
        {
            ListItem.Clear();
            return true;
        }
    }
}