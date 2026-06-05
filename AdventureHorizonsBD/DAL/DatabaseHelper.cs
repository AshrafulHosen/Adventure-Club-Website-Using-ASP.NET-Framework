using System;
using System.Configuration;
using System.Data.SqlClient;

namespace AdventureHorizonsBD.DAL
{
    public static class DatabaseHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AdventureClubDB"].ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
}
