using System.Diagnostics.Contracts;

namespace Sprachtml.Conventions.Extensions
{
    public static class HtmlConventionExtensions
    {
        [Pure]
        public static HtmlConvention WithSolutionSubdirectory(this HtmlConvention convention, string solutionSubdirectory)
        {
            return new HtmlConvention(convention.Rules, convention.SearchPattern, convention.ExcludedFiles)
            {
                SolutionSubdirectory = solutionSubdirectory
            };
        }
    }
}