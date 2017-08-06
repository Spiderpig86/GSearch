using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSearch
{
    public static class Constants
    {
        public static const string GOOGLE_XML_URL = "http://google.com/complete/search?output=toolbar&q={0}&hl=en-US"; // Head of the query URL

        // Languages
        public static enum LANGUAGES
        {
            ENGLISH = "en",
            ENGLISH_US = "en-US",
            ENGLISH_UK = "en-UK"
        }
    }
}
