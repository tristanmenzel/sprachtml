using System;
using NUnit.Framework;
using Shouldly;
using Sprache;
using Sprachtml.Parsers;

namespace Sprachtml.Tests.TracerTests
{
    [TestFixture]
    public class WhenAnAnchorHasNoSpacesBetweenItsAttributes
    {
        [Test]
        public void TheTracerWillRecordThePositionOfTheInvalidAttribute()
        {
            var input = @"<body><section><h1>hi</h1><a href=""#""target=""_blank"">Wooh</a></section></body>";

            var tracer = new Tracer();

            Action parse = () => SprachtmlParsers.HtmlParser(tracer).Parse(input);
            parse.ShouldThrow<ParseException>();

            var pos = tracer.NodePositions.Peek();
            pos.Pos.ShouldBe(27);
            pos.Column.ShouldBe(28);
            pos.Line.ShouldBe(1);

            var attrPos = tracer.AttrPositions.Peek();
            attrPos.Pos.ShouldBe(37);
            attrPos.Column.ShouldBe(38);
            attrPos.Line.ShouldBe(1);

            tracer.Nodes.Count.ShouldBe(3);
            var nodes = tracer.Nodes.ToArray();
            nodes[0].ShouldBe("a");
            nodes[1].ShouldBe("section");
            nodes[2].ShouldBe("body");
        }
    }
}