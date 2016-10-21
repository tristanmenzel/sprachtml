using System;
using Sprache;

namespace Sprachtml.Exceptions
{
    public class SprachtmlParseException:Exception
    {
        public Position Location { get; }
        public string XPath { get; }

        public SprachtmlParseException(string message,
            Position location,
            string xPath,
            ParseException innerException)
            :base(message, innerException)
        {
            Location = location;
            XPath = xPath;
        }
    }
}