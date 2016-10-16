using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.GenericTags
{
    public class GivenHtmlWithNewLinesInIt : ParsingTestBase
    {
        [Test]
        public void ShouldParseWithoutIssue()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(3);

            parsed[1].NodeType.ShouldBe(HtmlNodeType.Div);
        }

        protected override string Markup => @"
                        <div>
                        </div>
                        ";
    }
}