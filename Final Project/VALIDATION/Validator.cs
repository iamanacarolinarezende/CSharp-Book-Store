using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Final_Project.VALIDATION
{
    public class Validator
    {
        //Validate Email
        public static bool IsValidEmailFormat(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(input, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        //Validate Password
        public static bool IsValidPassword(string input)
        {
            if (!Regex.IsMatch(input, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
            {
                return false;

            }
            return true;
        }

        //Validate Phone Number Canada
        public static bool IsValidPhone(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string pattern = @"^(\d{3})-(\d{3})-(\d{4})$";

            return Regex.IsMatch(input, pattern);
        }

        //Validate Employee ID
        public static bool IsValidId(string input)
        {
            if (!Regex.IsMatch(input, @"^\d{4}$"))
            {
                return false;
            }

            return true;
        }
        public static bool IsValidId(string input, int size)
        {
            if (!Regex.IsMatch(input, @"^\d{" + size + "}$"))
            {
                return false;
            }

            return true;
        }

        //Validate Name
        public static bool IsValidName(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (!(Char.IsLetter(input[i])) && !(Char.IsWhiteSpace(input[i])))
                {
                    return false;

                }
            }

            return true;
        }

        //Validate Zip Code as Canada Format
        public static bool IsValidZip(string postalCode)
        {
            if (postalCode.Length == 0)
            {
                return false;
            }

            postalCode = postalCode.Trim().ToUpper();

            if ((postalCode.StartsWith("G") || postalCode.StartsWith("H") || postalCode.StartsWith("J"))
                 && char.IsDigit(postalCode[1]))
            {
                string postalCodePattern = @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$";
                return System.Text.RegularExpressions.Regex.IsMatch(postalCode, postalCodePattern);
            }
            else
            {
                return false;
            }
        }

        //Validate Job role
        public static bool IsValidJobRole(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length > 50)
            {
                return false;
            }

            foreach (char c in input)
            {
                if (!(char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\''))
                {
                    return false;
                }
            }

            return true;
        }

        //Validate ISBN
        public static bool IsValidISBN(string isbn)
        {
            isbn = isbn.Replace(" ", "").Replace("-", "");

            if (isbn.Length != 10 && isbn.Length != 13)
            {
                return false;
            }

            if (isbn.Length == 10)
            {
                int checksum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int digit = 0;
                    if (!int.TryParse(isbn[i].ToString(), out digit))
                    {
                        return false;
                    }
                    checksum += (digit * (10 - i));
                }

                char lastDigit = isbn[9];
                if (lastDigit == 'X')
                {
                    checksum += 10;
                }
                else
                {
                    int digit = 0;
                    if (!int.TryParse(lastDigit.ToString(), out digit))
                    {
                        return false;
                    }
                    checksum += digit;
                }

                return (checksum % 11 == 0);
            }
            else if (isbn.Length == 13)
            {
                int checksum = 0;
                for (int i = 0; i < 12; i++)
                {
                    int digit = 0;
                    if (!int.TryParse(isbn[i].ToString(), out digit))
                    {
                        return false;
                    }
                    checksum += (i % 2 == 0) ? digit : digit * 3;
                }

                int lastDigit = 0;
                if (!int.TryParse(isbn[12].ToString(), out lastDigit))
                {
                    return false;
                }

                int remainder = checksum % 10;
                int checkDigit = (remainder == 0) ? 0 : (10 - remainder);
                return (lastDigit == checkDigit);
            }

            return false;
        }

        //Book Year validation
            public static bool IsValidYear(string year)
            {
                if (year.Length != 4)
                {
                    return false;
                }

                if (!int.TryParse(year, out int yearValue)) //string to int
                {
                    return false;
                }

                int currentYear = DateTime.Now.Year;
                if (yearValue < currentYear)
                {
                    return true;
                }

                return false;
            }


    }

}
