using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.ScriptTags
{
    public class EmptyScriptTag : ParsingTestBase
    {

        protected override string Markup => "<script></script>";

        [Test]
        public void ShouldParseAsScriptTagWithNoContents()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[0].ShouldBeOfType<ScriptNode>();
            parsed[0].Contents.ShouldBeEmpty();
        }

    }
}