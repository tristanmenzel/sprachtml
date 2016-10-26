using NUnit.Framework;
using Sprachtml.Conventions.Rules;
using Sprachtml.Conventions.Tests.SolutionConventions.Shared;

namespace Sprachtml.Conventions.Tests.SolutionConventions.ButtonMustSpecifyTypeAttributeTests
{
    [TestFixture]
    public class AllHtmlFilesInSolutionWhichAreSuffixedWithPass
        : AllFilesInFolderWhichAreSuffixedWithDotPassDotHtml<ButtonMustSpecifyTypeAttribute>
    {
    }
}