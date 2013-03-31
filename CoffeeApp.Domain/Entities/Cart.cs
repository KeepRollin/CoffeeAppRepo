using System.Collections.Generic;
using System.Linq;
using CoffeeApp.Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System;

namespace CoffeeApp.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void addToCart(Coffee coffee, int quantity, int n)
        {
            CartLine line = lineCollection.Where(l => l.Coffee.CoffeeID == coffee.CoffeeID).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine() { Coffee = coffee, Quantity = quantity, N = n});
            }
            else if ((line.Quantity + quantity) > line.maxQuantity)
            {
                line.Quantity = line.maxQuantity;
                throw new Exception("You've reached maxQuantity for this coffee");
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveFromCart(Coffee coffee)
        {         
            lineCollection.RemoveAll(l => l.Coffee.CoffeeID == coffee.CoffeeID);
        }

        public decimal CalculateTotal(decimal x = 10, decimal m = 2)
        {
            decimal result = 0;
            foreach (var item in lineCollection)
            {
                result += item.LinePrice;
            }
            if ((int)(result / x) <= 1)
            {
                return result + m;
            }
            return result;
        }

        public IEnumerable<CartLine> Lines
        {
            get
            {
                return lineCollection.OrderBy(c=>c.Coffee.Name);
            }
        }

        public class CartLine
        {
            public int maxQuantity = 10;

            public Coffee Coffee { get; set; }

            [Range(1,10)]
            public int Quantity { get; set; }            

            public int N { set; private get; }
            [DisplayFormat(DataFormatString = "{0:C}")]
            public decimal LinePrice
            {
                get
                {
                    return Coffee.Price * (Quantity - (int)(Quantity / N));
                }
            }
        }
    }
}