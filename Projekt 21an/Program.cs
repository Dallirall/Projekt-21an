using System;
using System.IO;
using System.Collections.Generic;
using Projekt_21an;
using static Projekt_21an._21an_spelet;
using Microsoft.Data.SqlClient;
using Dapper;
using CardGames;

//ToDo: Borde jag ha en variable istället för att hardcoda kortvalue leveln för när man drar kort i svår/lätt svårighetsgrad? Och för procentsatserna?

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
                StringManipulationMethods.SkrivUtIFärg($"Välkommen till {spelet.SpeletsNamn}!\n", ConsoleColor.Green);
                Console.WriteLine($"Välj ett alternativ\r\n1. Spela {spelet.SpeletsNamn}\r\n2. Visa vinnarstatistik\r\n3. Spelets regler\r\n4. Inställningar\r\n5. Avsluta programmet");
                EnumVärden.StartmenyVal menyVal = (EnumVärden.StartmenyVal)int.Parse(Console.ReadLine());
                Console.WriteLine("");
                switch (menyVal)
                {
                    case EnumVärden.StartmenyVal.Val_spela_spelet:
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

                    case EnumVärden.StartmenyVal.Val_visa_vinnarstatistik:
                        SqlMetoder.DisplayaVinststatistik();
                        Console.ReadKey();
                        break;

                    case EnumVärden.StartmenyVal.Val_spelets_regler:
                        Console.WriteLine(spelet.Regler);
                        Console.ReadKey();
                        break;

                    case EnumVärden.StartmenyVal.Val_inställningar:
                        Spelinställningar.Inställningar(spelet);
                        break;

                    case EnumVärden.StartmenyVal.Val_avsluta_programmet:
                        StringManipulationMethods.SkrivUtIFärg("Tack för att du spelade!", ConsoleColor.Green);
                        Console.ReadKey();
                        programRunning = false;
                        break;

                    default:
                        break;
                }
            }
        }
    }    
}

