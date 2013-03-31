using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepo)
        {
            this._orderRepo = orderRepo;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepo.AddOrder(order);
                return View("Success");
            }
            return View(order);
        }
    }
}
