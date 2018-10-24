using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _1530Application
{
    public class DbConnection
    {
        SqlConnection dbConnection;
        public DbConnection()
        {
            dbConnection = new SqlConnection("user id=admin;" +
                                       "password=oakland1530;server=oitdb.ccubo8pyjzvy.us-east-1.rds.amazonaws.com,1433;" +
                                       "Trusted_Connection=yes;" +
                                       "database=OitDb; " +
                                       "connection timeout=30");
            try
            {
                dbConnection.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("SqlConnection Failed Exception" + e.Message);
            }
        }
        ~DbConnection()
        {
            Console.WriteLine("Closing Sql Connection");
            dbConnection.Close();
        }
    }
}