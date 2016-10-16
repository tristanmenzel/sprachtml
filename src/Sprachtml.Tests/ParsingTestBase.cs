using NUnit.Framework;
using Sprachtml.Models;

namespace Sprachtml.Tests
{
    [TestFixture]
    public abstract class ParsingTestBase
    {
        protected abstract string Markup { get; }

        protected IHtmlNode[] GetNodes() => SprachtmlParser.Parse(Markup);
    }
}