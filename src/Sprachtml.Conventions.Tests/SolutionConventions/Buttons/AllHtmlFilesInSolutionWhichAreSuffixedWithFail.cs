using Conventional;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Conventions.Rules;

namespace Sprachtml.Conventions.Tests.SolutionConventions.Buttons
{
    [TestFixture]
    public class AllHtmlFilesInSolutionWhichAreSuffixedWithFail
    {
        [Test]
        public void ShouldFailTheConvention()
        {
            var convention = new HtmlConvention(new IConventionRule[] {new ButtonMustSpecifyTypeAttribute()}, "*.Fail.html")
            {
                SolutionSubdirectory = @"Sprachtml.Conventions.Tests\Html\Buttons\"
            };
            var failureAssertionCount = 0;
            ThisSolution.MustConformTo(convention).WithFailureAssertion(x => failureAssertionCount++);
            failureAssertionCount.ShouldBe(1, "There should be 1 file which fails the convention");
        }
    }
}