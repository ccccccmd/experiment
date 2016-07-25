using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFTest.Models
{
    public class SecondCar
    {
        public int Id { get; set; }

        [MaxLength(20), Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
    }
}