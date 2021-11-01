using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stark.NumberGenerator.Library.Banks;

namespace Stark.NumberGenerator.Library.Banks
{
  public class ABSA : BaseBank
  {
    public ABSA()
      : base("632005", String.Empty, 0, 0, new List<String>() { "40", "90", "91", "92" }, 10)
    {
      AccountExceptCode = Convert.ToChar("f");
    }
  }
}
