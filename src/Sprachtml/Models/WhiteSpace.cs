namespace Sprachtml.Models
{
    public class WhiteSpace : IHtmlNode
    {
        public string Contents { get; }

        public WhiteSpace(string contents)
        {
            Contents = contents;
        }

        public HtmlNodeType NodeType => HtmlNodeType.Style;
        IHtmlNode[] IHtmlNode.Children => new IHtmlNode[0];
        HtmlAttribute[] IHtmlNode.Attributes => new HtmlAttribute[0];
    }
}