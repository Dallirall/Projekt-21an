using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public class Spelare
    {
        //private string? _namn;
        public string? Namn { get ; set; }
        public int Vinster { get; set; }
        public int Förluster { get; set; }
        public int Oavgjort { get; set; }
        public int Poäng { get; set; }
        public Spelare(string namn)
        {
            Namn = namn;
        }
        
    }
}
