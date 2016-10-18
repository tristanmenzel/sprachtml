using System;
using System.IO;
using System.Linq;
using Conventional;
using NUnit.Framework;
using Sprachtml.Conventions.Rules;

namespace Sprachtml.Conventions.Tests.SolutionConventions.Buttons
{
    [TestFixture]
    public class AllHtmlFilesInSolution
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Note: This is only required until my PR for conventional is merged.
            KnownPaths.SolutionRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @"..\..\..\"));
        }

        [Test]
        public void ShouldHaveAnExplicitTypeAttribute()
        {
            var convention = new HtmlConvention(new[] {new ButtonMustSpecifyTypeAttribute()});
            ThisSolution.MustConformTo(convention).WithFailureAssertion(Assert.Fail);
        }
    }
}