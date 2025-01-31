using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public static partial class Spelinställningar
    {
        private static int _kortMaxVärde = 10;
        public static int KortMaxVärde { get { return _kortMaxVärde; } set { _kortMaxVärde = value; } }

        private static int _kortMinVärde = 1;
        public static int KortMinVärde { get { return _kortMinVärde; } set { _kortMinVärde = value; } }

        private static EnumVärden.Svårighetsgrader _svårighetsgrad = EnumVärden.Svårighetsgrader.Lätt;
        public static EnumVärden.Svårighetsgrader Svårighetsgrad { get { return _svårighetsgrad; } set { _svårighetsgrad = value; } }

        private static int _antalKortAttBörjaMed = 2;
        public static int AntalKortAttBörjaMed { get { return _antalKortAttBörjaMed; } set { _antalKortAttBörjaMed = value; } }

        private static bool _möjligtMedOavgjort = false;
        public static bool MöjligtMedOavgjort { get { return _möjligtMedOavgjort; } set { _möjligtMedOavgjort = value; } }

        public static int _datornSlutarDraKortVid = 21;



        public static int DatornSlutarDraKortVid { get { return _datornSlutarDraKortVid; } set { _datornSlutarDraKortVid = value; } }

        private static double _procentsatsLättTillSpelaren = 0.5;
        public static double ProcentsatsLättTillSpelaren { get { return _procentsatsLättTillSpelaren; } set { _procentsatsLättTillSpelaren = value; } }

        private static double _procentsatsSvårTillSpelaren = 0.15;
        public static double ProcentsatsSvårTillSpelaren { get { return _procentsatsSvårTillSpelaren; } set { _procentsatsSvårTillSpelaren = value; } }

        private static double _procentsatsOmöjligTillSpelarenLägre = 0.1;
        public static double ProcentsatsOmöjligTillSpelarenLägre { get { return _procentsatsOmöjligTillSpelarenLägre; } set { _procentsatsOmöjligTillSpelarenLägre = value; } }

        private static double _procentsatsOmöjligTillSpelarenHögre = 0.5;
        public static double ProcentsatsOmöjligTillSpelarenHögre { get { return _procentsatsOmöjligTillSpelarenHögre; } set { _procentsatsOmöjligTillSpelarenHögre = value; } }


        private static double _procentsatsLättTillDatorn = 0.1;
        public static double ProcentsatsLättTillDatorn { get { return _procentsatsLättTillDatorn; } set { _procentsatsLättTillDatorn = value; } }

        private static double _procentsatsOmöjligTillDatornLägre = 0.4;
        public static double ProcentsatsOmöjligTillDatornLägre { get { return _procentsatsOmöjligTillDatornLägre; } set { _procentsatsOmöjligTillDatornLägre = value; } }

        private static double _procentsatsOmöjligTillDatornHögre = 0.7;
        public static double ProcentsatsOmöjligTillDatornHögre { get { return _procentsatsOmöjligTillDatornHögre; } set { _procentsatsOmöjligTillDatornHögre = value; } }


        public static int[] SpelarensLättaKortvärden { get; set; }

    }
}
