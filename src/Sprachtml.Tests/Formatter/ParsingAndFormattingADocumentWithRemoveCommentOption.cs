using BlurkCompare;
using NUnit.Framework;
using Sprachtml.Formatters;

namespace Sprachtml.Tests.Formatter
{
    [TestFixture]
    public class ParsingAndFormattingADocumentWithRemoveCommentOption

    {
        [Test]
        public void ShouldProduceTheSameOutputAsInputMinusTheComment()
        {
            var input = @"<!DOCTYPE html>
<html>
 <head>
  <title>Testing 123</title>
  <script type=""text/javascript"">alert('boo');</script>
  <style type=""text/css"">h1 { color: red; }</style>
 </head>
 <body>
  <!-- Start of body ROFL -->
  <h1>Hello world</h1>
  <p>What a great paragraph</p>
 </body>
</html>";
            var expected = @"<!DOCTYPE html>
<html>
 <head>
  <title>Testing 123</title>
  <script type=""text/javascript"">alert('boo');</script>
  <style type=""text/css"">h1 { color: red; }</style>
 </head>
 <body>
  
  <h1>Hello world</h1>
  <p>What a great paragraph</p>
 </body>
</html>";
            var parsed = SprachtmlParser.Parse(input);
            var str = HtmlFormatter.WriteAsString(parsed, HtmlFormatterOptions.RemoveComments);
            Blurk.Compare(expected).To(str).AssertAreTheSame(Assert.Fail);
        }
    }
}