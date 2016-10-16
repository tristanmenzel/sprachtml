using System;
using System.Linq;
using Sprache;
using Sprachtml.Exceptions;
using Sprachtml.Models;
using Sprachtml.Parsers;

namespace Sprachtml
{
    public static class SprachtmlParser
    {
        public static IHtmlNode[] Parse(string html)
        {
            try
            {
                return SprachtmlParsers.HtmlParser.Parse(html).ToArray();
            }
            catch (ParseException parseException)
            {
                throw new SprachtmlParseException("Exception parsing html", parseException);
            }
        }
    }
}