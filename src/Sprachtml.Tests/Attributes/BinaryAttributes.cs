using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.Attributes
{
    public class BinaryAttributes : ParsingTestBase
    {
        [Test]
        public void ShouldParseAndBeMarkedAsBinary()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Textarea);
            parsed[0].Attributes.Length.ShouldBe(1);
            parsed[0].Attributes[0].Name.ShouldBe("readonly");
            parsed[0].Attributes[0].Binary.ShouldBeTrue();
        }

        protected override string Markup => "<textarea readonly></textarea>";
    }
}