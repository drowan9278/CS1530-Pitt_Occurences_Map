using System.Data.SqlClient;

namespace _1530Application.HelperClasses
{
    public class MapListing
    {
        private double Xcord { get; set; }
        private double YCord { get; set; }
        private string Description { get; set; }
        private int Image { get; set; }
        private int Upvotes { get; set; }
        private int Downvotes { get; set; }
        private string Creator { get; set; }
        private string Tags { get; set; }

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
            Image = int.Parse(image);
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
            Image = reader.GetInt32(4);
            Upvotes = reader.GetInt32(5);
            Downvotes = reader.GetInt32(6);
            Creator = reader.GetString(7);
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