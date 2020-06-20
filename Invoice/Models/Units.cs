using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Invoice.Models
{
   public class Units
    {
        [Key]
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public double UnitPrice { get; set; }
        public Items Items { get; set; }
    }
}
