using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boodle.NumberGenerator.Library.CreditCards
{
    public class CardDetails
    {
        public string Provider { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int CVV { get; set; }
    }
}
