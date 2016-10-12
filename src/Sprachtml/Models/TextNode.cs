namespace Sprachtml.Models
{
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
}