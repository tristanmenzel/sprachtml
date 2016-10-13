using NUnit.Framework;
using Shouldly;

namespace Sprachtml.Conventions.Tests.Rules.ButtonMustSpecifyTypeAttributeScenarios
{
    public class GivenButtonsThatDoNotHaveTypeAttributes: ScenarioBase
    {
        protected override string Html => @"
<form>
<fieldset>
<legend>Test form </legend>
<label>Things <input type=text /></label>
<button >Submit</button>
<button >Reset</button>
<button >go do something else</button>
</fieldset>
</form>
";




        [Test]
        public void TheConventionShouldFail()
        {
            var result = Sut.Test(GetParsed());

            result.Passed.ShouldBe(false);
        }

        [Test]
        public void ThereShouldBeThreeOffendingNodes()
        {
            var result = Sut.Test(GetParsed());
            result.OffendingNodes.Length.ShouldBe(3);
        }
    }
}