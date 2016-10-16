using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.CustomElements
{
    public class CustomElements:ParsingTestBase
    {
        [Test]
        public void ShouldParseSuccessfullyWithATypeOfCustom()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Custom);
            ((HtmlNode) parsed[0]).TagName.ShouldBe("my-custom-element");
        }

        protected override string Markup => "<my-custom-element></my-custom-element>";
    }
}