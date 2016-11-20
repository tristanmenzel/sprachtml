using System;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Exceptions;

namespace Sprachtml.Tests.VoidTags
{
    public class VoidTagsWhichAreNotInTheAllowedList : ParsingTestBase
    {

        protected override string Markup => "<ul><li>Nope<li>Not happening</ul>";

        [Test]
        public void ShouldFail()
        {
            Action go = ()=>GetNodes();
            go.ShouldThrow<SprachtmlParseException>();
        }


    }
}