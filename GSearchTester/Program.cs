using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSearch;

namespace GSearchTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing program for GSearch library. \n");
            ProcessInput();
        }

        static void ProcessInput()
        {
            Console.WriteLine("Enter a search term: ");
            String query = Console.ReadLine();
            string[] results = GSearch.GSearch.GetResultsAsArray(query); // Get results
            Console.WriteLine("Autocomplete Suggestions: ");
            foreach (string s in results)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("");
            Console.WriteLine("Raw XML:");
            Console.WriteLine(GSearch.GSearch.GetResultsAsString(query));

            ProcessInput();
        }
    }
}
