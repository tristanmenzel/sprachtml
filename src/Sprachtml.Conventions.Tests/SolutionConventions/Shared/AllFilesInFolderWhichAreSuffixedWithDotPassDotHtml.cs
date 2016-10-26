using Conventional;
using NUnit.Framework;
using Sprachtml.Conventions.Rules;

namespace Sprachtml.Conventions.Tests.SolutionConventions.Shared
{
    public abstract class AllFilesInFolderWhichAreSuffixedWithDotPassDotHtml<TConventionRule>
        : AllFilesInFolderBase<TConventionRule>
        where TConventionRule : IConventionRule
    {

        protected override string FileMask => "*.Pass.html";

        [Test]
        public void ShouldPassTheConvention()
        {
            var convention = new HtmlConvention(new IConventionRule[] { GetRuleInstance() }, FileMask)
            {
                SolutionSubdirectory = Subdirectory
            };
            ThisSolution.MustConformTo(convention).WithFailureAssertion(Assert.Fail);
        }
    }
}