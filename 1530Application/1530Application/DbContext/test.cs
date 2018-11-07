using MySql.Data;
using MySql.Data.MySqlClient;

namespace _1530Application
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
           return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    return false;
                string connstring = string.Format("Server=localhost; database={0}; UID=UserName; password=your password", databaseName);
                connection = new MySqlConnection(connstring);
                connection.Open();
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
// usage: 
// var dbCon = DBConnection.Instance();
// dbCon.DatabaseName = "YourDatabase";
// if (dbCon.IsConnect())
// {
//     string query = "SELECT col0,col1 FROM YourTable";
//     var cmd = new MySqlCommand(query, dbCon.Connection);
//     var reader = cmd.ExecuteReader();
//     while(reader.Read())
//     {
//         string someStringFromColumnZero = reader.GetString(0);
//         string someStringFromColumnOne = reader.GetString(1);
//         Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
//     }
//     dbCon.Close();
// }
