using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Stark.NumberGenerator.Library.CDVs
{
  public class IDNumberVerification
  {
    public static bool IsBlank(string inStr, string caption, out string errorMsg)
    {
      errorMsg = string.Empty;
      if (inStr.Trim() == string.Empty)
      {
        errorMsg = string.Format("Please specify a {0}.", caption);
        return true;
      }
      return false;
    }

    public static bool IsNumeric(string inStr)
    {
      double dResult;
      if (double.TryParse(inStr, System.Globalization.NumberStyles.Any, null, out dResult)) return true;
      return false;
    }

    public static bool ValidIDNumber(string inIDNo, string caption, out string gender, out string dateOfBirth, out string errorMsg)
    {
      errorMsg = string.Empty;
      gender = string.Empty;
      dateOfBirth = string.Empty;

      if (IsBlank(inIDNo.Trim(), caption, out errorMsg)) return false;

      if (!IsNumeric(inIDNo.Trim()))
      {
        errorMsg = string.Format("Please specify a valid {0}.", caption);
        return false;
      }

      Regex _expression;
      Match _match;
      string _IDExpression = @"(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])";

      _expression = new Regex(_IDExpression, RegexOptions.Compiled | RegexOptions.Singleline);
      _match = _expression.Match(inIDNo.Trim());

      if (!_match.Success)
      {
        errorMsg = string.Format("Please specify a valid {0}.", caption);
        return false;
      }

      // Calculate total A by adding the figures in the odd positions i.e. the first, third, fifth,
      // seventh, ninth and eleventh digits.
      int a = int.Parse(_match.Value.Substring(0, 1)) + int.Parse(_match.Value.Substring(2, 1)) +
      int.Parse(_match.Value.Substring(4, 1)) + int.Parse(_match.Value.Substring(6, 1)) +
      int.Parse(_match.Value.Substring(8, 1)) + int.Parse(_match.Value.Substring(10, 1));

      // Calculate total B by taking the even figures of the number as a whole number, and then
      // multiplying that number by 2, and then add the individual figures together.
      int b = int.Parse(_match.Value.Substring(1, 1) + _match.Value.Substring(3, 1) +
      _match.Value.Substring(5, 1) + _match.Value.Substring(7, 1) +
      _match.Value.Substring(9, 1) + _match.Value.Substring(11, 1));

      b *= 2;
      string bString = b.ToString();

      b = 0;
      for (int index = 0; (index < bString.Length); index++)
      {
        b += int.Parse(bString.Substring(index, 1));
      }

      // Calculate total C by adding total A to total B.
      int c = (a + b);

      // The control-figure can now be determined by subtracting the ones in figure C from 10.
      int control = 0;
      string cString = c.ToString();
      cString = cString.Substring(cString.Length - 1, 1);

      // Where the total C is a multiple of 10, the control figure will be 0.
      if (cString != "0")
      {
        control = (10 - int.Parse(cString.Substring(cString.Length - 1, 1)));
      }

      if (_match.Groups["Control"].Value != control.ToString())
      {
        errorMsg = string.Format("The {0} you have entered is invalid.", caption);
        return false;
      }
      // Build up date of birth
      dateOfBirth = string.Format("19{0}{1}{2}", _match.Groups["Year"].Value, _match.Groups["Month"].Value, _match.Groups["Day"].Value);
      // Gender checks
      gender = (Convert.ToInt32(_match.Groups["Gender"].Value) <= 4) ? "F" : (Convert.ToInt32(_match.Groups["Gender"].Value) >= 5) ? "M" : "Unknown";
      //
      return true;
    }
  }
}
