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
            Console.WriteLine("Please specify a locale (e.g: 'en'):");
            String langCode = Console.ReadLine();

            // Set to English by default of empty
            if (langCode.Length == 0)
                GSearch.GSearch.SetLocale(Constants.Languages.English);
            else
            {
                Type structType = typeof(GSearch.Constants.Languages); // First extract the struct from the constants class
                System.Reflection.FieldInfo[] languages = structType.GetFields(); // Use reflection to get the different fields

                foreach (var field in languages)
                {
                    if (field.GetValue(structType).ToString() == langCode) // Match up the fields to see if user entered encoding exists
                    {
                        GSearch.GSearch.SetLocale(field.GetValue(structType).ToString()); // Set the locale
                        break;
                    }
                }
            }

            Console.WriteLine(String.Format("Locale set to: {0} \n", GSearch.GSearch.GetLocale())); // Locale will be set to 'en' by default as a safeguard to invalid inputs
            
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
