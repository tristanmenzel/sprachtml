using System.Collections.Generic;
using Sprachtml.Models;

namespace Sprachtml.Extensions
{
    public static class HtmlNodeExtensions
    {

        public static IEnumerable<IHtmlNode> TraverseAll(this IHtmlNode node)
        {
            yield return node;
            foreach (var child in node.Children)
                yield return child;
        }
    }
}