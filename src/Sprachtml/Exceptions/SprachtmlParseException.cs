using System;
using Sprache;

namespace Sprachtml.Exceptions
{
    public class SprachtmlParseException:Exception
    {
        public Position NodeLocation { get; }
        public Position AttrPosition { get; }
        public string XPath { get; }

        public SprachtmlParseException(string message,
            Position nodeLocation,
            string xPath,
            ParseException innerException)
            :base(message, innerException)
        {
            NodeLocation = nodeLocation;
            XPath = xPath;
        }


        public SprachtmlParseException(string message,
            Position nodeLocation,
            Position attrPosition,
            string xPath,
            ParseException innerException)
            : base(message, innerException)
        {
            NodeLocation = nodeLocation;
            AttrPosition = attrPosition;
            XPath = xPath;
        }
    }
}