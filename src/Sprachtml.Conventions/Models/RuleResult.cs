using Sprachtml.Models;

namespace Sprachtml.Conventions.Models
{
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
}