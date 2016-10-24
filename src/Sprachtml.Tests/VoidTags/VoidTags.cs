using System;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Exceptions;
using Sprachtml.Models;

namespace Sprachtml.Tests.VoidTags
{
    public class VoidTagsWhichAreInTheAllowedList : ParsingTestBase
    {

        protected override string Markup => "<input type='text' ><img> <area><br><hr>";

        [Test]
        public void ShouldParseSuccessfully()
        {
            var nodes = GetNodes();
            nodes.Length.ShouldBe(6);
            nodes[0].NodeType.ShouldBe(HtmlNodeType.Input);
            nodes[1].NodeType.ShouldBe(HtmlNodeType.Img);
            nodes[2].NodeType.ShouldBe(HtmlNodeType.Text);
            nodes[3].NodeType.ShouldBe(HtmlNodeType.Area);
            nodes[4].NodeType.ShouldBe(HtmlNodeType.Br);
            nodes[5].NodeType.ShouldBe(HtmlNodeType.Hr);
        }


    }

    public class VoidTagsWhichAreNotInTheAllowedList : ParsingTestBase
    {

        protected override string Markup => "<ul><li>Nope<li>Not happening</ul>";

        [Test]
        public void ShouldFail()
        {
            Action go = ()=>GetNodes();
            go.ShouldThrow<SprachtmlParseException>();
        }


    }
}