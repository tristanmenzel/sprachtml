using Sprache;

namespace Sprachtml.Models
{
    public class CommentNode : IHtmlNode
    {
        public string Contents { get; }
        public Position NodeLocation { get; set; }

        public CommentNode(string contents)
        {
            Contents = contents;
        }

        public TagStyle TagStyle => TagStyle.Special;
        public HtmlNodeType NodeType => HtmlNodeType.Comment;
        IHtmlNode[] IHtmlNode.Children => new IHtmlNode[0];
        HtmlAttribute[] IHtmlNode.Attributes => new HtmlAttribute[0];
    }
}