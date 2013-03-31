using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Domain.Abstract
{
    public interface IOrderRepository
    {
        bool AddOrder(Order order);
        Order GetOrder(int orderId);
    }
}
