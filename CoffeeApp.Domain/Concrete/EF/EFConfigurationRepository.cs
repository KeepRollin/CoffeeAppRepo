using System.Linq;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Domain.Concrete.EF
{
    public class EFConfigurationRepository : IConfigurationRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Configuration> Configuration { get { return context.Configurations; } }
    }
}