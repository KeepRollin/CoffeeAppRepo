using System.Collections.Generic;
using System.Linq;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Concrete.ADO;
using CoffeeApp.Domain.Concrete.EF;
using CoffeeApp.Domain.Entities;
using CoffeeApp.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using System;

namespace CoffeeApp.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Add_Order_Via_Controller()
        {
            //ARRANGE
            List<Order> orders = new List<Order>();
            Mock<IOrderRepository> mockOrder = new Mock<IOrderRepository>();
            mockOrder.Setup(m => m.AddOrder(It.IsAny<Order>())).Returns((Order order) =>
            {
                if (orders.LastOrDefault() == null)
                {
                    order.OrderID = 1;
                }
                else
                {
                    order.OrderID = orders.Last().OrderID + 1;
                }
                orders.Add(order);
                return true;
            });
            mockOrder.Setup(m => m.GetOrder(It.IsAny<int>())).Returns((int id) =>
            {
                return orders.Where(o => o.OrderID == id).FirstOrDefault();
            });
            OrderController target = new OrderController(mockOrder.Object);

            //ACT
            target.Index(new Order { Address = "lalala st.", Name = "lala", OrderDate = DateTime.Now });
            target.Index(new Order { Address = "dadada st.", Name = "dada", OrderDate = DateTime.Now });

            //ASSERT
            Assert.IsNotNull(orders.Last());
            Assert.AreEqual(orders.Last().Name, "dada");
            Assert.AreEqual(orders.Last().OrderID, orders.First().OrderID + 1);
        }   
    }
}