using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Projekt_21an
{
    public delegate bool ExistsInDatabase(string namn);
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

        public static void RegistreraResultatIDatabasen(string resultat, ExistsInDatabase existsDel)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string insertQuery = $"INSERT INTO vinststatistik (namn) VALUES ({Spelare.Namn})";

                connection.Query(insertQuery);
            }

            
        }

        //ToDo: Se om jag kan öva LINQ här.
        public static bool ExistsInDatabaseCheck(string namn)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string selectQuery = "SELECT namn FROM vinststatistik";

                List<string> spelareNamn = connection.Query<string>(selectQuery).ToList();
                if (spelareNamn.Contains(namn))
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
