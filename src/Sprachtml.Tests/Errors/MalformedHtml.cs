
using System;
using NUnit.Framework;
using Shouldly;
using Sprachtml.Exceptions;

namespace Sprachtml.Tests.Errors
{

    [TestFixture]
    public class TagsWithoutClosingTag
    {
        [Test]
        public void ShouldNotParse()
        {
            Action parseHtml = () => SprachtmlParser.Parse("<span>");

            parseHtml.ShouldThrow<SprachtmlParseException>();
        }

    }
}