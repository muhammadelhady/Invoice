using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Helpers
{
    class ItemCalculationHelper
    {
        public double Price { get; set; }
        public double  Quantity { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public double Net { get; set; }
    }
}
