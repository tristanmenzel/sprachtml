using System.Linq;
using Conventional;
using NUnit.Framework;
using Sprachtml.Conventions.Rules;

namespace Sprachtml.Conventions.Tests.SolutionConventions.Buttons
{
    [TestFixture]
    public class AllHtmlFilesInSolution
    {
        [Test]
        public void ShouldHaveAnExplicitTypeAttribute()
        {
            // TODO: sort out why 'ThisSolution' is not working 
            var convention = new HtmlConvention(new[] {new ButtonMustSpecifyTypeAttribute()});
            ThisSolution.MustConformTo(convention).WithFailureAssertion(Assert.Fail);
        }
    }
}