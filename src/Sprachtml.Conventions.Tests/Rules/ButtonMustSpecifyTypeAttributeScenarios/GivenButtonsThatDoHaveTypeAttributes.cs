using NUnit.Framework;
using Shouldly;

namespace Sprachtml.Conventions.Tests.Rules.ButtonMustSpecifyTypeAttributeScenarios
{
    public class GivenButtonsThatDoHaveTypeAttributes: ScenarioBase
    {
        protected override string Html => @"
<form>
<fieldset>
<legend>Test form </legend>
<label>Things <input type=text /></label>
<button type=submit>Submit</button>
<button type=""reset"">Reset</button>
<button type='button'>go do something else</button>
</fieldset>
</form>
";


        [Test]
        public void TheConventionShouldPass()
        {
            var result = Sut.Test(GetParsed());

            result.Passed.ShouldBe(true);



        }
    }
}