using NUnit.Framework;
using Shouldly;

namespace Sprachtml.Conventions.Tests.Rules.ButtonMustSpecifyTypeAttributeScenarios
{
    public class GivenNoButtons: ScenarioBase
    {
        protected override string Html => @"

";


        [Test]
        public void TheConventionShouldPass()
        {
            var result = Sut.Test(GetParsed());

            result.Passed.ShouldBe(true);
        }
    }
}