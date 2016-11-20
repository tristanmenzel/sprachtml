using Sprache;

namespace Sprachtml.Models
{
    public class StyleNode : IHtmlNode
    {
        public string Contents { get; }
        public Position NodeLocation { get; set; }

        public StyleNode(string contents, HtmlAttribute[] attributes)
        {
            Contents = contents;
            Attributes = attributes;
        }

        public TagStyle TagStyle => TagStyle.Closed;
        public HtmlNodeType NodeType => HtmlNodeType.Style;
        IHtmlNode[] IHtmlNode.Children => new IHtmlNode[0];
        public HtmlAttribute[] Attributes { get; }
    }
}