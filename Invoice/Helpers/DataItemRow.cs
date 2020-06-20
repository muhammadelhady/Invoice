using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Helpers
{
    class DataItemRow
    {
        public string Item { get; set; }
        public string Unit { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Total { get; set; }
        public string Discount { get; set; }
        public string Net { get; set; }

    }
}
