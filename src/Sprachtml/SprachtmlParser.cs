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
            try
            {
                Tracer.Instance = new Tracer();
                return SprachtmlParsers.HtmlParser.Parse(html).ToArray();
            }
            catch (ParseException parseException)
            {
                if (Tracer.Instance.Positions.Any())
                    throw new SprachtmlParseException($"The input could not be parsed",
                        Tracer.Instance.Positions.Peek(),
                        String.Join(" > ", Tracer.Instance.Nodes.Reverse()),
                        parseException);
                throw new SprachtmlParseException($"The input could not be parsed",
                    new Position(1, 1, 1),
                    String.Join(" > ", Tracer.Instance.Nodes.Reverse()),
                    parseException);
            }
        }
    }
}