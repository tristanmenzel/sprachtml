namespace Sprachtml.Models
{
    public class CommentNode : IHtmlNode
    {
        public string Contents { get; }

        public CommentNode(string contents)
        {
            Contents = contents;
        }

        public HtmlNodeType NodeType => HtmlNodeType.Comment;
        IHtmlNode[] IHtmlNode.Children => new IHtmlNode[0];
        HtmlAttribute[] IHtmlNode.Attributes => new HtmlAttribute[0];
    }
}