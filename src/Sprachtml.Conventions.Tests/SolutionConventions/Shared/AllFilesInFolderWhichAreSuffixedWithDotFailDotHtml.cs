using Conventional;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Conventions.Rules;

namespace Sprachtml.Conventions.Tests.SolutionConventions.Shared
{
    public abstract class AllFilesInFolderWhichAreSuffixedWithDotFailDotHtml<TConventionRule>
        : AllFilesInFolderBase<TConventionRule>
        where TConventionRule : IConventionRule
    {

        protected override string FileMask => "*.Fail.html";

        [Test]
        public void ShouldFailTheConvention()
        {
            var convention = new HtmlConvention(new IConventionRule[] { GetRuleInstance() }, FileMask)
            {
                SolutionSubdirectory = Subdirectory
            };

            var totalFiles = GetFileCount();
            var result = ThisSolution.MustConformTo(convention);
            result.Failures.Length.ShouldBe(totalFiles);
        }
    }
}