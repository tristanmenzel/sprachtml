using System.Linq;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.Text
{
    [TestFixture]
    public class TextWithHtml : ParsingTestBase
    {
        protected override string Markup => "<p>This is a paragraph with <strong>strong</strong> content</p>";

        [Test]
        public void ShouldParseAsHtmlAndTextNodes()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);


            parsed.Single().NodeType.ShouldBe(HtmlNodeType.P);

            parsed.Single().Children.Length.ShouldBe(3);

            var children = parsed.Single().Children;

            children[0].NodeType.ShouldBe(HtmlNodeType.Text);
            children[1].NodeType.ShouldBe(HtmlNodeType.Strong);
            children[2].NodeType.ShouldBe(HtmlNodeType.Text);


        }
    }
}