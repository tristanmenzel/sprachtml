using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.ScriptTags
{
    public class HtmlInScriptTagThatIsNotCloseScript : ParsingTestBase
    {
        protected string Contents => "<h1>I might not be valid javascript, but I am valid html</h1>";

        protected override string Markup => $"<script>{Contents}</script>";

        [Test]
        public void ShouldBeIgnored()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[0].ShouldBeOfType<ScriptNode>();
            parsed[0].Contents.ShouldBe(Contents);
        }
    }
}