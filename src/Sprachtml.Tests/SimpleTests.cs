using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using Sprache;
using Sprachtml.Models;

namespace Sprachtml.Tests
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void GivenDivMarkup_Should_ParseToDiv()
        {
            var html = "<div></div>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Div);
            parsed[0].Children.ShouldBeEmpty();
        }


        [Test]
        public void GivenSpanInsideDivMarkup_Should_ParseToDivWithChildOfSpan()
        {
            var html = "<div><span></span></div>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Div);
            parsed[0].Children.Length.ShouldBe(1);

            parsed[0].Children[0].NodeType.ShouldBe(HtmlNodeType.Span);
            parsed[0].Children[0].Children.ShouldBeEmpty();
        }

        [Test]
        public void GivenDivWithIdMarkup_Should_ParseToDivWithIdAttribute()
        {
            var html = "<div id=\"TestId\"></div>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            var div = parsed.Single();

            div.NodeType.ShouldBe(HtmlNodeType.Div);
            div.Attributes.Length.ShouldBe(1);

            div.Attributes[0].Name.ShouldBe("id");
            div.Attributes[0].Value.ShouldBe("TestId");
            div.Attributes[0].Binary.ShouldBeFalse("Id should not be a binary attribute");
        }

        [Test]
        public void BinaryAttribute_Should_ParseAndBeMarkedAsBinary()
        {
            var html = "<textarea readonly></textarea>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Textarea);
            parsed[0].Attributes.Length.ShouldBe(1);
            parsed[0].Attributes[0].Name.ShouldBe("readonly");
            parsed[0].Attributes[0].Binary.ShouldBeTrue();
        }
        [Test]
        public void SelfClosingTag_Should_ParseAndHaveNoChildren()
        {
            var html = "<br />";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);
            parsed[0].Children.ShouldBeEmpty();
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Br);
        }
        [Test]
        public void Text_Should_ParseAsTextNode()
        {
            var html = "I am text with no html";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);
            parsed[0].Children.ShouldBeEmpty();
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Text);
            ((TextNode)parsed[0]).Contents.ShouldBe("I am text with no html");

        }
        [Test]
        public void TextWithHtml_Should_ParseAsHtmlAndTextNodes()
        {
            var html = "<p>This is a paragraph with <strong>strong</strong> content</p>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);


            parsed.Single().NodeType.ShouldBe(HtmlNodeType.P);

            parsed.Single().Children.Length.ShouldBe(3);

            var children = parsed.Single().Children;

            children[0].NodeType.ShouldBe(HtmlNodeType.Text);
            children[1].NodeType.ShouldBe(HtmlNodeType.Strong);
            children[2].NodeType.ShouldBe(HtmlNodeType.Text);


        }

        [Test]
        public void SimpleComment_Should_ParseAsHtmlComment()
        {
            var html = "<!-- test -->";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Comment);
        }


        [Test]
        public void HtmlInComment_Should_ParseAsHtmlComment()
        {
            var html = "<!-- <strong>Test</strong> <br /> -->";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Comment);
        }

        [Test]
        public void UnquotedAttributesWithNoSpaces_Should_ParseAsValidAttributes()
        {
            var html = "<input type=text value=test />";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Input);

            parsed[0].Attributes.Length.ShouldBe(2);
            var type = parsed[0].Attributes[0];
            type.Name.ShouldBe("type");
            type.Value.ShouldBe("text");
            type.QuoteType.ShouldBe(QuoteType.None);

            var value = parsed[0].Attributes[1];
            value.Name.ShouldBe("value");
            value.Value.ShouldBe("test");
            value.QuoteType.ShouldBe(QuoteType.None);
        }

        [Test]
        public void EmptyScriptTag_Should_ParseAsScriptTagWithNoContents()
        {
            var html = "<script></script>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[0].ShouldBeOfType<ScriptNode>();
            parsed[0].Contents.ShouldBeEmpty();
        }
        [Test]
        public void NonEmptyScriptTag_Should_ParseAsScriptTagWithContents()
        {
            var html = "<script>Four</script>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[0].ShouldBeOfType<ScriptNode>();
            parsed[0].Contents.Length.ShouldBe(4);
        }

        [Test]
        public void HtmlInScriptTagThatIsNotCloseScript_Should_BeIgnored()
        {
            var contents = "<h1>I might not be valid javascript, but I am valid html</h1>";
            var html = $"<script>{contents}</script>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[0].ShouldBeOfType<ScriptNode>();
            parsed[0].Contents.ShouldBe(contents);
        }
        [Test]
        public void RegularHtmlAndScriptTags_Should_CoexistPeacefully()
        {
            var contents = "<h1>I might not be valid javascript, but I am valid html</h1>";
            var html = $"{contents}<script></script>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(2);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.H1);

            parsed[1].NodeType.ShouldBe(HtmlNodeType.Script);
            parsed[1].ShouldBeOfType<ScriptNode>();
            parsed[1].Contents.ShouldBeEmpty();
        }


        [Test]
        public void EmptyStyleTag_Should_ParseAsStyleTagWithNoContents()
        {
            var html = "<style></style>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);


            parsed[0].NodeType.ShouldBe(HtmlNodeType.Style);
            parsed[0].ShouldBeOfType<StyleNode>();
            parsed[0].Contents.ShouldBeEmpty();
        }


        /*
         * TODO
         * Doc types
         * 
         * 
         * Different casing?
         * 
         */
    }
}
