using _1530Application;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Driver Program to test core features of the program");
            DbConnection1530 dbcon = new DbConnection1530();
            dbcon.SearchMapListings();//Return all
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams["Image"] = "1";//Add as many parameters as you would like
            Console.WriteLine("Testing with Dictionary");
            var list = dbcon.SearchMapListings(queryParams);
            Console.WriteLine("Done Testing");
        }
    }
}