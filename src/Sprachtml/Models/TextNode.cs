namespace Sprachtml.Models
{
    public class TextNode : IHtmlNode
    {
        public string Contents { get; }

        public TextNode(string contents)
        {
            Contents = contents;
        }

        public HtmlNodeType NodeType => HtmlNodeType.Text;
        IHtmlNode[] IHtmlNode.Children => new IHtmlNode[0];
        HtmlAttribute[] IHtmlNode.Attributes => new HtmlAttribute[0];
    }
}