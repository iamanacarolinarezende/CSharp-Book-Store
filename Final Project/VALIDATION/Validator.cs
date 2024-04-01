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

            string pattern = @"^(\d{3})[-\s]?(\d{3})[-\s]?(\d{4})$";

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
    }
}
