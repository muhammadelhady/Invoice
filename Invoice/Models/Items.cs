using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace Invoice.Models
{
   public class Items
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Stores Stores { get; set; }
    }
}
