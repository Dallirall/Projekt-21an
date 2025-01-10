using System;
using System.IO;
using System.Collections.Generic;
using Projekt_21an;
using static Projekt_21an._21an_spelet;
using Microsoft.Data.SqlClient;
using Dapper;
using CardGames;
using Projekt_21an.ÖversättningAvKortlek;

namespace spel21an
{
    public class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
            //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //    builder.DataSource = "DESKTOP-US1L7JK.database.windows.net";
            //    //builder.UserID = "sa";
            //    //builder.Password = "plokij";
            //    builder.InitialCatalog = "_21anDB";
            //    builder.TrustServerCertificate = true;

            //    using (SqlConnection connection = new SqlConnection())
            //    {
            //        Console.WriteLine("\nQuery data example:");
            //        Console.WriteLine("=========================================\n");

            //        String sql = "SELECT namn FROM vinststatistik";

            //        using (SqlCommand command = new SqlCommand(sql, connection))
            //        {
            //            connection.Open();
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (SqlException e)
            //{
            //    Console.WriteLine(e.ToString());
            //}

            //string result = (SqlMetoder.ExistsInDatabaseCheck("Datorn")) ? "Datorn finns i databasen" : "Finns ej"; 
            //Console.WriteLine(result);
            //Console.ReadKey();

            //Spelare datorn = new Spelare("Datorn");
            //Spelare test = new Spelare("Test");
            //SqlMetoder.ExistsInDatabaseCheck(test.Namn);
            //SqlMetoder.DisplayaVinststatistik();

            //SqlMetoder.TestaLite();

            CardDeck cardDeck = new CardDeck();
            cardDeck.DisplayDrawnCardValues();

            var kortlek = new ÖversättningAvKortlek();
            kortlek.DisplayDrawnCardValues();

            Console.ReadKey();


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
                        Spelinställningar.Inställningar();
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

