using _1530Application;
using System;
using System.Data.Common;



namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DbConnection1530 dbcon = new DbConnection1530();
            dbcon.SearchMapListings(" ");
        }
    }
}
