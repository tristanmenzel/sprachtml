using Sprache;

namespace Sprachtml.Models
{
    public class HtmlNode : IHtmlNode
    {
        public HtmlNode(TagStyle tagStyle, HtmlNodeType nodeType, string tagName, HtmlAttribute[] attributes, IHtmlNode[] children)
        {
            TagStyle = tagStyle;
            NodeType = nodeType;
            TagName = tagName;
            Attributes = attributes;
            Children = children;
        }

        public TagStyle TagStyle { get; }
        public HtmlNodeType NodeType { get; }
        public string TagName { get; set; }
        public HtmlAttribute[] Attributes { get; }
        string IHtmlNode.Contents => null;
        public Position NodeLocation { get; set; }
        public IHtmlNode[] Children { get; }
    }

    public enum TagStyle
    {
        Closed,
        SelfClosing,
        Void,
        Special
    }
}