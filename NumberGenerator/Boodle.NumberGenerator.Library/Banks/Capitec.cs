using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stark.NumberGenerator.Library.Banks
{
  public class Capitec : BaseBank
  {
    public Capitec()
      : base("470010", "21987654321", 0, 11, new List<string>() { "11", "12", "13" }, 10)
    { }
  }
}
