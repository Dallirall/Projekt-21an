using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Projekt_21an
{
    public static class SqlMetoder
    {


        public static string connectionString = @"Server=.;Database=_21anDB;Trusted_Connection=True;TrustServerCertificate=True;";
        public static string ConnectionString { get; }

        public static void DisplayaVinststatistik()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

        }

        public static void RegistreraResultatIDatabasen(string resultat)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string insertQuery = $"INSERT INTO vinststatistik (namn) VALUES ({Spelare.Namn})";

            connection.Query(insertQuery);
        }



        
    }
}
