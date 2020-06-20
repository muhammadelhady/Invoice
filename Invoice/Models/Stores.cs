using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Invoice.Models
{
  public  class Stores
    {
        [Key]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
     
    }
}
