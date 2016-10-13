using Sprache;

namespace Sprachtml.Models
{
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
        string IHtmlNode.Contents => null;
        public Position NodeLocation { get; set; }
        public IHtmlNode[] Children { get; }
    }
}