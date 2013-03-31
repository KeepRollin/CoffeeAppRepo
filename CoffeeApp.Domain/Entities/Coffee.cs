using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeApp.Domain.Entities
{
    public class Coffee
    {
        public int CoffeeID { get; set; }

        [Display(Name = "Name of the coffee")]
        public string Name { get; set; }

        [Display(Name = "Description of the coffee")]
        public string Description { get; set; }
     
        [Display(Name = "Price of the coffee")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
    }
}