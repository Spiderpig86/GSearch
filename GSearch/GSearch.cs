using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
            string searchData = fetchData(query);

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
            return fetchData(query);
        }

        // Helper Methods
        private static string fetchData(string query)
        {
            // Set up variables and XML request
            string searchData = "";
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                //client.Encoding = Encoding.UTF8;
                //searchData = client.DownloadString(String.Format(Constants.GOOGLE_XML_URL, query, locale)); // Get the XML results
                client.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
                byte[] stream = client.DownloadData(String.Format(Constants.GOOGLE_XML_URL, query, locale)); // Get the XML results
                searchData = Encoding.UTF8.GetString(stream); // Convert to Unicode
            }
            return searchData;
        }

        public static String getLocale()
        {
            return locale;
        }

        public static void setLocale(String lang)
        {
            locale = lang;
        }
    }
}
