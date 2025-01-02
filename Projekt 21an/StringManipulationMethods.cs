using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public static class StringManipulationMethods
    {
        public static string CapitalizeFirstLetter(string str)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        public static void SkrivUtIFärg(string textAttSkrivaUt, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(textAttSkrivaUt);
            Console.ForegroundColor = ConsoleColor.Black;
        }

    }
}
