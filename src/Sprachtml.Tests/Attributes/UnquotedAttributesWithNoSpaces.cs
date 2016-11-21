using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.Attributes
{
    public class UnquotedAttributesWithNoSpaces : ParsingTestBase
    {
        protected override string Markup => "<input type=text value=test />";

        [Test]
        public void ShouldParseAsValidAttributes()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Input);

            parsed[0].Attributes.Length.ShouldBe(2);
            var type = parsed[0].Attributes[0];
            type.Name.ShouldBe("type");
            type.Value.Text.ShouldBe("text");
            type.Value.QuoteType.ShouldBe(QuoteType.None);

            var value = parsed[0].Attributes[1];
            value.Name.ShouldBe("value");
            value.Value.Text.ShouldBe("test");
            value.Value.QuoteType.ShouldBe(QuoteType.None);
        }
    }
}