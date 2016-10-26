using NUnit.Framework;
using Sprachtml.Conventions.Rules;
using Sprachtml.Conventions.Tests.SolutionConventions.Shared;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Tests.SolutionConventions.ElementMustHaveAtLeastOneOfAttributeTests
{
    [TestFixture]
    public class AllFilesInFolderWhichAreSuffixedWithDotFailDotHtml : AllFilesInFolderWhichAreSuffixedWithDotFailDotHtml<ElementMustHaveAtLeastOneOfAttribute>
    {
        protected override ElementMustHaveAtLeastOneOfAttribute GetRuleInstance()=> new ElementMustHaveAtLeastOneOfAttribute(HtmlNodeType.A, new [] {"href", "ng-href"});
    }
}