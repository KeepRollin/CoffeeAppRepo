﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Web.Models.ModelViews
{
    public class CartModelView
    {
        public Cart Cart { get; set; }
        public string returnUrl { get; set; }
    }
}