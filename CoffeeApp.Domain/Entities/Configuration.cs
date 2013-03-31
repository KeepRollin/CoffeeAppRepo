using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeApp.Domain.Entities
{
    public class Configuration
    {
        public int ConfigurationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Value { get; set; }
    }
}