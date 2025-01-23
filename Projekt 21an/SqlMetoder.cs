using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static Projekt_21an.EnumVärden;
using spel21an;
using Projekt_21an.PathsForPlatforms;

namespace Projekt_21an
{
    public delegate bool CheckInDatabaseDel(string spelarnamn);
    public static class SqlMetoder
    {
        private static string _databaseFileName = "Projekt_21an_sqliteDB.db";
        public static string DatabaseFileName { get { return _databaseFileName; } }
        public static string DatabaseLocationPath { get; private set; }

        public static string ConnectionString { get; private set; }

        static SqlMetoder()
        {
            if (DatabaseLocationPath == null)
            {
                InitializeDatabaseLocationPath();
            }

            if (ConnectionString == null)
            {
                try
                {
                    SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder()
                    {
                        DataSource = DatabaseLocationPath,
                        Mode = SqliteOpenMode.ReadWriteCreate,
                        Cache = SqliteCacheMode.Shared
                    };
                    ConnectionString = builder.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }

                
                //ConnectionString = 
            }
        }

        private static void InitializeDatabaseLocationPath()
        {
            string databaseSourcePath = Path.Combine(PlatformPaths.CurrentPlatform.GetBaseDirectoryPath(), DatabaseFileName);

        }

        public static void DisplayaVinststatistik()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                
                string selectQuery = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'vinststatistik'";
                string[] kolumner = connection.Query<string>(selectQuery).ToArray();
                
                selectQuery = "SELECT * FROM vinststatistik";
                List<Spelare> spelareLista = connection.Query<Spelare>(selectQuery).ToList();
                
                if (spelareLista.Count > 1 )
                {
                    StringManipulationMethods.SkrivUtIFärg("\nVinststatistik\n\n", ConsoleColor.DarkMagenta);
                    foreach (string kolumn in kolumner)
                    {
                        StringManipulationMethods.SkrivUtIFärg($"{StringManipulationMethods.CapitalizeFirstLetter(kolumn)}\t\t", ConsoleColor.DarkBlue);
                    }
                    Console.WriteLine("");
                    foreach (Spelare spelare in spelareLista)
                    {
                        Console.WriteLine($"{spelare.Namn}\t\t{spelare.Vinster}\t\t{spelare.Förluster}\t\t\t{spelare.Oavgjort}\n");
                    }
                }
                else
                {
                    Console.WriteLine("Finns ingen statistik ännu! Spela spelet först.");
                }
            }
        }

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

        public static bool ExistsInDatabaseCheck(string spelarnamn)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {                
                string selectQuery = "SELECT COUNT(1) FROM vinststatistik WHERE namn = @Namn";

                int count = connection.ExecuteScalar<int>(selectQuery, new {Namn = spelarnamn});
                return count > 0;
            }                  
        }        
    }
}
