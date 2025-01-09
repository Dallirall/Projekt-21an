using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public interface IKonsolKortspel
    {
        string SpeletsNamn { get; }
        string Regler { get; }



        void RunGame();
    }
}
