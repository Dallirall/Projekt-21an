using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public static partial class Spelinställningar
    {
        public static int KortMaxVärde { get; set; }
        public static int KortMinVärde { get; set; }
        public static EnumVärden.Svårighetsgrader Svårighetsgrad { get; set; }
        public static int AntalKortAttBörjaMed { get; set; }
        public static bool MöjligtMedOavgjort { get; set; }
        public static int DatornSlutarDraKortVid { get; set; }


        public static double ProcentsatsLättTillSpelaren { get; set; }
        public static double ProcentsatsSvårTillSpelaren { get; set; }
        public static double ProcentsatsOmöjligTillSpelarenLägre { get; set; }
        public static double ProcentsatsOmöjligTillSpelarenHögre { get; set; }


        public static double ProcentsatsLättTillDatorn { get; set; }
        public static double ProcentsatsOmöjligTillDatornLägre { get; set; }
        public static double ProcentsatsOmöjligTillDatornHögre { get; set; }


        public static List<int> SpelarensLättaKortvärden { get; set; } = new List<int>();
        public static List<int> SpelarensSvåraKortvärden { get; set; } = new List<int>();
        public static List<int> SpelarensOmöjligaKortvärden { get; set; } = new List<int>();

        public static List<int> DatornsSvåraKortvärden { get; set; } = new List<int>();
        public static List<int> DatornsLättaKortvärden { get; set; } = new List<int>();
        public static List<int> DatornsVäldigtLättaKortvärden { get; set; } = new List<int>();
    }
}
