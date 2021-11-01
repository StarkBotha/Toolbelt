using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stark.NumberGenerator.Library.CDVs;

namespace Stark.NumberGenerator.Library
{
    public class IDNumberModel
    {
        public String IDNumber { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public static IDNumberModel GenerateIDNumber()
        {
            DateTime today = DateTime.Now.Date;
            int endYear = Convert.ToInt32(today.AddYears(-21).ToString().Substring(2, 2));
            int startYear = Convert.ToInt32(today.AddYears(-65).ToString().Substring(2, 2));

            Random ran = new Random();

            while (true)
            {
                string year = ran.Next(endYear > startYear ? startYear : endYear, endYear > startYear ? endYear : startYear).ToString().PadLeft(2, '0');
                string month = ran.Next(1, 12).ToString().PadLeft(2, '0');
                string day = ran.Next(1, 31).ToString().PadLeft(2, '0');
                string lastDigits = ran.Next(0, 9999999).ToString().PadLeft(7, '0');
                string idNoString = String.Format("{0}{1}{2}{3}", year, month, day, lastDigits);

                string gender;
                string dateOfBirth;
                string errorMessage;
                bool result = IDNumberVerification.ValidIDNumber(idNoString, "", out gender, out dateOfBirth, out errorMessage);

                if (result)
                {
                    DateTime dob = new DateTime(Convert.ToInt32("19" + year), Convert.ToInt32(month), Convert.ToInt32(day));
                    return new IDNumberModel() { Gender = gender, DateOfBirth = dob, IDNumber = idNoString };
                }
            }

        }

        public override string ToString()
        {
            return String.Format("ID Number: {0}{1}Date of Birth: {2}{3}Gender: {4}", IDNumber, Environment.NewLine,
              DateOfBirth.ToShortDateString(), Environment.NewLine, Gender);
        }
    }
}
