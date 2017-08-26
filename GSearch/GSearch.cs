using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GSearch
{
    /// <summary>
    ///     GSearch is a simple library designed to fetch Google autocomplete results for easy access in .NET applications.
    /// </summary>
    public class GSearch
    {
        
        // Vars
        private static String locale = Constants.Languages.English; // Set current search locale

        /// <summary>
        ///     Returns an array of results as string from search.
        /// </summary>
        /// <param name="query">The search query for Google.</param>
        /// <returns></returns>
        public static string[] GetResultsAsArray(string query)
        {
            // Throw exception if empty
            if (String.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Argment cannot be null or empty!", "GetResultsAsArray(query)");

            string searchData = FetchData(query);

            // Parse through the suggestions
            if (searchData != null)
            {
                HashSet<string> suggestions = new HashSet<string>();
                using (XmlReader reader = XmlReader.Create(new StringReader(searchData)))
                {
                    while (reader.Read()) // Keep reading as long as response is not null
                    {
                        if (reader.IsStartElement()) // Make sure it is not nested
                        {
                            if (reader.Name == "suggestion")
                            {
                                suggestions.Add(reader.GetAttribute("data")); // Get the suggestion text
                            }
                        }
                    }
                }

                return suggestions.ToArray(); // Return result converted to an array
            }

            return new String[0]; // Else return an empty string array
        }

        /// <summary>
        ///     Returns the raw XML response of the search results.
        /// </summary>
        /// <param name="query">The search term entere by the user.</param>
        /// <returns></returns>
        public static string GetResultsAsString(string query)
        {
            return FetchData(query);
        }

        // Helper Methods
        private static string FetchData(string query)
        {
            // Set up variables and XML request
            string searchData = String.Empty;
            using (WebClient client = new WebClient())
            {
                string searchURL = String.Format(Constants.GOOGLE_XML_URL, query, locale);
                String charset = GetEncoding(client, searchURL); // Get the charset of the search results first so characters are printed correctly
                client.Encoding = Encoding.GetEncoding(charset); // Convert charset string to Encoding
                searchData = client.DownloadString(searchURL);
            }
            return searchData;
        }

        // ASYNC METHODS
        public async static Task<String[]> GetResultsAsArrayAsync(string query)
        {
            if (String.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Argment cannot be null or empty!", "GetResultsAsArray(query)");

            string searchData = String.Empty;

            // Using HTTP Client to fetch instead since it supports async operations
            using (HttpClient client = new HttpClient())
            {
                searchData = await client.GetStringAsync(String.Format(Constants.GOOGLE_XML_URL, query, locale));
            }

            // Create an XML document parser
            XDocument xdoc = XDocument.Parse(searchData);
            var suggestions = from suggestion in xdoc.Descendants("CompleteSuggestion")
                              select suggestion.Element("suggestion").Attribute("data").Value; // Using LINQ to get all values

            return suggestions.ToArray<String>(); // Return as string array
        }

        /// <summary>
        ///     Get the current locale of the client
        /// </summary>
        /// <returns></returns>
        public static String GetLocale()
        {
            return locale;
        }

        /// <summary>
        ///     Set the locale of the client
        /// </summary>
        /// <param name="lang">Set the locale from Constants.Language.* </param>
        public static void SetLocale(String lang)
        {
            Type structType = typeof(Constants.Languages); // First extract the struct from the constants class
            System.Reflection.FieldInfo[] languages = structType.GetFields(); // Use reflection to get the different fields

            foreach (var field in languages)
            {
                if (field.GetValue(structType).ToString() == lang) // Match up the fields to see if user entered encoding exists
                {
                    locale = field.GetValue(structType).ToString(); // Set the locale
                    return;
                }
            }

            // If not found, default locale is 'en'
        }

        /// <summary>
        ///     Get the encoding of the page.
        /// </summary>
        /// <param name="client">WebClient used to load up the page</param>
        /// <param name="url">The page we want to load</param>
        /// <returns>Charset of the page</returns>
        public static string GetEncoding(WebClient client, String url)
        {
            client.DownloadString(url);
            var contentType = client.ResponseHeaders["Content-Type"];
            return System.Text.RegularExpressions.Regex.Match(contentType, "charset=([^;]+)").Groups[1].Value;
        }
    }
}
