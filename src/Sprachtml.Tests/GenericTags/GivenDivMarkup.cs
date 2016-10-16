using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.GenericTags
{
    public class GivenDivMarkup : ParsingTestBase
    {
        [Test]
        public void ShouldParseToDiv()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Div);
            parsed[0].Children.ShouldBeEmpty();
        }

        protected override string Markup => "<div></div>";

    }
}