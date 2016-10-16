using System;
using Sprache;

namespace Sprachtml.Exceptions
{
    public class SprachtmlParseException:Exception
    {
        public SprachtmlParseException(string message, ParseException innerException)
            :base(message, innerException)
        {
            
        }
        
    }
}