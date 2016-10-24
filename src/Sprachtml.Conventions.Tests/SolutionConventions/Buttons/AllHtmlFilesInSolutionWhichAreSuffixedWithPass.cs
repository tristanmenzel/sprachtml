using Conventional;
using NUnit.Framework;
using Sprachtml.Conventions.Rules;

namespace Sprachtml.Conventions.Tests.SolutionConventions.Buttons
{
    [TestFixture]
    public class AllHtmlFilesInSolutionWhichAreSuffixedWithPass
    {
        [Test]
        public void ShouldPassTheConvention()
        {
            var convention = new HtmlConvention(new IConventionRule[] {new ButtonMustSpecifyTypeAttribute()}, "*.Pass.html")
            {
                SolutionSubdirectory = @"Sprachtml.Conventions.Tests\Html\Buttons\"
            };
            ThisSolution.MustConformTo(convention).WithFailureAssertion(Assert.Fail);
        }
    }
}