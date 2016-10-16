using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.Text
{
    [TestFixture]
    public class PlainText : ParsingTestBase
    {
        [Test]
        public void ShouldParseAsTextNode()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);
            parsed[0].Children.ShouldBeEmpty();
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Text);
            parsed[0].Contents.ShouldBe("I am text with no html");

        }

        protected override string Markup => "I am text with no html";
    }
}