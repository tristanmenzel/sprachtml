using System.Linq;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Rules
{
    public class ButtonMustSpecifyTypeAttribute : ConventionRuleBase
    {
        protected override bool IsOffending(IHtmlNode node)
        {
            if (node.NodeType != HtmlNodeType.Button)
                return false;
            return node.Attributes.All(a => !string.Equals(a.Name, "type"));
        }
    }
}