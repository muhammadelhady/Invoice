using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Invoice.Models
{
   public class Invoices
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public double InvoiceTotal { get; set; }
        public double InvoiceTax { get; set; }
        public double InvoiceNet { get; set; }
        public Stores StoreId { get; set; }
    }
}
