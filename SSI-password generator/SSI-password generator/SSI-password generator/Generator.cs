using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI_password_generator
{
    public static class Generator
    {
        public static string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static string alphabetUP = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string numbers = "0123456789";
        public static string symbols = "!@#$%^&*()_-+=[{]};:<>|./?";

        public static int[] elements = new int[4];
        public static string GeneratePassword(int length)
        {
            Random rnd = new Random();
            int k = 0;
            int x;
            string pass = "";
            string from = "";
            if (elements[0] == 1) { from = from + alphabet; k = k + alphabet.Length; }
            if (elements[1] == 1) { from = from + alphabetUP; k = k + alphabetUP.Length; }
            if (elements[2] == 1) { from = from + numbers; k = k + numbers.Length; }
            if (elements[3] == 1) { from = from + symbols; k = k + symbols.Length; }
            if (k == 0) return "check properties!";
            for (int i = 0; i < length; i++)
            {
                x = rnd.Next(k);
                pass = pass + from[x];
            }

            return pass;
        }
    }
}