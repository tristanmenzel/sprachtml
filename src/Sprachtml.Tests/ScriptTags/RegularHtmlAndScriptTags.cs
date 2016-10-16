using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.ScriptTags
{
    public class RegularHtmlAndScriptTags : ParsingTestBase
    {
        protected string Contents => "<h1>I might not be valid javascript, but I am valid html</h1>";

        protected override string Markup => $"{Contents}<script></script>";

        [Test]
        public void ShouldCoexistPeacefully()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(2);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.H1);

            parsed[1].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[1].ShouldBeOfType<ScriptNode>();
            parsed[1].Contents.ShouldBeEmpty();
        }

    }
}