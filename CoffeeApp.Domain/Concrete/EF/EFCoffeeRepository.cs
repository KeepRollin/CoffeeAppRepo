using System.Linq;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Domain.Concrete.EF
{
    public class EFCoffeeRepository : ICoffeeRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Coffee> Coffee { get { return context.Coffees; } }
    }
}