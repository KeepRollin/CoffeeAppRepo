using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Entities;
using CoffeeApp.Web.Models.ModelViews;

namespace CoffeeApp.Web.Controllers
{
    public class CartController : Controller
    {
        private ICoffeeRepository coffeeRepo;
        private IConfigurationRepository configRepo;
        private decimal M;
        private decimal X;

        public CartController(ICoffeeRepository coffeeRepo, IConfigurationRepository configRepo)
        {
            this.coffeeRepo = coffeeRepo;
            this.configRepo = configRepo;
            this.M = configRepo.Configuration.Where(c => c.Name.Trim() == "m").FirstOrDefault().Value;
            this.X = configRepo.Configuration.Where(c => c.Name.Trim() == "x").FirstOrDefault().Value;
        }

        public ActionResult addToCart(Cart cart, int quantity, int coffeeId)
        {
            Coffee coffee = coffeeRepo.Coffee.Where(c => c.CoffeeID == coffeeId).FirstOrDefault();
            if (quantity < 1 || quantity > 10 || coffee == null)
            {
                ModelState.AddModelError("", "quantity range is from 1 to 10 and Coffee must exist");
                if (Request.IsAjaxRequest())
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Content("Error");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    cart.addToCart(coffee, quantity, configRepo.Configuration.Where(c => c.Name.Trim() == "n").FirstOrDefault().Value);
                }
                catch (Exception ex)
                {
                    if (Request.IsAjaxRequest())
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Content(ex.Message);
                    }
                }
            }
            CartTotalModelView model = new CartTotalModelView()
            {
                Cart = cart,
                M = this.M,
                X = this.X
            };
            if (Request.IsAjaxRequest()) return View("Summary", model);
            return View("Index", new CartModelView { Cart = cart });
        }

        public ActionResult removeFromCart(Cart cart, int coffeeId)
        {
            Coffee coffee = coffeeRepo.Coffee.Where(c => c.CoffeeID == coffeeId).FirstOrDefault();
            if (coffee == null)
            {
                ModelState.AddModelError("", "coffee must exist");
                if (Request.IsAjaxRequest())
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            if (ModelState.IsValid)
            {
                cart.RemoveFromCart(coffee);
            }
            if (Request.IsAjaxRequest()) return PartialView("CartItems", cart);
            return View("Index", new CartModelView { Cart = cart });
        }

        public ViewResult Index(Cart cart, string returnUrl = "/Coffee/List")
        {
            CartModelView model = new CartModelView()
            {
                Cart = cart,
                returnUrl = returnUrl
            };
            return View(model);
        }

        public ViewResult Summary(Cart cart)
        {
            CartTotalModelView model = new CartTotalModelView()
            {
                Cart = cart,
                M = configRepo.Configuration.Where(c => c.Name.Trim() == "m").FirstOrDefault().Value,
                X = configRepo.Configuration.Where(c => c.Name.Trim() == "x").FirstOrDefault().Value
            };
            return View(model);
        }

        public string GetTotal(Cart cart)
        {
            if (cart.Lines.Count() == 0) return String.Format("{0:C}", 0);
            return cart.CalculateTotal(X, M).ToString("C");
        }
    }
}