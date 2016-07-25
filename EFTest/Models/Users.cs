using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EFTest.Models
{
    public class Users
    {
        public int Id { get; set; }
        [MaxLength(50), Required(ErrorMessage = "不能为空")]
        public string UserName { get; set; }
        [Required ]
        public int Age { get; set; }
      // [NotMapped]
      public int? InterestCarId { get; set; }

        [ForeignKey("InterestCarId")]
        public virtual Cars InterestCar { get; set; }

       
    }
}