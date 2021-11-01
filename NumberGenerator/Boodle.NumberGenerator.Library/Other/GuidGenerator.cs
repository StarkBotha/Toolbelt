using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boodle.NumberGenerator.Library.Other
{
    public class GuidGenerator
    {
        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
