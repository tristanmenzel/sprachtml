using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.StyleTags
{
    public class EmptyStyleTag : ParsingTestBase
    {
        [Test]
        public void ShouldParseAsStyleTagWithNoContents()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Style);
            parsed[0].ShouldBeOfType<StyleNode>();
            parsed[0].Contents.ShouldBeEmpty();
        }


        protected override string Markup => "<style></style>";
    }
}
