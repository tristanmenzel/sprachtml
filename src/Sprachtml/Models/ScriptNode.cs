using Sprache;

namespace Sprachtml.Models
{
    public class ScriptNode : IHtmlNode
    {
        public string Contents { get; }
        public Position NodeLocation { get; set; }

        public ScriptNode(string contents, HtmlAttribute[] attributes)
        {
            Contents = contents;
            Attributes = attributes;

        }

        public HtmlNodeType NodeType => HtmlNodeType.Script;
        IHtmlNode[] IHtmlNode.Children => new IHtmlNode[0];
        public HtmlAttribute[] Attributes { get; }
    }
}