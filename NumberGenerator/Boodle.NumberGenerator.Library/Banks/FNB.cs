using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stark.NumberGenerator.Library.Banks
{
  public class FNB : BaseBank
  {
    public FNB()
      : base("250655", "12121212121", 0, 10, new List<string>() { "62" }, 11)
    { }
  }
}
