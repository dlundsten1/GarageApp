using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GarageApp.Models
{
    public class CarModel
    {
      
        public int Id { get; set; }
        [Display(Name = "Registringsnummer")]
        public string Reg { get; set; }
        [Display(Name = "Instämpling")]
        public System.DateTime In { get; set; }
        [Display(Name = "Utstämpling")]
        public System.DateTime Out { get; set; }
        [Display(Name = "Pris")]
        public int Price { get; set; }
        [Display(Name = "Betalad")]
        public Boolean Paid { get; set; }
        [Display(Name = "Parkerad tid")]
        public System.TimeSpan timeSpan { get; set; }
}
}