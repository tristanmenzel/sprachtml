using Sprache;

namespace Sprachtml.Models
{
    public interface IHtmlNode
    {
        TagStyle TagStyle { get; }
        HtmlNodeType NodeType { get; }
        IHtmlNode[] Children { get; }
        HtmlAttribute[] Attributes { get; }
        string Contents { get; }

        Position NodeLocation { get; set; }
    }
}