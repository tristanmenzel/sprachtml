using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.SelfClosingTags
{
    public class SelfClosingTags: ParsingTestBase
    {
        [Test]
        public void ShouldParseAndHaveNoChildren()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);
            parsed[0].Children.ShouldBeEmpty();
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Br);
            parsed[0].TagStyle.ShouldBe(TagStyle.SelfClosing);
        }

        protected override string Markup => "<br />";
    }
}