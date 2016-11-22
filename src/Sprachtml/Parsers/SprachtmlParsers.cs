using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;
using Sprachtml.Extensions;
using Sprachtml.Helpers;
using Sprachtml.Models;

namespace Sprachtml.Parsers
{
    public static class SprachtmlParsers
    {
        public static Parser<IHtmlNode> Comment =>
            from comment in new CommentParser("<!--", "-->").AnyComment
            select new CommentNode(comment);

        public static Parser<char> LetterDigitOrHyphen = Parse.LetterOrDigit.Or(Parse.Char('-'));

        public static Parser<TagIdentifier> TagNameParser =>
            from identifier in LetterDigitOrHyphen.AtLeastOnce()
            select new TagIdentifier(identifier.AsString());

        public static Parser<QuotedString> SingleQuotedString =>
            from o in Parse.Char('\'').Once()
            from any in Parse.CharExcept('\'').Many()
            from c in Parse.Char('\'').Once()
            select new QuotedString(QuoteType.Single, any.AsString());

        public static Parser<QuotedString> DoubleQuotedString =>
            from o in Parse.Char('\"').Once()
            from any in Parse.CharExcept('\"').Many()
            from c in Parse.Char('\"').Once()
            select new QuotedString(QuoteType.Double, any.AsString());

        public static Parser<QuotedString> UnquotedString =>
            from val in Parse.LetterOrDigit.AtLeastOnce()
            select new QuotedString(QuoteType.None, val.AsString());

        public static Parser<QuotedString> QuotedString => SingleQuotedString.Or(DoubleQuotedString).Or(UnquotedString);

        public static Parser<QuotedString> AttributeValue =>
            from ws1 in Parse.WhiteSpace.Many()
            from eq in Parse.Char('=').Once()
            from ws2 in Parse.WhiteSpace.Many()
            from value in QuotedString.Once()
            select value.Single();

        public static Func<Tracer, Parser<IEnumerable<HtmlAttribute>>> AttributesParser => tracer =>
            from reset in tracer.ResetAttrPositions()
            from attrs in SingleAttributeParser(tracer).Many()
            select attrs;

        public static Func<Tracer, Parser<HtmlAttribute>> SingleAttributeParser => tracer =>
            from pos in tracer.CurrentPosition
            from t1 in tracer.PushAttr(pos)
            from leadingWhiteSpace in Parse.WhiteSpace.AtLeastOnce()
            from identifier in LetterDigitOrHyphen.AtLeastOnce()
            from attrValue in AttributeValue.Optional()
            select new HtmlAttribute(identifier.AsString(), attrValue);


        public static Parser<IHtmlNode> TextNode =>
            from t in Parse.CharExcept('<').AtLeastOnce()
            select new TextNode(t.AsString());

        public static Func<Tracer,Parser<IHtmlNode>> ScriptTag => tracer =>
            from scriptOpen in Parse.IgnoreCase("<script").Once()
            from attributes in AttributesParser(tracer)
            from ws in Parse.WhiteSpace.Many()
            from openGt in Parse.Char('>').Once()
            from reset in tracer.ResetAttrPositions()
            from contents in Parse.AnyChar.Except(Parse.IgnoreCase("</script>")).Many()
            from close in Parse.IgnoreCase("</script>").Once()
            select new ScriptNode(contents.AsString(), attributes.ToArray());

        public static Func<Tracer,Parser<IHtmlNode>> StyleTag => tracer =>
            from scriptOpen in Parse.IgnoreCase("<style").Once()
            from attributes in AttributesParser(tracer)
            from ws in Parse.WhiteSpace.Many()
            from openGt in Parse.Char('>').Once()
            from reset in tracer.ResetAttrPositions()
            from contents in Parse.AnyChar.Except(Parse.IgnoreCase("</style>")).Many()
            from close in Parse.IgnoreCase("</style>").Once()
            select new StyleNode(contents.AsString(), attributes.ToArray());

        public static Func<Tracer, Parser<IHtmlNode>> HtmlTag => tracer =>
             from openLt in Parse.Char('<')
             from pos in tracer.CurrentPosition
             from tagName in TagNameParser
             from t1 in tracer.Push(tagName.Value, pos)
             from attributes in AttributesParser(tracer)
             from ws in Parse.WhiteSpace.Many()
             from openGt in Parse.Char('>').Once()
             from reset in tracer.ResetAttrPositions()
             from children in HtmlChildParser(tracer)
             from closeLt in Parse.Char('<')
             from slash in Parse.Char('/')
             from closeTagName in Parse.IgnoreCase(tagName.Value)
             from closeGt in Parse.Char('>')
             from t2 in tracer.Pop()
             select new HtmlNode(TagStyle.Closed, tagName.NodeType, tagName.Value, attributes.ToArray(), children.ToArray());

        public static Parser<TagIdentifier> AllowedVoidTags =>
            from tagName in TagHelper.GetVoidNodeTypes()
                .Aggregate<HtmlNodeType, Parser<IEnumerable<char>>>(null,
                    (parser, tag) => parser == null ? Parse.IgnoreCase(tag.ToString()) : parser.Or(Parse.IgnoreCase(tag.ToString())))
            select new TagIdentifier(tagName.AsString());

        public static Func<Tracer, Parser<IHtmlNode>> VoidHtmlTag => tracer =>
            from openLt in Parse.Char('<')
            from tagName in AllowedVoidTags
            from attributes in AttributesParser(tracer)
            from ws in Parse.WhiteSpace.Many()
            from openGt in Parse.Char('>').Once()
            from reset in tracer.ResetAttrPositions()
            select new HtmlNode(TagStyle.Void, tagName.NodeType, tagName.Value, attributes.ToArray(), new IHtmlNode[0]);

        public static Func<Tracer, Parser<IHtmlNode>> SelfClosingHtmlTag => tracer =>
            from openLt in Parse.Char('<')
            from tagName in TagNameParser
            from attributes in AttributesParser(tracer)
            from ws in Parse.WhiteSpace.Many()
            from selfClosingSlash in Parse.Char('/')
            from openGt in Parse.Char('>').Once()
            from reset in tracer.ResetAttrPositions()
            select new HtmlNode(TagStyle.SelfClosing, tagName.NodeType, tagName.Value, attributes.ToArray(), new IHtmlNode[0]);

        public static Parser<QuotedString> DocTypeProperties =>
            from ws in Parse.WhiteSpace.AtLeastOnce()
            from prop in QuotedString
            select prop;

        public static Parser<IHtmlNode> DocTypeTag =>
            from lt in Parse.Char('<')
            from bang in Parse.Char('!')
            from docType in Parse.IgnoreCase("doctype")
            from properties in DocTypeProperties.Many()
            from ws in Parse.WhiteSpace.Many()
            from gt in Parse.Char('>')
            select new DocTypeNode(properties.ToArray());



        private static Parser<T> WithPosition<T>(this Parser<T> parser) where T : IHtmlNode
        {
            return i =>
            {
                var r = parser(i);
                if (r.WasSuccessful)
                    r.Value.NodeLocation = new Position(i.Position, i.Line, i.Column);

                return r;
            };
        }

        private static Func<Tracer, Parser<IEnumerable<IHtmlNode>>> HtmlChildParser => tracer =>
             Comment
                 .Or(ScriptTag(tracer))
                 .Or(StyleTag(tracer))
                 .Or(SelfClosingHtmlTag(tracer))
                 .Or(VoidHtmlTag(tracer))
                 .Or(HtmlTag(tracer))
                 .Or(TextNode)
                 .WithPosition()
                 .Many();


        public static Func<Tracer, Parser<IEnumerable<IHtmlNode>>> HtmlParser => tracer =>
            DocTypeTag
                .Or(Comment)
                .Or(ScriptTag(tracer))
                .Or(StyleTag(tracer))
                .Or(SelfClosingHtmlTag(tracer))
                .Or(VoidHtmlTag(tracer))
                .Or(HtmlTag(tracer))
                .Or(TextNode)
                .WithPosition()
                .Many()
                .End();
    }
}
