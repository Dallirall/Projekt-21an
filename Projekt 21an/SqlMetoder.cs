using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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

            }

        }

        public static void RegistreraNySpelareIDatabasen(Spelare nySpelare)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string insertQuery = "INSERT INTO vinststatistik (namn) VALUES (@namn);";

                var affectedRows = connection.Execute(insertQuery, new {namn = $"{nySpelare.Namn}"});
            }
        }

        public static void RegistreraResultatIDatabasen(bool oavgjort, Spelare vinnare, Spelare förlorare)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string updateQuery = "";

                if (oavgjort)
                {
                    updateQuery = $"UPDATE vinststatistik SET oavgjort = oavgjort + 1 WHERE namn IN (@vinnare, @förlorare)";
                    connection.Execute(updateQuery, new { vinnare = $"{vinnare.Namn}", förlorare = $"{förlorare.Namn}"}); ;
                }
                else
                {
                    updateQuery = $"UPDATE vinststatistik SET vinster = vinster + 1 WHERE namn = @namn";
                    connection.Execute(updateQuery, new {namn = $"{vinnare.Namn}"});

                    updateQuery = $"UPDATE vinststatistik SET förluster = förluster + 1 WHERE namn = @namn";
                    connection.Execute(updateQuery, new {namn = $"{förlorare.Namn}"});
                }
            }

            
        }

        //ToDo: Se om jag kan öva LINQ här.
        public static bool ExistsInDatabaseCheck(string spelarnamn)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string selectQuery = "SELECT namn FROM vinststatistik";

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
