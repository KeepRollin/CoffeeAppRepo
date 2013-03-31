using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Web.Controllers
{
    public class CoffeeController : Controller
    {
        ICoffeeRepository coffee_repo;
        IConfigurationRepository config_repo;
        //
        // GET: /Home/
        public CoffeeController(ICoffeeRepository coffee_repo, IConfigurationRepository config_repo)
        {
            this.coffee_repo = coffee_repo;
            this.config_repo = config_repo;
        }

        public ActionResult List()
        {
            
            return View(coffee_repo.Coffee.OrderBy(c=>c.Name));
        }

        public PartialViewResult Discount()
        {
            return PartialView(config_repo.Configuration);
        }
    }
}
