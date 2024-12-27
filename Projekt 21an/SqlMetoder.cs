﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static Projekt_21an.EnumVärden;
using spel21an;

namespace Projekt_21an
{
    public delegate bool CheckInDatabaseDel(string spelarnamn);
    public static class SqlMetoder
    {


        private static string _connectionString = @"Server=.;Database=_21anDB;Trusted_Connection=True;TrustServerCertificate=True;";
        public static string ConnectionString { get { return _connectionString; } }

        public static void DisplayaVinststatistik()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string selectQuery = "SELECT * FROM vinststatistik";

                List<Spelare> spelareLista = connection.Query<Spelare>(selectQuery).ToList();
                if (spelareLista.Count > 1 )
                {
                    Console.WriteLine("\nVinststatistik\n\n");
                    Program.SkrivUtIFärg($"Namn\t\tVinster\t\tFörluster\tOavgjort\n\n", ConsoleColor.DarkBlue);
                    foreach (Spelare spelare in spelareLista)
                    {
                        Console.WriteLine($"{spelare.Namn}\t\t{spelare.Vinster}\t\t{spelare.Förluster}\t\t{spelare.Oavgjort}\n");
                    }
                }
                else
                {
                    Console.WriteLine("Finns ingen statistik ännu! Spela spelet först.");
                }
            }
        }

        //public static void TestaLite()
        //{


        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
                
        //        string selectQuery = "SELECT * FROM vinststatistik";
        //        IEnumerable<Spelare> result = connection.Query<Spelare>(selectQuery);
        //        Console.WriteLine("Namn\t\tVinster\tFörluster\tOavgjort\n");
        //        foreach (Spelare spelare in result)
        //        {
        //            Console.WriteLine($"{spelare.Namn}\t\t{spelare.Vinster}\t{spelare.Förluster}\t\t{spelare.Oavgjort}\n");
        //        }
        //        Console.ReadKey();
        //    }
        //}

        public static void RegistreraNySpelareIDatabasen(Spelare nySpelare)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string insertQuery = "INSERT INTO vinststatistik (Namn) VALUES (@Namn);";

                connection.Execute(insertQuery, new {Namn = $"{nySpelare.Namn}"});
            }
        }

        public static void RegistreraResultatIDatabasen(bool oavgjort, Spelare vinnare, Spelare förlorare)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string updateQuery = "";

                if (oavgjort)
                {
                    updateQuery = $"UPDATE vinststatistik SET Oavgjort = Oavgjort + 1 WHERE Namn IN (@Vinnare, @Förlorare)";
                    connection.Execute(updateQuery, new { Vinnare = $"{vinnare.Namn}", Förlorare = $"{förlorare.Namn}"}); ;
                }
                else
                {
                    updateQuery = $"UPDATE vinststatistik SET Vinster = Vinster + 1 WHERE Namn = @Namn";
                    connection.Execute(updateQuery, new {Namn = $"{vinnare.Namn}"});

                    updateQuery = $"UPDATE vinststatistik SET Förluster = Förluster + 1 WHERE Namn = @Namn";
                    connection.Execute(updateQuery, new {Namn = $"{förlorare.Namn}"});
                }
            }

            
        }

        //ToDo: Se om jag kan öva LINQ här.
        public static bool ExistsInDatabaseCheck(string spelarnamn)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string selectQuery = "SELECT Namn FROM vinststatistik";

                List<string> spelareNamn = connection.Query<string>(selectQuery).ToList();
                if (spelareNamn.Contains(spelarnamn))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            
        }

        
    }
}
