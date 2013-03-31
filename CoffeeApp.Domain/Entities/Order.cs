using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CoffeeApp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeApp.Domain.Entities
{
    public class Order
    {        
        public int OrderID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Name should have length between 2 and 15 characters")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Address should have length between 2 and 15 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage= "Phone number is required")]
        [RegularExpression(@"\+\d{1,3}\s?\d{10}", ErrorMessage = "Specify phone number in international format please (+XXX YYYYYYYYYY)")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        public System.DateTime OrderDate { get; set; }

        public int OrderNumber { get; set; }
    }
}