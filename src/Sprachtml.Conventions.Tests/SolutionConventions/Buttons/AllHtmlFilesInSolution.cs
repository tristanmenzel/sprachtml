using System;
using System.IO;
using System.Linq;
using Conventional;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Conventions.Rules;

namespace Sprachtml.Conventions.Tests.SolutionConventions.Buttons
{
    [TestFixture]
    public class AllHtmlFilesInSolution
    {
        [Test]
        public void ShouldHaveAnExplicitTypeAttribute()
        {
            var convention = new HtmlConvention(new IConventionRule[] {new ButtonMustSpecifyTypeAttribute()});
            var failureAssertionCount = 0;
            ThisSolution.MustConformTo(convention).WithFailureAssertion(x=>failureAssertionCount++);
            failureAssertionCount.ShouldBe(1, "There should be 1 file which fails the convention");
        }
    }
}