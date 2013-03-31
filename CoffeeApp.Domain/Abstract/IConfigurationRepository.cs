using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Domain.Abstract
{
    public interface IConfigurationRepository
    {
        IQueryable<Configuration> Configuration { get; }   
    }
}
