using CardGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public static class _21an_Metoder
    {

        public static bool HarÖverskridit21poäng(bool datornHarFörlorat, bool duHarFörlorat, Spelare spelaren, Spelare datorn)
        {
            if (duHarFörlorat && datornHarFörlorat)
            {
                if (Spelinställningar.MöjligtMedOavgjort)
                {
                    RegistreraVinnaren(EnumVärden.Resultat.BådaÖver21, spelaren, datorn);
                }
                else
                {
                    RegistreraVinnaren(EnumVärden.Resultat.PoängÖverskridit21, datorn, spelaren);
                }
                return true;
            }
            else if (duHarFörlorat)
            {
                RegistreraVinnaren(EnumVärden.Resultat.PoängÖverskridit21, datorn, spelaren);
                return true;
            }
            else if (datornHarFörlorat)
            {
                RegistreraVinnaren(EnumVärden.Resultat.PoängÖverskridit21, spelaren, datorn);
                return true;
            }
            return false;
        }

        public static bool CheckaVinstConditions(Spelare datorn, Spelare spelare)
        {

            if (datorn.Poäng > spelare.Poäng)
            {
                RegistreraVinnaren(EnumVärden.Resultat.PoängNärmast21, datorn, spelare);
                return true;
            }
            else if (datorn.Poäng < Spelinställningar.DatornSlutarDraKortVid)
            {
                return false;
            }
            else if (datorn.Poäng < spelare.Poäng)
            {
                Console.WriteLine("\nDatorn väljer att inte dra fler kort. \n");
                RegistreraVinnaren(EnumVärden.Resultat.PoängNärmast21, spelare, datorn);
                return true;
            }
            else if (datorn.Poäng == spelare.Poäng)
            {
                if (Spelinställningar.MöjligtMedOavgjort)
                {
                    RegistreraVinnaren(EnumVärden.Resultat.BådaSammaPoäng, spelare, datorn);
                    return true;
                }
                else
                {
                    RegistreraVinnaren(EnumVärden.Resultat.BådaSammaPoäng, datorn, spelare);
                    return true;
                }
            }
            return false;
        }

        public static void RegistreraVinnaren(EnumVärden.Resultat resultat, Spelare vinnare, Spelare förlorare)
        {
            bool oavgjort = false;
            switch (resultat)
            {
                case EnumVärden.Resultat.PoängÖverskridit21:
                    if (förlorare.Namn == "Datorn")
                    {
                        Console.WriteLine("Datorns poäng har överskridit 21.");
                        StringManipulationMethods.SkrivUtIFärg($"Grattis {vinnare.Namn}! Du har vunnit!\n", ConsoleColor.DarkCyan);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Din poäng har överskridit 21.");
                        StringManipulationMethods.SkrivUtIFärg("Du har förlorat.\n", ConsoleColor.DarkRed);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    break;
                case EnumVärden.Resultat.PoängNärmast21:
                    if (vinnare.Namn == "Datorn")
                    {
                        Console.WriteLine("Datorns poäng är närmast 21. ");
                        StringManipulationMethods.SkrivUtIFärg("Datorn har vunnit!", ConsoleColor.DarkRed);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Din poäng är närmast 21. ");
                        StringManipulationMethods.SkrivUtIFärg($"Grattis {vinnare.Namn}! Du har vunnit!\n", ConsoleColor.DarkCyan);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    break;
                case EnumVärden.Resultat.BådaÖver21:
                    Console.WriteLine("Både din och datorns poäng har överskridit 21.");
                    StringManipulationMethods.SkrivUtIFärg("Det blev oavgjort.\n", ConsoleColor.DarkGray);
                    oavgjort = true;
                    SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    break;
                case EnumVärden.Resultat.BådaSammaPoäng:
                    if (Spelinställningar.MöjligtMedOavgjort)
                    {
                        Console.WriteLine("Du och datorn har landat på samma poäng.");
                        StringManipulationMethods.SkrivUtIFärg("Det blev oavgjort.\n", ConsoleColor.DarkGray);
                        oavgjort = true;
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Samma poäng. Då vinner ");
                        StringManipulationMethods.SkrivUtIFärg("datorn\n", ConsoleColor.DarkRed);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }

        public static void PrintaDragetKort(string spelare, Card dragetKort)
        {
            if (spelare == "Datorn")
            {
                Console.Write($"{spelare} drog ");
            }
            else
            {
                Console.Write("Du drog ");
            }
            StringManipulationMethods.SkrivUtIFärg($"{dragetKort.CardName}", ConsoleColor.DarkCyan);
            Console.Write(". ");
        }

        public static int RandomCardTillSpelaren(ÖversattKortlek kortspel)
        {
            Random rnd = new Random();
            Card dragetKort = new();


            switch (Spelinställningar.Svårighetsgrad)
            {
                
                case EnumVärden.Svårighetsgrader.Lätt:
                    if (rnd.NextDouble() < 0.5)
                    {
                        dragetKort = kortspel.DrawCardOfSpecificValues(new[] { 1, 2 });
                    }
                    else
                    {
                        dragetKort = kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Custom:

                case EnumVärden.Svårighetsgrader.Medel:
                    dragetKort = kortspel.DrawACardFromDeck();
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Svår:
                    if (rnd.NextDouble() < 0.15)
                    {
                        int[] svåraKort = Enumerable.Range(6, (Spelinställningar.KortMaxVärde - 6 + 1)).ToArray();
                        dragetKort = kortspel.DrawCardOfSpecificValues(svåraKort);
                    }
                    else
                    {
                        dragetKort = kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    double procent = rnd.NextDouble();
                    if (procent < 0.1)
                    {
                        int[] svåraKort = Enumerable.Range(9, (Spelinställningar.KortMaxVärde - 9 + 1)).ToArray();
                        dragetKort = kortspel.DrawCardOfSpecificValues(svåraKort);
                    }
                    else if (procent < 0.5)
                    {
                        int[] svåraKort = Enumerable.Range(6, (Spelinställningar.KortMaxVärde - 6 + 1)).ToArray();
                        dragetKort = kortspel.DrawCardOfSpecificValues(svåraKort);
                    }
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;

                default:
                    dragetKort = kortspel.DrawACardFromDeck();
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;
            }
        }
        //svårighetsgrader: Lätt = 50% chans för mina kort att bli 1-2, 10% för datorn att bli över 3. Datorn drar kort till 18. Kortmaxvärde 10.
        //Medel = Defaultläge, slutardrakortvid 20
        //Svår = maxvärde kort: 13, slutardrakortvid 21,  15% chans för mina kort att bli över 5
        //Omöjlig = 50% mina kort över 5, 10% över 8, datorn 40% under 3, 70% under 5
        public static int RandomCardTillDatorn(ÖversattKortlek kortspel)
        {
            Random rnd = new Random();
            Card dragetKort = new();

            switch (Spelinställningar.Svårighetsgrad)
            {

                case EnumVärden.Svårighetsgrader.Lätt:
                    if (rnd.NextDouble() < 0.1)
                    {
                        int[] svåraKort = Enumerable.Range(4, (Spelinställningar.KortMaxVärde - 4 + 1)).ToArray();
                        dragetKort = kortspel.DrawCardOfSpecificValues(svåraKort);
                    }
                    else
                    {
                        dragetKort = kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Datorn", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Custom:

                case EnumVärden.Svårighetsgrader.Medel:

                case EnumVärden.Svårighetsgrader.Svår:
                    dragetKort = kortspel.DrawACardFromDeck();
                    PrintaDragetKort("Datorn", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    double procent = rnd.NextDouble();
                    if (procent < 0.4)
                    {
                        dragetKort = kortspel.DrawCardOfSpecificValues(new[] { 1, 2 });
                    }
                    else if (procent < 0.7)
                    {
                        dragetKort = kortspel.DrawCardOfSpecificValues(new[] { 1, 2, 3, 4 });
                    }
                    else
                    {
                        dragetKort = kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Datorn", dragetKort);
                    return dragetKort.CardValue;

                default:
                    dragetKort = kortspel.DrawACardFromDeck();
                    PrintaDragetKort("Datorn", dragetKort);
                    return dragetKort.CardValue;
            }
        }

        
    }
}
