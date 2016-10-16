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
                Tracer.Instance = new Tracer();
                return SprachtmlParsers.HtmlParser.Parse(html).ToArray();
            }
            catch (ParseException parseException)
            {
                throw new SprachtmlParseException($"Incomplete tag at `{string.Join(" > ", Tracer.Instance.Nodes.Reverse())}`", parseException);
            }
        }
    }
}