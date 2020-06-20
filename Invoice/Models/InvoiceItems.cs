using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Transactions;

namespace Invoice.Models
{
    public class InvoiceItems
    {
        [Key]
        public int  ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemUnit { get; set; }
        public double ItemPrice { get; set; }
        public long   ItemQuantity { get; set; }
                     
        public double ItemTotal { get; set; }
        public double ItemDiscount { get; set; }
        public double ItemNet { get; set; }
        public Invoices InvoiceId { get; set; }

    }
}
