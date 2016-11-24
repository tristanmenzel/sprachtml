using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.DocTypes
{
    public class GivenDocTypeAtStartOfInput : ParsingTestBase
    {
        protected override string Markup => @"<!DOCTYPE>
                                              <html>
                                                <head></head>
                                                <body></body>
                                              </html>";
        [Test]
        public void ShouldParseAsDocTypeNode()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(2);
            parsed[0].ShouldBeOfType<DocTypeNode>();
        }
    }
}