using System;
using System.Data.SqlClient;

namespace _1530Application
{
    public class DbConnection
    {
        public SqlConnection dbConnection;

        public DbConnection()
        {
            dbConnection = new SqlConnection("user id=admin;" +
                                       "password=oakland1530;server=oitdb.ccubo8pyjzvy.us-east-1.rds.amazonaws.com;" +
                                       "Trusted_Connection=no;" +
                                       "database=OitDb; " +
                                       "connection timeout=30");
            try
            {
                dbConnection.Open();
                Console.WriteLine("Connected to Datebase: " + dbConnection.ConnectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlConnection Failed Exception" + e.Message);
            }
        }
    }
}