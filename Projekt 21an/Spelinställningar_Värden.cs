using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public static partial class Spelinställningar
    {
        private static int kortMaxVärde = 10;
        public static int KortMaxVärde { get { return kortMaxVärde; } set { kortMaxVärde = value; } }

        private static int kortMinVärde = 1;
        public static int KortMinVärde { get { return kortMinVärde; } set { kortMinVärde = value; } }

        private static EnumVärden.Svårighetsgrader svårighetsgrad = EnumVärden.Svårighetsgrader.Lätt;
        public static EnumVärden.Svårighetsgrader Svårighetsgrad { get { return svårighetsgrad; } set { svårighetsgrad = value; } }

        private static int antalKortAttBörjaMed = 2;
        public static int AntalKortAttBörjaMed { get { return antalKortAttBörjaMed; } set { antalKortAttBörjaMed = value; } }

        private static bool möjligtMedOavgjort = false;
        public static bool MöjligtMedOavgjort { get { return möjligtMedOavgjort; } set { möjligtMedOavgjort = value; } }

        public static int datornSlutarDraKortVid = 21;

        public static int DatornSlutarDraKortVid { get { return datornSlutarDraKortVid; } set { datornSlutarDraKortVid = value; } }


    }
}
