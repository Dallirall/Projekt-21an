using System;
using System.IO;
using System.Collections.Generic;
using Projekt_21an;
using static Projekt_21an._21an_spelet;
using Microsoft.Data.SqlClient;
using Dapper;

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

