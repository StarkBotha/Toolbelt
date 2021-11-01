using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stark.NumberGenerator.Library.Banks
{
  public abstract class BaseBank
  {
    public String AccountNumber { get; set; }
    public String BranchCode { get; set; }
    public String AccountType { get; set; }
    public String AccountWeighting { get; set; }
    public Byte AccountfudgeFactor { get; set; }
    public Byte AccountModulus { get; set; }
    public Char? AccountExceptCode { get; set; }
    public Int32 AccountNumberLength { get; set; }
    public List<String> StartingDigits { get; set; }

    public BaseBank(string branchCode, string accountWeighting, byte accountFudgeFactor, byte accountModulus, List<String> startingDigits, int accountNumberLength)
    {
      BranchCode = branchCode;
      AccountWeighting = accountWeighting;
      AccountfudgeFactor = accountFudgeFactor;
      AccountModulus = accountModulus;
      AccountExceptCode = new Char?();
      StartingDigits = startingDigits;
      AccountNumberLength = accountNumberLength;
    }

    public BaseBank GenerateAccountNumber()
    {
      Random ran = new Random();
      int remaining = AccountNumberLength - StartingDigits.FirstOrDefault().Length;
      int nextInt = Convert.ToInt32(("9".PadLeft(remaining, '9')));

      while (true)
      {
        string randomStr = ran.Next(0, nextInt).ToString().PadLeft(remaining, '0');
        var accountStr = String.Format("{0}{1}", StartingDigits.ElementAt(ran.Next(0, StartingDigits.Count())), randomStr);
        string resultMessage;
        bool result = CheckDigitVerification.PerformCDV(AccountWeighting, accountStr, AccountModulus, AccountfudgeFactor, AccountExceptCode, out resultMessage);
        if (result)
        {
          AccountNumber = accountStr;
          return this;
        }
      }
    }

    public override string ToString()
    {
      return String.Format("Account Number: {0}{1}Branch Code: {2}", AccountNumber, Environment.NewLine, BranchCode);
    }
  }
}
