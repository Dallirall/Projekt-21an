using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    interface IGame
    {
        string SpeletsNamn { get; }
        string Regler { get; }
        string? Vinnare { get; }

        void Spelinställningar();

        void RunGame();
    }
}
