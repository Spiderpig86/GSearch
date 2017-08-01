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
        const string GOOGLE_XML_URL = "http://google.com/complete/search?output=toolbar&q="; // Head of the query URL

        public static string[] GetResults(string query)
        {
            // Set up variables and XML request
            string searchData = "";
            using (WebClient client = new WebClient())
            {
                searchData = client.DownloadString(GOOGLE_XML_URL + query); // Get the XML results
            }

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
    }
}
