using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stark.NumberGenerator.Library.Banks
{
  public class Nedbank : BaseBank
  {
    public Nedbank(byte accountFudgeFactor = 9 /* 18 */)
      : base("198765", "11987654321", accountFudgeFactor, 11, new List<string>() { "10", "11", "12", "13", "15", "19", "20", "21", "23" }, 10)
    { }
  }
}
