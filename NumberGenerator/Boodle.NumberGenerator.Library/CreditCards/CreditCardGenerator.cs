using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Boodle.NumberGenerator.Library.CreditCards
{
    public class CreditCardGenerator
    {
        //1
        public static string[] AMEX_PREFIX_LIST = new[] { "34", "37" };

        //2
        public static string[] DINERS_PREFIX_LIST = new[]
                                                        {
                                                            "300",
                                                            "301", "302", "303", "36", "38"
                                                        };

        //3
        public static string[] DISCOVER_PREFIX_LIST = new[] { "6011" };

        //4
        public static string[] ENROUTE_PREFIX_LIST = new[]
                                                        {
                                                            "2014",
                                                            "2149"
                                                        };

        //5
        public static string[] JCB_15_PREFIX_LIST = new[]
                                                        {
                                                            "2100",
                                                            "1800"
                                                        };

        //6
        public static string[] JCB_16_PREFIX_LIST = new[]
                                                        {
                                                            "3088",
                                                            "3096", "3112", "3158", "3337", "3528"
                                                        };

        //7
        public static string[] MASTERCARD_PREFIX_LIST = new[]
                                                            {
                                                                "51",
                                                                "52", "53", "54", "55"
                                                            };

        //8
        public static string[] VISA_PREFIX_LIST = new[]
                                                    {
                                                        "4539",
                                                        "4556", "4916", "4532", "4929", "40240071", "4485", "4716", "4"
                                                    };

        //9
        public static string[] VOYAGER_PREFIX_LIST = new[] { "8699" };

        /*
            'prefix' is the start of the  CC number as a string, any number
            private of digits   'length' is the length of the CC number to generate.
        * Typically 13 or  16
        */
        private static string CreateFakeCreditCardNumber(string prefix, int length)
        {
            string ccnumber = prefix;

            while (ccnumber.Length < (length - 1))
            {
                double rnd = (new Random().NextDouble() * 1.0f - 0f);

                ccnumber += Math.Floor(rnd * 10);

                //sleep so we get a different seed

                Thread.Sleep(20);
            }


            // reverse number and convert to int
            var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();

            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

            // calculate sum

            int sum = 0;
            int pos = 0;
            int[] reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < length - 1)
            {
                int odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != (length - 2))
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }

            // calculate check digit
            int checkdigit =
                Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;

            ccnumber += checkdigit;

            return ccnumber;
        }


        public static IEnumerable<string> GetCreditCardNumbers(string[] prefixList, int length, int howMany)
        {
            var result = new Stack<string>();

            for (int i = 0; i < howMany; i++)
            {
                int randomPrefix = new Random().Next(0, prefixList.Length - 1);

                if (randomPrefix > 1)
                {
                    randomPrefix--;
                }

                string ccnumber = prefixList[randomPrefix];

                result.Push(CreateFakeCreditCardNumber(ccnumber, length));
            }

            return result;
        }


        public static IEnumerable<string> GenerateMasterCardNumbers(int howMany)
        {
            return GetCreditCardNumbers(MASTERCARD_PREFIX_LIST, 16, howMany);
        }


        public static string GenerateMasterCardNumber()
        {
            return GetCreditCardNumbers(MASTERCARD_PREFIX_LIST, 16, 1).First();
        }
        public static bool IsValidCreditCardNumber(string creditCardNumber)
        {
            try
            {
                var reversedNumber = creditCardNumber.ToCharArray().Reverse();

                int mod10Count = 0;
                for (int i = 0; i < reversedNumber.Count(); i++)
                {
                    int augend = Convert.ToInt32(reversedNumber.ElementAt(i).ToString());

                    if (((i + 1) % 2) == 0)
                    {
                        string productstring = (augend * 2).ToString();
                        augend = 0;
                        for (int j = 0; j < productstring.Length; j++)
                        {
                            augend += Convert.ToInt32(productstring.ElementAt(j).ToString());
                        }
                    }
                    mod10Count += augend;
                }

                if ((mod10Count % 10) == 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static CardDetails GenerateRandomCardNumber()
        {
            var rnd = new Random();
            var randomisedProvider = rnd.Next(4);

            List<string> selectedProvider = MASTERCARD_PREFIX_LIST.ToList(); //Default to mastercard

            var cardDetail = new CardDetails();

            switch (randomisedProvider)
            {
                case 0:
                    selectedProvider = VISA_PREFIX_LIST.ToList();
                    cardDetail.Provider = "VISA";
                    break;
                case 1:
                    selectedProvider = AMEX_PREFIX_LIST.ToList();
                    cardDetail.Provider = "AMEX";
                    break;
                case 2:
                    selectedProvider = DINERS_PREFIX_LIST.ToList();
                    cardDetail.Provider = "DINERS";
                    break;
                case 3:
                    selectedProvider = MASTERCARD_PREFIX_LIST.ToList();
                    cardDetail.Provider = "MASTERCARD";
                    break;                
            }

            cardDetail.CardNumber = GetCreditCardNumbers(selectedProvider.ToArray(), 16, 1).First();
            cardDetail.CVV = rnd.Next(100,999);
            cardDetail.ExpiryDate = $"{rnd.Next(DateTime.Now.Month, 13)}/{rnd.Next(DateTime.Now.Year, DateTime.Now.Year + 6).ToString().Substring(2,2)}";

            return cardDetail;
        }
    }
}
