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
    }
    
}
