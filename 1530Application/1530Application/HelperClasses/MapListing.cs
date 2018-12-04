using System;
using System.Data.SqlClient;

namespace _1530Application.HelperClasses
{
    [Serializable]
    public class MapListing
    {
        public double Xcord { get; set; }
        public double YCord { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public string Creator { get; set; }
        public string Tags { get; set; }

        public MapListing(string id,
            string xcord,
            string ycord,
            string description,
            string image,
            string upvotes,
            string downvotes,
            string creator,
            string tags)
        {
            Xcord = double.Parse(xcord);
            YCord = double.Parse(ycord);
            Description = description;
            Image = image;
            Upvotes = int.Parse(upvotes);
            Downvotes = int.Parse(downvotes);
            Creator = creator;
            Tags = tags;
        }

        public MapListing(SqlDataReader reader)
        {
            Xcord = reader.GetDouble(1);
            YCord = reader.GetDouble(2);
            Description = reader.GetString(3);
            Image = reader.GetString(4);
            Upvotes = reader.GetInt32(5);
            
            Creator = reader.GetString(6);
            try
            {
                Tags = reader.GetString(8);
            }
            catch (System.Exception e)
            {
                Tags = "";
            }
        }
    }
}