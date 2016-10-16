using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.ScriptTags
{
    public class NonEmptyScriptTag : ParsingTestBase
    {
        protected override string Markup => "<script>Four</script>";

        [Test]
        public void ShouldParseAsScriptTagWithContents()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[0].ShouldBeOfType<ScriptNode>();
            parsed[0].Contents.Length.ShouldBe(4);
        }

    }
}