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
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Testing program for GSearch library. \n");
            ProcessInput();
        }

        static void ProcessInput()
        {
            GSearch.GSearch.setLocale(Constants.Languages.Chinese_Simplified);
            Console.WriteLine("Enter a search term: 生西向黃新青");
            String query = Console.ReadLine();
            string[] results = GSearch.GSearch.GetResultsAsArray(query); // Get results
            Console.WriteLine("Autocomplete Suggestions: ");
            System.IO.File.WriteAllText(@"C:\Users\Stan\Desktop\WriteText.txt", GSearch.GSearch.GetResultsAsString(query));
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
