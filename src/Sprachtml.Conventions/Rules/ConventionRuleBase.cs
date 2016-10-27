using System.Collections.Generic;
using System.Linq;
using Sprachtml.Conventions.Models;
using Sprachtml.Extensions;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Rules
{
    public abstract class ConventionRuleBase : IConventionRule
    {
        public RuleResult Test(ICollection<IHtmlNode> nodes)
        {
            var offendingNodes = nodes.TraverseAll()
                .Where(IsOffending)
                .ToArray();
            if (offendingNodes.Any())
                return new RuleResult(false, $"Input failed rule {GetType().Name}",
                    offendingNodes);
            return RuleResult.Pass();
        }

        protected abstract bool IsOffending(IHtmlNode node);
    }
}