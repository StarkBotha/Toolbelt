using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stark.NumberGenerator.Library
{
  public class CheckDigitVerification
  {
    public static bool VerifyException(string accountNo, char? exceptCode)
    {
      var passed = false;
      if (exceptCode != null)
      {
        if (exceptCode.ToString() == "e")
        {
          var leastDigit1 = int.Parse(accountNo.Substring(accountNo.Length - 2, 1));
          var leastDigit2 = int.Parse(accountNo.Substring(accountNo.Length - 3, 1));
          var leastDigit10 = int.Parse(accountNo.Substring(accountNo.Length - 11, 1));
          var leastDigit11 = int.Parse(accountNo.Substring(accountNo.Length - 12, 1));

          if (leastDigit1 > 0 && leastDigit2 > 0 && leastDigit11 == 0 && leastDigit10 > 0)
            passed = true;
        }
        else if (exceptCode.ToString() == "f")
        {
          if (accountNo.Length == 11)
          {
            if (accountNo.StartsWith("53"))
            {
              if (CheckResult(accountNo, "00000000000", 0, 0, false))
                passed = true;
            }
          }

          var arrNextWeighting = new string[] { "17329874321", "14327654321", "54327654321", "11327654321" };
          byte weightCount = 0;
          while (weightCount < arrNextWeighting.Length)
          {
            passed = CheckResult(accountNo, arrNextWeighting[weightCount], 0, (weightCount == (byte)0 ? (byte)10 : (byte)11),
                               (weightCount == 2 ? true : false));
            if (passed)
              break;
            weightCount += 1;
          }

          if (!passed)
          {
            if (accountNo.Length < 10)
            {
              if (!CheckResult(accountNo, "11327654321", 0, 11, false))
              {
                var leastDigit1 = int.Parse(accountNo.Substring(accountNo.Length - 2, 1));
                var changedAccountNo = accountNo.Substring(0, accountNo.Length - 1) +
                                       (leastDigit1 + 6).ToString();
                if (CheckResult(changedAccountNo, "11327654321", 0, 11, false))
                  passed = true;
              }
            }

            if (!passed)
            {
              if (CheckResult(accountNo, "14329874321", 0, 10, false))
                passed = true;
            }
          }
        }
        else if (exceptCode.ToString() == "m") // Standard Bank
        {
          if (accountNo.Length <= 9)
          {
            accountNo = accountNo.PadLeft(9, Convert.ToChar("0"));
            if (CheckResult(accountNo, "11987654321", 0, 11, false))
            {
              passed = true;
            }
          }
          else if (accountNo.StartsWith("1") && accountNo.Length == 11)
          {
            if (CheckResult(accountNo, "1312987654321", 0, 11, false))
              passed = true;
          }
        }
      }
      return passed;
    }

    public static bool CheckResult(string accountNo, string weighting, byte fudgeFactor, byte modulus, bool allowSingleRemainder)
    {
      // Pad account no.'s
      if (accountNo.Length != weighting.Length && weighting.Length <= 11)
      {
        accountNo = accountNo.PadLeft(weighting.Length, Convert.ToChar("0"));
      }

      // split weighting if >= 11 characters for CDV Routine 
      var weightingSplit = new int[11];
      if (weighting.Length >= 11)
      {
        if (weighting.Length > 11)
        {
          weightingSplit[0] = int.Parse(weighting.Substring(0, 2));
          weightingSplit[1] = int.Parse(weighting.Substring(2, 2));
        }
        else
        {
          weightingSplit[0] = int.Parse(weighting.Substring(0, 1));
          weightingSplit[1] = int.Parse(weighting.Substring(1, 1));
        }

        for (int i = 2; i < 11; i++)
        {
          weightingSplit[i] = int.Parse(weighting[i + ((weighting.Length == 11) ? 0 : 2)].ToString());
        }
      }

      var passed = false;
      int totalWeight = 0;

      for (int i = accountNo.Length; i >= 1; i--)
      {
        if (((i + weighting.Length) - accountNo.Length) == 0)
          totalWeight = totalWeight + (int.Parse(accountNo[i].ToString()) * int.Parse(weighting[(i + weighting.Length) - accountNo.Length].ToString()));
      }
      for (int i = 0; i <= accountNo.Length - 1; i++)
      {
        var weightingValue = int.Parse(weighting[i + ((weighting.Length == 11) ? 0 : 2)].ToString());
        if (accountNo.Length >= 11)
        {
          weightingValue = weightingSplit[i];
        }
        totalWeight = totalWeight + (int.Parse(accountNo[i].ToString()) * weightingValue);
      }
      totalWeight = totalWeight + fudgeFactor;

      var modRemainder = 2;
      if (modulus > 0)
        modRemainder = totalWeight % modulus;
      if (modRemainder == 0 | (allowSingleRemainder && modRemainder == 1))
        passed = true;
      return passed;
    }

    public static bool PerformCDV(string weighting, string accountNo, byte modulus, byte fudgeFactor, char? exceptCode, out string returnMessage)
    {
      returnMessage = "";
      var cdvPass = false;
      var finalWeighting = "1";

      var bankAccountNo = accountNo;

      if (weighting != null && exceptCode == null)
      {
        finalWeighting = weighting;

        if (weighting != "1")
        {
          if (bankAccountNo.Length > weighting.Length)
            bankAccountNo = bankAccountNo.Substring(0, weighting.Length);
          else if (bankAccountNo.Length < weighting.Length)
            bankAccountNo = bankAccountNo.PadLeft(weighting.Length, Convert.ToChar("0"));
        }

      }
      if (modulus > 0 && weighting != null && exceptCode == null)
      {
        if (CheckResult(bankAccountNo, finalWeighting, fudgeFactor, modulus, false))
          cdvPass = true;
      }
      else
      {
        if (VerifyException(bankAccountNo, exceptCode))
          cdvPass = true;
      }

      if (!cdvPass)
        returnMessage = "Invalid bank account - CDV failed";

      return cdvPass;
    }
  }
}
