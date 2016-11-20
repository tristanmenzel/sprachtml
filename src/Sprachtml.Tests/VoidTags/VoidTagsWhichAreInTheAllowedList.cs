using System.Linq;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.VoidTags
{
    public class VoidTagsWhichAreInTheAllowedList : ParsingTestBase
    {

        protected override string Markup => "<input type='text' ><img> <area><br><hr><br /><span></span>";

        [Test]
        public void ShouldParseSuccessfully()
        {
            var nodes = GetNodes();
            nodes.Length.ShouldBe(8);
            nodes[0].NodeType.ShouldBe(HtmlNodeType.Input);
            nodes[1].NodeType.ShouldBe(HtmlNodeType.Img);
            nodes[2].NodeType.ShouldBe(HtmlNodeType.Text);
            nodes[3].NodeType.ShouldBe(HtmlNodeType.Area);
            nodes[4].NodeType.ShouldBe(HtmlNodeType.Br);
            nodes[5].NodeType.ShouldBe(HtmlNodeType.Hr);

            nodes.Count(n => n.TagStyle == TagStyle.Void).ShouldBe(5, "There should be 5 void nodes");
            nodes.Count(n => n.TagStyle == TagStyle.SelfClosing).ShouldBe(1, "There should be 1 self closing node");
            nodes.Count(n => n.TagStyle == TagStyle.Closed).ShouldBe(1, "There should be 1 closed node");
            nodes.Count(n => n.TagStyle == TagStyle.Special).ShouldBe(1, "There should be 1 special node");

        }


    }
}