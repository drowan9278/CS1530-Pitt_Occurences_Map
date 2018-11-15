using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _1530Application
{
    public class DbConnection1530
    {
        public SqlConnection dbConnection;

        public DbConnection1530()
        {
            dbConnection = new SqlConnection("user id=admin;" +
                                       "password=oakland1530;server=oitdb.ccubo8pyjzvy.us-east-1.rds.amazonaws.com;" +
                                       "Trusted_Connection=no;" +
                                       "database=Oitdb; " +
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

        public void InsertUser(string payload)
        {
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
        }

        public string SearchUser(string payload)
        {
            string query = "SELECT ALL FROM Users where email= @email";

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

        public string InsertMapListing(string payload)
        {
            string query = "INSERT INTO MapListings (Xcoord, Ycoord, Description, Image, Upvotes, Downvotes, Creator, Tags)";
            query = query + "VALUES (@xcoord, @ycoord, @description, @image, @upvotes, @downvotes, @creator, @tags)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            //command.Parameters.Add("@xcoord", SqlDbType.String);
            //command.Parameters.Add("@ycoord", SqlDbType.String);
            //command.Parameters.Add("@description", SqlDbType.String);
            command.Parameters.Add("@image", SqlDbType.Int);
            command.Parameters.Add("@upvotes", SqlDbType.Int);
            command.Parameters.Add("@downvotes", SqlDbType.Int);
            command.Parameters.Add("@creator", SqlDbType.Int);
            command.Parameters.Add("@tags", SqlDbType.Int);

            // will probably need to convert payload from string to json or someting liek that

            //command.Parameters["@xcoord"].Value = payload.something;
            //command.Parameters["@ycoord"].Value = payload.something2;
            //command.Parameters["@description"].Value = payload.something3;
            //command.Parameters["@image"].Value = payload.something4;
            //command.Parameters["@upvotes"].Value = payload.something5;
            //command.Parameters["@downvotes"].Value = payload.something5;
            //command.Parameters["@creator"].Value = payload.something5;
            //command.Parameters["@tags"].Value = payload.something5;

            command.ExecuteNonQuery();
            return null;
        }
        /// <summary>
        /// Return all Map Listings
        /// </summary>
        /// <returns></returns>
        public string SearchMapListings()
        {
            string query = "SELECT * FROM dbo.MapListings;";  //TODO this

            SqlCommand command = new SqlCommand(query, dbConnection);
            //command.Parameters.Add("@", SqlDbType.String); // TODO
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            //command.Parameters["@"].Value = payload.something; // TODO
            SqlDataReader reader = command.ExecuteReader();
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in retrieving map listings");
            }
            // return results?
            return null;
        }
        /// <summary>
        /// Query based on certain inputs
        /// </summary>
        /// <param name="queryParams">
        /// Dictionary of Paramters to query on
        /// </param>
        /// <returns></returns>
        public string SearchMapListings( Dictionary<string, string> queryParams)
        {
            string query = "SELECT * FROM dbo.MapListings;";  //TODO this

            SqlCommand command = new SqlCommand(query, dbConnection);
            //command.Parameters.Add("@", SqlDbType.String); // TODO
            command.CommandTimeout = 15;
            command.CommandType = CommandType.Text;
            //command.Parameters["@"].Value = payload.something; // TODO
            SqlDataReader reader = command.ExecuteReader();
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in retrieving map listings");
            }
            // return results?
            return null;
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
    }
}