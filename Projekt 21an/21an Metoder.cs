﻿using CardGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public partial class _21an_spelet
    {

        public bool HarÖverskridit21poäng(bool datornHarFörlorat, bool duHarFörlorat, Spelare spelaren, Spelare datorn)
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

        public bool CheckaVinstConditions(Spelare datorn, Spelare spelare)
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

        public void RegistreraVinnaren(EnumVärden.Resultat resultat, Spelare vinnare, Spelare förlorare)
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

        public void PrintaDragetKort(string spelare, Card dragetKort)
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

        public int RandomCardTillSpelaren()
        {
            Random rnd = new Random();
            Card dragetKort = new();


            switch (Spelinställningar.Svårighetsgrad)
            {                
                case EnumVärden.Svårighetsgrader.Lätt:
                    if (rnd.NextDouble() < Spelinställningar.ProcentsatsLättTillSpelaren)
                    {
                        dragetKort = Kortspel.DrawCardOfSpecificValues(Spelinställningar.SpelarensLättaKortvärden);
                    }
                    else
                    {
                        dragetKort = Kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Custom:

                case EnumVärden.Svårighetsgrader.Medel:
                    dragetKort = Kortspel.DrawACardFromDeck();
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Svår:
                    if (rnd.NextDouble() < Spelinställningar.ProcentsatsSvårTillSpelaren)
                    {
                        dragetKort = Kortspel.DrawCardOfSpecificValues(Spelinställningar.SpelarensSvåraKortvärden);
                    }
                    else
                    {
                        dragetKort = Kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    double procent = rnd.NextDouble();
                    if (procent < Spelinställningar.ProcentsatsOmöjligTillSpelarenLägre)
                    {
                        dragetKort = Kortspel.DrawCardOfSpecificValues(Spelinställningar.SpelarensOmöjligaKortvärden);
                    }
                    else if (procent < Spelinställningar.ProcentsatsOmöjligTillSpelarenHögre)
                    {
                        dragetKort = Kortspel.DrawCardOfSpecificValues(Spelinställningar.SpelarensSvåraKortvärden);
                    }
                    else
                    {
                        dragetKort = Kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;

                default:
                    dragetKort = Kortspel.DrawACardFromDeck();
                    PrintaDragetKort("Du", dragetKort);
                    return dragetKort.CardValue;
            }
        }
        //Svårighetsgrader: Lätt = 50% chans för mina kort att bli 1-2, 10% för datorn att bli över 3. Datorn drar kort till 18. Kortmaxvärde 10.
        //Medel = Defaultläge, slutar dra kort vid 20.
        //Svår = Maxvärde kort: 13, slutar dra kort vid 21,  15% chans för mina kort att bli över 5.
        //Omöjlig = 50% chans för mina kort att bli över 5, 10% chans för över 8. Datorn 40% chans för kort under 3, 70% chans för under 5.
        public int RandomCardTillDatorn()
        {
            Random rnd = new Random();
            Card dragetKort = new();

            switch (Spelinställningar.Svårighetsgrad)
            {
                case EnumVärden.Svårighetsgrader.Lätt:
                    if (rnd.NextDouble() < Spelinställningar.ProcentsatsLättTillDatorn)
                    {                        
                        dragetKort = Kortspel.DrawCardOfSpecificValues(Spelinställningar.DatornsSvåraKortvärden);
                    }
                    else
                    {
                        dragetKort = Kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Datorn", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Custom:

                case EnumVärden.Svårighetsgrader.Medel:

                case EnumVärden.Svårighetsgrader.Svår:
                    dragetKort = Kortspel.DrawACardFromDeck();
                    PrintaDragetKort("Datorn", dragetKort);
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    double procent = rnd.NextDouble();
                    if (procent < Spelinställningar.ProcentsatsOmöjligTillDatornLägre)
                    {
                        dragetKort = Kortspel.DrawCardOfSpecificValues(Spelinställningar.DatornsVäldigtLättaKortvärden);
                    }
                    else if (procent < Spelinställningar.ProcentsatsOmöjligTillDatornHögre)
                    {
                        dragetKort = Kortspel.DrawCardOfSpecificValues(Spelinställningar.DatornsLättaKortvärden);
                    }
                    else
                    {
                        dragetKort = Kortspel.DrawACardFromDeck();
                    }
                    PrintaDragetKort("Datorn", dragetKort);
                    return dragetKort.CardValue;

                default:
                    dragetKort = Kortspel.DrawACardFromDeck();
                    PrintaDragetKort("Datorn", dragetKort);
                    return dragetKort.CardValue;
            }
        }

        
    }
}
