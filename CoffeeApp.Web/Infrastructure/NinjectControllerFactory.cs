using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Concrete.ADO;
using CoffeeApp.Domain.Concrete.EF;
using CoffeeApp.Domain.Entities;
using Moq;
using Ninject;

namespace CoffeeApp.Web.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(
        RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            Mock<ICoffeeRepository> mockCoffee = new Mock<ICoffeeRepository>();
            Mock<IConfigurationRepository> mockConfig = new Mock<IConfigurationRepository>();
            Mock<IOrderRepository> mockOrder = new Mock<IOrderRepository>();
            mockCoffee.Setup(m => m.Coffee).Returns(new List<Coffee>
            {
                new Coffee { Name = "Cappuccino", Price = 5, CoffeeID = 1},
                new Coffee { Name = "Espresso", Price = 4, CoffeeID = 2},
                new Coffee { Name = "Double espresso", Price = 3, CoffeeID = 3},
                new Coffee { Name = "Regular coffee", Price = 1, CoffeeID = 4},
                new Coffee { Name = "Latte", Price = 1, CoffeeID = 5},
                new Coffee { Name = "Affogato", Price = 1, CoffeeID = 6}
            }.AsQueryable());

            mockConfig.Setup(m => m.Configuration).Returns(new List<Configuration>
            {
                new Configuration { Name = "m", Value = 2, ConfigurationId = 1 },
                new Configuration { Name = "n", Value = 5, ConfigurationId = 2 },
                new Configuration { Name = "x", Value = 10, ConfigurationId = 3}
            }.AsQueryable());

            List<Order> orders = new List<Order>();
            mockOrder.Setup(m => m.AddOrder(It.IsAny<Order>())).Returns((Order order) =>
            {
                order.OrderDate = DateTime.Now;
                if (orders.LastOrDefault() == null)
                {
                    order.OrderID = 1;
                    order.OrderNumber = (int)(order.OrderDate.Year + order.OrderDate.Month + order.OrderDate.Day) * (order.OrderDate.Hour + order.OrderDate.Minute + order.OrderDate.Second) + 1;
                }
                else
                {
                    order.OrderID = orders.Last().OrderID + 1;
                    order.OrderNumber = orders.First().OrderNumber + order.OrderID;
                }
                orders.Add(order);
                return true;
            });
            mockOrder.Setup(m => m.GetOrder(It.IsAny<int>())).Returns((int id) =>
            {
                return orders.Where(o => o.OrderID == id).FirstOrDefault();
            });

            //string ConnectionVia = System.Configuration.ConfigurationManager.AppSettings["ConnectionVia"];
            string ConnectionVia = null;
            if (ConnectionVia == "EF")
            {
                ninjectKernel.Bind<ICoffeeRepository>().To<EFCoffeeRepository>();
                ninjectKernel.Bind<IConfigurationRepository>().To<EFConfigurationRepository>();
            }
            else if (ConnectionVia == "ADO")
            {
                ninjectKernel.Bind<ICoffeeRepository>().To<ADOCoffeeRepository>();
                ninjectKernel.Bind<IConfigurationRepository>().To<ADOConfigurationRepository>();
            }
            else
            {
                ninjectKernel.Bind<ICoffeeRepository>().ToConstant(mockCoffee.Object);
                ninjectKernel.Bind<IConfigurationRepository>().ToConstant(mockConfig.Object);
                ninjectKernel.Bind<IOrderRepository>().ToConstant(mockOrder.Object);
            }
        }
    }
}