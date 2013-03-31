using System.Linq;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Domain.Abstract
{
    public interface ICoffeeRepository
    {
        IQueryable<Coffee> Coffee { get; }
    }
}