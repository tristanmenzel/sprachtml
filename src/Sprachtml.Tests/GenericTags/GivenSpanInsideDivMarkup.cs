using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.GenericTags
{
    public class GivenSpanInsideDivMarkup : ParsingTestBase
    {


        [Test]
        public void ShouldParseToDivWithChildOfSpan()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Div);
            parsed[0].Children.Length.ShouldBe(1);

            parsed[0].Children[0].NodeType.ShouldBe(HtmlNodeType.Span);
            parsed[0].Children[0].Children.ShouldBeEmpty();
        }

        protected override string Markup => "<div><span></span></div>";
    }
}