using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public class EnumVärden
    {
        public enum Svårighetsgrader
        {
            Custom = 0,
            Lätt,
            Medel,
            Svår,
            Mer_eller_mindre_omöjlig
        }

        public enum SettingsMaxvärden
        {
            MaxLimit_AntalKortAttBörjaMed = 5,
            MaxLimit_KortMaxVärde = 13,
            MaxLimit_DatornSlutarDraKortVid = 21
        }

        public enum Resultat
        {
            PoängÖverskridit21,
            PoängNärmast21,
            BådaÖver21,
            BådaSammaPoäng
            //SammaPoängOavgjortDisabled
        }
    }
}
