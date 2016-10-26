using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sprachtml.Conventions.Models;
using Sprachtml.Extensions;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Rules
{
    public class ButtonMustSpecifyTypeAttribute : IConventionRule
    {
        public RuleResult Test(ICollection<IHtmlNode> nodes)
        {
            var offendingNodes = nodes.TraverseAll()
                .Where(n => n.NodeType == HtmlNodeType.Button)
                .Where(n => n.Attributes.All(a => !string.Equals(a.Name, "type")))
                .ToArray();
            if (offendingNodes.Any())
                return new RuleResult(false, $"Input failed rule {nameof(ButtonMustSpecifyTypeAttribute)}",
                    offendingNodes);
            return RuleResult.Pass();
        }
    }
}