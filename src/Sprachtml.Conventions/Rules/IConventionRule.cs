using System.Collections.Generic;
using Sprachtml.Conventions.Models;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Rules
{
    public interface IConventionRule
    {
        RuleResult Test(ICollection<IHtmlNode> nodes);
    }
}