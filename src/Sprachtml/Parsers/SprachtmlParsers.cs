using System.Collections.Generic;
using System.Linq;
using Sprache;
using Sprachtml.Extensions;
using Sprachtml.Models;

namespace Sprachtml.Parsers
{
    public static class SprachtmlParsers
    {

        public static Parser<char> LetterDigitOrHyphen = Parse.LetterOrDigit.Or(Parse.Char('-'));

        public static Parser<TagIdentifier> TagNameParser => (
            from identifier in LetterDigitOrHyphen.AtLeastOnce()
            select new TagIdentifier(identifier.AsString()));

        public static Parser<QuotedString> SingleQuotedString => (
            from o in Parse.Char('\'').Once()
            from any in Parse.CharExcept('\'').Many()
            from c in Parse.Char('\'').Once()
            select new QuotedString(QuoteType.Single, any.AsString())
            );
        public static Parser<QuotedString> DoubleQuotedString => (
        from o in Parse.Char('\"').Once()
        from any in Parse.CharExcept('\"').Many()
        from c in Parse.Char('\"').Once()
        select new QuotedString(QuoteType.Double, any.AsString())
        );

        public static Parser<QuotedString> QuotedString => SingleQuotedString.Or(DoubleQuotedString);

        public static Parser<QuotedString> AttributeValue => 
            from ws1 in Parse.WhiteSpace.Many()
            from eq in Parse.Char('=').Once()
            from ws2 in Parse.WhiteSpace.Many()
            from value in QuotedString.Once()
            select value.Single();

        public static Parser<HtmlAttribute> AttributeParser => 
            from leadingWhiteSpace in Parse.WhiteSpace.AtLeastOnce()
            from identifier in LetterDigitOrHyphen.AtLeastOnce()
            from attrValue in AttributeValue.Optional()
            select new HtmlAttribute(identifier.AsString(), attrValue);


        public static Parser<IHtmlNode> TextNode => 
		    from t in Parse.CharExcept('<').AtLeastOnce()
		    select new TextNode(t.AsString());


        public static Parser<IHtmlNode> HtmlTag =>
            from leadingWs in Parse.WhiteSpace.Many()
            from openLt in Parse.Char('<')
            from tagName in TagNameParser
            from attributes in AttributeParser.Many()
            from ws1 in Parse.WhiteSpace.Many()
            from openGt in Parse.Char('>')
            from children in SelfClosingHtmlTag.Or(HtmlTag).Or(TextNode).Many()
            from closeLt in Parse.Char('<')
            from slash in Parse.Char('/')
            from closeTagName in Parse.String(tagName.Value)
            from closeGt in Parse.Char('>')
            select new HtmlNode(tagName.NodeType, attributes.ToArray(), children.ToArray());

        public static Parser<IHtmlNode> SelfClosingHtmlTag =>
            from leadingWs in Parse.WhiteSpace.Many()
            from openLt in Parse.Char('<')
            from tagName in TagNameParser
            from attributes in AttributeParser.Many()
            from ws1 in Parse.WhiteSpace.Many()
            from selfClosingSlash in Parse.Char('/')
            from openGt in Parse.Char('>')
            select new HtmlNode(tagName.NodeType, attributes.ToArray(), new IHtmlNode[0]);

        public static Parser<IEnumerable<IHtmlNode>> HtmlParser =>
            SelfClosingHtmlTag
                .Or(HtmlTag)
                .Or(TextNode)
                .Many()
                .End();
    }
}
