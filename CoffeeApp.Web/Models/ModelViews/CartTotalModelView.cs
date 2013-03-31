using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Web.Models.ModelViews
{
    public class CartTotalModelView
    {
        public decimal M { get; set; }
        public decimal X { get; set; }
        public Cart Cart { get; set; }
    }
}