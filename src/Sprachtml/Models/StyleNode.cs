namespace Sprachtml.Models
{
    public class StyleNode : IHtmlNode
    {
        public string Contents { get; }

        public StyleNode(string contents, HtmlAttribute[] attributes)
        {
            Contents = contents;
            Attributes = attributes;
        }

        public HtmlNodeType NodeType => HtmlNodeType.Style;
        IHtmlNode[] IHtmlNode.Children => new IHtmlNode[0];
        public HtmlAttribute[] Attributes { get; }
    }
}