using System;
using System.IO;
using System.Collections.Generic;
using Projekt_21an;
using static Projekt_21an._21an_spelet;

namespace spel21an
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            _21an_spelet spelet = new _21an_spelet();
            bool programRunning = true;

            while (programRunning)
            {
                Console.Clear();
                SkrivUtIFärg($"Välkommen till {spelet.SpeletsNamn}!\n", ConsoleColor.Green);
                Console.WriteLine($"Välj ett alternativ\r\n1. Spela {spelet.SpeletsNamn}\r\n2. Visa vinnarstatistik\r\n3. Spelets regler\r\n4. Inställningar\r\n5. Avsluta programmet");
                string menyVal = Console.ReadLine();
                Console.WriteLine("");
                switch (menyVal)
                {
                    case "1":
                        while (true)
                        {
                            spelet.RunGame();
                            Console.WriteLine("\nSpela igen? (j/n): ");
                            if (Console.ReadLine() != "j")
                            {
                                break;
                            }
                        }
                        break;

                    case "2":
                        if (spelet.Vinnare != null)
                        {
                            Console.Write($"Senaste vinnaren: ");
                            SkrivUtIFärg($"{spelet.Vinnare}\n", ConsoleColor.Green);
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Det finns ännu ingen vinnare.");
                            Console.ReadKey();
                        }
                        break;
                    case "3":
                        Console.WriteLine(spelet.Regler);
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.WriteLine("Välj svårighetsgrad: \r\n1. Lätt \r\n2. Medel \r\n3. Svår \r\n4. Mer eller mindre omöjlig \r\nVill du inte ha en förinställd nivå utan justera inställningar själv, skriv '0'. ");
                        spelet.Svårighetsgrad = int.Parse(Console.ReadLine());
                        switch (spelet.Svårighetsgrad)
                        {
                            case 0:
                                //Fixa maxvärden
                                Console.WriteLine("Välj antal kort som varje spelare ska dra i början av spelet (default 2)");
                                spelet.AntalKortAttBörjaMed = int.Parse(Console.ReadLine());
                                Console.WriteLine("Välj maxvärdet på korten som dras (default 10, max 13)");
                                spelet.KortMaxVärde = int.Parse(Console.ReadLine());
                                Console.WriteLine("Välj det värde då datorn ska sluta dra kort (default 21, max 21)");
                                spelet.DatornSlutarDraKortVid = int.Parse(Console.ReadLine());
                                break;
                            case 1:
                                spelet.AntalKortAttBörjaMed = 2;
                                spelet.DatornSlutarDraKortVid = 18;
                                spelet.KortMaxVärde = 10;
                                break;
                            case 2:
                                spelet.AntalKortAttBörjaMed = 2;
                                spelet.DatornSlutarDraKortVid = 20;
                                spelet.KortMaxVärde = 10;
                                break;
                            case 3:
                                spelet.AntalKortAttBörjaMed = 2;
                                spelet.DatornSlutarDraKortVid = 21;
                                spelet.KortMaxVärde = 13;
                                break;
                            case 4:
                                spelet.AntalKortAttBörjaMed = 2;
                                spelet.DatornSlutarDraKortVid = 21;
                                spelet.KortMaxVärde = 13;
                                break;
                            default: break;
                        }
                        Console.WriteLine("Skriv 'ja' om du vill att det ska kunna bli oavgjort. Annars skriv 'nej', så kommer datorn vinna vid lika resultat.");
                        if (Console.ReadLine() == "ja")
                        {
                            spelet.MöjligtMedOavgjort = true;
                        }
                        else
                        {
                            spelet.MöjligtMedOavgjort = false;
                        }
                        Console.WriteLine("Inställningar sparade!");
                        Console.ReadKey();
                        break;
                    case "5":
                        SkrivUtIFärg("Tack för att du spelade!", ConsoleColor.Green);
                        Console.ReadKey();
                        programRunning = false;
                        break;
                    default:
                        break;
                }

            }


        }
        public static void SkrivUtIFärg(string textAttSkrivaUt, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(textAttSkrivaUt);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}

