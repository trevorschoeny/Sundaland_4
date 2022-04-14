using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sundaland.Models
{
    public class Purchase
    {
        [Key]
        [BindNever]
        public int PurchaseId { get; set; }

        [BindNever]
        public ICollection<BasketLineItem> Lines { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please enter the city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter the state")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter the zip code")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please enter the country")]
        public string Country { get; set; }

        [BindNever]
        public bool PurchaseShipped { get; set; } = false;
    }
}
