using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.ScriptTags
{
    public class ScriptTagWithAttributes : ParsingTestBase
    {
        protected override string Markup => "<script type='text/javascript' id=myscript ></script>";

        [Test]
        public void ShouldParseAsScriptTagWithAttributes()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[0].ShouldBeOfType<ScriptNode>();
            parsed[0].Contents.ShouldBeEmpty();

            parsed[0].Attributes.Length.ShouldBe(2);
        }

    }
}