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
            var tracer = new Tracer();
            try
            {
                return SprachtmlParsers.HtmlParser(tracer).Parse(html).ToArray();
            }
            catch (ParseException parseException)
            {
                if (tracer.AttrPositions.Any())
                    throw new SprachtmlParseException($"The input could not be parsed. Invalid attribute at ({tracer.AttrPositions.Peek()})",
                        tracer.NodePositions.Peek(),
                        tracer.AttrPositions.Peek(),
                        String.Join(" > ", tracer.Nodes.Reverse()),
                        parseException);

                if (tracer.NodePositions.Any())
                    throw new SprachtmlParseException($"The input could not be parsed. Incomplete node at ({tracer.NodePositions.Peek()})",
                        tracer.NodePositions.Peek(),
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