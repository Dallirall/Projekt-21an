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
                        spelet.Spelinställningar();

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

