using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stark.NumberGenerator.Library.Banks
{
  public class StandardBank : BaseBank
  {
    public StandardBank()
      : base("051001", null, 0, 11, new List<string>() { "00", "03", "23", "25", "28", "42", "35" }, 9)
    {
      AccountExceptCode = Convert.ToChar("m");
    }
  }
}
