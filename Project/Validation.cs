using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project
{
    public  static class Validation
    {
        public static bool IsThisNameValid(this string a)
        {
            Regex regex = new Regex("^[A-Za-z]{3,32}$");
            if (!regex.IsMatch(a)) 
                return false;
            return true;
        }
        public static bool IsThisEmailValid(this string a)
        {
            Regex regex = new Regex("^[A-Za-z0-9]{3,32}@[A-Za-z0-9]{3,32}\\.[A-Za-z]{2,3}$");
            if (!regex.IsMatch(a))
                return false;
            return true;
        }
        public static bool IsThisPhoneNumberValid(this string a)
        {
            Regex regex = new Regex("^09[0-9]{9}$");
            if (!regex.IsMatch(a)) 
                return false;
            return true;
        }
        public static bool IsThisPasswordValid(this string a)
        {
            Regex regex = new Regex(" ^(?=.*[a-z])(?=.*[A-Z] )(?=.*[0-9] )[a-zA-Z0-9]{8,32}$");
            if (!regex.IsMatch(a)) 
                return false;
            return true;
        }
        public static bool IsThisUserPassValid(this string a)
        {
            Regex regex = new Regex("^[0-9]{8}");
            if(!regex.IsMatch(a))
                return false;
            return true;
        }
        public static bool IsThisUserValid(this string a)
        {
            Regex regex = new Regex("^user[0-9]{1,4}");
            if (!regex.IsMatch(a))
                return false;
            return true;
        }
        public static bool IsThisCVVValid(this string a)
        {
            Regex regex = new Regex("^[0-9]{3,4}$");
            if (!regex.IsMatch(a)) 
                return false;
            return true;
        }
        public static bool IsThisSSNValid(this string a)
        {
            Regex regex = new Regex("^00[0-9]{8}$");
            if(!regex.IsMatch(a))
                return false;
            return true;

        }
        public static bool IsThisIDValid(this string a)
        {
            Regex regex = new Regex("^[0-9]{2}9[0-9]{2}$");
            if (!regex.IsMatch(a))
                return false;
            return true;
        }
        public static bool IsExpired(this int year, int month)
        {
            DateTime now = DateTime.Now;
            int year1 = now.Year;
            int month1 = now.Month;

            if (year >= year1)
            {
                if (year > year1) return true;
                if(month >= month1) return true;
            }
            return false;
        }
        public static bool IsCardValid(this string code)
        {
            int sum = 0;

            for (int i = 0; i < code.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sum += int.Parse(code[i].ToString());
                }
                else
                {
                    sum += int.Parse(code[i].ToString()) * 2;
                }
            }
            if (sum % 10 != 0) 
                return false;
            return true;
        }

    }
}
