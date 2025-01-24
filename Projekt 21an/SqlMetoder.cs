using Microsoft.Data.Sqlite;
using System.IO;
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
        
        private static string _databaseFolderName = "21an_Data";
        public static string DatabaseFolderName { get { return _databaseFolderName; } }
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

            }
        }

        private static void InitializeDatabaseLocationPath()
        {
            string databaseSourceFilePath = Path.Combine(PlatformPaths.CurrentPlatform.GetBaseDirectoryPath(), DatabaseFileName);
            string destinationFolder = Path.Combine(PlatformPaths.CurrentPlatform.GetAppDataFolderPath(), DatabaseFolderName);

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            string destinationFilePath = Path.Combine(destinationFolder, DatabaseFileName);

            if (!File.Exists(destinationFilePath))
            {
                if (File.Exists(databaseSourceFilePath))
                {
                    File.Copy(databaseSourceFilePath, destinationFilePath);
                    Console.WriteLine("\nFile copied! \n");
                }
                else 
                {
                    Console.WriteLine("\nError: Source file not found. \n");
                }
            }

            DatabaseLocationPath = destinationFilePath;
        }

        public static void DisplayaVinststatistik()
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                
                string selectQuery = "PRAGMA table_info(vinststatistik);";
                string[] kolumner = connection.Query(selectQuery).Select(row => (string)row.name).ToArray();
                
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
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                string insertQuery = "INSERT INTO vinststatistik (Namn) VALUES (@Namn);";

                connection.Execute(insertQuery, new {Namn = $"{nySpelare.Namn}"});
            }
        }

        public static void RegistreraResultatIDatabasen(bool oavgjort, Spelare vinnare, Spelare förlorare)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
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
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {                
                string selectQuery = "SELECT COUNT(1) FROM vinststatistik WHERE namn = @Namn";

                int count = connection.ExecuteScalar<int>(selectQuery, new {Namn = spelarnamn});
                return count > 0;
            }                  
        }        
    }
}
