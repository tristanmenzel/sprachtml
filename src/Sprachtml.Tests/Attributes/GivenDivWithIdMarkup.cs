using System.Linq;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.Attributes
{
    public class GivenDivWithIdMarkup : ParsingTestBase
    {
        protected override string Markup => "<div id=\"TestId\"></div>";

        [Test]
        public void ShouldParseToDivWithIdAttribute()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            var div = parsed.Single();

            div.NodeType.ShouldBe(HtmlNodeType.Div);
            div.Attributes.Length.ShouldBe(1);

            div.Attributes[0].Name.ShouldBe("id");
            div.Attributes[0].Value.ShouldBe("TestId");
            div.Attributes[0].Binary.ShouldBeFalse("Id should not be a binary attribute");
        }
    }
}