using _1530Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;

namespace _1530Application
{
    public class DbConnection1530
    {
        public SqlConnection dbConnection;
        private Dictionary<String, String[]> tableRestrictions; // Keep track of what each table is named and what can be queried upon

        public DbConnection1530()
        {
            tableRestrictions = new Dictionary<string, string[]>();
            dbConnection = new SqlConnection("user id=admin;" +
                                       "password=oakland1530;server=oitdb.ccubo8pyjzvy.us-east-1.rds.amazonaws.com;" +
                                       "Trusted_Connection=no;" +
                                       "database=OITDB_v2; " +
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
            List<String> tableNames = new List<string>();
            DataTable tables = dbConnection.GetSchema("Tables");
            foreach (DataRow row in tables.Rows)
            {
                string tablename = (string)row[2];
                tableNames.Add(tablename);
            }
            foreach (String row in tableNames)//get names and columns for each table in the database
            {
                dbConnection.Close();
                dbConnection.Open();
                var command = new SqlCommand("SELECt * FROM dbo." + row + " WHERE 1 =2;", dbConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read() || reader.VisibleFieldCount != 0)
                {
                    List<string> colNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        colNames.Add(reader.GetName(i));
                    }
                    tableRestrictions[row] = colNames.ToArray();
                    break;
                }
            }
            Console.WriteLine("Done finding table restrictions");
            dbConnection.Close();
        }

        public void InsertUser(string payload)
        {
            dbConnection.Open();
            string query = "INSERT INTO Users (Email, Password, AdminAccess, ListingAmounts, ListScore)";
            query = query + "VALUES (@email, @password, @adminAccess, @listingAmounts, @listScore)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            //command.Parameters.Add("@email", SqlDbType.String);
            //command.Parameters.Add("@password", SqlDbType.String);
            //command.Parameters.Add("@adminAcccess", SqlDbType.String);
            command.Parameters.Add("@listingAmounts", SqlDbType.Int);
            command.Parameters.Add("@listScore", SqlDbType.Int);

            // will probably need to convert payload from string to json or someting liek that

            //command.Parameters["@email"].Value = payload.something;
            //command.Parameters["@password"].Value = payload.something2;
            //command.Parameters["@adminAccess"].Value = payload.something3;
            //command.Parameters["@listingAmounts"].Value = payload.something4;
            //command.Parameters["@listScore"].Value = payload.something5;

            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns></returns>
        public string SearchUser()
        {
            dbConnection.Open();
            string query = "SELECT * FROM dbo.Users;";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                Console.WriteLine("User Rows Below");
                while (reader.Read())
                {
                    Console.WriteLine("*** New Row ***");
                    for (int x = 0; x < reader.FieldCount; x++)
                    {
                        Console.WriteLine(reader.GetName(x) + " : " + reader[x]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error retrieving users");
            }
            dbConnection.Close();
            return null;
        }

        /// <summary>
        /// Return a queried list of users to be selected upon
        /// </summary>
        /// <param name="payload">
        /// Dictonary of query paramters to query based on
        /// </param>
        /// <returns></returns>
        public string SearchUser(Dictionary<string, string> payload)
        {
            string query = "SELECT * FROM dbo.Users WHERE " +
                string.Join(",", payload.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + ";";

            SqlCommand command = new SqlCommand(query, dbConnection);
            //command.Parameters.Add("@email", SqlDbType.String);

            //command.Parameters["@email"].Value = payload.something;
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    // do something
                }
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public string InsertMapListing(Dictionary<string, string> entries)
        {
            Random rnd = new Random();
            dbConnection.Open();
            string query = String.Format("INSERT INTO dbo.MapListings ([listID],[Xcoord] ,[Ycoord] ,[description],[iType],[voteVal],[creator]) VALUES({0}, {1}, {2}, '{3}', '{4}', {5},'{6}')",
                                            rnd.Next(0, int.MaxValue),
                                            entries["Xcoord"],
                                            entries["Ycoord"],
                                            entries["Description"],
                                            entries["Image"],
                                            entries["Upvotes"],
                                            entries["Creator"]);

            SqlCommand command = new SqlCommand("proc_addListing", dbConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Xcoord", entries["Xcoord"]));
            command.Parameters.Add(new SqlParameter("@Ycoord", entries["Ycoord"]));
            command.Parameters.Add(new SqlParameter("@description", entries["Description"]));
            command.Parameters.Add(new SqlParameter("@iType", entries["Image"]));
            command.Parameters.Add(new SqlParameter("@creator", entries["Creator"]));
            command.Parameters.Add(new SqlParameter("@allTags", entries["Tags"]));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
            dbConnection.Close();
            return null;

        }

        /// <summary>
        /// Return all Map Listings
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<MapListing> SearchMapListings()
        {

            SqlConnection dbConnection = new SqlConnection("user id=admin;" +
                                       "password=oakland1530;server=oitdb.ccubo8pyjzvy.us-east-1.rds.amazonaws.com;" +
                                       "Trusted_Connection=no;" +
                                       "database=Oitdb; " +
                                       "connection timeout=30");
            dbConnection.Open();
            string query = "SELECT * FROM dbo.MapListings;";  //TODO this

            SqlCommand command = new SqlCommand(query, dbConnection);
            //command.Parameters.Add("@", SqlDbType.String); // TODO
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            //command.Parameters["@"].Value = payload.something; // TODO
            SqlDataReader reader = command.ExecuteReader();
            List<MapListing> listings = new List<MapListing>();
            try
            {
                Console.WriteLine("Below are all MapListings rows");
                while (reader.Read())
                {
                    Console.WriteLine("*** New Row ***");
                    for (int x = 0; x < reader.FieldCount; x++)
                    {
                        Console.WriteLine(reader.GetName(x) + " : " + reader[x]);
                    }
                    listings.Add(new MapListing(reader));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in retrieving map listings : " + e);
            }
            dbConnection.Close();
            // return results?
            return listings;
        }

        /// <summary>
        /// Query based on certain inputs
        /// </summary>
        /// <param name="queryParams">
        /// Dictionary of Paramters to query on
        /// </param>
        /// <returns></returns>
        public List<MapListing> SearchMapListings(Dictionary<string, string> queryParams)
        {
            dbConnection.Open();
            string query = "SELECT * FROM dbo.MapListings WHERE " +
                string.Join(",", queryParams.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + ";";  //Pass in a dictionary of query parameters to be used in the
                                                                                                      // in the where statement
            SqlCommand command = new SqlCommand(query, dbConnection);
            //command.Parameters.Add("@", SqlDbType.String); // TODO
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            //command.Parameters["@"].Value = payload.something; // TODO
            SqlDataReader reader = command.ExecuteReader();
            List<MapListing> listings = new List<MapListing>();
            try
            {
                Console.WriteLine("Below are all MapListings rows");
                while (reader.Read())
                {
                    Console.WriteLine("*** New Row ***");
                    for (int x = 0; x < reader.FieldCount; x++)
                    {
                        Console.WriteLine(reader.GetName(x) + " : " + reader[x]);
                    }
                    listings.Add(new MapListing(reader));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in retrieving map listings");
            }
            dbConnection.Close();
            // return results?
            return listings;
        }

        public string InsertTag(string payload)
        {
            string query = "INSERT INTO Tags (TagName, Description)";
            query = query + "VALUES (@tagName, @description)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            //command.Parameters.Add("@tagName", SqlDbType.String);
            //command.Parameters.Add("@description", SqlDbType.String);

            // will probably need to convert payload from string to json or someting liek that

            //command.Parameters["@tagName"].Value = payload.something;
            //command.Parameters["@description"].Value = payload.something2;

            command.ExecuteNonQuery();
            return null;
        }

        public string SearchTags(Object payload)
        {
            string query = "SELECT ALL FROM Tags where TagName= @name";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.Add("@name", SqlDbType.NChar);

            //command.Parameters["@name"].Value = payload.something;
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    // do something
                }
            }
            catch (Exception e)
            {
            }
            // return results?
            return null;
        }

        public void UpvoteListing(int payload)
        {
            dbConnection.Open();
            string query = "INSERT INTO VotedOn VALUES('Anonymous@pitt.edu', @listID, 1)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.Add("@listID", SqlDbType.Int);
            command.Parameters["@listID"].Value = payload;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

        public void DownvoteListing(int payload)
        {
            dbConnection.Open();
            string query = "INSERT INTO VotedOn VALUES('Anonymous@pitt.edu', @listID, -1)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.Add("@listID", SqlDbType.Int);
            command.Parameters["@listID"].Value = payload;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }
    }
}