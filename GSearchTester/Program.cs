using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSearch;

namespace GSearchTester
{
    class Program
    {
        private static bool printToFile = false;
        private static string filePath = "";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;
            Console.WriteLine("Testing program for GSearch library. \n");
            Console.WriteLine("Please note that results in foreign langauges may not appear correctly in the console, but will appear correctly when written to a file.");

            // Check if user wants to print results to a file
            Console.WriteLine("Print search results to a file? (Y/N)");
            String ans = Console.ReadLine();
            if (ans.ToLower() == "y")
            {
                printToFile = true;
                Console.WriteLine("Enter path to file with file name + .ext: ");
                filePath = Console.ReadLine();
            }

            // Set Locale
            GSearch.GSearch.SetLocale(Constants.Languages.English);
            Console.WriteLine(String.Format("Locale set to: {0} \n", GSearch.GSearch.GetLocale()));
            
            ProcessInput();
        }

        static void ProcessInput()
        {
            Console.WriteLine("Enter a search term:");
            String query = Console.ReadLine();
            string[] results = GSearch.GSearch.GetResultsAsArray(query); // Get results
            Console.WriteLine("Autocomplete Suggestions: ");
            foreach (string s in results)
            {
                Console.WriteLine(s);
            }
            if (printToFile)
            {
                try
                {
                    System.IO.File.WriteAllText(filePath, GSearch.GSearch.GetResultsAsString(query));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Raw XML:");
            Console.WriteLine(GSearch.GSearch.GetResultsAsString(query) + "\n");

            ProcessInput();
        }
    }
}
