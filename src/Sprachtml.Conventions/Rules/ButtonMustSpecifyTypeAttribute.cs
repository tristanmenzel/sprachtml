using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sprachtml.Extensions;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Rules
{
    public interface IConventionRule
    {
        RuleResult Test(ICollection<IHtmlNode> nodes);
    }

    public class RuleResult
    {

        public RuleResult(bool passed, string message = null, IHtmlNode[] offendingNodes = null)
        {
            OffendingNodes = offendingNodes ?? new IHtmlNode[0];
            Passed = passed;
            Message = message;
        }

        public IHtmlNode[] OffendingNodes { get; }
        public bool Passed { get; }
        public string Message { get; }

        public static RuleResult Pass() => new RuleResult(true);
    }

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