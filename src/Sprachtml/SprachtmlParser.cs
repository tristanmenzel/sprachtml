using System;
using System.Linq;
using System.Net.Http.Headers;
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
            var tracer = new Tracer();
            try
            {
                return SprachtmlParsers.HtmlParser(tracer).Parse(html).ToArray();
            }
            catch (ParseException parseException)
            {
                if (tracer.Positions.Any())
                    throw new SprachtmlParseException($"The input could not be parsed",
                        tracer.Positions.Peek(),
                        String.Join(" > ", tracer.Nodes.Reverse()),
                        parseException);
                throw new SprachtmlParseException($"The input could not be parsed",
                    new Position(1, 1, 1),
                    String.Join(" > ", tracer.Nodes.Reverse()),
                    parseException);
            }
        }
    }
}