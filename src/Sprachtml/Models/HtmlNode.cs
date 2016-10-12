using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace Sprachtml.Models
{
    public interface IHtmlNode
    {
        HtmlNodeType NodeType { get; }
        IHtmlNode[] Children { get; }
        HtmlAttribute[] Attributes { get; }

        
    }

    public class HtmlNode : IHtmlNode
    {
        public HtmlNode(HtmlNodeType nodeType, HtmlAttribute[] attributes, IHtmlNode[] children)
        {
            NodeType = nodeType;
            Attributes = attributes;
            Children = children;
        }

        public HtmlNodeType NodeType { get; }
        public HtmlAttribute[] Attributes { get; }
        public IHtmlNode[] Children { get; }


    }

    public class TextNode : IHtmlNode
    {
        public string Contents { get; }

        public TextNode(string contents)
        {
            Contents = contents;
            NodeType = HtmlNodeType.Text;
        }

        public HtmlNodeType NodeType { get; }
        IHtmlNode[] IHtmlNode.Children => new IHtmlNode[0];
        HtmlAttribute[] IHtmlNode.Attributes => new HtmlAttribute[0];
    }

    public class HtmlAttribute
    {
        public string Name { get; }
        public string Value { get; }

        public bool Binary { get; }

        public HtmlAttribute(string name, IOption<QuotedString> value )
        {
            Name = name;
            if (value.IsDefined)
            {
                Value = value.Get().Text;
                QuoteType = value.Get().QuoteType;
            }
            else
            {
                Binary = true;
            }
        }

        public QuoteType QuoteType { get;  }
    }

    public class QuotedString
    {
        public QuotedString(QuoteType quoteType, string text)
        {
            QuoteType = quoteType;
            Text = text;
        }
        public QuoteType QuoteType { get;  }
        public string Text { get;  }
    }

    public enum QuoteType
    {
        None,
        Single,
        Double
    }

    public class TagIdentifier
    {
        public TagIdentifier(string identifier)
        {
            Value = identifier;
            // SingleOrDefault will default to custom for no match
            NodeType = Enum.GetValues(typeof(HtmlNodeType))
                .Cast<HtmlNodeType>()
                .SingleOrDefault(n => string.Equals(n.ToString(), identifier, StringComparison.InvariantCultureIgnoreCase));


        }
        public string Value { get; }
        public HtmlNodeType NodeType { get; }

    }
}