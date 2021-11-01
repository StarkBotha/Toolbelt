using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stark.NumberGenerator.Library.Other
{
    public class RandomGeneratorList
    {
        public static Dictionary<Int32, String> GetRandomGeneratorList()
        {
            Dictionary<Int32, String> result = new Dictionary<Int32, String>();
            result.Add(1, "ID Number");
            result.Add(2, "ABSA Account Number");
            result.Add(3, "Capitec Account Number");
            result.Add(4, "FNB Account Number");
            result.Add(5, "Nedbank Account Number");
            result.Add(6, "Standard Bank Account Number");
            result.Add(7, "GUID Generator");
            return result;
        }
    }
}
