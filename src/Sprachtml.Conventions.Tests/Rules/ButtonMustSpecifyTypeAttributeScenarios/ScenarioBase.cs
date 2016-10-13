using NUnit.Framework;
using Sprachtml.Conventions.Rules;
using Sprachtml.Models;

namespace Sprachtml.Conventions.Tests.Rules.ButtonMustSpecifyTypeAttributeScenarios
{
    [TestFixture]
    public abstract class ScenarioBase
    {
        protected ButtonMustSpecifyTypeAttribute Sut=> new ButtonMustSpecifyTypeAttribute();

        protected abstract string Html { get; }

        protected IHtmlNode[] GetParsed() => SprachtmlParser.Parse(Html);
    }
}