using _1530Application;
using System;
using System.Collections.Generic;
using System.Data.Common;



namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DbConnection1530 dbcon = new DbConnection1530();
            dbcon.SearchMapListings();//Return all
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams["Image"] = "1";//Add as many parameters as you would like
            Console.WriteLine("Testing with Dictionary");
            dbcon.SearchMapListings(queryParams);
            Console.WriteLine("Done Testing");
        }
    }
}
