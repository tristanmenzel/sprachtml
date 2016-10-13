﻿using System.Linq;
using Sprache;
using Sprachtml.Models;
using Sprachtml.Parsers;

namespace Sprachtml
{
    public static class SprachtmlParser
    {
        public static IHtmlNode[] Parse(string html)
        {
            return SprachtmlParsers.HtmlParser.Parse(html).ToArray();
        }
    }
}