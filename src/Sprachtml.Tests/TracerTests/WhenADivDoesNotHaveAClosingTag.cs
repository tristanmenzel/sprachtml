using System;
using NUnit.Framework;
using Shouldly;
using Sprache;
using Sprachtml.Parsers;

namespace Sprachtml.Tests.TracerTests
{
    [TestFixture]
    public class WhenADivDoesNotHaveAClosingTag
    {
        [Test]
        public void TheTracerWillRecordThePositionOfTheOpeningTag()
        {
            var input = $"<body><section><h1>hi</h1><div><span>hey</span></section></body>";

            var tracer = new Tracer();
            
            Action parse = ()=> SprachtmlParsers.HtmlParser(tracer).Parse(input);
            parse.ShouldThrow<ParseException>();

            tracer.AttrPositions.Count.ShouldBe(0);

            var pos = tracer.NodePositions.Peek();
            pos.Pos.ShouldBe(27);
            pos.Column.ShouldBe(28);
            pos.Line.ShouldBe(1);
            tracer.Nodes.Count.ShouldBe(3);
            var nodes = tracer.Nodes.ToArray();
            nodes[0].ShouldBe("div");
            nodes[1].ShouldBe("section");
            nodes[2].ShouldBe("body");
        }
    }
}