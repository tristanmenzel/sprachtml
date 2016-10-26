using NUnit.Framework;
using Sprachtml.Conventions.Rules;
using Sprachtml.Conventions.Tests.SolutionConventions.Shared;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Tests.SolutionConventions.MustHaveAnyAttributeTests
{
    [TestFixture]
    public class AllFilesInFolderWhichAreSuffixedWithDotFailDotHtml : AllFilesInFolderWhichAreSuffixedWithDotFailDotHtml<ElementMustHaveAnyAttribute>
    {
        protected override ElementMustHaveAnyAttribute GetRuleInstance()=> new ElementMustHaveAnyAttribute(HtmlNodeType.A, new [] {"href", "ng-href"});
    }
}