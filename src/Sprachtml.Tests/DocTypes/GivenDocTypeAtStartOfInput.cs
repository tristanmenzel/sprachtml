using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.DocTypes
{
    public class GivenDocTypeAtStartOfInput:ParsingTestBase
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

            parsed.Length.ShouldBe(3);
            parsed[0].ShouldBeOfType<DocTypeNode>();
        }
    }


    public class GivvenADocTypeWithProperties : ParsingTestBase
    {
        protected override string Markup => @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"">
                                              <html>
                                                <head></head>
                                                <body></body>
                                              </html>";
        [Test]
        public void ParsedDocTypeShouldIncludeProperties()
        {
            var parsed = GetNodes();

            var docType = (DocTypeNode)parsed[0];
            docType.Properties.Length.ShouldBe(3);

            docType.Properties[0].Text.ShouldBe("html");
            docType.Properties[0].QuoteType.ShouldBe(QuoteType.None);
            docType.Properties[1].Text.ShouldBe("PUBLIC");
            docType.Properties[1].QuoteType.ShouldBe(QuoteType.None);
            docType.Properties[2].Text.ShouldBe("-//W3C//DTD XHTML 1.0 Transitional//EN");
            docType.Properties[2].QuoteType.ShouldBe(QuoteType.Double);
        }
    }
}