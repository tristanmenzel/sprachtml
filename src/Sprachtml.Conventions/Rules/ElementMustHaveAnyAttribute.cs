using System;
using System.Collections.Generic;
using System.Linq;
using Sprachtml.Conventions.Models;
using Sprachtml.Extensions;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Rules
{
    public class ElementMustHaveAnyAttribute : IConventionRule
    {
        private readonly HtmlNodeType? _nodeType;
        private readonly string _tagName;
        private readonly string[] _attributes;

        public ElementMustHaveAnyAttribute(HtmlNodeType nodeType, string[] attributes)
        {
            _nodeType = nodeType;
            _attributes = attributes;
        }

        public ElementMustHaveAnyAttribute(string tagName, string[] attributes)
        {
            _tagName = tagName;
            _attributes = attributes;
        }

        public RuleResult Test(ICollection<IHtmlNode> nodes)
        {
            var offendingNodes = nodes.TraverseAll()
                .Where(n=>!_nodeType.HasValue || n.NodeType == _nodeType)
                .Where(n=>_tagName == null || string.Equals((n as HtmlNode)?.TagName, _tagName, StringComparison.InvariantCultureIgnoreCase))
                .Where(
                    n =>
                        !n.Attributes.Any(
                            a => _attributes.Any(rq => rq.Equals(a.Name, StringComparison.InvariantCultureIgnoreCase))))
                .ToArray();
            if (offendingNodes.Any())
                return new RuleResult(false, $"Input failed rule {nameof(ButtonMustSpecifyTypeAttribute)}",
                    offendingNodes);
            return RuleResult.Pass();
        }
    }
}