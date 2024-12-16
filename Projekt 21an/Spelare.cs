using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public static class Spelare
    {

        //public Spelare(string namn) 
        //{
        //    namn = Namn;
        //    //vinster = Vinster;
        //    //förluster = Förluster;
        //    //oavgjort = Oavgjort;
        //}
        public static string Namn { get; set; }
        public static int Vinster { get; set; }
        public static int Förluster { get; set; }
        public static int Oavgjort { get; set; }
    }
}
