using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.GenericTags
{
    public class TagsWithDifferentCasing : ParsingTestBase
    {
        [Test]
        public void ShouldParseAsOneElement()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Div);
            parsed[0].Children.Length.ShouldBe(1);
            parsed[0].Children[0].NodeType.ShouldBe(HtmlNodeType.Span);
        }

        protected override string Markup => "<div><sPan></spaN></DIV>";

    }
}