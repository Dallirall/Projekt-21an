using CardGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projekt_21an.EnumVärden;

namespace Projekt_21an
{
    public static partial class Spelinställningar
    {
        static Spelinställningar()
        {
            InitialiseraDefaultVärden();
        }

        private static void InitialiseraDefaultVärden()
        {
            throw new NotImplementedException();
        }

        public static void Inställningar(IKonsolKortspel spelet)
        {
            Console.WriteLine("Skriv siffra för att välja svårighetsgrad: \r\n1. Lätt \r\n2. Medel \r\n3. Svår \r\n4. Mer eller mindre omöjlig \r\nVill du inte ha en förinställd nivå utan justera inställningar själv, skriv '0'. ");

            Svårighetsgrad = (EnumVärden.Svårighetsgrader)int.Parse(Console.ReadLine());
            switch (Svårighetsgrad)
            {
                case EnumVärden.Svårighetsgrader.Custom:
                    Console.WriteLine($"Välj antal kort som varje spelare ska dra i början av spelet (default 2, max {(int)EnumVärden.SettingsMaxvärden.MaxLimit_AntalKortAttBörjaMed})");
                    AntalKortAttBörjaMed = int.Parse(Console.ReadLine());
                    if (AntalKortAttBörjaMed > (int)EnumVärden.SettingsMaxvärden.MaxLimit_AntalKortAttBörjaMed)
                    {
                        Console.WriteLine($"Input överskred maxvärdet. Ställs in till {(int)EnumVärden.SettingsMaxvärden.MaxLimit_AntalKortAttBörjaMed}");
                        AntalKortAttBörjaMed = (int)EnumVärden.SettingsMaxvärden.MaxLimit_AntalKortAttBörjaMed;
                    }

                    Console.WriteLine($"Välj maxvärdet på korten som dras (default 10, max {(int)EnumVärden.SettingsMaxvärden.MaxLimit_KortMaxVärde})");
                    KortMaxVärde = int.Parse(Console.ReadLine());
                    if (KortMaxVärde > (int)EnumVärden.SettingsMaxvärden.MaxLimit_KortMaxVärde)
                    {
                        Console.WriteLine($"Input överskred maxvärdet. Ställs in till {(int)EnumVärden.SettingsMaxvärden.MaxLimit_KortMaxVärde}");
                        KortMaxVärde = (int)EnumVärden.SettingsMaxvärden.MaxLimit_KortMaxVärde;
                    }
                    int[] kortAttSorteraBort;
                    kortAttSorteraBort = Enumerable.Range((KortMaxVärde + 1),
                                        (Enum.GetValues(typeof(ÖversattKortlek.Kortvalörer)).Length - KortMaxVärde)).ToArray();
                    spelet.Kortspel.Kortlek = spelet.Kortspel.SortAwayUnwantedCards(spelet.Kortspel.Kortlek, new[] { "" }, kortAttSorteraBort);

                    Console.WriteLine($"Välj det värde då datorn ska sluta dra kort (default 21, max {(int)EnumVärden.SettingsMaxvärden.MaxLimit_DatornSlutarDraKortVid})");
                    DatornSlutarDraKortVid = int.Parse(Console.ReadLine());
                    if (DatornSlutarDraKortVid > (int)EnumVärden.SettingsMaxvärden.MaxLimit_DatornSlutarDraKortVid)
                    {
                        Console.WriteLine($"Input överskred maxvärdet. Ställs in till {(int)EnumVärden.SettingsMaxvärden.MaxLimit_DatornSlutarDraKortVid}");
                        DatornSlutarDraKortVid = (int)EnumVärden.SettingsMaxvärden.MaxLimit_DatornSlutarDraKortVid;
                    }
                    break;
                case EnumVärden.Svårighetsgrader.Lätt:
                    AntalKortAttBörjaMed = 2;
                    DatornSlutarDraKortVid = 18;
                    break;
                case EnumVärden.Svårighetsgrader.Medel:
                    AntalKortAttBörjaMed = 2;
                    DatornSlutarDraKortVid = 20;
                    break;
                case EnumVärden.Svårighetsgrader.Svår:
                    AntalKortAttBörjaMed = 2;
                    DatornSlutarDraKortVid = 21;
                    KortMaxVärde = 13;
                    break;
                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    AntalKortAttBörjaMed = 2;
                    DatornSlutarDraKortVid = 21;
                    KortMaxVärde = 13;
                    break;
                default: break;
            }
            Console.WriteLine("Skriv 'j' om du vill att det ska kunna bli oavgjort. Annars skriv 'n', så kommer datorn vinna vid lika resultat.");
            if (Console.ReadLine() == "j")
            {
                MöjligtMedOavgjort = true;
            }
            else
            {
                MöjligtMedOavgjort = false;
            }
            Console.WriteLine("Inställningar sparade!");
            Console.ReadKey();
        }        
    }
}
