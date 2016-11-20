using Sprache;

namespace Sprachtml.Models
{
    public class DocTypeNode : IHtmlNode
    {
        public QuotedString[] Properties { get; }

        public DocTypeNode(QuotedString[] properties)
        {
            Properties = properties;
        }

        public TagStyle TagStyle => TagStyle.Special;
        public HtmlNodeType NodeType => HtmlNodeType.DocType;
        public IHtmlNode[] Children => new IHtmlNode[0];
        public HtmlAttribute[] Attributes => new HtmlAttribute[0];
        public string Contents => null;
        public Position NodeLocation { get; set; }


    }
}